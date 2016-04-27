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
            this.ribbonOrbMenuItem1 = new System.Windows.Forms.RibbonOrbMenuItem( );
            this.ribbonOrbMenuItem2 = new System.Windows.Forms.RibbonOrbMenuItem( );
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab( );
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel( );
            this.btnOriginalCLPInput = new System.Windows.Forms.RibbonButton( );
            this.btnOriginalCLPDeleteColumn = new System.Windows.Forms.RibbonButton( );
            this.btnOriginalCLPAddBasicData = new System.Windows.Forms.RibbonButton( );
            this.btnOriginalCLPDoubleOrSet = new System.Windows.Forms.RibbonButton( );
            this.btnOriginalCLPFilterSupervisionCondition = new System.Windows.Forms.RibbonButton( );
            this.btnOriginalCLPOutput = new System.Windows.Forms.RibbonButton( );
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel( );
            this.btnUnionCLPInput = new System.Windows.Forms.RibbonButton( );
            this.btnUnionCLPTable = new System.Windows.Forms.RibbonButton( );
            this.btnDealSet = new System.Windows.Forms.RibbonButton( );
            this.btnDealInspection = new System.Windows.Forms.RibbonButton( );
            this.btnDealNonInspection = new System.Windows.Forms.RibbonButton( );
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab( );
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab( );
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel( );
            this.btnCLPBasicDataInput = new System.Windows.Forms.RibbonButton( );
            this.btnCLPBasicDataOutput = new System.Windows.Forms.RibbonButton( );
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel( );
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton10 = new System.Windows.Forms.RibbonButton( );
            this.ribbonButton11 = new System.Windows.Forms.RibbonButton( );
            this.statusStrip1 = new System.Windows.Forms.StatusStrip( );
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel( );
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel( );
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog( );
            this.btnUnionCLPTotalOutput = new System.Windows.Forms.RibbonButton( );
            this.statusStrip1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // ribbon1
            // 
            this.ribbon1.CaptionBarVisible = false;
            this.ribbon1.Font = new System.Drawing.Font( "微软雅黑" , 9F );
            this.ribbon1.Location = new System.Drawing.Point( 0 , 0 );
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point( 0 , 0 );
            this.ribbon1.OrbDropDown.MenuItems.Add( this.ribbonOrbMenuItem1 );
            this.ribbon1.OrbDropDown.MenuItems.Add( this.ribbonOrbMenuItem2 );
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size( 527 , 160 );
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbText = "Menu";
            this.ribbon1.OrbVisible = false;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font( "Trebuchet MS" , 9F );
            this.ribbon1.Size = new System.Drawing.Size( 1161 , 126 );
            this.ribbon1.TabIndex = 1;
            this.ribbon1.Tabs.Add( this.ribbonTab1 );
            this.ribbon1.Tabs.Add( this.ribbonTab2 );
            this.ribbon1.Tabs.Add( this.ribbonTab3 );
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding( 12 , 2 , 20 , 0 );
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribbonOrbMenuItem1
            // 
            this.ribbonOrbMenuItem1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItem1.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.address_book_2;
            this.ribbonOrbMenuItem1.Name = "ribbonOrbMenuItem1";
            this.ribbonOrbMenuItem1.SmallImage = global::DecathlonDataProcessSystem.App.Properties.Resources.address_book_2;
            this.ribbonOrbMenuItem1.Tag = "";
            this.ribbonOrbMenuItem1.Text = "导入";
            // 
            // ribbonOrbMenuItem2
            // 
            this.ribbonOrbMenuItem2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.ribbonOrbMenuItem2.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.adress_book;
            this.ribbonOrbMenuItem2.Name = "ribbonOrbMenuItem2";
            this.ribbonOrbMenuItem2.SmallImage = global::DecathlonDataProcessSystem.App.Properties.Resources.adress_book;
            this.ribbonOrbMenuItem2.Text = "导出";
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
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPDeleteColumn );
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPAddBasicData );
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPDoubleOrSet );
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPFilterSupervisionCondition );
            this.ribbonPanel1.Items.Add( this.btnOriginalCLPOutput );
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "原始CLP制作";
            // 
            // btnOriginalCLPInput
            // 
            this.btnOriginalCLPInput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.xls;
            this.btnOriginalCLPInput.Name = "btnOriginalCLPInput";
            this.btnOriginalCLPInput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPInput.SmallImage" ) ) );
            this.btnOriginalCLPInput.Text = "原始资料导入";
            this.btnOriginalCLPInput.Click += new System.EventHandler( this.btnOriginalCLPInput_Click );
            // 
            // btnOriginalCLPDeleteColumn
            // 
            this.btnOriginalCLPDeleteColumn.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.app;
            this.btnOriginalCLPDeleteColumn.Name = "btnOriginalCLPDeleteColumn";
            this.btnOriginalCLPDeleteColumn.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPDeleteColumn.SmallImage" ) ) );
            this.btnOriginalCLPDeleteColumn.Text = "删除多余列";
            this.btnOriginalCLPDeleteColumn.Click += new System.EventHandler( this.btnOriginalCLPDeleteColumn_Click );
            // 
            // btnOriginalCLPAddBasicData
            // 
            this.btnOriginalCLPAddBasicData.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.donate;
            this.btnOriginalCLPAddBasicData.Name = "btnOriginalCLPAddBasicData";
            this.btnOriginalCLPAddBasicData.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPAddBasicData.SmallImage" ) ) );
            this.btnOriginalCLPAddBasicData.Text = "添加基础数据";
            this.btnOriginalCLPAddBasicData.Click += new System.EventHandler( this.btnOriginalCLPAddBasicData_Click );
            // 
            // btnOriginalCLPDoubleOrSet
            // 
            this.btnOriginalCLPDoubleOrSet.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.pencil_2;
            this.btnOriginalCLPDoubleOrSet.Name = "btnOriginalCLPDoubleOrSet";
            this.btnOriginalCLPDoubleOrSet.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPDoubleOrSet.SmallImage" ) ) );
            this.btnOriginalCLPDoubleOrSet.Text = "Double或Set";
            this.btnOriginalCLPDoubleOrSet.Click += new System.EventHandler( this.btnOriginalCLPDoubleOrSet_Click );
            // 
            // btnOriginalCLPFilterSupervisionCondition
            // 
            this.btnOriginalCLPFilterSupervisionCondition.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.basket;
            this.btnOriginalCLPFilterSupervisionCondition.Name = "btnOriginalCLPFilterSupervisionCondition";
            this.btnOriginalCLPFilterSupervisionCondition.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPFilterSupervisionCondition.SmallImage" ) ) );
            this.btnOriginalCLPFilterSupervisionCondition.Text = "掏箱与不出运";
            this.btnOriginalCLPFilterSupervisionCondition.Click += new System.EventHandler( this.btnOriginalCLPFilterSupervisionCondition_Click );
            // 
            // btnOriginalCLPOutput
            // 
            this.btnOriginalCLPOutput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.calendar;
            this.btnOriginalCLPOutput.Name = "btnOriginalCLPOutput";
            this.btnOriginalCLPOutput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnOriginalCLPOutput.SmallImage" ) ) );
            this.btnOriginalCLPOutput.Text = "原始资料导出";
            this.btnOriginalCLPOutput.Click += new System.EventHandler( this.btnOriginalCLPOutput_Click );
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add( this.btnUnionCLPInput );
            this.ribbonPanel2.Items.Add( this.btnUnionCLPTable );
            this.ribbonPanel2.Items.Add( this.btnDealSet );
            this.ribbonPanel2.Items.Add( this.btnDealInspection );
            this.ribbonPanel2.Items.Add( this.btnDealNonInspection );
            this.ribbonPanel2.Items.Add( this.btnUnionCLPTotalOutput );
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "汇总CLP制作";
            // 
            // btnUnionCLPInput
            // 
            this.btnUnionCLPInput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.xls;
            this.btnUnionCLPInput.Name = "btnUnionCLPInput";
            this.btnUnionCLPInput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnUnionCLPInput.SmallImage" ) ) );
            this.btnUnionCLPInput.Text = "CLP导入";
            this.btnUnionCLPInput.Click += new System.EventHandler( this.btnUnionCLPInput_Click );
            // 
            // btnUnionCLPTable
            // 
            this.btnUnionCLPTable.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.burn;
            this.btnUnionCLPTable.Name = "btnUnionCLPTable";
            this.btnUnionCLPTable.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnUnionCLPTable.SmallImage" ) ) );
            this.btnUnionCLPTable.Text = "合并CLP";
            this.btnUnionCLPTable.Click += new System.EventHandler( this.btnUnionCLPTable_Click );
            // 
            // btnDealSet
            // 
            this.btnDealSet.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.clock;
            this.btnDealSet.Name = "btnDealSet";
            this.btnDealSet.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnDealSet.SmallImage" ) ) );
            this.btnDealSet.Text = "处理SET数据";
            this.btnDealSet.Click += new System.EventHandler( this.btnDealSet_Click );
            // 
            // btnDealInspection
            // 
            this.btnDealInspection.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.collaboration;
            this.btnDealInspection.Name = "btnDealInspection";
            this.btnDealInspection.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnDealInspection.SmallImage" ) ) );
            this.btnDealInspection.Text = "处理商检数据";
            this.btnDealInspection.Click += new System.EventHandler( this.btnDealInspection_Click );
            // 
            // btnDealNonInspection
            // 
            this.btnDealNonInspection.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.comment;
            this.btnDealNonInspection.Name = "btnDealNonInspection";
            this.btnDealNonInspection.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnDealNonInspection.SmallImage" ) ) );
            this.btnDealNonInspection.Text = "处理非商检数据";
            this.btnDealNonInspection.Click += new System.EventHandler( this.btnDealNonInspection_Click );
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
            this.ribbonPanel3.Items.Add( this.btnCLPBasicDataInput );
            this.ribbonPanel3.Items.Add( this.btnCLPBasicDataOutput );
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "基础资料导入&导出";
            // 
            // btnCLPBasicDataInput
            // 
            this.btnCLPBasicDataInput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.xls;
            this.btnCLPBasicDataInput.Name = "btnCLPBasicDataInput";
            this.btnCLPBasicDataInput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnCLPBasicDataInput.SmallImage" ) ) );
            this.btnCLPBasicDataInput.Text = "基础资料导入";
            this.btnCLPBasicDataInput.Click += new System.EventHandler( this.btnCLPBasicDataInput_Click );
            // 
            // btnCLPBasicDataOutput
            // 
            this.btnCLPBasicDataOutput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.upcoming_work;
            this.btnCLPBasicDataOutput.Name = "btnCLPBasicDataOutput";
            this.btnCLPBasicDataOutput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnCLPBasicDataOutput.SmallImage" ) ) );
            this.btnCLPBasicDataOutput.Text = "基础资料导出";
            this.btnCLPBasicDataOutput.Click += new System.EventHandler( this.btnCLPBasicDataOutput_Click );
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
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2} );
            this.statusStrip1.Location = new System.Drawing.Point( 0 , 528 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size( 1161 , 22 );
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size( 0 , 17 );
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel2.Size = new System.Drawing.Size( 0 , 17 );
            // 
            // btnUnionCLPTotalOutput
            // 
            this.btnUnionCLPTotalOutput.Image = global::DecathlonDataProcessSystem.App.Properties.Resources.calendar;
            this.btnUnionCLPTotalOutput.Name = "btnUnionCLPTotalOutput";
            this.btnUnionCLPTotalOutput.SmallImage = ( (System.Drawing.Image)( resources.GetObject( "btnUnionCLPTotalOutput.SmallImage" ) ) );
            this.btnUnionCLPTotalOutput.Text = "CLP资料导出";
            this.btnUnionCLPTotalOutput.Click += new System.EventHandler( this.btnUnionCLPTotalOutput_Click );
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F , 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size( 1161 , 550 );
            this.Controls.Add( this.statusStrip1 );
            this.Controls.Add( this.ribbon1 );
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "迪卡侬报关资料处理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.RibbonButton btnOriginalCLPDeleteColumn;
        private System.Windows.Forms.RibbonButton btnOriginalCLPAddBasicData;
        private System.Windows.Forms.RibbonButton btnOriginalCLPDoubleOrSet;
        private System.Windows.Forms.RibbonButton btnUnionCLPTable;
        private System.Windows.Forms.RibbonButton btnOriginalCLPOutput;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton btnCLPBasicDataInput;
        private System.Windows.Forms.RibbonButton btnCLPBasicDataOutput;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.RibbonButton ribbonButton10;
        private System.Windows.Forms.RibbonButton ribbonButton11;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RibbonButton btnOriginalCLPFilterSupervisionCondition;
        private System.Windows.Forms.RibbonButton btnDealSet;
        private System.Windows.Forms.RibbonButton btnDealInspection;
        private System.Windows.Forms.RibbonButton btnDealNonInspection;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItem1;
        private System.Windows.Forms.RibbonOrbMenuItem ribbonOrbMenuItem2;
        private System.Windows.Forms.RibbonButton btnUnionCLPInput;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.RibbonButton btnUnionCLPTotalOutput;
    }
}