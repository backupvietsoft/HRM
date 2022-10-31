using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace VS.Report
{
    public partial class sprtTaiNanLaoDong : DevExpress.XtraReports.UI.XtraReport
    {
        public sprtTaiNanLaoDong(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;
        }

    }
}
