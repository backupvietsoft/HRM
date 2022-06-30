using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditVI_TRI_TUYEN_DUNG : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = 0;
        static Boolean AddEdit = true;  // true la add false la edit

        public frmEditVI_TRI_TUYEN_DUNG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditVI_TRI_TUYEN_DUNG_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_CV, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV", "", true);
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditVI_TRI_TUYEN_DUNG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "select * from VI_TRI_TUYEN_DUNG where ID_VTTD  = " + Id ;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                
                txtMS_VTTD.EditValue = dtTmp.Rows[0]["MS_VTTD"].ToString();
                txtVTTD.EditValue = dtTmp.Rows[0]["TEN_VTTD"].ToString();
                txtVTTD_A.EditValue = dtTmp.Rows[0]["TEN_VTTD_A"].ToString();
                txtVTTD_H.EditValue = dtTmp.Rows[0]["TEN_VTTD_H"].ToString();
                cboID_CV.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["ID_CV"].ToString()) ? -1 : Convert.ToInt64(dtTmp.Rows[0]["ID_CV"]);
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
                
                txtMS_VTTD.EditValue = String.Empty;
                txtVTTD.EditValue = String.Empty;
                txtVTTD_A.EditValue = String.Empty;
                txtVTTD_H.EditValue = String.Empty;
                cboID_CV.EditValue = -1;
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
                            if (!dxValidationProvider11.Validate()) return;
                            if (bKiemTrung()) return;
                            try
                            {
                                Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateVI_TRI_TUYEN_DUNG", (AddEdit ? -1 : Id), txtMS_VTTD.EditValue, txtVTTD.EditValue, txtVTTD_A.EditValue, txtVTTD_H.EditValue, cboID_CV.EditValue).ToString();

                                if (AddEdit)
                                {
                                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_ThemThanhCongBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        LoadTextNull();
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message.ToString());
                                throw;
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
                string sSql = "";
                if (AddEdit)
                {
                    sSql = "SELECT COUNT(*) FROM VI_TRI_TUYEN_DUNG WHERE MS_VTTD = '" + txtMS_VTTD.EditValue +"'";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void LoadCboID_CV()
        {
            
        }
    }
}