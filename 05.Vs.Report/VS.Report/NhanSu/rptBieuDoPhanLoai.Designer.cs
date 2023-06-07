namespace Vs.Report
{
    partial class rptBieuDoPhanLoai
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
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint1 = new DevExpress.XtraCharts.SeriesPoint(11D, new object[] {
            ((object)(20D))}, 0);
            DevExpress.XtraCharts.SeriesPoint seriesPoint2 = new DevExpress.XtraCharts.SeriesPoint(21D, new object[] {
            ((object)(80D))}, 1);
            DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView1 = new DevExpress.XtraCharts.Pie3DSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint3 = new DevExpress.XtraCharts.SeriesPoint(11D, new object[] {
            ((object)(20D))}, 0);
            DevExpress.XtraCharts.SeriesPoint seriesPoint4 = new DevExpress.XtraCharts.SeriesPoint(21D, new object[] {
            ((object)(50D))}, 1);
            DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView2 = new DevExpress.XtraCharts.Pie3DSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint5 = new DevExpress.XtraCharts.SeriesPoint(11D, new object[] {
            ((object)(20D))}, 0);
            DevExpress.XtraCharts.SeriesPoint seriesPoint6 = new DevExpress.XtraCharts.SeriesPoint(21D, new object[] {
            ((object)(50D))}, 1);
            DevExpress.XtraCharts.Pie3DSeriesView pie3DSeriesView3 = new DevExpress.XtraCharts.Pie3DSeriesView();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.chart_GT = new DevExpress.XtraReports.UI.XRChart();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.chart_IDD = new DevExpress.XtraReports.UI.XRChart();
            this.chart_LCV = new DevExpress.XtraReports.UI.XRChart();
            this.xrSubreport4 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport3 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.lblTieuDe = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.chart_GT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_IDD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_LCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // chart_GT
            // 
            this.chart_GT.AutoLayout = true;
            this.chart_GT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chart_GT.BorderColor = System.Drawing.Color.Black;
            this.chart_GT.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.chart_GT.Legend.Name = "Default Legend";
            this.chart_GT.LocationFloat = new DevExpress.Utils.PointFloat(0F, 146.9999F);
            this.chart_GT.Name = "chart_GT";
            series1.Name = "Series 1";
            seriesPoint1.ColorSerializable = "#FF6463";
            seriesPoint2.ColorSerializable = "#548DD4";
            series1.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint1,
            seriesPoint2});
            series1.View = pie3DSeriesView1;
            series1.Visible = false;
            this.chart_GT.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chart_GT.SizeF = new System.Drawing.SizeF(335.4167F, 240.625F);
            this.chart_GT.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_GT_CustomDrawSeriesPoint);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.chart_IDD,
            this.chart_LCV,
            this.chart_GT,
            this.xrSubreport4,
            this.xrSubreport3,
            this.xrSubreport2,
            this.lblTieuDe,
            this.xrSubreport1});
            this.ReportHeader.HeightF = 391.125F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // chart_IDD
            // 
            this.chart_IDD.AutoLayout = true;
            this.chart_IDD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chart_IDD.BorderColor = System.Drawing.Color.Black;
            this.chart_IDD.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.chart_IDD.Legend.Name = "Default Legend";
            this.chart_IDD.LocationFloat = new DevExpress.Utils.PointFloat(730.4583F, 146.9999F);
            this.chart_IDD.Name = "chart_IDD";
            series2.Name = "Series 1";
            seriesPoint3.ColorSerializable = "#FF6463";
            seriesPoint4.ColorSerializable = "#548DD4";
            series2.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint3,
            seriesPoint4});
            series2.View = pie3DSeriesView2;
            series2.Visible = false;
            this.chart_IDD.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chart_IDD.SizeF = new System.Drawing.SizeF(335.4167F, 240.625F);
            this.chart_IDD.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_IDD_CustomDrawSeriesPoint);
            // 
            // chart_LCV
            // 
            this.chart_LCV.AutoLayout = true;
            this.chart_LCV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chart_LCV.BorderColor = System.Drawing.Color.Black;
            this.chart_LCV.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.chart_LCV.Legend.Name = "Default Legend";
            this.chart_LCV.LocationFloat = new DevExpress.Utils.PointFloat(362.5001F, 146.9999F);
            this.chart_LCV.Name = "chart_LCV";
            series3.Name = "Series 1";
            seriesPoint5.ColorSerializable = "#FF6463";
            seriesPoint6.ColorSerializable = "#548DD4";
            series3.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint5,
            seriesPoint6});
            series3.View = pie3DSeriesView3;
            series3.Visible = false;
            this.chart_LCV.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chart_LCV.SizeF = new System.Drawing.SizeF(335.4167F, 240.625F);
            this.chart_LCV.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chart_LCV_CustomDrawSeriesPoint);
            // 
            // xrSubreport4
            // 
            this.xrSubreport4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 113.5832F);
            this.xrSubreport4.Name = "xrSubreport4";
            this.xrSubreport4.SizeF = new System.Drawing.SizeF(335.4166F, 23F);
            // 
            // xrSubreport3
            // 
            this.xrSubreport3.LocationFloat = new DevExpress.Utils.PointFloat(730.4583F, 113.5832F);
            this.xrSubreport3.Name = "xrSubreport3";
            this.xrSubreport3.SizeF = new System.Drawing.SizeF(335.4166F, 23F);
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(362.5001F, 113.5832F);
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.SizeF = new System.Drawing.SizeF(335.4167F, 23F);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new DevExpress.Drawing.DXFont("Times New Roman", 20.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lblTieuDe.LocationFloat = new DevExpress.Utils.PointFloat(3.178914E-05F, 34.33332F);
            this.lblTieuDe.Multiline = true;
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTieuDe.SizeF = new System.Drawing.SizeF(1069F, 40.70832F);
            this.lblTieuDe.StylePriority.UseFont = false;
            this.lblTieuDe.StylePriority.UseTextAlignment = false;
            this.lblTieuDe.Text = "BIỂU ĐỒ PHÂN LOẠI";
            this.lblTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrSubreport1
            // 
            this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.SizeF = new System.Drawing.SizeF(1069F, 23F);
            // 
            // rptBieuDoPhanLoai
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 50, 50);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "20.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.rptBieuDoPhanLoai_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_GT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_IDD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pie3DSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_LCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        private DevExpress.XtraReports.UI.XRLabel lblTieuDe;
        private DevExpress.XtraReports.UI.XRChart chart_GT;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport4;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport3;
        private DevExpress.XtraReports.UI.XRChart chart_IDD;
        private DevExpress.XtraReports.UI.XRChart chart_LCV;
    }
}
