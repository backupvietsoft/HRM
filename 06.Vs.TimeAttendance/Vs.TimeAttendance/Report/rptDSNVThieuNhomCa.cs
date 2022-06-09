using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSNVThieuNhomCa: DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSNVThieuNhomCa(DateTime ngayin,string tieuDe)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            NONlblTIEU_DE.Text = tieuDe;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + DateTime.Now.Day;
            string Thang = "0" + DateTime.Now.Month;
            string Nam = "00" + DateTime.Now.Year;

            string NgayXem = "0" + ngayin.Day;
            string ThangXem = "0" + ngayin.Month;
            string NamXem = "00" + ngayin.Year;

            NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) +
                               "/" + ThangXem.Substring(ThangXem.Length - 2, 2) + "/" + NamXem.Substring(NamXem.Length - 4, 4);

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }

    }
}
