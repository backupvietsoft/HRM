using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Vs.Report
{
    public partial class rptThongTinChung : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongTinChung()
        {
            InitializeComponent();
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel3.Text = DateTime.Now.Date.ToShortDateString();
        }
    }
}
