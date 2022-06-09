namespace Vs.Category
{
    partial class Form1
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
            this.tvwMenu = new DevExpress.XtraTreeList.TreeList();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tvwMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // tvwMenu
            // 
            this.tvwMenu.Location = new System.Drawing.Point(0, 0);
            this.tvwMenu.Name = "tvwMenu";
            this.tvwMenu.Size = new System.Drawing.Size(400, 200);
            this.tvwMenu.TabIndex = 0;


            this.tvwMenu.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Blue;
            this.tvwMenu.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.tvwMenu.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.tvwMenu.Appearance.HideSelectionRow.Options.UseForeColor = true;
            
            this.tvwMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwMenu.Location = new System.Drawing.Point(3, 3);
            
            this.tvwMenu.OptionsView.ShowCheckBoxes = true;
            this.tvwMenu.ParentFieldName = "";
            this.tvwMenu.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tvwMenu.Size = new System.Drawing.Size(369, 517);
            this.tvwMenu.TabIndex = 29;
            

            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(0, 497);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(980, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 520);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.tvwMenu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tvwMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tvwMenu;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}

