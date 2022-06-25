using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace Vs.Payroll
{
    public partial class ucBCLuongThang : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBCLuongThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
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

        private void ucBCLuongThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadThang();
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCACH_TINH_LUONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, -1));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCachTinhLuong, dt, "ID_CTL", "TEN", "TEN");
            cboCachTinhLuong.EditValue = 2; 
            lk_NgayIn.EditValue = DateTime.Today;
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
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

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        frmViewReport frm = new frmViewReport();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangSP", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {
                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 6;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 14;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AZ3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + sThang;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AZ4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + sThang;

                                        Range row6_TieuDe_Format = ws.get_Range("A6", "AZ8");
                                        row6_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row6_TieuDe_Format.Font.Name = fontName;
                                        row6_TieuDe_Format.Font.Bold = true;
                                        row6_TieuDe_Format.WrapText = true;
                                        row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                                            ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 2, col] = col;
                                        }

                                        ws.get_Range("A6", "AZ7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AZ8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A6", "AZ8"));
                                        row = 8;

                                        string TienMat = "";
                                        string ATM = "";

                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            TienMat = "";
                                            ATM = "";

                                            if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                                            {
                                                TienMat = "=AW" + row;
                                            }
                                            else
                                            {
                                                ATM = "=AW" + row;
                                            }

                                            Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                            dynamic[] arr = { row2["MA"].ToString(), stt, row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["GIOI_TINH"].ToString(), row2["TEN_TO"].ToString(),
                                            row2["TEN_CV"].ToString(), row2["NGAY_VL"].ToString(), row2["LUONG_HDLD"].ToString(), row2["NGAY_CONG"].ToString(), row2["GIO_CONG"].ToString(),
                                            row2["LSP"].ToString(), row2["TIEN_CDPS"].ToString(), "=SUM(L" + row + ":M" + row +")", row2["PHEP"].ToString(),
                                            "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*O" + row,
                                            row2["TC_NT"].ToString(), "=IF(Q"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*50%*Q"+ row +",0),0)",
                                            row2["TC_226"].ToString(), "=IF(S"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*S"+ row +",0),0)",
                                            row2["LAM_DEM"].ToString(), "=IF(U"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*30%*U"+ row +",0),0)",
                                            row2["TC_CN"].ToString(), "=IF(W"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*W"+ row +",0),0)",
                                            row2["VRCL"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*Y" + row, row2["LE_TET"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*AA" + row,
                                            row2["GIO_CN"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "8*AC" + row, row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(),
                                            row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(), row2["TIEN_CON_NHO"].ToString(), row2["TIEN_NGUYET_SAN"].ToString(),
                                            "=IF((("+  row2["MUC_BU_LUONG"].ToString() +"/(" + row2["NC_CHUAN"].ToString() + "*8))*(J"+ row +"*8+O"+ row +"*8+AA"+ row +"*8+Q"+ row +"*1.5))>(N"+ row +"+P"+ row +"+R"+ row +"+AB"+ row +"),(" + row2["MUC_BU_LUONG"].ToString() + "/(" + row2["NC_CHUAN"].ToString() + "*8))*(J"+ row +"*8+AA"+ row +"*8+O"+ row +"*8+Q"+ row +"*1.5)-(N"+ row +"+P"+ row +"+R"+ row +"+AB"+ row +"),0)",
                                            row2["TIEN_CONG_KHAC"].ToString(),"=ROUND(N"+ row +"+P"+ row +"+R"+ row +"+T"+ row +"+V"+ row +"+X"+ row +"+Z"+ row +"+AB"+ row +"+AD"+ row +"+SUM(AF"+ row +":AL"+ row +"),0)",
                                            row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                            "=ROUND(SUM(AN"+ row +":AR"+ row +"),0)","=AM"+ row +"-AS"+ row,row2["PHEP_TT"].ToString(),"=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*AU" + row,
                                            "=AT" + row + "+AV" + row, TienMat, ATM };


                                            Range rowData = ws.get_Range("A" + row, "AY" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 9; colSUM < 52; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("K9", "N" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("W9", "W" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("X9", "X" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Y9", "Y" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("Z9", "Z" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AA9", "AA" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("AB9", "AB" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AC9", "AC" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("AD9", "AD" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AE9", "AT" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AU9", "AU" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("AV9", "AY" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        ws.get_Range("A9", "B" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("A9", "B" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AZ" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AZ" + row));
                                    }
                                    catch
                                    { }
                                }
                                break;
                            case 1:
                                {


                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangQL", conn);

                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {

                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 6;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 14;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AS3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + sThang;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AS4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + sThang;

                                        Range row6_TieuDe_Format = ws.get_Range("A6", "AS8");
                                        row6_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row6_TieuDe_Format.Font.Name = fontName;
                                        row6_TieuDe_Format.Font.Bold = true;
                                        row6_TieuDe_Format.WrapText = true;
                                        row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                                            ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 2, col] = col;
                                        }

                                        ws.get_Range("A6", "AS7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AS8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A6", "AS8"));
                                        row = 8;

                                        string TienMat = "";
                                        string ATM = "";

                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            TienMat = "";
                                            ATM = "";

                                            if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                                            {
                                                TienMat = "=AP" + row;
                                            }
                                            else
                                            {
                                                ATM = "=AP" + row;
                                            }

                                            Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            rowDataFDate.NumberFormat = "dd/MM/yyyy";

                                            dynamic[] arr = { row2["MA"].ToString(), stt, row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["GIOI_TINH"].ToString(), row2["TEN_TO"].ToString(),
                                                row2["TEN_CV"].ToString(), row2["NGAY_VL"].ToString(), row2["LUONG_CB"].ToString(), row2["NGAY_CONG"].ToString(), row2["LUONG_CBQL"].ToString(),
                                                row2["PC_DT"].ToString(), row2["MUC_HT_DT"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*J" + row,
                                                "=(L" + row + "*M" + row + "/100)/" + row2["NC_CHUAN"].ToString() + "*J" + row, "=SUM(N" + row + ":O" + row + ")",
                                                row2["PHEP"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*Q" + row,
                                                row2["LE_TET"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*S" + row,
                                                row2["VRCL"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*U" + row,
                                                row2["GIO_CN"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*W" + row,
                                                row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(), row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(),
                                                row2["TIEN_CON_NHO"].ToString(), row2["TIEN_NGUYET_SAN"].ToString(), row2["TIEN_CONG_KHAC"].ToString(),
                                                "=ROUND(P" + row + "+R" + row + "+T" + row + "+V" + row + "+X" + row + "+Z" + row + "+AA" + row + "+AB" + row + "+AC" + row + "+AD" + row + "+AE" + row + ",0)",
                                                row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                                "=ROUND(SUM(AG"+ row +":AK"+ row +"),0)","=AF"+ row +"-AL"+ row,row2["PHEP_TT"].ToString(),"=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*AN" + row,
                                                "=AM" + row + "+AO" + row, TienMat, ATM };

                                            Range rowData = ws.get_Range("A" + row, "AR" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 9; colSUM < 45; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("K9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("W9", "AR" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        ws.get_Range("A9", "B" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("A9", "B" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AS" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AS" + row));
                                    }
                                    catch
                                    { }

                                }
                                break;
                            case 2:
                                {

                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangTG", conn);

                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {

                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 6;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 14;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AQ3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + sThang;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AQ4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + sThang;

                                        Range row6_TieuDe_Format = ws.get_Range("A6", "AQ8");
                                        row6_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row6_TieuDe_Format.Font.Name = fontName;
                                        row6_TieuDe_Format.Font.Bold = true;
                                        row6_TieuDe_Format.WrapText = true;
                                        row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                                            ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 2, col] = col;
                                        }

                                        ws.get_Range("A6", "AQ7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AQ8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A6", "AQ8"));
                                        row = 8;

                                        string TienMat = "";
                                        string ATM = "";

                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            TienMat = "";
                                            ATM = "";

                                            if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                                            {
                                                TienMat = "=AN" + row;
                                            }
                                            else
                                            {
                                                ATM = "=AN" + row;
                                            }

                                            Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            rowDataFDate.NumberFormat = "dd/MM/yyyy";

                                            dynamic[] arr = { row2["MA"].ToString(), stt, row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["GIOI_TINH"].ToString(), row2["TEN_TO"].ToString(),
                                                row2["TEN_CV"].ToString(), row2["NGAY_VL"].ToString(), row2["LUONG_CB"].ToString(), row2["NGAY_CONG"].ToString(), row2["LUONG_KHOAN"].ToString(),
                                                "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*J" + row, row2["PHEP"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*M" + row,
                                                row2["LE_TET"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*O" + row, row2["VRCL"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*Q" + row,
                                                row2["TC_NT"].ToString(),"=(K" + row + "/208)*S" + row + "*1.5", row2["TC_CN"].ToString(),"=(K" + row + "/208)*U" + row + "*2",
                                                row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(), row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(), row2["TIEN_CON_NHO"].ToString(),
                                                row2["TIEN_NGUYET_SAN"].ToString(), row2["TIEN_CONG_KHAC"].ToString(),"=ROUND(L" + row + " + N" + row + " + P" + row + " + R" + row + " + T" + row + " + V" + row + " + SUM(X"+ row +":AC"+ row + "),0)",
                                                row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                                "=ROUND(SUM(AE"+ row +":AI"+ row +"),0)","=AD"+ row +"-AJ"+ row,row2["PHEP_TT"].ToString(),"=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*AL" + row,
                                                "=AK" + row + "+AM" + row, TienMat, ATM };

                                            Range rowData = ws.get_Range("A" + row, "AP" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 9; colSUM < 43; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("K9", "L" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("M9", "M" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("N9", "N" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("W9", "AP" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        ws.get_Range("A9", "B" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("A9", "B" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AQ" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AQ" + row));
                                    }
                                    catch
                                    { }

                                }
                                break;
                            case 3:
                                {

                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangQC", conn);

                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {

                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 6;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 14;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AQ3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + sThang;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AQ4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + sThang;

                                        Range row6_TieuDe_Format = ws.get_Range("A6", "AQ8");
                                        row6_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row6_TieuDe_Format.Font.Name = fontName;
                                        row6_TieuDe_Format.Font.Bold = true;
                                        row6_TieuDe_Format.WrapText = true;
                                        row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                                            ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 2, col] = col;
                                        }

                                        ws.get_Range("A6", "AP7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AP8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A6", "AP8"));
                                        row = 8;

                                        string TienMat = "";
                                        string ATM = "";

                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            TienMat = "";
                                            ATM = "";

                                            if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                                            {
                                                TienMat = "=AM" + row;
                                            }
                                            else
                                            {
                                                ATM = "=AM" + row;
                                            }

                                            Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            rowDataFDate.NumberFormat = "dd/MM/yyyy";

                                            dynamic[] arr = { row2["MA"].ToString(), stt, row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["GIOI_TINH"].ToString(), row2["TEN_TO"].ToString(),
                                                row2["TEN_CV"].ToString(), row2["NGAY_VL"].ToString(), row2["LUONG_CB"].ToString(), row2["NGAY_CONG"].ToString(), row2["LUONG_KHOAN"].ToString(),
                                                "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*J" + row, row2["LSP"].ToString(),
                                                row2["PHEP"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*N" + row,
                                                row2["LE_TET"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*P" + row,
                                                row2["VRCL"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*R" + row,
                                                row2["GIO_CN"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*T" + row,
                                                row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(), row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(), row2["TIEN_CON_NHO"].ToString(),
                                                row2["TIEN_NGUYET_SAN"].ToString(), row2["TIEN_CONG_KHAC"].ToString(),
                                                "=ROUND(L" + row + " + M" + row + " + O" + row + " + Q" + row + " + S" + row + " + U" + row + " + SUM(W"+ row +":AB"+ row + "),0)",
                                                row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                                "=ROUND(SUM(AD"+ row +":AH"+ row +"),0)","=AC"+ row +"-AI"+ row,row2["PHEP_TT"].ToString(),"=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*AK" + row,
                                                "=AJ" + row + "+AL" + row, TienMat, ATM };

                                            Range rowData = ws.get_Range("A" + row, "AO" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 9; colSUM < 42; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("K9", "M" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("N9", "N" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("O9", "O" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("P9", "P" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("U9", "AO" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        ws.get_Range("A9", "B" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("A9", "B" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AP" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AP" + row));
                                    }
                                    catch
                                    { }

                                }
                                break;
                            case 4:
                                {
                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangTT", conn);

                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {

                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 6;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 14;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AQ3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + sThang;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AQ4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + sThang;

                                        Range row6_TieuDe_Format = ws.get_Range("A6", "AQ8");
                                        row6_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row6_TieuDe_Format.Font.Name = fontName;
                                        row6_TieuDe_Format.Font.Bold = true;
                                        row6_TieuDe_Format.WrapText = true;
                                        row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                                            ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 2, col] = col;
                                        }

                                        ws.get_Range("A6", "AQ7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AQ8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A6", "AQ8"));
                                        row = 8;

                                        string TienMat = "";
                                        string ATM = "";

                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            TienMat = "";
                                            ATM = "";

                                            if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                                            {
                                                TienMat = "=AN" + row;
                                            }
                                            else
                                            {
                                                ATM = "=AN" + row;
                                            }

                                            Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            rowDataFDate.NumberFormat = "dd/MM/yyyy";

                                            dynamic[] arr = { row2["MA"].ToString(), stt, row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["GIOI_TINH"].ToString(), row2["TEN_TO"].ToString(),
                                                row2["TEN_CV"].ToString(), row2["NGAY_VL"].ToString(), row2["LUONG_CB"].ToString(), row2["NGAY_CONG"].ToString(), row2["LUONG_KHOAN"].ToString(),
                                                "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*J" + row, row2["PHEP"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*M" + row,
                                                row2["LE_TET"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*O" + row, row2["VRCL"].ToString(), "=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*Q" + row,
                                                row2["TC_NT"].ToString(),"=(K" + row + "/208)*S" + row + "*1.5", row2["TC_CN"].ToString(),"=(K" + row + "/208)*U" + row + "*2",
                                                row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(), row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(), row2["TIEN_CON_NHO"].ToString(),
                                                row2["TIEN_NGUYET_SAN"].ToString(), row2["TIEN_CONG_KHAC"].ToString(),"=ROUND(L" + row + " + N" + row + " + P" + row + " + R" + row + " + T" + row + " + V" + row + " + SUM(X"+ row +":AC"+ row + "),0)",
                                                row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                                "=ROUND(SUM(AE"+ row +":AI"+ row +"),0)","=AD"+ row +"-AJ"+ row,row2["PHEP_TT"].ToString(),"=K" + row + "/" + row2["NC_CHUAN"].ToString() + "*AL" + row,
                                                "=AK" + row + "+AM" + row, TienMat, ATM };

                                            Range rowData = ws.get_Range("A" + row, "AP" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 9; colSUM < 43; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("K9", "L" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("M9", "M" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("N9", "N" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                        ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("W9", "AP" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        ws.get_Range("A9", "B" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("A9", "B" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AQ" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AQ" + row));
                                    }
                                    catch
                                    { }
                                }
                                break;
                            case 5:
                                {
                                    string sThang = cboThang.EditValue.ToString();
                                    DateTime dNgayIn = Convert.ToDateTime(lk_NgayIn.EditValue.ToString());

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangChuyenATM", conn);

                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    //dt2 = new DataTable();
                                    //dt2 = ds.Tables[2].Copy();

                                    try
                                    {

                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 1;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 12;
                                        int fontSizeNoiDung = 12;

                                        foreach (DataRow rowdt in dt.Rows)
                                        {
                                            Range row_DonVi = ws.get_Range("A1", "C2");
                                            row_DonVi.Merge();
                                            row_DonVi.Font.Size = fontSizeTieuDe;
                                            row_DonVi.Font.Name = fontName;
                                            row_DonVi.Font.Bold = true;
                                            row_DonVi.WrapText = true;
                                            row_DonVi.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_DonVi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_DonVi.Value2 = "ĐƠN VỊ : " + rowdt["TEN_DV"].ToString();

                                            Range row_ND1 = ws.get_Range("D1", "H1");
                                            row_ND1.Merge();
                                            row_ND1.Font.Size = fontSizeTieuDe;
                                            row_ND1.Font.Name = fontName;
                                            row_ND1.Font.Bold = true;
                                            row_ND1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND1.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND1.Value2 = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";

                                            Range row_ND2 = ws.get_Range("D2", "H2");
                                            row_ND2.Merge();
                                            row_ND2.Font.Size = fontSizeTieuDe;
                                            row_ND2.Font.Name = fontName;
                                            row_ND2.Font.Bold = true;
                                            row_ND2.Font.Underline = true;
                                            row_ND2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND2.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND2.Value2 = "Độc Lập - Tự Do - Hạnh Phúc";

                                            Range row_ND3 = ws.get_Range("D4", "H4");
                                            row_ND3.Merge();
                                            row_ND3.Font.Size = fontSizeTieuDe;
                                            row_ND3.Font.Name = fontName;
                                            row_ND3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND3.Value2 = rowdt["TINH_THANH"].ToString() + ", Ngày " + dNgayIn.Day + " Tháng " + dNgayIn.Month + " Năm " + dNgayIn.Year;

                                            Range row_ND4 = ws.get_Range("A6", "H6");
                                            row_ND4.Merge();
                                            row_ND4.Font.Size = fontSizeTieuDe;
                                            row_ND4.Font.Name = fontName;
                                            row_ND4.Font.Bold = true;
                                            row_ND4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND4.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND4.Value2 = "Kính gửi: NGÂN HÀNG TMCP CÔNG THƯƠNG CN TIỀN GIANG.";

                                            Range row_ND5 = ws.get_Range("A8", "H8");
                                            row_ND5.Merge();
                                            row_ND5.Font.Size = fontSizeTieuDe;
                                            row_ND5.Font.Name = fontName;
                                            row_ND5.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND5.Value2 = "Trích yếu: V / v lập danh sách chi trả lương";

                                            Range row_ND6 = ws.get_Range("A9", "H9");
                                            row_ND6.Merge();
                                            row_ND6.Font.Size = fontSizeTieuDe;
                                            row_ND6.Font.Name = fontName;
                                            row_ND6.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND6.Value2 = "Tháng " + sThang + " qua tài khoản thẻ ATM";

                                            Range row_ND7 = ws.get_Range("A11", "H11");
                                            row_ND7.Merge();
                                            row_ND7.Font.Size = fontSizeTieuDe;
                                            row_ND7.Font.Name = fontName;
                                            row_ND7.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND7.Value2 = "Căn cứ vào mục d, điểm 2.1 điều 2 theo Hợp đồng sử dụng dịch vụ chuyển lương qua tài khoản ATM";

                                            Range row_ND8 = ws.get_Range("A12", "H12");
                                            row_ND8.Merge();
                                            row_ND8.Font.Size = fontSizeTieuDe;
                                            row_ND8.Font.Name = fontName;
                                            row_ND8.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND8.Value2 = "số: 02/HĐ-KHCN-2011 ngày 06 tháng 05 năm 2011 giữa Ngân hàng Công thương Tiền Giang và" + rowdt["TEN_DV"].ToString();

                                            Range row_ND9 = ws.get_Range("A13", "H13");
                                            row_ND9.Merge();
                                            row_ND9.Font.Size = fontSizeTieuDe;
                                            row_ND9.Font.Name = fontName;
                                            row_ND9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND9.Value2 = "Sau đây là danh sách trả lương tháng " + sThang + " của cán bộ công nhân viên như sau :";

                                            Range row_Format = ws.get_Range("A15", "H15");
                                            row_Format.Font.Size = fontSizeTieuDe;
                                            row_Format.Font.Name = fontName;
                                            row_Format.Font.Bold = true;
                                            row_Format.WrapText = true;
                                            row_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                            Range row_ND10 = ws.get_Range("A15", "A15");
                                            row_ND10.ColumnWidth = 6;
                                            row_ND10.Value2 = "STT";
                                            Range row_ND11 = ws.get_Range("B15", "B15");
                                            row_ND11.ColumnWidth = 12;
                                            row_ND11.Value2 = "Mã số";
                                            Range row_ND12 = ws.get_Range("C15", "C15");
                                            row_ND12.ColumnWidth = 30;
                                            row_ND12.Value2 = "Họ và tên";
                                            Range row_ND13 = ws.get_Range("D15", "D15");
                                            row_ND13.ColumnWidth = 20;
                                            row_ND13.Value2 = "Số tài khoản ATM";
                                            Range row_ND14 = ws.get_Range("E15", "E15");
                                            row_ND14.ColumnWidth = 15;
                                            row_ND14.Value2 = "Số tiền lương đươc hưởng";
                                            Range row_ND15 = ws.get_Range("F15", "F15");
                                            row_ND15.ColumnWidth = 18;
                                            row_ND15.Value2 = "Số tiền chế độ BH được hưởng";
                                            Range row_ND16 = ws.get_Range("G15", "G15");
                                            row_ND16.ColumnWidth = 15;
                                            row_ND16.Value2 = "Tổng số tiền được hưởng";
                                            Range row_ND17 = ws.get_Range("H15", "H15");
                                            row_ND17.ColumnWidth = 20;
                                            row_ND17.Value2 = "Ghi chú";

                                            row = 15;
                                            foreach (DataRow row1 in dt1.Rows)
                                            {
                                                stt++;
                                                row++;

                                                Range rowDataFText = ws.get_Range("D" + row, "D" + row);
                                                rowDataFText.NumberFormat = "@";
                                                Range rowDataFNum = ws.get_Range("E" + row, "G" + row);
                                                rowDataFNum.NumberFormat = "#,##0;(#,##0); ; ";

                                                dynamic[] arr = { stt, row1["MS_CN"].ToString(), row1["TEN_KHONG_DAU"].ToString(), row1["MA_THE_ATM"].ToString(), row1["TIEN_LUONG"].ToString(),
                                                0, "=E" + row + "+F" + row, "" };

                                                Range rowData = ws.get_Range("A" + row, "H" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                                rowData.Font.Size = fontSizeNoiDung;
                                                rowData.Font.Name = fontName;
                                                rowData.Value2 = arr;
                                            }

                                            row++;
                                            ws.Cells[row, 3] = "Cộng";
                                            ws.Cells[row, 5] = "=SUM(" + CellAddress(ws, 16, 5) + ":" + CellAddress(ws, row - 1, 5) + ")";
                                            ws.Cells[row, 5].NumberFormat = "#,##0;(#,##0); ; ";
                                            ws.Cells[row, 7] = "=SUM(" + CellAddress(ws, 16, 7) + ":" + CellAddress(ws, row - 1, 7) + ")";
                                            ws.Cells[row, 7].NumberFormat = "#,##0;(#,##0); ; ";

                                            Range rowFormatF = ws.get_Range("A" + row, "H" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowFormatF.Font.Size = fontSizeNoiDung;
                                            rowFormatF.Font.Name = fontName;
                                            rowFormatF.Font.Bold = true;

                                            BorderAround(ws.get_Range("A15", "H" + row));

                                            row = row + 2;
                                            Range row_ND18 = ws.get_Range("A" + row, "H" + row);
                                            row_ND18.Merge();
                                            row_ND18.Font.Size = fontSizeTieuDe;
                                            row_ND18.Font.Name = fontName;
                                            row_ND18.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND18.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND18.Value2 = "Trên đây là danh sách trả lương CBCNV tháng " + sThang + " của " + rowdt["TEN_DV"].ToString();

                                            row++;
                                            Range row_ND19 = ws.get_Range("A" + row, "H" + row);
                                            row_ND19.Merge();
                                            row_ND19.Font.Size = fontSizeTieuDe;
                                            row_ND19.Font.Name = fontName;
                                            row_ND19.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND19.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND19.Value2 = "Số liệu trên đây bảo đảm chính xác theo bảng gốc đã lưu và khớp đúng với số liệu trên đĩa mềm gửi kèm theo.";

                                            row = row + 2;
                                            Range row_ND20 = ws.get_Range("F" + row, "F" + row);
                                            row_ND20.Merge();
                                            row_ND20.Font.Size = fontSizeTieuDe;
                                            row_ND20.Font.Name = fontName;
                                            row_ND20.Font.Bold = true;
                                            row_ND20.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND20.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND20.Value2 = "THỦ TRƯỞNG ĐƠN VỊ";

                                            row = row + 6;
                                            Range row_ND21 = ws.get_Range("F" + row, "F" + row);
                                            row_ND21.Merge();
                                            row_ND21.Font.Size = fontSizeTieuDe;
                                            row_ND21.Font.Name = fontName;
                                            row_ND21.Font.Bold = true;
                                            row_ND21.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_ND21.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND21.Value2 = "BARTSCH JOACHIM";

                                            row = row + 2;
                                            Range row_ND22 = ws.get_Range("B" + row, "B" + row);
                                            row_ND22.Font.Size = fontSizeTieuDe;
                                            row_ND22.Font.Name = fontName;
                                            row_ND22.Font.Bold = true;
                                            row_ND22.Font.Underline = true;
                                            row_ND22.Value2 = "Lưu ý:";

                                            row++;
                                            Range row_ND23 = ws.get_Range("C" + row, "H" + row);
                                            row_ND23.Merge();
                                            row_ND23.Font.Size = fontSizeTieuDe;
                                            row_ND23.Font.Name = fontName;
                                            row_ND23.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND23.Value2 = "_Họ tên: phải viết hoa, không bỏ dấu.";

                                            row++;
                                            Range row_ND24 = ws.get_Range("C" + row, "H" + row);
                                            row_ND24.Merge();
                                            row_ND24.Font.Size = fontSizeTieuDe;
                                            row_ND24.Font.Name = fontName;
                                            row_ND24.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND24.Value2 = "_Số tiền được hưởng: là số tiền thực lãnh sau khi đã trừ BHXH, BHYT ... và tính luôn số lẽ không được làm tròn số.";

                                            row++;
                                            Range row_ND25 = ws.get_Range("C" + row, "H" + row);
                                            row_ND25.Merge();
                                            row_ND25.Font.Size = fontSizeTieuDe;
                                            row_ND25.Font.Name = fontName;
                                            row_ND25.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_ND25.Value2 = "_Danh sách này phải được đơn vị ký tên, đóng dấu. Cty lưu một bảng, một bảng gửi cho Ngân hàng, đồng thời chép vào USB gửi NHCT (nơi kế toán cần giao dịch - Cẩm Tú) để kế toán hạch toán vào TK ATM của CBCNV";
                                        }
                                    }
                                    catch
                                    { }
                                }
                                break;
                            case 6:
                                {

                                    string sMS_CTL = "";
                                    try
                                    {
                                        sMS_CTL = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT MA_SO FROM dbo.CACH_TINH_LUONG WHERE ID_CTL = " + Convert.ToInt32(cboCachTinhLuong.EditValue) + "").ToString();
                                    }
                                    catch
                                    {

                                    }
                                    InPhieuNhanLuongCNSP(sMS_CTL);
                                }
                                break;
                            case 7:
                                {
                                    //try
                                    //{
                                    //    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\lib\\BangLuongTongHop.xlsx");
                                    //}
                                    //catch { }
                                    string sThang = cboThang.EditValue.ToString();

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    DataTable dt2;

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangTH", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    dt1 = new DataTable();
                                    dt1 = ds.Tables[1].Copy();

                                    dt2 = new DataTable();
                                    dt2 = ds.Tables[2].Copy();

                                    try
                                    {
                                        Excel.Application xlApp = new Excel.Application();

                                        if (xlApp == null)
                                        {
                                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show("Không thể tạo được WorkSheet");
                                            return;
                                        }

                                        int stt = 0;
                                        int col = 0;
                                        int row = 7;
                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 20;
                                        int fontSizeNoiDung = 8;

                                        Range row3_TieuDe_BaoCao = ws.get_Range("A3", "AJ3");
                                        row3_TieuDe_BaoCao.Merge();
                                        row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row3_TieuDe_BaoCao.Font.Name = fontName;
                                        row3_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row3_TieuDe_BaoCao.Font.Bold = true;
                                        row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row3_TieuDe_BaoCao.RowHeight = 30;
                                        row3_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_VIET"].ToString() + " " + dt.Rows[1]["TIENG_VIET"] + " " + Convert.ToDateTime(cboThang.EditValue).Month + " " + dt.Rows[2]["TIENG_VIET"] + " " + Convert.ToDateTime(cboThang.EditValue).Year;


                                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "AJ4");
                                        row4_TieuDe_BaoCao.Merge();
                                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                        row4_TieuDe_BaoCao.Font.Name = fontName;
                                        row4_TieuDe_BaoCao.Font.Color = XlRgbColor.rgbRed;
                                        row4_TieuDe_BaoCao.Font.Bold = true;
                                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row4_TieuDe_BaoCao.RowHeight = 30;
                                        row4_TieuDe_BaoCao.Value2 = dt.Rows[0]["TIENG_ANH"].ToString() + " " + Convert.ToDateTime(cboThang.EditValue).ToString("MM-yyyy");

                                        Range row7_TieuDe_Format = ws.get_Range("A7", "AJ8");
                                        row7_TieuDe_Format.Font.Size = fontSizeNoiDung;
                                        row7_TieuDe_Format.Font.Name = fontName;
                                        row7_TieuDe_Format.Font.Bold = true;
                                        row7_TieuDe_Format.WrapText = true;
                                        row7_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row7_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        foreach (DataRow rowTitle in dt1.Rows)
                                        {
                                            col++;
                                            ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                                            ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString() + " (" + rowTitle["TIENG_ANH"].ToString() + ")";
                                            //ws.Cells[row + 1, col] = rowTitle["TIENG_ANH"].ToString();
                                            ws.Cells[row + 1, col] = col;
                                        }

                                        ws.get_Range("A7", "AJ7").Font.Color = XlRgbColor.rgbBlue;
                                        ws.get_Range("A8", "AJ8").Font.Color = XlRgbColor.rgbRed;

                                        BorderAround(ws.get_Range("A7", "AJ8"));
                                        row = 8;


                                        foreach (DataRow row2 in dt2.Rows)
                                        {
                                            stt++;
                                            row++;

                                            //Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                                            //rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                            dynamic[] arr = { stt, row2["TEN_XN"].ToString(), row2["NC"].ToString(), row2["GC"].ToString(), row2["LUONG_SP"].ToString(),
                                            row2["NGAY_PHEP"].ToString(), row2["TIEN_PHEP"].ToString(), row2["NGAY_LE"].ToString(), row2["TIEN_LE"].ToString(), row2["NGHI_VR"].ToString(), row2["TIEN_NGHI_VR"].ToString(), row2["GIO_TC"].ToString(),
                                            row2["HT_NGUYET_SAN"].ToString(), row2["GIO_CD_NU"].ToString(), row2["TIEN_CD_NU"].ToString(), row2["HT_NHA"].ToString(), row2["HT_DIEN_THOAI"].ToString(), row2["HT_XANG"].ToString() , row2["HT_CN"].ToString() ,
                                            row2["HT_NGUYET_SAN"].ToString(), row2["BU_LUONG"].ToString() , row2["THANH_TOAN_KHAC"].ToString(), row2["TONG_THANH_TOAN"].ToString(), row2["TIEN_BAO_HIEM"].ToString(), row2["THUE_TNCN"].ToString()
                                            , row2["CD_PHI"].ToString(), row2["TAM_UNG"].ToString(), row2["KHAU_TRU_KHAC"].ToString(), row2["TONG_KHAU_TRU"].ToString()
                                            , row2["TONG_LUONG_CL"].ToString(), row2["NGAY_PHEP_TON"].ToString(), row2["LUONG_PHEP_TON"].ToString(), row2["THUC_LINH"].ToString()
                                            , row2["TIEN_MAT"].ToString(), row2["ATM"].ToString()};
                                            //,
                                            //row2["TC_NT"].ToString(), "=IF(Q"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*50%*Q"+ row +",0),0)",
                                            //row2["TC_226"].ToString(), "=IF(S"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*S"+ row +",0),0)",
                                            //row2["LAM_DEM"].ToString(), "=IF(U"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*30%*U"+ row +",0),0)",
                                            //row2["TC_CN"].ToString(), "=IF(W"+ row +">0,ROUND(N"+ row +"/(IF(K" + row +">208,208,K"+ row +")+Q"+ row +"+S"+ row +"+W"+ row +")*W"+ row +",0),0)",
                                            //row2["VRCL"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*Y" + row, row2["LE_TET"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*AA" + row,
                                            //row2["GIO_CN"].ToString(), "=I" + row + "/" + row2["NC_CHUAN"].ToString() + "8*AC" + row, row2["DIEM_CC"].ToString(), row2["TIEN_CHUYEN_CAN"].ToString(),
                                            //row2["TIEN_THAM_NIEN"].ToString(), row2["TIEN_DI_LAI"].ToString(), row2["TIEN_CON_NHO"].ToString(), row2["TIEN_NGUYET_SAN"].ToString(),
                                            //"=IF((("+  row2["MUC_BU_LUONG"].ToString() +"/(" + row2["NC_CHUAN"].ToString() + "*8))*(J"+ row +"*8+O"+ row +"*8+AA"+ row +"*8+Q"+ row +"*1.5))>(N"+ row +"+P"+ row +"+R"+ row +"+AB"+ row +"),(" + row2["MUC_BU_LUONG"].ToString() + "/(" + row2["NC_CHUAN"].ToString() + "*8))*(J"+ row +"*8+AA"+ row +"*8+O"+ row +"*8+Q"+ row +"*1.5)-(N"+ row +"+P"+ row +"+R"+ row +"+AB"+ row +"),0)",
                                            //row2["TIEN_CONG_KHAC"].ToString(),"=ROUND(N"+ row +"+P"+ row +"+R"+ row +"+T"+ row +"+V"+ row +"+X"+ row +"+Z"+ row +"+AB"+ row +"+AD"+ row +"+SUM(AF"+ row +":AL"+ row +"),0)",
                                            //row2["TIEN_BHXH"].ToString(),row2["TIEN_THUE"].ToString(),row2["TRICH_NOP_PCD"].ToString(),row2["TAM_UNG"].ToString(),row2["TIEN_TRU_KHAC"].ToString(),
                                            //"=ROUND(SUM(AN"+ row +":AR"+ row +"),0)","=AM"+ row +"-AS"+ row,row2["PHEP_TT"].ToString(),"=I" + row + "/" + row2["NC_CHUAN"].ToString() + "*AU" + row,
                                            //"=AT" + row + "+AV" + row, TienMat, ATM };


                                            Range rowData = ws.get_Range("A" + row, "AI" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }
                                        row++;
                                        for (int colSUM = 3; colSUM < 36; colSUM++)
                                        {
                                            ws.Cells[row, colSUM] = "=SUM(" + CellAddress(ws, 9, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                                        }

                                        //Range colFormat = ws.get_Range("I8", "I" + row);
                                        //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("C9", "C" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("D9", "D" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("E9", "E" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("F9", "F" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("G9", "G" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("H9", "H" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("I9", "I" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("J9", "J" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("K9", "K" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("L9", "L" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("M9", "M" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("N9", "N" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("O9", "O" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("S9", "S" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("U9", "U" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("W9", "W" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("X9", "X" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Y9", "Y" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("Z9", "Z" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AA9", "AA" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AB9", "AB" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AC9", "AC" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AD9", "AD" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AE9", "AE" + row).NumberFormat = "#,###.0;(#,###.0); ; ";
                                        ws.get_Range("AF9", "AF" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AG9", "AG" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AH9", "AH" + row).NumberFormat = "#,##0;(#,##0); ; ";
                                        ws.get_Range("AI9", "AI" + row).NumberFormat = "#,##0;(#,##0); ; ";

                                        //ws.get_Range("E9", "E" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                        //ws.get_Range("E9", "E" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        //ws.get_Range("D9", "D" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                        //ws.get_Range("D9", "D" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                        //ws.get_Range("H9", "H" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                        //ws.get_Range("H9", "H" + row).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                        Range rowLBTC = ws.get_Range("A" + row, "B" + row);
                                        rowLBTC.Merge();
                                        rowLBTC.Value2 = "Tổng cộng (Total)";

                                        Range rowTC = ws.get_Range("A" + row, "AI" + row);
                                        rowTC.Font.Size = fontSizeNoiDung;
                                        rowTC.Font.Name = fontName;
                                        rowTC.Font.Bold = true;
                                        rowTC.Font.Color = XlRgbColor.rgbBlue;

                                        BorderAround(ws.get_Range("A9", "AJ" + row));
                                    }
                                    catch
                                    { }
                                }
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private string RangeAddress(Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
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
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LoadThang()
        {
            try
            {

                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                if (dtthang.Rows.Count > 0)
                {
                    cboThang.EditValue = dtthang.Rows[0][2];
                }
                else
                {
                    cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                }

                //cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 6:
                    cboCachTinhLuong.Enabled = true;
                    break;
                default:
                    cboCachTinhLuong.Enabled = false;
                    break;
            }
        }
        private void InPhieuNhanLuongCNSP(string MaSo)
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongCNSP", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@MA_SO", SqlDbType.NVarChar).Value = MaSo;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "PhieuNhanLuong";
                ds.Tables[1].TableName = "info";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                DialogResult res = saveFileDialog.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (res == DialogResult.OK)
                {
                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplatePhieuNhanLuong.xlsx", ds, new string[] { "{", "}" });
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch
            {

            }
        }
        private void InPhieuNhanLuongCNGT(string MaSo)
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongCNGT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@MA_SO", SqlDbType.NVarChar).Value = MaSo;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "PhieuNhanLuong";
                ds.Tables[1].TableName = "info";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                DialogResult res = saveFileDialog.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (res == DialogResult.OK)
                {
                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplatePhieuLuong_GT.xlsx", ds, new string[] { "{", "}" });
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch
            {

            }
        }
        private void InPhieuNhanLuongCNQC(string MaSo)
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongCNQC", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@MA_SO", SqlDbType.NVarChar).Value = MaSo;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "PhieuNhanLuong";
                ds.Tables[1].TableName = "info";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                DialogResult res = saveFileDialog.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (res == DialogResult.OK)
                {
                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplatePhieuLuongQC.xlsx", ds, new string[] { "{", "}" });
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch
            {

            }
        }
    }
}
