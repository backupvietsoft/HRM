using DevExpress.XtraBars.Docking2010;
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
using System.Reflection;
using System.Collections.Generic;
using DevExpress.DataAccess.ConnectionParameters;

namespace Vs.Payroll
{
    public partial class ucBCLuongSanPham : DevExpress.XtraEditors.XtraUserControl
    {
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

            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_bangtonghopluongmahang").FirstOrDefault());
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            LoadCboXN();
            LoadCboTo();
            LoadChuyen();
            LoadGrvCongNhan();
            LoadGrvMaHang();
            datThang.DateTime = DateTime.Now;
            datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datThang.Properties.EditFormat.FormatString = "MM/yyyy";
            datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            datThang.Properties.Mask.EditMask = "MM/yyyy";
            datNgayXem.DateTime = DateTime.Now;
            Commons.OSystems.SetDateEditFormat(datNgayXem);

            datTNgay.EditValue = Convert.ToDateTime(datThang.EditValue).AddDays((-datThang.DateTime.Day) + 1);
            datDNgay.EditValue = Convert.ToDateTime(datThang.EditValue).AddDays((-datThang.DateTime.Day)).AddMonths(+1);

            Commons.OSystems.SetDateEditFormat(datTNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);

            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";
            chkInTheoCongNhan.EditValue = false;
            grdCN.Visible = false;
            searchControl1.Visible = false;
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
                            case "rdo_bangluongsanphamtonghop": // 1
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                LuongSPTongHopNgay();
                                                break;
                                            }
                                        default:
                                            {
                                                LuongSPTongHopNgay();
                                                break;
                                            }
                                    }
                                }
                                break;
                            case "rdo_bangluongsnaphamtonghoptheoMH": //2
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                LuongSPTongHopThang();
                                                break;
                                            }
                                        case "BT":
                                            {
                                                LSPThangTheoTungCongDoan_BT();
                                                break;
                                            }
                                        default:
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                dt = new DataTable();

                                                frm.rpt = new rptBangLSPTheoMaHang(datThang.DateTime, datNgayXem.DateTime);

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
                                                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = datThang.DateTime;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DA_TA";
                                                    frm.AddDataSource(dt);
                                                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                                }
                                                catch
                                                { }


                                                frm.ShowDialog();
                                                break;
                                            }
                                    }
                                }
                                break;
                            case "rdo_luongspcanhan": //3
                                {
                                    LSPThangTheoTungCongDoan_DM();
                                    break;
                                }
                            case "rdo_luongspchitiet": // 6
                                {
                                    LuongSPTongHopTN();
                                    break;
                                }
                            case "rdo_luongspchitietcanhan": // 4
                                {
                                    try
                                    {
                                        System.Data.SqlClient.SqlConnection conn;
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        DataTable dtTTChung;
                                        DataTable dtChuyen;
                                        DataTable dtBCLSP;

                                        dtTTChung = new DataTable();
                                        dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung(-1);

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                                            oSheet.Name = rowC[1].ToString();
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
                                                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");

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

                                            System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHCNTheoChuyen", conn);
                                            cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                                            cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                                            cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                                            oSheet.Cells[oRow, 4] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                                            oSheet.Cells[oRow, 4].ColumnWidth = 20;
                                            oSheet.Cells[oRow, 5] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
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
                                                formatRange.NumberFormat = "#,##0;(#,##0);;";
                                                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }

                                            //set formular
                                            oSheet.Cells[rowBD, totalColumn + 1] = "=SUM(F" + rowBD + ":" + lastColumn + rowBD + ")";
                                            oSheet.Cells[rowBD, totalColumn + 1].NumberFormat = "#,##0;(#,##0);;";
                                            oSheet.Cells[rowBD, totalColumn + 1].Copy();

                                            CurentColumn = CharacterIncrement(totalColumn);
                                            Microsoft.Office.Interop.Excel.Range formularRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + oRow.ToString());
                                            formularRange.PasteSpecial(Microsoft.Office.Interop.Excel.XlPasteType.xlPasteFormulas);
                                            formularRange.NumberFormat = "#,##0;(#,##0);;";

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
                                                oSheet.Cells[oRow, colMH].NumberFormat = "#,##0;(#,##0);;";
                                            }

                                            Microsoft.Office.Interop.Excel.Range row_Format_TongCong = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_Format_TongCong.Font.Size = fontSizeNoiDung;
                                            row_Format_TongCong.Font.Name = fontName;
                                            row_Format_TongCong.Font.Bold = true;
                                            row_Format_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                            BorderAround(row_Format_TongCong);

                                            oRow = 1;
                                            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;
                                            oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                                        }
                                        this.Cursor = Cursors.Default;
                                        oBook.Sheets[1].Activate();
                                        oApp.Visible = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Cursor = Cursors.Default;
                                        XtraMessageBox.Show(ex.Message);
                                    }

                                    // frm.ShowDialog();
                                }
                                break;
                            case "rdo_bangtonghopluongmahang": // 7
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangTongHopLuongMaHang(datThang.DateTime, datNgayXem.DateTime, lk_NgayIn.DateTime);

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
                                        cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = datThang.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                    }
                                    catch
                                    { }

                                    frm.ShowDialog();
                                    break;
                                }
                            case "rdo_BaoCaoChiTietHangNgay": // 0
                                {
                                    LuongSPChiTietTheoNgay();
                                    break;
                                }
                            case "rdo_BCSanLuongTheoCN": // 5
                                {
                                    LuongSPTheoCongNhan();
                                    break;
                                }
                            case "rdo_BCDoanhThuTongHop": //8
                                {
                                    BaoCaoDoanhThuTL();
                                    break;
                                }
                        }

                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "guimail":
                    {
                        frmsendMail frm = new frmsendMail(2, Convert.ToInt32(LK_DON_VI.EditValue), LK_DON_VI.Text);
                        frm.ShowDialog();
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
            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() == "rdo_bangluongsnaphamtonghoptheoMH") return;
            try
            {
                DataTable dtCongNhan = new DataTable();
                dtCongNhan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanBC", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue,
                                                        LK_TO.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, datTNgay.DateTime, datDNgay.DateTime));
                dtCongNhan.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dtCongNhan, true, true, false, true, true, this.Name);
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
        private void LoadGrvMaHang()
        {
            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() != "rdo_bangluongsnaphamtonghoptheoMH") return;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen_BT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dt, true, true, false, true, true, this.Name);
                try
                {
                    grvCN.Columns["CHON"].Visible = false;
                    grvCN.Columns["ID_ORD"].Visible = false;
                    grvCN.Columns["ID_CHUYEN"].Visible = false;
                    grvCN.OptionsSelection.CheckBoxSelectorField = "CHON";
                }
                catch { }
            }
            catch { }
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXN();
            LoadCboTo();
            LoadChuyen();
            LoadGrvCongNhan();
            LoadGrvMaHang();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
            LoadGrvCongNhan();
            LoadGrvMaHang();

        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
            LoadGrvMaHang();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_bangluongsanphamtonghop":
                    {
                        lblNgayXem.Enabled = true;
                        datNgayXem.Enabled = true;

                        lbThang.Enabled = false;
                        datThang.Enabled = false;

                        lblTNgay.Enabled = false;
                        datTNgay.Enabled = false;
                        lblDNgay.Enabled = false;
                        datDNgay.Enabled = false;

                        //grdCN.Visible = false;
                        chkInTheoCongNhan.Visible = false;
                        chkInTheoCongNhan.EditValue = false;
                        break;
                    }
                case "rdo_BCSanLuongTheoCN":
                    {
                        LoadGrvCongNhan();
                        chkInTheoCongNhan.EditValue = true;
                        chkInTheoCongNhan.Visible = true;
                        chkInTheoCongNhan.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblInTheoCongNhan");
                        //grdCN.Visible = true;
                        break;
                    }
                case "rdo_bangluongsnaphamtonghoptheoMH":
                    {
                        if (Commons.Modules.KyHieuDV == "BT")
                        {
                            LoadGrvMaHang();
                            chkInTheoCongNhan.EditValue = true;
                            chkInTheoCongNhan.Visible = true;
                            chkInTheoCongNhan.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblInTheoMaHang");
                        }
                        lblNgayXem.Enabled = false;
                        datNgayXem.Enabled = false;

                        lbThang.Enabled = true;
                        datThang.Enabled = true;
                        lblTNgay.Enabled = true;
                        datTNgay.Enabled = true;
                        lblDNgay.Enabled = true;
                        datDNgay.Enabled = true;
                        break;

                    }
                default:
                    {
                        lblNgayXem.Enabled = false;
                        datNgayXem.Enabled = false;

                        lbThang.Enabled = true;
                        datThang.Enabled = true;
                        lblTNgay.Enabled = true;
                        datTNgay.Enabled = true;
                        lblDNgay.Enabled = true;
                        datDNgay.Enabled = true;
                        //grdCN.Visible = false;
                        chkInTheoCongNhan.Visible = false;
                        chkInTheoCongNhan.EditValue = false;
                        chkInTheoCongNhan.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblInTheoCongNhan");
                        break;
                    }
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
        private void LuongSPTongHopNgay()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopNgay", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = datNgayXem.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
                    return;
                }


                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM NGÀY " + Convert.ToDateTime(datNgayXem.EditValue).ToString("dd/MM/yyyy") + "";

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 242, 204);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tiền lương sản phẩm";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Số giờ thực tế";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Lương SP bình quân 1 giờ";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Lương ngày theo giờ HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 9], oSheet.Cells[7, 9]];
                row4_TieuDe_TTC.Value2 = "Summary theo ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 7;
                oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;

                rowCnt++;
                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;


                formatRange = oSheet.Range[oSheet.Cells[8, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 8], oSheet.Cells[rowCnt, 8]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[1].Copy();
                string sLCBNgay = dtBCThang.Rows[0][0].ToString();
                string sForMatLCB = dtBCThang.Rows[0][1].ToString();
                sForMatLCB = sForMatLCB.Replace(',', '.');

                for (int i = 0; i < rowCnt - 8; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 7], oSheet.Cells[(i + 8), 7]];
                    formatRange.Value = "=IFERROR(" + CellAddress(oSheet, (i + 8), 5) + "/" + CellAddress(oSheet, (i + 8), 6) + ",0)";

                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 8], oSheet.Cells[(i + 8), 8]];
                    formatRange.Value = "=" + CellAddress(oSheet, (i + 8), 7) + " * 9.6";

                    formatRange = oSheet.Range[oSheet.Cells[(i + 8), 9], oSheet.Cells[(i + 8), 9]];
                    formatRange.Value = "=+IF(" + CellAddress(oSheet, (i + 8), 8) + "<" + sLCBNgay + @","" < " + sForMatLCB + @""",IF(" + CellAddress(oSheet, (i + 8), 8) + ">=" + sLCBNgay + @","" >= " + sForMatLCB + @"""))";
                }


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.Value2 = "=SUBTOTAL(9," + CellAddress(oSheet, 8, 5) + ":" + CellAddress(oSheet, rowCnt - 1, 5) + ")";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;


                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                oSheet.Name = "Tổng hợp";


                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[1, 8]];
                formatRange.Merge();
                formatRange.Value2 = "BẢNG TỔNG HỢP LƯƠNG SẢN PHẨM THEO TỔ SX NGÀY " + datNgayXem.DateTime.ToString("dd/MM/yyyy");
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 16;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                formatRange = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, 8]];
                formatRange.Font.Size = fontSizeTieuDe;
                formatRange.Font.Name = fontName;
                formatRange.Font.Bold = true;
                formatRange.WrapText = true;
                formatRange.NumberFormat = "@";
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Interior.Color = Color.FromArgb(255, 242, 204);

                formatRange = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, 1]];
                formatRange.Value = "STT";
                formatRange.ColumnWidth = 5;

                formatRange = oSheet.Range[oSheet.Cells[3, 2], oSheet.Cells[3, 2]];
                formatRange.Value = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                formatRange.ColumnWidth = 35;

                formatRange = oSheet.Range[oSheet.Cells[3, 3], oSheet.Cells[3, 3]];
                formatRange.Value = "Số LĐ";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 4], oSheet.Cells[3, 4]];
                formatRange.Value = "Số LĐTT có mặt";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 5], oSheet.Cells[3, 5]];
                formatRange.Value = "Số LĐ có mặt";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 6], oSheet.Cells[3, 6]];
                formatRange.Value = "Tổng tiền lương";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 7], oSheet.Cells[3, 7]];
                formatRange.Value = "Lương SP BQ / người";
                formatRange.ColumnWidth = 15;

                formatRange = oSheet.Range[oSheet.Cells[3, 8], oSheet.Cells[3, 8]];
                formatRange.Value = "Ghi chú";
                formatRange.ColumnWidth = 15;



                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[2].Copy();
                lastColumn = dtBCThang.Columns.Count;
                dr = dtBCThang.Select();
                rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 3;
                oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;


                formatRange = oSheet.Range[oSheet.Cells[4, 3], oSheet.Cells[rowCnt, 3]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "#,##0.0;(#,##0.0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }


                formatRange = oSheet.Range[oSheet.Cells[4, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }


                for (int i = 0; i < rowCnt - 3; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[(i + 4), 7], oSheet.Cells[(i + 4), 7]];
                    formatRange.Value = "=IFERROR(" + CellAddress(oSheet, (i + 4), 6) + "/" + CellAddress(oSheet, (i + 4), 5) + ",0)";
                }

                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 2]];
                formatRange.Merge();
                formatRange.Value = "Tổng";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 3) + ":" + CellAddress(oSheet, rowCnt - 1, 3) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0.0;(#,##0.0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 4) + ":" + CellAddress(oSheet, rowCnt - 1, 4) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 5) + ":" + CellAddress(oSheet, rowCnt - 1, 5) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.Value = "=SUBTOTAL(9," + CellAddress(oSheet, 4, 6) + ":" + CellAddress(oSheet, rowCnt - 1, 6) + ")";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Bold = true;
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                for (int i = 1; i < 5; i++)
                {
                    if (i != 2)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[4, i], oSheet.Cells[rowCnt, i]];
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }
                }

                BorderAround(oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[rowCnt, lastColumn]]);

                oWB.Sheets[1].Activate();
                Commons.Modules.ObjSystems.HideWaitForm();
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void LuongSPTongHopThang()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                DateTime dNgayDauThang = datTNgay.DateTime;
                DateTime dNgayCuoiThang = datDNgay.DateTime;

                DateTime TuNgay = dNgayDauThang;
                DateTime DenNgay = dNgayCuoiThang;

                int soNgay = DenNgay.Day - TuNgay.Day;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@bNgoaiChuyen", SqlDbType.Bit).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.HideWaitForm();
                    this.Cursor = Cursors.Default;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;
                int ngayCongChuanThang = 1;
                try
                {
                    DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                    DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                    ngayCongChuanThang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tình trạng nhân sự";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Ngày vào";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Thâm niên Tính lương (tháng)";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Phân loại thâm niên";
                row4_TieuDe_TTC.ColumnWidth = 15;

                int iCot = 9;
                while (TuNgay <= DenNgay)
                {

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                    row4_TieuDe_TTC.Value2 = TuNgay.ToString("dd/MM/yyyy");
                    row4_TieuDe_TTC.ColumnWidth = 15;

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }
                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương sản phẩm ngày thường";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP ngày T7,CN";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Tổng kê tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "TG HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 150%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 200%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 300%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương BQ 1h";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC dự tính tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Tổng lương SP gốc)";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Summarry (Tổng lương SP HC dự tính Tháng " + datTNgay.DateTime.Month + "-" + ngayCongChuanThang + " ngày))";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC BQ/ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Ghi chú";
                row4_TieuDe_TTC.ColumnWidth = 25;

                oSheet.Application.ActiveWindow.SplitColumn = 6;
                oSheet.Application.ActiveWindow.SplitRow = 7;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                int rowCnt = 7;
                int stt = 0;

                int iCotSauNgay = 8 + soNgay + 1;

                TuNgay = TuNgay = dNgayDauThang;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    stt++;
                    rowCnt++;

                    dynamic[] arr = {row2["STT"].ToString(), row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["TEN_TO"].ToString(),
                        row2["TINH_TRANG_HT"].ToString(), row2["NGAY_VAO_LAM"].ToString(), row2["THAM_NIEN"].ToString(), row2["PHAN_LOAI_TN"].ToString()
                    };
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["NGAY_" + TuNgay.Day.ToString() + ""].ToString()).ToArray();
                        TuNgay = TuNgay.AddDays(1);
                    }
                    //for (int i = 1; i <= soNgay + 1; i++)
                    //{
                    //    //arr[i + (arr.Length)] = row2["NGAY_" + i + ""].ToString();
                    //    arr = arr.Append(row2["NGAY_" + i + ""].ToString()).ToArray();
                    //}

                    arr = arr.Append(row2["LUONG_SP_NGAY_THUONG"].ToString()).ToArray();
                    arr = arr.Append(row2["LUONG_SP_OT"].ToString()).ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 1) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 2) + "").ToArray(); // AN + AO
                    arr = arr.Append(row2["TG_HC"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_150"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_200"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_300"].ToString()).ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + "/(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 5) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 6) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 7) + "),0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "*" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + ",0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6*" + ngayCongChuanThang + ",0)").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @"<4100000),""3.500.000 -= 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 9) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6").ToArray();
                    arr = arr.Append(row2["GHI_CHU"].ToString()).ToArray();

                    //string s = @"=+IF("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<500000,"" < 500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1000000),""500.000 -<= 1000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">100000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">1500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=4100000),""3.500.000 -<= 4.100.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=4100000),"" >= 4.100.000"",0)))))))))";

                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;

                    TuNgay = dNgayDauThang;
                }

                rowCnt++;

                for (int colSUM = 9; colSUM < dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                for (int i = 1; i <= soNgay + 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, i + 8], oSheet.Cells[rowCnt, i + 8]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }


                for (int i = 1; i <= 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 3) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 3) + i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0);;";

                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                for (int i = 1; i <= 3; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 7) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 7) + i]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";

                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.Range[oSheet.Cells[8, iCotSauNgay + 13], oSheet.Cells[rowCnt, iCotSauNgay + 13]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";

                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt - 1, 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt - 1, 7]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 3], oSheet.Cells[rowCnt - 1, lastColumn - 3]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 2], oSheet.Cells[rowCnt - 1, lastColumn - 2]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);

                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                //oSheet.Name = rowC[1].ToString();


                dtBCThang = new DataTable();

                dNgayDauThang = datTNgay.DateTime;
                dNgayCuoiThang = datDNgay.DateTime;

                TuNgay = dNgayDauThang;
                DenNgay = dNgayCuoiThang;

                soNgay = DenNgay.Day - TuNgay.Day;

                cmd = new System.Data.SqlClient.SqlCommand("rptLuongSPTongHopThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@bNgoaiChuyen", SqlDbType.Bit).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                fontName = "Times New Roman";
                fontSizeTieuDe = 10;
                fontSizeNoiDung = 10;

                lastColumn = dtBCThang.Columns.Count;
                ngayCongChuanThang = 1;
                try
                {
                    DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                    DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                    ngayCongChuanThang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");

                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row4_TieuDe_TTNV.Value2 = "STT";
                row4_TieuDe_TTNV.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row4_TieuDe_TTC.Value2 = "Mã nhân viên";
                row4_TieuDe_TTC.ColumnWidth = 11;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row4_TieuDe_TTC.Value2 = "Họ tên";
                row4_TieuDe_TTC.ColumnWidth = 20;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[7, 4]];
                row4_TieuDe_TTC.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[7, 5]];
                row4_TieuDe_TTC.Value2 = "Tình trạng nhân sự";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[7, 6]];
                row4_TieuDe_TTC.Value2 = "Ngày vào";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[7, 7]];
                row4_TieuDe_TTC.Value2 = "Thâm niên Tính lương (tháng)";
                row4_TieuDe_TTC.ColumnWidth = 15;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 8], oSheet.Cells[7, 8]];
                row4_TieuDe_TTC.Value2 = "Phân loại thâm niên";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot = 9;
                while (TuNgay <= DenNgay)
                {

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                    row4_TieuDe_TTC.Value2 = TuNgay.ToString("dd/MM/yyyy");
                    row4_TieuDe_TTC.ColumnWidth = 15;

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }
                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương sản phẩm ngày thường";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP ngày T7,CN";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Tổng kê tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "TG HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 150%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 200%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "OT 300%";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương BQ 1h";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC dự tính tháng";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Tổng lương SP gốc)";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Summarry (Summarry (Tổng lương SP HC dự tính Tháng " + datTNgay.DateTime.Month + "-" + ngayCongChuanThang + " ngày))";
                row4_TieuDe_TTC.ColumnWidth = 25;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Lương SP HC BQ/ngày";
                row4_TieuDe_TTC.ColumnWidth = 15;

                iCot++;
                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot]];
                row4_TieuDe_TTC.Value2 = "Ghi chú";
                row4_TieuDe_TTC.ColumnWidth = 25;

                oSheet.Application.ActiveWindow.SplitColumn = 6;
                oSheet.Application.ActiveWindow.SplitRow = 7;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                rowCnt = 7;
                stt = 0;

                iCotSauNgay = 8 + soNgay + 1;

                TuNgay = TuNgay = dNgayDauThang;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    stt++;
                    rowCnt++;

                    dynamic[] arr = {row2["STT"].ToString(), row2["MS_CN"].ToString(), row2["HO_TEN"].ToString(), row2["TEN_TO"].ToString(),
                        row2["TINH_TRANG_HT"].ToString(), row2["NGAY_VAO_LAM"].ToString(), row2["THAM_NIEN"].ToString(), row2["PHAN_LOAI_TN"].ToString()
                    };
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["NGAY_" + TuNgay.Day.ToString() + ""].ToString()).ToArray();
                        TuNgay = TuNgay.AddDays(1);
                    }
                    //for (int i = 1; i <= soNgay + 1; i++)
                    //{
                    //    //arr[i + (arr.Length)] = row2["NGAY_" + i + ""].ToString();
                    //    arr = arr.Append(row2["NGAY_" + i + ""].ToString()).ToArray();
                    //}

                    arr = arr.Append(row2["LUONG_SP_NGAY_THUONG"].ToString()).ToArray();
                    arr = arr.Append(row2["LUONG_SP_OT"].ToString()).ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 1) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 2) + "").ToArray(); // AN + AO
                    arr = arr.Append(row2["TG_HC"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_150"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_200"].ToString()).ToArray();
                    arr = arr.Append(row2["OT_300"].ToString()).ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + "/(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 5) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 6) + "+" + CellAddress(oSheet, rowCnt, iCotSauNgay + 7) + "),0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 4) + "*" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + ",0)").ToArray();
                    arr = arr.Append("=IFERROR(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6*" + ngayCongChuanThang + ",0)").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @"<4100000),""3.500.000 -= 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 3) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append(@"=+IF(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<500000,"" < 500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1000000),""500.000 -<= 1000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">100000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">1500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=2500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3000000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + ">=3500000," + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @"<4100000),""3.500.000 -< 4.100.000"",IF(AND(" + CellAddress(oSheet, rowCnt, iCotSauNgay + 10) + @">=4100000),"" >= 4.100.000"",0)))))))))").ToArray();
                    arr = arr.Append("=" + CellAddress(oSheet, rowCnt, iCotSauNgay + 8) + "*9.6").ToArray();
                    arr = arr.Append(row2["GHI_CHU"].ToString()).ToArray();

                    //string s = @"=+IF("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<500000,"" < 500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1000000),""500.000 -<= 1000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">100000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=1500000),"" > 1.000.000 -<= 1.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">1500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2000000),"" > 1.500.000 -<= 2.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=2500000),""2.000.000 -<= 2.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=2500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3000000),""2.500.000 -<= 3.000.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3000000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=3500000),""3.000.000 -<= 3.500.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=3500000,"+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+"<=4100000),""3.500.000 -<= 4.100.000"",IF(AND("+CellAddress(oSheet, rowCnt, iCotSauNgay + 3)+">=4100000),"" >= 4.100.000"",0)))))))))";

                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;

                    TuNgay = dNgayDauThang;
                }

                rowCnt++;

                for (int colSUM = 9; colSUM < dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 3]];
                formatRange.Merge();
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                for (int i = 1; i <= soNgay + 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, i + 8], oSheet.Cells[rowCnt, i + 8]];
                    //formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    formatRange.NumberFormat = "#,##0;-#,##0;";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }


                for (int i = 1; i <= 4; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 3) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 3) + i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0);;";

                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                for (int i = 1; i <= 3; i++) //cộng thêm 3 cột sau ngày
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, (iCotSauNgay + 7) + i], oSheet.Cells[rowCnt, (iCotSauNgay + 7) + i]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";

                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.Range[oSheet.Cells[8, iCotSauNgay + 13], oSheet.Cells[rowCnt, iCotSauNgay + 13]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";

                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt - 1, 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[rowCnt - 1, 7]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 3], oSheet.Cells[rowCnt - 1, lastColumn - 3]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, lastColumn - 2], oSheet.Cells[rowCnt - 1, lastColumn - 2]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);

                Commons.Modules.ObjSystems.HideWaitForm();
                oWB.Sheets[1].Activate();
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void LuongSPTongHopTN()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                DateTime NgayDauThang = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                DateTime TuNgay = datTNgay.DateTime;
                DateTime DenNgay = datDNgay.DateTime;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPVTheoThamNien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 1]];
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THỐNG KÊ TIỀN LƯƠNG SẢN PHẨM THEO PHÒNG BAN VÀ THÂM NIÊN THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy") + "";

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[6, 3]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row4_TieuDe_TTC.ColumnWidth = 35;

                string SoTienCongChuanThang = "0";
                try
                {
                    SoTienCongChuanThang = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT FORMAT(ROUND(CONVERT(INT,T1.LUONG_TOI_THIEU / dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')),2),'N0') FROM dbo.LUONG_TOI_THIEU T1 INNER JOIN (SELECT ID_LTT, MAX(NGAY_QD) MAX_NGAY FROM dbo.LUONG_TOI_THIEU WHERE ID_DV = " + LK_DON_VI.EditValue + " GROUP BY ID_LTT) T2 ON T2.ID_LTT = T1.ID_LTT AND T1.NGAY_QD = T2.MAX_NGAY"));
                    //string s = Convert.ToString(SoTienCongChuanThang).ToString("#,##0.00");
                }
                catch { }
                int iCot = 2;
                while (TuNgay <= DenNgay)
                {
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[7, iCot], oSheet.Cells[7, iCot + 2]];
                    row4_TieuDe_TTC.Merge();
                    row4_TieuDe_TTC.Value = TuNgay.ToString("dd/MM/yyyy");

                    if (TuNgay.DayOfWeek.ToString() == "Sunday" || TuNgay.DayOfWeek.ToString() == "Saturday")
                    {
                        row4_TieuDe_TTC.Interior.Color = Color.FromArgb(255, 255, 0);
                    }

                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = ">=" + SoTienCongChuanThang + "";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    iCot++;
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = "<" + SoTienCongChuanThang + "";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    iCot++;
                    row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[8, iCot], oSheet.Cells[8, iCot]];
                    row4_TieuDe_TTC.Value2 = "Grand Total";
                    row4_TieuDe_TTC.ColumnWidth = 10;

                    TuNgay = TuNgay.AddDays(1);
                    iCot++;
                }

                int rowCnt = 8;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["TEN_TO"].ToString() };
                    TuNgay = datTNgay.DateTime;
                    while (TuNgay <= DenNgay)
                    {
                        arr = arr.Append(row2["GREATER_" + TuNgay.Day + ""].ToString()).ToArray();
                        arr = arr.Append(row2["LESS_" + TuNgay.Day + ""].ToString()).ToArray();
                        arr = arr.Append(row2["TOTAL_" + TuNgay.Day + ""].ToString()).ToArray();

                        TuNgay = TuNgay.AddDays(1);
                    }
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }

                rowCnt++;
                Microsoft.Office.Interop.Excel.Range formatRange;

                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, 8, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[9, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0);;";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);


                ////////////////////////////////////////////////////////////////// Bảng 2 /////////////////////////////////////////////////
                rowCnt = rowCnt + 5;
                int luongCBNgay = 0;
                try
                {
                    luongCBNgay = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + NgayDauThang.ToString("MM/dd/yyyy") + "','" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));
                }
                catch { }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng lương SP HC tháng " + datThang.DateTime.Month + "-" + luongCBNgay + " ngày (theo bộ phận)";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                rowCnt++;
                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]];
                row4_TieuDe_TTC.Value2 = "<500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                row4_TieuDe_TTC.Value2 = "500.000-<=1000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                row4_TieuDe_TTC.Value2 = ">1.000.000-<=1.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                row4_TieuDe_TTC.Value2 = ">1.500.000-<=2.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                row4_TieuDe_TTC.Value2 = "2.000.000-<=2.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]];
                row4_TieuDe_TTC.Value2 = "2.500.000-<=3.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 8], oSheet.Cells[rowCnt, 8]];
                row4_TieuDe_TTC.Value2 = "3.000.000-<=3.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 9], oSheet.Cells[rowCnt, 9]];
                row4_TieuDe_TTC.Value2 = "3.500.000-<4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 10], oSheet.Cells[rowCnt, 10]];
                row4_TieuDe_TTC.Value2 = ">=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 11], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_TTC.Value2 = "Grand Total";
                row4_TieuDe_TTC.ColumnWidth = 10;

                dtBCThang = ds.Tables[1].Copy();
                int currentRow = rowCnt;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["TEN_TO"].ToString(), row2["1"].ToString(), row2["2"].ToString(), row2["3"].ToString(), row2["4"].ToString(), row2["5"].ToString(), row2["6"].ToString(),
                        row2["7"].ToString(), row2["8"].ToString(), row2["9"].ToString(),
                        "=SUM(" + CellAddress(oSheet, rowCnt, 2) + ":" + CellAddress(oSheet, rowCnt, 10) + ")"
                    };
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }
                rowCnt++;
                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, currentRow + 1, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[currentRow + 1, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0);;";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[currentRow, 1], oSheet.Cells[rowCnt, 11]]);

                ////////////////////////////////////////////////////////////////// Bảng 3 /////////////////////////////////////////////////

                rowCnt = rowCnt + 5;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng lương SP HC tháng " + datThang.DateTime.Month + "-" + luongCBNgay + " ngày (theo thâm niên)";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                rowCnt++;
                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(221, 235, 247);


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Length of service";
                row4_TieuDe_TTC.ColumnWidth = 35;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]];
                row4_TieuDe_TTC.Value2 = "<500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                row4_TieuDe_TTC.Value2 = "500.000-<=1000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                row4_TieuDe_TTC.Value2 = ">1.000.000-<=1.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                row4_TieuDe_TTC.Value2 = ">1.500.000-<=2.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                row4_TieuDe_TTC.Value2 = "2.000.000-<=2.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]];
                row4_TieuDe_TTC.Value2 = "2.500.000-<=3.000.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 8], oSheet.Cells[rowCnt, 8]];
                row4_TieuDe_TTC.Value2 = "3.000.000-<=3.500.000";
                row4_TieuDe_TTC.ColumnWidth = 10;


                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 9], oSheet.Cells[rowCnt, 9]];
                row4_TieuDe_TTC.Value2 = "3.500.000-<=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 10], oSheet.Cells[rowCnt, 10]];
                row4_TieuDe_TTC.Value2 = ">=4.100.000";
                row4_TieuDe_TTC.ColumnWidth = 10;

                row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[rowCnt, 11], oSheet.Cells[rowCnt, 11]];
                row4_TieuDe_TTC.Value2 = "Grand Total";
                row4_TieuDe_TTC.ColumnWidth = 10;

                dtBCThang = ds.Tables[2].Copy();
                currentRow = rowCnt;
                foreach (DataRow row2 in dtBCThang.Rows)
                {
                    rowCnt++;
                    dynamic[] arr = { row2["THAM_NIEN"].ToString(), row2["1"].ToString(), row2["2"].ToString(), row2["3"].ToString(), row2["4"].ToString(), row2["5"].ToString(), row2["6"].ToString(),
                        row2["7"].ToString(), row2["8"].ToString(), row2["9"].ToString(),
                        "=SUM(" + CellAddress(oSheet, rowCnt, 2) + ":" + CellAddress(oSheet, rowCnt, 10) + ")"
                    };
                    Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 11]];
                    rowData.Font.Size = fontSizeNoiDung;
                    rowData.Font.Name = fontName;
                    rowData.Value2 = arr;
                }
                rowCnt++;
                for (int colSUM = 2; colSUM <= dtBCThang.Columns.Count; colSUM++)
                {
                    //ws.Cells[row, colSUM] = "=SUBTOTAL(9,N10:N" + (row - 1).ToString() + ")";
                    oSheet.Cells[rowCnt, colSUM] = "=SUM(" + CellAddress(oSheet, currentRow + 1, colSUM) + ":" + CellAddress(oSheet, rowCnt - 1, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;

                    formatRange = oSheet.Range[oSheet.Cells[currentRow + 1, colSUM], oSheet.Cells[rowCnt + 1, colSUM]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0);;";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.Value2 = "Tổng";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;

                BorderAround(oSheet.Range[oSheet.Cells[currentRow, 1], oSheet.Cells[rowCnt, 11]]);

                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 7);




                Commons.Modules.ObjSystems.HideWaitForm();
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void LuongSPChiTietTheoNgay()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung(-1);

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                Excel.Worksheet oSheet;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);
                    oSheet.Name = rowC[1].ToString();
                    if (oRow == 1)
                    {
                        Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 15]];
                        row4_TieuDe_BaoCao.Merge();
                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                        row4_TieuDe_BaoCao.Font.Name = fontName;
                        row4_TieuDe_BaoCao.Font.Bold = true;
                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe_BaoCao.RowHeight = 30;
                        row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG CHUYỀN MAY THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy");
                        oRow = 6;
                    }

                    Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.Range[oSheet.Cells[oRow, 7], oSheet.Cells[oRow, 7]];
                    row_Chuyen.Merge();
                    row_Chuyen.Value2 = rowC[1].ToString();
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.RowHeight = 30;

                    oRow++;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBCLuongSPChiTietNgay", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[0].Copy();
                    int totalColumn = dtBCLSP.Columns.Count;


                    oSheet.Cells[oRow, 1] = "STT";
                    oSheet.Cells[oRow, 1].ColumnWidth = 10;
                    oSheet.Cells[oRow, 2] = "Ngày";
                    oSheet.Cells[oRow, 2].ColumnWidth = 15;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 25;
                    oSheet.Cells[oRow, 4] = "Mã NV";
                    oSheet.Cells[oRow, 4].ColumnWidth = 15;
                    oSheet.Cells[oRow, 5] = "Mã NV (4 số)";
                    oSheet.Cells[oRow, 5].ColumnWidth = 10;
                    oSheet.Cells[oRow, 6] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                    oSheet.Cells[oRow, 6].ColumnWidth = 35;
                    oSheet.Cells[oRow, 7] = "Mã đơn hàng";
                    oSheet.Cells[oRow, 7].ColumnWidth = 25;
                    oSheet.Cells[oRow, 8] = "Mã công đoạn";
                    oSheet.Cells[oRow, 8].ColumnWidth = 10;
                    oSheet.Cells[oRow, 9] = "Tên công đoạn";
                    oSheet.Cells[oRow, 9].ColumnWidth = 35;
                    oSheet.Cells[oRow, 10] = "Sản lượng ghi nhận";
                    oSheet.Cells[oRow, 10].ColumnWidth = 15;
                    oSheet.Cells[oRow, 11] = "Đơn giá";
                    oSheet.Cells[oRow, 11].ColumnWidth = 15;
                    oSheet.Cells[oRow, 12] = "Tổng tiền lương";
                    oSheet.Cells[oRow, 12].ColumnWidth = 10;
                    oSheet.Cells[oRow, 13] = "Tổng SL theo MVN";
                    oSheet.Cells[oRow, 13].ColumnWidth = 15;
                    oSheet.Cells[oRow, 14] = "Tổng SL theo Cđoan";
                    oSheet.Cells[oRow, 14].ColumnWidth = 15;
                    oSheet.Cells[oRow, 15] = "Ghi chú";
                    oSheet.Cells[oRow, 15].ColumnWidth = 15;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, totalColumn]];
                    row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row_TieuDe_BaoCao.Font.Name = fontName;
                    row_TieuDe_BaoCao.Font.Bold = true;
                    row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TieuDe_BaoCao.Cells.WrapText = true;
                    row_TieuDe_BaoCao.Interior.Color = Color.FromArgb(198, 224, 180);
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
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[rowBD, 8], oSheet.Cells[oRow, totalColumn]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    BorderAround(oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, totalColumn]]);

                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 2]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 2], oSheet.Cells[oRow, 2]];
                    formatRange.NumberFormat = "dd/MM/yyyy";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 10], oSheet.Cells[oRow, 10]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 11], oSheet.Cells[oRow, 11]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.000);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 12], oSheet.Cells[oRow, 12]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 13], oSheet.Cells[oRow, 13]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 14], oSheet.Cells[oRow, 14]];
                    formatRange.NumberFormat = "#,##0;(#,##0.000);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Value2 = "=SUBTOTAL(9,L" + rowBD + ":L" + oRow.ToString() + ")";
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0);;";
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Font.Bold = true;
                    oSheet.get_Range("I" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = oRow + 2;

                    oRow = 1;
                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                this.Cursor = Cursors.Default;
                oApp.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void LuongSPTheoCongNhan_TEST()
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung(-1);

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                        row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datThang.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datNgayXem.EditValue).ToString("dd/MM/yyyy");

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
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                    cmdCT.Parameters.Add("@SendMail", SqlDbType.Bit).Value = 0;

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
                    oSheet.Cells[oRow, 2] = "Ngày";
                    oSheet.Cells[oRow, 2].ColumnWidth = 12;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 25;
                    oSheet.Cells[oRow, 4] = "Mã NV";
                    oSheet.Cells[oRow, 4].ColumnWidth = 15;
                    oSheet.Cells[oRow, 5] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
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
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0.000;(#,##0.000);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    //string CurentColumn = string.Empty;
                    //for (int colMH = 5; colMH <= totalColumn - 1; colMH++)
                    //{
                    //    CurentColumn = CharacterIncrement(colMH);
                    //}

                    //set formular
                    oSheet.get_Range("K7", "K7").Value2 = "=SUM(K" + rowBD + ":K" + oRow.ToString() + ")"; ;
                    oSheet.get_Range("K7", "K7").NumberFormat = "#,##0;(#,##0);;";
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
        private void LuongSPTheoCongNhan()
        {
            string sBTCongNhan = "sBTBCLSP" + Commons.Modules.iIDUser;

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

                this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dt1, "");

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCLSP;

                System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHNgayTheoCN_DM", conn);
                cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmdCT.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                cmdCT.Parameters.Add("@SendMail", SqlDbType.Bit).Value = 0;

                cmdCT.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                DataSet dsCT = new DataSet();
                adpCT.Fill(dsCT);
                dtBCLSP = new DataTable();
                dtBCLSP = dsCT.Tables[0].Copy();
                if (dtBCLSP.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;

                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = false;

                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                int lastColumn = dtBCLSP.Columns.Count;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);


                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row4_TieuDe_BaoCao.Merge();
                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_TieuDe_BaoCao.Font.Name = fontName;
                row4_TieuDe_BaoCao.Font.Bold = true;
                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_BaoCao.RowHeight = 30;
                row4_TieuDe_BaoCao.Value2 = "BÁO CÁO SẢN LƯỢNG THEO CÔNG NHÂN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row5_TieuDe_BaoCao.Merge();
                row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;
                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_BaoCao.RowHeight = 20;
                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + datTNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + datDNgay.DateTime.ToString("dd/MM/yyyy");

                oRow = 7;

                oRow++;




                oSheet.Cells[oRow, 1] = "STT";
                oSheet.Cells[oRow, 1].ColumnWidth = 9;
                oSheet.Cells[oRow, 2] = "Mã thẻ CNV";
                oSheet.Cells[oRow, 2].ColumnWidth = 15;
                oSheet.Cells[oRow, 3] = "Họ tên";
                oSheet.Cells[oRow, 3].ColumnWidth = 25;
                oSheet.Cells[oRow, 4] = "Mã hàng";
                oSheet.Cells[oRow, 4].ColumnWidth = 10;
                oSheet.Cells[oRow, 5] = "Mã công đoạn";
                oSheet.Cells[oRow, 5].ColumnWidth = 8;
                oSheet.Cells[oRow, 6] = "Tên công đoạn";
                oSheet.Cells[oRow, 6].ColumnWidth = 35;
                oSheet.Cells[oRow, 7] = "Tổng SL CN kê";
                oSheet.Cells[oRow, 7].ColumnWidth = 10;
                oSheet.Cells[oRow, 8] = "Đơn giá";
                oSheet.Cells[oRow, 8].ColumnWidth = 10;
                oSheet.Cells[oRow, 9] = "Thành tiền";
                oSheet.Cells[oRow, 9].ColumnWidth = 15;
                oSheet.Cells[oRow, 10] = "Chuyền thực hiện";
                oSheet.Cells[oRow, 10].ColumnWidth = 20;


                Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
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
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;
                BorderAround(oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range formatRange;


                formatRange = oSheet.Range[oSheet.Cells[rowBD, 7], oSheet.Cells[oRow, 7]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 8], oSheet.Cells[oRow, 8]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0.00;(#,##0.000);;";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 9], oSheet.Cells[oRow, 9]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //can giua

                // STT
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 1]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                // ma ql
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                // SUM
                formatRange = oSheet.Range[oSheet.Cells[7, 9], oSheet.Cells[7, 9]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Font.Bold = true;
                formatRange.Value = "=SUBTOTAL(9,I" + rowBD + ":I" + oRow.ToString() + ")";
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();

                oApp.Visible = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoDoanhThuTL()
        {
            this.Cursor = Cursors.WaitCursor;
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBaoCaoDoanhThuTinhLuong", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TuThang", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DenThang", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime TuNgay;
                DateTime DenNgay;
                TuNgay = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, 1);
                DenNgay = new DateTime(datDNgay.DateTime.Year, datDNgay.DateTime.Month, 1);

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 10;


                int lastColumn = dtBCThang.Columns.Count;

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 24;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO DOANH THU TÍNH LƯƠNG TỪ THÁNG " + datTNgay.DateTime.Month + "-" + datDNgay.DateTime.Month + " NĂM " + datTNgay.DateTime.Year + "";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[7, lastColumn]];
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row5_TieuDe_DV.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_DV.ColumnWidth = 15;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[7, 2]];
                row5_TieuDe_LDBQ.Value2 = "Khách hàng";
                row5_TieuDe_LDBQ.ColumnWidth = 20;

                Range row5_TieuDe_LDT = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[7, 3]];
                row5_TieuDe_LDT.Value2 = "Mã hàng";
                row5_TieuDe_LDT.ColumnWidth = 15;


                Range row6_TieuDe_TT = oSheet.Range[oSheet.Cells[6, 4], oSheet.Cells[7, 4]];
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "Sản lượng đơn hàng theo kế hoạch";
                row6_TieuDe_TT.ColumnWidth = 15;

                int colTieuDe = 4; // column bắt đầu từ cột E
                while (TuNgay <= DenNgay)
                {
                    colTieuDe++;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[6, colTieuDe], oSheet.Cells[6, colTieuDe + 4]];
                    row6_TieuDe_TT.Merge();
                    row6_TieuDe_TT.Value2 = TuNgay.ToString("MM/yyyy");
                    row6_TieuDe_TT.ColumnWidth = 15;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[7, colTieuDe], oSheet.Cells[7, colTieuDe]];
                    row6_TieuDe_TT.Value2 = "Sản lượng T" + TuNgay.Month + "";
                    row6_TieuDe_TT.ColumnWidth = 15;

                    colTieuDe++;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[7, colTieuDe], oSheet.Cells[7, colTieuDe]];
                    row6_TieuDe_TT.Value2 = "Đơn giá";
                    row6_TieuDe_TT.ColumnWidth = 15;

                    colTieuDe++;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[7, colTieuDe], oSheet.Cells[7, colTieuDe]];
                    row6_TieuDe_TT.Value2 = "Thành tiền";
                    row6_TieuDe_TT.ColumnWidth = 15;

                    colTieuDe++;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[7, colTieuDe], oSheet.Cells[7, colTieuDe]];
                    row6_TieuDe_TT.Value2 = "Lương chốt";
                    row6_TieuDe_TT.ColumnWidth = 15;

                    colTieuDe++;

                    row6_TieuDe_TT = oSheet.Range[oSheet.Cells[7, colTieuDe], oSheet.Cells[7, colTieuDe]];
                    row6_TieuDe_TT.Value2 = "Chênh lệch  doanh thu theo đơn giá với lương chốt";
                    row6_TieuDe_TT.ColumnWidth = 15;

                    TuNgay = TuNgay.AddMonths(1);
                }

                colTieuDe++;

                row6_TieuDe_TT = oSheet.Range[oSheet.Cells[6, colTieuDe], oSheet.Cells[7, colTieuDe]];
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "TỔNG SL TÍNH LƯƠNG ĐẾN THÁNG";
                row6_TieuDe_TT.ColumnWidth = 11;

                colTieuDe++;

                row6_TieuDe_TT = oSheet.Range[oSheet.Cells[6, colTieuDe], oSheet.Cells[7, colTieuDe]];
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "CHÊNH LỆCH SL TÍNH LƯƠNG VỚI KẾ HOẠCH";
                row6_TieuDe_TT.ColumnWidth = 11;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowBD = 7;
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                for (int j = 0; j < TEN_TO.Count(); j++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
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

                    //Đổ dữ liệu của tổ
                    oSheet.Range[oSheet.Cells[rowBD + 1, 1], oSheet.Cells[rowCnt + 1, lastColumn]].Value2 = rowData;

                    //Tính tổng xí nghiệp
                    Range row_groupTONG_Format = oSheet.Range[oSheet.Cells[(rowBD + current_dr + 1), 1], oSheet.Cells[(rowBD + current_dr + 1), lastColumn]];
                    row_groupTONG_Format.Interior.Color = Color.Yellow;
                    row_groupTONG_Format.Font.Bold = true;
                    oSheet.Cells[(rowBD + current_dr + 1), 1] = "Tổng " + TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    for (int colSUM = 5; colSUM <= dtBCThang.Columns.Count; colSUM++)
                    {
                        oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    }

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt + 2;
                Microsoft.Office.Interop.Excel.Range formatRange;

                for (col = 5; col <= dtBCThang.Columns.Count; col++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[8, col], oSheet.Cells[rowCnt, col]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);

                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
                //Sum đơn vị
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }
        private void LSPThangTheoTungCongDoan_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung(-1);

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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

                    oSheet.Name = rowC[1].ToString();

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
                        row4_TieuDe_BaoCao.Value2 = "BẢNG KÊ SẢN LƯỢNG THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy");

                        Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "M5");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 20;
                        row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(datTNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");

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

                    row_Chuyen = oSheet.get_Range("B" + oRow.ToString(), "B" + oRow.ToString());
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.Value2 = "Tổ trưởng";
                    oRow++;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN_DM", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                    oSheet.Cells[oRow, 4] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                    oSheet.Cells[oRow, 4].ColumnWidth = 15;
                    oSheet.Cells[oRow, 5] = "Mã hàng";
                    oSheet.Cells[oRow, 5].ColumnWidth = 10;
                    oSheet.Cells[oRow, 6] = "Mã công đoạn";
                    oSheet.Cells[oRow, 6].ColumnWidth = 8;
                    oSheet.Cells[oRow, 7] = "Tên công đoạn";
                    oSheet.Cells[oRow, 7].ColumnWidth = 35;
                    oSheet.Cells[oRow, 8] = "Tổng sản lượng cá nhân đã kê";
                    oSheet.Cells[oRow, 8].ColumnWidth = 10;
                    oSheet.Cells[oRow, 9] = "Sản lượng nhập";
                    oSheet.Cells[oRow, 9].ColumnWidth = 10;
                    oSheet.Cells[oRow, 10] = "Sản lượng sau điều chỉnh";
                    oSheet.Cells[oRow, 10].ColumnWidth = 10;
                    oSheet.Cells[oRow, 11] = "Tổng sản lượng công đoạn";
                    oSheet.Cells[oRow, 11].ColumnWidth = 10;
                    oSheet.Cells[oRow, 12] = "Sản lượng chốt tính lương";
                    oSheet.Cells[oRow, 12].ColumnWidth = 15;
                    oSheet.Cells[oRow, 13] = "Thừa(-)/ Thiếu(+)";
                    oSheet.Cells[oRow, 13].ColumnWidth = 10;
                    oSheet.Cells[oRow, 14] = "Đơn giá";
                    oSheet.Cells[oRow, 14].ColumnWidth = 10;
                    oSheet.Cells[oRow, 15] = "Thành tiền";
                    oSheet.Cells[oRow, 15].ColumnWidth = 15;

                    string LastTitleColumn = string.Empty;
                    LastTitleColumn = "O";
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
                    Microsoft.Office.Interop.Excel.Range formatRange1;

                    formatRange1 = oSheet.get_Range("J" + rowBD, "J" + rowBD.ToString());
                    formatRange1.Value2 = "=H8+I8";
                    formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }


                    // công thức cột thừa thiếu
                    formatRange1 = oSheet.get_Range("M" + rowBD, "M" + rowBD.ToString());
                    formatRange1.Value2 = "=L8-K8";
                    formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    // công thức cột tổng sản lượng công đoạn -- K
                    formatRange1 = oSheet.get_Range("K" + rowBD, "K" + rowBD.ToString());
                    formatRange1.Value2 = "=SUMIFS($J$8:$J$" + oRow + ",$E$8:$E$" + oRow + ",E8,$F$8:$F$" + oRow + ",F8)";
                    formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    // công thức cột thành tiền
                    formatRange1 = oSheet.get_Range("O" + rowBD, "O" + rowBD.ToString());
                    formatRange1.Value2 = "=J8*N8";
                    formatRange = oSheet.get_Range("O" + rowBD, "O" + oRow.ToString());

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    formatRange = oSheet.get_Range("H" + rowBD, "H" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("I" + rowBD, "I" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("J" + rowBD, "J" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("K" + rowBD, "K" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("L" + rowBD, "L" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("M" + rowBD, "M" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("N" + rowBD, "N" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("O" + rowBD, "O" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);


                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Value2 = "=SUBTOTAL(9,O" + rowBD + ":O" + oRow.ToString() + ")";
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0);;";
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Bold = true;
                    oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = 1;
                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                this.Cursor = Cursors.Default;

                oApp.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void LSPThangTheoTungCongDoan_BT()
        {
            try
            {
                string sBT = "";
                if (chkInTheoCongNhan.Checked)
                {
                    sBT = "sBTMaHang" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdCN.DataSource, "");
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTTChung;
                DataTable dtChuyen;
                DataTable dtBCLSP;

                dtTTChung = new DataTable();
                dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung(-1);

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen_BT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                int countSheetName = 1;
                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    oSheet.Name = countSheetName.ToString() + "-" + rowC[2].ToString();
                    countSheetName++;
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
                        row4_TieuDe_BaoCao.Value2 = "KẾT MÃ HÀNG THÁNG " + Convert.ToDateTime(datThang.EditValue).ToString("MM/yyyy");

                        Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "H5");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 20;
                        row5_TieuDe_BaoCao.Value2 = "CHUYỀN : " + rowC[2].ToString() + " - MÃ HÀNG : " + rowC[4].ToString() + " ";

                        row5_TieuDe_BaoCao = oSheet.get_Range("A6", "H6");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 20;
                        row5_TieuDe_BaoCao.Value2 = "*Lưu ý : mã hàng chuyển lại cho LĐTL phải đầy đủ chữ ký của công nhân";

                        oRow = 7;
                    }

                    Microsoft.Office.Interop.Excel.Range row_Chuyen = oSheet.get_Range("G" + oRow.ToString(), "G" + oRow.ToString());
                    row_Chuyen.Merge();
                    row_Chuyen.Value2 = rowC[2].ToString();
                    row_Chuyen.Font.Size = fontSizeNoiDung;
                    row_Chuyen.Font.Name = fontName;
                    row_Chuyen.Font.Bold = true;
                    row_Chuyen.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_Chuyen.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_Chuyen.RowHeight = 30;

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN_BT", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[1];
                    cmdCT.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = rowC[3];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
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
                    oSheet.Cells[oRow, 2] = "Tên công đoạn";
                    oSheet.Cells[oRow, 2].ColumnWidth = 35;
                    oSheet.Cells[oRow, 3] = "Họ tên";
                    oSheet.Cells[oRow, 3].ColumnWidth = 25;
                    oSheet.Cells[oRow, 4] = "Số lượng";
                    oSheet.Cells[oRow, 4].ColumnWidth = 10;
                    oSheet.Cells[oRow, 5] = "Đơn giá";
                    oSheet.Cells[oRow, 5].ColumnWidth = 10;
                    oSheet.Cells[oRow, 6] = "Thành tiền";
                    oSheet.Cells[oRow, 6].ColumnWidth = 15;
                    oSheet.Cells[oRow, 7] = "Ký nhận";
                    oSheet.Cells[oRow, 7].ColumnWidth = 15;
                    oSheet.Cells[oRow, 8] = "Ghi chú";
                    oSheet.Cells[oRow, 8].ColumnWidth = 15;

                    string LastTitleColumn = string.Empty;
                    LastTitleColumn = "H";
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
                    int rowBD = 8;

                    string[] HO_TEN = dtBCLSP.AsEnumerable().Select(r => r.Field<string>("HO_TEN")).Distinct().ToArray();
                    DataTable dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                    int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu



                    Microsoft.Office.Interop.Excel.Range formatRange;
                    Microsoft.Office.Interop.Excel.Range formatRange1;
                    for (int i = 0; i < HO_TEN.Count(); i++)
                    {
                        dtBCLSP = dsCT.Tables[0].Copy();
                        dtBCLSP = dtBCLSP.AsEnumerable().Where(r => r.Field<string>("HO_TEN") == HO_TEN[i]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCLSP.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCLSP.Columns.Count];

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

                        formatRange = oSheet.get_Range("F" + (oRow + 1).ToString(), "F" + (oRow + 1).ToString());
                        formatRange.Value2 = "=SUM(F" + rowBD + ":F" + oRow + ")";
                        formatRange.Font.Size = fontSizeNoiDung;
                        formatRange.Font.Name = fontName;
                        formatRange.Font.Bold = true;

                        formatRange1 = oSheet.get_Range("F" + rowBD, "F" + rowBD.ToString());
                        formatRange1.Value2 = "=D8*E8";
                        formatRange = oSheet.get_Range("F" + rowBD, "F" + oRow.ToString());

                        if (dtBCLSP.Rows.Count > 1)
                        {
                            formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                        }

                        formatRange = oSheet.get_Range("A" + rowBD.ToString(), LastTitleColumn + (oRow + 1).ToString());
                        formatRange.BorderAround();
                        Microsoft.Office.Interop.Excel.Borders borders = formatRange.Borders;
                        borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // Thêm border liên tục cho hàng dọc

                        rowBD = oRow + 2;
                        rowCnt = 0;
                    }

                    rowBD = 8;

                    //BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + (oRow + 1).ToString()));

                    // công thức cột thành tiền

                    formatRange = oSheet.get_Range("D" + rowBD, "D" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("E" + rowBD, "E" + oRow.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    formatRange = oSheet.get_Range("F" + rowBD, "F" + (oRow + 1).ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Value2 = "=SUBTOTAL(9,O" + rowBD + ":O" + oRow.ToString() + ")";
                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").NumberFormat = "#,##0;(#,##0);;";
                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Size = fontSizeNoiDung;
                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Name = fontName;
                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Font.Bold = true;
                    //oSheet.get_Range("O" + (rowBD - 2).ToString() + "").Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = 1;
                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                this.Cursor = Cursors.Default;

                oApp.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }

        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung(-1);
                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

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
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

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
        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }

        private void datThang_EditValueChanged(object sender, EventArgs e)
        {
            datTNgay.EditValue = Convert.ToDateTime(datThang.EditValue).AddDays((-datThang.DateTime.Day) + 1);
            datDNgay.EditValue = Convert.ToDateTime(datTNgay.EditValue).AddMonths(1).AddDays(-1);
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
            LoadGrvMaHang();
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
            LoadGrvMaHang();
        }
    }
}
