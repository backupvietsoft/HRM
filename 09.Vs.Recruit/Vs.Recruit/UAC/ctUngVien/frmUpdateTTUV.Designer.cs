namespace Vs.Recruit.UAC.ctUngVien
{
    partial class frmUpdateTTUV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateTTUV));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.ID_TT_HDLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForID_TT_HD = new DevExpress.XtraEditors.LabelControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.ID_TT_HTLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ItemForID_TT_HT = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TT_HDLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TT_HTLookUpEdit.Properties)).BeginInit();
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "Print";
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 112);
            this.btnALL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnALL.Size = new System.Drawing.Size(362, 40);
            this.btnALL.TabIndex = 9;
            this.btnALL.Text = "windowsUIButtonPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // ID_TT_HDLookUpEdit
            // 
            this.tablePanel1.SetColumn(this.ID_TT_HDLookUpEdit, 2);
            this.ID_TT_HDLookUpEdit.Location = new System.Drawing.Point(185, 18);
            this.ID_TT_HDLookUpEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ID_TT_HDLookUpEdit.Name = "ID_TT_HDLookUpEdit";
            this.ID_TT_HDLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_TT_HDLookUpEdit.Properties.NullText = "";
            this.tablePanel1.SetRow(this.ID_TT_HDLookUpEdit, 1);
            this.ID_TT_HDLookUpEdit.Size = new System.Drawing.Size(162, 26);
            this.ID_TT_HDLookUpEdit.TabIndex = 2;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.ID_TT_HDLookUpEdit, conditionValidationRule2);
            // 
            // ItemForID_TT_HD
            // 
            this.tablePanel1.SetColumn(this.ItemForID_TT_HD, 1);
            this.ItemForID_TT_HD.Location = new System.Drawing.Point(15, 21);
            this.ItemForID_TT_HD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ItemForID_TT_HD.Name = "ItemForID_TT_HD";
            this.tablePanel1.SetRow(this.ItemForID_TT_HD, 1);
            this.ItemForID_TT_HD.Size = new System.Drawing.Size(65, 20);
            this.ItemForID_TT_HD.TabIndex = 0;
            this.ItemForID_TT_HD.Text = "ID_TT_HD";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F)});
            this.tablePanel1.Controls.Add(this.ItemForID_TT_HT);
            this.tablePanel1.Controls.Add(this.ItemForID_TT_HD);
            this.tablePanel1.Controls.Add(this.ID_TT_HTLookUpEdit);
            this.tablePanel1.Controls.Add(this.ID_TT_HDLookUpEdit);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 25F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 8F)});
            this.tablePanel1.Size = new System.Drawing.Size(362, 112);
            this.tablePanel1.TabIndex = 10;
            // 
            // ID_TT_HTLookUpEdit
            // 
            this.tablePanel1.SetColumn(this.ID_TT_HTLookUpEdit, 2);
            this.ID_TT_HTLookUpEdit.Location = new System.Drawing.Point(185, 56);
            this.ID_TT_HTLookUpEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ID_TT_HTLookUpEdit.Name = "ID_TT_HTLookUpEdit";
            this.ID_TT_HTLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_TT_HTLookUpEdit.Properties.NullText = "";
            this.tablePanel1.SetRow(this.ID_TT_HTLookUpEdit, 2);
            this.ID_TT_HTLookUpEdit.Size = new System.Drawing.Size(162, 26);
            this.ID_TT_HTLookUpEdit.TabIndex = 3;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.ID_TT_HTLookUpEdit, conditionValidationRule1);
            // 
            // ItemForID_TT_HT
            // 
            this.tablePanel1.SetColumn(this.ItemForID_TT_HT, 1);
            this.ItemForID_TT_HT.Location = new System.Drawing.Point(15, 59);
            this.ItemForID_TT_HT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ItemForID_TT_HT.Name = "ItemForID_TT_HT";
            this.tablePanel1.SetRow(this.ItemForID_TT_HT, 2);
            this.ItemForID_TT_HT.Size = new System.Drawing.Size(62, 20);
            this.ItemForID_TT_HT.TabIndex = 1;
            this.ItemForID_TT_HT.Text = "ID_TT_HT";
            // 
            // frmUpdateTTUV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 152);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmUpdateTTUV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmUpdateTTUV";
            this.Load += new System.EventHandler(this.frmUpdateTTUV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TT_HDLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TT_HTLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.LabelControl ItemForID_TT_HD;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LookUpEdit ID_TT_HDLookUpEdit;
        private DevExpress.XtraEditors.LabelControl ItemForID_TT_HT;
        private DevExpress.XtraEditors.LookUpEdit ID_TT_HTLookUpEdit;
    }
}