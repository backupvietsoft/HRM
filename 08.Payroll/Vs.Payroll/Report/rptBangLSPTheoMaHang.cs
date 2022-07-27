using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptBangLSPTheoMaHang : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBangLSPTheoMaHang(DateTime tngay, DateTime dngay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            time.Text = Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTheoMaHang", "lblTNgay") + " " + tngay.ToString("dd/MM/yyyy") + " " +  Commons.Modules.ObjLanguages.GetLanguage("rptBangLSPTheoMaHang", "lblDNgay") + " " + dngay.ToString("dd/MM/yyyy");

        }

    }

}
