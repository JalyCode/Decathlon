namespace DecathlonDataProcessSystem.App
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( FrmLogin ) );
            this.btnLogin = new System.Windows.Forms.Button( );
            this.label1 = new System.Windows.Forms.Label( );
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel( );
            this.label2 = new System.Windows.Forms.Label( );
            this.lblVersion = new System.Windows.Forms.Label( );
            this.btnCancel = new System.Windows.Forms.Button( );
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel( );
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel( );
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel( );
            this.lblUserName = new System.Windows.Forms.Label( );
            this.lblPassword = new System.Windows.Forms.Label( );
            this.txtUserNo = new System.Windows.Forms.TextBox( );
            this.txtPassword = new System.Windows.Forms.TextBox( );
            this.tableLayoutPanel4.SuspendLayout( );
            this.tableLayoutPanel1.SuspendLayout( );
            this.tableLayoutPanel2.SuspendLayout( );
            this.tableLayoutPanel3.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLogin.Location = new System.Drawing.Point( 3 , 88 );
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size( 69 , 23 );
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登 录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler( this.btnLogin_Click );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font( "微软雅黑" , 14.25F , System.Drawing.FontStyle.Bold , System.Drawing.GraphicsUnit.Point , ( (byte)( 134 ) ) );
            this.label1.Location = new System.Drawing.Point( 3 , 3 );
            this.label1.Margin = new System.Windows.Forms.Padding( 3 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 575 , 83 );
            this.label1.TabIndex = 1;
            this.label1.Text = "迪卡侬报关资料处理系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 48.70912F ) );
            this.tableLayoutPanel4.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 51.29088F ) );
            this.tableLayoutPanel4.Controls.Add( this.label2 , 0 , 1 );
            this.tableLayoutPanel4.Controls.Add( this.lblVersion , 1 , 1 );
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point( 0 , 208 );
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding( 0 );
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel4.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 30F ) );
            this.tableLayoutPanel4.Size = new System.Drawing.Size( 581 , 90 );
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point( 3 , 63 );
            this.label2.Margin = new System.Windows.Forms.Padding( 3 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 276 , 24 );
            this.label2.TabIndex = 0;
            this.label2.Text = "版权所有：飞力达股份有限公司";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point( 285 , 63 );
            this.lblVersion.Margin = new System.Windows.Forms.Padding( 3 );
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size( 293 , 24 );
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "当前版本：DecathlonDataProcessSystemV1.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.Location = new System.Drawing.Point( 83 , 88 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75 , 23 );
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel1.Controls.Add( this.tableLayoutPanel2 , 0 , 1 );
            this.tableLayoutPanel1.Controls.Add( this.label1 , 0 , 0 );
            this.tableLayoutPanel1.Controls.Add( this.tableLayoutPanel4 , 0 , 2 );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 0 , 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 30F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 40F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 30F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 581 , 298 );
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 50F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Absolute , 200F ) );
            this.tableLayoutPanel2.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 50F ) );
            this.tableLayoutPanel2.Controls.Add( this.tableLayoutPanel3 , 1 , 0 );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point( 0 , 89 );
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding( 0 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent , 100F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size( 581 , 119 );
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 40F ) );
            this.tableLayoutPanel3.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent , 60F ) );
            this.tableLayoutPanel3.Controls.Add( this.btnLogin , 0 , 2 );
            this.tableLayoutPanel3.Controls.Add( this.btnCancel , 1 , 2 );
            this.tableLayoutPanel3.Controls.Add( this.lblUserName , 0 , 0 );
            this.tableLayoutPanel3.Controls.Add( this.lblPassword , 0 , 1 );
            this.tableLayoutPanel3.Controls.Add( this.txtUserNo , 1 , 0 );
            this.tableLayoutPanel3.Controls.Add( this.txtPassword , 1 , 1 );
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point( 190 , 0 );
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding( 0 );
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 40F ) );
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 40F ) );
            this.tableLayoutPanel3.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Absolute , 40F ) );
            this.tableLayoutPanel3.Size = new System.Drawing.Size( 200 , 119 );
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point( 5 , 14 );
            this.lblUserName.Margin = new System.Windows.Forms.Padding( 5 );
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size( 47 , 12 );
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "工 号：";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point( 5 , 54 );
            this.lblPassword.Margin = new System.Windows.Forms.Padding( 5 );
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size( 47 , 12 );
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "密 码：";
            // 
            // txtUserNo
            // 
            this.txtUserNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtUserNo.Location = new System.Drawing.Point( 83 , 9 );
            this.txtUserNo.MaxLength = 11;
            this.txtUserNo.Name = "txtUserNo";
            this.txtUserNo.Size = new System.Drawing.Size( 107 , 21 );
            this.txtUserNo.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPassword.Location = new System.Drawing.Point( 83 , 49 );
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size( 107 , 21 );
            this.txtPassword.TabIndex = 5;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler( this.txtPassword_KeyDown );
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size( 581 , 298 );
            this.Controls.Add( this.tableLayoutPanel1 );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.FrmLogin_FormClosing );
            this.Load += new System.EventHandler( this.FrmLogin_Load );
            this.tableLayoutPanel4.ResumeLayout( false );
            this.tableLayoutPanel4.PerformLayout( );
            this.tableLayoutPanel1.ResumeLayout( false );
            this.tableLayoutPanel1.PerformLayout( );
            this.tableLayoutPanel2.ResumeLayout( false );
            this.tableLayoutPanel3.ResumeLayout( false );
            this.tableLayoutPanel3.PerformLayout( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserNo;
        private System.Windows.Forms.TextBox txtPassword;
    }
}