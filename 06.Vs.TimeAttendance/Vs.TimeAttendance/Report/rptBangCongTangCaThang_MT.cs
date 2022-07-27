using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptBangCongTangCaThang_MT : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangCongTangCaThang_MT(string TieuDe, DateTime ngayin, DateTime TNgay, DateTime DNgay)
        {
            InitializeComponent();
            this.Tag = "rptBangCongTangCaThang_MT";
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + DateTime.Now.Day;
            string Thang = "0" + DateTime.Now.Month;
            string Nam = "00" + DateTime.Now.Year;

            string NgayXem = "0" + ngayin.Day;
            string ThangXem = "0" + ngayin.Month;
            string NamXem = "00" + ngayin.Year;

            lblTIEU_DE.Text = TieuDe;
            time.Text = "Từ ngày " + TNgay.ToString("dd/MM/yyyy") + " đến ngày " + DNgay.ToString("dd/MM/yyyy");
            //NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) +
            //                   "/" + ThangXem.Substring(ThangXem.Length - 2, 2) + "/" + NamXem.Substring(NamXem.Length - 4, 4);

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangXem.Substring(ThangXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamXem.Substring(NamXem.Length - 4, 4);
        }

    }
}
