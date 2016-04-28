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
using System.Text.RegularExpressions;

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
        UnionCLPForSetUpBill=11,
        UnionCLPForParcelNumber=12,
        UnionCLPForCureNumber=13
    }
    public partial class FrmMain : Form
    {
        private Form _CurrentMdiChild;
        public OperateType operateType;
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
        /// <summary>
        /// 分票数据存储
        /// </summary>
        private DataTable _UnionCLPSETTotalCloneForBillTable;
        public DataTable UnionCLPSETTotalCloneForBillTable
        {
            get { return _UnionCLPSETTotalCloneForBillTable; }
            set { _UnionCLPSETTotalCloneForBillTable=value; }
        }
        /// <summary>
        /// 净重调整
        /// </summary>
        private DataTable _NetWeightAdjustmentTable;
        public DataTable NetWeightAdjustmentTable
        {
            get { return _NetWeightAdjustmentTable; }
            set { _NetWeightAdjustmentTable=value; }
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
            _ParcelSetUpTable=new DataTable( );
            _NetWeightAdjustmentTable=new DataTable( );
            _CubeNumberAdjustmentTable=new DataTable( );
            _CLPBasicDataTable=basicDataBLL.GetDistinctModeCodeList( ).Tables[0];
            _RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
            _SentialFactors=sftBLL.GetModelList( "" );
            _SetBreakUpTable=sbuBll.GetAllList( ).Tables[0];
            _ParcelSetUpTable=new ParcelSetUpBLL( ).GetAllList( ).Tables[0];
            _NetWeightAdjustmentTable=new NetWeightAdjustmentBLL( ).GetAllList( ).Tables[0];
            _CubeNumberAdjustmentTable=new CubeNumberAdjustmentBLL( ).GetAllList( ).Tables[0];
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
                    if ( this.operateType==OperateType.UnionCLPForSetUpBill )
                    {
                        object[] args = new object[] { "制作发票箱单【第一步：处理【分票数据处理】" , OperateType.UnionCLPForSetUpBill , _UnionCLPSETTotalCloneForBillTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPForParcelNumber )
                    {
                        object[] args = new object[] { "制作发票箱单【第二步：处理【配箱数据处理】" , OperateType.UnionCLPForParcelNumber , _BillSmallClassNetWeightTable };
                        Form frm = (Form)Activator.CreateInstance( t , args );
                        ShowMdiChild( frm );
                    }
                    if ( this.operateType==OperateType.UnionCLPForCureNumber )
                    {
                        object[] args = new object[] { "制作发票箱单【第三步：处理【配立方数数据处理】" , OperateType.UnionCLPForCureNumber , _BillSmallClassNetWeightTable };
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
                    RemoveEmpty( _OriginalCLPTable );
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
                string errMsg=string.Empty;
                foreach ( DataRow dr in _OriginalCLPTable.Rows )
                {
                    if ( decimal.Parse( dr["QUANTITY"].ToString( ) )<=0 )
                    {
                        errMsg+=string.Format( "MODELCODE为{0}的数量不能小于等于0.\r\n" , dr["MODEL_CODE"].ToString( ) );
                    }
                    if ( decimal.Parse( dr["TOTAL_VALUE"].ToString( ) )<=0 )
                    {
                        errMsg+=string.Format( "MODELCODE为{0}的金额不能小于等于0.\r\n" , dr["MODEL_CODE"].ToString( ) );
                    }
                    if(decimal.Parse( dr["GROSS_WEIGHT"].ToString( ))<=0 )
                    {
                        errMsg+=string.Format( "MODELCODE为{0}的毛重不能小于等于0.\r\n" , dr["MODEL_CODE"].ToString( ) );
                    }
                    if(decimal.Parse( dr["NET_WEIGHT"].ToString( ) )<=0)
                    {
                        errMsg+=string.Format( "MODELCODE为{0}的净重不能小于等于0.\r\n" , dr["MODEL_CODE"].ToString( ) );
                    }
                    if ( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) )-decimal.Parse( dr["NET_WEIGHT"].ToString( ) )<=0 )
                    {
                        errMsg+=string.Format( "MODELCODE为{0}的净毛重之差不能小于等于0.\r\n" , dr["MODEL_CODE"].ToString( ) );
                    }
                }
                if(errMsg!=string.Empty)
                    MessageBox.Show( errMsg );

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
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString();
            toolStripStatusLabel2.Text="操作结果：多余列成功删除！";
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
            DoubleOperateForUnionCLP( _DoubleOrSetTable , ref errmsg );
            if ( errmsg!="" )
            {
                MessageBox.Show( errmsg );
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
            _ParcelNumberDataSet.AcceptChanges( );
            this.operateType=OperateType.FilterSupervisionCondition;
            SetSubForm( "FrmOriginalCLPParcelTakeOut" );
            toolStripStatusLabel1.Text="操作文件："+_fileName.ToString( );
            toolStripStatusLabel2.Text="操作结果：筛选监管条件完成！";

        }

        private DataTable DoubleOrSetTableItemModelDataFill( DataTable _dosTable )
        {
            DataTable tempDT=new DataTable( );
            tempDT=_dosTable.Copy( );
            string itemcode=string.Empty;
            string modelcode=string.Empty;
            string QUANTITY=string.Empty;
            string PARCEL_NUMBER=string.Empty;
            string SHIPPING_NUMBER=string.Empty;
            string ORDER_NUMBER=string.Empty;
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
                if ( dr["SHIPPING_NUMBER"].ToString( )!="" )
                    SHIPPING_NUMBER=dr["SHIPPING_NUMBER"].ToString( );
                else
                    dr["SHIPPING_NUMBER"]=SHIPPING_NUMBER;
                if ( dr["ORDER_NUMBER"].ToString( )!="" )
                    ORDER_NUMBER=dr["ORDER_NUMBER"].ToString( );
                else
                    dr["ORDER_NUMBER"]=ORDER_NUMBER;
            }
            return tempDT;
        }
        private DataTable ParcelTakeOutForHandSocks( DataTable totalDT , ref DataTable _notakeoutDT , ref DataTable _takeoutDT )
        {
            DataTable resultDT=new DataTable( );
            DataTable tempDT=totalDT.Copy( );
            int intFlag;
            foreach ( DataRow dr in totalDT.Rows )
            {
                intFlag=0;//intFlag=1为掏箱 intFlag=2为出运
                if ( dr["Double_or_Set"].ToString( ).ToUpper().Contains( "DOUBLE" )&& dr["中文品名"].ToString( ).Contains( "袜" ) )
                {
                    string strSize=dr["SIZE"].ToString( );
                    if ( strSize!="" )
                    {
                        if ( strSize.Contains( "EU" ) )
                        {
                            int result;
                            string strtemp=strSize.Split( new[] { "EU" } , StringSplitOptions.RemoveEmptyEntries )[0];
                            strtemp=strtemp.Split( new[] { "UK" } , StringSplitOptions.RemoveEmptyEntries )[0];
                            if ( strtemp.Contains( '-' ) )
                            {
                                string[] strs=strtemp.Split( '-' );
                                result=0;
                                if ( int.TryParse( strs[0] , out result ) )
                                {
                                    if ( result>=23 )
                                        intFlag=1;
                                }
                                result=0;
                                if ( int.TryParse( strs[1] , out result ) )
                                {
                                    if ( result<=22 )
                                        intFlag=2;
                                }
                            }
                            else if ( strtemp.Contains( '/' ) )
                            {
                                string[] strs=strtemp.Split( '/' );
                                result=0;
                                if ( int.TryParse( strs[0] , out result ) )
                                {
                                    if ( result>=23 )
                                        intFlag=1;
                                }
                                result=0;
                                if ( int.TryParse( strs[1] , out result ) )
                                {
                                    if ( result<=22 )
                                        intFlag=2;
                                }
                            }
                            else
                            {
                                if ( int.TryParse( strtemp , out result ) )
                                {
                                    if ( result<=22 )
                                    {
                                        intFlag=2;
                                    }
                                    else if ( result>=23 )
                                    {
                                        intFlag=1;
                                    }
                                }
                            }
                        }
                    }
                }
                if ( intFlag>0 )
                {
                    DataRow[] itemcodes=tempDT.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                    foreach ( DataRow row in itemcodes )
                    {
                        if ( intFlag==1 )
                        {
                            _takeoutDT.Rows.Add( row.ItemArray );
                            tempDT.Rows.Remove( row );
                        }
                        else if ( intFlag==2 )
                        {
                            _notakeoutDT.Rows.Add( row.ItemArray );
                            tempDT.Rows.Remove( row );
                        }
                        tempDT.AcceptChanges( );
                    }
                }
            }
            DataView dv=_takeoutDT.DefaultView;
            _takeoutDT=dv.ToTable( true );
            DataView dv1=_notakeoutDT.DefaultView;
            _notakeoutDT=dv1.ToTable( true );
            return tempDT;
        }
        private DataTable ParcelTakeOutForHandleHelmet( DataTable totalDT , ref DataTable _notakeoutDT , ref DataTable _takeoutDT )
        {
            DataRow[] drs;
            string stritemCode=string.Empty;
            string strmodeCode=string.Empty;
            string strunit=string.Empty;
            int sum;
            //DataTable totalDT=_dosTable.Copy( );
            DataTable resultDT=new DataTable( );
            DataTable tempDT=totalDT.Copy( );
            //resultDT=totalDT.Clone( );
            //totalDT=DoubleOrSetTableItemModelDataFill( _dosTable );
            //tempDT=totalDT;
            foreach ( DataRow dr in totalDT.Rows )
            {
                drs=_ParcelTakeOutConditionTable.Select( "ItemCode='"+dr["Item_Code"].ToString( )+"' AND ModelCode='"+dr["Model_code"].ToString( )+"' AND LocalProductName='"+dr["中文品名"].ToString( )+"'" );
                if ( drs.Length>0 )
                {
                    sum=0;
                    DataRow[] itemcodes=tempDT.Select( "ITEM_CODE='"+dr["ITEM_CODE"].ToString( )+"'" );
                    foreach ( DataRow row in itemcodes )
                    {
                        strunit=row["法定计量单位"].ToString( );
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
                            List<IGrouping<string , DataRow>> parcelnumberGroups=itemcodes.AsEnumerable( ).GroupBy( m => m.Field<string>( "PARCEL_NUMBER" ) , m => m ).ToList( );
                            foreach ( IGrouping<string , DataRow> igv in parcelnumberGroups )
                            {
                                DataRow[] parcelnumber=tempDT.Select( "PARCEL_NUMBER='"+igv.Key+"'" );
                                foreach ( DataRow row in parcelnumber )
                                {
                                    _takeoutDT.Rows.Add( row.ItemArray );
                                    tempDT.Rows.Remove( row );
                                }
                                tempDT.AcceptChanges( );                                                                
                                //igv.Key
                            }
                            //List<IGrouping<int , DataRow>> setbreakupGroups=setBreakUpCloneTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "GroupID" ) , m => m ).ToList( );
                            //tempDT.AcceptChanges( );
                        }                        
                    }
                }
            }
            DataView dv=_takeoutDT.DefaultView;
            _takeoutDT=dv.ToTable( true );
            DataView dv1=_notakeoutDT.DefaultView;
            _notakeoutDT=dv1.ToTable( true );
            return tempDT;
            //if ( totalDT.Rows.Count>tempDT.Rows.Count )
            //{
            //    resultDT=totalDT.AsEnumerable( ).Except( tempDT.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( );
            //    return resultDT;
            //}
            //else
            //    return totalDT;
        }
        private void ParcelTakeOut( DataTable _dosTable , ref DataTable _notakeoutDT , ref DataTable _takeoutDT )
        {
            //DataRow[] drs;
            //string stritemCode=string.Empty;
            //string strmodeCode=string.Empty;
            //string strunit=string.Empty;
            //int sum;
            DataTable totalDT=_dosTable.Clone( );
            DataTable resultDT=_dosTable.Clone( );
            DataTable tempDT=_dosTable.Clone( );
            totalDT=DoubleOrSetTableItemModelDataFill( _dosTable );
            //处理袜子EU<=22 婴儿 出运 EU>=23 成人 掏箱
            resultDT=ParcelTakeOutForHandSocks( totalDT , ref _notakeoutDT , ref _takeoutDT );
            //tempDT=totalDT;
            //处理头盔类MODEL CODE
            resultDT=ParcelTakeOutForHandleHelmet( resultDT , ref _notakeoutDT , ref _takeoutDT );
            resultDT.AcceptChanges( );
            tempDT=resultDT.Copy();
            //掏箱表中符合条件的直接掏箱
            foreach ( DataRow dr in resultDT.Rows )
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
                    tempDT.AcceptChanges( );
                }
            }
            resultDT=tempDT.Copy();
            resultDT.AcceptChanges( );
            //资料类型NORMAL、监管条件为B的直接掏箱
            foreach ( DataRow dr in resultDT.Rows )
            {
                if ( ( _CubeNumberAdjustmentTable.Select( "ShippingNumber='"+dr["SHIPPING_NUMBER"].ToString( )+"' and ConfirmType='normal'" ).Length>0 )&&( dr["监管条件"].ToString( ).Contains( 'B' ) ) )
                {
                    DataRow[] parcelnumber=tempDT.Select( "PARCEL_NUMBER='"+dr["PARCEL_NUMBER"].ToString( )+"'" );
                    foreach ( DataRow row in parcelnumber )
                    {
                        _takeoutDT.Rows.Add( row.ItemArray );
                        tempDT.Rows.Remove( row );
                    }
                    tempDT.AcceptChanges( );
                }
            }
            if ( _takeoutDT.Rows.Count>0 )
                _notakeoutDT=totalDT.AsEnumerable( ).Except( _takeoutDT.AsEnumerable( ) , DataRowComparer.Default ).CopyToDataTable( );
            else
                _notakeoutDT=totalDT;
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
                model.LocalProductName=row["中文品名"].ToString( );
                model.EnglishProductName =row["英文品名"].ToString( );
                model.SentialFactor=row["申报要素"].ToString( );
                //model.ModelCode=row["MODEL CODE"].ToString( );
                model.SupervisionCondition =row["监管条件"].ToString( );
                model.DoubleOrSet=row["Double or Set"].ToString( );
                model.Description=row["备注"].ToString( );
                model.ExaminingReport=row["检测报告号"].ToString( );
                //model.ModelCodeDescription="";
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
        #region 基础信息操作
        private bool ExcelDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=11 )
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
            if ( excelDT.Columns[8].Caption!="备注" )
            {
                errMsg="基础资料表第九列为备注，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[9].Caption!="现检测报告号" )
            {
                errMsg="基础资料表第十列为现检测报告号，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[10].Caption!="SIZE" )
            {
                errMsg="基础资料表第十一列为SIZE，请核对后重新导入！";
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
                    RemoveEmpty( dt );
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
            _OriginalCLPTotalTable.Columns.Add( "备注" , typeof( String ) );
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
                    Unit = basicdata.Field<String>( "QuantityUnit" ) ,
                    HSCodeInCat = basicdata.Field<String>( "HSCodeInCat" ) ,
                    //ModelCodeDescription = basicdata.Field<String>( "ModelCodeDescription" ) ,
                    LocalProductName = basicdata.Field<String>( "LocalProductName" ) ,
                    EnglishProductName = basicdata.Field<String>( "EnglishProductName" ) ,
                    SentialFactor = basicdata.Field<String>( "SentialFactor" ) ,
                    SupervisionCondition = basicdata.Field<String>( "SupervisionCondition" ) ,
                    DoubleOrSet = basicdata.Field<String>( "DoubleOrSet" ) ,
                    Description = basicdata.Field<String>( "Description" ) ,
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
                    obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Description , obj.Brand ,
                    obj.TypeOfGoods , obj.Price , obj.Currency , obj.TotalValue , decimal.Round( decimal.Parse( obj.NETWeight ) , 2 ) , decimal.Round( decimal.Parse( obj.GrossWeight ) , 2 ) , obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName );
            }
            _OriginalCLPTotalTable.AcceptChanges( );
        }
        /// <summary>
        /// 区分成人与婴儿
        /// </summary>
        /// <param name="row"></param>
        /// <returns>0:其他；1：成人；2：婴儿</returns>
        //private int IsAdultOrBabyBySize( DataRow row )
        //{
        //    int result=0;
        //    int flag=0;
        //    string strSize=row["SIZE"].ToString( ).ToUpper( ).Trim( );
        //    if ( strSize.Contains( "MONTHS" ) )
        //    {

        //        if ( int.TryParse( strSize.Split( ' ' )[0] , out result ) )
        //        {
        //            if ( result<=24 )
        //                flag=2;
        //            else
        //                flag=1;
        //        }
        //    }
        //    if ( strSize.Contains( "AGE" ) )
        //    {
        //        if ( int.TryParse( strSize.Split( ' ' )[1] , out result ) )
        //        {
        //            if ( result<3 )
        //                flag=2;
        //            else
        //                flag=1;
        //        }
        //    }
        //    //if ( strSize.Contains( "EU" ) )
        //    //{
        //    //    if ( int.TryParse( strSize.Split( ' ' )[1] , out result ) )
        //    //    {
        //    //        if ( result<3 )
        //    //            flag=2;
        //    //        else
        //    //            flag=1;
        //    //    }
        //    //}
        //    return flag;
        //}
        private int IsAdultOrBabyBySize( DataRow row )
        {
            int result=0;
            int flag=0;
            string strlocalProductName=string.Empty;
            string strSize=row["SIZE"].ToString( ).ToUpper( ).Trim( );
            if ( strSize.Contains( "MONTHS" ) )
            {
                flag=2;
            }
            if (strSize.Contains("AGE") || strSize.ToUpper().Contains("YEARS"))
            {
                string strage=strSize.Split( new[] { "AGE" } , StringSplitOptions.RemoveEmptyEntries )[0];
                if ( strage.Contains( '-' ) )
                {
                    string[] strAges=strage.Split( '-' );
                    result=0;
                    if ( int.TryParse( strAges[0] , out result ) )
                    {
                        if ( result>24 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strAges[1] , out result ) )
                    {
                        if ( result<=24 )
                            flag=2;
                    }
                }
                else if ( strage.Contains( '/' ) )
                {
                    string[] strAges=strage.Split( '/' );
                    result=0;
                    if ( int.TryParse( strAges[0] , out result ) )
                    {
                        if ( result>24 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strAges[1] , out result ) )
                    {
                        if ( result<=24 )
                            flag=2;
                    }
                }
                else
                {
                    if ( int.TryParse( strage , out result ) )
                    {
                        if ( result<=2 )
                            flag=2;
                        else
                            flag=1;
                    }
                }
            }
            strlocalProductName=row["中文品名"].ToString( ).Trim( );
            if ( strlocalProductName.Contains( "鞋" )&&strSize.Contains( "EU" ) )
            {
                //strSplit1   =   Regex.Replace( str20 , "[a-z]" , "" , RegexOptions.IgnoreCase ); 
                string strtemp=strSize.Split( new[] { "EU" } , StringSplitOptions.RemoveEmptyEntries )[0];
                strtemp=strtemp.Split( new[] { "UK" } , StringSplitOptions.RemoveEmptyEntries )[0];
                if ( strtemp.Contains( '-' ) )
                {
                    string[] strs=strtemp.Split( '-' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=38 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                    }
                }
                else if ( strtemp.Contains( '/' ) )
                {
                    string[] strs=strtemp.Split( '/' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=38 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                    }
                }
                else
                {
                    if ( int.TryParse( strtemp , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                        else if ( result>=38 )
                            flag=1;
                    }
                }
            }
            if ( strlocalProductName.Contains( "靴" )&&strSize.Contains( "EU" ) )
            {
                string strtemp=strSize.Split( new[] { "EU" } , StringSplitOptions.RemoveEmptyEntries )[0];
                strtemp=strtemp.Split( new[] { "UK" } , StringSplitOptions.RemoveEmptyEntries )[0];
                if ( strtemp.Contains( '-' ) )
                {
                    string[] strs=strtemp.Split( '-' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=38 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                    }
                }
                else if ( strtemp.Contains( '/' ) )
                {
                    string[] strs=strtemp.Split( '/' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=38 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                    }
                }
                else
                {
                    if ( int.TryParse( strtemp , out result ) )
                    {
                        if ( result<=37 )
                            flag=2;
                        else if ( result>=38 )
                            flag=1;
                    }
                }
            }
            if ( strlocalProductName.Contains( "袜" )&&strSize.Contains( "EU" ) )
            {
                string strtemp=strSize.Split( new[] { "EU" } , StringSplitOptions.RemoveEmptyEntries )[0];
                strtemp=strtemp.Split( new[] { "UK" } , StringSplitOptions.RemoveEmptyEntries )[0];
                if ( strtemp.Contains( '-' ) )
                {
                    string[] strs=strtemp.Split( '-' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=23 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=22 )
                            flag=2;
                    }
                }
                else if ( strtemp.Contains( '/' ) )
                {
                    string[] strs=strtemp.Split( '/' );
                    result=0;
                    if ( int.TryParse( strs[0] , out result ) )
                    {
                        if ( result>=23 )
                            flag=1;
                    }
                    result=0;
                    if ( int.TryParse( strs[1] , out result ) )
                    {
                        if ( result<=22 )
                            flag=2;
                    }
                }
                else
                {
                    if ( int.TryParse( strtemp , out result ) )
                    {
                        if ( result<=22 )
                            flag=2;
                        else if ( result>=23 )
                            flag=1;
                    }
                }
            }
            return flag;
        }
        private void DoubleOperateForUnionCLP( DataTable unionCLPTable , ref string errmsg )
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
                    {
                        if ( row["中文品名"].ToString( ).Contains( "鞋" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≧EU38 )' OR Size='COMMON')";
                        else if ( row["中文品名"].ToString( ).Contains( "靴" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≧EU38 )' OR Size='COMMON')";
                        else if(row["中文品名"].ToString( ).Contains( "袜" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≧EU23 )' OR Size='COMMON')";
                        else
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≧3岁 )' OR Size='COMMON')";
                    }
                    //strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size='COMMON')";
                    else if ( flag==2 )
                    {
                        if ( row["中文品名"].ToString( ).Contains( "鞋" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≦EU37 )' OR Size='COMMON')";
                        else if ( row["中文品名"].ToString( ).Contains( "靴" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≦EU37 )' OR Size='COMMON')";
                        else if(row["中文品名"].ToString( ).Contains( "袜" ) && row["SIZE"].ToString( ).Contains( "EU" ) )
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≦EU22 )' OR Size='COMMON')";
                        else
                            strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size='(≦2岁 )' OR Size='COMMON')";
                    }                       
                    //strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧3岁 )' OR Size='COMMON')";
                    else
                    {

                        strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"'";
                    }
                    DataRow[] drs=RepetitiveBasicDataTable.Select( strfilter );
                    if ( drs.Count( )==0 )
                    {
                        errmsg+=string.Format( "基础资料表MODELCODE为{0}找不到符合DOUBLE操作条件的记录" , row["MODEL_CODE"].ToString( ) );
                        return;
                    }
                    else
                    {
                        row["法定计量单位"]=drs[0]["QuantityUnit"];
                        row["HS_CODE_(IN_CAT)"]=drs[0]["HSCodeInCat"];
                        row["中文品名"]=drs[0]["LocalProductName"];
                        row["英文品名"]=drs[0]["EnglishProductName"];
                        row["申报要素"]=drs[0]["SentialFactor"];
                        row["备注"]=drs[0]["Description"];
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
                        //if ( flag==1 )
                        //    strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size is null)";
                        //else if ( flag==2 )
                        //    strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧3岁 )' OR Size is null)";
                        if ( flag==1 )
                        {
                            if ( dr["中文品名"].ToString( ).Contains( "鞋" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦EU37 )' OR Size is null)";
                            else if ( dr["中文品名"].ToString( ).Contains( "靴" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦EU37 )' OR Size is null)";
                            else if ( dr["中文品名"].ToString( ).Contains( "袜" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦EU22 )' OR Size is null)";
                            else
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size is null)";
                        }
                        //strfilter="ModelCode='"+row["MODEL_CODE"].ToString( )+"' AND (Size<>'(≦2岁 )' OR Size='COMMON')";
                        else if ( flag==2 )
                        {
                            if ( dr["中文品名"].ToString( ).Contains( "鞋" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧EU38 )' OR Size is null)";
                            else if ( dr["中文品名"].ToString( ).Contains( "靴" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧EU38 )' OR Size is null)";
                            else if ( dr["中文品名"].ToString( ).Contains( "袜" ) && dr["SIZE"].ToString( ).Contains( "EU" ) )
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧EU23 )' OR Size is null)";
                            else
                                strfilter="ModelCode='"+dr["MODEL_CODE"].ToString( )+"' AND (Size<>'(≧3岁 )' OR Size is null)";
                        } 
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
                            newdr["中文品名"]=dr1["LocalProductName"];
                            newdr["英文品名"]=dr1["EnglishProductName"];
                            newdr["申报要素"]=dr1["SentialFactor"];
                            newdr["备注"]=dr1["Description"];
                            newdr["监管条件"]=dr1["SupervisionCondition"];
                            //Double_or_Set 添加原产国 2016-4-27 by hualin
                            newdr["ORIGIN"] = dr["ORIGIN"];
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
                    if (dr["Double_or_Set"].ToString( )=="0" )
                        dr["Double_or_Set"]="N";
                    //if ( dr["Double_or_Set"].ToString( ).ToUpper( )=="DOUBLE"||dr["Double_or_Set"].ToString( )=="0" )
                    //    dr["Double_or_Set"]="N";
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
                    //if ( dr["Double_or_Set"].ToString( ).ToUpper( )=="DOUBLE"||dr["Double_or_Set"].ToString( )=="0" )
                    if ( dr["Double_or_Set"].ToString( )=="0" )
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
        private void SupervisionConditionForUnionCLP( DataTable _doubleorsetTable )
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
            //string shippingnumber=string.Empty;
            //unoinclp=DoubleOrSetTableItemModelDataFill( unoinclp );
            
            foreach ( DataRow row in unoinclp.Rows )
            {
                //shippingnumber=row["SHIPPING_NUMBER"].ToString().Trim();
                //if ( shippingnumber.Contains( '-' ) )
                //    shippingnumber=shippingnumber.Split( '-' )[0];
                //if ( _CubeNumberAdjustmentTable.Select( "ShippingNumber='"+shippingnumber+"' and ConfirmType='sensitive'" ).Length>0 )
                //{ 
                    
                //}
                //if (row["MODEL_CODE_(IN_CAT)"].ToString( )=="" )
                row["申报要素"]=row["申报要素"].ToString( ).Replace( '：' , ':' );
                //if ( row["ORDER_NUMBER"].ToString( )==""&&row["PALLET_NUMBER"].ToString( )=="" )
                //    _UnionCLPSETTable.Rows.Add( row.ItemArray );
                if ( row["Double_Or_Set"].ToString( )==""&&row["PALLET_NUMBER"].ToString( )=="" )
                    _UnionCLPSETTable.Rows.Add( row.ItemArray );
                else
                {
                    if ( row["监管条件"].ToString( ).Contains( 'B' ) )
                        _UnionCLPCommodityInspectionTable.Rows.Add( row.ItemArray );
                    else
                    {
                        if ( row["Double_Or_Set"].ToString( )=="N"||row["Double_Or_Set"].ToString( ).ToUpper()=="DOUBLE" )
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
        private Dictionary<List<DataRow> , List<DataRow>> UnionCLPSetTableSentialFactorTotalHandle1( DataTable sourceDT )
        {
            Dictionary<List<DataRow> , List<DataRow>> dic=new Dictionary<List<DataRow> , List<DataRow>>( );
            Queue<DataRow> clpsetQueue = new Queue<DataRow>( );
            List<DataRow> tkeys=new List<DataRow>( );
            List<DataRow> tvalues=new List<DataRow>( );
            bool flag=false;
            int keyindex=0;
            int valueindex=0;
            int intquantity=0;
            int j=0;
            string strunit=String.Empty;
            //foreach ( DataRow dr in sourceDT.Rows )
            //{

            //    if ( dr["QUANTITY"].ToString( )!=""&& dr["QUANTITY_UNIT"].ToString( )!="" )
            //    {
            //        if ( !flag )
            //        {
            //            intquantity=0;
            //            keyindex=sourceDT.Rows.IndexOf( dr );
            //            flag=true;
            //            j++;
            //        }
            //        intquantity+=int.Parse( dr["QUANTITY"].ToString( ) );
            //    }
            //    else
            //    {
            //        valueindex=sourceDT.Rows.IndexOf( dr );
            //        if ( valueindex<sourceDT.Rows.Count-1 )
            //        {
            //            if ( sourceDT.Rows[valueindex+1]["QUANTITY"].ToString( )!=""&& sourceDT.Rows[valueindex+1]["QUANTITY_UNIT"].ToString( )!="" )
            //            {
            //                flag=false;
            //            }
            //        }
            //        strunit=dr["法定计量单位"].ToString( );
            //        if ( strunit.ToUpper( ).Contains( 'X' ) )
            //        {
            //            string[] arr=strunit.ToUpper( ).Split( 'X' );
            //            dr["QUANTITY"]=intquantity*int.Parse( arr[1] );
            //            if ( dr["QUANTITY"].ToString( ).Trim( )=="" )
            //            {
            //                dr["QUANTITY"]=int.Parse( arr[1] );
            //                dr["法定计量单位"]=arr[0];
            //            }
            //            else
            //            {

            //                dr["法定计量单位"]=arr[0];
            //            }
            //        }
            //        dr["QUANTITY"]=intquantity;
            //    }
            //    dr["GroupID"]=j;
            //}

            var query = from t in sourceDT.AsEnumerable( )
                        group t by new { t1 = t.Field<int>( "GroupID" ) } into m
                        select new
                        {
                            GroupID = m.Key.t1
                        };
            foreach ( var obj in query )
            {
                sourceDT.Select( "GroupID="+obj.GroupID ).AsEnumerable<DataRow>( ).ToList( ).ForEach(
                    delegate( DataRow dr )
                    {
                        if ( dr["QUANTITY"].ToString( )!=""&& dr["QUANTITY_UNIT"].ToString( )!="" )
                            tkeys.Add( dr );
                        else
                            tvalues.Add( dr );
                    } );
                DataTable keydt=new DataTable( );
                DataTable valuedt=new DataTable( );
                keydt=sourceDT.Clone( );
                valuedt=sourceDT.Clone( );
                if ( tkeys.Count>1 )
                {
                    UnionCLPSentialFactorTotalHandleForSet( tkeys.CopyToDataTable( ) , ref keydt );
                    tkeys=keydt.Select( ).ToList<DataRow>( );
                }
                if ( tvalues.Count>1 )
                {
                    UnionCLPSentialFactorTotalHandleForSet( tvalues.CopyToDataTable( ) , ref valuedt );
                    tvalues=valuedt.Select( ).ToList<DataRow>( );
                }
                dic.Add( tkeys , tvalues );
                tkeys=new List<DataRow>( );
                tvalues=new List<DataRow>( );
            }
            return dic;
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
            //int intquantity=0;
            int j=0;
            string strunit=String.Empty;
            foreach ( DataRow dr in sourceDT.Rows )
            {

                if ( dr["QUANTITY_UNIT"].ToString( )!="" )
                {
                    if ( !flag )
                    {
                        //intquantity=0;
                        keyindex=sourceDT.Rows.IndexOf( dr );
                        flag=true;
                        j++;
                    }
                    //intquantity+=int.Parse( dr["QUANTITY"].ToString( ) );
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
                sourceDT.Select( "GroupID="+obj.GroupID ).AsEnumerable<DataRow>( ).ToList( ).ForEach(
                    delegate( DataRow dr )
                    {
                        if ( dr["QUANTITY_UNIT"].ToString( )!="" )
                            tkeys.Add( dr );
                        else
                            tvalues.Add( dr );
                    } );
                dic.Add( tkeys , tvalues );
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
                keyGroupList.AddRange( GetGroupIDByLocalProductName( strkey ) );
            }
            foreach ( string strvalue in ValueList )
            {
                VauleGroupList.AddRange( GetGroupIDByLocalProductName( strvalue ) );
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
        private ArrayList GetGroupIDByLocalProductName( string localProductName )
        {
            ArrayList list=new ArrayList( );
            foreach ( DataRow row in _SetBreakUpTable.Select( "LocalProductName='"+localProductName+"'" ) )
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
                    string strResult="";
                    //return  ( dr["中文品名"].ToString( )==dr1["中文品名"].ToString( ) )&&
                    //        ( dr["HS_CODE_(IN_CAT)"].ToString( )==dr1["HS_CODE_(IN_CAT)"].ToString( ) )&&
                    //        ( SentialFactorCompare( dr["申报要素"].ToString( ) , dr1["申报要素"].ToString( ) , ref strResult ) );
                    //添加原产国分组 by hualin 2016/4/27
                    return (dr["中文品名"].ToString() == dr1["中文品名"].ToString()) && (dr["ORIGIN"].ToString() == dr1["ORIGIN"].ToString()) &&
                            (dr["HS_CODE_(IN_CAT)"].ToString() == dr1["HS_CODE_(IN_CAT)"].ToString()) &&
                            (SentialFactorCompare(dr["申报要素"].ToString(), dr1["申报要素"].ToString(), ref strResult));
                } )==null )
                    flag=false;
            }
            return flag;
        }
        private List<DataRow> UnionCLPSETDataRowGroup( List<DataRow> drs2 , List<DataRow> drs1 )
        {
            for ( int i=0 ; i<drs2.Count ; i++ )
            {
                drs2[i]["QUANTITY"]=int.Parse( drs1[i]["QUANTITY"].ToString( ) )+int.Parse( drs2[i]["QUANTITY"].ToString( ) );
                drs2[i]["TOTAL_VALUE"]=decimal.Parse( drs1[i]["TOTAL_VALUE"].ToString( ) )+decimal.Parse( drs2[i]["TOTAL_VALUE"].ToString( ) );
                drs2[i]["NET_WEIGHT"]=decimal.Parse( drs1[i]["NET_WEIGHT"].ToString( ) )+decimal.Parse( drs2[i]["NET_WEIGHT"].ToString( ) );
                drs2[i]["GROSS_WEIGHT"]=decimal.Parse( drs1[i]["GROSS_WEIGHT"].ToString( ) )+decimal.Parse( drs2[i]["GROSS_WEIGHT"].ToString( ) );
            }
            return drs2;
        }
        private DataTable GetSetBreakUpGroupInfo( DataTable setBreakUpCloneTable,List<IGrouping<int , DataRow>> SetGroups)
        {
            DataTable returnDT=new DataTable( );
            List<DataRow> DRList=new List<DataRow>( );
            foreach ( IGrouping<int , DataRow> ig in SetGroups )
            {
                DRList=ig.ToList<DataRow>( );
                int i=0;
                foreach ( DataRow dr in setBreakUpCloneTable.Rows )
                {

                    if ( DRList.Where( delegate( DataRow ro ) { return (ro["ModelCode"].ToString( ).Trim( )==dr["Model_Code"].ToString( ).Trim())&&(ro["LocalProductName"].ToString( ).Trim( )==dr["中文品名"].ToString( ).Trim( )); } ).Count( )>0 )
                        i++;
                }
                if ( i==setBreakUpCloneTable.Rows.Count )
                {
                    returnDT=DRList.CopyToDataTable( );
                    break;
                }
            }
            return returnDT;
        }
        private DataTable UnionCLPSETDataRowGroupDataTotal( DataTable unionClpSetTable , DataTable setBreakUpCloneTable )
        {
            DataTable unionCLPSETTableClone=unionClpSetTable.Clone( );
            List<IGrouping<int , DataRow>> setbreakupGroups=setBreakUpCloneTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "GroupID" ) , m => m ).ToList( );
            unionClpSetTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "GroupID" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<int , DataRow> ig )
            {
                var query1 =
                from clp in ig.ToList<DataRow>( )
                join setdata in GetSetBreakUpGroupInfo(ig.ToList<DataRow>().CopyToDataTable(),setbreakupGroups).AsEnumerable( )
                //on new { md=clp.Field<String>( "Model_Code" ), cn=clp.Field<String>( "中文品名" ) }  equals new {setdata.Field<String>( "ModelCode" ), setdata.Field<String>( "LocalProductName" )}  
                on clp.Field<String>("中文品名") equals setdata.Field<String>("LocalProductName")
                where (clp.Field<String>( "Model_Code" ) == setdata.Field<String>( "ModelCode" )) 
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
                    LocalProductName = clp.Field<String>( "中文品名" ) ,
                    EnglishProductName = clp.Field<String>( "英文品名" ) ,
                    SentialFactor = clp.Field<String>( "申报要素" ) ,
                    SupervisionCondition = clp.Field<String>( "监管条件" ) ,
                    DoubleOrSet = clp.Field<String>( "Double_or_Set" ) ,
                    Description = clp.Field<String>( "备注" ) ,
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
                    GrossWeightRatio = setdata.Field<Decimal>( "GrossWeightRatio" ) ,
                    GroupID= clp.Field<int>( "GroupID" )
                };
                //unionClpSetTable.Rows.Clear( );
                string strTotalValue="0";
                string strNETWeight="0";
                string strGrossWeight="0";
                bool TotalValueFlag=true;
                bool NETWeightFlag=true;
                bool GrossWeightFlag=true;
                decimal sumTotalValue=0;
                decimal sumNETWeight=0;
                decimal sumGrossWeight=0;
                foreach ( var obj in query1 )
                {
                    if ( obj.TotalValue!="" && obj.TotalValue!=null )
                    {
                        if ( TotalValueFlag )
                        {
                            TotalValueFlag=false;
                            sumTotalValue=0;
                        }
                        strTotalValue=obj.TotalValue;
                        sumTotalValue+=decimal.Parse( obj.TotalValue );
                        //sumTotalValue+=decimal.Parse( strTotalValue )*obj.TotalValueRatio;
                    }
                    else
                    {
                        TotalValueFlag=true;
                    }

                    if ( obj.NETWeight!="" && obj.NETWeight!=null )
                    {
                        if ( NETWeightFlag )
                        {
                            NETWeightFlag=false;
                            sumNETWeight=0;
                        }
                        strNETWeight=obj.NETWeight;
                        sumNETWeight+=decimal.Parse( obj.NETWeight );
                        //sumNETWeight+=decimal.Parse( strNETWeight )*obj.NetWeightRatio;
                    }
                    else
                        NETWeightFlag=true;
                    if ( obj.GrossWeight!="" && obj.GrossWeight!=null )
                    {
                        if ( GrossWeightFlag )
                        {
                            GrossWeightFlag=false;
                            sumGrossWeight=0;
                        }
                        strGrossWeight=obj.GrossWeight;
                        sumGrossWeight+=decimal.Parse( obj.GrossWeight );
                        //sumGrossWeight+=decimal.Parse( strGrossWeight )*obj.GrossWeightRatio;
                    }
                    else
                        GrossWeightFlag=true;
                    if ( obj.TotalValue!="" && obj.TotalValue!=null )
                        unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                            obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
                            obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Description , obj.Brand ,
                            obj.TypeOfGoods , obj.Price , obj.Currency , ( decimal.Parse( strTotalValue )*obj.TotalValueRatio ).ToString( ) ,
                            ( decimal.Parse( strNETWeight )*obj.NetWeightRatio ).ToString( ) , ( decimal.Parse( strGrossWeight )*obj.GrossWeightRatio ).ToString( ) ,
                            obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName , obj.GroupID );
                    else
                    {
                        unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
                        obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
                        obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Description , obj.Brand ,
                        obj.TypeOfGoods , obj.Price , obj.Currency , ( sumTotalValue*obj.TotalValueRatio ).ToString( ) ,
                            ( sumNETWeight*obj.NetWeightRatio ).ToString( ) , ( sumGrossWeight*obj.GrossWeightRatio ).ToString( ) ,
                            obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName , obj.GroupID );
                    }
                }

            } );            
            unionCLPSETTableClone.AcceptChanges( );
            unionClpSetTable.Clear( );
            unionClpSetTable=unionCLPSETTableClone;
            unionClpSetTable.AcceptChanges( );
            return unionClpSetTable;
        }
        //private DataTable UnionCLPSETDataRowGroupDataTotal11( DataTable unionClpSetTable , DataTable setBreakUpCloneTable )
        //{
        //    DataTable unionCLPSETTableClone=unionClpSetTable.Clone( );
        //    var query1 =
        //        from clp in unionClpSetTable.AsEnumerable( )
        //        join setdata in setBreakUpCloneTable.AsEnumerable( )
        //        on clp.Field<String>( "中文品名" ) equals setdata.Field<String>( "LocalProductName" )
        //        select new
        //        {
        //            ItemCode = clp.Field<String>( "ITEM_CODE" ) ,
        //            ShippingNumber = clp.Field<String>( "SHIPPING_NUMBER" ) ,
        //            OrderNumber = clp.Field<String>( "ORDER_NUMBER" ) ,
        //            PalletNumber = clp.Field<String>( "PALLET_NUMBER" ) ,
        //            ParcelNumber = clp.Field<String>( "PARCEL_NUMBER" ) ,
        //            ModelCode = clp.Field<String>( "MODEL_CODE" ) ,
        //            Origin = clp.Field<String>( "ORIGIN" ) ,
        //            Quantity = clp.Field<String>( "QUANTITY" ) ,
        //            QuantityUnit = clp.Field<String>( "QUANTITY_UNIT" ) ,
        //            DispatchingKey = clp.Field<String>( "DISPATCHING_KEY" ) ,
        //            EnglishComposition = clp.Field<String>( "English_Composition" ) ,
        //            Size = clp.Field<String>( "SIZE" ) ,
        //            Unit = clp.Field<String>( "法定计量单位" ) ,
        //            HSCodeInCat = clp.Field<String>( "HS_CODE_(IN_CAT)" ) ,
        //            LocalProductName = clp.Field<String>( "中文品名" ) ,
        //            EnglishProductName = clp.Field<String>( "英文品名" ) ,
        //            SentialFactor = clp.Field<String>( "申报要素" ) ,
        //            SupervisionCondition = clp.Field<String>( "监管条件" ) ,
        //            DoubleOrSet = clp.Field<String>( "Double_or_Set" ) ,
        //            Description = clp.Field<String>( "备注" ) ,
        //            Brand = clp.Field<String>( "BRAND" ) ,
        //            TypeOfGoods = clp.Field<String>( "TYPE_OF_GOODS" ) ,
        //            Price = clp.Field<String>( "PRICE" ) ,
        //            Currency = clp.Field<String>( "CURRENCY" ) ,
        //            TotalValue = clp.Field<String>( "TOTAL_VALUE" ) ,
        //            NETWeight = clp.Field<String>( "NET_WEIGHT" ) ,
        //            GrossWeight = clp.Field<String>( "GROSS_WEIGHT" ) ,
        //            CommercialInvoiceNO = clp.Field<String>( "COMMERCIAL_INVOICE_NO" ) ,
        //            StoreNO = clp.Field<String>( "STORE_NO" ) ,
        //            StoreName = clp.Field<String>( "STORE_NAME" ) ,
        //            TotalValueRatio = setdata.Field<Decimal>( "TotalValueRatio" ) ,
        //            NetWeightRatio = setdata.Field<Decimal>( "NetWeightRatio" ) ,
        //            GrossWeightRatio = setdata.Field<Decimal>( "GrossWeightRatio" ) ,
        //            GroupID= clp.Field<int>( "GroupID" )
        //        };
        //    //unionClpSetTable.Rows.Clear( );
        //    string strTotalValue="0";
        //    string strNETWeight="0";
        //    string strGrossWeight="0";
        //    bool TotalValueFlag=true;
        //    bool NETWeightFlag=true;
        //    bool GrossWeightFlag=true;
        //    decimal sumTotalValue=0;
        //    decimal sumNETWeight=0;
        //    decimal sumGrossWeight=0;
        //    foreach ( var obj in query1 )
        //    {
        //        if ( obj.TotalValue!="" && obj.TotalValue!=null )
        //        {
        //            if ( TotalValueFlag )
        //            {
        //                TotalValueFlag=false;
        //                sumTotalValue=0;
        //            }
        //            strTotalValue=obj.TotalValue;
        //            sumTotalValue+=decimal.Parse( obj.TotalValue );
        //            //sumTotalValue+=decimal.Parse( strTotalValue )*obj.TotalValueRatio;
        //        }
        //        else
        //        {
        //            TotalValueFlag=true;
        //        }

        //        if ( obj.NETWeight!="" && obj.NETWeight!=null )
        //        {
        //            if ( NETWeightFlag )
        //            {
        //                NETWeightFlag=false;
        //                sumNETWeight=0;
        //            }
        //            strNETWeight=obj.NETWeight;
        //            sumNETWeight+=decimal.Parse( obj.NETWeight );
        //            //sumNETWeight+=decimal.Parse( strNETWeight )*obj.NetWeightRatio;
        //        }
        //        else
        //            NETWeightFlag=true;
        //        if ( obj.GrossWeight!="" && obj.GrossWeight!=null )
        //        {
        //            if ( GrossWeightFlag )
        //            {
        //                GrossWeightFlag=false;
        //                sumGrossWeight=0;
        //            }
        //            strGrossWeight=obj.GrossWeight;
        //            sumGrossWeight+=decimal.Parse( obj.GrossWeight );
        //            //sumGrossWeight+=decimal.Parse( strGrossWeight )*obj.GrossWeightRatio;
        //        }
        //        else
        //            GrossWeightFlag=true;
        //        if ( obj.TotalValue!="" && obj.TotalValue!=null )
        //            unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
        //                obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
        //                obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Description , obj.Brand ,
        //                obj.TypeOfGoods , obj.Price , obj.Currency , ( decimal.Parse( strTotalValue )*obj.TotalValueRatio ).ToString( ) ,
        //                ( decimal.Parse( strNETWeight )*obj.NetWeightRatio ).ToString( ) , ( decimal.Parse( strGrossWeight )*obj.GrossWeightRatio ).ToString( ) ,
        //                obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName , obj.GroupID );
        //        else
        //        {
        //            unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
        //            obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
        //            obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Description , obj.Brand ,
        //            obj.TypeOfGoods , obj.Price , obj.Currency , ( sumTotalValue*obj.TotalValueRatio ).ToString( ) ,
        //                ( sumNETWeight*obj.NetWeightRatio ).ToString( ) , ( sumGrossWeight*obj.GrossWeightRatio ).ToString( ) ,
        //                obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName , obj.GroupID );
        //        }
        //        //                unionCLPSETTableClone.Rows.Add( obj.ItemCode , obj.ShippingNumber , obj.OrderNumber , obj.PalletNumber , obj.ParcelNumber , obj.ModelCode ,
        //        //obj.Origin , obj.Quantity , obj.QuantityUnit , obj.DispatchingKey , obj.EnglishComposition , obj.Size , obj.Unit , obj.HSCodeInCat ,
        //        //obj.LocalProductName , obj.EnglishProductName , obj.SentialFactor , obj.SupervisionCondition , obj.DoubleOrSet , obj.Brand ,
        //        //obj.TypeOfGoods , obj.Price , obj.Currency , sumTotalValue.ToString( ) , sumNETWeight.ToString( ) , sumGrossWeight.ToString( ) ,
        //        //obj.CommercialInvoiceNO , obj.StoreNO , obj.StoreName , obj.GroupID );
        //    }
        //    unionCLPSETTableClone.AcceptChanges( );
        //    unionClpSetTable.Clear( );
        //    unionClpSetTable=unionCLPSETTableClone;
        //    unionClpSetTable.AcceptChanges( );
        //    return unionClpSetTable;
        //}
        private DataTable UnionCLPSentialFactorExactCompareTotalHandle( DataTable sourceDT )
        {
            DataTable tempDT=new DataTable( );
            DataTable copyDT=new DataTable( );
            tempDT=sourceDT.Copy( );
            DataView dv=tempDT.DefaultView;
            //dv.Sort="中文品名 ASC,HS_CODE_(IN_CAT) ASC,申报要素 ASC";
            //添加原产国分组 by hualin 2016-4-27
            dv.Sort = "中文品名 ASC,HS_CODE_(IN_CAT) ASC,申报要素 ASC,ORIGIN ASC";
            tempDT=dv.ToTable( );
            copyDT=tempDT.Clone( );
            var query = from t in tempDT.AsEnumerable( )
                        //group t by new { t1 = t.Field<string>( "中文品名" ) , t2 = t.Field<string>( "HS_CODE_(IN_CAT)" ) , t3 = t.Field<string>( "申报要素" ) } into m
                        //添加原产国分组 by hualin 2016-4-27
                        group t by new { t1 = t.Field<string>("中文品名"), t2 = t.Field<string>("HS_CODE_(IN_CAT)"), t3 = t.Field<string>("申报要素"), t4 = t.Field<string>("ORIGIN") } into m
                        select new
                        {
                            中文品名 = m.Key.t1 ,
                            HS_CODE = m.Key.t2 ,
                            申报要素=m.Key.t3.ToString( ) ,
                            ORIGIN = m.Key.t4.ToString(),
                            //QUANTITY = m.Sum( n => decimal.Parse( n.Field<String>( "QUANTITY" ) )>0? ) ,
                            QUANTITY = m.Sum( n => n.Field<String>( "QUANTITY" )!=""? decimal.Parse( n.Field<String>( "QUANTITY" ) ):0 ) ,
                            //TOTAL_VALUE=m.Sum( n => decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ) ) ,
                            TOTAL_VALUE=m.Sum( n => n.Field<string>( "TOTAL_VALUE" )!=""?decimal.Parse( n.Field<string>( "TOTAL_VALUE" ) ):0 ) ,
                            英文品名=m.First( ).Field<string>( "英文品名" ) ,
                            监管条件=m.First( ).Field<string>( "监管条件" ) ,
                            QUANTITY_UNIT = m.First( ).Field<string>( "法定计量单位" ) ,
                            //NET_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ) ) ,
                            NET_WEIGHT=m.Sum( n => n.Field<string>( "NET_WEIGHT" )!=""? decimal.Parse( n.Field<string>( "NET_WEIGHT" ) ):0 ) ,
                            //GROSS_WEIGHT=m.Sum( n => decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ) ) ,
                            GROSS_WEIGHT=m.Sum( n => n.Field<string>( "GROSS_WEIGHT" )!=""?decimal.Parse( n.Field<string>( "GROSS_WEIGHT" ) ):0 )
                        };
            foreach ( var obj in query )
            {
                //copyDT.Rows.Add( obj.QUANTITY , obj.QUANTITY_UNIT , obj.HS_CODE , obj.中文品名 , obj.英文品名 , obj.申报要素 , obj.监管条件 , obj.TOTAL_VALUE ,
                //     obj.NET_WEIGHT , obj.GROSS_WEIGHT );
                copyDT.Rows.Add(obj.ORIGIN,  obj.QUANTITY , obj.QUANTITY_UNIT , obj.HS_CODE , obj.中文品名 , obj.英文品名 , obj.申报要素 , obj.监管条件 , obj.TOTAL_VALUE ,
                     obj.NET_WEIGHT , obj.GROSS_WEIGHT);
            }
            return copyDT;
        }
        private bool SentialFactorCompare11( string strSentail1 , string strSentail2 , ref string strResult )
        {
            strResult=string.Empty;
            string strResult1=String.Empty;
            string strBrand1=String.Empty;
            string strModel1=String.Empty;
            string strResult2=String.Empty;
            string strBrand2=String.Empty;
            string strModel2=String.Empty;
            if ( strSentail1.EndsWith( ";" ) )
                strSentail1=strSentail1.Substring( 0 , strSentail1.Length-1 );
            if ( strSentail2.EndsWith( ";" ) )
                strSentail2=strSentail2.Substring( 0 , strSentail2.Length-1 );
            if ( strSentail1==strSentail2 )
            {
                strResult=strSentail1;
                return true;
            }
            string[] strArray1=strSentail1.Split( '|' );
            string[] strArray2=strSentail2.Split( '|' );
            foreach ( string sentail1 in strArray1 )
            {
                if ( sentail1.Contains( "品牌" ) )
                    strBrand1=sentail1.Trim( );
                else if ( sentail1.Contains( "型号" )||sentail1.Contains( "货号" ) )
                    strModel1=sentail1.Trim( );
                else
                    strResult1+=sentail1.Trim( )+"|";
            }
            foreach ( string sentail2 in strArray2 )
            {
                if ( sentail2.Contains( "品牌" ) )
                    strBrand2=sentail2.Trim( );
                else if ( sentail2.Contains( "型号" )||sentail2.Contains( "货号" ) )
                    strModel2=sentail2.Trim( );
                else
                    strResult2+=sentail2.Trim( )+"|";
            }
            if ( strResult1!=strResult2 )
                return false;
            else
            {
                if ( strBrand1!=string.Empty )
                {
                    if ( !strBrand1.EndsWith( "等" ) )
                        strResult1+=strBrand1.Trim( )+"等|";
                    else
                        strResult1+=strBrand1.Trim( )+"|";
                }
                else
                {
                    if ( strBrand2!=string.Empty )

                        if ( !strBrand2.EndsWith( "等" ) )
                            strResult1+=strBrand2.Trim( )+"等|";
                        else
                            strResult1+=strBrand1.Trim( )+"|";
                }
                if ( strModel1!=string.Empty )
                {
                    if ( !strModel1.EndsWith( "等" ) )
                        strResult1+=strModel1.Trim( )+"等|";
                    else
                        strResult1+=strModel1.Trim( )+"|";
                }
                else
                {
                    if ( strModel2!=string.Empty )
                    {
                        if ( !strModel2.EndsWith( "等" ) )
                            strResult1+=strModel2.Trim( )+"等|";
                        else
                            strResult1+=strModel2.Trim( )+"|";
                    }
                }
                if ( strResult1.EndsWith( "|" ) )
                {
                    strResult1=strResult1.Substring( 0 , strResult1.Length-1 );
                    strResult=strResult1;
                }
                return true;
            }
        }

        public decimal GetNumber( string str )
        {
            decimal result = 0;
            if ( str != null && str != string.Empty )
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace( str , @"[^/d./d]" , "" );
                // 如果是数字，则转换为decimal类型 
                if ( Regex.IsMatch( str , @"^[+-]?/d*[.]?/d*$" ) )
                {
                    result = decimal.Parse( str );
                }
            }
            return result;
        }

        public int GetNumberInt( string str )
        {
            int result = 0;
            if ( str != null && str != string.Empty )
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace( str , @"[^/d./d]" , "" );
                // 如果是数字，则转换为decimal类型 
                if ( Regex.IsMatch( str , @"^[+-]?/d*[.]?/d*$" ) )
                {
                    result = int.Parse( str );
                }
            }
            return result;
        }
        private bool SentialFactorCompare( string strSentail1 , string strSentail2 , ref string strResult )
        {
            strResult=string.Empty;
            string strResult1=String.Empty;
            string strBrand1=String.Empty;
            string strModel1=String.Empty;
            string strInnerBottomLength1=string.Empty;//内底长度
            string strExerciseUse1=string.Empty;//运动时使用
            string strMonofilamentFineness1=string.Empty;//单丝细度
            //ArrayList MonofilamentFinenessList=new ArrayList( );
            //bool MonofilamentFinenessFlag=false;
            string strMonofilamentFinenessUnit=string.Empty;
            string strSpecificationSize1=string.Empty;//规格尺寸
            string strSize1=string.Empty;//尺寸

            string strResult2=String.Empty;
            string strBrand2=String.Empty;
            string strModel2=String.Empty;
            string strInnerBottomLength2=string.Empty;////内底长度
            string strExerciseUse2=string.Empty;//运动时使用
            string strMonofilamentFineness2=string.Empty;//单丝细度
            string strSpecificationSize2=string.Empty;//规格尺寸
            string strSize2=string.Empty;//尺寸

            if ( strSentail1.EndsWith( ";" ) )
                strSentail1=strSentail1.Substring( 0 , strSentail1.Length-1 );
            if ( strSentail2.EndsWith( ";" ) )
                strSentail2=strSentail2.Substring( 0 , strSentail2.Length-1 );
            if ( strSentail1==strSentail2 )
            {
                strResult=strSentail1;
                return true;
            }
            string[] strArray1=strSentail1.Split( '|' );
            string[] strArray2=strSentail2.Split( '|' );
            foreach ( string sentail1 in strArray1 )
            {
                if ( sentail1.Contains( "品牌" ) )
                    strBrand1=sentail1.Trim( );
                else if ( sentail1.Contains( "型号" )||sentail1.Contains( "货号" ) )
                    strModel1=sentail1.Trim( );
                else if ( sentail1.Contains( "内底长度" ) )
                    strInnerBottomLength1=sentail1.Trim( );
                else if ( sentail1.Contains( "运动时使用" ) )
                    strExerciseUse1=sentail1.Trim( );
                else if ( sentail1.Contains( "单丝细度" ) )
                    strMonofilamentFineness1=sentail1.Trim( );
                else if ( sentail1.Contains( "规格尺寸" ) )
                    strSpecificationSize1=sentail1.Trim( );
                else if ( sentail1.Contains( "尺寸" ) )
                    strSize1=sentail1.Trim( );
                else
                    strResult1+=sentail1.Trim( )+"|";
            }
            foreach ( string sentail2 in strArray2 )
            {
                if ( sentail2.Contains( "品牌" ) )
                    strBrand2=sentail2.Trim( );
                else if ( sentail2.Contains( "型号" )||sentail2.Contains( "货号" ) )
                    strModel2=sentail2.Trim( );
                else if ( sentail2.Contains( "内底长度" ) )
                    strInnerBottomLength2=sentail2.Trim( );
                else if ( sentail2.Contains( "运动时使用" ) )
                    strExerciseUse2=sentail2.Trim( );
                else if ( sentail2.Contains( "单丝细度" ) )
                    strMonofilamentFineness2=sentail2.Trim( );
                else if ( sentail2.Contains( "规格尺寸" ) )
                    strSpecificationSize2=sentail2.Trim( );
                else if ( sentail2.Contains( "尺寸" ) )
                    strSize2=sentail2.Trim( );
                else
                    strResult2+=sentail2.Trim( )+"|";
            }
            if ( strResult1!=strResult2 )
                return false;
            else
            {
                if ( strBrand1!=string.Empty )
                {
                    if ( !strBrand1.EndsWith( "等" ) )
                        strResult1+=strBrand1.Trim( )+"等|";
                    else
                        strResult1+=strBrand1.Trim( )+"|";
                }
                else
                {
                    if ( strBrand2!=string.Empty )

                        if ( !strBrand2.EndsWith( "等" ) )
                            strResult1+=strBrand2.Trim( )+"等|";
                        else
                            strResult1+=strBrand1.Trim( )+"|";
                }
                if ( strModel1!=string.Empty )
                {
                    if ( !strModel1.EndsWith( "等" ) )
                        strResult1+=strModel1.Trim( )+"等|";
                    else
                        strResult1+=strModel1.Trim( )+"|";
                }
                else
                {
                    if ( strModel2!=string.Empty )
                    {
                        if ( !strModel2.EndsWith( "等" ) )
                            strResult1+=strModel2.Trim( )+"等|";
                        else
                            strResult1+=strModel2.Trim( )+"|";
                    }
                }
                //运动时使用
                if ( strExerciseUse1!=string.Empty )
                {
                    strResult1+=strExerciseUse1.Trim( )+"|";
                }
                else
                {
                    if ( strExerciseUse2!=string.Empty )
                    {
                        strResult1+=strExerciseUse2.Trim( )+"|";
                    }
                }
                //内底长度比较
                SpecificationSizeCompare( "内底长度:" , strInnerBottomLength1 , strInnerBottomLength2 , ref strResult1 );
                //单丝细度比较
                SpecificationSizeCompare( "单丝细度:" , strMonofilamentFineness1 , strMonofilamentFineness2 , ref strResult1 );
                //规格尺寸比较
                SpecificationSizeCompare( "规格尺寸:" , strSpecificationSize1 , strSpecificationSize2 , ref strResult1 );
                //尺寸比较
                SpecificationSizeCompare( "尺寸:" , strSize1 , strSize2 , ref strResult1 );
                if ( strResult1.EndsWith( "|" ) )
                {
                    strResult1=strResult1.Substring( 0 , strResult1.Length-1 );
                    strResult=strResult1;
                }
                return true;
            }
        }
        
        private void SpecificationSizeCompare( string strKeyName , string strSpecificationSize1 , string strSpecificationSize2 , ref string strResult )
        {
            ArrayList SpecificationSizeList=new ArrayList( );
            string strSpecificationSizeUnit=String.Empty;
            string strSpecificationSizeNameUnit=String.Empty;
            //单丝细度
            //strResult=String.Empty;
            if ( ( strSpecificationSize1!=string.Empty )&&( strSpecificationSize2==string.Empty ) )
            {
                strResult+=strSpecificationSize1.Trim( )+"|";
                return;
            }
            if ( ( strSpecificationSize1==string.Empty )&&( strSpecificationSize2!=string.Empty ) )
            {
                strResult+=strSpecificationSize2.Trim( )+"|";
                return;
            }
            if ( ( strSpecificationSize1!=string.Empty )&&( strSpecificationSize2!=string.Empty ) )
            {
                if ( strSpecificationSize1.Trim( )==strSpecificationSize2.Trim( ) )
                {
                    strResult+=strSpecificationSize1.Trim( )+"|";
                    return;
                }
                if ( strSpecificationSize1.Contains( '~' ) )
                {
                    string[] ss1=strSpecificationSize1.Split( ':' )[1].Split( '~' );
                    string strnametemp=Regex.Replace( ss1[0] , @"(-?\d+)(\.\d+)?$" , "" , RegexOptions.IgnoreCase );
                    if ( strnametemp!="" )
                        strSpecificationSizeNameUnit=strnametemp;
                    string strvalue1=Regex.Replace( ss1[0] , "^[\u4e00-\u9fa5]+|[A-Za-z]+" , "" , RegexOptions.IgnoreCase );
                    SpecificationSizeList.Add( strvalue1 );
                    string strtemp=Regex.Replace( ss1[1] , @"^(-?\d+)(\.\d+)?" , "" , RegexOptions.IgnoreCase );
                    if ( strtemp!="" )
                        strSpecificationSizeUnit=strtemp;
                    string strvalue2=Regex.Replace( ss1[1] , "[\u4e00-\u9fa5]+|[A-Za-z]+$" , "" , RegexOptions.IgnoreCase );
                    SpecificationSizeList.Add( strvalue2 );
                }
                if ( strSpecificationSize2.Contains( '~' ) )
                {
                    string[] ss2=strSpecificationSize2.Split( ':' )[1].Split( '~' );
                    string strnametemp=Regex.Replace( ss2[0] , @"(-?\d+)(\.\d+)?$" , "" , RegexOptions.IgnoreCase );
                    if ( strnametemp!="" )
                        strSpecificationSizeNameUnit=strnametemp;
                    string strvalue1=Regex.Replace( ss2[0] , "^[\u4e00-\u9fa5]+|[A-Za-z]+" , "" , RegexOptions.IgnoreCase );
                    SpecificationSizeList.Add( strvalue1 );
                    string strtemp=Regex.Replace( ss2[1] , @"^(-?\d+)(\.\d+)?" , "" , RegexOptions.IgnoreCase );
                    if ( strtemp!="" )
                        strSpecificationSizeUnit=strtemp;
                    string strvalue2=Regex.Replace( ss2[1] , "[\u4e00-\u9fa5]+|[A-Za-z]+$" , "" , RegexOptions.IgnoreCase );
                    SpecificationSizeList.Add( strvalue2 );
                    SpecificationSizeList.Sort( );

                }
                if ( strSpecificationSize1.Contains( '~' )||strSpecificationSize2.Contains( '~' ) )
                {
                    strResult+=strKeyName+strSpecificationSizeUnit+SpecificationSizeList.ToArray( ).FirstOrDefault( ).ToString( )+"~"+SpecificationSizeList.ToArray( ).LastOrDefault( ).ToString( )+strSpecificationSizeUnit+"|";
                    return;
                }
                //if ( strSpecificationSize1.Contains( '~' )||strSpecificationSize2.Contains( '~' ) )
                //{
                //    strSpecificationSize1.Split( ':' )[1].Split( '~' ).ToList( ).ForEach( delegate( string strvalue )
                //    {
                //        string strnametemp=Regex.Replace( strvalue , "[\u4e00-\u9fa5]" , "" , RegexOptions.IgnoreCase );
                //        if ( strnametemp!="" )
                //            strSpecificationSizeNameUnit=strnametemp;
                //        string strtemp=Regex.Replace( strvalue , "[a-z]" , "" , RegexOptions.IgnoreCase );
                //        if ( strtemp!="" )
                //            strSpecificationSizeUnit=strtemp;
                //        SpecificationSizeList.Add( GetNumber( strvalue ) );
                //    } );
                //    strSpecificationSize2.Split( ':' )[1].Split( '~' ).ToList( ).ForEach( delegate( string strvalue )
                //    {
                //        string strnametemp=Regex.Replace( strvalue , "[\u4e00-\u9fa5]" , "" , RegexOptions.IgnoreCase );
                //        if ( strnametemp!="" )
                //            strSpecificationSizeNameUnit=strnametemp;
                //        string strtemp=Regex.Replace( strvalue , "[a-z]" , "" , RegexOptions.IgnoreCase );
                //        if ( strtemp!="" )
                //            strSpecificationSizeUnit=strtemp;
                //        SpecificationSizeList.Add( GetNumber( strvalue ) );
                //    } );
                //    SpecificationSizeList.Sort( );
                //    strResult+=strKeyName+strSpecificationSizeUnit+SpecificationSizeList.ToArray( ).FirstOrDefault( ).ToString( )+"~"+SpecificationSizeList.ToArray( ).LastOrDefault( ).ToString( )+strSpecificationSizeUnit+"|";
                //    return;
                //}
            }
        }

        private void UnionCLPSentialFactorTotalHandle( DataTable sourceDT , ref DataTable resultDT )
        {
            DataTable deleteDT=new DataTable( );
            deleteDT=sourceDT.Clone( );
            bool flag=false;
            string strResult;
            DataRow curDR;
            string strUNIT=string.Empty;
            string strEnglishName=string.Empty;
            string strHSCODE=string.Empty;
            string strLocalNAME=string.Empty;
            string strSentialFactor=string.Empty;
            string strQUANTITY=string.Empty;
            string strTOTAL_VALUE=string.Empty;
            string strNET_WEIGHT=string.Empty;
            string strGROSS_WEIGHT=string.Empty;
            string strSupervisionCondition=string.Empty;
            string strOrigin = string.Empty;
            if ( sourceDT.Rows.Count>0 )
            {
                curDR=sourceDT.Rows.OfType<DataRow>( ).First( );
                strUNIT=curDR["法定计量单位"].ToString( );
                strEnglishName=curDR["英文品名"].ToString( );
                strHSCODE=curDR["HS_CODE_(IN_CAT)"].ToString( );
                strLocalNAME=curDR["中文品名"].ToString( );
                strSentialFactor=curDR["申报要素"].ToString( );
                strQUANTITY=curDR["QUANTITY"].ToString( );
                strTOTAL_VALUE=curDR["TOTAL_VALUE"].ToString( );
                strNET_WEIGHT=curDR["NET_WEIGHT"].ToString( );
                strGROSS_WEIGHT=curDR["GROSS_WEIGHT"].ToString( );
                strSupervisionCondition=curDR["监管条件"].ToString( );
                strOrigin = curDR["ORIGIN"].ToString();
                sourceDT.Rows[0].Delete( );
                sourceDT.AcceptChanges( );
                //deleteDT.Rows.Add( curDR.ItemArray );
                //sourceDT.Rows[0].Delete( );
                //sourceDT.AcceptChanges( );

                //curDR.Delete( );
                //sourceDT.Rows.RemoveAt( 0 );
                //sourceDT.AcceptChanges( );
                for ( int i=0 ; i<sourceDT.Rows.Count ; i++ )
                {
                    //if ( ( sourceDT.Rows[i]["HS_CODE_(IN_CAT)"].ToString( )==strHSCODE )&&( sourceDT.Rows[i]["中文品名"].ToString( )==strLocalNAME ) )
                    // 新增原产国分组 by hualin 
                    if ((sourceDT.Rows[i]["HS_CODE_(IN_CAT)"].ToString() == strHSCODE) && (sourceDT.Rows[i]["中文品名"].ToString() == strLocalNAME) && (sourceDT.Rows[i]["ORIGIN"].ToString() == strOrigin))
                    {
                        strResult="";
                        flag=SentialFactorCompare( sourceDT.Rows[i]["申报要素"].ToString( ) , strSentialFactor , ref strResult );
                        if ( flag )
                        {
                            strQUANTITY=( int.Parse( sourceDT.Rows[i]["QUANTITY"].ToString( ) )+int.Parse( strQUANTITY ) ).ToString( );
                            strTOTAL_VALUE=( decimal.Parse( sourceDT.Rows[i]["TOTAL_VALUE"].ToString( ) )+decimal.Parse( strTOTAL_VALUE ) ).ToString( );
                            strNET_WEIGHT=( decimal.Parse( sourceDT.Rows[i]["NET_WEIGHT"].ToString( ) )+decimal.Parse( strNET_WEIGHT ) ).ToString( );
                            strGROSS_WEIGHT=( decimal.Parse( sourceDT.Rows[i]["GROSS_WEIGHT"].ToString( ) )+decimal.Parse( strGROSS_WEIGHT ) ).ToString( );
                            strSentialFactor=strResult;
                            //deleteDT.Rows.Add( curDR.ItemArray );
                            sourceDT.Rows[i].Delete( );
                            //sourceDT.Rows.RemoveAt( i );
                            //sourceDT.AcceptChanges( );
                            //tempDT.Rows.Add( curDR.ItemArray );
                        }
                    }
                }
                // 新增原产国分组 by hualin 
                resultDT.Rows.Add(new string[] { strOrigin, strQUANTITY, strUNIT, strHSCODE, strLocalNAME, strEnglishName, strSentialFactor, strSupervisionCondition, strTOTAL_VALUE, strNET_WEIGHT, strGROSS_WEIGHT });
                sourceDT.AcceptChanges( );
                UnionCLPSentialFactorTotalHandle( sourceDT , ref resultDT );
            }

        }
        private void UnionCLPSentialFactorTotalHandleForSet( DataTable sourceDT , ref DataTable resultDT )
        {
            DataTable deleteDT=new DataTable( );
            deleteDT=sourceDT.Clone( );
            bool flag=false;
            string strResult;
            DataRow curDR;
            string strUNIT=string.Empty;
            string strEnglishName=string.Empty;
            string strHSCODE=string.Empty;
            string strLocalNAME=string.Empty;
            string strSentialFactor=string.Empty;
            string strQUANTITY=string.Empty;
            string strTOTAL_VALUE=string.Empty;
            string strNET_WEIGHT=string.Empty;
            string strGROSS_WEIGHT=string.Empty;

            string ItemCode=string.Empty;
            string ShippingNumber=string.Empty;
            string OrderNumber=string.Empty;
            string PalletNumber=string.Empty;
            string ParcelNumber=string.Empty;
            string ModelCode=string.Empty;
            string Origin=string.Empty;
            string DispatchingKey=string.Empty;
            string EnglishComposition=string.Empty;
            string Size=string.Empty;
            string SupervisionCondition=string.Empty;
            string DoubleOrSet=string.Empty;
            string Description=string.Empty;
            string Brand=string.Empty;
            string TypeOfGoods=string.Empty;
            string Price=string.Empty;
            string Currency=string.Empty;
            string CommercialInvoiceNO=string.Empty;
            string StoreNO=string.Empty;
            string StoreName=string.Empty;
            string GroupID=string.Empty;
            string QuantityUnit=string.Empty;
            if ( sourceDT.Rows.Count>0 )
            {
                curDR=sourceDT.Rows.OfType<DataRow>( ).First( );

                ItemCode = curDR["ITEM_CODE"].ToString( );
                ShippingNumber = curDR["SHIPPING_NUMBER"].ToString( );
                OrderNumber = curDR["ORDER_NUMBER"].ToString( );
                PalletNumber = curDR["PALLET_NUMBER"].ToString( );
                ParcelNumber = curDR["PARCEL_NUMBER"].ToString( );
                ModelCode = curDR["MODEL_CODE"].ToString( );
                Origin = curDR["ORIGIN"].ToString( );
                QuantityUnit = curDR["QUANTITY_UNIT"].ToString( );
                DispatchingKey = curDR["DISPATCHING_KEY"].ToString( );
                EnglishComposition = curDR["English_Composition"].ToString( );
                Size = curDR["SIZE"].ToString( );

                SupervisionCondition =curDR["监管条件"].ToString( );
                DoubleOrSet = curDR["Double_or_Set"].ToString( );
                Description=curDR["备注"].ToString( );
                Brand = curDR["BRAND"].ToString( );
                TypeOfGoods =curDR["TYPE_OF_GOODS"].ToString( );
                Price = curDR["PRICE"].ToString( );
                Currency = curDR["CURRENCY"].ToString( );

                strUNIT=curDR["法定计量单位"].ToString( );
                strEnglishName=curDR["英文品名"].ToString( );
                strHSCODE=curDR["HS_CODE_(IN_CAT)"].ToString( );
                strLocalNAME=curDR["中文品名"].ToString( );
                strSentialFactor=curDR["申报要素"].ToString( );
                strQUANTITY=curDR["QUANTITY"].ToString( );
                strTOTAL_VALUE=curDR["TOTAL_VALUE"].ToString( );
                strNET_WEIGHT=curDR["NET_WEIGHT"].ToString( );
                strGROSS_WEIGHT=curDR["GROSS_WEIGHT"].ToString( );

                CommercialInvoiceNO = curDR["COMMERCIAL_INVOICE_NO"].ToString( );
                StoreNO = curDR["STORE_NO"].ToString( );
                StoreName = curDR["STORE_NAME"].ToString( );
                GroupID=curDR["GroupID"].ToString( );
                sourceDT.Rows[0].Delete( );
                sourceDT.AcceptChanges( );
                //deleteDT.Rows.Add( curDR.ItemArray );
                //sourceDT.Rows[0].Delete( );
                //sourceDT.AcceptChanges( );

                //curDR.Delete( );
                //sourceDT.Rows.RemoveAt( 0 );
                //sourceDT.AcceptChanges( );
                for ( int i=0 ; i<sourceDT.Rows.Count ; i++ )
                {
                    //if ( ( sourceDT.Rows[i]["HS_CODE_(IN_CAT)"].ToString( )==strHSCODE )&&( sourceDT.Rows[i]["中文品名"].ToString( )==strLocalNAME ) )
                    //分组添加原产国字段 by hualin @2016/4/27
                    if ((sourceDT.Rows[i]["HS_CODE_(IN_CAT)"].ToString() == strHSCODE) && (sourceDT.Rows[i]["中文品名"].ToString() == strLocalNAME) && (sourceDT.Rows[i]["ORIGIN"].ToString() == Origin))
                    {
                        strResult="";
                        flag=SentialFactorCompare( sourceDT.Rows[i]["申报要素"].ToString( ) , strSentialFactor , ref strResult );
                        if ( flag )
                        {
                            strQUANTITY=( int.Parse( sourceDT.Rows[i]["QUANTITY"].ToString( ) )+int.Parse( strQUANTITY ) ).ToString( );
                            strTOTAL_VALUE=( decimal.Parse( sourceDT.Rows[i]["TOTAL_VALUE"].ToString( ) )+decimal.Parse( strTOTAL_VALUE ) ).ToString( );
                            strNET_WEIGHT=( decimal.Parse( sourceDT.Rows[i]["NET_WEIGHT"].ToString( ) )+decimal.Parse( strNET_WEIGHT ) ).ToString( );
                            strGROSS_WEIGHT=( decimal.Parse( sourceDT.Rows[i]["GROSS_WEIGHT"].ToString( ) )+decimal.Parse( strGROSS_WEIGHT ) ).ToString( );
                            strSentialFactor=strResult;
                            //deleteDT.Rows.Add( curDR.ItemArray );
                            sourceDT.Rows[i].Delete( );
                            //sourceDT.Rows.RemoveAt( i );
                            //sourceDT.AcceptChanges( );
                            //tempDT.Rows.Add( curDR.ItemArray );
                        }
                    }
                }
                resultDT.Rows.Add( new string[] { ItemCode , ShippingNumber , OrderNumber ,
                PalletNumber , ParcelNumber , ModelCode ,Origin ,strQUANTITY ,QuantityUnit,DispatchingKey,EnglishComposition,Size,strUNIT,strHSCODE , strLocalNAME , strEnglishName , strSentialFactor ,SupervisionCondition,DoubleOrSet,Description,Brand,TypeOfGoods,Price,Currency, strTOTAL_VALUE , strNET_WEIGHT , strGROSS_WEIGHT,CommercialInvoiceNO,StoreNO,StoreName,GroupID } );
                sourceDT.AcceptChanges( );
                UnionCLPSentialFactorTotalHandleForSet( sourceDT , ref resultDT );
            }

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
            List<DataRow> KVkey=new List<DataRow>( );
            List<DataRow> KVvalue=new List<DataRow>( );
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
                                //break;
                            }
                        }
                    }
                    dicresult.Add( KVkey , KVvalue );
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
            sourceDT.Columns["ORIGIN"].SetOrdinal(9);
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
                        RemoveEmpty( _CLPTable );
                        string strunit=String.Empty;
                        int sumSET=0;
                        bool setFlag=false;
                        foreach ( DataRow dr in _CLPTable.Rows )
                        {
                            if ( dr["QUANTITY"].ToString( )!=""&&dr["QUANTITY_UNIT"].ToString( ).ToUpper().Contains("SET") )
                            {
                                if ( !setFlag )
                                {
                                    setFlag=true;
                                    sumSET=0;
                                }
                                sumSET+=int.Parse( dr["QUANTITY"].ToString( ) );
                                //intquantity+=int.Parse( dr["QUANTITY"].ToString( ) );
                            }
                            else if ( dr["QUANTITY"].ToString( )=="" )
                            {
                                dr["QUANTITY"]=sumSET.ToString( );
                                setFlag=false;
                            }
                        }
                        foreach ( DataRow dr in _CLPTable.Rows )
                        {
                            if ( dr["Double_or_Set"].ToString( )=="0" )
                                dr["Double_or_Set"]="N";
                            //strunit=dr["法定计量单位"].ToString( );
                            //if ( strunit.ToUpper( ).Contains( 'X' ) )
                            //{
                            //    string[] arr=strunit.ToUpper( ).Split( 'X' );
                            //    dr["QUANTITY"]=int.Parse( dr["QUANTITY"].ToString( ) )*int.Parse( arr[1] );
                            //    dr["法定计量单位"]=arr[0];
                            //    //if ( dr["QUANTITY"].ToString( ).Trim( )=="" )
                            //    //{
                            //    //    dr["QUANTITY"]=int.Parse( arr[1] );
                            //    //    dr["法定计量单位"]=arr[0];
                            //    //}
                            //    //else
                            //    //{
                            //    //    dr["QUANTITY"]=int.Parse( dr["QUANTITY"].ToString( ) )*int.Parse( arr[1] );
                            //    //    dr["法定计量单位"]=arr[0];
                            //    //}
                            //}
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
                _ParcelNumberDataSet.AcceptChanges( );
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
                    _UnionCLPTotalTable.Rows.Add( dr.ItemArray );
                }
            }
            _UnionCLPTotalTable.TableName="CLP初始";
            _UnionCLPTotalTable.AcceptChanges( );
            _ParcelNumberDataSet.Tables.Clear( );
            _ParcelNumberDataSet=SubTotalForUnionCLPTable( _UnionCLPTotalTable );
            SetSubForm( "FrmTotalCLPHandle" );
            toolStripStatusLabel1.Text="操作阶段：合并CLP文件";
            toolStripStatusLabel2.Text="操作结果：成功合并CLP文件！";

        }

        private void btnDealSet_Click( object sender , EventArgs e )
        {
            string strResult=string.Empty;
            //SentialFactorCompare( "织造方法:针织|种类:上衣|类别:婴儿|成分含量:85%涤纶,15%氨纶|尺寸:衣长26~38CM|品牌:TRIBORD" , "织造方法:针织|种类:上衣|类别:婴儿|成分含量:85%涤纶,15%氨纶|尺寸:衣长30~32CM|品牌:TRIBORD" , ref strResult );
            if ( !Flag )
            {
                MessageBox.Show( "处理SET数据之前请先合并CLP！" );
                return;
            }
            this.operateType=OperateType.UnionCLPSET;
            //if ( _UnionCLPSETTotalTable!=null)
            //    _UnionCLPSETTotalTable.Clear( );
            DataTable _SetBreakUpCloneTable=new DataTable( );
            DataTable _UnionCLPSETCloneTable=new DataTable( );
            _UnionCLPSETTotalTable=new DataTable( );
            _UnionCLPSETTotalTable=_UnionCLPSETTable.Clone( );
            //_UnionCLPSETTotalTable=_UnionCLPSETTable;
            _UnionCLPSETCloneTable=_UnionCLPSETTable.Copy( );
            _SetBreakUpCloneTable=_SetBreakUpTable.Clone( );
            Dictionary<List<DataRow> , List<DataRow>> dic=new Dictionary<List<DataRow> , List<DataRow>>( );
            Dictionary<List<DataRow> , List<DataRow>> DicClone=new Dictionary<List<DataRow> , List<DataRow>>( );
            _UnionCLPSETCloneTable.Columns.Add( "GroupID" , typeof( int ) );
            _UnionCLPSETTotalTable.Columns.Add( "GroupID" , typeof( int ) );
            //计算分组
            dic=UnionCLPSetTableSentialFactorTotalHandle( _UnionCLPSETCloneTable );
            //通过分组数据获取SET划分数据
            _SetBreakUpCloneTable=GetSetBreakUpDataRowGroupDataByCLPSetGroup( dic );
            //通过分组的各类比重计算价值、毛重、净重
            _UnionCLPSETCloneTable=UnionCLPSETDataRowGroupDataTotal( _UnionCLPSETCloneTable , _SetBreakUpCloneTable );
            //_UnionCLPSETTable.Columns.Remove( "GroupID" );
            //_UnionCLPSETTable.AcceptChanges( );
            string strunit=string.Empty;
            foreach ( DataRow row in _UnionCLPSETCloneTable.Rows )
            {
                if ( row["NET_WEIGHT"].ToString( )!="" )
                    row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( row["GROSS_WEIGHT"].ToString( )!="" )
                    row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
                strunit=row["法定计量单位"].ToString( );
                if ( strunit.ToUpper( ).Contains( 'X' ) )
                {
                    string[] arr=strunit.ToUpper( ).Split( 'X' );
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( arr[1] );
                    row["法定计量单位"]=arr[0];
                }
            }
            dic=UnionCLPSetTableSentialFactorTotalHandle1( _UnionCLPSETCloneTable.Copy( ) );

            DicClone=SetDataGourpCompare( dic );
            //UnionCLPSentialFactorTotalHandle11( dic );
            //_UnionCLPSETCloneTable.Clear( );
            foreach ( KeyValuePair<List<DataRow> , List<DataRow>> kv in DicClone )
            {
                foreach ( DataRow dr in kv.Key )
                {
                    _UnionCLPSETTotalTable.Rows.Add( dr.ItemArray );
                }
                foreach ( DataRow dr in kv.Value )
                {
                    _UnionCLPSETTotalTable.Rows.Add( dr.ItemArray );
                }
            }
            _UnionCLPSETCloneTable.Columns.Remove( "GroupID" );
            _UnionCLPSETCloneTable.AcceptChanges( );
            //_UnionCLPSETTotalCloneTable.Columns.Remove( "GroupID" );
            _UnionCLPSETTotalTable.AcceptChanges( );
            //_UnionCLPSETTotalTable=UnionCLPSentialFactorTotalHandleForSetData( _UnionCLPSETCloneTable );
            //_UnionCLPSETTotalTable=DeleteTableColumns( _UnionCLPSETTotalTable , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            //"DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","Double_or_Set","备注","BRAND","TYPE_OF_GOODS",
            //"PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            //添加原产国分组
            _UnionCLPSETTotalTable = DeleteTableColumns(_UnionCLPSETTotalTable, new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE",
            "DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","Double_or_Set","备注","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>());

            UnionCLPColumnSequenceAdjust( _UnionCLPSETTotalTable );
            _UnionCLPSETTotalTable.TableName="SET汇总";

            _ParcelNumberDataSet.Tables.Clear( );
            //_ParcelNumberDataSet.Tables.Add( _UnionCLPTotalTable );
            _ParcelNumberDataSet.Tables.Add( _UnionCLPSETCloneTable );
            //_UnionCLPSETTotalCloneTable=_UnionCLPSETTotalCloneTable.Copy( );
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
            string strunit=string.Empty;
            foreach ( DataRow row in _UnionCLPCommodityInspectionTable.Rows )
            {
                if ( row["NET_WEIGHT"].ToString( )!="" )
                    row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( row["GROSS_WEIGHT"].ToString( )!="" )
                    row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
                strunit=row["法定计量单位"].ToString( );
                if ( strunit.ToUpper( ).Contains( 'X' ) )
                {
                    string[] arr=strunit.ToUpper( ).Split( 'X' );
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( arr[1] );
                    row["法定计量单位"]=arr[0];
                }
            }
            DataTable tempDT=new DataTable( );
            DataTable copyDT=new DataTable( );
            tempDT=_UnionCLPCommodityInspectionTable.Copy( );
            //tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            //"DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","监管条件","Double_or_Set","BRAND","TYPE_OF_GOODS",
            //"PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            //添加原产国分钟 by hualin 2016/4/27
            tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE",
            "DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","Double_or_Set","备注","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            copyDT=UnionCLPSentialFactorExactCompareTotalHandle( tempDT );
            DataTable resultDT=new DataTable( );
            resultDT=copyDT.Clone( );
            UnionCLPSentialFactorTotalHandle( copyDT , ref resultDT );
            _UnionCLPCommodityInspectionTotalTable=resultDT;

            //copyDT=UnionCLPSentialFactorExactCompareTotalHandle( tempDT );
            //UnionCLPSentialFactorTotalHandle( copyDT , ref _UnionCLPCommodityInspectionTable );
            //_UnionCLPCommodityInspectionTotalTable=_UnionCLPCommodityInspectionTable;
            //_UnionCLPCommodityInspectionTotalTable=UnionCLPSentialFactorTotalHandle( _UnionCLPCommodityInspectionTable );
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
            string strunit=string.Empty;
            foreach ( DataRow row in _UnionCLPNonCommodityInspectionTable.Rows )
            {
                if ( row["NET_WEIGHT"].ToString( )!="" )
                    row["NET_WEIGHT"]=decimal.Round( decimal.Parse( row["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( row["GROSS_WEIGHT"].ToString( )!="" )
                    row["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( row["GROSS_WEIGHT"].ToString( ) ) , 2 );
                strunit=row["法定计量单位"].ToString( );
                if ( strunit.ToUpper( ).Contains( 'X' ) )
                {
                    string[] arr=strunit.ToUpper( ).Split( 'X' );
                    row["QUANTITY"]=int.Parse( row["QUANTITY"].ToString( ) )*int.Parse( arr[1] );
                    row["法定计量单位"]=arr[0];
                }
            }
            DataTable tempDT=new DataTable( );
            DataTable copyDT=new DataTable( );
            tempDT=_UnionCLPNonCommodityInspectionTable.Copy( );
            //tempDT=DeleteTableColumns( tempDT , new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE","ORIGIN",
            //"DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","Double_or_Set","备注","BRAND","TYPE_OF_GOODS",
            //"PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>( ) );
            //添加原产国分组 by hualin
            tempDT = DeleteTableColumns(tempDT, new string[]{"ITEM_CODE","SHIPPING_NUMBER","ORDER_NUMBER","PALLET_NUMBER","PARCEL_NUMBER","MODEL_CODE",
            "DISPATCHING_KEY","English_Composition","SIZE","QUANTITY_UNIT","Double_or_Set","备注","BRAND","TYPE_OF_GOODS",
            "PRICE","CURRENCY","COMMERCIAL_INVOICE_NO",	"STORE_NO",	"STORE_NAME"}.ToList<string>());
            copyDT=UnionCLPSentialFactorExactCompareTotalHandle( tempDT );
            DataTable resultDT=new DataTable( );
            resultDT=copyDT.Clone( );
            UnionCLPSentialFactorTotalHandle( copyDT , ref resultDT );
            _UnionCLPNonCommodityInspectionTotalTable=resultDT;

            //_UnionCLPNonCommodityInspectionTotalTable=UnionCLPSentialFactorTotalHandle( _UnionCLPNonCommodityInspectionTable );
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
                ||this.operateType==OperateType.UnionCLPCommodityInspection|| this.operateType==OperateType.UnionCLPNonCommodityInspection )
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

        private void btnCLPBasicDataTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"DataBase模板.xls" , FileName , false );
                MessageBox.Show( this , "DataBase模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }

        private void btnParcelTakeOutTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"掏箱Model模板.xls" , FileName , false );
                MessageBox.Show( this , "掏箱Model模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ParcelTakeOutDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=3 )
            {
                errMsg="掏箱Model表列数为3列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="Model_code" )
            {
                errMsg="掏箱Model表第一列为Model_code，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="中文品名" )
            {
                errMsg="掏箱Model表第二列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="备注" )
            {
                errMsg="掏箱Model表第三列为备注，请核对后重新导入！";
                return false;
            }
            return true;
        }
        private void btnParcelTakeOutInput_Click( object sender , EventArgs e )
        {
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ParcelTakeOutDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //dt.Columns.Add()
                new ParcelTakeOutBLL( ).BulkParcelTakeOutInsert( dt , 1000 );
                //basicDataBLL.BulkBasicDataInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}
                //_CLPBasicDataTable.Clear( );
                //_CLPBasicDataTable=basicDataBLL.GetDistinctModeCodeList( ).Tables[0];
                //_CLPBasicDataTable.AcceptChanges( );
                //_RepetitiveBasicDataTable.Clear( );
                //_RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                //_RepetitiveBasicDataTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "掏箱Model资料成功导入！" );
            }
        }

        private void btnParcelTakeOutOoutput_Click( object sender , EventArgs e )
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
                //string fileNameExt = localFilePath.Substring( localFilePath.LastIndexOf( "\\" ) + 1 ); //获取文件名，不带路径
                ExcelRender.RenderToExcel( new ParcelTakeOutBLL( ).GetParcelTakeOutList( "" ).Tables[0] , localFilePath );
                MessageBox.Show( "掏箱Model资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        private void btnShippingTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"出运Model模板.xls" , FileName , false );
                MessageBox.Show( this , "出运Model模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ShippingDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=4 )
            {
                errMsg="出运Model表列数为4列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="Item_code" )
            {
                errMsg="出运Model表第一列为Item code，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="Model_code" )
            {
                errMsg="出运Model表第二列为Model code，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="中文品名" )
            {
                errMsg="掏箱Model表第三列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[3].Caption!="Minimun_Export_QTY" )
            {
                errMsg="掏箱Model表第四列为Minimun Export QTY，请核对后重新导入！";
                return false;
            }
            return true;
        }
        private void btnShippingInput_Click( object sender , EventArgs e )
        {
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ShippingDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                new ParcelTakeOutConditionBLL( ).BulkShippingInsert( dt , 1000 );
                MessageBox.Show( "出运Model资料成功导入！" );
            }
        }

        private void btnShippingOutput_Click( object sender , EventArgs e )
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
                //string fileNameExt = localFilePath.Substring( localFilePath.LastIndexOf( "\\" ) + 1 ); //获取文件名，不带路径
                ExcelRender.RenderToExcel( new ParcelTakeOutConditionBLL( ).GetShippingList( "" ).Tables[0] , localFilePath );
                MessageBox.Show( "掏箱Model资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }

        #region 导出发票箱单
        private DataTable _ParcelSetUpTable;
        private DataTable SortByLocalProductNameType( DataTable sourceDT )
        {
            foreach ( DataRow dr in sourceDT.Rows )
            {
                if ( _ParcelSetUpTable.Select( "Flag=1 and LocalProductName='"+dr["中文品名"].ToString( )+"'" ).Length>0 )
                {
                     dr["BigClass"]=1;
                    //if ( dr["GroupID"].ToString( )=="" )
                       
                    //else
                    //{
                    //    foreach ( DataRow dr2 in sourceDT.Rows )
                    //    {
                    //        if ( dr2["GroupID"].ToString( )==dr["GroupID"].ToString( )&&dr["BigClass"].ToString()=="0")
                    //            dr["BigClass"]=1; 
                    //    }
                    //}
                }
            }
            sourceDT.Select( "BigClass=1 and GroupID>0" ).GroupBy( m => m.Field<int>( "GroupID" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<int , DataRow> ig )
            {
                foreach ( DataRow dr2 in sourceDT.Rows )
                {
                    if ( dr2["GroupID"].ToString( )==ig.Key.ToString())
                        dr2["BigClass"]=1;
                }
            } );
            //foreach ( DataRow dr in sourceDT.Rows )
            //{
            //    if ( _ParcelSetUpTable.Select( "Flag=1 and LocalProductName='"+dr["中文品名"].ToString( )+"'" ).Length>0 )
            //    {
            //        if ( dr["GroupID"].ToString( )=="" )
            //            dr["BigClass"]=1;
            //        else
            //        {
            //            foreach ( DataRow dr2 in sourceDT.Rows )
            //            {
            //                if ( dr2["GroupID"].ToString( )==dr["GroupID"].ToString( )&&dr["BigClass"].ToString( )=="0" )
            //                    dr["BigClass"]=1;
            //            }
            //        }
            //    }
            //}
            //DataView dv=sourceDT.DefaultView;
            //dv.Sort="BigClass ASC,NET_WEIGHT ASC,GROSS_WEIGHT ASC";
            //sourceDT=dv.ToTable( );
            return sourceDT;
        }
        
        private void UnionCLPCommodityInspectionDataForBill(ref  DataTable sourceSetTable , DataTable _sourceCommodityInspectionTable ,ref int i, ref Dictionary<int , List<DataRow>> dic )
        {
            List<DataRow> CommodityInspectionSETLST=new List<DataRow>( );
            //筛选set数据，将set数据放入对应的商检与非商检数据
            var query = from uclp in sourceSetTable.AsEnumerable( )
                        where uclp.Field<String>( "监管条件" ).ToUpper( ).Contains( "B" )
                        group uclp by new { t1 = uclp.Field<int>( "GroupID" ) } into m
                        select new
                        {
                            GroupID = m.Key.t1
                        };
            List<DataRow> listHave=new List<DataRow>();
            List<DataRow> lst;

            //DataTable dttemp11=_UnionCLPSETTotalTable.Clone( );
            //int curNum=0;
            //int i=0;

            //Dictionary<int , List<DataRow> >dic=new Dictionary<int , List<DataRow>>( );
            
            if ( query.Count( )>0 )
            {
                listHave=new List<DataRow>( );
                dic.Add( ++i , listHave );
                foreach ( var obj in query)
                {
                    lst=new List<DataRow>( );
                    foreach(DataRow dr in sourceSetTable.Rows)
                    {
                        if(int.Parse(dr["GroupID"].ToString())==obj.GroupID)                                                
                        {
                            dr["数据类型"]="商检数据";
                        }
                    }
                    lst=sourceSetTable.Select( "GroupID="+obj.GroupID ).AsEnumerable<DataRow>( ).ToList( );
                    //sourceSetTable.Select( "GroupID="+obj.GroupID )
                    //CommodityInspectionSETLST.AddRange( lst );
                    //CommodityInspectionSETDT.
                    //sourceSetTable=sourceSetTable.AsEnumerable( ).Except( lst ).CopyToDataTable( );
                    //sourceSetTable.AcceptChanges( );

                    //foreach ( DataRow dr in lst )
                    //    dr["数据类型"]="商检数据";
                    //_UnionCLPSETTotalCloneTable.AsEnumerable( ).ToList<DataRow>( ).AddRange( lst );
                    if ( listHave.Count+lst.Count<=20 )
                    {
                        listHave.AddRange( lst );
                        dic[i]=listHave;
                    }
                    else
                    {
                        if ( lst.Count>0 )
                        {
                            listHave=new List<DataRow>( );
                            listHave.AddRange( lst );
                            dic.Add( ++i , listHave );
                        }

                    }
                }
                //DataTable dt=CommodityInspectionSETLST.Distinct( ).CopyToDataTable();
            }
            DataTable tempDT;
            foreach ( KeyValuePair<int , List<DataRow>> kvp in dic )
            {
                if ( kvp.Value.Count>0 )
                {
                    while ( kvp.Value.Count<20 && kvp.Value.CopyToDataTable( ).Select( "数据类型='商检数据'" ).Count( )>0 &&_sourceCommodityInspectionTable.Rows.Count>0 )
                    {

                        tempDT=new DataTable( );
                        tempDT=sourceSetTable.Clone( );
                        tempDT.Rows.Add( new object[] { _sourceCommodityInspectionTable.Rows[0]["中文品名"] , _sourceCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceCommodityInspectionTable.Rows[0]["申报要素"] , _sourceCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                            _sourceCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceCommodityInspectionTable.Rows[0]["英文品名"] , _sourceCommodityInspectionTable.Rows[0]["法定计量单位"] ,  _sourceCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                            _sourceCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceCommodityInspectionTable.Rows[0]["监管条件"] , _sourceCommodityInspectionTable.Rows[0]["GroupID"],_sourceCommodityInspectionTable.Rows[0]["数据类型"],_sourceCommodityInspectionTable.Rows[0]["BigClass"] } );
                        kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                        _sourceCommodityInspectionTable.Rows.RemoveAt( 0 );
                        _sourceCommodityInspectionTable.AcceptChanges( );
                    }
                }
                else
                {
                    tempDT=new DataTable( );
                    tempDT=sourceSetTable.Clone( );
                    tempDT.Rows.Add( new object[] { _sourceCommodityInspectionTable.Rows[0]["中文品名"] , _sourceCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceCommodityInspectionTable.Rows[0]["申报要素"] , _sourceCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                            _sourceCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceCommodityInspectionTable.Rows[0]["英文品名"] , _sourceCommodityInspectionTable.Rows[0]["法定计量单位"] ,  _sourceCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                            _sourceCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceCommodityInspectionTable.Rows[0]["监管条件"] , _sourceCommodityInspectionTable.Rows[0]["GroupID"],_sourceCommodityInspectionTable.Rows[0]["数据类型"],_sourceCommodityInspectionTable.Rows[0]["BigClass"] } );
                    kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                    _sourceCommodityInspectionTable.Rows.RemoveAt( 0 );
                    _sourceCommodityInspectionTable.AcceptChanges( );
                }
                //if ( _sourceCommodityInspectionTable.Rows.Count>0 )
                //{

                //}
                //if ( _sourceCommodityInspectionTable.Rows.Count>0 )
                //{
                //    while ( kvp.Value.Count<20 && kvp.Value.CopyToDataTable( ).Select( "数据类型='商检数据'" ).Count( )>0 )
                //    {

                //        tempDT=new DataTable( );
                //        tempDT=sourceSetTable.Clone( );
                //        tempDT.Rows.Add( new object[] { _sourceCommodityInspectionTable.Rows[0]["中文品名"] , _sourceCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceCommodityInspectionTable.Rows[0]["申报要素"] , _sourceCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                //            _sourceCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceCommodityInspectionTable.Rows[0]["英文品名"] , _sourceCommodityInspectionTable.Rows[0]["法定计量单位"] ,  _sourceCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                //            _sourceCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceCommodityInspectionTable.Rows[0]["监管条件"] , _sourceCommodityInspectionTable.Rows[0]["GroupID"],_sourceCommodityInspectionTable.Rows[0]["数据类型"],_sourceCommodityInspectionTable.Rows[0]["BigClass"] } );
                //        kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                //        _sourceCommodityInspectionTable.Rows.RemoveAt( 0 );
                //        _sourceCommodityInspectionTable.AcceptChanges( );
                //    }
                //}
            }
            //if ( sourceSetTable.Rows.Count>CommodityInspectionSETLST.AsEnumerable( ).Count( ) )
            //    sourceSetTable=sourceSetTable.AsEnumerable( ).Except( CommodityInspectionSETLST.AsEnumerable( ) ).CopyToDataTable( );
            //else
            //    sourceSetTable.Clear( );
            //sourceSetTable.AcceptChanges( );

            if ( _sourceCommodityInspectionTable.Rows.Count<=0 )
            {
                return;
            }
            for ( int j=0 ; j<_sourceCommodityInspectionTable.Rows.Count ; j++ )
            {
                //if ( j%20==0 )
                //从20增加到48
                if (j % 48 == 0)
                {
                    listHave=new List<DataRow>( );
                    listHave.Add( _sourceCommodityInspectionTable.Rows[j] );
                    dic.Add( ++i , listHave );
                }
                else
                {
                    listHave.Add( _sourceCommodityInspectionTable.Rows[j] );
                    dic[i]=listHave;
                }
            }

        }
        //非商检数据分票
        private void UnionCLPNonCommodityInspectionDataForBill(ref DataTable sourceSetTable , DataTable _sourceNonCommodityInspectionTable , ref int i , ref Dictionary<int , List<DataRow>> dic )
        {
            //筛选set数据，将set数据放入对应的商检与非商检数据
            var query = from uclp in sourceSetTable.AsEnumerable( )
                        where !uclp.Field<String>( "监管条件" ).ToUpper( ).Contains( "B" ) && uclp.Field<string>( "数据类型" )!="商检数据"
                        group uclp by new { t1 = uclp.Field<int>( "GroupID" ) } into m
                        select new
                        {
                            GroupID = m.Key.t1
                        };
            List<DataRow> listHave=new List<DataRow>();
            List<DataRow> lst;

            //DataTable dttemp11=_UnionCLPSETTotalTable.Clone( );
            //int curNum=0;
            //int i=0;

            //Dictionary<int , List<DataRow> >dic=new Dictionary<int , List<DataRow>>( );
            if ( query.Count( )>0 )
            {
                listHave=new List<DataRow>( );
                dic.Add( ++i , listHave );
                foreach ( var obj in query )
                {
                    lst=new List<DataRow>( );
                    foreach ( DataRow dr in sourceSetTable.Rows )
                    {
                        if ( int.Parse( dr["GroupID"].ToString( ) )==obj.GroupID )
                        {
                            dr["数据类型"]="非商检数据";
                        }
                    }
                    lst=sourceSetTable.Select( "GroupID="+obj.GroupID ).AsEnumerable<DataRow>( ).ToList( );
                    //sourceSetTable=sourceSetTable.AsEnumerable( ).Except( lst ).CopyToDataTable( );
                    //sourceSetTable.AcceptChanges( );
                    //foreach ( DataRow dr in lst )
                    //    dr["数据类型"]="非商检数据";
                    //_UnionCLPSETTotalCloneTable.AsEnumerable( ).ToList<DataRow>( ).AddRange( lst );
                    if ( listHave.Count+lst.Count<=20 )
                    {
                        listHave.AddRange( lst );
                        dic[i]=listHave;
                    }
                    else
                    {
                        if ( lst.Count>0 )
                        {
                            listHave=new List<DataRow>( );
                            listHave.AddRange( lst );
                            dic.Add( ++i , listHave );
                        }
                        //else
                        //{
                        //    listHave=new List<DataRow>( );
                        //    //listHave.AddRange( lst );
                        //    dic.Add( ++i , listHave );
                        //}
                    }
                }
                
            }
            DataTable tempDT;
            foreach ( KeyValuePair<int , List<DataRow>> kvp in dic )
            {
                
                //kvp.Value.Select( "数据类型='非商检数据'" ).Count( )
                if ( kvp.Value.Count>0 )
                {
                    while ( kvp.Value.Count<20 && kvp.Value.CopyToDataTable( ).Select( "数据类型='非商检数据'" ).Count( )>0 &&_sourceNonCommodityInspectionTable.Rows.Count>0 )
                    {
                        tempDT=new DataTable( );
                        tempDT=sourceSetTable.Clone( );
                        tempDT.Rows.Add( new object[] { _sourceNonCommodityInspectionTable.Rows[0]["中文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceNonCommodityInspectionTable.Rows[0]["申报要素"] , _sourceNonCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                                _sourceNonCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceNonCommodityInspectionTable.Rows[0]["英文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["法定计量单位"] , _sourceNonCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                                _sourceNonCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceNonCommodityInspectionTable.Rows[0]["监管条件"] , _sourceNonCommodityInspectionTable.Rows[0]["GroupID"],_sourceNonCommodityInspectionTable.Rows[0]["数据类型"],_sourceNonCommodityInspectionTable.Rows[0]["BigClass"] } );
                        kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                        _sourceNonCommodityInspectionTable.Rows.RemoveAt( 0 );
                        _sourceNonCommodityInspectionTable.AcceptChanges( );
                    }
                }
                else
                {
                    tempDT=new DataTable( );
                    tempDT=sourceSetTable.Clone( );
                    tempDT.Rows.Add( new object[] { _sourceNonCommodityInspectionTable.Rows[0]["中文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceNonCommodityInspectionTable.Rows[0]["申报要素"] , _sourceNonCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                                _sourceNonCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceNonCommodityInspectionTable.Rows[0]["英文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["法定计量单位"] , _sourceNonCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                                _sourceNonCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceNonCommodityInspectionTable.Rows[0]["监管条件"] , _sourceNonCommodityInspectionTable.Rows[0]["GroupID"],_sourceNonCommodityInspectionTable.Rows[0]["数据类型"],_sourceNonCommodityInspectionTable.Rows[0]["BigClass"] } );
                    kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                    _sourceNonCommodityInspectionTable.Rows.RemoveAt( 0 );
                    _sourceNonCommodityInspectionTable.AcceptChanges( );
                }
                //if ( _sourceNonCommodityInspectionTable.Rows.Count>0 )
                //{
                //    while ( kvp.Value.Count<20 && kvp.Value.CopyToDataTable( ).Select( "数据类型='非商检数据'" ).Count( )>0 )
                //    {
                //        tempDT=new DataTable( );
                //        tempDT=sourceSetTable.Clone( );
                //        tempDT.Rows.Add( new object[] { _sourceNonCommodityInspectionTable.Rows[0]["中文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["HS_CODE_(IN_CAT)"] , _sourceNonCommodityInspectionTable.Rows[0]["申报要素"] , _sourceNonCommodityInspectionTable.Rows[0]["QUANTITY"] , 
                //            _sourceNonCommodityInspectionTable.Rows[0]["TOTAL_VALUE"] , _sourceNonCommodityInspectionTable.Rows[0]["英文品名"] , _sourceNonCommodityInspectionTable.Rows[0]["法定计量单位"] , _sourceNonCommodityInspectionTable.Rows[0]["NET_WEIGHT"] , 
                //            _sourceNonCommodityInspectionTable.Rows[0]["GROSS_WEIGHT"] , _sourceNonCommodityInspectionTable.Rows[0]["监管条件"] , _sourceNonCommodityInspectionTable.Rows[0]["GroupID"],_sourceNonCommodityInspectionTable.Rows[0]["数据类型"],_sourceNonCommodityInspectionTable.Rows[0]["BigClass"] } );
                //        kvp.Value.AddRange( tempDT.AsEnumerable( ) );
                //        _sourceNonCommodityInspectionTable.Rows.RemoveAt( 0 );
                //        _sourceNonCommodityInspectionTable.AcceptChanges( );

                //    }
                //}

            }
            if ( _sourceNonCommodityInspectionTable.Rows.Count<=0 )
            {
                return;
            }
            for ( int j=0 ; j<_sourceNonCommodityInspectionTable.Rows.Count ; j++ )
            {
                if ( j%20==0 )
                {
                    listHave=new List<DataRow>( );
                    listHave.Add( _sourceNonCommodityInspectionTable.Rows[j] );
                    dic.Add( ++i , listHave );
                }
                else
                {
                    listHave.Add( _sourceNonCommodityInspectionTable.Rows[j] );
                    dic[i]=listHave;
                }
            }
        }
        private void btnBillSetUp_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPForSetUpBill;
            DataTable _UnionCLPSETTotalTableCopy=new DataTable( );
            _UnionCLPSETTotalTableCopy=_UnionCLPSETTotalTable.Copy( );
            _UnionCLPSETTotalTableCopy.Columns.Add( "数据类型" , typeof( string ) );
            _UnionCLPSETTotalTableCopy.Columns.Add( "BigClass" , typeof( int ) );
            _UnionCLPSETTotalCloneForBillTable=new DataTable( );
            _UnionCLPSETTotalCloneForBillTable=_UnionCLPSETTotalTableCopy.Clone( );
            DataTable _UnionCLPCommodityInspectionTotalCloneTable=_UnionCLPCommodityInspectionTotalTable.Copy( );
            DataTable _UnionCLPNonCommodityInspectionTotalCloneTable=_UnionCLPNonCommodityInspectionTotalTable.Copy( );
            _UnionCLPCommodityInspectionTotalCloneTable.Columns.Add( "GroupID" , typeof( int ) );
            _UnionCLPCommodityInspectionTotalCloneTable.Columns.Add( "数据类型" , typeof( string ) );
            _UnionCLPCommodityInspectionTotalCloneTable.Columns.Add( "BigClass" , typeof( int ) );
            _UnionCLPNonCommodityInspectionTotalCloneTable.Columns.Add( "GroupID" , typeof( int ) );
            _UnionCLPNonCommodityInspectionTotalCloneTable.Columns.Add( "数据类型" , typeof( string ) );
            _UnionCLPNonCommodityInspectionTotalCloneTable.Columns.Add( "BigClass" , typeof( int ) );
            //_UnionCLPSETTotalCloneForBillTable.Columns.Add( "数据类型" , typeof( string ) );
            _UnionCLPSETTotalCloneForBillTable.Columns.Add( "BillNO" , typeof( int ) );
            int i=0;
            //区分大类与小类
            foreach ( DataRow dr in _UnionCLPSETTotalTableCopy.Rows )
            {
                dr["BigClass"]="0";
            }
            _UnionCLPSETTotalTableCopy=SortByLocalProductNameType( _UnionCLPSETTotalTableCopy );
            //分票数据保存
            Dictionary<int , List<DataRow>> dic=new Dictionary<int , List<DataRow>>( );
            foreach ( DataRow dr in _UnionCLPCommodityInspectionTotalCloneTable.Rows )
            {
                dr["GroupID"]=0;
                dr["BigClass"]=0;
                dr["数据类型"]="商检数据";
            }
            //_UnionCLPCommodityInspectionTotalCloneTable=SortByLocalProductNameType( _UnionCLPCommodityInspectionTotalCloneTable );
            UnionCLPCommodityInspectionDataForBill(ref _UnionCLPSETTotalTableCopy , _UnionCLPCommodityInspectionTotalCloneTable , ref i , ref dic );
            _UnionCLPCommodityInspectionTotalCloneTable=SortByLocalProductNameType( _UnionCLPCommodityInspectionTotalCloneTable );
            foreach ( DataRow dr in _UnionCLPNonCommodityInspectionTotalCloneTable.Rows )
            {
                dr["GroupID"]=0;
                dr["BigClass"]=0;
                dr["数据类型"]="非商检数据";
            }
            //_UnionCLPNonCommodityInspectionTotalCloneTable=SortByLocalProductNameType( _UnionCLPNonCommodityInspectionTotalCloneTable );
            UnionCLPNonCommodityInspectionDataForBill(ref _UnionCLPSETTotalTableCopy , _UnionCLPNonCommodityInspectionTotalCloneTable , ref i , ref dic );
            _UnionCLPNonCommodityInspectionTotalCloneTable=SortByLocalProductNameType( _UnionCLPNonCommodityInspectionTotalCloneTable );
            foreach ( KeyValuePair<int , List<DataRow>> kvp in dic )
            {
                foreach ( DataRow dr in kvp.Value )
                {
                    _UnionCLPSETTotalCloneForBillTable.Rows.Add(new object[] { dr["中文品名"], dr["HS_CODE_(IN_CAT)"], dr["申报要素"], dr["QUANTITY"], dr["TOTAL_VALUE"], dr["英文品名"], dr["法定计量单位"], dr["NET_WEIGHT"], dr["GROSS_WEIGHT"], dr["ORIGIN"], dr["监管条件"], dr["GroupID"], dr["数据类型"], dr["BigClass"], kvp.Key });
                }
            }
            DataRow[] drs;
            foreach ( DataRow dr in _UnionCLPSETTotalCloneForBillTable.Rows )
            {
                if ( dr["BigClass"].ToString( )=="0" )
                {
                    if ( decimal.Parse( dr["NET_WEIGHT"].ToString( ) )/int.Parse( dr["QUANTITY"].ToString( ) )>=1 )
                    {
                        drs=_NetWeightAdjustmentTable.Select( "LocalProductName='"+dr["中文品名"].ToString( )+"'" );
                        if ( drs.Length>0 )
                            dr["NET_WEIGHT"]=decimal.Parse( dr["QUANTITY"].ToString( ) )*decimal.Parse( drs[0]["AdjustRatio"].ToString( ) );
                    }
                }
            }

            foreach ( DataRow dr in _UnionCLPSETTotalCloneForBillTable.Rows )
            {
                if ( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) )-decimal.Parse( dr["NET_WEIGHT"].ToString( ) )>=1 )
                {
                    if ( dr["BigClass"].ToString( )=="1" )
                    {
                        //DataTable dd=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( ) ==dr["中文品名"].ToString( ) ).CopyToDataTable( );
                        //dd.AsEnumerable( ).GroupBy( a => a["PARCEL_NUMBER"] ).ToList( );
                        //int ss=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( ) ==dr["中文品名"].ToString( ) ).GroupBy( a => a["PARCEL_NUMBER"] ).Count();
                        dr["GROSS_WEIGHT"]=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( ) ==dr["中文品名"].ToString( ) ).GroupBy( a => a["PARCEL_NUMBER"] ).Count( )*1+decimal.Parse( dr["NET_WEIGHT"].ToString( ) );
                    }
                    else
                    {
                        dr["GROSS_WEIGHT"]=decimal.Parse( dr["QUANTITY"].ToString( ) )/30+decimal.Parse( dr["NET_WEIGHT"].ToString( ) );
                    }
                }

            }
            foreach ( DataRow dr in _UnionCLPSETTotalCloneForBillTable.Rows )
            {
                if ( dr["NET_WEIGHT"].ToString( )!="" )
                    dr["NET_WEIGHT"]=decimal.Round( decimal.Parse( dr["NET_WEIGHT"].ToString( ) ) , 2 );
                if ( dr["GROSS_WEIGHT"].ToString( )!="" )
                    dr["GROSS_WEIGHT"]= decimal.Round( decimal.Parse( dr["GROSS_WEIGHT"].ToString( ) ) , 2 );
            }
            var netgrossweightquery = from uclp in _UnionCLPSETTotalCloneForBillTable.AsEnumerable( )
                        group uclp by new { t1 = uclp.Field<int>( "BillNO" ) } into m
                        select new
                        {
                            BillNO = m.Key.t1,
                            sumNetWeight=m.Sum(p => decimal.Parse( p.Field<string>( "NET_WEIGHT" ) )),
                            sumGrossWeight=m.Sum(p => decimal.Parse( p.Field<string>( "GROSS_WEIGHT" ) ))

                        };
            //_UnionCLPSETTotalCloneForBillTable.AsEnumerable( ).GroupBy( a => a["GroupID"] )..Sum( p => decimal.Parse( p.Field<string>( "TOTAL_VALUE" ) ) );
            //DataTable dttt=_UnionCLPSETTotalCloneForBillTable;
            SetSubForm( "FrmCLPDataHandleForBill" );
            string errMsg=string.Empty;
            foreach ( var obj in netgrossweightquery )
            {
                if ( obj.sumGrossWeight<1 )
                {
                    errMsg+=string.Format( "票据号为{0}的毛重之和小于1" , obj.BillNO.ToString( ) );
                }
                else if ( obj.sumNetWeight<1 )
                {
                    errMsg+=string.Format( "票据号为{0}的净重之和小于1" , obj.BillNO.ToString( ) );
                }
            }
            if ( !Directory.Exists( UnionCLPPath ) )
                Directory.CreateDirectory( UnionCLPPath );
            ArrayList shippingnumbers=new ArrayList( );
            string filename="CLP";
            _UnionCLPTotalTable.AsEnumerable( ).GroupBy( m => m.Field<string>( "SHIPPING_NUMBER" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<string , DataRow> ig )
            {
                filename+="-"+ig.Key;
                shippingnumbers.Add( ig.Key );
            } );
            ExcelRender.RenderToExcel( _UnionCLPSETTotalCloneForBillTable , UnionCLPPath+filename+".xls" );
            if ( errMsg!=string.Empty )
            {
                MessageBox.Show( errMsg+"，需要进行人工调整!" );
            }
            //toolStripStatusLabel1.Text="成功进行非商检数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理分票数据";
            toolStripStatusLabel2.Text="操作结果：成功进行分票数据处理！";
        }
        private void updateParcelNumberForBillNO( int billNo , int parcelNumbers )
        {
            foreach ( DataRow dr in _BillSmallClassNetWeightTable.Rows)
            {
                if ( int.Parse( dr["BillNO"].ToString( ) )==billNo )
                {
                    if ( dr["CartonQty"].ToString( ) !="" )
                        dr["CartonQty"]=int.Parse( dr["CartonQty"].ToString( ) )+parcelNumbers;
                    else
                        dr["CartonQty"]=parcelNumbers;
                }
            }
        }
        private void updateSmallClassNetWeightForBillNO( int billNo , decimal netweight )
        {
            foreach ( DataRow dr in _BillSmallClassNetWeightTable.Rows )
            {
                if ( int.Parse( dr["BillNO"].ToString( ) )==billNo )
                {
                    dr["NW"]=netweight;
                        
                }
            }
        }
        private void updateParcelNumberForBillNO( int billNo , decimal netWeightRadio , int parcelNumbers )
        {
            foreach ( DataRow dr in _BillSmallClassNetWeightTable.Rows )
            {
                if ( int.Parse( dr["BillNO"].ToString( ) )==billNo )
                {
                    if ( dr["CartonQty"].ToString( ) !="" )
                        dr["CartonQty"]=int.Parse( dr["CartonQty"].ToString( ) )+parcelNumbers;
                    else
                        dr["CartonQty"]=parcelNumbers;
                    dr["NWRadio"]=netWeightRadio;
                }
            }
        }
        private int _TotalParcelNumber=0;
        private void btnParcelSetUp_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPForParcelNumber;
            _BillSmallClassNetWeightTable=new DataTable( );
            CreateBillSmallClassNetWeightTableSchema( );
            _UnionCLPSETTotalCloneForBillTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "BillNO" ),m => m ).ToList().ForEach( delegate( IGrouping<int,DataRow> ig )
            {
                DataRow row=_BillSmallClassNetWeightTable.NewRow( );
                row["BillNO"]=ig.Key;
                row["Qty"]=ig.ToList<DataRow>().Sum(p=>int.Parse(p.Field<string>("QUANTITY")));
                row["Amount"]=ig.ToList<DataRow>().Sum( p => decimal.Parse( p.Field<string>( "TOTAL_VALUE" ) ) );
                row["CartonQty"]=0;
                row["GW"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "GROSS_WEIGHT" ) ) );
                row["NW"]=0;
                row["Volume"]=0;
                row["NWRadio"]=0;
                _BillSmallClassNetWeightTable.Rows.Add( row );
            } );
            _TotalParcelNumber=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["PARCEL_NUMBER"].ToString( ) !="" ).GroupBy( a => a["PARCEL_NUMBER"] ).Count( );
            int totalBigClassParcelNumber=0;
            int totalsmallClassParcelNumber=0;
            int totalsmallClassParcelNumber1=0;
            int tempparcelNumber=0;
            var query = from uclp in _UnionCLPSETTotalCloneForBillTable.AsEnumerable( )
                where uclp.Field<int>( "GroupID" )>0 && uclp.Field<int>( "BigClass" )==1
                group uclp by new { t0 = uclp.Field<int>( "BillNO" ) , t1 = uclp.Field<int>( "GroupID" ) } into m
            select new
            {
                BillNO=m.Key.t0,
                GroupID = m.Key.t1,
                Quantity=int.Parse(m.FirstOrDefault()["QUANTITY"].ToString()),
                LocalProductName=m.FirstOrDefault()["中文品名"].ToString()
            };
            if ( query.Count( )>0 )
            {
                //foreach ( var obj in query.GroupBy( a => a.LocalProductName ).Select( g => g.First( ) ) )
                int totalParcelNumber;
                decimal totalQuantity;
                foreach ( var obj in query )
                {
                    totalParcelNumber=0;
                    totalQuantity=0;
                    tempparcelNumber=0;
                    totalQuantity=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( )== obj.LocalProductName ).Sum( a => decimal.Parse( a["QUANTITY"].ToString( ) ) );
                    totalParcelNumber=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( )== obj.LocalProductName ).GroupBy( a => a["PARCEL_NUMBER"] ).Count( );
                    tempparcelNumber=int.Parse(Math.Round( totalParcelNumber*obj.Quantity/totalQuantity).ToString()); 
                    updateParcelNumberForBillNO( obj.BillNO , tempparcelNumber );
                    totalBigClassParcelNumber+=tempparcelNumber;
                }
            }
            var query1 = from uclp in _UnionCLPSETTotalCloneForBillTable.AsEnumerable( )
                        where uclp.Field<int>( "GroupID" )==0 && uclp.Field<int>( "BigClass" )==1
                         select new
                         {
                             BillNO=uclp.Field<int>( "BillNO" ).ToString( ),
                             Quantity=int.Parse(uclp.Field<string>("QUANTITY" )),
                             LocalProductName=uclp.Field<string>( "中文品名" ).ToString( )
                         };
            if ( query1.Count( )>0 )
            {
                int totalParcelNumber;
                decimal totalQuantity;
                foreach ( var obj in query1 )
                {
                    totalParcelNumber=0;
                    totalQuantity=0;
                    //tempparcelNumber=0;
                    tempparcelNumber=0;
                    totalQuantity=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( )== obj.LocalProductName ).Sum( a => decimal.Parse( a["QUANTITY"].ToString( ) ) );
                    totalParcelNumber=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( )== obj.LocalProductName ).GroupBy( a => a["PARCEL_NUMBER"] ).Count( );
                    tempparcelNumber=int.Parse( Math.Round( totalParcelNumber*obj.Quantity/totalQuantity ).ToString( ) );
                    //updateParcelNumberForBillNO( obj.BillNO , tempparcelNumber );
                    //totalBigClassParcelNumber+=tempparcelNumber;
                    
                    //tempparcelNumber=_UnionCLPTotalTable.AsEnumerable( ).Where<DataRow>( row => row["中文品名"].ToString( )== obj.LocalProductName ).GroupBy( a => a["PARCEL_NUMBER"] ).Count( );
                    updateParcelNumberForBillNO( int.Parse(obj.BillNO) , tempparcelNumber );
                    totalBigClassParcelNumber+=tempparcelNumber;
                }
            }
            totalsmallClassParcelNumber=_TotalParcelNumber-totalBigClassParcelNumber;

            var query2 = from uclp in _UnionCLPSETTotalCloneForBillTable.AsEnumerable( )
                         where uclp.Field<int>( "BigClass" )==0 
                         group uclp by uclp.Field<int>( "BillNO" ) into m
                         select new
                         {
                             BillNO = m.Key ,
                             BillSmallClassNetWeight=m.Sum( m1 => decimal.Parse( m1.Field<string>( "NET_WEIGHT" ) ) )
                         };
            decimal BillTotalSmallClassNetWeight=0;
            foreach ( var obj in query2 )
            {
                updateSmallClassNetWeightForBillNO( obj.BillNO , obj.BillSmallClassNetWeight );
                BillTotalSmallClassNetWeight+=obj.BillSmallClassNetWeight;
            }

            //_BillSmallClassNetWeightTable=new DataTable( );
            //CreateBillSmallClassNetWeightTableSchema( );
            foreach ( var obj in query2 )
            {
                updateParcelNumberForBillNO( obj.BillNO , obj.BillSmallClassNetWeight/BillTotalSmallClassNetWeight , (int)( obj.BillSmallClassNetWeight/BillTotalSmallClassNetWeight*totalsmallClassParcelNumber ) );
                //DataRow dr=_BillSmallClassNetWeightTable.NewRow( );
                //dr["BillNO"]=obj.BillNO;
                //dr["NetWeightRadio"]=obj.BillSmallClassNetWeight/BillTotalSmallClassNetWeight;
                //dr["ParcelNumber"]=obj.BillSmallClassNetWeight/BillTotalSmallClassNetWeight*totalParcelNumber;
                //_BillSmallClassNetWeightTable.Rows.Add( dr );
                totalsmallClassParcelNumber1+=(int)( obj.BillSmallClassNetWeight/BillTotalSmallClassNetWeight*totalsmallClassParcelNumber );
                
            }
            int totalBillNumbers=_UnionCLPSETTotalCloneForBillTable.AsEnumerable( ).GroupBy( a => a["BillNO"] ).Count( );
            if ( totalsmallClassParcelNumber-totalsmallClassParcelNumber1!=0 )
            {
                int m=( totalsmallClassParcelNumber-totalsmallClassParcelNumber1 )/totalBillNumbers;
                int n=( totalsmallClassParcelNumber-totalsmallClassParcelNumber1 )%totalBillNumbers;
                for ( int x=0 ; x<=m ; x++ )
                {
                    if ( x==0 )
                        _BillSmallClassNetWeightTable.Rows[x]["CartonQty"]=int.Parse( _BillSmallClassNetWeightTable.Rows[x]["CartonQty"].ToString( ) )+m+n;
                    _BillSmallClassNetWeightTable.Rows[x]["CartonQty"]=int.Parse( _BillSmallClassNetWeightTable.Rows[x]["CartonQty"].ToString( ) )+m;
                }
            }
            string errMsg=string.Empty;
            foreach ( DataRow row in _BillSmallClassNetWeightTable.Rows)
            {
                if(int.Parse(row["CartonQty"].ToString())<=0)
                {
                    errMsg+="分票为"+row["BillNO"].ToString( )+"箱数不能小于等于0\r\n";
                }
            }
            if ( !Directory.Exists( UnionCLPPath ) )
                Directory.CreateDirectory( UnionCLPPath );
            string filename="立方与箱数";
            //_UnionCLPTotalTable.AsEnumerable( ).GroupBy( m => m.Field<string>( "SHIPPING_NUMBER" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<string , DataRow> ig )
            //{
            //    filename+="-"+ig.Key;
            //    shippingnumbers.Add( ig.Key );
            //} );
            _UnionCLPSETTotalCloneForBillTable.AsEnumerable( ).GroupBy( m => m.Field<int>( "BillNO" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<int , DataRow> ig )
            {
                foreach ( DataRow row in _BillSmallClassNetWeightTable.Rows )
                {
                    if ( int.Parse( row["BillNO"].ToString( ) )==ig.Key )
                    {
                        row["NW"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "NET_WEIGHT" ) ) );
                        //ig.ToList<DataRow>( ).Sum(p => p.Field<decimal>( "NET_WEIGHT" ))
                        //_UnionCLPSETTotalCloneForBillTable.AsEnumerable().ToList<DataRow>( ).Sum( p => p.Field<decimal>( "NET_WEIGHT" ) )

                        row["NWRadio"]=( ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "NET_WEIGHT" ) ) )/_UnionCLPSETTotalCloneForBillTable.Select( "BigClass=0" ).AsEnumerable( ).ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "NET_WEIGHT" ) ) ) ).ToString( "N3" );
                    }
                }
                //row["BillNO"]=ig.Key;
                //row["Qty"]=ig.ToList<DataRow>( ).Sum( p => int.Parse( p.Field<string>( "QUANTITY" ) ) );
                //row["Amount"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "TOTAL_VALUE" ) ) );
                //row["CartonQty"]=0;
                //row["GW"]=ig.ToList<DataRow>( ).Sum( p => decimal.Parse( p.Field<string>( "GROSS_WEIGHT" ) ) );
                //row["NW"]=0;
                //row["Volume"]=0;
                //row["NWRadio"]=0;
            } );
            ExcelRender.RenderToExcel( _BillSmallClassNetWeightTable , UnionCLPPath+filename+".xls" );
            if ( errMsg!=string.Empty )
            {
                MessageBox.Show( errMsg+"，需要进行人工调整!" );
            }
            SetSubForm( "FrmCLPDataHandleForBill" );
            //toolStripStatusLabel1.Text="成功进行非商检数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理配箱数据";
            toolStripStatusLabel2.Text="操作结果：成功进行配箱数据处理！";
            //DataTable dd=_BillSmallClassNetWeightTable;
            //_UnionCLPSETTotalCloneForBillTable.AsEnumerable( ).Where<DataRow>( row => row["GroupID"].ToString( ) !="").GroupBy( a => a["GroupID"] ).
            //_UnionCLPSETTotalCloneForBillTable.AsEnumerable().g
            //foreach()
        }
        private DataTable _BillSmallClassNetWeightTable;
        private void CreateBillSmallClassNetWeightTableSchema( )
        {
            //DtAll.Columns.Add("CLPID", typeof(Int32));
            _BillSmallClassNetWeightTable.Columns.Add( "BillNO" , typeof( int ) );
            _BillSmallClassNetWeightTable.Columns.Add( "CartonQty" , typeof( int ) );
            _BillSmallClassNetWeightTable.Columns.Add( "Qty" , typeof( int ) );
            _BillSmallClassNetWeightTable.Columns.Add( "GW" , typeof( decimal ) );
            _BillSmallClassNetWeightTable.Columns.Add( "NW" , typeof( decimal ) );
            _BillSmallClassNetWeightTable.Columns.Add( "Volume" , typeof( decimal ) );
            _BillSmallClassNetWeightTable.Columns.Add( "Amount" , typeof( decimal ) );
            _BillSmallClassNetWeightTable.Columns.Add( "NWRadio" , typeof( decimal ) );
            //_BillSmallClassNetWeightTable.Columns.Add( "NetWeightRadio" , typeof( decimal ) );
            

            //_BillSmallClassNetWeightTable.Columns.Add( "CureNumber" , typeof( decimal ) );
            //_BillSmallClassNetWeightTable.Columns.Add( "NetWeightRadio" , typeof( decimal ) );
            //_UnionCLPTable.Columns.Add( "Flag" , typeof( String ) );
        }
        private DataTable _CubeNumberAdjustmentTable;
        private void btnCubeNubmerSetUp_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPForCureNumber;
            //dtSET.AsEnumerable( ).Distinct( new DataTableRowCompare( "MODEL_CODE" ) ).CopyToDataTable( );
            var query2 = from cna in _CubeNumberAdjustmentTable.AsEnumerable( )
                         join uclp in _UnionCLPTotalTable.AsEnumerable( ).Distinct( new DataTableRowCompare( "SHIPPING_NUMBER" ) )
                        //join cna in _CubeNumberAdjustmentTable.AsEnumerable( ) 
                         on cna.Field<string>( "ShippingNumber" ) equals uclp.Field<string>( "SHIPPING_NUMBER" ).Contains( '-' )?uclp.Field<string>( "SHIPPING_NUMBER" ).Split( '-' )[0]:uclp.Field<string>( "SHIPPING_NUMBER" )  
                         where uclp.Field<string>( "SHIPPING_NUMBER" )!=""
                         group cna by cna.Field<string>( "ShippingNumber" ) into m
                         select new
                         {
                             SHIPPINGNUMBER = m.Key ,
                             TotalCubeNumber=m.Sum( m1 => m1.Field<decimal>( "TotalCubeNumber" ) ),
                             TotalParcelNumber=m.Sum( m1 => m1.Field<int>( "TotalParcelNumber"  ) )
                         };
            decimal totalCubeNumber=0;
            decimal BillTotalSmallClassNetWeight=0;
            foreach ( var obj in query2 )
            {
                string ss=obj.SHIPPINGNUMBER;
                totalCubeNumber+=obj.TotalCubeNumber;
            }
            if ( _BillSmallClassNetWeightTable !=null )
            {
                for ( int i=0 ; i< _BillSmallClassNetWeightTable.Rows.Count ; i++ )
                {

                    if ( i!=_BillSmallClassNetWeightTable.Rows.Count-1 )
                    {
                        _BillSmallClassNetWeightTable.Rows[i]["Volume"]=decimal.Round( decimal.Parse( _BillSmallClassNetWeightTable.Rows[i]["CartonQty"].ToString( ) )*totalCubeNumber/_TotalParcelNumber , 2 );
                        //_BillSmallClassNetWeightTable.Rows[i]["CureNumber"]=int.Parse( _BillSmallClassNetWeightTable.Rows[i]["ParcelNumber"].ToString( ) )/totalCubeNumber*_TotalParcelNumber;
                    }
                    else
                    {
                        _BillSmallClassNetWeightTable.Rows[i]["Volume"]=totalCubeNumber-decimal.Parse( _BillSmallClassNetWeightTable.Compute( "Sum(Volume)" , "true" ).ToString( ) );
                    }
                }
            }
            string errMsg=string.Empty;
            foreach ( DataRow row in _BillSmallClassNetWeightTable.Rows )
            {
                if ( decimal.Parse( row["Volume"].ToString( ) )<=0 )
                {
                    errMsg+="分票为"+row["BillNO"].ToString( )+"立方数不能小于等于0\r\n";
                }
            }
            if ( !Directory.Exists( UnionCLPPath ) )
                Directory.CreateDirectory( UnionCLPPath );
            string filename="立方与箱数";
            //_UnionCLPTotalTable.AsEnumerable( ).GroupBy( m => m.Field<string>( "SHIPPING_NUMBER" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<string , DataRow> ig )
            //{
            //    filename+="-"+ig.Key;
            //    shippingnumbers.Add( ig.Key );
            //} );
            ExcelRender.RenderToExcel( _BillSmallClassNetWeightTable , UnionCLPPath+filename+".xls" );
            if ( errMsg!=string.Empty )
            {
                MessageBox.Show( errMsg+"，需要进行人工调整!" );
            }
            SetSubForm( "FrmCLPDataHandleForBill" );
            //toolStripStatusLabel1.Text="成功进行非商检数据处理！";
            toolStripStatusLabel1.Text="操作阶段：处理立方数数据";
            toolStripStatusLabel2.Text="操作结果：成功进行立方数数据处理！";

//        from p in context.ParentTable
//join c in context.ChildTable on p.ParentId equals c.ChildParentId into j1
//from j2 in j1.DefaultIfEmpty()
//group j2 by p.ParentId into grouped
//select new { ParentId = grouped.Key, Count = grouped.Count(t=>t.ChildId != null) }
        }
        private void btnOutputBill_Click( object sender , EventArgs e )
        {
            FrmBillFieldManage form = new FrmBillFieldManage( );
            //form.ShowDialog(); 
            form.ShowDialog( this );
            if ( !Directory.Exists( UnionCLPPath ) )
                Directory.CreateDirectory( UnionCLPPath );
            ArrayList shippingnumbers=new ArrayList( );
            string filename="invpk";
            _UnionCLPTotalTable.AsEnumerable( ).GroupBy( m => m.Field<string>( "SHIPPING_NUMBER" ) , m => m ).ToList( ).ForEach( delegate( IGrouping<string , DataRow> ig )
            {
                filename+="-"+ig.Key;
                shippingnumbers.Add( ig.Key );
            } );
            ExcelRenderExtend.ExportInvoiceListToExcel( _UnionCLPSETTotalCloneForBillTable , _BillSmallClassNetWeightTable , UnionCLPPath , filename,
            shippingnumbers,  BuyerCN , BuyerEN , AddressCN ,  AddressEN , TEL , FAX , ShipmentDate ,  ShipmentType, 
             LoadingPort , DeliveryPort ,  PaymentTerm , DeliveryCountryCN, DeliveryCountryEN ,  DomesticSources ,
             PackingSpecifications ,  Incoterm ,  TransportMode ,  ShippingMark ,  Destination,Currency);
            MessageBox.Show( this , "发票清单成功导出！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
        }

        private void btnBigClassTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"大类货物模板.xls" , FileName , false );
                MessageBox.Show( this , "大类货物模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ExcelBigClassDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=4 )
            {
                errMsg="大类资料表列数为4列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="中文品名" )
            {
                errMsg="大类资料表第一列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="是否大类" )
            {
                errMsg="大类资料表第二列为是否大类，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="需多配箱数" )
            {
                errMsg="大类资料表第三列为需多配箱数，请核对后重新导入！";
                return false;
            }

            if ( excelDT.Columns[3].Caption!="备注说明" )
            {
                errMsg="大类资料表第四列为备注说明，请核对后重新导入！";
                return false;
            }
            return true;
        }
        private void btnBigClassDataInput_Click( object sender , EventArgs e )
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelBigClassDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //dt.Columns.Add()
                new ParcelSetUpBLL( ).BulkParcelSetUpInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}
                _ParcelSetUpTable.Clear( );
                _ParcelSetUpTable=new ParcelSetUpBLL( ).GetAllList( ).Tables[0];
                _ParcelSetUpTable.AcceptChanges( );
                //_RepetitiveBasicDataTable.Clear( );
                //_RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                //_RepetitiveBasicDataTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "大类资料成功导入！" );
            }
        }
        private void btnBigClassDataOutput_Click( object sender , EventArgs e )
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
                ExcelRender.RenderToExcel( new ParcelSetUpBLL( ).GetAllList( ).Tables[0] , localFilePath );
                MessageBox.Show( "大类资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        private void btnNetWeightAdjustTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"净重调整模板.xls" , FileName , false );
                MessageBox.Show( this , "净重调整模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ExcelNetWeightAdjustDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=2 )
            {
                errMsg="净重资料表列数为2列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="中文品名" )
            {
                errMsg="净重资料表第一列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="调整系数" )
            {
                errMsg="净重资料表第二列为调整系数，请核对后重新导入！";
                return false;
            }

            return true;
        }
        private void btnNetWeightAdjustDataInput_Click( object sender , EventArgs e )
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelNetWeightAdjustDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //dt.Columns.Add()
                new NetWeightAdjustmentBLL( ).BulkNetWeightAdjustInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}
                _NetWeightAdjustmentTable.Clear( );
                _NetWeightAdjustmentTable=new NetWeightAdjustmentBLL( ).GetAllList( ).Tables[0];
                _NetWeightAdjustmentTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "净重调整资料成功导入！" );
            }
        }
        private void btnNetWeightAdjustDataOutput_Click( object sender , EventArgs e )
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
                ExcelRender.RenderToExcel( new NetWeightAdjustmentBLL( ).GetAllList( ).Tables[0] , localFilePath );
                MessageBox.Show( "净重调整资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        private void btnCubeNumberAdjustTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"总立方数调整模板.xls" , FileName , false );
                MessageBox.Show( this , "总立方数调整模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ExcelCubeNumberAdjustDataValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=4 )
            {
                errMsg="总立方数资料表列数为4列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="SHIPPING_NUMBER" )
            {
                errMsg="总立方数资料表第一列为SHIPPING NUMBER，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="确认类型" )
            {
                errMsg="总立方数资料表第二列为确认类型，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="总箱数" )
            {
                errMsg="总立方数资料表第三列为总箱数，请核对后重新导入！";
                return false;
            }

            if ( excelDT.Columns[3].Caption!="总立方数" )
            {
                errMsg="总立方数资料表第四列为总立方数，请核对后重新导入！";
                return false;
            }

            return true;
        }
        private void btnCubeNumberAdjustDataInput_Click( object sender , EventArgs e )
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelCubeNumberAdjustDataValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //dt.Columns.Add()
                new CubeNumberAdjustmentBLL( ).BulkCubeNumberAdjustmentInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}

                _CubeNumberAdjustmentTable.Clear( );
                _CubeNumberAdjustmentTable=new CubeNumberAdjustmentBLL( ).GetAllList( ).Tables[0];
                _CubeNumberAdjustmentTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "总立方数资料成功导入！" );
            }
        }
        private void btnCubeNumberAdjustDataOutput_Click( object sender , EventArgs e )
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
                ExcelRender.RenderToExcel( new CubeNumberAdjustmentBLL( ).GetAllList( ).Tables[0] , localFilePath );
                MessageBox.Show( "总立方资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }
        //循环去除datatable中的空行
        protected void RemoveEmpty( DataTable dt )
        {
            List<DataRow> removelist = new List<DataRow>( );
            for ( int i = 0 ; i < dt.Rows.Count ; i++ )
            {
                bool rowdataisnull = true;
                for ( int j = 0 ; j < dt.Columns.Count ; j++ )
                {

                    if ( !string.IsNullOrEmpty( dt.Rows[i][j].ToString( ).Trim( ) ) )
                    {

                        rowdataisnull = false;
                    }

                }
                if ( rowdataisnull )
                {
                    removelist.Add( dt.Rows[i] );
                }

            }
            for ( int i = 0 ; i < removelist.Count ; i++ )
            {
                dt.Rows.Remove( removelist[i] );
            }
        }
        #endregion
        #region 导出发票清单
        public string BuyerCN="";
        public string BuyerEN="";
        public string AddressCN="";
        public string AddressEN="";
        public string TEL="";
        public string FAX="";
        public string ShipmentDate="";
        public string ShipmentType="";
        public string LoadingPort="";
        public string DeliveryPort="";
        public string PaymentTerm="";
        public string DeliveryCountryCN="";
        public string DeliveryCountryEN="";
        public string DomesticSources="";//境内货源地
        public string PackingSpecifications;//包装规格
        public string Incoterm="";//价格条款
        public string TransportMode="";//运输方式
        public string ShippingMark="";
        public string Destination="";
        public string Currency="";
        #endregion 
        private void btnSETBreakUpTemplateDownload_Click( object sender , EventArgs e )
        {
            SaveFileDialog kk = new SaveFileDialog( );
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if ( kk.ShowDialog( ) == DialogResult.OK )
            {
                //string FileName = kk.FileName + ".xls";
                string FileName = kk.FileName;
                if ( File.Exists( FileName ) )
                    File.Delete( FileName );
                File.Copy( System.AppDomain.CurrentDomain.BaseDirectory+"SET拆分比例模板.xls" , FileName , false );
                MessageBox.Show( this , "SET拆分比例模板文件成功下载！" , "提示" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }
        private bool ExcelSetBreakUpValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=6 )
            {
                errMsg="SET拆分比例资料表列数为6列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="MODEL_CODE" )
            {
                errMsg="SET拆分比例资料表第一列为MODEL CODE，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="中文品名" )
            {
                errMsg="SET拆分比例资料表第二列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="金额" )
            {
                errMsg="SET拆分比例资料表第三列为金额，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[3].Caption!="净重" )
            {
                errMsg="SET拆分比例资料表第四列为净重，请核对后重新导入！";
                return false;
            }

            if ( excelDT.Columns[4].Caption!="毛重" )
            {
                errMsg="SET拆分比例资料表第五列为毛重，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[5].Caption!="分组号" )
            {
                errMsg="SET拆分比例资料表第六列为分组号，请核对后重新导入！";
                return false;
            }
            return true;
        }

        private void btnSETBreakUpDataInput_Click( object sender , EventArgs e )
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelSetBreakUpValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //dt.Columns.Add()
                new SetBreakUpBLL( ).BulkSETBreakUpInsert( dt , 1000 );
                //if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                //{
                //    MessageBox.Show( errMsg );
                //    return;
                //}

                _SetBreakUpTable.Clear( );
                _SetBreakUpTable=new SetBreakUpBLL( ).GetAllList( ).Tables[0];
                _SetBreakUpTable.AcceptChanges( );
                //_RepetitiveBasicDataTable.Clear( );
                //_RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                //_RepetitiveBasicDataTable.AcceptChanges( );
                //SetSubForm( "FrmBasicData" );
                MessageBox.Show( "SET拆分比例资料成功导入！" );
            }
        }

        private void btnSETBreakUpDataOutput_Click( object sender , EventArgs e )
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
                ExcelRender.RenderToExcel( new SetBreakUpBLL( ).GetSetBreakUpList("").Tables[0] , localFilePath );
                MessageBox.Show( " SET拆分比例资料成功导出！" );
                //toolStripStatusLabel1.Text="基础资料成功导出！";
            }
        }

        private void btnReImport_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPForSetUpBill;
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelBillSetUpValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                //DataTable temp=_UnionCLPSETTotalCloneForBillTable;
                _UnionCLPSETTotalCloneForBillTable.Clear( );
                foreach ( DataRow row in dt.Rows )
                {
                    _UnionCLPSETTotalCloneForBillTable.Rows.Add( row.ItemArray );
                }
                _UnionCLPSETTotalCloneForBillTable.AcceptChanges( );
                //_UnionCLPSETTotalCloneForBillTable=dt;
                SetSubForm( "FrmCLPDataHandleForBill" );
                toolStripStatusLabel1.Text="操作阶段：分票数据导入";
                toolStripStatusLabel2.Text="操作结果：分票数据净毛重调整后重新导入！";
                ////dt.Columns.Add()
                //new SetBreakUpBLL( ).BulkSETBreakUpInsert( dt , 1000 );
                ////if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                ////{
                ////    MessageBox.Show( errMsg );
                ////    return;
                ////}

                //_SetBreakUpTable.Clear( );
                //_SetBreakUpTable=new SetBreakUpBLL( ).GetAllList( ).Tables[0];
                //_SetBreakUpTable.AcceptChanges( );
                ////_RepetitiveBasicDataTable.Clear( );
                ////_RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                ////_RepetitiveBasicDataTable.AcceptChanges( );
                ////SetSubForm( "FrmBasicData" );
                MessageBox.Show( "分票资料成功导入！" );
            }
        }

        private bool ExcelBillSetUpValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=14 )
            {
                errMsg="分票资料表列数为14列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="中文品名" )
            {
                errMsg="分票资料表第一列为中文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="HS_CODE_(IN_CAT)" )
            {
                errMsg="分票资料表第二列为HS_CODE_(IN_CAT)，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="申报要素" )
            {
                errMsg="分票资料表第三列为申报要素，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[3].Caption!="QUANTITY" )
            {
                errMsg="分票资料表第四列为QUANTITY，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[4].Caption!="TOTAL_VALUE" )
            {
                errMsg="分票资料表第五列为TOTAL_VALUE，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[5].Caption!="英文品名" )
            {
                errMsg="分票资料表第六列为英文品名，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[6].Caption!="法定计量单位" )
            {
                errMsg="分票资料表第七列为法定计量单位，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[7].Caption!="NET_WEIGHT" )
            {
                errMsg="分票资料表第八列为NET_WEIGHT，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[8].Caption!="GROSS_WEIGHT" )
            {
                errMsg="分票资料表第九列为GROSS_WEIGHT，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[9].Caption!="监管条件" )
            {
                errMsg="分票资料表第十列为监管条件，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[10].Caption!="GroupID" )
            {
                errMsg="分票资料表第十一列为GroupID，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[11].Caption!="数据类型" )
            {
                errMsg="分票资料表第十二列为数据类型，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[12].Caption!="BigClass" )
            {
                errMsg="分票资料表第十三列为BigClass，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[13].Caption!="BillNO" )
            {
                errMsg="分票资料表第十四列为BillNO，请核对后重新导入！";
                return false;
            }
            return true;
        }

        private void btnCureNumberReImport_Click( object sender , EventArgs e )
        {
            this.operateType=OperateType.UnionCLPForCureNumber;
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
                    RemoveEmpty( dt );
                }
                string errMsg="";
                if ( !ExcelCureNumberReImportValidate( dt , ref errMsg ) )
                {
                    MessageBox.Show( errMsg );
                    return;
                }
                _BillSmallClassNetWeightTable.Clear( );
                foreach ( DataRow row in dt.Rows )
                {
                    _BillSmallClassNetWeightTable.Rows.Add( row.ItemArray );
                }
                _BillSmallClassNetWeightTable.AcceptChanges( );
                //_BillSmallClassNetWeightTable=dt;
                SetSubForm( "FrmCLPDataHandleForBill" );
                toolStripStatusLabel1.Text="操作阶段：立方&箱数数据导入";
                toolStripStatusLabel2.Text="操作结果：分票数据立方&箱数调整后重新导入！";
                ////dt.Columns.Add()
                //new SetBreakUpBLL( ).BulkSETBreakUpInsert( dt , 1000 );
                ////if ( !ExcuteBasicDataInput( dt , fd.FileName , ref errMsg ) )
                ////{
                ////    MessageBox.Show( errMsg );
                ////    return;
                ////}

                //_SetBreakUpTable.Clear( );
                //_SetBreakUpTable=new SetBreakUpBLL( ).GetAllList( ).Tables[0];
                //_SetBreakUpTable.AcceptChanges( );
                ////_RepetitiveBasicDataTable.Clear( );
                ////_RepetitiveBasicDataTable=basicDataBLL.GetRepetitiveModeCodeList( ).Tables[0];
                ////_RepetitiveBasicDataTable.AcceptChanges( );
                ////SetSubForm( "FrmBasicData" );
                MessageBox.Show( "立方&箱数资料成功导入！" );
            }
        }

        private bool ExcelCureNumberReImportValidate( DataTable excelDT , ref string errMsg )
        {
            if ( excelDT.Columns.Count!=8 )
            {
                errMsg="立方箱数资料表列数为8列，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[0].Caption!="BillNO" )
            {
                errMsg="立方箱数资料表第一列为BillNO，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[1].Caption!="CartonQty" )
            {
                errMsg="立方箱数资料表第二列为CartonQty，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[2].Caption!="Qty" )
            {
                errMsg="立方箱数资料表第三列为Qty，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[3].Caption!="GW" )
            {
                errMsg="立方箱数资料表第四列为GW，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[4].Caption!="NW" )
            {
                errMsg="立方箱数资料表第五列为NW，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[5].Caption!="Volume" )
            {
                errMsg="立方箱数资料表第六列为Volume，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[6].Caption!="Amount" )
            {
                errMsg="立方箱数资料表第七列为Amount，请核对后重新导入！";
                return false;
            }
            if ( excelDT.Columns[7].Caption!="NWRadio" )
            {
                errMsg="立方箱数资料表第八列为NWRadio，请核对后重新导入！";
                return false;
            }
            return true;
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
