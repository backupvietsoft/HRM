using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmThongTinTiepNhanUV : DevExpress.XtraEditors.XtraForm
    {
        private ucCTQLUV ucUV;
        private long iID_UV;
        private int iMS_CV;
        public AccordionControl accorMenuleft;
        private int dem = 0;
        private string sNGayChuyen = "";
        private long iID_YCTD = -1;
        private long iID_VTTD = -1;
        public DataTable dtTemp;
        public frmThongTinTiepNhanUV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }
        #region even
        private void frmThongTinTiepNhanUV_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNGUOI_DT, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_CHUYEN, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);

                DataTable dt = new DataTable();
                string strSQL = "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE ID_TT_HD = 3";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHDLD, dt, "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD", true, true);

                Commons.OSystems.SetDateEditFormat(datNgayHenDL);
                Commons.OSystems.SetDateEditFormat(datNGAY_NHAN_VIEC);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex) { }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "ghi":
                        {
                            if(chkHOAN_THANH_DT.Checked == true)
                            {
                                if(cboNGUOI_DT.Text == "")
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonNguoiDaoTao"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cboNGUOI_DT.Focus();
                                    return;
                                }
                            }
                            string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dtTemp, "");
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                            cmd.Parameters.Add("@DNgay1", SqlDbType.DateTime).Value = datNgayHenDL.Text == "" ? datNgayHenDL.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHenDL.Text);
                            cmd.Parameters.Add("@DNgay2", SqlDbType.DateTime).Value = datNGAY_NHAN_VIEC.Text == "" ? datNGAY_NHAN_VIEC.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_NHAN_VIEC.Text);
                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboID_LHDLD.Text == "" ? cboID_LHDLD.EditValue = null : Convert.ToInt64(cboID_LHDLD.EditValue);
                            cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = chkHOAN_THANH_DT.EditValue;
                            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = cboNGUOI_DT.Text == "" ? cboNGUOI_DT.EditValue = null : Convert.ToInt64(cboNGUOI_DT.EditValue);
                            cmd.Parameters.Add("@fCot1", SqlDbType.Float).Value = txtMUC_LUONG_DN.Text == "" ? txtMUC_LUONG_DN.EditValue = null : Convert.ToDouble(txtMUC_LUONG_DN.Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();

                            Commons.Modules.ObjSystems.XoaTable(sBT);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable("sBTUngVien" + Commons.Modules.iIDUser);
            }
        }
        #endregion

        #region function
        private int KiemSLTuyen()
        {
            try
            {
                int Kiem = 0;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "CHUYEN_SANG_NS";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = iID_YCTD;
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = iID_VTTD;
                cmd.CommandType = CommandType.StoredProcedure;
                Kiem = Convert.ToInt32(cmd.ExecuteScalar());
                return Kiem;
            }
            catch
            {
                return 3;
            }
        }

        #endregion
        private void cboID_LHDLD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                txtSO_NGAY.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + cboID_LHDLD.EditValue + "");
            }
            catch { }
        }
        private void ThongTinTiepNhanUV_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
