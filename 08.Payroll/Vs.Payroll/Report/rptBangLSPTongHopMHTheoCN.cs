using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptBangLSPTongHopMHTheoCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangLSPTongHopMHTheoCN(DateTime tngay, DateTime dngay, DateTime ngayxem)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            xrTlbltungay.Text = Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTongHopMHTheoCN", "lblTNgay") + " " + tngay.ToString("dd/MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTongHopMHTheoCN", "lblDNgay") + " " + dngay.ToString("dd/MM/yyyy");

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string NgayXem = "0" + ngayxem.Day;
            string ThangXem = "0" + ngayxem.Month;
            string NamXem = "00" + ngayxem.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangXem.Substring(ThangXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamXem.Substring(NamXem.Length - 4, 4);

        }

    }

}
