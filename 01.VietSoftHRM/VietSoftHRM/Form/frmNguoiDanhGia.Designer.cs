namespace VietSoftHRM
{
    partial class frmNguoiDanhGia
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNguoiDanhGia));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions7 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions8 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions9 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions10 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.grdNDG = new DevExpress.XtraGrid.GridControl();
            this.grvNGD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblNguoiDanhGia = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNDG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNGD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNguoiDanhGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.btnALL);
            this.dataLayoutControl1.Controls.Add(this.grdNDG);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(975, 559);
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
            windowsUIButtonImageOptions6.Image = ((System.Drawing.Image)(resources.GetObject("windowsUIButtonImageOptions6.Image")));
            windowsUIButtonImageOptions6.ImageUri.Uri = "Edit";
            windowsUIButtonImageOptions7.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions8.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions9.ImageUri.Uri = "SaveAndClose";
            windowsUIButtonImageOptions10.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "sua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions8, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions9, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions10, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Location = new System.Drawing.Point(12, 515);
            this.btnALL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnALL.Size = new System.Drawing.Size(951, 32);
            this.btnALL.TabIndex = 34;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl1.Location = new System.Drawing.Point(3, 5);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.FindDelay = 100;
            this.searchControl1.Size = new System.Drawing.Size(194, 24);
            this.searchControl1.StyleController = this.dataLayoutControl1;
            this.searchControl1.TabIndex = 56;
            // 
            // grdNDG
            // 
            this.grdNDG.Location = new System.Drawing.Point(12, 45);
            this.grdNDG.MainView = this.grvNGD;
            this.grdNDG.Name = "grdNDG";
            this.grdNDG.Size = new System.Drawing.Size(951, 466);
            this.grdNDG.TabIndex = 4;
            this.grdNDG.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNGD});
            this.grdNDG.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdViTri_ProcessGridKey);
            // 
            // grvNGD
            // 
            this.grvNGD.GridControl = this.grdNDG;
            this.grvNGD.Name = "grvNGD";
            this.grvNGD.OptionsSelection.MultiSelect = true;
            this.grvNGD.OptionsView.ShowGroupPanel = false;
            this.grvNGD.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvViTri_InitNewRow);
            this.grvNGD.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvViTri_InvalidRowException);
            this.grvNGD.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvViTri_ValidatingEditor);
            this.grvNGD.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvViTri_InvalidValueException);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblNguoiDanhGia,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(975, 559);
            this.Root.TextVisible = false;
            // 
            // lblNguoiDanhGia
            // 
            this.lblNguoiDanhGia.AllowHotTrack = false;
            this.lblNguoiDanhGia.AllowHtmlStringInCaption = true;
            this.lblNguoiDanhGia.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguoiDanhGia.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNguoiDanhGia.AppearanceItemCaption.Options.UseFont = true;
            this.lblNguoiDanhGia.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblNguoiDanhGia.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblNguoiDanhGia.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblNguoiDanhGia.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblNguoiDanhGia.Location = new System.Drawing.Point(0, 0);
            this.lblNguoiDanhGia.MaxSize = new System.Drawing.Size(0, 33);
            this.lblNguoiDanhGia.MinSize = new System.Drawing.Size(136, 33);
            this.lblNguoiDanhGia.Name = "lblNguoiDanhGia";
            this.lblNguoiDanhGia.Size = new System.Drawing.Size(955, 33);
            this.lblNguoiDanhGia.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNguoiDanhGia.TextSize = new System.Drawing.Size(157, 21);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnALL;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 503);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(955, 36);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdNDG;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(955, 470);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmNguoiDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 559);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "frmNguoiDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNguoiDanhGia";
            this.Load += new System.EventHandler(this.frmNguoiDanhGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNDG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNGD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNguoiDanhGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleLabelItem lblNguoiDanhGia;
        private DevExpress.XtraGrid.GridControl grdNDG;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNGD;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}