using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Vs.Report
{
    public partial class rptQuyetDinhThayDoiLuong_SB : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuyetDinhThayDoiLuong_SB(DateTime Thang)
        {
            InitializeComponent();
            lbThang.Text = "No. " + Thang.ToString("MM/yyyy");
        }

    }
}
