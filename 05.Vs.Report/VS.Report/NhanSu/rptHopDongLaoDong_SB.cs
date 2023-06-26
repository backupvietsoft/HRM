using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptHopDongLaoDong_SB : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHopDongLaoDong_SB(DateTime ngayin) // hợp đồng đầu tiên
        {   //DateTime ngayin
            InitializeComponent();

            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;
        }

        private void rptHopDongLaoDong_SB_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }
    }
}


