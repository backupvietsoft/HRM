namespace Vs.Payroll
{
    partial class frmEditHSBT
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
            this.HSBTTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.TEN_BAC_THO_HTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_BAC_THO_ATextEdit = new DevExpress.XtraEditors.TextEdit();
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.TEN_BAC_THOTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForHSBT = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_BAC_THO = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSTT = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_BAC_THO_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_BAC_THO_H = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HSBTTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THO_HTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THO_ATextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THOTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForHSBT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 321);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(785, 34);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // HSBTTextEdit
            // 
            this.HSBTTextEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HSBTTextEdit.Location = new System.Drawing.Point(150, 84);
            this.HSBTTextEdit.Margin = new System.Windows.Forms.Padding(4);
            this.HSBTTextEdit.Name = "HSBTTextEdit";
            this.HSBTTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.HSBTTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.HSBTTextEdit.Properties.Mask.EditMask = "n2";
            this.HSBTTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.HSBTTextEdit.Size = new System.Drawing.Size(159, 24);
            this.HSBTTextEdit.StyleController = this.dataLayoutControl1;
            this.HSBTTextEdit.TabIndex = 7;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.HSBTTextEdit, conditionValidationRule2);
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.TEN_BAC_THO_HTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_BAC_THO_ATextEdit);
            this.dataLayoutControl1.Controls.Add(this.txtSTT);
            this.dataLayoutControl1.Controls.Add(this.HSBTTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_BAC_THOTextEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(83, 20);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(630, 281, 650, 400);
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(620, 297);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TEN_BAC_THO_HTextEdit
            // 
            this.TEN_BAC_THO_HTextEdit.Location = new System.Drawing.Point(150, 58);
            this.TEN_BAC_THO_HTextEdit.Name = "TEN_BAC_THO_HTextEdit";
            this.TEN_BAC_THO_HTextEdit.Size = new System.Drawing.Size(464, 24);
            this.TEN_BAC_THO_HTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_BAC_THO_HTextEdit.TabIndex = 10;
            // 
            // TEN_BAC_THO_ATextEdit
            // 
            this.TEN_BAC_THO_ATextEdit.Location = new System.Drawing.Point(150, 32);
            this.TEN_BAC_THO_ATextEdit.Name = "TEN_BAC_THO_ATextEdit";
            this.TEN_BAC_THO_ATextEdit.Size = new System.Drawing.Size(464, 24);
            this.TEN_BAC_THO_ATextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_BAC_THO_ATextEdit.TabIndex = 9;
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(455, 84);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSTT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSTT.Properties.Mask.EditMask = "N0";
            this.txtSTT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSTT.Size = new System.Drawing.Size(159, 24);
            this.txtSTT.StyleController = this.dataLayoutControl1;
            this.txtSTT.TabIndex = 8;
            // 
            // TEN_BAC_THOTextEdit
            // 
            this.TEN_BAC_THOTextEdit.Location = new System.Drawing.Point(150, 6);
            this.TEN_BAC_THOTextEdit.Margin = new System.Windows.Forms.Padding(4);
            this.TEN_BAC_THOTextEdit.Name = "TEN_BAC_THOTextEdit";
            this.TEN_BAC_THOTextEdit.Properties.DisplayFormat.FormatString = "d";
            this.TEN_BAC_THOTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TEN_BAC_THOTextEdit.Properties.EditFormat.FormatString = "d";
            this.TEN_BAC_THOTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TEN_BAC_THOTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TEN_BAC_THOTextEdit.Size = new System.Drawing.Size(464, 24);
            this.TEN_BAC_THOTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_BAC_THOTextEdit.TabIndex = 6;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.TEN_BAC_THOTextEdit, conditionValidationRule1);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(620, 297);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForHSBT,
            this.ItemForTEN_BAC_THO,
            this.ItemForSTT,
            this.ItemForTEN_BAC_THO_A,
            this.ItemForTEN_BAC_THO_H});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(610, 287);
            // 
            // ItemForHSBT
            // 
            this.ItemForHSBT.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ItemForHSBT.AppearanceItemCaption.Options.UseForeColor = true;
            this.ItemForHSBT.Control = this.HSBTTextEdit;
            this.ItemForHSBT.Location = new System.Drawing.Point(0, 78);
            this.ItemForHSBT.Name = "ItemForHSBT";
            this.ItemForHSBT.Size = new System.Drawing.Size(305, 209);
            this.ItemForHSBT.Text = "Hệ số BT";
            this.ItemForHSBT.TextSize = new System.Drawing.Size(141, 17);
            // 
            // ItemForTEN_BAC_THO
            // 
            this.ItemForTEN_BAC_THO.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ItemForTEN_BAC_THO.AppearanceItemCaption.Options.UseForeColor = true;
            this.ItemForTEN_BAC_THO.Control = this.TEN_BAC_THOTextEdit;
            this.ItemForTEN_BAC_THO.Location = new System.Drawing.Point(0, 0);
            this.ItemForTEN_BAC_THO.Name = "ItemForTEN_BAC_THO";
            this.ItemForTEN_BAC_THO.Size = new System.Drawing.Size(610, 26);
            this.ItemForTEN_BAC_THO.Text = "TEN_BAC_THO";
            this.ItemForTEN_BAC_THO.TextSize = new System.Drawing.Size(141, 17);
            // 
            // ItemForSTT
            // 
            this.ItemForSTT.Control = this.txtSTT;
            this.ItemForSTT.Location = new System.Drawing.Point(305, 78);
            this.ItemForSTT.Name = "ItemForSTT";
            this.ItemForSTT.Size = new System.Drawing.Size(305, 209);
            this.ItemForSTT.TextSize = new System.Drawing.Size(141, 17);
            // 
            // ItemForTEN_BAC_THO_A
            // 
            this.ItemForTEN_BAC_THO_A.Control = this.TEN_BAC_THO_ATextEdit;
            this.ItemForTEN_BAC_THO_A.Location = new System.Drawing.Point(0, 26);
            this.ItemForTEN_BAC_THO_A.Name = "ItemForTEN_BAC_THO_A";
            this.ItemForTEN_BAC_THO_A.Size = new System.Drawing.Size(610, 26);
            this.ItemForTEN_BAC_THO_A.TextSize = new System.Drawing.Size(141, 17);
            // 
            // ItemForTEN_BAC_THO_H
            // 
            this.ItemForTEN_BAC_THO_H.Control = this.TEN_BAC_THO_HTextEdit;
            this.ItemForTEN_BAC_THO_H.Location = new System.Drawing.Point(0, 52);
            this.ItemForTEN_BAC_THO_H.Name = "ItemForTEN_BAC_THO_H";
            this.ItemForTEN_BAC_THO_H.Size = new System.Drawing.Size(610, 26);
            this.ItemForTEN_BAC_THO_H.TextSize = new System.Drawing.Size(141, 17);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.tablePanel1.Controls.Add(this.dataLayoutControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 95F)});
            this.tablePanel1.Size = new System.Drawing.Size(785, 321);
            this.tablePanel1.TabIndex = 12;
            // 
            // frmEditHSBT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 355);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEditHSBT";
            this.Text = "frmEditHSBT";
            this.Load += new System.EventHandler(this.frmEditHSBT_Load);
            this.Resize += new System.EventHandler(this.frmEditHSBT_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HSBTTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THO_HTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THO_ATextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_BAC_THOTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForHSBT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_BAC_THO_H)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit HSBTTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_BAC_THO;
        private DevExpress.XtraLayout.LayoutControlItem ItemForHSBT;
        private DevExpress.XtraEditors.TextEdit TEN_BAC_THOTextEdit;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT;
        private DevExpress.XtraEditors.TextEdit TEN_BAC_THO_HTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_BAC_THO_ATextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_BAC_THO_A;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_BAC_THO_H;
    }
}