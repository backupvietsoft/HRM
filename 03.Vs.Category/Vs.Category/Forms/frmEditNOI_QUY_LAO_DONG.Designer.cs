namespace Vs.Category
{
    partial class frmEditNOI_QUY_LAO_DONG
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
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.txtNoiDung_H = new DevExpress.XtraEditors.TextEdit();
            this.txtNoiDung_A = new DevExpress.XtraEditors.TextEdit();
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.txtNoiDung = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblNoiDung = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSTT = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNoiDung_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNoiDung_H = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung_H.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung_A.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
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
            this.btnALL.Location = new System.Drawing.Point(0, 281);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(709, 34);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
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
            this.tablePanel1.Size = new System.Drawing.Size(709, 315);
            this.tablePanel1.TabIndex = 10;
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.txtNoiDung_H);
            this.dataLayoutControl1.Controls.Add(this.txtNoiDung_A);
            this.dataLayoutControl1.Controls.Add(this.txtSTT);
            this.dataLayoutControl1.Controls.Add(this.txtNoiDung);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(75, 20);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(559, 291);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // txtNoiDung_H
            // 
            this.txtNoiDung_H.Location = new System.Drawing.Point(89, 58);
            this.txtNoiDung_H.Name = "txtNoiDung_H";
            this.txtNoiDung_H.Size = new System.Drawing.Size(464, 24);
            this.txtNoiDung_H.StyleController = this.dataLayoutControl1;
            this.txtNoiDung_H.TabIndex = 7;
            // 
            // txtNoiDung_A
            // 
            this.txtNoiDung_A.Location = new System.Drawing.Point(89, 32);
            this.txtNoiDung_A.Name = "txtNoiDung_A";
            this.txtNoiDung_A.Size = new System.Drawing.Size(464, 24);
            this.txtNoiDung_A.StyleController = this.dataLayoutControl1;
            this.txtNoiDung_A.TabIndex = 6;
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(89, 84);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSTT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSTT.Properties.Mask.EditMask = "N0";
            this.txtSTT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSTT.Size = new System.Drawing.Size(464, 24);
            this.txtSTT.StyleController = this.dataLayoutControl1;
            this.txtSTT.TabIndex = 5;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(89, 6);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(464, 24);
            this.txtNoiDung.StyleController = this.dataLayoutControl1;
            this.txtNoiDung.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.txtNoiDung, conditionValidationRule1);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(559, 291);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblNoiDung,
            this.ItemForSTT,
            this.lblNoiDung_A,
            this.lblNoiDung_H});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(549, 281);
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoiDung.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNoiDung.AppearanceItemCaption.Options.UseFont = true;
            this.lblNoiDung.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblNoiDung.Control = this.txtNoiDung;
            this.lblNoiDung.Location = new System.Drawing.Point(0, 0);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(549, 26);
            this.lblNoiDung.TextSize = new System.Drawing.Size(80, 19);
            // 
            // ItemForSTT
            // 
            this.ItemForSTT.Control = this.txtSTT;
            this.ItemForSTT.Location = new System.Drawing.Point(0, 78);
            this.ItemForSTT.Name = "ItemForSTT";
            this.ItemForSTT.Size = new System.Drawing.Size(549, 203);
            this.ItemForSTT.TextSize = new System.Drawing.Size(80, 17);
            // 
            // lblNoiDung_A
            // 
            this.lblNoiDung_A.Control = this.txtNoiDung_A;
            this.lblNoiDung_A.Location = new System.Drawing.Point(0, 26);
            this.lblNoiDung_A.Name = "lblNoiDung_A";
            this.lblNoiDung_A.Size = new System.Drawing.Size(549, 26);
            this.lblNoiDung_A.TextSize = new System.Drawing.Size(80, 17);
            // 
            // lblNoiDung_H
            // 
            this.lblNoiDung_H.Control = this.txtNoiDung_H;
            this.lblNoiDung_H.Location = new System.Drawing.Point(0, 52);
            this.lblNoiDung_H.Name = "lblNoiDung_H";
            this.lblNoiDung_H.Size = new System.Drawing.Size(549, 26);
            this.lblNoiDung_H.TextSize = new System.Drawing.Size(80, 17);
            // 
            // frmEditNOI_QUY_LAO_DONG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 315);
            this.Controls.Add(this.btnALL);
            this.Controls.Add(this.tablePanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEditNOI_QUY_LAO_DONG";
            this.Text = "frmEditNOI_QUY_LAO_DONG";
            this.Load += new System.EventHandler(this.frmEditNOI_QUY_LAO_DONG_Load);
            this.Resize += new System.EventHandler(this.frmEditNOI_QUY_LAO_DONG_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung_H.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung_A.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNoiDung_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraLayout.LayoutControlItem lblNoiDung;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT;
        private DevExpress.XtraEditors.TextEdit txtNoiDung;
        private DevExpress.XtraEditors.TextEdit txtNoiDung_H;
        private DevExpress.XtraEditors.TextEdit txtNoiDung_A;
        private DevExpress.XtraLayout.LayoutControlItem lblNoiDung_A;
        private DevExpress.XtraLayout.LayoutControlItem lblNoiDung_H;
    }
}