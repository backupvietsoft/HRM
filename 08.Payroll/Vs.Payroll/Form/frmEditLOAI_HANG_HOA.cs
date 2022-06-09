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
    public partial class frmEditLOAI_HANG_HOA : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLOAI_HANG_HOA(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        #region even
        private void frmEditLOAI_HANG_HOA_Load(object sender, EventArgs e)
        {
            LoadcboID_NHH();
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


                            Commons.Modules.sId = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_HANG_HOA", (AddEdit ? -1 : Id),Convert.ToInt32(cboID_NHH.EditValue) ,txtTEN_LHH.EditValue.ToString(), txtTEN_LHH_A.EditValue.ToString(), txtTEN_LHH_H.EditValue.ToString(), txtTHU_TU.EditValue));
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
        private void frmEditLOAI_HANG_HOA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        #endregion

        #region function
        private void LoadcboID_NHH()
        {
            try
            {
                DataTable dt_nhh = new DataTable();
                dt_nhh.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCboNHOM_HANG_HOA", Commons.Modules.TypeLanguage, false));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NHH, dt_nhh, "ID_NHH", "TEN_NHH", "TEN_NHH", true, false);
            }
            catch { }
        }
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LHH,ID_NHH, TEN_LHH, TEN_LHH_A, TEN_LHH_H, THU_TU " +
                    "FROM LOAI_HANG_HOA WHERE ID_LHH = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                cboID_NHH.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_NHH FROM LOAI_HANG_HOA T1, NHOM_HANG_HOA T2 WHERE " + Id + " = T1.ID_LHH AND T1.ID_NHH=T2.ID_NHH"));
                txtTEN_LHH.EditValue = dtTmp.Rows[0]["TEN_LHH"].ToString();
                txtTEN_LHH_A.EditValue = dtTmp.Rows[0]["TEN_LHH_A"].ToString();
                txtTEN_LHH_H.EditValue = dtTmp.Rows[0]["TEN_LHH_H"].ToString();
                txtTHU_TU.EditValue = dtTmp.Rows[0]["THU_TU"].ToString();
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
                txtTEN_LHH.EditValue = String.Empty;
                txtTEN_LHH_A.EditValue = String.Empty;
                txtTEN_LHH_H.EditValue = String.Empty;
                txtTHU_TU.EditValue = String.Empty;
                txtTEN_LHH.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHH",
                    (AddEdit ? "-1" : Id.ToString()), "LOAI_HANG_HOA", "TEN_LHH", txtTEN_LHH.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    txtTEN_LHH.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(txtTEN_LHH_A.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHH",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_HANG_HOA", "TEN_LHH_A", txtTEN_LHH_A.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_LHH_A.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(txtTEN_LHH_H.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHH",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_HANG_HOA", "TEN_LHH_H", txtTEN_LHH_H.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_LHH_H.Focus();
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
