namespace DecathlonDataProcessSystem.App
{
    partial class FrmCLPDataHandleForBill
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel( );
            this.lblCaption = new System.Windows.Forms.Label( );
            this.lblRemark = new System.Windows.Forms.Label( );
            this.dataGridView1 = new System.Windows.Forms.DataGridView( );
            this.tableLayoutPanel1.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel1.Controls.Add( this.lblCaption , 0 , 0 );
            this.tableLayoutPanel1.Controls.Add( this.lblRemark , 0 , 2 );
            this.tableLayoutPanel1.Controls.Add( this.dataGridView1 , 0 , 1 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0 , 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 684 , 413 );
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCaption.Font = new System.Drawing.Font( "微软雅黑" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            this.lblCaption.ForeColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 42 ) ) ) ) , ( (int)( ( (byte)( 138 ) ) ) ) , ( (int)( ( (byte)( 212 ) ) ) ) );
            this.lblCaption.Location = new System.Drawing.Point( 3 , 3 );
            this.lblCaption.Margin = new System.Windows.Forms.Padding( 3 );
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size( 678 , 14 );
            this.lblCaption.TabIndex = 6;
            // 
            // lblRemark
            // 
            this.lblRemark.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRemark.Location = new System.Drawing.Point( 3 , 396 );
            this.lblRemark.Margin = new System.Windows.Forms.Padding( 3 );
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size( 678 , 14 );
            this.lblRemark.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point( 3 , 23 );
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size( 678 , 367 );
            this.dataGridView1.TabIndex = 9;
            // 
            // FrmCLPDataHandleForBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size( 684 , 413 );
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Name = "FrmCLPDataHandleForBill";
            this.Shown += new System.EventHandler( this.FrmCLPDataHandleForBill_Shown );
            this.tableLayoutPanel1.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.DataGridView dataGridView1;


    }
}