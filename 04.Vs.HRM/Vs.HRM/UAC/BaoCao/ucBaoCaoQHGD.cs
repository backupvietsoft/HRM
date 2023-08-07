//Update
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Linq;
using XlHAlign = Excel.XlHAlign;
using XlVAlign = Excel.XlVAlign;
using Application = System.Windows.Forms.Application;
using Microsoft.ApplicationBlocks.Data;
using Excel;
using Vs.Report;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Vs.HRM
{
    public partial class ucBaoCaoQHGD : DevExpress.XtraEditors.XtraUserControl
    {
        //Update
        public ucBaoCaoQHGD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        private void ucBaoCaoQHGD_Load(object sender, EventArgs e)
        {
            rdo_ConCongNhan.SelectedIndex = 0;
            lk_NgayTinh.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.ObjSystems.LoadCboDonVi(lkDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(lkDonVi, lkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
            Commons.Modules.ObjSystems.LoadCboQHGD(lk_QuanHeGD);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            //Update
            tuNgay.EditValue = Commons.Modules.ObjSystems.setDate1Month(DateTime.Now, 0);
            denNgay.EditValue = Commons.Modules.ObjSystems.setDate1Month(DateTime.Now, 1);
            Commons.OSystems.SetDateEditFormat(tuNgay);
            Commons.OSystems.SetDateEditFormat(denNgay);
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
        private void FormatTieuDeBaoCao(ref Excel.Range row, bool isMerge = true, bool isBold = false, int fontSizeNoiDung = 11, string fontName = "Times New Roman", string numberFormant = "@", Excel.XlHAlign horizontalAlignment = XlHAlign.xlHAlignLeft, Excel.XlVAlign verticalAlignment = XlVAlign.xlVAlignCenter, string Value = "")
        {
            if (isMerge)
            {
                row.Merge();
            }
            if (isBold)
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

        //update
        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop, float MWidth, float MHeight)
        {
            try
            {
                System.Data.DataTable dtTmp = new System.Data.DataTable();
                string sSql = "";
                sSql = "SELECT CASE WHEN " + Commons.Modules.TypeLanguage + " = 0  THEN TEN_CTY ELSE TEN_CTY_A END AS TEN_CTY, LOGO, CASE WHEN " + Commons.Modules.TypeLanguage + "= 0 THEN DIA_CHI  ELSE DIA_CHI_A  END AS DIA_CHI,DIEN_THOAI, Fax,LOGO FROM THONG_TIN_CHUNG  ";

                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));


                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotKT - 2], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Ngày in:" + DateTime.Today.ToString("dd/MM/yyyy");
                //CurCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //CurCell.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 11;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];



                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 11;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 11;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " + " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

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
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, MWidth, MHeight);
                System.IO.File.Delete(Application.StartupPath + @"\logo.bmp");

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
        //Update
        private void HeaderReport(ref Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11, int Tcot = 0)
        {
            try
            {
                int Result = TaoTTChung(oSheet, 1, 2, 1, Tcot, 0, 0, 50, 50);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Excel.Range row6_TieuDe_BaoCao = oSheet.get_Range("A6", lastColumn + "6"); // = A6 - N6
            FormatTieuDeBaoCao(ref row6_TieuDe_BaoCao, true, true, 18, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "DANH SÁCH ĐĂNG KÝ CON DƯỚI 6 TUỔI CỦA CBCNV NHÀ MÁY");

            Excel.Range row7_TieuDe_BaoCao = oSheet.get_Range("A7", lastColumn + "7"); // = A7 - N7
            FormatTieuDeBaoCao(ref row7_TieuDe_BaoCao, true, true, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, rdo_ConCongNhan.SelectedIndex == 2 ? "Từ ngày: " + System.Convert.ToDateTime(tuNgay.EditValue).ToString("dd/MM/yyyy") + "   Đến ngày: " + System.Convert.ToDateTime(denNgay.EditValue).ToString("dd/MM/yyyy") : "Tháng " + System.Convert.ToDateTime(denNgay.EditValue).ToString("MM/yyyy"));

            return;
        }
        //Update
        private void FormatTitleTable(ref Excel.Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, Color BackgroundColor = default(Color), int ColumnWidth = 10, bool isMerge = false, string Value = "", int rowHeight = 30)
        {
            if (isMerge)
            {
                range.Merge();
            }
            range.Font.Name = fontName;
            range.Font.Bold = true;
            range.Font.Size = fontSizeNoiDung;
            range.Interior.Color = BackgroundColor;
            range.ColumnWidth = ColumnWidth;
            range.WrapText = true;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            range.Value2 = Value;
            range.RowHeight = rowHeight;
        }

        //Update
        public void CreateHeaderTable(ref Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11)
        {
            int height_Double = 20;
            int height_Single = 10;
            Excel.Range row9_Header_Table_STT = oSheet.get_Range("A9", "A10"); // A9-A10
            FormatTitleTable(ref row9_Header_Table_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "STT", height_Double);

            Excel.Range row9_Header_Table_MS = oSheet.get_Range("B9", "B10"); // B9-B10
            FormatTitleTable(ref row9_Header_Table_MS, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "Mã thẻ", height_Double);

            Excel.Range row9_Header_Table_Bo_Phan = oSheet.get_Range("C9", "C10"); // C9-C10
            FormatTitleTable(ref row9_Header_Table_Bo_Phan, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 20, true, "Bộ phận", height_Double);

            Excel.Range row9_Header_Table_Ho_Ten_Me = oSheet.get_Range("D9", "D10"); // D9-D10
            FormatTitleTable(ref row9_Header_Table_Ho_Ten_Me, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Họ và tên mẹ", height_Double);

            Excel.Range row9_Header_Table_Ngay_Vao = oSheet.get_Range("E9", "G9"); // E9-G9
            FormatTitleTable(ref row9_Header_Table_Ngay_Vao, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Ngày vào", height_Single);

            Excel.Range row9_Header_Table_Ngay = oSheet.get_Range("E10"); // E10
            FormatTitleTable(ref row9_Header_Table_Ngay, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Ngày", height_Single);

            Excel.Range row9_Header_Table_Thang = oSheet.get_Range("F10"); // F10
            FormatTitleTable(ref row9_Header_Table_Thang, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Tháng", height_Single);

            Excel.Range row9_Header_Table_Nam = oSheet.get_Range("G10"); // G10
            FormatTitleTable(ref row9_Header_Table_Nam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Năm", height_Single);

            Excel.Range row9_Header_Table_Da_Co_HD = oSheet.get_Range("H9", "I10"); // H9 - I10
            FormatTitleTable(ref row9_Header_Table_Da_Co_HD, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, true, "Đã có hợp đồng", height_Double);

            Excel.Range row9_Header_Table_Ho_Ten_Con = oSheet.get_Range("J9", "J10"); // J9 - J10
            FormatTitleTable(ref row9_Header_Table_Ho_Ten_Con, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Họ và tên con", height_Double);

            Excel.Range row9_Header_Table_Ngay_Sinh = oSheet.get_Range("K9", "M9"); // K9-M9
            FormatTitleTable(ref row9_Header_Table_Ngay_Sinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 30, true, "Ngày tháng năm sinh", height_Single);

            Excel.Range row9_Header_Table_Day = oSheet.get_Range("K10"); // K10
            FormatTitleTable(ref row9_Header_Table_Day, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Ngày", height_Single);

            Excel.Range row9_Header_Table_Month = oSheet.get_Range("L10"); // L10
            FormatTitleTable(ref row9_Header_Table_Month, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Tháng", height_Single);

            Excel.Range row9_Header_Table_Year = oSheet.get_Range("M10"); // M10
            FormatTitleTable(ref row9_Header_Table_Year, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 10, false, "Năm", height_Single);

            Excel.Range row9_Header_Table_Ghi_Chu = oSheet.get_Range("N9", "N10"); // N9-N10
            FormatTitleTable(ref row9_Header_Table_Ghi_Chu, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 255), 15, true, "Ghi chú", height_Double);
        }

        //Update
        public void FooterReport(ref Excel.Worksheet oSheet, string fontName = "Times New Roman", int rowCnt = 1)
        {
            Range row_Ngay_Xuat = oSheet.get_Range("K" + (rowCnt + 2).ToString(), "N" + (rowCnt + 2).ToString());// Cell Signing Date
            DateTime ExportDate = System.Convert.ToDateTime(lk_NgayTinh.EditValue);
            string Value = "Excel, Ngày " + ExportDate.Day.ToString() + " Tháng " + ExportDate.Month.ToString() + " Năm " + ExportDate.Year.ToString();
            FormatTieuDeBaoCao(ref row_Ngay_Xuat, true, false, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, Value);

            Range row_Nguoi_Lap = oSheet.get_Range("A" + (rowCnt + 3).ToString(), "D" + (rowCnt + 3).ToString());// Cell Nguoi Lap Bieu
            FormatTieuDeBaoCao(ref row_Nguoi_Lap, true, true, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "NGƯỜI LẬP BIỂU");

            Range row_Phong_HCNS = oSheet.get_Range("F" + (rowCnt + 3).ToString(), "H" + (rowCnt + 3).ToString());// Cell PHONG HCNS
            FormatTieuDeBaoCao(ref row_Phong_HCNS, true, true, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "PHÒNG HCNS");

            Range row_Ban_Giam_Doc = oSheet.get_Range("K" + (rowCnt + 3).ToString(), "N" + (rowCnt + 3).ToString());// Cell BAN GIAM DOC
            FormatTieuDeBaoCao(ref row_Ban_Giam_Doc, true, true, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "BAN GIÁM ĐỐC");

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

        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
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
        public void CreateHeaderTable_DSGDCBCNV(int dt, ref Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int RowCount = 0)
        {

            int height_Double = 25;
            int height_All = 35;
            RowCount = RowCount + 10;

            Range row5_Header_Table_STT = oSheet.get_Range("A9", "A9");
            FormatTitleTable_DSGDCBCNV(ref row5_Header_Table_STT, fontName, fontSizeNoiDung, height_All, Color.FromArgb(255, 255, 255), true, "Stt", 6);
            row5_Header_Table_STT.ColumnWidth = 10;
            row5_Header_Table_STT.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            row5_Header_Table_STT.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            Range row5_Header_Table_Ma_The = oSheet.get_Range("B9", "B9");
            FormatTitleTable_DSGDCBCNV(ref row5_Header_Table_Ma_The, fontName, fontSizeNoiDung, height_All, Color.FromArgb(255, 255, 255), true, "Mã thẻ", 10);

            Range row5_Header_Table_Bo_Phan = oSheet.get_Range("C9", "C9");
            FormatTitleTable_DSGDCBCNV(ref row5_Header_Table_Bo_Phan, fontName, fontSizeNoiDung, height_All, Color.FromArgb(255, 255, 255), true, "Bộ phận", 15);

            Range row5_Header_Table_Ho_Ten = oSheet.get_Range("D9", "D9");
            FormatTitleTable_DSGDCBCNV(ref row5_Header_Table_Ho_Ten, fontName, fontSizeNoiDung, height_All, Color.FromArgb(255, 255, 255), true, "Họ và tên", 30);

            Range row5_Header_Table_Merge = oSheet.get_Range("E9", lastColumn + "9");
            FormatTitleTable_DSGDCBCNV(ref row5_Header_Table_Merge, fontName, fontSizeNoiDung, height_All, Color.FromArgb(255, 255, 255), true, "");

            int CountColumn = dt - 4;
            int Set = 0;
            while (CountColumn != 0)
            {
                object ColumnName = CharacterIncrement(dt - CountColumn);

                Set = Set + 1;
                switch (Set)
                {
                    case 1:
                        {
                            Range row6_Ho_Ten = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Ho_Ten, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Họ tên", 30);
                            break;
                        }
                    case 2:
                        {
                            Range row6_Ngay_Sinh = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Ngay_Sinh, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Ngày sinh", 12);
                            row6_Ngay_Sinh.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_Ngay_Sinh.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            Range row6_Ngay_Sinh_Data = oSheet.get_Range(ColumnName + "11", ColumnName + RowCount.ToString());
                            row6_Ngay_Sinh_Data.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_Ngay_Sinh_Data.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            break;
                        }
                    case 3:
                        {
                            Range row6_So_CMND = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_So_CMND, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Số CMND/CCCD", 12);
                            row6_So_CMND.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_So_CMND.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            Range row6_So_CMND_Data = oSheet.get_Range(ColumnName + "11", ColumnName + RowCount.ToString());
                            row6_So_CMND_Data.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_So_CMND_Data.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            break;
                        }
                    case 4:
                        {
                            Range row6_So_So_HK = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_So_So_HK, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Số sổ hộ khẩu", 12);
                            row6_So_So_HK.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_So_So_HK.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            Range row6_So_So_HK_Data = oSheet.get_Range(ColumnName + "11", ColumnName + RowCount.ToString());
                            row6_So_So_HK_Data.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_So_So_HK_Data.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            break;
                        }
                    case 5:
                        {
                            Range row6_Dia_Chi_Khai_Sinh = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Dia_Chi_Khai_Sinh, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Địa chỉ khai sinh", 20);
                            break;
                        }
                    case 6:
                        {
                            Range row6_Dia_Chi_Ho_Khau = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Dia_Chi_Ho_Khau, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Địa chỉ hộ khẩu", 20);
                            break;
                        }
                    case 7:
                        {
                            Range row6_Quan_He = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Quan_He, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Quan hệ", 10);
                            break;
                        }
                    case 8:
                        {
                            Range row6_Quan_He_Ho_Khau = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Quan_He_Ho_Khau, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Quan hệ hộ khẩu", 10);
                            break;
                        }
                    case 9:
                        {
                            Range row6_Loai_Quan_He = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Loai_Quan_He, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Loại quan hệ", 10);
                            break;
                        }
                    case 10:
                        {
                            Range row6_Dien_Thoai = oSheet.get_Range(ColumnName + "10");
                            FormatTitleTable_DSGDCBCNV(ref row6_Dien_Thoai, fontName, fontSizeNoiDung, height_Double, Color.FromArgb(255, 255, 255), false, "Điện thoại", 12);
                            row6_Dien_Thoai.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_Dien_Thoai.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            Range row6_Dien_Thoai_Data = oSheet.get_Range(ColumnName + "11", ColumnName + RowCount.ToString());
                            row6_Dien_Thoai_Data.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            row6_Dien_Thoai_Data.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            break;
                        }
                    _:
                        {
                            break;
                        }
                }
                if (Set == 10)
                {
                    Set = 0;
                }
                CountColumn = CountColumn - 1;
            }


            Excel.Range formatRangeTitleTable = oSheet.get_Range("A9", lastColumn + "10");//Format title of Data table

            formatRangeTitleTable.Font.Bold = true;
            formatRangeTitleTable.Font.Name = "Times New Roman";
            formatRangeTitleTable.WrapText = true;
            formatRangeTitleTable.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            formatRangeTitleTable.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders.Color = Color.Black;
            formatRangeTitleTable.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            formatRangeTitleTable.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            formatRangeTitleTable.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;

        }
        private void FormatTitleTable_DSGDCBCNV(ref Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, int rowHeight = 30, Color BackgroundColor = default(Color), bool isMerge = false, string Value = "", int ColumnWidth = 9)
        {
            if (isMerge)
            {
                range.Merge();
            }
            range.Value2 = Value;
            range.ColumnWidth = ColumnWidth;
            range.RowHeight = rowHeight;
            range.Font.Bold = true;
            range.WrapText = true;
        }
        private void HeaderReport_DSGDCBCNV(ref Excel.Worksheet oSheet, bool CungCongTy = false, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11, int Tcot = 0)
        {

            try
            {
                int Result = TaoTTChung(oSheet, 1, 2, 1, Tcot, 0, 0, 50, 50);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (CungCongTy)
            {

                Excel.Range row6_TieuDe_BaoCao = oSheet.get_Range("A6", lastColumn + "6"); // = A6 - N6
                FormatTieuDeBaoCao(ref row6_TieuDe_BaoCao, true, true, 18, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "DANH SÁCH GIA ĐÌNH CÁN BỘ, CÔNG NHÂN, NHÂN VIÊN NHÀ MÁY LÀM CÙNG CÔNG TY");
            }
            else
            {

                Excel.Range row6_TieuDe_BaoCao = oSheet.get_Range("A6", lastColumn + "6"); // = A6 - N6
                FormatTieuDeBaoCao(ref row6_TieuDe_BaoCao, true, true, 18, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "DANH SÁCH GIA ĐÌNH CÁN BỘ, CÔNG NHÂN, NHÂN VIÊN NHÀ MÁY");

            }
            DateTime ngaytinh = System.Convert.ToDateTime(lk_NgayTinh.EditValue);
            string Ngay = ngaytinh.ToString("dd");
            string Thang = ngaytinh.ToString("MM");
            string Nam = ngaytinh.Year.ToString();
            Range row4_TieuDe_BaoCao = oSheet.get_Range("A7", lastColumn + "7"); // = A7 - BL7
            FormatTieuDeBaoCao(ref row4_TieuDe_BaoCao, true, true, 11, fontName, "@", XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, "Ngày " + Ngay + " Tháng " + Thang + " Năm " + Nam);

            return;
        }
        private void DanhSachGDCBCNV()
        {
            System.Data.SqlClient.SqlConnection conn;

            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachGDCBCNVNhaMay", conn);

                string dateString = lk_NgayTinh.EditValue.ToString();
                DateTime EditDay = DateTime.Parse(dateString);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = lkDonVi.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = lkXiNghiep.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = lkTo.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = EditDay;
                cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
                cmd.Parameters.Add("@CungCTy", SqlDbType.Bit).Value = checkBoxLamCungCongTy.Checked;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                adp.Fill(ds);
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";

                //Init object to work with Excel
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = oWB.ActiveSheet;

                string lastColumn = String.Empty;
                int C = dt.Columns.Count;
                lastColumn = CharacterIncrement(dt.Columns.Count - 1);

                //Create header of report
                HeaderReport_DSGDCBCNV(ref oSheet, checkBoxLamCungCongTy.Checked, "Times New Roman", 11, lastColumn, 11, dt.Columns.Count - 2);

                //Create header of table
                CreateHeaderTable_DSGDCBCNV(dt.Columns.Count, ref oSheet, "Times New Roman", 11, lastColumn, dt.Columns.Count);

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
                Range range = oSheet.get_Range("A11", lastColumn + rowCnt.ToString());
                BorderAround(range);
                range.Font.Name = "Times New Roman";
                range.Font.Size = 11;
                range.WrapText = true;
                range.RowHeight = 15;

                Excel.Range rangeAB = oSheet.get_Range("A11", "B" + rowCnt.ToString());
                rangeAB.VerticalAlignment = XlVAlign.xlVAlignCenter;
                rangeAB.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DanhSachDangKyConDuoiSauTuoi()
        {
            // Update
            System.Data.SqlClient.SqlConnection conn;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                // Gets data from database
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd;

                if (Commons.Modules.KyHieuDV == "NB" || Commons.Modules.KyHieuDV == "NC")
                {
                    cmd = new System.Data.SqlClient.SqlCommand("rptDSGiaDinh_NB", conn);
                    DateTime denNgayValue = Convert.ToDateTime(denNgay.EditValue);
                    DateTime ngayDauThang = new DateTime(denNgayValue.Year, denNgayValue.Month, 1);
                    DateTime ngayCuoiThang = ngayDauThang.AddMonths(1).AddDays(-1);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = rdo_ConCongNhan.SelectedIndex;
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                    cmd.Parameters.Add("@TuoiTu", SqlDbType.Int).Value = txt_Tu.Text.ToString() == "" ? 0 : txt_Tu.EditValue;
                    cmd.Parameters.Add("@TuoiDen", SqlDbType.Int).Value = txt_Den.Text.ToString() == "" ? 99 : txt_Den.EditValue;
                    cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
                    cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = ngayDauThang;
                    cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = ngayCuoiThang;
                }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("rptDSGiaDinh", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = rdo_ConCongNhan.SelectedIndex;
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                    cmd.Parameters.Add("@TuoiTu", SqlDbType.Int).Value = txt_Tu.Text.ToString() == "" ? 0 : txt_Tu.EditValue;
                    cmd.Parameters.Add("@TuoiDen", SqlDbType.Int).Value = txt_Den.Text.ToString() == "" ? 99 : txt_Den.EditValue;
                    cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
                    cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.EditValue;
                    cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.EditValue;
                }

               
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                adp.Fill(ds);
                dt = ds.Tables[0];
                dt.TableName = "DA_TA";

                //Init object to work with Excel
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = false;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = oWB.ActiveSheet;

                string lastColumn = String.Empty;
                lastColumn = CharacterIncrement(dt.Columns.Count - 1);


                //Create header of report
                HeaderReport(ref oSheet, "Times New Roman", 11, lastColumn, 11, dt.Columns.Count - 2);

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
                oXL.Visible = true;


                //Fill data from dt into Data table of Excel
                oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).Value2 = rowData;

                Excel.Range formatRangeAll = oSheet.get_Range("A11", lastColumn + rowCnt.ToString());//Format all Data table
                Excel.Range formatRange1 = oSheet.get_Range("E11", "I" + rowCnt.ToString());//Format colum E->I of Data table
                Excel.Range formatRange2 = oSheet.get_Range("K11", "N" + rowCnt.ToString());//Format colum K->N of Data table
                Excel.Range formatRange3 = oSheet.get_Range("J11", "J" + rowCnt.ToString());////Format colum J of Data table
                Excel.Range formatRange4 = oSheet.get_Range("A11", "A" + rowCnt.ToString());//Format colum A of Data table

                formatRangeAll.WrapText = true;
                formatRangeAll.Font.Name = "Times New Roman";
                formatRangeAll.Font.Size = 11;
                formatRangeAll.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                if(Commons.Modules.KyHieuDV == "NB" || Commons.Modules.KyHieuDV == "NC")
                {
                    //Commons.Modules.MExcel.MFormatExcel(oSheet, dt, 11, this.Name, false, true, true);

                    Excel.Range formatRange = oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[dt.Rows.Count + 10, 2]];
                    formatRange.NumberFormat = "0";
                    try
                    {
                        for (int row = 11; row <= rowCnt + 10; row++)
                        {
                            if (int.TryParse(oSheet.Cells[row, 2].Value.ToString(), out int value))
                            {
                                oSheet.Cells[row, 2].Value = value;
                            }
                        }
                    }
                    catch { }
                    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }





                formatRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                formatRange2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                formatRange3.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                formatRange4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange4.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                formatRangeAll.RowHeight = 15;

                //Format border for all of table
                BorderAround(oSheet.get_Range("A9", lastColumn + (rowCnt).ToString()));

                //Footer report
                FooterReport(ref oSheet, "Times New Roman", rowCnt);

                oXL.Visible = true;
                oXL.UserControl = true;


              
            }
            catch
            { }
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "Print":
                        {
                            switch (rdo_ConCongNhan.SelectedIndex)
                            {   
                                case 0:
                                    {
                                        DanhSachGDCBCNV();
                                        break;
                                    }
                                case 1:
                                    {
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
                                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = lkDonVi.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = lkXiNghiep.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = lkTo.EditValue;
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
                                            frm.ShowDialog();
                                        }
                                        catch { }
                                        break;
                                    }

                                case 2:
                                case 3:
                                    {
                                        DanhSachDangKyConDuoiSauTuoi();
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }


        private void lkDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(lkDonVi, lkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
        }

        private void lkXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
        }

        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.sLoad = "";
        }

        private void rdo_ConCongNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ConCongNhan.Properties.Items[rdo_ConCongNhan.SelectedIndex].Tag)
            {
                case "rdo_DenNgay":
                    {
                        lk_NgayTinh.Enabled = true;
                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;

                        //Update
                        tuNgay.Enabled = false;
                        denNgay.Enabled = false;
                        lk_QuanHeGD.Enabled = true;
                        checkBoxLamCungCongTy.Enabled = true;
                    }
                    break;
                case "rdo_DoTuoi":
                    {
                        lk_NgayTinh.Enabled = false;
                        txt_Tu.Enabled = true;
                        txt_Den.Enabled = true;

                        //Update
                        tuNgay.Enabled = false;
                        denNgay.Enabled = false;
                        lk_QuanHeGD.Enabled = true;
                        checkBoxLamCungCongTy.Enabled = false;
                    }
                    break;
                case "rdo_DuoiSauTuoi":
                    {
                        tuNgay.Enabled = true;
                        denNgay.Enabled = true;
                        lk_NgayTinh.Enabled = false;

                        //Update
                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;
                        lk_QuanHeGD.Enabled = false;
                        checkBoxLamCungCongTy.Enabled = false;
                        break;
                    }
                case "rdo_DSDKCon6TuoiTongHop":
                    {
                        tuNgay.Enabled = false;
                        denNgay.Enabled = true;
                        lk_NgayTinh.Enabled = false;

                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;
                        lk_QuanHeGD.Enabled = false;
                        checkBoxLamCungCongTy.Enabled = false;
                        denNgay.EditValue = Commons.Modules.ObjSystems.setDate1Month(denNgay.DateTime, 1);
                        break;
                    }

                default:
                    lk_NgayTinh.Enabled = true;
                    txt_Tu.Enabled = false;
                    txt_Den.Enabled = false;

                    //Update
                    tuNgay.Enabled = false;
                    denNgay.Enabled = false;
                    lk_QuanHeGD.Enabled = true;
                    checkBoxLamCungCongTy.Enabled = true;
                    break;
            }
        }

    }
}
