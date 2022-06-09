using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptBCLaoDongThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCLaoDongThang(DateTime ngayin)
        {
            InitializeComponent();
            lblTieuDe.Text += " " + ngayin.Date.ToString().Substring(3, 7);
        }

    }
}
