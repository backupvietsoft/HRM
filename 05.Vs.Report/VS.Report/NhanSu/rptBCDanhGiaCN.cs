﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBCDanhGiaCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCDanhGiaCN()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

        }

    }
}
