using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Commons;

namespace Vs.Report
{
    public partial class rptDSQuanLy : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSQuanLy()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();
        }

    }
}
