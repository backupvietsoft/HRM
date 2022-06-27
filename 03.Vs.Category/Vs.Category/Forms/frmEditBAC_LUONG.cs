using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmEditBAC_LUONG : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = 0;
        static Boolean AddEdit = true;  // true la add false la edit

        public frmEditBAC_LUONG(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditBAC_LUONG_Load(object sender, EventArgs e)
        {
            LoadDonVi();
            LoadNgachLuong();
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            Commons.OSystems.SetDateEditFormat(NGAY_QDDateEdit);
        }
        private void frmEditBAC_LUONG_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT ID_BL, ID_DV, ID_NL, NGAY_QD, TEN_BL, MUC_LUONG, " +
                    "PC_DH, THUONG_TC, THUONG_CV_CC, PC_SINH_HOAT, PC_KY_NANG, STT " +
                    "FROM BAC_LUONG WHERE ID_BL = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                ID_DVSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_DV"].ToString();
                ID_NLSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_NL"].ToString();
                NGAY_QDDateEdit.EditValue = Convert.ToDateTime(dtTmp.Rows[0]["NGAY_QD"].ToString());
                TEN_BLTextEdit.EditValue = dtTmp.Rows[0]["TEN_BL"].ToString();
                MUC_LUONGTextEdit.EditValue = dtTmp.Rows[0]["MUC_LUONG"].ToString();
                PC_DHTextEdit.EditValue = dtTmp.Rows[0]["PC_DH"].ToString();
                THUONG_TCTextEdit.EditValue = dtTmp.Rows[0]["THUONG_TC"].ToString();
                THUONG_CV_CCTextEdit.EditValue = dtTmp.Rows[0]["THUONG_CV_CC"].ToString();
                PC_SINH_HOATTextEdit.EditValue = dtTmp.Rows[0]["PC_SINH_HOAT"].ToString();
                PC_KY_NANGTextEdit.EditValue = dtTmp.Rows[0]["PC_KY_NANG"].ToString();
                txtSTT.EditValue = dtTmp.Rows[0]["STT"].ToString();
            }
            catch 
            {
            }

        }
        private void LoadTextNull()
        {
            try
            {
                TEN_BLTextEdit.EditValue = String.Empty;
                MUC_LUONGTextEdit.EditValue = 0;
                PC_DHTextEdit.EditValue = 0;
                THUONG_TCTextEdit.EditValue = 0;
                THUONG_CV_CCTextEdit.EditValue = 0;
                PC_SINH_HOATTextEdit.EditValue = 0;
                PC_KY_NANGTextEdit.EditValue = 0;
                txtSTT.EditValue = 1;
                TEN_BLTextEdit.Focus();
            }
            catch { }
        }
        private void LoadNgachLuong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNGACH_LUONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            ID_NLSearchLookUpEdit.Properties.DataSource = dt;
            ID_NLSearchLookUpEdit.Properties.ValueMember = "ID_NL";
            ID_NLSearchLookUpEdit.Properties.DisplayMember = "MS_NL";
            ID_NLSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {
                //SELECT DISTINCT T1.ID_DV,MSDV, T2.TEN_DV,T1.DIA_CHI,T1.DIEN_THOAI,T1.FAX,T1.SO_TAI_KHOAN,T1.TEN_NGAN_HANG ,T1.MAC_DINH
                ID_NLSearchLookUpEdit.Properties.View.Columns["ID_NL"].Visible = false;
                ID_NLSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;

                ID_NLSearchLookUpEdit.Properties.View.Columns["TEN_NL"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_NL");
                ID_NLSearchLookUpEdit.Properties.View.Columns["MS_NL"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "MS_NL");

                //ID_NLSearchLookUpEdit.Properties.View.Columns["TEN_NL"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
                ID_DVSearchLookUpEdit.Properties.View.Columns["PC_CN"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["STT_DV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_NGAN_HANG"].Visible = false;
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

                                Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateBAC_LUONG", (AddEdit ? -1 : Id).ToString(),
                                    ID_DVSearchLookUpEdit.EditValue, ID_NLSearchLookUpEdit.EditValue,
                                    NGAY_QDDateEdit.EditValue, TEN_BLTextEdit.EditValue,
                                    (string.IsNullOrEmpty(Convert.ToString(MUC_LUONGTextEdit.EditValue))) ? 0 : Convert.ToDouble(MUC_LUONGTextEdit.EditValue),
                                    (string.IsNullOrEmpty(Convert.ToString(PC_DHTextEdit.EditValue))) ? 0 : Convert.ToDouble(PC_DHTextEdit.EditValue),
                                    (string.IsNullOrEmpty(Convert.ToString(THUONG_TCTextEdit.EditValue))) ? 0 : Convert.ToDouble(THUONG_TCTextEdit.EditValue),
                                    (string.IsNullOrEmpty(Convert.ToString(THUONG_CV_CCTextEdit.EditValue))) ? 0 : Convert.ToDouble(THUONG_CV_CCTextEdit.EditValue),
                                    (string.IsNullOrEmpty(Convert.ToString(PC_SINH_HOATTextEdit.EditValue))) ? 0 : Convert.ToDouble(PC_SINH_HOATTextEdit.EditValue),
                                    (string.IsNullOrEmpty(Convert.ToString(PC_KY_NANGTextEdit.EditValue))) ? 0 : Convert.ToDouble(PC_KY_NANGTextEdit.EditValue),
                                    (txtSTT.Text == "") ? txtSTT.EditValue = null : txtSTT.EditValue
                                    ).ToString();
                            }
                            catch 
                            {

                                throw;
                            }

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
        private bool bKiemTrung()
        {
            try
            {

                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_BL",
                    (AddEdit ? "-1" : Id.ToString()), "BAC_LUONG", "ID_NL", ID_NLSearchLookUpEdit.EditValue.ToString(),
                    "TEN_BL", TEN_BLTextEdit.EditValue.ToString(), "ID_DV", ID_DVSearchLookUpEdit.EditValue.ToString()));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_BLTextEdit.Focus();
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