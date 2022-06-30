using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditKINH_NGHIEM_LV : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = 0;
        static Boolean AddEdit = true;  // true la add false la edit

        public frmEditKINH_NGHIEM_LV(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditKINH_NGHIEM_LV_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditKINH_NGHIEM_LV_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "select * from KINH_NGHIEM_LV where ID_KNLV  = " + Id ;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                
                txtMS_KNLV.EditValue = dtTmp.Rows[0]["MS_KNLV"].ToString();
                txtKNLV.EditValue = dtTmp.Rows[0]["TEN_KNLV"].ToString();
                txtKNLV_A.EditValue = dtTmp.Rows[0]["TEN_KNLV_A"].ToString();
                txtKNLV_H.EditValue = dtTmp.Rows[0]["TEN_KNLV_H"].ToString();
                txtGHI_CHU.EditValue = dtTmp.Rows[0]["GHI_CHU"].ToString();
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
                txtMS_KNLV.EditValue = String.Empty;
                txtKNLV.EditValue = String.Empty;
                txtKNLV_A.EditValue = String.Empty;
                txtKNLV_H.EditValue = String.Empty;
                txtGHI_CHU.EditValue = String.Empty;
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
                                Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateKINH_NGHIEM_LV", (AddEdit ? -1 : Id), txtMS_KNLV.EditValue, txtKNLV.EditValue, txtKNLV_A.EditValue, txtKNLV_H.EditValue, txtGHI_CHU.EditValue).ToString();
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
                    sSql = "SELECT COUNT(*) FROM KINH_NGHIEM_LV WHERE MS_KNLV = '" + txtMS_KNLV.EditValue +"'";
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
    }
}