namespace Vs.Category
{
    partial class frmEditQUAN
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.TEN_QUANTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.TEN_QUAN_ATextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_QUAN_HTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.MS_QUANTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ID_QGSearchLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID_TPSearchLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTEN_QUAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_QUAN_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_QUAN_H = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForID_TP = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForID_QG = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForMS_QUAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnWDUI = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUANTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUAN_ATextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUAN_HTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MS_QUANTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_QGSearchLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TPSearchLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_TP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_QG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_QUAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dxValidationProvider1
            // 
            this.dxValidationProvider1.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Manual;
            // 
            // TEN_QUANTextEdit
            // 
            this.TEN_QUANTextEdit.Location = new System.Drawing.Point(109, 62);
            this.TEN_QUANTextEdit.Name = "TEN_QUANTextEdit";
            this.TEN_QUANTextEdit.Size = new System.Drawing.Size(437, 26);
            this.TEN_QUANTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_QUANTextEdit.TabIndex = 4;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.TEN_QUANTextEdit, conditionValidationRule4);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.AllowGeneratingCollectionProperties = DevExpress.Utils.DefaultBoolean.False;
            this.dataLayoutControl1.AllowGeneratingNestedGroups = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.TEN_QUANTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_QUAN_ATextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_QUAN_HTextEdit);
            this.dataLayoutControl1.Controls.Add(this.MS_QUANTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ID_QGSearchLookUpEdit);
            this.dataLayoutControl1.Controls.Add(this.ID_TPSearchLookUpEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(73, 39);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(552, 322);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TEN_QUAN_ATextEdit
            // 
            this.TEN_QUAN_ATextEdit.Location = new System.Drawing.Point(109, 90);
            this.TEN_QUAN_ATextEdit.Name = "TEN_QUAN_ATextEdit";
            this.TEN_QUAN_ATextEdit.Size = new System.Drawing.Size(437, 26);
            this.TEN_QUAN_ATextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_QUAN_ATextEdit.TabIndex = 5;
            // 
            // TEN_QUAN_HTextEdit
            // 
            this.TEN_QUAN_HTextEdit.Location = new System.Drawing.Point(109, 118);
            this.TEN_QUAN_HTextEdit.Name = "TEN_QUAN_HTextEdit";
            this.TEN_QUAN_HTextEdit.Size = new System.Drawing.Size(437, 26);
            this.TEN_QUAN_HTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_QUAN_HTextEdit.TabIndex = 6;
            // 
            // MS_QUANTextEdit
            // 
            this.MS_QUANTextEdit.Location = new System.Drawing.Point(109, 34);
            this.MS_QUANTextEdit.Name = "MS_QUANTextEdit";
            this.MS_QUANTextEdit.Size = new System.Drawing.Size(437, 26);
            this.MS_QUANTextEdit.StyleController = this.dataLayoutControl1;
            this.MS_QUANTextEdit.TabIndex = 8;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.MS_QUANTextEdit, conditionValidationRule1);
            // 
            // ID_QGSearchLookUpEdit
            // 
            this.ID_QGSearchLookUpEdit.Location = new System.Drawing.Point(109, 6);
            this.ID_QGSearchLookUpEdit.Name = "ID_QGSearchLookUpEdit";
            this.ID_QGSearchLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_QGSearchLookUpEdit.Properties.NullText = "";
            this.ID_QGSearchLookUpEdit.Properties.PopupView = this.gridView1;
            this.ID_QGSearchLookUpEdit.Size = new System.Drawing.Size(165, 26);
            this.ID_QGSearchLookUpEdit.StyleController = this.dataLayoutControl1;
            this.ID_QGSearchLookUpEdit.TabIndex = 7;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.ID_QGSearchLookUpEdit, conditionValidationRule2);
            this.ID_QGSearchLookUpEdit.EditValueChanged += new System.EventHandler(this.ID_QGSearchLookUpEdit_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 538;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // ID_TPSearchLookUpEdit
            // 
            this.ID_TPSearchLookUpEdit.Location = new System.Drawing.Point(379, 6);
            this.ID_TPSearchLookUpEdit.Name = "ID_TPSearchLookUpEdit";
            this.ID_TPSearchLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_TPSearchLookUpEdit.Properties.NullText = "";
            this.ID_TPSearchLookUpEdit.Properties.PopupView = this.searchLookUpEdit1View;
            this.ID_TPSearchLookUpEdit.Size = new System.Drawing.Size(167, 26);
            this.ID_TPSearchLookUpEdit.StyleController = this.dataLayoutControl1;
            this.ID_TPSearchLookUpEdit.TabIndex = 7;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.ID_TPSearchLookUpEdit, conditionValidationRule3);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 538;
            this.searchLookUpEdit1View.FixedLineWidth = 3;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(552, 322);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTEN_QUAN,
            this.ItemForTEN_QUAN_A,
            this.ItemForTEN_QUAN_H,
            this.ItemForID_TP,
            this.ItemForID_QG,
            this.ItemForMS_QUAN});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(542, 312);
            // 
            // ItemForTEN_QUAN
            // 
            this.ItemForTEN_QUAN.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForTEN_QUAN.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTEN_QUAN.Control = this.TEN_QUANTextEdit;
            this.ItemForTEN_QUAN.Location = new System.Drawing.Point(0, 56);
            this.ItemForTEN_QUAN.Name = "ItemForTEN_QUAN";
            this.ItemForTEN_QUAN.Size = new System.Drawing.Size(542, 28);
            this.ItemForTEN_QUAN.Text = "TEN_QUAN";
            this.ItemForTEN_QUAN.TextSize = new System.Drawing.Size(100, 20);
            // 
            // ItemForTEN_QUAN_A
            // 
            this.ItemForTEN_QUAN_A.Control = this.TEN_QUAN_ATextEdit;
            this.ItemForTEN_QUAN_A.Location = new System.Drawing.Point(0, 84);
            this.ItemForTEN_QUAN_A.Name = "ItemForTEN_QUAN_A";
            this.ItemForTEN_QUAN_A.Size = new System.Drawing.Size(542, 28);
            this.ItemForTEN_QUAN_A.Text = "TEN_QUAN_A";
            this.ItemForTEN_QUAN_A.TextSize = new System.Drawing.Size(100, 20);
            // 
            // ItemForTEN_QUAN_H
            // 
            this.ItemForTEN_QUAN_H.Control = this.TEN_QUAN_HTextEdit;
            this.ItemForTEN_QUAN_H.Location = new System.Drawing.Point(0, 112);
            this.ItemForTEN_QUAN_H.Name = "ItemForTEN_QUAN_H";
            this.ItemForTEN_QUAN_H.Size = new System.Drawing.Size(542, 200);
            this.ItemForTEN_QUAN_H.Text = "TEN_QUAN_H";
            this.ItemForTEN_QUAN_H.TextSize = new System.Drawing.Size(100, 20);
            // 
            // ItemForID_TP
            // 
            this.ItemForID_TP.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForID_TP.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForID_TP.Control = this.ID_TPSearchLookUpEdit;
            this.ItemForID_TP.Location = new System.Drawing.Point(270, 0);
            this.ItemForID_TP.Name = "ItemForID_TP";
            this.ItemForID_TP.Size = new System.Drawing.Size(272, 28);
            this.ItemForID_TP.Text = "ID_TP";
            this.ItemForID_TP.TextSize = new System.Drawing.Size(100, 20);
            // 
            // ItemForID_QG
            // 
            this.ItemForID_QG.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForID_QG.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForID_QG.Control = this.ID_QGSearchLookUpEdit;
            this.ItemForID_QG.Location = new System.Drawing.Point(0, 0);
            this.ItemForID_QG.Name = "ItemForID_QG";
            this.ItemForID_QG.Size = new System.Drawing.Size(270, 28);
            this.ItemForID_QG.TextSize = new System.Drawing.Size(100, 20);
            // 
            // ItemForMS_QUAN
            // 
            this.ItemForMS_QUAN.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForMS_QUAN.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForMS_QUAN.Control = this.MS_QUANTextEdit;
            this.ItemForMS_QUAN.Location = new System.Drawing.Point(0, 28);
            this.ItemForMS_QUAN.Name = "ItemForMS_QUAN";
            this.ItemForMS_QUAN.Size = new System.Drawing.Size(542, 28);
            this.ItemForMS_QUAN.Text = "MS_HUYEN";
            this.ItemForMS_QUAN.TextSize = new System.Drawing.Size(100, 20);
            // 
            // btnWDUI
            // 
            this.btnWDUI.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.btnWDUI.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.Gray;
            this.btnWDUI.AppearanceButton.Hovered.Options.UseFont = true;
            this.btnWDUI.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.btnWDUI.AppearanceButton.Normal.FontSizeDelta = -1;
            this.btnWDUI.AppearanceButton.Normal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnWDUI.AppearanceButton.Normal.Options.UseFont = true;
            this.btnWDUI.AppearanceButton.Normal.Options.UseForeColor = true;
            this.btnWDUI.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnWDUI.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.btnWDUI.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.btnWDUI.AppearanceButton.Pressed.Options.UseBorderColor = true;
            this.btnWDUI.AppearanceButton.Pressed.Options.UseFont = true;
            this.btnWDUI.AppearanceButton.Pressed.Options.UseImage = true;
            this.btnWDUI.AppearanceButton.Pressed.Options.UseTextOptions = true;
            windowsUIButtonImageOptions1.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnWDUI.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnWDUI.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWDUI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWDUI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnWDUI.Location = new System.Drawing.Point(0, 324);
            this.btnWDUI.Margin = new System.Windows.Forms.Padding(0);
            this.btnWDUI.Name = "btnWDUI";
            this.btnWDUI.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.btnWDUI.Size = new System.Drawing.Size(698, 40);
            this.btnWDUI.TabIndex = 10;
            this.btnWDUI.Text = "btnWDUIPanel1";
            this.btnWDUI.UseButtonBackgroundImages = false;
            this.btnWDUI.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnWDUI_ButtonClick);
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
            this.tablePanel1.Size = new System.Drawing.Size(698, 364);
            this.tablePanel1.TabIndex = 10;
            // 
            // frmEditQUAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 364);
            this.Controls.Add(this.btnWDUI);
            this.Controls.Add(this.tablePanel1);
            this.Name = "frmEditQUAN";
            this.Text = "frmEdirQUAN";
            this.Load += new System.EventHandler(this.frmEdirQUAN_Load);
            this.Resize += new System.EventHandler(this.frmEdirQUAN_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUANTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUAN_ATextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_QUAN_HTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MS_QUANTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_QGSearchLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_TPSearchLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_QUAN_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_TP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_QG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForMS_QUAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnWDUI;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit TEN_QUAN_ATextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_QUAN_HTextEdit;
        private DevExpress.XtraEditors.TextEdit MS_QUANTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_QUAN;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_QUAN_A;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_QUAN_H;
        private DevExpress.XtraLayout.LayoutControlItem ItemForMS_QUAN;
        private DevExpress.XtraLayout.LayoutControlItem ItemForID_TP;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit ID_TPSearchLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public DevExpress.XtraEditors.SearchLookUpEdit ID_QGSearchLookUpEdit;
        private DevExpress.XtraLayout.LayoutControlItem ItemForID_QG;
        public DevExpress.XtraEditors.TextEdit TEN_QUANTextEdit;
    }
}