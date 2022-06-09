using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using VS.Report;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSDonVi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSDonVi()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();
        }

    }
}
