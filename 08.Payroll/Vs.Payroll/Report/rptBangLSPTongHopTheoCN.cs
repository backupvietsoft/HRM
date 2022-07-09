﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptBangLSPTongHopTheoCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangLSPTongHopTheoCN(DateTime tngay, DateTime dngay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            time.Text = Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTongHopTheoCN","lblTNgay")  + " " + tngay.ToString("dd/MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTongHopTheoCN", "lblDNgay") + " " + dngay.ToString("dd/MM/yyyy");
        }

    }

}
