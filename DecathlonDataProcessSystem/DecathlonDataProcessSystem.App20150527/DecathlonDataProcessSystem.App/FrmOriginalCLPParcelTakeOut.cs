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
    public partial class FrmOriginalCLPParcelTakeOut : Form
    {
        public FrmOriginalCLPParcelTakeOut( )
        {
            InitializeComponent( );
        }
        public FrmOriginalCLPParcelTakeOut( string strCaption , OperateType operateType , DataSet ds )
        {
            InitializeComponent( );
            this.lblCaption.ForeColor= Color.DarkSlateBlue;
            this.lblCaption.Text=strCaption;
            DataBandingDgv( ds );
        }
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront( );
            dataGridView1.ClearSelection( );
            dataGridView2.ClearSelection( );
        }
        protected void DataBandingDgv( DataSet _ds )
        {
            if ( _ds.Tables.Count<=0 )
                return;
            tabPage1.Text=_ds.Tables[0].TableName;
            InitDataGridView( dataGridView1 );
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _ds.Tables[0].DefaultView;
            this.dataGridView1.CurrentCell = null;
            tabPage2.Text=_ds.Tables[1].TableName;
            InitDataGridView( dataGridView2 );
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = _ds.Tables[1].DefaultView;
            this.dataGridView2.CurrentCell = null;            
        }
        protected void InitDataGridView(DataGridView dgv )
        {
            //if ( dgv.Columns.Count > 0 )
            //{
            //    dgv.Columns.Clear( );
            //}
            dgv.AutoGenerateColumns=false;
            //自动调整列宽度
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            DataGridViewCellStyle dataGridViewCellStyle1=new DataGridViewCellStyle( );
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb( 255 , 255 , 255 );
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb( 193 , 235 , 255 );
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            DataGridViewCellStyle dataGridViewCellStyle2=new DataGridViewCellStyle( );
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb( 193 , 235 , 255 );
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle2;
            dgv.GridColor = System.Drawing.Color.FromArgb( 193 , 235 , 255 );
            dgv.MultiSelect = false;
            //dgv.Name = "dataGridView1";
            dgv.ReadOnly = true;
            DataGridViewCellStyle dataGridViewCellStyle3=new DataGridViewCellStyle( );
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 237 ) ) ) ) , ( (int)( ( (byte)( 245 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgv.RowHeadersVisible = false;

            DataGridViewCellStyle dataGridViewCellStyle4=new DataGridViewCellStyle( );
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgv.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgv.RowTemplate.Height = 23;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.CurrentCell = null;
            //dgv.Paint+=new PaintEventHandler( dataGridView_Paint );
        }

        private void FrmOriginalCLPParcelTakeOut_Shown( object sender , EventArgs e )
        {
            this.dataGridView1.CurrentCell = null;
            this.dataGridView2.CurrentCell = null;
        }
    }
}
