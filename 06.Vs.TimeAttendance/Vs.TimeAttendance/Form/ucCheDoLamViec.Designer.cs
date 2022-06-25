namespace Vs.TimeAttendance
{
    partial class ucCheDoLamViec
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
            this.ItemForNGAY = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboNgay = new Commons.MPopupContainerEdit();
            this.popListNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.grdNgay = new DevExpress.XtraGrid.GridControl();
            this.grvNgay = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.calNgay = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.cboNhomChamCong = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ItemForNHOM_CHAM_CONG = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).BeginInit();
            this.popListNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).BeginInit();
            this.popNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calNgay.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomChamCong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNHOM_CHAM_CONG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Location = new System.Drawing.Point(5, 44);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(703, 308);
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
            this.ItemForNGAY,
            this.ItemForNHOM_CHAM_CONG,
            this.emptySpaceItem1,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(713, 357);
            this.Root.TextVisible = false;
            // 
            // ItemForNGAY
            // 
            this.ItemForNGAY.Control = this.cboNgay;
            this.ItemForNGAY.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForNGAY.CustomizationFormText = "NGAY";
            this.ItemForNGAY.Location = new System.Drawing.Point(352, 0);
            this.ItemForNGAY.Name = "ItemForNGAY";
            this.ItemForNGAY.Size = new System.Drawing.Size(353, 26);
            this.ItemForNGAY.Text = "NGAY";
            this.ItemForNGAY.TextSize = new System.Drawing.Size(125, 17);
            // 
            // cboNgay
            // 
            this.cboNgay.Location = new System.Drawing.Point(485, 5);
            this.cboNgay.Name = "cboNgay";
            this.cboNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboNgay.Properties.DefaultActionButtonIndex = 0;
            this.cboNgay.Properties.DefaultPopupControl = this.popListNgay;
            this.cboNgay.Properties.DifferentActionButtonIndex = 1;
            this.cboNgay.Properties.DifferentPopupControl = this.popNgay;
            this.cboNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.cboNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.cboNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.cboNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.cboNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.cboNgay.Size = new System.Drawing.Size(223, 24);
            this.cboNgay.StyleController = this.layoutControl;
            this.cboNgay.TabIndex = 18;
            this.cboNgay.EditValueChanged += new System.EventHandler(this.cboNgay_EditValueChanged);
            // 
            // popListNgay
            // 
            this.popListNgay.AutoSize = true;
            this.popListNgay.Controls.Add(this.grdNgay);
            this.popListNgay.Location = new System.Drawing.Point(54, 58);
            this.popListNgay.MinimumSize = new System.Drawing.Size(350, 170);
            this.popListNgay.Name = "popListNgay";
            this.popListNgay.Size = new System.Drawing.Size(350, 170);
            this.popListNgay.TabIndex = 23;
            // 
            // grdNgay
            // 
            this.grdNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNgay.Location = new System.Drawing.Point(0, 0);
            this.grdNgay.MainView = this.grvNgay;
            this.grdNgay.Name = "grdNgay";
            this.grdNgay.Size = new System.Drawing.Size(350, 170);
            this.grdNgay.TabIndex = 15;
            this.grdNgay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNgay});
            // 
            // grvNgay
            // 
            this.grvNgay.DetailHeight = 297;
            this.grvNgay.GridControl = this.grdNgay;
            this.grvNgay.Name = "grvNgay";
            this.grvNgay.OptionsView.ShowAutoFilterRow = true;
            this.grvNgay.OptionsView.ShowGroupPanel = false;
            this.grvNgay.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvNgay_RowCellClick);
            // 
            // popNgay
            // 
            this.popNgay.AutoSize = true;
            this.popNgay.Controls.Add(this.calNgay);
            this.popNgay.Location = new System.Drawing.Point(16, 108);
            this.popNgay.MinimumSize = new System.Drawing.Size(315, 280);
            this.popNgay.Name = "popNgay";
            this.popNgay.Size = new System.Drawing.Size(315, 280);
            this.popNgay.TabIndex = 22;
            // 
            // calNgay
            // 
            this.calNgay.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calNgay.Location = new System.Drawing.Point(0, 0);
            this.calNgay.Name = "calNgay";
            this.calNgay.Padding = new System.Windows.Forms.Padding(0);
            this.calNgay.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Multiple;
            this.calNgay.ShowClearButton = true;
            this.calNgay.Size = new System.Drawing.Size(315, 280);
            this.calNgay.TabIndex = 1;
            this.calNgay.DateTimeCommit += new System.EventHandler(this.calNgay_DateTimeCommit);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.popListNgay);
            this.layoutControl.Controls.Add(this.popNgay);
            this.layoutControl.Controls.Add(this.grdData);
            this.layoutControl.Controls.Add(this.cboNhomChamCong);
            this.layoutControl.Controls.Add(this.cboNgay);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(367, 0, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(713, 357);
            this.layoutControl.TabIndex = 3;
            this.layoutControl.Text = "layoutControl1";
            // 
            // cboNhomChamCong
            // 
            this.cboNhomChamCong.EditValue = "\\";
            this.cboNhomChamCong.Location = new System.Drawing.Point(133, 5);
            this.cboNhomChamCong.Name = "cboNhomChamCong";
            this.cboNhomChamCong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNhomChamCong.Properties.NullText = "";
            this.cboNhomChamCong.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboNhomChamCong.Size = new System.Drawing.Size(222, 24);
            this.cboNhomChamCong.StyleController = this.layoutControl;
            this.cboNhomChamCong.TabIndex = 0;
            this.cboNhomChamCong.Visible = false;
            this.cboNhomChamCong.EditValueChanged += new System.EventHandler(this.cboNhomChamCong_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 297;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ItemForNHOM_CHAM_CONG
            // 
            this.ItemForNHOM_CHAM_CONG.Control = this.cboNhomChamCong;
            this.ItemForNHOM_CHAM_CONG.CustomizationFormText = "ItemForNHOM_CHAM_CONG";
            this.ItemForNHOM_CHAM_CONG.Location = new System.Drawing.Point(0, 0);
            this.ItemForNHOM_CHAM_CONG.Name = "ItemForNHOM_CHAM_CONG";
            this.ItemForNHOM_CHAM_CONG.Size = new System.Drawing.Size(352, 26);
            this.ItemForNHOM_CHAM_CONG.Text = "NHOM_CHAM_CONG";
            this.ItemForNHOM_CHAM_CONG.TextSize = new System.Drawing.Size(125, 17);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 13);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(9, 13);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(705, 13);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(705, 310);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 357);
            this.btnALL.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnALL.Size = new System.Drawing.Size(713, 34);
            this.btnALL.TabIndex = 17;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel1_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.grdData;
            this.searchControl1.Location = new System.Drawing.Point(4, 5);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchControl1.MaximumSize = new System.Drawing.Size(0, 25);
            this.searchControl1.MinimumSize = new System.Drawing.Size(0, 25);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.grdData;
            this.searchControl1.Size = new System.Drawing.Size(0, 24);
            this.searchControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.layoutControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 357);
            this.panel1.TabIndex = 7;
            // 
            // ucCheDoLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnALL);
            this.Name = "ucCheDoLamViec";
            this.Size = new System.Drawing.Size(713, 391);
            this.Load += new System.EventHandler(this.ucCheDoLamViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).EndInit();
            this.popListNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).EndInit();
            this.popNgay.ResumeLayout(false);
            this.popNgay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calNgay.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            this.layoutControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomChamCong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNHOM_CHAM_CONG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SearchLookUpEdit cboNhomChamCong;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNHOM_CHAM_CONG;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PopupContainerControl popNgay;
        private DevExpress.XtraEditors.Controls.CalendarControl calNgay;
        private DevExpress.XtraEditors.PopupContainerControl popListNgay;
        private DevExpress.XtraGrid.GridControl grdNgay;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNgay;
        private Commons.MPopupContainerEdit cboNgay;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNGAY;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}