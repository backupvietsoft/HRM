using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptBCXepLoaiKhenThuongNam : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCXepLoaiKhenThuongNam(DateTime ngayin,DateTime nam)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            NONlbGiaiDoan.Text = "Năm " + nam.ToString("yyyy");
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
        }
        private int count(string nhan = "A")
        {
            int a =
                 ((txT01.Value != null && txT01.Text == nhan) ? 1 : 0) +
                 ((txT02.Value != null && txT02.Text == nhan) ? 1 : 0) +
                 ((txT03.Value != null && txT03.Text == nhan) ? 1 : 0) +
                 ((txT04.Value != null && txT04.Text == nhan) ? 1 : 0) +
                 ((txT05.Value != null && txT05.Text == nhan) ? 1 : 0) +
                 ((txT06.Value != null && txT06.Text == nhan) ? 1 : 0) +
                 ((txT07.Value != null && txT07.Text == nhan) ? 1 : 0) +
                 ((txT08.Value != null && txT08.Text == nhan) ? 1 : 0) +
                 ((txT09.Value != null && txT09.Text == nhan) ? 1 : 0) +
                 ((txT10.Value != null && txT10.Text == nhan) ? 1 : 0) +
                 ((txT11.Value != null && txT11.Text == nhan) ? 1 : 0) +
                 ((txT12.Value != null && txT12.Text == nhan) ? 1 : 0);
            return a;
        }

        private void txA_BeforePrint(object sender, CancelEventArgs e)
        {

            txA.Text = count("A").ToString();
            txB.Text = count("B").ToString();
            txC.Text = count("C").ToString();


        }
    }
}
