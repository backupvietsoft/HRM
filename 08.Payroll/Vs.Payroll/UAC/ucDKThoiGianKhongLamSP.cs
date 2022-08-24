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

namespace Vs.Payroll
{
    public partial class ucDKThoiGianKhongLamSP : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucDKThoiGianKhongLamSP _instance;
        public static ucDKThoiGianKhongLamSP Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDKThoiGianKhongLamSP();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucDKThoiGianKhongLamSP()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucDKThoiGianKhongLamSP_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetlistDK_TG_KHONG_LAM_SP", Convert.ToDateTime(cboThang.EditValue),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, isAdd));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                grvData.Columns["ID_CN"].Visible = false;
                grvData.Columns["THANG"].Visible = false;
            }
            catch
            {

            }
            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatType = FormatType.Numeric;
            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatString = "N0";
        }

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.DK_TG_KHONG_LAM_SP ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.Month + "/" + now.Year.ToString();
            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "export":
                    {
                        try
                        {
                            string sPath = "";
                            sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                            if (sPath == "") return;
                            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                            excelApplication.DisplayAlerts = true;

                            excelApplication.Visible = false;


                            System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                            Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                            object misValue = System.Reflection.Missing.Value;
                            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                            excelWorkbook.SaveAs(sPath);

                            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                            DataTable dt = new DataTable();
                            dt = ((DataTable)grdData.DataSource).Copy();
                            dt.DefaultView.RowFilter = "";
                            DataView dv = dt.DefaultView;

                            DataTable dt1 = new DataTable();
                            dt1 = dv.ToTable(false, "MS_CN", "HO_TEN", "TEN_XN", "TEN_TO", "TG_HC", "TG_TC_NT", "TG_TC_CN");
                            dt1.Columns["MS_CN"].ColumnName = "MSCN";
                            dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                            dt1.Columns["TEN_XN"].ColumnName = "Xưởng/Phòng ban";
                            dt1.Columns["TEN_TO"].ColumnName = "Tổ";
                            dt1.Columns["TG_HC"].ColumnName = "Giờ hành chính";
                            dt1.Columns["TG_TC_NT"].ColumnName = "Giờ tăng ca ngày thường";
                            dt1.Columns["TG_TC_CN"].ColumnName = "Giờ tăng ca chủ nhật";
                            Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                            Ranges1.Range["A1:G1"].Font.Bold = true;
                            Ranges1.Range["A1:G1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            Ranges1.Range["A1:G1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            Ranges1.WrapText = true;
                            Ranges1.ColumnWidth = 20;
                            Ranges1.Range["B1"].ColumnWidth = 30;
                            Ranges1.Range["E2:E" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            Ranges1.Range["F2:F" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            Ranges1.Range["G2:G" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                            BorderAround(Ranges1.Range["A1:G" + (dt1.Rows.Count + 1) + ""]);
                            MExportExcel(dt1, excelWorkSheet, Ranges1);

                            excelApplication.Visible = true;
                            excelWorkbook.Save();
                        }
                        catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                        break;
                    }
                case "import":
                    {
                        DataTable dt_old = new DataTable();
                        dt_old = (DataTable)grdData.DataSource;
                        string sBT_Old = "sBTCongNhanOld" + Commons.Modules.iIDUser;
                        string sBT_import = "sBTCongNhanImport" + Commons.Modules.iIDUser;
                        string sPath = "";
                        sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");

                        DataTable dt = new DataTable();
                        if (sPath == "") return;
                        try
                        {
                            //Lấy đường dẫn
                            var source = new ExcelDataSource();
                            source.FileName = sPath;

                            //Lấy worksheet
                            DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
                            string ext = System.IO.Path.GetExtension(sPath);
                            if (ext.ToLower() == ".xlsx")
                                workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                            else
                                workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xls);
                            List<string> wSheet = new List<string>();
                            for (int i = 0; i < workbook.Worksheets.Count; i++)
                            {
                                wSheet.Add(workbook.Worksheets[i].Name.ToString());
                            }
                            //Load worksheet
                            XtraInputBoxArgs args = new XtraInputBoxArgs();
                            // set required Input Box options
                            args.Caption = "Chọn sheet cần nhập dữ liệu";
                            args.Prompt = "Chọn sheet cần nhập dữ liệu";
                            args.DefaultButtonIndex = 0;

                            // initialize a DateEdit editor with custom settings
                            ComboBoxEdit editor = new ComboBoxEdit();
                            editor.Properties.Items.AddRange(wSheet);
                            editor.EditValue = wSheet[0].ToString();

                            args.Editor = editor;
                            // a default DateEdit value
                            args.DefaultResponse = wSheet[0].ToString();
                            // display an Input Box with the custom editor
                            var result = XtraInputBox.Show(args);
                            if (result == null || result.ToString() == "") return;


                            var worksheetSettings = new ExcelWorksheetSettings(result.ToString());
                            source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                            source.Fill();
                            dt = new DataTable();
                            dt = ToDataTable(source);
                            if (dt == null) return;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_Old, (DataTable)grdData.DataSource, "");
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_import, dt, "");

                            DateTime dNgay;
                            //dNgay = DateTime.ParseExact(cboThang.Text, "dd/MM/yyyy", cultures);

                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportDKTGKhongLamSP", conn);

                            cmd.Parameters.Add("@sBT_Old", SqlDbType.NVarChar, 50).Value = sBT_Old;
                            cmd.Parameters.Add("@sBT_Import", SqlDbType.NVarChar, 50).Value = sBT_import;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt_temp = new DataTable();
                            dt_temp = ds.Tables[0].Copy();
                            //dt_temp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spImportDKLT", sBT_Old, sBT_import, SBT_grvLamThem));
                            grdData.DataSource = dt_temp;
                            Commons.Modules.ObjSystems.XoaTable(sBT_Old);
                            Commons.Modules.ObjSystems.XoaTable(sBT_import);
                            //DataTable dtTemp2 = new DataTable();
                            //dtTemp2 = dt_temp.Copy();


                            //grvCongNhan_FocusedRowChanged(null, null);

                            //ColName = cboCotLayDL.EditValue.ToString();
                            //dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                            ////grdChung.DataSource = dtemp;

                            ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                            //this.DialogResult = DialogResult.OK;
                            //this.Close();
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable(sBT_Old);
                            Commons.Modules.ObjSystems.XoaTable(sBT_import);
                            XtraMessageBox.Show(ex.Message);
                        }
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
                case "xoa":
                    {
                        XoaCheDoLV();
                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        isAdd = false;
                        LoadData();
                        LoadThang();
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
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "MS_CN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "HO_TEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "TEN_XN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "TEN_TO";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "TG_HC";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));

                            break;
                        }
                    case 5:
                        {
                            sTenCot = "TG_TC_NT";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));

                            break;
                        }
                    case 6:
                        {
                            sTenCot = "TG_TC_CN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 4 || i == 5 || i == 6)
                    {
                        try
                        {
                            //object s =  GetPropValue(props[i].GetValue(item), string.IsNullOrEmpty(Convert.ToString(props[i].GetValue(item))) ? "abc" : Convert.ToString(props[i].GetValue(item)));
                            //if ((props[i].GetValue(item) == null ? typeof(object) :props[i].GetValue(item).GetType()) == typeof(string))
                            //{
                            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return null;
                            //}
                            values[i] = Convert.ToDouble(props[i].GetValue(item));
                        }
                        catch
                        {
                            //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //return null;
                        }
                    }
                    else
                    {
                        values[i] = props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
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
            grvData.OptionsBehavior.Editable = visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.DK_TG_KHONG_LAM_SP WHERE ID_CN = " + grvData.GetFocusedRowCellValue("ID_CN") +
                                                        " AND THANG = '"
                                                        + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDK_TG_KHONG_LAM_SP", sTB);
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


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucDKThoiGianKhongLamSP", "CapNhatGio", Commons.Modules.TypeLanguage);
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
                if (grvData.FocusedColumn.FieldName.Substring(0, 3) != "TG_") return;
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
                if((input[i] == '.' && Char.IsDigit(input[i - 1]) && Char.IsDigit(input[i + 1])))
                    IsNumber = true;
            }
            return IsNumber;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}