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
    public partial class frmEditNHOM_CHAM_CONG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNHOM_CHAM_CONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditNHOM_CHAM_CONG_Load(object sender, EventArgs e)
        {
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT) FROM dbo.NHOM_CHAM_CONG";
                txtSTT.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditNHOM_CHAM_CONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {

            try
            {
                string sSql = "SELECT ID_NHOM, TEN_NHOM, TEN_NHOM_A, TEN_NHOM_H, CA_TU_DONG, ISNULL(NHOM_CHE_DO,0) NHOM_CHE_DO ,STT " +
                    "FROM NHOM_CHAM_CONG WHERE ID_NHOM = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_NHOMTextEdit.EditValue = dtTmp.Rows[0]["TEN_NHOM"].ToString();
                TEN_NHOM_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_NHOM_A"].ToString();
                TEN_NHOM_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_NHOM_H"].ToString();
                txtSTT.EditValue = dtTmp.Rows[0]["STT"];
                chkNhomCheDo.Checked = Convert.ToBoolean(dtTmp.Rows[0]["NHOM_CHE_DO"]);
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
                TEN_NHOMTextEdit.EditValue = String.Empty;
                TEN_NHOM_ATextEdit.EditValue = String.Empty;
                TEN_NHOM_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                chkNhomCheDo.Checked = false;
            }
            catch { }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNHOM_CHAM_CONG", (AddEdit ? -1 : Id),
                                TEN_NHOMTextEdit.EditValue, TEN_NHOM_ATextEdit.EditValue, TEN_NHOM_HTextEdit.EditValue, Convert.ToBoolean(chkNhomCheDo.Checked), (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();

                            if (AddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_ThemThanhCongBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            };
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHOM",
                    (AddEdit ? "-1" : Id.ToString()), "NHOM_CHAM_CONG", "TEN_NHOM", TEN_NHOMTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_NHOMTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_NHOM_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHOM",
                        (AddEdit ? "-1" : Id.ToString()), "NHOM_CHAM_CONG", "TEN_NHOM_A", TEN_NHOM_ATextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_NHOM_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NHOM_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHOM",
                        (AddEdit ? "-1" : Id.ToString()), "NHOM_CHAM_CONG", "TEN_NHOM_H", TEN_NHOM_HTextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_NHOM_HTextEdit.Focus();
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