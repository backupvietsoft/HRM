namespace Vs.Payroll
{
    partial class ucBangLuongThang13
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBangLuongThang13));
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
            this.cboTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cboXiNghiep = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.datNam = new DevExpress.XtraEditors.DateEdit();
            this.ItemForxn = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForNam = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSumNhanVien = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXiNghiep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNam.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.grdData.Location = new System.Drawing.Point(3, 121);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1320, 474);
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
            this.grvData.DetailHeight = 489;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.AllowHtmlDrawHeaders = true;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvData_PopupMenuShowing);
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            this.grvData.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvData_InvalidRowException);
            this.grvData.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.grvData_MouseWheel);
            this.grvData.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvData_ValidatingEditor);
            this.grvData.RowCountChanged += new System.EventHandler(this.grvData_RowCountChanged);
            this.grvData.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvData_InvalidValueException);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.ItemForDonVi,
            this.ItemForxn,
            this.ItemForTo,
            this.ItemForNam,
            this.ItemForSumNhanVien,
            this.emptySpaceItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1326, 703);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 118);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1326, 480);
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
            this.ItemForDonVi.Size = new System.Drawing.Size(442, 44);
            this.ItemForDonVi.Text = "Đơn vị";
            this.ItemForDonVi.TextSize = new System.Drawing.Size(124, 28);
            // 
            // cboDonVi
            // 
            this.cboDonVi.EditValue = "\\";
            this.cboDonVi.Location = new System.Drawing.Point(133, 3);
            this.cboDonVi.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDonVi.Size = new System.Drawing.Size(306, 34);
            this.cboDonVi.StyleController = this.layoutControl;
            this.cboDonVi.TabIndex = 0;
            this.cboDonVi.Visible = false;
            this.cboDonVi.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.btnALL);
            this.layoutControl.Controls.Add(this.grdData);
            this.layoutControl.Controls.Add(this.cboTo);
            this.layoutControl.Controls.Add(this.cboDonVi);
            this.layoutControl.Controls.Add(this.cboXiNghiep);
            this.layoutControl.Controls.Add(this.datNam);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(935, 264, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1326, 703);
            this.layoutControl.TabIndex = 3;
            this.layoutControl.Text = "layoutControl1";
            // 
            // cboTo
            // 
            this.cboTo.EditValue = "\\";
            this.cboTo.Location = new System.Drawing.Point(1016, 3);
            this.cboTo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cboTo.Name = "cboTo";
            this.cboTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTo.Size = new System.Drawing.Size(307, 34);
            this.cboTo.StyleController = this.layoutControl;
            this.cboTo.TabIndex = 0;
            this.cboTo.Visible = false;
            this.cboTo.EditValueChanged += new System.EventHandler(this.cboTo_EditValueChanged);
            // 
            // cboXiNghiep
            // 
            this.cboXiNghiep.EditValue = "\\";
            this.cboXiNghiep.Location = new System.Drawing.Point(574, 5);
            this.cboXiNghiep.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cboXiNghiep.Name = "cboXiNghiep";
            this.cboXiNghiep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboXiNghiep.Size = new System.Drawing.Size(307, 34);
            this.cboXiNghiep.StyleController = this.layoutControl;
            this.cboXiNghiep.TabIndex = 0;
            this.cboXiNghiep.Visible = false;
            this.cboXiNghiep.EditValueChanged += new System.EventHandler(this.cboXiNghiep_EditValueChanged);
            // 
            // datNam
            // 
            this.datNam.EditValue = null;
            this.datNam.Location = new System.Drawing.Point(133, 47);
            this.datNam.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.datNam.Name = "datNam";
            this.datNam.Properties.Appearance.Options.UseTextOptions = true;
            this.datNam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.datNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datNam.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datNam.Properties.DisplayFormat.FormatString = "yyyy";
            this.datNam.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datNam.Properties.EditFormat.FormatString = "yyyy";
            this.datNam.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datNam.Properties.Mask.EditMask = "yyyy";
            this.datNam.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datNam.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.datNam.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.datNam.Size = new System.Drawing.Size(306, 34);
            this.datNam.StyleController = this.layoutControl;
            this.datNam.TabIndex = 18;
            this.datNam.EditValueChanged += new System.EventHandler(this.cboThang_EditValueChanged);
            // 
            // ItemForxn
            // 
            this.ItemForxn.Control = this.cboXiNghiep;
            this.ItemForxn.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForxn.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForxn.Location = new System.Drawing.Point(442, 0);
            this.ItemForxn.Name = "ItemForxn";
            this.ItemForxn.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 5);
            this.ItemForxn.Size = new System.Drawing.Size(441, 44);
            this.ItemForxn.Text = "Xi nghiep";
            this.ItemForxn.TextSize = new System.Drawing.Size(124, 28);
            // 
            // ItemForTo
            // 
            this.ItemForTo.Control = this.cboTo;
            this.ItemForTo.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForTo.Location = new System.Drawing.Point(883, 0);
            this.ItemForTo.Name = "ItemForTo";
            this.ItemForTo.Size = new System.Drawing.Size(443, 44);
            this.ItemForTo.Text = "Tổ";
            this.ItemForTo.TextSize = new System.Drawing.Size(124, 28);
            // 
            // ItemForNam
            // 
            this.ItemForNam.Control = this.datNam;
            this.ItemForNam.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForNam.CustomizationFormText = "NGAY";
            this.ItemForNam.Location = new System.Drawing.Point(0, 44);
            this.ItemForNam.Name = "ItemForNam";
            this.ItemForNam.Size = new System.Drawing.Size(442, 40);
            this.ItemForNam.Text = "Năm";
            this.ItemForNam.TextSize = new System.Drawing.Size(124, 28);
            // 
            // ItemForSumNhanVien
            // 
            this.ItemForSumNhanVien.AllowHotTrack = false;
            this.ItemForSumNhanVien.AppearanceItemCaption.Options.UseTextOptions = true;
            this.ItemForSumNhanVien.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ItemForSumNhanVien.Location = new System.Drawing.Point(0, 84);
            this.ItemForSumNhanVien.Name = "ItemForSumNhanVien";
            this.ItemForSumNhanVien.Size = new System.Drawing.Size(1326, 34);
            this.ItemForSumNhanVien.Text = "SumNhanVien";
            this.ItemForSumNhanVien.TextSize = new System.Drawing.Size(124, 28);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(442, 44);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(884, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1326, 703);
            this.panel1.TabIndex = 7;
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "ExportToXLS";
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            windowsUIButtonImageOptions3.ImageUri.Uri = "spreadsheet/financial";
            windowsUIButtonImageOptions4.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions5.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("export", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "export", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("import", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "import", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "tinhluong", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnALL.Location = new System.Drawing.Point(3, 601);
            this.btnALL.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnALL.MaximumSize = new System.Drawing.Size(0, 99);
            this.btnALL.MinimumSize = new System.Drawing.Size(0, 99);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(6, 13, 6, 7);
            this.btnALL.Size = new System.Drawing.Size(1320, 99);
            this.btnALL.TabIndex = 19;
            this.btnALL.Text = "windowsUIButtonPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl1.Client = this.grdData;
            this.searchControl1.Location = new System.Drawing.Point(7, 31);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.grdData;
            this.searchControl1.Properties.FindDelay = 100;
            this.searchControl1.Size = new System.Drawing.Size(302, 34);
            this.searchControl1.TabIndex = 11;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnALL;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 598);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1326, 105);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucBangLuongThang13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "ucBangLuongThang13";
            this.Size = new System.Drawing.Size(1326, 703);
            this.Load += new System.EventHandler(this.ucBangLuongThang13_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXiNghiep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNam.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SearchLookUpEdit cboTo;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNam;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDonVi;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDonVi;
        private DevExpress.XtraEditors.SearchLookUpEdit cboXiNghiep;
        private DevExpress.XtraLayout.LayoutControlItem ItemForxn;
        private DevExpress.XtraEditors.DateEdit datNam;
        private DevExpress.XtraLayout.SimpleLabelItem ItemForSumNhanVien;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}