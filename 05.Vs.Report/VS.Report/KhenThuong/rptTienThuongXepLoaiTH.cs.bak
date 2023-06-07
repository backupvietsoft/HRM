using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptTienThuongXepLoaiTH : DevExpress.XtraReports.UI.XtraReport
    {
       
        public rptTienThuongXepLoaiTH(DateTime ngayIn, DateTime TuNgay, DateTime DenNgay)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            NONlbGiaiDoan.Text = "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy");
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));
            string Ngay = "0" + ngayIn.Day;
            string Thang = "0" + ngayIn.Month;
            string Nam = "00" + ngayIn.Year;
            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }
        private void NONNDocTien_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double fSum = 0;
            try
            {
                fSum = Convert.ToDouble(txTienThuong.Summary.GetResult());
            }
            catch
            {
            }
            int a = (int)Math.Round(fSum);

            string sSql = "SELECT dbo.DoiTienSoThanhChuTiengViet('" + a.ToString() + "','vnd')";
            string bangChu = ".";
            try
            {
                bangChu = Convert.ToString(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));
            }
            catch
            {
                bangChu = ".";
            }
            NONNDocTien.Text = bangChu + ".";
        }
    }
}
