namespace DecathlonDataProcessSystem.App
{
    partial class FrmTotalCLPHandle
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
            this.panel1 = new System.Windows.Forms.Panel( );
            this.tabControl1 = new System.Windows.Forms.TabControl( );
            this.tableLayoutPanel1.SuspendLayout( );
            this.panel1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel1.Controls.Add( this.lblCaption , 0 , 0 );
            this.tableLayoutPanel1.Controls.Add( this.lblRemark , 0 , 2 );
            this.tableLayoutPanel1.Controls.Add( this.panel1 , 0 , 1 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0 , 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 20F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 765 , 474 );
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblCaption
            // 
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCaption.Font = new System.Drawing.Font( "微软雅黑" , 9F , System.Drawing.FontStyle.Regular , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            this.lblCaption.ForeColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 42 ) ) ) ) , ( (int)( ( (byte)( 138 ) ) ) ) , ( (int)( ( (byte)( 212 ) ) ) ) );
            this.lblCaption.Location = new System.Drawing.Point( 3 , 3 );
            this.lblCaption.Margin = new System.Windows.Forms.Padding( 3 );
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size( 759 , 14 );
            this.lblCaption.TabIndex = 6;
            // 
            // lblRemark
            // 
            this.lblRemark.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRemark.Location = new System.Drawing.Point( 3 , 457 );
            this.lblRemark.Margin = new System.Windows.Forms.Padding( 3 );
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size( 759 , 14 );
            this.lblRemark.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.tabControl1 );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point( 3 , 23 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 759 , 428 );
            this.panel1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point( 0 , 0 );
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 759 , 428 );
            this.tabControl1.TabIndex = 5;
            // 
            // FrmTotalCLPHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size( 765 , 474 );
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Name = "FrmTotalCLPHandle";
            this.Text = "FrmTotalCLPHandle";
            this.tableLayoutPanel1.ResumeLayout( false );
            this.panel1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}