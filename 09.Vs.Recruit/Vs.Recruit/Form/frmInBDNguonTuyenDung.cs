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
    public partial class frmInBDNguonTuyenDung : DevExpress.XtraEditors.XtraForm
    {
        private string SaveExcelFile;
        public frmInBDNguonTuyenDung()
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
                        BieuDoPhanLoai();
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
        private void frmInBDNguonTuyenDung_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            dTuNgay.EditValue = Convert.ToDateTime("01/01/" + DateTime.Today.Year);
            dDenNgay.EditValue = Convert.ToDateTime("31/12/" + DateTime.Today.Year);
            Commons.Modules.sLoad = "";
        }



        #region Excel
        private void BieuDoPhanLoai()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetBieuDoUngVienNguonTD", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = dTuNgay.EditValue;
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = dDenNgay.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if(dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                    return;
                }   

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                object misValue = System.Reflection.Missing.Value;

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "C2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BIỂU ĐỒ NGUỒN TUYỂN DỤNG";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "C4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                #region GioiTinh
                // select columns giới tính
                int col = 1;
                int row_dl = 4;
                for (col = 0; col < dt.Columns.Count; col++)
                {
                    switch (dt.Columns[col].ColumnName.ToString())
                    {
                        case "TEN_NTD":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Nguổn tuyển dụng";
                                break;
                            }
                        case "SL":
                            {
                                oSheet.Cells[row_dl, col + 1] = "SL tuyển";
                                break;
                            }
                        case "TY_LE":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Phần trăm";
                                break;
                            }
                        default:
                            break;
                    }
                    oSheet.Cells[row_dl, col + 1].ColumnWidth = 15;
                    oSheet.Cells[row_dl, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Load dữ liệu Giới tính
                DataRow[] dr = dt.Select();
                string[,] rowData = new string[dr.Count(), dt.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dt.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("A5", "C" + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
                for (col = 1; col <= 3; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + ""+ 5 + dt.Rows.Count +"");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                rowCnt++;
                formatRange = oSheet.get_Range("A" + "" + (5 + dt.Rows.Count) + "");
                formatRange.Value = "Total";
                formatRange.ColumnWidth = 25;
                formatRange = oSheet.get_Range("B"+ (5 + dt.Rows.Count)  + "");
                formatRange.Value = "=SUM(B5:B"+ (4 + dt.Rows.Count) + ")";
                formatRange = oSheet.get_Range("C"+ (5 + dt.Rows.Count)  + "");
                formatRange.Value = "=SUM(C5:C"+ (4 + dt.Rows.Count) + ")";
                LoadBieuDoTron(oSheet, XlChartType.xl3DPie, CharacterIncrement(0), 5, CharacterIncrement(0), dt.Rows.Count + 4, CharacterIncrement(2), 5, CharacterIncrement(2),dt.Rows.Count + 4, "lblNguonTuyenDung", 1, 400, 10, 240, 240, true);
                #endregion

                BorderAround(oSheet.get_Range("A4", "C" + (5 + dt.Rows.Count) + ""));

                formatRange = oSheet.get_Range("A5", "C"+ (5 + dt.Rows.Count) + "");
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                formatRange = oSheet.get_Range("C5", "C" + (5 + dt.Rows.Count) + "");
                formatRange.NumberFormat = @"0%";

                Commons.Modules.MExcel.ThemDong((Excel.Worksheet)oSheet, XlInsertShiftDirection.xlShiftDown, 1, 3);
                Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A3", "C3"); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBieuDoTron(Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.XlChartType xlChartType, string sTenCotBD, int iDongBD, string sTenCotKT, int iDongKT, string sTenCotBDTyLe, int iDongBD_TL, string sTenCotKTTyLe, int iDongKT_TL, string sTitle, int iSoLan,
          double iLeft, double iTop, double iWidth, double iHeight, Boolean bTitile)
        {
            try
            {


                double iSLan;
                double sLe;
                double sChan;
                double sKQ;


                Microsoft.Office.Interop.Excel.ChartObjects chartObjs = (Microsoft.Office.Interop.Excel.ChartObjects)ExcelSheets.ChartObjects(Type.Missing);
                Microsoft.Office.Interop.Excel.ChartObject chartObj = chartObjs.Add(iLeft, iTop, iWidth, iHeight);
                Microsoft.Office.Interop.Excel.Chart xlChart = chartObj.Chart;
                Microsoft.Office.Interop.Excel.SeriesCollection xlSeriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)xlChart.SeriesCollection(Type.Missing);
                //xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;
                Microsoft.Office.Interop.Excel.Series xlSeries = xlSeriesCollection.NewSeries();
                xlChart.ChartType = xlChartType;



                var _with1 = xlSeries;
                _with1.Name = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucBaoCaoThongKeCongNhanBD", "Thang", Commons.Modules.TypeLanguage);// "=Sheet1!$A$" + (vDong + 1);                 //"=A" + vDong;
                _with1.XValues = ExcelSheets.get_Range("" + sTenCotBD + "" + iDongBD + "", "" + sTenCotKT + "" + iDongKT); // cột tên (Nam - nữ -khác)
                _with1.Values = ExcelSheets.get_Range("" + sTenCotBDTyLe + "" + iDongBD_TL + "", "" + sTenCotKTTyLe + "" + iDongKT_TL); //"B33"); // Cột dữ liệu

                if (bTitile)
                    xlChart.ChartTitle.Text = Commons.Modules.ObjLanguages.GetLanguage(
                    Commons.Modules.ModuleName, this.Name, sTitle, Commons.Modules.TypeLanguage);
                xlChart.Refresh();

                Microsoft.Office.Interop.Excel.DataLabel dl1;
                _with1.HasDataLabels = true;
                for (int i = 1; i < iDongKT - 3; i++)
                {
                    dl1 = _with1.DataLabels(i);
                    dl1.Font.Color = Color.FromArgb(255, 255, 255);
                    dl1.Font.Size = 10;
                    dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                }
                _with1.Border.Color = Color.FromArgb(255, 255, 255);

                xlChart.HasTitle = true;
                xlChart.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionBottom;
                xlChart.ChartArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(10, 10, 255));
                xlChart.PlotArea.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                xlChart.PlotArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 255));
                xlChart.Legend.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                //xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;

                xlChart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, false, false, false, true);
                //ax.TickLabels.Orientation = Microsoft.Office.Interop.Excel.XlTickLabelOrientation.xlTickLabelOrientationUpward;





            }
            catch (Exception ex)
            { }
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
            //int t = DateTime.DaysInMonth(dTuNgay.DateTime.Year, dTuNgay.DateTime.Month);
            //DateTime secondDateTime = new DateTime(dTuNgay.DateTime.Year, Convert.ToInt32(dTuNgay.DateTime.Month), t);
            //dDenNgay.EditValue = secondDateTime;
        }
    }
}
