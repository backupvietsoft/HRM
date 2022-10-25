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
    public partial class ucDoanhThuCat : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private string ChuoiKT = "";
        public static ucDoanhThuCat _instance;
        public static ucDoanhThuCat Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDoanhThuCat();
                return _instance;
            }
        }
        private int iTinhTrang = 1;
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucDoanhThuCat()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            Commons.Modules.ObjSystems.ThayDoiNN(this, btnCNCat);

        }

        private void ucDoanhThuCat_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                LoadCboTo();
                LoadThang();
                LoadData();
                EnableButon(isAdd);
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void LoadCboTo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDoanhThuCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");

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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDoanhThuCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.Text == "" ? 0 : Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = isAdd;
                cmd.Parameters.Add("@ID_DT", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, true, true, true, this.Name);
                    grvData.Columns["ID_ORD"].Visible = false;
                    grvData.Columns["ID_DTC"].Visible = false;
                    grvData.Columns["DON_GIA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["DON_GIA"].DisplayFormat.FormatString = "N2";
                    grvData.Columns["THANH_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THANH_TIEN"].DisplayFormat.FormatString = "N2";
                    grvData.Columns["SO_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THANH_TIEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["DON_GIA"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_KH"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_HH"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }
                if (!isAdd)
                {
                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[2].Copy();
                    lblTextDoanhThu.Text = "Doanh thu theo ngày : " + dt.Rows[0][0].ToString() + " đồng    Doanh thu tháng : " + dt1.Rows[0][0].ToString() + " đồng";
                    dt1 = new DataTable();
                    dt1 = ds.Tables[3].Copy();
                    iTinhTrang = 1;
                    iTinhTrang = Convert.ToInt32(dt1.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                iTinhTrang = 1;
            }
            EnableButon(isAdd);
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT NGAY FROM DOANH_THU_CAT WHERE ID_TO = " + (cboTo.Text == "" ? 0 : cboTo.EditValue) + " ORDER BY NGAY DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);

                cboThang.Text = grvThang1.GetFocusedRowCellValue("NGAY") == null ? DateTime.Now.ToString("dd/MM/yyyy") : Convert.ToDateTime(grvThang1.GetFocusedRowCellValue("NGAY")).ToString("dd/MM/yyyy");
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                    case "themsua":
                        {
                            isAdd = true;
                            LoadData();
                            Commons.Modules.ObjSystems.AddnewRow(grvData, false);
                            EnableButon(isAdd);
                            break;

                        }
                    case "ghi":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdData.DataSource;
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            LoadData();
                            EnableButon(isAdd);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            break;
                        }
                    case "khongghi":
                        {
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            isAdd = false;
                            LoadData();
                            EnableButon(isAdd);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch { }
        }
        private void btnCNCat_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "CNToCat":
                        {
                            frmTinhLuongCNToCat frm = new frmTinhLuongCNToCat();
                            frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                            frm.dNgay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                            if(frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
                            }
                            else
                            {
                                LoadData();
                            }
                            break;
                        }
                }
            }
            catch { }
        }
        private void EnableButon(bool visible)
        {
            if (iTinhTrang == 3)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnCNCat.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = !visible;
                btnALL.Buttons[1].Properties.Visible = !visible;
                btnALL.Buttons[2].Properties.Visible = !visible;
                btnALL.Buttons[3].Properties.Visible = visible;
                btnALL.Buttons[4].Properties.Visible = visible;
                btnCNCat.Visible = !visible;
                cboTo.Enabled = !visible;
                cboThang.Enabled = !visible;
                cboDonVi.Enabled = !visible;
                cboXiNghiep.Enabled = !visible;
                grvData.OptionsBehavior.Editable = visible;
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    view.SetFocusedRowCellValue("THANG", cboThang.EditValue);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
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
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.Text == "" ? 0 : Convert.ToInt32(cboTo.EditValue);
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
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "SO_LUONG")
            {
                grvData.SetFocusedRowCellValue("THANH_TIEN", (Convert.ToDouble(grvData.GetFocusedRowCellValue("DON_GIA")) * Convert.ToDouble(grvData.GetFocusedRowCellValue("SO_LUONG"))));
            }

        }
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = Convert.ToDateTime(grvThang1.GetFocusedRowCellValue("NGAY")).ToString("dd/MM/yyyy");
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
                cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            LoadCboTo();
            LoadThang();
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadCboTo();
            LoadThang();
            LoadData();
            //EnableButon(true);
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
        //Nhap ung vien
        public DXMenuItem MCreateMenuNhapUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "CapNhatSoTien", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(CapNhat));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void CapNhat(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvData.FocusedColumn.FieldName;
                if (grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName).ToString() == "") return;
                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, (DataTable)grdData.DataSource, "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
                grdData.DataSource = dt;
            }
            catch { }
        }
        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[3].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName != "TIEN_DO") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuNhapUngVien(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }
        #endregion

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {

        }
        private void grvData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }


    }
}