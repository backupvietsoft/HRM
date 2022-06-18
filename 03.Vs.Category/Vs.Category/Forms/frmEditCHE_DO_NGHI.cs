using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmEditCHE_DO_NGHI : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit

        public frmEditCHE_DO_NGHI(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        
        private void frmEditCHE_DO_NGHI_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditCHE_DO_NGHI_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_CHE_DO, TEN_CHE_DO, TEN_CHE_DO_A, TEN_CHE_DO_H, STT " +
                    "FROM CHE_DO_NGHI WHERE ID_CHE_DO = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_CHE_DOTextEdit.EditValue = dtTmp.Rows[0]["TEN_CHE_DO"].ToString();
                TEN_CHE_DO_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_CHE_DO_A"].ToString();
                TEN_CHE_DO_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_CHE_DO_H"].ToString();
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
                TEN_CHE_DOTextEdit.EditValue = String.Empty;
                TEN_CHE_DO_ATextEdit.EditValue = String.Empty;
                TEN_CHE_DO_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                TEN_CHE_DOTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateCHE_DO_NGHI", (AddEdit ? -1 : Id),
                                TEN_CHE_DOTextEdit.EditValue, TEN_CHE_DO_ATextEdit.EditValue, TEN_CHE_DO_HTextEdit.EditValue, (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_CHE_DO",
                    (AddEdit ? "-1" : Id.ToString()), "CHE_DO_NGHI", "TEN_CHE_DO", TEN_CHE_DOTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_CHE_DOTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_CHE_DO_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_CHE_DO",
                        (AddEdit ? "-1" : Id.ToString()), "CHE_DO_NGHI", "TEN_CHE_DO_A", TEN_CHE_DO_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_CHE_DO_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_CHE_DO_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_CHE_DO",
                        (AddEdit ? "-1" : Id.ToString()), "CHE_DO_NGHI", "TEN_CHE_DO_H", TEN_CHE_DO_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_CHE_DO_HTextEdit.Focus();
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