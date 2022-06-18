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

namespace Vs.Category
{
    public partial class frmEditNGUON_TUYEN_DUNG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNGUON_TUYEN_DUNG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        #region even

        private void frmEditNGUON_TUYEN_DUNG_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditNGUON_TUYEN_DUNG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNGUON_TUYEN_DUNG", (AddEdit ? -1 : Id),
                                TEN_NTDTextEdit.EditValue, TEN_NTD_ATextEdit.EditValue, TEN_NTD_HTextEdit.EditValue, GHI_CHUTextEdit.EditValue).ToString();
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
                string sSql = "SELECT ID_NTD, TEN_NTD, TEN_NTD_A, TEN_NTD_H, GHI_CHU " +
                    "FROM NGUON_TUYEN_DUNG WHERE ID_NTD = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_NTDTextEdit.EditValue = dtTmp.Rows[0]["TEN_NTD"].ToString();
                TEN_NTD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_NTD_A"].ToString();
                TEN_NTD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_NTD_H"].ToString();
                GHI_CHUTextEdit.EditValue = dtTmp.Rows[0]["GHI_CHU"].ToString();

            }
            catch(Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }

        private void LoadTextNull()
        {
            try
            {
                TEN_NTDTextEdit.EditValue = String.Empty;
                TEN_NTD_ATextEdit.EditValue = String.Empty;
                TEN_NTD_HTextEdit.EditValue = String.Empty;
                GHI_CHUTextEdit.EditValue = String.Empty;
                TEN_NTDTextEdit.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NTD",
                    (AddEdit ? "-1" : Id.ToString()), "NGUON_TUYEN_DUNG", "TEN_NTD", TEN_NTDTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_NTDTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_NTD_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NTD",
                        (AddEdit ? "-1" : Id.ToString()), "NGUON_TUYEN_DUNG", "TEN_NTD_A", TEN_NTD_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NTD_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NTD_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NTD",
                        (AddEdit ? "-1" : Id.ToString()), "NGUON_TUYEN_DUNG", "TEN_NTD_H", TEN_NTD_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NTD_HTextEdit.Focus();
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
