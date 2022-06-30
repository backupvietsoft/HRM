namespace Vs.HRM
{
    partial class frmInHopDongCN
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.chkDaThamGia = new DevExpress.XtraEditors.CheckEdit();
            this.dNgayIn = new DevExpress.XtraEditors.DateEdit();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.NONN_HoTenCN = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDaThamGia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).BeginInit();
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
            this.windowsUIButton.Location = new System.Drawing.Point(0, 251);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(447, 34);
            this.windowsUIButton.TabIndex = 8;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F)});
            this.tablePanel1.Controls.Add(this.chkDaThamGia);
            this.tablePanel1.Controls.Add(this.dNgayIn);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.rdo_ChonBaoCao);
            this.tablePanel1.Controls.Add(this.NONN_HoTenCN);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 56F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 60F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(447, 251);
            this.tablePanel1.TabIndex = 9;
            // 
            // chkDaThamGia
            // 
            this.tablePanel1.SetColumn(this.chkDaThamGia, 1);
            this.chkDaThamGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDaThamGia.Location = new System.Drawing.Point(21, 223);
            this.chkDaThamGia.Name = "chkDaThamGia";
            this.chkDaThamGia.Properties.Caption = "Đã tham gia";
            this.tablePanel1.SetRow(this.chkDaThamGia, 6);
            this.chkDaThamGia.Size = new System.Drawing.Size(181, 25);
            this.chkDaThamGia.TabIndex = 4;
            this.chkDaThamGia.CheckedChanged += new System.EventHandler(this.chkDaThamGia_CheckedChanged);
            // 
            // dNgayIn
            // 
            this.tablePanel1.SetColumn(this.dNgayIn, 2);
            this.dNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dNgayIn.EditValue = null;
            this.dNgayIn.Location = new System.Drawing.Point(208, 35);
            this.dNgayIn.Name = "dNgayIn";
            this.dNgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dNgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dNgayIn, 1);
            this.dNgayIn.Size = new System.Drawing.Size(106, 24);
            this.dNgayIn.TabIndex = 3;
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 1);
            this.lbNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgayIn.Location = new System.Drawing.Point(21, 35);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 1);
            this.lbNgayIn.Size = new System.Drawing.Size(181, 26);
            this.lbNgayIn.TabIndex = 2;
            this.lbNgayIn.Text = "labelControl1";
            // 
            // rdo_ChonBaoCao
            // 
            this.tablePanel1.SetColumn(this.rdo_ChonBaoCao, 1);
            this.tablePanel1.SetColumnSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(21, 75);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("", "Hợp đồng lao động", true, "rdo_HopDongLaoDong"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_HopDongThuViec", "Hợp đồng thử việc CD, DH", true, "rdo_HopDongThuViec"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_HopDongThucViecCN_QC", "Hợp đồng thử việc CN, QC", true, "rdo_HopDongThucViecCN_QC"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Hợp đồng đào tạo", true, "rdo_HopDongDaoTao"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_ToKhaiBHXH", "Tờ khai cấp sổ BHXH", true, "rdo_ToKhaiBHXH")});
            this.tablePanel1.SetRow(this.rdo_ChonBaoCao, 3);
            this.tablePanel1.SetRowSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(405, 142);
            this.rdo_ChonBaoCao.TabIndex = 1;
            this.rdo_ChonBaoCao.SelectedIndexChanged += new System.EventHandler(this.rdo_ChonBaoCao_SelectedIndexChanged);
            // 
            // NONN_HoTenCN
            // 
            this.NONN_HoTenCN.Appearance.ForeColor = System.Drawing.Color.Red;
            this.NONN_HoTenCN.Appearance.Options.UseForeColor = true;
            this.NONN_HoTenCN.Appearance.Options.UseTextOptions = true;
            this.NONN_HoTenCN.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NONN_HoTenCN.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.tablePanel1.SetColumn(this.NONN_HoTenCN, 1);
            this.tablePanel1.SetColumnSpan(this.NONN_HoTenCN, 2);
            this.NONN_HoTenCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NONN_HoTenCN.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.NONN_HoTenCN.Location = new System.Drawing.Point(21, 3);
            this.NONN_HoTenCN.Name = "NONN_HoTenCN";
            this.tablePanel1.SetRow(this.NONN_HoTenCN, 0);
            this.NONN_HoTenCN.Size = new System.Drawing.Size(293, 26);
            this.NONN_HoTenCN.TabIndex = 0;
            this.NONN_HoTenCN.Text = "labelControl1";
            // 
            // frmInHopDongCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 285);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmInHopDongCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInQTCT";
            this.Load += new System.EventHandler(this.formInHopDongCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDaThamGia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl NONN_HoTenCN;
        private DevExpress.XtraEditors.DateEdit dNgayIn;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private DevExpress.XtraEditors.CheckEdit chkDaThamGia;
    }
}