//Update
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Globalization;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoQHGD : DevExpress.XtraEditors.XtraUserControl
    {
        //Update
        private string SaveExcelFile;
        public ucBaoCaoQHGD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //Update
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
        
        //Update
        private void FormatTieuDeBaoCao(ref Range row, bool isMerge = true, bool isBold = false, int fontSizeNoiDung = 11, string fontName = "Times New Roman", string numberFormant = "@", Microsoft.Office.Interop.Excel.XlHAlign horizontalAlignment = XlHAlign.xlHAlignLeft, Microsoft.Office.Interop.Excel.XlVAlign verticalAlignment = XlVAlign.xlVAlignCenter,string Value = "" )
        {
            if(isMerge)
            {
                row.Merge();
            }
            if(isBold)
            {
                row.Font.Bold = true;
            }
            row.Font.Size = fontSizeNoiDung;
            row.Font.Name = fontName;
            row.NumberFormat = numberFormant;
            row.Cells.HorizontalAlignment = horizontalAlignment;
            row.Cells.VerticalAlignment = verticalAlignment;
            row.Value2 = Value;
        }

        //Update
        private void HeaderReport(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11)
        {
            Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "D2"); // = A2 - D2
            FormatTieuDeBaoCao(ref row2_TieuDe_BaoCao, true, false, 11, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Excel tailoring co.,ltd");

            Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", "D3"); // = A3 - D3
            FormatTieuDeBaoCao(ref row3_TieuDe_BaoCao, true, false, 11, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Yen Ninh Town - Yen Khanh District - Ninh Binh Province");

            Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "B4"); // = A4 - B4
            FormatTieuDeBaoCao(ref row4_TieuDe_BaoCao, true, false, 11, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Tel: 02293.840.358");

            Range row6_TieuDe_BaoCao = oSheet.get_Range("A6", "N6"); // = A6 - N6
            FormatTieuDeBaoCao(ref row6_TieuDe_BaoCao, true, true, 18, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "DANH SÁCH ĐĂNG KÝ CON DƯỚI 6 TUỔI CỦA NỮ CBCNV NHÀ MÁY");

            Range row7_TieuDe_BaoCao = oSheet.get_Range("A7", "N7"); // = A7 - N7
            FormatTieuDeBaoCao(ref row7_TieuDe_BaoCao, true, true, 12, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "Từ ngày: "+ System.Convert.ToDateTime(tuNgay.EditValue).ToString("MM/dd/yyyy") + "   Đến ngày: " + System.Convert.ToDateTime(denNgay.EditValue).ToString("MM/dd/yyyy"));

            return;
        }


        //Update
        private void FormatTitleTable(ref Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, Color BackgroundColor = default(Color), int ColumnWidth = 10, bool isMerge = false, string Value = "",int rowHeight = 30)
        {
            if(isMerge)
            {
                range.Merge();
            }
            range.Font.Name = fontName;
            range.Interior.Color = BackgroundColor;
            range.ColumnWidth = ColumnWidth;
            range.WrapText = true;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            range.Value2 = Value;
            range.RowHeight = rowHeight;
        }

        //Update
        public void CreateHeaderTable(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11)
        {
            int height_Double = 20;
            int height_Single = 10;
            Range row9_Header_Table_STT = oSheet.get_Range("A9","A10"); // A9-A10
            FormatTitleTable(ref row9_Header_Table_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "STT", height_Double);

            Range row9_Header_Table_MS = oSheet.get_Range("B9", "B10"); // B9-B10
            FormatTitleTable(ref row9_Header_Table_MS, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "Mã thẻ", height_Double);

            Range row9_Header_Table_Bo_Phan = oSheet.get_Range("C9", "C10"); // C9-C10
            FormatTitleTable(ref row9_Header_Table_Bo_Phan, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 20, true, "Bộ phận", height_Double);

            Range row9_Header_Table_Ho_Ten_Me = oSheet.get_Range("D9", "D10"); // D9-D10
            FormatTitleTable(ref row9_Header_Table_Ho_Ten_Me, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 25, true, "Họ và tên mẹ", height_Double);

            Range row9_Header_Table_Ngay_Vao = oSheet.get_Range("E9", "G9"); // E9-G9
            FormatTitleTable(ref row9_Header_Table_Ngay_Vao, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Ngày vào", height_Single);

            Range row9_Header_Table_Ngay = oSheet.get_Range("E10"); // E10
            FormatTitleTable(ref row9_Header_Table_Ngay, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Ngày", height_Single);

            Range row9_Header_Table_Thang = oSheet.get_Range("F10"); // F10
            FormatTitleTable(ref row9_Header_Table_Thang, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Tháng", height_Single);

            Range row9_Header_Table_Nam = oSheet.get_Range("G10"); // G10
            FormatTitleTable(ref row9_Header_Table_Nam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Năm", height_Single);

            Range row9_Header_Table_Da_Co_HD = oSheet.get_Range("H9","I10"); // H9 - I10
            FormatTitleTable(ref row9_Header_Table_Da_Co_HD, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "Đã có hợp đồng", height_Double);

            Range row9_Header_Table_Ho_Ten_Con = oSheet.get_Range("J9", "J10"); // J9 - J10
            FormatTitleTable(ref row9_Header_Table_Ho_Ten_Con, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 25, true, "Họ và tên con", height_Double);

            Range row9_Header_Table_Ngay_Sinh = oSheet.get_Range("K9", "M9"); // K9-M9
            FormatTitleTable(ref row9_Header_Table_Ngay_Sinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Ngày tháng năm sinh", height_Single);

            Range row9_Header_Table_Day = oSheet.get_Range("K10"); // K10
            FormatTitleTable(ref row9_Header_Table_Day, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Ngày", height_Single);

            Range row9_Header_Table_Month = oSheet.get_Range("L10"); // L10
            FormatTitleTable(ref row9_Header_Table_Month, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Tháng", height_Single);

            Range row9_Header_Table_Year = oSheet.get_Range("M10"); // M10
            FormatTitleTable(ref row9_Header_Table_Year, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Năm", height_Single);

            Range row9_Header_Table_Ghi_Chu = oSheet.get_Range("N9", "N10"); // N9-N10
            FormatTitleTable(ref row9_Header_Table_Ghi_Chu, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 15, true, "Ghi chú", height_Double);
        }

        //Update
        public void FooterReport(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, string fontName = "Times New Roman", int rowCnt = 1)
        {
            Range row_Ngay_Xuat = oSheet.get_Range("K" + (rowCnt + 2).ToString(), "N" + (rowCnt + 2).ToString());// Cell Signing Date
            DateTime ExportDate = System.Convert.ToDateTime(lk_NgayTinh.EditValue);
            string Value = "Excel, Ngày " + ExportDate.Day.ToString() + " Tháng " + ExportDate.Month.ToString() + " Năm " + ExportDate.Year.ToString();
            FormatTieuDeBaoCao(ref row_Ngay_Xuat, true, false, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, Value);

            Range row_Nguoi_Lap = oSheet.get_Range("A" + (rowCnt + 3).ToString(), "D" + (rowCnt + 3).ToString());// Cell Nguoi Lap Bieu
            FormatTieuDeBaoCao(ref row_Nguoi_Lap, true, true, 12, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "NGƯỜI LẬP BIỂU");

            Range row_Phong_HCNS = oSheet.get_Range("F" + (rowCnt + 3).ToString(), "H" + (rowCnt + 3).ToString());// Cell PHONG HCNS
            FormatTieuDeBaoCao(ref row_Phong_HCNS, true, true, 12, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "PHÒNG HCNS");

            Range row_Ban_Giam_Doc = oSheet.get_Range("K" + (rowCnt + 3).ToString(), "N" + (rowCnt + 3).ToString());// Cell BAN GIAM DOC
            FormatTieuDeBaoCao(ref row_Ban_Giam_Doc, true, true, 12, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "BAN GIÁM ĐỐC");

        }

        //Update
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

        //Update
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

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        /*
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptDSGiaDinh(lk_NgayIn.DateTime);
                          try
                          {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSGiaDinh", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = rdo_ConCongNhan.SelectedIndex;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                            cmd.Parameters.Add("@TuoiTu", SqlDbType.Int).Value = txt_Tu.Text.ToString() == "" ? 0 : txt_Tu.EditValue;
                            cmd.Parameters.Add("@TuoiDen", SqlDbType.Int).Value = txt_Den.Text.ToString() == "" ? 99 : txt_Den.EditValue; 
                            cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);
                            frm.ShowDialog()
                          }
                            catch{}
                            break;
                         */

                         
                        // Update
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        try
                        {
                        
                            // Gets data from database
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSGiaDinh", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = rdo_ConCongNhan.SelectedIndex;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                            cmd.Parameters.Add("@TuoiTu", SqlDbType.Int).Value = txt_Tu.Text.ToString() == "" ? 0 : txt_Tu.EditValue;
                            cmd.Parameters.Add("@TuoiDen", SqlDbType.Int).Value = txt_Den.Text.ToString() == "" ? 99 : txt_Den.EditValue;
                            cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
                            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.EditValue;
                            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            dt = new DataTable();
                            adp.Fill(dt);
                            dt.TableName = "DA_TA";

                            // Format for an Excel file
                            SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                            if (SaveExcelFile == "")
                            {
                                return;
                            }

                            //Init object to work with Excel
                            Microsoft.Office.Interop.Excel.Application oXL;
                            Microsoft.Office.Interop.Excel.Workbook oWB;
                            Microsoft.Office.Interop.Excel.Worksheet oSheet;
                            oXL = new Microsoft.Office.Interop.Excel.Application();
                            oXL.Visible = true;

                            oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                            string lastColumn = String.Empty;
                            lastColumn = CharacterIncrement(dt.Columns.Count - 1);

                            //Create header of report
                            HeaderReport(ref oSheet, "Times New Roman", 11, lastColumn, 11);
                            
                            //Create header of table
                            CreateHeaderTable(ref oSheet, "Times New Roman", 11);

                            DataRow[] dr = dt.Select();
                            string[,] rowData = new string[dr.Count(), dt.Columns.Count];

                            int rowCnt = 0;
                            int col_bd = 0;

                            //Transfer from Data Table class into a 2-dimention array.
                            foreach (DataRow row in dr)
                            {
                                for (col_bd = 0; col_bd < dt.Columns.Count; col_bd++)
                                {
                                    rowData[rowCnt, col_bd] = row[col_bd].ToString();
                                }
                                rowCnt++;
                            }
                            rowCnt = rowCnt + 10;

                            //Fill data from dt into Data table of Excel
                            oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).Value2 = rowData;

                            Microsoft.Office.Interop.Excel.Range formatRangeAll = oSheet.get_Range("A11", lastColumn + rowCnt.ToString());//Format all Data table
                            Microsoft.Office.Interop.Excel.Range formatRange1 = oSheet.get_Range("E11", "I" + rowCnt.ToString());//Format colum E->I of Data table
                            Microsoft.Office.Interop.Excel.Range formatRange2 = oSheet.get_Range("K11", "N" + rowCnt.ToString());//Format colum K->N of Data table
                            Microsoft.Office.Interop.Excel.Range formatRange3 = oSheet.get_Range("J" + rowCnt.ToString());////Format colum J of Data table

                            formatRangeAll.WrapText = true;
                            formatRangeAll.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            formatRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            formatRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            formatRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            formatRange2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            formatRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                            formatRange3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            //Format border for all of table
                            BorderAround(oSheet.get_Range("A9", lastColumn + (rowCnt).ToString()));

                            //Footer report
                            FooterReport(ref oSheet, "Times New Roman", rowCnt);

                            oXL.Visible = true;
                            oXL.UserControl = true;

                            oWB.SaveAs(SaveExcelFile, AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
                        }
                        catch
                        { }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoQHGD_Load(object sender, EventArgs e)
        {
            rdo_ConCongNhan.SelectedIndex = 0;
            lk_NgayTinh.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.ObjSystems.LoadCboQHGD(lk_QuanHeGD);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            //Update
            tuNgay.EditValue = new DateTime(2022, 08, 01);
            denNgay.EditValue = new DateTime(2022, 08, 31);
            Commons.OSystems.SetDateEditFormat(tuNgay);
            Commons.OSystems.SetDateEditFormat(denNgay);
        }

        private void rdo_ConCongNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ConCongNhan.SelectedIndex)
            {
                case 0:
                    {
                        lk_NgayTinh.Enabled = true;
                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;

                        //Update
                        tuNgay.Enabled = false;
                        denNgay.Enabled = false;
                        lk_QuanHeGD.Enabled = true;
                    }
                    break;
                case 1:
                    {
                        lk_NgayTinh.Enabled = false;
                        txt_Tu.Enabled = true;
                        txt_Den.Enabled = true;

                        //Update
                        tuNgay.Enabled = false;
                        denNgay.Enabled = false;
                        lk_QuanHeGD.Enabled = true;
                    }
                    break;
                case 2:
                    {
                        tuNgay.Enabled = true;
                        denNgay.Enabled = true;
                        lk_NgayTinh.Enabled = false;

                        //Update
                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;
                        lk_QuanHeGD.Enabled = false;

                    }
                    break;
               
                default:
                    lk_NgayTinh.Enabled = true;
                    txt_Tu.Enabled = false;
                    txt_Den.Enabled = false;

                    //Update
                    tuNgay.Enabled = false;
                    denNgay.Enabled = false;
                    lk_QuanHeGD.Enabled = true;
                    break;
            }
        }
    }
}
