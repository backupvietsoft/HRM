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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions17 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions18 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions19 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions20 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.windowButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.treeListMenu = new DevExpress.XtraTreeList.TreeList();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboNhomUser = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblNhom = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.windowButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
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
            windowsUIButtonImageOptions17.ImageUri.Uri = "Edit;Size32x32;GrayScaled";
            windowsUIButtonImageOptions18.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions19.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions20.ImageUri.Uri = "SaveAndClose";
            this.windowButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions17, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "them", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions18, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions19, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "luu", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions20, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongluu", -1, false)});
            this.windowButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowButton.Controls.Add(this.searchControl);
            this.windowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowButton.Location = new System.Drawing.Point(0, 533);
            this.windowButton.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.windowButton.Name = "windowButton";
            this.windowButton.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.windowButton.Size = new System.Drawing.Size(1100, 56);
            this.windowButton.TabIndex = 2;
            this.windowButton.Text = "windowsUIButtonPanel1";
            this.windowButton.UseButtonBackgroundImages = false;
            this.windowButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.treeListMenu;
            this.searchControl.Location = new System.Drawing.Point(8, 15);
            this.searchControl.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.AutoHeight = false;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.treeListMenu;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(302, 34);
            this.searchControl.TabIndex = 10;
            // 
            // treeListMenu
            // 
            this.treeListMenu.HorzScrollStep = 5;
            this.treeListMenu.Location = new System.Drawing.Point(12, 62);
            this.treeListMenu.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.treeListMenu.MinWidth = 28;
            this.treeListMenu.Name = "treeListMenu";
            this.treeListMenu.OptionsBehavior.AllowBoundCheckBoxesInVirtualMode = true;
            this.treeListMenu.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeListMenu.Size = new System.Drawing.Size(1076, 459);
            this.treeListMenu.TabIndex = 3;
            this.treeListMenu.TreeLevelWidth = 24;
            this.treeListMenu.RowCellClick += new DevExpress.XtraTreeList.RowCellClickEventHandler(this.treeListMenu_RowCellClick);
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.treeListMenu);
            this.dataLayoutControl1.Controls.Add(this.cboNhomUser);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1100, 533);
            this.dataLayoutControl1.TabIndex = 4;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lblNhom,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1100, 533);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.treeListMenu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1080, 463);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // cboNhomUser
            // 
            this.cboNhomUser.EditValue = "";
            this.cboNhomUser.Location = new System.Drawing.Point(453, 12);
            this.cboNhomUser.Margin = new System.Windows.Forms.Padding(5);
            this.cboNhomUser.Name = "cboNhomUser";
            this.cboNhomUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNhomUser.Properties.NullText = "";
            this.cboNhomUser.Properties.PopupView = this.gridView2;
            this.cboNhomUser.Size = new System.Drawing.Size(315, 34);
            this.cboNhomUser.StyleController = this.dataLayoutControl1;
            this.cboNhomUser.TabIndex = 15;
            this.cboNhomUser.EditValueChanged += new System.EventHandler(this.cboNhomUser_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 576;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // lblNhom
            // 
            this.lblNhom.Control = this.cboNhomUser;
            this.lblNhom.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lblNhom.CustomizationFormText = "lblNhom";
            this.lblNhom.Location = new System.Drawing.Point(360, 0);
            this.lblNhom.MaxSize = new System.Drawing.Size(400, 40);
            this.lblNhom.MinSize = new System.Drawing.Size(400, 40);
            this.lblNhom.Name = "lblNhom";
            this.lblNhom.Size = new System.Drawing.Size(400, 40);
            this.lblNhom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNhom.Text = "lblNhom";
            this.lblNhom.TextSize = new System.Drawing.Size(77, 28);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(760, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(320, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(360, 40);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 40);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 10);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(1080, 10);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucMENU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.windowButton);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "ucMENU";
            this.Size = new System.Drawing.Size(1100, 589);
            this.Load += new System.EventHandler(this.ucMENU_Load);
            this.windowButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowButton;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraTreeList.TreeList treeListMenu;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboNhomUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem lblNhom;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}
