using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmEditNOI_DUNG_THUONG_KHAC_LUONG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit

        public frmEditNOI_DUNG_THUONG_KHAC_LUONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        
        private void frmEditNOI_DUNG_THUONG_KHAC_LUONG_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditNOI_DUNG_THUONG_KHAC_LUONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NDTKL, TEN_THUONG, TEN_THUONG_A, TEN_THUONG_H, STT " +
                    "FROM NOI_DUNG_THUONG_KHAC_LUONG WHERE ID_NDTKL = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_TKLTextEdit.EditValue = dtTmp.Rows[0]["TEN_THUONG"].ToString();
                TEN_TKL_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_THUONG_A"].ToString();
                TEN_TKL_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_THUONG_H"].ToString();
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
                TEN_TKLTextEdit.EditValue = String.Empty;
                TEN_TKL_ATextEdit.EditValue = String.Empty;
                TEN_TKL_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                TEN_TKLTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNOI_DUNG_THUONG_KHAC_LUONG", (AddEdit ? -1 : Id),
                                TEN_TKLTextEdit.EditValue, TEN_TKL_ATextEdit.EditValue, TEN_TKL_HTextEdit.EditValue, (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NDTKL",
                    (AddEdit ? "-1" : Id.ToString()), "NOI_DUNG_THUONG_KHAC_LUONG", "TEN_THUONG", TEN_TKLTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_TKLTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_TKL_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NDTKL",
                        (AddEdit ? "-1" : Id.ToString()), "NOI_DUNG_THUONG_KHAC_LUONG", "TEN_THUONG_A", TEN_TKL_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_TKL_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_TKL_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NDTKL",
                        (AddEdit ? "-1" : Id.ToString()), "NOI_DUNG_THUONG_KHAC_LUONG", "TEN_THUONG_H", TEN_TKL_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_TKL_HTextEdit.Focus();
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