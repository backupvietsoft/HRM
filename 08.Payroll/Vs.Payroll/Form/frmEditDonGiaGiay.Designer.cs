namespace Vs.Payroll
{
    partial class frmEditDonGiaGiay
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.NGAY_QDDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.HS_DG_GIAYTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForHS_DG_GIAY = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForNGAY_QD = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGAY_QDDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGAY_QDDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HS_DG_GIAYTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForHS_DG_GIAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY_QD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnALL
            // 
            windowsUIButtonImageOptions1.Image = global::Vs.Payroll.Properties.Resources.iconsave;
            windowsUIButtonImageOptions2.Image = global::Vs.Payroll.Properties.Resources.iconxoa;
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Lưu", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Hủy", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "huy", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 530);
            this.btnALL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5, 9, 5, 5);
            this.btnALL.Size = new System.Drawing.Size(945, 78);
            this.btnALL.TabIndex = 11;
            this.btnALL.Text = "windowsUIButtonPanel2";
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // NGAY_QDDateEdit
            // 
            this.NGAY_QDDateEdit.EditValue = null;
            this.NGAY_QDDateEdit.Location = new System.Drawing.Point(125, 6);
            this.NGAY_QDDateEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NGAY_QDDateEdit.Name = "NGAY_QDDateEdit";
            this.NGAY_QDDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NGAY_QDDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NGAY_QDDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.NGAY_QDDateEdit.Size = new System.Drawing.Size(653, 26);
            this.NGAY_QDDateEdit.StyleController = this.dataLayoutControl1;
            this.NGAY_QDDateEdit.TabIndex = 6;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.NGAY_QDDateEdit, conditionValidationRule2);
            // 
            // HS_DG_GIAYTextEdit
            // 
            this.HS_DG_GIAYTextEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HS_DG_GIAYTextEdit.Location = new System.Drawing.Point(125, 34);
            this.HS_DG_GIAYTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HS_DG_GIAYTextEdit.Name = "HS_DG_GIAYTextEdit";
            this.HS_DG_GIAYTextEdit.Properties.DisplayFormat.FormatString = "N2";
            this.HS_DG_GIAYTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.HS_DG_GIAYTextEdit.Properties.EditFormat.FormatString = "N2";
            this.HS_DG_GIAYTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.HS_DG_GIAYTextEdit.Properties.Mask.EditMask = "n2";
            this.HS_DG_GIAYTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.HS_DG_GIAYTextEdit.Size = new System.Drawing.Size(653, 26);
            this.HS_DG_GIAYTextEdit.StyleController = this.dataLayoutControl1;
            this.HS_DG_GIAYTextEdit.TabIndex = 7;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.HS_DG_GIAYTextEdit, conditionValidationRule1);
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.NGAY_QDDateEdit);
            this.dataLayoutControl1.Controls.Add(this.HS_DG_GIAYTextEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(67, 59);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(630, 281, 650, 400);
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(784, 466);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(784, 466);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForHS_DG_GIAY,
            this.ItemForNGAY_QD});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(774, 456);
            // 
            // ItemForHS_DG_GIAY
            // 
            this.ItemForHS_DG_GIAY.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForHS_DG_GIAY.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForHS_DG_GIAY.Control = this.HS_DG_GIAYTextEdit;
            this.ItemForHS_DG_GIAY.Location = new System.Drawing.Point(0, 28);
            this.ItemForHS_DG_GIAY.Name = "ItemForHS_DG_GIAY";
            this.ItemForHS_DG_GIAY.Size = new System.Drawing.Size(774, 428);
            this.ItemForHS_DG_GIAY.Text = "Hệ số đơn giá";
            this.ItemForHS_DG_GIAY.TextSize = new System.Drawing.Size(116, 20);
            // 
            // ItemForNGAY_QD
            // 
            this.ItemForNGAY_QD.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForNGAY_QD.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForNGAY_QD.Control = this.NGAY_QDDateEdit;
            this.ItemForNGAY_QD.Location = new System.Drawing.Point(0, 0);
            this.ItemForNGAY_QD.Name = "ItemForNGAY_QD";
            this.ItemForNGAY_QD.Size = new System.Drawing.Size(774, 28);
            this.ItemForNGAY_QD.Text = "Ngày quyết định";
            this.ItemForNGAY_QD.TextSize = new System.Drawing.Size(116, 20);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 6.7F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 83.85F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 9.45F)});
            this.tablePanel1.Controls.Add(this.dataLayoutControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.19F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 89.81F)});
            this.tablePanel1.Size = new System.Drawing.Size(945, 530);
            this.tablePanel1.TabIndex = 12;
            // 
            // frmEditDonGiaGiay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 608);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmEditDonGiaGiay";
            this.Text = "frmEditDonGiaGiay";
            this.Load += new System.EventHandler(this.frmEditDonGiaGiay_Load);
            this.Resize += new System.EventHandler(this.frmEditDonGiaGiay_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGAY_QDDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGAY_QDDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HS_DG_GIAYTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForHS_DG_GIAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForNGAY_QD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DateEdit NGAY_QDDateEdit;
        private DevExpress.XtraEditors.TextEdit HS_DG_GIAYTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForNGAY_QD;
        private DevExpress.XtraLayout.LayoutControlItem ItemForHS_DG_GIAY;
    }
}