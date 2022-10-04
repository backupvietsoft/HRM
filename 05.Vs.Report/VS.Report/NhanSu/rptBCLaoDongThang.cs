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
        public rptBCLaoDongThang(DateTime Thang)
        {
            InitializeComponent();
            System.String BCThang = Thang.ToString("dd/MM/yyyy");
            lblTieuDe.Text += " " + BCThang.Substring(3, 7);
        }

    }
}
