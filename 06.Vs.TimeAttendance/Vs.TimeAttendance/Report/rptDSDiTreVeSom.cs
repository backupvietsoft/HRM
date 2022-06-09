using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSDiTreVeSom : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSDiTreVeSom(DateTime bcngay,string tieuDe, DateTime ngayxem)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            NONlblTIEU_DE.Text = tieuDe;
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string NgayXem = "0" + ngayxem.Day;
            string ThangXem = "0" + ngayxem.Month;
            string NamXem = "00" + ngayxem.Year;
            string BcNgay = "0" + bcngay.Day;
            string BcThang = "0" + bcngay.Month;
            string BcNam = "00" + bcngay.Year;
            NONNlbTieuDe2.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + BcNgay.Substring(BcNgay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + BcThang.Substring(BcThang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + BcNam.Substring(BcNam.Length - 4, 4);

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + ThangXem.Substring(ThangXem.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + NamXem.Substring(NamXem.Length - 4, 4);

        }

    }
}
