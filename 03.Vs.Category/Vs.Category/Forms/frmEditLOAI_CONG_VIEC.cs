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
    public partial class frmEditLOAI_CONG_VIEC : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLOAI_CONG_VIEC(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditLOAI_CONG_VIEC_Load(object sender, EventArgs e)
        {
            ItemForTEN_CV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNGACH_LUONG, Commons.Modules.ObjSystems.DataNgachLuong(false), "ID_NL", "TEN_NL", "TEN_NL", true, true);

            //ItemForTEN_XN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChucVu, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV");

                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChucVu, Commons.Modules.ObjSystems.DataXiNghiep(-1,false) , "ID_XN", "TEN_XN", "TEN_XN");
                ItemForTEN_CV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            

            LoadLoaiTO();
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_CVSearchLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV", true, true);
            if (!AddEdit)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT) FROM dbo.LOAI_CONG_VIEC";
                txtSTT.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
        }

        private void frmEditLOAI_CONG_VIEC_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadLoaiTO()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_TO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_LTSearchLookUpEdit, dt, "ID_LT", "TEN_LT", "TEN_LT", true, true);
            try
            {

                if (ID_LTSearchLookUpEdit.Properties.View.Columns["ID_LT"] != null) ID_LTSearchLookUpEdit.Properties.View.Columns["ID_LT"].Visible = false;
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
                string sSql = "SELECT * " +
                    "FROM LOAI_CONG_VIEC WHERE ID_LCV =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_LCVTextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV"].ToString();
                TEN_LCV_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV_A"].ToString();
                TEN_LCV_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_LCV_H"].ToString();
                DOC_HAICheckEdit.EditValue = Convert.ToBoolean(dtTmp.Rows[0]["DOC_HAI"]);
                PHEP_CTTextEdit.EditValue = dtTmp.Rows[0]["PHEP_CT"].ToString();
                ID_LTSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LT"];
                cboChucVu.EditValue = dtTmp.Rows[0]["ID_CV"].ToString() == "" ? -1 : Convert.ToInt64(dtTmp.Rows[0]["ID_CV"]);
                try
                {
                    txtSTT.EditValue = Convert.ToInt32(dtTmp.Rows[0]["STT"]);
                }
                catch { }
                txtCHUC_DANH.EditValue = dtTmp.Rows[0]["CHUC_DANH"];
                txtMO_TA_CV.EditValue = dtTmp.Rows[0]["MO_TA_CV_BHXH"];
                txtCHUC_DANH_A.EditValue = dtTmp.Rows[0]["CHUC_DANH_A"];
                txtMO_TA_CV_A.EditValue = dtTmp.Rows[0]["MO_TA_CV_BHXH_A"];
                cboNGACH_LUONG.EditValue = dtTmp.Rows[0]["ID_NL"].ToString() == "" ? -1 : Convert.ToInt64(dtTmp.Rows[0]["ID_NL"]); ;
            }
            catch
            {
            }
        }
        private void LoadTextNull()
        {
            try
            {
                TEN_LCVTextEdit.EditValue = String.Empty;
                TEN_LCV_ATextEdit.EditValue = String.Empty;
                TEN_LCV_HTextEdit.EditValue = String.Empty;
                DOC_HAICheckEdit.EditValue = false;
                PHEP_CTTextEdit.EditValue = 0;
                TEN_LCVTextEdit.Focus();
                cboChucVu.EditValue = -1;
                txtSTT.EditValue = 1;
                txtCHUC_DANH.Text = "";
                txtMO_TA_CV.Text = "";
                txtCHUC_DANH_A.Text = "";
                txtMO_TA_CV_A.Text = "";
                cboNGACH_LUONG.EditValue = -1;
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLOAI_CONG_VIEC", (AddEdit ? -1 : Id),
                                TEN_LCVTextEdit.EditValue, TEN_LCV_ATextEdit.EditValue,
                                TEN_LCV_HTextEdit.EditValue, DOC_HAICheckEdit.EditValue,
                                (PHEP_CTTextEdit.EditValue == null) ? 0 : PHEP_CTTextEdit.EditValue,
                                ID_LTSearchLookUpEdit.Text.Trim() == "" ? ID_LTSearchLookUpEdit.EditValue = null : ID_LTSearchLookUpEdit.EditValue,
                                Convert.ToInt64(cboChucVu.Text == "" ? cboChucVu.EditValue = null : cboChucVu.EditValue),
                                (txtSTT.Text == "") ? txtSTT.EditValue = null : txtSTT.EditValue,
                                txtCHUC_DANH.Text, txtCHUC_DANH_A.Text,
                                txtMO_TA_CV.Text, txtMO_TA_CV_A.Text , cboNGACH_LUONG.EditValue
                                ).ToString();
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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                    (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV", TEN_LCVTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TEN_LCVTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_LCV_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV_A", TEN_LCV_ATextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_LCV_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_LCV_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LCV",
                        (AddEdit ? "-1" : Id.ToString()), "LOAI_CONG_VIEC", "TEN_LCV_H", TEN_LCV_HTextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_LCV_HTextEdit.Focus();
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