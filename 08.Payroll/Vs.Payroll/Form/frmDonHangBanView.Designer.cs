namespace Vs.Payroll
{
    partial class frmDonHangBanView
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule compareAgainstControlValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule compareAgainstControlValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.txtTim = new DevExpress.XtraEditors.SearchControl();
            this.grdView = new DevExpress.XtraGrid.GridControl();
            this.grvView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.datTU_NGAY = new DevExpress.XtraEditors.DateEdit();
            this.datDEN_NGAY = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTU_NGAY = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDEN_NGAY = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTU_NGAY.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTU_NGAY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDEN_NGAY.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDEN_NGAY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTU_NGAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDEN_NGAY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.windowsUIButton);
            this.dataLayoutControl1.Controls.Add(this.txtTim);
            this.dataLayoutControl1.Controls.Add(this.grdView);
            this.dataLayoutControl1.Controls.Add(this.datTU_NGAY);
            this.dataLayoutControl1.Controls.Add(this.datDEN_NGAY);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(744, 468);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Location = new System.Drawing.Point(228, 424);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(504, 32);
            this.windowsUIButton.TabIndex = 19;
            this.windowsUIButton.Text = "S";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // txtTim
            // 
            this.txtTim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTim.Client = this.grdView;
            this.txtTim.Location = new System.Drawing.Point(12, 430);
            this.txtTim.Name = "txtTim";
            this.txtTim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.txtTim.Properties.Client = this.grdView;
            this.txtTim.Size = new System.Drawing.Size(212, 26);
            this.txtTim.StyleController = this.dataLayoutControl1;
            this.txtTim.TabIndex = 2;
            // 
            // grdView
            // 
            this.grdView.Location = new System.Drawing.Point(12, 42);
            this.grdView.MainView = this.grvView;
            this.grdView.Name = "grdView";
            this.grdView.Size = new System.Drawing.Size(720, 378);
            this.grdView.TabIndex = 7;
            this.grdView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvView});
            this.grdView.DoubleClick += new System.EventHandler(this.grdView_DoubleClick);
            // 
            // grvView
            // 
            this.grvView.DetailHeight = 217;
            this.grvView.FixedLineWidth = 1;
            this.grvView.GridControl = this.grdView;
            this.grvView.Name = "grvView";
            this.grvView.OptionsView.ShowGroupPanel = false;
            // 
            // datTU_NGAY
            // 
            this.datTU_NGAY.EditValue = null;
            this.datTU_NGAY.Location = new System.Drawing.Point(107, 12);
            this.datTU_NGAY.Name = "datTU_NGAY";
            this.datTU_NGAY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTU_NGAY.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datTU_NGAY.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.datTU_NGAY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datTU_NGAY.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.datTU_NGAY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datTU_NGAY.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.datTU_NGAY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datTU_NGAY.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datTU_NGAY.Size = new System.Drawing.Size(263, 26);
            this.datTU_NGAY.StyleController = this.dataLayoutControl1;
            this.datTU_NGAY.TabIndex = 0;
            compareAgainstControlValidationRule1.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.datTU_NGAY, compareAgainstControlValidationRule1);
            this.datTU_NGAY.EditValueChanged += new System.EventHandler(this.txtTU_NGAY_EditValueChanged);
            // 
            // datDEN_NGAY
            // 
            this.datDEN_NGAY.EditValue = null;
            this.datDEN_NGAY.Location = new System.Drawing.Point(469, 12);
            this.datDEN_NGAY.Name = "datDEN_NGAY";
            this.datDEN_NGAY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDEN_NGAY.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datDEN_NGAY.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.datDEN_NGAY.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datDEN_NGAY.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.datDEN_NGAY.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datDEN_NGAY.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.datDEN_NGAY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.datDEN_NGAY.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.datDEN_NGAY.Size = new System.Drawing.Size(263, 26);
            this.datDEN_NGAY.StyleController = this.dataLayoutControl1;
            this.datDEN_NGAY.TabIndex = 1;
            compareAgainstControlValidationRule2.CompareControlOperator = DevExpress.XtraEditors.DXErrorProvider.CompareControlOperator.GreaterOrEqual;
            compareAgainstControlValidationRule2.Control = this.datTU_NGAY;
            compareAgainstControlValidationRule2.ErrorText = "This value is not valid";
            this.dxValidationProvider1.SetValidationRule(this.datDEN_NGAY, compareAgainstControlValidationRule2);
            this.datDEN_NGAY.EditValueChanged += new System.EventHandler(this.txtDEN_NGAY_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.lblTU_NGAY,
            this.lblDEN_NGAY,
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(744, 468);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.grdView;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(724, 382);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // lblTU_NGAY
            // 
            this.lblTU_NGAY.Control = this.datTU_NGAY;
            this.lblTU_NGAY.Location = new System.Drawing.Point(0, 0);
            this.lblTU_NGAY.Name = "lblTU_NGAY";
            this.lblTU_NGAY.Size = new System.Drawing.Size(362, 30);
            this.lblTU_NGAY.TextSize = new System.Drawing.Size(92, 20);
            // 
            // lblDEN_NGAY
            // 
            this.lblDEN_NGAY.Control = this.datDEN_NGAY;
            this.lblDEN_NGAY.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblDEN_NGAY.CustomizationFormText = "layoutControlItem8";
            this.lblDEN_NGAY.Location = new System.Drawing.Point(362, 0);
            this.lblDEN_NGAY.Name = "lblDEN_NGAY";
            this.lblDEN_NGAY.Size = new System.Drawing.Size(362, 30);
            this.lblDEN_NGAY.TextSize = new System.Drawing.Size(92, 20);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.windowsUIButton;
            this.layoutControlItem2.Location = new System.Drawing.Point(216, 412);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(508, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.ContentVertAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.layoutControlItem1.Control = this.txtTim;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 412);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(216, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(216, 1);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(216, 36);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDonHangBanView
            // 
            this.ClientSize = new System.Drawing.Size(744, 468);
            this.Controls.Add(this.dataLayoutControl1);
            this.Name = "frmDonHangBanView";
            this.Text = "frmDonHangBanView";
            this.Load += new System.EventHandler(this.frmDonHangBanView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTU_NGAY.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datTU_NGAY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDEN_NGAY.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datDEN_NGAY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTU_NGAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDEN_NGAY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdView;
        private DevExpress.XtraGrid.Views.Grid.GridView grvView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem lblTU_NGAY;
        private DevExpress.XtraEditors.DateEdit datTU_NGAY;
        private DevExpress.XtraEditors.DateEdit datDEN_NGAY;
        private DevExpress.XtraLayout.LayoutControlItem lblDEN_NGAY;
        private DevExpress.XtraEditors.SearchControl txtTim;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}
