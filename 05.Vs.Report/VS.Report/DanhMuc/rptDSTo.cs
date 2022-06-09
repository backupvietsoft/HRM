using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VS.Report;
using Commons;

namespace Vs.Report
{
    public partial class rptDSTo : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSTo()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();
        }
    }
}
