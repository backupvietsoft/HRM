using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Office.Interop.Excel;


namespace Vs.Recruit
{
    public partial class frmInSoSanhThucTeDL : DevExpress.XtraEditors.XtraForm
    {
        private string SaveExcelFile;
        public frmInSoSanhThucTeDL()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        BaoCaoSoSanh(5);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
        private void frmInSoSanhThucTeDL_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            Commons.Modules.sLoad = "";
        }
       


        #region Excel
        private void HeaderReport(ref Excel.Worksheet oSheet, int LoaiBaoCao, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11,int DONG = 1)
        {
            if (LoaiBaoCao == 5)
            {
                Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A" + (DONG + 2).ToString() + "", lastColumn + (DONG + 2).ToString() +""); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH SO SÁNH TUYỂN DỤNG VÀ THỰC TẾ ĐI LÀM";

                Excel.Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A" + (DONG + 3).ToString() + "", lastColumn + (DONG + 3).ToString() +""); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");
                return;
            }
        }
        private void TitleTable(int LoaiBaoCao)
        {
            string[] TitleTableName = { };
        }
        private void BaoCaoSoSanh(int LoaiBaoCao)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_DM", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSUngVienThamGiaTD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                cmd.Parameters.Add("@LOAI_BC", SqlDbType.Int).Value = LoaiBaoCao;


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

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                int DONG = 0;
                DONG = Commons.Modules.MExcel.TaoTTChung(oSheet, 1, 2, 1, dtBCThang.Columns.Count, 0, 0);
                DONG = 3;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                //Header của báo cáo
                HeaderReport(ref oSheet, LoaiBaoCao, fontName, fontSizeNoiDung, lastColumn, fontSizeTieuDe,DONG);

                // Title Table
                Excel.Range row5_STT = oSheet.get_Range("A" + (DONG + 5).ToString() + "");
                row5_STT.Value2 = "STT";
                row5_STT.ColumnWidth = 9;
                FormatTitleTable(ref row5_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 5);

                Excel.Range row5_MaSo_UV = oSheet.get_Range("B" + (DONG + 5).ToString() + "");
                row5_MaSo_UV.Value2 = "Mã số yêu cầu tuyển dụng";
                FormatTitleTable(ref row5_MaSo_UV, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Excel.Range row5_HoTen = oSheet.get_Range("C" + (DONG + 5).ToString() + "");
                row5_HoTen.Value2 = "Ngày lập yêu cầu";
                FormatTitleTable(ref row5_HoTen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                //Excel.Range row5_NgaySinh = oSheet.get_Range("D" + (DONG + 5).ToString() + "");
                //row5_NgaySinh.Value2 = "Ngày vào làm";
                //FormatTitleTable(ref row5_NgaySinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Excel.Range row5_GioiTinh = oSheet.get_Range("D" + (DONG + 5).ToString() + "");
                row5_GioiTinh.Value2 = "Bộ phận";
                FormatTitleTable(ref row5_GioiTinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 35);

                Excel.Range row5_CMND = oSheet.get_Range("E" + (DONG + 5).ToString() + "");
                row5_CMND.Value2 = "Vị trí tuyển dụng";
                FormatTitleTable(ref row5_CMND, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 45);

                Excel.Range row5_NgayCap = oSheet.get_Range("F" + (DONG + 5).ToString() + "");
                row5_NgayCap.Value2 = "SL tham gia PV / KTTN";
                FormatTitleTable(ref row5_NgayCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Excel.Range row5_NoiCap = oSheet.get_Range("G" + (DONG + 5).ToString() + "");
                row5_NoiCap.Value2 = "SL đạt PV / KTTN";
                FormatTitleTable(ref row5_NoiCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Excel.Range row5_SLDL = oSheet.get_Range("H" + (DONG + 5).ToString() + "");
                row5_SLDL.Value2 = "Số lượng đi làm";
                FormatTitleTable(ref row5_SLDL, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Excel.Range row5_DiaChi = oSheet.get_Range("I" + (DONG + 5).ToString() + "");
                row5_DiaChi.Value2 = "% Tỷ lệ đi làm";
                FormatTitleTable(ref row5_DiaChi, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Excel.Range row5_FormatTieuDe = oSheet.get_Range("A" + (DONG + 5).ToString() + "", "J" + (DONG + 5).ToString() + "");
                row5_FormatTieuDe.Font.Bold = true;
                //End title table

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                int col_bd = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 5 + DONG;
                oSheet.get_Range("A" + (DONG + 6).ToString() + "", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                int cotJ = 6 + DONG;
                for (int i = 0; i < rowCnt - 5 - DONG; i++)
                {
                    formatRange = oSheet.get_Range("I" + cotJ.ToString() + "");
                    formatRange.Value = "=IFERROR(H" + cotJ + "/G" + cotJ + ",0)";
                    cotJ++;
                }
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
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}

                // Data table
                // A6->Last
                formatRange = oSheet.get_Range("A" + (DONG + 6).ToString() + "", "A" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // B6->Last
                formatRange = oSheet.get_Range("B" + (DONG + 6).ToString() + "", "B" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // C6->Last
                formatRange = oSheet.get_Range("C" + (DONG + 6).ToString() + "", "C" + (rowCnt).ToString());
                formatRange.EntireColumn.NumberFormat = "DD/MM/YYYY";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;
                //FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

   

                // E6->Last
                formatRange = oSheet.get_Range("D" + (DONG + 6).ToString() + "", "E" + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                // F6->Last
                formatRange = oSheet.get_Range("E" + (DONG + 6).ToString() + "", "F" + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                // G6->Last
                formatRange = oSheet.get_Range("F" + (DONG + 6).ToString() + "", "G" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // H6->Last
                formatRange = oSheet.get_Range("G" + (DONG + 6).ToString() + "", "H" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // I6->Last
                formatRange = oSheet.get_Range("H" + (DONG + 6).ToString() + "", "I" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // J6->Last
                formatRange = oSheet.get_Range("I" + (DONG + 6).ToString() + "", "J" + (rowCnt).ToString());
                formatRange.NumberFormat = "0.0%";
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);


                //End Data Table
                BorderAround(oSheet.get_Range("A" + (DONG + 5).ToString() + "", "aa"));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormatDataTable(ref Excel.Range formatRange, string fontName = "Times New Roman", int fontSizeNoiDung = 11, bool isFormatNumberic = false)
        {
            formatRange.Font.Name = fontName;
            formatRange.Font.Size = fontSizeNoiDung;
            formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            formatRange.WrapText = true;
            if (isFormatNumberic)
            {
                formatRange.NumberFormat = "dd/MM/yyyy";
            }
        }
        private void FormatTitleTable(ref Excel.Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, Color BackgroundColor = default(Color), int ColumnWidth = 15)
        {
            range.Font.Name = fontName;
            range.Interior.Color = Color.FromArgb(255, 255, 0);
            range.RowHeight = 40;
            range.ColumnWidth = ColumnWidth;
            range.WrapText = true;
            range.Font.Bold = true;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        }

        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
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
        #endregion

      
        private void dTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
            //dTuNgay.EditValue = firstDateTime;
            int t = DateTime.DaysInMonth(dTuNgay.DateTime.Year, dTuNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(dTuNgay.DateTime.Year, Convert.ToInt32(dTuNgay.DateTime.Month), t);
            dDenNgay.EditValue = secondDateTime;
        }
    }
}
