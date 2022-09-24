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
                                    switch (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString())
                                    {
                                        case "DM":
                                            {
                                                BangLuongThang_DM();
                                                break;
                                            }
                                        default:
                                            {
                                                PhieuLuongThang();
                                                break;
                                            }
                                    }

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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")

                                    {
                                        PhieuLuongThang_DM();
                                    }
                                    else
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
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        object misValue = System.Reflection.Missing.Value;

                                        xlApp.Visible = true;
                                        Workbook wb = xlApp.Workbooks.Add(misValue);

                                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                                        if (ws == null)
                                        {
                                            MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void PhieuLuongThang()
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
                    MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                object misValue = System.Reflection.Missing.Value;

                xlApp.Visible = true;
                Workbook wb = xlApp.Workbooks.Add(misValue);

                Worksheet ws = (Worksheet)wb.Worksheets[1];

                if (ws == null)
                {

                    MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void BangLuongThang_DM()
        {
            this.Cursor = Cursors.WaitCursor;
            string sThang = cboThang.EditValue.ToString();

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dt1;
            DataTable dt2;
            int NgayCuoiThang = DateTime.DaysInMonth(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangCN_DM", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@TNGAY", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
            cmd.Parameters.Add("@DNGAY", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(NgayCuoiThang.ToString() + "/" + cboThang.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);

            dt1 = new DataTable();
            dt1 = ds.Tables[0].Copy();

            dt2 = new DataTable();
            dt2 = ds.Tables[1].Copy();

            try
            {
                Excel.Application xlApp = new Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongTheSuDungThuVienEXCEL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                object misValue = System.Reflection.Missing.Value;

                xlApp.Visible = false;
                Workbook wb = xlApp.Workbooks.Add(misValue);

                Worksheet ws = (Worksheet)wb.Worksheets[1];

                if (ws == null)
                {

                    MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoTheTaoWorkSheet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string lastColumn = CharacterIncrement(dt2.Columns.Count);
                int stt = 0;
                int col = 0;
                int row = 8;
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 11;

                Range row1_TenDV = ws.get_Range("A1");
                row1_TenDV.Value = "Công ty Cổ phần May Duy Minh";
                row1_TenDV.Font.Size = 11;
                row1_TenDV.Font.Name = fontName;

                Range row2_TenDV = ws.get_Range("A2");
                row2_TenDV.Value = "Duy Minh Garment JSC";
                row2_TenDV.Font.Size = 11;
                row2_TenDV.Font.Name = fontName;

                Range row3_TieuDe = ws.get_Range("A3");
                row3_TieuDe.Value = "BẢNG LƯƠNG CÔNG NHÂN THÁNG " + sThang + "";
                row3_TieuDe.Font.Size = 11;
                row3_TieuDe.Font.Bold = true;
                row3_TieuDe.Font.Name = fontName;


                Range row6_TieuDe_Format = ws.get_Range("A7", "" + lastColumn + "9");
                row6_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row6_TieuDe_Format.Font.Name = fontName;
                row6_TieuDe_Format.Font.Bold = true;
                row6_TieuDe_Format.WrapText = true;
                row6_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row6_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range rowThongTin = ws.get_Range("A7", "M7");
                rowThongTin.Merge();
                rowThongTin.Value = "THÔNG TIN";

                Range rowTGLamViec = ws.get_Range("N7", "AC7");
                rowTGLamViec.Merge();
                rowTGLamViec.Value = "THỜI GIAN LÀM VIỆC";

                Range rowNHL = ws.get_Range("N7", "AC7");
                rowNHL.Merge();
                rowNHL.Value = "Thời gian nghỉ hưởng lương";

                Range rowLTG = ws.get_Range("AL7", "AQ7");
                rowLTG.Merge();
                rowLTG.Value = "LƯƠNG THỜI GIAN";

                Range rowLTGNHL = ws.get_Range("AR7", "AT7");
                rowLTGNHL.Merge();
                rowLTGNHL.Value = "LƯƠNG TG NGHỈ HƯỞNG LƯƠNG";

                Range rowLTGTG = ws.get_Range("AV7", "AZ7");
                rowLTGTG.Merge();
                rowLTGTG.Value = "LƯƠNG TG THÊM GIỜ";

                Range rowLSP = ws.get_Range("BB7", "BI7");
                rowLSP.Merge();
                rowLSP.Value = "LƯƠNG SP";

                Range rowTGTSP = ws.get_Range("BK7", "BN7");
                rowTGTSP.Merge();
                rowTGTSP.Value = "LƯƠNG THÊM GIỜ THEO SẢN PHẨM";

                Range rowTPC = ws.get_Range("BS7", "CG7");
                rowTPC.Merge();
                rowTPC.Value = "THƯỞNG+ PHỤ CẤP";

                Range rowCKGT = ws.get_Range("CH7", "CO7");
                rowCKGT.Merge();
                rowCKGT.Value = "CÁC KHOẢN GIẢM TRỪ";

                Range rowTL = ws.get_Range("CP7", "CS7");
                rowTL.Merge();
                rowTL.Value = "TỔNG LƯƠNG";

                Range rowKHAC = ws.get_Range("CT7", "CX7");
                rowKHAC.Merge();
                rowKHAC.Value = "KHÁC";


                Range rowBH = ws.get_Range("CY7", "DD7");
                rowBH.Merge();
                rowBH.Value = "BH , CÔNG ĐOÀN CTY ĐÓNG";

                foreach (DataRow rowTitle in dt1.Rows)
                {
                    col++;
                    ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                    ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                    ws.Cells[row + 1, col] = col;
                }



                ws.Application.ActiveWindow.SplitColumn = 7;
                ws.Application.ActiveWindow.SplitRow = 9;
                ws.Application.ActiveWindow.FreezePanes = true;

                ws.get_Range("A8", "" + lastColumn + "8").Font.Color = XlRgbColor.rgbBlue;
                ws.get_Range("A9", "" + lastColumn + "9").Font.Color = XlRgbColor.rgbRed;

                BorderAround(ws.get_Range("A7", "" + lastColumn + "9"));
                row = 9;

                string TienMat = "";
                string ATM = "";

                foreach (DataRow row2 in dt2.Rows)
                {
                    stt++;
                    row++;

                    //TienMat = "";
                    //ATM = "";

                    //if (string.IsNullOrEmpty(row2["MA_THE_ATM"].ToString()))
                    //{
                    //    TienMat = "=AW" + row;
                    //}
                    //else
                    //{
                    //    ATM = "=AW" + row;
                    //}

                    //Range rowDataFDate = ws.get_Range("H" + row, "H" + row);
                    //rowDataFDate.NumberFormat = "dd/MM/yyyy";
                    dynamic[] arr = { stt, row2["HO_TEN"].ToString(), row2["MS_CN"].ToString(), row2["BO_PHAN"].ToString(), row2["PHAN_BO"].ToString(),
                                            row2["TEN_CTL"].ToString(), row2["TEN_LCV"].ToString(), row2["TEN_TT_HT"].ToString(), row2["GIOI_TINH"].ToString(), row2["NGAY_VAO_LAM"].ToString(),
                                            row2["NGAY_HD"].ToString(), row2["NGAY_NGHI_VIEC"].ToString(), row2["THAM_NIEN"].ToString(),row2["NGAY_CONG"].ToString(),row2["GIO_CONG"].ToString(),
                                            row2["NGAY_CONG_CT"].ToString(), row2["NGAY_CONG_TV"].ToString(), row2["GIO_CD"].ToString(), row2["GIO_NGHI_NGAN"].ToString(), row2["GIO_CHU_KY"].ToString(),
                                            row2["GIO_KTSP"].ToString(),row2["TC_TV_150"].ToString(),row2["TC_CT_150"].ToString(),row2["GIO_KTSP_150"].ToString(),row2["TC_TV_200"].ToString(),
                                            row2["TC_CT_200"].ToString(),row2["GIO_KTSP_200"].ToString(),row2["TONG_GIO_LV"].ToString(),row2["TONG_GIO_LAM_SP"].ToString(),row2["PHEP_NAM_CT"].ToString(),
                                            row2["PHEP_NAM_TV"].ToString(),row2["NGHI_HL_CT"].ToString(),row2["NGHI_HL_TV"].ToString(),row2["TONG_NGHI_HL"].ToString(),row2["NGHI_KL"].ToString(),
                                            row2["GC_TL_SP"].ToString(),row2["SN_TC_2H"].ToString(),row2["LUONG_TV_NC"].ToString(),row2["LUONG_HDLD_NC"].ToString(),row2["LUONG_CD"].ToString(),
                                            row2["LUONG_NGHI_NGAN"].ToString(),row2["LUONG_CHU_KY"].ToString(),row2["LUONG_KTSP"].ToString(),row2["LUONG_NGHI_HL_CT"].ToString(),row2["LUONG_NGHI_HL_TV"].ToString(),
                                            row2["LUONG_PHEP_NAM"].ToString(),row2["TONG_LUONG_TG_HC"].ToString(),row2["LUONG_TV_150"].ToString(),row2["LUONG_CT_150"].ToString(),row2["LUONG_TV_200"].ToString(),
                                            row2["LUONG_CT_200"].ToString(),row2["TONG_LUONG_TC_TG"].ToString(),row2["TONG_LTG_HC_TC"].ToString(),row2["LUONG_SP"].ToString(),row2["PT_HT_LSP"].ToString(),
                                            row2["LSP_HO_TRO"].ToString(),row2["LSP_BQ_1G_HT"].ToString(),row2["LSP_BQ_1G_KHT"].ToString(),row2["LSP_LAM_HC"].ToString(),row2["LUONG_BP_PHU_CHUYEN"].ToString(),
                                            row2["LSP_LAM_HC_TG"].ToString(),row2["BU_LUONG"].ToString(),row2["LSP_TC_TV_150"].ToString(),row2["LSP_TC_CT_150"].ToString(),row2["LSP_TC_TV_200"].ToString(),
                                            row2["LSP_TC_CT_200"].ToString(),row2["LSP_TC_TONG"].ToString(),row2["SS_TC_TG_SP"].ToString(),row2["LUONG_TC_THANG"].ToString(),row2["TONG_BU_LUONG"].ToString(),
                                            row2["THUONG_CC"].ToString(),row2["THUONG_CN_MOI"].ToString(),row2["XEP_LOAI_HQ_SX"].ToString(),row2["THUONG_HQ_SX"].ToString(),row2["THUONG_HQ_QA"].ToString(),
                                            row2["THUONG_PHU_CHUYEN"].ToString(),row2["HO_TRO_AN"].ToString(),row2["HO_TRO_HO_SO"].ToString(),row2["HO_TRO_XANG_XE"].ToString(),row2["GIOI_THIEU_CN_MOI"].ToString(),
                                            row2["ATVSV"].ToString(),row2["PC_CON_NHO"].ToString(),row2["PC_QUA_DO"].ToString(),row2["PC_KHAC"].ToString(),row2["TONG_PHU_CAP"].ToString(),
                                            row2["TIEN_BHXH"].ToString(),row2["TIEN_BHYT"].ToString(),row2["TIEN_BHTN"].ToString(),row2["TONG_TIEN_BHXH"].ToString(),row2["PHI_CONG_DOAN"].ToString(),
                                            row2["THU_BHYT"].ToString(),row2["TRU_KHAC"].ToString(),row2["TONG_GIAM_TRU"].ToString(),row2["TL_TRUOC_GIAM_TRU"].ToString(),row2["TL_THUC_NHAN"].ToString(),
                                            row2["TL_TRUOC_HO_TRO"].ToString(),row2["TL_THUC_NHAN_CUOI"].ToString(),row2["THUC_NHAN_THANG_TRUOC"].ToString(),row2["CHENH_LECH"].ToString(),row2["THUE_TNCN"].ToString(),
                                            row2["TK_NGAN_HANG"].ToString(),row2["CHI_NHANH"].ToString(),row2["BHXH_CTY_TRA"].ToString(),row2["BHYT_CTY_TRA"].ToString(),row2["BHTN_CTY_TRA"].ToString(),
                                            row2["BHTNLD_CTY_TRA"].ToString(),row2["TONG_BH_CTY_TRA"].ToString(),row2["QUY_CONG_DOAN"].ToString(),row2["TL_CTY_TRA"].ToString(),row2["BHTN_CTY_TRA"].ToString()};

                    Range rowData = ws.get_Range("A" + row, lastColumn + row);//Lấy dòng thứ row ra để đổ dữ liệu
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }
                row++;
                Range FormatTong = ws.get_Range("A" + row + "", lastColumn + row);
                FormatTong.Interior.Color = Color.FromArgb(146, 208, 80);
                for (int colSUM = 14; colSUM < dt2.Columns.Count + 2; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    ws.Cells[row, colSUM] = "=SUBTOTAL(9," + CellAddress(ws, 10, colSUM) + ":" + CellAddress(ws, row - 1, colSUM) + ")";
                }

                for (int colFormat = 32; colFormat < dt2.Columns.Count + 2; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0;(#,##0); ; ";
                }
                //Range colFormat = ws.get_Range("I8", "I" + row);
                //colFormat.NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("K9", "N" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("W9", "W" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("X9", "X" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("Y9", "Y" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("Z9", "Z" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("AA9", "AA" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("AB9", "AB" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("AC9", "AC" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("AD9", "AD" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("AE9", "AT" + row).NumberFormat = "#,##0;(#,##0); ; ";
                //ws.get_Range("AU9", "AU" + row).NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //ws.get_Range("AV9", "AY" + row).NumberFormat = "#,##0;(#,##0); ; ";

                ws.get_Range("J10", "J" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("K10", "K" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("L10", "L" + row).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                //rowLBTC.Merge();
                //rowLBTC.Value2 = "Tổng cộng (Total)";


                Excel.Range myRange = ws.get_Range("A9", lastColumn + (row - 1).ToString());
                //Excel.Range myRange = ws.get_Range("A9", lastColumn + "10");
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

                Range rowTC = ws.get_Range("A" + row, lastColumn + row);
                rowTC.Font.Size = fontSizeNoiDung;
                rowTC.Font.Name = fontName;
                rowTC.Font.Bold = true;

                BorderAround(ws.get_Range("A9", lastColumn + row));

                xlApp.Visible = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        private void PhieuLuongThang_DM()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptPhieuLuongThang_DM(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongThangDM_TEST", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }
    }
}
