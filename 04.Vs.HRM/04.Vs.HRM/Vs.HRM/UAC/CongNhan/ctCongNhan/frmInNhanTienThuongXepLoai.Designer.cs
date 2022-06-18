namespace Vs.HRM
{
    partial class frmInNhanTienThuongXepLoai
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lbDenNgay = new DevExpress.XtraEditors.LabelControl();
            this.lbTuNgay = new DevExpress.XtraEditors.LabelControl();
            this.NONN_HoTenCN = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.NgayIn = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties)).BeginInit();
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
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "In", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 246);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(650, 40);
            this.windowsUIButton.TabIndex = 8;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // dtDenNgay
            // 
            this.tablePanel1.SetColumn(this.dtDenNgay, 4);
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(482, 35);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dtDenNgay, 1);
            this.dtDenNgay.Size = new System.Drawing.Size(148, 26);
            this.dtDenNgay.TabIndex = 3;
            // 
            // lbDenNgay
            // 
            this.tablePanel1.SetColumn(this.lbDenNgay, 3);
            this.lbDenNgay.Location = new System.Drawing.Point(343, 38);
            this.lbDenNgay.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.lbDenNgay.Name = "lbDenNgay";
            this.tablePanel1.SetRow(this.lbDenNgay, 1);
            this.lbDenNgay.Size = new System.Drawing.Size(66, 20);
            this.lbDenNgay.TabIndex = 2;
            this.lbDenNgay.Text = "Đến ngày:";
            // 
            // lbTuNgay
            // 
            this.tablePanel1.SetColumn(this.lbTuNgay, 1);
            this.lbTuNgay.Location = new System.Drawing.Point(21, 38);
            this.lbTuNgay.Name = "lbTuNgay";
            this.tablePanel1.SetRow(this.lbTuNgay, 1);
            this.lbTuNgay.Size = new System.Drawing.Size(56, 20);
            this.lbTuNgay.TabIndex = 2;
            this.lbTuNgay.Text = "Từ ngày:";
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
            this.NONN_HoTenCN.Size = new System.Drawing.Size(609, 26);
            this.NONN_HoTenCN.TabIndex = 0;
            this.NONN_HoTenCN.Text = "CHỌN HÌNH THỨC IN BÁO CÁO";
            // 
            // dtTuNgay
            // 
            this.tablePanel1.SetColumn(this.dtTuNgay, 2);
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.Location = new System.Drawing.Point(175, 35);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dtTuNgay, 1);
            this.dtTuNgay.Size = new System.Drawing.Size(148, 26);
            this.dtTuNgay.TabIndex = 3;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F)});
            this.tablePanel1.Controls.Add(this.NgayIn);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.dtTuNgay);
            this.tablePanel1.Controls.Add(this.NONN_HoTenCN);
            this.tablePanel1.Controls.Add(this.lbTuNgay);
            this.tablePanel1.Controls.Add(this.lbDenNgay);
            this.tablePanel1.Controls.Add(this.dtDenNgay);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(650, 246);
            this.tablePanel1.TabIndex = 9;
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 3);
            this.lbNgayIn.Location = new System.Drawing.Point(343, 70);
            this.lbNgayIn.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 2);
            this.lbNgayIn.Size = new System.Drawing.Size(90, 20);
            this.lbNgayIn.TabIndex = 4;
            this.lbNgayIn.Text = "labelControl1";
            // 
            // NgayIn
            // 
            this.tablePanel1.SetColumn(this.NgayIn, 4);
            this.NgayIn.EditValue = null;
            this.NgayIn.Location = new System.Drawing.Point(482, 67);
            this.NgayIn.Name = "NgayIn";
            this.NgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.NgayIn, 2);
            this.NgayIn.Size = new System.Drawing.Size(148, 26);
            this.NgayIn.TabIndex = 5;
            // 
            // frmInNhanTienThuongXepLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 286);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmInNhanTienThuongXepLoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NHẬN TIỀN THƯỞNG XẾP LOẠI";
            this.Load += new System.EventHandler(this.formInLuongCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.LabelControl NONN_HoTenCN;
        private DevExpress.XtraEditors.LabelControl lbTuNgay;
        private DevExpress.XtraEditors.LabelControl lbDenNgay;
        private DevExpress.XtraEditors.DateEdit NgayIn;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
    }
}