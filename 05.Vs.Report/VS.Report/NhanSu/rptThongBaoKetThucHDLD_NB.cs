using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptThongBaoKetThucHDLD_NB : DevExpress.XtraReports.UI.XtraReport
    {
        public rptThongBaoKetThucHDLD_NB(DateTime ngayin)
        {   //DateTime ngayin
            InitializeComponent();

            string NgayBC = ngayin.Day.ToString();
            string ThangBC = ngayin.Month.ToString();
            string NamBC = ngayin.Year.ToString();

            //lbNgay.Text = "Tp.HCM, Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

            lblNgayInHD.Text = "Ngày " + NgayBC + " Tháng " + ThangBC + " Năm " + NamBC;
        }

    }
}


