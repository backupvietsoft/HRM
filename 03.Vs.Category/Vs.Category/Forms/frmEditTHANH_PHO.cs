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
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Category
{
    public partial class frmEditTHANH_PHO : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = -1;
        Boolean AddEdit = true;  // true la add false la edit
        string Ma = "";
        public frmEditTHANH_PHO(Int64 iIdForm, Boolean bAddEditForm)
        {
            InitializeComponent();
            Id = iIdForm;
            AddEdit = bAddEditForm;
        }

        private void frmEditTHANH_PHO_Load(object sender, EventArgs e)
        {
            LoadQuocGia();
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

        }

        private void frmEditTHANH_PHO_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadQuocGia()
        {

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQUOC_GIA", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_QGSearchLookUpEdit, dt, "ID_QG", "TEN_QG", "TEN_QG");

        //    ID_QGSearchLookUpEdit.Properties.DataSource = dt;
       ////     ID_QGSearchLookUpEdit.Properties.ValueMember = "ID_QG";
         //   ID_QGSearchLookUpEdit.Properties.DisplayMember = "TEN_QG";
        //    ID_QGSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                ID_QGSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
            //    ID_QGSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
            //    ID_QGSearchLookUpEdit.Properties.View.Columns["TEN_QG"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_QG");
             //   ID_QGSearchLookUpEdit.Properties.View.Columns["TEN_QG"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                string sSql = "";

                if (AddEdit)
                {
                    try
                    {
                        sSql = "SELECT TOP 1 ID_QG FROM dbo.[THANH_PHO] WHERE ID_QG = 234"; // + Id.ToString();
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_QGSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);
                    }
                    catch
                    {

                        if (dt.Rows.Count > 0) ID_QGSearchLookUpEdit.EditValue = dt.Rows[0][0];
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
            try
            {
                string sSql = "";
                sSql = "SELECT ID_QG,TEN_TP, TEN_TP_A, TEN_TP_H,  MS_TINH FROM dbo.THANH_PHO WHERE ID_TP = " + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp.Rows.Count <= 0) return;

                ID_QGSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_QG"];
                TEN_TPTextEdit.EditValue = dtTmp.Rows[0]["TEN_TP"];
                TEN_TP_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_TP_A"];
                TEN_TP_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_TP_H"];
                MS_TINHTextEdit.EditValue = dtTmp.Rows[0]["MS_TINH"];
                Ma = dtTmp.Rows[0]["MS_TINH"].ToString();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());

            }

        }

        private void LoadTextNull()
        {
            try
            {
                TEN_TPTextEdit.EditValue = string.Empty;
                TEN_TP_ATextEdit.EditValue = string.Empty;
                TEN_TP_HTextEdit.EditValue = string.Empty;
                MS_TINHTextEdit.EditValue = string.Empty;
                ID_QGSearchLookUpEdit.EditValue = ((DataTable)ID_QGSearchLookUpEdit.Properties.DataSource).Rows[0][0];
            }
            catch { }
        }
        private bool bKiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TP",
                    (AddEdit ? "-1" : Id.ToString()), "THANH_PHO", "TEN_TP", TEN_TPTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_TPTextEdit.Focus();
                    return true;
                }

                iKiem = 0;
                string sSql = "";
                if (AddEdit || Ma!= MS_TINHTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM THANH_PHO WHERE MS_TINH = '" + MS_TINHTextEdit.EditValue + "'";

                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        return true;
                    }
                }
                if (!string.IsNullOrEmpty(TEN_TP_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TP",
                        (AddEdit ? "-1" : Id.ToString()), "THANH_PHO", "TEN_TP_A", TEN_TP_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_TP_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_TP_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TP",
                        (AddEdit ? "-1" : Id.ToString()), "THANH_PHO", "TEN_TP_H", TEN_TP_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_TP_HTextEdit.Focus();
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
                            if (bKiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateThanhPho", (AddEdit ? -1 : Id), ID_QGSearchLookUpEdit.EditValue, TEN_TPTextEdit.EditValue, TEN_TP_ATextEdit.EditValue, TEN_TP_HTextEdit.EditValue,MS_TINHTextEdit.EditValue).ToString();

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


    }
}