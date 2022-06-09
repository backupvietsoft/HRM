﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptDanhSachUngLuong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachUngLuong(DateTime lthang)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            string Thang = "0" + lthang.Month;
            string Nam = "00" + lthang.Year;

            time.Text = "Tháng " + Thang.Substring(Thang.Length - 2, 2) + " Năm " + Nam.Substring(Nam.Length - 4, 4);


        }

    }

}
