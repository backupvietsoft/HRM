namespace VietSoftHRM
{
    partial class ucImportExcel
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions10 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucImportExcel));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions11 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions12 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboDanhMucImport = new DevExpress.XtraEditors.LookUpEdit();
            this.cboMenuImport = new DevExpress.XtraEditors.LookUpEdit();
            this.cboChonSheet = new DevExpress.XtraEditors.LookUpEdit();
            this.btnFile = new DevExpress.XtraEditors.ButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.groThongTinImport = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblChonFile = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblChonSheet = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblMenuImport = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDanhMucImport = new DevExpress.XtraLayout.LayoutControlItem();
            this.groDLImport = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDanhMucImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMenuImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboChonSheet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groThongTinImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChonFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChonSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMenuImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDanhMucImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groDLImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
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
            windowsUIButtonImageOptions10.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions10.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions10.SvgImage")));
            windowsUIButtonImageOptions11.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions11.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions11.SvgImage")));
            windowsUIButtonImageOptions12.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions10, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "export", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions11, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "import", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions12, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 506);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(990, 40);
            this.windowsUIButton.TabIndex = 4;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Location = new System.Drawing.Point(2, 8);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(220, 30);
            this.searchControl.TabIndex = 10;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.grdData);
            this.dataLayoutControl1.Controls.Add(this.cboDanhMucImport);
            this.dataLayoutControl1.Controls.Add(this.cboMenuImport);
            this.dataLayoutControl1.Controls.Add(this.cboChonSheet);
            this.dataLayoutControl1.Controls.Add(this.btnFile);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(990, 506);
            this.dataLayoutControl1.TabIndex = 5;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // grdData
            // 
            this.grdData.Location = new System.Drawing.Point(24, 158);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(942, 324);
            this.grdData.TabIndex = 9;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsCustomization.AllowFilter = false;
            this.grvData.OptionsFilter.AllowFilterEditor = false;
            this.grvData.OptionsFind.AllowFindPanel = false;
            this.grvData.OptionsFind.ShowFindButton = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 100;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.ShownEditor += new System.EventHandler(this.grvData_ShownEditor);
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            // 
            // cboDanhMucImport
            // 
            this.cboDanhMucImport.Location = new System.Drawing.Point(625, 79);
            this.cboDanhMucImport.Name = "cboDanhMucImport";
            this.cboDanhMucImport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDanhMucImport.Properties.NullText = "";
            this.cboDanhMucImport.Size = new System.Drawing.Size(341, 26);
            this.cboDanhMucImport.StyleController = this.dataLayoutControl1;
            this.cboDanhMucImport.TabIndex = 8;
            // 
            // cboMenuImport
            // 
            this.cboMenuImport.Location = new System.Drawing.Point(152, 79);
            this.cboMenuImport.Name = "cboMenuImport";
            this.cboMenuImport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMenuImport.Properties.NullText = "";
            this.cboMenuImport.Size = new System.Drawing.Size(341, 26);
            this.cboMenuImport.StyleController = this.dataLayoutControl1;
            this.cboMenuImport.TabIndex = 7;
            this.cboMenuImport.EditValueChanged += new System.EventHandler(this.cboMenuImport_EditValueChanged);
            // 
            // cboChonSheet
            // 
            this.cboChonSheet.Location = new System.Drawing.Point(625, 49);
            this.cboChonSheet.Name = "cboChonSheet";
            this.cboChonSheet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboChonSheet.Properties.NullText = "";
            this.cboChonSheet.Size = new System.Drawing.Size(341, 26);
            this.cboChonSheet.StyleController = this.dataLayoutControl1;
            this.cboChonSheet.TabIndex = 6;
            this.cboChonSheet.EditValueChanged += new System.EventHandler(this.cboChonSheet_EditValueChanged);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(152, 49);
            this.btnFile.Name = "btnFile";
            this.btnFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnFile.Size = new System.Drawing.Size(341, 26);
            this.btnFile.StyleController = this.dataLayoutControl1;
            this.btnFile.TabIndex = 0;
            this.btnFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtChonFile_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.groThongTinImport,
            this.groDLImport});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(990, 506);
            this.Root.TextVisible = false;
            // 
            // groThongTinImport
            // 
            this.groThongTinImport.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblChonFile,
            this.lblChonSheet,
            this.lblMenuImport,
            this.lblDanhMucImport});
            this.groThongTinImport.Location = new System.Drawing.Point(0, 0);
            this.groThongTinImport.Name = "groThongTinImport";
            this.groThongTinImport.Size = new System.Drawing.Size(970, 109);
            // 
            // lblChonFile
            // 
            this.lblChonFile.Control = this.btnFile;
            this.lblChonFile.Location = new System.Drawing.Point(0, 0);
            this.lblChonFile.Name = "lblChonFile";
            this.lblChonFile.Size = new System.Drawing.Size(473, 30);
            this.lblChonFile.TextSize = new System.Drawing.Size(125, 20);
            // 
            // lblChonSheet
            // 
            this.lblChonSheet.Control = this.cboChonSheet;
            this.lblChonSheet.Location = new System.Drawing.Point(473, 0);
            this.lblChonSheet.Name = "lblChonSheet";
            this.lblChonSheet.Size = new System.Drawing.Size(473, 30);
            this.lblChonSheet.TextSize = new System.Drawing.Size(125, 20);
            // 
            // lblMenuImport
            // 
            this.lblMenuImport.Control = this.cboMenuImport;
            this.lblMenuImport.Location = new System.Drawing.Point(0, 30);
            this.lblMenuImport.Name = "lblMenuImport";
            this.lblMenuImport.Size = new System.Drawing.Size(473, 30);
            this.lblMenuImport.TextSize = new System.Drawing.Size(125, 20);
            // 
            // lblDanhMucImport
            // 
            this.lblDanhMucImport.Control = this.cboDanhMucImport;
            this.lblDanhMucImport.Location = new System.Drawing.Point(473, 30);
            this.lblDanhMucImport.Name = "lblDanhMucImport";
            this.lblDanhMucImport.Size = new System.Drawing.Size(473, 30);
            this.lblDanhMucImport.TextSize = new System.Drawing.Size(125, 20);
            // 
            // groDLImport
            // 
            this.groDLImport.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.groDLImport.Location = new System.Drawing.Point(0, 109);
            this.groDLImport.Name = "groDLImport";
            this.groDLImport.Size = new System.Drawing.Size(970, 377);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdData;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(946, 328);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "ucImportExcel";
            this.Size = new System.Drawing.Size(990, 546);
            this.Load += new System.EventHandler(this.ucImportExcel_Load);
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDanhMucImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMenuImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboChonSheet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groThongTinImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChonFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChonSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMenuImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDanhMucImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groDLImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraEditors.LookUpEdit cboDanhMucImport;
        private DevExpress.XtraEditors.LookUpEdit cboMenuImport;
        private DevExpress.XtraEditors.LookUpEdit cboChonSheet;
        private DevExpress.XtraEditors.ButtonEdit btnFile;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup groThongTinImport;
        private DevExpress.XtraLayout.LayoutControlItem lblChonFile;
        private DevExpress.XtraLayout.LayoutControlItem lblChonSheet;
        private DevExpress.XtraLayout.LayoutControlItem lblMenuImport;
        private DevExpress.XtraLayout.LayoutControlItem lblDanhMucImport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup groDLImport;
    }
}
