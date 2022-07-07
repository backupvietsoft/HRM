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
    public partial class frmEditLOAI_TRINH_DO : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLOAI_TRINH_DO(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLOAI_TRINH_DO_Load(object sender, EventArgs e)
        {
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT) FROM dbo.LOAI_TRINH_DO";
                txtSTT.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditLOAI_TRINH_DO_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LOAI_TD, TEN_LOAI_TD, TEN_LOAI_TD_A, TEN_LOAI_TD_H, KY_HIEU , STT " +
                    "FROM LOAI_TRINH_DO WHERE ID_LOAI_TD = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LOAI_TDTextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_TD"].ToString();
                TEN_LOAI_TD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_TD_A"].ToString();
                TEN_LOAI_TD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LOAI_TD_H"].ToString();
                txtKY_HIEU.EditValue = dtTmp.Rows[0]["KY_HIEU"].ToString();
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
                TEN_LOAI_TDTextEdit.EditValue = String.Empty;
                TEN_LOAI_TD_ATextEdit.EditValue = String.Empty;
                TEN_LOAI_TD_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                txtKY_HIEU.EditValue = string.Empty;
                TEN_LOAI_TDTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_TRINH_DO", (AddEdit ? -1 : Id),
                                TEN_LOAI_TDTextEdit.EditValue, TEN_LOAI_TD_ATextEdit.EditValue, TEN_LOAI_TD_HTextEdit.EditValue, txtKY_HIEU.EditValue ,(txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
                            if (AddEdit)
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
                    case "thoat":
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LOAI_TD",
                    (AddEdit ? "-1" : Id.ToString()), "LOAI_TRINH_DO", "TEN_LOAI_TD", TEN_LOAI_TDTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_LOAI_TDTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_LOAI_TD_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LOAI_TD",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_TRINH_DO", "TEN_LOAI_TD_A", TEN_LOAI_TD_ATextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_LOAI_TD_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_LOAI_TD_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LOAI_TD",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_TRINH_DO", "TEN_LOAI_TD_H", TEN_LOAI_TD_HTextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_LOAI_TD_HTextEdit.Focus();
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
    }
}