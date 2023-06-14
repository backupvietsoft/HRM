using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBangLuongThang_TG : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangLuongThang_TG(string ngay,string nx,string xn, string to)
        {
            InitializeComponent();
            DataTable dt = Commons.Modules.ObjSystems.DataThongTinChung();
            this.DataSource = dt;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            NONNX.Text = nx;
            NONXN.Text = xn;
            NONPB.Text = to;
            lblTIEU_DE.Text += " - " + ngay;
        }
    }
}
