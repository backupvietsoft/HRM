namespace Vs.Category
{
    partial class frmXetDuyet
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.txtTim = new DevExpress.XtraEditors.SearchControl();
            this.grdChung = new DevExpress.XtraGrid.GridControl();
            this.grvChung = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdDaDuyet = new DevExpress.XtraGrid.GridControl();
            this.grvDaDuyet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabChung = new DevExpress.XtraLayout.TabbedControlGroup();
            this.lcgXetDuyet = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgDaDuyet = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDaDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabChung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgXetDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDaDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.grdDaDuyet);
            this.dataLayoutControl1.Controls.Add(this.grdChung);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(973, 534);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // btnALL
            // 
            this.btnALL.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.btnALL.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.Gray;
            this.btnALL.AppearanceButton.Hovered.Options.UseFont = true;
            this.btnALL.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.btnALL.AppearanceButton.Normal.FontSizeDelta = -1;
            this.btnALL.AppearanceButton.Normal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnALL.AppearanceButton.Normal.Options.UseFont = true;
            this.btnALL.AppearanceButton.Normal.Options.UseForeColor = true;
            this.btnALL.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnALL.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.btnALL.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.btnALL.AppearanceButton.Pressed.Options.UseBorderColor = true;
            this.btnALL.AppearanceButton.Pressed.Options.UseFont = true;
            this.btnALL.AppearanceButton.Pressed.Options.UseImage = true;
            this.btnALL.AppearanceButton.Pressed.Options.UseTextOptions = true;
            windowsUIButtonImageOptions1.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.txtTim);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 534);
            this.btnALL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnALL.Size = new System.Drawing.Size(973, 32);
            this.btnALL.TabIndex = 36;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // txtTim
            // 
            this.txtTim.Client = this.grdChung;
            this.txtTim.Location = new System.Drawing.Point(13, 4);
            this.txtTim.Name = "txtTim";
            this.txtTim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.txtTim.Properties.Client = this.grdChung;
            this.txtTim.Size = new System.Drawing.Size(222, 24);
            this.txtTim.TabIndex = 13;
            // 
            // grdChung
            // 
            this.grdChung.Location = new System.Drawing.Point(12, 37);
            this.grdChung.MainView = this.grvChung;
            this.grdChung.Name = "grdChung";
            this.grdChung.Size = new System.Drawing.Size(949, 485);
            this.grdChung.TabIndex = 8;
            this.grdChung.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChung});
            // 
            // grvChung
            // 
            this.grvChung.DetailHeight = 861;
            this.grvChung.FixedLineWidth = 4;
            this.grvChung.GridControl = this.grdChung;
            this.grvChung.Name = "grvChung";
            this.grvChung.OptionsView.ShowGroupPanel = false;
            this.grvChung.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvChung_RowCellStyle);
            this.grvChung.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvChung_PopupMenuShowing);
            this.grvChung.DoubleClick += new System.EventHandler(this.grvChung_DoubleClick);
            // 
            // grdDaDuyet
            // 
            this.grdDaDuyet.Location = new System.Drawing.Point(12, 37);
            this.grdDaDuyet.MainView = this.grvDaDuyet;
            this.grdDaDuyet.Name = "grdDaDuyet";
            this.grdDaDuyet.Size = new System.Drawing.Size(949, 485);
            this.grdDaDuyet.TabIndex = 14;
            this.grdDaDuyet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDaDuyet});
            // 
            // grvDaDuyet
            // 
            this.grvDaDuyet.GridControl = this.grdDaDuyet;
            this.grvDaDuyet.Name = "grvDaDuyet";
            this.grvDaDuyet.OptionsView.ShowGroupPanel = false;
            this.grvDaDuyet.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvDaDuyet_RowCellStyle);
            this.grvDaDuyet.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvDaDuyet_PopupMenuShowing);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabChung});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(973, 534);
            this.Root.TextVisible = false;
            // 
            // tabChung
            // 
            this.tabChung.Location = new System.Drawing.Point(0, 0);
            this.tabChung.Name = "tabChung";
            this.tabChung.SelectedTabPage = this.lcgXetDuyet;
            this.tabChung.Size = new System.Drawing.Size(963, 524);
            this.tabChung.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgXetDuyet,
            this.lcgDaDuyet});
            this.tabChung.SelectedPageChanged += new DevExpress.XtraLayout.LayoutTabPageChangedEventHandler(this.tabChung_SelectedPageChanged);
            // 
            // lcgXetDuyet
            // 
            this.lcgXetDuyet.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.lcgXetDuyet.Location = new System.Drawing.Point(0, 0);
            this.lcgXetDuyet.Name = "lcgXetDuyet";
            this.lcgXetDuyet.Size = new System.Drawing.Size(951, 487);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.grdChung;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(951, 487);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // lcgDaDuyet
            // 
            this.lcgDaDuyet.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.lcgDaDuyet.Location = new System.Drawing.Point(0, 0);
            this.lcgDaDuyet.Name = "lcgDaDuyet";
            this.lcgDaDuyet.Size = new System.Drawing.Size(951, 487);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdDaDuyet;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(951, 487);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmXetDuyet
            // 
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmXetDuyet";
            this.Size = new System.Drawing.Size(973, 566);
            this.Load += new System.EventHandler(this.frmXetDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdChung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDaDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDaDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabChung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgXetDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDaDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdChung;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChung;
        private DevExpress.XtraEditors.SearchControl txtTim;
        private DevExpress.XtraGrid.GridControl grdDaDuyet;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDaDuyet;
        private DevExpress.XtraLayout.TabbedControlGroup tabChung;
        private DevExpress.XtraLayout.LayoutControlGroup lcgXetDuyet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDaDuyet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
    }
}