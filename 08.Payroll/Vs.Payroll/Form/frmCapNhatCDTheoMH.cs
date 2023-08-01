using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace Vs.Payroll
{
    public partial class frmCapNhatCDTheoMH : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_DV = -1;
        public Int64 iID_CHUYEN_SD = -1;
        public Int64 iID_ORD = -1;
        public Int64 iID_CD = 0; // ID_CD = 0 thêm mới, ngược lại là dữ liệu cũ
        public DataTable dtTemp;
        public DateTime datThang = DateTime.Now;
        public frmCapNhatCDTheoMH()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmCapNhatCDTheoMH_Load(object sender, EventArgs e)
        {
            LoadChuyen();
            cboChuyen.EditValue = iID_CHUYEN_SD;
            LoadData();
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        grvData.CloseEditor();
                        grvData.UpdateCurrentRow();
                        DataTable dt_CHON = new DataTable();
                        dt_CHON = ((DataTable)grdData.DataSource);
                        //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                        if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonMaHang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!KiemTraLuoi(dt_CHON))
                        {
                            return;
                        }
                        if (!SaveData())
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                            return;
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
            }
        }
        private void LoadChuyen()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCboChuyenThucHien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_DV, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboChuyen.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboChuyen.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            }
            catch { }
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatCDTheoMH", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboChuyen.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iID_ORD;
                cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = datThang;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, "");
                    grvData.Columns["ID_TO"].Visible = false;
                    grvData.Columns["ID_ORD"].Visible = false;
                    grvData.Columns["CHON"].Visible = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }

                grvData.OptionsSelection.MultiSelect = true;
                grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch { }
        }
        private bool SaveData()
        {
            try
            {
                string sBTData = "sBTDataCN" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTData, dtTemp, "");
                string sBTDSMH = "sBTDSMH" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTDSMH, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatCDTheoMH", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTData;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBTDSMH;
                cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = datThang;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (Convert.ToString(dt.Rows[0][0]) == "-99")
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool KiemTraLuoi(DataTable dtSource)
        {
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {
                    //Số hợp đồng lao động
                    string sID_TO = dr["ID_TO"].ToString();
                    string sID_ORD = dr["ID_ORD"].ToString();
                    string sMaQL = Convert.ToString(dtTemp.Rows[0]["MaQL"]);
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_TO = " + sID_TO + " AND ID_ORD = " + sID_ORD + " AND MaQL = " + sMaQL + "")) > 0)
                    {

                        string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgMaQLDaTonTai");
                        dr.SetColumnError("TEN_HH", sTenKTra);
                        dr.SetColumnError("TEN_DT", sTenKTra);
                        errorCount++;
                    }
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KiemTrungDL(DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {


                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                {

                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
    }
}