﻿namespace Vs.TimeAttendance
{
    partial class frmTinhTrangBangCong
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.datThang = new DevExpress.XtraEditors.DateEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboDV = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForDON_VI = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblThang = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datThang.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblThang)).BeginInit();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.datThang);
            this.layoutControl1.Controls.Add(this.grdData);
            this.layoutControl1.Controls.Add(this.cboDV);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(950, 219, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(687, 371);
            this.layoutControl1.TabIndex = 16;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // datThang
            // 
            this.datThang.EditValue = null;
            this.datThang.Location = new System.Drawing.Point(406, 6);
            this.datThang.Name = "datThang";
            this.datThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datThang.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datThang.Properties.ReadOnly = true;
            this.datThang.Size = new System.Drawing.Size(275, 24);
            this.datThang.StyleController = this.layoutControl1;
            this.datThang.TabIndex = 10;
            // 
            // grdData
            // 
            this.grdData.Location = new System.Drawing.Point(6, 47);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(675, 318);
            this.grdData.TabIndex = 9;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.DetailHeight = 297;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvData_PopupMenuShowing);
            // 
            // cboDV
            // 
            this.cboDV.Location = new System.Drawing.Point(68, 6);
            this.cboDV.Name = "cboDV";
            this.cboDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDV.Properties.NullText = "";
            this.cboDV.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDV.Properties.ReadOnly = true;
            this.cboDV.Size = new System.Drawing.Size(274, 24);
            this.cboDV.StyleController = this.layoutControl1;
            this.cboDV.TabIndex = 7;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 297;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForDON_VI,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.lblThang});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(687, 371);
            this.Root.TextVisible = false;
            // 
            // ItemForDON_VI
            // 
            this.ItemForDON_VI.Control = this.cboDV;
            this.ItemForDON_VI.CustomizationFormText = "ItemForDON_VI";
            this.ItemForDON_VI.Location = new System.Drawing.Point(0, 0);
            this.ItemForDON_VI.Name = "ItemForDON_VI";
            this.ItemForDON_VI.Size = new System.Drawing.Size(338, 26);
            this.ItemForDON_VI.Text = "DON_VI";
            this.ItemForDON_VI.TextSize = new System.Drawing.Size(50, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdData;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(677, 320);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 15);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 15);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(677, 15);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblThang
            // 
            this.lblThang.Control = this.datThang;
            this.lblThang.Location = new System.Drawing.Point(338, 0);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(339, 26);
            this.lblThang.TextSize = new System.Drawing.Size(50, 17);
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
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 371);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(687, 34);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Client = this.grdData;
            this.searchControl1.Location = new System.Drawing.Point(6, 3);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.Client = this.grdData;
            this.searchControl1.Size = new System.Drawing.Size(202, 24);
            this.searchControl1.TabIndex = 0;
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdData;
            this.searchControl.Location = new System.Drawing.Point(2, 32);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdData;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(189, 30);
            this.searchControl.TabIndex = 10;
            // 
            // frmTinhTrangBangCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 405);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmTinhTrangBangCong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmTinhTrangBangCong";
            this.Load += new System.EventHandler(this.frmTinhTrangBangCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datThang.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblThang)).EndInit();
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDV;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDON_VI;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.DateEdit datThang;
        private DevExpress.XtraLayout.LayoutControlItem lblThang;
    }
}