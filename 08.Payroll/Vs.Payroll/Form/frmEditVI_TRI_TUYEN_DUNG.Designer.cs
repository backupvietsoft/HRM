namespace Vs.Payroll
{
    partial class frmEditVI_TRI_TUYEN_DUNG
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.cboID_CV = new DevExpress.XtraEditors.LookUpEdit();
            this.txtVTTD = new DevExpress.XtraEditors.TextEdit();
            this.txtVTTD_A = new DevExpress.XtraEditors.TextEdit();
            this.txtVTTD_H = new DevExpress.XtraEditors.TextEdit();
            this.txtMS_VTTD = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForVTTD = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForVTTD_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForVTTD_H = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMS_VTTD = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForID_CV = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.dxValidationProvider11 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_CV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD_A.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD_H.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMS_VTTD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_VTTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_CV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).BeginInit();
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
            this.btnALL.Location = new System.Drawing.Point(0, 246);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.btnALL.Size = new System.Drawing.Size(678, 26);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.cboID_CV);
            this.dataLayoutControl1.Controls.Add(this.txtVTTD);
            this.dataLayoutControl1.Controls.Add(this.txtVTTD_A);
            this.dataLayoutControl1.Controls.Add(this.txtVTTD_H);
            this.dataLayoutControl1.Controls.Add(this.txtMS_VTTD);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(48, 28);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(630, 281, 650, 400);
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(563, 215);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // cboID_CV
            // 
            this.cboID_CV.Location = new System.Drawing.Point(145, 95);
            this.cboID_CV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboID_CV.Name = "cboID_CV";
            this.cboID_CV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_CV.Properties.NullText = "";
            this.cboID_CV.Size = new System.Drawing.Size(410, 20);
            this.cboID_CV.StyleController = this.dataLayoutControl1;
            this.cboID_CV.TabIndex = 8;
            // 
            // txtVTTD
            // 
            this.txtVTTD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVTTD.Location = new System.Drawing.Point(145, 29);
            this.txtVTTD.Name = "txtVTTD";
            this.txtVTTD.Size = new System.Drawing.Size(410, 20);
            this.txtVTTD.StyleController = this.dataLayoutControl1;
            this.txtVTTD.TabIndex = 7;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider11.SetValidationRule(this.txtVTTD, conditionValidationRule1);
            // 
            // txtVTTD_A
            // 
            this.txtVTTD_A.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVTTD_A.Location = new System.Drawing.Point(145, 51);
            this.txtVTTD_A.Name = "txtVTTD_A";
            this.txtVTTD_A.Size = new System.Drawing.Size(410, 20);
            this.txtVTTD_A.StyleController = this.dataLayoutControl1;
            this.txtVTTD_A.TabIndex = 7;
            // 
            // txtVTTD_H
            // 
            this.txtVTTD_H.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtVTTD_H.Location = new System.Drawing.Point(145, 73);
            this.txtVTTD_H.Name = "txtVTTD_H";
            this.txtVTTD_H.Size = new System.Drawing.Size(410, 20);
            this.txtVTTD_H.StyleController = this.dataLayoutControl1;
            this.txtVTTD_H.TabIndex = 7;
            // 
            // txtMS_VTTD
            // 
            this.txtMS_VTTD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMS_VTTD.Location = new System.Drawing.Point(145, 7);
            this.txtMS_VTTD.Name = "txtMS_VTTD";
            this.txtMS_VTTD.Size = new System.Drawing.Size(410, 20);
            this.txtMS_VTTD.StyleController = this.dataLayoutControl1;
            this.txtMS_VTTD.TabIndex = 7;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider11.SetValidationRule(this.txtMS_VTTD, conditionValidationRule2);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(563, 215);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForVTTD,
            this.ItemForVTTD_A,
            this.ItemForVTTD_H,
            this.ItemForMS_VTTD,
            this.ItemForID_CV});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(549, 203);
            // 
            // ItemForVTTD
            // 
            this.ItemForVTTD.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemForVTTD.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForVTTD.Control = this.txtVTTD;
            this.ItemForVTTD.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForVTTD.CustomizationFormText = "Hệ số đơn giá";
            this.ItemForVTTD.Location = new System.Drawing.Point(0, 22);
            this.ItemForVTTD.Name = "ItemForVTTD";
            this.ItemForVTTD.Size = new System.Drawing.Size(549, 22);
            this.ItemForVTTD.Text = "Kiểu công việc";
            this.ItemForVTTD.TextSize = new System.Drawing.Size(135, 20);
            // 
            // ItemForVTTD_A
            // 
            this.ItemForVTTD_A.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemForVTTD_A.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForVTTD_A.Control = this.txtVTTD_A;
            this.ItemForVTTD_A.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForVTTD_A.Location = new System.Drawing.Point(0, 44);
            this.ItemForVTTD_A.Name = "ItemForVTTD_A";
            this.ItemForVTTD_A.Size = new System.Drawing.Size(549, 22);
            this.ItemForVTTD_A.Text = "Kiểu công việc (Eng)";
            this.ItemForVTTD_A.TextSize = new System.Drawing.Size(135, 20);
            // 
            // ItemForVTTD_H
            // 
            this.ItemForVTTD_H.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemForVTTD_H.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForVTTD_H.Control = this.txtVTTD_H;
            this.ItemForVTTD_H.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForVTTD_H.Location = new System.Drawing.Point(0, 66);
            this.ItemForVTTD_H.Name = "ItemForVTTD_H";
            this.ItemForVTTD_H.Size = new System.Drawing.Size(549, 22);
            this.ItemForVTTD_H.Text = "Kiểu công việc (Ch)";
            this.ItemForVTTD_H.TextSize = new System.Drawing.Size(135, 20);
            // 
            // ItemForMS_VTTD
            // 
            this.ItemForMS_VTTD.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemForMS_VTTD.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForMS_VTTD.Control = this.txtMS_VTTD;
            this.ItemForMS_VTTD.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForMS_VTTD.CustomizationFormText = "MS kiểu công việc";
            this.ItemForMS_VTTD.Location = new System.Drawing.Point(0, 0);
            this.ItemForMS_VTTD.Name = "ItemForMS_VTTD";
            this.ItemForMS_VTTD.Size = new System.Drawing.Size(549, 22);
            this.ItemForMS_VTTD.Text = "MS kiểu công việc";
            this.ItemForMS_VTTD.TextSize = new System.Drawing.Size(135, 20);
            // 
            // ItemForID_CV
            // 
            this.ItemForID_CV.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemForID_CV.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForID_CV.Control = this.cboID_CV;
            this.ItemForID_CV.Location = new System.Drawing.Point(0, 88);
            this.ItemForID_CV.Name = "ItemForID_CV";
            this.ItemForID_CV.Size = new System.Drawing.Size(549, 115);
            this.ItemForID_CV.TextSize = new System.Drawing.Size(135, 20);
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
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10.19F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 89.81F)});
            this.tablePanel1.Size = new System.Drawing.Size(678, 246);
            this.tablePanel1.TabIndex = 12;
            // 
            // frmEditVI_TRI_TUYEN_DUNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 272);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmEditVI_TRI_TUYEN_DUNG";
            this.Text = "frmEditVI_TRI_TUYEN_DUNG";
            this.Load += new System.EventHandler(this.frmEditVI_TRI_TUYEN_DUNG_Load);
            this.Resize += new System.EventHandler(this.frmEditVI_TRI_TUYEN_DUNG_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboID_CV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD_A.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVTTD_H.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMS_VTTD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForVTTD_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_VTTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_CV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtVTTD;
        private DevExpress.XtraEditors.TextEdit txtVTTD_A;
        private DevExpress.XtraLayout.LayoutControlItem ItemForVTTD;
        private DevExpress.XtraLayout.LayoutControlItem ItemForVTTD_A;
        private DevExpress.XtraEditors.TextEdit txtVTTD_H;
        private DevExpress.XtraLayout.LayoutControlItem ItemForVTTD_H;
        private DevExpress.XtraEditors.TextEdit txtMS_VTTD;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider11;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMS_VTTD;
        private DevExpress.XtraEditors.LookUpEdit cboID_CV;
        private DevExpress.XtraLayout.LayoutControlItem ItemForID_CV;
    }
}