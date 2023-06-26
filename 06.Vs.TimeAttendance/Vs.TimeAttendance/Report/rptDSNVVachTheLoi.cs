using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Report
{
    public partial class rptDSNVVachTheLoi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDSNVVachTheLoi(DateTime dTNgay, DateTime dDNgay, DateTime ngayin, int iddv)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader(iddv);
            try
            {
                //lblTIEU_DE.Text = tieuDe;
                DataTable dtNgu = new DataTable();
                dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

                string Ngay = "0" + ngayin.Day;
                string Thang = "0" + ngayin.Month;
                string Nam = "00" + ngayin.Year;

                string NgayXem = "0" + dTNgay.Day;
                string ThangXem = "0" + dTNgay.Month;
                string NamXem = "00" + dTNgay.Year;

                if (dTNgay == dDNgay)
                {
                    lblngayxem.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + NgayXem.Substring(NgayXem.Length - 2, 2) +
                                      "/" + ThangXem.Substring(ThangXem.Length - 2, 2) + "/" + NamXem.Substring(NamXem.Length - 4, 4);
                }
                else
                {
                    lblngayxem.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Tag.ToString(), "lblTuNgay") + " " + dTNgay.ToString("dd/MM/yyyy") + "      " + Commons.Modules.ObjLanguages.GetLanguage(this.Tag.ToString(), "lblDenNgay") + " " + dDNgay.ToString("dd/MM/yyyy");
                }

                lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                    Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                    Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);

            }
            catch { }

        }

    }
}
