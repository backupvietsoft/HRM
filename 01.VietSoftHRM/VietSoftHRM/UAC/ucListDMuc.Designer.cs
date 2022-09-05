namespace VietSoftHRM
{
    partial class ucListDMuc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdDanhMuc = new DevExpress.XtraGrid.GridControl();
            this.grvDanhMuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NONNlab_Link = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.accorMenuleft = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDuLieuChoTD = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accorMenuleft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDanhMuc
            // 
            this.grdDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDanhMuc.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdDanhMuc.Location = new System.Drawing.Point(0, 0);
            this.grdDanhMuc.MainView = this.grvDanhMuc;
            this.grdDanhMuc.Margin = new System.Windows.Forms.Padding(2);
            this.grdDanhMuc.Name = "grdDanhMuc";
            this.grdDanhMuc.Size = new System.Drawing.Size(770, 340);
            this.grdDanhMuc.TabIndex = 2;
            this.grdDanhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDanhMuc});
            this.grdDanhMuc.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdDanhMuc_ProcessGridKey);
            this.grdDanhMuc.Click += new System.EventHandler(this.grdDanhMuc_Click);
            this.grdDanhMuc.Validated += new System.EventHandler(this.grdDanhMuc_Validated);
            // 
            // grvDanhMuc
            // 
            this.grvDanhMuc.ColumnPanelRowHeight = 1;
            this.grvDanhMuc.DetailHeight = 193;
            this.grvDanhMuc.FixedLineWidth = 1;
            this.grvDanhMuc.GridControl = this.grdDanhMuc;
            this.grvDanhMuc.Name = "grvDanhMuc";
            this.grvDanhMuc.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvDanhMuc.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvDanhMuc.OptionsCustomization.AllowRowSizing = true;
            this.grvDanhMuc.OptionsFind.FindDelay = 100;
            this.grvDanhMuc.OptionsPrint.AllowMultilineHeaders = true;
            this.grvDanhMuc.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            this.grvDanhMuc.OptionsScrollAnnotations.ShowErrors = DevExpress.Utils.DefaultBoolean.True;
            this.grvDanhMuc.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvDanhMuc.OptionsView.RowAutoHeight = true;
            this.grvDanhMuc.OptionsView.ShowAutoFilterRow = true;
            this.grvDanhMuc.OptionsView.ShowGroupPanel = false;
            this.grvDanhMuc.RowHeight = 1;
            this.grvDanhMuc.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.grvDanhMuc_CustomDrawGroupRow);
            this.grvDanhMuc.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvDanhMuc_PopupMenuShowing);
            this.grvDanhMuc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvDanhMuc_MouseDown);
            this.grvDanhMuc.DoubleClick += new System.EventHandler(this.grvDanhMuc_DoubleClick);
            // 
            // NONNlab_Link
            // 
            this.NONNlab_Link.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.NONNlab_Link.Appearance.Options.UseFont = true;
            this.NONNlab_Link.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.NONNlab_Link.Location = new System.Drawing.Point(1, -1);
            this.NONNlab_Link.Name = "NONNlab_Link";
            this.NONNlab_Link.Size = new System.Drawing.Size(991, 19);
            this.NONNlab_Link.StyleController = this.layoutControl1;
            this.NONNlab_Link.TabIndex = 1;
            this.NONNlab_Link.Text = "labelControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.NONNlab_Link);
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(993, 397);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.windowsUIButton);
            this.panel1.Controls.Add(this.accorMenuleft);
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 374);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.grdDanhMuc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(219, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(770, 340);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(770, 340);
            this.panel3.TabIndex = 5;
            // 
            // windowsUIButton
            // 
            this.windowsUIButton.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.Gray;
            this.windowsUIButton.AppearanceButton.Hovered.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.windowsUIButton.AppearanceButton.Normal.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Normal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.windowsUIButton.AppearanceButton.Normal.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Normal.Options.UseForeColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.windowsUIButton.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseBorderColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseImage = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseTextOptions = true;
            this.windowsUIButton.AutoSizeInLayoutControl = false;
            windowsUIButtonImageOptions1.ImageUri.Uri = "AddItem";
            windowsUIButtonImageOptions2.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions3.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions4.ImageUri.Uri = "ExportToXLSX";
            windowsUIButtonImageOptions5.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "them", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "sua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "excel", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl1);
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.ForeColor = System.Drawing.Color.White;
            this.windowsUIButton.Location = new System.Drawing.Point(219, 340);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.windowsUIButton.MaximumSize = new System.Drawing.Size(0, 78);
            this.windowsUIButton.MinimumSize = new System.Drawing.Size(9, 9);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Size = new System.Drawing.Size(770, 34);
            this.windowsUIButton.TabIndex = 11;
            this.windowsUIButton.Text = "windowsUIButtonPanel";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl1.Client = this.grdDanhMuc;
            this.searchControl1.Location = new System.Drawing.Point(3, 9);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.grdDanhMuc;
            this.searchControl1.Size = new System.Drawing.Size(192, 24);
            this.searchControl1.TabIndex = 0;
            // 
            // accorMenuleft
            // 
            this.accorMenuleft.AllowItemSelection = true;
            this.accorMenuleft.Appearance.Item.Normal.Options.UseTextOptions = true;
            this.accorMenuleft.Appearance.Item.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.accorMenuleft.Appearance.Item.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.accorMenuleft.Appearance.Item.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            this.accorMenuleft.Appearance.Item.Pressed.Options.UseBackColor = true;
            this.accorMenuleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.accorMenuleft.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Single;
            this.accorMenuleft.Location = new System.Drawing.Point(0, 0);
            this.accorMenuleft.Name = "accorMenuleft";
            this.accorMenuleft.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.accorMenuleft.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            this.accorMenuleft.Size = new System.Drawing.Size(219, 374);
            this.accorMenuleft.TabIndex = 3;
            this.accorMenuleft.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(993, 397);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panel1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 19);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(993, 378);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.NONNlab_Link;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(82, 19);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, -1, 1);
            this.layoutControlItem2.Size = new System.Drawing.Size(993, 19);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDuLieuChoTD});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(199, 26);
            // 
            // toolDuLieuChoTD
            // 
            this.toolDuLieuChoTD.Name = "toolDuLieuChoTD";
            this.toolDuLieuChoTD.Size = new System.Drawing.Size(198, 22);
            this.toolDuLieuChoTD.Text = "Dữ liệu cho tuyển dụng";
            this.toolDuLieuChoTD.Click += new System.EventHandler(this.toolDuLieuChoTD_Click);
            // 
            // ucListDMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucListDMuc";
            this.Size = new System.Drawing.Size(993, 397);
            this.Load += new System.EventHandler(this.ucListUser_Load);
            this.Resize += new System.EventHandler(this.ucListDMuc_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grdDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accorMenuleft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdDanhMuc;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDanhMuc;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.Navigation.AccordionControl accorMenuleft;
        public DevExpress.XtraEditors.LabelControl NONNlab_Link;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDuLieuChoTD;
    }
}
