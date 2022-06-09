using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBangCCTheoGD : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangCCTheoGD(DateTime tngay,DateTime dngay, String tieuDe ,DateTime ngayin)
        {


            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            lblTIEU_DE.Text = tieuDe;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            string tNgayXem = "0" + tngay.Day;
            string tThangXem = "0" + tngay.Month;
            string tNamXem = "00" + tngay.Year;

            lbltungay_ccct_GD.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "@tuNgay", "NgayThangNam") + " " + tNgayXem.Substring(tNgayXem.Length - 2, 2) +
                              "/" + tThangXem.Substring(tThangXem.Length - 2, 2) + "/" + tNamXem.Substring(tNamXem.Length - 4, 4);

            string dNgayXem = "0" + dngay.Day;
            string dThangXem = "0" + dngay.Month;
            string dNamXem = "00" + dngay.Year;

            lbldenngay_ccct_GD.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "@denNgay", "NgayThangNam") + " " + dNgayXem.Substring(dNgayXem.Length - 2, 2) +
                              "/" + dThangXem.Substring(dThangXem.Length - 2, 2) + "/" + dNamXem.Substring(dNamXem.Length - 4, 4);



            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }

    }
}
