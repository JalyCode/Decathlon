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
    public partial class FrmBasicData : Form
    {
        public FrmBasicData( )
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle( );
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.NotSet;
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "Microsoft Sans Serif" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 0 ) ) );
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
            this.dataGridView1.CurrentCell = null;
        }
        public FrmBasicData( string strCaption , OperateType operateType , DataTable dt )
        {
            InitializeComponent( );
            this.lblCaption.ForeColor= Color.DarkSlateBlue;
            this.lblCaption.Text=strCaption;
            InitDataGridView( );
            DataBandingDgv( dt );
            this.lblRemark.Text="记录总数："+dt.Rows.Count+"条";
        }
    }
}
