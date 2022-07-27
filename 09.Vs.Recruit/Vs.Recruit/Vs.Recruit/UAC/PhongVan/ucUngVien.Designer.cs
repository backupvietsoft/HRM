namespace Vs.Recruit
{
    partial class ucUngVien
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.accorMenuleft = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.NONNlab_Link = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accorMenuleft)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.accorMenuleft);
            this.panel1.Controls.Add(this.NONNlab_Link);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 600);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(250, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 578);
            this.panel2.TabIndex = 6;
            // 
            // accorMenuleft
            // 
            this.accorMenuleft.AllowItemSelection = true;
            this.accorMenuleft.Appearance.Item.Normal.Options.UseTextOptions = true;
            this.accorMenuleft.Appearance.Item.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.accorMenuleft.Appearance.Item.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.accorMenuleft.Appearance.Item.Pressed.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.accorMenuleft.Appearance.Item.Pressed.Options.UseFont = true;
            this.accorMenuleft.Cursor = System.Windows.Forms.Cursors.Default;
            this.accorMenuleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.accorMenuleft.Location = new System.Drawing.Point(0, 22);
            this.accorMenuleft.Name = "accorMenuleft";
            this.accorMenuleft.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.True;
            this.accorMenuleft.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.accorMenuleft.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            this.accorMenuleft.ShowItemExpandButtons = false;
            this.accorMenuleft.Size = new System.Drawing.Size(250, 578);
            this.accorMenuleft.TabIndex = 5;
            this.accorMenuleft.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // NONNlab_Link
            // 
            this.NONNlab_Link.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(1)), true);
            this.NONNlab_Link.Appearance.Options.UseFont = true;
            this.NONNlab_Link.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.NONNlab_Link.Dock = System.Windows.Forms.DockStyle.Top;
            this.NONNlab_Link.Location = new System.Drawing.Point(0, 0);
            this.NONNlab_Link.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NONNlab_Link.Name = "NONNlab_Link";
            this.NONNlab_Link.Size = new System.Drawing.Size(800, 22);
            this.NONNlab_Link.TabIndex = 2;
            this.NONNlab_Link.Text = "labelControl1";
            // 
            // ucUngVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucUngVien";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ucUngVien_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accorMenuleft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.LabelControl NONNlab_Link;
        private System.Windows.Forms.Panel panel2;
        internal DevExpress.XtraBars.Navigation.AccordionControl accorMenuleft;
    }
}
