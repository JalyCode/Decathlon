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
    public partial class FrmTotalCLPHandle : Form
    {
        public FrmTotalCLPHandle( )
        {
            InitializeComponent( );
        }
        public FrmTotalCLPHandle( string strCaption , OperateType operateType , DataSet ds )
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
        }
        protected void DataBandingDgv( DataSet _ds )
        {
            if ( _ds.Tables.Count<=0 )
                return;
            //tabPage1.Text=_ds.Tables[0].TableName;
            //InitDataGridView( dataGridView1 );
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = _ds.Tables[0].DefaultView;
            //tabPage2.Text=_ds.Tables[1].TableName;
            //InitDataGridView( dataGridView2 );
            //dataGridView2.DataSource = null;
            //dataGridView2.DataSource = _ds.Tables[1].DefaultView;

            foreach ( DataTable dt in _ds.Tables )
            {
                TabPage tab=new TabPage( );
                tab.Text=dt.TableName;
                DataGridView dgv=new DataGridView( );
                InitDataGridView( dgv );
                dgv.Dock=DockStyle.Fill;
                dgv.AutoGenerateColumns = true;  //规定不自动生成列
                dgv.DataSource = null;
                dgv.DataSource = dt.DefaultView;
                for ( int i = 0 ; i < dgv.Columns.Count ; i++ )
                {
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }

                dgv.CurrentCell = null;
                tab.Controls.Add( dgv );
                tabControl1.TabPages.Add( tab );

            }
        }
        protected void InitDataGridView(DataGridView dgv )
        {
            if ( dgv.Columns.Count > 0 )
            {
                dgv.Columns.Clear( );
            }
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            
            DataGridViewCellStyle dataGridViewCellStyle11=new DataGridViewCellStyle( );
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 237 ) ) ) ) , ( (int)( ( (byte)( 245 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            DataGridViewCellStyle dataGridViewCellStyle12=new DataGridViewCellStyle( );
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 181 ) ) ) ) , ( (int)( ( (byte)( 214 ) ) ) ) , ( (int)( ( (byte)( 230 ) ) ) ) );
            dataGridViewCellStyle12.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGridViewCellStyle dataGridViewCellStyle13=new DataGridViewCellStyle( );
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle13;
            dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            dgv.GridColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dgv.Location = new System.Drawing.Point( 0 , 0 );
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            DataGridViewCellStyle dataGridViewCellStyle14=new DataGridViewCellStyle( );
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 237 ) ) ) ) , ( (int)( ( (byte)( 245 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dgv.RowHeadersVisible = false;
            dgv.ForeColor = System.Drawing.Color.Black;
            DataGridViewCellStyle dataGridViewCellStyle15=new DataGridViewCellStyle( );
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle15;
            dgv.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgv.RowTemplate.Height = 20;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new System.Drawing.Size( 819 , 423 );
            dgv.TabIndex = 6;
        }
    }
}
