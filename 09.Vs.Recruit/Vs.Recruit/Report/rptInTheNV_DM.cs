using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Recruit
{
    public partial class rptInTheNV_DM : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInTheNV_DM(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;
            PicHINH_CN.DataBindings.Add("Text", this.DataSource, "HINH_CN");
        }

    }
}
