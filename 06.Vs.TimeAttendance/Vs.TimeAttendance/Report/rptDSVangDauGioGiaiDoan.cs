using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSVangDauGioGiaiDoan : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSVangDauGioGiaiDoan(DateTime tngay, DateTime dngay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string NgayIn = "0" + DateTime.Now.Day;
            string ThangIn = "0" + DateTime.Now.Month;
            string NamIn = "00" + DateTime.Now.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + NgayIn.Substring(NgayIn.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangIn.Substring(ThangIn.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamIn.Substring(NamIn.Length - 4, 4);

            string tNgayXem = "0" + tngay.Day;
            string tThangXem = "0" + tngay.Month;
            string tNamXem = "00" + tngay.Year;

            lbltungay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "@tuNgay", "NgayThangNam") + " " + tNgayXem.Substring(tNgayXem.Length - 2, 2) +
                              "/" + tThangXem.Substring(tThangXem.Length - 2, 2) + "/" + tNamXem.Substring(tNamXem.Length - 4, 4);

            string dNgayXem = "0" + dngay.Day;
            string dThangXem = "0" + dngay.Month;
            string dNamXem = "00" + dngay.Year;

            lbldenngay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "@denNgay", "NgayThangNam") + " " + dNgayXem.Substring(dNgayXem.Length - 2, 2) +
                              "/" + dThangXem.Substring(dThangXem.Length - 2, 2) + "/" + dNamXem.Substring(dNamXem.Length - 4, 4);

        }

        private void rptBCDanhGiaTrinhDo_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
