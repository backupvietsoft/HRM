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
    public partial class frmEditNGACH_LUONG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNGACH_LUONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditNGACH_LUONG_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditNGACH_LUONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NL, MS_NL, TEN_NL, TEN_NL_A, TEN_NL_H, STT FROM NGACH_LUONG WHERE ID_NL = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                MS_NLTextEdit.EditValue = dtTmp.Rows[0]["MS_NL"].ToString();
                TEN_NLTextEdit.EditValue = dtTmp.Rows[0]["TEN_NL"].ToString();
                TEN_NL_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_NL_A"].ToString();
                TEN_NL_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_NL_H"].ToString();
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
                MS_NLTextEdit.EditValue = String.Empty;
                TEN_NLTextEdit.EditValue = String.Empty;
                TEN_NL_ATextEdit.EditValue = String.Empty;
                TEN_NL_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                MS_NLTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNGACH_LUONG", (AddEdit ? -1 : Id),
                                MS_NLTextEdit.EditValue, TEN_NLTextEdit.EditValue, TEN_NL_ATextEdit.EditValue, TEN_NL_HTextEdit.EditValue, (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NL",
                    (AddEdit ? "-1" : Id.ToString()), "NGACH_LUONG", "MS_NL", MS_NLTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    MS_NLTextEdit.Focus();
                    return true;
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NLTextEdit.Text.ToString()))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NL", 
                        (AddEdit ? "-1" : Id.ToString()), "NGACH_LUONG", "TEN_NL", TEN_NLTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NLTextEdit.Focus();
                        return true;
                    }
                }
                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NL_ATextEdit.Text.ToString()))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NL",
                        (AddEdit ? "-1" : Id.ToString()), "NGACH_LUONG", "TEN_NL_A", TEN_NL_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NL_ATextEdit.Focus();
                        return true;
                    }
                }
                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NL_HTextEdit.Text.ToString()))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NL",
                        (AddEdit ? "-1" : Id.ToString()), "NGACH_LUONG", "TEN_NL_H", TEN_NL_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NL_HTextEdit.Focus();
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