using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBangTongHopCongNam : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangTongHopCongNam( DateTime TNgay, DateTime DNgay)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + DateTime.Now.Day;
            string Thang = "0" + DateTime.Now.Month;
            string Nam = "00" + DateTime.Now.Year;

            //string NgayXem = "0" + ngayin.Day;
            //string ThangXem = "0" + ngayin.Month;
            //string NamXem = "00" + ngayin.Year;

            tiNtime.Text = (Commons.Modules.TypeLanguage == 0 ? "Từ tháng " : "From month ") + TNgay.ToString("MM/yyyy") + (Commons.Modules.TypeLanguage == 0 ? " đến tháng " : " to the month ") + DNgay.ToString("MM/yyyy");
            lblNgay.Text = "Tiền Giang, " + Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam").ToLower() + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
        }

    }
}
