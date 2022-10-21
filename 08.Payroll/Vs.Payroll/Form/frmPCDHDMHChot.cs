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
using DevExpress.Utils;

namespace Vs.Payroll
{
    public partial class frmPCDHDMHChot : DevExpress.XtraEditors.XtraForm
    {
        int iChuyen = -1;
        int iChuyenSuDung = -1;
        int iHD = -1;
        int iMH = -1;
        int iOrd = -1;
        public int iID_DV = -1;
        private int iAdd = 0;
        public DateTime dThang = Convert.ToDateTime("2014-02-01");
        private int iTinhTrang = 0; // 1 Đang soạn, 2 đã hoàn thành, 3 Đã phát lương
        public frmPCDHDMHChot()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,windowsUIButton);
        }

        private void frmPCDHDMHChot_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
                datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.EditFormat.FormatString = "MM/yyyy";
                datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datThang.Properties.Mask.EditMask = "MM/yyyy";
                datThang.DateTime = dThang;
                LoadCbo();
                LoadLuoi();
                EnabelButton(true);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex) { }
        }
        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPHIEU_CONG_DOAN_CHOT_THANG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dThang;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKHACH_HANG, dt, "ID_DT", "TEN_KH", "TEN_KH");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyenTH, dt, "ID_TO", "TEN_TO", "TEN_TO");

                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                iTinhTrang = Convert.ToInt32(dt.Rows[0]["TINH_TRANG"]);
            }
            catch { }
        }
        private void LoadLuoi()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPHIEU_CONG_DOAN_CHOT_THANG", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(cboKHACH_HANG.EditValue);
            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = Convert.ToInt64(cboChuyenTH.EditValue);
            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
            cmd.Parameters.Add("@iAdd", SqlDbType.Int).Value = iAdd;
            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datThang.DateTime);
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            if (grdData.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, true, true, true, this.Name);
            }
            else
            {
                grdData.DataSource = dt;
            }

            grvData.Columns["SL_CHOT"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["SL_CHOT"].DisplayFormat.FormatString = "N0";

            dt = new DataTable();
            dt = ((DataTable)cboChuyenTH.Properties.DataSource).Copy();
            dt.Rows.RemoveAt(0);
            DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TO", "TEN_TO", "ID_TO", grvData, dt, this.Name);

            dt = new DataTable();
            dt = ((DataTable)cboKHACH_HANG.Properties.DataSource).Copy();
            dt.Rows.RemoveAt(0);
            cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_DT", "TEN_KH", "ID_DT", grvData, dt, this.Name);

            dt = new DataTable();
            dt = ds.Tables[1].Copy();
            DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_ORD", "TEN_HH", "ID_ORD", grvData, dt, this.Name);
            cbo1.BeforePopup += cboID_CN_BeforePopup;
            cbo1.EditValueChanged += cboID_CN_EditValueChanged;
            //grvHD.Columns["ID_CHUYEN_SD"].Visible = false;
            //grvHD.Columns["ID_ORD"].Visible = false;
            //grvHD.Columns["SL_CHOT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //grvHD.Columns["SL_CHOT"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;


        }
        private void cboID_CN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            }
            catch { }

        }
        private void cboID_CN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPHIEU_CONG_DOAN_CHOT_THANG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_DT"));
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = Convert.ToInt64(cboChuyenTH.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datThang.DateTime);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[1].Copy();
                lookUp.Properties.DataSource = dt;
            }
            catch { }
        }
        private void EnabelButton(bool visble)
        {
            if(iTinhTrang == 3)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
            }
            else
            {
                windowsUIButton.Buttons[0].Properties.Visible = visble;
                windowsUIButton.Buttons[1].Properties.Visible = visble;
            }
            windowsUIButton.Buttons[2].Properties.Visible = visble;
            windowsUIButton.Buttons[3].Properties.Visible = !visble;
            windowsUIButton.Buttons[4].Properties.Visible = !visble;
            grvData.OptionsBehavior.Editable = !visble;
            cboKHACH_HANG.Properties.ReadOnly = !visble;
            cboChuyenTH.Properties.ReadOnly = !visble;
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            iAdd = 1;
                            LoadLuoi();
                            EnabelButton(false);
                            //Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                            break;
                        }
                    case "xoa":
                        {
                            break;
                        }
                    case "luu":
                        {
                            if (!SaveData())
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            iAdd = 0;
                            LoadLuoi();
                            EnabelButton(true);
                            break;
                        }
                    case "khongluu":
                        {
                            iAdd = 0;
                            LoadLuoi();
                            EnabelButton(true);
                            //Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch { }
        }

        private void cboKHACH_HANG_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadLuoi();
        }

        private void cboChuyenTH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadLuoi();
        }
        private bool SaveData()
        {
            string sBT = "sBTPCDChotThang" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPHIEU_CONG_DOAN_CHOT_THANG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dThang;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return false;
            }
        }
    }
}