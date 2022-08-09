
namespace Vs.Recruit
{
    partial class frmChonUngVien
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonUngVien));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.grdChonUV = new DevExpress.XtraGrid.GridControl();
            this.grvChonUV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboID_VTTD = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboID_NTD = new DevExpress.XtraEditors.LookUpEdit();
            this.cboID_TD = new DevExpress.XtraEditors.LookUpEdit();
            this.cboID_KNLV = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblID_VTTD = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblID_NTD = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblID_TD = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblID_KNLV = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChonUV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChonUV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_VTTD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_NTD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_TD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_KNLV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_VTTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_NTD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_TD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_KNLV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.btnALL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F)});
            this.tablePanel1.Controls.Add(this.dataLayoutControl1);
            this.tablePanel1.Controls.Add(this.btnALL);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 90F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 46F)});
            this.tablePanel1.Size = new System.Drawing.Size(998, 535);
            this.tablePanel1.TabIndex = 0;
            // 
            // dataLayoutControl1
            // 
            this.tablePanel1.SetColumn(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Controls.Add(this.grdChonUV);
            this.dataLayoutControl1.Controls.Add(this.cboID_VTTD);
            this.dataLayoutControl1.Controls.Add(this.cboID_NTD);
            this.dataLayoutControl1.Controls.Add(this.cboID_TD);
            this.dataLayoutControl1.Controls.Add(this.cboID_KNLV);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(13, 11);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.tablePanel1.SetRow(this.dataLayoutControl1, 1);
            this.dataLayoutControl1.Size = new System.Drawing.Size(972, 475);
            this.dataLayoutControl1.TabIndex = 13;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // grdChonUV
            // 
            this.grdChonUV.Location = new System.Drawing.Point(12, 68);
            this.grdChonUV.MainView = this.grvChonUV;
            this.grdChonUV.Name = "grdChonUV";
            this.grdChonUV.Size = new System.Drawing.Size(948, 395);
            this.grdChonUV.TabIndex = 5;
            this.grdChonUV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChonUV});
            // 
            // grvChonUV
            // 
            this.grvChonUV.ColumnPanelRowHeight = 0;
            this.grvChonUV.FixedLineWidth = 1;
            this.grvChonUV.FooterPanelHeight = 0;
            this.grvChonUV.GridControl = this.grdChonUV;
            this.grvChonUV.GroupRowHeight = 0;
            this.grvChonUV.Name = "grvChonUV";
            this.grvChonUV.OptionsSelection.MultiSelect = true;
            this.grvChonUV.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvChonUV.OptionsView.ShowGroupPanel = false;
            this.grvChonUV.RowHeight = 0;
            this.grvChonUV.ViewCaptionHeight = 0;
            this.grvChonUV.DoubleClick += new System.EventHandler(this.grvChonUV_DoubleClick);
            // 
            // cboID_VTTD
            // 
            this.cboID_VTTD.Location = new System.Drawing.Point(138, 12);
            this.cboID_VTTD.Name = "cboID_VTTD";
            this.cboID_VTTD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_VTTD.Properties.NullText = "";
            this.cboID_VTTD.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboID_VTTD.Size = new System.Drawing.Size(345, 24);
            this.cboID_VTTD.StyleController = this.dataLayoutControl1;
            this.cboID_VTTD.TabIndex = 1;
            this.cboID_VTTD.EditValueChanged += new System.EventHandler(this.cboID_VTTD_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cboID_NTD
            // 
            this.cboID_NTD.Location = new System.Drawing.Point(613, 12);
            this.cboID_NTD.Name = "cboID_NTD";
            this.cboID_NTD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_NTD.Properties.NullText = "";
            this.cboID_NTD.Size = new System.Drawing.Size(347, 24);
            this.cboID_NTD.StyleController = this.dataLayoutControl1;
            this.cboID_NTD.TabIndex = 2;
            this.cboID_NTD.EditValueChanged += new System.EventHandler(this.cboID_VTTD_EditValueChanged);
            // 
            // cboID_TD
            // 
            this.cboID_TD.Location = new System.Drawing.Point(138, 40);
            this.cboID_TD.Name = "cboID_TD";
            this.cboID_TD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_TD.Properties.NullText = "";
            this.cboID_TD.Size = new System.Drawing.Size(345, 24);
            this.cboID_TD.StyleController = this.dataLayoutControl1;
            this.cboID_TD.TabIndex = 3;
            this.cboID_TD.EditValueChanged += new System.EventHandler(this.cboID_VTTD_EditValueChanged);
            // 
            // cboID_KNLV
            // 
            this.cboID_KNLV.Location = new System.Drawing.Point(613, 40);
            this.cboID_KNLV.Name = "cboID_KNLV";
            this.cboID_KNLV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_KNLV.Properties.NullText = "";
            this.cboID_KNLV.Size = new System.Drawing.Size(347, 24);
            this.cboID_KNLV.StyleController = this.dataLayoutControl1;
            this.cboID_KNLV.TabIndex = 4;
            this.cboID_KNLV.EditValueChanged += new System.EventHandler(this.cboID_VTTD_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblID_VTTD,
            this.lblID_NTD,
            this.lblID_TD,
            this.lblID_KNLV,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(972, 475);
            this.Root.TextVisible = false;
            // 
            // lblID_VTTD
            // 
            this.lblID_VTTD.Control = this.cboID_VTTD;
            this.lblID_VTTD.Location = new System.Drawing.Point(0, 0);
            this.lblID_VTTD.Name = "lblID_VTTD";
            this.lblID_VTTD.Size = new System.Drawing.Size(475, 28);
            this.lblID_VTTD.Text = "Vị trí tuyển dụng";
            this.lblID_VTTD.TextSize = new System.Drawing.Size(123, 17);
            // 
            // lblID_NTD
            // 
            this.lblID_NTD.Control = this.cboID_NTD;
            this.lblID_NTD.Location = new System.Drawing.Point(475, 0);
            this.lblID_NTD.Name = "lblID_NTD";
            this.lblID_NTD.Size = new System.Drawing.Size(477, 28);
            this.lblID_NTD.Text = "Nguồn tuyển dụng";
            this.lblID_NTD.TextSize = new System.Drawing.Size(123, 17);
            // 
            // lblID_TD
            // 
            this.lblID_TD.Control = this.cboID_TD;
            this.lblID_TD.Location = new System.Drawing.Point(0, 28);
            this.lblID_TD.Name = "lblID_TD";
            this.lblID_TD.Size = new System.Drawing.Size(475, 28);
            this.lblID_TD.Text = "Trình độ";
            this.lblID_TD.TextSize = new System.Drawing.Size(123, 17);
            // 
            // lblID_KNLV
            // 
            this.lblID_KNLV.Control = this.cboID_KNLV;
            this.lblID_KNLV.Location = new System.Drawing.Point(475, 28);
            this.lblID_KNLV.Name = "lblID_KNLV";
            this.lblID_KNLV.Size = new System.Drawing.Size(477, 28);
            this.lblID_KNLV.Text = "Kinh nghiệm làm việc";
            this.lblID_KNLV.TextSize = new System.Drawing.Size(123, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.grdChonUV;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(952, 399);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
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
            windowsUIButtonImageOptions3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions3.SvgImage")));
            windowsUIButtonImageOptions4.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions4.SvgImage")));
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "ghi", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongghi", -1, false)});
            this.tablePanel1.SetColumn(this.btnALL, 0);
            this.tablePanel1.SetColumnSpan(this.btnALL, 3);
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Controls.Add(this.searchControl1);
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnALL.Location = new System.Drawing.Point(3, 492);
            this.btnALL.Name = "btnALL";
            this.tablePanel1.SetRow(this.btnALL, 2);
            this.btnALL.Size = new System.Drawing.Size(992, 40);
            this.btnALL.TabIndex = 6;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.btnALL_ButtonClick);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(19, 12);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(208, 24);
            this.searchControl1.TabIndex = 0;
            // 
            // frmChonUngVien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(998, 535);
            this.Controls.Add(this.tablePanel1);
            this.Name = "frmChonUngVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn ứng viên";
            this.Load += new System.EventHandler(this.frmChonUngVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChonUV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChonUV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_VTTD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_NTD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_TD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_KNLV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_VTTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_NTD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_TD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblID_KNLV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.btnALL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraGrid.GridControl grdChonUV;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChonUV;
        private DevExpress.XtraEditors.SearchLookUpEdit cboID_VTTD;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblID_VTTD;
        private DevExpress.XtraLayout.LayoutControlItem lblID_NTD;
        private DevExpress.XtraLayout.LayoutControlItem lblID_TD;
        private DevExpress.XtraLayout.LayoutControlItem lblID_KNLV;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.LookUpEdit cboID_NTD;
        private DevExpress.XtraEditors.LookUpEdit cboID_TD;
        private DevExpress.XtraEditors.LookUpEdit cboID_KNLV;
    }
}