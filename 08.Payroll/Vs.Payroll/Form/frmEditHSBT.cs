using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditHSBT : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = -1;
        static Boolean AddEdit = true;  // true la add false la edit
        string hsbt = "";

        public frmEditHSBT(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditHSBT_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditHSBT_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "select * from HSBT where ID_BT  = " + Id;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_BAC_THOTextEdit.EditValue = dtTmp.Rows[0]["TEN_BAC_THO"];
                TEN_BAC_THO_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_BAC_THO_A"];
                TEN_BAC_THO_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_BAC_THO_H"];
                hsbt = dtTmp.Rows[0]["TEN_BAC_THO"].ToString();
                HSBTTextEdit.EditValue = dtTmp.Rows[0]["HE_SO_BAC_THO"].ToString();
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
                TEN_BAC_THOTextEdit.EditValue = String.Empty;
                TEN_BAC_THO_ATextEdit.EditValue = String.Empty;
                TEN_BAC_THO_HTextEdit.EditValue = String.Empty;
                HSBTTextEdit.EditValue = String.Empty;
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
                            if (KiemTrung()) return;

                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateHSBT", (AddEdit ? -1 : Id),
                                TEN_BAC_THOTextEdit.Text,
                                TEN_BAC_THO_ATextEdit.Text,
                                TEN_BAC_THO_HTextEdit.Text,
                                HSBTTextEdit.Text,
                                txtSTT.Text == "" ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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
        private Boolean KiemTrung()
        {
            try
            {
                string sSql = "";
                string tenSql = "";
                if (AddEdit || hsbt != TEN_BAC_THOTextEdit.EditValue.ToString())
                {
                    tenSql = "SELECT TEN_BAC_THO FROM HSBT WHERE TEN_BAC_THO = '" + TEN_BAC_THOTextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == (Convert.ToString((TEN_BAC_THOTextEdit.EditValue)) == "" ?  "-1" : Convert.ToString((TEN_BAC_THOTextEdit.EditValue))))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        return true;
                    }
                }

                if (AddEdit || hsbt != TEN_BAC_THO_ATextEdit.EditValue.ToString())
                {
                    tenSql = "SELECT TEN_BAC_THO_A FROM HSBT WHERE TEN_BAC_THO_A = '" + TEN_BAC_THO_ATextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == (Convert.ToString((TEN_BAC_THO_ATextEdit.EditValue)) == "" ?  "-1" : Convert.ToString((TEN_BAC_THO_ATextEdit.EditValue))))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        return true;
                    }
                }

                if (AddEdit || hsbt != TEN_BAC_THO_HTextEdit.EditValue.ToString())
                {
                    tenSql = "SELECT TEN_BAC_THO_H FROM HSBT WHERE TEN_BAC_THO_H = '" + TEN_BAC_THO_HTextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == (Convert.ToString((TEN_BAC_THO_HTextEdit.EditValue)) == "" ? "-1" : Convert.ToString((TEN_BAC_THO_HTextEdit.EditValue))))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

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