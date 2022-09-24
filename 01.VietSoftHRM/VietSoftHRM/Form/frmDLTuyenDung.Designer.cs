namespace VietSoftHRM
{
    partial class frmDLTuyenDung
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions17 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDLTuyenDung));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions18 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions19 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions20 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.grdThamGiaTD = new DevExpress.XtraGrid.GridControl();
            this.grvThamGiaTD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdViTri = new DevExpress.XtraGrid.GridControl();
            this.grvViTri = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabControl = new DevExpress.XtraLayout.TabbedControlGroup();
            this.tabVitri = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabThamGiaTD = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNhaMayBoPhan = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThamGiaTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThamGiaTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabVitri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabThamGiaTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNhaMayBoPhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.btnALL);
            this.dataLayoutControl1.Controls.Add(this.grdThamGiaTD);
            this.dataLayoutControl1.Controls.Add(this.grdViTri);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(565, 338);
            this.dataLayoutControl1.TabIndex = 7;
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
            windowsUIButtonImageOptions17.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions17.Image")));
            windowsUIButtonImageOptions17.ImageUri.Uri = "Edit";
            windowsUIButtonImageOptions18.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions19.ImageUri.Uri = "SaveAndClose";
            windowsUIButtonImageOptions20.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions17, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "sua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions18, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions19, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions20, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Location = new System.Drawing.Point(7, 299);
            this.btnALL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnALL.Size = new System.Drawing.Size(551, 32);
            this.btnALL.TabIndex = 34;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl1.Location = new System.Drawing.Point(3, -4);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.FindDelay = 100;
            this.searchControl1.Size = new System.Drawing.Size(194, 24);
            this.searchControl1.StyleController = this.dataLayoutControl1;
            this.searchControl1.TabIndex = 56;
            // 
            // grdThamGiaTD
            // 
            this.grdThamGiaTD.Location = new System.Drawing.Point(15, 72);
            this.grdThamGiaTD.MainView = this.grvThamGiaTD;
            this.grdThamGiaTD.Name = "grdThamGiaTD";
            this.grdThamGiaTD.Size = new System.Drawing.Size(535, 217);
            this.grdThamGiaTD.TabIndex = 5;
            this.grdThamGiaTD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThamGiaTD});
            this.grdThamGiaTD.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdThamGiaTD_ProcessGridKey);
            // 
            // grvThamGiaTD
            // 
            this.grvThamGiaTD.GridControl = this.grdThamGiaTD;
            this.grvThamGiaTD.Name = "grvThamGiaTD";
            this.grvThamGiaTD.OptionsView.ShowGroupPanel = false;
            this.grvThamGiaTD.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvThamGiaTD_InvalidRowException);
            this.grvThamGiaTD.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvThamGiaTD_ValidatingEditor);
            this.grvThamGiaTD.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvThamGiaTD_InvalidValueException);
            // 
            // grdViTri
            // 
            this.grdViTri.Location = new System.Drawing.Point(15, 72);
            this.grdViTri.MainView = this.grvViTri;
            this.grdViTri.Name = "grdViTri";
            this.grdViTri.Size = new System.Drawing.Size(535, 217);
            this.grdViTri.TabIndex = 4;
            this.grdViTri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvViTri});
            this.grdViTri.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdViTri_ProcessGridKey);
            // 
            // grvViTri
            // 
            this.grvViTri.GridControl = this.grdViTri;
            this.grvViTri.Name = "grvViTri";
            this.grvViTri.OptionsSelection.MultiSelect = true;
            this.grvViTri.OptionsView.ShowGroupPanel = false;
            this.grvViTri.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvViTri_InitNewRow);
            this.grvViTri.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvViTri_InvalidRowException);
            this.grvViTri.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvViTri_ValidatingEditor);
            this.grvViTri.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvViTri_InvalidValueException);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabControl,
            this.lblNhaMayBoPhan,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(565, 338);
            this.Root.TextVisible = false;
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(0, 33);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabThamGiaTD;
            this.tabControl.Size = new System.Drawing.Size(553, 259);
            this.tabControl.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabVitri,
            this.tabThamGiaTD});
            this.tabControl.SelectedPageChanged += new DevExpress.XtraLayout.LayoutTabPageChangedEventHandler(this.tabControl_SelectedPageChanged);
            // 
            // tabVitri
            // 
            this.tabVitri.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.tabVitri.Location = new System.Drawing.Point(0, 0);
            this.tabVitri.Name = "tabVitri";
            this.tabVitri.Size = new System.Drawing.Size(537, 219);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdViTri;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(537, 219);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tabThamGiaTD
            // 
            this.tabThamGiaTD.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.tabThamGiaTD.Location = new System.Drawing.Point(0, 0);
            this.tabThamGiaTD.Name = "tabThamGiaTD";
            this.tabThamGiaTD.Size = new System.Drawing.Size(537, 219);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdThamGiaTD;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(537, 219);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblNhaMayBoPhan
            // 
            this.lblNhaMayBoPhan.AllowHotTrack = false;
            this.lblNhaMayBoPhan.AllowHtmlStringInCaption = true;
            this.lblNhaMayBoPhan.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhaMayBoPhan.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNhaMayBoPhan.AppearanceItemCaption.Options.UseFont = true;
            this.lblNhaMayBoPhan.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblNhaMayBoPhan.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblNhaMayBoPhan.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblNhaMayBoPhan.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblNhaMayBoPhan.Location = new System.Drawing.Point(0, 0);
            this.lblNhaMayBoPhan.MaxSize = new System.Drawing.Size(0, 33);
            this.lblNhaMayBoPhan.MinSize = new System.Drawing.Size(136, 33);
            this.lblNhaMayBoPhan.Name = "lblNhaMayBoPhan";
            this.lblNhaMayBoPhan.Size = new System.Drawing.Size(553, 33);
            this.lblNhaMayBoPhan.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNhaMayBoPhan.TextSize = new System.Drawing.Size(166, 21);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnALL;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 292);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(553, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmDLTuyenDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 338);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "frmDLTuyenDung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDLTuyenDung";
            this.Load += new System.EventHandler(this.frmDLTuyenDung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThamGiaTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThamGiaTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvViTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabVitri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabThamGiaTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNhaMayBoPhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.TabbedControlGroup tabControl;
        private DevExpress.XtraLayout.LayoutControlGroup tabThamGiaTD;
        private DevExpress.XtraLayout.LayoutControlGroup tabVitri;
        private DevExpress.XtraLayout.SimpleLabelItem lblNhaMayBoPhan;
        private DevExpress.XtraGrid.GridControl grdThamGiaTD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThamGiaTD;
        private DevExpress.XtraGrid.GridControl grdViTri;
        private DevExpress.XtraGrid.Views.Grid.GridView grvViTri;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SearchControl searchControl1;
    }
}