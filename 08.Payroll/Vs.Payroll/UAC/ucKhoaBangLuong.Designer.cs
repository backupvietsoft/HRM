﻿namespace Vs.Payroll
{
    partial class ucKhoaBangLuong
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForDonVi = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboDonVi = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.popListNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.grdThang = new DevExpress.XtraGrid.GridControl();
            this.grvThang1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.calThang = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.cboThang = new Commons.MPopupContainerEdit();
            this.ItemForThang = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).BeginInit();
            this.popListNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).BeginInit();
            this.popNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdData.Location = new System.Drawing.Point(12, 55);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(977, 445);
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
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.ItemForDonVi,
            this.emptySpaceItem2,
            this.ItemForThang});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1001, 512);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(981, 449);
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
            this.ItemForDonVi.Size = new System.Drawing.Size(490, 28);
            this.ItemForDonVi.Text = "Đơn vị";
            this.ItemForDonVi.TextSize = new System.Drawing.Size(37, 17);
            // 
            // cboDonVi
            // 
            this.cboDonVi.EditValue = "\\";
            this.cboDonVi.Location = new System.Drawing.Point(53, 12);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDonVi.Properties.NullText = "";
            this.cboDonVi.Size = new System.Drawing.Size(445, 24);
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
            this.layoutControl.Controls.Add(this.cboThang);
            this.layoutControl.Controls.Add(this.cboDonVi);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(367, 0, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1001, 512);
            this.layoutControl.TabIndex = 3;
            this.layoutControl.Text = "layoutControl1";
            // 
            // popListNgay
            // 
            this.popListNgay.AutoSize = true;
            this.popListNgay.Controls.Add(this.grdThang);
            this.popListNgay.Location = new System.Drawing.Point(181, 80);
            this.popListNgay.MinimumSize = new System.Drawing.Size(350, 170);
            this.popListNgay.Name = "popListNgay";
            this.popListNgay.Size = new System.Drawing.Size(350, 170);
            this.popListNgay.TabIndex = 23;
            // 
            // grdThang
            // 
            this.grdThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThang.Location = new System.Drawing.Point(0, 0);
            this.grdThang.MainView = this.grvThang1;
            this.grdThang.Name = "grdThang";
            this.grdThang.Size = new System.Drawing.Size(350, 170);
            this.grdThang.TabIndex = 15;
            this.grdThang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThang1});
            // 
            // grvThang1
            // 
            this.grvThang1.DetailHeight = 297;
            this.grvThang1.GridControl = this.grdThang;
            this.grvThang1.Name = "grvThang1";
            this.grvThang1.OptionsView.ShowAutoFilterRow = true;
            this.grvThang1.OptionsView.ShowGroupPanel = false;
            this.grvThang1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvNgay_RowCellClick);
            // 
            // popNgay
            // 
            this.popNgay.AutoSize = true;
            this.popNgay.Controls.Add(this.calThang);
            this.popNgay.Location = new System.Drawing.Point(114, 146);
            this.popNgay.MinimumSize = new System.Drawing.Size(315, 280);
            this.popNgay.Name = "popNgay";
            this.popNgay.Size = new System.Drawing.Size(315, 280);
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
            this.calThang.Size = new System.Drawing.Size(315, 280);
            this.calThang.TabIndex = 2;
            this.calThang.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.calThang.DateTimeCommit += new System.EventHandler(this.calThang_DateTimeCommit);
            // 
            // cboThang
            // 
            this.cboThang.Location = new System.Drawing.Point(543, 12);
            this.cboThang.Name = "cboThang";
            this.cboThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboThang.Properties.DefaultActionButtonIndex = 0;
            this.cboThang.Properties.DefaultPopupControl = this.popListNgay;
            this.cboThang.Properties.DifferentActionButtonIndex = 1;
            this.cboThang.Properties.DifferentPopupControl = this.popNgay;
            this.cboThang.Size = new System.Drawing.Size(446, 24);
            this.cboThang.StyleController = this.layoutControl;
            this.cboThang.TabIndex = 18;
            this.cboThang.EditValueChanged += new System.EventHandler(this.cboNgay_EditValueChanged);
            // 
            // ItemForThang
            // 
            this.ItemForThang.Control = this.cboThang;
            this.ItemForThang.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForThang.CustomizationFormText = "NGAY";
            this.ItemForThang.Location = new System.Drawing.Point(490, 0);
            this.ItemForThang.Name = "ItemForThang";
            this.ItemForThang.Size = new System.Drawing.Size(491, 28);
            this.ItemForThang.Text = "Tháng";
            this.ItemForThang.TextSize = new System.Drawing.Size(37, 17);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 28);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 15);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 15);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(981, 15);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
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
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.layoutControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 512);
            this.panel1.TabIndex = 7;
            // 
            // ucKhoaBangLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnALL);
            this.Name = "ucKhoaBangLuong";
            this.Size = new System.Drawing.Size(1001, 546);
            this.Load += new System.EventHandler(this.ucKhoaBangLuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDonVi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            this.layoutControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).EndInit();
            this.popListNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).EndInit();
            this.popNgay.ResumeLayout(false);
            this.popNgay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PopupContainerControl popNgay;
        private DevExpress.XtraEditors.PopupContainerControl popListNgay;
        private DevExpress.XtraGrid.GridControl grdThang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThang1;
        private Commons.MPopupContainerEdit cboThang;
        private DevExpress.XtraLayout.LayoutControlItem ItemForThang;
        private DevExpress.XtraEditors.Controls.CalendarControl calThang;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDonVi;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDonVi;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}