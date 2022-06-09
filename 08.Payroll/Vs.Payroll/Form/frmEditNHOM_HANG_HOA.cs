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
    public partial class frmEditNHOM_HANG_HOA : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNHOM_HANG_HOA(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        #region even
        private void frmEditNHOM_HANG_HOA_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
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


                            Commons.Modules.sId = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNHOM_HANG_HOA", (AddEdit ? -1 : Id), txtTEN_NHH.EditValue.ToString(), txtTEN_NHH_A.EditValue.ToString(), txtTEN_NHH_H.EditValue.ToString(), txtTHU_TU.EditValue, txtNOTE.EditValue.ToString()));
                            if (AddEdit)
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
        private void frmEditNHOM_HANG_HOA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        #endregion

        #region function

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NHH, TEN_NHH, TEN_NHH_A, TEN_NHH_H, THU_TU, NOTE " +
                    "FROM NHOM_HANG_HOA WHERE ID_NHH = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                txtTEN_NHH.EditValue = dtTmp.Rows[0]["TEN_NHH"].ToString();
                txtTEN_NHH_A.EditValue = dtTmp.Rows[0]["TEN_NHH_A"].ToString();
                txtTEN_NHH_H.EditValue = dtTmp.Rows[0]["TEN_NHH_H"].ToString();
                txtTHU_TU.EditValue = dtTmp.Rows[0]["THU_TU"].ToString();
                txtNOTE.EditValue = dtTmp.Rows[0]["NOTE"].ToString();
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
                txtTEN_NHH.EditValue = String.Empty;
                txtTEN_NHH_A.EditValue = String.Empty;
                txtTEN_NHH_H.EditValue = String.Empty;
                txtTHU_TU.EditValue = String.Empty;
                txtTEN_NHH.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHH",
                    (AddEdit ? "-1" : Id.ToString()), "NHOM_HANG_HOA", "TEN_NHH", txtTEN_NHH.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    txtTEN_NHH.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(txtTEN_NHH_A.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHH",
                        (AddEdit ? "-1" : Id.ToString()), "NHOM_HANG_HOA", "TEN_NHH_A", txtTEN_NHH_A.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_NHH_A.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(txtTEN_NHH_H.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NHH",
                        (AddEdit ? "-1" : Id.ToString()), "NHOM_HANG_HOA", "TEN_NHH_H", txtTEN_NHH_H.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_NHH_H.Focus();
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
