﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBCQuaTrinhLuongTH : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCQuaTrinhLuongTH(DateTime ngayin, DateTime TuNgay, DateTime DenNgay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            NONlbGiaiDoan.Text = "Ngày " + ngayin.ToString("dd/MM/yyyy");


            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
            MergeByTag();
        }

        public void MergeByTag()
        {
            ExpressionBinding expressionBinding = new ExpressionBinding("BeforePrint", "Tag", "ToStr([MS_CN])");
            this.xrTableCell13.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell13.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell13.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell10.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell10.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell10.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell21.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell21.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell21.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell20.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell20.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell20.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;
        }

    }
}
