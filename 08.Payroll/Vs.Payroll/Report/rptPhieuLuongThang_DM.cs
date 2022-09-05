using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Vs.Payroll
{
    public partial class rptPhieuLuongThang_DM : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPhieuLuongThang_DM(DateTime datTHang)
        {
            InitializeComponent();
            lblTieuDe.Text = "PHIẾU THANH TOÁN LƯƠNG THÁNG "+Convert.ToDateTime(datTHang).ToString("MM/yyyy")+"";
        }

    }
}
