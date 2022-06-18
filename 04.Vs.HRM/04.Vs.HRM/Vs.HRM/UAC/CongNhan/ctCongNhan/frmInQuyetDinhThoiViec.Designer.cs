namespace Vs.HRM
{
    partial class frmInQuyetDinhThoiViec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInQuyetDinhThoiViec));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.btnALL = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.chkTiengAnh = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTiengAnh.Properties)).BeginInit();
            this.SuspendLayout();
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.btnALL.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "In", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.btnALL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnALL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnALL.Location = new System.Drawing.Point(0, 203);
            this.btnALL.Margin = new System.Windows.Forms.Padding(0);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(596, 40);
            this.btnALL.TabIndex = 10;
            this.btnALL.Text = "btnALLPanel1";
            this.btnALL.UseButtonBackgroundImages = false;
            this.btnALL.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 13.28F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 88.03F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15.68F)});
            this.tablePanel1.Controls.Add(this.chkTiengAnh);
            this.tablePanel1.Controls.Add(this.rdo_ChonBaoCao);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 13F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(596, 203);
            this.tablePanel1.TabIndex = 9;
            // 
            // rdo_ChonBaoCao
            // 
            this.tablePanel1.SetColumn(this.rdo_ChonBaoCao, 1);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(57, 16);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_QuyetDinhThoiViec", "Quyết định thôi việc"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_QuyetDinhSaThai", "Quyết định sa thải"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thanh lý hợp đồng trước 2008"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_QuyetDinhThoiViecCoTroCap", "Thanh lý hợp đồng sau 2008 có trợ cấp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("rdo_QuyetDinhSaThaiCoTroCap", "Quyết định sa thải có trợ cấp"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Quyết định thôi việc vi phạm thời gian báo trước", true, "rdo_QDThoiViecViPhamThoiGianBaoTruoc")});
            this.tablePanel1.SetRow(this.rdo_ChonBaoCao, 1);
            this.tablePanel1.SetRowSpan(this.rdo_ChonBaoCao, 2);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(352, 184);
            this.rdo_ChonBaoCao.TabIndex = 1;
            // 
            // chkTiengAnh
            // 
            this.tablePanel1.SetColumn(this.chkTiengAnh, 2);
            this.chkTiengAnh.Location = new System.Drawing.Point(415, 16);
            this.chkTiengAnh.Name = "chkTiengAnh";
            this.chkTiengAnh.Properties.Caption = "checkEdit1";
            this.tablePanel1.SetRow(this.chkTiengAnh, 1);
            this.chkTiengAnh.Size = new System.Drawing.Size(114, 24);
            this.chkTiengAnh.TabIndex = 2;
            // 
            // frmInQuyetDinhThoiViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 243);
            this.Controls.Add(this.tablePanel1);
            this.Controls.Add(this.btnALL);
            this.Name = "frmInQuyetDinhThoiViec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInQTCT";
            this.Load += new System.EventHandler(this.frmInQuyetDinhThoiViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTiengAnh.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnALL;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private DevExpress.XtraEditors.CheckEdit chkTiengAnh;
    }
}