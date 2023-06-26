using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Category
{
    public partial class frmEditDON_VI : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdDV = -1;
        Boolean bAddEditDV = true;  // true la add false la edit
        string MS = "";
        public frmEditDON_VI(Int64 iId, Boolean bAddEdit)//DataRowView row
        {
            InitializeComponent();
            iIdDV = iId;
            bAddEditDV = bAddEdit;
        }

        private void frmEditDON_VI_Load(object sender, EventArgs e)
        {
            try
            {
                ItemForTruongDonVi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                DIEN_THOAI_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                EMAIL_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                HOTLINE_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForFB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblTenDataLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblDuongDanDataLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (Commons.Modules.KyHieuDV == "DM")
                {
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt, "ID_CN", "HO_TEN", "HO_TEN");
                    ItemForTruongDonVi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    DIEN_THOAI_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    EMAIL_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    HOTLINE_TD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    ItemForFB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Commons.Modules.UserName == "admin" || Commons.Modules.UserName == "administrator")
                {
                    lblTenDataLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lblDuongDanDataLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (!bAddEditDV)
                {
                    LoadText();
                }
                else
                {
                    string strSQL = "SELECT MAX(STT_DV) FROM dbo.DON_VI";
                    STT_DVTextEdit.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
                }


                //System.Threading.Thread myNewThread = new System.Threading.Thread(() => Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL));
                //myNewThread.Start();
            }
            catch { }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

        }

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_DV ,MSDV ,TEN_DV ,TEN_DV_A ,TEN_DV_H ,TEN_NGAN ,DIA_CHI ,MAC_DINH ,CHU_QUAN ,DIEN_THOAI ,FAX ,MS_BHYT ,MS_BHXH ,SO_TAI_KHOAN ,TEN_NGAN_HANG ,KY_HIEU ,NGUOI_DAI_DIEN ,CHUC_VU ,SO_HS,STT_DV, ID_CN, FACEBOOK_TD, TEN_DATA_LINK, DUONG_DAN_DATA_LINK,EMAIL_TD,DIEN_THOAI_TD,HOTLINE_TD, ISNULL(ACTIVE_DV,0) ACTIVE_DV FROM dbo.DON_VI WHERE ID_DV =	" + iIdDV.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                ItemForMSDV.Control.Text = dtTmp.Rows[0]["MSDV"].ToString();
                MS = dtTmp.Rows[0]["MSDV"].ToString();
                ItemForTEN_DON_VI.Control.Text = dtTmp.Rows[0]["TEN_DV"].ToString();
                ItemForTEN_DON_VI_A.Control.Text = dtTmp.Rows[0]["TEN_DV_A"].ToString();
                ItemForTEN_DON_VI_H.Control.Text = dtTmp.Rows[0]["TEN_DV_H"].ToString();
                ItemForTEN_NGAN.Control.Text = dtTmp.Rows[0]["TEN_NGAN"].ToString();
                ItemForDIA_CHI.Control.Text = dtTmp.Rows[0]["DIA_CHI"].ToString();
                MAC_DINHCheckEdit.EditValue = Convert.ToBoolean(dtTmp.Rows[0]["MAC_DINH"]);
                ItemForCHU_QUAN.Control.Text = dtTmp.Rows[0]["CHU_QUAN"].ToString();
                ItemForDIEN_THOAI.Control.Text = dtTmp.Rows[0]["DIEN_THOAI"].ToString();
                ItemForFAX.Control.Text = dtTmp.Rows[0]["FAX"].ToString();
                ItemForMS_BHYT.Control.Text = dtTmp.Rows[0]["MS_BHYT"].ToString();
                ItemForMS_BHXH.Control.Text = dtTmp.Rows[0]["MS_BHXH"].ToString();
                ItemForSO_TAI_KHOAN.Control.Text = dtTmp.Rows[0]["SO_TAI_KHOAN"].ToString();
                ItemForTEN_NGAN_HANG.Control.Text = dtTmp.Rows[0]["TEN_NGAN_HANG"].ToString();
                ItemForFB.Control.Text = dtTmp.Rows[0]["FACEBOOK_TD"].ToString();
                ItemForNGUOI_DAI_DIEN.Control.Text = dtTmp.Rows[0]["NGUOI_DAI_DIEN"].ToString();
                ItemForSTT_DV.Control.Text = dtTmp.Rows[0]["STT_DV"].ToString();
                cboID_CN.EditValue = dtTmp.Rows[0]["ID_CN"].ToString() == "" ? -1 : Convert.ToInt64(dtTmp.Rows[0]["ID_CN"]);
                txtTenDataLink.Text = dtTmp.Rows[0]["TEN_DATA_LINK"].ToString();
                txtDuongDanDataLink.Text = dtTmp.Rows[0]["DUONG_DAN_DATA_LINK"].ToString();
                EMAIL_TDTextEdit.Text = dtTmp.Rows[0]["EMAIL_TD"].ToString();
                DIEN_THOAI_TDtextEdit.Text = dtTmp.Rows[0]["DIEN_THOAI_TD"].ToString();
                HOTLINE_TDtextEdit.Text = dtTmp.Rows[0]["HOTLINE_TD"].ToString();
                if (bAddEditDV)
                {
                    sSql = "SELECT ISNULL(MAX(STT_DV),0) + 1 FROM dbo.[DON_VI] ";
                    sSql = (String)SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                    STT_DVTextEdit.EditValue = Convert.ToInt64(sSql);
                }
                chkActive.EditValue = dtTmp.Rows[0]["ACTIVE_DV"];
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }

        }


        private void LoadTextNull()
        {
            try
            {
                ItemForMSDV.Control.Text = String.Empty;
                ItemForTEN_DON_VI.Control.Text = String.Empty;
                ItemForTEN_DON_VI_A.Control.Text = String.Empty;
                ItemForTEN_DON_VI_H.Control.Text = String.Empty;
                ItemForTEN_NGAN.Control.Text = String.Empty;
                ItemForDIA_CHI.Control.Text = String.Empty;
                MAC_DINHCheckEdit.EditValue = false;
                ItemForCHU_QUAN.Control.Text = String.Empty;
                ItemForDIEN_THOAI.Control.Text = String.Empty;
                ItemForFAX.Control.Text = String.Empty;
                ItemForMS_BHYT.Control.Text = String.Empty;
                ItemForMS_BHXH.Control.Text = String.Empty;
                ItemForSO_TAI_KHOAN.Control.Text = String.Empty;
                ItemForTEN_NGAN_HANG.Control.Text = String.Empty;
                ItemForNGUOI_DAI_DIEN.Control.Text = String.Empty;
                ItemForSTT_DV.Control.Text = String.Empty;
                ItemForFB.Control.Text = String.Empty;
                cboID_CN.EditValue = -1;
                txtTenDataLink.Text = String.Empty;
                txtDuongDanDataLink.Text = String.Empty;
                EMAIL_TDTextEdit.Text = string.Empty;
                DIEN_THOAI_TDtextEdit.Text = string.Empty;
                HOTLINE_TDtextEdit.Text = string.Empty;
                MSDVTextEdit.Focus();
                chkActive.Checked = false;  

            }
            catch { }
        }


        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (bKiemTrung()) return;
                            Commons.Modules.sId =
                            SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateDonVi", (bAddEditDV ? -1 : iIdDV), ItemForMSDV.Control.Text,
                                    ItemForTEN_DON_VI.Control.Text, ItemForTEN_DON_VI_A.Control.Text, ItemForTEN_DON_VI_H.Control.Text, ItemForTEN_NGAN.Control.Text,
                                    ItemForDIA_CHI.Control.Text, Convert.ToBoolean(MAC_DINHCheckEdit.EditValue), ItemForCHU_QUAN.Control.Text, ItemForDIEN_THOAI.Control.Text,
                                    ItemForFAX.Control.Text, ItemForMS_BHYT.Control.Text, ItemForMS_BHXH.Control.Text, ItemForSO_TAI_KHOAN.Control.Text,
                                    ItemForTEN_NGAN_HANG.Control.Text, ItemForNGUOI_DAI_DIEN.Control.Text,
                                     ItemForSTT_DV.Control.Text == "" ? ItemForSTT_DV.Control.Text = null : ItemForSTT_DV.Control.Text, Convert.ToInt64(cboID_CN.Text == "" ? cboID_CN.EditValue = null : cboID_CN.EditValue), ItemForFB.Control.Text, txtTenDataLink.Text, txtDuongDanDataLink.Text,EMAIL_TDTextEdit.Text,DIEN_THOAI_TDtextEdit.Text,HOTLINE_TDtextEdit.Text, chkActive.EditValue).ToString();

                            if (bAddEditDV)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_ThemThanhCongBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "huy":
                        {
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_DV",
                    (bAddEditDV ? "-1" : iIdDV.ToString()), "DON_VI", "MSDV", MSDVTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MSDVTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_DON_VITextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_DV",
                        (bAddEditDV ? "-1" : iIdDV.ToString()), "DON_VI", "TEN_DV", TEN_DON_VITextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_DON_VITextEdit.Focus();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
            return false;
        }

        private void frmEditDON_VI_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();


    }
}
