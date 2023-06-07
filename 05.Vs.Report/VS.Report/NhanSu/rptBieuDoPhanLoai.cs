using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using System.Windows.Forms;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBieuDoPhanLoai : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int Nam = 2021;
        public rptBieuDoPhanLoai(DataTable dtt, DataTable dtt1, DataTable dtt2, int nam)
        {
            InitializeComponent();
            dt = dtt; // gioi tinh
            dt1 = dtt1; //loai_cv
            dt2 = dtt2;// idd
            Nam = nam;
            xrSubreport1.ReportSource = new SubReportHeader();
        }

        private void loadcharGioiTinh(DataTable dt)
        {

            chart_GT.Series.Clear();
            chart_GT.Titles.Clear();
            chart_GT.Titles.Add(new ChartTitle() { Text = Commons.Modules.ObjLanguages.GetLanguage("rptBieuDoPhanLoai", "lblGioiTinh"), Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
            chart_GT.SizeF = new SizeF(335.42F, 240.62F);
            // Create a pie series.
            Series series1 = new Series("Land Area by Country", ViewType.Pie3D);

            // Bind the series to data.
            series1.DataSource = dt;
            series1.ArgumentDataMember = "GIOI_TINH";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_GT" });
            // Add the series to the chart.
            chart_GT.Series.Add(series1);
            // Format the the series labels.
            //series1.Label.TextPattern = "{VP:p0} ({V:.##}M km²)";
            // Format the series legend items.
            series1.LegendTextPattern = "{A}";

             // Adjust the position of series labels. 
             ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;

            // Detect overlapping of series labels.
            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the view-type-specific options of the series.
            Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;

            // Specify a data filter to explode points.
            //myView.RuntimeExploding = true;
            myView.SizeAsPercentage = 100;
            // Customize the legend.
            chart_GT.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            Legend legend = chart_GT.Legend;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            // Add the chart to the form.
            chart_GT.Dock = XRDockStyle.Fill;
            //chart_GT.Dock = DockStyle.Fill;
            //this.Controls.Add(chart_GT);
        }
        private void loadcharLoaiCV(DataTable dt)
        {

            chart_LCV.Series.Clear();
            chart_LCV.Titles.Clear();
            chart_LCV.Titles.Add(new ChartTitle() { Text = Commons.Modules.ObjLanguages.GetLanguage("rptBieuDoPhanLoai", "lblTyLeCongNhanMay"), Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });

            chart_LCV.SizeF = new SizeF(335.42F, 240.62F);
            // Create a pie series.
            Series series1 = new Series("Land Area by Country", ViewType.Pie3D);

            // Bind the series to data.
            series1.DataSource = dt;
            series1.ArgumentDataMember = "TEN_LCV";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_CNM" });
            // Add the series to the chart.
            chart_LCV.Series.Add(series1);
            // Format the the series labels.
            //series1.Label.TextPattern = "{VP:p0} ({V:.##}M km²)";
            // Format the series legend items.
            series1.LegendTextPattern = "{A}";

            // Adjust the position of series labels. 
            ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;

            // Detect overlapping of series labels.
            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the view-type-specific options of the series.
            Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;

            // Specify a data filter to explode points.
            //myView.RuntimeExploding = true;
            myView.SizeAsPercentage = 100;
            // Customize the legend.
            chart_LCV.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            Legend legend = chart_LCV.Legend;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            // Add the chart to the form.
            chart_LCV.Dock = XRDockStyle.Fill;
            //this.Controls.Add(chart_GT);
        }

        private void loadchartIDD(DataTable dt)
        {

            chart_IDD.Series.Clear();
            chart_IDD.Titles.Clear();
            chart_IDD.Titles.Add(new ChartTitle() { Text = Commons.Modules.ObjLanguages.GetLanguage("rptBieuDoPhanLoai", "lblIDD") , Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)))});
            //chart_LCV.Titles.
            chart_IDD.SizeF = new SizeF(335.42F, 240.62F);
            // Create a pie series.
            Series series1 = new Series("Land Area by Country", ViewType.Pie3D);

            // Bind the series to data.
            series1.DataSource = dt;
            series1.ArgumentDataMember = "IDD";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_IDD" });
            // Add the series to the chart.
            chart_IDD.Series.Add(series1);
            // Format the the series labels.
            //series1.Label.TextPattern = "{VP:p0} ({V:.##}M km²)";
            // Format the series legend items.
            series1.LegendTextPattern = "{A}";

            // Adjust the position of series labels. 
            ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;

            // Detect overlapping of series labels.
            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the view-type-specific options of the series.
            Pie3DSeriesView myView = (Pie3DSeriesView)series1.View;

            // Specify a data filter to explode points.
            //myView.RuntimeExploding = true;
            myView.SizeAsPercentage = 100;
            // Customize the legend.
            chart_IDD.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            Legend legend = chart_IDD.Legend;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            // Add the chart to the form.
            chart_IDD.Dock = XRDockStyle.Fill;
            //this.Controls.Add(chart_GT);
        }

        private void rptBieuDoPhanLoai_BeforePrint(object sender, CancelEventArgs e)
        {
            this.xrSubreport4.ReportSource = new srptBDGioiTinh(dt);
            this.xrSubreport2.ReportSource = new srptBDLoaiCV(dt1);
            this.xrSubreport3.ReportSource = new srptBDIDD(dt2);

            loadcharGioiTinh(dt);
            loadcharLoaiCV(dt1);
            loadchartIDD(dt2);
        }

        private void chart_GT_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if(e.SeriesPoint.Argument.ToString() != "Nam")
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
            }
            else
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
            }

        }

        private void chart_LCV_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Argument.ToString() == "May")
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
            }
            else
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
            }
        }

        private void chart_IDD_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Argument.ToString() == "Trực tiếp")
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#548DD4");
            }
            else
            {
                e.SeriesDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
                e.LegendDrawOptions.Color = System.Drawing.ColorTranslator.FromHtml("#FF6463");
            }
        }
    }
}
