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
    public partial class frmEditTINH_TRANG_HD : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditTINH_TRANG_HD(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        
        private void frmEditTINH_TRANG_HD_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditTINH_TRANG_HD_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_TT_HD, TEN_TT_HD, TEN_TT_HD_A, TEN_TT_HD_H, STT " +
                    "FROM TINH_TRANG_HD WHERE ID_TT_HD = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_TT_HDTextEdit.EditValue = dtTmp.Rows[0]["TEN_TT_HD"].ToString();
                TEN_TT_HD_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_TT_HD_A"].ToString();
                TEN_TT_HD_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_TT_HD_H"].ToString();
                txtSTT.EditValue = Convert.ToInt32(dtTmp.Rows[0]["STT"]);
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
                TEN_TT_HDTextEdit.EditValue = String.Empty;
                TEN_TT_HD_ATextEdit.EditValue = String.Empty;
                TEN_TT_HD_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                TEN_TT_HDTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTINH_TRANG_HD", (AddEdit ? -1 : Id),
                                TEN_TT_HDTextEdit.EditValue, TEN_TT_HD_ATextEdit.EditValue, TEN_TT_HD_HTextEdit.EditValue, txtSTT.Text == "" ?  null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TT_HD",
                    (AddEdit ? "-1" : Id.ToString()), "TINH_TRANG_HD", "TEN_TT_HD", TEN_TT_HDTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_TT_HDTextEdit.Focus();
                    return true;
                }

                iKiem = 0;
                
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