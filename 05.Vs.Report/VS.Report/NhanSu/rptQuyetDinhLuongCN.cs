using System;
using System.Data;

namespace Vs.Report
{
    public partial class rptQuyetDinhLuongCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuyetDinhLuongCN(DateTime ngayin)
        {  
            InitializeComponent();
            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;
            lbNgay.Text = "Tp.HCM, Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

        }
    }
}


