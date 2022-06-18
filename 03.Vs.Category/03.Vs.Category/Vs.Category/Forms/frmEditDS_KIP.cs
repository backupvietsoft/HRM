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
    public partial class frmEditDS_KIP : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditDS_KIP(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditDS_KIP_Load(object sender, EventArgs e)
        {
           if (!AddEdit) LoadText();
           Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditDS_KIP_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {

            try
            {
                string sSql = "SELECT ID_KIP, TEN_KIP, TEN_KIP_A, TEN_KIP_B " +
                    "FROM DS_KIP WHERE ID_KIP = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_KIPTextEdit.EditValue = dtTmp.Rows[0]["TEN_KIP"].ToString();
                TEN_KIP_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_KIP_A"].ToString();
                TEN_KIP_BTextEdit.EditValue = dtTmp.Rows[0]["TEN_KIP_B"].ToString();

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
                TEN_KIPTextEdit.EditValue = String.Empty;
                TEN_KIP_ATextEdit.EditValue = String.Empty;
                TEN_KIP_BTextEdit.EditValue = String.Empty;
            }
            catch { }
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateDS_KIP", (AddEdit ? -1 : Id),
                                TEN_KIPTextEdit.EditValue, TEN_KIP_ATextEdit.EditValue, TEN_KIP_BTextEdit.EditValue ).ToString();

                            if (AddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgThemThanhCongBanMuonThemTiep"), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            };
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
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KIP",
                    (AddEdit ? "-1" : Id.ToString()), "DS_KIP", "TEN_KIP", TEN_KIPTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "@msgTEN_KIPNayDaTonTai"));
                    TEN_KIPTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_KIP_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KIP",
                        (AddEdit ? "-1" : Id.ToString()), "DS_KIP", "TEN_KIP_A", TEN_KIP_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTEN_KIP_ANayDaTonTai"));
                        TEN_KIP_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_KIP_BTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KIP",
                        (AddEdit ? "-1" : Id.ToString()), "DS_KIP", "TEN_KIP_B", TEN_KIP_BTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTEN_KIP_BNayDaTonTai"));
                        TEN_KIP_BTextEdit.Focus();
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