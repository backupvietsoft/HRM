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
    public partial class frmEditLOAI_HINH_CONG_VIEC : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;         //true là thêm , fales là sửa
        public frmEditLOAI_HINH_CONG_VIEC(Int64 iID, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iID;
            AddEdit = bAddEdit;
        }

        #region even
        private void frmEditLOAI_HINH_CONG_VIEC_Load(object sender, EventArgs e)
        {

            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1,btnALL);
        }

        private void frmEditLOAI_HINH_CONG_VIEC_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_HINH_CONG_VIEC", (AddEdit ? -1 : Id),
                                TEN_LHCVTextEdit.EditValue, TEN_LHCV_ATextEdit.EditValue, TEN_LHCV_HTextEdit.EditValue, GHI_CHUTextEdit.EditValue).ToString();
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

        #endregion

        #region function

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_LHCV, TEN_LHCV, TEN_LHCV_A, TEN_LHCV_H, GHI_CHU " +
                   "FROM LOAI_HINH_CONG_VIEC WHERE ID_LHCV = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LHCVTextEdit.EditValue = dtTmp.Rows[0]["TEN_LHCV"].ToString();
                TEN_LHCV_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LHCV_A"].ToString();
                TEN_LHCV_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LHCV_H"].ToString();
                GHI_CHUTextEdit.EditValue = dtTmp.Rows[0]["GHI_CHU"].ToString();
            }
            catch(Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }

        private void LoadTextNull()
        {
            try
            {
                TEN_LHCVTextEdit.EditValue = String.Empty;
                TEN_LHCV_ATextEdit.EditValue = String.Empty;
                TEN_LHCV_HTextEdit.EditValue = String.Empty;
                GHI_CHUTextEdit.EditValue = String.Empty;
                TEN_LHCVTextEdit.Focus();
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHCV",
                    (AddEdit ? "-1" : Id.ToString()), "LOAI_HINH_CONG_VIEC", "TEN_LHCV", TEN_LHCVTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_LHCVTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_LHCV_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_HINH_CONG_VIEC", "TEN_LHCV_A", TEN_LHCV_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LHCV_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_LHCV_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LHCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_HINH_CONG_VIEC", "TEN_LHCV_H", TEN_LHCV_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LHCV_HTextEdit.Focus();
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
