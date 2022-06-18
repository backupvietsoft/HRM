using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmEditNGAY_NGHI_LE : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = 0;
        static Boolean AddEdit = true;  // true la add false la edit

        public frmEditNGAY_NGHI_LE(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditNGAY_NGHI_LE_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAYDateEdit);
        }
        private void frmEditNGAY_NGHI_LE_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NNL,NGAY, LY_DO, STT " +
                    "FROM NGAY_NGHI_LE WHERE ID_NNL = " + Id ;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                NGAYDateEdit.EditValue = dtTmp.Rows[0]["NGAY"];
                LY_DOTextEdit.EditValue = dtTmp.Rows[0]["LY_DO"].ToString();
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
                NGAYDateEdit.EditValue = DateTime.Now;
                txtSTT.EditValue = 1;
                NGAYDateEdit.Focus();
                LY_DOTextEdit.EditValue = String.Empty;
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
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateNGAY_NGHI_LE", (AddEdit ? 1 : 0), NGAYDateEdit.EditValue, LY_DOTextEdit.EditValue.ToString(), (txtSTT.EditValue == "") ? txtSTT.EditValue = null : txtSTT.EditValue));

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
                string sSql = "";
                if (AddEdit)
                {
                    sSql = "SELECT COUNT(*) FROM NGAY_NGHI_LE WHERE CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(NGAYDateEdit.EditValue).ToString("yyyyMMdd") +"'";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_NgayTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        NGAYDateEdit.Focus();
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