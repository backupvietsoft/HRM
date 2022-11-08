﻿namespace Vs.TimeAttendance
{
    partial class frmBaoCaoDangKyLamThem
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
            this.dNgayIn = new DevExpress.XtraEditors.DateEdit();
            this.lbNgayIn = new DevExpress.XtraEditors.LabelControl();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Vs.TimeAttendance.UAC.BaoCao.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
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
            this.windowsUIButton.Location = new System.Drawing.Point(0, 145);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(407, 34);
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
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 18F)});
            this.tablePanel1.Controls.Add(this.dNgayIn);
            this.tablePanel1.Controls.Add(this.lbNgayIn);
            this.tablePanel1.Controls.Add(this.rdo_ChonBaoCao);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 8F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 56F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 10F)});
            this.tablePanel1.Size = new System.Drawing.Size(407, 145);
            this.tablePanel1.TabIndex = 9;
            // 
            // dNgayIn
            // 
            this.tablePanel1.SetColumn(this.dNgayIn, 2);
            this.dNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dNgayIn.EditValue = null;
            this.dNgayIn.Location = new System.Drawing.Point(164, 13);
            this.dNgayIn.Name = "dNgayIn";
            this.dNgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dNgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tablePanel1.SetRow(this.dNgayIn, 1);
            this.dNgayIn.Size = new System.Drawing.Size(137, 24);
            this.dNgayIn.TabIndex = 3;
            // 
            // lbNgayIn
            // 
            this.tablePanel1.SetColumn(this.lbNgayIn, 1);
            this.lbNgayIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgayIn.Location = new System.Drawing.Point(21, 13);
            this.lbNgayIn.Name = "lbNgayIn";
            this.tablePanel1.SetRow(this.lbNgayIn, 1);
            this.lbNgayIn.Size = new System.Drawing.Size(137, 26);
            this.lbNgayIn.TabIndex = 2;
            this.lbNgayIn.Text = "labelControl1";
            // 
            // rdo_ChonBaoCao
            // 
            this.tablePanel1.SetColumn(this.rdo_ChonBaoCao, 1);
            this.tablePanel1.SetColumnSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(21, 53);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Biểu mẫu đăng ký làm thêm giờ", true, "rdo_BieuMauLamThemGio"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Danh sách nhân viên tăng ca ngày", true, "rdo_BaoCaoNhanSuNgay")});
            this.tablePanel1.SetRow(this.rdo_ChonBaoCao, 3);
            this.tablePanel1.SetRowSpan(this.rdo_ChonBaoCao, 2);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(365, 82);
            this.rdo_ChonBaoCao.TabIndex = 1;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmBaoCaoDangKyLamThem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 179);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmBaoCaoDangKyLamThem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInQTCT";
            this.Load += new System.EventHandler(this.frmBaoCaoDangKyLamThem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dNgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.DateEdit dNgayIn;
        private DevExpress.XtraEditors.LabelControl lbNgayIn;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}