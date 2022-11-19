namespace Vs.Payroll
{
    partial class ucMaHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMaHang));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDonVi = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboDonVi = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.cboLHH = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cboKhachHang = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.chkDaDong = new DevExpress.XtraEditors.CheckEdit();
            this.datTNgay = new DevExpress.XtraEditors.DateEdit();
            this.datDNgay = new DevExpress.XtraEditors.DateEdit();
            this.ItemForKH = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForLHH = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSumNhanVien = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblDaHoanThanh = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLHH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhachHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDaDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLHH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDaHoanThanh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.btnALL.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdData.Location = new System.Drawing.Point(12, 91);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(977, 409);
            this.grdData.TabIndex = 3;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.DetailHeight = 297;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.AllowHtmlDrawHeaders = true;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvData_PopupMenuShowing);
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            this.grvData.RowCountChanged += new System.EventHandler(this.grvData_RowCountChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.ItemForDonVi,
            this.ItemForKH,
            this.ItemForLHH,
            this.ItemForSumNhanVien,
            this.lblDaHoanThanh,
            this.ItemForTNgay,
            this.ItemForDNgay});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1001, 512);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(981, 413);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForDonVi
            // 
            this.ItemForDonVi.Control = this.cboDonVi;
            this.ItemForDonVi.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForDonVi.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForDonVi.Location = new System.Drawing.Point(0, 0);
            this.ItemForDonVi.Name = "ItemForDonVi";
            this.ItemForDonVi.Size = new System.Drawing.Size(327, 30);
            this.ItemForDonVi.Text = "Đơn vị";
            this.ItemForDonVi.TextSize = new System.Drawing.Size(96, 17);
            // 
            // cboDonVi
            // 
            this.cboDonVi.EditValue = "\\";
            this.cboDonVi.Location = new System.Drawing.Point(112, 12);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDonVi.Properties.NullText = "";
            this.cboDonVi.Size = new System.Drawing.Size(223, 24);
            this.cboDonVi.StyleController = this.layoutControl;
            this.cboDonVi.TabIndex = 0;
            this.cboDonVi.Visible = false;
            this.cboDonVi.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.grdData);
            this.layoutControl.Controls.Add(this.cboLHH);
            this.layoutControl.Controls.Add(this.cboDonVi);
            this.layoutControl.Controls.Add(this.cboKhachHang);
            this.layoutControl.Controls.Add(this.chkDaDong);
            this.layoutControl.Controls.Add(this.datTNgay);
            this.layoutControl.Controls.Add(this.datDNgay);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(367, 0, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1001, 512);
            this.layoutControl.TabIndex = 3;
            this.layoutControl.Text = "layoutControl1";
            // 
            // cboLHH
            // 
            this.cboLHH.EditValue = "\\";
            this.cboLHH.Location = new System.Drawing.Point(766, 12);
            this.cboLHH.Name = "cboLHH";
            this.cboLHH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLHH.Properties.NullText = "";
            this.cboLHH.Size = new System.Drawing.Size(223, 24);
            this.cboLHH.StyleController = this.layoutControl;
            this.cboLHH.TabIndex = 0;
            this.cboLHH.Visible = false;
            this.cboLHH.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.EditValue = "\\";
            this.cboKhachHang.Location = new System.Drawing.Point(438, 13);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboKhachHang.Properties.NullText = "";
            this.cboKhachHang.Size = new System.Drawing.Size(225, 24);
            this.cboKhachHang.StyleController = this.layoutControl;
            this.cboKhachHang.TabIndex = 0;
            this.cboKhachHang.Visible = false;
            this.cboKhachHang.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // chkDaDong
            // 
            this.chkDaDong.Location = new System.Drawing.Point(112, 42);
            this.chkDaDong.Name = "chkDaDong";
            this.chkDaDong.Properties.Caption = "";
            this.chkDaDong.Properties.DisplayValueChecked = "1";
            this.chkDaDong.Properties.DisplayValueGrayed = "0";
            this.chkDaDong.Properties.DisplayValueUnchecked = "0";
            this.chkDaDong.Size = new System.Drawing.Size(223, 19);
            this.chkDaDong.StyleController = this.layoutControl;
            this.chkDaDong.TabIndex = 4;
            this.chkDaDong.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // datTNgay
            // 
            this.datTNgay.EditValue = null;
            this.datTNgay.Location = new System.Drawing.Point(439, 42);
            this.datTNgay.Name = "datTNgay";
            this.datTNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTNgay.Properties.CalendarTimeProperties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.datTNgay.Size = new System.Drawing.Size(223, 24);
            this.datTNgay.StyleController = this.layoutControl;
            this.datTNgay.TabIndex = 5;
            this.datTNgay.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // datDNgay
            // 
            this.datDNgay.EditValue = null;
            this.datDNgay.Location = new System.Drawing.Point(766, 42);
            this.datDNgay.Name = "datDNgay";
            this.datDNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Size = new System.Drawing.Size(223, 24);
            this.datDNgay.StyleController = this.layoutControl;
            this.datDNgay.TabIndex = 6;
            this.datDNgay.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // ItemForKH
            // 
            this.ItemForKH.Control = this.cboKhachHang;
            this.ItemForKH.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForKH.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForKH.Location = new System.Drawing.Point(327, 0);
            this.ItemForKH.Name = "ItemForKH";
            this.ItemForKH.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 3, 3);
            this.ItemForKH.Size = new System.Drawing.Size(327, 30);
            this.ItemForKH.Text = "Khách hàng";
            this.ItemForKH.TextSize = new System.Drawing.Size(96, 17);
            // 
            // ItemForLHH
            // 
            this.ItemForLHH.Control = this.cboLHH;
            this.ItemForLHH.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForLHH.Location = new System.Drawing.Point(654, 0);
            this.ItemForLHH.Name = "ItemForLHH";
            this.ItemForLHH.Size = new System.Drawing.Size(327, 30);
            this.ItemForLHH.Text = "Loại hàng hoá";
            this.ItemForLHH.TextSize = new System.Drawing.Size(96, 17);
            // 
            // ItemForSumNhanVien
            // 
            this.ItemForSumNhanVien.AllowHotTrack = false;
            this.ItemForSumNhanVien.AppearanceItemCaption.Options.UseTextOptions = true;
            this.ItemForSumNhanVien.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ItemForSumNhanVien.Location = new System.Drawing.Point(0, 58);
            this.ItemForSumNhanVien.Name = "ItemForSumNhanVien";
            this.ItemForSumNhanVien.Size = new System.Drawing.Size(981, 21);
            this.ItemForSumNhanVien.Text = "SumNhanVien";
            this.ItemForSumNhanVien.TextSize = new System.Drawing.Size(96, 17);
            // 
            // lblDaHoanThanh
            // 
            this.lblDaHoanThanh.Control = this.chkDaDong;
            this.lblDaHoanThanh.Location = new System.Drawing.Point(0, 30);
            this.lblDaHoanThanh.Name = "lblDaHoanThanh";
            this.lblDaHoanThanh.Size = new System.Drawing.Size(327, 28);
            this.lblDaHoanThanh.TextSize = new System.Drawing.Size(96, 17);
            // 
            // ItemForTNgay
            // 
            this.ItemForTNgay.Control = this.datTNgay;
            this.ItemForTNgay.Location = new System.Drawing.Point(327, 30);
            this.ItemForTNgay.Name = "ItemForTNgay";
            this.ItemForTNgay.Size = new System.Drawing.Size(327, 28);
            this.ItemForTNgay.Text = "Từ ngày";
            this.ItemForTNgay.TextSize = new System.Drawing.Size(96, 17);
            // 
            // ItemForDNgay
            // 
            this.ItemForDNgay.Control = this.datDNgay;
            this.ItemForDNgay.Location = new System.Drawing.Point(654, 30);
            this.ItemForDNgay.Name = "ItemForDNgay";
            this.ItemForDNgay.Size = new System.Drawing.Size(327, 28);
            this.ItemForDNgay.Text = "Đến ngày";
            this.ItemForDNgay.TextSize = new System.Drawing.Size(96, 17);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 227;
            this.searchLookUpEdit1View.FixedLineWidth = 1;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.layoutControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 512);
            this.panel1.TabIndex = 7;
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdData;
            this.searchControl.Location = new System.Drawing.Point(15, 7);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdData;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(192, 24);
            this.searchControl.TabIndex = 11;
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
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions3.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions4.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions5.ImageUri.Uri = "SaveAll";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "them", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "sua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "ghi", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.searchControl);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 512);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnALL.Size = new System.Drawing.Size(1001, 34);
            this.btnALL.TabIndex = 17;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // ucMaHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnALL);
            this.Name = "ucMaHang";
            this.Size = new System.Drawing.Size(1001, 546);
            this.Load += new System.EventHandler(this.ucMaHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboLHH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhachHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDaDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForLHH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDaHoanThanh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.btnALL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SearchLookUpEdit cboLHH;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForLHH;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDonVi;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDonVi;
        private DevExpress.XtraEditors.SearchLookUpEdit cboKhachHang;
        private DevExpress.XtraLayout.LayoutControlItem ItemForKH;
        private DevExpress.XtraLayout.SimpleLabelItem ItemForSumNhanVien;
        private DevExpress.XtraEditors.CheckEdit chkDaDong;
        private DevExpress.XtraEditors.DateEdit datTNgay;
        private DevExpress.XtraEditors.DateEdit datDNgay;
        private DevExpress.XtraLayout.LayoutControlItem lblDaHoanThanh;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTNgay;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDNgay;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
    }
}