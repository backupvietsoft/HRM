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
    public partial class frmEditNOI_QUY_LAO_DONG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditNOI_QUY_LAO_DONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditNOI_QUY_LAO_DONG_Load(object sender, EventArgs e)
        {
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT) FROM dbo.NOI_QUY_LAO_DONG";
                txtSTT.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            //lblNoiDung.Name = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNoiDung");
        }
        private void frmEditNOI_QUY_LAO_DONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_NQLD, NOI_DUNG,NOI_DUNG_A, NOI_DUNG_H, STT FROM NOI_QUY_LAO_DONG WHERE ID_NQLD = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                txtNoiDung.EditValue = dtTmp.Rows[0]["NOI_DUNG"].ToString();
                txtNoiDung_A.EditValue = dtTmp.Rows[0]["NOI_DUNG_A"].ToString();
                txtNoiDung_H.EditValue = dtTmp.Rows[0]["NOI_DUNG_H"].ToString();
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
                txtNoiDung.EditValue = String.Empty;
                txtNoiDung.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNOI_QUY_LAO_DONG", (AddEdit ? -1 : Id),
                                txtNoiDung.EditValue, txtNoiDung_A.Text, txtNoiDung_H.Text ,(txtSTT.Text == "") ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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
    }
}