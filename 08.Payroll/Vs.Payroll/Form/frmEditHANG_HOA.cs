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
    public partial class frmEditHANG_HOA : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditHANG_HOA(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        #region even
        private void frmEditHANG_HOA_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadcboID_NHH();
            LoadcboID_DT();
            LoadcboID_LHH();
            Commons.Modules.sLoad = "";
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditHANG_HOA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();


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
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateHANG_HOA", conn);
                            cmd.Parameters.Add("@ID_HH", SqlDbType.BigInt).Value = Id;
                            cmd.Parameters.Add("@MS_HH", SqlDbType.NVarChar).Value = txtMS_HH.Text;
                            cmd.Parameters.Add("@MS_HIEN_THI", SqlDbType.NVarChar).Value = txtMS_HIEN_THI.Text;
                            cmd.Parameters.Add("@TEN_HH", SqlDbType.NVarChar).Value = txtTEN_HH.Text;
                            cmd.Parameters.Add("@TEN_HH_A", SqlDbType.NVarChar).Value = txtTEN_HH_A.Text;
                            cmd.Parameters.Add("@ID_NHH", SqlDbType.Int).Value = cboID_NHH.EditValue;
                            cmd.Parameters.Add("@ID_LHH", SqlDbType.Int).Value = cboID_LHH.EditValue;
                            cmd.Parameters.Add("@ID_DT_MH", SqlDbType.BigInt).Value = cboID_DT.EditValue;
                            cmd.Parameters.Add("@INACTIVE", SqlDbType.Bit).Value = chkINACTIVE.EditValue;
                            cmd.Parameters.Add("@MO_TA", SqlDbType.NVarChar).Value = txtMOTA.Text;
                            cmd.CommandType = CommandType.StoredProcedure;
                            Commons.Modules.sId = Convert.ToString(cmd.ExecuteScalar());
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
        #endregion

        #region function
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_HH, MS_HH, MS_HIEN_THI, TEN_HH, TEN_HH_A, ID_NHH , ID_LHH,ID_DT_MH, INACTIVE, MOTA " +
                    "FROM HANG_HOA WHERE ID_HH = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                txtMS_HH.EditValue = dtTmp.Rows[0]["MS_HH"].ToString();
                txtMS_HIEN_THI.EditValue = dtTmp.Rows[0]["MS_HIEN_THI"].ToString();
                txtTEN_HH.EditValue = dtTmp.Rows[0]["TEN_HH"].ToString();
                txtTEN_HH_A.EditValue = dtTmp.Rows[0]["TEN_HH_A"].ToString();
                cboID_NHH.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_NHH FROM HANG_HOA T1, NHOM_HANG_HOA T2 WHERE " + Id + " = T1.ID_HH AND T1.ID_NHH=T2.ID_NHH"));
                cboID_LHH.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_LHH FROM HANG_HOA T1, LOAI_HANG_HOA T2 WHERE " + Id + " = T1.ID_HH AND T1.ID_LHH=T2.ID_LHH"));
                cboID_DT.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_DT FROM HANG_HOA T1, DOI_TAC T2 WHERE " + Id + " = T1.ID_HH AND T1.ID_DT_MH=T2.ID_DT"));
                chkINACTIVE.Checked = bool.Parse(dtTmp.Rows[0]["INACTIVE"].ToString());
                txtMOTA.EditValue = dtTmp.Rows[0]["MOTA"].ToString();
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }


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

        private void LoadcboID_LHH()
        {
            DataTable dt_lhh = new DataTable();
            dt_lhh.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCboLOAI_HANG_HOA", Commons.Modules.TypeLanguage, false));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHH, dt_lhh, "ID_LHH", "TEN_LHH", "TEN_LHH", true, false);
        }

        private void LoadcboID_DT()
        {
            DataTable dt_dt = new DataTable();
            dt_dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCboDOI_TAC", Commons.Modules.TypeLanguage, false));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DT, dt_dt, "ID_DT", "TEN_NGAN", "TEN_NGAN", true, false);
        }
       


        private void LoadTextNull()
        {
            try
            {
                txtMS_HH.EditValue = String.Empty;
                txtMS_HIEN_THI.EditValue = String.Empty;
                txtTEN_HH.EditValue = String.Empty;
                txtTEN_HH_A.EditValue = String.Empty;
                chkINACTIVE.Checked = false;
                cboID_LHH.EditValue = String.Empty;
                cboID_DT.EditValue = String.Empty;
                txtMOTA.EditValue = String.Empty;
            }
            catch { }
        }
        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_HH",
                    (AddEdit ? "-1" : Id.ToString()), "HANG_HOA", "MS_HH", txtMS_HH.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    txtMS_HH.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(txtMS_HIEN_THI.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_HH",
                        (AddEdit ? "-1" : Id.ToString()), "HANG_HOA", "MS_HIEN_THI", txtMS_HIEN_THI.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtMS_HIEN_THI.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(txtTEN_HH.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_HH",
                        (AddEdit ? "-1" : Id.ToString()), "HANG_HOA", "TEN_HH", txtTEN_HH.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_HH.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(txtTEN_HH_A.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_HH",
                        (AddEdit ? "-1" : Id.ToString()), "HANG_HOA", "TEN_HH_A", txtTEN_HH_A.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        txtTEN_HH_A.Focus();
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
