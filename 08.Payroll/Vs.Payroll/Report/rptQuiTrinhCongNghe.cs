using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptQuiTrinhCongNghe : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuiTrinhCongNghe()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));
            //XRSubreport subreport = new XRSubreport()
            //{
            //    BoundsF = new RectangleF(0, 100, 550, 25),
            //};
            //// "mainReport" is an XtraReport instance.
            //// Add subreport to the main report's DetailBand.
            //.Bands["DetailBand"].Controls.Add(subreport);


            //string Ngay = "0" + ngayin.Day;
            //string Thang = "0" + ngayin.Month;
            //string Nam = "00" + ngayin.Year;

            //lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay","NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
            //    Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

        }

    }
}
