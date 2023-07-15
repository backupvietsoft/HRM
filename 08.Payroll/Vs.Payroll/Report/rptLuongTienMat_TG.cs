using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;

namespace Vs.Payroll
{
    public partial class rptLuongTienMat_TG : DevExpress.XtraReports.UI.XtraReport
    {
        public rptLuongTienMat_TG(string ngay, string nx, int ixn, string to,DateTime ngayin)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            if(ixn == 2)
            {
                lblGiamDocXN.Text = "GĐNM CL";
            }
            else
            {
                lblGDDH_CL.Visible = false;
            }    
            NONNX.Text = nx;
            NONPB.Text = to;
            lblThang.Text += " - " + ngay;

            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;
            lblNgay.Text = "Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);

        }
    }

}
