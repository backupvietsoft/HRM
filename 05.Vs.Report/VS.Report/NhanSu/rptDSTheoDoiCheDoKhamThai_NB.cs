using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSTheoDoiCheDoKhamThai_NB : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSTheoDoiCheDoKhamThai_NB(string Ngay , string Thang, string Nam )
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + Ngay + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam;

        }

    }
}
