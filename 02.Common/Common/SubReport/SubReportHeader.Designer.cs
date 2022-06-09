namespace Commons
{
    partial class SubReportHeader
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubReportHeader));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.picLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lblCONG_TY = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 142F;
            this.TopMargin.Name = "TopMargin";
            // 
            // picLogo
            // 
            this.picLogo.Dpi = 254F;
            this.picLogo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[LOGO]")});
            this.picLogo.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            this.picLogo.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("picLogo.ImageSource"));
            this.picLogo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.picLogo.Name = "picLogo";
            this.picLogo.SizeF = new System.Drawing.SizeF(372.2662F, 210F);
            this.picLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // lblCONG_TY
            // 
            this.lblCONG_TY.AllowMarkupText = true;
            this.lblCONG_TY.AutoWidth = true;
            this.lblCONG_TY.Dpi = 254F;
            this.lblCONG_TY.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TTC]")});
            this.lblCONG_TY.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCONG_TY.LocationFloat = new DevExpress.Utils.PointFloat(372.2662F, 0F);
            this.lblCONG_TY.Multiline = true;
            this.lblCONG_TY.Name = "lblCONG_TY";
            this.lblCONG_TY.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 5, 0, 254F);
            this.lblCONG_TY.SizeF = new System.Drawing.SizeF(1473.734F, 210F);
            this.lblCONG_TY.StylePriority.UseFont = false;
            this.lblCONG_TY.StylePriority.UsePadding = false;
            this.lblCONG_TY.StylePriority.UseTextAlignment = false;
            this.lblCONG_TY.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 192F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.picLogo,
            this.lblCONG_TY});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 215.1875F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // SubReportHeader
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(119, 135, 142, 192);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPictureBox picLogo;
        private DevExpress.XtraReports.UI.XRLabel lblCONG_TY;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
    }
}
