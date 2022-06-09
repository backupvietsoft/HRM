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
    public partial class frmEditPHUONG_XA : DevExpress.XtraEditors.XtraForm
    {
        Int64 Id = 0;
        Boolean AddEdit = true;  // true la add false la edit
        Boolean bLoad = true;
        string MS = "", TEN = "";
        public frmEditPHUONG_XA(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditPHUONG_XA_Load(object sender, EventArgs e)
        {
            
            LoadTHANH_PHO();
            bLoad = false;
            try
            {
                if (string.IsNullOrEmpty(ID_TPSearchLookUpEdit.EditValue.ToString()))
                    LoadQuan(-1);
                else
                    LoadQuan(Int64.Parse(ID_TPSearchLookUpEdit.EditValue.ToString()));
            }
            catch { LoadQuan(-1); }

            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        
        private void frmEditPHUONG_XA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadTHANH_PHO()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTHANH_PHO", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            ID_TPSearchLookUpEdit.Properties.DataSource = dt;
            ID_TPSearchLookUpEdit.Properties.ValueMember = "ID_TP";
            ID_TPSearchLookUpEdit.Properties.DisplayMember = "TEN_TP";
            ID_TPSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                ID_TPSearchLookUpEdit.Properties.View.Columns["ID_TP"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.View.Columns["ID_QG"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_QG"].Visible = false;
                //ID_TPSearchLookUpEdit.Properties.View.Columns["MS_TINH"].Visible = false;
                ID_TPSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_TP"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_TP");
                ID_TPSearchLookUpEdit.Properties.View.Columns["MS_TINH"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "MS_TINH");
                ID_TPSearchLookUpEdit.Properties.View.Columns["TEN_TP"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
                string sSql = "SELECT T1.ID_PX, T1.ID_QUAN, T2.ID_TP, T1.TEN_PX, T1.TEN_PX_A, T1.TEN_PX_H, T1.MS_XA " +
                    "FROM PHUONG_XA T1 INNER JOIN QUAN T2 ON T1.ID_QUAN = T2.ID_QUAN " +
                    "WHERE T1.ID_PX =	" + Id.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_PXTextEdit.EditValue = dtTmp.Rows[0]["TEN_PX"].ToString();
                TEN = dtTmp.Rows[0]["TEN_PX"].ToString();
                TEN_PX_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_PX_A"].ToString();
                TEN_PX_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_PX_H"].ToString();
                MS_XATextEdit.EditValue = dtTmp.Rows[0]["MS_XA"].ToString();
                MS = dtTmp.Rows[0]["MS_XA"].ToString();

                ID_TPSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_TP"];
                LoadQuan(Int64.Parse(ID_TPSearchLookUpEdit.EditValue.ToString()));
                ID_QUANSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_QUAN"];
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void LoadQuan(Int64 iIdTP)
        {
            if (bLoad) return;
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQUAN", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.DefaultView.RowFilter = " ID_TP = " + iIdTP.ToString();
            dt = dt.DefaultView.ToTable();
            ID_QUANSearchLookUpEdit.Properties.DataSource = null;
            ID_QUANSearchLookUpEdit.EditValue = null;
            ID_QUANSearchLookUpEdit.Properties.View.Columns.Clear();

            ID_QUANSearchLookUpEdit.Properties.DataSource = dt;
            ID_QUANSearchLookUpEdit.Properties.ValueMember = "ID_QUAN";
            ID_QUANSearchLookUpEdit.Properties.DisplayMember = "TEN_QUAN";
            ID_QUANSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

                ID_QUANSearchLookUpEdit.Properties.View.Columns["ID_QUAN"].Visible = false;
                ID_QUANSearchLookUpEdit.Properties.View.Columns["ID_TP"].Visible = false;
                ID_QUANSearchLookUpEdit.Properties.View.Columns["TEN_TP"].Visible = false;
                //ID_QUANSearchLookUpEdit.Properties.View.Columns["MS_QUAN"].Visible = false;

                ID_QUANSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
                ID_QUANSearchLookUpEdit.Properties.View.Columns["TEN_QUAN"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_QUAN");
                ID_QUANSearchLookUpEdit.Properties.View.Columns["MS_QUAN"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "MS_QUAN");
                ID_QUANSearchLookUpEdit.Properties.View.Columns["TEN_QUAN"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
                TEN_PXTextEdit.EditValue = String.Empty;
                TEN_PX_ATextEdit.EditValue = String.Empty;
                TEN_PX_HTextEdit.EditValue = String.Empty;
                MS_XATextEdit.EditValue = String.Empty;

                TEN_PXTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdatePHUONG_XA", (AddEdit ? -1 : Id),
                                MS_XATextEdit.EditValue, TEN_PXTextEdit.EditValue, TEN_PX_ATextEdit.EditValue,
                                TEN_PX_HTextEdit.EditValue, ID_QUANSearchLookUpEdit.EditValue
                                ).ToString();
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
                string sSql = "";
                if (AddEdit || MS!= MS_XATextEdit.EditValue.ToString() || TEN != TEN_PXTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM PHUONG_XA WHERE MS_XA = '" + MS_XATextEdit.EditValue + "' AND ID_QUAN = "+ ID_QUANSearchLookUpEdit.EditValue + "";

                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        return true;
                    }
                }
                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_PX",
                    (AddEdit ? "-1" : Id.ToString()), "PHUONG_XA", "TEN_PX", TEN_PXTextEdit.EditValue.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                    TEN_PXTextEdit.Focus();
                    return true;
                }

                iKiem = 0;
                
                if (!string.IsNullOrEmpty(TEN_PX_ATextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_PX",
                        (AddEdit ? "-1" : Id.ToString()), "PHUONG_XA", "TEN_PX_A", TEN_PX_ATextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_PX_ATextEdit.Focus();
                        return true;
                    }
                }

                iKiem = 0;
                if (!string.IsNullOrEmpty(TEN_PX_HTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_PX",
                        (AddEdit ? "-1" : Id.ToString()), "PHUONG_XA", "TEN_PX_H", TEN_PX_HTextEdit.EditValue.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_PX_HTextEdit.Focus();
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

        private void ID_TPSearchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (bLoad) return;
            try
            {
                if (string.IsNullOrEmpty(ID_TPSearchLookUpEdit.EditValue.ToString()))
                    LoadQuan(-1);
                else
                    LoadQuan(Int64.Parse(ID_TPSearchLookUpEdit.EditValue.ToString()));
            }
            catch { LoadQuan(-1); }
        }
    }
}