using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace VS.Report
{
    public partial class sprtKhachQuanKhoTranh : DevExpress.XtraReports.UI.XtraReport
    {
        public sprtKhachQuanKhoTranh(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;

        }

    }
}
