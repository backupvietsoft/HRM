using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptLuongChuyenKhoan_TG : DevExpress.XtraReports.UI.XtraReport
    {
        string sNgay = "";
        public rptLuongChuyenKhoan_TG(DateTime ngayin,string sTinh,int iIDDV)
        {

            InitializeComponent();

            DataTable dt = Commons.Modules.ObjSystems.DataReportHeader(iIDDV);

            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;
            lbNgay.Text = sTinh +", Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

        }
    }

}
