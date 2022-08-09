using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace VS.Report.NhanSu
{
    public partial class srptTest : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable idt = new DataTable();
        public srptTest(DataTable dt)
        {
            InitializeComponent();
            idt = dt;
            this.DataSource = idt;
            this.Tag = "srptBDCNTheoHuyen";
            this.Name = "srptBDCNTheoHuyen";
        }

        private void loadcharTPX(DataTable dt)
        {

            xrChart1.Series.Clear();
            xrChart1.Titles.Clear();
            xrChart1.Parameters.Add(new XRControlParameter("TEN_QUAN", null, "DATA_PX.TEN_QUAN"));
            // Create the first side-by-side bar series and add points to it.
            Series series1 = new Series("", ViewType.Bar);
            series1.DataSource = dt;
            series1.ArgumentDataMember = "TEN_PX";
            series1.ValueDataMembers.AddRange(new string[] { "TY_LE_PX" });
            //series1.FilterString  = 
            series1.FilterString = "DATA_PX.TEN_QUAN = ?TEN_QUAN";
            // Set up a filter to show products from the specified category only. Use the created parameter's name in the filter string.
            series1.FilterCriteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("DATA_PX.TEN_QUAN = ?TEN_QUAN");
            //xrChart1.Series[0].Label.TextPattern = "{A}: {V:F2}";
            // Add the series to the chart.
            xrChart1.Series.Add(series1);
            ExpressionBinding expressionBinding = new ExpressionBinding("BeforePrint", "Text", "[DATA_PX].[TEN_QUAN]");
            //xrTableCell8.ExpressionBindings.Add(expressionBinding);
            //xrChart1.Titles.Add(new ChartTitle() { Text =  
            //    , Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))) });

            // Hide the legend (if necessary).
            xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ((BarSeriesLabel)series1.Label).Position = BarSeriesLabelPosition.Top;
            ((BarSeriesLabel)series1.Label).LineVisibility = DevExpress.Utils.DefaultBoolean.False;
            ((BarSeriesLabel)series1.Label).Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ((BarSeriesLabel)series1.Label).Border.Color = Color.FromArgb(255, 255, 255);

            // Rotate the diagram (if necessary).
            ((XYDiagram)xrChart1.Diagram).Rotated = true;
            XYDiagram diagram = (XYDiagram)xrChart1.Diagram;
            diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //diagram.AxisX.LabelPosition = AxisLabelPosition.Inside;
            SideBySideBarSeriesView view = series1.View as SideBySideBarSeriesView;
            view.BarWidth = 0.2;
            view.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            // Add a title to the chart (if necessary).
            ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.True;\
            string s = series1.FilterCriteria.ToString();
            chartTitle1.Text = "123";
            // Add the chart to the form.
            //xrChart1.Dock = DockStyle.Fill;
            //this.Controls.Add(xrChart1);

        }

        private void srptTest_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            loadcharTPX(idt);
        }
    }
}
