namespace Vs.HRM
{
    partial class ucBaoCaoTangLaoDong
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
            this.lbXiNghiep = new DevExpress.Utils.Layout.TablePanel();
            this.popNam = new DevExpress.XtraEditors.PopupContainerControl();
            this.calNam = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.txtNam = new Commons.MPopupContainerEdit();
            this.rdo_ChonBaoCao = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lbTo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbDonVi = new DevExpress.XtraEditors.LabelControl();
            this.lbNgay = new DevExpress.XtraEditors.LabelControl();
            this.lk_NgayIn = new DevExpress.XtraEditors.DateEdit();
            this.LK_XI_NGHIEP = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.LK_DON_VI = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.LK_TO = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbXiNghiep)).BeginInit();
            this.lbXiNghiep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popNam)).BeginInit();
            this.popNam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calNam.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_TO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.lbXiNghiep);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1128, 618);
            this.layoutControl1.TabIndex = 17;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lbXiNghiep
            // 
            this.lbXiNghiep.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 14.7F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 19.7F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 14.4F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 18.4F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 15.5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 17.3F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 28F)});
            this.lbXiNghiep.Controls.Add(this.popNam);
            this.lbXiNghiep.Controls.Add(this.txtNam);
            this.lbXiNghiep.Controls.Add(this.rdo_ChonBaoCao);
            this.lbXiNghiep.Controls.Add(this.labelControl6);
            this.lbXiNghiep.Controls.Add(this.lbTo);
            this.lbXiNghiep.Controls.Add(this.labelControl2);
            this.lbXiNghiep.Controls.Add(this.lbDonVi);
            this.lbXiNghiep.Controls.Add(this.lbNgay);
            this.lbXiNghiep.Controls.Add(this.lk_NgayIn);
            this.lbXiNghiep.Controls.Add(this.LK_XI_NGHIEP);
            this.lbXiNghiep.Controls.Add(this.LK_DON_VI);
            this.lbXiNghiep.Controls.Add(this.LK_TO);
            this.lbXiNghiep.Controls.Add(this.labelControl4);
            this.lbXiNghiep.Controls.Add(this.labelControl5);
            this.lbXiNghiep.Controls.Add(this.dDenNgay);
            this.lbXiNghiep.Controls.Add(this.dTuNgay);
            this.lbXiNghiep.Location = new System.Drawing.Point(6, 6);
            this.lbXiNghiep.Name = "lbXiNghiep";
            this.lbXiNghiep.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 40F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 35F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 34F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 33F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 32F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 135F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.lbXiNghiep.Size = new System.Drawing.Size(1116, 606);
            this.lbXiNghiep.TabIndex = 4;
            // 
            // popNam
            // 
            this.lbXiNghiep.SetColumn(this.popNam, 3);
            this.lbXiNghiep.SetColumnSpan(this.popNam, 2);
            this.popNam.Controls.Add(this.calNam);
            this.popNam.Location = new System.Drawing.Point(396, 283);
            this.popNam.Name = "popNam";
            this.lbXiNghiep.SetRow(this.popNam, 5);
            this.lbXiNghiep.SetRowSpan(this.popNam, 2);
            this.popNam.Size = new System.Drawing.Size(341, 214);
            this.popNam.TabIndex = 19;
            // 
            // calNam
            // 
            this.calNam.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calNam.Location = new System.Drawing.Point(0, 0);
            this.calNam.Name = "calNam";
            this.calNam.Size = new System.Drawing.Size(386, 313);
            this.calNam.TabIndex = 18;
            this.calNam.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.calNam.DateTimeCommit += new System.EventHandler(this.calNam_DateTimeCommit);
            // 
            // txtNam
            // 
            this.lbXiNghiep.SetColumn(this.txtNam, 4);
            this.txtNam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNam.Location = new System.Drawing.Point(549, 114);
            this.txtNam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNam.Name = "txtNam";
            this.txtNam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNam.Properties.DefaultActionButtonIndex = 0;
            this.txtNam.Properties.DefaultPopupControl = this.popNam;
            this.txtNam.Properties.DifferentActionButtonIndex = 0;
            this.txtNam.Properties.DifferentPopupControl = null;
            this.lbXiNghiep.SetRow(this.txtNam, 3);
            this.txtNam.Size = new System.Drawing.Size(187, 26);
            this.txtNam.TabIndex = 7;
            this.txtNam.BeforePopup += new System.EventHandler(this.mPopupContainerEdit1_BeforePopup);
            // 
            // rdo_ChonBaoCao
            // 
            this.lbXiNghiep.SetColumn(this.rdo_ChonBaoCao, 1);
            this.lbXiNghiep.SetColumnSpan(this.rdo_ChonBaoCao, 2);
            this.rdo_ChonBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdo_ChonBaoCao.Location = new System.Drawing.Point(31, 80);
            this.rdo_ChonBaoCao.Margin = new System.Windows.Forms.Padding(3, 5, 4, 5);
            this.rdo_ChonBaoCao.Name = "rdo_ChonBaoCao";
            this.rdo_ChonBaoCao.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Giai đoạn", true, "rdo_GiaiDoan"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 tháng đầu năm", true, "rdo_6ThangDauNam"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "6 tháng cuối năm", true, "rdo_6ThangCuoiNam")});
            this.lbXiNghiep.SetRow(this.rdo_ChonBaoCao, 2);
            this.lbXiNghiep.SetRowSpan(this.rdo_ChonBaoCao, 3);
            this.rdo_ChonBaoCao.Size = new System.Drawing.Size(358, 89);
            this.rdo_ChonBaoCao.TabIndex = 4;
            this.rdo_ChonBaoCao.SelectedIndexChanged += new System.EventHandler(this.rdo_ChonBaoCao_SelectedIndexChanged);
            // 
            // labelControl6
            // 
            this.lbXiNghiep.SetColumn(this.labelControl6, 3);
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(425, 112);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.labelControl6.Name = "labelControl6";
            this.lbXiNghiep.SetRow(this.labelControl6, 3);
            this.labelControl6.Size = new System.Drawing.Size(118, 27);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "labelControl6";
            // 
            // lbTo
            // 
            this.lbXiNghiep.SetColumn(this.lbTo, 5);
            this.lbTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTo.Location = new System.Drawing.Point(772, 43);
            this.lbTo.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbTo.Name = "lbTo";
            this.lbXiNghiep.SetRow(this.lbTo, 1);
            this.lbTo.Size = new System.Drawing.Size(129, 29);
            this.lbTo.TabIndex = 11;
            this.lbTo.Text = "labelControl3";
            // 
            // labelControl2
            // 
            this.lbXiNghiep.SetColumn(this.labelControl2, 3);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(425, 43);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.labelControl2.Name = "labelControl2";
            this.lbXiNghiep.SetRow(this.labelControl2, 1);
            this.labelControl2.Size = new System.Drawing.Size(118, 29);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "labelControl2";
            // 
            // lbDonVi
            // 
            this.lbXiNghiep.SetColumn(this.lbDonVi, 1);
            this.lbDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDonVi.Location = new System.Drawing.Point(31, 43);
            this.lbDonVi.Name = "lbDonVi";
            this.lbXiNghiep.SetRow(this.lbDonVi, 1);
            this.lbDonVi.Size = new System.Drawing.Size(150, 29);
            this.lbDonVi.TabIndex = 9;
            this.lbDonVi.Text = "labelControl1";
            // 
            // lbNgay
            // 
            this.lbXiNghiep.SetColumn(this.lbNgay, 5);
            this.lbNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNgay.Location = new System.Drawing.Point(772, 112);
            this.lbNgay.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.lbNgay.Name = "lbNgay";
            this.lbXiNghiep.SetRow(this.lbNgay, 3);
            this.lbNgay.Size = new System.Drawing.Size(129, 27);
            this.lbNgay.TabIndex = 7;
            this.lbNgay.Text = "lbNgay";
            // 
            // lk_NgayIn
            // 
            this.lk_NgayIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXiNghiep.SetColumn(this.lk_NgayIn, 6);
            this.lk_NgayIn.EditValue = null;
            this.lk_NgayIn.Location = new System.Drawing.Point(909, 114);
            this.lk_NgayIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lk_NgayIn.Name = "lk_NgayIn";
            this.lk_NgayIn.Properties.Appearance.Options.UseTextOptions = true;
            this.lk_NgayIn.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lk_NgayIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lk_NgayIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.lk_NgayIn, 3);
            this.lk_NgayIn.Size = new System.Drawing.Size(175, 26);
            this.lk_NgayIn.TabIndex = 8;
            // 
            // LK_XI_NGHIEP
            // 
            this.lbXiNghiep.SetColumn(this.LK_XI_NGHIEP, 4);
            this.LK_XI_NGHIEP.Location = new System.Drawing.Point(549, 45);
            this.LK_XI_NGHIEP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LK_XI_NGHIEP.Name = "LK_XI_NGHIEP";
            this.LK_XI_NGHIEP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.LK_XI_NGHIEP, 1);
            this.LK_XI_NGHIEP.Size = new System.Drawing.Size(187, 26);
            this.LK_XI_NGHIEP.TabIndex = 2;
            this.LK_XI_NGHIEP.EditValueChanged += new System.EventHandler(this.LK_XI_NGHIEP_EditValueChanged);
            // 
            // LK_DON_VI
            // 
            this.lbXiNghiep.SetColumn(this.LK_DON_VI, 2);
            this.LK_DON_VI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LK_DON_VI.Location = new System.Drawing.Point(188, 45);
            this.LK_DON_VI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LK_DON_VI.Name = "LK_DON_VI";
            this.LK_DON_VI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.LK_DON_VI, 1);
            this.LK_DON_VI.Size = new System.Drawing.Size(201, 26);
            this.LK_DON_VI.TabIndex = 1;
            this.LK_DON_VI.EditValueChanged += new System.EventHandler(this.LK_DON_VI_EditValueChanged);
            // 
            // LK_TO
            // 
            this.lbXiNghiep.SetColumn(this.LK_TO, 6);
            this.LK_TO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LK_TO.Location = new System.Drawing.Point(909, 45);
            this.LK_TO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LK_TO.Name = "LK_TO";
            this.LK_TO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.LK_TO, 1);
            this.LK_TO.Size = new System.Drawing.Size(175, 26);
            this.LK_TO.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.lbXiNghiep.SetColumn(this.labelControl4, 3);
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(425, 78);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.labelControl4.Name = "labelControl4";
            this.lbXiNghiep.SetRow(this.labelControl4, 2);
            this.labelControl4.Size = new System.Drawing.Size(118, 28);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "labelControl3";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lbXiNghiep.SetColumn(this.labelControl5, 5);
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(772, 78);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(32, 3, 3, 3);
            this.labelControl5.Name = "labelControl5";
            this.lbXiNghiep.SetRow(this.labelControl5, 2);
            this.labelControl5.Size = new System.Drawing.Size(129, 28);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "labelControl3";
            // 
            // dDenNgay
            // 
            this.dDenNgay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXiNghiep.SetColumn(this.dDenNgay, 6);
            this.dDenNgay.EditValue = null;
            this.dDenNgay.Location = new System.Drawing.Point(909, 80);
            this.dDenNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dDenNgay.Name = "dDenNgay";
            this.dDenNgay.Properties.Appearance.Options.UseTextOptions = true;
            this.dDenNgay.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.dDenNgay, 2);
            this.dDenNgay.Size = new System.Drawing.Size(175, 26);
            this.dDenNgay.TabIndex = 6;
            // 
            // dTuNgay
            // 
            this.lbXiNghiep.SetColumn(this.dTuNgay, 4);
            this.dTuNgay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dTuNgay.EditValue = null;
            this.dTuNgay.Location = new System.Drawing.Point(549, 80);
            this.dTuNgay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dTuNgay.Name = "dTuNgay";
            this.dTuNgay.Properties.Appearance.Options.UseTextOptions = true;
            this.dTuNgay.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbXiNghiep.SetRow(this.dTuNgay, 2);
            this.dTuNgay.Size = new System.Drawing.Size(187, 26);
            this.dTuNgay.TabIndex = 5;
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
            this.layoutControlItem1.Control = this.lbXiNghiep;
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
            // ucBaoCaoTangLaoDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucBaoCaoTangLaoDong";
            this.Size = new System.Drawing.Size(1128, 658);
            this.Load += new System.EventHandler(this.ucBaoCaoTangLaoDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbXiNghiep)).EndInit();
            this.lbXiNghiep.ResumeLayout(false);
            this.lbXiNghiep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popNam)).EndInit();
            this.popNam.ResumeLayout(false);
            this.popNam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calNam.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo_ChonBaoCao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lk_NgayIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_XI_NGHIEP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_DON_VI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LK_TO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTuNgay.Properties)).EndInit();
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
        private DevExpress.Utils.Layout.TablePanel lbXiNghiep;
        private DevExpress.XtraEditors.DateEdit lk_NgayIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lbNgay;
        private DevExpress.XtraEditors.LabelControl lbTo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbDonVi;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_XI_NGHIEP;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_DON_VI;
        private DevExpress.XtraEditors.SearchLookUpEdit LK_TO;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dDenNgay;
        private DevExpress.XtraEditors.DateEdit dTuNgay;
        private DevExpress.XtraEditors.RadioGroup rdo_ChonBaoCao;
        private Commons.MPopupContainerEdit txtNam;
        private DevExpress.XtraEditors.Controls.CalendarControl calNam;
        private DevExpress.XtraEditors.PopupContainerControl popNam;
    }
}
