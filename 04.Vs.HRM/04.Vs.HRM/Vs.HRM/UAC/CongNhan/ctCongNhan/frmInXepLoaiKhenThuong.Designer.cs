namespace Vs.HRM
{
    partial class frmXepLoaiKhenThuong
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
            this.dtNam = new DevExpress.XtraEditors.DateEdit();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.lbChonBaoCao = new DevExpress.XtraEditors.LabelControl();
            this.dtThang = new DevExpress.XtraEditors.DateEdit();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.NgayIn = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtNam.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties)).BeginInit();
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
            this.windowsUIButton.Size = new System.Drawing.Size(650, 40);
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
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F)});
            this.tablePanel1.Controls.Add(this.NgayIn);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.dtNam);
            this.tablePanel1.Controls.Add(this.rdo_ChonBaoCao);
            this.tablePanel1.Controls.Add(this.lbChonBaoCao);
            this.tablePanel1.Controls.Add(this.dtThang);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F)});
            this.tablePanel1.Size = new System.Drawing.Size(650, 246);
            this.tablePanel1.TabIndex = 9;
            // 
            // dtNam
            // 
            this.tablePanel1.SetColumn(this.dtNam, 3);
            this.dtNam.EditValue = null;
            this.dtNam.Location = new System.Drawing.Point(343, 131);
            this.dtNam.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.dtNam.Name = "dtNam";
            this.dtNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNam.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNam.Properties.DisplayFormat.FormatString = "yyyy";
            this.dtNam.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtNam.Properties.EditFormat.FormatString = "yyyy";
            this.dtNam.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtNam.Properties.Mask.EditMask = "yyyy";
            this.dtNam.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.tablePanel1.SetRow(this.dtNam, 4);
            this.dtNam.Size = new System.Drawing.Size(133, 26);
            this.dtNam.TabIndex = 3;
            // 
            // rdo_ChonBaoCao
            // 
            this.tablePanel1.SetColumn(this.rdo_ChonBaoCao, 1);
            this.tablePanel1.SetColumnSpan(this.rdo_ChonBaoCao, 2);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(21, 99);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.Columns = 1;
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("raTongHopThang", "Tổng hợp tháng"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("raTongHopNam", "Tổng hợp năm")});
            this.tablePanel1.SetRow(this.rdo_ChonBaoCao, 3);
            this.tablePanel1.SetRowSpan(this.rdo_ChonBaoCao, 2);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(302, 58);
            this.rdo_ChonBaoCao.TabIndex = 1;
            this.rdo_ChonBaoCao.SelectedIndexChanged += new System.EventHandler(this.rdo_ChonBaoCao_SelectedIndexChanged);
            // 
            // lbChonBaoCao
            // 
            this.lbChonBaoCao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbChonBaoCao.Appearance.Options.UseForeColor = true;
            this.lbChonBaoCao.Appearance.Options.UseTextOptions = true;
            this.lbChonBaoCao.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbChonBaoCao.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.tablePanel1.SetColumn(this.lbChonBaoCao, 1);
            this.tablePanel1.SetColumnSpan(this.lbChonBaoCao, 4);
            this.lbChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbChonBaoCao.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lbChonBaoCao.Location = new System.Drawing.Point(21, 3);
            this.lbChonBaoCao.Name = "lbChonBaoCao";
            this.tablePanel1.SetRow(this.lbChonBaoCao, 0);
            this.lbChonBaoCao.Size = new System.Drawing.Size(609, 26);
            this.lbChonBaoCao.TabIndex = 0;
            this.lbChonBaoCao.Text = "Chọn báo cáo";
            // 
            // dtThang
            // 
            this.tablePanel1.SetColumn(this.dtThang, 3);
            this.dtThang.EditValue = null;
            this.dtThang.Location = new System.Drawing.Point(343, 99);
            this.dtThang.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.dtThang.Name = "dtThang";
            this.dtThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtThang.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtThang.Properties.EditFormat.FormatString = "MM/yyyy";
            this.dtThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtThang.Properties.Mask.EditMask = "MM/yyyy";
            this.dtThang.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.tablePanel1.SetRow(this.dtThang, 3);
            this.dtThang.Size = new System.Drawing.Size(133, 26);
            this.dtThang.TabIndex = 3;
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 3);
            this.lbNgayIn.Location = new System.Drawing.Point(343, 38);
            this.lbNgayIn.Margin = new System.Windows.Forms.Padding(18, 3, 3, 3);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 1);
            this.lbNgayIn.Size = new System.Drawing.Size(90, 20);
            this.lbNgayIn.TabIndex = 4;
            this.lbNgayIn.Text = "labelControl1";
            // 
            // NgayIn
            // 
            this.tablePanel1.SetColumn(this.NgayIn, 4);
            this.NgayIn.EditValue = null;
            this.NgayIn.Location = new System.Drawing.Point(482, 35);
            this.NgayIn.Name = "NgayIn";
            this.NgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.NgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.NgayIn, 1);
            this.NgayIn.Size = new System.Drawing.Size(148, 26);
            this.NgayIn.TabIndex = 5;
            // 
            // frmXepLoaiKhenThuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 286);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmXepLoaiKhenThuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInXepLoaiKhenThuong";
            this.Load += new System.EventHandler(this.formInQTCT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtNam.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NgayIn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl lbChonBaoCao;
        private DevExpress.XtraEditors.DateEdit dtNam;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private DevExpress.XtraEditors.DateEdit dtThang;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
        private DevExpress.XtraEditors.DateEdit NgayIn;
    }
}