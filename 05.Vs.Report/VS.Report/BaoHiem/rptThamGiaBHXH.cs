using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptThamGiaBHXH : DevExpress.XtraReports.UI.XtraReport
    {
        Int64 idcn = 0;
        public rptThamGiaBHXH(Int64 id)
        {
            InitializeComponent();
            idcn = id;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            try
            {

                xrTableCell10.TextFormatString = this.xrTableCell10.TextFormatString = "{0:n" + Commons.Modules.iSoLeTT.ToString() + "}";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptThamGiaBHXH", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                lblSO_BHXH.Text = lblSO_BHXH.Text + " :<b> " + dt.Rows[0]["SO_BHXH"] + "</b>";
                lbl_HO_TEN.Text = lbl_HO_TEN.Text + " :<b> " + dt.Rows[0]["HO_TEN"] + "</b>";
                lblNGAY_SINH.Text = lblNGAY_SINH.Text + " :<b> " + Convert.ToDateTime(dt.Rows[0]["NGAY_SINH"]).ToString("dd/MM/yyyy") + "</b>";
                lblCHUC_VU.Text = lblCHUC_VU.Text + " :<b> " + dt.Rows[0]["TEN_CV"] + "</b>";
                lblDON_VI_CONG_TAC.Text = lblDON_VI_CONG_TAC.Text + " :<b> " + dt.Rows[0]["TEN_XN"] + "</b>";
                lblGIOI_TINH.Text = lblGIOI_TINH.Text + " :<b> " + dt.Rows[0]["PHAI"] + "</b>";
                lblDIA_CHI_THUONG_TRU.Text = lblDIA_CHI_THUONG_TRU.Text + " :<b> " + dt.Rows[0]["DIA_CHI_THUONG_TRU"] + "</b>";
                lblDON_VI.Text = "<b> " + dt.Rows[0]["TEN_DV"] + "</b>";
                lblDIA_CHI_THUONG_TRU.Text = lblDIA_CHI_THUONG_TRU.Text + " :<b> " + dt.Rows[0]["DIA_CHI_THUONG_TRU"] + "</b>";
            }
            catch { }
        }

    }
}


