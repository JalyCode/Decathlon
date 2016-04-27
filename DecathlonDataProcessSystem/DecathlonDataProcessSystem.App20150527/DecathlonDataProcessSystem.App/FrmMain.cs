using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DecathlonDataProcessSystem.Common;
using DecathlonDataProcessSystem.Model;
using DecathlonDataProcessSystem.BLL;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;

namespace DecathlonDataProcessSystem.App
{
    public enum OperateType
    {
        OriginalCLPDataInput= 0 ,
        OriginalCLPDataColumnDelete = 1 ,
        OriginalCLPBasicDataAdd = 2 ,
        OriginalCLPDorbleOrSet = 3 ,
        FilterSupervisionCondition = 4 ,
        UnionCLPInput = 5 ,
        UnionCLPHandle = 6 ,
        BasicDataInput = 7 ,
        UnionCLPSET=8 ,
        UnionCLPCommodityInspection=9 ,
        UnionCLPNonCommodityInspection=10 ,
    }
    public enum StatusFlag
    {
        OriginalFlag=0,
        KeyFlag=1,
        ValueFlag=2,
    }
    public partial class FrmMain : Form
    {
        private Form _CurrentMdiChild;
        public OperateType operateType;
        public StatusFlag statusFlag;
        //原始CLP导入后数据存放表
        private DataTable _OriginalCLPTable;
        public DataTable OriginalCLPTable
        {
            get { return _OriginalCLPTable; }
            set { _OriginalCLPTable=value; }
        }
        //原始CLP删除列后数据存放表
        private DataTable _CLPDeleteColumnTable;
        public DataTable CLPDeleteColumnTable
        {
            get { return _CLPDeleteColumnTable; }
            set { _CLPDeleteColumnTable=value; }
        }
        //CLP基础资料信息表
        private DataTable _CLPBasicDataTable;
        public DataTable CLPBasicDataTable
        {
            get { return _CLPBasicDataTable; }
            set { _CLPBasicDataTable =value; }
        }
        //原始CLP汇总后数据存放表
        private DataTable _OriginalCLPTotalTable;
        public DataTable OriginalCLPTotalTable
        {
            get { return _OriginalCLPTotalTable; }
            set { _OriginalCLPTotalTable=value; }
        }
        //汇总阶段CLP导入
        private DataTable _CLPTable= new DataTable( );
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
        private DataTable _DoubleOrSetTable;
        public DataTable DoubleOrSetTable
        {
            get { return _DoubleOrSetTable; }
            set { _DoubleOrSetTable=value; }
        }

        private DataTable _RepetitiveBasicDataTable;
        public DataTable RepetitiveBasicDataTable
        {
            get { return _RepetitiveBasicDataTable; }
            set { _RepetitiveBasicDataTable =value; }
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
        private List<string> _SentialFactors;
        public List<string> SentialFactors
        {
            get { return _SentialFactors; }
            set { _SentialFactors=value; }
        }
        private bool Flag=false;
        public string OriginalCLPPath
        {
            get { return ConfigurationManager.AppSettings["OriginalCLPPath"].EndsWith( "\\" )?ConfigurationManager.AppSettings["OriginalCLPPath"]:ConfigurationManager.AppSettings["OriginalCLPPath"]+"\\"; }
        }
        public string UnionCLPPath
        {
            get { return ConfigurationManager.AppSettings["UnionCLPPath"].EndsWith( "\\" )?ConfigurationManager.AppSettings["UnionCLPPath"]:ConfigurationManager.AppSettings["UnionCLPPath"]+"\\"; }
        }
        private readonly OriginalCLPBLL bll=new OriginalCLPBLL( );
        private readonly BasicDataBLL basicDataBLL=new BasicDataBLL( );
        private readonly SentialFactorTypeBLL sftBLL=new SentialFactorTypeBLL( );
        private readonly SetBreakUpBLL sbuBll=new SetBreakUpBLL( );
        public FrmMain( )
        {
            InitializeComponent( );

            _SentialFactors=new List<string>( );
            _OriginalCLPTable=new DataTable( );
            _CLPDeleteColumnTable=new DataTable( );
            _CLPBasicDataTable=new DataTable( );
            _OriginalCLPTotalTable=new DataTable( );
            _DoubleOrSetTable=new DataTable( );
            CreateOriginalCLPUnionTableSchema( );
            _SetBreakUpTable=new DataTable( );
            //_OriginalCLPBasicDataTable=new DataTable( );
            _RepetitiveBasicDataTable=new DataTable( );
            _CLPBasicDataTable=basicDataBLL.GetDistinctModeCodeList( ).Tables[0];
            _RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
            _SentialFactors=sftBLL.GetModelList( "" );
            _SetBreakUpTable=sbuBll.GetAllList( ).Tables[0];
        }
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
        }
        #region 子窗体
        private void SetSubForm( string className )
        {
            try
            {
                string assemblyName = Assembly.GetExecutingAssembly( ).GetName( ).Name;
                className = assemblyName + "." + className;
                Type t = Type.GetType( className );
                if ( t != null )
                {
                    if ( this.operateType==OperateType.OriginalCLPDataInput )
                    {
                        object[] args = new object[] { "原始CLP制作【第一步：导入原始资料】" , OperateType.OriginalCLPDataInput , OriginalCLPTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.OriginalCLPDataColumnDelete )
                    {
                        object[] args = new object[] { "原始CLP制作【第二步：删除多余列】" , OperateType.OriginalCLPDataColumnDelete , CLPDeleteColumnTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.OriginalCLPBasicDataAdd )
                    {
                        object[] args = new object[] { "原始CLP制作【第三步：添加基础数据】" , OperateType.OriginalCLPBasicDataAdd , OriginalCLPTotalTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.OriginalCLPDorbleOrSet )
                    {
                        object[] args = new object[] { "原始CLP制作【第四步：Double或Set操作】" , OperateType.OriginalCLPDorbleOrSet , DoubleOrSetTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.FilterSupervisionCondition )
                    {
                        object[] args = new object[] { "原始CLP制作【第五步：掏箱与不出运】" , OperateType.FilterSupervisionCondition , ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPInput )
                    {
                        object[] args = new object[] { "汇总CLP制作【第一步：处理【CLP文件导入】" , OperateType.FilterSupervisionCondition , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPHandle )
                    {
                        object[] args = new object[] { "汇总CLP制作【第二步：处理【CLP文件处理】" , OperateType.FilterSupervisionCondition , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPSET )
                    {
                        object[] args = new object[] { "汇总CLP制作【第三步：处理【处理SET数据】" , OperateType.UnionCLPSET , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPCommodityInspection )
                    {
                        object[] args = new object[] { "汇总CLP制作【第三步：处理【处理商检数据】" , OperateType.UnionCLPCommodityInspection , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPNonCommodityInspection )
                    {
                        object[] args = new object[] { "汇总CLP制作【第三步：处理【处理非商检数据】" , OperateType.UnionCLPNonCommodityInspection , _ParcelNumberDataSet };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
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
        #endregion
        #region 原始CLP制作

        /// <summary>
        /// 导入原始资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPInput_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.OriginalCLPDataInput;
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
                    _OriginalCLPTable.Clear( );
                    //_OriginalCLPTable.Columns.Clear( );
                    _OriginalCLPTable=ExcelRender.RenderFromExcel( ms , 0 , 0 );
                }
                _fileName=fd.FileName.Substring( fd.FileName.LastIndexOf( '\\' )+1 );
                _OriginalCLPTable.TableName=_fileName;
                foreach ( DataRow dr in _OriginalCLPTable.Rows )
                {
                    if ( dr["NET_WEIGHT"].ToString( )!="" )
                        dr["NET_WEIGHT"]=decimal.Round( decimal.Parse( dr["NET_WEIGHT"].ToString( ) ) , 2 );
                    if ( dr["GROSS_WEIGHT"].ToString( )!="" )
                        dr["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) ) , 2 );
                }
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
                SetSubForm( "FrmOriginalCLPDataHandle" );
                toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
                toolStripStatusLabel2.Text="操作结果：原始CLP成功导入！";
                //MessageBox.Show( "原始CLP成功导入！" );

            }
        }
        /// <summary>
        /// 删除多余列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPDeleteColumn_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.OriginalCLPDataColumnDelete;
            _CLPDeleteColumnTable.Clear( );
            _CLPDeleteColumnTable=_OriginalCLPTable.Copy( );
            _CLPDeleteColumnTable=DeleteTableColumns( _CLPDeleteColumnTable , new string[] { "ORIGINAL_PO_NUMBER" , "Local_Composition" , "English_Description" , "Local_Description" , "HS_CODE" , "UNIT" }.ToList<string>( ) );
            _CLPDeleteColumnTable.AcceptChanges( );
            //_CLPDeleteColumnTable=OriginalCLPTable;
            SetSubForm( "FrmOriginalCLPDataHandle" );
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            toolStripStatusLabel2.Text="操作结果：多余列成功删除！";

            //SetSubForm( "FrmCLPDataInput" );
            //toolStripStatusLabel1.Text="多余列成功删除！";
            //MessageBox.Show( "多余列成功删除！" );
        }
        /// <summary>
        /// 添加基础数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPAddBasicData_Click( object sender , EventArgs e )
        {
            if ( _CLPBasicDataTable.Rows.Count==0 )
            {
                MessageBox.Show( "基础资料记录为空，请选导入基础资料！" );
                return;
            }
            this.operateType=OperateType.OriginalCLPBasicDataAdd;
            UnitDataTable( _CLPDeleteColumnTable , _CLPBasicDataTable );
            if ( _CLPDeleteColumnTable.Rows.Count!=_OriginalCLPTotalTable.Rows.Count )
            {
                string errmsg=string.Empty;
                errmsg="相关记录在原始资料表无对应的记录！详细信息如下：\r\n";
                DataTable diffDT=bll.GetDiffrenceOriginalCLPRecord( _fileName );
                foreach ( DataRow dr in diffDT.Rows )
                {
                    errmsg+=string.Format( "  ITEMCODE:{0},MODELCODE:{1},HSCode:{2} \r\n" , dr["ItemCode"].ToString( ) , dr["ModelCode"].ToString( ) , dr["HSCode"].ToString( ) );
                }
                MessageBox.Show( errmsg );
                return;
            }
            DataTable dtcopy=_OriginalCLPTotalTable.Copy( );
            DataView dv = dtcopy.DefaultView;
            dv.Sort = "Double_or_Set desc,MODEL_CODE desc";
            _OriginalCLPTotalTable=dv.ToTable( );
            SetSubForm( "FrmOriginalCLPDataHandle" );
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            toolStripStatusLabel2.Text="操作结果：成功添加基础数据！";

            //SetSubForm( "FrmCLPDataInput" );
            //toolStripStatusLabel1.Text="成功添加基础数据！";
            //MessageBox.Show( "成功添加基础数据！" );

        }

        /// <summary>
        /// Double Or Set操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPDoubleOrSet_Click( object sender , EventArgs e )
        {
            //BasicDataTable=basicDataBLL.GetAllList( ).Tables[0];
            if ( CLPBasicDataTable.Rows.Count==0 )
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

            if ( _OriginalCLPTotalTable.Rows.Count==0 )
            {
                MessageBox.Show( "请删除多余列后添加基础数据！" );
                return;
            }
            //_UnionCLPTable.Columns.Add( "STORE_NAME" , typeof( String ) );
            _DoubleOrSetTable.Clear( );
            _DoubleOrSetTable=_OriginalCLPTotalTable.Copy( );
            string errmsg=string.Empty;
            DoubleOperateForUnionCLP( _DoubleOrSetTable, ref errmsg);
            if ( errmsg!="" )
            {
                MessageBox.Show(errmsg);
                return;
            }
            SetOperateForUnionCLP( _DoubleOrSetTable );
            SupervisionConditionForUnionCLP( _DoubleOrSetTable );
            this.operateType=OperateType.OriginalCLPDorbleOrSet;
            SetSubForm( "FrmOriginalCLPDataHandle" );
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            toolStripStatusLabel2.Text="操作结果：成功进行Double或Set数据处理！";
            //toolStripStatusLabel1.Text="成功进行Double或Set数据处理！";
            //MessageBox.Show( "成功添加基础数据！" );
        }
        private DataTable _ParcelTakeOutTable;
        public DataTable ParcelTakeOutTable
        {
            get { return _ParcelTakeOutTable; }
            set { _ParcelTakeOutTable =value; }
        }
        private DataTable _ParcelTakeOutConditionTable;
        public DataTable ParcelTakeOutConditionTable
        {
            get { return _ParcelTakeOutConditionTable; }
            set { _ParcelTakeOutConditionTable =value; }
        }
        /// <summary>
        /// 掏箱与不出运
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPFilterSupervisionCondition_Click( object sender , EventArgs e )
        {

            DataTable _DoubleOrSetCloneTable=new DataTable( );
            //DataTable newdt = new DataTable( );
            //DataTable resultdt=new DataTable( );
            //newdt = _DoubleOrSetTable.Clone( );
            //resultdt=_DoubleOrSetTable.Clone( );
            _DoubleOrSetCloneTable=_DoubleOrSetTable.Clone( );
            _ParcelTakeOutTable=new ParcelTakeOutBLL( ).GetAllList( ).Tables[0];
            _ParcelTakeOutConditionTable=new ParcelTakeOutConditionBLL( ).GetAllList( ).Tables[0];
            DataTable _notakeoutDT=new DataTable( );
            _notakeoutDT= _DoubleOrSetTable.Clone( );
            DataTable _takeoutDT=new DataTable( );
            _takeoutDT=_DoubleOrSetTable.Clone( );

            if ( _ParcelTakeOutTable.Rows.Count==0 )
            {
                MessageBox.Show( "需要掏箱的Model Code不能为空，请先导入！" );
                return;
            }

            //foreach ( DataRow row in _DoubleOrSetTable.Select( "Double_or_Set<>'N'" ) )
            //{
            //    newdt.Rows.Add( row.ItemArray );
            //}

            //newdt.Select( "监管条件 LIKE '%B%'" ).ToList( ).ForEach(
            //    dr => resultdt.Rows.Add( dr.ItemArray )
            //    );
            ////int b=resultdt.Rows.Count;
            //foreach ( DataRow row in newdt.Select( "监管条件 NOT LIKE '%B%'" ) )
            //{
            //    resultdt.Rows.Add( row.ItemArray );
            //}
            ////int c=resultdt.Rows.Count;
            //foreach ( DataRow row in _DoubleOrSetTable.Select( "Double_or_Set='N'" ) )
            //{
            //    resultdt.Rows.Add( row.ItemArray );
            //}
            ////int d=resultdt.Rows.Count;
            //foreach ( DataRow row in resultdt.Rows )
            //{
            //    if ( row["NET_WEIGHT"].ToString( )!="" )
            //        row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
            //    if ( row["GROSS_WEIGHT"].ToString( )!="" )
            //        row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
            //}

            //resultdt.AcceptChanges( );
            ////_DoubleOrSetTable.Clear( );
            ////_DoubleOrSetTable=SetOperateForUnionCLP1( resultdt );
            //_DoubleOrSetCloneTable=resultdt;
            //_DoubleOrSetCloneTable.AcceptChanges( );

            ParcelTakeOut( _DoubleOrSetTable , ref _notakeoutDT , ref _takeoutDT );
            foreach ( DataRow dr in _notakeoutDT.Rows )
            {
                if ( dr["SHIPPING_NUMBER"].ToString( )=="" )
                    dr["ITEM_CODE"]=null;
                if ( dr["NET_WEIGHT"].ToString( )!="" )
                    dr["NET_WEIGHT"]=decimal.Round( decimal.Parse( dr["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( dr["GROSS_WEIGHT"].ToString( )!="" )
                    dr["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) ) , 2 );
            }
            foreach ( DataRow dr in _takeoutDT.Rows )
            {
                if ( dr["SHIPPING_NUMBER"].ToString( )=="" )
                    dr["ITEM_CODE"]=null;
                if ( dr["NET_WEIGHT"].ToString( )!="" )
                    dr["NET_WEIGHT"]=decimal.Round( decimal.Parse( dr["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( dr["GROSS_WEIGHT"].ToString( )!="" )
                    dr["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) ) , 2 );
            }
            //DataTable tempdt=dtparcelnumer;
            string[] strFileName=_fileName.Split( '.' );
            _ParcelNumberDataSet.Tables.Clear( );
            _ParcelNumberDataSet.Tables.Add( _notakeoutDT.Copy( ) );
            _ParcelNumberDataSet.Tables.Add( _takeoutDT.Copy( ) );
            int i=0;
            foreach ( DataTable dt in _ParcelNumberDataSet.Tables )
            {
                i++;
                dt.TableName=strFileName[0]+"-"+i.ToString( )+"."+strFileName[1];
                foreach ( DataRow dr in dt.Rows )
                {
                    if ( dr["SHIPPING_NUMBER"].ToString( )!="" )
                        dr["SHIPPING_NUMBER"]=dr["SHIPPING_NUMBER"].ToString( )+"-"+i.ToString( );
                }
                dt.AcceptChanges( );
            }

            //_notakeoutDT.TableName=strFileName[0]+"-1."+strFileName[1];
            //_takeoutDT.TableName=strFileName[0]+"-2."+strFileName[1];

            //DataTable temp=resultdt;
            _ParcelNumberDataSet.AcceptChanges( );
            this.operateType=OperateType.FilterSupervisionCondition;
            SetSubForm( "FrmOriginalCLPParcelTakeOut" );
            //toolStripStatusLabel1.Text="筛选监管条件完成！";
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            toolStripStatusLabel2.Text="操作结果：筛选监管条件完成！";
            //DataRow[] drs=DoubleOrSetTable.Select( "Double<>'N'" ).CopyToDataTable().Select("监管条件 LIKE '%B%'");
            //DataTable test=drs.CopyToDataTable( );

        }
        //private DataTable ParcelTakeOutDataSort(DataTable temp)
        //{
        //    //DataTable _DoubleOrSetCloneTable=new DataTable( );
        //    DataTable newdt = new DataTable( );
        //    DataTable resultdt=new DataTable( );
        //    newdt = _DoubleOrSetTable.Clone( );
        //    resultdt=_DoubleOrSetTable.Clone( );
        //    //_DoubleOrSetCloneTable=_DoubleOrSetTable.Clone( );
        //    //_ParcelTakeOutTable=new ParcelTakeOutBLL( ).GetAllList( ).Tables[0];
        //    //_ParcelTakeOutConditionTable=new ParcelTakeOutConditionBLL( ).GetAllList( ).Tables[0];
        //    //DataTable _notakeoutDT=new DataTable( );
        //    //_notakeoutDT= _DoubleOrSetTable.Clone( );
        //    //DataTable _takeoutDT=new DataTable( );
        //    //_takeoutDT=_DoubleOrSetTable.Clone( );

        //    foreach ( DataRow row in _DoubleOrSetTable.Select( "Double_or_Set<>'N'" ) )
        //    {
        //        newdt.Rows.Add( row.ItemArray );
        //    }

        //    newdt.Select( "监管条件 LIKE '%B%'" ).ToList( ).ForEach(
        //        dr => resultdt.Rows.Add( dr.ItemArray )
        //        );
        //    //int b=resultdt.Rows.Count;
        //    foreach ( DataRow row in newdt.Select( "监管条件 NOT LIKE '%B%'" ) )
        //    {
        //        resultdt.Rows.Add( row.ItemArray );
        //    }
        //    //int c=resultdt.Rows.Count;
        //    foreach ( DataRow row in _DoubleOrSetTable.Select( "Double_or_Set='N'" ) )
        //    {
        //        resultdt.Rows.Add( row.ItemArray );
        //    }
        //    //int d=resultdt.Rows.Count;
        //    foreach ( DataRow row in resultdt.Rows )
        //    {
        //        if ( row["NET_WEIGHT"].ToString( )!="" )
        //            row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
        //        if ( row["GROSS_WEIGHT"].ToString( )!="" )
        //            row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
        //    }
        //    resultdt.AcceptChanges( );
        //    return resultdt;        
        //}
        private DataTable DoubleOrSetTableItemModelDataFill( DataTable _dosTable )
        {
            DataTable tempDT=new DataTable( );
            tempDT=_dosTable.Copy( );
            string itemcode=string.Empty;
            string modelcode=string.Empty;
            string QUANTITY=string.Empty;
            string PARCEL_NUMBER=string.Empty;
            string SHIPPING_NUMBER=string.Empty;
            foreach ( DataRow dr in tempDT.Rows )
            {
                if ( dr["ITEM_CODE"].ToString( )!="" )
                    itemcode=dr["ITEM_CODE"].ToString( );
                else
                    dr["ITEM_CODE"]=itemcode;
                if ( dr["MODEL_CODE"].ToString( )!="" )
                    modelcode=dr["MODEL_CODE"].ToString( );
                else
                    dr["MODEL_CODE"]=modelcode;
                if ( dr["QUANTITY"].ToString( )!="" )
                    QUANTITY=dr["QUANTITY"].ToString( );
                else
                    dr["QUANTITY"]=QUANTITY;
                if ( dr["PARCEL_NUMBER"].ToString( )!="" )
                    PARCEL_NUMBER=dr["PARCEL_NUMBER"].ToString( );
                else
                    dr["PARCEL_NUMBER"]=PARCEL_NUMBER;
                //if ( dr["SHIPPING_NUMBER"].ToString( )!="" )
                //    SHIPPING_NUMBER=dr["SHIPPING_NUMBER"].ToString( );
                //else
                //    dr["SHIPPING_NUMBER"]=PARCEL_NUMBER;
            }
            return tempDT;
        }
        private void ParcelTakeOut( DataTable _dosTable , ref DataTable _notakeoutDT , ref DataTable _takeoutDT )
        {
            DataRow[] drs;
            string stritemCode=string.Empty;
            string strmodeCode=string.Empty;
            string strunit=string.Empty;
            int sum;
            DataTable tempDT=_dosTable.Copy( );
            tempDT=DoubleOrSetTableItemModelDataFill(_dosTable);

            foreach ( DataRow dr in _dosTable.Select( " Model_code is not null " ) )
            {
                drs=_ParcelTakeOutConditionTable.Select( "ItemCode='"+dr["Item_Code"].ToString( )+"' AND ModelCode='"+dr["Model_code"].ToString( )+"' AND LocalProdectName='"+dr["中文品名"].ToString( )+"'" );
                if ( drs.Length>0 )
                {
                    sum=0;
                    DataRow[] parcelnumber=tempDT.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                    foreach ( DataRow row in parcelnumber )
                    {
                        strunit=row["QUANTITY_UNIT"].ToString( );
                        if ( strunit.Contains( '×' ) )
                        {
                            sum+=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( strunit.Split( '×' )[1] );
                        }
                        else
                            sum+=int.Parse( row["QUANTITY"].ToString( ) );
                    }
                    if ( drs[0]["MinExportQTY"].ToString( )!="" )
                    {
                        if ( sum<int.Parse( drs[0]["MinExportQTY"].ToString( ) ) )
                        {
                            foreach ( DataRow row in parcelnumber )
                            {
                                _takeoutDT.Rows.Add( row.ItemArray );
                                tempDT.Rows.Remove( row );
                            }
                        }
                        //tempDT.AcceptChanges( );
                    }
                }
                else
                {

                    if ( _ParcelTakeOutTable.Select( "ModelCode='"+dr["Model_code"].ToString( )+"'" ).Count( )>0 )
                    {
                        //DataRow[] parcelnumber=_dosTable.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                        DataRow[] parcelnumber=tempDT.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                        foreach ( DataRow row in parcelnumber )
                        {
                            _takeoutDT.Rows.Add( row.ItemArray );
                            tempDT.Rows.Remove( row );
                        }
                        //tempDT.AcceptChanges( );
                    }
                }

            }
            //_notakeoutDT=_dosTable.AsEnumerable( ).Except( _takeoutDT.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( );
            _notakeoutDT=tempDT;
            //_notakeoutDT=tempDT.AsEnumerable( ).Except( _takeoutDT.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( );
            //foreach ( DataRow dr in tempDT.Rows )
            //{
            //    _notakeoutDT.Rows.Add( dr.ItemArray );
            //}
        }
        /// <summary>
        /// 原始资料导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOriginalCLPOutput_Click( object sender , EventArgs e )
        {
            if ( !Directory.Exists( OriginalCLPPath ) )
                Directory.CreateDirectory( OriginalCLPPath );
            string fullfilepath=OriginalCLPPath+_fileName;
            if ( this.operateType==OperateType.OriginalCLPDataInput )
            {
                ExcelRender.RenderToExcel( _OriginalCLPTable , fullfilepath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
                toolStripStatusLabel2.Text="操作结果：原始CLP成功导出！";
                MessageBox.Show( "原始CLP成功导出！" );
                //toolStripStatusLabel1.Text="原始CLP成功导出！";
            }
            else if ( this.operateType==OperateType.OriginalCLPDataColumnDelete )
            {
                ExcelRender.RenderToExcel( _CLPDeleteColumnTable , fullfilepath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
                toolStripStatusLabel2.Text="操作结果：删除多余列的原始CLP成功导出！";
                MessageBox.Show( "删除多余列的原始CLP成功导出！" );
            }
            else if ( this.operateType==OperateType.OriginalCLPBasicDataAdd )
            {
                ExcelRender.RenderToExcel( _OriginalCLPTotalTable , fullfilepath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
                toolStripStatusLabel2.Text="操作结果：添加基础数据的原始CLP成功导出！";
                MessageBox.Show( "添加基础数据的原始CLP成功导出！" );
            }
            else if ( this.operateType==OperateType.OriginalCLPDorbleOrSet )
            {
                ExcelRender.RenderToExcel( _DoubleOrSetTable , fullfilepath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
                toolStripStatusLabel2.Text="操作结果：Double或Set数据处理后的原始CLP成功导出！";
                MessageBox.Show( "Double或Set数据处理后的原始CLP成功导出！" );
            }
            else if ( this.operateType==OperateType.FilterSupervisionCondition )
            {
                //string sheetname=String.Empty;
                string filenames=string.Empty;
                foreach ( DataTable dt in ParcelNumberDataSet.Tables )
                {
                    if ( dt.Rows.Count>0 )
                    {
                        //sheetname+=dt.TableName+",";
                        filenames+=dt.TableName+",";
                        if ( File.Exists( OriginalCLPPath+dt.TableName ) )
                        {
                            try
                            {
                                File.Delete( OriginalCLPPath+dt.TableName );
                            }
                            catch ( Exception ex )
                            {
                                MessageBox.Show( "请将相关的EXCEL文件关闭后重新进行操作！" );
                                return;
                            }
                        }
                        ExcelRender.RenderToExcel( dt , OriginalCLPPath+dt.TableName );
                    }
                }
                //sheetname=sheetname.Substring( 0 , sheetname.Length-1 );
                //ExcelRender.RenderToExcel( ParcelNumberDataSet , sheetname , localFilePath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作文件："+filenames.Substring( 0 , filenames.Length-1 );
                toolStripStatusLabel2.Text="操作结果：掏箱与不出运操作后的原始CLP成功导出！";
                MessageBox.Show( "掏箱与不出运操作后的原始CLP成功导出！" );
                //toolStripStatusLabel1.Text="掏箱与不出运操作后的原始CLP成功导出！";
            }

            //SaveFileDialog sfd = new SaveFileDialog( );
            ////设置文件类型 
            //sfd.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            //sfd.Title = "保存文件";
            ////设置默认文件类型显示顺序 
            //sfd.FilterIndex = 1;
            ////保存对话框是否记忆上次打开的目录 
            //sfd.RestoreDirectory = true;
            ////点了保存按钮进入 
            //if ( sfd.ShowDialog( ) == DialogResult.OK )
            //{
            //    string localFilePath = sfd.FileName.ToString( ); //获得文件路径 
            //    string fileNameExt = localFilePath.Substring( localFilePath.LastIndexOf( "\\" ) + 1 );
            //    if ( this.operateType==OperateType.OriginalCLPDataInput )
            //    {
            //        ExcelRender.RenderToExcel( _OriginalCLPTable , localFilePath );
            //        //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
            //        toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            //        toolStripStatusLabel2.Text="操作结果：原始CLP成功导出！";
            //        MessageBox.Show( "原始CLP成功导出！" );
            //        //toolStripStatusLabel1.Text="原始CLP成功导出！";
            //    }
            //    else if ( this.operateType==OperateType.OriginalCLPDataColumnDelete )
            //    {
            //        ExcelRender.RenderToExcel( _CLPDeleteColumnTable , localFilePath );
            //        //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
            //        toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            //        toolStripStatusLabel2.Text="操作结果：删除多余列的原始CLP成功导出！";
            //        MessageBox.Show( "删除多余列的原始CLP成功导出！" );
            //    }
            //    else if ( this.operateType==OperateType.OriginalCLPBasicDataAdd )
            //    {
            //        ExcelRender.RenderToExcel( _OriginalCLPTotalTable , localFilePath );
            //        //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
            //        toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            //        toolStripStatusLabel2.Text="操作结果：添加基础数据的原始CLP成功导出！";
            //        MessageBox.Show( "添加基础数据的原始CLP成功导出！" );
            //    }
            //    else if ( this.operateType==OperateType.OriginalCLPDorbleOrSet )
            //    {
            //        ExcelRender.RenderToExcel( _DoubleOrSetTable , localFilePath );
            //        //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
            //        toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            //        toolStripStatusLabel2.Text="操作结果：Double或Set数据处理后的原始CLP成功导出！";
            //        MessageBox.Show( "Double或Set数据处理后的原始CLP成功导出！" );
            //    }
            //    else if ( this.operateType==OperateType.FilterSupervisionCondition )
            //    {
            //        string sheetname=String.Empty;
            //        foreach ( DataTable dt in ParcelNumberDataSet.Tables )
            //        {
            //            sheetname+=dt.TableName+",";
            //        }
            //        sheetname=sheetname.Substring( 0 , sheetname.Length-1 );
            //        ExcelRender.RenderToExcel( ParcelNumberDataSet , sheetname , localFilePath );
            //        //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
            //        toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            //        toolStripStatusLabel2.Text="操作结果：掏箱与不出运操作后的原始CLP成功导出！";
            //        MessageBox.Show( "掏箱与不出运操作后的原始CLP成功导出！" );
            //        //toolStripStatusLabel1.Text="掏箱与不出运操作后的原始CLP成功导出！";
            //    }

            //}
        }
        #endregion


        public bool ExcuteBasicDataInput( DataTable excelDT , string fileName , ref string errMsg )
        {
            //basicDataBLL.BulkBasicDataInsert( excelDT , 1000 );
            foreach ( DataRow row in excelDT.Rows )
            {
                BasicDataEntity model=new BasicDataEntity( );
                model.ModelCode=row["Model code"].ToString( );
                model.QuantityUnit=row["法定计量单位"].ToString( );
                model.HSCodeInCat=row["HS CODE (IN CAT)"].ToString( );
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
        //private void btnDeleteColumn_Click( object sender , EventArgs e )
        //{
        //    this.operateType=OperateType.CLPDataColumnDelete;
        //    if ( OriginalCLPTable.Columns.Contains( "ORIGINAL_PO_NUMBER" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "ORIGINAL_PO_NUMBER" );
        //    }
        //    if ( OriginalCLPTable.Columns.Contains( "Local_Composition" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "Local_Composition" );
        //    }
        //    if ( OriginalCLPTable.Columns.Contains( "English_Description" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "English_Description" );
        //    }
        //    if ( OriginalCLPTable.Columns.Contains( "Local_Description" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "Local_Description" );
        //    }
        //    if ( OriginalCLPTable.Columns.Contains( "HS_CODE" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "HS_CODE" );
        //    }
        //    if ( OriginalCLPTable.Columns.Contains( "UNIT" ) )
        //    {
        //        OriginalCLPTable.Columns.Remove( "UNIT" );
        //    }
        //    OriginalCLPTable.AcceptChanges( );
        //    SetSubForm( "FrmCLPDataInput" );
        //    toolStripStatusLabel1.Text="多余列成功删除！";
        //    //MessageBox.Show( "多余列成功删除！" );
        //}
        //添加基础数据

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
        #region 基础信息操作
        private bool ExcelDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=10 )
            {
                errMsg="基础资料表列数为10列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="Model_code" )
            {
                errMsg="基础资料表第一列为Model code，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="法定计量单位" )
            {
                errMsg="基础资料表第二列为法定计量单位，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="HS_CODE_(IN_CAT)" )
            {
                errMsg="基础资料表第三列为HS CODE (IN CAT)，请核对后重新导入！";
                return false;
            }

            if ( excelDT.Columns[3].Caption!="中文品名" )
            {
                errMsg="基础资料表第四列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[4].Caption!="英文品名" )
            {
                errMsg="基础资料表第五列为英文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[5].Caption!="申报要素" )
            {
                errMsg="基础资料表第六列为申报要素，请核对后重新导入！";
                return false;
            }

            if ( excelDT.Columns[6].Caption!="监管条件" )
            {
                errMsg="基础资料表第七列为英文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[7].Caption!="Double_or_Set" )
            {
                errMsg="基础资料表第八列为Double or Set，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[8].Caption!="现检测报告号" )
            {
                errMsg="基础资料表第九列为现检测报告号，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[9].Caption!="SIZE" )
            {
                errMsg="基础资料表第十列为SIZE，请核对后重新导入！";
                return false;
            }
            return true;
        }
        private void btnCLPBasicDataInput_Click( object sender , EventArgs e )
        {

            this.operateType=OperateType.BasicDataInput;
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog( );
            if ( !ExcelRender.CheckFiles( fd.FileName ) )
            {
                MessageBox.Show( "当前需要导入的Excel文件正在被占用，请关闭该文件后重新导入！" );
                return;
            }
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
                //dt.Columns.Add()
                basicDataBLL.BulkBasicDataInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}
                _CLPBasicDataTable.Clear( );
                _CLPBasicDataTable=basicDataBLL.GetDistinctModeCodeList( ).Tables[0];
                _CLPBasicDataTable.AcceptChanges( );
                _RepetitiveBasicDataTable.Clear( );
                _RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                _RepetitiveBasicDataTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "基础资料成功导入！" );
            }
        }
        private void btnCLPBasicDataOutput_Click( object sender , EventArgs e )
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
                ExcelRender.RenderToExcel( basicDataBLL.GetAllList( ).Tables[0] , localFilePath );
                MessageBox.Show( "基础资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        #endregion

        private void CreateOriginalCLPUnionTableSchema( )
        {
            //DtAll.Columns.Add("CLPID", typeof(Int32));
            _OriginalCLPTotalTable.Columns.Add( "ITEM_CODE" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "SHIPPING_NUMBER" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "ORDER_NUMBER" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "PALLET_NUMBER" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "PARCEL_NUMBER" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "MODEL_CODE" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "ORIGIN" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "QUANTITY" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "QUANTITY_UNIT" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "DISPATCHING_KEY" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "English_Composition" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "SIZE" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "法定计量单位" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "HS_CODE_(IN_CAT)" , typeof( String ) );
            //_OriginalCLPTotalTable.Columns.Add( "DESCRIPTION" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "中文品名" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "英文品名" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "申报要素" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "监管条件" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "Double_or_Set" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "BRAND" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "TYPE_OF_GOODS" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "PRICE" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "CURRENCY" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "TOTAL_VALUE" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "NET_WEIGHT" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "GROSS_WEIGHT" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "COMMERCIAL_INVOICE_NO" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "STORE_NO" , typeof( String ) );
            _OriginalCLPTotalTable.Columns.Add( "STORE_NAME" , typeof( String ) );
            //_UnionCLPTable.Columns.Add( "Flag" , typeof( String ) );
        }

        /// <summary>
        /// 将两个列不同的DataTable合并成一个新的DataTable
        /// </summary>
        /// <param name="dt1">表1</param>
        /// <param name="dt2">表2</param>
        /// <returns></returns>
        private void UnitDataTable( DataTable dt1 , DataTable dt2 )
        {
            //from tt in temp.DefaultIfEmpty( )
            _OriginalCLPTotalTable.Rows.Clear( );
            _OriginalCLPTotalTable.AcceptChanges( );

            var query1 =
                from clp in dt1.AsEnumerable( )
                join basicdata in dt2.AsEnumerable( )
                on clp.Field<String>( "MODEL_CODE" ) equals basicdata.Field<String>( "ModelCode" )
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

                    Unit = basicdata.Field<String>( "QuantityUnit" ) ,
                    HSCodeInCat = basicdata.Field<String>( "HSCodeInCat" ) ,
                    //ModelCodeDescription = basicdata.Field<String>( "ModelCodeDescription" ) ,
                    LocalProdectName = basicdata.Field<String>( "LocalProdectName" ) ,
                    EnglishProductName = basicdata.Field<String>( "EnglishProductName" ) ,
                    SentialFactor = basicdata.Field<String>( "SentialFactor" ) ,
                    SupervisionCondition = basicdata.Field<String>( "SupervisionCondition" ) ,
                    DoubleOrSet = basicdata.Field<String>( "DoubleOrSet" ) ,
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
                _OriginalCLPTotalTable.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                    obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
                    obj.LocalProdectName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Brand ,
                    obj.TypeOfGoods , obj.Price , obj.Currency , obj.TotalValue , decimal.Round( decimal.Parse( obj.NETWeight ) , 2 ) , decimal.Round( decimal.Parse( obj.GrossWeight ) , 2 ) , obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName );
            }
            _OriginalCLPTotalTable.AcceptChanges( );
        }
        /// <summary>
        /// 区分成人与婴儿
        /// </summary>
        /// <param name="row"></param>
        /// <returns>0:其他；1：成人；2：婴儿</returns>
        private int IsAdultOrBabyBySize( DataRow row )
        {
            int result=0;
            int flag=0;
            string strSize=row["SIZE"].ToString( ).ToUpper( ).Trim( );
            if ( strSize.Contains( "MONTHS" ) )
            {

                if ( int.TryParse( strSize.Split( ' ' )[0] , out result ) )
                {
                    if ( result<=24 )
                        flag=2;
                    else
                        flag=1;
                }
            }
            if ( strSize.Contains( "AGE" ) )
            {
                if ( int.TryParse( strSize.Split( ' ' )[1] , out result ) )
                {
                    if ( result<3 )
                        flag=2;
                    else
                        flag=1;
                }
            }
            return flag;            
        }

        private void DoubleOperateForUnionCLP( DataTable unionCLPTable,ref string errmsg )
        {
            int flag;
            string strfilter;
            //int result;
            foreach ( DataRow row in unionCLPTable.Rows )
            {
                flag=0;
                strfilter=string.Empty;
                //result=0;
                if ( row["Double_or_Set"].ToString( ).Trim( ).ToUpper( ).Contains( "DOUBLE" ) )
                {
                    flag=IsAdultOrBabyBySize( row );
                    if ( flag==1 )
                        strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size is null)";
                    else if ( flag==2 )
                        strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧3岁 )' OR Size is null)";
                    else
                        strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"'";
                    DataRow[] drs=RepetitiveBasicDataTable.Select( strfilter );
                    if ( drs.Count( )==0 )
                    {
                        errmsg+=string.Format( "基础资料表MODELCODE为{0}找不到符合DOUBLE操作条件的记录" , row["MODEL_CODE"].ToString( ) );
                        return;
                    }
                    else
                    {
                        row["UNIT"]=drs[0]["QuantityUnit"];
                        row["HS_CODE_(IN_CAT)"]=drs[0]["HSCodeInCat"];
                        row["中文品名"]=drs[0]["LocalProdectName"];
                        row["英文品名"]=drs[0]["EnglishProductName"];
                        row["申报要素"]=drs[0]["SentialFactor"];
                        row["监管条件"]=drs[0]["SupervisionCondition"];
                        unionCLPTable.AcceptChanges( );
                    }
                }

            }

        }
        private void SetOperateForUnionCLP( DataTable unionCLPTable )
        {
            int flag;
            string strfilter;
            DataTable dtSET = unionCLPTable.Clone( );
            //DataTable dtNEW=dtSET.Copy( );
            DataTable dtsetCopy=dtSET.Clone( );
            IEnumerable<DataRow> queryforSet = from uclp in unionCLPTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double_or_Set" ).ToUpper( ).Contains( "SET" )
                                               select uclp;
            if ( queryforSet.Count( )>0 )
            {
                dtSET = queryforSet.CopyToDataTable<DataRow>( );
                foreach ( DataRow dr in dtSET.Rows )
                {
                    flag=0;
                    strfilter=string.Empty;
                    dtsetCopy.Rows.Add( dr.ItemArray );
                    try
                    {
                        flag=IsAdultOrBabyBySize( dr );
                        if ( flag==1 )
                            strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size is null)";
                        else if ( flag==2 )
                            strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧3岁 )' OR Size is null)";
                        else
                            strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"'";
                        //if ( IsAdultOrBabyBySize( dr ) )
                        //    strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND Size<>'(≦2岁 )'";
                        //else
                        //    strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND Size='(≦2岁 )'";
                        foreach ( DataRow dr1 in RepetitiveBasicDataTable.Select( strfilter ).Skip<DataRow>( 1 ) )
                        {
                            DataRow newdr = dtsetCopy.NewRow( );
                            newdr["法定计量单位"]=dr1["QuantityUnit"];
                            newdr["HS_CODE_(IN_CAT)"]=dr1["HSCodeInCat"];
                            newdr["中文品名"]=dr1["LocalProdectName"];
                            newdr["英文品名"]=dr1["EnglishProductName"];
                            newdr["申报要素"]=dr1["SentialFactor"];
                            newdr["监管条件"]=dr1["SupervisionCondition"];
                            //newdr["Double_or_Set"]=dr1["DoubleOrSet"];
                            //newdr["Flag"]=dr1["DoubleOrSet"];
                            dtsetCopy.Rows.Add( newdr );
                            //dtNEW.Rows.InsertAt( dr , indexlast+i );
                        }
                    }
                    catch ( ArgumentNullException ex )
                    {
                        MessageBox.Show( "原始资料表中SET操作"+ dr["MODEL_CODE"].ToString( )+"无对应的记录！" );
                        return;
                    }
                }
                //添加DataTable2的数据
                foreach ( DataRow dr in unionCLPTable.AsEnumerable( ).Except( dtSET.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( ).Rows )
                {
                    dtsetCopy.ImportRow( dr );
                }
                //set做完之后将所有的0改为N
                foreach ( DataRow dr in dtsetCopy.Rows )
                {
                    if ( dr["Double_or_Set"].ToString( ).ToUpper( )=="DOUBLE"||dr["Double_or_Set"].ToString( )=="0" )
                        dr["Double_or_Set"]="N";
                }
                _DoubleOrSetTable.Clear( );
                _DoubleOrSetTable=dtsetCopy;
                _DoubleOrSetTable.AcceptChanges( );
            }
            else
            {
                dtsetCopy=unionCLPTable.Copy( );
                //set做完之后将所有的0改为N
                foreach ( DataRow dr in dtsetCopy.Rows )
                {
                    if ( dr["Double_or_Set"].ToString( ).ToUpper( )=="DOUBLE"||dr["Double_or_Set"].ToString( )=="0" )
                        dr["Double_or_Set"]="N";
                }
                _DoubleOrSetTable.Clear( );
                _DoubleOrSetTable=dtsetCopy;
                _DoubleOrSetTable.AcceptChanges( );
            }
        }
        private string getSentialFactorCompareItems( string strsentail )
        {
            string strresult=String.Empty;
            string[] strArray=strsentail.Split( '|' );
            foreach ( string sentail in strArray )
            {
                if ( sentail.Contains( ':' ) )
                {
                    string[] keyvalues=sentail.Split( ':' );
                    if ( _SentialFactors.Contains( keyvalues[0] ) )
                    {
                        strresult+=sentail+"|";
                    }
                }
                else
                {
                    if ( _SentialFactors.Contains( sentail ) )
                    {
                        strresult+=sentail+"|";
                    }
                }
            }
            if ( strresult.EndsWith( "|" ) )
                strresult=strresult.Substring( 0 , strresult.Length-1 );
            return strresult;
        }
        private DataTable SetOperateForUnionCLP1( DataTable ClpTable )
        {

            IEnumerable<DataRow> queryforSet = from uclp in ClpTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double" ).ToUpper( ).IndexOf( "SET" )>-1
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
                    foreach ( DataRow dr1 in RepetitiveBasicDataTable.Select( "ModelCode="+"'"+row["MODEL_CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ) )
                    {
                        i++;
                        DataRow dr = dtNEW.NewRow( );
                        dr["法定计量单位"]=dr1["QuantityUnit"];
                        dr["HS_CODE"]=dr1["HSCode"];
                        dr["中文品名"]=dr1["LocalProdectName"];
                        dr["英文品名"]=dr1["EnglishProductName"];
                        dr["申报要素"]=dr1["SentialFactor"];
                        dr["监管条件"]=dr1["SupervisionCondition"];
                        dtNEW.Rows.InsertAt( dr , indexlast+i );
                        dtNEW.AcceptChanges( );

                    }
                    i++;
                    //DataTable temp=BasicDataTable.Select( "ModelCode="+"'"+row["MODEL CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ).CopyToDataTable( );
                }
                catch ( ArgumentNullException ex )
                {
                    MessageBox.Show( "原始资料表中SET操作"+ row["MODEL_CODE"].ToString( )+"无对应的记录！" );
                    return null;
                }

            }
            //return dtNEW;
            //添加DataTable2的数据
            foreach ( DataRow dr in ClpTable.AsEnumerable( ).Except( dtSET.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( ).Rows )
            {
                dtNEW.ImportRow( dr );
            }
            return dtNEW;
            //DoubleOrSetTable.Clear( );
            //DoubleOrSetTable=dtNEW;
            //DoubleOrSetTable.AcceptChanges( );
        }
        private DataTable SetOperateForUnionCLP2( DataTable ClpTable )
        {

            IEnumerable<DataRow> queryforSet = from uclp in ClpTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double_or_Set" ).Contains( "SET" )
                                               select uclp;
            //where uclp.Field<String>( "Double_or_Set" ).IndexOf( "SET" )>-1
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
                    foreach ( DataRow dr1 in RepetitiveBasicDataTable.Select( "ModelCode="+"'"+row["MODEL_CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ) )
                    {
                        i++;
                        DataRow dr = dtNEW.NewRow( );
                        dr["法定计量单位"]=dr1["QuantityUnit"];
                        dr["HS_CODE_(IN_CAT)"]=dr1["HSCodeInCat"];
                        dr["中文品名"]=dr1["LocalProdectName"];
                        dr["英文品名"]=dr1["EnglishProductName"];
                        dr["申报要素"]=dr1["SentialFactor"];
                        dr["监管条件"]=dr1["SupervisionCondition"];
                        dtNEW.Rows.InsertAt( dr , indexlast+i );
                        dtNEW.AcceptChanges( );

                    }
                    i++;
                    //DataTable temp=BasicDataTable.Select( "ModelCode="+"'"+row["MODEL CODE"].ToString( )+"'" ).Skip<DataRow>( 1 ).CopyToDataTable( );
                }
                catch ( ArgumentNullException ex )
                {
                    MessageBox.Show( "原始资料表中SET操作"+ row["MODEL_CODE"].ToString( )+"无对应的记录！" );
                    return null;
                }

            }
            //return dtNEW;
            //添加DataTable2的数据
            foreach ( DataRow dr in ClpTable.AsEnumerable( ).Except( dtSET.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( ).Rows )
            {
                dtNEW.ImportRow( dr );
            }
            return dtNEW;
            //DoubleOrSetTable.Clear( );
            //DoubleOrSetTable=dtNEW;
            //DoubleOrSetTable.AcceptChanges( );
        }
        private void DoubleOrSetOperate( DataTable unionCLPTable )
        {
            string errmsg=string.Empty;
            //DOUBLE OPERATE 
            DoubleOperateForUnionCLP( unionCLPTable,ref errmsg );
            if ( errmsg!="" )
            {
                MessageBox.Show( errmsg);
                return;
            }
            //UnionCLPTable.AcceptChanges( );
            DataTable dtSET=DataTableHelper.GetNewTableByDataView( unionCLPTable , "Double='SET'" );

            DataTable dtnew=dtSET.Clone( );
            IEnumerable<DataRow> queryforSet = from uclp in unionCLPTable.AsEnumerable( )
                                               where uclp.Field<String>( "Double" ) =="SET"
                                               select uclp;


            DataTable boundTable = queryforSet.CopyToDataTable<DataRow>( );



            //DataColumn[] cols1= new  DataColumn[]{BasicDataTable.Columns["MODEL CODE"]};
            //DataColumn[] cols2= new  DataColumn[]{dtSET.Columns["MODEL CODE"]};
            DataTable dtResult=DataTableHelper.LeftOuterJoin( CLPBasicDataTable , dtSET , new DataColumn[] { CLPBasicDataTable.Columns["ModelCode"] } , new DataColumn[] { dtSET.Columns["MODEL_CODE"] } );
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
        //private void SupervisionConditionForUnionCLP( )
        //{
        //    SupervisionConditionBLL scbll=new SupervisionConditionBLL( );
        //    SupervisionConditionEntity currenyRntity;
        //    List<SupervisionConditionEntity> scList=scbll.GetModelList( "" );
        //    foreach ( DataRow row in _DoubleOrSetTable.Rows )
        //    {
        //        currenyRntity=scList.Find( delegate( SupervisionConditionEntity entity )
        //        {
        //            return entity.SupervisionConditionName==row["监管条件"].ToString( );
        //        } );
        //        if ( currenyRntity!=null )
        //            row["Double_or_Set"]="N";

        //    }
        //    _DoubleOrSetTable.AcceptChanges( );
        //}
        private void SupervisionConditionForUnionCLP(DataTable _doubleorsetTable )
        {
            SupervisionConditionBLL scbll=new SupervisionConditionBLL( );
            SupervisionConditionEntity currenyRntity;
            List<SupervisionConditionEntity> scList=scbll.GetModelList( "" );
            foreach ( DataRow row in _doubleorsetTable.Rows )
            {
                currenyRntity=scList.Find( delegate( SupervisionConditionEntity entity )
                {
                    return entity.SupervisionConditionName==row["Double_or_Set"].ToString( );
                } );
                if ( currenyRntity!=null )
                    row["Double_or_Set"]="N";

            }
            _doubleorsetTable.AcceptChanges( );
        }

        #region 汇总CLP制作
        private DataTable _UnionCLPTotalTable;
        public DataTable UnionCLPTotalTable
        {
            get { return _UnionCLPTotalTable; }
            set { _UnionCLPTotalTable=value; }
        }
        //SET数据
        private DataTable _UnionCLPSETTable;
        public DataTable UnionCLPSETTable
        {
            get { return _UnionCLPSETTable; }
            set { _UnionCLPSETTable=value; }
        }
        //SET数据拆分（毛重、净重处理）
        private DataTable _SetBreakUpTable;
        public DataTable SetBreakUpTable
        {
            get { return _SetBreakUpTable; }
            set { _SetBreakUpTable=value; }
        }

        //SET处理汇总表
        private DataTable _UnionCLPSETTotalTable;
        public DataTable UnionCLPSETTotalTable
        {
            get { return _UnionCLPSETTotalTable; }
            set { _UnionCLPSETTotalTable=value; }
        }
        //商检数据
        private DataTable _UnionCLPCommodityInspectionTable;
        public DataTable UnionCLPCommodityInspectionTable
        {
            get { return _UnionCLPCommodityInspectionTable; }
            set { _UnionCLPCommodityInspectionTable =value; }
        }
        //商检处理汇总表
        private DataTable _UnionCLPCommodityInspectionTotalTable;
        public DataTable UnionCLPCommodityInspectionTotalTable
        {
            get { return _UnionCLPCommodityInspectionTotalTable; }
            set { _UnionCLPCommodityInspectionTotalTable =value; }
        }
        //非商检数据
        private DataTable _UnionCLPNonCommodityInspectionTable;
        public DataTable UnionCLPNonCommodityInspectionTable
        {
            get { return _UnionCLPNonCommodityInspectionTable; }
            set { _UnionCLPNonCommodityInspectionTable =value; }
        }
        //非商检处理汇总表
        private DataTable _UnionCLPNonCommodityInspectionTotalTable;
        public DataTable UnionCLPNonCommodityInspectionTotalTable
        {
            get { return _UnionCLPNonCommodityInspectionTotalTable; }
            set { _UnionCLPNonCommodityInspectionTotalTable =value; }
        }
        private DataSet SubTotalForUnionCLPTable( DataTable unoinclp )
        {
            DataSet ds=new DataSet( );
            if ( unoinclp.Rows.Count==0 )
                return ds;
            ds.Tables.Add( unoinclp );
            _UnionCLPSETTable=new DataTable( );
            _UnionCLPSETTable=unoinclp.Clone( );
            _UnionCLPSETTable.TableName="SET";
            _UnionCLPCommodityInspectionTable=new DataTable( );
            _UnionCLPCommodityInspectionTable=unoinclp.Clone( );
            _UnionCLPCommodityInspectionTable.TableName="商检";
            _UnionCLPNonCommodityInspectionTable=new DataTable( );
            _UnionCLPNonCommodityInspectionTable=unoinclp.Clone( );
            _UnionCLPNonCommodityInspectionTable.TableName="非商检";

            foreach ( DataRow row in unoinclp.Rows )
            {
                //if (row["MODEL_CODE_(IN_CAT)"].ToString( )=="" )
                if ( row["ITEM_CODE"].ToString( )==""&&row["MODEL_CODE"].ToString( )=="" )
                    _UnionCLPSETTable.Rows.Add( row.ItemArray );
                else
                {
                    if ( row["监管条件"].ToString( ).Contains( 'B' ) )
                        _UnionCLPCommodityInspectionTable.Rows.Add( row.ItemArray );
                    else
                    {
                        if ( row["Double_Or_Set"].ToString( )=="N" )
                            _UnionCLPNonCommodityInspectionTable.Rows.Add( row.ItemArray );
                        else
                            _UnionCLPSETTable.Rows.Add( row.ItemArray );
                    }

                }
            }
            ds.Tables.Add( _UnionCLPSETTable );
            ds.Tables.Add( _UnionCLPCommodityInspectionTable );
            ds.Tables.Add( _UnionCLPNonCommodityInspectionTable );
            ds.AcceptChanges( );
            return ds;
        }
        private DataTable DeleteTableColumns( DataTable sourceDT , List<string> delColumns )
        {
            DataTable dt=sourceDT.Copy( );
            foreach ( string strColumn in delColumns )
            {
                if ( dt.Columns.Contains( strColumn ) )
                {
                    dt.Columns.Remove( strColumn );
                }
            }
            dt.AcceptChanges( );
            return dt;
        }
        private DataTable UnionCLPDeleteColumnAndSentialFactorHandle( DataTable sourceDT )
        {
            DataTable tempDT=new DataTable( );
            //DataTable copyDT=new DataTable( );
            tempDT=sourceDT.Copy( );
            tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            "DISPATCHING_KEY","English_Composition","SIZE","法定计量单位","监管条件","Double_or_Set","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );

            //_UnionCLPCommodityInspectionTable=dt.Copy( );
            string strunit=String.Empty;
            foreach ( DataRow row in tempDT.Rows )
            {
                strunit=row["QUANTITY_UNIT"].ToString( );
                if ( strunit.Contains( '×' ) )
                {
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( strunit.Split( '×' )[1] );
                    row["QUANTITY_UNIT"]=strunit.Split( '×' )[0];
                }
                if ( row["申报要素"].ToString( ).Contains( ';' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ';' , ' ' ).Trim( );
                }
                if ( row["申报要素"].ToString( ).Contains( ',' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ',' , ' ' ).Trim( );
                }
                row["申报要素"]=getSentialFactorCompareItems( row["申报要素"].ToString( ) );
            }
            return tempDT;
        }

        private Dictionary<List<DataRow> , List<DataRow>> UnionCLPSetTableSentialFactorTotalHandle( DataTable sourceDT )
        {
            Dictionary<List<DataRow> , List<DataRow>> dic=new Dictionary<List<DataRow> , List<DataRow>>( );
            Queue<DataRow> clpsetQueue = new Queue<DataRow>( );
            List<DataRow> tkeys=new List<DataRow>( );
            List<DataRow> tvalues=new List<DataRow>( );
            bool flag=false;
            int keyindex=0;
            int valueindex=0;
            int j=0;
            foreach ( DataRow dr in sourceDT.Rows )
            {

                if ( dr["QUANTITY"].ToString( )!=""&& dr["QUANTITY_UNIT"].ToString( )!="" )
                {
                    if(!flag)
                    {
                        keyindex=sourceDT.Rows.IndexOf( dr );
                        flag=true;
                        j++;
                    }
                } 
                else
                {                                                 
                    valueindex=sourceDT.Rows.IndexOf( dr );
                    if ( valueindex<sourceDT.Rows.Count-1 )
                    {
                        if ( sourceDT.Rows[valueindex+1]["QUANTITY"].ToString( )!=""&& sourceDT.Rows[valueindex+1]["QUANTITY_UNIT"].ToString( )!="" )
                        {
                            flag=false;
                        }
                    }

                }
                dr["GroupID"]=j;
            }

            var query = from t in sourceDT.AsEnumerable( )
                        group t by new { t1 = t.Field<int>( "GroupID" ) } into m
                        select new
                        {
                            GroupID = m.Key.t1
                        };
            foreach ( var obj in query )
            {
                sourceDT.Select("GroupID="+obj.GroupID).AsEnumerable<DataRow>().ToList().ForEach(
                    delegate( DataRow dr){
                        if(dr["QUANTITY"].ToString( )!=""&& dr["QUANTITY_UNIT"].ToString( )!="")
                            tkeys.Add(dr);
                        else 
                            tvalues.Add(dr);                        
                    });
                dic.Add( tkeys, tvalues );
                tkeys=new List<DataRow>( );
                tvalues=new List<DataRow>( );
            }            
            return dic;
        }
        private DataTable GetSetBreakUpDataRowGroupDataByCLPSetGroup( Dictionary<List<DataRow> , List<DataRow>> dic )
        {
            ArrayList KeyList=new ArrayList( );
            ArrayList keyGroupList=new ArrayList( );

            ArrayList ValueList=new ArrayList( );
            ArrayList VauleGroupList=new ArrayList( );
            DataTable setbreakuptableclone=_SetBreakUpTable.Clone( );
            foreach ( var dickey in dic.Keys )
            {
                foreach ( DataRow dr in dickey.Distinct<DataRow>( new DataTableRowCompare( "中文品名" ) ).AsEnumerable( ).CopyToDataTable( ).Rows )
                {
                    KeyList.Add( dr["中文品名"].ToString( ) );
                }
                foreach ( DataRow dr in dic[dickey].Distinct<DataRow>( new DataTableRowCompare( "中文品名" ) ).AsEnumerable( ).CopyToDataTable( ).Rows )
                {
                    ValueList.Add( dr["中文品名"].ToString( ) );
                }
            }
            foreach ( string strkey in KeyList )
            {
                keyGroupList.AddRange( GetGroupIDByLocalProdectName( strkey ) );
            }
            foreach ( string strvalue in ValueList )
            {
                VauleGroupList.AddRange( GetGroupIDByLocalProdectName( strvalue ) );
            }
            setbreakuptableclone.BeginLoadData( );
            foreach ( string strgroupid in keyGroupList.ToArray( ).Distinct( ).Intersect( VauleGroupList.ToArray( ).Distinct( ) ) )
            {
                foreach ( DataRow row in _SetBreakUpTable.Select( "GroupID="+strgroupid ) )
                {
                    setbreakuptableclone.LoadDataRow( row.ItemArray , true );
                }
            }
            setbreakuptableclone.EndLoadData( );
            return setbreakuptableclone;
        }
        private ArrayList GetGroupIDByLocalProdectName( string localProdectName )
        {
            ArrayList list=new ArrayList( );
            foreach ( DataRow row in _SetBreakUpTable.Select( "LocalProdectName='"+localProdectName+"'" ) )
            {
                list.Add( row["GroupID"].ToString( ) );
            }
            return list;
        }
        private bool UnionCLPSETDataRowGroupCompare( List<DataRow> drs1 , List<DataRow> drs2 )
        {
            if ( drs1.Count!=drs2.Count )
                return false;
            bool flag=true;
            foreach ( DataRow dr1 in drs1 )
            {
                if ( drs2.Find( delegate( DataRow dr )
                {
                    return ( dr["中文品名"].ToString( )==dr1["中文品名"].ToString( ) )&&
                        ( dr["HS_CODE_(IN_CAT)"].ToString( )==dr1["HS_CODE_(IN_CAT)"].ToString( ) )&&
                        ( getSentialFactorCompareItems( dr["申报要素"].ToString( ) )==getSentialFactorCompareItems( dr1["申报要素"].ToString( ) ) );
                } )==null )
                    flag=false;
            }
            return flag;
        }
        private List<DataRow> UnionCLPSETDataRowGroup( List<DataRow> drs2 , List<DataRow> drs1 )
        {
            for ( int i=0 ; i<drs2.Count ; i++ )
            {
                drs2[i]["TOTAL_VALUE"]=decimal.Parse( drs1[i]["TOTAL_VALUE"].ToString( ) )+decimal.Parse( drs2[i]["TOTAL_VALUE"].ToString( ) );
                drs2[i]["NET_WEIGHT"]=decimal.Parse( drs1[i]["NET_WEIGHT"].ToString( ) )+decimal.Parse( drs2[i]["NET_WEIGHT"].ToString( ) );
                drs2[i]["GROSS_WEIGHT"]=decimal.Parse( drs1[i]["GROSS_WEIGHT"].ToString( ) )+decimal.Parse( drs2[i]["GROSS_WEIGHT"].ToString( ) );
            }
            return drs2;
        }
        private DataTable UnionCLPSETDataRowGroupDataTotal( DataTable unionClpSetTable , DataTable setBreakUpCloneTable )
        {
            DataTable unionCLPSETTableClone=unionClpSetTable.Clone( );
            var query1 =
                from clp in unionClpSetTable.AsEnumerable( )
                join setdata in setBreakUpCloneTable.AsEnumerable( )
                on clp.Field<String>( "中文品名" ) equals setdata.Field<String>( "LocalProdectName" )
                select new
                {
                    ItemCode = clp.Field<String>( "ITEM_CODE" ) ,
                    ShippingNumber = clp.Field<String>( "SHIPPING_NUMBER" ) ,
                    OrderNumber = clp.Field<String>( "ORDER_NUMBER" ) ,
                    PalletNumber = clp.Field<String>( "PALLET_NUMBER" ) ,
                    ParcelNumber = clp.Field<String>( "PARCEL_NUMBER" ) ,
                    ModelCode = clp.Field<String>( "MODEL_CODE" ) ,
                    Origin = clp.Field<String>( "ORIGIN" ) ,
                    Quantity = clp.Field<String>( "QUANTITY" ) ,
                    QuantityUnit = clp.Field<String>( "QUANTITY_UNIT" ) ,
                    DispatchingKey = clp.Field<String>( "DISPATCHING_KEY" ) ,
                    EnglishComposition = clp.Field<String>( "English_Composition" ) ,
                    Size = clp.Field<String>( "SIZE" ) ,
                    Unit = clp.Field<String>( "法定计量单位" ) ,
                    HSCodeInCat = clp.Field<String>( "HS_CODE_(IN_CAT)" ) ,
                    LocalProdectName = clp.Field<String>( "中文品名" ) ,
                    EnglishProductName = clp.Field<String>( "英文品名" ) ,
                    SentialFactor = clp.Field<String>( "申报要素" ) ,
                    SupervisionCondition = clp.Field<String>( "监管条件" ) ,
                    DoubleOrSet = clp.Field<String>( "Double_or_Set" ) ,
                    Brand = clp.Field<String>( "BRAND" ) ,
                    TypeOfGoods = clp.Field<String>( "TYPE_OF_GOODS" ) ,
                    Price = clp.Field<String>( "PRICE" ) ,
                    Currency = clp.Field<String>( "CURRENCY" ) ,
                    TotalValue = clp.Field<String>( "TOTAL_VALUE" ) ,
                    NETWeight = clp.Field<String>( "NET_WEIGHT" ) ,
                    GrossWeight = clp.Field<String>( "GROSS_WEIGHT" ) ,
                    CommercialInvoiceNO = clp.Field<String>( "COMMERCIAL_INVOICE_NO" ) ,
                    StoreNO = clp.Field<String>( "STORE_NO" ) ,
                    StoreName = clp.Field<String>( "STORE_NAME" ) ,
                    TotalValueRatio = setdata.Field<Decimal>( "TotalValueRatio" ) ,
                    NetWeightRatio = setdata.Field<Decimal>( "NetWeightRatio" ) ,
                    GrossWeightRatio = setdata.Field<Decimal>( "GrossWeightRatio" ),
                    GroupID= clp.Field<int>( "GroupID" )
                };
            //unionClpSetTable.Rows.Clear( );
            string strTotalValue="0";
            string strNETWeight="0";
            string strGrossWeight="0";
            foreach ( var obj in query1 )
            {
                if ( obj.TotalValue!="" )
                    strTotalValue=obj.TotalValue;
                if ( obj.NETWeight!="" )
                    strNETWeight=obj.NETWeight;
                if ( obj.GrossWeight!="" )
                    strGrossWeight=obj.GrossWeight;
                //decimal a=decimal.Parse( obj.TotalValue )*obj.TotalValueRatio;
                //decimal b=decimal.Parse( obj.NETWeight )*obj.NetWeightRatio;
                //decimal c=decimal.Parse( obj.GrossWeight )*obj.GrossWeightRatio;
                unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                    obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
                    obj.LocalProdectName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Brand ,
                    obj.TypeOfGoods , obj.Price , obj.Currency , ( decimal.Parse( strTotalValue )*obj.TotalValueRatio ).ToString( ) ,
                    ( decimal.Parse( strNETWeight )*obj.NetWeightRatio ).ToString( ) , ( decimal.Parse( strGrossWeight )*obj.GrossWeightRatio ).ToString( ) ,
                    obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName,obj.GroupID );
            }
            unionCLPSETTableClone.AcceptChanges( );
            unionClpSetTable.Clear( );
            unionClpSetTable=unionCLPSETTableClone;
            unionClpSetTable.AcceptChanges( );
            return unionClpSetTable;
        }
        private DataTable UnionCLPSETDataRowGroupDataTotal1( DataTable unionClpSetTable )
        {

            DataTable unionCLPSETTableClone=unionClpSetTable.Clone( );
            var query1 =
                from clp in unionClpSetTable.AsEnumerable( )
                join setdata in _SetBreakUpTable.AsEnumerable( )
                on clp.Field<String>( "中文品名" ) equals setdata.Field<String>( "LocalProdectName" )
                select new
                {
                    ItemCode = clp.Field<String>( "ITEM_CODE" ) ,
                    ShippingNumber = clp.Field<String>( "SHIPPING_NUMBER" ) ,
                    OrderNumber = clp.Field<String>( "ORDER_NUMBER" ) ,
                    PalletNumber = clp.Field<String>( "PALLET_NUMBER" ) ,
                    ParcelNumber = clp.Field<String>( "PARCEL_NUMBER" ) ,
                    ModelCode = clp.Field<String>( "MODEL_CODE" ) ,
                    Origin = clp.Field<String>( "ORIGIN" ) ,
                    Quantity = clp.Field<String>( "QUANTITY" ) ,
                    QuantityUnit = clp.Field<String>( "QUANTITY_UNIT" ) ,
                    DispatchingKey = clp.Field<String>( "DISPATCHING_KEY" ) ,
                    EnglishComposition = clp.Field<String>( "English_Composition" ) ,
                    Size = clp.Field<String>( "SIZE" ) ,
                    Unit = clp.Field<String>( "法定计量单位" ) ,
                    HSCodeInCat = clp.Field<String>( "HS_CODE_(IN_CAT)" ) ,
                    LocalProdectName = clp.Field<String>( "中文品名" ) ,
                    EnglishProductName = clp.Field<String>( "英文品名" ) ,
                    SentialFactor = clp.Field<String>( "申报要素" ) ,
                    SupervisionCondition = clp.Field<String>( "监管条件" ) ,
                    DoubleOrSet = clp.Field<String>( "Double_or_Set" ) ,
                    Brand = clp.Field<String>( "BRAND" ) ,
                    TypeOfGoods = clp.Field<String>( "TYPE_OF_GOODS" ) ,
                    Price = clp.Field<String>( "PRICE" ) ,
                    Currency = clp.Field<String>( "CURRENCY" ) ,
                    TotalValue = clp.Field<String>( "TOTAL_VALUE" ) ,
                    NETWeight = clp.Field<String>( "NET_WEIGHT" ) ,
                    GrossWeight = clp.Field<String>( "GROSS_WEIGHT" ) ,
                    CommercialInvoiceNO = clp.Field<String>( "COMMERCIAL_INVOICE_NO" ) ,
                    StoreNO = clp.Field<String>( "STORE_NO" ) ,
                    StoreName = clp.Field<String>( "STORE_NAME" ) ,
                    TotalValueRatio = setdata.Field<Decimal>( "TotalValueRatio" ) ,
                    NetWeightRatio = setdata.Field<Decimal>( "NetWeightRatio" ) ,
                    GrossWeightRatio = setdata.Field<Decimal>( "GrossWeightRatio" )
                };
            //unionClpSetTable.Rows.Clear( );
            string strTotalValue="0";
            string strNETWeight="0";
            string strGrossWeight="0";
            foreach ( var obj in query1 )
            {
                if ( obj.TotalValue!=null )
                    strTotalValue=obj.TotalValue;
                if ( obj.NETWeight!=null )
                    strNETWeight=obj.NETWeight;
                if ( obj.GrossWeight!=null )
                    strGrossWeight=obj.GrossWeight;
                //decimal a=decimal.Parse( obj.TotalValue )*obj.TotalValueRatio;
                //decimal b=decimal.Parse( obj.NETWeight )*obj.NetWeightRatio;
                //decimal c=decimal.Parse( obj.GrossWeight )*obj.GrossWeightRatio;
                unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                    obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
                    obj.LocalProdectName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Brand ,
                    obj.TypeOfGoods , obj.Price , obj.Currency , ( decimal.Parse( strTotalValue )*obj.TotalValueRatio ).ToString( ) ,
                    ( decimal.Parse( strNETWeight )*obj.NetWeightRatio ).ToString( ) , ( decimal.Parse( strGrossWeight )*obj.GrossWeightRatio ).ToString( ) ,
                    obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName );
            }
            unionCLPSETTableClone.AcceptChanges( );
            unionClpSetTable.Clear( );
            unionClpSetTable=unionCLPSETTableClone;
            unionClpSetTable.AcceptChanges( );
            return unionClpSetTable;
        }
        private DataTable UnionCLPSentialFactorTotalHandle( DataTable sourceDT )
        {
            DataTable tempDT=new DataTable( );
            DataTable copyDT=new DataTable( );
            tempDT=sourceDT.Copy( );
            tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            "DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","监管条件","Double_or_Set","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            //_UnionCLPCommodityInspectionTable=dt.Copy( );
            string strunit=String.Empty;
            foreach ( DataRow row in tempDT.Rows )
            {
                strunit=row["法定计量单位"].ToString( );
                if ( strunit.Contains( '×' ) )
                {
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( strunit.Split( '×' )[1] );
                    row["法定计量单位"]=strunit.Split( '×' )[0];
                }
                if ( row["申报要素"].ToString( ).Contains( ';' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ';' , ' ' ).Trim( );
                }
                if ( row["申报要素"].ToString( ).Contains( ',' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ',' , ' ' ).Trim( );
                }
                row["申报要素"]=getSentialFactorCompareItems( row["申报要素"].ToString( ) );
            }
            int sumQUANTITY=0;
            decimal sumTOTAL_VALUE=0;
            decimal sumNET_WEIGHT=0;
            decimal sumGROSS_WEIGHT=0;
            string strQUANTITY_UNIT=string.Empty;
            for ( int i=0 ; i<tempDT.Rows.Count ; i++ )
            {

                if ( tempDT.Rows[i]["QUANTITY"].ToString( )!=""&& tempDT.Rows[i]["法定计量单位"].ToString( )!="" )
                {
                    //curDR["中文品名"]=tempDT.Rows[i]["中文品名"];
                    //curDR["HS_CODE"]=tempDT.Rows[i]["HS_CODE"];
                    //curDR["申报要素"]=tempDT.Rows[i]["HS_CODE"];
                    sumQUANTITY+=int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );
                    sumTOTAL_VALUE+=decimal.Parse( tempDT.Rows[i]["TOTAL_VALUE"].ToString( ) );
                    sumNET_WEIGHT+=decimal.Parse( tempDT.Rows[i]["NET_WEIGHT"].ToString( ) );
                    sumGROSS_WEIGHT+=decimal.Parse( tempDT.Rows[i]["GROSS_WEIGHT"].ToString( ) );
                    strQUANTITY_UNIT=tempDT.Rows[i]["法定计量单位"].ToString( );
                    //curDR["QUANTITY"]=int.Parse( curDR["QUANTITY"].ToString( ) )+int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );
                    //curDR["QUANTITY"]=int.Parse( curDR["QUANTITY"].ToString( ) )+int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );

                }
                else
                {
                    tempDT.Rows[i]["QUANTITY"]=sumQUANTITY;
                    tempDT.Rows[i]["TOTAL_VALUE"]=sumTOTAL_VALUE;
                    tempDT.Rows[i]["NET_WEIGHT"]=sumNET_WEIGHT;
                    tempDT.Rows[i]["GROSS_WEIGHT"]=sumGROSS_WEIGHT;
                    tempDT.Rows[i]["法定计量单位"]=strQUANTITY_UNIT;
                    sumQUANTITY=0;
                    sumTOTAL_VALUE=0;
                    sumNET_WEIGHT=0;
                    sumGROSS_WEIGHT=0;
                    strQUANTITY_UNIT=string.Empty;
                }

            }
            DataView dv=tempDT.DefaultView;
            dv.Sort="中文品名 ASC,HS_CODE_(IN_CAT) ASC,申报要素 ASC";
            tempDT=dv.ToTable( );
            copyDT=tempDT.Clone( );
            var query = from t in tempDT.AsEnumerable( )
                        group t by new { t1 = t.Field<string>( "中文品名" ) , t2 = t.Field<string>( "HS_CODE_(IN_CAT)" ) , t3 = t.Field<string>( "申报要素" ) } into m
                        select new
                        {
                            中文品名 = m.Key.t1 ,
                            HS_CODE = m.Key.t2 ,
                            申报要素=m.Key.t3.ToString( ) ,
                            //QUANTITY = m.Sum( n => decimal.Parse( n.Field<String>( "QUANTITY" ) )>0? ) ,
                            QUANTITY = m.Sum( n => n.Field<String>( "QUANTITY" )!=""? decimal.Parse( n.Field<String>( "QUANTITY" ) ):0 ) ,
                            //TOTAL_VALUE=m.Sum( n => decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ) ) ,
                            TOTAL_VALUE=m.Sum( n => n.Field<string>( "TOTAL_VALUE" )!=""?decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ):0 ) ,
                            英文品名=m.First( ).Field<string>( "英文品名" ) ,
                            QUANTITY_UNIT = m.First( ).Field<string>( "法定计量单位" ) ,
                            //NET_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ) ) ,
                            NET_WEIGHT=m.Sum( n => n.Field<string>( "NET_WEIGHT" )!=""? decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ):0 ) ,
                            //GROSS_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ) ) ,
                            GROSS_WEIGHT=m.Sum( n => n.Field<string>( "GROSS_WEIGHT" )!=""?decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ):0 )
                        };
            foreach ( var obj in query )
            {
                copyDT.Rows.Add( obj.QUANTITY , obj.QUANTITY_UNIT , obj.HS_CODE , obj.中文品名 , obj.英文品名 , obj.申报要素 , obj.TOTAL_VALUE ,
                     obj.NET_WEIGHT , obj.GROSS_WEIGHT );
            }
            return copyDT;
        }
        private DataTable UnionCLPSentialFactorTotalHandleForSetData( DataTable sourceDT )
        {
            DataTable tempDT=new DataTable( );
            DataTable copyDT=new DataTable( );
            tempDT=sourceDT.Copy( );
            tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            "DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","监管条件","Double_or_Set","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            //_UnionCLPCommodityInspectionTable=dt.Copy( );
            string strunit=String.Empty;
            foreach ( DataRow row in tempDT.Rows )
            {
                strunit=row["法定计量单位"].ToString( );
                if ( strunit.Contains( '×' ) )
                {
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( strunit.Split( '×' )[1] );
                    row["法定计量单位"]=strunit.Split( '×' )[0];
                }
                if ( row["申报要素"].ToString( ).Contains( ';' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ';' , ' ' ).Trim( );
                }
                if ( row["申报要素"].ToString( ).Contains( ',' ) )
                {
                    row["申报要素"]=row["申报要素"].ToString( ).Replace( ',' , ' ' ).Trim( );
                }
                row["申报要素"]=getSentialFactorCompareItems( row["申报要素"].ToString( ) );
            }
            //int sumQUANTITY=0;
            //decimal sumTOTAL_VALUE=0;
            //decimal sumNET_WEIGHT=0;
            //decimal sumGROSS_WEIGHT=0;
            //string strQUANTITY_UNIT=string.Empty;
            //for ( int i=0 ; i<tempDT.Rows.Count ; i++ )
            //{

            //    if ( tempDT.Rows[i]["QUANTITY"].ToString( )!=""&& tempDT.Rows[i]["法定计量单位"].ToString( )!="" )
            //    {
            //        //curDR["中文品名"]=tempDT.Rows[i]["中文品名"];
            //        //curDR["HS_CODE"]=tempDT.Rows[i]["HS_CODE"];
            //        //curDR["申报要素"]=tempDT.Rows[i]["HS_CODE"];
            //        sumQUANTITY+=int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );
            //        sumTOTAL_VALUE+=decimal.Parse( tempDT.Rows[i]["TOTAL_VALUE"].ToString( ) );
            //        sumNET_WEIGHT+=decimal.Parse( tempDT.Rows[i]["NET_WEIGHT"].ToString( ) );
            //        sumGROSS_WEIGHT+=decimal.Parse( tempDT.Rows[i]["GROSS_WEIGHT"].ToString( ) );
            //        strQUANTITY_UNIT=tempDT.Rows[i]["法定计量单位"].ToString( );
            //        //curDR["QUANTITY"]=int.Parse( curDR["QUANTITY"].ToString( ) )+int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );
            //        //curDR["QUANTITY"]=int.Parse( curDR["QUANTITY"].ToString( ) )+int.Parse( tempDT.Rows[i]["QUANTITY"].ToString( ) );

            //    }
            //    else
            //    {
            //        tempDT.Rows[i]["QUANTITY"]=sumQUANTITY;
            //        tempDT.Rows[i]["TOTAL_VALUE"]=sumTOTAL_VALUE;
            //        tempDT.Rows[i]["NET_WEIGHT"]=sumNET_WEIGHT;
            //        tempDT.Rows[i]["GROSS_WEIGHT"]=sumGROSS_WEIGHT;
            //        tempDT.Rows[i]["法定计量单位"]=strQUANTITY_UNIT;
            //        sumQUANTITY=0;
            //        sumTOTAL_VALUE=0;
            //        sumNET_WEIGHT=0;
            //        sumGROSS_WEIGHT=0;
            //        strQUANTITY_UNIT=string.Empty;
            //    }

            //}

            //DataView dv=tempDT.DefaultView;
            //dv.Sort="中文品名 ASC,HS_CODE_(IN_CAT) ASC,申报要素 ASC";
            //tempDT=dv.ToTable( );
            //copyDT=tempDT.Clone( );
            //var query = from t in tempDT.AsEnumerable( )
            //            group t by new { t1 = t.Field<string>( "中文品名" ) , t2 = t.Field<string>( "HS_CODE_(IN_CAT)" ) , t3 = t.Field<string>( "申报要素" ) } into m
            //            select new
            //            {
            //                中文品名 = m.Key.t1 ,
            //                HS_CODE = m.Key.t2 ,
            //                申报要素=m.Key.t3.ToString( ) ,
            //                //QUANTITY = m.Sum( n => decimal.Parse( n.Field<String>( "QUANTITY" ) )>0? ) ,
            //                QUANTITY = m.Sum( n => n.Field<String>( "QUANTITY" )!=""? decimal.Parse( n.Field<String>( "QUANTITY" ) ):0 ) ,
            //                //TOTAL_VALUE=m.Sum( n => decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ) ) ,
            //                TOTAL_VALUE=m.Sum( n => n.Field<string>( "TOTAL_VALUE" )!=""?decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ):0 ) ,
            //                英文品名=m.First( ).Field<string>( "英文品名" ) ,
            //                QUANTITY_UNIT = m.First( ).Field<string>( "QUANTITY_UNIT" ) ,
            //                //NET_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ) ) ,
            //                NET_WEIGHT=m.Sum( n => n.Field<string>( "NET_WEIGHT" )!=""? decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ):0 ) ,
            //                //GROSS_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ) ) ,
            //                GROSS_WEIGHT=m.Sum( n => n.Field<string>( "GROSS_WEIGHT" )!=""?decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ):0 )
            //            };
            //foreach ( var obj in query )
            //{
            //    copyDT.Rows.Add( obj.QUANTITY , obj.QUANTITY_UNIT , obj.HS_CODE , obj.中文品名 , obj.英文品名 , obj.申报要素 , obj.TOTAL_VALUE ,
            //         obj.NET_WEIGHT , obj.GROSS_WEIGHT );
            //}
            return tempDT;
        }
        private Dictionary<List<DataRow> , List<DataRow>> SetDataGourpCompare( Dictionary<List<DataRow> , List<DataRow>> dic )
        {
            Dictionary<List<DataRow> , List<DataRow>> dicresult = new Dictionary<List<DataRow> , List<DataRow>>( );
            List<DataRow> KVkey=new List<DataRow>();
            List<DataRow> KVvalue=new List<DataRow>();
            //bool flag=false;
            //List<List<DataRow>> havekeyLst=new List<List<DataRow>>( );
            foreach ( KeyValuePair<List<DataRow> , List<DataRow>> kv in dic )
            {
                KVkey=kv.Key;
                KVvalue=kv.Value;
                if ( dicresult.Count==0 )
                {
                    dicresult.Add( KVkey , KVvalue );
                }
                else
                {
                    List<List<DataRow>> lst = new List<List<DataRow>>( dicresult.Keys );
                    for ( int i=0 ; i<=dicresult.Count-1 ; i++ )
                    {
                        if ( UnionCLPSETDataRowGroupCompare( lst[i] , kv.Key ) )
                        {
                            if ( UnionCLPSETDataRowGroupCompare( dicresult[lst[i]] , kv.Value ) )
                            {
                                //flag=true;
                                KVkey=UnionCLPSETDataRowGroup( lst[i] , kv.Key );
                                KVvalue=UnionCLPSETDataRowGroup( dicresult[lst[i]] , kv.Value );
                                dicresult.Remove( lst[i] );
                                break;
                            }
                        }
                    }
                    dicresult.Add( KVkey , KVvalue );

                    ////flag=false;
                    //if ( dic.Count>2 )
                    //{
                    //    List<List<DataRow>> lst = new List<List<DataRow>>( dicresult.Keys );
                    //    for ( int i=0 ; i<=dicresult.Count-1 ; i++ )
                    //    {
                    //        if ( UnionCLPSETDataRowGroupCompare( lst[i] , kv.Key ) )
                    //        {
                    //            if ( UnionCLPSETDataRowGroupCompare( dicresult[lst[i]] , kv.Value ) )
                    //            {
                    //                //flag=true;
                    //                KVkey=UnionCLPSETDataRowGroup( lst[i] , kv.Key );
                    //                KVvalue=UnionCLPSETDataRowGroup( dicresult[lst[i]] , kv.Value );
                    //                dicresult.Remove( lst[i] );
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    dicresult.Add( KVkey , KVvalue );
                    //}
                    //else
                    //{
                    //    if ( UnionCLPSETDataRowGroupCompare( KVkey , kv.Key ) )
                    //    {
                    //        if ( UnionCLPSETDataRowGroupCompare( KVvalue , kv.Value ) )
                    //        {
                    //            //flag=true;
                    //            KVkey=UnionCLPSETDataRowGroup( KVkey , kv.Key );
                    //            KVvalue=UnionCLPSETDataRowGroup( KVvalue , kv.Value );
                    //            dicresult.Remove( kv.Key );
                    //            dicresult.Add( KVkey , KVvalue );
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
            //Dictionary<List<DataRow> , List<DataRow>> dicresult11=dicresult;
            return dicresult;
        }

        private void UnionCLPColumnSequenceAdjust( DataTable sourceDT )
        {
            sourceDT.Columns["中文品名"].SetOrdinal( 0 );
            sourceDT.Columns["HS_CODE_(IN_CAT)"].SetOrdinal( 1 );
            sourceDT.Columns["申报要素"].SetOrdinal( 2 );
            sourceDT.Columns["QUANTITY"].SetOrdinal( 3 );
            sourceDT.Columns["TOTAL_VALUE"].SetOrdinal( 4 );
            sourceDT.Columns["英文品名"].SetOrdinal( 5 );
            sourceDT.Columns["法定计量单位"].SetOrdinal( 6 );
            sourceDT.Columns["NET_WEIGHT"].SetOrdinal( 7 );
            sourceDT.Columns["GROSS_WEIGHT"].SetOrdinal( 8 );
            sourceDT.AcceptChanges( );
        }
        private void btnUnionCLPInput_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPInput;
            Flag=false;
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
                        foreach ( DataRow dr in _CLPTable.Rows )
                        {
                            if (dr["Double_or_Set"].ToString( )=="0" )
                                dr["Double_or_Set"]="N";
                        }
                        SupervisionConditionForUnionCLP( _CLPTable );
                        //_CLPTable=ExcelRender.RenderFromExcel( ms , 0 , 0 ).Select( string.Format( "中文品名 <> ''" ) ).CopyToDataTable( );
                        //DataTable dt=_CLPTable.Select( string.Format( "中文品名 <> ''" ) ).CopyToDataTable( );
                        _CLPTable.TableName=filename.Substring( filename.LastIndexOf( '\\' )+1 );
                        ds.Tables.Add( _CLPTable );
                    }

                }
                _ParcelNumberDataSet.Clear( );
                _ParcelNumberDataSet=ds;
                SetSubForm( "FrmTotalCLPHandle" );
                //toolStripStatusLabel1.Text="原始CLP成功导入！";
                toolStripStatusLabel1.Text="操作阶段：汇总CLP导入";
                toolStripStatusLabel2.Text="操作结果：汇总CLP文件成功导入！";
                //MessageBox.Show( "原始CLP成功导入！" );

            }
        }

        private void btnUnionCLPTable_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPHandle;
            Flag=true;
            _UnionCLPTotalTable=new DataTable( );
            if ( ParcelNumberDataSet.Tables.Count==0 )
            {
                MessageBox.Show( "请先进行汇总CLP导入！" );
                return;
            }
            _UnionCLPTotalTable=ParcelNumberDataSet.Tables[0].Clone( );
            foreach ( DataTable dt in ParcelNumberDataSet.Tables )
            {
                foreach ( DataRow dr in dt.Rows )
                {
                    _UnionCLPTotalTable.ImportRow( dr );
                    //if ( dr["法定计量单位"].ToString( )!=""&&dr["HS_CODE_(IN_CAT)"].ToString( )!=""&&dr["中文品名"].ToString( )!=""&& dr["申报要素"].ToString( )!="" )
                    //    _UnionCLPTotalTable.ImportRow( dr );
                }
            }
            _UnionCLPTotalTable.TableName="CLP初始";
            _UnionCLPTotalTable.AcceptChanges( );
            //_UnionCLPTotalTable=UnionCLPDeleteColumnAndSentialFactorHandle( _UnionCLPTotalTable );
            _ParcelNumberDataSet=SubTotalForUnionCLPTable( _UnionCLPTotalTable );
            SetSubForm( "FrmTotalCLPHandle" );
            toolStripStatusLabel1.Text="操作阶段：合并CLP文件";
            toolStripStatusLabel2.Text="操作结果：成功合并CLP文件！";
            //toolStripStatusLabel1.Text="成功合并CLP文件！";
            //MessageBox.Show( "原始CLP成功导入！" );

        }

        private void btnDealSet_Click( object sender , EventArgs e )
        {
            if ( !Flag )
            {
                MessageBox.Show( "处理SET数据之前请先合并CLP！" );
                return;
            }
            this.operateType=OperateType.UnionCLPSET;
            _UnionCLPSETTable.Columns.Add( "GroupID" , typeof( int ) );
            DataTable _SetBreakUpCloneTable=new DataTable( );
            DataTable _UnionCLPSETCloneTable=new DataTable( );
            _UnionCLPSETTotalTable=new DataTable( );
            _UnionCLPSETTotalTable=_UnionCLPSETTable.Clone( );            
            _UnionCLPSETCloneTable=UnionCLPSETTable.Clone( );

            _SetBreakUpCloneTable=_SetBreakUpTable.Clone( );
            Dictionary<List<DataRow> , List<DataRow>> dic=new Dictionary<List<DataRow> , List<DataRow>>( );
            Dictionary<List<DataRow> , List<DataRow>> DicClone=new Dictionary<List<DataRow> , List<DataRow>>( );
            //计算分组
            dic=UnionCLPSetTableSentialFactorTotalHandle( _UnionCLPSETTable );
            //通过分组数据获取SET划分数据
            _SetBreakUpCloneTable=GetSetBreakUpDataRowGroupDataByCLPSetGroup( dic );
            //通过分组的各类比重计算价值、毛重、净重
            _UnionCLPSETTable=UnionCLPSETDataRowGroupDataTotal( _UnionCLPSETTable , _SetBreakUpCloneTable );
            //_UnionCLPSETTable.Columns.Remove( "GroupID" );
            //_UnionCLPSETTable.AcceptChanges( );
            foreach ( DataRow row in _UnionCLPSETTable.Rows )
            {
                if ( row["NET_WEIGHT"].ToString( )!="" )
                    row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( row["GROSS_WEIGHT"].ToString( )!="" )
                    row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
            }
            dic=UnionCLPSetTableSentialFactorTotalHandle( _UnionCLPSETTable );
            //下面进行SET数据合并
            DicClone=dic;
            dic=SetDataGourpCompare( DicClone );
            //UnionCLPSentialFactorTotalHandle11( dic );
            _UnionCLPSETCloneTable.Clear( );
            foreach ( KeyValuePair<List<DataRow> , List<DataRow>> kv in dic )
            {
                foreach ( DataRow dr in kv.Key )
                {
                    _UnionCLPSETCloneTable.ImportRow( dr );
                }
                foreach ( DataRow dr in kv.Value )
                {
                    _UnionCLPSETCloneTable.ImportRow( dr );
                }
            }
            _UnionCLPSETCloneTable.Columns.Remove( "GroupID" );
            _UnionCLPSETCloneTable.AcceptChanges( );
            _UnionCLPSETTable.Columns.Remove( "GroupID" );
            _UnionCLPSETTable.AcceptChanges( );
            _UnionCLPSETTotalTable=UnionCLPSentialFactorTotalHandleForSetData( _UnionCLPSETCloneTable );
            UnionCLPColumnSequenceAdjust( _UnionCLPSETTotalTable );
            _UnionCLPSETTotalTable.TableName="SET汇总";
            //_ParcelNumberDataSet.Tables
            //DataTable tempdt=_UnionCLPSETTable.Copy( );
            //_UnionCLPSETTotalTable.Columns["中文品名"].SetOrdinal( 0 );
            //_UnionCLPSETTotalTable.Columns["HS_CODE_(IN_CAT)"].SetOrdinal( 1 );
            //_UnionCLPSETTotalTable.Columns["申报要素"].SetOrdinal( 2 );
            //_UnionCLPSETTotalTable.Columns["QUANTITY"].SetOrdinal( 3 );
            //_UnionCLPSETTotalTable.Columns["TOTAL_VALUE"].SetOrdinal( 4 );
            //_UnionCLPSETTotalTable.Columns["英文品名"].SetOrdinal( 5 );
            //_UnionCLPSETTotalTable.Columns["QUANTITY_UNIT"].SetOrdinal( 6 );
            //_UnionCLPSETTotalTable.Columns["NET_WEIGHT"].SetOrdinal( 7 );
            //_UnionCLPSETTotalTable.Columns["GROSS_WEIGHT"].SetOrdinal( 8 );
            //_UnionCLPSETTotalTable.AcceptChanges( );
            //this.operateType=OperateType.DorbleOrSet;
            _ParcelNumberDataSet.Tables.Clear( );
            //_ParcelNumberDataSet.Tables.Add( _UnionCLPTotalTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPSETTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPSETTotalTable );
            SetSubForm( "FrmMulSheet" );
            //toolStripStatusLabel1.Text="成功进行Set数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理SET数据";
            toolStripStatusLabel2.Text="操作结果：成功进行Set数据处理！";
        }

        private void btnDealInspection_Click( object sender , EventArgs e )
        {
            if ( !Flag )
            {
                MessageBox.Show( "处理商检数据之前请先合并CLP！" );
                return;
            }
            this.operateType=OperateType.UnionCLPCommodityInspection;
            _UnionCLPCommodityInspectionTotalTable=new DataTable( );
            _UnionCLPCommodityInspectionTotalTable=_UnionCLPCommodityInspectionTable.Clone( );
            _UnionCLPCommodityInspectionTotalTable=UnionCLPSentialFactorTotalHandle( _UnionCLPCommodityInspectionTable );
            UnionCLPColumnSequenceAdjust( _UnionCLPCommodityInspectionTotalTable );
            _UnionCLPCommodityInspectionTotalTable.TableName="商检汇总";
            _ParcelNumberDataSet.Tables.Clear( );
            //_ParcelNumberDataSet.Tables.Add( _UnionCLPTotalTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPCommodityInspectionTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPCommodityInspectionTotalTable );
            SetSubForm( "FrmMulSheet" );
            //toolStripStatusLabel1.Text="成功进行商检数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理商检数据";
            toolStripStatusLabel2.Text="操作结果：成功进行商检数据处理！";

        }

        private void btnDealNonInspection_Click( object sender , EventArgs e )
        {
            if ( !Flag )
            {
                MessageBox.Show( "处理非商检数据之前请先合并CLP！" );
                return;
            }
            this.operateType=OperateType.UnionCLPNonCommodityInspection;
            _UnionCLPNonCommodityInspectionTotalTable=new DataTable( );
            _UnionCLPNonCommodityInspectionTotalTable=_UnionCLPNonCommodityInspectionTable.Clone( );
            _UnionCLPNonCommodityInspectionTotalTable=UnionCLPSentialFactorTotalHandle( _UnionCLPNonCommodityInspectionTable );
            UnionCLPColumnSequenceAdjust( _UnionCLPNonCommodityInspectionTotalTable );
            _UnionCLPNonCommodityInspectionTotalTable.TableName="非商检汇总";
            _ParcelNumberDataSet.Tables.Clear( );
            //_ParcelNumberDataSet.Tables.Add( _UnionCLPTotalTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPNonCommodityInspectionTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPNonCommodityInspectionTotalTable );
            SetSubForm( "FrmMulSheet" );
            //toolStripStatusLabel1.Text="成功进行非商检数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理非商检数据";
            toolStripStatusLabel2.Text="操作结果：成功进行非商检数据处理！";
        }

        private void btnUnionCLPTotalOutput_Click( object sender , EventArgs e )
        {
            if ( !Directory.Exists( UnionCLPPath ) )
                Directory.CreateDirectory( UnionCLPPath );
            //string fullfilepath=UnionCLPPath+_fileName;
            if ( this.operateType==OperateType.UnionCLPInput||this.operateType==OperateType.UnionCLPHandle||this.operateType==OperateType.UnionCLPSET 
                ||this.operateType==OperateType.UnionCLPCommodityInspection|| this.operateType==OperateType.UnionCLPNonCommodityInspection)
            {
                string filenames=string.Empty;
                string fileFullPath=string.Empty;
                foreach ( DataTable dt in _ParcelNumberDataSet.Tables )
                {
                    if ( dt.Rows.Count>0 )
                    {
                        //sheetname+=dt.TableName+",";
                        filenames+=dt.TableName+",";
                        fileFullPath=UnionCLPPath+dt.TableName;
                        if ( !( UnionCLPPath+dt.TableName ).EndsWith( ".xls" )&&!( UnionCLPPath+dt.TableName ).EndsWith( ".xlsx" ) )
                            fileFullPath+=".xls";
                        if ( File.Exists( fileFullPath ) )
                        {
                            try
                            {
                                File.Delete( fileFullPath );
                            }
                            catch ( Exception ex )
                            {
                                MessageBox.Show( "请将相关的EXCEL文件关闭后重新进行操作！" );
                                return;
                            }
                        }
                        ExcelRender.RenderToExcel( dt , fileFullPath );
                    }
                }
                //sheetname=sheetname.Substring( 0 , sheetname.Length-1 );
                //ExcelRender.RenderToExcel( ParcelNumberDataSet , sheetname , localFilePath );
                //ExcelRender.RenderToExcel( BasicDataTable , localFilePath );
                toolStripStatusLabel1.Text="操作阶段：汇总CLP导出";
                toolStripStatusLabel2.Text="操作结果：汇总处理阶段CLP文件成功导出！";
                MessageBox.Show( "汇总处理阶段CLP文件成功导出！" );
            }

        }
        #endregion








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
