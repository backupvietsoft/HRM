using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoThongKeCongNhanBD : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoThongKeCongNhanBD()
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

                        string NamBC;
                        NamBC = "";
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        switch (rdoChonBC.SelectedIndex)
                        {
                            case 0:
                                {
                                    try
                                    {
                                        //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        //    conn.Open();

                                        //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoPhanLoai", conn);
                                        //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        //    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        //    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        //    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        //    cmd.CommandType = CommandType.StoredProcedure;

                                        //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        //    DataSet ds = new DataSet();
                                        //    adp.Fill(ds);



                                        //    dt = new DataTable();
                                        //    dt = ds.Tables[0].Copy();
                                        //    dt.TableName = "DATA_GT";
                                        //    //frm.AddDataSource(dt);

                                        //    DataTable dt1 = new DataTable();
                                        //    dt1 = ds.Tables[1].Copy();
                                        //    dt1.TableName = "DATA_LCV";
                                        //    //frm.AddDataSource(dt1);

                                        //    DataTable dt2 = new DataTable();
                                        //    dt2 = ds.Tables[2].Copy();
                                        //    dt2.TableName = "DATA_IDD";
                                        //    //frm.AddDataSource(dt2);

                                        //    frm.rpt = new rptBieuDoPhanLoai(dt, dt1, dt2, 2021);
                                        BieuDoPhanLoai();
                                    }

                                    catch
                                    { }

                                }
                                break;
                            case 1:
                                {
                                    try
                                    {
                                        //conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        //conn.Open();

                                        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoChiaTheoDiaLy", conn);
                                        //cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        //cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        //cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        //cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        //cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        //cmd.CommandType = CommandType.StoredProcedure;

                                        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        //DataSet ds = new DataSet();
                                        //adp.Fill(ds);

                                        //DataTable dt1 = new DataTable();
                                        //dt1 = ds.Tables[1].Copy();
                                        //dt1.TableName = "DATA_PX";
                                        //frm.AddDataSource(dt1);

                                        //dt = new DataTable();
                                        //dt = ds.Tables[0].Copy();
                                        //dt.TableName = "DATA_Q";
                                        ////frm.AddDataSource(dt);



                                        //frm.rpt = new rptBieuDoChiaTheoDiaLy(dt, dt1);

                                        BieuDoChiaTheoDiaLy();
                                    }
                                    catch
                                    { }

                                    //frm.ShowDialog();
                                }
                                break;

                            case 2:
                                {
                                    BieuDoBaoCaoSoLaoDong();
                                    //_with1.Name = TDBCao;

                                    break;
                                }
                            case 3:
                                {
                                    BieuDoSoCNVaoTheoThang();
                                    break;
                                }
                            default: break;
                        }
                        //if (rdoChonBC.SelectedIndex != 3)
                        //    frm.ShowDialog();

                    }
                    break;

                default:
                    break;
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
                //xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;
                Microsoft.Office.Interop.Excel.Series xlSeries = xlSeriesCollection.NewSeries();
                xlChart.ChartType = xlChartType;



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
        private void LoadBieuDoCot(Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.XlChartType xlChartType, string sTenCotBD, int iDongBD, string sTenCotKT, int iDongKT, string sTenCotBDTyLe, int iDongBD_TL, string sTenCotKTTyLe, int iDongKT_TL, string sTitle, int iSoLan,
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
                //xlChart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlPie;
                Microsoft.Office.Interop.Excel.Series xlSeries = xlSeriesCollection.NewSeries();
                xlChart.ChartType = xlChartType;



                var _with1 = xlSeries;
                _with1.Name = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucBaoCaoThongKeCongNhanBD", "lblTyLe", Commons.Modules.TypeLanguage);// "=Sheet1!$A$" + (vDong + 1);                 //"=A" + vDong;
                _with1.XValues = ExcelSheets.get_Range("" + sTenCotBD + "" + iDongBD + "", "" + sTenCotKT + "" + iDongKT); // cột tên (Nam - nữ -khác)
                _with1.Values = ExcelSheets.get_Range("" + sTenCotBDTyLe + "" + iDongBD_TL + "", "" + sTenCotKTTyLe + "" + iDongKT_TL); //"B33"); // Cột dữ liệu (Tỷ lệ :50% -50%)
                //Microsoft.Office.Interop.Excel.Range title = Commons.Modules.MExcel.GetRange(ExcelSheets, iDong, 2, iDong, 2);
                //Microsoft.Office.Interop.Excel.Range title1 = Commons.Modules.MExcel.GetRange(ExcelSheets, iDong, 3, iDong, 3);

                if (bTitile)
                {
                    xlChart.ChartTitle.Text = sTitle;
                    xlChart.ChartTitle.Font.Size = 12;
                }


                //else
                //    xlChart.ChartTitle.Text = title.Value + " - " + title1.Value;
                //"=CONCATENATE(Sheet1!$B$" + (iDong) + ", \"-\" , Sheet1!$C$" + (iDong) + ")";
                //= CONCATENATE(B15, " - ", C15)
                xlChart.Refresh();

                Microsoft.Office.Interop.Excel.DataLabel dl1;
                _with1.HasDataLabels = true;
                for (int i = 1; i < iDongKT; i++)
                {
                    dl1 = _with1.DataLabels(i);
                    //dl1.Font.Color = Color.FromArgb(255, 255, 255);
                    dl1.Font.Size = 10;
                    //dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                }
                _with1.Border.Color = Color.FromArgb(255, 255, 255);

                xlChart.HasDataTable = true;
                xlChart.DataTable.HasBorderOutline = true;

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
        private void BieuDoPhanLoai()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtGioiTinh;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoPhanLoai", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
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

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "M2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BIỂU ĐỒ PHÂN LOẠI";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "X4"); //27 + 31
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
                    oSheet.Cells[row_dl, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Load dữ liệu Giới tính
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
                rowCnt++;
                formatRange = oSheet.get_Range("A" + "" + rowCnt + "");
                formatRange.Value = "Total";
                formatRange = oSheet.get_Range("B7");
                formatRange.Value = "=SUM(B5,B6)";
                formatRange = oSheet.get_Range("C7");
                formatRange.Value = "=SUM(C5,C6)";
                BorderAround(oSheet.get_Range("A4", "C7"));
                LoadBieuDoTron(oSheet, XlChartType.xl3DPie, CharacterIncrement(0), 5, CharacterIncrement(0), 6, CharacterIncrement(2), 5, CharacterIncrement(2), 6, "lblGioiTinh", 1, 10, 155, 240, 240, true);
                #endregion
                #region CNMay
                // Tạo cột công nhân may
                row_dl = 4;
                for (col = 0; col < dtCNMay.Columns.Count; col++)
                {
                    switch (dtCNMay.Columns[col].ColumnName.ToString())
                    {
                        case "TEN_LCV":
                            {
                                oSheet.Cells[row_dl, col + 6] = "Tỷ lệ CN may";
                                break;
                            }
                        case "TONG_SO_CMN":
                            {
                                oSheet.Cells[row_dl, col + 6] = "Tổng số";
                                break;
                            }
                        case "TY_LE_CNM":
                            {
                                oSheet.Cells[row_dl, col + 6] = "Tỷ lệ";
                                break;
                            }
                        default:
                            //oSheet.Cells[row_dl, col + 1] = dtBCThang.Columns[col].ColumnName.ToString();
                            break;
                    }
                    oSheet.Cells[row_dl, col + 6].ColumnWidth = 15;
                    oSheet.Cells[row_dl, col + 6].Interior.Color = Color.FromArgb(255, 255, 0);

                }

                //Load dữ liệu cong nhan may
                DataRow[] dr1 = dtCNMay.Select();
                string[,] rowData1 = new string[dr1.Count(), dtCNMay.Columns.Count];

                rowCnt = 0;
                foreach (DataRow row in dr1)
                {
                    for (col = 0; col < dtCNMay.Columns.Count; col++)
                    {
                        rowData1[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("F5", "H" + rowCnt.ToString()).Value2 = rowData1;
                for (col = 6; col < 9; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + "6");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                rowCnt++;
                formatRange = oSheet.get_Range("F" + "" + rowCnt + "");
                formatRange.Value = "Total";
                formatRange = oSheet.get_Range("G7");
                formatRange.Value = "=SUM(G5,G6)";
                formatRange = oSheet.get_Range("H7");
                formatRange.Value = "=SUM(H5,H6)";
                BorderAround(oSheet.get_Range("F4", "H7"));

                LoadBieuDoTron(oSheet, XlChartType.xl3DPie, CharacterIncrement(5), 5, CharacterIncrement(5), 6, CharacterIncrement(7), 5, CharacterIncrement(7), 6, "lblTyLeCNMay", 1, 345, 155, 245, 240, true);
                #endregion
                #region IDD
                // Tạo cột IDD
                row_dl = 4;
                for (col = 0; col < dtIDD.Columns.Count; col++)
                {
                    switch (dtIDD.Columns[col].ColumnName.ToString())
                    {
                        case "TONG_SO_IDD":
                            {
                                oSheet.Cells[row_dl, col + 11] = "Tổng số";
                                break;
                            }
                        case "TY_LE_IDD":
                            {
                                oSheet.Cells[row_dl, col + 11] = "Tỷ lệ";
                                break;
                            }
                        default:
                            oSheet.Cells[row_dl, col + 11] = dtIDD.Columns[col].ColumnName.ToString();
                            break;
                    }
                    oSheet.Cells[row_dl, col + 11].ColumnWidth = 15;
                    oSheet.Cells[row_dl, col + 11].Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Load dữ liệu IDD
                DataRow[] dr2 = dtIDD.Select();
                string[,] rowData2 = new string[dr2.Count(), dtIDD.Columns.Count];

                rowCnt = 0;
                foreach (DataRow row in dr2)
                {
                    for (col = 0; col < dtIDD.Columns.Count; col++)
                    {
                        rowData2[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("K5", "M" + rowCnt.ToString()).Value2 = rowData2;
                for (col = 11; col < 14; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + "6");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                rowCnt++;
                formatRange = oSheet.get_Range("K" + "" + rowCnt + "");
                formatRange.Value = "Total";
                formatRange = oSheet.get_Range("L7");
                formatRange.Value = "=SUM(L5,L6)";
                formatRange = oSheet.get_Range("M7");
                formatRange.Value = "=SUM(M5,M6)";
                BorderAround(oSheet.get_Range("K4", "M7"));
                LoadBieuDoTron(oSheet, XlChartType.xl3DPie, CharacterIncrement(10), 5, CharacterIncrement(10), 6, CharacterIncrement(12), 5, CharacterIncrement(12), 6, "lblIDD", 1, 690, 155, 245, 240, true);
                #endregion

                formatRange = oSheet.get_Range("A5", "M7"); //27 + 31
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

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
        private void BieuDoChiaTheoDiaLy()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoChiaTheoDiaLy", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);



                DataTable dtQuan = new DataTable();
                dtQuan = ds.Tables[0].Copy();

                DataTable dtPhuongXa = new DataTable();
                dtPhuongXa = ds.Tables[1].Copy();

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
                oSheet.Name = "Tổng hợp";
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "M2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BIỂU ĐỒ CHIA THEO ĐỊA LÝ";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "M4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                #region GioiTinh
                int col = 1;
                int row_dl = 4;
                for (col = 0; col < dtQuan.Columns.Count; col++)
                {
                    switch (dtQuan.Columns[col].ColumnName.ToString())
                    {
                        case "TEN_QUAN":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Công nhân may theo huyện";
                                break;
                            }
                        case "SO_LUONG":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Số lượng";
                                break;
                            }
                        case "TY_LE_QUAN":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Tỷ lệ";
                                break;
                            }
                        default:
                            //oSheet.Cells[row_dl, col + 1] = dtBCThang.Columns[col].ColumnName.ToString();
                            break;
                    }
                    oSheet.Cells[row_dl, col + 1].ColumnWidth = 15;
                    oSheet.Cells[row_dl, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                }

                DataRow[] dr = dtQuan.Select();
                string[,] rowData = new string[dr.Count(), dtQuan.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtQuan.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("A5", "C" + rowCnt.ToString()).Value2 = rowData;
                LoadBieuDoTron(oSheet, XlChartType.xlPie, CharacterIncrement(0), 5, CharacterIncrement(0), rowCnt, CharacterIncrement(2), 5, CharacterIncrement(2), rowCnt, "lblSoLuong", 1, 300, 50, 300, 300, true);

                Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt++;
                formatRange = oSheet.get_Range("A" + rowCnt + "");
                formatRange.Value = "Grand Total";
                formatRange = oSheet.get_Range("B" + rowCnt + "");
                formatRange.Value = "=SUM(B5:B" + (rowCnt - 1).ToString() + ")";
                formatRange = oSheet.get_Range("C" + rowCnt + "");
                formatRange.Value = "=SUM(C5:C" + (rowCnt - 1).ToString() + ")";

                for (col = 1; col <= 3; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + "" + rowCnt + "");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                //// Tạo hyperlink
                //for (int i = 5; i <= rowCnt; i++)
                //{
                //    formatRange = oSheet.get_Range("D" + i + ""); //27 + 31
                //    formatRange.Hyperlinks.Add(formatRange, "#Sheet2!C5", Type.Missing, "Microsoft", "Click me!");
                //}

                BorderAround(oSheet.get_Range("A4", "D" + rowCnt + ""));
                #endregion

                formatRange = oSheet.get_Range("A5", "C" + rowCnt + ""); //27 + 31
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                keepRowCnt = rowCnt;

                rowCnt = 0;
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 1;
                string cotCN_A = "";
                string cotCN_B = "";
                string[] TEN_QUAN = dtPhuongXa.AsEnumerable().Select(r => r.Field<string>("TEN_QUAN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[1].Copy(); // Dữ row count data

                for (int i = 0; i < TEN_QUAN.Count(); i++)
                {
                    Worksheet sheet2;
                    sheet2 = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    sheet2 = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    sheet2.Name = TEN_QUAN[i].ToString();

                    // Tạo hyperlink
                    for (int j = 5; j <= keepRowCnt - 1; j++)
                    {
                        formatRange = oSheet.get_Range("A" + j + "");
                        if (formatRange.Value == TEN_QUAN[i].ToString())
                        {
                            formatRange = oSheet.get_Range("D" + j + ""); //27 + 31
                            formatRange.ColumnWidth = 15;
                            formatRange.Font.Size = fontSizeNoiDung;
                            formatRange.Font.Name = fontName;
                            formatRange.Hyperlinks.Add(formatRange, "#'" + sheet2.Name + "'!C5", Type.Missing, "Microsoft", "Xem chi tiết");
                        }
                    }

                    dtPhuongXa = ds.Tables[1].Copy();
                    dtPhuongXa = dtPhuongXa.AsEnumerable().Where(r => r.Field<string>("TEN_QUAN") == TEN_QUAN[i]).CopyToDataTable().Copy();
                    DataRow[] dr1 = dtPhuongXa.Select();
                    current_dr = dr1.Count();
                    string[,] rowData1 = new string[dr1.Count(), dtPhuongXa.Columns.Count - 1];
                    foreach (DataRow row in dr1)
                    {
                        for (col = 0; col < dtPhuongXa.Columns.Count - 1; col++)
                        {
                            rowData1[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                    }
                    rowBD = rowBD + dr_Cu + rowBD_XN;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;

                    bool flag = false;
                    // Tạo cột
                    for (col = 0; col < dtPhuongXa.Columns.Count; col++)
                    {
                        switch (dtPhuongXa.Columns[col].ColumnName.ToString())
                        {
                            case "TEN_PX":
                                {
                                    sheet2.Cells[rowBD, col + 1] = TEN_QUAN[i].ToString();
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 25;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                            case "TEN_QUAN":
                                {
                                    if (flag == false)
                                    {
                                        col--;
                                        flag = true;
                                    }
                                    break;
                                }
                            case "KHOANG_CACH":
                                {
                                    sheet2.Cells[rowBD, col + 1] = "Khoảng cách";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                            case "SO_CN_MAY":
                                {
                                    sheet2.Cells[rowBD, col + 1] = "Số công nhân may";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    sheet2.Cells[rowBD, col + 1].WrapText = true;
                                    break;
                                }
                            case "SO_LUONG":
                                {
                                    sheet2.Cells[rowBD, col + 1] = "Số lượng";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                            case "TY_LE_PX":
                                {
                                    sheet2.Cells[rowBD, col + 1] = "Tỷ lệ";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                            case "GHI_CHU":
                                {
                                    sheet2.Cells[rowBD, col + 1] = "Ghi chú";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                            default:
                                {
                                    sheet2.Cells[rowBD, col + 1] = dtPhuongXa.Columns[col].ColumnName.ToString();
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                        }
                    }

                    //Đổ dữ liệu từng sheet
                    sheet2.get_Range("A" + (rowBD + 1) + "", "G" + (rowCnt + 1).ToString()).Value2 = rowData1;

                    for (col = 4; col <= 6; col++)
                    {
                        formatRange = sheet2.get_Range("" + CharacterIncrement(col - 1) + "2", "" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + "");
                        formatRange.NumberFormat = "0.0;-0;;@";
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }
                    BorderAround(sheet2.get_Range("A1", "G" + (rowCnt + 1).ToString() + ""));

                    formatRange = sheet2.get_Range("A1", "G1"); //27 + 31
                    formatRange.Font.Size = fontSizeTieuDe;
                    formatRange.Font.Bold = true;
                    formatRange.Font.Name = fontName;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = sheet2.get_Range("A2", "G" + (rowCnt + 1).ToString() + ""); //27 + 31
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    LoadBieuDoCot(sheet2, XlChartType.xlBarClustered, CharacterIncrement(1), 2, CharacterIncrement(1), rowCnt + 1, CharacterIncrement(5), 2, CharacterIncrement(5), rowCnt + 1, "Số công nhân may theo xã của" + " " + TEN_QUAN[i].ToString(), 1, 700, 05, 300, 300, true);

                    formatRange = sheet2.get_Range("P1"); //27 + 31
                    formatRange.Font.Size = fontSizeTieuDe;
                    formatRange.Font.Name = fontName;
                    formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                    formatRange.Hyperlinks.Add(formatRange, "#'Tổng hợp'!C5", Type.Missing, "Microsoft", "Trở lại");

                    dr_Cu = current_dr;
                    //keepRowCnt = rowCnt;
                    rowCnt = 0;
                    rowBD = 0;
                    rowBD_XN = 0;
                    dr_Cu = 0;
                }



                oSheet.Activate();

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
        private void BieuDoBaoCaoSoLaoDong()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoBaoCaoSoLD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Convert.ToDateTime(dtTuNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dtBieuDo = new DataTable();
                dtBieuDo = ds.Tables[1].Copy();


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
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THEO DÕI LAO ĐỘNG";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                int col = 0;
                int row_dl = 4;
                for (col = 0; col < dtBCThang.Columns.Count; col++)
                {
                    switch (dtBCThang.Columns[col].ColumnName.ToString())
                    {
                        case "TT":
                            {
                                oSheet.Cells[row_dl, col + 1] = "";
                                break;
                            }
                        default:
                            oSheet.Cells[row_dl, col + 1] = dtBCThang.Columns[col].ColumnName.ToString();
                            break;
                    }
                    oSheet.Cells[row_dl, col + 1].ColumnWidth = 15;
                }

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.get_Range("A5", lastColumn + rowCnt.ToString()).Value2 = rowData;
                rowCnt++;
                oSheet.get_Range("A" + rowCnt + "").Value2 = "Grand Total";
                for (col = 2; col <= dtBCThang.Columns.Count; col++)
                {
                    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).Value2 = "=+SUM(" + CharacterIncrement(col - 1) + "5:" + CharacterIncrement(col - 1) + "6)";
                }

                rowCnt++;
                oSheet.get_Range("A" + rowCnt + "").Value2 = "IDD";
                for (col = 2; col <= dtBCThang.Columns.Count; col++)
                {
                    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).Value2 = "=+" + CharacterIncrement(col - 1) + "5/" + CharacterIncrement(col - 1) + "6";
                    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).NumberFormat = "0.00;-0;;@";
                    oSheet.get_Range(CharacterIncrement(col - 1) + rowCnt).TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                //Microsoft.Office.Interop.Excel.Range formatRange;
                ////rowCnt = keepRowCnt + 2;

                //////dịnh dạng
                //////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);
                Microsoft.Office.Interop.Excel.Range formatRange;
                string CurentColumn = string.Empty;
                int colBD = 1;
                int colKT = dtBCThang.Columns.Count;
                //format

                for (col = colBD; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "5", CurentColumn + (rowCnt - 2).ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));

                //formatRange = oSheet.get_Range("N11", lastColumn + "13");

                row_dl = 11;
                for (col = 0; col < dtBieuDo.Columns.Count; col++)
                {
                    oSheet.Cells[row_dl, col + 14].ColumnWidth = 15;
                    switch (dtBieuDo.Columns[col].ColumnName.ToString())
                    {
                        case "IDD":
                            {
                                oSheet.Cells[row_dl, col + 14] = "";
                                break;
                            }
                        case "TONG_SO_IDD":
                            {
                                oSheet.Cells[row_dl, col + 14] = "Số lượng";
                                oSheet.Cells[row_dl, col + 14].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                oSheet.Cells[row_dl, col + 14].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                break;
                            }
                        case "TY_LE_IDD":
                            {
                                oSheet.Cells[row_dl, col + 14] = "%";
                                oSheet.Cells[row_dl, col + 14].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                oSheet.Cells[row_dl, col + 14].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                break;
                            }
                        default:
                            break;
                    }

                }

                DataRow[] dr1 = dtBieuDo.Select();
                string[,] rowData1 = new string[dr.Count(), dtBieuDo.Columns.Count];

                int rowCnt1 = 0;
                foreach (DataRow row1 in dr1)
                {
                    for (col = 0; col < dtBieuDo.Columns.Count; col++)
                    {
                        rowData1[rowCnt1, col] = row1[col].ToString();
                    }
                    rowCnt1++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("N12", lastColumn + rowCnt.ToString()).Value2 = rowData1;

                formatRange = oSheet.get_Range("A5", lastColumn + "" + rowCnt + ""); //27 + 31
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                BorderAround(oSheet.get_Range("N11", lastColumn + rowCnt.ToString()));

                for (col = 14; col <= 16; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "11", "" + CharacterIncrement(col - 1) + "13");
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                }

                for (col = 14; col <= 16; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "12", "" + CharacterIncrement(col - 1) + "13");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }


                ////LoadBieuDo(oSheet, 13, 16, "", 1, 10, 80, 300, 250, true);

                //Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
                //Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(200, 500, 200, 100);
                //Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

                Microsoft.Office.Interop.Excel.Range chartRange;
                Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)oSheet.ChartObjects(Type.Missing);
                Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(80, 155, 300, 250);
                Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
                chartRange = oSheet.get_Range("N12", "P13");
                chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;
                Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = chartPage.SeriesCollection();
                Microsoft.Office.Interop.Excel.Series series1 = seriesCollection.NewSeries();
                series1.ChartType = XlChartType.xl3DPie;
                series1.XValues = oSheet.Range["N12", "N13"];
                series1.Values = oSheet.Range["P12", "P13"];

                series1.HasDataLabels = true;
                Microsoft.Office.Interop.Excel.DataLabel dl1 = series1.DataLabels(1);
                dl1.Font.Color = Color.FromArgb(255, 255, 255);
                dl1.Font.Size = 12;
                dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                dl1 = series1.DataLabels(2);
                dl1.Font.Size = 12;
                dl1.Font.Color = Color.FromArgb(255, 255, 255);
                dl1.Position = XlDataLabelPosition.xlLabelPositionInsideEnd;
                //dl1.Border.Color = Color.FromArgb(255, 255, 255);
                series1.Border.Color = Color.FromArgb(255, 255, 255);


                chartPage.HasTitle = true;
                chartPage.ChartTitle.Text = "IDD";
                chartPage.Refresh();

                chartPage.Legend.Position = Microsoft.Office.Interop.Excel.XlLegendPosition.xlLegendPositionBottom;
                chartPage.ChartArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(10, 10, 255));
                chartPage.PlotArea.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                chartPage.PlotArea.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 255, 255));
                chartPage.Legend.Border.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
                Microsoft.Office.Interop.Excel.Axis ax = chartPage.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary) as Microsoft.Office.Interop.Excel.Axis;
                chartPage.Refresh();
                chartPage.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, false, false, false, true);
                //chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;

                //chartPage.Refresh();

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
        private void BieuDoSoCNVaoTheoThang()
        {
            try
            {
                //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoTongLDVaoLamThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToInt32(txNam.Text);
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dtNam = new DataTable();
                dtNam = ds.Tables[0].Copy();

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

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                //oSheet.Name = "Tổng hợp";
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtNam.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "M2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THEO DÕI LAO ĐỘNG";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "D4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(191, 191, 191);

                #region Nam
                Range row4_TieuDe_Nam = oSheet.get_Range("A4");
                row4_TieuDe_Nam.Value = "NĂM";
                row4_TieuDe_Nam.ColumnWidth = 22;

                Range row4_TieuDe_ThaNG = oSheet.get_Range("B4");
                row4_TieuDe_ThaNG.Value = "THÁNG";
                row4_TieuDe_ThaNG.ColumnWidth = 17;

                Range row4_TieuDe_TONG = oSheet.get_Range("C4");
                row4_TieuDe_TONG.Value = "TỔNG";
                row4_TieuDe_TONG.ColumnWidth = 13;

                Range row4_TieuDe_TYLE = oSheet.get_Range("D4");
                row4_TieuDe_TYLE.Value = "TỶ LỆ";
                row4_TieuDe_TYLE.ColumnWidth = 13;
                row4_TieuDe_TYLE.RowHeight = 30;
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                #endregion
                Microsoft.Office.Interop.Excel.Range formatRange;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 5;
                string[] NAM = dtNam.AsEnumerable().Select(r => r.Field<string>("NAM")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                string sCotGocC = "";
                string sCotGocD = "";
                Range row_groupTONG_Format;
                for (int i = 0; i < NAM.Count(); i++)
                {
                    dtNam = ds.Tables[0].Copy();
                    dtNam = dtNam.AsEnumerable().Where(r => r.Field<string>("NAM") == NAM[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtNam.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtNam.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtNam.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        if (dr_Cu != 1)
                        {
                            rowCONG = 1;
                        }
                        else
                        {
                            rowCONG = 0;
                        }
                    }
                    rowBD = rowBD + dr_Cu + rowCONG;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;

                    oSheet.get_Range("A" + (rowBD) + "", lastColumn + (rowCnt).ToString()).Value2 = rowData;
                    formatRange = oSheet.get_Range("A" + (rowBD));
                    string s = formatRange.Value;
                    formatRange = oSheet.get_Range("A" + (rowBD) + "", "C" + (rowCnt).ToString());
                    if (Convert.ToInt32(s.Substring(0, 4)) % 2 == 0)
                    {
                        formatRange.Interior.Color = Color.FromArgb(180, 199, 231);
                    }
                    else
                    {
                        formatRange.Interior.Color = Color.FromArgb(169, 209, 142);
                    }
                    if (current_dr != 1 && current_dr != 0)
                    {

                        //Tính tổng 
                        row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr) + "".ToString(), lastColumn + "" + (rowBD + current_dr) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                        row_groupTONG_Format.Interior.Color = Color.FromArgb(197, 224, 180);
                        //row_groupTONG_Format.Font.Bold = true;

                        row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr - 1) + "".ToString());

                        oSheet.Cells[(rowBD + current_dr), 1] = row_groupTONG_Format.Value;

                        for (int j = 0; j < 12; j++)
                        {
                            row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + j) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                            row_groupTONG_Format.Value = new DateTime(2022, j + 1, 05).ToString("MMM", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        formatRange = oSheet.get_Range("A" + (rowBD) + "", "C" + (rowCnt).ToString());
                        formatRange.Interior.Color = Color.FromArgb(197, 224, 180);
                        //row_groupTONG_Format.Interior.Color = Color.FromArgb(197, 224, 180);
                        oSheet.Cells[(rowBD + current_dr), 3] = "=SUM(C" + rowBD + ":C" + rowCnt + ")";
                        oSheet.Cells[(rowBD + current_dr), 4] = "=SUM(D" + rowBD + ":D" + rowCnt + ")";

                        sCotGocC = sCotGocC + CharacterIncrement(2) + (rowBD + current_dr) + "+";
                        sCotGocD = sCotGocD + CharacterIncrement(3) + (rowBD + current_dr) + "+";

                        //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();
                    }
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                for (col = 3; col <= 4; col++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "5", "" + CharacterIncrement(col - 1) + "" + rowCnt + "");
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                rowCnt++;
                //sCotGocC.Substring(0, sCotGocC.Length - 1);
                formatRange = oSheet.get_Range("A" + (rowCnt + 1) + "");
                formatRange.Value = "SUBTOTAL";
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange = oSheet.get_Range("B" + (rowCnt + 1) + "");
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange = oSheet.get_Range("C" + (rowCnt + 1) + "");
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange.Value = "=SUM(C5:C" + (rowCnt) + ")-(" + sCotGocC.Substring(0, sCotGocC.Length - 1) + ")";
                formatRange = oSheet.get_Range("D" + (rowCnt + 1) + "");
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange.Value = "=SUM(D5:D" + (rowCnt) + ")-(" + sCotGocD.Substring(0, sCotGocD.Length - 1) + ")";

                LoadBieuDoCot(oSheet, XlChartType.xlColumnClustered, CharacterIncrement(0), 5, CharacterIncrement(0), rowCnt, CharacterIncrement(3), 5, CharacterIncrement(3), rowCnt, "Tổng người lao động theo tháng", 1, 400, 50, 1000, 1200, true);
                //sCotGocC;

                formatRange = oSheet.get_Range("A5", "D" + (rowCnt + 1).ToString() + "");
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt + 1).ToString()));

                formatRange = oSheet.get_Range("A5", "C" + (rowCnt + 1).ToString() + "");
                formatRange.Font.Bold = true;

                formatRange = oSheet.get_Range("D5", "D" + (rowCnt).ToString() + "");
                formatRange.Interior.Color = Color.FromArgb(180, 199, 231);


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
        private void ucBaoCaoThongKeCongNhanBD_Load(object sender, EventArgs e)
        {

            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            lk_NgayIn.EditValue = DateTime.Today;
            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            txNam.Text = (DateTime.Today.Year.ToString());
            Commons.Modules.sLoad = "";

        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    switch (rdo_ChonBaoCao.SelectedIndex)
            //    {
            //        case 0:
            //            {
            //                dtTuNgay.Enabled = true;
            //                dtDenNgay.Enabled = true;
            //                txNam.Enabled = false;
            //            }
            //            break;

            //        case 1:
            //            {
            //                dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 1, 1);
            //                dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 6, 30);
            //                dtTuNgay.Enabled = false;
            //                dtDenNgay.Enabled = false;
            //                txNam.Enabled = true;
            //            }
            //            break;
            //        case 2:
            //            {
            //                dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 7, 1);
            //                dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 12, 31);
            //                dtTuNgay.Enabled = false;
            //                dtDenNgay.Enabled = false;
            //                txNam.Enabled = true;
            //            }
            //            break;

            //        default:
            //            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            //            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            //            dtTuNgay.Enabled = true;
            //            dtDenNgay.Enabled = true;
            //            break;
            //    }
            //}
            //catch
            //{ }
        }

        private void txNam_EditValueChanged(object sender, EventArgs e)
        {
            //if (rdo_ChonBaoCao.SelectedIndex==1)
            //{
            //    dtTuNgay.EditValue = Convert.ToDateTime(("01/01/" + txNam.Text));
            //    dtDenNgay.EditValue = Convert.ToDateTime(("30/06/" + txNam.Text));
            //}
            //if(rdo_ChonBaoCao.SelectedIndex ==2)
            //{
            //    dtTuNgay.EditValue = Convert.ToDateTime(("01/07/" + txNam.Text));
            //    dtDenNgay.EditValue = Convert.ToDateTime(("31/12/" + txNam.Text));
            //}

        }
        private void DrawFractionChart(Microsoft.Office.Interop.Excel.Worksheet activeSheet, Microsoft.Office.Interop.Excel.ChartObjects xlCharts, Microsoft.Office.Interop.Excel.Range xRange, Microsoft.Office.Interop.Excel.Range yRange)
        {
            Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(200, 500, 200, 100);
            Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

            Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = chartPage.SeriesCollection();
            Microsoft.Office.Interop.Excel.Series series1 = seriesCollection.NewSeries();
            series1.XValues = activeSheet.Range["N12", "N13"];
            series1.Values = activeSheet.Range["P12", "P13"];

            chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xl3DPie;

            //Microsoft.Office.Interop.Excel.Axis axis = chartPage.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary) as Microsoft.Office.Interop.Excel.Axis;

            series1.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, true, true, true, true, true, true);
        }
    }
}
