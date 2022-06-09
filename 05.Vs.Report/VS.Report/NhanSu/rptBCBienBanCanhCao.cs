using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace VS.Report
{
    public partial class rptBCBienBanCanhCao : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCBienBanCanhCao(DateTime ngayin)
        {
            InitializeComponent();

            lbNgay.Text = ngayin.ToString("dd/MM/yyyy");
        }

    }
}
