using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditDOI_TAC : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditDOI_TAC(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        #region even
        private void frmEditDOI_TAC_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditDOI_TAC_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();


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

                            Commons.Modules.sId = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateDOI_TAC", (AddEdit ? -1 : Id), txtMA_SO.EditValue.ToString(), txtTEN_NGAN.EditValue.ToString(), txtTEN_CTY_DAY_DU.EditValue.ToString(),  (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue));
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
        #endregion

        #region function
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_DT, MA_SO, TEN_NGAN, TEN_CTY_DAY_DU, INACTIVE, STT " +
                    "FROM DOI_TAC WHERE ID_DT = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                txtMA_SO.EditValue = dtTmp.Rows[0]["MA_SO"].ToString();
                txtTEN_NGAN.EditValue = dtTmp.Rows[0]["TEN_NGAN"].ToString();
                txtTEN_CTY_DAY_DU.EditValue = dtTmp.Rows[0]["TEN_CTY_DAY_DU"].ToString();
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
                txtMA_SO.EditValue = String.Empty;
                txtTEN_NGAN.EditValue = String.Empty;
                txtTEN_CTY_DAY_DU.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                txtMA_SO.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_DT",
                    (AddEdit ? "-1" : Id.ToString()), "DOI_TAC", "MA_SO", txtMA_SO.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMA_SO.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(txtTEN_NGAN.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_DT",
                        (AddEdit ? "-1" : Id.ToString()), "DOI_TAC", "TEN_NGAN", txtTEN_NGAN.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTEN_NGAN.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(txtTEN_CTY_DAY_DU.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_DT",
                        (AddEdit ? "-1" : Id.ToString()), "DOI_TAC", "TEN_CTY_DAY_DU", txtTEN_CTY_DAY_DU.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTEN_CTY_DAY_DU.Focus();
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
        #endregion

    }
}
