using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Payroll
{
    public partial class rptPhieuLuongThang_TG : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuLuongThang_TG(string sThang)
        {
            InitializeComponent();
            lblThang.Text += sThang;
        }

    }
}
