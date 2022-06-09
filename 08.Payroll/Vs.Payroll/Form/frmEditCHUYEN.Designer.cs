namespace Vs.Payroll
{
    partial class frmEditCHUYEN
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.TEN_CHUYENTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TOLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.STT_CHUYENTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTO = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_CHUYEN = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSTT_CHUYEN = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.dxValidationProvider11 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.ItemForSTT = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_CHUYENTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.STT_CHUYENTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_CHUYEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT_CHUYEN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.txtSTT);
            this.dataLayoutControl1.Controls.Add(this.TEN_CHUYENTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TOLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.STT_CHUYENTextEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(84, 44);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(644, 366);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TEN_CHUYENTextEdit
            // 
            this.TEN_CHUYENTextEdit.Location = new System.Drawing.Point(110, 40);
            this.TEN_CHUYENTextEdit.Name = "TEN_CHUYENTextEdit";
            this.TEN_CHUYENTextEdit.Size = new System.Drawing.Size(522, 26);
            this.TEN_CHUYENTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_CHUYENTextEdit.TabIndex = 7;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.Value1 = "@";
            this.dxValidationProvider1.SetValidationRule(this.TEN_CHUYENTextEdit, conditionValidationRule1);
            // 
            // TOLookUpEdit
            // 
            this.TOLookUpEdit.EditValue = "[Not null]";
            this.TOLookUpEdit.Location = new System.Drawing.Point(110, 72);
            this.TOLookUpEdit.Name = "TOLookUpEdit";
            this.TOLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TOLookUpEdit.Properties.NullText = "";
            this.TOLookUpEdit.Size = new System.Drawing.Size(522, 26);
            this.TOLookUpEdit.StyleController = this.dataLayoutControl1;
            this.TOLookUpEdit.TabIndex = 11;
            // 
            // STT_CHUYENTextEdit
            // 
            this.STT_CHUYENTextEdit.Location = new System.Drawing.Point(110, 8);
            this.STT_CHUYENTextEdit.Name = "STT_CHUYENTextEdit";
            this.STT_CHUYENTextEdit.Size = new System.Drawing.Size(522, 26);
            this.STT_CHUYENTextEdit.StyleController = this.dataLayoutControl1;
            this.STT_CHUYENTextEdit.TabIndex = 7;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.STT_CHUYENTextEdit, conditionValidationRule2);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(644, 366);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTO,
            this.ItemForTEN_CHUYEN,
            this.ItemForSTT_CHUYEN,
            this.ItemForSTT});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(634, 356);
            // 
            // ItemForTO
            // 
            this.ItemForTO.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForTO.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTO.Control = this.TOLookUpEdit;
            this.ItemForTO.Location = new System.Drawing.Point(0, 64);
            this.ItemForTO.Name = "ItemForTO";
            this.ItemForTO.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForTO.Size = new System.Drawing.Size(634, 32);
            this.ItemForTO.Text = "TO";
            this.ItemForTO.TextSize = new System.Drawing.Size(95, 20);
            // 
            // ItemForTEN_CHUYEN
            // 
            this.ItemForTEN_CHUYEN.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForTEN_CHUYEN.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTEN_CHUYEN.Control = this.TEN_CHUYENTextEdit;
            this.ItemForTEN_CHUYEN.Location = new System.Drawing.Point(0, 32);
            this.ItemForTEN_CHUYEN.Name = "ItemForTEN_CHUYEN";
            this.ItemForTEN_CHUYEN.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForTEN_CHUYEN.Size = new System.Drawing.Size(634, 32);
            this.ItemForTEN_CHUYEN.Text = "TEN_CHUYEN";
            this.ItemForTEN_CHUYEN.TextSize = new System.Drawing.Size(95, 20);
            // 
            // ItemForSTT_CHUYEN
            // 
            this.ItemForSTT_CHUYEN.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.ItemForSTT_CHUYEN.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForSTT_CHUYEN.Control = this.STT_CHUYENTextEdit;
            this.ItemForSTT_CHUYEN.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.ItemForSTT_CHUYEN.CustomizationFormText = "TEN_CHUYEN";
            this.ItemForSTT_CHUYEN.Location = new System.Drawing.Point(0, 0);
            this.ItemForSTT_CHUYEN.Name = "ItemForSTT_CHUYEN";
            this.ItemForSTT_CHUYEN.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForSTT_CHUYEN.Size = new System.Drawing.Size(634, 32);
            this.ItemForSTT_CHUYEN.Text = "STT_CHUYEN";
            this.ItemForSTT_CHUYEN.TextSize = new System.Drawing.Size(95, 20);
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
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 90F)});
            this.tablePanel1.Size = new System.Drawing.Size(813, 413);
            this.tablePanel1.TabIndex = 7;
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
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "huy", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 413);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.btnALL.Size = new System.Drawing.Size(813, 40);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel2_ButtonClick);
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(104, 102);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(534, 26);
            this.txtSTT.StyleController = this.dataLayoutControl1;
            this.txtSTT.TabIndex = 12;
            // 
            // ItemForSTT
            // 
            this.ItemForSTT.Control = this.txtSTT;
            this.ItemForSTT.Location = new System.Drawing.Point(0, 96);
            this.ItemForSTT.Name = "ItemForSTT";
            this.ItemForSTT.Size = new System.Drawing.Size(634, 260);
            this.ItemForSTT.TextSize = new System.Drawing.Size(95, 20);
            // 
            // frmEditCHUYEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 453);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmEditCHUYEN";
            this.Load += new System.EventHandler(this.frmEditCHUYEN_Load);
            this.Resize += new System.EventHandler(this.frmEditCHUYEN_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TEN_CHUYENTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.STT_CHUYENTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_CHUYEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT_CHUYEN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit TEN_CHUYENTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_CHUYEN;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTO;
        private DevExpress.XtraEditors.LookUpEdit TOLookUpEdit;
        private DevExpress.XtraEditors.TextEdit STT_CHUYENTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT_CHUYEN;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider11;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT;
    }
}
