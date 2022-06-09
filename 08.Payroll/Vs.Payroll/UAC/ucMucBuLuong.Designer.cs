namespace Vs.Payroll
{
    partial class ucMucBuLuong
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForNHOM_CHAM_CONG = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboDonVi = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.popListNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.grdThang = new DevExpress.XtraGrid.GridControl();
            this.grvThang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.calThang = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.cboNgay = new Commons.MPopupContainerEdit();
            this.ItemForNGAY = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNHOM_CHAM_CONG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).BeginInit();
            this.popListNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).BeginInit();
            this.popNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdData.Location = new System.Drawing.Point(6, 34);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(993, 490);
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
            this.grvData.DetailHeight = 349;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.AllowHtmlDrawHeaders = true;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvData_InitNewRow);
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            this.grvData.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvData_InvalidRowException);
            this.grvData.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvData_ValidatingEditor);
            this.grvData.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvData_InvalidValueException);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.ItemForNHOM_CHAM_CONG,
            this.ItemForNGAY,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1005, 530);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(995, 492);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForNHOM_CHAM_CONG
            // 
            this.ItemForNHOM_CHAM_CONG.Control = this.cboDonVi;
            this.ItemForNHOM_CHAM_CONG.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForNHOM_CHAM_CONG.Location = new System.Drawing.Point(0, 0);
            this.ItemForNHOM_CHAM_CONG.Name = "ItemForNHOM_CHAM_CONG";
            this.ItemForNHOM_CHAM_CONG.Size = new System.Drawing.Size(497, 28);
            this.ItemForNHOM_CHAM_CONG.Text = "Don_Vi";
            this.ItemForNHOM_CHAM_CONG.TextSize = new System.Drawing.Size(47, 20);
            // 
            // cboDonVi
            // 
            this.cboDonVi.EditValue = "\\";
            this.cboDonVi.Location = new System.Drawing.Point(56, 6);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDonVi.Size = new System.Drawing.Size(445, 26);
            this.cboDonVi.StyleController = this.layoutControl;
            this.cboDonVi.TabIndex = 0;
            this.cboDonVi.Visible = false;
            this.cboDonVi.EditValueChanged += new System.EventHandler(this.cboDonVi_EditValueChanged);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.popListNgay);
            this.layoutControl.Controls.Add(this.popNgay);
            this.layoutControl.Controls.Add(this.grdData);
            this.layoutControl.Controls.Add(this.cboDonVi);
            this.layoutControl.Controls.Add(this.cboNgay);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(274, 288, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1005, 530);
            this.layoutControl.TabIndex = 3;
            this.layoutControl.Text = "layoutControl1";
            // 
            // popListNgay
            // 
            this.popListNgay.AutoSize = true;
            this.popListNgay.Controls.Add(this.grdThang);
            this.popListNgay.Location = new System.Drawing.Point(207, 94);
            this.popListNgay.MinimumSize = new System.Drawing.Size(400, 200);
            this.popListNgay.Name = "popListNgay";
            this.popListNgay.Size = new System.Drawing.Size(400, 200);
            this.popListNgay.TabIndex = 23;
            // 
            // grdThang
            // 
            this.grdThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThang.Location = new System.Drawing.Point(0, 0);
            this.grdThang.MainView = this.grvThang;
            this.grdThang.Name = "grdThang";
            this.grdThang.Size = new System.Drawing.Size(400, 200);
            this.grdThang.TabIndex = 15;
            this.grdThang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThang});
            // 
            // grvThang
            // 
            this.grvThang.DetailHeight = 349;
            this.grvThang.GridControl = this.grdThang;
            this.grvThang.Name = "grvThang";
            this.grvThang.OptionsView.ShowAutoFilterRow = true;
            this.grvThang.OptionsView.ShowGroupPanel = false;
            this.grvThang.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvNgay_RowCellClick);
            // 
            // popNgay
            // 
            this.popNgay.AutoSize = true;
            this.popNgay.Controls.Add(this.calThang);
            this.popNgay.Location = new System.Drawing.Point(19, 128);
            this.popNgay.MinimumSize = new System.Drawing.Size(360, 329);
            this.popNgay.Name = "popNgay";
            this.popNgay.Size = new System.Drawing.Size(360, 329);
            this.popNgay.TabIndex = 22;
            // 
            // calThang
            // 
            this.calThang.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calThang.Location = new System.Drawing.Point(0, 0);
            this.calThang.Name = "calThang";
            this.calThang.Padding = new System.Windows.Forms.Padding(0);
            this.calThang.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Multiple;
            this.calThang.ShowClearButton = true;
            this.calThang.Size = new System.Drawing.Size(360, 329);
            this.calThang.TabIndex = 2;
            this.calThang.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.calThang.DateTimeCommit += new System.EventHandler(this.calThang_DateTimeCommit);
            // 
            // cboNgay
            // 
            this.cboNgay.Location = new System.Drawing.Point(553, 6);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboNgay.Properties.DefaultActionButtonIndex = 0;
            this.cboNgay.Properties.DefaultPopupControl = this.popListNgay;
            this.cboNgay.Properties.DifferentActionButtonIndex = 1;
            this.cboNgay.Properties.DifferentPopupControl = this.popNgay;
            this.cboNgay.Size = new System.Drawing.Size(201, 26);
            this.cboNgay.StyleController = this.layoutControl;
            this.cboNgay.TabIndex = 18;
            this.cboNgay.EditValueChanged += new System.EventHandler(this.cboNgay_EditValueChanged);
            // 
            // ItemForNGAY
            // 
            this.ItemForNGAY.Control = this.cboNgay;
            this.ItemForNGAY.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForNGAY.CustomizationFormText = "NGAY";
            this.ItemForNGAY.Location = new System.Drawing.Point(497, 0);
            this.ItemForNGAY.Name = "ItemForNGAY";
            this.ItemForNGAY.Size = new System.Drawing.Size(253, 28);
            this.ItemForNGAY.Text = "NGAY";
            this.ItemForNGAY.TextSize = new System.Drawing.Size(47, 20);
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions2.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions3.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions4.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions5.ImageUri.Uri = "SaveAndClose";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "themsua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "ghi", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongghi", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 530);
            this.btnALL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5);
            this.btnALL.Size = new System.Drawing.Size(1005, 62);
            this.btnALL.TabIndex = 17;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.layoutControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 530);
            this.panel1.TabIndex = 7;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(750, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(245, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucMucBuLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnALL);
            this.Name = "ucMucBuLuong";
            this.Size = new System.Drawing.Size(1005, 592);
            this.Load += new System.EventHandler(this.ucMucBuLuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNHOM_CHAM_CONG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            this.layoutControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).EndInit();
            this.popListNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).EndInit();
            this.popNgay.ResumeLayout(false);
            this.popNgay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDonVi;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNHOM_CHAM_CONG;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PopupContainerControl popNgay;
        private DevExpress.XtraEditors.PopupContainerControl popListNgay;
        private DevExpress.XtraGrid.GridControl grdThang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThang;
        private Commons.MPopupContainerEdit cboNgay;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNGAY;
        private DevExpress.XtraEditors.Controls.CalendarControl calThang;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}