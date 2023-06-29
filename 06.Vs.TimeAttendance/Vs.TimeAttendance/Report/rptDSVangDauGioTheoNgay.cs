using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSVangDauGioTheoNgay : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSVangDauGioTheoNgay(DateTime ngayin,DateTime ngayXemBC, int iddv)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader(iddv);

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            string NgayXemBC = "0" + ngayXemBC.Day;
            string ThangXemBC = "0" + ngayXemBC.Month;
            string NamXemBC = "00" + ngayXemBC.Year;

            //lblTIEU_DE.Text = tieuDe;

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

            NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXemBC.Substring(NgayXemBC.Length - 2, 2) +
                                "/" + ThangXemBC.Substring(ThangXemBC.Length - 2, 2) + 
                                "/" + NamXemBC.Substring(NamXemBC.Length - 4, 4);

        }

    }
}
