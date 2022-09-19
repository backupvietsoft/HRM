namespace Vs.Recruit
{
    partial class frmDSPhongVan
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
            DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule compareAgainstControlValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.datNgayLapTNgay = new DevExpress.XtraEditors.DateEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.rdoTinhTrang = new DevExpress.XtraEditors.RadioGroup();
            this.grdPhongVan = new DevExpress.XtraGrid.GridControl();
            this.grvPhongVan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.datDNgay = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblNgayLapTNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDNgay = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.txtTim = new DevExpress.XtraEditors.SearchControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.datNgayLapTNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNgayLapTNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoTinhTrang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhongVan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhongVan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNgayLapTNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // datNgayLapTNgay
            // 
            this.datNgayLapTNgay.EditValue = null;
            this.datNgayLapTNgay.Location = new System.Drawing.Point(98, 42);
            this.datNgayLapTNgay.Name = "datNgayLapTNgay";
            this.datNgayLapTNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datNgayLapTNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datNgayLapTNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.datNgayLapTNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datNgayLapTNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.datNgayLapTNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datNgayLapTNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.datNgayLapTNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datNgayLapTNgay.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datNgayLapTNgay.Size = new System.Drawing.Size(221, 20);
            this.datNgayLapTNgay.StyleController = this.dataLayoutControl1;
            this.datNgayLapTNgay.TabIndex = 2;
            this.datNgayLapTNgay.EditValueChanged += new System.EventHandler(this.datNgayLapTNgay_EditValueChanged);
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.rdoTinhTrang);
            this.dataLayoutControl1.Controls.Add(this.grdPhongVan);
            this.dataLayoutControl1.Controls.Add(this.datDNgay);
            this.dataLayoutControl1.Controls.Add(this.datNgayLapTNgay);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(13, 11);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(643, 310);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // rdoTinhTrang
            // 
            this.rdoTinhTrang.Location = new System.Drawing.Point(12, 12);
            this.rdoTinhTrang.Name = "rdoTinhTrang";
            this.rdoTinhTrang.Properties.Columns = 3;
            this.rdoTinhTrang.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "rdoDangSoan", true, null, "rdoDangSoan"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "rdoDangThucHien", true, null, "rdoDangThucHien"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "rdoDaKetThuc", true, null, "rdoDaKetThuc")});
            this.rdoTinhTrang.Size = new System.Drawing.Size(619, 26);
            this.rdoTinhTrang.StyleController = this.dataLayoutControl1;
            this.rdoTinhTrang.TabIndex = 1;
            this.rdoTinhTrang.SelectedIndexChanged += new System.EventHandler(this.rdoTinhTrang_SelectedIndexChanged);
            // 
            // grdPhongVan
            // 
            this.grdPhongVan.Location = new System.Drawing.Point(12, 66);
            this.grdPhongVan.MainView = this.grvPhongVan;
            this.grdPhongVan.Name = "grdPhongVan";
            this.grdPhongVan.Size = new System.Drawing.Size(619, 232);
            this.grdPhongVan.TabIndex = 4;
            this.grdPhongVan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPhongVan});
            // 
            // grvPhongVan
            // 
            this.grvPhongVan.GridControl = this.grdPhongVan;
            this.grvPhongVan.Name = "grvPhongVan";
            this.grvPhongVan.OptionsView.ShowGroupPanel = false;
            this.grvPhongVan.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.grvPhongVan_MouseWheel);
            this.grvPhongVan.DoubleClick += new System.EventHandler(this.grvPhongVan_DoubleClick);
            // 
            // datDNgay
            // 
            this.datDNgay.EditValue = null;
            this.datDNgay.Location = new System.Drawing.Point(409, 42);
            this.datDNgay.Name = "datDNgay";
            this.datDNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.datDNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datDNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.datDNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datDNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.datDNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datDNgay.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datDNgay.Size = new System.Drawing.Size(222, 20);
            this.datDNgay.StyleController = this.dataLayoutControl1;
            this.datDNgay.TabIndex = 3;
            compareAgainstControlValidationRule1.CompareControlOperator = DevExpress.XtraEditors.DXErrorProvider.CompareControlOperator.GreaterOrEqual;
            compareAgainstControlValidationRule1.Control = this.datNgayLapTNgay;
            compareAgainstControlValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.datDNgay, compareAgainstControlValidationRule1);
            this.datDNgay.EditValueChanged += new System.EventHandler(this.datDNgay_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblNgayLapTNgay,
            this.lblDNgay,
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(643, 310);
            this.Root.TextVisible = false;
            // 
            // lblNgayLapTNgay
            // 
            this.lblNgayLapTNgay.Control = this.datNgayLapTNgay;
            this.lblNgayLapTNgay.Location = new System.Drawing.Point(0, 30);
            this.lblNgayLapTNgay.Name = "lblNgayLapTNgay";
            this.lblNgayLapTNgay.Size = new System.Drawing.Size(311, 24);
            this.lblNgayLapTNgay.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblDNgay
            // 
            this.lblDNgay.Control = this.datDNgay;
            this.lblDNgay.Location = new System.Drawing.Point(311, 30);
            this.lblDNgay.Name = "lblDNgay";
            this.lblDNgay.Size = new System.Drawing.Size(312, 24);
            this.lblDNgay.TextSize = new System.Drawing.Size(83, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdPhongVan;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(623, 236);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rdoTinhTrang;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(623, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F)});
            this.tablePanel1.Controls.Add(this.btnALL);
            this.tablePanel1.Controls.Add(this.dataLayoutControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 90F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 46F)});
            this.tablePanel1.Size = new System.Drawing.Size(669, 370);
            this.tablePanel1.TabIndex = 0;
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.tablePanel1.SetColumn(this.btnALL, 1);
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.txtTim);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnALL.Location = new System.Drawing.Point(13, 327);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tablePanel1.SetRow(this.btnALL, 2);
            this.btnALL.Size = new System.Drawing.Size(643, 40);
            this.btnALL.TabIndex = 6;
            this.btnALL.Text = "S";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // txtTim
            // 
            this.txtTim.Client = this.grdPhongVan;
            this.txtTim.Location = new System.Drawing.Point(12, 11);
            this.txtTim.Name = "txtTim";
            this.txtTim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.txtTim.Properties.Client = this.grdPhongVan;
            this.txtTim.Size = new System.Drawing.Size(212, 20);
            this.txtTim.StyleController = this.dataLayoutControl1;
            this.txtTim.TabIndex = 5;
            // 
            // frmDSPhongVan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 370);
            this.Controls.Add(this.tablePanel1);
            this.Name = "frmDSPhongVan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDSPhongVan";
            this.Load += new System.EventHandler(this.frmDSPhongVan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datNgayLapTNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datNgayLapTNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoTinhTrang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPhongVan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPhongVan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNgayLapTNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraGrid.GridControl grdPhongVan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPhongVan;
        private DevExpress.XtraEditors.DateEdit datDNgay;
        private DevExpress.XtraEditors.DateEdit datNgayLapTNgay;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblNgayLapTNgay;
        private DevExpress.XtraLayout.LayoutControlItem lblDNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.SearchControl txtTim;
        private DevExpress.XtraEditors.RadioGroup rdoTinhTrang;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}