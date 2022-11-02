﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Drawing;
using DataTable = System.Data.DataTable;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class ucBCLuongSanPham : DevExpress.XtraEditors.XtraUserControl
    {
        private string sKyHieuDV = "";
        public ucBCLuongSanPham()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
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

        private void ucBCLuongSanPham_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            sKyHieuDV = Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString();
            if (sKyHieuDV != "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(3);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(0);
            }
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            LoadCboXN();
            LoadCboTo();
            LoadChuyen();
            LoadGrvCongNhan();
            grdCN.Visible = false;
            searchControl1.Visible = false;
            lk_TuNgay.EditValue = Convert.ToDateTime("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
            lk_DenNgay.EditValue = Convert.ToDateTime("01/" + Convert.ToDateTime(lk_TuNgay.EditValue).AddMonths(+1).Month + "/" + Convert.ToDateTime(lk_TuNgay.EditValue).AddMonths(+1).Year).AddDays(-1);

            //lk_TuNgay.EditValue = Convert.ToDateTime("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year).ToString("dd/MM/yyyy");
            //DateTime dtTN = DateTime.Today;
            //DateTime dtDN = DateTime.Today;
            //lk_DenNgay.EditValue = dtTN.AddDays((-1));
            //dtDN = dtDN.AddMonths(1);
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";
            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
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
                        DataTable dt;
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_bangluongsanphamtonghop":
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangLSPTongHopTheoCN(lk_TuNgay.DateTime, lk_DenNgay.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopTheoCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case "rdo_bangluongsnaphamtonghoptheoMH":
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    //string sTieuDe = "DANH SÁCH NHÂN VIÊN ĐI TRỄ VỀ SỚM THEO GIAI ĐOẠN";

                                    frm.rpt = new rptBangLSPTheoMaHang(lk_TuNgay.DateTime, lk_DenNgay.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLSPTheoMaHang", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = -1;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case "rdo_luongspcanhan":
                                {
                                    DataTable dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grdCN);
                                    dt1.Columns["CHON"].ReadOnly = false;
                                    if (chkInTheoCongNhan.Checked == false)
                                    {
                                        dt1.AsEnumerable().ToList<DataRow>().ForEach(r => r["CHON"] = true);
                                    }

                                    if (dt1.AsEnumerable().Count(x => Convert.ToBoolean(x["CHON"]) == true) == 0)
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    string sBT = "rptGetLSP" + Commons.Modules.iIDUser;
                                    try
                                    {
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdCN), "");
                                        System.Data.SqlClient.SqlConnection conn;
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        DataTable dtTTChung;
                                        DataTable dtChuyen;
                                        DataTable dtBCLSP;

                                        dtTTChung = new DataTable();
                                        dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                        cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dtChuyen = new DataTable();
                                        dtChuyen = ds.Tables[0].Copy();
                                        if (dtChuyen.Rows.Count == 0)
                                        {
                                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        Microsoft.Office.Interop.Excel.Application oApp;
                                        Microsoft.Office.Interop.Excel.Workbook oBook;
                                        Microsoft.Office.Interop.Excel.Worksheet oSheet;
                                        oApp = new Microsoft.Office.Interop.Excel.Application();
                                        oApp.Visible = false;
                                        this.Cursor = Cursors.WaitCursor;
                                        oBook = oApp.Workbooks.Add();
                                        oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets.get_Item(1);

                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 16;
                                        int fontSizeNoiDung = 11;
                                        int oRow = 1;

                                        foreach (DataRow rowC in dtChuyen.Rows)
                                        {
                                            if (oRow == 1)
                                            {
                                                Microsoft.Office.Interop.Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                                                row1_ThongTinCty.Merge();
                                                row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row1_ThongTinCty.Font.Name = fontName;
                                                row1_ThongTinCty.Font.Bold = true;
                                                row1_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row1_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                                                Microsoft.Office.Interop.Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                                                row2_ThongTinCty.Merge();
                                                row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row2_ThongTinCty.Font.Name = fontName;
                                                row2_ThongTinCty.Font.Bold = true;
                                                row2_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row2_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                                                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "M4");
                                                row4_TieuDe_BaoCao.Merge();
                                                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                                row4_TieuDe_BaoCao.Font.Name = fontName;
                                                row4_TieuDe_BaoCao.Font.Bold = true;
                                                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row4_TieuDe_BaoCao.RowHeight = 30;
                                                row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                                                //Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "M5");
                                                //row5_TieuDe_BaoCao.Merge();
                                                //row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                                //row5_TieuDe_BaoCao.Font.Name = fontName;
                                                //row5_TieuDe_BaoCao.Font.Bold = true;
                                                //row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                //row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                //row5_TieuDe_BaoCao.RowHeight = 20;
                                                //row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy");

                                                oRow = 6;
                                            }

                                            Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("G" + oRow.ToString(), "G" + oRow.ToString());
                                            row_Chuyen.Merge();
                                            row_Chuyen.Value2 = rowC[1].ToString();
                                            row_Chuyen.Font.Size = fontSizeNoiDung;
                                            row_Chuyen.Font.Name = fontName;
                                            row_Chuyen.Font.Bold = true;
                                            row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                            row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_Chuyen.RowHeight = 30;

                                            oRow++;

                                            System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN_DM", conn);
                                            cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                                            cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                            cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                            cmdCT.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                            cmdCT.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                                            DataSet dsCT = new DataSet();
                                            adpCT.Fill(dsCT);
                                            dtBCLSP = new DataTable();
                                            dtBCLSP = dsCT.Tables[0].Copy();
                                            int totalColumn = dtBCLSP.Columns.Count;
                                            string lastColumn = string.Empty;
                                            lastColumn = CharacterIncrement(totalColumn - 1);

                                            oSheet.Cells[oRow, 1] = "Stt";
                                            oSheet.Cells[oRow, 1].ColumnWidth = 6;
                                            oSheet.Cells[oRow, 2] = "Mã NV";
                                            oSheet.Cells[oRow, 2].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 3] = "Họ tên";
                                            oSheet.Cells[oRow, 3].ColumnWidth = 25;
                                            oSheet.Cells[oRow, 4] = "Bộ phận";
                                            oSheet.Cells[oRow, 4].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 5] = "Mã hàng";
                                            oSheet.Cells[oRow, 5].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 6] = "Mã công đoạn";
                                            oSheet.Cells[oRow, 6].ColumnWidth = 8;
                                            oSheet.Cells[oRow, 7] = "Tên công đoạn";
                                            oSheet.Cells[oRow, 7].ColumnWidth = 35;
                                            oSheet.Cells[oRow, 8] = "Tổng sản lượng cá nhân đã kê";
                                            oSheet.Cells[oRow, 8].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 9] = "Tổng sản lượng công đoạn";
                                            oSheet.Cells[oRow, 9].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 10] = "Sản lượng chốt tính lương";
                                            oSheet.Cells[oRow, 10].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 11] = "Thừa(-)/ Thiếu(+)";
                                            oSheet.Cells[oRow, 11].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 12] = "Đơn giá";
                                            oSheet.Cells[oRow, 12].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 13] = "Thành tiền";
                                            oSheet.Cells[oRow, 13].ColumnWidth = 15;

                                            string LastTitleColumn = string.Empty;
                                            LastTitleColumn = "M";
                                            Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                            row_TieuDe_BaoCao.Font.Name = fontName;
                                            row_TieuDe_BaoCao.Font.Bold = true;
                                            row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_TieuDe_BaoCao.Cells.WrapText = true;
                                            BorderAround(row_TieuDe_BaoCao);

                                            oRow++;
                                            DataRow[] dr = dtBCLSP.Select();
                                            string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                                            int rowCnt = 0;
                                            int rowBD = oRow;
                                            foreach (DataRow row in dtBCLSP.Rows)
                                            {
                                                for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                                                {
                                                    rowData[rowCnt, col] = row[col].ToString();
                                                }
                                                rowCnt++;
                                            }
                                            oRow = rowBD + rowCnt - 1;
                                            oSheet.get_Range("A" + rowBD, lastColumn + oRow.ToString()).Value2 = rowData;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Size = fontSizeNoiDung;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Name = fontName;
                                            oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                                            Microsoft.Office.Interop.Excel.Range formatRange;
                                            formatRange = oSheet.get_Range("H" + rowBD, "H" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("I" + rowBD, "I" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0.000;(#,##0.000); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            //formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                                            //formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            //formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                                            //formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").Value2 = "=SUM(M" + rowBD + ":M" + oRow.ToString() + ")";
                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0); ; ";
                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").Font.Bold = true;
                                            oSheet.get_Range("M" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                            oRow = oRow + 2;
                                        }
                                        oApp.Visible = true;
                                        this.Cursor = Cursors.Default;
                                        Commons.Modules.ObjSystems.XoaTable(sBT);

                                        oApp.UserControl = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Cursor = Cursors.Default;
                                        Commons.Modules.ObjSystems.XoaTable(sBT);
                                        XtraMessageBox.Show(ex.Message);
                                    }
                                }
                                break;
                            case "rdo_luongspchitiet":
                                {
                                    try
                                    {
                                        DataTable dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grdCN);
                                        dt1.Columns["CHON"].ReadOnly = false;
                                        if (chkInTheoCongNhan.Checked == false)
                                        {
                                            dt1.AsEnumerable().ToList<DataRow>().ForEach(r => r["CHON"] = true);
                                        }

                                        if (dt1.AsEnumerable().Count(x => Convert.ToBoolean(x["CHON"]) == true) == 0)
                                        {
                                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        string sBT = "rptGetLSP" + Commons.Modules.iIDUser;
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdCN), "");
                                        System.Data.SqlClient.SqlConnection conn;
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        DataTable dtTTChung;
                                        DataTable dtChuyen;
                                        DataTable dtBCLSP;

                                        dtTTChung = new DataTable();
                                        dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                        cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dtChuyen = new DataTable();
                                        dtChuyen = ds.Tables[0].Copy();

                                        Microsoft.Office.Interop.Excel.Application oApp;
                                        Microsoft.Office.Interop.Excel.Workbook oBook;
                                        Microsoft.Office.Interop.Excel.Worksheet oSheet;

                                        oApp = new Microsoft.Office.Interop.Excel.Application();
                                        oApp.Visible = true;

                                        oBook = oApp.Workbooks.Add();
                                        oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets.get_Item(1);

                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 16;
                                        int fontSizeNoiDung = 11;
                                        int oRow = 1;

                                        foreach (DataRow rowC in dtChuyen.Rows)
                                        {
                                            if (oRow == 1)
                                            {
                                                Microsoft.Office.Interop.Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                                                row1_ThongTinCty.Merge();
                                                row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row1_ThongTinCty.Font.Name = fontName;
                                                row1_ThongTinCty.Font.Bold = true;
                                                row1_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row1_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                                                Microsoft.Office.Interop.Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                                                row2_ThongTinCty.Merge();
                                                row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row2_ThongTinCty.Font.Name = fontName;
                                                row2_ThongTinCty.Font.Bold = true;
                                                row2_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row2_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                                                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "M4");
                                                row4_TieuDe_BaoCao.Merge();
                                                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                                row4_TieuDe_BaoCao.Font.Name = fontName;
                                                row4_TieuDe_BaoCao.Font.Bold = true;
                                                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row4_TieuDe_BaoCao.RowHeight = 30;
                                                row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG CHUYỀN MAY";

                                                Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "M5");
                                                row5_TieuDe_BaoCao.Merge();
                                                row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                                row5_TieuDe_BaoCao.Font.Name = fontName;
                                                row5_TieuDe_BaoCao.Font.Bold = true;
                                                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row5_TieuDe_BaoCao.RowHeight = 20;
                                                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy");

                                                oRow = 7;
                                            }

                                            Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("H" + oRow.ToString(), "H" + oRow.ToString());
                                            row_Chuyen.Merge();
                                            row_Chuyen.Value2 = rowC[1].ToString();
                                            row_Chuyen.Font.Size = fontSizeNoiDung;
                                            row_Chuyen.Font.Name = fontName;
                                            row_Chuyen.Font.Bold = true;
                                            row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                            row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_Chuyen.RowHeight = 30;

                                            oRow++;

                                            System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHNgayTheoCN_DM", conn);
                                            cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                                            cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                            cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                            cmdCT.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;

                                            cmdCT.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                                            DataSet dsCT = new DataSet();
                                            adpCT.Fill(dsCT);
                                            dtBCLSP = new DataTable();
                                            dtBCLSP = dsCT.Tables[0].Copy();
                                            int totalColumn = dtBCLSP.Columns.Count;
                                            string lastColumn = string.Empty;
                                            lastColumn = CharacterIncrement(totalColumn - 1);

                                            //DataRow[] dr = dtBCLSP.Select();
                                            //string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                                            //int oCol = 1;
                                            //foreach (DataColumn col in dtBCLSP.Columns)
                                            //{
                                            //    oSheet.Cells[oRow, oCol] = col.Caption;
                                            //    oSheet.Cells[oRow, oCol].ColumnWidth = 12;
                                            //    //oSheet.Cells[oRow, oCol].Wraptext = true;
                                            //    oCol = oCol + 1;
                                            //}

                                            oSheet.Cells[oRow, 1] = "Stt";
                                            oSheet.Cells[oRow, 1].ColumnWidth = 6;
                                            oSheet.Cells[oRow, 2] = "Ngày";
                                            oSheet.Cells[oRow, 2].ColumnWidth = 12;
                                            oSheet.Cells[oRow, 3] = "Họ tên";
                                            oSheet.Cells[oRow, 3].ColumnWidth = 25;
                                            oSheet.Cells[oRow, 4] = "Mã NV";
                                            oSheet.Cells[oRow, 4].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 5] = "Bộ phận";
                                            oSheet.Cells[oRow, 5].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 6] = "Mã đơn hàng";
                                            oSheet.Cells[oRow, 6].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 7] = "Mã công đoạn";
                                            oSheet.Cells[oRow, 7].ColumnWidth = 8;
                                            oSheet.Cells[oRow, 8] = "Tên công đoạn";
                                            oSheet.Cells[oRow, 8].ColumnWidth = 35;
                                            oSheet.Cells[oRow, 9] = "Sản lượng ghi nhận";
                                            oSheet.Cells[oRow, 9].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 10] = "Đơn giá";
                                            oSheet.Cells[oRow, 10].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 11] = "Tổng tiền lương";
                                            oSheet.Cells[oRow, 11].ColumnWidth = 15;
                                            oSheet.Cells[oRow, 12] = "Tổng SL theo MNV";
                                            oSheet.Cells[oRow, 12].ColumnWidth = 10;
                                            oSheet.Cells[oRow, 13] = "Tổng SL theo Cđoạn";
                                            oSheet.Cells[oRow, 13].ColumnWidth = 10;


                                            string LastTitleColumn = string.Empty;
                                            LastTitleColumn = "M";
                                            Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                            row_TieuDe_BaoCao.Font.Name = fontName;
                                            row_TieuDe_BaoCao.Font.Bold = true;
                                            row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_TieuDe_BaoCao.Cells.WrapText = true;
                                            BorderAround(row_TieuDe_BaoCao);

                                            oRow++;
                                            DataRow[] dr = dtBCLSP.Select();
                                            string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                                            int rowCnt = 0;
                                            int rowBD = oRow;
                                            foreach (DataRow row in dtBCLSP.Rows)
                                            {
                                                for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                                                {
                                                    rowData[rowCnt, col] = row[col].ToString();
                                                }
                                                rowCnt++;
                                            }
                                            oRow = rowBD + rowCnt - 1;
                                            oSheet.get_Range("A" + rowBD, lastColumn + oRow.ToString()).Value2 = rowData;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Size = fontSizeNoiDung;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Name = fontName;
                                            oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                                            Microsoft.Office.Interop.Excel.Range formatRange;
                                            formatRange = oSheet.get_Range("I" + rowBD, "I" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0.000;(#,##0.000); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                                            //string CurentColumn = string.Empty;
                                            //for (int colMH = 5; colMH <= totalColumn - 1; colMH++)
                                            //{
                                            //    CurentColumn = CharacterIncrement(colMH);
                                            //}

                                            //set formular
                                            oSheet.get_Range("K7", "K7").Value2 = "=SUM(K" + rowBD + ":K" + oRow.ToString() + ")"; ;
                                            oSheet.get_Range("K7", "K7").NumberFormat = "#,##0;(#,##0); ; ";
                                            oSheet.get_Range("K7", "K7").Font.Size = fontSizeNoiDung;
                                            oSheet.get_Range("K7", "K7").Font.Name = fontName;
                                            oSheet.get_Range("K7", "K7").Font.Bold = true;
                                            //row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                            oSheet.get_Range("K7", "K7").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            //CurentColumn = CharacterIncrement(totalColumn);
                                            //Microsoft.Office.Interop.Excel.Range formularRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + oRow.ToString());
                                            //formularRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormulas);
                                            //formularRange.NumberFormat = "#,##0;(#,##0); ; ";

                                            //oRow++;
                                            //Microsoft.Office.Interop.Excel.Range row_TongCong = oSheet.get_Range("A" + oRow.ToString(), "E" + oRow.ToString());
                                            //row_TongCong.Merge();
                                            //row_TongCong.Font.Size = fontSizeNoiDung;
                                            //row_TongCong.Font.Name = fontName;
                                            //row_TongCong.Font.Bold = true;
                                            //row_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            //row_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            //row_TongCong.RowHeight = 30;
                                            //row_TongCong.Value2 = "Tổng cộng";

                                            //for (int colMH = 6; colMH <= totalColumn + 1; colMH++)
                                            //{
                                            //    CurentColumn = CharacterIncrement(colMH - 1);
                                            //    oSheet.Cells[oRow, colMH] = "=SUM(" + CurentColumn + rowBD.ToString() + ":" + CurentColumn + (oRow - 1).ToString() + ")";
                                            //    oSheet.Cells[oRow, colMH].NumberFormat = "#,##0;(#,##0); ; ";
                                            //}

                                            //Microsoft.Office.Interop.Excel.Range row_Format_TongCong = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            //row_Format_TongCong.Font.Size = fontSizeNoiDung;
                                            //row_Format_TongCong.Font.Name = fontName;
                                            //row_Format_TongCong.Font.Bold = true;
                                            //row_Format_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            //BorderAround(row_Format_TongCong);

                                            oRow = oRow + 2;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        XtraMessageBox.Show(ex.Message);
                                    }
                                }
                                break;
                            case "rdo_luongspchitietcanhan":
                                {
                                    DataTable dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grdCN);
                                    dt1.Columns["CHON"].ReadOnly = false;
                                    if (chkInTheoCongNhan.Checked == false)
                                    {
                                        dt1.AsEnumerable().ToList<DataRow>().ForEach(r => r["CHON"] = true);
                                    }

                                    if (dt1.AsEnumerable().Count(x => Convert.ToBoolean(x["CHON"]) == true) == 0)
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    string sBT = "rptGetLSP" + Commons.Modules.iIDUser;
                                    try
                                    {
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdCN), "");
                                        System.Data.SqlClient.SqlConnection conn;
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        DataTable dtTTChung;
                                        DataTable dtChuyen;
                                        DataTable dtBCLSP;

                                        dtTTChung = new DataTable();
                                        dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                        cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dtChuyen = new DataTable();
                                        dtChuyen = ds.Tables[0].Copy();

                                        Microsoft.Office.Interop.Excel.Application oApp;
                                        Microsoft.Office.Interop.Excel.Workbook oBook;
                                        Microsoft.Office.Interop.Excel.Worksheet oSheet;
                                        this.Cursor = Cursors.WaitCursor;
                                        oApp = new Microsoft.Office.Interop.Excel.Application();
                                        oApp.Visible = false;

                                        oBook = oApp.Workbooks.Add();
                                        oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.Worksheets.get_Item(1);

                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 16;
                                        int fontSizeNoiDung = 12;
                                        int oRow = 1;

                                        foreach (DataRow rowC in dtChuyen.Rows)
                                        {
                                            if (oRow == 1)
                                            {
                                                Microsoft.Office.Interop.Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                                                row1_ThongTinCty.Merge();
                                                row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row1_ThongTinCty.Font.Name = fontName;
                                                row1_ThongTinCty.Font.Bold = true;
                                                row1_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row1_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                                                Microsoft.Office.Interop.Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                                                row2_ThongTinCty.Merge();
                                                row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row2_ThongTinCty.Font.Name = fontName;
                                                row2_ThongTinCty.Font.Bold = true;
                                                row2_ThongTinCty.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                row2_ThongTinCty.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                                                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "H4");
                                                row4_TieuDe_BaoCao.Merge();
                                                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                                row4_TieuDe_BaoCao.Font.Name = fontName;
                                                row4_TieuDe_BaoCao.Font.Bold = true;
                                                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row4_TieuDe_BaoCao.RowHeight = 30;
                                                row4_TieuDe_BaoCao.Value2 = "BẢNG LƯƠNG SẢN PHẨM MÃ HÀNG CÔNG NHÂN THEO CHUYỀN";

                                                Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "H5");
                                                row5_TieuDe_BaoCao.Merge();
                                                row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                                row5_TieuDe_BaoCao.Font.Name = fontName;
                                                row5_TieuDe_BaoCao.Font.Bold = true;
                                                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                row5_TieuDe_BaoCao.RowHeight = 20;
                                                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy");

                                                oRow = 7;
                                            }

                                            Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("A" + oRow.ToString(), "H" + oRow.ToString());
                                            row_Chuyen.Merge();
                                            row_Chuyen.Value2 = "Chuyền : " + rowC[1].ToString();
                                            row_Chuyen.Font.Size = fontSizeNoiDung;
                                            row_Chuyen.Font.Name = fontName;
                                            row_Chuyen.Font.Bold = true;
                                            row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                            row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_Chuyen.RowHeight = 30;

                                            oRow++;

                                            System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN_DM", conn);
                                            cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                                            cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                            cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                            cmdCT.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                            cmdCT.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                                            DataSet dsCT = new DataSet();
                                            adpCT.Fill(dsCT);
                                            dtBCLSP = new DataTable();
                                            dtBCLSP = dsCT.Tables[0].Copy();
                                            int totalColumn = dtBCLSP.Columns.Count;
                                            string lastColumn = string.Empty;
                                            lastColumn = CharacterIncrement(totalColumn - 1);

                                            DataRow[] dr = dtBCLSP.Select();
                                            string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

                                            int oCol = 1;
                                            foreach (DataColumn col in dtBCLSP.Columns)
                                            {
                                                oSheet.Cells[oRow, oCol] = col.Caption;
                                                oSheet.Cells[oRow, oCol].ColumnWidth = 12;
                                                //oSheet.Cells[oRow, oCol].Wraptext = true;
                                                oCol = oCol + 1;
                                            }

                                            oSheet.Cells[oRow, 1] = "Stt";
                                            oSheet.Cells[oRow, 1].ColumnWidth = 6;
                                            oSheet.Cells[oRow, 2] = "Mã NV";
                                            oSheet.Cells[oRow, 2].ColumnWidth = 12;
                                            oSheet.Cells[oRow, 3] = "Họ tên";
                                            oSheet.Cells[oRow, 3].ColumnWidth = 35;
                                            oSheet.Cells[oRow, 4] = "Bộ phận";
                                            oSheet.Cells[oRow, 4].ColumnWidth = 20;
                                            oSheet.Cells[oRow, 5] = "Chuyền/Phòng";
                                            oSheet.Cells[oRow, 5].ColumnWidth = 20;
                                            oSheet.Cells[oRow, totalColumn + 1] = "Tổng cộng";
                                            oSheet.Cells[oRow, totalColumn + 1].ColumnWidth = 15;
                                            oSheet.Cells[oRow, totalColumn + 2] = "CN ký xác nhận";

                                            string LastTitleColumn = string.Empty;
                                            LastTitleColumn = CharacterIncrement(totalColumn + 1);
                                            Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                            row_TieuDe_BaoCao.Font.Name = fontName;
                                            row_TieuDe_BaoCao.Font.Bold = true;
                                            row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_TieuDe_BaoCao.Cells.WrapText = true;
                                            BorderAround(row_TieuDe_BaoCao);

                                            oRow++;
                                            int rowCnt = 0;
                                            int rowBD = oRow;
                                            foreach (DataRow row in dtBCLSP.Rows)
                                            {
                                                for (int col = 0; col < dtBCLSP.Columns.Count; col++)
                                                {
                                                    rowData[rowCnt, col] = row[col].ToString();
                                                }
                                                rowCnt++;
                                            }
                                            oRow = rowBD + rowCnt - 1;
                                            oSheet.get_Range("A" + rowBD, lastColumn + oRow.ToString()).Value2 = rowData;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Size = fontSizeNoiDung;
                                            oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()).Font.Name = fontName;
                                            oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                                            Microsoft.Office.Interop.Excel.Range formatRange;
                                            string CurentColumn = string.Empty;
                                            for (int colMH = 5; colMH <= totalColumn - 1; colMH++)
                                            {
                                                CurentColumn = CharacterIncrement(colMH);
                                                formatRange = oSheet.get_Range(CurentColumn + rowBD, CurentColumn + oRow.ToString());
                                                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }

                                            //set formular
                                            oSheet.Cells[rowBD, totalColumn + 1] = "=SUM(F" + rowBD + ":" + lastColumn + rowBD + ")";
                                            oSheet.Cells[rowBD, totalColumn + 1].NumberFormat = "#,##0;(#,##0); ; ";
                                            oSheet.Cells[rowBD, totalColumn + 1].Copy();

                                            CurentColumn = CharacterIncrement(totalColumn);
                                            Microsoft.Office.Interop.Excel.Range formularRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + oRow.ToString());
                                            formularRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormulas);
                                            formularRange.NumberFormat = "#,##0;(#,##0); ; ";

                                            oRow++;
                                            Microsoft.Office.Interop.Excel.Range row_TongCong = oSheet.get_Range("A" + oRow.ToString(), "E" + oRow.ToString());
                                            row_TongCong.Merge();
                                            row_TongCong.Font.Size = fontSizeNoiDung;
                                            row_TongCong.Font.Name = fontName;
                                            row_TongCong.Font.Bold = true;
                                            row_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            row_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            row_TongCong.RowHeight = 30;
                                            row_TongCong.Value2 = "Tổng cộng";

                                            for (int colMH = 6; colMH <= totalColumn + 1; colMH++)
                                            {
                                                CurentColumn = CharacterIncrement(colMH - 1);
                                                oSheet.Cells[oRow, colMH] = "=SUM(" + CurentColumn + rowBD.ToString() + ":" + CurentColumn + (oRow - 1).ToString() + ")";
                                                oSheet.Cells[oRow, colMH].NumberFormat = "#,##0;(#,##0); ; ";
                                            }

                                            Microsoft.Office.Interop.Excel.Range row_Format_TongCong = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_Format_TongCong.Font.Size = fontSizeNoiDung;
                                            row_Format_TongCong.Font.Name = fontName;
                                            row_Format_TongCong.Font.Bold = true;
                                            row_Format_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            BorderAround(row_Format_TongCong);

                                            oRow = oRow + 2;
                                        }
                                        Commons.Modules.ObjSystems.XoaTable(sBT);
                                        this.Cursor = Cursors.Default;
                                        oApp.Visible = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Cursor = Cursors.Default;
                                        Commons.Modules.ObjSystems.XoaTable(sBT);
                                        XtraMessageBox.Show(ex.Message);
                                    }

                                    // frm.ShowDialog();
                                }
                                break;
                            case "rdo_bangtonghopluongmahang":
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangTongHopLuongMaHang(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        int idCN = -1;
                                        if (chkInTheoCongNhan.Checked)
                                        {
                                            idCN = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                                        }

                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangTongHopLuongMaHang", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                        }

                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }

        private void BorderAround(Microsoft.Office.Interop.Excel.Range range)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
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

        private void LoadGrvCongNhan()
        {
            try
            {
                DataTable dtCongNhan = new DataTable();
                dtCongNhan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanBC", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue,
                                                        LK_TO.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdCN.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dtCongNhan, true, false, false, true, true, this.Name);
                    dtCongNhan.Columns["CHON"].ReadOnly = false;
                }
                else
                {
                    grdCN.DataSource = dtCongNhan;
                }
                try
                {
                    grvCN.Columns["CHON"].Visible = false;
                    grvCN.OptionsSelection.CheckBoxSelectorField = "CHON";
                }
                catch { }
            }
            catch
            {

            }

            //format grid view Cong nhan
            grvCN.Columns["ID_CN"].Visible = false;
            //grvCN.OptionsView.ShowColumnHeaders = false;
            grvCN.OptionsView.ShowGroupPanel = false;
            //grvCN.OptionsView.ShowFooter = true;
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXN();
            LoadCboTo();
            LoadChuyen();
            LoadGrvCongNhan();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
            LoadGrvCongNhan();
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                //case "rdo_bangluongsanphamtonghop":
                //    {
                //        grdCN.Visible = false;
                //    }
                //    break;
                //case "rdo_bangluongsnaphamtonghoptheoMH":
                //    {
                //        grdCN.Visible = false;
                //    }
                //    break;
                //case "rdo_luongspcanhan":
                //    {
                //        grdCN.Visible = true;
                //    }
                //    break;
                //case "rdo_luongspchitiet":
                //    {
                //        grdCN.Visible = true;
                //    }
                //    break;
                //case "rdo_luongspchitietcanhan":
                //    {
                //        grdCN.Visible = true;
                //    }
                //    break;
                //case "rdo_bangtonghopluongmahang":
                //    {
                //        grdCN.Visible = false;
                //    }
                //    break;
            }
        }

        private void chkInTheoCongNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInTheoCongNhan.Checked == true)
            {
                grdCN.Visible = true;
                searchControl1.Visible = true;
            }
            else
            {
                grdCN.Visible = false;
                searchControl1.Visible = false;
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                string sSQL = "SELECT T.ID_TO, T.TEN_TO  FROM (SELECT T2.ID_TO, T2.TEN_TO, T2.STT_TO FROM(SELECT ID_TO, TEN_TO, STT_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) AND(ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND(ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1)) T2 UNION SELECT - 1, '< All >', -1) T ORDER BY STT_TO";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
            }
            catch (Exception ex) { }
        }
        private void LoadCboXN()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  T.ID_XN,T.TEN_XN FROM (SELECT  DISTINCT  STT_DV, STT_XN, ID_XN, TEN_XN  AS TEN_XN  FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) UNION SELECT - 1, -1, -1, '< All >') T ORDER BY T.STT_DV, T.STT_XN"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
            }
            catch { }
        }
        private void LoadChuyen()
        {
            try
            {
                string sSql = "SELECT T.ID_TO, T.TEN_TO FROM (SELECT [TO].ID_TO, [TO].TEN_TO, [TO].STT_TO FROM dbo.[TO] INNER JOIN dbo.XI_NGHIEP XN ON XN.ID_XN = [TO].ID_XN WHERE [TO].ID_LOAI_CHUYEN IN (1,2,3,4,5,6,7) AND (XN.ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) UNION SELECT -1, ' < All > ', -1) T ORDER BY T.STT_TO";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_CHUYEN, dt, "ID_TO", "TEN_TO", "TEN_TO");
            }
            catch { }
        }
    }
}
