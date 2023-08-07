using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Vs.Report
{
    public partial class rptBCChenhLechTangCa : DevExpress.XtraReports.UI.XtraReport
    {
        private DateTime tngay;
        public rptBCChenhLechTangCa(string TieuDe, DateTime ngayin, DateTime TNgay, DateTime DNgay, int iddv)
        {
            InitializeComponent();
            tngay = TNgay;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            xrSubreport1.ReportSource = new SubReportHeader(iddv);

            string Ngay = "0" + DateTime.Now.Day;
            string Thang = "0" + DateTime.Now.Month;
            string Nam = "00" + DateTime.Now.Year;

            string NgayXem = "0" + ngayin.Day;
            string ThangXem = "0" + ngayin.Month;
            string NamXem = "00" + ngayin.Year;

            NONlblTIEU_DE.Text = TieuDe;
            NONNlbTieuDe2.Text = "Từ tháng " + TNgay.ToString("MM/yyyy") + " đến tháng " + DNgay.ToString("MM/yyyy");
            //NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) +
            //                   "/" + ThangXem.Substring(ThangXem.Length - 2, 2) + "/" + NamXem.Substring(NamXem.Length - 4, 4);

            lblNgay.Text = "Tiền Giang, " + Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam").ToLower() + " " + NgayXem.Substring(NgayXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangXem.Substring(ThangXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamXem.Substring(NamXem.Length - 4, 4);
        }

        private void xrTable1_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void rptBCChenhLechTangCa_BeforePrint(object sender, CancelEventArgs e)
        {
            for (int i = 8; i <= 39; i++)
            {
                try
                {
                    string sDate = xrTableRow1.Cells[i].Text.ToString() + "/" + tngay.ToString("MM/yyyy");
                    if (Convert.ToDateTime(sDate).DayOfWeek == DayOfWeek.Sunday)
                    {
                        xrTableRow1.Cells[i].BackColor = Color.Orange;
                    }
                }
                catch { }
            }
        }
    }
}
