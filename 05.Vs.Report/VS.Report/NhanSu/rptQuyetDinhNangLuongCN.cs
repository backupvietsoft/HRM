using System;
using System.Data;

namespace Vs.Report
{
    public partial class rptQuyetDinhNangLuongCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptQuyetDinhNangLuongCN(DateTime ngayin)
        {  
            InitializeComponent();
            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;
            lbNgay.Text = "Tiền Giang, Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

        }
    }
}


