namespace Vs.HRM
{
    partial class XtraForm1
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
            this.cboThang = new Commons.MPopupContainerEdit();
            this.calThang = new DevExpress.XtraEditors.Controls.CalendarControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grvThang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdThang = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).BeginInit();
            this.SuspendLayout();
            // 
            // cboThang
            // 
            this.cboThang.Location = new System.Drawing.Point(65, 69);
            this.cboThang.Name = "cboThang";
            this.cboThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cboThang.Properties.DefaultActionButtonIndex = 0;
            this.cboThang.Properties.DefaultPopupControl = null;
            this.cboThang.Properties.DifferentActionButtonIndex = 1;
            this.cboThang.Properties.DifferentPopupControl = null;
            this.cboThang.Size = new System.Drawing.Size(256, 26);
            this.cboThang.TabIndex = 17;
            // 
            // calThang
            // 
            this.calThang.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calThang.Location = new System.Drawing.Point(195, 141);
            this.calThang.Name = "calThang";
            this.calThang.Padding = new System.Windows.Forms.Padding(0);
            this.calThang.SelectionMode = DevExpress.XtraEditors.Repository.CalendarSelectionMode.Multiple;
            this.calThang.ShowClearButton = true;
            this.calThang.Size = new System.Drawing.Size(366, 314);
            this.calThang.TabIndex = 16;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 349;
            this.gridView2.GridControl = this.grdThang;
            this.gridView2.Name = "gridView2";
            // 
            // grvThang
            // 
            this.grvThang.DetailHeight = 349;
            this.grvThang.GridControl = this.grdThang;
            this.grvThang.Name = "grvThang";
            this.grvThang.OptionsView.ShowAutoFilterRow = true;
            this.grvThang.OptionsView.ShowGroupPanel = false;
            // 
            // grdThang
            // 
            this.grdThang.Location = new System.Drawing.Point(437, 54);
            this.grdThang.MainView = this.grvThang;
            this.grdThang.Name = "grdThang";
            this.grdThang.Size = new System.Drawing.Size(400, 200);
            this.grdThang.TabIndex = 18;
            this.grdThang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThang,
            this.gridView2});
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 413);
            this.Controls.Add(this.grdThang);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.calThang);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calThang.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdThang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Commons.MPopupContainerEdit cboThang;
        private DevExpress.XtraEditors.Controls.CalendarControl calThang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl grdThang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThang;
    }
}