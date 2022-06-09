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
    public partial class frmEditLY_DO_VANG : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLY_DO_VANG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLY_DO_VANG_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadCheDoNghi();
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditLY_DO_VANG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadCheDoNghi()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCHE_DO_NGHI", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            ID_CHE_DOSearchLookUpEdit.Properties.DataSource = dt;
            ID_CHE_DOSearchLookUpEdit.Properties.ValueMember = "ID_CHE_DO";
            ID_CHE_DOSearchLookUpEdit.Properties.DisplayMember = "TEN_CHE_DO";
            ID_CHE_DOSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                ID_CHE_DOSearchLookUpEdit.Properties.View.Columns["ID_CHE_DO"].Visible = false;
                ID_CHE_DOSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
                ID_CHE_DOSearchLookUpEdit.Properties.View.Columns["TEN_CHE_DO"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_CHE_DO");
                ID_CHE_DOSearchLookUpEdit.Properties.View.Columns["TEN_CHE_DO"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
                string sSql = "SELECT ID_LDV, MS_LDV, TEN_LDV, TEN_LDV_A, TEN_LDV_H, ID_CHE_DO, " +
                    "PHEP, PHAN_TRAM_TRO_CAP, TINH_BHXH, KY_HIEU, TINH_LUONG, STT_LDV, ID_TT_HT " +
                    "FROM LY_DO_VANG WHERE ID_LDV =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                MS_LDVTextEdit.EditValue = dtTmp.Rows[0]["MS_LDV"].ToString();
                TEN_LDVTextEdit.EditValue = dtTmp.Rows[0]["TEN_LDV"].ToString();
                TEN_LDV_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LDV_A"].ToString();
                TEN_LDV_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LDV_H"].ToString();
                PHEPCheckEdit.EditValue = dtTmp.Rows[0]["PHEP"];
                PHAN_TRAM_TRO_CAPTextEdit.EditValue = dtTmp.Rows[0]["PHAN_TRAM_TRO_CAP"].ToString();
                TINH_BHXHCheckEdit.EditValue = dtTmp.Rows[0]["TINH_BHXH"];
                KY_HIEUTextEdit.EditValue = dtTmp.Rows[0]["KY_HIEU"].ToString();
                TINH_LUONGCheckEdit.EditValue = dtTmp.Rows[0]["TINH_LUONG"];
                STT_LDVTextEdit.EditValue = dtTmp.Rows[0]["STT_LDV"].ToString();
                ID_CHE_DOSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_CHE_DO"];
                cboID_TT_HT.EditValue = dtTmp.Rows[0]["ID_TT_HT"];
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
                MS_LDVTextEdit.EditValue = String.Empty;
                TEN_LDVTextEdit.EditValue = String.Empty;
                TEN_LDV_ATextEdit.EditValue = String.Empty;
                TEN_LDV_HTextEdit.EditValue = String.Empty;
                PHAN_TRAM_TRO_CAPTextEdit.EditValue = 0;
                KY_HIEUTextEdit.EditValue = String.Empty;
                STT_LDVTextEdit.EditValue = String.Empty;
                PHEPCheckEdit.EditValue = false;
                TINH_BHXHCheckEdit.EditValue = false;
                TINH_LUONGCheckEdit.EditValue = false;
                cboID_TT_HT.EditValue = -1;
                MS_LDVTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLY_DO_VANG", (AddEdit ? -1 : Id),
                                MS_LDVTextEdit.EditValue,
                                TEN_LDVTextEdit.EditValue,
                                TEN_LDV_ATextEdit.EditValue,
                                TEN_LDV_HTextEdit.EditValue,
                                ID_CHE_DOSearchLookUpEdit.EditValue,
                                PHEPCheckEdit.EditValue,
                                (PHAN_TRAM_TRO_CAPTextEdit.EditValue == null) ? 0 : PHAN_TRAM_TRO_CAPTextEdit.EditValue,
                                TINH_BHXHCheckEdit.EditValue,
                                KY_HIEUTextEdit.EditValue,
                                TINH_LUONGCheckEdit.EditValue,
                                (STT_LDVTextEdit.EditValue == null) ? 0 : STT_LDVTextEdit.EditValue, Convert.ToInt64(cboID_TT_HT.EditValue)).ToString();
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
        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LDV",
                    (AddEdit ? "-1" : Id.ToString()), "LY_DO_VANG", "TEN_LDV", TEN_LDVTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_LDVTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_LDV_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LDV",
                        (AddEdit ? "-1" : Id.ToString()), "LY_DO_VANG", "TEN_LDV_A", TEN_LDV_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LDV_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_LDV_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LDV",
                        (AddEdit ? "-1" : Id.ToString()), "LY_DO_VANG", "TEN_LDV_H", TEN_LDV_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_LDV_HTextEdit.Focus();
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

        private void LoadCombo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TT_HT, Commons.Modules.ObjSystems.DataTinHTrangHT(false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", true, true);
            }
            catch { }
        }
    }
}