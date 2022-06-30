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
    public partial class frmEditLY_DO_THOI_VIEC : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLY_DO_THOI_VIEC(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLY_DO_THOI_VIEC_Load(object sender, EventArgs e)
        {
            LoadCombo();
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditLY_DO_THOI_VIEC_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LD_TV, TEN_LD_TV, TEN_LD_TV_A, TEN_LD_TV_H, HE_SO, ID_TT_HT, STT " +
                    "FROM LY_DO_THOI_VIEC WHERE ID_LD_TV = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LD_TVTextEdit.EditValue = dtTmp.Rows[0]["TEN_LD_TV"].ToString();
                TEN_LD_TV_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LD_TV_A"].ToString();
                TEN_LD_TV_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LD_TV_H"].ToString();
                HE_SOTextEdit.EditValue = dtTmp.Rows[0]["HE_SO"].ToString();
                cboID_TT_HT.EditValue = dtTmp.Rows[0]["ID_TT_HT"].ToString();
                txtSTT.EditValue = Convert.ToInt32(dtTmp.Rows[0]["STT"]);
            }
            catch (Exception EX)
            {
                //XtraMessageBox.Show(EX.Message.ToString());
            }

        }
        private void LoadTextNull()
        {
            try
            {
                TEN_LD_TVTextEdit.EditValue = String.Empty;
                TEN_LD_TV_ATextEdit.EditValue = String.Empty;
                TEN_LD_TV_HTextEdit.EditValue = String.Empty;
                HE_SOTextEdit.EditValue = 0;
                cboID_TT_HT.EditValue = -1;
                txtSTT.EditValue = 1;
                TEN_LD_TVTextEdit.Focus();
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
                            if(Convert.ToInt64(cboID_TT_HT.EditValue) < 0)
                            {
                                XtraMessageBox.Show(ItemForTEN_TT_HT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cboID_TT_HT.Focus();
                                return;
                            }
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLY_DO_THOI_VIEC", (AddEdit ? -1 : Id),
                                TEN_LD_TVTextEdit.EditValue, TEN_LD_TV_ATextEdit.EditValue, TEN_LD_TV_HTextEdit.EditValue, 
                                (HE_SOTextEdit.EditValue == null) ? 0 : HE_SOTextEdit.EditValue, Convert.ToInt64(cboID_TT_HT.EditValue), (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LD_TV",
                    (AddEdit ? "-1" : Id.ToString()), "LY_DO_THOI_VIEC", "TEN_LD_TV", TEN_LD_TVTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_LD_TVTextEdit.Focus();
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
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TT_HT, Commons.Modules.ObjSystems.DataTinHTrangHT(false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", true,true);
            }
            catch { }
        }
        
    }
}