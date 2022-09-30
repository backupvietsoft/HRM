using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Category
{
    public partial class frmEditTO : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdTo = 0;
        Boolean bAddEditTo = true;  // true la add false la edit
        string MSTO = "";

        public frmEditTO(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            iIdTo = iId;
            bAddEditTo = bAddEdit;
        }
        private void frmEditTO_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            ItemForToTruong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {

                try
                {
                    if (iIdTo > 0)
                    {
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_DVLookUpEdit, Commons.Modules.ObjSystems.DataDonVi(true), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);

                        string strSQLDV = "SELECT XN.ID_DV FROM [TO] T INNER JOIN XI_NGHIEP XN ON T.ID_XN = XN.ID_XN WHERE ID_TO = " + iIdTo;
                        int EditValueDV = System.Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQLDV));
                        ID_DVLookUpEdit.EditValue = EditValueDV;

                        DataTable dt = new DataTable();
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt, "ID_CN", "HO_TEN", "HO_TEN");
                        ItemForToTruong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                        DataTable dt1 = new DataTable();
                        dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhanBo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_PBLookUpEdit, dt1, "ID_LPB", "TEN_LPB", "TEN_LPB", true, false, false);

                        string strSQLPB = "SELECT T.PHAN_BO FROM dbo.[TO] T WHERE T.ID_TO = " + iIdTo;
                        var obj = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQLPB);
                        if(obj == null)
                        {
                            ID_PBLookUpEdit.EditValue = -1;
                        }
                        else
                        {
                            ID_PBLookUpEdit.EditValue = obj;
                        }
                    }
                    else
                    {
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_DVLookUpEdit, Commons.Modules.ObjSystems.DataDonVi(true), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);
                        ID_DVLookUpEdit.EditValue = -1;

                        DataTable dt = new DataTable();
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt, "ID_CN", "HO_TEN", "HO_TEN",true, false, false);
                        ItemForToTruong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                        DataTable dt1 = new DataTable();
                        dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhanBo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
                        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_PBLookUpEdit, dt1, "ID_LPB", "TEN_LPB", "TEN_LPB", true, false, false);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            LoadXiNghiep();
            if (!bAddEditTo)
            {
                LoadText();
            }
            else
            {
                string strSQL = "SELECT MAX(STT_TO) FROM dbo.[TO]";
                STT_TOTextEdit.EditValue = (string.IsNullOrEmpty(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL).ToString()) ? 0 : Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL))) + 1;
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

            Commons.Modules.sLoad = "";
        }
        private void LoadXiNghiep()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListXI_NGHIEP", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            ID_XNLookUpEdit.Properties.DataSource = dt;
            ID_XNLookUpEdit.Properties.ValueMember = "ID_XN";
            ID_XNLookUpEdit.Properties.DisplayMember = "TEN_XN";
            ID_XNLookUpEdit.Properties.PopulateViewColumns();

            try
            {
                
                ID_XNLookUpEdit.Properties.View.Columns["ID_XN"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["ID_DV"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["TEN_DV"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["MS_XN"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["STT_XN"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["GOP_PB"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["GOP_TH"].Visible = false;
                ID_XNLookUpEdit.Properties.View.Columns["STT_DV"].Visible = false;
                ID_XNLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
                
                ID_XNLookUpEdit.Properties.View.Columns["TEN_XN"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_XN");

                ID_XNLookUpEdit.Properties.View.Columns["TEN_XN"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                ID_XNLookUpEdit.Properties.View.Columns["TEN_DV"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


                if (bAddEditTo)
                {
                    try
                    {
                        string sSql = "SELECT TOP 1 ID_XN FROM dbo.[TO] WHERE ID_TO = " + iIdTo.ToString();
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_XNLookUpEdit.EditValue = Convert.ToInt64(sSql);

                        sSql = "SELECT ISNULL(MAX(STT_TO),0) + 1 FROM dbo.[TO] WHERE ID_XN = " + sSql.ToString();
                        sSql = (String)SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                        STT_TOTextEdit.EditValue = Convert.ToInt64(sSql);
                    }
                    catch
                    {

                        if(dt.Rows.Count>0) ID_XNLookUpEdit.EditValue = dt.Rows[0][0];
                    }
                }


            }
            catch(Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());

            }
        }
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT ID_TO,ID_XN,MS_TO,TEN_TO,TEN_TO_A,TEN_TO_H,STT_TO,ID_CN FROM dbo.[TO] WHERE ID_TO = " + iIdTo.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;

            ID_XNLookUpEdit.EditValue = dtTmp.Rows[0]["ID_XN"];
            MS_TOTextEdit.EditValue = dtTmp.Rows[0]["MS_TO"];
            MSTO = dtTmp.Rows[0]["MS_TO"].ToString();
            TEN_TOTextEdit.EditValue = dtTmp.Rows[0]["TEN_TO"];
            TEN_TO_ANHTextEdit.EditValue = dtTmp.Rows[0]["TEN_TO_A"];
            TEN_TO_HOATextEdit.EditValue = dtTmp.Rows[0]["TEN_TO_H"];
            STT_TOTextEdit.EditValue = dtTmp.Rows[0]["STT_TO"];
            cboID_CN.EditValue = dtTmp.Rows[0]["ID_CN"].ToString() == "" ? -1 : Convert.ToInt64(dtTmp.Rows[0]["ID_CN"]);
        }

        private void LoadTextNull()
        {
            try
            {
                //ID_XNLookUpEdit.EditValue = String.Empty;
                MS_TOTextEdit.EditValue = String.Empty;
                TEN_TOTextEdit.EditValue = String.Empty;
                TEN_TO_ANHTextEdit.EditValue = String.Empty;
                TEN_TO_HOATextEdit.EditValue = String.Empty;
                STT_TOTextEdit.EditValue = String.Empty;
                cboID_CN.EditValue = -1;
                MS_TOTextEdit.Focus();
            }
            catch { }
        }

        private void windowsUIButtonPanel2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
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
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTo", (bAddEditTo ? -1 : iIdTo), ID_XNLookUpEdit.EditValue, ID_PBLookUpEdit.EditValue, MS_TOTextEdit.EditValue, TEN_TOTextEdit.EditValue, TEN_TO_ANHTextEdit.EditValue, TEN_TO_HOATextEdit.EditValue, STT_TOTextEdit.EditValue, Convert.ToInt64(cboID_CN.Text == "" ? cboID_CN.EditValue = null : cboID_CN.EditValue), Commons.Modules.UserName).ToString();
                            if (bAddEditTo)
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
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private bool KiemTrung()
        {
            try
            {
                DataTable dtTmp = new DataTable();
                Int16 iKiem = 0;

                iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TO",
                    (bAddEditTo ? "-1" : iIdTo.ToString()), "[TO]", "MS_TO", MS_TOTextEdit.Text.ToString(),
                    "", "", "", ""));
                if (iKiem > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MS_TOTextEdit.Focus();
                    return true;
                }

                iKiem = 0;

                if (!string.IsNullOrEmpty(TEN_TOTextEdit.Text))
                {
                    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_TO",
                        (bAddEditTo ? "-1" : iIdTo.ToString()), "[TO]", "TEN_TO", TEN_TOTextEdit.Text.ToString(),
                        "", "", "", ""));
                    if (iKiem > 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TEN_TOTextEdit.Focus();
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
        private void frmEditTO_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

        private void MS_TOTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void ID_DVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
        }
    }

}
