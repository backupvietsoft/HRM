using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Payroll
{
    public partial class frmEditCHUYEN : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdTo = 0;
        Boolean bAddEditTo = true;  // true la add false la edit
        string TEN = "";

        public frmEditCHUYEN(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            iIdTo = iId;
            bAddEditTo = bAddEdit;
        }
        private void frmEditCHUYEN_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (!bAddEditTo) LoadText();
        }
        private void LoadCombobox()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(TOLookUpEdit, Commons.Modules.ObjSystems.DataToChuyen(false),"ID_TO", "TEN_TO",Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO"),true);
            }
            catch
            {
            }
        }
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT * FROM dbo.[CHUYEN] WHERE ID_CHUYEN = " + iIdTo.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;

            TOLookUpEdit.EditValue = dtTmp.Rows[0]["ID_TO"];
            TEN_CHUYENTextEdit.EditValue = dtTmp.Rows[0]["TEN_CHUYEN"];
            TEN_CHUYEN_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_CHUYEN_A"];
            TEN_CHUYEN_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_CHUYEN_H"];
            TEN = dtTmp.Rows[0]["TEN_CHUYEN"].ToString();
            STT_CHUYENTextEdit.EditValue = dtTmp.Rows[0]["MS_CHUYEN"];
            txtSTT.EditValue = dtTmp.Rows[0]["STT"].ToString();

        }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if(TOLookUpEdit.Text=="")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ChuaChonTo"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                TOLookUpEdit.Focus();
                                return;
                            }
                            if (bKiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateCHUYEN", (bAddEditTo ? -1 : iIdTo),
                                STT_CHUYENTextEdit.EditValue,
                                TEN_CHUYENTextEdit.EditValue,
                                TEN_CHUYEN_ATextEdit.EditValue,
                                TEN_CHUYEN_HTextEdit.EditValue,
                                TOLookUpEdit.EditValue,
                                (txtSTT.Text == "") ? txtSTT.EditValue = null : txtSTT.EditValue
                                ).ToString();
                            if (bAddEditTo)
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
                    case "huy":
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
         private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;
                string tenSql = "";
                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_CHUYEN",
                    (bAddEditTo ? "-1" : iIdTo.ToString()), "CHUYEN", "MS_CHUYEN", STT_CHUYENTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    STT_CHUYENTextEdit.Focus();
                    return true;
                }
                if (bAddEditTo || TEN != TEN_CHUYENTextEdit.EditValue.ToString())
                {

                    tenSql = "SELECT TEN_CHUYEN FROM CHUYEN WHERE TEN_CHUYEN = '" + TEN_CHUYENTextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_CHUYENTextEdit.EditValue)))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void frmEditCHUYEN_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadTextNull()
        {
            try
            {
                STT_CHUYENTextEdit.EditValue = String.Empty;
                TEN_CHUYENTextEdit.EditValue = String.Empty;
                TEN_CHUYEN_ATextEdit.EditValue = String.Empty;
                TEN_CHUYEN_HTextEdit.EditValue = String.Empty;
                TOLookUpEdit.EditValue = null;
                txtSTT.EditValue = null;
                STT_CHUYENTextEdit.Focus();
            }
            catch { }
        }
    }

}
