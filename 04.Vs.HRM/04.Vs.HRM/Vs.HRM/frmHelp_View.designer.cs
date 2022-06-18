namespace Vs.HRM
{
    partial class frmHelp_View
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
            this.txHelp = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txHelp
            // 
            this.txHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txHelp.Location = new System.Drawing.Point(0, 0);
            this.txHelp.Name = "txHelp";
            this.txHelp.Size = new System.Drawing.Size(719, 268);
            this.txHelp.TabIndex = 0;
            this.txHelp.Text = "";
            // 
            // frmHelp_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 268);
            this.Controls.Add(this.txHelp);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmHelp_View";
            this.Text = "frmHelp_View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHelp_View_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox txHelp;
    }
}