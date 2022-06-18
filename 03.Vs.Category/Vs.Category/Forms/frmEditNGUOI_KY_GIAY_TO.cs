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
    public partial class frmEditNGUOI_KY_GIAY_TO : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNGUOI_KY_GIAY_TO(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditNGUOI_KY_GIAY_TO_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAY_SINHDateEdit);
            Commons.OSystems.SetDateEditFormat(CAP_NGAYDateEdit);
        }

        private void frmEditNGUOI_KY_GIAY_TO_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NK, HO_TEN, CHUC_VU, CHUC_VU_A, CHUC_VU_H, QUOC_TICH, NGAY_SINH, " +
                    "SO_CMND, CAP_NGAY, NOI_CAP, DIA_CHI, STT " +
                    "FROM NGUOI_KY_GIAY_TO WHERE ID_NK = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                HO_TENTextEdit.EditValue = dtTmp.Rows[0]["HO_TEN"].ToString();
                CHUC_VUTextEdit.EditValue = dtTmp.Rows[0]["CHUC_VU"].ToString();
                CHUC_VU_ATextEdit.EditValue = dtTmp.Rows[0]["CHUC_VU_A"].ToString();
                txtCHUC_VU_H.EditValue = dtTmp.Rows[0]["CHUC_VU_H"].ToString();
                QUOC_TICHTextEdit.EditValue = dtTmp.Rows[0]["QUOC_TICH"].ToString();
    
                if (string.IsNullOrEmpty(dtTmp.Rows[0]["NGAY_SINH"].ToString()))
                {
                    NGAY_SINHDateEdit.EditValue = null;
                }
                else
                {
                    NGAY_SINHDateEdit.EditValue = Convert.ToDateTime(dtTmp.Rows[0]["NGAY_SINH"]).ToString("dd/MM/yyyy");
                }

                SO_CMNDTextEdit.EditValue = dtTmp.Rows[0]["SO_CMND"].ToString();

                if (string.IsNullOrEmpty(dtTmp.Rows[0]["CAP_NGAY"].ToString()))
                {
                    CAP_NGAYDateEdit.EditValue = null;
                }
                else
                {
                    CAP_NGAYDateEdit.EditValue = Convert.ToDateTime(dtTmp.Rows[0]["CAP_NGAY"]).ToString("dd/MM/yyyy");
                }
                NOI_CAPTextEdit.EditValue = dtTmp.Rows[0]["NOI_CAP"].ToString();
                DIA_CHITextEdit.EditValue = dtTmp.Rows[0]["DIA_CHI"].ToString();
            }
            catch 
            {
                
            }

        }
        private void LoadTextNull()
        {
            try
            {
                HO_TENTextEdit.EditValue = String.Empty;
                CHUC_VUTextEdit.EditValue = String.Empty;
                CHUC_VU_ATextEdit.EditValue = String.Empty;
                txtCHUC_VU_H.EditValue = String.Empty;
                QUOC_TICHTextEdit.EditValue = String.Empty;
                NGAY_SINHDateEdit.EditValue = String.Empty;
                SO_CMNDTextEdit.EditValue = String.Empty;
                CAP_NGAYDateEdit.EditValue = String.Empty;
                NOI_CAPTextEdit.EditValue = String.Empty;
                DIA_CHITextEdit.EditValue = String.Empty;
                HO_TENTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNGUOI_KY_GIAY_TO", (AddEdit ? -1 : Id),
                                HO_TENTextEdit.EditValue, CHUC_VUTextEdit.EditValue, CHUC_VU_ATextEdit.EditValue, txtCHUC_VU_H.EditValue,
                                QUOC_TICHTextEdit.EditValue, NGAY_SINHDateEdit.Text == "" ? null : NGAY_SINHDateEdit.EditValue,
                                SO_CMNDTextEdit.EditValue, CAP_NGAYDateEdit.Text == "" ? null : CAP_NGAYDateEdit.EditValue, NOI_CAPTextEdit.EditValue, 
                                DIA_CHITextEdit.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NK",
                    (AddEdit ? "-1" : Id.ToString()), "NGUOI_KY_GIAY_TO", "HO_TEN", HO_TENTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    HO_TENTextEdit.Focus();
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
        
    }
}