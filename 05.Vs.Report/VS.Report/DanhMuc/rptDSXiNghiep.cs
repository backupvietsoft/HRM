using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Commons;

namespace Vs.Report
{
    public partial class rptDSXiNghiep : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSXiNghiep()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();
        }

    }
}
