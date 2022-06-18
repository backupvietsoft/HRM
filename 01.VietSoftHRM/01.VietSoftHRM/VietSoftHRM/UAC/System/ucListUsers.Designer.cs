namespace VietSoftHRM
{
    partial class ucListUsers
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.grdListUser = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiResetPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKick = new System.Windows.Forms.ToolStripMenuItem();
            this.grvListUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.groupListUsers = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListUser)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvListUser)).BeginInit();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupListUsers)).BeginInit();
            this.groupListUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdListUser;
            this.searchControl.Location = new System.Drawing.Point(0, 10);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdListUser;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(220, 30);
            this.searchControl.TabIndex = 10;
            // 
            // grdListUser
            // 
            this.grdListUser.ContextMenuStrip = this.contextMenuStrip1;
            this.grdListUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdListUser.Location = new System.Drawing.Point(2, 27);
            this.grdListUser.MainView = this.grvListUser;
            this.grdListUser.Name = "grdListUser";
            this.grdListUser.Size = new System.Drawing.Size(904, 366);
            this.grdListUser.TabIndex = 0;
            this.grdListUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvListUser});
            this.grdListUser.Load += new System.EventHandler(this.grdListUser_Load);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiResetPassword,
            this.tsmiKick});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiResetPassword
            // 
            this.tsmiResetPassword.Name = "tsmiResetPassword";
            this.tsmiResetPassword.Size = new System.Drawing.Size(180, 22);
            this.tsmiResetPassword.Text = "Reset pass";
            this.tsmiResetPassword.Click += new System.EventHandler(this.tsmiResetPassword_Click);
            // 
            // tsmiKick
            // 
            this.tsmiKick.Name = "tsmiKick";
            this.tsmiKick.Size = new System.Drawing.Size(180, 22);
            this.tsmiKick.Text = "Kick out";
            this.tsmiKick.Click += new System.EventHandler(this.tsmiKick_Click);
            // 
            // grvListUser
            // 
            this.grvListUser.GridControl = this.grdListUser;
            this.grvListUser.Name = "grvListUser";
            this.grvListUser.OptionsView.ShowGroupPanel = false;
            this.grvListUser.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvListUser_RowStyle);
            this.grvListUser.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvListUser_PopupMenuShowing);
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "outlook%20inspired/resetview";
            windowsUIButtonImageOptions2.ImageUri.Uri = "richedit/clearheaderandfooter";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "resetpass", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 395);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(908, 40);
            this.windowsUIButton.TabIndex = 4;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowButton_ButtonClick);
            // 
            // groupListUsers
            // 
            this.groupListUsers.Controls.Add(this.grdListUser);
            this.groupListUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupListUsers.Location = new System.Drawing.Point(0, 0);
            this.groupListUsers.Name = "groupListUsers";
            this.groupListUsers.Size = new System.Drawing.Size(908, 395);
            this.groupListUsers.TabIndex = 5;
            // 
            // ucListUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupListUsers);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "ucListUsers";
            this.Size = new System.Drawing.Size(908, 435);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListUser)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvListUser)).EndInit();
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupListUsers)).EndInit();
            this.groupListUsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraEditors.GroupControl groupListUsers;
        private DevExpress.XtraGrid.GridControl grdListUser;
        private DevExpress.XtraGrid.Views.Grid.GridView grvListUser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiResetPassword;
        private System.Windows.Forms.ToolStripMenuItem tsmiKick;
    }
}
