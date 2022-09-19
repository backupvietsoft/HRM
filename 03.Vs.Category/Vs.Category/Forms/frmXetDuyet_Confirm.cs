using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmXetDuyet_Confirm : DevExpress.XtraEditors.XtraForm
    {
        private Int64 iID_DQT = -1;
        private int iPQ = -1;
        public frmXetDuyet_Confirm(int PQ, Int64 ID_DQT)
        {
            iPQ = PQ;
            iID_DQT = ID_DQT;
            InitializeComponent();

            chkDINH_KEM.ReadOnly = true;
            chkKHAN_CAP.ReadOnly = true;
        }

        #region Event
        private void frmXetDuyet_Confirm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCbo();
                LoadData();
                LoadNN();
                Commons.Modules.ObjSystems.ThayDoiNN(this,Root,btnALL);
            }
            catch { }
        }
        private void chkCHAP_NHAN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCHAP_NHAN.Checked)
                {
                    //chkKHAN_CAP.Enabled = true;
                    cboID_USER_DEN.ReadOnly = false;
                }
                else
                {
                    //chkKHAN_CAP.Enabled = false;
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
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]))) == 1)
                    e.Appearance.BackColor = Color.FromArgb(255, 204, 255);
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]).ToString()) ? 0 : Convert.ToUInt32(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]))) == 0)
                    e.Appearance.BackColor = Color.FromArgb(141, 180, 226);
            }
            catch
            { }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                case "capnhat":
                    {
                        try
                        {
                            Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                            if (!dxValidationProvider1.Validate()) return;
                            if (chkKHAN_CAP.Checked && KiemTra_KhanCap()) return;

                            if (chkCHAP_NHAN.Checked)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonDuyetTaiLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OKCancel) != DialogResult.OK) return;
                            }
                            else
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonKhongChapNhanDuyetTaiLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OKCancel) != DialogResult.OK) return;
                            }
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 12;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_DQT", SqlDbType.BigInt).Value = iID_DQT;
                            cmd.Parameters.Add("@iID", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = Commons.Modules.iIDUser;
                            cmd.Parameters.Add("@Y_KIEN", SqlDbType.NVarChar).Value = txtY_KIEN.Text;
                            cmd.Parameters.Add("@CHAP_NHAN", SqlDbType.Bit).Value = chkCHAP_NHAN.Checked;
                            cmd.Parameters.Add("@KHAN_CAP", SqlDbType.Bit).Value = chkKHAN_CAP.Checked;
                            cmd.Parameters.Add("@DINH_KEM", SqlDbType.Bit).Value = chkDINH_KEM.Checked;
                            cmd.Parameters.Add("@ID_USER_DEN", SqlDbType.BigInt).Value = cboID_USER_DEN.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            int iTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][0])) ? -99 : Convert.ToInt32(dt.Rows[0][0]);
                            string sTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][1])) ? "" : Convert.ToString(dt.Rows[0][1]);

                            if (iTEMP == -99)
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuyetThatBai") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
                        this.Close();
                        break;
                    }
                default: break;
            }
        }

        #endregion


        #region Function
        private void LoadNN()
        {
            try
            {
                grcLichSuDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "grcLichSuDuyet");
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
                cmd.Parameters.Add("@ID_DQT", SqlDbType.BigInt).Value = iID_DQT;
                cmd.Parameters.Add("@iID", SqlDbType.Int).Value = 1;
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

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 16;
                cmd.Parameters.Add("@ID_DQT", SqlDbType.BigInt).Value = iID_DQT;
                cmd.Parameters.Add("@iID", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();


                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, false, false,true,this.Name);
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


                //Lấy ID_DQT_GOC
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool KiemTra_KhanCap()
        {
            try
            {
                List<SqlParameter> lPar = new List<SqlParameter>
                {
                    new SqlParameter("@iLoai", 14),
                    new SqlParameter("@ID_DQT", iID_DQT),
                    new SqlParameter("@iID", 1),
                };

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 14;
                cmd.Parameters.Add("@ID_DQT", SqlDbType.BigInt).Value = iID_DQT;
                cmd.Parameters.Add("@iID", SqlDbType.Int).Value = 1;
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

        #endregion

    }
}
