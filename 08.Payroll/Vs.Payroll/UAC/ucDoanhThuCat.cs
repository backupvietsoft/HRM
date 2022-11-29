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
using Microsoft.Office.Interop.Excel;

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
        private double iTongDoanhThu = 0;
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
                    grvData.Columns["STT"].Visible = false;
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
                    iTongDoanhThu = Convert.ToDouble(dt1.Rows[0][0]);
                    lblTextDoanhThu.Text = "Doanh thu theo ngày : " + dt.Rows[0][0].ToString() + " đồng    Doanh thu tháng : " + Convert.ToDouble(dt1.Rows[0][0]).ToString("#,##0") + " đồng";
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
                string sSql = "SELECT DISTINCT FORMAT(NGAY,'MM/yyyy') THANG FROM DOANH_THU_CAT WHERE ID_TO = " + (cboTo.Text == "" ? 0 : cboTo.EditValue) + " ORDER BY THANG DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG") == null ? DateTime.Now.ToString("MM/yyyy") : Convert.ToDateTime(grvThang1.GetFocusedRowCellValue("THANG")).ToString("MM/yyyy");
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
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
                    case "In":
                        {
                            inDoanhThuCat();
                            break;
                        }
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
                    case "xoa":
                        {
                            if (grvData.RowCount == 0) return;
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.DOANH_THU_CAT WHERE ID = " + grvData.GetFocusedRowCellValue("ID") + "");
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
                            frm.fTongDoanhThu = iTongDoanhThu;
                            if (frm.ShowDialog() == DialogResult.OK)
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
                btnALL.Buttons[2].Properties.Visible = false;
                btnALL.Buttons[3].Properties.Visible = false;
                btnCNCat.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = !visible;
                btnALL.Buttons[1].Properties.Visible = !visible;
                btnALL.Buttons[2].Properties.Visible = !visible;
                btnALL.Buttons[3].Properties.Visible = !visible;
                btnALL.Buttons[4].Properties.Visible = !visible;
                btnALL.Buttons[5].Properties.Visible = visible;
                btnALL.Buttons[6].Properties.Visible = visible;
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
                cboThang.Text = Convert.ToDateTime(grvThang1.GetFocusedRowCellValue("THANG")).ToString("MM/yyyy");
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
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
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
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
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

        private void inDoanhThuCat()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelApplication.DisplayAlerts = true;

                excelApplication.Visible = false;


                System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);


                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                DataTable dt = new DataTable();
                dt = ((DataTable)grdData.DataSource).Copy();
                DataView dv = dt.DefaultView;

                DataTable dt1 = new DataTable();
                dt1 = dv.ToTable(false, "STT", "TEN_KH", "TEN_HH", "SO_LUONG", "DON_GIA", "THANH_TIEN");
                dt1.Columns["TEN_KH"].ColumnName = "Khách hàng";
                dt1.Columns["TEN_HH"].ColumnName = "Mã hàng";
                dt1.Columns["DON_GIA"].ColumnName = "Đơn giá";
                dt1.Columns["SO_LUONG"].ColumnName = "Số lượng";
                dt1.Columns["THANH_TIEN"].ColumnName = "Thành tiền";
                dt1.Columns["STT"].ColumnName = "STT";


                TaoTTChung(excelWorkSheet, 1, 2, 1, 7, 0, 0);

                Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[7, 1], excelWorkSheet.Cells[dt1.Rows.Count + 7, dt1.Columns.Count]];
                Ranges1.Font.Name = "Times New Roman";
                MExportExcel(dt1, excelWorkSheet, Ranges1);

                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[5, 1], excelWorkSheet.Cells[5, 5]];
                Ranges1.Merge();
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 16;
                Ranges1.Font.Bold = true;
                Ranges1.Value = "DOANH THU BỘ PHẬN CẮT THÁNG " + cboThang.Text;
                Ranges1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;



                //FORMAT tiêu đề
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[7, 1], excelWorkSheet.Cells[7, 6]];
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //format cột 1 STT canh giữa
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[7, 1], excelWorkSheet.Cells[dt1.Rows.Count + 7, 1]];
                Ranges1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Ranges1.ColumnWidth = 9;

                //FORMAT cột 4
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[8, 4], excelWorkSheet.Cells[dt1.Rows.Count + 7, 4]];
                Ranges1.NumberFormat = "#,##0;(#,##0); ; ";

                //FORMAT cột 6
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[8, 6], excelWorkSheet.Cells[dt1.Rows.Count + 7, 6]];
                Ranges1.NumberFormat = "#,##0;(#,##0); ; ";


                // canh trái cột 3
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[8, 3], excelWorkSheet.Cells[dt1.Rows.Count + 7, 3]];
                Ranges1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                //set column wid từ cột 2 đến cột 6
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[7, 2], excelWorkSheet.Cells[dt1.Rows.Count + 7, 6]];
                Ranges1.ColumnWidth = 25;

                int rowCnt = 0;
                rowCnt = dt1.Rows.Count + 7;
                rowCnt++;

                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 1], excelWorkSheet.Cells[rowCnt, 3]];
                Ranges1.Merge();
                Ranges1.Value = "Tổng";
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;

                // cột tổng số lượng
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 4], excelWorkSheet.Cells[rowCnt, 4]];
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;
                Ranges1.Value = "=SUBTOTAL(9," + CellAddress(excelWorkSheet, 8, 4) + ":" + CellAddress(excelWorkSheet, rowCnt - 1, 4) + ")";
                Ranges1.NumberFormat = "#,##0;(#,##0); ; ";

                // cột tổng thành tiền
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 6], excelWorkSheet.Cells[rowCnt, 6]];
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;
                Ranges1.Value = "=SUBTOTAL(9," + CellAddress(excelWorkSheet, 8, 6) + ":" + CellAddress(excelWorkSheet, rowCnt - 1, 6) + ")";
                Ranges1.NumberFormat = "#,##0;(#,##0); ; ";


                rowCnt++;

                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 1], excelWorkSheet.Cells[rowCnt, 3]];
                Ranges1.Merge();
                Ranges1.Value = "Lương bình quân/giờ";
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;

                double iSGLV = 0;
                DateTime dtNgayDauThang;
                DateTime dtNgayCuoiThang;
                dtNgayDauThang = new DateTime(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month, 1);
                dtNgayCuoiThang = dtNgayDauThang.AddMonths(1).AddDays(-1);
                try
                {

                    iSGLV = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text,
                        "SELECT ISNULL(ROUND(SUM(T3.SGLV),2),0) SG_LV FROM dbo.LUONG_CONG_NHAN_CAT T1 INNER JOIN dbo.CONG_NHAN CN ON CN.ID_CN = T1.ID_CN INNER JOIN(SELECT CCCT.ID_CN, SUM(CCCT.SG_LV_TT) SGLV " +
                    "FROM dbo.CHAM_CONG_CHI_TIET CCCT INNER JOIN dbo.CHAM_CONG CC ON CC.NGAY = CCCT.NGAY AND CC.ID_CN = CCCT.ID_CN WHERE CCCT.NGAY BETWEEN '" + dtNgayDauThang.ToString("MM/dd/yyyy") + "' AND '" + dtNgayCuoiThang.ToString("MM/dd/yyyy") + "' GROUP BY CCCT.ID_CN) T3 ON T3.ID_CN = T1.ID_CN  WHERE T1.THANG BETWEEN '" + dtNgayDauThang.ToString("MM/dd/yyyy") + "' AND '" + dtNgayCuoiThang.ToString("MM/dd/yyyy") + "'"
                        ));
                }
                catch { }

                // lương 1h
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 6], excelWorkSheet.Cells[rowCnt, 6]];
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;
                Ranges1.Value = "=" + CellAddress(excelWorkSheet, rowCnt - 1, 6) + ":" + CellAddress(excelWorkSheet, rowCnt - 1, 6) + "/" + iSGLV + "";
                Ranges1.NumberFormat = "#,##0;(#,##0); ; ";

                rowCnt++;
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 1], excelWorkSheet.Cells[rowCnt, 3]];
                Ranges1.Merge();
                Ranges1.Value = "Số giờ làm việc";
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;

                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[rowCnt, 6], excelWorkSheet.Cells[rowCnt, 6]];
                Ranges1.Font.Bold = true;
                Ranges1.Font.Name = "Times New Roman";
                Ranges1.Font.Size = 12;
                Ranges1.Value = iSGLV;
                Ranges1.NumberFormat = "#,##0.00;(#,##0.000); ; ";


                // border từ cột 7 đến dòng cuối cùng
                Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[7, 1], excelWorkSheet.Cells[rowCnt, 6]];
                BorderAround(Ranges1);

                this.Cursor = Cursors.Default;
                excelApplication.Visible = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        public string SaveFiles(string MFilter)
        {
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = MFilter;
                f.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        private void MExportExcel(DataTable dtTmp, Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.Range sRange)
        {
            object[,] rawData = new object[dtTmp.Rows.Count + 1, dtTmp.Columns.Count - 1 + 1];
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
                rawData[0, col] = dtTmp.Columns[col].Caption;
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            {
                for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                    rawData[row + 1, col] = dtTmp.Rows[row][col].ToString();
            }
            sRange.Value = rawData;
        }
        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung();
                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
            }
        }
        public void GetImage(byte[] Logo, string sPath, string sFile)
        {
            try
            {
                string strPath = sPath + @"\" + sFile;
                System.IO.MemoryStream stream = new System.IO.MemoryStream(Logo);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(strPath);
            }
            catch (Exception)
            {
            }
        }
        private void BorderAround(Microsoft.Office.Interop.Excel.Range range)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }
        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
    }
}