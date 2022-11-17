namespace Vs.HRM
{
    partial class ucBaoHiemYTe
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions7 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions8 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions9 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions10 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions11 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions12 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdBHYT = new DevExpress.XtraGrid.GridControl();
            this.grvNgungDongBHXH = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cboDV = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboXN = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForDON_VI = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForXI_NGHIEP = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTO = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ItemForSumNhanVien = new DevExpress.XtraLayout.SimpleLabelItem();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.cboID_LTTHT = new DevExpress.XtraEditors.LookUpEdit();
            this.lblLoaiTTHienTai = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdBHYT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgungDongBHXH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).BeginInit();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_LTTHT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoaiTTHienTai)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBHYT
            // 
            this.grdBHYT.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdBHYT.Location = new System.Drawing.Point(6, 77);
            this.grdBHYT.MainView = this.grvNgungDongBHXH;
            this.grdBHYT.Name = "grdBHYT";
            this.grdBHYT.Size = new System.Drawing.Size(916, 382);
            this.grdBHYT.TabIndex = 9;
            this.grdBHYT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNgungDongBHXH});
            this.grdBHYT.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdBHYT_ProcessGridKey);
            // 
            // grvNgungDongBHXH
            // 
            this.grvNgungDongBHXH.DetailHeight = 297;
            this.grvNgungDongBHXH.GridControl = this.grdBHYT;
            this.grvNgungDongBHXH.Name = "grvNgungDongBHXH";
            this.grvNgungDongBHXH.OptionsView.ShowGroupPanel = false;
            this.grvNgungDongBHXH.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvNgungDongBHXH_CellValueChanged);
            this.grvNgungDongBHXH.RowCountChanged += new System.EventHandler(this.grvNgungDongBHXH_RowCountChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cboID_LTTHT);
            this.layoutControl1.Controls.Add(this.grdBHYT);
            this.layoutControl1.Controls.Add(this.cboDV);
            this.layoutControl1.Controls.Add(this.cboXN);
            this.layoutControl1.Controls.Add(this.cboTo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(928, 465);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cboDV
            // 
            this.cboDV.Location = new System.Drawing.Point(113, 6);
            this.cboDV.Name = "cboDV";
            this.cboDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDV.Properties.NullText = "";
            this.cboDV.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDV.Size = new System.Drawing.Size(197, 24);
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
            this.cboXN.Location = new System.Drawing.Point(419, 6);
            this.cboXN.Name = "cboXN";
            this.cboXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboXN.Properties.NullText = "";
            this.cboXN.Properties.PopupView = this.searchLookUpEdit2View;
            this.cboXN.Size = new System.Drawing.Size(193, 24);
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
            this.cboTo.Location = new System.Drawing.Point(721, 6);
            this.cboTo.Name = "cboTo";
            this.cboTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTo.Properties.NullText = "";
            this.cboTo.Properties.PopupView = this.searchLookUpEdit3View;
            this.cboTo.Size = new System.Drawing.Size(201, 24);
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
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForDON_VI,
            this.ItemForXI_NGHIEP,
            this.layoutControlItem1,
            this.ItemForTO,
            this.emptySpaceItem1,
            this.ItemForSumNhanVien,
            this.lblLoaiTTHienTai});
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
            this.ItemForDON_VI.Size = new System.Drawing.Size(306, 26);
            this.ItemForDON_VI.Text = "DON_VI";
            this.ItemForDON_VI.TextSize = new System.Drawing.Size(95, 17);
            // 
            // ItemForXI_NGHIEP
            // 
            this.ItemForXI_NGHIEP.Control = this.cboXN;
            this.ItemForXI_NGHIEP.CustomizationFormText = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Location = new System.Drawing.Point(306, 0);
            this.ItemForXI_NGHIEP.Name = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Size = new System.Drawing.Size(302, 26);
            this.ItemForXI_NGHIEP.Text = "XI_NGHIEP";
            this.ItemForXI_NGHIEP.TextSize = new System.Drawing.Size(95, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdBHYT;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(918, 384);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForTO
            // 
            this.ItemForTO.Control = this.cboTo;
            this.ItemForTO.CustomizationFormText = "ItemForTO";
            this.ItemForTO.Location = new System.Drawing.Point(608, 0);
            this.ItemForTO.Name = "ItemForTO";
            this.ItemForTO.Size = new System.Drawing.Size(310, 26);
            this.ItemForTO.Text = "TO";
            this.ItemForTO.TextSize = new System.Drawing.Size(95, 17);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(306, 26);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 15);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 15);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(612, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            this.ItemForSumNhanVien.TextSize = new System.Drawing.Size(95, 17);
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
            windowsUIButtonImageOptions7.ImageUri.Uri = "business%20objects/bo_audit_changehistory";
            windowsUIButtonImageOptions8.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions9.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions10.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions11.ImageUri.Uri = "SaveAndClose";
            windowsUIButtonImageOptions12.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "ngayhethan", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions8, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "themsua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions9, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions10, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions11, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions12, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
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
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdBHYT;
            this.searchControl.Location = new System.Drawing.Point(13, 9);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.AutoHeight = false;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdBHYT;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(192, 22);
            this.searchControl.TabIndex = 10;
            // 
            // cboID_LTTHT
            // 
            this.cboID_LTTHT.Location = new System.Drawing.Point(113, 32);
            this.cboID_LTTHT.Name = "cboID_LTTHT";
            this.cboID_LTTHT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_LTTHT.Properties.NullText = "";
            this.cboID_LTTHT.Size = new System.Drawing.Size(197, 24);
            this.cboID_LTTHT.StyleController = this.layoutControl1;
            this.cboID_LTTHT.TabIndex = 19;
            this.cboID_LTTHT.EditValueChanged += new System.EventHandler(this.cboID_LTTHT_EditValueChanged);
            // 
            // lblLoaiTTHienTai
            // 
            this.lblLoaiTTHienTai.Control = this.cboID_LTTHT;
            this.lblLoaiTTHienTai.Location = new System.Drawing.Point(0, 26);
            this.lblLoaiTTHienTai.Name = "lblLoaiTTHienTai";
            this.lblLoaiTTHienTai.Size = new System.Drawing.Size(306, 26);
            this.lblLoaiTTHienTai.TextSize = new System.Drawing.Size(95, 17);
            // 
            // ucBaoHiemYTe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucBaoHiemYTe";
            this.Size = new System.Drawing.Size(928, 499);
            this.Load += new System.EventHandler(this.ucBaoHiemYTe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBHYT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNgungDongBHXH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).EndInit();
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_LTTHT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoaiTTHienTai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdBHYT;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNgungDongBHXH;
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
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem ItemForSumNhanVien;
        private DevExpress.XtraEditors.LookUpEdit cboID_LTTHT;
        private DevExpress.XtraLayout.LayoutControlItem lblLoaiTTHienTai;
    }
}
