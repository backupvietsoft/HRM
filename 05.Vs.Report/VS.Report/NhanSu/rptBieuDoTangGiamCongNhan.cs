using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using System.Windows.Forms;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace VS.Report.NhanSu
{
    public partial class rptBieuDoTangGiamCongNhan : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        int Nam = 2021;
        public rptBieuDoTangGiamCongNhan(DataTable dt, int nam)
        {
            InitializeComponent();
            dt1 = dt;
            Nam = nam;
        }

        private void loadcharTinhTrangCN(DataTable dt)
        {
            
            xrChart1.Series.Clear();
            xrChart1.Titles.Clear();

            xrChart1.SizeF = new SizeF(650F, 600F);
            xrChart1.Titles.Add(new ChartTitle() { Text = Commons.Modules.TypeLanguage == 1 ? "APPOINTED AND RESIGNED EMPLOYEES COMPARISON OF" : "BIỂU ĐỒ TĂNG GIẢM CÔNG NHÂN "+Nam+"" });
            // Create a pie series.

            Series series1 = new Series("Appointed", ViewType.Bar);
            series1.ArgumentScaleType = ScaleType.Numerical;
            // Bind the series to data.
            series1.DataSource = dt;
            series1.ArgumentDataMember = "THANG";
            series1.ValueScaleType = ScaleType.Numerical;
            series1.ValueDataMembers.AddRange(new string[] { "SL_TANG" });

            //Series series1 = new Series("Side-by-Side Bar Series 2", ViewType.Bar);
            //series1.Points.Add(new SeriesPoint("A", 15));
            //series1.Points.Add(new SeriesPoint("B", 18));
            //series1.Points.Add(new SeriesPoint("C", 25));
            //series1.Points.Add(new SeriesPoint("D", 33));


            //Series series2 = new Series("Side-by-Side Bar Series 2", ViewType.Bar);
            //series2.Points.Add(new SeriesPoint("A", 15));
            //series2.Points.Add(new SeriesPoint("B", 18));
            //series2.Points.Add(new SeriesPoint("C", 25));
            //series2.Points.Add(new SeriesPoint("D", 33));

            Series series2 = new Series("Resigned", ViewType.Bar);
            series2.ArgumentScaleType = ScaleType.Numerical;
            // Bind the series to data.
            series2.DataSource = dt;
            series2.ArgumentDataMember = "THANG";
            series2.ValueScaleType = ScaleType.Numerical;
            series2.ValueDataMembers.AddRange(new string[] { "SL_GIAM" });


            xrChart1.Series.Add(series1);
            xrChart1.Series.Add(series2);

            // Set some properties to get a nice-looking chart.
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            ((SideBySideBarSeriesView)series1.View).Color = Color.Orange;
            ((SideBySideBarSeriesView)series2.View).Color = Color.Blue;
            //((XYDiagram)xrChart1.Diagram).AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // Dock the chart into its parent and add it to the current form.
            //xrChart1.Dock = DockStyle.Fill;
            //this.Controls.Add(xrChart1);



            NumericScaleOptions numericScaleOptionsX = ((XYDiagram)xrChart1.Diagram).AxisX.NumericScaleOptions;
            numericScaleOptionsX.GridSpacing = 1;

            NumericScaleOptions numericScaleOptionsY = ((XYDiagram)xrChart1.Diagram).AxisY.NumericScaleOptions;
            numericScaleOptionsY.GridSpacing = 1;


            Legend legend = xrChart1.Legend;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            legend.Direction = LegendDirection.LeftToRight;

        }

        private void rptBieuDoTangGiamCongNhan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            loadcharTinhTrangCN(dt1);
        }
    }
}
