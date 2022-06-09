using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptBCTaiKyHopDongLaoDong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCTaiKyHopDongLaoDong(DateTime ngayin, String sTitle, DateTime TuNgay, DateTime DenNgay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            //string Ngay = "0" + ngayin.Day;
            //string Thang = "0" + ngayin.Month;
            //string Nam = "00" + ngayin.Year;

            NONlblTIEU_DE.Text = sTitle + TuNgay.ToString("MM/yyyy");
            //if (TuNgay == DenNgay)
            //{
            //    NONlblTIEU_DE.Text = " Đến ngày " + ngayin.ToString("dd/MM/yyyy");
            //}
            //else
            //{
            //    NONlblTIEU_DE.Text = "Từ ngày " + TuNgay.ToString("dd/MM/yyyy") + " đến ngày " + DenNgay.ToString("dd/MM/yyyy");
            //}

            //lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }

    }
}
