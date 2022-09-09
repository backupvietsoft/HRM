using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace Vs.Payroll
{
    public partial class frmInBaoCaoHTTienDo : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idCT;
        private string sThang = "";
        private string SaveExcelFile;
        DataTable dt = new DataTable();
        public frmInBaoCaoHTTienDo(DataTable dt1, string Thang)
        {
            InitializeComponent();
            dt = dt1;
            sThang = Thang;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void frmInBaoCaoHTTienDo_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
        }

        private void dNgayIn_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tablePanel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    DSCongNhanHTTienDo();
                                    break;
                                }
                            case 1:
                                {
                                    BaoCaoTongHopHTTienDo();
                                    break;
                                }
                            default:
                                {

                                    break;
                                }
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void DSCongNhanHTTienDo()
        {
            try
            {
                string sPath = "";
                sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelApplication.DisplayAlerts = true;

                excelApplication.Visible = true;


                System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                excelWorkbook.SaveAs(sPath);

                Microsoft.Office.Interop.Excel.Worksheet oSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                DataTable dt1 = new DataTable();
                dt1 = dt.Copy();
                dt1.Columns.Remove("ID_CN");
                //DataTable dt1 = new DataTable();
                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    //dt.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt.Columns[i].ColumnName.ToString()); ;
                    dt1.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt1.Columns[i].ColumnName.ToString());
                }

                string lastColumn = CharacterIncrement(dt1.Columns.Count - 1);
                string fontName = "Time News Roman";
                int fontSizeTieuDe = 13;
                int fontSizeNoiDung = 9;
                //dt1 = dv.ToTable(false, "MS_CN", "HO_TEN");
                //dt1.Columns["MS_CN"].ColumnName = "MSCN";
                //dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";

                //Microsoft.Office.Interop.Excel.Range Ranges1 = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                Microsoft.Office.Interop.Excel.Range Ranges1 = oSheet.get_Range("A3", "" + lastColumn + "" + (dt1.Rows.Count + 3).ToString() + "");

                //Ranges1.Range["A1", ""+ lastColumn + "1"].Merge();
                //Ranges1.Range["A1", "" + lastColumn + "1"].Value = "DANH SÁCH CÔNG NHÂN HỖ TRỢ TIỀN ĐÒ THÁNG 08/2022";
                //Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                //Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range row1_TieuDe = oSheet.get_Range("A1", "" + lastColumn + "1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Size = 16;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.Value = "DANH SÁCH CÔNG NHÂN HỖ TRỢ TIỀN ĐÒ THÁNG " + sThang + "";
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range row5_TieuDe_Format = oSheet.get_Range("A3", "" + lastColumn + "3");
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row5_NoiDung_Format = oSheet.get_Range("A4", "" + lastColumn + "" + (dt1.Rows.Count + 3).ToString() + "");
                row5_NoiDung_Format.Font.Size = fontSizeNoiDung;
                row5_NoiDung_Format.Font.Name = fontName;
                row5_NoiDung_Format.WrapText = true;


                Ranges1.ColumnWidth = 30;
                BorderAround(oSheet.get_Range("A3", lastColumn + (dt1.Rows.Count + 3).ToString()));
                MExportExcel(dt1, oSheet, Ranges1);



                excelApplication.Visible = true;
                excelWorkbook.Save();
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }
        private void BaoCaoTongHopHTTienDo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBaoCaoLuong", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "DSCN_HT_TIEN_DO";
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(sThang);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();


                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "B2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 11;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "SỐ LƯỢNG CÔNG NHÂN QUA ĐÒ THÁNG " + sThang + "";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "B4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.get_Range("A4");
                row4_TieuDe_TTNV.Value2 = "Xã";
                row4_TieuDe_TTNV.ColumnWidth = 30;

                Range row4_TieuDe_TTC = oSheet.get_Range("B4");
                row4_TieuDe_TTC.Value2 = "Số lượng";
                row4_TieuDe_TTC.ColumnWidth = 20;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("A5", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                formatRange = oSheet.get_Range("B5", "B" + rowCnt.ToString());
                formatRange.NumberFormat = "0";
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}

                rowCnt++;
                formatRange = oSheet.get_Range("A" + (rowCnt).ToString());
                formatRange.Value = "Tổng";
                formatRange.Font.Bold = true;

                formatRange = oSheet.get_Range("B" + (rowCnt).ToString());
                formatRange.Value = "=SUM(B5:B" + (rowCnt - 1).ToString() + ")";

                formatRange = oSheet.get_Range("A5", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region excel
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
        #endregion  
    }
}