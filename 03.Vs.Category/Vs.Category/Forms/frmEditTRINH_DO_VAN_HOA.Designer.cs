namespace Vs.Category
{
    partial class frmEditTRINH_DO_VAN_HOA
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.TEN_TDVHTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_TDVH_ATextEdit = new DevExpress.XtraEditors.TextEdit();
            this.TEN_TDVH_HTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ID_LOAI_TDSearchLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ItemForTEN_TDVH = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_TDVH_A = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForTEN_TDVH_H = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemForID_LOAI_TD = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.txtSTT = new DevExpress.XtraEditors.TextEdit();
            this.ItemForSTT = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVHTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVH_ATextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVH_HTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_LOAI_TDSearchLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_LOAI_TD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).BeginInit();
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
            this.btnALL.Location = new System.Drawing.Point(0, 328);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.btnALL.Size = new System.Drawing.Size(774, 40);
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
            this.tablePanel1.Size = new System.Drawing.Size(774, 368);
            this.tablePanel1.TabIndex = 10;
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.txtSTT);
            this.dataLayoutControl1.Controls.Add(this.TEN_TDVHTextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_TDVH_ATextEdit);
            this.dataLayoutControl1.Controls.Add(this.TEN_TDVH_HTextEdit);
            this.dataLayoutControl1.Controls.Add(this.ID_LOAI_TDSearchLookUpEdit);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(81, 42);
            this.dataLayoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(611, 321);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // TEN_TDVHTextEdit
            // 
            this.TEN_TDVHTextEdit.Location = new System.Drawing.Point(98, 34);
            this.TEN_TDVHTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_TDVHTextEdit.Name = "TEN_TDVHTextEdit";
            this.TEN_TDVHTextEdit.Size = new System.Drawing.Size(507, 26);
            this.TEN_TDVHTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TDVHTextEdit.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.TEN_TDVHTextEdit, conditionValidationRule1);
            // 
            // TEN_TDVH_ATextEdit
            // 
            this.TEN_TDVH_ATextEdit.Location = new System.Drawing.Point(98, 62);
            this.TEN_TDVH_ATextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_TDVH_ATextEdit.Name = "TEN_TDVH_ATextEdit";
            this.TEN_TDVH_ATextEdit.Size = new System.Drawing.Size(507, 26);
            this.TEN_TDVH_ATextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TDVH_ATextEdit.TabIndex = 5;
            // 
            // TEN_TDVH_HTextEdit
            // 
            this.TEN_TDVH_HTextEdit.Location = new System.Drawing.Point(98, 90);
            this.TEN_TDVH_HTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TEN_TDVH_HTextEdit.Name = "TEN_TDVH_HTextEdit";
            this.TEN_TDVH_HTextEdit.Size = new System.Drawing.Size(507, 26);
            this.TEN_TDVH_HTextEdit.StyleController = this.dataLayoutControl1;
            this.TEN_TDVH_HTextEdit.TabIndex = 6;
            // 
            // ID_LOAI_TDSearchLookUpEdit
            // 
            this.ID_LOAI_TDSearchLookUpEdit.Location = new System.Drawing.Point(98, 6);
            this.ID_LOAI_TDSearchLookUpEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ID_LOAI_TDSearchLookUpEdit.Name = "ID_LOAI_TDSearchLookUpEdit";
            this.ID_LOAI_TDSearchLookUpEdit.Properties.Appearance.Options.UseTextOptions = true;
            this.ID_LOAI_TDSearchLookUpEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.ID_LOAI_TDSearchLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ID_LOAI_TDSearchLookUpEdit.Properties.NullText = "";
            this.ID_LOAI_TDSearchLookUpEdit.Properties.PopupView = this.searchLookUpEdit1View;
            this.ID_LOAI_TDSearchLookUpEdit.Size = new System.Drawing.Size(507, 26);
            this.ID_LOAI_TDSearchLookUpEdit.StyleController = this.dataLayoutControl1;
            this.ID_LOAI_TDSearchLookUpEdit.TabIndex = 7;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.ID_LOAI_TDSearchLookUpEdit, conditionValidationRule2);
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
            this.Root.Size = new System.Drawing.Size(611, 321);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AllowDrawBackground = false;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ItemForTEN_TDVH,
            this.ItemForTEN_TDVH_A,
            this.ItemForTEN_TDVH_H,
            this.ItemForID_LOAI_TD,
            this.ItemForSTT});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "autoGeneratedGroup0";
            this.layoutControlGroup1.Size = new System.Drawing.Size(601, 311);
            // 
            // ItemForTEN_TDVH
            // 
            this.ItemForTEN_TDVH.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForTEN_TDVH.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForTEN_TDVH.Control = this.TEN_TDVHTextEdit;
            this.ItemForTEN_TDVH.Location = new System.Drawing.Point(0, 28);
            this.ItemForTEN_TDVH.Name = "ItemForTEN_TDVH";
            this.ItemForTEN_TDVH.Size = new System.Drawing.Size(601, 28);
            this.ItemForTEN_TDVH.Text = "TEN_TDVH";
            this.ItemForTEN_TDVH.TextSize = new System.Drawing.Size(89, 20);
            // 
            // ItemForTEN_TDVH_A
            // 
            this.ItemForTEN_TDVH_A.Control = this.TEN_TDVH_ATextEdit;
            this.ItemForTEN_TDVH_A.Location = new System.Drawing.Point(0, 56);
            this.ItemForTEN_TDVH_A.Name = "ItemForTEN_TDVH_A";
            this.ItemForTEN_TDVH_A.Size = new System.Drawing.Size(601, 28);
            this.ItemForTEN_TDVH_A.Text = "TEN_TDVH_A";
            this.ItemForTEN_TDVH_A.TextSize = new System.Drawing.Size(89, 20);
            // 
            // ItemForTEN_TDVH_H
            // 
            this.ItemForTEN_TDVH_H.Control = this.TEN_TDVH_HTextEdit;
            this.ItemForTEN_TDVH_H.Location = new System.Drawing.Point(0, 84);
            this.ItemForTEN_TDVH_H.Name = "ItemForTEN_TDVH_H";
            this.ItemForTEN_TDVH_H.Size = new System.Drawing.Size(601, 28);
            this.ItemForTEN_TDVH_H.Text = "TEN_TDVH_H";
            this.ItemForTEN_TDVH_H.TextSize = new System.Drawing.Size(89, 20);
            // 
            // ItemForID_LOAI_TD
            // 
            this.ItemForID_LOAI_TD.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.ItemForID_LOAI_TD.AppearanceItemCaption.Options.UseFont = true;
            this.ItemForID_LOAI_TD.Control = this.ID_LOAI_TDSearchLookUpEdit;
            this.ItemForID_LOAI_TD.Location = new System.Drawing.Point(0, 0);
            this.ItemForID_LOAI_TD.Name = "ItemForID_LOAI_TD";
            this.ItemForID_LOAI_TD.Size = new System.Drawing.Size(601, 28);
            this.ItemForID_LOAI_TD.Text = "ID_LOAI_TD";
            this.ItemForID_LOAI_TD.TextSize = new System.Drawing.Size(89, 20);
            // 
            // txtSTT
            // 
            this.txtSTT.Location = new System.Drawing.Point(98, 118);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(507, 26);
            this.txtSTT.StyleController = this.dataLayoutControl1;
            this.txtSTT.TabIndex = 8;
            // 
            // ItemForSTT
            // 
            this.ItemForSTT.Control = this.txtSTT;
            this.ItemForSTT.Location = new System.Drawing.Point(0, 112);
            this.ItemForSTT.Name = "ItemForSTT";
            this.ItemForSTT.Size = new System.Drawing.Size(601, 199);
            this.ItemForSTT.TextSize = new System.Drawing.Size(89, 20);
            // 
            // frmEditTRINH_DO_VAN_HOA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 368);
            this.Controls.Add(this.btnALL);
            this.Controls.Add(this.tablePanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmEditTRINH_DO_VAN_HOA";
            this.Text = "frmEditTRINH_DO_VAN_HOA";
            this.Load += new System.EventHandler(this.frmEditTRINH_DO_VAN_HOA_Load);
            this.Resize += new System.EventHandler(this.frmEditTRINH_DO_VAN_HOA_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVHTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVH_ATextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TEN_TDVH_HTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID_LOAI_TDSearchLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForTEN_TDVH_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForID_LOAI_TD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemForSTT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit TEN_TDVHTextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_TDVH_ATextEdit;
        private DevExpress.XtraEditors.TextEdit TEN_TDVH_HTextEdit;
        private DevExpress.XtraEditors.SearchLookUpEdit ID_LOAI_TDSearchLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TDVH;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TDVH_A;
        private DevExpress.XtraLayout.LayoutControlItem ItemForTEN_TDVH_H;
        private DevExpress.XtraLayout.LayoutControlItem ItemForID_LOAI_TD;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.TextEdit txtSTT;
        private DevExpress.XtraLayout.LayoutControlItem ItemForSTT;
    }
}