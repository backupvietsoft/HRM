using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBCChenhLechTangCa : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCChenhLechTangCa(string TieuDe, DateTime ngayin, DateTime TNgay, DateTime DNgay)
        {
            InitializeComponent();
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            xrSubreport1.ReportSource = new SubReportHeader();

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

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void rptBCChenhLechTangCa_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
