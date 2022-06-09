using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDKTangCa : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDKTangCa(DateTime ngay,string tieuDe)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            DateTime ngayin = DateTime.Now;

            string NgayIn = "0" + ngayin.Day;
            string ThangIn = "0" + ngayin.Month;
            string NamIn = "00" + ngayin.Year;

            string Ngay = "0" + ngay.Day;
            string Thang = "0" + ngay.Month;
            string Nam = "00" + ngay.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + NgayIn.Substring(NgayIn.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangIn.Substring(ThangIn.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamIn.Substring(NamIn.Length - 4, 4);

            NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + "/" +
                                                                  Thang.Substring(Thang.Length - 2, 2) + "/" + Nam.Substring(Nam.Length - 4, 4);

        }

    }
}
