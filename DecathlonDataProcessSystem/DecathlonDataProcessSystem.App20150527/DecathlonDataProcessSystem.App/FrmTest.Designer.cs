namespace DecathlonDataProcessSystem.App
{
    partial class FrmTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle( );
            this.panelMain = new System.Windows.Forms.Panel( );
            this.dataGridView1 = new System.Windows.Forms.DataGridView( );
            this.panelMain.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 194 ) ) ) ) , ( (int)( ( (byte)( 224 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            this.panelMain.Controls.Add( this.dataGridView1 );
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point( 0 , 0 );
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size( 674 , 481 );
            this.panelMain.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font( "宋体" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 193 ) ) ) ) , ( (int)( ( (byte)( 235 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            this.dataGridView1.Location = new System.Drawing.Point( 0 , 0 );
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightBlue;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size( 674 , 481 );
            this.dataGridView1.TabIndex = 6;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 674 , 481 );
            this.Controls.Add( this.panelMain );
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelMain.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}