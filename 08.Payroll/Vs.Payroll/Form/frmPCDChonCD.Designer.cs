namespace Vs.Payroll
{
    partial class frmPCDChonCD
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
            this.grdCD = new DevExpress.XtraGrid.GridControl();
            this.grvCD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnChon = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.searchControl2 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCD)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCD
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grdCD, 4);
            this.grdCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCD.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.grdCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCD.Location = new System.Drawing.Point(3, 9);
            this.grdCD.MainView = this.grvCD;
            this.grdCD.Name = "grdCD";
            this.grdCD.Size = new System.Drawing.Size(613, 414);
            this.grdCD.TabIndex = 40;
            this.grdCD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCD});
            // 
            // grvCD
            // 
            this.grvCD.Appearance.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grvCD.Appearance.Preview.Options.UseFont = true;
            this.grvCD.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grvCD.Appearance.Row.Options.UseFont = true;
            this.grvCD.DetailHeight = 349;
            this.grvCD.GridControl = this.grdCD;
            this.grvCD.Name = "grvCD";
            this.grvCD.OptionsView.ShowGroupPanel = false;
            this.grvCD.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.grvCD_RowUpdated);
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.Location = new System.Drawing.Point(523, 428);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(93, 25);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnChon
            // 
            this.btnChon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnChon.Location = new System.Drawing.Point(411, 428);
            this.btnChon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(106, 25);
            this.btnChon.TabIndex = 0;
            this.btnChon.Text = "Cập nhập";
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.Controls.Add(this.btnThoat, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.searchControl2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnChon, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.grdCD, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(619, 461);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // searchControl2
            // 
            this.searchControl2.Client = this.grdCD;
            this.searchControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchControl2.Location = new System.Drawing.Point(3, 428);
            this.searchControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchControl2.Name = "searchControl2";
            this.searchControl2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl2.Properties.Client = this.grdCD;
            this.searchControl2.Size = new System.Drawing.Size(179, 26);
            this.searchControl2.TabIndex = 39;
            // 
            // frmPCDChonCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmPCDChonCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPCDChonCD";
            this.Load += new System.EventHandler(this.frmPCDChonCD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCD)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdCD;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCD;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnChon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SearchControl searchControl2;
    }
}