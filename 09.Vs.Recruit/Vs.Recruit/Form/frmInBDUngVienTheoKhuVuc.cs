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
using DevExpress.XtraEditors.Filtering.Templates;

namespace Vs.Recruit
{
    public partial class frmInBDUngVienTheoKhuVuc : DevExpress.XtraEditors.XtraForm
    {
        private string SaveExcelFile;
        public frmInBDUngVienTheoKhuVuc()
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
                        Commons.Modules.ObjSystems.ShowWaitForm(this);
                        BieuDoChiaTheoDiaLy();
                        Commons.Modules.ObjSystems.HideWaitForm();
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
        private void frmInBDUngVienTheoKhuVuc_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            dTuNgay.EditValue = Convert.ToDateTime("01/01/" + DateTime.Today.Year);
            dDenNgay.EditValue = Convert.ToDateTime("31/12/" + DateTime.Today.Year);
            Commons.Modules.sLoad = "";
        }

        #region Excel
        private void BieuDoChiaTheoDiaLy()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoUngVienChiaTheoDiaLy", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = dTuNgay.EditValue;
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = dDenNgay.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);


                DataTable dtQuan = new DataTable();
                dtQuan = ds.Tables[0].Copy();

                DataTable dtPhuongXa = new DataTable();
                dtPhuongXa = ds.Tables[1].Copy();

                if (dtQuan.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.HideWaitForm();
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                    return;
                }

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    Commons.Modules.ObjSystems.HideWaitForm();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
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


                try
                {
                    Microsoft.Office.Interop.Excel.Worksheet worksheet2 = (Worksheet)oWB.Worksheets["Sheet2"];
                    worksheet2.Delete();
                    Microsoft.Office.Interop.Excel.Worksheet worksheet3 = (Worksheet)oWB.Worksheets["Sheet3"];
                    worksheet3.Delete();
                }
                catch
                {
                }

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
                row2_TieuDe_BaoCao.Value2 = "BIỂU ĐỒ ỨNG VIÊN CHIA THEO ĐỊA LÝ";


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
                                oSheet.Cells[row_dl, col + 1] = "Ứng viên theo huyện";
                                break;
                            }
                        case "SO_LUONG":
                            {
                                oSheet.Cells[row_dl, col + 1] = "Số lượng";
                                break;
                            }
                        case "TY_LE_QUAN":
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
                BorderAround(oSheet.Range[oSheet.Cells[row_dl, 1], oSheet.Cells[(row_dl + dtQuan.Rows.Count + 1), 3]]);

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
                LoadBieuDoTron(oSheet, XlChartType.xlPie, CharacterIncrement(0), 5, CharacterIncrement(0), rowCnt, CharacterIncrement(2), 5, CharacterIncrement(2), rowCnt, "lblSoLuong", 1, 300, 50, rowCnt < 5 ? 300 : 550, rowCnt < 5 ? 300 : 500, true);

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
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }


                formatRange = oSheet.get_Range("C5", "" + "C" + rowCnt + "");
                formatRange.NumberFormat = "0.0%";
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
                    if (chanVongDau == "Chan")
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
                    rowCnt = rowBD + current_dr - 1;

                    bool flag = false;
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
                                    sheet2.Cells[rowBD, col + 1] = "Phần trăm";
                                    sheet2.Cells[rowBD, col + 1].ColumnWidth = 15;
                                    sheet2.Cells[rowBD, col + 1].Interior.Color = Color.FromArgb(255, 255, 0);
                                    break;
                                }
                        }
                    }

                    //Đổ dữ liệu từng sheet
                    sheet2.get_Range("A" + (rowBD + 1) + "", "F" + (rowCnt + 1).ToString()).Value2 = rowData1;

                    for (col = 4; col <= 6; col++)
                    {
                        formatRange = sheet2.get_Range("" + CharacterIncrement(col - 1) + "2", "" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + "");
                        formatRange.NumberFormat = "0";
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }
                    BorderAround(sheet2.get_Range("A1", "F" + (rowCnt + 1).ToString() + ""));

                    formatRange = sheet2.get_Range("A1", "F1"); //27 + 31
                    formatRange.Font.Size = fontSizeTieuDe;
                    formatRange.Font.Bold = true;
                    formatRange.Font.Name = fontName;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = sheet2.get_Range("A2", "F" + (rowCnt + 1).ToString() + ""); //27 + 31
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = sheet2.get_Range("F2", "" + "F" + (rowCnt + 1) + "");
                    formatRange.NumberFormat = "0.0%";

                    LoadBieuDoCot(sheet2, XlChartType.xlBarClustered, CharacterIncrement(1), 2, CharacterIncrement(1), rowCnt + 1, CharacterIncrement(5), 2, CharacterIncrement(5), rowCnt + 1, "Số công nhân may theo xã của" + " " + TEN_QUAN[i].ToString(), 1, 700, 35, rowCnt < 10 ? 400 : 700, rowCnt < 10 ? 400 : 800, true);

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

                Commons.Modules.MExcel.ThemDong((Excel.Worksheet)oSheet, XlInsertShiftDirection.xlShiftDown, 1, 3);
                Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A3", "M3"); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");

                oSheet.Activate();
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
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

                if (bTitile)
                {
                    xlChart.ChartTitle.Text = sTitle;
                    xlChart.ChartTitle.Font.Size = 12;
                }
                xlChart.Refresh();
                Microsoft.Office.Interop.Excel.DataLabel dl1;
                _with1.HasDataLabels = true;
                for (int i = 1; i < iDongKT; i++)
                {
                    dl1 = _with1.DataLabels(i);
                    dl1.Font.Size = 10;
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


                xlChart.ApplyDataLabels(Microsoft.Office.Interop.Excel.XlDataLabelsType.xlDataLabelsShowPercent, true, true, false, false, false, false, true);
            }
            catch (Exception ex)
            { }
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
                    Commons.Modules.ModuleName, "ucBaoCaoThongKeCongNhanBD", sTitle, Commons.Modules.TypeLanguage);
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
