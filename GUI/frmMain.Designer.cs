
using DevExpress.LookAndFeel;

namespace Analyze.DesktopApp.GUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtnListFollow = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSupport = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnMyList = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnConfigFx = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnConfigNotify = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnBlackList = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTop30 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnListTrade = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRealTime = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnStart = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnStop = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnVersion = new DevExpress.XtraBars.BarButtonItem();
            this.barBtn24h = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAnalyze = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonGroupSupport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barBtnListFollow,
            this.barBtnSupport,
            this.barBtnMyList,
            this.barBtnConfigFx,
            this.barBtnConfigNotify,
            this.barBtnBlackList,
            this.barBtnTop30,
            this.barBtnListTrade,
            this.barBtnRealTime,
            this.barBtnStart,
            this.barBtnStop,
            this.barBtnVersion,
            this.barBtn24h,
            this.barBtnAdd,
            this.barBtnAnalyze});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 17;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.MacOffice;
            this.ribbon.Size = new System.Drawing.Size(1022, 141);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barBtnListFollow
            // 
            this.barBtnListFollow.Caption = "Danh sách theo dõi";
            this.barBtnListFollow.Enabled = false;
            this.barBtnListFollow.Id = 1;
            this.barBtnListFollow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnListFollow.ImageOptions.SvgImage")));
            this.barBtnListFollow.Name = "barBtnListFollow";
            this.barBtnListFollow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnListFollow_ItemClick);
            // 
            // barBtnSupport
            // 
            this.barBtnSupport.Caption = "Hỗ trợ";
            this.barBtnSupport.Enabled = false;
            this.barBtnSupport.Id = 2;
            this.barBtnSupport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnSupport.ImageOptions.SvgImage")));
            this.barBtnSupport.Name = "barBtnSupport";
            this.barBtnSupport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSupport_ItemClick);
            // 
            // barBtnMyList
            // 
            this.barBtnMyList.Caption = "My List";
            this.barBtnMyList.Enabled = false;
            this.barBtnMyList.Id = 3;
            this.barBtnMyList.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnMyList.ImageOptions.SvgImage")));
            this.barBtnMyList.Name = "barBtnMyList";
            this.barBtnMyList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnInfo_ItemClick);
            // 
            // barBtnConfigFx
            // 
            this.barBtnConfigFx.Caption = "Cấu hình chỉ báo";
            this.barBtnConfigFx.Enabled = false;
            this.barBtnConfigFx.Id = 5;
            this.barBtnConfigFx.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnConfigFx.ImageOptions.SvgImage")));
            this.barBtnConfigFx.Name = "barBtnConfigFx";
            this.barBtnConfigFx.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnConfigFx_ItemClick);
            // 
            // barBtnConfigNotify
            // 
            this.barBtnConfigNotify.Caption = "Cấu hình thông báo";
            this.barBtnConfigNotify.Enabled = false;
            this.barBtnConfigNotify.Id = 6;
            this.barBtnConfigNotify.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnConfigNotify.ImageOptions.SvgImage")));
            this.barBtnConfigNotify.Name = "barBtnConfigNotify";
            // 
            // barBtnBlackList
            // 
            this.barBtnBlackList.Caption = "Danh sách đen";
            this.barBtnBlackList.Enabled = false;
            this.barBtnBlackList.Id = 7;
            this.barBtnBlackList.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnBlackList.ImageOptions.SvgImage")));
            this.barBtnBlackList.Name = "barBtnBlackList";
            this.barBtnBlackList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBlackList_ItemClick);
            // 
            // barBtnTop30
            // 
            this.barBtnTop30.Caption = "Top30";
            this.barBtnTop30.Enabled = false;
            this.barBtnTop30.Id = 8;
            this.barBtnTop30.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnTop30.ImageOptions.SvgImage")));
            this.barBtnTop30.Name = "barBtnTop30";
            this.barBtnTop30.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTop30_ItemClick);
            // 
            // barBtnListTrade
            // 
            this.barBtnListTrade.Caption = "Danh sách Trade";
            this.barBtnListTrade.Enabled = false;
            this.barBtnListTrade.Id = 9;
            this.barBtnListTrade.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnListTrade.ImageOptions.SvgImage")));
            this.barBtnListTrade.Name = "barBtnListTrade";
            this.barBtnListTrade.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnListTrade_ItemClick);
            // 
            // barBtnRealTime
            // 
            this.barBtnRealTime.Caption = "Thời gian thực";
            this.barBtnRealTime.Enabled = false;
            this.barBtnRealTime.Id = 10;
            this.barBtnRealTime.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnRealTime.ImageOptions.SvgImage")));
            this.barBtnRealTime.Name = "barBtnRealTime";
            this.barBtnRealTime.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRealTime_ItemClick);
            // 
            // barBtnStart
            // 
            this.barBtnStart.Caption = "Start";
            this.barBtnStart.Id = 11;
            this.barBtnStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnStart.ImageOptions.SvgImage")));
            this.barBtnStart.Name = "barBtnStart";
            this.barBtnStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnStart_ItemClick);
            // 
            // barBtnStop
            // 
            this.barBtnStop.Caption = "Stop";
            this.barBtnStop.Enabled = false;
            this.barBtnStop.Id = 12;
            this.barBtnStop.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnStop.ImageOptions.SvgImage")));
            this.barBtnStop.Name = "barBtnStop";
            this.barBtnStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnStop_ItemClick);
            // 
            // barBtnVersion
            // 
            this.barBtnVersion.Caption = "Phiên bản";
            this.barBtnVersion.Enabled = false;
            this.barBtnVersion.Id = 13;
            this.barBtnVersion.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barBtnVersion.ImageOptions.SvgImage")));
            this.barBtnVersion.Name = "barBtnVersion";
            // 
            // barBtn24h
            // 
            this.barBtn24h.Caption = "24H";
            this.barBtn24h.Enabled = false;
            this.barBtn24h.Id = 14;
            this.barBtn24h.ImageOptions.Image = global::Analyze.DesktopApp.Properties.Resources._24h;
            this.barBtn24h.Name = "barBtn24h";
            this.barBtn24h.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barBtn24h.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn24h_ItemClick);
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Thêm";
            this.barBtnAdd.Enabled = false;
            this.barBtnAdd.Id = 15;
            this.barBtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.Image")));
            this.barBtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.LargeImage")));
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnAnalyze
            // 
            this.barBtnAnalyze.Caption = "Analyze";
            this.barBtnAnalyze.Id = 16;
            this.barBtnAnalyze.ImageOptions.Image = global::Analyze.DesktopApp.Properties.Resources.kpi_16x16;
            this.barBtnAnalyze.ImageOptions.LargeImage = global::Analyze.DesktopApp.Properties.Resources.kpi_32x32;
            this.barBtnAnalyze.Name = "barBtnAnalyze";
            this.barBtnAnalyze.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAnalyze_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonGroup1,
            this.ribbonGroup2,
            this.ribbonGroupSupport,
            this.ribbonGroup3,
            this.ribbonGroup4,
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Main";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.ItemLinks.Add(this.barBtnMyList);
            this.ribbonGroup1.ItemLinks.Add(this.barBtnAdd);
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "Thông tin";
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.ItemLinks.Add(this.barBtn24h);
            this.ribbonGroup2.ItemLinks.Add(this.barBtnRealTime);
            this.ribbonGroup2.ItemLinks.Add(this.barBtnTop30);
            this.ribbonGroup2.ItemLinks.Add(this.barBtnAnalyze);
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "Thống kê";
            // 
            // ribbonGroupSupport
            // 
            this.ribbonGroupSupport.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageGroupAlignment.Far;
            this.ribbonGroupSupport.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.True;
            this.ribbonGroupSupport.ItemLinks.Add(this.barBtnVersion);
            this.ribbonGroupSupport.ItemLinks.Add(this.barBtnSupport);
            this.ribbonGroupSupport.Name = "ribbonGroupSupport";
            // 
            // ribbonGroup3
            // 
            this.ribbonGroup3.ItemLinks.Add(this.barBtnListTrade);
            this.ribbonGroup3.ItemLinks.Add(this.barBtnListFollow);
            this.ribbonGroup3.ItemLinks.Add(this.barBtnBlackList);
            this.ribbonGroup3.Name = "ribbonGroup3";
            this.ribbonGroup3.Text = "Sở hữu";
            // 
            // ribbonGroup4
            // 
            this.ribbonGroup4.ItemLinks.Add(this.barBtnConfigFx);
            this.ribbonGroup4.ItemLinks.Add(this.barBtnConfigNotify);
            this.ribbonGroup4.Name = "ribbonGroup4";
            this.ribbonGroup4.Text = "Cài đặt";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Enabled = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtnStart);
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtnStop);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Action";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 743);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1022, 24);
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "ribbonPage4";
            // 
            // tabControl
            // 
            this.tabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InTabControlHeader;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 141);
            this.tabControl.LookAndFeel.SkinName = "McSkin";
            this.tabControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(1022, 602);
            this.tabControl.TabIndex = 2;
            this.tabControl.CloseButtonClick += new System.EventHandler(this.tabControl_CloseButtonClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "MCDX";
            this.barButtonItem2.Id = 4;
            this.barButtonItem2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem2.ImageOptions.SvgImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "MCDX";
            this.barButtonItem3.Id = 4;
            this.barButtonItem3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "MCDX";
            this.barButtonItem4.Id = 4;
            this.barButtonItem4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem4.ImageOptions.SvgImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 767);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "frmMain";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroupSupport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroup3;
        private DevExpress.XtraBars.BarButtonItem barBtnListFollow;
        private DevExpress.XtraBars.BarButtonItem barBtnSupport;
        public DevExpress.XtraBars.BarButtonItem barBtnMyList;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.BarButtonItem barBtnConfigFx;
        private DevExpress.XtraBars.BarButtonItem barBtnConfigNotify;
        private DevExpress.XtraBars.BarButtonItem barBtnBlackList;
        private DevExpress.XtraBars.BarButtonItem barBtnTop30;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroup4;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraBars.BarButtonItem barBtnListTrade;
        private DevExpress.XtraBars.BarButtonItem barBtnRealTime;
        private DevExpress.XtraBars.BarButtonItem barBtnStart;
        private DevExpress.XtraBars.BarButtonItem barBtnStop;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barBtnVersion;
        public DevExpress.XtraBars.BarButtonItem barBtn24h;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        public DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnAnalyze;
    }
}