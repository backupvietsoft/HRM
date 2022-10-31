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
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DataTable = System.Data.DataTable;
using DevExpress.DataAccess.Excel;
using DevExpress.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using Borders = Microsoft.Office.Interop.Excel.Borders;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace Vs.Payroll
{
    public partial class ucTienThuongPhuCap : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        private static bool isAdd = false;
        private string ChuoiKT = "";
        public static ucTienThuongPhuCap _instance;
        public static ucTienThuongPhuCap Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTienThuongPhuCap();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucTienThuongPhuCap()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucTienThuongPhuCap_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadComboThang();
            LoadComboTinhTrang();
            LoadData();
            EnableButon(isAdd, Convert.ToInt32(cboTinhTrang.EditValue));
            Commons.Modules.sLoad = "";
        }

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongPhuCap", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                Int64 iID = -1;
                try
                {
                    iID = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 ID FROM dbo.DM_TIEN_THUONG_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.Text).ToString("MM/dd/yyyy") + "' AND ID_DV = " + cboDonVi.EditValue + ""));
                }
                catch { }
                cmd.Parameters.Add("@ID_TTT", SqlDbType.BigInt).Value = iID;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = isAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_TTCT"].Visible = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }
                if (isAdd)
                {
                    grvData.OptionsBehavior.Editable = true;
                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["NGAY_VAO_LAM"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["NGAY_BAT_DAU_HD"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grvData.OptionsBehavior.Editable = false;
                }
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID", "TEN_TIEN_THUONG", "ID_DM_LTT", grvData, Commons.Modules.ObjSystems.DataLoaiTienThuong(false), this.Name);
                grvData.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvData.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
            }
            catch (Exception ex)
            {

            }

            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatType = FormatType.Numeric;
            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatString = "N0";
        }
        private void LoadComboThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongPhuCap", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, true, true, true, true, this.Name);
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                grvThang.Columns["ID"].Visible = false;
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
            }
        }

        private void LoadComboTinhTrang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongPhuCap", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboTinhTrang, dt, "ID_TT", "TEN_TT", "TEN_TT", "THANG");
            }
            catch
            {

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
                    case "export":
                        {
                            Export();
                            break;
                        }
                    case "import":
                        {
                            if (cboThang.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonThang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            frmImportTienThuongPhuCap frm = new frmImportTienThuongPhuCap();
                            frm.dtThang = Convert.ToDateTime(cboThang.Text);
                            frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadComboThang();
                                LoadData();
                            }
                            else
                            {
                                LoadComboThang();
                                LoadData();
                            }
                            break;
                        }
                    case "themsua":
                        {
                            isAdd = true;
                            LoadData();
                            EnableButon(isAdd, 0);
                            break;

                        }
                    case "xoa":
                        {
                            XoaTienThuongPC();
                            break;
                        }
                    case "ghi":
                        {
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdData.DataSource;
                            if (!KiemTraLuoi(dt)) return;
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            LoadData();
                            EnableButon(isAdd, 0);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            break;
                        }
                    case "khongghi":
                        {
                            isAdd = false;
                            LoadData();
                            EnableButon(isAdd, 0);
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

        private void EnableButon(bool visible, int iTinhTrang)
        {
            if (iTinhTrang == 1)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnALL.Buttons[2].Properties.Visible = false;

                btnALL.Buttons[3].Properties.Visible = false;
                btnALL.Buttons[4].Properties.Visible = false;
                btnALL.Buttons[5].Properties.Visible = false;
                btnALL.Buttons[6].Properties.Visible = true;

                btnALL.Buttons[7].Properties.Visible = false;
                btnALL.Buttons[8].Properties.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = visible;
                btnALL.Buttons[1].Properties.Visible = !visible;
                btnALL.Buttons[2].Properties.Visible = visible;

                btnALL.Buttons[3].Properties.Visible = !visible;
                btnALL.Buttons[4].Properties.Visible = !visible;
                btnALL.Buttons[5].Properties.Visible = !visible;
                btnALL.Buttons[6].Properties.Visible = !visible;

                btnALL.Buttons[7].Properties.Visible = visible;
                btnALL.Buttons[8].Properties.Visible = visible;
                cboTo.Enabled = !visible;
                cboThang.Enabled = !visible;
                cboDonVi.Enabled = !visible;
                cboXiNghiep.Enabled = !visible;
            }
            cboTinhTrang.Properties.ReadOnly = true;
        }

        private void XoaTienThuongPC()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }


            DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgDeletTienThuongPhuCap"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                string sBT = "sBTTLPC" + Commons.Modules.iIDUser;
                try
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTLPC" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                    string sSql = "DELETE FROM dbo.DM_TIEN_THUONG_THANG_CT FROM dbo.DM_TIEN_THUONG_THANG_CT T1 INNER JOIN " + sBT + " T2 ON T1.ID = T2.ID_TTCT";

                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    Commons.Modules.ObjSystems.XoaTable(sBT);
                    LoadData();
                }
                catch(Exception EX)
                {
                    Commons.Modules.ObjSystems.XoaTable(sBT);
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                }

                try
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DM_TIEN_THUONG_THANG_CT WHERE ID_TTT = " + grvThang.GetFocusedRowCellValue("ID") + "")) == 0)
                    {
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DM_TIEN_THUONG_THANG WHERE ID = " + grvThang.GetFocusedRowCellValue("ID") + "");
                        LoadComboThang();
                    }
                }
                catch { }
            }
            else if (res == DialogResult.No)
            {
                try
                {
                    string sSql = "DELETE FROM dbo.DM_TIEN_THUONG_THANG_CT WHERE ID = " + grvData.GetFocusedRowCellValue("ID_TTCT") + "";

                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    grvData.DeleteSelectedRows();
                }
                catch
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                }

                try
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DM_TIEN_THUONG_THANG_CT WHERE ID_TTT = " + grvThang.GetFocusedRowCellValue("ID") + "")) == 0)
                    {
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DM_TIEN_THUONG_THANG WHERE ID = " + grvThang.GetFocusedRowCellValue("ID") + "");
                        LoadComboThang();
                    }
                }
                catch { }
            }
            else
            {
                return;
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
            string sTB = "LK_Tam" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongPhuCap", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                Int64 iID = -1;
                try
                {
                    iID = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 ID FROM dbo.DM_TIEN_THUONG_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.Text).ToString("MM/dd/yyyy") + "' AND ID_DV = " + cboDonVi.EditValue + ""));
                }
                catch { }
                cmd.Parameters.Add("@ID_TTT", SqlDbType.BigInt).Value = iID;
                cmd.Parameters.Add("@dNgay", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.Text);
                cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = Convert.ToInt32(cboTinhTrang.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadComboThang();
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "CapNhatAll", Commons.Modules.TypeLanguage);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai", sBTCongNhan, sCotCN, grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName)));
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                grdData.DataSource = dt;
            }
            catch (Exception ex) { }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[3].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName == "MS_CN" || grvData.FocusedColumn.FieldName == "HO_TEN" || grvData.FocusedColumn.FieldName == "TEN_XN" || grvData.FocusedColumn.FieldName == "TEN_TO") return;
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
        private void MExportExcel(DataTable dtTmp, Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.Range sRange)
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
        private void BorderAround(Range range)
        {
            Borders borders = range.Borders;
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

        #endregion


        public bool IsNumeric(string input)
        {
            bool IsNumber = true;
            for (int i = 1; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                    IsNumber = false;
                if ((input[i] == '.' && Char.IsDigit(input[i - 1]) && Char.IsDigit(input[i + 1])))
                    IsNumber = true;
            }
            return IsNumber;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        #region kiemTra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                if (!KiemDuLieuSo(grvData, dr, "SO_TIEN", "Số tiền", 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                Double dSoTien = Convert.ToDouble(dr[grvData.Columns["SO_TIEN"].FieldName.ToString()]);
                if(dSoTien != 0)
                {
                    if (!KiemDuLieu(grvData, dr, "ID_DM_LTT", true, 0, this.Name))
                    {
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
        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }
        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }

        #endregion

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            try
            {
                cboTinhTrang.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(TRANG_THAI,0) TINH_TRANG FROM dbo.DM_TIEN_THUONG_THANG WHERE ID = " + (grvThang.GetFocusedRowCellValue("ID") == null ? -1 : Convert.ToInt32(grvThang.GetFocusedRowCellValue("ID"))) + ""));
            }
            catch { }
            Commons.Modules.sLoad = "";
        }

        private void cboTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            EnableButon(false, Convert.ToInt32(cboTinhTrang.EditValue));
        }
        private void Export()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongPhuCap", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = isAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                lastColumn = "Z";
                Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1");
                row2_TieuDe_BaoCao0.Font.Size = 12;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";


                Range row4_TieuDe_Format = oSheet.get_Range("A3", "I3");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Merge();
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Value2 = "TỔNG HỢP TIỀN THƯỞNG ABC, THƯỞNG HIỆU QUẢ CÔNG VIỆC";

                oSheet.get_Range("A5").RowHeight = 45;
                Microsoft.Office.Interop.Excel.Range row4_A = oSheet.get_Range("A5");
                row4_A.ColumnWidth = 5;
                row4_A.Value2 = "STT";

                Range row4_B = oSheet.get_Range("B5");
                row4_B.ColumnWidth = 16;
                row4_B.Value2 = "Mã nhân viên";

                Range row4_C = oSheet.get_Range("C5");
                row4_C.ColumnWidth = 25;
                row4_C.Value2 = "Họ tên";

                Range row4_D = oSheet.get_Range("D5");
                row4_D.ColumnWidth = 40;
                row4_D.Value2 = "Phòng/Chuyền";

                Range row4_E = oSheet.get_Range("E5");
                row4_E.ColumnWidth = 15;
                row4_E.Value2 = "Ngày vào";

                Range row4_F = oSheet.get_Range("F5");
                row4_F.ColumnWidth = 15;
                row4_F.Value2 = "Ngày ký HĐLĐ";

                Range row4_G = oSheet.get_Range("G5");
                row4_G.ColumnWidth = 12;
                row4_G.Value2 = "Xếp loại";

                Range row4_H = oSheet.get_Range("H5");
                row4_H.ColumnWidth = 12;
                row4_H.Value2 = "Tiền thưởng";

                Range row4_I = oSheet.get_Range("I5");
                row4_I.ColumnWidth = 25;
                row4_I.Value2 = "Loại thưởng";

                Range row4_J = oSheet.get_Range("J5");
                row4_J.ColumnWidth = 11;
                row4_J.Value2 = "Ghi chú";

                Range row4_FormatTieuDe = oSheet.get_Range("A5", "J5");
                row4_FormatTieuDe.Font.Size = fontSizeTieuDe;
                row4_FormatTieuDe.Font.Name = fontName;
                row4_FormatTieuDe.Font.Bold = true;
                row4_FormatTieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_FormatTieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                DataRow[] dr = dtBCPhep.Select();
                string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];

                int col = 0;
                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCPhep.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", "J" + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A6", "J" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A6", "J" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                BorderAround(oSheet.get_Range("A5", "J" + rowCnt.ToString()));
                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.get_Range("H6", "H" + rowCnt.ToString());
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                //var list = new System.Collections.Generic.List<string>();
                DataTable dt = new DataTable();
                dt = Commons.Modules.ObjSystems.DataLoaiTienThuong(false);
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    list.Add(dt.Rows[i]["TEN_TIEN_THUONG"].ToString());
                //}
                //var flatList = string.Join(",", list.ToArray());

                //formatRange = oSheet.get_Range("H6", "H" + rowCnt.ToString());
                //formatRange.Validation.Delete();
                //formatRange.Validation.Add(
                //   XlDVType.xlValidateList,
                //   XlDVAlertStyle.xlValidAlertInformation,
                //   XlFormatConditionOperator.xlBetween,
                //   flatList,
                //   Type.Missing);

                //formatRange.Validation.IgnoreBlank = true;
                //formatRange.Validation.InCellDropdown = true;
                //formatRange.Validation.ErrorMessage = "Dữ liệu bạn nhập không đúng bạn có muốn tiếp tục?";
                //formatRange.Validation.ShowError = true;
                //formatRange.Validation.ErrorTitle = "Nhập sai dữ liệu";
                formatRange = oSheet.get_Range("I6", "I" + rowCnt.ToString());
                Commons.Modules.ObjSystems.AddDropDownExcel(oSheet, formatRange, dt, "TEN_TIEN_THUONG");
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
        }

        private void calThangc_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }
    }
}