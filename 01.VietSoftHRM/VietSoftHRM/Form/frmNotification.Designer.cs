namespace VietSoftHRM
{
    partial class frmNotification
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.grdSource = new DevExpress.XtraGrid.GridControl();
            this.grvSource = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblTabbedControlGroup = new DevExpress.XtraLayout.TabbedControlGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.tabQuery = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cboSearchSP = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCauQuery = new DevExpress.XtraEditors.MemoEdit();
            this.grdQuery = new DevExpress.XtraGrid.GridControl();
            this.grvQuery = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCauQuery = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblStoredProcedures = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabThongBao = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTabbedControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchSP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCauQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCauQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStoredProcedures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabThongBao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowsUIButton
            // 
            this.windowsUIButton.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.Gray;
            this.windowsUIButton.AppearanceButton.Hovered.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.windowsUIButton.AppearanceButton.Normal.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Normal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.windowsUIButton.AppearanceButton.Normal.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Normal.Options.UseForeColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.windowsUIButton.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseBorderColor = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseFont = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseImage = true;
            this.windowsUIButton.AppearanceButton.Pressed.Options.UseTextOptions = true;
            windowsUIButtonImageOptions5.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            windowsUIButtonImageOptions6.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "EXEC", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 454);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(780, 34);
            this.windowsUIButton.TabIndex = 5;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdSource;
            this.searchControl.Location = new System.Drawing.Point(2, 7);
            this.searchControl.Margin = new System.Windows.Forms.Padding(2);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdSource;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(140, 24);
            this.searchControl.TabIndex = 10;
            // 
            // grdSource
            // 
            this.grdSource.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdSource.Location = new System.Drawing.Point(12, 37);
            this.grdSource.MainView = this.grvSource;
            this.grdSource.Name = "grdSource";
            this.grdSource.Size = new System.Drawing.Size(756, 405);
            this.grdSource.TabIndex = 6;
            this.grdSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvSource});
            // 
            // grvSource
            // 
            this.grvSource.DetailHeight = 297;
            this.grvSource.FixedLineWidth = 1;
            this.grvSource.GridControl = this.grdSource;
            this.grvSource.Name = "grvSource";
            this.grvSource.OptionsView.ShowGroupPanel = false;
            // 
            // lblTabbedControlGroup
            // 
            this.lblTabbedControlGroup.Location = new System.Drawing.Point(0, 0);
            this.lblTabbedControlGroup.Name = "lblTabbedControlGroup";
            this.lblTabbedControlGroup.SelectedTabPage = null;
            this.lblTabbedControlGroup.Size = new System.Drawing.Size(770, 444);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(780, 454);
            this.Root.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.tabQuery;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(770, 444);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabThongBao,
            this.tabQuery});
            // 
            // tabQuery
            // 
            this.tabQuery.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.tabQuery.Location = new System.Drawing.Point(0, 0);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Size = new System.Drawing.Size(758, 407);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(758, 407);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtCauQuery);
            this.layoutControl1.Controls.Add(this.cboSearchSP);
            this.layoutControl1.Controls.Add(this.grdQuery);
            this.layoutControl1.Location = new System.Drawing.Point(12, 37);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(756, 405);
            this.layoutControl1.TabIndex = 7;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cboSearchSP
            // 
            this.cboSearchSP.Location = new System.Drawing.Point(127, 6);
            this.cboSearchSP.Name = "cboSearchSP";
            this.cboSearchSP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSearchSP.Properties.NullText = "";
            this.cboSearchSP.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboSearchSP.Size = new System.Drawing.Size(623, 24);
            this.cboSearchSP.StyleController = this.layoutControl1;
            this.cboSearchSP.TabIndex = 7;
            this.cboSearchSP.EditValueChanged += new System.EventHandler(this.cboSearchSP_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtCauQuery
            // 
            this.txtCauQuery.Location = new System.Drawing.Point(6, 32);
            this.txtCauQuery.Name = "txtCauQuery";
            this.txtCauQuery.Size = new System.Drawing.Size(744, 113);
            this.txtCauQuery.StyleController = this.layoutControl1;
            this.txtCauQuery.TabIndex = 6;
            this.txtCauQuery.TextChanged += new System.EventHandler(this.txtCauQuery_TextChanged);
            this.txtCauQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCauQuery_KeyDown);
            // 
            // grdQuery
            // 
            this.grdQuery.Location = new System.Drawing.Point(6, 147);
            this.grdQuery.MainView = this.grvQuery;
            this.grdQuery.Name = "grdQuery";
            this.grdQuery.Size = new System.Drawing.Size(744, 252);
            this.grdQuery.TabIndex = 5;
            this.grdQuery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvQuery});
            // 
            // grvQuery
            // 
            this.grvQuery.GridControl = this.grdQuery;
            this.grvQuery.Name = "grvQuery";
            this.grvQuery.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.lblCauQuery,
            this.lblStoredProcedures});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(756, 405);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.grdQuery;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 141);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(746, 254);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCauQuery
            // 
            this.lblCauQuery.Control = this.txtCauQuery;
            this.lblCauQuery.Location = new System.Drawing.Point(0, 26);
            this.lblCauQuery.Name = "lblCauQuery";
            this.lblCauQuery.Size = new System.Drawing.Size(746, 115);
            this.lblCauQuery.TextSize = new System.Drawing.Size(0, 0);
            this.lblCauQuery.TextVisible = false;
            // 
            // lblStoredProcedures
            // 
            this.lblStoredProcedures.Control = this.cboSearchSP;
            this.lblStoredProcedures.Location = new System.Drawing.Point(0, 0);
            this.lblStoredProcedures.Name = "lblStoredProcedures";
            this.lblStoredProcedures.Size = new System.Drawing.Size(746, 26);
            this.lblStoredProcedures.Text = "Stored Procedures";
            this.lblStoredProcedures.TextSize = new System.Drawing.Size(109, 17);
            // 
            // tabThongBao
            // 
            this.tabThongBao.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.tabThongBao.Location = new System.Drawing.Point(0, 0);
            this.tabThongBao.Name = "tabThongBao";
            this.tabThongBao.Size = new System.Drawing.Size(758, 407);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdSource;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(758, 407);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.layoutControl1);
            this.dataLayoutControl1.Controls.Add(this.grdSource);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1213, 182, 650, 400);
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(780, 454);
            this.dataLayoutControl1.TabIndex = 7;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 488);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmNotification";
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTabbedControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchSP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCauQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCauQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStoredProcedures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabThongBao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraLayout.TabbedControlGroup lblTabbedControlGroup;
        private DevExpress.XtraGrid.GridControl grdSource;
        private DevExpress.XtraGrid.Views.Grid.GridView grvSource;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup tabQuery;
        private DevExpress.XtraLayout.LayoutControlGroup tabThongBao;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl grdQuery;
        private DevExpress.XtraGrid.Views.Grid.GridView grvQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.MemoEdit txtCauQuery;
        private DevExpress.XtraLayout.LayoutControlItem lblCauQuery;
        private DevExpress.XtraEditors.SearchLookUpEdit cboSearchSP;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblStoredProcedures;
    }
}