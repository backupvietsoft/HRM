using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class frmEditHSBT : DevExpress.XtraEditors.XtraForm
    {
        static Int64 Id = -1;
        static Boolean AddEdit = true;  // true la add false la edit
        string hsbt = "";

        public frmEditHSBT(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            Id = iId;
            AddEdit = bAddEdit;
        }

        private void frmEditHSBT_Load(object sender, EventArgs e)
        {
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, btnALL);
        }
        private void frmEditHSBT_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();
        private void LoadText()
        {
            try
            {
                string sSql = "select * from HSBT where ID_BT  = " + Id;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                TEN_BAC_THOTextEdit.EditValue = dtTmp.Rows[0]["TEN_BAC_THO"];
                hsbt = dtTmp.Rows[0]["TEN_BAC_THO"].ToString();
                HSBTTextEdit.EditValue = dtTmp.Rows[0]["HE_SO_BAC_THO"].ToString();
                txtSTT.EditValue = dtTmp.Rows[0]["STT"].ToString();
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
                TEN_BAC_THOTextEdit.EditValue = String.Empty;
                HSBTTextEdit.EditValue = String.Empty;
            }
            catch { }
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
                            if (KiemTrung()) return;



                            #region Them
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateHSBT", conn);
                            if (AddEdit)
                            {
                                cmd.Parameters.Add("@ID_BAC_THO", SqlDbType.Int).Value = -1;
                            }
                            else
                            {
                                cmd.Parameters.Add("@ID_BAC_THO", SqlDbType.Int).Value = Id;
                            }

                            cmd.Parameters.Add("@TEN_BAC_THO", SqlDbType.NVarChar).Value = TEN_BAC_THOTextEdit.Text;
                            cmd.Parameters.Add("@HE_SO_BAC_THO", SqlDbType.Float).Value = (HSBTTextEdit.Text);
                            cmd.Parameters.Add("@STT", SqlDbType.Int).Value = (txtSTT.EditValue == null) ? 0 : txtSTT.EditValue;


                            cmd.CommandType = CommandType.StoredProcedure;
                            Commons.Modules.sId = Convert.ToString(cmd.ExecuteScalar());
                            Commons.Modules.sId = (-1).ToString();
                            if (AddEdit)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    LoadTextNull();
                                    return;
                                }
                            }

                            #endregion


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
        private Boolean KiemTrung()
        {
            try
            {
                string sSql = "";
                string tenSql = "";
                if (AddEdit || hsbt != TEN_BAC_THOTextEdit.EditValue.ToString())
                {
                    tenSql = "SELECT TEN_BAC_THO FROM HSBT WHERE TEN_BAC_THO = '" + TEN_BAC_THOTextEdit.EditValue + "'";

                    if (Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, tenSql)) == Convert.ToString((TEN_BAC_THOTextEdit.EditValue)))
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_MaSoTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}