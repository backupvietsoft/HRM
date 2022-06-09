namespace Vs.TimeAttendance
{
    partial class ucMaTheChamCong
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.grdMTCC = new DevExpress.XtraGrid.GridControl();
            this.grvMTCC = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cboDV = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboXN = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dNgayXem = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForDON_VI = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForXI_NGHIEP = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTO = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForNGAY_XEM = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSumNhanVien = new DevExpress.XtraLayout.SimpleLabelItem();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdMTCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMTCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayXem.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayXem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY_XEM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).BeginInit();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMTCC
            // 
            this.grdMTCC.Location = new System.Drawing.Point(12, 72);
            this.grdMTCC.MainView = this.grvMTCC;
            this.grdMTCC.Name = "grdMTCC";
            this.grdMTCC.Size = new System.Drawing.Size(776, 371);
            this.grdMTCC.TabIndex = 9;
            this.grdMTCC.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMTCC});
            // 
            // grvMTCC
            // 
            this.grvMTCC.GridControl = this.grdMTCC;
            this.grvMTCC.Name = "grvMTCC";
            this.grvMTCC.OptionsView.ShowGroupPanel = false;
            this.grvMTCC.RowCountChanged += new System.EventHandler(this.grvMTCC_RowCountChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdMTCC);
            this.layoutControl1.Controls.Add(this.cboDV);
            this.layoutControl1.Controls.Add(this.cboXN);
            this.layoutControl1.Controls.Add(this.cboTo);
            this.layoutControl1.Controls.Add(this.dNgayXem);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(800, 455);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cboDV
            // 
            this.cboDV.Location = new System.Drawing.Point(108, 12);
            this.cboDV.Name = "cboDV";
            this.cboDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDV.Properties.NullText = "";
            this.cboDV.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDV.Size = new System.Drawing.Size(161, 26);
            this.cboDV.StyleController = this.layoutControl1;
            this.cboDV.TabIndex = 7;
            this.cboDV.EditValueChanged += new System.EventHandler(this.cboDV_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cboXN
            // 
            this.cboXN.Location = new System.Drawing.Point(369, 12);
            this.cboXN.Name = "cboXN";
            this.cboXN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboXN.Properties.NullText = "";
            this.cboXN.Properties.PopupView = this.searchLookUpEdit2View;
            this.cboXN.Size = new System.Drawing.Size(159, 26);
            this.cboXN.StyleController = this.layoutControl1;
            this.cboXN.TabIndex = 8;
            this.cboXN.EditValueChanged += new System.EventHandler(this.cboXN_EditValueChanged);
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // cboTo
            // 
            this.cboTo.Location = new System.Drawing.Point(628, 12);
            this.cboTo.Name = "cboTo";
            this.cboTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTo.Properties.NullText = "";
            this.cboTo.Properties.PopupView = this.searchLookUpEdit3View;
            this.cboTo.Size = new System.Drawing.Size(160, 26);
            this.cboTo.StyleController = this.layoutControl1;
            this.cboTo.TabIndex = 9;
            this.cboTo.EditValueChanged += new System.EventHandler(this.cboTo_EditValueChanged);
            // 
            // searchLookUpEdit3View
            // 
            this.searchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit3View.Name = "searchLookUpEdit3View";
            this.searchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // dNgayXem
            // 
            this.dNgayXem.EditValue = null;
            this.dNgayXem.Location = new System.Drawing.Point(108, 42);
            this.dNgayXem.Name = "dNgayXem";
            this.dNgayXem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dNgayXem.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dNgayXem.Size = new System.Drawing.Size(161, 26);
            this.dNgayXem.StyleController = this.layoutControl1;
            this.dNgayXem.TabIndex = 10;
            this.dNgayXem.EditValueChanged += new System.EventHandler(this.dNgayXem_EditValueChanged);
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
            this.ItemForNGAY_XEM,
            this.ItemForSumNhanVien});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(800, 455);
            this.Root.TextVisible = false;
            // 
            // ItemForDON_VI
            // 
            this.ItemForDON_VI.Control = this.cboDV;
            this.ItemForDON_VI.CustomizationFormText = "ItemForDON_VI";
            this.ItemForDON_VI.Location = new System.Drawing.Point(0, 0);
            this.ItemForDON_VI.Name = "ItemForDON_VI";
            this.ItemForDON_VI.Size = new System.Drawing.Size(261, 30);
            this.ItemForDON_VI.Text = "DON_VI";
            this.ItemForDON_VI.TextSize = new System.Drawing.Size(93, 20);
            // 
            // ItemForXI_NGHIEP
            // 
            this.ItemForXI_NGHIEP.Control = this.cboXN;
            this.ItemForXI_NGHIEP.CustomizationFormText = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Location = new System.Drawing.Point(261, 0);
            this.ItemForXI_NGHIEP.Name = "ItemForXI_NGHIEP";
            this.ItemForXI_NGHIEP.Size = new System.Drawing.Size(259, 30);
            this.ItemForXI_NGHIEP.Text = "XI_NGHIEP";
            this.ItemForXI_NGHIEP.TextSize = new System.Drawing.Size(93, 20);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdMTCC;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(780, 375);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ItemForTO
            // 
            this.ItemForTO.Control = this.cboTo;
            this.ItemForTO.CustomizationFormText = "ItemForTO";
            this.ItemForTO.Location = new System.Drawing.Point(520, 0);
            this.ItemForTO.Name = "ItemForTO";
            this.ItemForTO.Size = new System.Drawing.Size(260, 30);
            this.ItemForTO.Text = "TO";
            this.ItemForTO.TextSize = new System.Drawing.Size(93, 20);
            // 
            // ItemForNGAY_XEM
            // 
            this.ItemForNGAY_XEM.Control = this.dNgayXem;
            this.ItemForNGAY_XEM.Location = new System.Drawing.Point(0, 30);
            this.ItemForNGAY_XEM.Name = "ItemForNGAY_XEM";
            this.ItemForNGAY_XEM.Size = new System.Drawing.Size(261, 30);
            this.ItemForNGAY_XEM.Text = "NGAY_XEM";
            this.ItemForNGAY_XEM.TextSize = new System.Drawing.Size(93, 20);
            // 
            // ItemForSumNhanVien
            // 
            this.ItemForSumNhanVien.AllowHotTrack = false;
            this.ItemForSumNhanVien.AppearanceItemCaption.Options.UseTextOptions = true;
            this.ItemForSumNhanVien.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ItemForSumNhanVien.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ItemForSumNhanVien.Location = new System.Drawing.Point(261, 30);
            this.ItemForSumNhanVien.Name = "ItemForSumNhanVien";
            this.ItemForSumNhanVien.Size = new System.Drawing.Size(519, 30);
            this.ItemForSumNhanVien.Text = "SumNhanVien";
            this.ItemForSumNhanVien.TextSize = new System.Drawing.Size(93, 20);
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions3.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions4.ImageUri.Uri = "SaveAndClose";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "themsua", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 455);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(800, 40);
            this.windowsUIButton.TabIndex = 15;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Client = this.grdMTCC;
            this.searchControl.Location = new System.Drawing.Point(0, 10);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdMTCC;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(220, 30);
            this.searchControl.TabIndex = 10;
            // 
            // ucMaTheChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucMaTheChamCong";
            this.Size = new System.Drawing.Size(800, 495);
            this.Load += new System.EventHandler(this.ucMaTheChamCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMTCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMTCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboXN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayXem.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayXem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForDON_VI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForXI_NGHIEP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY_XEM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSumNhanVien)).EndInit();
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdMTCC;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMTCC;
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
        private DevExpress.XtraEditors.DateEdit dNgayXem;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNGAY_XEM;
        private DevExpress.XtraLayout.SimpleLabelItem ItemForSumNhanVien;
    }
}
