using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DecathlonDataProcessSystem.BLL;

namespace DecathlonDataProcessSystem.App
{
    public partial class FrmTest : Form
    {
        public FrmTest( )
        {
            InitializeComponent( );
            this.dataGridView1.DataSource=null;
            this.dataGridView1.DataSource=new OriginalCLPBLL( ).GetAllList( ).Tables[0].DefaultView;
        }
    }
    public enum OperateType
    {
        CLPDataInput= 0 ,
        CLPDataColumnDelete = 1 ,
        BasicDataAdd = 2 ,
        DorbleOrSet = 3 ,
        FilterSupervisionCondition = 4 ,
        TotalCLPHandle = 5 ,
        Space = 6 ,
    }

    public partial class FrmMain : Form
    {
        private Form _CurrentMdiChild;
        public OperateType operateType;
        private DataTable _OriginalCLPTable;
        public DataTable OriginalCLPTable
        {
            get { return _OriginalCLPTable; }
            set { _OriginalCLPTable=value; }
        }
        private DataTable _CLPTable;
        public DataTable CLPTable
        {
            get { return _CLPTable; }
            set { _CLPTable=value; }
        }
        private DataTable _UnionCLPTable= new DataTable( );
        public DataTable UnionCLPTable
        {
            get { return _UnionCLPTable; }
            set { _UnionCLPTable=value; }
        }
        private DataTable _DoubleOrSetTable=new DataTable( );
        public DataTable DoubleOrSetTable
        {
            get { return _DoubleOrSetTable; }
            set { _DoubleOrSetTable=value; }
        }
        private DataTable _BasicDataTable;
        public DataTable BasicDataTable
        {
            get { return _BasicDataTable; }
            set { _BasicDataTable =value; }
        }
        public DataSet _ParcelNumberDataSet=new DataSet( );
        public DataSet ParcelNumberDataSet
        {
            get { return _ParcelNumberDataSet; }
            set { _ParcelNumberDataSet=value; }
        }
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName=value; }
        }
        private readonly OriginalCLPBLL bll=new OriginalCLPBLL( );
        private readonly BasicDataBLL basicDataBLL=new BasicDataBLL( );
        public FrmMain( )
        {
            InitializeComponent( );
            CreateUnionDataTableSchema( );
        }
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
        }
        private void SetSubForm( string className )
        {
            try
            {
                string assemblyName = Assembly.GetExecutingAssembly( ).GetName( ).Name;
                className = assemblyName + "." + className;
                Type t = Type.GetType( className );
                if ( t != null )
                {
                    if ( this.operateType==OperateType.CLPDataInput )
                    {
                        object[] args = new object[] { "原始CLP制作【第一步：导入原始资料】" , OperateType.CLPDataInput , OriginalCLPTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.CLPDataColumnDelete )
                    {
                        object[] args = new object[] { "原始CLP制作【第二步：删除多余列】" , OperateType.CLPDataColumnDelete , OriginalCLPTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.BasicDataAdd )
                    {
                        object[] args = new object[] { "原始CLP制作【第三步：添加基础数据】" , OperateType.BasicDataAdd , UnionCLPTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.DorbleOrSet )
                    {
                        object[] args = new object[] { "原始CLP制作【第四步：Double或Set操作】" , OperateType.DorbleOrSet , DoubleOrSetTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.FilterSupervisionCondition )
                    {
                        object[] args = new object[] { "原始CLP制作【第五步：掏箱与不出运】" , OperateType.FilterSupervisionCondition , ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.TotalCLPHandle )
                    {
                        object[] args = new object[] { "汇总CLP制作【第一步：处理【CLP文件导入】" , OperateType.FilterSupervisionCondition , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    //if ( className.Equals( "DecathlonDataProcessSystem.App.FrmBasicData" ) )
                    //{
                    //    Form frm = (Form)Activator.CreateInstance( t , BasicDataTable );
                    //    ShowMdiChild( frm );
                    //}
                    //else if ( className.Equals( "DecathlonDataProcessSystem.App.FrmCLPDataInput" )&&this.operateType==OperateType.BasicDataAdd )
                    //{
                    //    Form frm = (Form)Activator.CreateInstance( t , UnionCLPTable );
                    //    ShowMdiChild( frm );
                    //}
                    //else if ( className.Equals( "DecathlonDataProcessSystem.App.FrmCLPDataInput" )&&this.operateType==OperateType.DorbleOrSet )
                    //{
                    //    Form frm = (Form)Activator.CreateInstance( t , DoubleOrSetTable );
                    //    ShowMdiChild( frm );
                    //}
                    //else if ( className.Equals( "DecathlonDataProcessSystem.App.FrmMulSheet" )&&this.operateType==OperateType.FilterSupervisionCondition)
                    //{
                    //    Form frm = (Form)Activator.CreateInstance( t , ParcelNumberDataSet );
                    //    ShowMdiChild( frm );
                    //}
                    //else
                    //{
                    //    object[] args = new object[] { "原始CLP制作【第一步：导入原始资料】",OperateType.CLPDataInput , OriginalCLPTable };

                    //    //object obj = Activator.CreateInstance( typeofControl , true , System.Reflection.BindingFlags.Default , null , args , null , null );            
                    //    Form frm = (Form)Activator.CreateInstance( t,args);
                    //    ShowMdiChild( frm );
                    //}
                }
            }
            catch ( Exception err )
            {
                MessageBox.Show( err.Message );
                return;
            }
        }
        private void ShowMdiChild( Form mdiForm )
        {
            if ( this._CurrentMdiChild != null )
            {
                this._CurrentMdiChild.Close( ); //关闭当前窗体
            }
            this._CurrentMdiChild = mdiForm; //本窗体设置成为当前窗体
            mdiForm.MdiParent = this;
            mdiForm.Show( );
        }
        private void btnOriginalCLPInput_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.CLPDataInput;
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog( );
            if ( fd.ShowDialog( ) == DialogResult.OK )
            {
                if ( !ExcelRender.CheckFiles( fd.FileName ) )
                {
                    MessageBox.Show( "当前需要导入的Excel文件正在被占用，请关闭该文件后重新导入！" );
                    return;
                }
                using ( FileStream fs = new FileStream( fd.FileName , FileMode.Open , FileAccess.Read ) )
                {
                    //把文件读取到字节数组
                    byte[] data = new byte[fs.Length];
                    fs.Read( data , 0 , data.Length );
                    fs.Close( );
                    MemoryStream ms = new MemoryStream( data );
                    _OriginalCLPTable=ExcelRender.RenderFromExcel( ms , 0 , 0 );
                }
                _fileName=fd.FileName.Substring( fd.FileName.LastIndexOf( '\\' )+1 );
                _OriginalCLPTable.TableName=_fileName;
                _OriginalCLPTable.AcceptChanges( );
                DataTable tempDT=new DataTable( );
                tempDT=_OriginalCLPTable.Copy( );
                tempDT.Columns.Add( "FileName" , typeof( String ) );
                tempDT.Columns.Add( "CreateTime" , typeof( DateTime ) );
                tempDT.Columns.Add( "Creator" , typeof( String ) );
                foreach ( DataRow row in tempDT.Rows )
                {
                    row["FileName"]=FileName;
                    row["CreateTime"]=DateTime.Now;
                    row["Creator"]="admin";
                }
                bll.BulkOriginalCLPInsert( tempDT , 1000 );
                SetSubForm( "FrmCLPDataInput" );
                toolStripStatusLabel1.Text="原始CLP成功导入！";
                //MessageBox.Show( "原始CLP成功导入！" );

            }
        }

        public bool ExcuteBasicDataInput( DataTable excelDT , string fileName , ref string errMsg )
        {
            //basicDataBLL.BulkBasicDataInsert( excelDT , 1000 );
            foreach ( DataRow row in excelDT.Rows )
            {
                BasicDataEntity model=new BasicDataEntity( );
                model.ModelCode=row["Model code"].ToString( );
                model.QuanitityUnit=row["法定计量单位"].ToString( );
                model.HSCode=row["HS CODE (IN CAT)"].ToString( );
                model.LocalProdectName=row["中文品名"].ToString( );
                model.EnglishProductName =row["英文品名"].ToString( );
                model.SentialFactor=row["申报要素"].ToString( );
                model.ModelCode=row["MODEL CODE"].ToString( );
                model.SupervisionCondition =row["监管条件"].ToString( );
                model.DoubleOrSet=row["Double or Set"].ToString( );
                model.ExaminingReport=row["检测报告号"].ToString( );
                model.ModelCodeDescription="";
                model.LocalComposition="";
                if ( !basicDataBLL.ModelCodeExists( row["Model code"].ToString( ) ) )
                    basicDataBLL.Add( model );
                else
                    basicDataBLL.Update( model );
            }
            if ( errMsg!="" )
                return false;
            else
                return true;
        }
        private void btnDeleteColumn_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.CLPDataColumnDelete;
            if ( OriginalCLPTable.Columns.Contains( "ORIGINAL_PO_NUMBER" ) )
            {
                OriginalCLPTable.Columns.Remove( "ORIGINAL_PO_NUMBER" );
            }
            if ( OriginalCLPTable.Columns.Contains( "Local_Composition" ) )
            {
                OriginalCLPTable.Columns.Remove( "Local_Composition" );
            }
            if ( OriginalCLPTable.Columns.Contains( "English_Description" ) )
            {
                OriginalCLPTable.Columns.Remove( "English_Description" );
            }
            if ( OriginalCLPTable.Columns.Contains( "Local_Description" ) )
            {
                OriginalCLPTable.Columns.Remove( "Local_Description" );
            }
            if ( OriginalCLPTable.Columns.Contains( "HS_CODE" ) )
            {
                OriginalCLPTable.Columns.Remove( "HS_CODE" );
            }
            if ( OriginalCLPTable.Columns.Contains( "UNIT" ) )
            {
                OriginalCLPTable.Columns.Remove( "UNIT" );
            }
            OriginalCLPTable.AcceptChanges( );
            SetSubForm( "FrmCLPDataInput" );
            toolStripStatusLabel1.Text="多余列成功删除！";
            //MessageBox.Show( "多余列成功删除！" );
        }
        //添加基础数据
        private bool ExcelDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=9 )
            {
                errMsg="基础资料表列数为9列，请核对后重新导入！";
            }
            if ( excelDT.Columns[0].Caption!="Model code" )
            {
                errMsg="基础资料表第一列为Model code，请核对后重新导入！";
            }
            if ( excelDT.Columns[1].Caption!="法定计量单位" )
            {
                errMsg="基础资料表第二列为法定计量单位，请核对后重新导入！";
            }
            if ( excelDT.Columns[2].Caption!="HS CODE (IN CAT)" )
            {
                errMsg="基础资料表第三列为HS CODE (IN CAT)，请核对后重新导入！";
            }

            if ( excelDT.Columns[3].Caption!="中文品名" )
            {
                errMsg="基础资料表第四列为中文品名，请核对后重新导入！";
            }
            if ( excelDT.Columns[4].Caption!="英文品名" )
            {
                errMsg="基础资料表第五列为英文品名，请核对后重新导入！";
            }
            if ( excelDT.Columns[5].Caption!="申报要素" )
            {
                errMsg="基础资料表第六列为申报要素，请核对后重新导入！";
            }

            if ( excelDT.Columns[6].Caption!="监管条件" )
            {
                errMsg="基础资料表第七列为英文品名，请核对后重新导入！";
            }
            if ( excelDT.Columns[7].Caption!="Double or Set" )
            {
                errMsg="基础资料表第八列为Double or Set，请核对后重新导入！";
            }
            if ( excelDT.Columns[8].Caption!="检测报告号" )
            {
                errMsg="基础资料表第九列为检测报告号，请核对后重新导入！";
            }
            if ( errMsg!="" )
                return false;
            else
                return true;
        }
        //private bool ExcelBasicDataValidate( DataTable excelDT , ref string errMsg )
        //{
        //    if ( excelDT.Columns.Count!=9 )
        //    {
        //        errMsg="基础资料表列数为9列，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[0].Caption!="Model code" )
        //    {
        //        errMsg="基础资料表第一列为Model code，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[1].Caption!="法定计量单位" )
        //    {
        //        errMsg="基础资料表第二列为法定计量单位，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[2].Caption!="HS CODE (IN CAT)" )
        //    {
        //        errMsg="基础资料表第三列为HS CODE (IN CAT)，请核对后重新导入！";
        //    }

        //    if ( excelDT.Columns[3].Caption!="中文品名" )
        //    {
        //        errMsg="基础资料表第四列为中文品名，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[4].Caption!="英文品名" )
        //    {
        //        errMsg="基础资料表第五列为英文品名，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[5].Caption!="申报要素" )
        //    {
        //        errMsg="基础资料表第六列为申报要素，请核对后重新导入！";
        //    }

        //    if ( excelDT.Columns[6].Caption!="监管条件" )
        //    {
        //        errMsg="基础资料表第七列为英文品名，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[7].Caption!="Double or Set" )
        //    {
        //        errMsg="基础资料表第八列为Double or Set，请核对后重新导入！";
        //    }
        //    if ( excelDT.Columns[8].Caption!="检测报告号" )
        //    {
        //        errMsg="基础资料表第九列为检测报告号，请核对后重新导入！";
        //    }
        //    if ( errMsg!="" )
        //        return false;
        //    else
        //        return true;
        //}
        private void btnBasicDataInput_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.BasicDataAdd;
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog( );
            if ( fd.ShowDialog( ) == DialogResult.OK )
            {
                DataTable dt=null;
                using ( FileStream fs = new FileStream( fd.FileName , FileMode.Open , FileAccess.Read ) )
                {
                    //把文件读取到字节数组
                    byte[] data = new byte[fs.Length];
                    fs.Read( data , 0 , data.Length );
                    fs.Close( );
                    MemoryStream ms = new MemoryStream( data );
                    dt=ExcelRender.RenderFromExcel( ms , 0 , 0 );
                }
                string errMsg="";
                if ( !ExcelDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                basicDataBLL.BulkBasicDataInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}
                BasicDataTable=dt;
                SetSubForm( "FrmBasicData" );
                MessageBox.Show( "基础资料成功导入！" );
            }
        }
        private void btnBasicDataOutput_Click( object sender , EventArgs e )
        {
            SaveFileDialog sfd = new SaveFileDialog( );
            //设置文件类型 
            sfd.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            sfd.Title = "保存文件";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //点了保存按钮进入 
            if ( sfd.ShowDialog( ) == DialogResult.OK )
            {
                string localFilePath = sfd.FileName.ToString( ); //获得文件路径 
                string fileNameExt = localFilePath.Substring( localFilePath.LastIndexOf( "\\" ) + 1 ); //获取文件名，不带路径
                ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                MessageBox.Show( "基础资料成功导出！" );
                toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        private void CreateUnionDataTableSchema( )
        {
            //DtAll.Columns.Add("CLPID", typeof(Int32));
            _UnionCLPTable.Columns.Add( "ITEM_CODE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "SHIPPING_NUMBER" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "ORDER_NUMBER" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "PALLET_NUMBER" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "PARCEL_NUMBER" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "MODEL_CODE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "ORIGIN" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "QUANTITY" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "QUANTITY_UNIT" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "DISPATCHING_KEY" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "English_Composition" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "SIZE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "UNIT" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "HS_CODE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "DESCRIPTION" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "中文品名" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "英文品名" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "申报要素" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "监管条件" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "Double" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "BRAND" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "TYPE_OF_GOODS" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "PRICE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "CURRENCY" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "TOTAL_VALUE" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "NET_WEIGHT" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "GROSS_WEIGHT" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "COMMERCIAL_INVOICE_NO" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "STORE_NO" , typeof( String ) );
            _UnionCLPTable.Columns.Add( "STORE_NAME" , typeof( String ) );
        }

        /// <summary>
        /// 将两个列不同的DataTable合并成一个新的DataTable
        /// </summary>
        /// <param name="dt1">表1</param>
        /// <param name="dt2">表2</param>
        /// <returns></returns>
        private void UnitDataTable( DataTable dt1 , DataTable dt2 )
        {
            _UnionCLPTable.Rows.Clear( );
            _UnionCLPTable.AcceptChanges( );

            var query1 =
                from clp in dt1.AsEnumerable( )
                join basicdata in dt2.AsEnumerable( ).Distinct( new DataTableRowCompare( "ModelCode" ) )
                on clp.Field<String>( "MODEL_CODE" ) equals basicdata.Field<String>( "ModelCode" ) into temp
                from tt in temp.DefaultIfEmpty( )
                select new
                {
                    ItemCode = clp.Field<String>( "ITEM_CODE" ) ,
                    ShippingNumber = clp.Field<String>( "SHIPPING_NUMBER" ) ,
                    OrderNumber = clp.Field<String>( "ORDER_NUMBER" ) ,
                    //OriginalPONumber = clp.Field<String>("OriginalPONumber"),
                    PalletNumber = clp.Field<String>( "PALLET_NUMBER" ) ,
                    ParcelNumber = clp.Field<String>( "PARCEL_NUMBER" ) ,
                    ModelCode = clp.Field<String>( "MODEL_CODE" ) ,
                    Origin = clp.Field<String>( "ORIGIN" ) ,
                    Quantity = clp.Field<String>( "QUANTITY" ) ,
                    QuantityUnit = clp.Field<String>( "QUANTITY_UNIT" ) ,
                    DispatchingKey = clp.Field<String>( "DISPATCHING_KEY" ) ,
                    EnglishComposition = clp.Field<String>( "English_Composition" ) ,
                    //LocalComposition = clp.Field<String>("LocalComposition"),
                    Size = clp.Field<String>( "SIZE" ) ,
                    //EnglishDescription = clp.Field<String>("EnglishDescription"),
                    //LocalDescription = clp.Field<String>("LocalDescription"),
                    //brand前面插入UNIT、HS CODE、DESCRIPTION、英文品名、申报要素、监管条件、Double
                    //QuanitityUnit = basicdata.Field<String>("QuanitityUnit"),
                    QuanitityUnit = tt==null?"":tt.Field<String>( "QuanitityUnit" ) ,
                    HSCode = tt==null?"":tt.Field<String>( "HSCode" ) ,
                    ModelCodeDescription = tt==null?"":tt.Field<String>( "ModelCodeDescription" ) ,
                    LocalProdectName = tt==null?"":tt.Field<String>( "LocalProdectName" ) ,
                    EnglishProductName = tt==null?"":tt.Field<String>( "EnglishProductName" ) ,
                    SentialFactor = tt==null?"":tt.Field<String>( "SentialFactor" ) ,
                    SupervisionCondition = tt==null?"":tt.Field<String>( "SupervisionCondition" ) ,
                    DoubleOrSet = tt==null?"":tt.Field<String>( "DoubleOrSet" ) ,
                    Brand = clp.Field<String>( "BRAND" ) ,
                    TypeOfGoods = clp.Field<String>( "TYPE_OF_GOODS" ) ,
                    Price = clp.Field<String>( "PRICE" ) ,
                    Currency = clp.Field<String>( "CURRENCY" ) ,
                    //HSCode = clp.Field<String>("HSCode"),
                    TotalValue = clp.Field<String>( "TOTAL_VALUE" ) ,
                    //Unit = clp.Field<String>("Unit"),
                    NETWeight = clp.Field<String>( "NET_WEIGHT" ) ,
                    GrossWeight = clp.Field<String>( "GROSS_WEIGHT" ) ,
                    CommercialInvoiceNO = clp.Field<String>( "COMMERCIAL_INVOICE_NO" ) ,
                    StoreNO = clp.Field<String>( "STORE_NO" ) ,
                    StoreName = clp.Field<String>( "STORE_NAME" ) ,
                };
            foreach ( var obj in query1 )
            {
                _UnionCLPTable.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                    obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.QuanitityUnit , obj.HSCode ,
                    obj.ModelCodeDescription , obj.LocalProdectName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Brand ,
                    obj.TypeOfGoods , obj.Price , obj.Currency , obj.TotalValue , obj.NETWeight , obj.GrossWeight , obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName );
            }
            _UnionCLPTable.AcceptChanges( );
        }

        private void btnAddBasicData_Click( object sender , EventArgs e )
        {
            _BasicDataTable=basicDataBLL.GetAllList( ).Tables[0];
            if ( BasicDataTable.Rows.Count==0 )
            {
                MessageBox.Show( "基础资料记录为空，请选导入基础资料！" );
                return;
            }
            //CreateDataTableSchema( );
            UnitDataTable( OriginalCLPTable , BasicDataTable );
            this.operateType=OperateType.BasicDataAdd;
            //UnionCLPTable=DtAll;
            DataTable dtcopy=UnionCLPTable.Copy( );
            DataView dv = dtcopy.DefaultView;
            dv.Sort = "Double desc,MODEL_CODE desc";
            UnionCLPTable=dv.ToTable( );
            SetSubForm( "FrmCLPDataInput" );
            toolStripStatusLabel1.Text="成功添加基础数据！";
            //MessageBox.Show( "成功添加基础数据！" );

        }
        private void DoubleOperateForUnionCLP( DataTable unionCLPTable )
        {
            bool flag;
            int result;
            foreach ( DataRow row in unionCLPTable.Rows )
            {
                flag=false;
                result=0;
                if ( row["Double"].ToString( ).Trim( ).ToUpper( )=="DOUBLE" )
                {

                    string strSize=row["SIZE"].ToString( ).Trim( );
                    if ( strSize.Contains( "MONTHS" ) )
                    {

                        if ( int.TryParse( strSize.Split( ' ' )[0] , out result ) )
                        {
                            if ( result>24 )
                                flag=true;
                        }
                    }
                    if ( strSize.Contains( "AGE" ) )
                    {
                        if ( int.TryParse( strSize.Split( ' ' )[1] , out result ) )
                        {
                            if ( result>=3 )
                                flag=true;
                        }
                    }
                    if ( flag )
                    {
                        if ( row["申报要素"].ToString( ).Contains( "类别:婴儿" ) )
                        {
                            string strfilter="ModelCode="+"'"+row["MODEL_CODE"].ToString( )+"'"+" AND SentialFactor<>"+"'"+row["申报要素"].ToString( )+"'";
                            //DataTable dttemp=DataTableHelper.GetNewTableByDataView(BasicDataTable,strfilter);
                            DataRow[] drs=BasicDataTable.Select( strfilter );
                            row["UNIT"]=drs[0]["QuanitityUnit"];
                            row["HS_CODE"]=drs[0]["HSCode"];
                            row["中文品名"]=drs[0]["LocalProdectName"];
                            row["英文品名"]=drs[0]["EnglishProductName"];
                            row["申报要素"]=drs[0]["SentialFactor"];
                            row["监管条件"]=drs[0]["SupervisionCondition"];
                            row["Double"]="N";
                            unionCLPTable.AcceptChanges( );
                        }
                        else
                        {
                            row["Double"]="N";
                            unionCLPTable.AcceptChanges( );
                        }
                    }
                    else
                    {
                        if ( !row["申报要素"].ToString( ).Contains( "类别:婴儿" ) )
                        {
                            string strfilter="ModelCode="+"'"+row["MODEL_CODE"].ToString( )+"'"+" AND SentialFactor<>"+"'"+row["申报要素"].ToString( )+"'";
                            //DataTable dttemp=DataTableHelper.GetNewTableByDataView(BasicDataTable,strfilter);
                            DataRow[] drs=BasicDataTable.Select( strfilter );
                            row["UNIT"]=drs[0]["QuanitityUnit"];
                            row["HS_CODE"]=drs[0]["HSCode"];
                            row["中文品名"]=drs[0]["LocalProdectName"];
                            row["英文品名"]=drs[0]["EnglishProductName"];
                            row["申报要素"]=drs[0]["SentialFactor"];
                            row["监管条件"]=drs[0]["SupervisionCondition"];
                            row["Double"]="N";
                            unionCLPTable.AcceptChanges( );
                        }
                        else
                        {
                            row["Double"]="N";
                            unionCLPTable.AcceptChanges( );
                        }
                    }
                    unionCLPTable.AcceptChanges( );
                }

            }

        }
        private void SetOperateForUnionCLP( DataTable unionCLPTable )
        {

            IEnumerable<DataRow> queryforSet = from uclp in unionCLPTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double" ) =="SET"
                                               select uclp;


            DataTable dtSET = queryforSet.CopyToDataTable<DataRow>( );
            DataTable dtNEW=dtSET.Copy( );
            DataTable dtsetCopy=dtSET.Clone( );
            dtsetCopy=dtSET.AsEnumerable( ).Distinct( new DataTableRowCompare( "MODEL_CODE" ) ).CopyToDataTable( );
            int i=0;
            foreach ( DataRow row in dtsetCopy.Rows )
            {
                try
                {
                    int indexlast=dtSET.Rows.IndexOf( dtSET.AsEnumerable( ).Last( e => e.Field<String>( "MODEL_CODE" )==row["MODEL_CODE"].ToString( ) ) );
                    foreach ( DataRow dr1 in BasicDataTable.Select( "ModelCode="+"'"+row["MODEL_CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ) )
                    {
                        i++;
                        DataRow dr = dtNEW.NewRow( );
                        dr["UNIT"]=dr1["QuanitityUnit"];
                        dr["HS_CODE"]=dr1["HSCode"];
                        dr["中文品名"]=dr1["LocalProdectName"];
                        dr["英文品名"]=dr1["EnglishProductName"];
                        dr["申报要素"]=dr1["SentialFactor"];
                        dr["监管条件"]=dr1["SupervisionCondition"];
                        dtNEW.Rows.InsertAt( dr , indexlast+i );
                    }
                    i++;
                    //DataTable temp=BasicDataTable.Select( "ModelCode="+"'"+row["MODEL CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ).CopyToDataTable( );
                }
                catch ( ArgumentNullException ex )
                {
                    MessageBox.Show( "原始资料表中SET操作"+ row["MODEL_CODE"].ToString( )+"无对应的记录！" );
                    return;
                }
            }

            //添加DataTable2的数据
            foreach ( DataRow dr in unionCLPTable.AsEnumerable( ).Except( dtSET.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( ).Rows )
            {
                dtNEW.ImportRow( dr );
            }
            DoubleOrSetTable.Clear( );
            DoubleOrSetTable=dtNEW;
            DoubleOrSetTable.AcceptChanges( );

            //unionCLPTable=dtNEW;

            //MODEL CODE
            //var query1 =
            //    from basicdata in BasicDataTable.AsEnumerable( )
            //    join setdata in dtSET.AsEnumerable( )
            //    on basicdata.Field<String>( "ModelCode" ) equals setdata.Field<String>( "MODEL CODE" )
            //    select basicdata;

            //DataTable dtBasicData = query1.Distinct().CopyToDataTable<DataRow>( );
        }
        private void DoubleOrSetOperate( DataTable unionCLPTable )
        {
            //DOUBLE OPERATE 
            DoubleOperateForUnionCLP( unionCLPTable );
            //UnionCLPTable.AcceptChanges( );
            DataTable dtSET=DataTableHelper.GetNewTableByDataView( unionCLPTable , "Double='SET'" );

            DataTable dtnew=dtSET.Clone( );
            IEnumerable<DataRow> queryforSet = from uclp in unionCLPTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double" ) =="SET"
                                               select uclp;


            DataTable boundTable = queryforSet.CopyToDataTable<DataRow>( );



            //DataColumn[] cols1= new  DataColumn[]{BasicDataTable.Columns["MODEL CODE"]};
            //DataColumn[] cols2= new  DataColumn[]{dtSET.Columns["MODEL CODE"]};
            DataTable dtResult=DataTableHelper.LeftOuterJoin( BasicDataTable , dtSET , new DataColumn[] { BasicDataTable.Columns["ModelCode"] } , new DataColumn[] { dtSET.Columns["MODEL_CODE"] } );
            //foreach ( DataRow row in UnionCLPTable.Rows )
            //{
            //    if ( row["Double"].ToString( ).Trim( ).ToUpper( )=="SET" )
            //    {
            //        DataTable dtSET=DataTableHelper.GetNewTableByDataView( unionCLPTable , "Double=SET" );
            //        //DataColumn[] cols1= new  DataColumn[]{BasicDataTable.Columns["MODEL CODE"]};
            //        //DataColumn[] cols2= new  DataColumn[]{dtSET.Columns["MODEL CODE"]};
            //        DataTable dtResult=DataTableHelper.LeftOuterJoin(BasicDataTable,dtSET,new  DataColumn[]{BasicDataTable.Columns["MODEL CODE"]},new  DataColumn[]{dtSET.Columns["MODEL CODE"]});


            //    }
            //}

        }
        private void SupervisionConditionForUnionCLP( )
        {
            SupervisionConditionBLL scbll=new SupervisionConditionBLL( );
            SupervisionConditionEntity currenyRntity;
            List<SupervisionConditionEntity> scList=scbll.GetModelList( "" );
            foreach ( DataRow row in DoubleOrSetTable.Rows )
            {
                currenyRntity=scList.Find( delegate( SupervisionConditionEntity entity )
                {
                    return entity.SupervisionConditionName==row["监管条件"].ToString( );
                } );
                if ( currenyRntity!=null )
                    row["监管条件"]="N";

            }
            DoubleOrSetTable.AcceptChanges( );
        }
        private void btnDoubleOrSet_Click( object sender , EventArgs e )
        {
            BasicDataTable=basicDataBLL.GetAllList( ).Tables[0];
            if ( BasicDataTable.Rows.Count==0 )
            {
                MessageBox.Show( "基础资料记录为空，请选导入基础资料！" );
                return;
            }
            //BasicDataTable=basicDataBLL.GetAllList( ).Tables[0];
            if ( OriginalCLPTable.Rows.Count==0 )
            {
                MessageBox.Show( "原始资料记录为空，请选导入资料资料！" );
                return;
            }

            if ( UnionCLPTable.Rows.Count==0 )
            {
                MessageBox.Show( "请删除多余列后添加基础数据！" );
                return;
            }

            DoubleOperateForUnionCLP( UnionCLPTable );
            SetOperateForUnionCLP( UnionCLPTable );
            SupervisionConditionForUnionCLP( );
            this.operateType=OperateType.DorbleOrSet;
            SetSubForm( "FrmCLPDataInput" );
            toolStripStatusLabel1.Text="成功添加基础数据！";
            //MessageBox.Show( "成功添加基础数据！" );
        }

        private void btnFilterSupervisionCondition_Click( object sender , EventArgs e )
        {
            DataTable newdt = new DataTable( );
            DataTable resultdt=new DataTable( );
            resultdt=newdt = DoubleOrSetTable.Clone( );
            foreach ( DataRow row in DoubleOrSetTable.Select( "Double<>'N'" ) )
            {
                newdt.Rows.Add( row.ItemArray );
            }
            foreach ( DataRow row in newdt.Select( "监管条件 LIKE '%B%'" ) )
            {
                resultdt.Rows.Add( row.ItemArray );
            }
            foreach ( DataRow row in newdt.Select( "监管条件 NOT LIKE '%B%'" ) )
            {
                resultdt.Rows.Add( row.ItemArray );
            }
            foreach ( DataRow row in DoubleOrSetTable.Select( "Double='N'" ) )
            {
                resultdt.Rows.Add( row.ItemArray );
            }
            foreach ( DataRow row in resultdt.Rows )
            {

                row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
                row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
            }
            resultdt.AcceptChanges( );
            _DoubleOrSetTable.Clear( );
            _DoubleOrSetTable=resultdt;
            _DoubleOrSetTable.AcceptChanges( );
            DataTable dtparcelnumer=_DoubleOrSetTable.Clone( );
            //掏箱
            foreach ( DataRow dr in _DoubleOrSetTable.Select( "Double LIKE '%掏箱%'" ).AsEnumerable( ).Distinct<DataRow>( new DataTableRowCompare( "PARCEL_NUMBER" ) ).CopyToDataTable( ).Rows )
            {
                DataRow[] tempdrs=_DoubleOrSetTable.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                foreach ( DataRow row in tempdrs )
                {
                    dtparcelnumer.Rows.Add( row.ItemArray );
                    _DoubleOrSetTable.Rows.Remove( row );
                }
            }
            _DoubleOrSetTable.AcceptChanges( );

            DataTable tempdt=dtparcelnumer;
            string[] strFileName=_fileName.Split( '.' );
            dtparcelnumer.TableName=strFileName[0]+"-2."+strFileName[1];
            _DoubleOrSetTable.TableName=_fileName;
            _ParcelNumberDataSet.Tables.Add( _DoubleOrSetTable.Copy( ) );
            _ParcelNumberDataSet.Tables.Add( dtparcelnumer.Copy( ) );
            //DataTable temp=resultdt;
            this.operateType=OperateType.FilterSupervisionCondition;
            SetSubForm( "FrmMulSheet" );
            toolStripStatusLabel1.Text="筛选监管条件完成！";
            //DataRow[] drs=DoubleOrSetTable.Select( "Double<>'N'" ).CopyToDataTable().Select("监管条件 LIKE '%B%'");
            //DataTable test=drs.CopyToDataTable( );

        }

        private void btnCLPOutput_Click( object sender , EventArgs e )
        {
            SaveFileDialog sfd = new SaveFileDialog( );
            //设置文件类型 
            sfd.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            sfd.Title = "保存文件";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //点了保存按钮进入 
            if ( sfd.ShowDialog( ) == DialogResult.OK )
            {
                string localFilePath = sfd.FileName.ToString( ); //获得文件路径 
                string fileNameExt = localFilePath.Substring( localFilePath.LastIndexOf( "\\" ) + 1 );
                string sheetname=String.Empty;
                foreach ( DataTable dt in ParcelNumberDataSet.Tables )
                {
                    sheetname+=dt.TableName+",";
                }
                sheetname=sheetname.Substring( 0 , sheetname.Length-1 );
                ExcelRender.RenderToExcel( ParcelNumberDataSet , sheetname , localFilePath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                MessageBox.Show( "原始CLP成功导出！" );
                toolStripStatusLabel1.Text="原始CLP成功导出！";
            }
        }

        private void btnUnionCLPTable_Click( object sender , EventArgs e )
        {
            DataTable newdt=new DataTable( );
            newdt=ParcelNumberDataSet.Tables[0].Clone( );
            foreach ( DataTable dt in ParcelNumberDataSet.Tables )
            {
                foreach ( DataRow dr in dt.Rows)
                {
                    newdt.ImportRow( dr );
                }                
            }
            newdt.AcceptChanges( );
            newdt.Select("")
        }

        private void btnDealSet_Click( object sender , EventArgs e )
        {

        }

        private void btnDealInspection_Click( object sender , EventArgs e )
        {

        }

        private void btnDealNonInspection_Click( object sender , EventArgs e )
        {

        }

        private void btnCLPInput_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.TotalCLPHandle;
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog( );
            fd.Multiselect=true;
            if ( fd.ShowDialog( ) == DialogResult.OK )
            {
                bool flag=false;
                foreach ( string filename in fd.FileNames )
                {
                    if ( !ExcelRender.CheckFiles( filename ) )
                    {
                        flag=true;
                        break;
                    }
                }
                if ( flag )
                {
                    MessageBox.Show( "当前需要导入的Excel文件正在被占用，请关闭该文件后重新导入！" );
                    return;
                }
                DataSet ds=new DataSet( );
                foreach ( string filename in fd.FileNames )
                {
                    using ( FileStream fs = new FileStream( filename , FileMode.Open , FileAccess.Read ) )
                    {
                        //把文件读取到字节数组
                        byte[] data = new byte[fs.Length];
                        fs.Read( data , 0 , data.Length );
                        fs.Close( );
                        MemoryStream ms = new MemoryStream( data );
                        _CLPTable=new DataTable( );
                        _CLPTable=ExcelRender.RenderFromExcel( ms , 0 , 0 );
                        _CLPTable.TableName=filename.Substring( filename.LastIndexOf( '\\' )+1 );
                        ds.Tables.Add( _CLPTable );
                    }

                }
                _ParcelNumberDataSet=ds;
                //_fileName=fd.FileName.Substring( fd.FileName.LastIndexOf( '\\' )+1 );
                //_OriginalCLPTable.TableName=_fileName;
                //_OriginalCLPTable.AcceptChanges( );
                //DataTable tempDT=new DataTable( );
                //tempDT=_OriginalCLPTable.Copy( );
                //tempDT.Columns.Add( "FileName" , typeof( String ) );
                //tempDT.Columns.Add( "CreateTime" , typeof( DateTime ) );
                //tempDT.Columns.Add( "Creator" , typeof( String ) );
                //foreach ( DataRow row in tempDT.Rows )
                //{
                //    row["FileName"]=FileName;
                //    row["CreateTime"]=DateTime.Now;
                //    row["Creator"]="admin";
                //}
                //bll.BulkOriginalCLPInsert( tempDT , 1000 );
                SetSubForm( "FrmTotalCLPHandle" );
                toolStripStatusLabel1.Text="原始CLP成功导入！";
                //MessageBox.Show( "原始CLP成功导入！" );

            }
        }

    }

    public class DataTableRowCompare : IEqualityComparer<DataRow>
    {
        private string ColumnAttr;
        #region IEqualityComparer<DataRow> 成员
        public DataTableRowCompare( string strColumn )
        {
            this.ColumnAttr=strColumn;
        }
        public bool Equals( DataRow x , DataRow y )
        {
            return ( x.Field<String>( ColumnAttr ) == y.Field<String>( ColumnAttr ) );
        }

        public int GetHashCode( DataRow obj )
        {
            return obj.ToString( ).GetHashCode( );
        }

        #endregion
    }
}
