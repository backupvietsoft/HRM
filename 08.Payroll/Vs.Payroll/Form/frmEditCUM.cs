using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditCUM : DevExpress.XtraEditors.XtraForm
    {
        Int64 iIdTo = -1;
        Boolean bAddEditTo = true;  // true la add false la edit
        string MS = "", TEN = "";

        public frmEditCUM(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
            iIdTo = iId;
            bAddEditTo = bAddEdit;
        }
        private void frmEditCUM_Load(object sender, EventArgs e)
        {
            LoadCombobox();
            if (!bAddEditTo) LoadText();
        }
        private void LoadCombobox()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(TEN_LOAI_SAN_PHAMLookUpEdit, Commons.Modules.ObjSystems.DataNhomHangHoa(false),"ID_NHH", "TEN_NHH",Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHH"),false);
            }
            catch
            {
            }
        }
        private void LoadText()
        {
            string sSql = "";
            sSql = "SELECT * FROM dbo.[CUM] WHERE ID_CUM = " + iIdTo.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (dtTmp.Rows.Count <= 0) return;

            TEN_LOAI_SAN_PHAMLookUpEdit.EditValue = dtTmp.Rows[0]["ID_NHH"];
            MS_CUMTextEdit.EditValue = dtTmp.Rows[0]["MS_CUM"];
            MS = dtTmp.Rows[0]["MS_CUM"].ToString();
            TEN_CUMTextEdit.EditValue = dtTmp.Rows[0]["TEN_CUM"];
            TEN = dtTmp.Rows[0]["TEN_CUM"].ToString();
            TEN_CUM_ATextEdit.EditValue = dtTmp.Rows[0]["TEN_CUM_A"];
            TEN_CUM_HTextEdit.EditValue = dtTmp.Rows[0]["TEN_CUM_H"];
            STT_CUMTextEdit.EditValue = dtTmp.Rows[0]["STT"];
            TINH_TGCheckEdit.EditValue = dtTmp.Rows[0]["TINH_TG"];
            LOAI_CUMTextEdit.EditValue = dtTmp.Rows[0]["LOAI_CUM"];
            CUM_PSCheckEdit.EditValue = dtTmp.Rows[0]["CUM_PS"];
            CUM_CUOICheckEdit.EditValue = dtTmp.Rows[0]["CUM_CUOI"];

           

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
                            if (bAddEditTo) iIdTo = -1;
                            #region Them
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateCUM", conn);
                            
                            cmd.Parameters.Add("@ID_CUM", SqlDbType.Int).Value = iIdTo;
                            

                            cmd.Parameters.Add("@MS_CUM", SqlDbType.NVarChar).Value = MS_CUMTextEdit.Text;
                            cmd.Parameters.Add("@TEN_CUM", SqlDbType.NVarChar).Value = TEN_CUMTextEdit.Text;
                            cmd.Parameters.Add("@TEN_CUM_A", SqlDbType.NVarChar).Value = TEN_CUM_ATextEdit.Text;
                            cmd.Parameters.Add("@TEN_CUM_H", SqlDbType.NVarChar).Value = TEN_CUM_HTextEdit.Text;
                            cmd.Parameters.Add("@STT", SqlDbType.SmallInt).Value = STT_CUMTextEdit.Text==""? 1: STT_CUMTextEdit.EditValue;
                            cmd.Parameters.Add("@ID_LSP", SqlDbType.Int).Value =TEN_LOAI_SAN_PHAMLookUpEdit.EditValue;
                            cmd.Parameters.Add("@LOAI_CUM", SqlDbType.NVarChar).Value = LOAI_CUMTextEdit.Text;

                            cmd.Parameters.Add("@TINH_TG", SqlDbType.Bit).Value = TINH_TGCheckEdit.Checked == true ? true : false;
                            cmd.Parameters.Add("@CUM_PS", SqlDbType.Bit).Value = CUM_PSCheckEdit.Checked == true ? true : false;
                            cmd.Parameters.Add("@CUM_CUOI", SqlDbType.Bit).Value = CUM_CUOICheckEdit.Checked == true ? true : false;


                            cmd.CommandType = CommandType.StoredProcedure;
                            Commons.Modules.sId = Convert.ToString(cmd.ExecuteScalar());


                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            #endregion

                            break;
                        }
                    case "huy":
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
        private Boolean KiemTrung()
        {

            try
            {
                string sSql = "";
                string tenSql = "";
                if (bAddEditTo || MS != MS_CUMTextEdit.EditValue.ToString() || TEN != TEN_CUMTextEdit.EditValue.ToString())
                {
                    sSql = "SELECT COUNT(*) FROM [CUM] WHERE MS_CUM = '" + MS_CUMTextEdit.EditValue + "'";
                    tenSql = "SELECT TEN_CUM FROM [CUM] WHERE TEN_CUM = '" + TEN_CUMTextEdit.EditValue + "'";
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0 && MS != MS_CUMTextEdit.EditValue.ToString())
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

                        return true;
                    }
                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_CUMTextEdit.EditValue)) && TEN != TEN_CUMTextEdit.EditValue.ToString())
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
        private void frmEditCUM_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

    }

}
