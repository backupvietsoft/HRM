using DevExpress.DataAccess.Excel;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using System.Threading;

namespace Vs.HRM
{
    public partial class frmImportNhanSu : DevExpress.XtraEditors.XtraForm
    {
        string fileName = "";
        Point ptChung;
        string ChuoiKT = "";
        DataTable _table = new DataTable();
        DataTable dtemp;
        string sCheck = "";
        public frmImportNhanSu()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }
        private void frmImportNhanSu_Load(object sender, EventArgs e)
        {
            try
            {
                sCheck = Commons.Modules.ObjSystems.getCheckImport();
                if (sCheck != "")
                {
                    string[] sArray = sCheck.Split(',');
                    DateTime datOld;
                    datOld = Convert.ToDateTime(sArray[0]).AddHours(1);
                    DateTime datCurren = DateTime.Now;
                    try
                    {
                        datCurren = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT GETDATE()"));
                    }
                    catch { }
                    if (datOld < datCurren)
                    {
                        Commons.Modules.ObjSystems.setCheckImport(0);
                    }
                    else
                    {
                        XtraMessageBox.Show("User " + sArray[2] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgdangimportdulieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
                if (!Commons.Modules.ObjSystems.setCheckImport(1)) this.Close();
            }
            catch { }
        }

        private void btnFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    OpenFileDialog oFile = new OpenFileDialog();
            //    oFile.Filter = "All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*";
            //    if (oFile.ShowDialog() != DialogResult.OK) return;

            //    fileName = oFile.FileName;
            //    btnFile.Text = fileName;
            //    if (!System.IO.File.Exists(fileName)) return;

            //    if (Commons.Modules.MExcel.MGetSheetNames(fileName, cboChonSheet))
            //    {
            //        cboChonSheet_EditValueChanged(null, null);
            //    }
            //    else
            //    {
            //        grdData.DataSource = null;
            //        cboChonSheet.Properties.DataSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}
            string sPath = "";
            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");
            if (sPath == "") return;
            btnFile.Text = sPath;
            try
            {
                cboChonSheet.Properties.DataSource = null;
                Workbook workbook = new Workbook();

                string ext = System.IO.Path.GetExtension(sPath);
                if (ext.ToLower() == ".xlsx")
                    workbook.LoadDocument(btnFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                else
                    workbook.LoadDocument(btnFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xls);
                List<string> wSheet = new List<string>();
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    wSheet.Add(workbook.Worksheets[i].Name.ToString());
                }
                cboChonSheet.Properties.DataSource = wSheet;
                //cboChonSheet.Properties.Items.AddRange(wSheet);
                Commons.Modules.sLoad = "0Load";
                cboChonSheet.EditValue = wSheet[0].ToString();
                Commons.Modules.sLoad = "";
                cboChonSheet_EditValueChanged(null, null);
                ////grdChung.DataSource = dtemp;

                ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void cboChonSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            string sBT = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                DataTable dt = new DataTable();
                var source = new ExcelDataSource();
                source.FileName = btnFile.Text;
                var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text);
                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                source.Fill();
                dt = new DataTable();
                dt = ToDataTable(source);
                dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));

                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");
                //dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCreateMSImport", sBT));
                //Commons.Modules.ObjSystems.XoaTable(sBT);

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                grvData.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvData.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvData.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvData.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grdData.DataSource = dt;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                grdData.DataSource = null;
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {

                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                //Commons.Modules.ObjSystems.ShowWaitForm(this);
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
                                excelWorkSheet.Name = "01 - Thông tin nhân viên";

                                DataTable dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spImportNhanSu", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                                //dt = ((DataTable)grdData.DataSource).Copy();
                                string lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                string fontName = "Time News Roman";
                                int fontSizeTieuDe = 13;
                                int fontSizeNoiDung = 9;

                                dt.DefaultView.RowFilter = "";
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    //dt.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt.Columns[i].ColumnName.ToString()); ;
                                    dt.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt.Columns[i].ColumnName.ToString());
                                }
                                Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                MExportExcel(dt, excelWorkSheet, Ranges1);

                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet1.Name = "02 - Quốc Gia";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT MA_QG AS N'Mã quốc gia', TEN_QG AS N'Tên quốc gia' FROM dbo.QUOC_GIA"));
                                Ranges1 = excelWorkSheet1.Range[excelWorkSheet1.Cells[1, 1], excelWorkSheet1.Cells[dt.Rows.Count + 1, dt.Columns.Count]];

                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                MExportExcel(dt, excelWorkSheet1, Ranges1);

                                //Tổ
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet4 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet4 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet4.Name = "03 - Phòng";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T3.TEN_DV AS N'Tên nhà máy' , T2.TEN_XN AS N'Tên bộ phận' , T1.TEN_TO AS N'Tên Chuyền/Phòng' FROM dbo.[TO] T1 INNER JOIN dbo.XI_NGHIEP T2 ON T2.ID_XN = T1.ID_XN INNER JOIN dbo.DON_VI T3 ON T3.ID_DV = T2.ID_DV"));
                                Ranges1 = excelWorkSheet4.Range[excelWorkSheet4.Cells[1, 1], excelWorkSheet4.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                Microsoft.Office.Interop.Excel.Range myRange = excelWorkSheet4.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet4, Ranges1);

                                //Chức vụ
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet5 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet5 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet5.Name = "04 - Chức vụ";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_CV AS N'Tên chức vụ' FROM dbo.CHUC_VU"));
                                Ranges1 = excelWorkSheet5.Range[excelWorkSheet5.Cells[1, 1], excelWorkSheet5.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet5.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet5, Ranges1);

                                //Coong viec
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet6 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet6 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet6.Name = "05 - Công việc";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_LCV AS N'Tên công việc' FROM dbo.LOAI_CONG_VIEC"));
                                Ranges1 = excelWorkSheet6.Range[excelWorkSheet6.Cells[1, 1], excelWorkSheet6.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet6.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet6, Ranges1);


                                //Tình trạng hợp đồng
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet8 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet8 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet8.Name = "06 - Tình trạng hợp đồng";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_TT_HD AS N'Tên tình trạng HĐ' FROM dbo.TINH_TRANG_HD"));
                                Ranges1 = excelWorkSheet8.Range[excelWorkSheet8.Cells[1, 1], excelWorkSheet8.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet8.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet8, Ranges1);

                                //Tình trạng hiện tại
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet9 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet9 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet9.Name = "07 - Tình trạng nhân sự";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_TT_HT AS N'Tên tình trạng NS' FROM dbo.TINH_TRANG_HT"));
                                Ranges1 = excelWorkSheet9.Range[excelWorkSheet9.Cells[1, 1], excelWorkSheet9.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet9.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet9, Ranges1);

                                //Dân tộc
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet10 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet10 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet10.Name = "08 - Dân tộc";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_DT AS N'Tên dân tộc' FROM dbo.DAN_TOC"));
                                Ranges1 = excelWorkSheet10.Range[excelWorkSheet10.Cells[1, 1], excelWorkSheet10.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet10.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet10, Ranges1);

                                //Tình trạng hôn nhân
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet11 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet11 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet11.Name = "09 - Tình trạng hôn nhân";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_TT_HN AS N'Tên TT hôn nhân' FROM dbo.TT_HON_NHAN"));
                                Ranges1 = excelWorkSheet11.Range[excelWorkSheet11.Cells[1, 1], excelWorkSheet11.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet11.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet11, Ranges1);

                                //Tỉnh
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet12 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet12 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet12.Name = "10 - Tỉnh-Thành phố";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_TP AS N'Tên tỉnh' FROM dbo.THANH_PHO"));
                                Ranges1 = excelWorkSheet12.Range[excelWorkSheet12.Cells[1, 1], excelWorkSheet12.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet12.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet12, Ranges1);

                                //Quận/huyện
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet13 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet13 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet13.Name = "11 - Quận-Huyện";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.TEN_TP AS N'Tên Tỉnh/Thành phố', T1.TEN_QUAN AS N'Tên Quận/Huyện'  FROM dbo.QUAN T1 INNER JOIN dbo.THANH_PHO T2 ON T2.ID_TP = T1.ID_TP"));
                                Ranges1 = excelWorkSheet13.Range[excelWorkSheet13.Cells[1, 1], excelWorkSheet13.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet13.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet13, Ranges1);

                                //Phường/xã
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet14 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet14 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet14.Name = "12 - Phường-Xã";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T3.TEN_TP AS N'Tên Tỉnh/Thành phố', T2.TEN_QUAN AS N'Tên Quận/Huyện', T1.TEN_PX AS N'Tên Phường/Xã'  FROM dbo.PHUONG_XA T1 INNER JOIN dbo.QUAN T2 ON T2.ID_QUAN = T1.ID_QUAN INNER JOIN dbo.THANH_PHO T3 ON T3.ID_TP = T2.ID_TP"));
                                Ranges1 = excelWorkSheet14.Range[excelWorkSheet14.Cells[1, 1], excelWorkSheet14.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet14.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet14, Ranges1);


                                // Loại trình độ
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet15 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet15 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet15.Name = "13 - Loại trình độ";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_LOAI_TD AS N'Tên loại trình độ' FROM dbo.LOAI_TRINH_DO"));
                                Ranges1 = excelWorkSheet15.Range[excelWorkSheet15.Cells[1, 1], excelWorkSheet15.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet15.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet15, Ranges1);

                                // Trình độ
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet16 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet16 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet16.Name = "14 - Trình độ";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT CHAR(13)+ TEN_TDVH AS N'Tên trình độ' FROM dbo.TRINH_DO_VAN_HOA "));
                                Ranges1 = excelWorkSheet16.Range[excelWorkSheet16.Cells[1, 1], excelWorkSheet16.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet16.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet16, Ranges1);


                                // Trình độ
                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet17 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
                                excelWorkSheet17 = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Worksheets.Add(After: excelWorkbook.Sheets[excelWorkbook.Sheets.Count]);
                                excelWorkSheet17.Name = "15 - Lý do thôi việc";
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT CHAR(13)+ TEN_LD_TV AS N'Lý do thôi việc' FROM dbo.LY_DO_THOI_VIEC"));
                                Ranges1 = excelWorkSheet17.Range[excelWorkSheet17.Cells[1, 1], excelWorkSheet17.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                                Ranges1.ColumnWidth = 20;
                                Ranges1.Font.Name = fontName;
                                Ranges1.Font.Size = fontSizeNoiDung;
                                lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                                Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                                Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                myRange = excelWorkSheet17.get_Range("A1", lastColumn + (dt.Rows.Count + 1).ToString());
                                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                                MExportExcel(dt, excelWorkSheet17, Ranges1);

                                excelWorkSheet.Activate();

                                excelApplication.Visible = true;
                                excelWorkbook.Save();
                            }
                            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

                            break;
                        }
                    case "import":
                        {
                            grvData.PostEditor();
                            grvData.UpdateCurrentRow();
                            Commons.Modules.ObjSystems.MChooseGrid(false, "XOA", grvData);
                            DataTable dtSource = Commons.Modules.ObjSystems.ConvertDatatable(grdData);
                            if (cboChonSheet.Text == "" || dtSource == null || dtSource.Rows.Count <= 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            grvData.Columns.View.ClearColumnErrors();
                            ImportUngVien(dtSource);

                            break;
                        }
                    case "xoa":
                        {
                            try
                            {
                                DataTable dtTmp = new DataTable();
                                dtTmp = (DataTable)grdData.DataSource;

                                if (dtTmp == null || dtTmp.Select("XOA = 1").Count() == 0) return;

                                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (res == DialogResult.No) return;

                                dtTmp.AcceptChanges();
                                foreach (DataRow dr in dtTmp.Rows)
                                {
                                    if (dr["XOA"].ToString() == "True")
                                    {
                                        dr.Delete();
                                    }
                                }
                                dtTmp.AcceptChanges();
                            }
                            catch
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            Commons.Modules.ObjSystems.setCheckImport(0); //xoa
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX) {
                XtraMessageBox.Show(EX.Message);
            }
        }
        private bool KiemTrungDL_MTCC(GridView grvData, DataTable dt, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName,string ColName_NGHI  , string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {
                DataTable DTTMP = new DataTable();
                DTTMP = dt.Copy();
                try
                {

                    DTTMP = DTTMP.AsEnumerable().Where(x => x.Field<string>(iCot).Trim().Equals(sDLKiem) && (string.IsNullOrEmpty(Convert.ToString(x[ColName_NGHI])) ? "" : " ") == "").CopyToDataTable();
                }
                catch { DTTMP.Clear(); }
                DTTMP.AcceptChanges();

                if (DTTMP.Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "' AND " + ColName_NGHI +" IS NOT NULL ")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                        dr["XOA"] = 1;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                dr["XOA"] = 1;
                return false;
            }
        }

        #region import ứng viên
        private void ImportUngVien(DataTable dtSource)
        {
            this.Cursor = Cursors.WaitCursor;
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            int errorMS = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Mã số nhân viên
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "CONG_NHAN", "MS_CN", this.Name))
                    {
                        errorCount++;
                        errorMS++;
                    }
                    if (Commons.Modules.KyHieuDV == "DM")
                    {
                        if (dr[grvData.Columns[col].FieldName.ToString()].ToString().Substring(0, 3) != dr[grvData.Columns[8].FieldName.ToString()].ToString().Substring(0, 3))
                        {
                            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDungDinhDang");
                            dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), sTenKTra);
                            dr["XOA"] = 1;
                            errorMS++;
                        }
                    }
                }

                // Mã số thẻ CC
                col = 1;
                string sMS_The_CC = dr[grvData.Columns[col].FieldName.ToString()].ToString().Trim();

                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
   
                    if (!KiemTrungDL_MTCC(grvData, dtSource, dr, col, sMS_The_CC, "CONG_NHAN", "MS_THE_CC","NGAY_NGHI_VIEC", this.Name))
                    {
                        errorCount++;
                        errorMS++;
                    }
                }

                col = 2;
                //Họ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 50, this.Name))
                {
                    errorCount++;
                }
                col = 3;
                
                //Tên 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, Commons.Modules.KyHieuDV == "NB" ? false : true, 20, this.Name))
                {
                    errorCount++;
                }

                // Quốc gia
                col = 4;
                string sQuocGia = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuocGia, "QUOC_GIA", "TEN_QG", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ngày sinh   
                col = 5;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                {
                    errorCount++;
                }

                col = 6;
                //Năm sinh
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Năm sinh ", 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                col = 7;
                //Giới tính
                string sGioiTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sGioiTinh, false))
                {
                    errorCount++;
                }

                //Tổ  
                col = 8;
                string sTo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTo, "[TO]", "TEN_TO", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Chức vụ  
                col = 9;
                string sChucVu = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sChucVu, "CHUC_VU", "TEN_CV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Loại công việc
                col = 10;
                string sLoaiCongViec = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLoaiCongViec, "LOAI_CONG_VIEC", "TEN_LCV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ngày thử việc
                col = 11;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày vào làm
                col = 12;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                {
                    errorCount++;
                }

                //Vào làm lại
                col = 13;
                string sVaoLamLai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sVaoLamLai, false))
                {
                    errorCount++;
                }

                //Tình trạng hợp đồng
                col = 14;
                string sTinhTrangHD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTinhTrangHD, "TINH_TRANG_HD", "TEN_TT_HD", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Tình trạng nhân sự
                col = 15;
                string sTinhTrangHT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTinhTrangHT, "TINH_TRANG_HT", "TEN_TT_HT", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ngày nghỉ việc
                col = 16;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Tình trạng nhân sự
                col = 17;
                string sLyDoThoiViec = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLyDoThoiViec, "LY_DO_THOI_VIEC", "TEN_LD_TV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Hình thức tuyển
                col = 18;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tham gia BHXH
                col = 19;
                string sThamGiaBHXH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sThamGiaBHXH, false))
                {
                    errorCount++;
                }

                //LD Tỉnh
                col = 20;
                string sLDTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sLDTinh, false))
                {
                    errorCount++;
                }

                //Ghi chú
                col = 21;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Dân tộc
                col = 22;
                string sTenDanToc = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTenDanToc, "DAN_TOC", "TEN_DT", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Tôn giáo
                col = 23;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Nơi sinh
                col = 24;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Nguyên quán
                col = 25;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Số CMND
                col = 26;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngày cấp
                col = 27;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Nơi cấp
                col = 28;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tình trạng hôn nhân
                col = 29;
                string sTTHonNhan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTTHonNhan, "TT_HON_NHAN", "TEN_TT_HN", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Mã thẻ ATM
                col = 30;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Số tài khoản
                col = 31;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Mã số thuế
                col = 32;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tên không dấu
                col = 33;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Lao động nước ngoài
                col = 34;
                string sLDNuocNgoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sLDNuocNgoai, false))
                {
                    errorCount++;
                }

                //ĐT di động
                col = 35;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //ĐT nhà
                col = 36;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //ĐT người thân
                col = 37;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Email
                col = 38;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Dia chi thuong tru
                col = 39;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Thành phố
                col = 40;
                string sThanhPho = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPho, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }

                //Quận
                col = 41;
                string sQuan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuan, "QUAN", "TEN_QUAN", false, this.Name))
                {
                    errorCount++;
                }

                //phường xã
                col = 42;
                string sPhuongXa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sPhuongXa, "PHUONG_XA", "TEN_PX", false, this.Name))
                {
                    errorCount++;
                }

                //Thôn xóm
                col = 43;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Địa chỉ tạm trú
                col = 44;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Thành phố tạm trú
                col = 45;
                string sThanhPhoTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPhoTamTru, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }

                //Quận tạm trú
                col = 46;
                string sQuanTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuanTamTru, "QUAN", "TEN_QUAN", false, this.Name))
                {
                    errorCount++;
                }

                //phường xã tạm trú
                col = 47;
                string sPhuongXaTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sPhuongXaTamTru, "PHUONG_XA", "TEN_PX", false, this.Name))
                {
                    errorCount++;
                }

                //Thôn xóm tạm trú
                col = 48;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //số BHXH
                col = 49;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngày đóng BHXH
                col = 50;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //loại trình độ 
                col = 51;

                string sLoaiTrinhDo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLoaiTrinhDo, "LOAI_TRINH_DO", "TEN_LOAI_TD", false, this.Name))
                {
                    errorCount++;
                }

                //Trình độ văn hóa
                col = 52;
                string sTDVanHoa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTDVanHoa, "TRINH_DO_VAN_HOA", "TEN_TDVH", false, this.Name))
                {
                    errorCount++;
                }


                //Chuyên môn
                col = 53;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngoại ngữ
                col = 54;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngân hàng
                col = 55;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Chi nhánh ngân hàng
                col = 56;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
            }
            this.Cursor = Cursors.Default;
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            int errorEmpty = 0;
            int errorExist = 0;
            string sBT = "sBTImport" + Commons.Modules.iIDUser;
            if (Commons.Modules.KyHieuDV == "DM")
            {
                if (errorMS != 0)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCoMaSoCongNhanBiLoiBanCoMuonTaoMaMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    try
                    {
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            if (dtSource.Rows[i][grvData.Columns[8].FieldName.ToString()].ToString() == "")
                            {
                                errorEmpty++;
                            }
                            else
                            {
                                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[TO] A WHERE A.[TEN_TO] = N'" + dtSource.Rows[i][grvData.Columns[8].FieldName.ToString()].ToString() + "'")) == 0)
                                {
                                    errorExist++;
                                }
                            }
                        }
                        if (errorEmpty != 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanPhaiChonToTruoc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (errorExist != 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTenToKhongTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string sMaMaxDMS = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(1, 1)").ToString();
                        string sMaMaxDMT = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(2, 1)").ToString();

                        int iMaTemp = 0;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            string sMaSo = dtSource.Rows[i][grvData.Columns[0].FieldName.ToString()].ToString();
                            string sHo = dtSource.Rows[i][grvData.Columns[2].FieldName.ToString()].ToString();
                            string sTen = dtSource.Rows[i][grvData.Columns[3].FieldName.ToString()].ToString();
                            string sNgaySinh = Convert.ToDateTime(dtSource.Rows[i][grvData.Columns[5].FieldName.ToString()]).ToString("MM/dd/yyyy");
                            string sMaSoTheCC = dtSource.Rows[i][grvData.Columns[1].FieldName.ToString()].ToString();
                            string sGetID_DV = "SELECT T1.ID_DV FROM dbo.DON_VI T1 INNER JOIN dbo.XI_NGHIEP T2 ON T2.ID_DV = T1.ID_DV INNER JOIN dbo.[TO] T3 ON T3.ID_XN = T2.ID_XN WHERE T3.TEN_TO = N'" + dtSource.Rows[i][grvData.Columns[8].FieldName.ToString()].ToString() + "'";
                            int iID_DV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sGetID_DV));

                            if (dtSource.AsEnumerable().Where(x => x.Field<string>(grvData.Columns[0].FieldName.ToString()).Trim().Equals(sMaSo)).CopyToDataTable().Rows.Count > 1)
                            {
                                string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaMaxDMS + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaSo + "' AND A.[" + grvData.Columns[2].FieldName.ToString() + "] = N'" + sHo + "' AND A.[" + grvData.Columns[3].FieldName.ToString() + "] = N'" + sTen + "' AND A.[" + grvData.Columns[5].FieldName.ToString() + "] = '" + sNgaySinh + "'";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                dtSource.Rows[i][0] = sMaMaxDMS;
                                i--;
                            }
                            else
                            {
                                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[CONG_NHAN] WHERE MS_CN = N'" + sMaSo + "'")) > 0)
                                {
                                    if (iID_DV == 1)
                                    {
                                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + sBT + "] A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaMaxDMS + "'")) > 0)
                                        {
                                            iMaTemp = Convert.ToInt32(sMaMaxDMS.Substring(3, sMaMaxDMS.Length - 3)) + 1;
                                            sMaMaxDMS = sMaMaxDMS.Substring(0, sMaMaxDMS.Length - (iMaTemp.ToString().Length)) + iMaTemp.ToString();
                                            i--;
                                        }
                                        else
                                        {
                                            string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaMaxDMS + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaSo + "'";
                                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                            iMaTemp = Convert.ToInt32(sMaMaxDMS.Substring(3, sMaMaxDMS.Length - 3)) + 1;
                                            sMaMaxDMS = sMaMaxDMS.Substring(0, sMaMaxDMS.Length - (iMaTemp.ToString().Length)) + iMaTemp.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + sBT + "] A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaMaxDMT + "'")) > 0)
                                        {
                                            iMaTemp = Convert.ToInt32(sMaMaxDMT.Substring(3, sMaMaxDMT.Length - 3)) + 1;
                                            sMaMaxDMT = sMaMaxDMT.Substring(0, sMaMaxDMT.Length - (iMaTemp.ToString().Length)) + iMaTemp.ToString();
                                            i--;
                                        }
                                        else
                                        {
                                            string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaMaxDMT + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + sMaSo + "'";
                                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                            iMaTemp = Convert.ToInt32(sMaMaxDMT.Substring(3, sMaMaxDMT.Length - 3)) + 1;
                                            sMaMaxDMT = sMaMaxDMT.Substring(0, sMaMaxDMT.Length - (iMaTemp.ToString().Length)) + iMaTemp.ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (iID_DV == 1)
                                    {
                                        if (sMaSo.Substring(0, 3).ToString() != "DMS")
                                        {

                                            //string sTEMP = "000000" + sMaSo.Substring(3, sMaSo.Length - 3);
                                            //sMaSo = sMaSo.Substring(0, 3).ToString() + sTEMP.Substring(sTEMP.Length - 6);
                                            string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + "DMS" + sMaSo.Substring(3, sMaSo.Length - 3).ToString().Trim() + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + dtSource.Rows[i][0].ToString().Trim() + "'";
                                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                            dtSource.Rows[i][0] = "DMS" + sMaSo.Substring(3, sMaSo.Length - 3);
                                            i--;
                                        }
                                        else
                                        {
                                            if (sMaSo.Substring(3, sMaSo.Length - 3).Length != 6)
                                            {
                                                string sTEMP = "000000" + sMaSo.Substring(3, sMaSo.Length - 3);
                                                sMaSo = sMaSo.Substring(0, 3).ToString() + sTEMP.Substring(sTEMP.Length - 6);
                                                string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + "DMS" + sMaSo.Substring(3, sMaSo.Length - 3).ToString().Trim() + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + dtSource.Rows[i][0].ToString().Trim() + "'";
                                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                                dtSource.Rows[i][0] = "DMS" + sMaSo.Substring(3, sMaSo.Length - 3);
                                                i--;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (sMaSo.Substring(0, 3).ToString() != "DMT")
                                        {

                                            //string sTEMP = "000000" + sMaSo.Substring(3, sMaSo.Length - 3);
                                            //sMaSo = sMaSo.Substring(0, 3).ToString() + sTEMP.Substring(sTEMP.Length - 6);
                                            string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + "DMT" + sMaSo.Substring(3, sMaSo.Length - 3).ToString().Trim() + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + dtSource.Rows[i][0].ToString().Trim() + "'";
                                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                            dtSource.Rows[i][0] = "DMT" + sMaSo.Substring(3, sMaSo.Length - 3);
                                            i--;
                                        }
                                        else
                                        {
                                            if (sMaSo.Substring(3, sMaSo.Length - 3).Length != 6)
                                            {
                                                string sTEMP = "000000" + sMaSo.Substring(3, sMaSo.Length - 3);
                                                sMaSo = sMaSo.Substring(0, 3).ToString() + sTEMP.Substring(sTEMP.Length - 6);
                                                string strSQL = "UPDATE  A SET A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + "DMT" + sMaSo.Substring(3, sMaSo.Length - 3).ToString().Trim() + "' FROM " + sBT + " A WHERE A.[" + grvData.Columns[0].FieldName.ToString() + "] = N'" + dtSource.Rows[i][0].ToString().Trim() + "'";
                                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                                                dtSource.Rows[i][0] = "DMT" + sMaSo.Substring(3, sMaSo.Length - 3);
                                                i--;
                                            }
                                        }
                                    }
                                }
                            }

                            //MaTheChamCong
                        }

                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE T1 SET T1.[" + grvData.Columns[1].FieldName.ToString() + "] = '1' + RIGHT(T1.[" + grvData.Columns[0].FieldName.ToString() + "], 6) FROM " + sBT + " T1");
                        DataTable dt = new DataTable();
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM " + sBT + ""));
                        grdData.DataSource = dt;
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        return;
                    }
                }
            }
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string sbt = "sBTUV" + Commons.Modules.iIDUser;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveImportNhanSu", sbt);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grdData.DataSource = dtSource.Clone();
                        cboChonSheet.Text = string.Empty;
                        btnFile.Text = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        #endregion

        #region  Ứng viên bằng cấp
        private void ImportBangCap(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Tên bằng    
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 200, this.Name))
                {
                    errorCount++;
                }

                //Tên trường  
                col = 2;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                //Từ năm  
                col = 3;
                string sTuNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sTuNam, -999999, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 4;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sDenNam, -999999, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Xếp loại
                col = 5;
                string sXepLoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXepLoai, "XEP_LOAI", "TEN_XL", true, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTUVBC" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_BANG_CAP(ID_UV,TEN_BANG,TEN_TRUONG,TU_NAM,DEN_NAM,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[5].FieldName.ToString() + "]) FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }

        private void ImportKinhNghiem(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Tên công ty    
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //chức vụ  
                col = 2;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 200, this.Name))
                {
                    errorCount++;
                }
                //Mức lương
                col = 3;
                string sMucLuong = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sMucLuong, 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                //từ năm
                col = 4;
                string sTuNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sTuNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 5;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sDenNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //lý do nghĩ
                col = 6;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTUVKN" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_KINH_NGHIEM(ID_UV,TEN_CONG_TY,CHUC_VU,MUC_LUONG,TU_NAM,DEN_NAM,LD_NGHI_VIEC) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "] FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }

        private void ImportThongTinKhac(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Nội dung  
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                //Xếp loại
                col = 2;
                string sXepLoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXepLoai, "XEP_LOAI", "TEN_XL", true, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTTK" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_THONG_TIN_KHAC(ID_UV,NOI_DUNG,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[2].FieldName.ToString() + "]) FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }
        #endregion
        private void grvData_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                grvData = (GridView)sender;
                ptChung = grvData.GridControl.PointToClient(Control.MousePosition);
                grvData.ActiveEditor.DoubleClick += new EventHandler(ActiveEditor_DoubleClick);
            }
            catch
            { }
        }
        private void ActiveEditor_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoRowDoubleClick(grvData, ptChung);
                grvData.RefreshData();
            }
            catch
            { }
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
            if (cboChonSheet.Text == "") return;
            try
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
                int col = -1;
                col = info.Column.AbsoluteIndex;
                if (col == -1)
                    return;

                string sSql = "";
                string sKQ = "";

                System.Data.DataRow row = grvData.GetDataRow(info.RowHandle);
                System.Data.DataRow drow;

                switch (col)
                {
                    case 0:
                        //sSql = "SELECT T2.TEN_NHH, TEN_LHH FROM dbo.LOAI_HANG_HOA T1 INNER JOIN dbo.NHOM_HANG_HOA T2 ON T2.ID_NHH = T1.ID_NHH ORDER BY T2.THU_TU,T1.THU_TU, T2.TEN_NHH,T1.TEN_LHH";
                        //drow = GetData("ID_LHH", sSql);
                        //sKQ = Convert.ToString(drow["TEN_LHH"]);
                        //row.ClearErrors();
                        break;

                    case 1:
                        {
                            break;
                        }
                    case 4:
                        {
                            sSql = "SELECT MA_QG, TEN_QG FROM dbo.QUOC_GIA ORDER BY MA_QG";
                            drow = GetData("ID_QG", sSql);
                            sKQ = Convert.ToString(drow["TEN_QG"]);
                            row.ClearErrors();
                            break;
                        }
                    case 8:
                        {
                            sSql = "SELECT T1.MS_TO, T1.TEN_TO FROM dbo.[TO] T1 INNER JOIN dbo.XI_NGHIEP T2 ON T2.ID_XN = T1.ID_XN INNER JOIN dbo.DON_VI T3 ON T3.ID_DV = T2.ID_DV WHERE(T3.ID_DV = " + -1 + " OR " + -1 + " = -1) AND(T2.ID_XN = " + -1 + " OR " + -1 + " = -1) ORDER BY T3.STT_DV, T2.STT_XN, T1.STT_TO";
                            drow = GetData("ID_TO", sSql);
                            sKQ = Convert.ToString(drow["TEN_TO"]);
                            sKQ = sKQ.Substring(0, sKQ.Length - 3).Trim();
                            row.ClearErrors();
                            break;
                        }
                    case 9:
                        {
                            sSql = "SELECT MS_CV, TEN_CV FROM dbo.CHUC_VU ORDER BY STT_IN_CV";
                            drow = GetData("ID_CV", sSql);
                            sKQ = Convert.ToString(drow["TEN_CV"]);
                            row.ClearErrors();
                            break;
                        }

                    case 10:
                        {
                            sSql = "SELECT TEN_LCV FROM dbo.LOAI_CONG_VIEC ORDER BY STT";
                            drow = GetData("ID_LCV", sSql);
                            sKQ = Convert.ToString(drow["TEN_LCV"]);
                            row.ClearErrors();
                            break;
                        }

                    case 14:
                        {
                            sSql = "SELECT TEN_TT_HD FROM dbo.TINH_TRANG_HD ORDER BY STT";
                            drow = GetData("ID_TT_HD", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HD"]);
                            row.ClearErrors();
                            break;
                        }
                    case 15:
                        {
                            sSql = "SELECT TEN_TT_HT FROM dbo.TINH_TRANG_HT ORDER BY STT";
                            drow = GetData("ID_TT_HT", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HT"]);
                            row.ClearErrors();
                            break;
                        }
                    case 17:
                        {
                            sSql = "SELECT TEN_LD_TV FROM dbo.LY_DO_THOI_VIEC ORDER BY STT";
                            drow = GetData("ID_LD_TV", sSql);
                            sKQ = Convert.ToString(drow["ID_LD_TV"]);
                            row.ClearErrors();
                            break;
                        }
                    case 22:
                        {
                            sSql = "SELECT TEN_DT FROM dbo.DAN_TOC";
                            drow = GetData("ID_DT", sSql);
                            sKQ = Convert.ToString(drow["TEN_DT"]);
                            row.ClearErrors();
                            break;
                        }

                    case 29:
                        {
                            sSql = "SELECT TEN_TT_HN FROM dbo.TT_HON_NHAN";
                            drow = GetData("ID_TT_HN", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HN"]);
                            row.ClearErrors();
                            break;
                        }

                    case 40:
                        {
                            sSql = "SELECT MS_TINH, TEN_TP FROM dbo.THANH_PHO ORDER BY TEN_TP";
                            drow = GetData("ID_TP", sSql);
                            sKQ = Convert.ToString(drow["TEN_TP"]);
                            row.ClearErrors();
                            break;
                        }
                    case 41:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[38]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            sSql = "SELECT MS_QUAN, TEN_QUAN FROM dbo.QUAN WHERE ID_TP = " + (id_tp == 0 ? -1 : id_tp) + " OR " + (id_tp == 0 ? -1 : id_tp) + " = -1 ORDER BY TEN_QUAN";
                            drow = GetData("ID_QUAN", sSql);
                            sKQ = Convert.ToString(drow["TEN_QUAN"]);
                            row.ClearErrors();
                            break;
                        }
                    case 42:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[38]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            string strSQL1 = "SELECT ISNULL(ID_QUAN,-1) ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[39]).ToString().Trim() + "'";
                            int id_quan = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL1));


                            sSql = "SELECT MS_XA, TEN_PX FROM dbo.PHUONG_XA WHERE (ID_QUAN = " + (id_quan == 0 ? -1 : id_quan) + " OR " + (id_quan == 0 ? -1 : id_quan) + " = -1) ORDER BY TEN_PX";
                            drow = GetData("ID_PX", sSql);
                            sKQ = Convert.ToString(drow["TEN_PX"]);
                            row.ClearErrors();
                            break;
                        }
                    case 45:
                        {
                            sSql = "SELECT MS_TINH, TEN_TP FROM dbo.THANH_PHO ORDER BY TEN_TP";
                            drow = GetData("ID_TP", sSql);
                            sKQ = Convert.ToString(drow["TEN_TP"]);
                            row.ClearErrors();
                            break;
                        }
                    case 46:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[43]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            sSql = "SELECT MS_QUAN, TEN_QUAN FROM dbo.QUAN WHERE ID_TP = " + (id_tp == 0 ? -1 : id_tp) + " OR " + (id_tp == 0 ? -1 : id_tp) + " = -1 ORDER BY TEN_QUAN";
                            drow = GetData("ID_QUAN", sSql);
                            sKQ = Convert.ToString(drow["TEN_QUAN"]);
                            row.ClearErrors();
                            break;
                        }
                    case 47:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[43]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            string strSQL1 = "SELECT ISNULL(ID_QUAN,-1) ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[44]).ToString().Trim() + "'";
                            int id_quan = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL1));


                            sSql = "SELECT MS_XA, TEN_PX FROM dbo.PHUONG_XA WHERE (ID_QUAN = " + (id_quan == 0 ? -1 : id_quan) + " OR " + (id_quan == 0 ? -1 : id_quan) + " = -1) ORDER BY TEN_PX";
                            drow = GetData("ID_PX", sSql);
                            sKQ = Convert.ToString(drow["TEN_PX"]);
                            row.ClearErrors();
                            break;
                        }
                    case 51:
                        {
                            sSql = "SELECT TEN_LOAI_TD FROM dbo.LOAI_TRINH_DO ORDER BY STT";
                            drow = GetData("ID_LOAI_TD", sSql);
                            sKQ = Convert.ToString(drow["TEN_LOAI_TD"]);
                            row.ClearErrors();
                            break;
                        }
                    case 52:
                        {
                            sSql = "SELECT T2.TEN_LOAI_TD, T1.TEN_TDVH FROM dbo.TRINH_DO_VAN_HOA T1 INNER JOIN dbo.LOAI_TRINH_DO T2 ON T2.ID_LOAI_TD = T1.ID_LOAI_TD ORDER BY T2.STT, T1.STT";
                            drow = GetData("ID_TDVH", sSql);
                            sKQ = Convert.ToString(drow["TEN_TDVH"]);
                            row.ClearErrors();

                            break;
                        }
                    default:
                        break;
                }

                if (sKQ != null && sKQ != "")
                    grvData.SetFocusedRowCellValue(info.Column.FieldName, sKQ);
                grvData.RefreshData();
            }
            catch (Exception ex) { }
        }
        private DataRow GetData(string ImportType, string SQL)
        {
            try
            {
                frmImportView frm = new frmImportView(ImportType, SQL);
                if (frm.ShowDialog() == DialogResult.OK)
                    return frm._dtrow;
            }
            catch { }
            return null;
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
        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            string nameType = "";
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
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "MS_THE_CC";
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "HO";
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "TEN";
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "TEN_QG";
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "NGAY_SINH";
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "NAM_SINH";
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "PHAI";
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "TEN_TO";
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "TEN_CV";
                            break;
                        }
                    case 10:
                        {
                            sTenCot = "TEN_LCV";
                            break;
                        }
                    case 11:
                        {
                            sTenCot = "NGAY_THU_VIEC";
                            break;
                        }
                    case 12:
                        {
                            sTenCot = "NGAY_VAO_LAM";
                            break;
                        }
                    case 13:
                        {
                            sTenCot = "VAO_LAM_LAI";
                            break;
                        }
                    case 14:
                        {
                            sTenCot = "TEN_TT_HD";
                            break;
                        }
                    case 15:
                        {
                            sTenCot = "TEN_TT_HT";
                            break;
                        }
                    case 16:
                        {
                            sTenCot = "NGAY_NGHI_VIEC";
                            break;
                        }
                    case 17:
                        {
                            sTenCot = "TEN_LD_TV";
                            break;
                        }
                    case 18:
                        {
                            sTenCot = "HINH_THUC_TUYEN";
                            break;
                        }
                    case 19:
                        {
                            sTenCot = "THAM_GIA_BHXH";
                            break;
                        }
                    case 20:
                        {
                            sTenCot = "LD_TINH";
                            break;
                        }
                    case 21:
                        {
                            sTenCot = "GHI_CHU";
                            break;
                        }
                    case 22:
                        {
                            sTenCot = "TEN_DT";
                            break;
                        }
                    case 23:
                        {
                            sTenCot = "TON_GIAO";
                            break;
                        }
                    case 24:
                        {
                            sTenCot = "NOI_SINH";
                            break;
                        }
                    case 25:
                        {
                            sTenCot = "NGUYEN_QUAN";
                            break;
                        }
                    case 26:
                        {
                            sTenCot = "SO_CMND";
                            break;
                        }
                    case 27:
                        {
                            sTenCot = "NGAY_CAP";
                            break;
                        }
                    case 28:
                        {
                            sTenCot = "NOI_CAP";
                            break;
                        }
                    case 29:
                        {
                            sTenCot = "TEN_TT_HN";
                            break;
                        }
                    case 30:
                        {
                            sTenCot = "MA_THE_ATM";
                            break;
                        }
                    case 31:
                        {
                            sTenCot = "SO_TAI_KHOAN";
                            break;
                        }
                    case 32:
                        {
                            sTenCot = "MS_THUE";
                            break;
                        }
                    case 33:
                        {
                            sTenCot = "TEN_KHONG_DAU";
                            break;
                        }
                    case 34:
                        {
                            sTenCot = "LD_NN";
                            break;
                        }
                    case 35:
                        {
                            sTenCot = "DT_DI_DONG";
                            break;
                        }
                    case 36:
                        {
                            sTenCot = "DT_NHA";
                            break;
                        }
                    case 37:
                        {
                            sTenCot = "DT_NGUOI_THAN";
                            break;
                        }
                    case 38:
                        {
                            sTenCot = "EMAIL";
                            break;
                        }
                    case 39:
                        {
                            sTenCot = "DIA_CHI_THUONG_TRU";
                            break;
                        }
                    case 40:
                        {
                            sTenCot = "TEN_TP";
                            break;
                        }
                    case 41:
                        {
                            sTenCot = "TEN_QUAN";
                            break;
                        }
                    case 42:
                        {
                            sTenCot = "TEN_PX";
                            break;
                        }
                    case 43:
                        {
                            sTenCot = "THON_XOM";
                            break;
                        }
                    case 44:
                        {
                            sTenCot = "DIA_CHI_TAM_TRU";
                            break;
                        }
                    case 45:
                        {
                            sTenCot = "TEN_TP_TAM_TRU";
                            break;
                        }
                    case 46:
                        {
                            sTenCot = "TEN_QUAN_TAM_TRU";
                            break;
                        }
                    case 47:
                        {
                            sTenCot = "TEN_PX_TAM_TRU";
                            break;
                        }
                    case 48:
                        {
                            sTenCot = "THON_XOM_TAM_TRU";
                            break;
                        }
                    case 49:
                        {
                            sTenCot = "SO_BHXH";
                            break;
                        }
                    case 50:
                        {
                            sTenCot = "NGAY_DBHXH";
                            break;
                        }
                    case 51:
                        {
                            sTenCot = "TEN_LOAI_TD";
                            break;
                        }
                    case 52:
                        {
                            sTenCot = "TEN_TDVH";
                            break;
                        }
                    case 53:
                        {
                            sTenCot = "CHUYEN_MON";
                            break;
                        }
                    case 54:
                        {
                            sTenCot = "NGOAI_NGU";
                            break;
                        }
                    case 55:
                        {
                            sTenCot = "NGAN_HANG";
                            break;
                        }
                    case 56:
                        {
                            sTenCot = "CHI_NHANH_NH";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                table.Columns.Add(sTenCot.Trim(), (i == 0 || i == 1) ? typeof(string) : prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    nameType = props[i].PropertyType.Name.ToLower();

                    if (props[i].GetValue(item) == null)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    else if (i == 0 || i == 1)
                    {
                        values[i] = props[i].GetValue(item).ToString();
                    }
                    else
                    {
                        values[i] = nameType == "string" ? props[i].GetValue(item).ToString().Trim() : props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }
        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
        }

        private void frmImportNhanSu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Commons.Modules.ObjSystems.setCheckImport(0); //xoa
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(delegate ()
                {
                    timer1.Stop();
                    Thread.Sleep(300000);//chi nghỉ 5 phút
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Hide();
                            string[] sArray = sCheck.Split(',');
                            DateTime datOld;
                            datOld = Convert.ToDateTime(sArray[0]).AddHours(1);
                            DateTime datCurren = DateTime.Now;
                            try
                            {
                                datCurren = DateTime.Now;
                            }
                            catch { }
                            if (datOld < datCurren)
                            {
                                Commons.Modules.ObjSystems.setCheckImport(0);
                            }

                            this.Close();
                        }));
                    }
                }, Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds));
                thread.Start();
            }
            catch { }
        }
        //private void ExportUngVien(string sPath)
        //{
        //    try
        //    {
        //        DataTable dtTmp = new DataTable();
        //        string SQL = "SELECT TOP 0 MS_UV AS  N'Mã số',HO AS N'Họ',TEN AS N'Tên',PHAI AS N'Giới tính',NGAY_SINH AS N'Ngày sinh',NOI_SINH AS N'Nơi sinh',SO_CMND AS N'CMND',NGAY_CAP AS N'Ngày cấp',NOI_CAP AS N'Nơi cấp',CONVERT(NVARCHAR(250), ID_TT_HN) AS N'Tình trạng HN',HO_TEN_VC AS N'Họ tên V/C',NGHE_NGHIEP_VC AS N'Nghề nghiệp V/C',SO_CON AS N'Số con',DT_DI_DONG AS N'Điện thoại',EMAIL AS N'Email',NGUOI_LIEN_HE AS N'Người liên hệ',QUAN_HE AS N'Quan hệ',DT_NGUOI_LIEN_HE AS N'ĐT Người liên hệ',CONVERT(NVARCHAR(250), ID_TP) AS N'Thành phố',CONVERT(NVARCHAR(250), ID_QUAN) AS N'Quận',CONVERT(NVARCHAR(250), ID_PX) AS N'Phường xã',THON_XOM AS N'Thôn xóm',DIA_CHI_THUONG_TRU AS N'Địa chỉ',CONVERT(NVARCHAR(250), ID_NTD) AS N'Nguồn tuyển',CONVERT(NVARCHAR(250), ID_CN) AS N'Người giới thiệu',CONVERT(NVARCHAR(250), TIENG_ANH) AS N'TIENG_ANH',CONVERT(NVARCHAR(250), TIENG_TRUNG) AS N'TIENG_TRUNG',CONVERT(NVARCHAR(250), TIENG_KHAC) AS N'TIENG_KHAC',CONVERT(NVARCHAR(250), ID_DGTN) AS N'Đánh giá tay nghề',CONVERT(NVARCHAR(250), VI_TRI_TD_1) AS N'Vị trí tuyển 1',CONVERT(NVARCHAR(250), VI_TRI_TD_2) AS N'Vị trí tuyển 2',NGAY_HEN_DI_LAM AS N'Ngày hẹn đi làm',XAC_NHAN_DL AS N'Xác nhận đi làm',NGAY_NHAN_VIEC AS N'Ngày nhận việc',XAC_NHAN_DTDH AS N'Xác nhận đào tạo định hướng',DA_CHUYEN AS N'Chuyển sang nhân sự',GHI_CHU AS N'Ghi chú',DA_GIOI_THIEU AS N'Đã giới thiệu',HUY_TUYEN_DUNG AS N'Hủy tuyển dụng'FROM dbo.UNG_VIEN";

        //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

        //        //export datatable to excel
        //        Workbook book = new Workbook();
        //        Worksheet sheet1 = book.Worksheets[0];
        //        sheet1.Name = "01-Danh sách ứng viên";
        //        sheet1.DefaultColumnWidth = 20;

        //        sheet1.InsertDataTable(dtTmp, true, 1, 1);

        //        sheet1.Range[2, 1].Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();

        //        sheet1.Range[1, 1, 1, 39].Style.WrapText = true;
        //        sheet1.Range[1, 1, 1, 39].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet1.Range[1, 1, 1, 39].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet1.Range[1, 1, 1, 39].Style.Font.IsBold = true;

        //        sheet1.Range[1, 1].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 2].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 3].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 30].Style.Font.Color = Color.Red;


        //        sheet1.Range[1, 1].Comment.RichText.Text = "Mã ứng viên sẽ được đặt theo cấu trúc MUV-000001 trong đó(MUV-: cố định,còn 000001 sẽ được tăng thêm 1 khi có một ứng viên mới).";
        //        sheet1.Range[1, 4].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataPhai());
        //        sheet1.Range[1, 10].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataTinHTrangHN(false));
        //        sheet1.Range[1, 19].Comment.RichText.Text = "Nhập đúng cấp tỉnh/thành phố trong danh mục.";
        //        sheet1.Range[1, 20].Comment.RichText.Text = "Nhập đúng cấp quận/huyện trong danh mục.";
        //        sheet1.Range[1, 21].Comment.RichText.Text = "Nhập đúng cấp phường/xã trong danh mục.";
        //        sheet1.Range[1, 24].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataNguonTD(false));
        //        sheet1.Range[1, 25].Comment.RichText.Text = "Họ và tên nhân viên trong công ty giới thiệu.";

        //        sheet1.Range[1, 26].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataMucDoTieng(false));
        //        sheet1.Range[1, 27].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataMucDoTieng(false));
        //        //sheet1.Range[1, 28].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataKinhNghiemLV(false));
        //        sheet1.Range[1, 29].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false));

        //        sheet1.Range[1, 30].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)));
        //        sheet1.Range[1, 31].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)));

        //        sheet1.Range[1, 33].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 35].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 36].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 38].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 39].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";

        //        sheet1.FreezePanes(2, 4);
        //        //Tên trường Từ năm	Đến năm	Xếp loại

        //        Worksheet sheet2 = book.Worksheets[1];
        //        sheet2.Name = "02-Bằng cấp";
        //        sheet2.DefaultColumnWidth = 20;

        //        sheet2.Range[1, 1].Text = "Mã số";
        //        sheet2.Range[1, 2].Text = "Tên bằng";
        //        sheet2.Range[1, 3].Text = "Tên trường";
        //        sheet2.Range[1, 4].Text = "Từ năm";
        //        sheet2.Range[1, 5].Text = "Đến năm";
        //        sheet2.Range[1, 6].Text = "Xếp loại";
        //        sheet2.Range[1, 6].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

        //        sheet2.Range[1, 1, 1, 6].Style.WrapText = true;
        //        sheet2.Range[1, 1, 1, 6].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet2.Range[1, 1, 1, 6].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet2.Range[1, 1, 1, 6].Style.Font.IsBold = true;


        //        Worksheet sheet3 = book.Worksheets[2];
        //        sheet3.Name = "03-Kinh nghiệm làm việc";
        //        sheet3.DefaultColumnWidth = 20;

        //        sheet3.Range[1, 1].Text = "Mã số";
        //        sheet3.Range[1, 2].Text = "Tên công ty";
        //        sheet3.Range[1, 3].Text = "Chức vụ";
        //        sheet3.Range[1, 4].Text = "Mức lương";
        //        sheet3.Range[1, 5].Text = "Từ năm";
        //        sheet3.Range[1, 6].Text = "Đến năm";
        //        sheet3.Range[1, 7].Text = "Lý do nghĩ";

        //        sheet3.Range[1, 1, 1, 7].Style.WrapText = true;
        //        sheet3.Range[1, 1, 1, 7].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet3.Range[1, 1, 1, 7].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet3.Range[1, 1, 1, 7].Style.Font.IsBold = true;

        //        //Worksheet sheet4 = book.Worksheets.Add("04-Thông tin khác");
        //        //sheet4.DefaultColumnWidth = 20;

        //        //sheet4.Range[1, 1].Text = "Mã số";
        //        //sheet4.Range[1, 2].Text = "Nội dung";
        //        //sheet4.Range[1, 3].Text = "Xếp loại";

        //        //sheet4.Range[1, 3].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

        //        //sheet4.Range[1, 1, 1, 3].Style.WrapText = true;
        //        //sheet4.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
        //        //sheet4.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        //sheet4.Range[1, 1, 1, 3].Style.Font.IsBold = true;

        //        book.SaveToFile(sPath);
        //        System.Diagnostics.Process.Start(sPath);
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}
