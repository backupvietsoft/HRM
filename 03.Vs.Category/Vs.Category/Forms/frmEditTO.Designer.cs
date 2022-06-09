namespace Vs.Category
{
    partial class frmEditTO
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.ID_XNLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MS_TOTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_TOTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_TO_ANHTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_TO_HOATextEdit = new DevExpress.XtraEditors.TextEdit();
            this.STT_TOTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTEN_TO = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_TO_ANH = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_TO_HOA = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForID_XN = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMS_TO = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForSTT_TO = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ID_XNLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MS_TOTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TOTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TO_ANHTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TO_HOATextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.STT_TOTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO_ANH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO_HOA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_XN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_TO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT_TO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.ID_XNLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.MS_TOTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_TOTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_TO_ANHTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_TO_HOATextEdit);
            this.dataLayoutControl1.Controls.Add(this.STT_TOTextEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(80, 38);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(609, 306);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // ID_XNLookUpEdit
            // 
            this.ID_XNLookUpEdit.EditValue = "[Not null]";
            this.ID_XNLookUpEdit.Location = new System.Drawing.Point(110, 13);
            this.ID_XNLookUpEdit.Name = "ID_XNLookUpEdit";
            this.ID_XNLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_XNLookUpEdit.Properties.NullText = "";
            this.ID_XNLookUpEdit.Properties.PopupView = this.searchLookUpEdit1View;
            this.ID_XNLookUpEdit.Size = new System.Drawing.Size(482, 26);
            this.ID_XNLookUpEdit.StyleController = this.dataLayoutControl1;
            this.ID_XNLookUpEdit.TabIndex = 11;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.Value1 = "@";
            conditionValidationRule1.Value2 = "";
            this.dxValidationProvider1.SetValidationRule(this.ID_XNLookUpEdit, conditionValidationRule1);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 349;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // MS_TOTextEdit
            // 
            this.MS_TOTextEdit.Location = new System.Drawing.Point(110, 45);
            this.MS_TOTextEdit.Name = "MS_TOTextEdit";
            this.MS_TOTextEdit.Size = new System.Drawing.Size(187, 26);
            this.MS_TOTextEdit.StyleController = this.dataLayoutControl1;
            this.MS_TOTextEdit.TabIndex = 6;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.Value1 = "@";
            this.dxValidationProvider1.SetValidationRule(this.MS_TOTextEdit, conditionValidationRule2);
            // 
            // TEN_TOTextEdit
            // 
            this.TEN_TOTextEdit.Location = new System.Drawing.Point(110, 77);
            this.TEN_TOTextEdit.Name = "TEN_TOTextEdit";
            this.TEN_TOTextEdit.Size = new System.Drawing.Size(482, 26);
            this.TEN_TOTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TOTextEdit.TabIndex = 7;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            conditionValidationRule3.Value1 = "@";
            this.dxValidationProvider1.SetValidationRule(this.TEN_TOTextEdit, conditionValidationRule3);
            // 
            // TEN_TO_ANHTextEdit
            // 
            this.TEN_TO_ANHTextEdit.Location = new System.Drawing.Point(110, 109);
            this.TEN_TO_ANHTextEdit.Name = "TEN_TO_ANHTextEdit";
            this.TEN_TO_ANHTextEdit.Size = new System.Drawing.Size(482, 26);
            this.TEN_TO_ANHTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TO_ANHTextEdit.TabIndex = 8;
            // 
            // TEN_TO_HOATextEdit
            // 
            this.TEN_TO_HOATextEdit.Location = new System.Drawing.Point(110, 141);
            this.TEN_TO_HOATextEdit.Name = "TEN_TO_HOATextEdit";
            this.TEN_TO_HOATextEdit.Size = new System.Drawing.Size(482, 26);
            this.TEN_TO_HOATextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TO_HOATextEdit.TabIndex = 9;
            // 
            // STT_TOTextEdit
            // 
            this.STT_TOTextEdit.Location = new System.Drawing.Point(404, 45);
            this.STT_TOTextEdit.Name = "STT_TOTextEdit";
            this.STT_TOTextEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.STT_TOTextEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.STT_TOTextEdit.Properties.Mask.EditMask = "N0";
            this.STT_TOTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.STT_TOTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.STT_TOTextEdit.Size = new System.Drawing.Size(188, 26);
            this.STT_TOTextEdit.StyleController = this.dataLayoutControl1;
            this.STT_TOTextEdit.TabIndex = 10;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(609, 306);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTEN_TO,
            this.ItemForTEN_TO_ANH,
            this.ItemForTEN_TO_HOA,
            this.ItemForID_XN,
            this.ItemForMS_TO,
            this.ItemForSTT_TO});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(589, 286);
            // 
            // ItemForTEN_TO
            // 
            this.ItemForTEN_TO.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForTEN_TO.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTEN_TO.Control = this.TEN_TOTextEdit;
            this.ItemForTEN_TO.Location = new System.Drawing.Point(0, 64);
            this.ItemForTEN_TO.Name = "ItemForTEN_TO";
            this.ItemForTEN_TO.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForTEN_TO.Size = new System.Drawing.Size(589, 32);
            this.ItemForTEN_TO.Text = "TEN_TO";
            this.ItemForTEN_TO.TextSize = new System.Drawing.Size(90, 20);
            // 
            // ItemForTEN_TO_ANH
            // 
            this.ItemForTEN_TO_ANH.Control = this.TEN_TO_ANHTextEdit;
            this.ItemForTEN_TO_ANH.Location = new System.Drawing.Point(0, 96);
            this.ItemForTEN_TO_ANH.Name = "ItemForTEN_TO_ANH";
            this.ItemForTEN_TO_ANH.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForTEN_TO_ANH.Size = new System.Drawing.Size(589, 32);
            this.ItemForTEN_TO_ANH.Text = "TEN_TO_ANH";
            this.ItemForTEN_TO_ANH.TextSize = new System.Drawing.Size(90, 20);
            // 
            // ItemForTEN_TO_HOA
            // 
            this.ItemForTEN_TO_HOA.Control = this.TEN_TO_HOATextEdit;
            this.ItemForTEN_TO_HOA.Location = new System.Drawing.Point(0, 128);
            this.ItemForTEN_TO_HOA.Name = "ItemForTEN_TO_HOA";
            this.ItemForTEN_TO_HOA.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForTEN_TO_HOA.Size = new System.Drawing.Size(589, 158);
            this.ItemForTEN_TO_HOA.Text = "TEN_TO_HOA";
            this.ItemForTEN_TO_HOA.TextSize = new System.Drawing.Size(90, 20);
            // 
            // ItemForID_XN
            // 
            this.ItemForID_XN.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForID_XN.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForID_XN.Control = this.ID_XNLookUpEdit;
            this.ItemForID_XN.Location = new System.Drawing.Point(0, 0);
            this.ItemForID_XN.Name = "ItemForID_XN";
            this.ItemForID_XN.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForID_XN.Size = new System.Drawing.Size(589, 32);
            this.ItemForID_XN.Text = "ID_XN";
            this.ItemForID_XN.TextSize = new System.Drawing.Size(90, 20);
            // 
            // ItemForMS_TO
            // 
            this.ItemForMS_TO.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForMS_TO.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForMS_TO.Control = this.MS_TOTextEdit;
            this.ItemForMS_TO.Location = new System.Drawing.Point(0, 32);
            this.ItemForMS_TO.Name = "ItemForMS_TO";
            this.ItemForMS_TO.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForMS_TO.Size = new System.Drawing.Size(294, 32);
            this.ItemForMS_TO.Text = "MS_TO";
            this.ItemForMS_TO.TextSize = new System.Drawing.Size(90, 20);
            // 
            // ItemForSTT_TO
            // 
            this.ItemForSTT_TO.Control = this.STT_TOTextEdit;
            this.ItemForSTT_TO.Location = new System.Drawing.Point(294, 32);
            this.ItemForSTT_TO.Name = "ItemForSTT_TO";
            this.ItemForSTT_TO.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 3, 3);
            this.ItemForSTT_TO.Size = new System.Drawing.Size(295, 32);
            this.ItemForSTT_TO.Text = "STT_TO";
            this.ItemForSTT_TO.TextSize = new System.Drawing.Size(90, 20);
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
            this.tablePanel1.Size = new System.Drawing.Size(769, 347);
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
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 347);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.btnALL.Size = new System.Drawing.Size(769, 40);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButtonPanel2_ButtonClick);
            // 
            // frmEditTO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 387);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmEditTO";
            this.Load += new System.EventHandler(this.frmEditTO_Load);
            this.Resize += new System.EventHandler(this.frmEditTO_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ID_XNLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MS_TOTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TOTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TO_ANHTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TO_HOATextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.STT_TOTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO_ANH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TO_HOA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_XN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_TO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT_TO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit MS_TOTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_TOTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_TO_ANHTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_TO_HOATextEdit;
        private DevExpress.XtraEditors.TextEdit STT_TOTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TO;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TO_ANH;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TO_HOA;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT_TO;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMS_TO;
        private DevExpress.XtraEditors.SearchLookUpEdit ID_XNLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem ItemForID_XN;
    }
}
