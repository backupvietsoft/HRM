namespace Vs.TimeAttendance
{
    partial class frmCapNhatNhom
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule compareAgainstControlValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule compareAgainstControlValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.CompareAgainstControlValidationRule();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapNhatNhom));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.datGioKT = new DevExpress.XtraEditors.DateEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.txtSoGioTC = new DevExpress.XtraEditors.TextEdit();
            this.txtPhutAnCa = new DevExpress.XtraEditors.TextEdit();
            this.datGioBD = new DevExpress.XtraEditors.DateEdit();
            this.cboCA = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboID_NHOM = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNHOM = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCA = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblGioBD = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblGioKT = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSoPhutAnCa = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSoGioTC = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNgay = new DevExpress.XtraLayout.SimpleLabelItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.datGioKT.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioKT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoGioTC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhutAnCa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioBD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioBD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_NHOM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNHOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGioBD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGioKT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoPhutAnCa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoGioTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // datGioKT
            // 
            this.datGioKT.EditValue = null;
            this.datGioKT.Location = new System.Drawing.Point(122, 119);
            this.datGioKT.Name = "datGioKT";
            this.datGioKT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datGioKT.Properties.CalendarDateEditing = false;
            this.datGioKT.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datGioKT.Size = new System.Drawing.Size(603, 28);
            this.datGioKT.StyleController = this.dataLayoutControl1;
            this.datGioKT.TabIndex = 13;
            compareAgainstControlValidationRule2.CompareControlOperator = DevExpress.XtraEditors.DXErrorProvider.CompareControlOperator.Less;
            compareAgainstControlValidationRule2.Control = this.datGioBD;
            compareAgainstControlValidationRule2.ErrorText = "Giờ kết thúc phải lớn hơn giờ bắt đầu";
            this.dxValidationProvider1.SetValidationRule(this.datGioKT, compareAgainstControlValidationRule2);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.txtSoGioTC);
            this.dataLayoutControl1.Controls.Add(this.txtPhutAnCa);
            this.dataLayoutControl1.Controls.Add(this.datGioKT);
            this.dataLayoutControl1.Controls.Add(this.datGioBD);
            this.dataLayoutControl1.Controls.Add(this.cboCA);
            this.dataLayoutControl1.Controls.Add(this.cboID_NHOM);
            this.dataLayoutControl1.Controls.Add(this.windowsUIButton);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(533, 0, 650, 400);
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(731, 300);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // txtSoGioTC
            // 
            this.txtSoGioTC.Location = new System.Drawing.Point(122, 179);
            this.txtSoGioTC.Name = "txtSoGioTC";
            this.txtSoGioTC.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSoGioTC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtSoGioTC.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSoGioTC.Properties.MaskSettings.Set("mask", "0.0");
            this.txtSoGioTC.Size = new System.Drawing.Size(603, 28);
            this.txtSoGioTC.StyleController = this.dataLayoutControl1;
            this.txtSoGioTC.TabIndex = 15;
            // 
            // txtPhutAnCa
            // 
            this.txtPhutAnCa.Location = new System.Drawing.Point(122, 149);
            this.txtPhutAnCa.Name = "txtPhutAnCa";
            this.txtPhutAnCa.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPhutAnCa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPhutAnCa.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPhutAnCa.Properties.MaskSettings.Set("mask", "0.0");
            this.txtPhutAnCa.Size = new System.Drawing.Size(603, 28);
            this.txtPhutAnCa.StyleController = this.dataLayoutControl1;
            this.txtPhutAnCa.TabIndex = 14;
            // 
            // datGioBD
            // 
            this.datGioBD.EditValue = null;
            this.datGioBD.Location = new System.Drawing.Point(122, 89);
            this.datGioBD.Name = "datGioBD";
            this.datGioBD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datGioBD.Properties.CalendarDateEditing = false;
            this.datGioBD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datGioBD.Size = new System.Drawing.Size(603, 28);
            this.datGioBD.StyleController = this.dataLayoutControl1;
            this.datGioBD.TabIndex = 12;
            compareAgainstControlValidationRule1.CompareControlOperator = DevExpress.XtraEditors.DXErrorProvider.CompareControlOperator.Greater;
            compareAgainstControlValidationRule1.Control = this.datGioKT;
            compareAgainstControlValidationRule1.ErrorText = "Giờ bắt đầu phải lớn hơn giờ kết thúc";
            this.dxValidationProvider1.SetValidationRule(this.datGioBD, compareAgainstControlValidationRule1);
            // 
            // cboCA
            // 
            this.cboCA.Location = new System.Drawing.Point(122, 59);
            this.cboCA.Name = "cboCA";
            this.cboCA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCA.Properties.NullText = "";
            this.cboCA.Properties.PopupView = this.searchLookUpEdit2View;
            this.cboCA.Size = new System.Drawing.Size(603, 28);
            this.cboCA.StyleController = this.dataLayoutControl1;
            this.cboCA.TabIndex = 11;
            this.cboCA.EditValueChanged += new System.EventHandler(this.cboCA_EditValueChanged);
            // 
            // searchLookUpEdit2View
            // 
            this.searchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit2View.Name = "searchLookUpEdit2View";
            this.searchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // cboID_NHOM
            // 
            this.cboID_NHOM.Location = new System.Drawing.Point(122, 29);
            this.cboID_NHOM.Name = "cboID_NHOM";
            this.cboID_NHOM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboID_NHOM.Properties.NullText = "";
            this.cboID_NHOM.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboID_NHOM.Size = new System.Drawing.Size(603, 28);
            this.cboID_NHOM.StyleController = this.dataLayoutControl1;
            this.cboID_NHOM.TabIndex = 10;
            this.cboID_NHOM.EditValueChanged += new System.EventHandler(this.cboID_nhom_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
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
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "capnhat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Location = new System.Drawing.Point(6, 254);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.windowsUIButton.Size = new System.Drawing.Size(719, 40);
            this.windowsUIButton.TabIndex = 9;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem5,
            this.layoutControlItem1,
            this.lblNHOM,
            this.lblCA,
            this.lblGioBD,
            this.lblGioKT,
            this.lblSoPhutAnCa,
            this.lblSoGioTC,
            this.lblNgay});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(731, 300);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 203);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(721, 45);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.windowsUIButton;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 248);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(721, 42);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblNHOM
            // 
            this.lblNHOM.Control = this.cboID_NHOM;
            this.lblNHOM.Location = new System.Drawing.Point(0, 23);
            this.lblNHOM.Name = "lblNHOM";
            this.lblNHOM.Size = new System.Drawing.Size(721, 30);
            this.lblNHOM.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblCA
            // 
            this.lblCA.Control = this.cboCA;
            this.lblCA.Location = new System.Drawing.Point(0, 53);
            this.lblCA.Name = "lblCA";
            this.lblCA.Size = new System.Drawing.Size(721, 30);
            this.lblCA.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblGioBD
            // 
            this.lblGioBD.Control = this.datGioBD;
            this.lblGioBD.Location = new System.Drawing.Point(0, 83);
            this.lblGioBD.Name = "lblGioBD";
            this.lblGioBD.Size = new System.Drawing.Size(721, 30);
            this.lblGioBD.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblGioKT
            // 
            this.lblGioKT.Control = this.datGioKT;
            this.lblGioKT.Location = new System.Drawing.Point(0, 113);
            this.lblGioKT.Name = "lblGioKT";
            this.lblGioKT.Size = new System.Drawing.Size(721, 30);
            this.lblGioKT.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblSoPhutAnCa
            // 
            this.lblSoPhutAnCa.Control = this.txtPhutAnCa;
            this.lblSoPhutAnCa.Location = new System.Drawing.Point(0, 143);
            this.lblSoPhutAnCa.Name = "lblSoPhutAnCa";
            this.lblSoPhutAnCa.Size = new System.Drawing.Size(721, 30);
            this.lblSoPhutAnCa.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblSoGioTC
            // 
            this.lblSoGioTC.Control = this.txtSoGioTC;
            this.lblSoGioTC.Location = new System.Drawing.Point(0, 173);
            this.lblSoGioTC.Name = "lblSoGioTC";
            this.lblSoGioTC.Size = new System.Drawing.Size(721, 30);
            this.lblSoGioTC.TextSize = new System.Drawing.Size(104, 21);
            // 
            // lblNgay
            // 
            this.lblNgay.AllowHotTrack = false;
            this.lblNgay.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblNgay.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblNgay.Location = new System.Drawing.Point(0, 0);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(721, 23);
            this.lblNgay.TextSize = new System.Drawing.Size(104, 21);
            // 
            // frmCapNhatNhom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 300);
            this.Controls.Add(this.dataLayoutControl1);
            this.MaximizeBox = false;
            this.Name = "frmCapNhatNhom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCapNhatNhom";
            this.Load += new System.EventHandler(this.frmCapNhatNhom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datGioKT.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioKT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSoGioTC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhutAnCa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioBD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datGioBD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboID_NHOM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNHOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGioBD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGioKT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoPhutAnCa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoGioTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtSoGioTC;
        private DevExpress.XtraEditors.TextEdit txtPhutAnCa;
        private DevExpress.XtraEditors.DateEdit datGioKT;
        private DevExpress.XtraEditors.DateEdit datGioBD;
        private DevExpress.XtraEditors.SearchLookUpEdit cboCA;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit2View;
        private DevExpress.XtraEditors.SearchLookUpEdit cboID_NHOM;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblNHOM;
        private DevExpress.XtraLayout.LayoutControlItem lblCA;
        private DevExpress.XtraLayout.LayoutControlItem lblGioBD;
        private DevExpress.XtraLayout.LayoutControlItem lblGioKT;
        private DevExpress.XtraLayout.LayoutControlItem lblSoPhutAnCa;
        private DevExpress.XtraLayout.LayoutControlItem lblSoGioTC;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraLayout.SimpleLabelItem lblNgay;
    }
}