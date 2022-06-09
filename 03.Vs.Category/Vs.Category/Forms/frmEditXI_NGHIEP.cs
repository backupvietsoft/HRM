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
    public partial class frmEditXI_NGHIEP : DevExpress.XtraEditors.XtraForm
    {
        Int64 iId = -1;
        Boolean bAddEdit = true;  // true la add false la edit
        string MS = "";
        public frmEditXI_NGHIEP(Int64 iIdForm, Boolean bAddEditForm)
        {
            InitializeComponent();
            iId = iIdForm;
            bAddEdit = bAddEditForm;
        }

        private void frmEditXI_NGHIEP_Load(object sender, EventArgs e)
        {
            LoadDonVi();
            if (!bAddEdit) {
                LoadText();
               
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

        }

        private void frmEditXI_NGHIEP_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

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
                ID_DVSearchLookUpEdit.Properties.View.Columns["ID_DV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["MSDV"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["DIA_CHI"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["DIEN_THOAI"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["FAX"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["SO_TAI_KHOAN"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_NGAN_HANG"].Visible = false;
                ID_DVSearchLookUpEdit.Properties.View.Columns["MAC_DINH"].Visible = false;

                ID_DVSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;

                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_DV"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_DV");

                ID_DVSearchLookUpEdit.Properties.View.Columns["TEN_DV"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                if (bAddEdit)
                {
                    try
                    {
                        string sSql = "SELECT TOP 1 ID_DV FROM dbo.XI_NGHIEP WHERE ID_XN = " + iId.ToString();
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_DVSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);

                        sSql = "SELECT ISNULL(MAX(STT_XN),0) + 1 FROM dbo.XI_NGHIEP WHERE ID_DV = " + sSql.ToString();
                        sSql = (String)SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                        STT_XNTextEdit.EditValue = Convert.ToInt64(sSql);
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
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT ID_XN, ID_DV, MS_XN, TEN_XN, TEN_XN_A, TEN_XN_H, STT_XN FROM dbo.XI_NGHIEP WHERE ID_XN =  " + iId.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;

            ID_DVSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_DV"];
            MS_XNTextEdit.EditValue = dtTmp.Rows[0]["MS_XN"];
            MS = dtTmp.Rows[0]["MS_XN"].ToString();
            TEN_XNTextEdit.EditValue = dtTmp.Rows[0]["TEN_XN"];
            TEN_XN_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_XN_A"];
            TEN_XN_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_XN_H"];
            STT_XNTextEdit.EditValue = dtTmp.Rows[0]["STT_XN"];

        }

        private void LoadTextNull()
        {
            try
            {
                ID_DVSearchLookUpEdit.EditValue = String.Empty;
                MS_XNTextEdit.EditValue = String.Empty;
                TEN_XNTextEdit.EditValue = String.Empty;
                TEN_XN_ATextEdit.EditValue = String.Empty;
                TEN_XN_HTextEdit.EditValue = String.Empty;
                STT_XNTextEdit.EditValue = String.Empty;
                MS_XNTextEdit.Focus();
            }
            catch { }
        }

        private void btnWDUI_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (KiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateXiNghiep", (bAddEdit ? -1 : iId), ID_DVSearchLookUpEdit.EditValue, MS_XNTextEdit.EditValue, TEN_XNTextEdit.EditValue, TEN_XN_ATextEdit.EditValue, TEN_XN_HTextEdit.EditValue, STT_XNTextEdit.EditValue).ToString();
                            if (bAddEdit)
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
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private bool KiemTrung()
        {
            try
            {
                string sSql = "";
                string tenSql = "";
                if (bAddEdit || MS != MS_XNTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM XI_NGHIEP WHERE MS_XN = '" + MS_XNTextEdit.EditValue + "'";
                    tenSql = "SELECT TEN_XN FROM XI_NGHIEP WHERE TEN_XN = N'" + TEN_XNTextEdit.EditValue + "'";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        MS_XNTextEdit.Focus();
                        return true;
                    }
                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_XNTextEdit.EditValue)))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_XNTextEdit.Focus();
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