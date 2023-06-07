using DevExpress.DataAccess.Excel;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class ucQuyDinhHQSX : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucQuyDinhHQSX _instance;
        public static ucQuyDinhHQSX Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyDinhHQSX();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucQuyDinhHQSX()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucQuyDinhHQSX_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadCboDonvi();
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
                LoadThang();
                LoadGrdHQSX();
                Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                EnableButon(isAdd);
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            }
            catch { }
        }

        private void LoadCboDonvi()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, dt, "ID_DV", "TEN_DV", "TEN_DV");
        }
        private void LoadGrdHQSX()
        {
            try
            {


                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQuyDinhHQSX", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue),
                                       cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, chkNhinNuoc.EditValue, 1));

                dt.Columns["ID"].ReadOnly = false;
                dt.AcceptChanges();

                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatString = "N0";


                }
                else
                {
                    grdData.DataSource = dt;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatString = "N0";
                }
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TO", "TEN_TO", "ID_TO", grvData, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), false), this.Name);
            }
            catch (Exception ex)
            {

            }
            //////grvData.Columns["ID_GTGC"].Visible = false;
            //////grvData.Columns["THANG"].Visible = false;
            //////grvData.Columns["ID_CN"].Visible = false;
            //////grvData.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
            //////grvData.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
            //grvData.Columns["THANG"].Visible = false;
            //grvData.Columns["TT"].Visible = false;

        }
        public void LoadThang()
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListQuyDinhHQSX", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVI", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXiNghiep.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dt, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
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
                            this.Cursor = Cursors.WaitCursor;
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
                            dt = ((DataTable)grdData.DataSource).Clone();
                            dt.DefaultView.RowFilter = "";
                            DataView dv = dt.DefaultView;

                            DataTable dt1 = new DataTable();
                            dt1 = dv.ToTable(false, "ID_TO", "THANG_TINH", "MUC_CHI_TIEU_TU", "MUC_CHI_TIEU_DEN", "PT_HUONG", "CHUYEN_CAN_TU", "CHUYEN_CAN_DEN", "XEP_LOAI", "LOAI_THUONG_HQSX");
                            dt1.Columns["ID_TO"].ColumnName = "Chuyền/Phòng";
                            dt1.Columns["THANG_TINH"].ColumnName = "Tháng tính";
                            dt1.Columns["MUC_CHI_TIEU_TU"].ColumnName = "Mức chỉ tiêu từ";
                            dt1.Columns["MUC_CHI_TIEU_DEN"].ColumnName = "Mức chỉ tiêu đến";
                            dt1.Columns["PT_HUONG"].ColumnName = "% Hưởng";
                            dt1.Columns["CHUYEN_CAN_TU"].ColumnName = "Chuyên cần từ";
                            dt1.Columns["CHUYEN_CAN_DEN"].ColumnName = "Chuyên cần đến";
                            dt1.Columns["XEP_LOAI"].ColumnName = "Xếp loại";
                            dt1.Columns["LOAI_THUONG_HQSX"].ColumnName = "Loại thưởng";
                            Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                            Ranges1.Range["A1:I1"].Font.Bold = true;
                            Ranges1.Range["A1:I1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            Ranges1.Range["A1:I1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            Ranges1.ColumnWidth = 20;
                            Ranges1.Range["B1"].ColumnWidth = 30;



                            MExportExcel(dt1, excelWorkSheet, Ranges1);

                            Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[100, 1]];
                            Commons.Modules.ObjSystems.AddDropDownExcel(excelWorkSheet, Ranges1, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), false), "TEN_TO");
                            try
                            {
                                Ranges1.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                            }
                            catch { }

                            this.Cursor = Cursors.Default;
                            excelApplication.Visible = true;
                            excelWorkbook.Save();
                        }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            XtraMessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case "import":
                    {

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
                            Workbook workbook = new Workbook();
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
                            dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                            dt.Columns.Add("ID", System.Type.GetType("System.Int64"));
                            grdData.DataSource = dt;

                            isAdd = true;
                            EnableButon(isAdd);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        break;
                    }
                case "themsua":
                    {
                        isAdd = true;
                        LoadGrdHQSX();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        EnableButon(isAdd);
                        break;

                    }
                case "xoa":
                    {
                        XoaHQSX();
                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"));
                        }
                        isAdd = false;
                        LoadGrdHQSX();

                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadGrdHQSX();
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void EnableButon(bool visible)
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDonVi.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)) == 2)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnALL.Buttons[2].Properties.Visible = false;
                btnALL.Buttons[4].Properties.Visible = false;
                btnALL.Buttons[5].Properties.Visible = false;
                btnALL.Buttons[7].Properties.Visible = false;
                btnALL.Buttons[8].Properties.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = !visible;
                btnALL.Buttons[1].Properties.Visible = !visible;
                btnALL.Buttons[2].Properties.Visible = !visible;
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

        private void XoaHQSX()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuXoa")); return; }
            if (Commons.Modules.ObjSystems.MsgQuestion(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong")) == 0) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.QUY_DINH_THUONG_HQSX WHERE ID = " + grvData.GetFocusedRowCellValue("ID");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuXoa"));
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                grvData.SetFocusedRowCellValue("ID", 0);
                grvData.SetFocusedRowCellValue("THANG_AD", Convert.ToDateTime(cboThang.EditValue));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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
            string sTB = "HQSX_Tam" + Commons.Modules.UserName;
            try
            {
                grvData.CloseEditor();
                grvData.UpdateCurrentRow();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSaveQuyDinhHQSX", sTB, "SAVE", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)));
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
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
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdHQSX();
            EnableButon(isAdd);
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
            LoadGrdHQSX();
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadThang();
            LoadGrdHQSX();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadThang();
            LoadGrdHQSX();
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
        public DXMenuItem MCreateMenuUpdate(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblUpdateQTCN", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(Update));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void Update(object sender, EventArgs e)
        {

            grvData.CloseEditor();
            grvData.UpdateCurrentRow();

            if (cboThang.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sBT = "sBTHQSX" + Commons.Modules.iIDUser;
            try
            {
                //Load worksheet
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblChonChuyenUpDate");
                args.Prompt = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblChonChuyenUpDate");
                args.DefaultButtonIndex = 0;

                // initialize a DateEdit editor with custom settings
                CheckedComboBoxEdit editor = new CheckedComboBoxEdit();
                //editor.Properties.Items.AddRange(wSheet);
                //editor.EditValue = wSheet[0].ToString();
                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(editor, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true);
                editor.SetEditValue(grvData.GetFocusedRowCellValue("ID_TO"));

                args.Editor = editor;
                // a default DateEdit value
                //args.DefaultResponse = chkCboEditChuyen.EditValue;
                // display an Input Box with the custom editor
                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;


                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSaveQuyDinhHQSX", conn);
                cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = result.ToString();
                cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = "UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }

                Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                isAdd = false;
                LoadGrdHQSX();
                EnableButon(isAdd);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCongVuiLongKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible) return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuUpdate(view, irow);
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

        private void chkNhinNuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            DevExpress.DataAccess.Native.Excel.DataView dv_temp = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;

            excelDataSource.SourceOptions = new CsvSourceOptions() { CellRange = "A1:" + "I" + (dv_temp.Count + 6) + "" };
            excelDataSource.SourceOptions.SkipEmptyRows = false;
            excelDataSource.SourceOptions.UseFirstRowAsHeader = true;
            excelDataSource.Fill();
            DevExpress.DataAccess.Native.Excel.DataView dv = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;
            for (int i = 0; i < dv.Count; i++)
            {
                DevExpress.DataAccess.Native.Excel.ViewRow row = dv[i] as DevExpress.DataAccess.Native.Excel.ViewRow;
                foreach (DevExpress.DataAccess.Native.Excel.ViewColumn col in dv.Columns)
                {
                    object val = col.GetValue(row);
                }
            }

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
                            sTenCot = "ID_TO";
                            table.Columns.Add(sTenCot.Trim(), typeof(int));
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "THANG_TINH";
                            table.Columns.Add(sTenCot.Trim(), typeof(int));
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "MUC_CHI_TIEU_TU";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "MUC_CHI_TIEU_DEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "PT_HUONG";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));

                            break;
                        }
                    case 5:
                        {
                            sTenCot = "CHUYEN_CAN_TU";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "CHUYEN_CAN_DEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "XEP_LOAI";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "LOAI_THUONG_HQSX";
                            table.Columns.Add(sTenCot.Trim(), typeof(bool));
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
                    try
                    {
                        if (i == 0)
                        {
                            values[i] = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(ID_TO,0) FROM dbo.[TO] WHERE TEN_TO = N'" + Convert.ToString(props[i].GetValue(item)) + "'"));
                        }
                        else
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                try
                {
                    table.Rows.Add(values);
                }
                catch (Exception ex) { }
            }
            return table;
        }


    }
}