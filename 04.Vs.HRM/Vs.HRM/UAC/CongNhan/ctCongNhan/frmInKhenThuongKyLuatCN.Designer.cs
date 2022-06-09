namespace Vs.HRM
{
    partial class frmInKhenThuongKyLuatCN
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.dNgayIn = new DevExpress.XtraEditors.DateEdit();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.NONN_HoTenCN = new DevExpress.XtraEditors.LabelControl();
            this.lbTuNgay = new DevExpress.XtraEditors.LabelControl();
            this.dTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lbDenNgay = new DevExpress.XtraEditors.LabelControl();
            this.dDenNgay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties)).BeginInit();
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
            windowsUIButtonImageOptions3.ImageUri.Uri = "Print";
            windowsUIButtonImageOptions4.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "In", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 246);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(501, 40);
            this.windowsUIButton.TabIndex = 8;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F)});
            this.tablePanel1.Controls.Add(this.dNgayIn);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.rdo_ChonBaoCao);
            this.tablePanel1.Controls.Add(this.NONN_HoTenCN);
            this.tablePanel1.Controls.Add(this.lbTuNgay);
            this.tablePanel1.Controls.Add(this.dTuNgay);
            this.tablePanel1.Controls.Add(this.lbDenNgay);
            this.tablePanel1.Controls.Add(this.dDenNgay);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F)});
            this.tablePanel1.Size = new System.Drawing.Size(501, 246);
            this.tablePanel1.TabIndex = 9;
            // 
            // dNgayIn
            // 
            this.tablePanel1.SetColumn(this.dNgayIn, 4);
            this.dNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dNgayIn.EditValue = null;
            this.dNgayIn.Location = new System.Drawing.Point(381, 67);
            this.dNgayIn.Name = "dNgayIn";
            this.dNgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dNgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dNgayIn, 2);
            this.dNgayIn.Size = new System.Drawing.Size(114, 26);
            this.dNgayIn.TabIndex = 3;
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 3);
            this.lbNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgayIn.Location = new System.Drawing.Point(276, 67);
            this.lbNgayIn.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 2);
            this.lbNgayIn.Size = new System.Drawing.Size(99, 26);
            this.lbNgayIn.TabIndex = 2;
            this.lbNgayIn.Text = "labelControl1";
            // 
            // rdo_ChonBaoCao
            // 
            this.tablePanel1.SetColumn(this.rdo_ChonBaoCao, 1);
            this.tablePanel1.SetColumnSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(21, 99);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_KhenThuong", "Khen thưởng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_KyLuat", "Kỷ luật"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Biên bản cảnh cáo", true, "rdo_BienBangCanhCao")});
            this.rdo_ChonBaoCao.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Column;
            this.tablePanel1.SetRow(this.rdo_ChonBaoCao, 3);
            this.tablePanel1.SetRowSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(354, 90);
            this.rdo_ChonBaoCao.TabIndex = 1;
            // 
            // NONN_HoTenCN
            // 
            this.NONN_HoTenCN.Appearance.ForeColor = System.Drawing.Color.Red;
            this.NONN_HoTenCN.Appearance.Options.UseForeColor = true;
            this.NONN_HoTenCN.Appearance.Options.UseTextOptions = true;
            this.NONN_HoTenCN.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NONN_HoTenCN.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.tablePanel1.SetColumn(this.NONN_HoTenCN, 1);
            this.tablePanel1.SetColumnSpan(this.NONN_HoTenCN, 4);
            this.NONN_HoTenCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NONN_HoTenCN.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.NONN_HoTenCN.Location = new System.Drawing.Point(21, 3);
            this.NONN_HoTenCN.Name = "NONN_HoTenCN";
            this.tablePanel1.SetRow(this.NONN_HoTenCN, 0);
            this.NONN_HoTenCN.Size = new System.Drawing.Size(474, 26);
            this.NONN_HoTenCN.TabIndex = 0;
            this.NONN_HoTenCN.Text = "labelControl1";
            // 
            // lbTuNgay
            // 
            this.tablePanel1.SetColumn(this.lbTuNgay, 1);
            this.lbTuNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTuNgay.Location = new System.Drawing.Point(21, 35);
            this.lbTuNgay.Name = "lbTuNgay";
            this.tablePanel1.SetRow(this.lbTuNgay, 1);
            this.lbTuNgay.Size = new System.Drawing.Size(114, 26);
            this.lbTuNgay.TabIndex = 2;
            this.lbTuNgay.Text = "labelControl1";
            // 
            // dTuNgay
            // 
            this.tablePanel1.SetColumn(this.dTuNgay, 2);
            this.dTuNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dTuNgay.EditValue = null;
            this.dTuNgay.Location = new System.Drawing.Point(141, 35);
            this.dTuNgay.Name = "dTuNgay";
            this.dTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dTuNgay, 1);
            this.dTuNgay.Size = new System.Drawing.Size(114, 26);
            this.dTuNgay.TabIndex = 3;
            // 
            // lbDenNgay
            // 
            this.tablePanel1.SetColumn(this.lbDenNgay, 3);
            this.lbDenNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDenNgay.Location = new System.Drawing.Point(276, 35);
            this.lbDenNgay.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.lbDenNgay.Name = "lbDenNgay";
            this.tablePanel1.SetRow(this.lbDenNgay, 1);
            this.lbDenNgay.Size = new System.Drawing.Size(99, 26);
            this.lbDenNgay.TabIndex = 2;
            this.lbDenNgay.Text = "labelControl1";
            // 
            // dDenNgay
            // 
            this.tablePanel1.SetColumn(this.dDenNgay, 4);
            this.dDenNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dDenNgay.EditValue = null;
            this.dDenNgay.Location = new System.Drawing.Point(381, 35);
            this.dDenNgay.Name = "dDenNgay";
            this.dDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dDenNgay, 1);
            this.dDenNgay.Size = new System.Drawing.Size(114, 26);
            this.dDenNgay.TabIndex = 3;
            // 
            // frmInKhenThuongKyLuatCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 286);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmInKhenThuongKyLuatCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInQTCT";
            this.Load += new System.EventHandler(this.formInKhenThuongKyLuatCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl NONN_HoTenCN;
        private DevExpress.XtraEditors.DateEdit dNgayIn;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private DevExpress.XtraEditors.LabelControl lbTuNgay;
        private DevExpress.XtraEditors.DateEdit dTuNgay;
        private DevExpress.XtraEditors.LabelControl lbDenNgay;
        private DevExpress.XtraEditors.DateEdit dDenNgay;
    }
}