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

namespace Vs.Payroll
{
    public partial class ucTienThuongPhuCap : DevExpress.XtraEditors.XtraUserControl
    {
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
                cmd.Parameters.Add("@ID_TTT", SqlDbType.BigInt).Value = cboThang.EditValue;
                cmd.Parameters.Add("@bCot1", SqlDbType.BigInt).Value = isAdd;
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
                }
                else
                {
                    grdData.DataSource = dt;
                }

                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID", "TEN_TIEN_THUONG", "ID_DM_LTT", grvData, Commons.Modules.ObjSystems.DataLoaiTienThuong(false), this.Name);
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
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboThang, dt, "ID", "THANG", "THANG", "THANG");
            }
            catch
            {

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
                            //try
                            //{
                            //    string sPath = "";
                            //    sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                            //    if (sPath == "") return;
                            //    Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                            //    excelApplication.DisplayAlerts = true;

                            //    excelApplication.Visible = false;


                            //    System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                            //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                            //    Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                            //    object misValue = System.Reflection.Missing.Value;
                            //    Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                            //    excelWorkbook.SaveAs(sPath);

                            //    Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                            //    DataTable dt = new DataTable();
                            //    dt = ((DataTable)grdData.DataSource).Copy();
                            //    dt.DefaultView.RowFilter = "";
                            //    DataView dv = dt.DefaultView;

                            //    DataTable dt1 = new DataTable();
                            //    dt1 = dv.ToTable(false, "MS_CN", "HO_TEN", "TEN_XN", "TEN_TO", "TG_HC", "TG_TC_NT", "TG_TC_CN");
                            //    dt1.Columns["MS_CN"].ColumnName = "MSCN";
                            //    dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                            //    dt1.Columns["TEN_XN"].ColumnName = "Xưởng/Phòng ban";
                            //    dt1.Columns["TEN_TO"].ColumnName = "Tổ";
                            //    dt1.Columns["TG_HC"].ColumnName = "Giờ hành chính";
                            //    dt1.Columns["TG_TC_NT"].ColumnName = "Giờ tăng ca ngày thường";
                            //    dt1.Columns["TG_TC_CN"].ColumnName = "Giờ tăng ca chủ nhật";
                            //    Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                            //    Ranges1.Range["A1:G1"].Font.Bold = true;
                            //    Ranges1.Range["A1:G1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            //    Ranges1.Range["A1:G1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            //    Ranges1.WrapText = true;
                            //    Ranges1.ColumnWidth = 20;
                            //    Ranges1.Range["B1"].ColumnWidth = 30;
                            //    Ranges1.Range["E2:E" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            //    Ranges1.Range["F2:F" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            //    Ranges1.Range["G2:G" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            //    BorderAround(Ranges1.Range["A1:G" + (dt1.Rows.Count + 1) + ""]);
                            //    MExportExcel(dt1, excelWorkSheet, Ranges1);

                            //    excelApplication.Visible = true;
                            //    excelWorkbook.Save();
                            //}
                            //catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

                            try
                            {
                                System.Data.SqlClient.SqlConnection conn;
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetlistDK_TG_KHONG_LAM_SP", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDonVi.EditValue;
                                cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = cboXiNghiep.EditValue;
                                cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = cboTo.EditValue;
                                cmd.Parameters.Add("@THEM", SqlDbType.Int).Value = 2;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                ds.Tables[0].TableName = "KhongLamSP";
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                saveFileDialog.FilterIndex = 0;
                                saveFileDialog.RestoreDirectory = true;
                                //saveFileDialog.CreatePrompt = true;
                                saveFileDialog.CheckFileExists = false;
                                saveFileDialog.CheckPathExists = false;
                                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                saveFileDialog.Title = "Export Excel File To";
                                DialogResult res = saveFileDialog.ShowDialog();
                                // If the file name is not an empty string open it for saving.
                                if (res == DialogResult.OK)
                                {
                                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateKhongLamRaSP.xlsx", ds, new string[] { "{", "}" });
                                    Process.Start(saveFileDialog.FileName);
                                }
                            }
                            catch (Exception EX
                            )
                            {

                            }



                            break;
                        }
                    case "import":
                        {
                            //DataTable dt_old = new DataTable();
                            //dt_old = (DataTable)grdData.DataSource;
                            //string sBT_Old = "sBTCongNhanOld" + Commons.Modules.iIDUser;
                            //string sBT_import = "sBTCongNhanImport" + Commons.Modules.iIDUser;
                            //string sPath = "";
                            //sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");

                            //DataTable dt = new DataTable();
                            //if (sPath == "") return;
                            //try
                            //{
                            //    //Lấy đường dẫn
                            //    var source = new ExcelDataSource();
                            //    source.FileName = sPath;

                            //    //Lấy worksheet
                            //    DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
                            //    string ext = System.IO.Path.GetExtension(sPath);
                            //    if (ext.ToLower() == ".xlsx")
                            //        workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                            //    else
                            //        workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xls);
                            //    List<string> wSheet = new List<string>();
                            //    for (int i = 0; i < workbook.Worksheets.Count; i++)
                            //    {
                            //        wSheet.Add(workbook.Worksheets[i].Name.ToString());
                            //    }
                            //    //Load worksheet
                            //    XtraInputBoxArgs args = new XtraInputBoxArgs();
                            //    // set required Input Box options
                            //    args.Caption = "Chọn sheet cần nhập dữ liệu";
                            //    args.Prompt = "Chọn sheet cần nhập dữ liệu";
                            //    args.DefaultButtonIndex = 0;

                            //    // initialize a DateEdit editor with custom settings
                            //    ComboBoxEdit editor = new ComboBoxEdit();
                            //    editor.Properties.Items.AddRange(wSheet);
                            //    editor.EditValue = wSheet[0].ToString();

                            //    args.Editor = editor;
                            //    // a default DateEdit value
                            //    args.DefaultResponse = wSheet[0].ToString();
                            //    // display an Input Box with the custom editor
                            //    var result = XtraInputBox.Show(args);
                            //    if (result == null || result.ToString() == "") return;


                            //    var worksheetSettings = new ExcelWorksheetSettings(result.ToString());
                            //    source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                            //    source.Fill();
                            //    dt = new DataTable();
                            //    dt = ToDataTable(source);
                            //    if (dt == null) return;
                            //    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_Old, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                            //    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_import, dt, "");

                            //    DateTime dNgay;
                            //    //dNgay = DateTime.ParseExact(cboThang.Text, "dd/MM/yyyy", cultures);

                            //    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            //    conn.Open();

                            //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportDKTGKhongLamSP", conn);

                            //    cmd.Parameters.Add("@sBT_Old", SqlDbType.NVarChar, 50).Value = sBT_Old;
                            //    cmd.Parameters.Add("@sBT_Import", SqlDbType.NVarChar, 50).Value = sBT_import;
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            //    DataSet ds = new DataSet();
                            //    adp.Fill(ds);
                            //    DataTable dt_temp = new DataTable();
                            //    dt_temp = ds.Tables[0].Copy();
                            //    //dt_temp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spImportDKLT", sBT_Old, sBT_import, SBT_grvLamThem));
                            //    grdData.DataSource = dt_temp;
                            //    Commons.Modules.ObjSystems.XoaTable(sBT_Old);
                            //    Commons.Modules.ObjSystems.XoaTable(sBT_import);
                            //    //DataTable dtTemp2 = new DataTable();
                            //    //dtTemp2 = dt_temp.Copy();


                            //    //grvCongNhan_FocusedRowChanged(null, null);

                            //    //ColName = cboCotLayDL.EditValue.ToString();
                            //    //dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                            //    ////grdChung.DataSource = dtemp;

                            //    ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                            //    //this.DialogResult = DialogResult.OK;
                            //    //this.Close();
                            //}
                            //catch (Exception ex)
                            //{
                            //    Commons.Modules.ObjSystems.XoaTable(sBT_Old);
                            //    Commons.Modules.ObjSystems.XoaTable(sBT_import);
                            //    XtraMessageBox.Show(ex.Message);
                            //}

                            //frmImportDangKyKLSP frm = new frmImportDangKyKLSP();
                            //if (frm.ShowDialog() == DialogResult.OK)
                            //{
                            //    LoadData();
                            //}
                            //else
                            //{
                            //    LoadData();
                            //}

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
                            XoaCheDoLV();
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

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                //string sSql = "DELETE dbo.DK_TG_KHONG_LAM_SP WHERE ID_CN = " + grvData.GetFocusedRowCellValue("ID_CN") +
                //                                        " AND NGAY = '"
                //                                        + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'";
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                //grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveDK_TG_KHONG_LAM_SP", sTB);
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "CapNhatGio", Commons.Modules.TypeLanguage);
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
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["TEN_XN"].ReadOnly = true;
                dt.Columns["TEN_TO"].ReadOnly = true;
                grdData.DataSource = dt;
            }
            catch { }
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
                col = 0;

                if (!KiemDuLieuSo(grvData, dr, "COT_1", grvData.Columns["COT_1"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_2", grvData.Columns["COT_2"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_3", grvData.Columns["COT_3"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_4", grvData.Columns["COT_4"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_5", grvData.Columns["COT_5"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_6", grvData.Columns["COT_6"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_7", grvData.Columns["COT_7"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_HC", grvData.Columns["TG_HC"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_TC_CN", grvData.Columns["TG_TC_CN"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_TC_CN", grvData.Columns["TG_TC_CN"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
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
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
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
        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
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
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Trim().Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                        dr.SetColumnError(sCot, sTenKTra);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        #endregion

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            cboTinhTrang.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(TRANG_THAI,0) TINH_TRANG FROM dbo.DM_TIEN_THUONG_THANG WHERE ID = " + (cboThang.Text == "" ? -1 : Convert.ToInt32(cboThang.EditValue)) + ""));
            Commons.Modules.sLoad = "";
        }

        private void cboTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            EnableButon(false, Convert.ToInt32(cboTinhTrang.EditValue));
        }
    }
}