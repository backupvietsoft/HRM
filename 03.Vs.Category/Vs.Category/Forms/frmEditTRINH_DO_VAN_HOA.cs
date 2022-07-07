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
    public partial class frmEditTRINH_DO_VAN_HOA : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditTRINH_DO_VAN_HOA(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditTRINH_DO_VAN_HOA_Load(object sender, EventArgs e)
        {
            LoadLoaiTD();
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT) FROM dbo.TRINH_DO_VAN_HOA";
                txtSTT.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }

        private void frmEditTRINH_DO_VAN_HOA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadLoaiTD()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_TRINH_DO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_LOAI_TDSearchLookUpEdit, dt, "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD");
            try
            {

            if (ID_LOAI_TDSearchLookUpEdit.Properties.View.Columns["ID_LOAI_TD"]!=null)    ID_LOAI_TDSearchLookUpEdit.Properties.View.Columns["ID_LOAI_TD"].Visible = false;
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_TDVH, TEN_TDVH, TEN_TDVH_A, TEN_TDVH_H, ID_LOAI_TD, STT " +
                    "FROM TRINH_DO_VAN_HOA WHERE ID_TDVH =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_TDVHTextEdit.EditValue = dtTmp.Rows[0]["TEN_TDVH"].ToString();
                TEN_TDVH_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_TDVH_A"].ToString();
                TEN_TDVH_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_TDVH_H"].ToString();
                ID_LOAI_TDSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LOAI_TD"];
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
                TEN_TDVHTextEdit.EditValue = String.Empty;
                TEN_TDVH_ATextEdit.EditValue = String.Empty;
                TEN_TDVH_HTextEdit.EditValue = String.Empty;
                txtSTT.EditValue = 1;
                TEN_TDVHTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTRINH_DO_VAN_HOA", (AddEdit ? -1 : Id),
                                TEN_TDVHTextEdit.EditValue, TEN_TDVH_ATextEdit.EditValue,
                                TEN_TDVH_HTextEdit.EditValue, ID_LOAI_TDSearchLookUpEdit.EditValue, txtSTT.EditValue == "" ? txtSTT.EditValue = null : txtSTT.EditValue).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TDVH",
                    (AddEdit ? "-1" : Id.ToString()), "TRINH_DO_VAN_HOA", "TEN_TDVH", TEN_TDVHTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_TDVHTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_TDVH_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TDVH",
                        (AddEdit ? "-1" : Id.ToString()), "TRINH_DO_VAN_HOA", "TEN_TDVH_A", TEN_TDVH_ATextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_TDVH_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_TDVH_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TDVH",
                        (AddEdit ? "-1" : Id.ToString()), "TRINH_DO_VAN_HOA", "TEN_TDVH_H", TEN_TDVH_HTextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_TDVH_HTextEdit.Focus();
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