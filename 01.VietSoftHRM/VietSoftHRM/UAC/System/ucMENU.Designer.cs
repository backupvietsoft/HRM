namespace VietSoftHRM
{
    partial class ucMENU
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.treeListMenu = new DevExpress.XtraTreeList.TreeList();
            this.windowButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // windowButton
            // 
            this.windowButton.AppearanceButton.Hovered.FontSizeDelta = -1;
            this.windowButton.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.Gray;
            this.windowButton.AppearanceButton.Hovered.Options.UseFont = true;
            this.windowButton.AppearanceButton.Hovered.Options.UseForeColor = true;
            this.windowButton.AppearanceButton.Normal.FontSizeDelta = -1;
            this.windowButton.AppearanceButton.Normal.ForeColor = System.Drawing.Color.DodgerBlue;
            this.windowButton.AppearanceButton.Normal.Options.UseFont = true;
            this.windowButton.AppearanceButton.Normal.Options.UseForeColor = true;
            this.windowButton.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.windowButton.AppearanceButton.Pressed.FontSizeDelta = -1;
            this.windowButton.AppearanceButton.Pressed.Options.UseBackColor = true;
            this.windowButton.AppearanceButton.Pressed.Options.UseBorderColor = true;
            this.windowButton.AppearanceButton.Pressed.Options.UseFont = true;
            this.windowButton.AppearanceButton.Pressed.Options.UseImage = true;
            this.windowButton.AppearanceButton.Pressed.Options.UseTextOptions = true;
            windowsUIButtonImageOptions1.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions3.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions4.ImageUri.Uri = "SaveAndClose";
            this.windowButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "them", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false)});
            this.windowButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowButton.Controls.Add(this.searchControl);
            this.windowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowButton.Location = new System.Drawing.Point(0, 560);
            this.windowButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowButton.Name = "windowButton";
            this.windowButton.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.windowButton.Size = new System.Drawing.Size(800, 40);
            this.windowButton.TabIndex = 2;
            this.windowButton.Text = "windowsUIButtonPanel1";
            this.windowButton.UseButtonBackgroundImages = false;
            this.windowButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.treeListMenu;
            this.searchControl.Location = new System.Drawing.Point(0, 8);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.treeListMenu;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(220, 30);
            this.searchControl.TabIndex = 10;
            // 
            // treeListMenu
            // 
            this.treeListMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListMenu.FixedLineWidth = 1;
            this.treeListMenu.Location = new System.Drawing.Point(0, 0);
            this.treeListMenu.MinWidth = 21;
            this.treeListMenu.Name = "treeListMenu";
            this.treeListMenu.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = true;
            this.treeListMenu.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeListMenu.Size = new System.Drawing.Size(800, 560);
            this.treeListMenu.TabIndex = 3;
            this.treeListMenu.TreeLevelWidth = 17;
            this.treeListMenu.RowCellClick += new DevExpress.XtraTreeList.RowCellClickEventHandler(this.treeListMenu_RowCellClick);
            // 
            // ucMENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListMenu);
            this.Controls.Add(this.windowButton);
            this.Name = "ucMENU";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ucMENU_Load);
            this.windowButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowButton;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraTreeList.TreeList treeListMenu;
    }
}
