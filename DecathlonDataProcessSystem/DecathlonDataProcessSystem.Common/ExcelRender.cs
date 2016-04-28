using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;


namespace DecathlonDataProcessSystem.Common
{
    /// <summary>
    /// 使用NPOI操作Excel，无需Office COM组件
    /// Created By 囧月 http://lwme.cnblogs.com
    /// 部分代码取自http://msdn.microsoft.com/zh-tw/ee818993.aspx
    /// NPOI是POI的.NET移植版本，目前稳定版本中仅支持对xls文件（Excel 97-2003）文件格式的读写
    /// NPOI官方网站http://npoi.codeplex.com/
    /// </summary>
    public class ExcelRender
    {
        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue( ICell cell )
        {
            if ( cell == null )
                return string.Empty;
            switch ( cell.CellType )
            {
                case CellType.BLANK:
                    return string.Empty;
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue.ToString( ).Trim();
                case CellType.ERROR:
                    return cell.ErrorCellValue.ToString( ).Trim( );
                case CellType.NUMERIC:
                    return cell.NumericCellValue.ToString( ).Trim( );                        
                case CellType.Unknown:
                default:
                    return cell.ToString( ).Trim( );//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.STRING:
                    return cell.StringCellValue.Trim( );
                case CellType.FORMULA:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator( cell.Sheet.Workbook );
                        e.EvaluateInCell( cell );
                        return cell.ToString( ).Trim( );
                    }
                    catch
                    {
                        return cell.StringCellValue.Trim( );
                        //return cell.NumericCellValue.ToString( );
                    }
            }
        }

        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns( ISheet sheet )
        {
            if ( sheet.PhysicalNumberOfRows > 0 )
            {
                IRow headerRow = sheet.GetRow( 0 );

                for ( int i = 0 , l = headerRow.LastCellNum ; i < l ; i++ )
                {
                    sheet.AutoSizeColumn( i );
                }
            }
        }

        /// <summary>
        /// 保存Excel文档流到文件
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="fileName">文件名</param>
        private static void SaveToFile( MemoryStream ms , string fileName )
        {
            using ( FileStream fs = new FileStream( fileName , FileMode.Create , FileAccess.Write ) )
            {
                byte[] data = ms.ToArray( );

                fs.Write( data , 0 , data.Length );
                fs.Flush( );

                data = null;
            }
        }

        /// <summary>
        /// 输出文件到浏览器
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="fileName">文件名</param>
        private static void RenderToBrowser( MemoryStream ms , HttpContext context , string fileName )
        {
            if ( context.Request.Browser.Browser == "IE" )
                fileName = HttpUtility.UrlEncode( fileName );
            context.Response.AddHeader( "Content-Disposition" , "attachment;fileName=" + fileName );
            context.Response.BinaryWrite( ms.ToArray( ) );
        }

        /// <summary>
        /// DataReader转换成Excel文档流
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel( IDataReader reader )
        {
            MemoryStream ms = new MemoryStream( );

            using ( reader )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( ) )
                {
                    using ( ISheet sheet = workbook.CreateSheet( ) )
                    {
                        IRow headerRow = sheet.CreateRow( 0 );
                        int cellCount = reader.FieldCount;

                        // handling header.
                        for ( int i = 0 ; i < cellCount ; i++ )
                        {
                            headerRow.CreateCell( i ).SetCellValue( reader.GetName( i ) );
                        }

                        // handling value.
                        int rowIndex = 1;
                        while ( reader.Read( ) )
                        {
                            IRow dataRow = sheet.CreateRow( rowIndex );

                            for ( int i = 0 ; i < cellCount ; i++ )
                            {
                                dataRow.CreateCell( i ).SetCellValue( reader[i].ToString( ) );
                            }

                            rowIndex++;
                        }

                        AutoSizeColumns( sheet );

                        workbook.Write( ms );
                        ms.Flush( );
                        ms.Position = 0;
                    }
                }
            }
            return ms;
        }

        /// <summary>
        /// DataReader转换成Excel文档流，并保存到文件
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="fileName">保存的路径</param>
        public static void RenderToExcel( IDataReader reader , string fileName )
        {
            using ( MemoryStream ms = RenderToExcel( reader ) )
            {
                SaveToFile( ms , fileName );
            }
        }

        /// <summary>
        /// DataReader转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="context">HTTP上下文</param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel( IDataReader reader , HttpContext context , string fileName )
        {
            using ( MemoryStream ms = RenderToExcel( reader ) )
            {
                RenderToBrowser( ms , context , fileName );
            }
        }

        /// <summary>
        /// DataTable转换成Excel文档流
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel( DataTable table )
        {
            MemoryStream ms = new MemoryStream( );

            using ( table )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( ) )
                {
                    using ( ISheet sheet = workbook.CreateSheet( ) )
                    {
                        IRow headerRow = sheet.CreateRow( 0 );

                        // handling header.
                        foreach ( DataColumn column in table.Columns )
                            headerRow.CreateCell( column.Ordinal ).SetCellValue( column.Caption );//If Caption not set, returns the ColumnName value

                        // handling value.
                        int rowIndex = 1;

                        foreach ( DataRow row in table.Rows )
                        {
                            IRow dataRow = sheet.CreateRow( rowIndex );

                            foreach ( DataColumn column in table.Columns )
                            {
                                dataRow.CreateCell( column.Ordinal ).SetCellValue( row[column].ToString( ) );
                            }

                            rowIndex++;
                        }
                        AutoSizeColumns( sheet );

                        workbook.Write( ms );
                        ms.Flush( );
                        ms.Position = 0;
                    }
                }
            }
            return ms;
        }

        /// <summary>    
        /// 由DataSet导出Excel    
        /// </summary>    
        /// <param name="sourceTable">要导出数据的DataTable</param>    
        /// <param name="sheetName">工作表名称</param>    
        /// <returns>Excel工作表</returns>    
        private static MemoryStream ExportDataSetToExcel( DataSet sourceDs , string sheetName )
        {
            HSSFWorkbook workbook = new HSSFWorkbook( );
            MemoryStream ms = new MemoryStream( );
            string[] sheetNames = sheetName.Split( ',' );
            for ( int i = 0 ; i< sheetNames.Length ; i++ )
            {
                ISheet sheet = workbook.CreateSheet( sheetNames[i] );
                IRow headerRow = sheet.CreateRow( 0 );
                // handling header.            
                foreach ( DataColumn column in sourceDs.Tables[i].Columns )
                    headerRow.CreateCell( column.Ordinal ).SetCellValue( column.ColumnName );
                // handling value.            
                int rowIndex = 1;
                foreach ( DataRow row in sourceDs.Tables[i].Rows )
                {
                    IRow dataRow = sheet.CreateRow( rowIndex );
                    foreach ( DataColumn column in sourceDs.Tables[i].Columns )
                    {
                        dataRow.CreateCell( column.Ordinal ).SetCellValue( row[column].ToString( ) );
                    }
                    rowIndex++;
                }
            }
            workbook.Write( ms );
            ms.Flush( );
            ms.Position = 0;
            workbook = null;
            return ms;
        }

        /// <summary>
        /// DataTable转换成Excel文档流，并保存到文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName">保存的路径</param>
        public static void RenderToExcel( DataTable table , string fileName )
        {
            using ( MemoryStream ms = RenderToExcel( table ) )
            {
                SaveToFile( ms , fileName );
            }
        }
        /// <summary>
        /// DataSet转换成Excel文档流，并保存到文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fileName">保存的路径</param>
        public static void RenderToExcel( DataSet ds , string sheetName , string fileName )
        {
            using ( MemoryStream ms = ExportDataSetToExcel( ds,sheetName ) )
            {
                SaveToFile( ms , fileName );
            }
        }
        /// <summary>
        /// DataTable转换成Excel文档流，并输出到客户端
        /// </summary>
        /// <param name="table"></param>
        /// <param name="response"></param>
        /// <param name="fileName">输出的文件名</param>
        public static void RenderToExcel( DataTable table , HttpContext context , string fileName )
        {
            using ( MemoryStream ms = RenderToExcel( table ) )
            {
                RenderToBrowser( ms , context , fileName );
            }
        }

        /// <summary>
        /// Excel文档流是否有数据
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <returns></returns>
        public static bool HasData( Stream excelFileStream )
        {
            return HasData( excelFileStream , 0 );
        }

        /// <summary>
        /// Excel文档流是否有数据
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <returns></returns>
        public static bool HasData( Stream excelFileStream , int sheetIndex )
        {
            using ( excelFileStream )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( excelFileStream ) )
                {
                    if ( workbook.NumberOfSheets > 0 )
                    {
                        if ( sheetIndex < workbook.NumberOfSheets )
                        {
                            using ( ISheet sheet = workbook.GetSheetAt( sheetIndex ) )
                            {
                                return sheet.PhysicalNumberOfRows > 0;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetName">表名称</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel( Stream excelFileStream , string sheetName )
        {
            return RenderFromExcel( excelFileStream , sheetName , 0 );
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetName">表名称</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel( Stream excelFileStream , string sheetName , int headerRowIndex )
        {
            DataTable table = null;

            using ( excelFileStream )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( excelFileStream ) )
                {
                    using ( ISheet sheet = workbook.GetSheet( sheetName ) )
                    {
                        table = RenderFromExcel( sheet , headerRowIndex );
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 默认转换Excel的第一个表
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel( Stream excelFileStream )
        {
            return RenderFromExcel( excelFileStream , 0 , 0 );
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel( Stream excelFileStream , int sheetIndex )
        {
            return RenderFromExcel( excelFileStream , sheetIndex , 0 );
        }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel( Stream excelFileStream , int sheetIndex , int headerRowIndex )
        {
            DataTable table = null;

            using ( excelFileStream )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( excelFileStream ) )
                {
                    using ( ISheet sheet = workbook.GetSheetAt( sheetIndex ) )
                    {
                        table = RenderFromExcel( sheet , headerRowIndex );
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// Excel表格转换成DataTable
        /// </summary>
        /// <param name="sheet">表格</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        private static DataTable RenderFromExcel( ISheet sheet , int headerRowIndex )
        {
            DataTable table = new DataTable( );

            IRow headerRow = sheet.GetRow( headerRowIndex );
            int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            //handling header.
            for ( int i = headerRow.FirstCellNum ; i < cellCount ; i++ )
            {
                DataColumn column = new DataColumn( headerRow.GetCell( i ).StringCellValue.Trim().Replace(' ','_') );
                table.Columns.Add( column );
            }

            for ( int i = ( sheet.FirstRowNum + 1 ) ; i <= rowCount ; i++ )
            {
                IRow row = sheet.GetRow( i );
                DataRow dataRow = table.NewRow( );

                if ( row != null )
                {
                    for ( int j = row.FirstCellNum ; j < cellCount ; j++ )
                    {
                        if ( row.GetCell( j ) != null )
                            dataRow[j] = GetCellValue( row.GetCell( j ) );
                    }
                }

                table.Rows.Add( dataRow );
            }

            return table;
        }

        /// <summary>
        /// Excel文档导入到数据库
        /// 默认取Excel的第一个表
        /// 第一行必须为标题行
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="insertSql">插入语句</param>
        /// <param name="dbAction">更新到数据库的方法</param>
        /// <returns></returns>
        public static int RenderToDb( Stream excelFileStream , string insertSql , DBAction dbAction )
        {
            return RenderToDb( excelFileStream , insertSql , dbAction , 0 , 0 );
        }

        public delegate int DBAction( string sql , params IDataParameter[] parameters );

        /// <summary>
        /// Excel文档导入到数据库
        /// </summary>
        /// <param name="excelFileStream">Excel文档流</param>
        /// <param name="insertSql">插入语句</param>
        /// <param name="dbAction">更新到数据库的方法</param>
        /// <param name="sheetIndex">表索引号，如第一个表为0</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        public static int RenderToDb( Stream excelFileStream , string insertSql , DBAction dbAction , int sheetIndex , int headerRowIndex )
        {
            int rowAffected = 0;
            using ( excelFileStream )
            {
                using ( IWorkbook workbook = new HSSFWorkbook( excelFileStream ) )
                {
                    using ( ISheet sheet = workbook.GetSheetAt( sheetIndex ) )
                    {
                        StringBuilder builder = new StringBuilder( );

                        IRow headerRow = sheet.GetRow( headerRowIndex );
                        int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
                        int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

                        for ( int i = ( sheet.FirstRowNum + 1 ) ; i <= rowCount ; i++ )
                        {
                            IRow row = sheet.GetRow( i );
                            if ( row != null )
                            {
                                builder.Append( insertSql );
                                builder.Append( " values (" );
                                for ( int j = row.FirstCellNum ; j < cellCount ; j++ )
                                {
                                    builder.AppendFormat( "'{0}'," , GetCellValue( row.GetCell( j ) ).Replace( "'" , "''" ) );
                                }
                                builder.Length = builder.Length - 1;
                                builder.Append( ");" );
                            }

                            if ( ( i % 50 == 0 || i == rowCount ) && builder.Length > 0 )
                            {
                                //每50条记录一次批量插入到数据库
                                rowAffected += dbAction( builder.ToString( ) );
                                builder.Length = 0;
                            }
                        }
                    }
                }
            }
            return rowAffected;
        }
        #region 
        /// <summary>
        /// NPOI DataGridView 导出 EXCEL
        /// </summary>
        /// <param name="fileName"> 默认保存文件名</param>
        /// <param name="dgv">DataGridView</param>
        /// <param name="fontname">字体名称</param>
        /// <param name="fontsize">字体大小</param>
        public static void ExportExcel( DataGridView dgv , string fontname , short fontsize )
        {
            //检测是否有数据
            if (dgv.Rows.Count == 0) return;
            //创建主要对象
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Weight");
            //设置字体，大小，对齐方式
            HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontName = fontname;
            font.FontHeightInPoints = fontsize;
            style.SetFont(font);
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER; //居中对齐
            //添加表头
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if ( dgv.Columns[i].Visible )
                {
                    dataRow.CreateCell( i ).SetCellValue( dgv.Columns[i].HeaderText );
                    dataRow.GetCell( i ).CellStyle = style;
                }
            }
            //注释的这行是设置筛选的
            //sheet.SetAutoFilter(new CellRangeAddress(0, dgv.Columns.Count, 0, dgv.Columns.Count));
            //添加列及内容
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dataRow = (HSSFRow)sheet.CreateRow(i + 1);
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if ( dgv.Columns[j].Visible )
                    {
                        string ValueType = dgv.Rows[i].Cells[j].Value.GetType( ).ToString( );
                        string Value = dgv.Rows[i].Cells[j].Value.ToString( );
                        switch ( ValueType )
                        {
                            case "System.String"://字符串类型
                                dataRow.CreateCell( j ).SetCellValue( Value );
                                break;
                            case "System.DateTime"://日期类型
                                System.DateTime dateV;
                                System.DateTime.TryParse( Value , out dateV );
                                dataRow.CreateCell( j ).SetCellValue( dateV );
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse( Value , out boolV );
                                dataRow.CreateCell( j ).SetCellValue( boolV );
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse( Value , out intV );
                                dataRow.CreateCell( j ).SetCellValue( intV );
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse( Value , out doubV );
                                dataRow.CreateCell( j ).SetCellValue( doubV );
                                break;
                            case "System.DBNull"://空值处理
                                dataRow.CreateCell( j ).SetCellValue( "" );
                                break;
                            default:
                                dataRow.CreateCell( j ).SetCellValue( "" );
                                break;
                        }
                        dataRow.GetCell( j ).CellStyle = style;
                        //设置宽度
                        sheet.SetColumnWidth( j , ( Value.Length + 10 ) * 256 );
                    }
                    else
                        sheet.SetColumnHidden( j , true);

                }
            }
            //保存文件
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            MemoryStream ms = new MemoryStream();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                if (!CheckFiles(saveFileName))
                {
                    MessageBox.Show("文件被站用，请关闭文件 " + saveFileName);
                    workbook = null;
                    ms.Close();
                    ms.Dispose();
                    return;
                }
                workbook.Write(ms);
                FileStream file = new FileStream(saveFileName, FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook = null;
                ms.Close();
                ms.Dispose();
                MessageBox.Show( saveDialog.FileName + " 保存成功" , "提示" , MessageBoxButtons.OK );
            }
            else
            {
                workbook = null;
                ms.Close();
                ms.Dispose();
            }
        }
        #endregion

        #region 检测文件被占用 
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        /// <summary>
        /// 检测文件被占用 
        /// </summary>
        /// <param name="FileNames">要检测的文件路径</param>
        /// <returns></returns>
        public static bool CheckFiles(string FileNames)
        {
            if (!File.Exists(FileNames))
            {
                //文件不存在
                return true;
            }
            IntPtr vHandle = _lopen(FileNames, OF_READWRITE | OF_SHARE_DENY_NONE);
            if ( vHandle == HFILE_ERROR )
            {
                //文件被占用
                return false;
            }
            //文件没被占用
            CloseHandle(vHandle);
            return true;
        }
        #endregion
    }
}
