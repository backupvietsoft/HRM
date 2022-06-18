namespace Vs.HRM
{
    partial class frmLoaiXepLoai
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.popListThang = new DevExpress.XtraEditors.PopupContainerControl();
            this.grdThang = new DevExpress.XtraGrid.GridControl();
            this.grvThang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popThang = new DevExpress.XtraEditors.PopupContainerControl();
            this.calThang = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.cboThang = new Commons.MPopupContainerEdit();
            this.grdLXL = new DevExpress.XtraGrid.GridControl();
            this.grvLXL = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.windowsUIButton = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popListThang)).BeginInit();
            this.popListThang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popThang)).BeginInit();
            this.popThang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLXL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLXL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.windowsUIButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.popListThang);
            this.dataLayoutControl1.Controls.Add(this.cboThang);
            this.dataLayoutControl1.Controls.Add(this.grdLXL);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.Root;
            this.dataLayoutControl1.Size = new System.Drawing.Size(800, 435);
            this.dataLayoutControl1.TabIndex = 0;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // popListThang
            // 
            this.popListThang.Controls.Add(this.grdThang);
            this.popListThang.Controls.Add(this.popThang);
            this.popListThang.Location = new System.Drawing.Point(145, 61);
            this.popListThang.MinimumSize = new System.Drawing.Size(400, 200);
            this.popListThang.Name = "popListThang";
            this.popListThang.Size = new System.Drawing.Size(400, 200);
            this.popListThang.TabIndex = 15;
            // 
            // grdThang
            // 
            this.grdThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdThang.Location = new System.Drawing.Point(0, 0);
            this.grdThang.MainView = this.grvThang;
            this.grdThang.MinimumSize = new System.Drawing.Size(400, 200);
            this.grdThang.Name = "grdThang";
            this.grdThang.Size = new System.Drawing.Size(400, 200);
            this.grdThang.TabIndex = 15;
            this.grdThang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThang});
            // 
            // grvThang
            // 
            this.grvThang.GridControl = this.grdThang;
            this.grvThang.Name = "grvThang";
            this.grvThang.OptionsView.ShowAutoFilterRow = true;
            this.grvThang.OptionsView.ShowGroupPanel = false;
            this.grvThang.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.grvThang_RowCellClick);
            // 
            // popThang
            // 
            this.popThang.Controls.Add(this.calThang);
            this.popThang.Location = new System.Drawing.Point(286, 3);
            this.popThang.MinimumSize = new System.Drawing.Size(386, 313);
            this.popThang.Name = "popThang";
            this.popThang.Size = new System.Drawing.Size(386, 313);
            this.popThang.TabIndex = 6;
            // 
            // calThang
            // 
            this.calThang.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calThang.Location = new System.Drawing.Point(0, 0);
            this.calThang.MinimumSize = new System.Drawing.Size(386, 313);
            this.calThang.Name = "calThang";
            this.calThang.Size = new System.Drawing.Size(386, 313);
            this.calThang.TabIndex = 1;
            this.calThang.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.calThang.DateTimeCommit += new System.EventHandler(this.calThang_DateTimeCommit);
            // 
            // cboThang
            // 
            this.cboThang.Location = new System.Drawing.Point(339, 12);
            this.cboThang.Name = "cboThang";
            this.cboThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboThang.Properties.DefaultActionButtonIndex = 0;
            this.cboThang.Properties.DefaultPopupControl = this.popListThang;
            this.cboThang.Properties.DifferentActionButtonIndex = 1;
            this.cboThang.Properties.DifferentPopupControl = this.popThang;
            this.cboThang.Size = new System.Drawing.Size(250, 26);
            this.cboThang.StyleController = this.dataLayoutControl1;
            this.cboThang.TabIndex = 6;
            this.cboThang.EditValueChanged += new System.EventHandler(this.cboThang_EditValueChanged);
            // 
            // grdLXL
            // 
            this.grdLXL.Location = new System.Drawing.Point(12, 42);
            this.grdLXL.MainView = this.grvLXL;
            this.grdLXL.Name = "grdLXL";
            this.grdLXL.Size = new System.Drawing.Size(776, 381);
            this.grdLXL.TabIndex = 5;
            this.grdLXL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLXL});
            this.grdLXL.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.grdLXL_ProcessGridKey);
            // 
            // grvLXL
            // 
            this.grvLXL.GridControl = this.grdLXL;
            this.grvLXL.Name = "grvLXL";
            this.grvLXL.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvLXL.OptionsView.ShowAutoFilterRow = true;
            this.grvLXL.OptionsView.ShowGroupPanel = false;
            this.grvLXL.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvLXL_InitNewRow);
            this.grvLXL.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.grvLXL_InvalidRowException);
            this.grvLXL.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grvLXL_ValidateRow);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(800, 435);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdLXL;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(780, 385);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cboThang;
            this.layoutControlItem2.Location = new System.Drawing.Point(196, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(385, 30);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(128, 20);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(196, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(581, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(199, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
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
            windowsUIButtonImageOptions1.ImageUri.Uri = "AddItem";
            windowsUIButtonImageOptions2.ImageUri.Uri = "snap/snapdeletelist";
            windowsUIButtonImageOptions3.ImageUri.Uri = "richedit/clearheaderandfooter";
            windowsUIButtonImageOptions4.ImageUri.Uri = "SaveAll";
            windowsUIButtonImageOptions5.ImageUri.Uri = "SaveAndClose";
            this.windowsUIButton.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "them", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "xoa", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUISeparator(),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "thoat", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "ghi", -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "khongghi", -1, false)});
            this.windowsUIButton.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.windowsUIButton.Controls.Add(this.searchControl);
            this.windowsUIButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windowsUIButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.windowsUIButton.Location = new System.Drawing.Point(0, 435);
            this.windowsUIButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.windowsUIButton.Name = "windowsUIButton";
            this.windowsUIButton.Padding = new System.Windows.Forms.Padding(5);
            this.windowsUIButton.Size = new System.Drawing.Size(800, 40);
            this.windowsUIButton.TabIndex = 14;
            this.windowsUIButton.Text = "windowsUIButtonPanel1";
            this.windowsUIButton.UseButtonBackgroundImages = false;
            this.windowsUIButton.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.windowsUIButton_ButtonClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.searchControl.Client = this.grdLXL;
            this.searchControl.Location = new System.Drawing.Point(0, 10);
            this.searchControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F);
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.Client = this.grdLXL;
            this.searchControl.Properties.FindDelay = 100;
            this.searchControl.Size = new System.Drawing.Size(209, 30);
            this.searchControl.TabIndex = 11;
            // 
            // frmLoaiXepLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.windowsUIButton);
            this.Name = "frmLoaiXepLoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLoaiXepLoai";
            this.Load += new System.EventHandler(this.frmLoaiXepLoai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popListThang)).EndInit();
            this.popListThang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popThang)).EndInit();
            this.popThang.ResumeLayout(false);
            this.popThang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLXL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLXL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.windowsUIButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private Commons.MPopupContainerEdit cboThang;
        private DevExpress.XtraGrid.GridControl grdLXL;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLXL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PopupContainerControl popListThang;
        private DevExpress.XtraGrid.GridControl grdThang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThang;
        private DevExpress.XtraEditors.PopupContainerControl popThang;
        private DevExpress.XtraEditors.Controls.CalendarControl calThang;
    }
}