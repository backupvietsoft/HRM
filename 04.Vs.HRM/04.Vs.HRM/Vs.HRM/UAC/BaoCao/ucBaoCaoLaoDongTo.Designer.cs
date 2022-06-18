namespace Vs.HRM
{
    partial class ucBaoCaoLaoDongTo
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.lbDonVi = new DevExpress.XtraEditors.LabelControl();
            this.lbXiNghiep = new DevExpress.XtraEditors.LabelControl();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.lk_NgayIn = new DevExpress.XtraEditors.DateEdit();
            this.LK_XI_NGHIEP = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.LK_DON_VI = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).BeginInit();
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
            windowsUIButtonImageOptions2.ImageUri.Uri = "Print";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "Print", -1, false)});
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
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 35F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F)});
            this.tablePanel1.Controls.Add(this.lbDonVi);
            this.tablePanel1.Controls.Add(this.lbXiNghiep);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.lk_NgayIn);
            this.tablePanel1.Controls.Add(this.LK_XI_NGHIEP);
            this.tablePanel1.Controls.Add(this.LK_DON_VI);
            this.tablePanel1.Location = new System.Drawing.Point(6, 6);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 31F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 33F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 36F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1116, 606);
            this.tablePanel1.TabIndex = 4;
            // 
            // lbDonVi
            // 
            this.tablePanel1.SetColumn(this.lbDonVi, 1);
            this.lbDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDonVi.Location = new System.Drawing.Point(31, 48);
            this.lbDonVi.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.lbDonVi.Name = "lbDonVi";
            this.tablePanel1.SetRow(this.lbDonVi, 1);
            this.lbDonVi.Size = new System.Drawing.Size(171, 20);
            this.lbDonVi.TabIndex = 9;
            this.lbDonVi.Text = "lbDonVi";
            // 
            // lbXiNghiep
            // 
            this.tablePanel1.SetColumn(this.lbXiNghiep, 3);
            this.lbXiNghiep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbXiNghiep.Location = new System.Drawing.Point(413, 48);
            this.lbXiNghiep.Margin = new System.Windows.Forms.Padding(32, 8, 3, 3);
            this.lbXiNghiep.Name = "lbXiNghiep";
            this.tablePanel1.SetRow(this.lbXiNghiep, 1);
            this.lbXiNghiep.Size = new System.Drawing.Size(142, 20);
            this.lbXiNghiep.TabIndex = 8;
            this.lbXiNghiep.Text = "lbXiNghiep";
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 5);
            this.lbNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgayIn.Location = new System.Drawing.Point(767, 43);
            this.lbNgayIn.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 1);
            this.lbNgayIn.Size = new System.Drawing.Size(142, 25);
            this.lbNgayIn.TabIndex = 7;
            this.lbNgayIn.Text = "lbNgayIn";
            // 
            // lk_NgayIn
            // 
            this.tablePanel1.SetColumn(this.lk_NgayIn, 6);
            this.lk_NgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lk_NgayIn.EditValue = null;
            this.lk_NgayIn.Location = new System.Drawing.Point(915, 45);
            this.lk_NgayIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lk_NgayIn.Name = "lk_NgayIn";
            this.lk_NgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lk_NgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.lk_NgayIn, 1);
            this.lk_NgayIn.Size = new System.Drawing.Size(169, 26);
            this.lk_NgayIn.TabIndex = 5;
            this.lk_NgayIn.EditValueChanged += new System.EventHandler(this.lk_NgayIn_EditValueChanged);
            // 
            // LK_XI_NGHIEP
            // 
            this.tablePanel1.SetColumn(this.LK_XI_NGHIEP, 4);
            this.LK_XI_NGHIEP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LK_XI_NGHIEP.Location = new System.Drawing.Point(562, 45);
            this.LK_XI_NGHIEP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LK_XI_NGHIEP.Name = "LK_XI_NGHIEP";
            this.LK_XI_NGHIEP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.LK_XI_NGHIEP, 1);
            this.LK_XI_NGHIEP.Size = new System.Drawing.Size(169, 26);
            this.LK_XI_NGHIEP.TabIndex = 3;
            // 
            // LK_DON_VI
            // 
            this.tablePanel1.SetColumn(this.LK_DON_VI, 2);
            this.LK_DON_VI.Location = new System.Drawing.Point(209, 45);
            this.LK_DON_VI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LK_DON_VI.Name = "LK_DON_VI";
            this.LK_DON_VI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.LK_DON_VI, 1);
            this.LK_DON_VI.Size = new System.Drawing.Size(169, 26);
            this.LK_DON_VI.TabIndex = 3;
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
            // ucBaoCaoLaoDongTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucBaoCaoLaoDongTo";
            this.Size = new System.Drawing.Size(1128, 658);
            this.Load += new System.EventHandler(this.ucBaoCaoLaoDongTo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).EndInit();
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
        private DevExpress.XtraEditors.SearchLookUpEdit LK_XI_NGHIEP;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lbXiNghiep;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
        private DevExpress.XtraEditors.LabelControl lbDonVi;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_DON_VI;
    }
}
