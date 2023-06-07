namespace Vs.HRM
{
    partial class ucInNhanVien
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInNhanVien));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdTTNhanVien = new DevExpress.XtraGrid.GridControl();
            this.grvTTNhanVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.datDNgay = new DevExpress.XtraEditors.DateEdit();
            this.datTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.cboDV = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboXN = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboID_IHL = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForDON_VI = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForXI_NGHIEP = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTO = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSumNhanVien = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblTNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblIN_HANG_LOAT = new DevExpress.XtraLayout.LayoutControlItem();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.grdTTNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTTNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_IHL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIN_HANG_LOAT)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTTNhanVien
            // 
            this.grdTTNhanVien.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdTTNhanVien.Location = new System.Drawing.Point(6, 77);
            this.grdTTNhanVien.MainView = this.grvTTNhanVien;
            this.grdTTNhanVien.Name = "grdTTNhanVien";
            this.grdTTNhanVien.Size = new System.Drawing.Size(916, 382);
            this.grdTTNhanVien.TabIndex = 9;
            this.grdTTNhanVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTTNhanVien});
            // 
            // grvTTNhanVien
            // 
            this.grvTTNhanVien.DetailHeight = 297;
            this.grvTTNhanVien.GridControl = this.grdTTNhanVien;
            this.grvTTNhanVien.Name = "grvTTNhanVien";
            this.grvTTNhanVien.OptionsSelection.MultiSelect = true;
            this.grvTTNhanVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvTTNhanVien.OptionsView.ShowGroupPanel = false;
            this.grvTTNhanVien.RowCountChanged += new System.EventHandler(this.grvTTNhanVien_RowCountChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.datDNgay);
            this.layoutControl1.Controls.Add(this.datTuNgay);
            this.layoutControl1.Controls.Add(this.grdTTNhanVien);
            this.layoutControl1.Controls.Add(this.cboDV);
            this.layoutControl1.Controls.Add(this.cboXN);
            this.layoutControl1.Controls.Add(this.cboTo);
            this.layoutControl1.Controls.Add(this.cboID_IHL);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(928, 465);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // datDNgay
            // 
            this.datDNgay.EditValue = null;
            this.datDNgay.Location = new System.Drawing.Point(405, 32);
            this.datDNgay.Name = "datDNgay";
            this.datDNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Size = new System.Drawing.Size(190, 24);
            this.datDNgay.StyleController = this.layoutControl1;
            this.datDNgay.TabIndex = 11;
            this.datDNgay.EditValueChanged += new System.EventHandler(this.datDNgay_EditValueChanged);
            // 
            // datTuNgay
            // 
            this.datTuNgay.EditValue = null;
            this.datTuNgay.Location = new System.Drawing.Point(122, 32);
            this.datTuNgay.Name = "datTuNgay";
            this.datTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTuNgay.Size = new System.Drawing.Size(165, 24);
            this.datTuNgay.StyleController = this.layoutControl1;
            this.datTuNgay.TabIndex = 10;
            this.datTuNgay.EditValueChanged += new System.EventHandler(this.datTuNgay_EditValueChanged);
            // 
            // cboDV
            // 
            this.cboDV.Location = new System.Drawing.Point(122, 6);
            this.cboDV.Name = "cboDV";
            this.cboDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDV.Properties.NullText = "";
            this.cboDV.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDV.Size = new System.Drawing.Size(165, 24);
            this.cboDV.StyleController = this.layoutControl1;
            this.cboDV.TabIndex = 7;
            this.cboDV.EditValueChanged += new System.EventHandler(this.cboDV_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 297;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cboXN
            // 
            this.cboXN.Location = new System.Drawing.Point(405, 6);
            this.cboXN.Name = "cboXN";
            this.cboXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboXN.Properties.NullText = "";
            this.cboXN.Properties.PopupView = this.searchLookUpEdit2View;
            this.cboXN.Size = new System.Drawing.Size(190, 24);
            this.cboXN.StyleController = this.layoutControl1;
            this.cboXN.TabIndex = 8;
            this.cboXN.EditValueChanged += new System.EventHandler(this.cboXN_EditValueChanged);
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.DetailHeight = 297;
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // cboTo
            // 
            this.cboTo.Location = new System.Drawing.Point(713, 6);
            this.cboTo.Name = "cboTo";
            this.cboTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTo.Properties.NullText = "";
            this.cboTo.Properties.PopupView = this.searchLookUpEdit3View;
            this.cboTo.Size = new System.Drawing.Size(209, 24);
            this.cboTo.StyleController = this.layoutControl1;
            this.cboTo.TabIndex = 9;
            this.cboTo.EditValueChanged += new System.EventHandler(this.cboTo_EditValueChanged);
            // 
            // searchLookUpEdit3View
            // 
            this.searchLookUpEdit3View.DetailHeight = 297;
            this.searchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit3View.Name = "searchLookUpEdit3View";
            this.searchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // cboID_IHL
            // 
            this.cboID_IHL.Location = new System.Drawing.Point(713, 32);
            this.cboID_IHL.Name = "cboID_IHL";
            this.cboID_IHL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_IHL.Properties.NullText = "";
            this.cboID_IHL.Properties.PopupView = this.searchLookUpEdit2View1;
            this.cboID_IHL.Size = new System.Drawing.Size(209, 24);
            this.cboID_IHL.StyleController = this.layoutControl1;
            this.cboID_IHL.TabIndex = 8;
            this.cboID_IHL.EditValueChanged += new System.EventHandler(this.cboID_IHL_EditValueChanged);
            // 
            // searchLookUpEdit2View1
            // 
            this.searchLookUpEdit2View1.DetailHeight = 297;
            this.searchLookUpEdit2View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View1.Name = "searchLookUpEdit2View1";
            this.searchLookUpEdit2View1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View1.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForDON_VI,
            this.ItemForXI_NGHIEP,
            this.ItemForTO,
            this.layoutControlItem1,
            this.ItemForSumNhanVien,
            this.lblTNgay,
            this.lblDNgay,
            this.lblIN_HANG_LOAT});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(928, 465);
            this.Root.TextVisible = false;
            // 
            // ItemForDON_VI
            // 
            this.ItemForDON_VI.Control = this.cboDV;
            this.ItemForDON_VI.CustomizationFormText = "ItemForDON_VI";
            this.ItemForDON_VI.Location = new System.Drawing.Point(0, 0);
            this.ItemForDON_VI.Name = "ItemForDON_VI";
            this.ItemForDON_VI.Size = new System.Drawing.Size(283, 26);
            this.ItemForDON_VI.Text = "DON_VI";
            this.ItemForDON_VI.TextSize = new System.Drawing.Size(104, 17);
            // 
            // ItemForXI_NGHIEP
            // 
            this.ItemForXI_NGHIEP.Control = this.cboXN;
            this.ItemForXI_NGHIEP.CustomizationFormText = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Location = new System.Drawing.Point(283, 0);
            this.ItemForXI_NGHIEP.Name = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Size = new System.Drawing.Size(308, 26);
            this.ItemForXI_NGHIEP.Text = "XI_NGHIEP";
            this.ItemForXI_NGHIEP.TextSize = new System.Drawing.Size(104, 17);
            // 
            // ItemForTO
            // 
            this.ItemForTO.Control = this.cboTo;
            this.ItemForTO.CustomizationFormText = "ItemForTO";
            this.ItemForTO.Location = new System.Drawing.Point(591, 0);
            this.ItemForTO.Name = "ItemForTO";
            this.ItemForTO.Size = new System.Drawing.Size(327, 26);
            this.ItemForTO.Text = "TO";
            this.ItemForTO.TextSize = new System.Drawing.Size(104, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdTTNhanVien;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(918, 384);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForSumNhanVien
            // 
            this.ItemForSumNhanVien.AllowHotTrack = false;
            this.ItemForSumNhanVien.AppearanceItemCaption.Options.UseTextOptions = true;
            this.ItemForSumNhanVien.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ItemForSumNhanVien.Location = new System.Drawing.Point(0, 52);
            this.ItemForSumNhanVien.Name = "ItemForSumNhanVien";
            this.ItemForSumNhanVien.Size = new System.Drawing.Size(918, 19);
            this.ItemForSumNhanVien.Text = "SumNhanVien";
            this.ItemForSumNhanVien.TextSize = new System.Drawing.Size(104, 17);
            // 
            // lblTNgay
            // 
            this.lblTNgay.Control = this.datTuNgay;
            this.lblTNgay.Location = new System.Drawing.Point(0, 26);
            this.lblTNgay.Name = "lblTNgay";
            this.lblTNgay.Size = new System.Drawing.Size(283, 26);
            this.lblTNgay.TextSize = new System.Drawing.Size(104, 17);
            // 
            // lblDNgay
            // 
            this.lblDNgay.Control = this.datDNgay;
            this.lblDNgay.Location = new System.Drawing.Point(283, 26);
            this.lblDNgay.Name = "lblDNgay";
            this.lblDNgay.Size = new System.Drawing.Size(308, 26);
            this.lblDNgay.TextSize = new System.Drawing.Size(104, 17);
            // 
            // lblIN_HANG_LOAT
            // 
            this.lblIN_HANG_LOAT.Control = this.cboID_IHL;
            this.lblIN_HANG_LOAT.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblIN_HANG_LOAT.CustomizationFormText = "ItemForXI_NGHIEP";
            this.lblIN_HANG_LOAT.Location = new System.Drawing.Point(591, 26);
            this.lblIN_HANG_LOAT.Name = "lblIN_HANG_LOAT";
            this.lblIN_HANG_LOAT.Size = new System.Drawing.Size(327, 26);
            this.lblIN_HANG_LOAT.Text = "lblIN_HANG_LOAT";
            this.lblIN_HANG_LOAT.TextSize = new System.Drawing.Size(104, 17);
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
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            windowsUIButtonImageOptions6.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "in", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 465);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(928, 34);
            this.windowsUIButton.TabIndex = 15;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // ucInNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucInNhanVien";
            this.Size = new System.Drawing.Size(928, 499);
            this.Load += new System.EventHandler(this.ucThongTinNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTTNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTTNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_IHL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIN_HANG_LOAT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdTTNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTTNhanVien;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDV;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit cboXN;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForDON_VI;
        private DevExpress.XtraLayout.LayoutControlItem ItemForXI_NGHIEP;
        private DevExpress.XtraEditors.SearchLookUpEdit cboTo;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit3View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTO;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem ItemForSumNhanVien;
        private DevExpress.XtraEditors.DateEdit datDNgay;
        private DevExpress.XtraEditors.DateEdit datTuNgay;
        private DevExpress.XtraLayout.LayoutControlItem lblTNgay;
        private DevExpress.XtraLayout.LayoutControlItem lblDNgay;
        private DevExpress.XtraEditors.SearchLookUpEdit cboID_IHL;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View1;
        private DevExpress.XtraLayout.LayoutControlItem lblIN_HANG_LOAT;
    }
}
