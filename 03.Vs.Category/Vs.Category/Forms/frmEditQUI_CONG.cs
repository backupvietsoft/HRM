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
    public partial class frmEditQUI_CONG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditQUI_CONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditQUI_CONG_Load(object sender, EventArgs e)
        {
            if (!AddEdit)
            {
                LoadText();
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditQUI_CONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID, SO_GIO_TU, SO_GIO_DEN, SO_CONG, SO_GIO_QD FROM dbo.QUI_CONG WHERE ID = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                txtSoGioTu.EditValue = Convert.ToDouble(dtTmp.Rows[0]["SO_GIO_TU"]);
                txtSoGioDen.EditValue = Convert.ToDouble(dtTmp.Rows[0]["SO_GIO_DEN"]);
                txtSoCong.EditValue = Convert.ToDouble(dtTmp.Rows[0]["SO_CONG"]);
                txtSoGioQD.EditValue = Convert.ToDouble(dtTmp.Rows[0]["SO_GIO_QD"]);
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
                txtSoGioTu.EditValue = String.Empty;
                txtSoGioDen.EditValue = String.Empty;
                txtSoCong.EditValue = String.Empty;
                txtSoGioQD.EditValue = String.Empty;
                txtSoGioTu.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateQUI_CONG", (AddEdit ? -1 : Id),
                                txtSoGioTu.EditValue, txtSoGioDen.EditValue, txtSoCong.EditValue, txtSoGioQD.EditValue).ToString();
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