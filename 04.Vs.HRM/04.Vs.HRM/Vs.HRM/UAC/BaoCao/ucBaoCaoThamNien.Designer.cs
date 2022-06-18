namespace Vs.HRM
{
    partial class ucBaoCaoThamNien
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.txtTu = new DevExpress.XtraEditors.TextEdit();
            this.lbTo = new DevExpress.XtraEditors.LabelControl();
            this.lbXiNghiep = new DevExpress.XtraEditors.LabelControl();
            this.lbDonVi = new DevExpress.XtraEditors.LabelControl();
            this.lbNgay = new DevExpress.XtraEditors.LabelControl();
            this.lk_NgayIn = new DevExpress.XtraEditors.DateEdit();
            this.LK_XI_NGHIEP = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.LK_DON_VI = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.LK_TO = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDen = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_TO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "Print";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "Print", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 618);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(1128, 40);
            this.windowsUIButton.TabIndex = 16;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tablePanel1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1128, 618);
            this.layoutControl1.TabIndex = 17;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 13.5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F)});
            this.tablePanel1.Controls.Add(this.txtTu);
            this.tablePanel1.Controls.Add(this.lbTo);
            this.tablePanel1.Controls.Add(this.lbXiNghiep);
            this.tablePanel1.Controls.Add(this.lbDonVi);
            this.tablePanel1.Controls.Add(this.lbNgay);
            this.tablePanel1.Controls.Add(this.lk_NgayIn);
            this.tablePanel1.Controls.Add(this.LK_XI_NGHIEP);
            this.tablePanel1.Controls.Add(this.LK_DON_VI);
            this.tablePanel1.Controls.Add(this.LK_TO);
            this.tablePanel1.Controls.Add(this.labelControl4);
            this.tablePanel1.Controls.Add(this.labelControl5);
            this.tablePanel1.Controls.Add(this.txtDen);
            this.tablePanel1.Location = new System.Drawing.Point(6, 6);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1116, 606);
            this.tablePanel1.TabIndex = 4;
            // 
            // txtTu
            // 
            this.tablePanel1.SetColumn(this.txtTu, 2);
            this.txtTu.Location = new System.Drawing.Point(193, 75);
            this.txtTu.Name = "txtTu";
            this.txtTu.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTu.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTu.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel1.SetRow(this.txtTu, 2);
            this.txtTu.Size = new System.Drawing.Size(174, 26);
            this.txtTu.TabIndex = 12;
            // 
            // lbTo
            // 
            this.tablePanel1.SetColumn(this.lbTo, 5);
            this.lbTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTo.Location = new System.Drawing.Point(761, 43);
            this.lbTo.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbTo.Name = "lbTo";
            this.tablePanel1.SetRow(this.lbTo, 1);
            this.lbTo.Size = new System.Drawing.Size(145, 26);
            this.lbTo.TabIndex = 11;
            this.lbTo.Text = "labelControl3";
            // 
            // lbXiNghiep
            // 
            this.tablePanel1.SetColumn(this.lbXiNghiep, 3);
            this.lbXiNghiep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbXiNghiep.Location = new System.Drawing.Point(401, 43);
            this.lbXiNghiep.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbXiNghiep.Name = "lbXiNghiep";
            this.tablePanel1.SetRow(this.lbXiNghiep, 1);
            this.lbXiNghiep.Size = new System.Drawing.Size(145, 26);
            this.lbXiNghiep.TabIndex = 10;
            this.lbXiNghiep.Text = "labelControl2";
            // 
            // lbDonVi
            // 
            this.tablePanel1.SetColumn(this.lbDonVi, 1);
            this.lbDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDonVi.Location = new System.Drawing.Point(31, 43);
            this.lbDonVi.Name = "lbDonVi";
            this.tablePanel1.SetRow(this.lbDonVi, 1);
            this.lbDonVi.Size = new System.Drawing.Size(156, 26);
            this.lbDonVi.TabIndex = 9;
            this.lbDonVi.Text = "labelControl1";
            // 
            // lbNgay
            // 
            this.tablePanel1.SetColumn(this.lbNgay, 5);
            this.lbNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgay.Location = new System.Drawing.Point(761, 75);
            this.lbNgay.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbNgay.Name = "lbNgay";
            this.tablePanel1.SetRow(this.lbNgay, 2);
            this.lbNgay.Size = new System.Drawing.Size(145, 26);
            this.lbNgay.TabIndex = 7;
            this.lbNgay.Text = "lbNgay";
            // 
            // lk_NgayIn
            // 
            this.tablePanel1.SetColumn(this.lk_NgayIn, 6);
            this.lk_NgayIn.EditValue = null;
            this.lk_NgayIn.Location = new System.Drawing.Point(911, 75);
            this.lk_NgayIn.Name = "lk_NgayIn";
            this.lk_NgayIn.Properties.Appearance.Options.UseTextOptions = true;
            this.lk_NgayIn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lk_NgayIn.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lk_NgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lk_NgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.lk_NgayIn, 2);
            this.lk_NgayIn.Size = new System.Drawing.Size(174, 26);
            this.lk_NgayIn.TabIndex = 5;
            // 
            // LK_XI_NGHIEP
            // 
            this.tablePanel1.SetColumn(this.LK_XI_NGHIEP, 4);
            this.LK_XI_NGHIEP.Location = new System.Drawing.Point(552, 43);
            this.LK_XI_NGHIEP.Name = "LK_XI_NGHIEP";
            this.LK_XI_NGHIEP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.LK_XI_NGHIEP, 1);
            this.LK_XI_NGHIEP.Size = new System.Drawing.Size(174, 26);
            this.LK_XI_NGHIEP.TabIndex = 3;
            this.LK_XI_NGHIEP.EditValueChanged += new System.EventHandler(this.LK_XI_NGHIEP_EditValueChanged);
            // 
            // LK_DON_VI
            // 
            this.tablePanel1.SetColumn(this.LK_DON_VI, 2);
            this.LK_DON_VI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LK_DON_VI.Location = new System.Drawing.Point(193, 43);
            this.LK_DON_VI.Name = "LK_DON_VI";
            this.LK_DON_VI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.LK_DON_VI, 1);
            this.LK_DON_VI.Size = new System.Drawing.Size(174, 26);
            this.LK_DON_VI.TabIndex = 1;
            this.LK_DON_VI.EditValueChanged += new System.EventHandler(this.LK_DON_VI_EditValueChanged);
            // 
            // LK_TO
            // 
            this.tablePanel1.SetColumn(this.LK_TO, 6);
            this.LK_TO.Location = new System.Drawing.Point(911, 43);
            this.LK_TO.Name = "LK_TO";
            this.LK_TO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.LK_TO, 1);
            this.LK_TO.Size = new System.Drawing.Size(174, 26);
            this.LK_TO.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.tablePanel1.SetColumn(this.labelControl4, 1);
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(31, 75);
            this.labelControl4.Name = "labelControl4";
            this.tablePanel1.SetRow(this.labelControl4, 2);
            this.labelControl4.Size = new System.Drawing.Size(156, 26);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "labelControl3";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.tablePanel1.SetColumn(this.labelControl5, 3);
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(401, 75);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.labelControl5.Name = "labelControl5";
            this.tablePanel1.SetRow(this.labelControl5, 2);
            this.labelControl5.Size = new System.Drawing.Size(145, 26);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "labelControl3";
            // 
            // txtDen
            // 
            this.tablePanel1.SetColumn(this.txtDen, 4);
            this.txtDen.Location = new System.Drawing.Point(552, 75);
            this.txtDen.Name = "txtDen";
            this.txtDen.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDen.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel1.SetRow(this.txtDen, 2);
            this.txtDen.Size = new System.Drawing.Size(174, 26);
            this.txtDen.TabIndex = 12;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1128, 618);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.tablePanel1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1118, 608);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 349;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
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
            // ucBaoCaoThamNien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucBaoCaoThamNien";
            this.Size = new System.Drawing.Size(1128, 658);
            this.Load += new System.EventHandler(this.ucBaoCaoThamNien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_TO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.DateEdit lk_NgayIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lbNgay;
        private DevExpress.XtraEditors.LabelControl lbTo;
        private DevExpress.XtraEditors.LabelControl lbXiNghiep;
        private DevExpress.XtraEditors.LabelControl lbDonVi;
        private DevExpress.XtraEditors.TextEdit txtTu;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_XI_NGHIEP;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_DON_VI;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_TO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDen;
    }
}
