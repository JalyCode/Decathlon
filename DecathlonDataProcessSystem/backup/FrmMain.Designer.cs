namespace DecathlonDataProcessSystem.App
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( FrmMain ) );
            this.ribbon1 = new System.Windows.Forms.Ribbon( );
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab( );
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel( );
            this.btnOriginalCLPInput = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton2 = new System.Windows.Forms.RibbonButton( );
            this.btnAddBasicData = new System.Windows.Forms.RibbonButton( );
            this.btnDoubleOrSet = new System.Windows.Forms.RibbonButton( );
            this.btnFilterSupervisionCondition = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton6 = new System.Windows.Forms.RibbonButton( );
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel( );
            this.ribbonButton5 = new System.Windows.Forms.RibbonButton( );
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab( );
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab( );
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel( );
            this.btnBasicDataInput = new System.Windows.Forms.RibbonButton( );
            this.btnBasicDataOutput = new System.Windows.Forms.RibbonButton( );
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel( );
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton10 = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton11 = new System.Windows.Forms.RibbonButton( );
            this.statusStrip1 = new System.Windows.Forms.StatusStrip( );
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel( );
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog( );
            this.statusStrip1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font( "微软雅黑" , 9F );
            this.ribbon1.Location = new System.Drawing.Point( 0 , 0 );
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point( 0 , 0 );
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size( 527 , 447 );
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbText = "Menu";
            this.ribbon1.RibbonTabFont = new System.Drawing.Font( "Trebuchet MS" , 9F );
            this.ribbon1.Size = new System.Drawing.Size( 983 , 126 );
            this.ribbon1.TabIndex = 1;
            this.ribbon1.Tabs.Add( this.ribbonTab1 );
            this.ribbon1.Tabs.Add( this.ribbonTab2 );
            this.ribbon1.Tabs.Add( this.ribbonTab3 );
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding( 12 , 26 , 20 , 0 );
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add( this.ribbonPanel1 );
            this.ribbonTab1.Panels.Add( this.ribbonPanel2 );
            this.ribbonTab1.Text = "报关数据处理";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPInput );
            this.ribbonPanel1.Items.Add( this.ribbonButton2 );
            this.ribbonPanel1.Items.Add( this.btnAddBasicData );
            this.ribbonPanel1.Items.Add( this.btnDoubleOrSet );
            this.ribbonPanel1.Items.Add( this.btnFilterSupervisionCondition );
            this.ribbonPanel1.Items.Add( this.ribbonButton6 );
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "原始CLP制作";
            // 
            // btnOriginalCLPInput
            // 
            this.btnOriginalCLPInput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.xls;
            this.btnOriginalCLPInput.Name = "btnOriginalCLPInput";
            this.btnOriginalCLPInput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPInput.SmallImage" ) ) );
            this.btnOriginalCLPInput.Text = "导入原始资料";
            this.btnOriginalCLPInput.Click += new System.EventHandler( this.btnOriginalCLPInput_Click );
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.app;
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton2.SmallImage" ) ) );
            this.ribbonButton2.Text = "删除多余列";
            this.ribbonButton2.Click += new System.EventHandler( this.ribbonButton2_Click );
            // 
            // btnAddBasicData
            // 
            this.btnAddBasicData.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.donate;
            this.btnAddBasicData.Name = "btnAddBasicData";
            this.btnAddBasicData.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnAddBasicData.SmallImage" ) ) );
            this.btnAddBasicData.Text = "添加基础数据";
            this.btnAddBasicData.Click += new System.EventHandler( this.btnAddBasicData_Click );
            // 
            // btnDoubleOrSet
            // 
            this.btnDoubleOrSet.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.pencil_2;
            this.btnDoubleOrSet.Name = "btnDoubleOrSet";
            this.btnDoubleOrSet.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnDoubleOrSet.SmallImage" ) ) );
            this.btnDoubleOrSet.Text = "Double或Set";
            this.btnDoubleOrSet.Click += new System.EventHandler( this.btnDoubleOrSet_Click );
            // 
            // btnFilterSupervisionCondition
            // 
            this.btnFilterSupervisionCondition.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.basket;
            this.btnFilterSupervisionCondition.Name = "btnFilterSupervisionCondition";
            this.btnFilterSupervisionCondition.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnFilterSupervisionCondition.SmallImage" ) ) );
            this.btnFilterSupervisionCondition.Text = "掏箱与不出运";
            this.btnFilterSupervisionCondition.Click += new System.EventHandler( this.btnFilterSupervisionCondition_Click );
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.calendar;
            this.ribbonButton6.Name = "ribbonButton6";
            this.ribbonButton6.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton6.SmallImage" ) ) );
            this.ribbonButton6.Text = "原始资料导出";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add( this.ribbonButton5 );
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "汇总CLP制作";
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.Image = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton5.Image" ) ) );
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton5.SmallImage" ) ) );
            this.ribbonButton5.Text = "合并CLP";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "导出发票箱单";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Panels.Add( this.ribbonPanel3 );
            this.ribbonTab3.Panels.Add( this.ribbonPanel4 );
            this.ribbonTab3.Text = "基础资料维护";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Items.Add( this.btnBasicDataInput );
            this.ribbonPanel3.Items.Add( this.btnBasicDataOutput );
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "基础资料导入&导出";
            // 
            // btnBasicDataInput
            // 
            this.btnBasicDataInput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.xls;
            this.btnBasicDataInput.Name = "btnBasicDataInput";
            this.btnBasicDataInput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnBasicDataInput.SmallImage" ) ) );
            this.btnBasicDataInput.Text = "基础资料导入";
            this.btnBasicDataInput.Click += new System.EventHandler( this.btnBasicDataInput_Click );
            // 
            // btnBasicDataOutput
            // 
            this.btnBasicDataOutput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.upcoming_work;
            this.btnBasicDataOutput.Name = "btnBasicDataOutput";
            this.btnBasicDataOutput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnBasicDataOutput.SmallImage" ) ) );
            this.btnBasicDataOutput.Text = "基础资料导出";
            this.btnBasicDataOutput.Click += new System.EventHandler( this.btnBasicDataOutput_Click );
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.Items.Add( this.ribbonButton9 );
            this.ribbonPanel4.Items.Add( this.ribbonButton10 );
            this.ribbonPanel4.Items.Add( this.ribbonButton11 );
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "基础资料在线维护";
            // 
            // ribbonButton9
            // 
            this.ribbonButton9.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.collaboration;
            this.ribbonButton9.Name = "ribbonButton9";
            this.ribbonButton9.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton9.SmallImage" ) ) );
            this.ribbonButton9.Text = "新增基础资料";
            // 
            // ribbonButton10
            // 
            this.ribbonButton10.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.administrative_docs;
            this.ribbonButton10.Name = "ribbonButton10";
            this.ribbonButton10.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton10.SmallImage" ) ) );
            this.ribbonButton10.Text = "修改基础资料";
            // 
            // ribbonButton11
            // 
            this.ribbonButton11.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.busy;
            this.ribbonButton11.Name = "ribbonButton11";
            this.ribbonButton11.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "ribbonButton11.SmallImage" ) ) );
            this.ribbonButton11.Text = "删除基础资料";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1} );
            this.statusStrip1.Location = new System.Drawing.Point( 0 , 458 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size( 983 , 22 );
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size( 0 , 17 );
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size( 983 , 480 );
            this.Controls.Add( this.statusStrip1 );
            this.Controls.Add( this.ribbon1 );
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "迪卡侬报关资料处理";
            this.statusStrip1.ResumeLayout( false );
            this.statusStrip1.PerformLayout( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonButton btnOriginalCLPInput;
        private System.Windows.Forms.RibbonButton ribbonButton2;
        private System.Windows.Forms.RibbonButton btnAddBasicData;
        private System.Windows.Forms.RibbonButton btnDoubleOrSet;
        private System.Windows.Forms.RibbonButton ribbonButton5;
        private System.Windows.Forms.RibbonButton ribbonButton6;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton btnBasicDataInput;
        private System.Windows.Forms.RibbonButton btnBasicDataOutput;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.RibbonButton ribbonButton10;
        private System.Windows.Forms.RibbonButton ribbonButton11;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RibbonButton btnFilterSupervisionCondition;
    }
}