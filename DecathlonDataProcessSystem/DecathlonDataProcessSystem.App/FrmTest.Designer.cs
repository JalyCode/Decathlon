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
            this.panelMain = new System.Windows.Forms.Panel( );
            this.SuspendLayout( );
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 194 ) ) ) ) , ( (int)( ( (byte)( 224 ) ) ) ) , ( (int)( ( (byte)( 255 ) ) ) ) );
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point( 0 , 0 );
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size( 949 , 481 );
            this.panelMain.TabIndex = 3;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 949 , 481 );
            this.Controls.Add( this.panelMain );
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
    }
}