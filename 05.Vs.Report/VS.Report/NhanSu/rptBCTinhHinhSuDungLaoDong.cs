using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptBCTinhHinhSuDungLaoDong : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCTinhHinhSuDungLaoDong()
        {

        }
        public rptBCTinhHinhSuDungLaoDong(DateTime ngayIn, string strTieuDe1, string strTieuDe2)
        {   //DateTime ngayin
            InitializeComponent();
            NONNtieuDe.Text = NONNtieuDe.Text + " "+ strTieuDe1;
            NONNtieuDe2.Text = NONNtieuDe2.Text + " " + strTieuDe2;
            lblNgay.Text = "Ngày " + ngayIn.ToString("dd") + " Tháng " + ngayIn.ToString("MM") + " Năm " + ngayIn.ToString("yyyy");

        }

    }
}


