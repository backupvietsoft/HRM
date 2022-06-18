using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditDON_GIA_GIAY : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = 0;
        static Boolean AddEdit = true;  // true la add false la edit

        public frmEditDON_GIA_GIAY(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditDonGiaGiay_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAY_QDDateEdit);
        }
        private void frmEditDonGiaGiay_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT * FROM DON_GIA_GIAY WHERE ID_DGG  = " + Id ;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                NGAY_QDDateEdit.EditValue = dtTmp.Rows[0]["NGAY_QD"];
                HS_DG_GIAYTextEdit.EditValue = dtTmp.Rows[0]["HS_DG_GIAY"].ToString();
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
                NGAY_QDDateEdit.EditValue = DateTime.Now;
                NGAY_QDDateEdit.Focus();
                txtSTT.EditValue = 1;
                HS_DG_GIAYTextEdit.EditValue = String.Empty;
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
                            try
                            {
                                DataTable dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateDON_GIA_GIAY", (AddEdit ? 1 : 0), NGAY_QDDateEdit.EditValue, HS_DG_GIAYTextEdit.EditValue.ToString(), (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue));

                                if (AddEdit)
                                {
                                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
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
        private bool bKiemTrung()
        {
            try
            {
                string sSql = "";
                if (AddEdit)
                {
                    sSql = "SELECT COUNT(*) FROM DON_GIA_GIAY WHERE CONVERT(NVARCHAR,NGAY_QD,112) = '" + Convert.ToDateTime(NGAY_QDDateEdit.EditValue).ToString("yyyyMMdd") +"'";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_NgayTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        NGAY_QDDateEdit.Focus();
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