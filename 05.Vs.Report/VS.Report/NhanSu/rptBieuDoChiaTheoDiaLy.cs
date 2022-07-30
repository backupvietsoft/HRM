using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;
using DevExpress.XtraCharts;

namespace Vs.Report
{
    public partial class rptBieuDoChiaTheoDiaLy : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public rptBieuDoChiaTheoDiaLy(DataTable dtt, DataTable dtt1)
        {
            InitializeComponent();
            dt = dtt; 
            dt1 = dtt1;
            xrSubreport1.ReportSource = new SubReportHeader();
        }

        private void loadchartQUAN(DataTable dt)
        {
            // Create an empty chart.
            //ChartControl pieChart = new ChartControl();
            chart_QUAN.Series.Clear();
            chart_QUAN.Titles.Clear();
            chart_QUAN.Titles.Add(new ChartTitle() { Text = Commons.Modules.ObjLanguages.GetLanguage("rptBieuDoChiaTheoDiaLy", "lblSoLuong"), Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
            chart_QUAN.SizeF = new SizeF(550F, 560F);
            // Create a pie series.
            Series series1 = new Series("", ViewType.Pie);

            // Bind the series to data.
            series1.DataSource = dt;
            series1.ArgumentDataMember = "TEN_QUAN";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_QUAN" });

            // Add the series to the chart.
            chart_QUAN.Series.Add(series1);

            // Format the series legend items.
            series1.LegendTextPattern = "{A}";

            // Adjust the position of series labels. 
            //((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.Inside;

            // Detect overlapping of series labels.
            //((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            // Access the view-type-specific options of the series.
            PieSeriesView myView = (PieSeriesView)series1.View;

            // Specify a data filter to explode points.
            //myView.RuntimeExploding = true;
            //myView.SizeAsPercentage = 100;
            // Customize the legend.
            chart_QUAN.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart_QUAN.Legend.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Legend legend = chart_QUAN.Legend;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            // Add the chart to the form.
            chart_QUAN.Dock = XRDockStyle.Fill;
            //this.Controls.Add(chart_GT);
        }

        private void loadcharTPX(DataTable dt)
        {

            xrChart1.Series.Clear();
            xrChart1.Titles.Clear();
            xrChart1.Parameters.Add(new XRControlParameter("TEN_QUAN", null, "DATA_PX.TEN_QUAN"));
            //xrChart1.Titles.Add(new ChartTitle() { Text = series1.FilterCriteria.ToString(), Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });
            // Create the first side-by-side bar series and add points to it.
            Series series1 = new Series("", ViewType.Bar);
            series1.DataSource = dt;
            series1.ArgumentDataMember = "TEN_PX";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_PX" });
            //series1.FilterString  = 
            series1.FilterString = "DATA_PX.TEN_QUAN = ?TEN_QUAN";
            // Set up a filter to show products from the specified category only. Use the created parameter's name in the filter string.
            series1.FilterCriteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("DATA_PX.TEN_QUAN = ?TEN_QUAN");

            //// Create the second side-by-side bar series and add points to it.
            //Series series2 = new Series("Side-by-Side Bar Series 2", ViewType.Bar);
            //series2.Points.Add(new SeriesPoint("A", 15));
            //series2.Points.Add(new SeriesPoint("B", 18));
            //series2.Points.Add(new SeriesPoint("C", 25));
            //series2.Points.Add(new SeriesPoint("D", 33));
            //DataRowView row = series1.Points[0].Tag as DataRowView;
            //object a = row["TEN_QUAN"];

            // Add the series to the chart.
            xrChart1.Series.Add(series1);
            
            //xrChart1.Series.Add(series2);

            // Hide the legend (if necessary).
            xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // Rotate the diagram (if necessary).
            ((XYDiagram)xrChart1.Diagram).Rotated = true;

            // Add a title to the chart (if necessary).
            ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.True;\
            string s = series1.FilterCriteria.ToString();
            chartTitle1.Text = "123";

            // Add the chart to the form.
            //xrChart1.Dock = DockStyle.Fill;
            //this.Controls.Add(xrChart1);

        }

        private void rptBieuDoChiaTheoDiaLy_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            xrSubreport2.ReportSource = new srptBDCNTheoHuyen(dt);
            loadchartQUAN(dt);
            loadcharTPX(dt1);
            Commons.Modules.sLoad = "";

        }

        private void xrChart1_BoundDataChanged(object sender, EventArgs e)
        {
            //foreach (Series series in xrChart1.Series)
            //{
            //    if (series.Points.Count > 0)
            //    {
            //        DataRowView row = series.Points[0].Tag as DataRowView;
            //        ((SideBySideBarSeriesView)series.View).StackedGroup = row["TEN_QUAN"];
            //    }
            //}
        }
    }
}
