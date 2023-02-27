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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DataTable = System.Data.DataTable;
using DevExpress.Spreadsheet;

namespace Vs.Payroll
{
    public partial class ucKhoaBangLuong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private string ChuoiKT = "";
        public static ucKhoaBangLuong _instance;
        public static ucKhoaBangLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKhoaBangLuong();
                return _instance;
            }
        }
        private int iTinhTrang = 1;
        private double iTongDoanhThu = 0;
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucKhoaBangLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();



        }

        private void ucKhoaBangLuong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                repositoryItemTimeEdit1.Mask.EditMask = "MM/yyyy";

                repositoryItemTimeEdit1.NullText = "MM/yyyy";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "MM/yyyy";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "MM/yyyy";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                LoadThang();
                LoadData();
                Commons.Modules.sLoad = "";
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKhoaBangLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboThang.Text);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    grvData.Columns["ID_DV"].Visible = false;
                    grvData.Columns["THANG"].ColumnEdit = this.repositoryItemTimeEdit1;
                }
                else
                {
                    grdData.DataSource = dt;
                }

                dt = new DataTable();
                dt = ds.Tables[1].Copy();

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TT", "TEN_TT", "ID_TT", grvData, dt, this.Name);
            }
            catch (Exception ex)
            {
                iTinhTrang = 1;
            }
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKhoaBangLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtthang = new DataTable();
                dtthang = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                cboThang.Text = grvThang1.GetFocusedRowCellValue("NAM") == null ? DateTime.Now.ToString("yyyy") : Convert.ToDateTime(grvThang1.GetFocusedRowCellValue("NAM")).ToString("yyyy");
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("yyyy");
            }
        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch { }
        }

        private bool Savedata()
        {
            string sTB = "sBTDoanhThuCat" + Commons.Modules.iIDUser;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDoanhThuCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = Convert.ToString(grvThang1.GetFocusedRowCellValue("NAM"));
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("yyyy");
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            LoadData();
            Commons.Modules.sLoad = "";
        }
        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }


            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        public DXMenuItem MCreateMenuKhoaBangLuong(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = "";
            if (grvData.GetFocusedRowCellValue("ID_TT").ToString() == "2" && (Commons.Modules.UserName == "admin" || Commons.Modules.UserName == "administrator"))
            {
                sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblMoKhoa", Commons.Modules.TypeLanguage);
            }
            else if (grvData.GetFocusedRowCellValue("ID_TT").ToString() == "1")
            {
                sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblKhoaBangLuong", Commons.Modules.TypeLanguage);
            }
            DXMenuItem menuKhoaBangLuong = new DXMenuItem(sStr, new EventHandler(KhoaBangLuong));
            menuKhoaBangLuong.Tag = new RowInfo(view, rowHandle);
            return menuKhoaBangLuong;
        }
        public void KhoaBangLuong(object sender, EventArgs e)
        {
            try
            {
                DialogResult res;
                if (grvData.GetFocusedRowCellValue("ID_TT").ToString() == "2" && (Commons.Modules.UserName == "admin" || Commons.Modules.UserName == "administrator"))
                {
                    res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacMuonMoKhoaKhoaBangLuongKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacMuonKhoaBangLuongKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (res == DialogResult.No) return;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKhoaBangLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(grvData.GetFocusedRowCellValue("THANG"));
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@ID_TT", SqlDbType.Int).Value = Convert.ToInt32(grvData.GetFocusedRowCellValue("ID_TT"));
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtthang = new DataTable();
                dtthang = ds.Tables[0].Copy();
                if (dtthang.Rows[0][0].ToString() == "-99")
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                }
                else
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable("sBTKiemSoatLuongSP" + Commons.Modules.iIDUser);
            }
        }
        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (grvData.GetFocusedRowCellValue("ID_TT").ToString() == "2" && (Commons.Modules.UserName != "admin" && Commons.Modules.UserName != "administrator")) return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuKhoaBangLuong(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}