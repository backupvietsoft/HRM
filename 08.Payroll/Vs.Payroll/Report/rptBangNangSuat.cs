﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBangNangSuat : DevExpress.XtraReports.UI.XtraReport
    {
        DateTime ngay;
        DateTime TNgay;
        DateTime DNgay;
        public rptBangNangSuat(DateTime ngayin, DateTime tngay, DateTime dngay  )
        {

            InitializeComponent();

            TNgay = tngay;
            DNgay = dngay;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            ngay = ngayin;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));
            lbDNgay.Text = ngay.ToString("dd/MM/yyyy");
            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }

        private void rptBangNangSuat_BeforePrint(object sender, CancelEventArgs e)
        {
            xrLabel5.Text = TNgay.ToShortDateString();
            xrLabel3.Text = DNgay.ToShortDateString();
        }
    }
}
