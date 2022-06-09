using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptDSNVTangCaTheoNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSNVTangCaTheoNgay(DateTime NgayTC,DateTime ngayin,string tieuDe)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            NONlblTIEU_DE.Text = tieuDe;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + DateTime.Now.Day;
            string Thang = "0" + DateTime.Now.Month;
            string Nam = "00" + DateTime.Now.Year;

            string NgayXem = "0" + ngayin.Day;
            string ThangXem = "0" + ngayin.Month;
            string NamXem = "00" + ngayin.Year;

            //NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) +
            //                   "/" + ThangXem.Substring(ThangXem.Length - 2, 2) + "/" + NamXem.Substring(NamXem.Length - 4, 4);

            NONNlbTieuDe2.Text = Commons.Modules.TypeLanguage == 1 ? "Day" : "Ngày " + NgayTC.ToString("dd/MM/yyyy") + "";
            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangXem.Substring(ThangXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamXem.Substring(NamXem.Length - 4, 4);

        }

    }
}
