using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Vs.Report;
using MessageBox = System.Windows.Forms.MessageBox;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoBDTangGiamCN : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoBDTangGiamCN()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        switch (rdoChonBC.SelectedIndex)
                        {
                            case 0:
                                {
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoTyLeTangGiamLaoDongNam", conn);
                                        cmd.Parameters.Add("@DV", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToInt32(datNam.DateTime.Year);
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);

                                        DataTable dt1 = new DataTable();
                                        dt1 = ds.Tables[1].Copy();

                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.rpt = new VS.Report.NhanSu.rptBieuDoTangGiamCongNhan(dt1, datNam.DateTime.Year);
                                    }
                                    catch(Exception ex)
                                    { }
                                    break;
                                }
                            case 1:
                                {
                                    BieuDoPhanLoai();
                                    break;
                                }
                        }


                        frm.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoBDTangGiamCN_Load(object sender, EventArgs e)
        {
            try
            {

                datNam.DateTime = DateTime.Now.AddYears(-1);
                DataTable dt = new DataTable();
                Commons.Modules.UserName = "admin";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
                if (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)).ToString() != "SB")
                {
                    rdoChonBC.Properties.Items.RemoveAt(1);
                }
            }
            catch
            {

            }

        }
        private void BieuDoPhanLoai()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtGioiTinh;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoPhanLoai_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);



                dtGioiTinh = new DataTable();
                dtGioiTinh = ds.Tables[0].Copy();

                DataTable dtCNMay = new DataTable();
                dtCNMay = ds.Tables[1].Copy();

                DataTable dtIDD = new DataTable();
                dtIDD = ds.Tables[2].Copy();


                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

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

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "X2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BIỂU ĐỒ PHÂN LOẠI";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "C4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                // select columns giới tính
                int col = 1;
                int row_dl = 4;
                for (col = 0; col < dtGioiTinh.Columns.Count; col++)
                {
                    switch (dtGioiTinh.Columns[col].ColumnName.ToString())
                    {
                        case "GIOI_TINH":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Giới tính";
                                break;
                            }
                        case "TONG_SO_GT":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Tổng số";
                                break;
                            }
                        case "TY_LE_GT":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Tỷ lệ";
                                break;
                            }
                        default:
                            //oSheet.Cells[row_dl, col + 1] = dtBCThang.Columns[col].ColumnName.ToString();
                            break;
                    }
                    oSheet.Cells[row_dl, col + 1].ColumnWidth = 15;
                }

                DataRow[] dr = dtGioiTinh.Select();
                string[,] rowData = new string[dr.Count(), dtGioiTinh.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtGioiTinh.Columns.Count; col++)
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
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + "6");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                BorderAround(oSheet.get_Range("A4", "C6"));
                LoadBieuDo(oSheet, CharacterIncrement(0), 5, CharacterIncrement(0), 6, CharacterIncrement(2), 5, CharacterIncrement(2), 6, "lblGioiTinh", 1, 10, 155, 240, 240, true);
                //rowCnt++;
                //oSheet.get_Range("A" + rowCnt + "").Value2 = "Grand Total";
                //for (col = 2; col <= dtBCThang.Columns.Count; col++)
                //{
                //    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).Value2 = "=+SUM(" + CharacterIncrement(col - 1) + "5:" + CharacterIncrement(col - 1) + "6)";
                //}

                //rowCnt++;
                //oSheet.get_Range("A" + rowCnt + "").Value2 = "IDD";
                //for (col = 2; col <= dtBCThang.Columns.Count; col++)
                //{
                //    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).Value2 = "=+" + CharacterIncrement(col - 1) + "5/" + CharacterIncrement(col - 1) + "6";
                //    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).NumberFormat = "0.00;-0;;@";
                //    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //}

                ////Microsoft.Office.Interop.Excel.Range formatRange;
                //////rowCnt = keepRowCnt + 2;

                ////////dịnh dạng
                ////////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);
                //Microsoft.Office.Interop.Excel.Range formatRange;
                //string CurentColumn = string.Empty;
                //int colBD = 1;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "5", CurentColumn + (rowCnt - 2).ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}
                //BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));

                ////formatRange = oSheet.get_Range("N11", lastColumn + "13");

                //row_dl = 11;
                //for (col = 0; col < dtBieuDo.Columns.Count; col++)
                //{
                //    oSheet.Cells[row_dl, col + 14].ColumnWidth = 15;
                //    switch (dtBieuDo.Columns[col].ColumnName.ToString())
                //    {
                //        case "IDD":
                //            {
                //                oSheet.Cells[row_dl, col + 14] = "";
                //                break;
                //            }
                //        case "TONG_SO_IDD":
                //            {
                //                oSheet.Cells[row_dl, col + 14] = "Số lượng";
                //                oSheet.Cells[row_dl, col + 14].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //                oSheet.Cells[row_dl, col + 14].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //                break;
                //            }
                //        case "TY_LE_IDD":
                //            {
                //                oSheet.Cells[row_dl, col + 14] = "%";
                //                oSheet.Cells[row_dl, col + 14].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //                oSheet.Cells[row_dl, col + 14].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //                break;
                //            }
                //        default:
                //            break;
                //    }

                //}

                //DataRow[] dr1 = dtBieuDo.Select();
                //string[,] rowData1 = new string[dr.Count(), dtBieuDo.Columns.Count];

                //int rowCnt1 = 0;
                //foreach (DataRow row1 in dr1)
                //{
                //    for (col = 0; col < dtBieuDo.Columns.Count; col++)
                //    {
                //        rowData1[rowCnt1, col] = row1[col].ToString();
                //    }
                //    rowCnt1++;
                //}
                //rowCnt = rowCnt + 5;
                //oSheet.get_Range("N12", lastColumn + rowCnt.ToString()).Value2 = rowData1;
                //BorderAround(oSheet.get_Range("N11", lastColumn + rowCnt.ToString()));

                //for (col = 14; col <= 16; col++)
                //{
                //    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "11", "" + CharacterIncrement(col - 1) + "13");
                //    formatRange.Font.Size = fontSizeNoiDung;
                //    formatRange.Font.Name = fontName;
                //}

                //for (col = 14; col <= 16; col++)
                //{
                //    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "12", "" + CharacterIncrement(col - 1) + "13");
                //    formatRange.NumberFormat = "0.0;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}


                //////LoadBieuDo(oSheet, 13, 16, "", 1, 10, 80, 300, 250, true);

                ////Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
                ////Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(200, 500, 200, 100);
                ////Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

                //Microsoft.Office.Interop.Excel.Range chartRange;
                //Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
                //Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(80, 155, 300, 250);
                //Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
                //chartRange = oSheet.get_Range("N12", "P13");
                //chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;
                //Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = chartPage.SeriesCollection();
                //Microsoft.Office.Interop.Excel.Series series1 = seriesCollection.NewSeries();
                //series1.ChartType = XlChartType.xl3DPie;
                //series1.XValues = oSheet.Range["N12", "N13"];
                //series1.Values = oSheet.Range["P12", "P13"];

                //series1.HasDataLabels = true;
                //Microsoft.Office.Interop.Excel.DataLabel dl1 = series1.DataLabels(1);
                //dl1.Font.Color = Color.FromArgb(255, 255, 255);
                //dl1.Font.Size = 12;
                //dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                //dl1 = series1.DataLabels(2);
                //dl1.Font.Size = 12;
                //dl1.Font.Color = Color.FromArgb(255, 255, 255);
                //dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                ////dl1.Border.Color = Color.FromArgb(255, 255, 255);
                //series1.Border.Color = Color.FromArgb(255, 255, 255);


                //chartPage.HasTitle = true;
                //chartPage.ChartTitle.Text = "IDD";
                //chartPage.Refresh();

                //chartPage.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionBottom;
                //chartPage.ChartArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(10, 10, 255));
                //chartPage.PlotArea.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                //chartPage.PlotArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 255));
                //chartPage.Legend.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                //Microsoft.Office.Interop.Excel.Axis ax = chartPage.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary) as Microsoft.Office.Interop.Excel.Axis;
                //chartPage.Refresh();
                //chartPage.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, false, false, false, true);
                ////chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;
                ////chartPage.Refresh();


                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void LoadBieuDo(Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, string sTenCotBD, int iDongBD, string sTenCotKT, int iDongKT, string sTenCotBDTyLe, int iDongBD_TL, string sTenCotKTTyLe, int iDongKT_TL, string sTitle, int iSoLan,
            double iLeft, double iTop, double iWidth, double iHeight, Boolean bTitile)
        {
            try
            {


                double iSLan;
                double sLe;
                double sChan;
                double sKQ;

                //iSLan = iSoLan;
                //sKQ = 0;
                //if (sCuoi)
                //{
                //    sChan = Math.Floor(iSLan / 10);
                //    sLe = iSLan - sChan * 10;
                //    if (sLe != 0)
                //    {
                //        sKQ = ((sChan + 1) * 10) + 1;

                //    }


                //    iSoLan = int.Parse(sKQ.ToString());


                //}

                //double iTmp = (iSoLan - 1);
                //iTmp = Math.Floor(iTmp / 10);
                //double iLan = (iSoLan - 1) % 10;
                //iLeft = iLeft + iLan * iWidth;
                //iTop = iTop + iHeight * iTmp;


                Microsoft.Office.Interop.Excel.ChartObjects chartObjs = (Microsoft.Office.Interop.Excel.ChartObjects)ExcelSheets.ChartObjects(Type.Missing);
                Microsoft.Office.Interop.Excel.ChartObject chartObj = chartObjs.Add(iLeft, iTop, iWidth, iHeight);
                Microsoft.Office.Interop.Excel.Chart xlChart = chartObj.Chart;
                Microsoft.Office.Interop.Excel.SeriesCollection xlSeriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)xlChart.SeriesCollection(Type.Missing);
                xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;
                Microsoft.Office.Interop.Excel.Series xlSeries = xlSeriesCollection.NewSeries();




                var _with1 = xlSeries;
                _with1.Name = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucBaoCaoThongKeCongNhanBD", "Thang", Commons.Modules.TypeLanguage);// "=Sheet1!$A$" + (vDong + 1);                 //"=A" + vDong;
                _with1.XValues = ExcelSheets.get_Range("" + sTenCotBD + "" + iDongBD + "", "" + sTenCotKT + "" + iDongKT); // cột tên (Nam - nữ -khác)
                _with1.Values = ExcelSheets.get_Range("" + sTenCotBDTyLe + "" + iDongBD_TL + "", "" + sTenCotKTTyLe + "" + iDongKT_TL); //"B33"); // Cột dữ liệu (Tỷ lệ :50% -50%)

                //Microsoft.Office.Interop.Excel.Range title = Commons.Modules.MExcel.GetRange(ExcelSheets, iDong, 2, iDong, 2);
                //Microsoft.Office.Interop.Excel.Range title1 = Commons.Modules.MExcel.GetRange(ExcelSheets, iDong, 3, iDong, 3);

                if (bTitile)
                    xlChart.ChartTitle.Text = Commons.Modules.ObjLanguages.GetLanguage(
                    Commons.Modules.ModuleName, "ucBaoCaoThongKeCongNhanBD", sTitle, Commons.Modules.TypeLanguage);
                //else
                //    xlChart.ChartTitle.Text = title.Value + " - " + title1.Value;
                //"=CONCATENATE(Sheet1!$B$" + (iDong) + ", \"-\" , Sheet1!$C$" + (iDong) + ")";
                //= CONCATENATE(B15, " - ", C15)
                xlChart.Refresh();

                _with1.HasDataLabels = true;
                Microsoft.Office.Interop.Excel.DataLabel dl1 = _with1.DataLabels(1);
                dl1.Font.Color = Color.FromArgb(255, 255, 255);
                dl1.Font.Size = 12;
                dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                dl1 = _with1.DataLabels(2);
                dl1.Font.Size = 12;
                dl1.Font.Color = Color.FromArgb(255, 255, 255);
                dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                //dl1.Border.Color = Color.FromArgb(255, 255, 255);
                _with1.Border.Color = Color.FromArgb(255, 255, 255);

                xlChart.HasTitle = true;
                xlChart.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionBottom;
                xlChart.ChartArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(10, 10, 255));
                xlChart.PlotArea.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                xlChart.PlotArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 255));
                xlChart.Legend.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;

                Microsoft.Office.Interop.Excel.Axis ax = xlChart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary) as Microsoft.Office.Interop.Excel.Axis;
                xlChart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, false, false, false, true);
                //ax.TickLabels.Orientation = Microsoft.Office.Interop.Excel.XlTickLabelOrientation.xlTickLabelOrientationUpward;


                xlChart.Refresh();



            }
            catch (Exception ex)
            { }
        }
    }
}
