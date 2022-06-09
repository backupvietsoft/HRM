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
    public partial class frmEditQUOC_GIA : DevExpress.XtraEditors.XtraForm
    {
        Int64 iId = -1;
        Boolean bAddEdit = true;  // true la add false la edit
        string Ma = "", Ten="";
        public frmEditQUOC_GIA(Int64 iIdForm, Boolean bAddEditForm)
        {
            InitializeComponent();
            iId = iIdForm;
            bAddEdit = bAddEditForm;
        }

        private void frmEditQUOC_GIA_Load(object sender, EventArgs e)
        {
            if (!bAddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnWDUI);

        }

        private void frmEditQUOC_GIA_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void LoadText()
        {
            try
            {
                string sSql = "";
                sSql = "SELECT ID_QG, MA_QG, TEN_QG, TEN_QG_A, TEN_QG_H FROM QUOC_GIA WHERE ID_QG =   " + iId.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp.Rows.Count <= 0) return;


                MA_QGTextEdit.EditValue = dtTmp.Rows[0]["MA_QG"];
                Ma = dtTmp.Rows[0]["MA_QG"].ToString();
                Ten = dtTmp.Rows[0]["TEN_QG"].ToString();
                TEN_QGTextEdit.EditValue = dtTmp.Rows[0]["TEN_QG"];
                TEN_QG_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_QG_A"];
                TEN_QG_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_QG_H"];

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
                MA_QGTextEdit.EditValue = String.Empty;
                TEN_QGTextEdit.EditValue = String.Empty;
                TEN_QG_ATextEdit.EditValue = String.Empty;
                TEN_QG_HTextEdit.EditValue = String.Empty;
                MA_QGTextEdit.Focus();
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateQuocGia", (bAddEdit ? -1 : iId), MA_QGTextEdit.EditValue, TEN_QGTextEdit.EditValue, TEN_QG_ATextEdit.EditValue, TEN_QG_HTextEdit.EditValue).ToString();

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
                if (bAddEdit || Ma != MA_QGTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM QUOC_GIA WHERE MA_QG = '" + MA_QGTextEdit.EditValue + "'";

                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        return true;
                    }
             
                }
                if (bAddEdit || Ten != TEN_QGTextEdit.EditValue.ToString())
                {
                    tenSql = "SELECT TEN_QG FROM QUOC_GIA WHERE TEN_QG = '" + TEN_QGTextEdit.EditValue + "'";
                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_QGTextEdit.EditValue)))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
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