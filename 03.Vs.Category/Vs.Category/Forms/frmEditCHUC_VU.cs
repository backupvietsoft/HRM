using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmEditCHUC_VU : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdCV = 0;
        Boolean bAddEditCV = true;  // true la add false la edit
        string MS = "", TEN="";

        public frmEditCHUC_VU(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            iIdCV = iId;
            bAddEditCV = bAddEdit;
        }
        private void frmEditCHUC_VU_Load(object sender, EventArgs e)
        {
            LoadLoaiCV();
            if (!bAddEditCV) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);

        }
        private void LoadLoaiCV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_CHUC_VU", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_LOAI_CVSearchLookUpEdit, dt, "ID_LOAI_CV", "TEN_LOAI_CV", "TEN_LOAI_CV");
         //   ID_LOAI_CVSearchLookUpEdit.Properties.DataSource = dt;
         //   ID_LOAI_CVSearchLookUpEdit.Properties.ValueMember = "ID_LOAI_CV";
         //   ID_LOAI_CVSearchLookUpEdit.Properties.DisplayMember = "TEN_LOAI_CV";
         //   ID_LOAI_CVSearchLookUpEdit.Properties.PopulateViewColumns();

            try
            {

              if(ID_LOAI_CVSearchLookUpEdit.Properties.View.Columns["ID_LOAI_CV"]!=null)  ID_LOAI_CVSearchLookUpEdit.Properties.View.Columns["ID_LOAI_CV"].Visible = false;
           //     ID_LOAI_CVSearchLookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.None;
            //    ID_LOAI_CVSearchLookUpEdit.Properties.View.Columns["TEN_LOAI_CV"].Caption = Commons.Modules.ObjLanguages.GetLanguage("ucListDMuc", "TEN_LOAI_CV");
             //   ID_LOAI_CVSearchLookUpEdit.Properties.View.Columns["TEN_LOAI_CV"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                if (bAddEditCV)
                {
                    try
                    {
                        string sSql = "SELECT TOP 1 ID_LOAI_CV FROM dbo.[CHUC_VU] WHERE ID_CV = " + iIdCV.ToString();
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
                        ID_LOAI_CVSearchLookUpEdit.EditValue = Convert.ToInt64(sSql);
                    }
                    catch
                    { ID_LOAI_CVSearchLookUpEdit.EditValue = dt.Rows[0][0]; }
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
                string sSql = "SELECT MS_CV, TEN_CV, TEN_CV_A, TEN_CV_H, ID_LOAI_CV, STT_IN_CV " +
                    "FROM CHUC_VU WHERE ID_CV =	" + iIdCV.ToString();
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                ItemForMS_CV.Control.Text = dtTmp.Rows[0]["MS_CV"].ToString();
                MS = dtTmp.Rows[0]["MS_CV"].ToString();
                ItemForTEN_CV.Control.Text = dtTmp.Rows[0]["TEN_CV"].ToString();
                TEN = dtTmp.Rows[0]["TEN_CV"].ToString();
                ItemForTEN_CV_A.Control.Text = dtTmp.Rows[0]["TEN_CV_A"].ToString();
                ItemForTEN_CV_H.Control.Text = dtTmp.Rows[0]["TEN_CV_H"].ToString();
                ID_LOAI_CVSearchLookUpEdit.EditValue = dtTmp.Rows[0]["ID_LOAI_CV"];
                ItemForSTT_IN_CV.Control.Text = dtTmp.Rows[0]["STT_IN_CV"].ToString();
                
            }
            catch (Exception EX)
            {

                XtraMessageBox.Show(EX.Message.ToString());
            }

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
                            if (KiemTrung()) return;
                            Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateCHUC_VU", (bAddEditCV ? -1 : Convert.ToInt32(iIdCV)), 
                                MS_CVTextEdit.EditValue, TEN_CVTextEdit.EditValue, TEN_CV_ATextEdit.EditValue, 
                                TEN_CV_HTextEdit.EditValue, ID_LOAI_CVSearchLookUpEdit.EditValue, (STT_IN_CVTextEdit.EditValue == "") ? STT_IN_CVTextEdit.EditValue = null : STT_IN_CVTextEdit.EditValue).ToString();
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
        private bool KiemTrung()
        {
            try
            {
                string sSql = "";
                string tenSql = "";
                if (bAddEditCV || MS != MS_CVTextEdit.EditValue.ToString()|| TEN != TEN_CVTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM CHUC_VU WHERE MS_CV = '" + MS_CVTextEdit.Text + "' AND ID_LOAI_CV ="+ ID_LOAI_CVSearchLookUpEdit.EditValue + " AND ID_CV <> "+iIdCV+"";
                    tenSql = "SELECT COUNT(*) FROM CHUC_VU WHERE TEN_CV = N'" + TEN_CVTextEdit.Text + "' AND ID_LOAI_CV =" + ID_LOAI_CVSearchLookUpEdit.EditValue + " AND ID_CV <> " + iIdCV + "";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        MS_CVTextEdit.Focus();
                        return true;
                    }
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) != 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
                        TEN_CVTextEdit.Focus();
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
        private void frmEditCHUC_VU_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
    }
}