namespace Vs.Category
{
    partial class frmEditDS_KIP
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
            this.TEN_KIPTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_KIP_ATextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_KIP_BTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lbltenkip = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_KIP_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_KIP_B = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIPTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIP_ATextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIP_BTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltenkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_KIP_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_KIP_B)).BeginInit();
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
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "huy", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 423);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.btnALL.Size = new System.Drawing.Size(842, 40);
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
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 90F)});
            this.tablePanel1.Size = new System.Drawing.Size(842, 463);
            this.tablePanel1.TabIndex = 10;
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.TEN_KIPTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_KIP_ATextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_KIP_BTextEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(88, 51);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(666, 407);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TEN_KIPTextEdit
            // 
            this.TEN_KIPTextEdit.Location = new System.Drawing.Point(105, 6);
            this.TEN_KIPTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_KIPTextEdit.Name = "TEN_KIPTextEdit";
            this.TEN_KIPTextEdit.Size = new System.Drawing.Size(555, 26);
            this.TEN_KIPTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_KIPTextEdit.TabIndex = 5;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.TEN_KIPTextEdit, conditionValidationRule1);
            // 
            // TEN_KIP_ATextEdit
            // 
            this.TEN_KIP_ATextEdit.Location = new System.Drawing.Point(105, 34);
            this.TEN_KIP_ATextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_KIP_ATextEdit.Name = "TEN_KIP_ATextEdit";
            this.TEN_KIP_ATextEdit.Size = new System.Drawing.Size(555, 26);
            this.TEN_KIP_ATextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_KIP_ATextEdit.TabIndex = 6;
            // 
            // TEN_KIP_BTextEdit
            // 
            this.TEN_KIP_BTextEdit.Location = new System.Drawing.Point(105, 62);
            this.TEN_KIP_BTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_KIP_BTextEdit.Name = "TEN_KIP_BTextEdit";
            this.TEN_KIP_BTextEdit.Size = new System.Drawing.Size(555, 26);
            this.TEN_KIP_BTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_KIP_BTextEdit.TabIndex = 7;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(666, 407);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lbltenkip,
            this.ItemForTEN_KIP_A,
            this.ItemForTEN_KIP_B});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(656, 397);
            // 
            // lbltenkip
            // 
            this.lbltenkip.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.lbltenkip.AppearanceItemCaption.Options.UseFont = true;
            this.lbltenkip.Control = this.TEN_KIPTextEdit;
            this.lbltenkip.Location = new System.Drawing.Point(0, 0);
            this.lbltenkip.Name = "lbltenkip";
            this.lbltenkip.Size = new System.Drawing.Size(656, 28);
            this.lbltenkip.Text = "TEN_NHOM";
            this.lbltenkip.TextSize = new System.Drawing.Size(96, 20);
            // 
            // ItemForTEN_KIP_A
            // 
            this.ItemForTEN_KIP_A.Control = this.TEN_KIP_ATextEdit;
            this.ItemForTEN_KIP_A.Location = new System.Drawing.Point(0, 28);
            this.ItemForTEN_KIP_A.Name = "ItemForTEN_KIP_A";
            this.ItemForTEN_KIP_A.Size = new System.Drawing.Size(656, 28);
            this.ItemForTEN_KIP_A.Text = "TEN_NHOM_A";
            this.ItemForTEN_KIP_A.TextSize = new System.Drawing.Size(96, 20);
            // 
            // ItemForTEN_KIP_B
            // 
            this.ItemForTEN_KIP_B.Control = this.TEN_KIP_BTextEdit;
            this.ItemForTEN_KIP_B.Location = new System.Drawing.Point(0, 56);
            this.ItemForTEN_KIP_B.Name = "ItemForTEN_KIP_B";
            this.ItemForTEN_KIP_B.Size = new System.Drawing.Size(656, 341);
            this.ItemForTEN_KIP_B.Text = "TEN_NHOM_H";
            this.ItemForTEN_KIP_B.TextSize = new System.Drawing.Size(96, 20);
            // 
            // frmEditDS_KIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 463);
            this.Controls.Add(this.btnALL);
            this.Controls.Add(this.tablePanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmEditDS_KIP";
            this.Text = "frmEditDS_KIP";
            this.Load += new System.EventHandler(this.frmEditDS_KIP_Load);
            this.Resize += new System.EventHandler(this.frmEditDS_KIP_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIPTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIP_ATextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_KIP_BTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltenkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_KIP_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_KIP_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.TextEdit TEN_KIPTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_KIP_ATextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_KIP_BTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lbltenkip;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_KIP_A;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_KIP_B;
    }
}