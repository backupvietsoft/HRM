namespace Commons
{
    partial class frmYKienYC
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmYKienYC));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txtYKien = new DevExpress.XtraEditors.MemoEdit();
            this.lblYKien = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkKhanCap = new DevExpress.XtraEditors.CheckEdit();
            this.lblKhanCap = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYKien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblYKien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKhanCap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblKhanCap)).BeginInit();
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
            windowsUIButtonImageOptions5.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            windowsUIButtonImageOptions6.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thuchien", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 161);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(4);
            this.windowsUIButton.Size = new System.Drawing.Size(457, 34);
            this.windowsUIButton.TabIndex = 5;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.chkKhanCap);
            this.dataLayoutControl1.Controls.Add(this.txtYKien);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(457, 161);
            this.dataLayoutControl1.TabIndex = 7;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblYKien,
            this.lblKhanCap});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(457, 161);
            this.Root.TextVisible = false;
            // 
            // txtYKien
            // 
            this.txtYKien.Location = new System.Drawing.Point(82, 12);
            this.txtYKien.Name = "txtYKien";
            this.txtYKien.Size = new System.Drawing.Size(363, 112);
            this.txtYKien.StyleController = this.dataLayoutControl1;
            this.txtYKien.TabIndex = 4;
            // 
            // lblYKien
            // 
            this.lblYKien.Control = this.txtYKien;
            this.lblYKien.Location = new System.Drawing.Point(0, 0);
            this.lblYKien.Name = "lblYKien";
            this.lblYKien.Size = new System.Drawing.Size(437, 116);
            this.lblYKien.TextSize = new System.Drawing.Size(66, 17);
            // 
            // chkKhanCap
            // 
            this.chkKhanCap.Location = new System.Drawing.Point(82, 128);
            this.chkKhanCap.Name = "chkKhanCap";
            this.chkKhanCap.Properties.Caption = "";
            this.chkKhanCap.Size = new System.Drawing.Size(363, 19);
            this.chkKhanCap.StyleController = this.dataLayoutControl1;
            this.chkKhanCap.TabIndex = 5;
            // 
            // lblKhanCap
            // 
            this.lblKhanCap.Control = this.chkKhanCap;
            this.lblKhanCap.Location = new System.Drawing.Point(0, 116);
            this.lblKhanCap.Name = "lblKhanCap";
            this.lblKhanCap.Size = new System.Drawing.Size(437, 25);
            this.lblKhanCap.TextSize = new System.Drawing.Size(66, 17);
            // 
            // frmYKienYC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 195);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmYKienYC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmYKienYC";
            this.Load += new System.EventHandler(this.frmYKienYC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYKien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblYKien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKhanCap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblKhanCap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem lblYKien;
        private DevExpress.XtraLayout.LayoutControlItem lblKhanCap;
        public DevExpress.XtraEditors.MemoEdit txtYKien;
        public DevExpress.XtraEditors.CheckEdit chkKhanCap;
    }
}