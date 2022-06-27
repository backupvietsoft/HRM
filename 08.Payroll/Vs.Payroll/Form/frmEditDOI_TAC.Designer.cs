namespace Vs.Payroll
{
    partial class frmEditDOI_TAC
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.txtTEN_CTY_DAY_DU = new DevExpress.XtraEditors.TextEdit();
            this.txtTEN_NGAN = new DevExpress.XtraEditors.TextEdit();
            this.txtMA_SO = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblMA_SO = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTEN_NGAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTEN_CTY_DAY_DU = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSTT = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTEN_CTY_DAY_DU.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTEN_NGAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMA_SO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMA_SO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTEN_NGAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTEN_CTY_DAY_DU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 80F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.tablePanel1.Controls.Add(this.btnALL);
            this.tablePanel1.Controls.Add(this.dataLayoutControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 90F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 46F)});
            this.tablePanel1.Size = new System.Drawing.Size(800, 450);
            this.tablePanel1.TabIndex = 2;
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
            windowsUIButtonImageOptions3.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions4.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "huy", -1, false)});
            this.tablePanel1.SetColumn(this.btnALL, 0);
            this.tablePanel1.SetColumnSpan(this.btnALL, 3);
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(3, 407);
            this.btnALL.Name = "btnALL";
            this.tablePanel1.SetRow(this.btnALL, 2);
            this.btnALL.Size = new System.Drawing.Size(794, 40);
            this.btnALL.TabIndex = 11;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.txtSTT);
            this.dataLayoutControl1.Controls.Add(this.txtTEN_CTY_DAY_DU);
            this.dataLayoutControl1.Controls.Add(this.txtTEN_NGAN);
            this.dataLayoutControl1.Controls.Add(this.txtMA_SO);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(83, 24);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(634, 377);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(126, 84);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSTT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSTT.Properties.Mask.EditMask = "N0";
            this.txtSTT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSTT.Size = new System.Drawing.Size(502, 24);
            this.txtSTT.StyleController = this.dataLayoutControl1;
            this.txtSTT.TabIndex = 8;
            // 
            // txtTEN_CTY_DAY_DU
            // 
            this.txtTEN_CTY_DAY_DU.Location = new System.Drawing.Point(126, 58);
            this.txtTEN_CTY_DAY_DU.Name = "txtTEN_CTY_DAY_DU";
            this.txtTEN_CTY_DAY_DU.Size = new System.Drawing.Size(502, 24);
            this.txtTEN_CTY_DAY_DU.StyleController = this.dataLayoutControl1;
            this.txtTEN_CTY_DAY_DU.TabIndex = 6;
            // 
            // txtTEN_NGAN
            // 
            this.txtTEN_NGAN.Location = new System.Drawing.Point(126, 32);
            this.txtTEN_NGAN.Name = "txtTEN_NGAN";
            this.txtTEN_NGAN.Size = new System.Drawing.Size(502, 24);
            this.txtTEN_NGAN.StyleController = this.dataLayoutControl1;
            this.txtTEN_NGAN.TabIndex = 5;
            // 
            // txtMA_SO
            // 
            this.txtMA_SO.Location = new System.Drawing.Point(126, 6);
            this.txtMA_SO.Name = "txtMA_SO";
            this.txtMA_SO.Size = new System.Drawing.Size(502, 24);
            this.txtMA_SO.StyleController = this.dataLayoutControl1;
            this.txtMA_SO.TabIndex = 4;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.txtMA_SO, conditionValidationRule2);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblMA_SO,
            this.lblTEN_NGAN,
            this.lblTEN_CTY_DAY_DU,
            this.ItemForSTT});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(634, 377);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblMA_SO
            // 
            this.lblMA_SO.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMA_SO.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMA_SO.AppearanceItemCaption.Options.UseFont = true;
            this.lblMA_SO.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblMA_SO.Control = this.txtMA_SO;
            this.lblMA_SO.Location = new System.Drawing.Point(0, 0);
            this.lblMA_SO.Name = "lblMA_SO";
            this.lblMA_SO.Size = new System.Drawing.Size(624, 26);
            this.lblMA_SO.TextSize = new System.Drawing.Size(117, 20);
            // 
            // lblTEN_NGAN
            // 
            this.lblTEN_NGAN.Control = this.txtTEN_NGAN;
            this.lblTEN_NGAN.Location = new System.Drawing.Point(0, 26);
            this.lblTEN_NGAN.Name = "lblTEN_NGAN";
            this.lblTEN_NGAN.Size = new System.Drawing.Size(624, 26);
            this.lblTEN_NGAN.TextSize = new System.Drawing.Size(117, 17);
            // 
            // lblTEN_CTY_DAY_DU
            // 
            this.lblTEN_CTY_DAY_DU.Control = this.txtTEN_CTY_DAY_DU;
            this.lblTEN_CTY_DAY_DU.Location = new System.Drawing.Point(0, 52);
            this.lblTEN_CTY_DAY_DU.Name = "lblTEN_CTY_DAY_DU";
            this.lblTEN_CTY_DAY_DU.Size = new System.Drawing.Size(624, 26);
            this.lblTEN_CTY_DAY_DU.TextSize = new System.Drawing.Size(117, 17);
            // 
            // ItemForSTT
            // 
            this.ItemForSTT.Control = this.txtSTT;
            this.ItemForSTT.Location = new System.Drawing.Point(0, 78);
            this.ItemForSTT.Name = "ItemForSTT";
            this.ItemForSTT.Size = new System.Drawing.Size(624, 289);
            this.ItemForSTT.TextSize = new System.Drawing.Size(117, 17);
            // 
            // frmEditDOI_TAC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tablePanel1);
            this.Name = "frmEditDOI_TAC";
            this.Text = "frmEditDOI_TAC";
            this.Load += new System.EventHandler(this.frmEditDOI_TAC_Load);
            this.Resize += new System.EventHandler(this.frmEditDOI_TAC_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTEN_CTY_DAY_DU.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTEN_NGAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMA_SO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMA_SO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTEN_NGAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTEN_CTY_DAY_DU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.TextEdit txtTEN_CTY_DAY_DU;
        private DevExpress.XtraEditors.TextEdit txtTEN_NGAN;
        private DevExpress.XtraEditors.TextEdit txtMA_SO;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblMA_SO;
        private DevExpress.XtraLayout.LayoutControlItem lblTEN_NGAN;
        private DevExpress.XtraLayout.LayoutControlItem lblTEN_CTY_DAY_DU;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
    }
}