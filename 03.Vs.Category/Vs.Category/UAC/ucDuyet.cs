using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Category
{
    public partial class ucDuyet : DevExpress.XtraEditors.XtraUserControl
    {
        public Int64 iID_DOC = -1;
        public string sDOC_NUM = "";
        public string sFORM_NAME = "";
        private int TRANG_THAI = 0;
        public string sCREATE_USER = "";
        public DateTime dCREATE_TIME = DateTime.Now;
        public string sLAST_USER = "";
        public DateTime dLAST_TIME = DateTime.Now;
        public Int64 _ID_DQT = 0;
        public Int64 _ID_TEMP = 0;

        public ucDuyet()
        {
            InitializeComponent();
            FormControl();
        }

        public ucDuyet(Int64 ID_DOC, string DOC_NUM, string FORM_NAME, string CREATE_USER, DateTime CREATE_TIME, string LAST_USER, DateTime LAST_TIME)
        {
            iID_DOC = ID_DOC;
            sDOC_NUM = DOC_NUM;
            sFORM_NAME = FORM_NAME;
            sCREATE_USER = CREATE_USER;
            dCREATE_TIME = CREATE_TIME;
            sLAST_USER = LAST_USER;
            dLAST_TIME = LAST_TIME;

            InitializeComponent();
            FormControl();
        }

        #region Event
        private void ucDuyet_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                LoadNN();
                Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);
                //Commons.Modules.ObjSystems.MSaveResertGrid(grvChung, this.Name);
            }
            catch { }
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {
                case "duyet":
                    {
                        try
                        {
                            Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                            if (!dxValidationProvider1.Validate()) return;
                            if (TRANG_THAI == 0) return;
                            if (chkKHAN_CAP.Checked && KiemTra_KhanCap()) return;
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonDuyetTaiLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OKCancel) != DialogResult.OK) return;

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                            cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = Commons.Modules.iIDUser;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 12;
                            cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                            cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sDOC_NUM;
                            cmd.Parameters.Add("@Y_KIEN", SqlDbType.NVarChar).Value = txtY_KIEN.Text;
                            cmd.Parameters.Add("@CHAP_NHAN", SqlDbType.Bit).Value = chkCHAP_NHAN.Checked;
                            cmd.Parameters.Add("@KHAN_CAP", SqlDbType.Bit).Value = chkKHAN_CAP.Checked;
                            cmd.Parameters.Add("@ID_USER_DEN", SqlDbType.BigInt).Value = cboID_USER_DEN.EditValue;
                            cmd.Parameters.Add("@DINH_KEM", SqlDbType.Bit).Value = chkDINH_KEM.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Copy();

                            int iTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][0])) ? -99 : Convert.ToInt32(dt.Rows[0][0]);
                            string sTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][1])) ? "" : Convert.ToString(dt.Rows[0][1]);

                            if (iTEMP == -99)
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuyetThatBai") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //LoadData();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void chkCHAP_NHAN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCHAP_NHAN.Checked)
                {
                    chkKHAN_CAP.Enabled = true;
                    cboID_USER_DEN.ReadOnly = false;
                }
                else
                {
                    chkKHAN_CAP.Enabled = false;
                    cboID_USER_DEN.ReadOnly = true;
                }
                LoadCbo();
            }
            catch { }
        }

        private void grvChung_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]))) == 1 && (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]))) == 0)
                    e.Appearance.BackColor = Color.FromArgb(255, 204, 255);
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]))) == 0 && (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]))) == 0)
                    e.Appearance.BackColor = Color.FromArgb(141, 180, 226);
            }
            catch { }
        }
        #endregion

        #region Function
        private void LoadNN()
        {
            try
            {

                //lblY_KIEN.Text = Com.Mod.OS.GetLanguage(this.Name, "lblY_KIEN");
                //lblID_USER_DEN.Text = Com.Mod.OS.GetLanguage(this.Name, "lblID_USER_DEN");
                //chkCHAP_NHAN.Text = Com.Mod.OS.GetLanguage(this.Name, "chkCHAP_NHAN");
                chkKHAN_CAP.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "chkKHAN_CAP");
                chkDINH_KEM.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "chkDINH_KEM");
                //btnDuyet.Text = Com.Mod.OS.GetLanguage(this.Name, "btnDuyet");
                grcDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "grcDuyet");


                //lblCREATE_USER.Text = Com.Mod.OS.GetLanguage(this.Name, "lblCREATE_USER");
                //lblCREATE_TIME.Text = Com.Mod.OS.GetLanguage(this.Name, "lblCREATE_TIME");
                //lblLAST_USER.Text = Com.Mod.OS.GetLanguage(this.Name, "lblLAST_USER");
                //lblLAST_TIME.Text = Com.Mod.OS.GetLanguage(this.Name, "lblLAST_TIME");

                //Com.Mod.OS.MLoadNNXtraGrid(grvChung, this.Name);
            }
            catch { }
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 15;
                cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                cmd.Parameters.Add("@ID_DOC", SqlDbType.BigInt).Value = iID_DOC;
                cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sDOC_NUM;
                cmd.Parameters.Add("@KHAN_CAP", SqlDbType.Bit).Value = chkKHAN_CAP.Checked;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_USER_DEN, dt, "ID_USER", "USER_NAME", this.Name);

                if (dt != null && dt.Rows.Count > 1)
                    cboID_USER_DEN.EditValue = string.IsNullOrEmpty(Convert.ToString(dt.Rows[1]["ID_USER"])) ? -99 : Convert.ToInt32(dt.Rows[1]["ID_USER"]);
            }
            catch { }
        }

        public void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 16;
                cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                cmd.Parameters.Add("@ID_DOC", SqlDbType.BigInt).Value = iID_DOC;
                cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sDOC_NUM;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, false, false, true, this.Name);
                    grvChung.Columns["ID_DQT"].Visible = false;
                    grvChung.Columns["ID_DQT_TRUOC"].Visible = false;
                    grvChung.Columns["ID_DQT_GOC"].Visible = false;
                    grvChung.Columns["ID_DQD"].Visible = false;
                    grvChung.Columns["ID_DOC"].Visible = false;
                    grvChung.Columns["DOC_NUM"].Visible = false;
                    grvChung.Columns["ID_DTL"].Visible = false;
                    grvChung.Columns["ID_USER_CHUYEN"].Visible = false;
                    grvChung.Columns["ID_USER_DEN"].Visible = false;
                }
                else
                    grdChung.DataSource = dt;

                txtCREATE_USER.Text = sCREATE_USER;
                datCREATE_TIME.EditValue = dCREATE_TIME;
                txtLAST_USER.Text = sLAST_USER;
                datLAST_TIME.EditValue = dLAST_TIME;

                //Lấy ID_DQT_GOC
                if (dt != null && dt.Rows.Count > 0)
                {
                    Int64 ID_DQT_GOC = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ID_DQT_GOC"])) ? 0 : Convert.ToInt64(dt.Rows[0]["ID_DQT_GOC"]);

                    DataTable dt1 = new DataTable();
                    try
                    {
                        dt1 = dt.AsEnumerable().Where(r => r.Field<Int64>("ID_DQT").Equals(ID_DQT_GOC)).CopyToDataTable();
                    }
                    catch { dt1 = dt.Clone(); }

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        chkDINH_KEM.Checked = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[0]["DINH_KEM"])) ? false : Convert.ToBoolean(dt1.Rows[0]["DINH_KEM"]);
                        chkKHAN_CAP.Checked = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[0]["KHAN_CAP"])) ? false : Convert.ToBoolean(dt1.Rows[0]["KHAN_CAP"]);
                    }
                }


                LoadCbo();
                KiemTra_User();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        private bool KiemTra_KhanCap()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 14;
                cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                cmd.Parameters.Add("@ID_DOC", SqlDbType.BigInt).Value = iID_DOC;
                cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sDOC_NUM;
                cmd.CommandType = CommandType.StoredProcedure;

                if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return true;
            }
        }

        private void KiemTra_User()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 18;
                cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                cmd.Parameters.Add("@ID_DOC", SqlDbType.BigInt).Value = iID_DOC;
                cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sDOC_NUM;
                cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = Commons.Modules.iIDUser;
                cmd.CommandType = CommandType.StoredProcedure;

                int iTEMP = Convert.ToInt32(cmd.ExecuteScalar());
                if (iTEMP == 1)
                {
                    TRANG_THAI = 1;
                }
                else
                {
                    TRANG_THAI = 0;
                }

                StatusControl(TRANG_THAI);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void StatusControl(int iTRANG_THAI)
        {
            txtCREATE_USER.ReadOnly = true;
            datCREATE_TIME.ReadOnly = true;
            txtLAST_USER.ReadOnly = true;
            datLAST_TIME.ReadOnly = true;

            chkDINH_KEM.ReadOnly = true;
            chkKHAN_CAP.ReadOnly = true;

            if (iTRANG_THAI == 1)
            {
                txtY_KIEN.ReadOnly = false;
                chkCHAP_NHAN.Enabled = true;
                //chkDINH_KEM.Enabled = true;
                cboID_USER_DEN.ReadOnly = true;
                //chkKHAN_CAP.Enabled = false;
            }
            else
            {
                txtY_KIEN.ReadOnly = true;
                chkCHAP_NHAN.Enabled = false;
                //chkDINH_KEM.Enabled = false;
                //cboID_USER_DEN.ReadOnly = true;
                //chkKHAN_CAP.Enabled = false;

                //chkKHAN_CAP.Checked = false;
                //chkDINH_KEM.Checked = false;
                chkCHAP_NHAN.Checked = false;
                txtY_KIEN.Text = null;
            }
        }

        private void FormControl()
        {
            try
            {
                chkKHAN_CAP.Enabled = false;
                cboID_USER_DEN.ReadOnly = true;

                btnALL.Buttons[0].Properties.Visible = false;
                //emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciCHAP_NHAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            catch { }
        }
        #endregion

       
    }
}
