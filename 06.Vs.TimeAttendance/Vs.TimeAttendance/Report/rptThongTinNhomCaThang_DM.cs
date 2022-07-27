using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Commons;
using System.Data;

namespace Vs.Report
{
    public partial class rptThongTinNhomCaThang_DM : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongTinNhomCaThang_DM(DateTime datTngay, DateTime datDNgay,DateTime ngayin)
        {
            InitializeComponent();
            this.Tag = "rptThongTinNhomCaThang_DM";
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrTTuNgay.Text = datTngay.ToString("dd/MM/yyyy");
            xrTDenNgay.Text = datDNgay.ToString("dd/MM/yyyy");
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lbTuNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
        }

    }
}
