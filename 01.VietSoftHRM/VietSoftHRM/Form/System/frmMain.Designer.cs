namespace VietSoftHRM
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.navigationFrame = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navigationPageHome = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUserName = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barLogout = new DevExpress.XtraBars.BarButtonItem();
            this.barSkin = new DevExpress.XtraBars.SkinDropDownButtonItem();
            this.barlanguage = new DevExpress.XtraBars.BarSubItem();
            this.barVietnam = new DevExpress.XtraBars.BarCheckItem();
            this.barEnglish = new DevExpress.XtraBars.BarCheckItem();
            this.barExits = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barLogin = new DevExpress.XtraBars.BarStaticItem();
            this.barVer = new DevExpress.XtraBars.BarStaticItem();
            this.barTTC = new DevExpress.XtraBars.BarStaticItem();
            this.barServer = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barInfo = new DevExpress.XtraBars.BarStaticItem();
            this.tileBar = new DevExpress.XtraBars.Navigation.TileBar();
            this.titlegroup = new DevExpress.XtraBars.Navigation.TileBarGroup();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).BeginInit();
            this.navigationFrame.SuspendLayout();
            this.navigationPageHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationFrame
            // 
            this.navigationFrame.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.SetColumn(this.navigationFrame, 0);
            this.tablePanel1.SetColumnSpan(this.navigationFrame, 2);
            this.navigationFrame.Controls.Add(this.navigationPageHome);
            this.navigationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame.Location = new System.Drawing.Point(4, 60);
            this.navigationFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.navigationFrame.Name = "navigationFrame";
            this.navigationFrame.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPageHome});
            this.navigationFrame.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.tablePanel1.SetRow(this.navigationFrame, 3);
            this.navigationFrame.SelectedPage = this.navigationPageHome;
            this.navigationFrame.Size = new System.Drawing.Size(1057, 534);
            this.navigationFrame.TabIndex = 5;
            this.navigationFrame.Text = "navigationFrame1";
            this.navigationFrame.TransitionAnimationProperties.FrameInterval = 5000;
            this.navigationFrame.TransitionType = DevExpress.Utils.Animation.Transitions.Zoom;
            // 
            // navigationPageHome
            // 
            this.navigationPageHome.Controls.Add(this.pictureBox1);
            this.navigationPageHome.Name = "navigationPageHome";
            this.navigationPageHome.Size = new System.Drawing.Size(1057, 534);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1057, 534);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnUserName
            // 
            this.btnUserName.AutoSize = true;
            this.btnUserName.AutoWidthInLayoutControl = true;
            this.btnUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUserName.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.btnUserName.DropDownControl = this.popupMenu1;
            this.btnUserName.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserName.ImageOptions.Image")));
            this.btnUserName.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnUserName.Location = new System.Drawing.Point(0, 0);
            this.btnUserName.Name = "btnUserName";
            this.btnUserName.Size = new System.Drawing.Size(135, 23);
            this.btnUserName.TabIndex = 6;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLogout),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSkin),
            new DevExpress.XtraBars.LinkPersistInfo(this.barlanguage),
            new DevExpress.XtraBars.LinkPersistInfo(this.barExits)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.CloseUp += new System.EventHandler(this.popupMenu1_CloseUp);
            // 
            // barLogout
            // 
            this.barLogout.Caption = "Đăng xuất";
            this.barLogout.Id = 4;
            this.barLogout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barLogout.ImageOptions.SvgImage")));
            this.barLogout.Name = "barLogout";
            this.barLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLogout_ItemClick);
            // 
            // barSkin
            // 
            this.barSkin.Id = 3;
            this.barSkin.Name = "barSkin";
            this.barSkin.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barSkin_DownChanged);
            // 
            // barlanguage
            // 
            this.barlanguage.Caption = "Ngôn ngữ";
            this.barlanguage.Id = 8;
            this.barlanguage.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barVietnam),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEnglish)});
            this.barlanguage.Name = "barlanguage";
            // 
            // barVietnam
            // 
            this.barVietnam.Caption = "Tiếng việt";
            this.barVietnam.Id = 1;
            this.barVietnam.Name = "barVietnam";
            this.barVietnam.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barVietnam_CheckedChanged);
            // 
            // barEnglish
            // 
            this.barEnglish.Caption = "Tiếng anh";
            this.barEnglish.Id = 2;
            this.barEnglish.Name = "barEnglish";
            this.barEnglish.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barEnglish_CheckedChanged);
            // 
            // barExits
            // 
            this.barExits.Caption = "Thoát";
            this.barExits.Id = 5;
            this.barExits.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barExits.ImageOptions.Image")));
            this.barExits.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barExits.ImageOptions.LargeImage")));
            this.barExits.Name = "barExits";
            this.barExits.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barExits_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barVietnam,
            this.barEnglish,
            this.barSkin,
            this.barLogout,
            this.barExits,
            this.barlanguage,
            this.barInfo,
            this.barServer,
            this.barTTC,
            this.barVer,
            this.barLogin});
            this.barManager1.MaxItemId = 14;
            this.barManager1.StatusBar = this.bar1;
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Disabled.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.bar1.BarAppearance.Disabled.Options.UseFont = true;
            this.bar1.BarAppearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.bar1.BarAppearance.Hovered.Options.UseFont = true;
            this.bar1.BarAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarAppearance.Pressed.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.bar1.BarAppearance.Pressed.Options.UseFont = true;
            this.bar1.BarName = "Custom 2";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLogin),
            new DevExpress.XtraBars.LinkPersistInfo(this.barVer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTTC),
            new DevExpress.XtraBars.LinkPersistInfo(this.barServer)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // barLogin
            // 
            this.barLogin.Caption = "barLogin";
            this.barLogin.Id = 13;
            this.barLogin.Name = "barLogin";
            // 
            // barVer
            // 
            this.barVer.Caption = "barVer";
            this.barVer.Id = 12;
            this.barVer.Name = "barVer";
            // 
            // barTTC
            // 
            this.barTTC.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.barTTC.Caption = "barTTC";
            this.barTTC.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.barTTC.Id = 11;
            this.barTTC.Name = "barTTC";
            // 
            // barServer
            // 
            this.barServer.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barServer.Caption = "barServer";
            this.barServer.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Far;
            this.barServer.Id = 10;
            this.barServer.Name = "barServer";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1065, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 597);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1065, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 597);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1065, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 597);
            // 
            // barInfo
            // 
            this.barInfo.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barInfo.Caption = "barInfo";
            this.barInfo.Id = 9;
            this.barInfo.Name = "barInfo";
            // 
            // tileBar
            // 
            this.tileBar.AllowGlyphSkinning = true;
            this.tileBar.AllowSelectedItem = true;
            this.tileBar.AllowSelectedItemBorder = false;
            this.tablePanel1.SetColumn(this.tileBar, 0);
            this.tileBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileBar.DropDownButtonWidth = 30;
            this.tileBar.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileBar.Groups.Add(this.titlegroup);
            this.tileBar.IndentBetweenGroups = 10;
            this.tileBar.IndentBetweenItems = 10;
            this.tileBar.ItemPadding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.tileBar.ItemSize = 50;
            this.tileBar.Location = new System.Drawing.Point(3, 3);
            this.tileBar.MaxId = 27;
            this.tileBar.Name = "tileBar";
            this.tileBar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tablePanel1.SetRow(this.tileBar, 0);
            this.tablePanel1.SetRowSpan(this.tileBar, 3);
            this.tileBar.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollButtons;
            this.tileBar.SelectionBorderWidth = 3;
            this.tileBar.SelectionColor = System.Drawing.Color.Empty;
            this.tileBar.SelectionColorMode = DevExpress.XtraBars.Navigation.SelectionColorMode.UseItemBackColor;
            this.tileBar.ShowGroupText = false;
            this.tileBar.Size = new System.Drawing.Size(918, 51);
            this.tileBar.TabIndex = 4;
            this.tileBar.Text = "tileBar";
            this.tileBar.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tileBar.WideTileWidth = 150;
            this.tileBar.SelectedItemChanged += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileBar_SelectedItemChanged);
            // 
            // titlegroup
            // 
            this.titlegroup.Name = "titlegroup";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 141F)});
            this.tablePanel1.Controls.Add(this.panel1);
            this.tablePanel1.Controls.Add(this.tileBar);
            this.tablePanel1.Controls.Add(this.navigationFrame);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 11F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 35F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 11F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Size = new System.Drawing.Size(1065, 597);
            this.tablePanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.tablePanel1.SetColumn(this.panel1, 1);
            this.panel1.Controls.Add(this.btnUserName);
            this.panel1.Location = new System.Drawing.Point(927, 17);
            this.panel1.Name = "panel1";
            this.tablePanel1.SetRow(this.panel1, 1);
            this.panel1.Size = new System.Drawing.Size(135, 23);
            this.panel1.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 625);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmMain.IconOptions.Image")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame)).EndInit();
            this.navigationFrame.ResumeLayout(false);
            this.navigationPageHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame;
        private DevExpress.XtraBars.Navigation.TileBarGroup titlegroup;
        public DevExpress.XtraBars.Navigation.TileBar tileBar;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPageHome;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.DropDownButton btnUserName;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barLogout;
        private DevExpress.XtraBars.BarCheckItem barVietnam;
        private DevExpress.XtraBars.BarCheckItem barEnglish;
        private DevExpress.XtraBars.SkinDropDownButtonItem barSkin;
        private DevExpress.XtraBars.BarButtonItem barExits;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barlanguage;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarStaticItem barInfo;
        private DevExpress.XtraBars.BarStaticItem barTTC;
        private DevExpress.XtraBars.BarStaticItem barServer;
        private DevExpress.XtraBars.BarStaticItem barVer;
        private DevExpress.XtraBars.BarStaticItem barLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Timer timer1;
    }
}