using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptTheNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTheNhanVien(DateTime ngayin)
        {   //DateTime ngayin
            InitializeComponent();

            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;

            //lbNgay.Text = "Tp.HCM, Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

            this.Margins.Top = 0;
            this.Margins.Bottom = 0; 
            
        }

    }
}


