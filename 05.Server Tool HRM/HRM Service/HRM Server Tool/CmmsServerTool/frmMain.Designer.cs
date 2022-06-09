namespace HRMServerTool
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboServices = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtHInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLic = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Services:";
            // 
            // cboServices
            // 
            this.cboServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServices.FormattingEnabled = true;
            this.cboServices.Location = new System.Drawing.Point(102, 24);
            this.cboServices.Name = "cboServices";
            this.cboServices.Size = new System.Drawing.Size(203, 21);
            this.cboServices.TabIndex = 12;
            this.cboServices.SelectedIndexChanged += new System.EventHandler(this.cboServices_SelectedIndexChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(179, 93);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(104, 93);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtHInfo
            // 
            this.txtHInfo.Location = new System.Drawing.Point(102, 56);
            this.txtHInfo.Name = "txtHInfo";
            this.txtHInfo.ReadOnly = true;
            this.txtHInfo.Size = new System.Drawing.Size(203, 20);
            this.txtHInfo.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Hardware Info";
            // 
            // btnLic
            // 
            this.btnLic.Location = new System.Drawing.Point(12, 93);
            this.btnLic.Name = "btnLic";
            this.btnLic.Size = new System.Drawing.Size(75, 23);
            this.btnLic.TabIndex = 8;
            this.btnLic.Text = "License";
            this.btnLic.UseVisualStyleBackColor = true;
            this.btnLic.Click += new System.EventHandler(this.btnLic_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 132);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHInfo);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.cboServices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLic);
            this.Controls.Add(this.btnStart);
            this.Name = "frmMain";
            this.Text = "HRM Server Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboServices;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtHInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLic;
        private System.Windows.Forms.Button button1;
    }
}

