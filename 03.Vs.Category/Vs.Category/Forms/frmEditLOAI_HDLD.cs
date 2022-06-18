using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Category
{
    public partial class frmEditLOAI_HDLD : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdLHD = 0;
        Boolean bAddEditLHD = true;  // true la add false la edit

        public frmEditLOAI_HDLD(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            iIdLHD = iId;
            bAddEditLHD = bAddEdit;
        }
        
        private void frmEditLOAI_HDLD_Load(object sender, EventArgs e)
        {
            LoadCombo();
            if (!bAddEditLHD) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditLOAI_HDLD_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LHDLD, TEN_LHDLD, TEN_LHDLD_A, TEN_LHDLD_H, SO_THANG, ID_TT_HD, STT " +
                    "FROM LOAI_HDLD WHERE ID_LHDLD = " + iIdLHD.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LHDLDTextEdit.EditValue = dtTmp.Rows[0]["TEN_LHDLD"].ToString();
                TEN_LHDLD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LHDLD_A"].ToString();
                TEN_LHDLD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LHDLD_H"].ToString();
                SO_THANGTextEdit.EditValue = dtTmp.Rows[0]["SO_THANG"].ToString();
                cboID_TT_HT.EditValue = dtTmp.Rows[0]["ID_TT_HD"].ToString();
                txtSTT.EditValue = dtTmp.Rows[0]["STT"].ToString();
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
                TEN_LHDLDTextEdit.EditValue = String.Empty;
                TEN_LHDLD_ATextEdit.EditValue = String.Empty;
                TEN_LHDLD_HTextEdit.EditValue = String.Empty;
                SO_THANGTextEdit.EditValue = 0;
                cboID_TT_HT.EditValue = -1;
                txtSTT.EditValue = 1;
                TEN_LHDLDTextEdit.Focus();
            }
            catch { }
        }

        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
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
                            if (Convert.ToInt64(cboID_TT_HT.EditValue) < 0)
                            {
                                XtraMessageBox.Show(ItemForTEN_TT_HT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                cboID_TT_HT.Focus();
                                return;
                            }
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_HDLD", (bAddEditLHD ? -1 : iIdLHD),
                                TEN_LHDLDTextEdit.EditValue, TEN_LHDLD_ATextEdit.EditValue,
                                TEN_LHDLD_HTextEdit.EditValue, SO_THANGTextEdit.EditValue, Convert.ToInt64(cboID_TT_HT.EditValue), (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
                            if (bAddEditLHD)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHDLD",
                    (bAddEditLHD ? "-1" : iIdLHD.ToString()), "LOAI_HDLD", "TEN_LHDLD", TEN_LHDLDTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_LHDLDTextEdit.Focus();
                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
            return false;
        }
        private void LoadCombo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TT_HT, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", true, true);
            }
            catch { }
        }
    }
}