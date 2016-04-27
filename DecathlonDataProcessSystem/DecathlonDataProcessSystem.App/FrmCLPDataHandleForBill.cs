using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DecathlonDataProcessSystem.App
{
    public partial class FrmCLPDataHandleForBill : Form
    {
        //private FrmMain frmParent;
        private OperateType opType;
        public FrmCLPDataHandleForBill( )
        {
            InitializeComponent( );
        }
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront( );
        }
        protected void DataBandingDgv( DataTable _dataTableOperate )
        {
            this.dataGridView1.AutoGenerateColumns = true;  //规定不自动生成列
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = _dataTableOperate.DefaultView;
            this.dataGridView1.CurrentCell = null;
            for ( int i = 0 ; i < this.dataGridView1.Columns.Count ; i++ )
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //if ( _dataTableOperate.Columns.Count > 0 )
            //{
            //    for ( int i = 0 ; i < _dataTableOperate.Columns.Count ; i++ )
            //    {
            //        this.dataGridView1.Columns[i].DataPropertyName =
            //            _dataTableOperate.Columns[i].ColumnName;
            //    }
            //    this.dataGridView1.DataSource = null;
            //    this.dataGridView1.DataSource = _dataTableOperate.DefaultView;
            //}
        }
        protected void InitDataGridView( )
        {
            if ( this.dataGridView1.Columns.Count > 0 )
            {
                this.dataGridView1.Columns.Clear( );
            }
            //自动调整列宽度
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            DataGridViewCellStyle dataGridViewCellStyle1=new DataGridViewCellStyle( );
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(255,255,255);
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb( 193,235,255);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DataGridViewCellStyle dataGridViewCellStyle2=new DataGridViewCellStyle( );
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb( 193 , 235 , 255 );
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb( 193 , 235 , 255 );
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            DataGridViewCellStyle dataGridViewCellStyle3=new DataGridViewCellStyle( );
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 237 ) ) ) ) , ( (int)( ( (byte)( 245 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;

            DataGridViewCellStyle dataGridViewCellStyle4=new DataGridViewCellStyle( );
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dataGridView1.CurrentCell = null;
            this.dataGridView1.Paint+=new PaintEventHandler(dataGridView1_Paint);
        }
        protected void DeleteDataGridViewColumn( )
        {
            if ( this.dataGridView1.Columns.Count <= 0 )
            {
                this.lblRemark.Text="无相关数据列，请核查后重新操作！";
                return;
            }
            this.dataGridView1.Columns.Remove( "ORIGINAL_PO_NUMBER");
            this.dataGridView1.Columns.Remove("Local_Composition" );
            this.dataGridView1.Columns.Remove("English_Description" );
            this.dataGridView1.Columns.Remove("Local_Description" );
            this.dataGridView1.Columns.Remove("HS_CODE" );
            this.dataGridView1.Columns.Remove("UNIT" );
        }
        public FrmCLPDataHandleForBill( string strCaption , OperateType operateType , DataTable dt )
        {
            InitializeComponent( );
            this.lblCaption.ForeColor= Color.DarkSlateBlue;
            this.lblCaption.Text=strCaption;
            this.opType=operateType;
            InitDataGridView( );
            DataBandingDgv( dt );
            this.lblRemark.Text="记录总数："+dt.Rows.Count+"条";
            //if ( operateType==OperateType.CLPDataColumnDelete )
            //{
            //    DeleteDataGridViewColumn( );
            //}
            //else
            //{
            //    InitDataGridView( );
            //    DataBandingDgv( dt );
            //}            
            //this.lblRemark.Text=bb;

        }

        /// <summary>
        ///商检货物背景色 
        /// </summary>
        private Color cCondition = Color.Goldenrod;
        private void dataGridView1_Paint( object sender , PaintEventArgs e )
        {
            if ( this.dataGridView1.Rows.Count > 0 )
            {
                if ( opType==OperateType.OriginalCLPDorbleOrSet )
                {
                    foreach ( DataGridViewRow row in this.dataGridView1.Rows )
                    {
                        string strCondition=String.Empty;
                        strCondition = row.Cells["Double"].Value.ToString( );
                        Color backColor = System.Drawing.Color.LightYellow;
                        //backColor = this.cCondition;
                        if ( strCondition.ToUpper().Contains( "SET" ) )  //等待开始
                        {
                            backColor = this.cCondition;
                        }
                        row.DefaultCellStyle.BackColor = backColor;
                    }
                }
                else if ( opType==OperateType.OriginalCLPDataInput )
                {
                    foreach ( DataGridViewRow row in this.dataGridView1.Rows )
                    {
                        string strCondition=String.Empty;
                        strCondition = row.Cells["监管条件"].Value.ToString( );
                        Color backColor = System.Drawing.Color.Blue;
                        //backColor = this.cCondition;
                        if ( strCondition.ToUpper( ).Contains( "B" ) )  //等待开始
                        {
                            backColor = this.cCondition;
                        }
                        row.DefaultCellStyle.BackColor = backColor;
                    }
                }


            }
        }

        private void FrmCLPDataHandleForBill_Shown( object sender , EventArgs e )
        {
            this.dataGridView1.CurrentCell = null;
        }
    }
}
