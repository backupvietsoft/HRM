namespace Vs.Report
{
    partial class rptInHangLoat
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
            this.components = new System.ComponentModel.Container();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lblPrinter = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblPrintDate = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblPages = new DevExpress.XtraReports.UI.XRPageInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.numericChartRangeControlClient1 = new DevExpress.XtraEditors.NumericChartRangeControlClient();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 31.35419F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblPrinter,
            this.lblPrintDate,
            this.lblPages});
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 64.74847F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // lblPrinter
            // 
            this.lblPrinter.Dpi = 254F;
            this.lblPrinter.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblPrinter.LocationFloat = new DevExpress.Utils.PointFloat(796.3958F, 0F);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblPrinter.PageInfo = DevExpress.XtraPrinting.PageInfo.UserName;
            this.lblPrinter.SizeF = new System.Drawing.SizeF(411.3331F, 58.42F);
            this.lblPrinter.StylePriority.UseFont = false;
            this.lblPrinter.StylePriority.UseTextAlignment = false;
            this.lblPrinter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblPrintDate
            // 
            this.lblPrintDate.Dpi = 254F;
            this.lblPrintDate.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblPrintDate.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblPrintDate.Name = "lblPrintDate";
            this.lblPrintDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblPrintDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.lblPrintDate.SizeF = new System.Drawing.SizeF(400F, 58.42F);
            this.lblPrintDate.StylePriority.UseFont = false;
            this.lblPrintDate.StylePriority.UseTextAlignment = false;
            this.lblPrintDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblPrintDate.TextFormatString = "Date: {0:dd/MM/yyyy HH:mm:ss}";
            // 
            // lblPages
            // 
            this.lblPages.Dpi = 254F;
            this.lblPages.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblPages.LocationFloat = new DevExpress.Utils.PointFloat(1379.38F, 0F);
            this.lblPages.Name = "lblPages";
            this.lblPages.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblPages.SizeF = new System.Drawing.SizeF(398.62F, 59.99999F);
            this.lblPages.StylePriority.UseFont = false;
            this.lblPages.StylePriority.UseTextAlignment = false;
            this.lblPages.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblPages.TextFormatString = "Page {0} of {1}";
            // 
            // Detail
            // 
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 260.689F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.SnapLinePadding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.Dpi = 254F;
            this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreport2.LockedInUserDesigner = true;
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.SizeF = new System.Drawing.SizeF(1778F, 42.93903F);
            // 
            // rptInHangLoat
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 254F;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.Margins = new DevExpress.Drawing.DXMargins(161F, 161F, 31.35419F, 64.74847F);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Tag = "rptInHangLoat";
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.numericChartRangeControlClient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraEditors.NumericChartRangeControlClient numericChartRangeControlClient1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Diagnostics.EventLog eventLog1;
        private DevExpress.XtraReports.UI.XRPageInfo lblPrinter;
        private DevExpress.XtraReports.UI.XRPageInfo lblPrintDate;
        private DevExpress.XtraReports.UI.XRPageInfo lblPages;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
    }
}
