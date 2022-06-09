using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBangTongHopCongThang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangTongHopCongThang(DateTime ngay, DateTime mgay2, string tde)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            lblTIEU_DE.Text = tde;
            xrSubreport1.ReportSource = new SubReportHeader();

        }

    }
}
