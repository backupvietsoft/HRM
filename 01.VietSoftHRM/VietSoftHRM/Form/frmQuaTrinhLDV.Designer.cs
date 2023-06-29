namespace Vs.Payroll
{
    partial class frmQuaTrinhLDV
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuaTrinhLDV));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.popListNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.grdNgay = new DevExpress.XtraGrid.GridControl();
            this.grvNgay1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popNgay = new DevExpress.XtraEditors.PopupContainerControl();
            this.calThang = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboThang = new Commons.MPopupContainerEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForThang = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tabQuaTrinhLDV = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolCapNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLoaiXepLoai = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).BeginInit();
            this.popListNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).BeginInit();
            this.popNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).BeginInit();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabQuaTrinhLDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.popListNgay);
            this.dataLayoutControl1.Controls.Add(this.popNgay);
            this.dataLayoutControl1.Controls.Add(this.windowsUIButton);
            this.dataLayoutControl1.Controls.Add(this.grdData);
            this.dataLayoutControl1.Controls.Add(this.cboThang);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(900, 580);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // popListNgay
            // 
            this.popListNgay.AutoSize = true;
            this.popListNgay.Controls.Add(this.grdNgay);
            this.popListNgay.Location = new System.Drawing.Point(166, 143);
            this.popListNgay.MinimumSize = new System.Drawing.Size(350, 170);
            this.popListNgay.Name = "popListNgay";
            this.popListNgay.Size = new System.Drawing.Size(350, 170);
            this.popListNgay.TabIndex = 1;
            // 
            // grdNgay
            // 
            this.grdNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNgay.Location = new System.Drawing.Point(0, 0);
            this.grdNgay.MainView = this.grvNgay1;
            this.grdNgay.Name = "grdNgay";
            this.grdNgay.Size = new System.Drawing.Size(350, 170);
            this.grdNgay.TabIndex = 15;
            this.grdNgay.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNgay1});
            // 
            // grvNgay1
            // 
            this.grvNgay1.DetailHeight = 297;
            this.grvNgay1.GridControl = this.grdNgay;
            this.grvNgay1.Name = "grvNgay1";
            this.grvNgay1.OptionsView.ShowAutoFilterRow = true;
            this.grvNgay1.OptionsView.ShowGroupPanel = false;
            this.grvNgay1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvNgay1_RowCellClick);
            // 
            // popNgay
            // 
            this.popNgay.AutoSize = true;
            this.popNgay.Controls.Add(this.calThang);
            this.popNgay.Location = new System.Drawing.Point(97, 174);
            this.popNgay.MinimumSize = new System.Drawing.Size(315, 280);
            this.popNgay.Name = "popNgay";
            this.popNgay.Size = new System.Drawing.Size(315, 280);
            this.popNgay.TabIndex = 1;
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
            this.calThang.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.calThang.DateTimeCommit += new System.EventHandler(this.calThang_DateTimeCommit);
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "Edit";
            windowsUIButtonImageOptions2.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions3.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions4.ImageUri.Uri = "Print";
            windowsUIButtonImageOptions4.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions4.SvgImage")));
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "themsua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl1);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Location = new System.Drawing.Point(6, 522);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(888, 32);
            this.windowsUIButton.TabIndex = 6;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
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
            this.searchControl1.Size = new System.Drawing.Size(198, 24);
            this.searchControl1.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.Location = new System.Drawing.Point(12, 57);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(876, 457);
            this.grdData.TabIndex = 1;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvHTL_PopupMenuShowing);
            this.grvData.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvData_InitNewRow);
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            this.grvData.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvData_InvalidRowException);
            this.grvData.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.grvData_ValidatingEditor);
            this.grvData.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.grvData_InvalidValueException);
            // 
            // cboThang
            // 
            this.cboThang.Location = new System.Drawing.Point(54, 6);
            this.cboThang.Name = "cboThang";
            this.cboThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboThang.Properties.DefaultActionButtonIndex = 0;
            this.cboThang.Properties.DefaultPopupControl = this.popListNgay;
            this.cboThang.Properties.DifferentActionButtonIndex = 1;
            this.cboThang.Properties.DifferentPopupControl = this.popNgay;
            this.cboThang.Size = new System.Drawing.Size(223, 24);
            this.cboThang.StyleController = this.dataLayoutControl1;
            this.cboThang.TabIndex = 4;
            this.cboThang.EditValueChanged += new System.EventHandler(this.cboThang_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.ItemForThang,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.tabQuaTrinhLDV});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(900, 580);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.windowsUIButton;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 516);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(890, 34);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForThang
            // 
            this.ItemForThang.Control = this.cboThang;
            this.ItemForThang.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForThang.CustomizationFormText = "NGAY";
            this.ItemForThang.Location = new System.Drawing.Point(0, 0);
            this.ItemForThang.Name = "ItemForThang";
            this.ItemForThang.Size = new System.Drawing.Size(273, 26);
            this.ItemForThang.Text = "Tháng";
            this.ItemForThang.TextSize = new System.Drawing.Size(36, 17);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(273, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(617, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 550);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(890, 20);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tabQuaTrinhLDV
            // 
            this.tabQuaTrinhLDV.CustomizationFormText = "tabVitri";
            this.tabQuaTrinhLDV.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.tabQuaTrinhLDV.Location = new System.Drawing.Point(0, 26);
            this.tabQuaTrinhLDV.Name = "tabQuaTrinhLDV";
            this.tabQuaTrinhLDV.OptionsItemText.TextToControlDistance = 3;
            this.tabQuaTrinhLDV.Size = new System.Drawing.Size(890, 490);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdData;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(878, 459);
            this.layoutControlItem2.Text = "layoutControlItem1";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCapNhat,
            this.toolLoaiXepLoai});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 48);
            // 
            // toolCapNhat
            // 
            this.toolCapNhat.Name = "toolCapNhat";
            this.toolCapNhat.Size = new System.Drawing.Size(140, 22);
            this.toolCapNhat.Text = "Cập nhật";
            this.toolCapNhat.Click += new System.EventHandler(this.toolCapNhat_Click);
            // 
            // toolLoaiXepLoai
            // 
            this.toolLoaiXepLoai.Name = "toolLoaiXepLoai";
            this.toolLoaiXepLoai.Size = new System.Drawing.Size(140, 22);
            this.toolLoaiXepLoai.Text = "Loại xếp loại";
            this.toolLoaiXepLoai.Click += new System.EventHandler(this.toolLoaiXepLoai_Click);
            // 
            // frmQuaTrinhLDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 580);
            this.Controls.Add(this.dataLayoutControl1);
            this.MaximizeBox = false;
            this.Name = "frmQuaTrinhLDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmQuaTrinhLDV";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuaTrinhLDV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.dataLayoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListNgay)).EndInit();
            this.popListNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popNgay)).EndInit();
            this.popNgay.ResumeLayout(false);
            this.popNgay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).EndInit();
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabQuaTrinhLDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private Commons.MPopupContainerEdit cboThang;
        private DevExpress.XtraLayout.LayoutControlItem ItemForThang;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolCapNhat;
        private DevExpress.XtraEditors.PopupContainerControl popNgay;
        private DevExpress.XtraEditors.Controls.CalendarControl calThang;
        private DevExpress.XtraEditors.PopupContainerControl popListNgay;
        private DevExpress.XtraGrid.GridControl grdNgay;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNgay1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup tabQuaTrinhLDV;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.ToolStripMenuItem toolLoaiXepLoai;
    }
}