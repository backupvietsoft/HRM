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
    public partial class frmEditLUONG_TOI_THIEU : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        public frmEditLUONG_TOI_THIEU(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }
        private void frmEditLUONG_TOI_THIEU_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "SELECT T1.ID_LTT, T1.NGAY_QD, T1.ID_DV, T1.LUONG_TOI_THIEU, T1.LUONG_TOI_THIEU_NN, " +
                    "T1.BHXH_CN, T1.MUC_GIAM_TRU, MUC_TN_TINH_THUE ,T1.BHYT_CN, T1.BHTN_CN, T1.BHXH_CT, T1.BHYT_CT, T1.BHTN_CT " +
                    "FROM LUONG_TOI_THIEU T1 " +
                    "WHERE T1.ID_LTT = " + Id.ToString(); 
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                NGAY_QDDateEdit.EditValue = Convert.ToDateTime(dtTmp.Rows[0]["NGAY_QD"].ToString());
                ID_DVSearchLookUpEdit.EditValue = Convert.ToInt64(dtTmp.Rows[0]["ID_DV"]);
                LUONG_TOI_THIEUTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["LUONG_TOI_THIEU"].ToString()) ? 0 : Convert.ToDecimal(dtTmp.Rows[0]["LUONG_TOI_THIEU"]);
                LUONG_TOI_THIEU_NNTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["LUONG_TOI_THIEU_NN"].ToString()) ? 0 : Convert.ToDecimal(dtTmp.Rows[0]["LUONG_TOI_THIEU_NN"]);
                txtMUC_GIAM_TRU.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["MUC_GIAM_TRU"].ToString()) ? 0 : Convert.ToDecimal(dtTmp.Rows[0]["MUC_GIAM_TRU"]);
                txtTN_TINH_THUE.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["MUC_TN_TINH_THUE"].ToString()) ? 0 : Convert.ToDecimal(dtTmp.Rows[0]["MUC_TN_TINH_THUE"]);
                BHXH_CNTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHXH_CN"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHXH_CN"]);
                BHYT_CNTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHYT_CN"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHYT_CN"]);
                BHTN_CNTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHTN_CN"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHTN_CN"]);
                BHXH_CTTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHXH_CT"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHXH_CT"]);
                BHYT_CTTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHYT_CT"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHYT_CT"]);
                BHTN_CTTextEdit.EditValue = string.IsNullOrEmpty(dtTmp.Rows[0]["BHTN_CT"].ToString()) ? 0 : Convert.ToDouble(dtTmp.Rows[0]["BHTN_CT"]);
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }

        }
        private void LoadDonVi()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            ID_DVSearchLookUpEdit.Properties.DataSource = dt;
            ID_DVSearchLookUpEdit.Properties.ValueMember = "ID_DV";
            ID_DVSearchLookUpEdit.Properties.DisplayMember = "TEN_DV";
            ID_DVSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {
                //SELECT DISTINCT T1.ID_DV,MSDV, T2.TEN_DV,T1.DIA_CHI,T1.DIEN_THOAI,T1.FAX,T1.SO_TAI_KHOAN,T1.TEN_NGAN_HANG ,T1.MAC_DINH
                ID_DVSearchLookUpEdit.Properties.View.Columns["ID_DV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["MSDV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["DIA_CHI"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["DIEN_THOAI"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["FAX"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["SO_TAI_KHOAN"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_NGAN_HANG"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["PC_CN"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["STT_DV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["MAC_DINH"].Visible = false;

                ID_DVSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;

                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_DV"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_DV");

                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_DV"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                if (AddEdit)
                {
                    try
                    {
                        string sSql = "SELECT TOP 1 ID_DV FROM dbo.DON_VI WHERE MAC_DINH = 1";
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_DVSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);
                    }
                    catch
                    {
                        if (dt.Rows.Count > 0) ID_DVSearchLookUpEdit.EditValue = dt.Rows[0][0];
                    }
                }


            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());

            }
        }
        private void frmEditLUONG_TOI_THIEU_Load(object sender, EventArgs e)
        {
            LoadDonVi();
            if (!AddEdit)
            {
                LoadText();
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAY_QDDateEdit);
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLUONG_TOI_THIEU", (AddEdit ? -1 : Id),
                                NGAY_QDDateEdit.EditValue, ID_DVSearchLookUpEdit.EditValue, 
                                (string.IsNullOrEmpty(LUONG_TOI_THIEUTextEdit.Text)) ? 0 : LUONG_TOI_THIEUTextEdit.EditValue,
                                (string.IsNullOrEmpty(LUONG_TOI_THIEU_NNTextEdit.Text)) ? 0 : LUONG_TOI_THIEU_NNTextEdit.EditValue,
                                (string.IsNullOrEmpty(txtMUC_GIAM_TRU.Text)) ? 0 : txtMUC_GIAM_TRU.EditValue,
                                (string.IsNullOrEmpty(txtTN_TINH_THUE.Text)) ? 0 : txtTN_TINH_THUE.EditValue,
                                (string.IsNullOrEmpty(BHXH_CNTextEdit.Text)) ? 0 : BHXH_CNTextEdit.EditValue,
                                (string.IsNullOrEmpty(BHYT_CNTextEdit.Text)) ? 0 : BHYT_CNTextEdit.EditValue,
                                (string.IsNullOrEmpty(BHTN_CNTextEdit.Text)) ? 0 : BHTN_CNTextEdit.EditValue,
                                (string.IsNullOrEmpty(BHXH_CTTextEdit.Text)) ? 0 : BHXH_CTTextEdit.EditValue,                              
                                (string.IsNullOrEmpty(BHYT_CTTextEdit.Text)) ? 0 : BHYT_CTTextEdit.EditValue,
                                (string.IsNullOrEmpty(BHTN_CTTextEdit.Text)) ? 0 : BHTN_CTTextEdit.EditValue
                                ).ToString();

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

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_LTT",
                    (AddEdit ? "-1" : Id.ToString()), "LUONG_TOI_THIEU", "NGAY_QD",  NGAY_QDDateEdit.DateTime.ToString("yyyy-MM-dd"),
                    "ID_DV", ID_DVSearchLookUpEdit.EditValue.ToString(),"",""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_NgayTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NGAY_QDDateEdit.Focus();
                    return true;
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