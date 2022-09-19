using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
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

namespace VS.ERP
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        private int iID_USER;
        private string sFORM_NAME;
        private DateTime dNGAY_LAP;
        private Int64 iID_DOC;
        private string sDOC_NUM;
        private Int64 iID_DQD = 0;

        public frmDuyet(int ID_USER, string FORM_NAME, DateTime NGAY_LAP, Int64 ID_DOC, string DOC_NUM)
        {
            iID_USER = ID_USER;
            sFORM_NAME = FORM_NAME;
            dNGAY_LAP = NGAY_LAP;
            iID_DOC = ID_DOC;
            sDOC_NUM = DOC_NUM;
            InitializeComponent();


            txtQuyTrinhDuyet.ReadOnly = true;
        }

        public frmDuyet(int ID_USER, string FORM_NAME, DateTime NGAY_LAP, Int64 ID_DOC, string DOC_NUM, Int64 ID_DOC1, Int64 ID_DOC14)
        {
            iID_USER = ID_USER;
            sFORM_NAME = FORM_NAME;
            dNGAY_LAP = NGAY_LAP;
            iID_DOC = ID_DOC;
            sDOC_NUM = DOC_NUM;
            InitializeComponent();


            txtQuyTrinhDuyet.ReadOnly = true;
        }

        #region Event
        private void frmDuyet_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                LoadNN();

                //Khi chuyển vào hàng chờ duyệt thì mặc định đính kèm file
                chkDINH_KEM.Checked = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }


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
                            if (chkKHAN_CAP.Checked && !KiemTra_KhanCap())
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoNguoiQuyetDinh"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 11;
                            cmd.Parameters.Add("@NGAY_LAP", SqlDbType.DateTime).Value = dNGAY_LAP;
                            cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                            cmd.Parameters.Add("@ID_DOC", SqlDbType.BigInt).Value = iID_DOC;
                            cmd.Parameters.Add("@DOC_NUM", SqlDbType.NVarChar).Value = sFORM_NAME;
                            cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = iID_USER;
                            cmd.Parameters.Add("@Y_KIEN", SqlDbType.NVarChar).Value = txtY_KIEN.Text;
                            cmd.Parameters.Add("@KHAN_CAP", SqlDbType.Bit).Value = chkKHAN_CAP.Checked;
                            cmd.Parameters.Add("@DINH_KEM", SqlDbType.Bit).Value = chkDINH_KEM.Checked;
                            cmd.Parameters.Add("@iLoaiDuyet", SqlDbType.Int).Value = 2;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);

                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Copy();

                            int iTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][0])) ? -99 : Convert.ToInt32(dt.Rows[0][0]);
                            string sTEMP = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0][1])) ? "" : Convert.ToString(dt.Rows[0][1]);

                            if (iTEMP == 0)
                                this.DialogResult = DialogResult.OK;
                            else if (iTEMP == -1)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaCoBuocDuyet") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuyetThatBai") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
                        break;
                    }
                default: break;
            }
        }

        #endregion

        #region Function
        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,btnALL);
        }

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 13;
                cmd.Parameters.Add("@NGAY_LAP", SqlDbType.DateTime).Value = dNGAY_LAP;
                cmd.Parameters.Add("@FORM_NAME", SqlDbType.NVarChar).Value = sFORM_NAME;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                iID_DQD = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ID_DQD"])) ? 0 : Convert.ToInt64(dt.Rows[0]["ID_DQD"]);
                txtQuyTrinhDuyet.Text = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["SO_DQD"])) ? "" : Convert.ToString(dt.Rows[0]["SO_DQD"]);
            }
            catch { }
        }

        private bool KiemTra_KhanCap()
        {
            try
            {
                List<SqlParameter> lPar = new List<SqlParameter>
                {
                    new SqlParameter("@iLoai", 14),
                    new SqlParameter("@ID_DQD", iID_DQD),
                };

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 13;
                cmd.Parameters.Add("@ID_DQD", SqlDbType.BigInt).Value = iID_DQD;
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
