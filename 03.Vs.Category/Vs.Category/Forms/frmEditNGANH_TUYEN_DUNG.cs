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
    public partial class frmEditNGANH_TUYEN_DUNG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNGANH_TUYEN_DUNG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }


        #region even
        private void frmEditNGANH_TUYEN_DUNG_Load(object sender, EventArgs e)
        {
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNGANH_TUYEN_DUNG", (AddEdit ? -1 : Id),
                                TEN_NGANH_TDTextEdit.EditValue, TEN_NGANH_TD_ATextEdit.EditValue, TEN_NGANH_TD_HTextEdit.EditValue, GHI_CHUTextEdit.EditValue).ToString();
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

        private void frmEditNGANH_TUYEN_DUNG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        #endregion

        #region function

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NGANH_TD, TEN_NGANH_TD, TEN_NGANH_TD_A, TEN_NGANH_TD_H, GHI_CHU " +
                    "FROM NGANH_TUYEN_DUNG WHERE ID_NGANH_TD = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_NGANH_TDTextEdit.EditValue = dtTmp.Rows[0]["TEN_NGANH_TD"].ToString();
                TEN_NGANH_TD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_NGANH_TD_A"].ToString();
                TEN_NGANH_TD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_NGANH_TD_H"].ToString();
                GHI_CHUTextEdit.EditValue = dtTmp.Rows[0]["GHI_CHU"].ToString();

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
                TEN_NGANH_TDTextEdit.EditValue = String.Empty;
                TEN_NGANH_TD_ATextEdit.EditValue = String.Empty;
                TEN_NGANH_TD_HTextEdit.EditValue = String.Empty;
                GHI_CHUTextEdit.EditValue = String.Empty;
                TEN_NGANH_TDTextEdit.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NGANH_TD",
                    (AddEdit ? "-1" : Id.ToString()), "NGANH_TUYEN_DUNG", "TEN_NGANH_TD", TEN_NGANH_TDTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_NGANH_TDTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_NGANH_TD_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NGANH_TD",
                        (AddEdit ? "-1" : Id.ToString()), "NGANH_TUYEN_DUNG", "TEN_NGANH_TD_A", TEN_NGANH_TD_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NGANH_TD_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_NGANH_TD_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_NGANH_TD",
                        (AddEdit ? "-1" : Id.ToString()), "NGANH_TUYEN_DUNG", "TEN_NGANH_TD_H", TEN_NGANH_TD_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_NGANH_TD_HTextEdit.Focus();
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
