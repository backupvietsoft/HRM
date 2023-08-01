using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using DevExpress.CodeParser;
using DevExpress.DataProcessing;
using DevExpress.XtraCharts.Native;
using OfficeOpenXml;
using DevExpress.XtraEditors.Controls;

namespace Vs.Payroll
{
    public partial class ucBCLuongThang : DevExpress.XtraEditors.XtraUserControl
    {
        string sKyHieuDV = "";
        public ucBCLuongThang()
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

        private void ucBCLuongThang_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
                Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO, Commons.Modules.KyHieuDV == "TG" ? true : false);
                LoadThang();
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCACH_TINH_LUONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, -1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCachTinhLuong, dt, "ID_CTL", "TEN", "TEN");
                cboCachTinhLuong.EditValue = 2;
                lk_NgayIn.EditValue = DateTime.Today;
                rdo_ChucVu.Visible = false;
                switch (Commons.Modules.KyHieuDV)
                {
                    case "MT":
                        {
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangNV").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongHotro").FirstOrDefault());
                            var item = new RadioGroupItem
                            {
                                Tag = "rdoAll",
                                Description = "All",
                                Value = -1
                            };
                            rdo_ChucVu.Properties.Items.Insert(0, item);
                            rdo_ChucVu.Visible = true;
                            rdoChinhThuc.Visible = false;
                            rdo_ChucVu.SelectedIndex = 1;
                            break;
                        }
                    case "DM":
                        {
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangToTruong").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangQC").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangThoiGian").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangCBQLChuyen").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongHotro").FirstOrDefault());
                            break;
                        }
                    case "TG":
                        {
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangQC").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangThoiGian").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangCBQLChuyen").FirstOrDefault());
                            //rdo_BangLuongThangNV => Bảng lương tháng văn phòng
                            //rdo_BangLuongThangToTruong =>Bảng lương tháng tiền mặt
                            var item = new RadioGroupItem
                            {
                                Tag = "rdoAll",
                                Description = "All",
                                Value = -1
                            };
                            rdo_ChucVu.Properties.Items.Insert(0, item);
                            rdo_ChucVu.Visible = true;
                            rdo_ChucVu.SelectedIndex = 1;
                            break;
                        }

                    default:
                        {
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangToTruong").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangQC").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangThoiGian").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangCBQLChuyen").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangNV").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongThangNV").FirstOrDefault());
                            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangLuongHotro").FirstOrDefault());
                            break;
                        }
                }
            }
            catch { }

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
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_BangLuongHotro":
                                {
                                    BangLuongThangHoTro_TG();
                                    break;
                                }
                            case "rdo_BangLuongThangSanXuat":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                BangLuongThang_DM();
                                                break;
                                            }

                                        case "BT":
                                            {
                                                BangLuongThang_BT();
                                                break;
                                            }
                                        case "TG":
                                            {
                                                BangLuongThang_TG();
                                                break;
                                            }
                                        case "MT":
                                            {
                                                BangLuongThang_MT();
                                                break;
                                            }
                                        default:
                                            {
                                                PhieuLuongThang();
                                                break;
                                            }

                                    }
                                    break;
                                }
                            case "rdo_BangLuongThangNV":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                BangLuongThangNV_DM();
                                                break;
                                            }
                                        case "TG":
                                            {
                                                BangLuongThangVanPhong_TG();
                                                break;
                                            }

                                    }
                                    break;

                                }
                            case "rdo_BangLuongThangCBQLChuyen":
                                {

                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangLuongThangCBQLC_MT();
                                                break;
                                            }
                                        default:
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

                                                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                                                    ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("K9", "P" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("W9", "AR" + row).NumberFormat = "#,##0;(#,##0);;";

                                                    ws.get_Range("A9", "B" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("A9", "B" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                                                break;
                                            }
                                    }


                                    break;
                                }

                            case "rdo_BangLuongThangThoiGian":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangLuongThangTG_MT();
                                                break;
                                            }
                                        default:
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

                                                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                                                    ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("K9", "L" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("M9", "M" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("N9", "N" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("W9", "AP" + row).NumberFormat = "#,##0;(#,##0);;";

                                                    ws.get_Range("A9", "B" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("A9", "B" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                                                break;
                                            }
                                    }

                                }
                                break;
                            case "rdo_BangLuongThangQC":
                                {
                                   

                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangLuongThangQCCHUYEN_MT();
                                                break;
                                            }
                                        default:
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
                                                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
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
                                                    ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("K9", "M" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("N9", "N" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("O9", "O" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("P9", "P" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("R9", "R" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("S9", "S" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("T9", "T" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("U9", "AO" + row).NumberFormat = "#,##0;(#,##0);;";

                                                    ws.get_Range("A9", "B" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("A9", "B" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                                                break;
                                            }
                                    }
                                }
                                break;

                            case "rdo_BangLuongThangToTruong":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
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

                                                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                                                    ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("K9", "L" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("M9", "M" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("N9", "N" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                                                    ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("W9", "AP" + row).NumberFormat = "#,##0;(#,##0);;";

                                                    ws.get_Range("A9", "B" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("A9", "B" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                                        case "TG":
                                            {
                                                BangLuongThangTienMat_TG();
                                                break;
                                            }
                                        case "MT":
                                            {
                                                BangLuongThangToTruong_MT();
                                                break;
                                            }

                                    }
                                }
                                break;
                            case "rdo_BangTienLuongChuyenATM":
                                {
                                    string sThang = cboThang.EditValue.ToString();
                                    DateTime dNgayIn = Convert.ToDateTime(lk_NgayIn.EditValue.ToString());

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    DataTable dt;
                                    DataTable dt1;
                                    string sPS = "";
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                sPS = "rptBangLuongThangChuyenNganHang_DM";
                                                break;
                                            }
                                        case "BT":
                                            {
                                                sPS = "rptBangLuongThangChuyenNganHang_BT";
                                                break;
                                            }
                                        case "MT":
                                            {
                                                BangLuongThangAMT_MT();
                                                return;
                                            }
                                        case "TG":
                                            {
                                                BangLuongThangATM_TG();
                                                return;
                                            }
                                    }
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sPS, conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();

                                    try
                                    {

                                        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                                        int fontSizeTieuDe = 15;
                                        int fontSizeNoiDung = 11;

                                        DataTable dtDV = new DataTable();
                                        dtDV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.DON_VI WHERE ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1"));

                                        Range row_DonVi = ws.get_Range("A1", "E1");
                                        row_DonVi.Merge();
                                        row_DonVi.Font.Size = fontSizeNoiDung;
                                        row_DonVi.Font.Name = fontName;
                                        row_DonVi.Font.Bold = true;
                                        row_DonVi.WrapText = true;
                                        row_DonVi.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row_DonVi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row_DonVi.Value2 = dtDV.Rows[0]["TEN_DV"];

                                        Range row_MST = ws.get_Range("A2", "E2");
                                        row_MST.Merge();
                                        row_MST.Font.Size = fontSizeNoiDung;
                                        row_MST.Font.Name = fontName;
                                        row_MST.Font.Bold = true;
                                        row_MST.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row_MST.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row_MST.Value2 = "MST: 0601156266";

                                        Range row_TieuDe = ws.get_Range("A3", "E3");
                                        row_TieuDe.Merge();
                                        row_TieuDe.Font.Size = fontSizeTieuDe;
                                        row_TieuDe.Font.Name = fontName;
                                        row_TieuDe.Font.Bold = true;
                                        row_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row_TieuDe.Value2 = "BẢNG THANH TOÁN LƯƠNG THÁNG " + sThang;

                                        Range row_ND10 = ws.get_Range("A5", "A5");
                                        row_ND10.ColumnWidth = 6;
                                        row_ND10.Value2 = "STT";

                                        Range row_ND11 = ws.get_Range("B5", "B5");
                                        row_ND11.ColumnWidth = 12;
                                        row_ND11.Value2 = "MA NV";

                                        Range row_ND12 = ws.get_Range("C5", "C5");
                                        row_ND12.ColumnWidth = 30;
                                        row_ND12.Value2 = "HỌ VÀ TÊN";

                                        Range row_ND13 = ws.get_Range("D5", "D5");
                                        row_ND13.ColumnWidth = 20;
                                        row_ND13.Value2 = "SỐ TK";

                                        Range row_ND14 = ws.get_Range("E5", "E5");
                                        row_ND14.ColumnWidth = 15;
                                        row_ND14.Value2 = "SỐ TIỀN";

                                        Range row_NDTD = ws.get_Range("A5", "E5");
                                        row_NDTD.Font.Size = fontSizeNoiDung;
                                        row_NDTD.Font.Name = fontName;
                                        row_NDTD.Font.Bold = true;
                                        row_NDTD.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        row_NDTD.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        row = 5;
                                        foreach (DataRow row1 in dt.Rows)
                                        {
                                            stt++;
                                            row++;

                                            Range rowDataFText = ws.get_Range("D" + row, "D" + row);
                                            rowDataFText.NumberFormat = "@";
                                            Range rowDataFNum = ws.get_Range("E" + row, "E" + row);
                                            rowDataFNum.NumberFormat = "#,##0;(#,##0); ; ";

                                            dynamic[] arr = { stt, row1["MS_CN"].ToString(), row1["HO_TEN"].ToString(), row1["SO_TAI_KHOAN"].ToString(), row1["TIEN_LUONG"].ToString() };

                                            Range rowData = ws.get_Range("A" + row, "E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                            rowData.Font.Size = fontSizeNoiDung;
                                            rowData.Font.Name = fontName;
                                            rowData.Value2 = arr;
                                        }

                                        row++;

                                        Range row_Tong = ws.get_Range("A" + row, "C" + row);
                                        row_Tong.Merge();
                                        row_Tong.Value2 = "TỔNG";

                                        ws.Cells[row, 5] = "=SUM(" + CellAddress(ws, 6, 5) + ":" + CellAddress(ws, row - 1, 5) + ")";
                                        ws.Cells[row, 5].NumberFormat = "#,##0;(#,##0); ; ";

                                        Range rowFormatF = ws.get_Range("A" + row, "E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                        rowFormatF.Font.Size = fontSizeNoiDung;
                                        rowFormatF.Font.Name = fontName;
                                        rowFormatF.Font.Bold = true;

                                        BorderAround(ws.get_Range("A5", "E" + row));

                                        row = row + 2;
                                        Range row_ND18 = ws.get_Range("A" + row, "A" + row);
                                        row_ND18.Font.Size = fontSizeNoiDung;
                                        row_ND18.Font.Name = fontName;
                                        row_ND18.Font.Bold = true;
                                        row_ND18.Font.Italic = true;
                                        string sSQL = "SELECT dbo.DoiTienSoThanhChuTiengViet(" + ws.Cells[row - 2, 5].Value + ",'VND')";
                                        row_ND18.Value2 = "Số tiền Bằng chữ : " + Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));

                                        row = row + 2;
                                        Range row_ND3 = ws.get_Range("D" + row, "D" + row);
                                        row_ND3.Font.Size = fontSizeNoiDung;
                                        row_ND3.Font.Name = fontName;
                                        //row_ND3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        //row_ND3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        row_ND3.Value2 = "Ngày " + dNgayIn.Day + " Tháng " + dNgayIn.Month + " Năm " + dNgayIn.Year;

                                        row = row + 1;
                                        Range row_ND19 = ws.get_Range("A" + row, "A" + row);
                                        row_ND19.Font.Size = fontSizeNoiDung;
                                        row_ND19.Font.Name = fontName;
                                        row_ND19.Font.Bold = true;
                                        row_ND19.Value2 = "Người lập biểu";

                                        Range row_ND20 = ws.get_Range("D" + row, "D" + row);
                                        row_ND20.Font.Size = fontSizeNoiDung;
                                        row_ND20.Font.Name = fontName;
                                        row_ND20.Font.Bold = true;
                                        row_ND20.Value2 = "Ban giám đốc";

                                    }
                                    catch
                                    { }
                                }
                                break;
                            case "rdo_PhieuNhanLuong":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                PhieuLuongThang_DM();
                                                break;
                                            }
                                        case "TG":
                                            {
                                                PhieuLuongThang_TG();
                                                break;
                                            }
                                        case "BT":
                                            {
                                                PhieuLuongThang_BT();
                                                break;
                                            }

                                        default:
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
                                                break;
                                            }
                                    }
                                }
                                break;
                            case "rdo_BangLuongThangTongHop":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangLuongThangTH_MT();
                                                break;
                                            }
                                        default:
                                            {

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
                                                    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                                                    ws.get_Range("C9", "C" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("D9", "D" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("E9", "E" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("F9", "F" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("G9", "G" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("H9", "H" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("I9", "I" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("J9", "J" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("K9", "K" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("L9", "L" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("M9", "M" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("N9", "N" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("O9", "O" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("S9", "S" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("U9", "U" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("W9", "W" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("X9", "X" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Y9", "Y" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("Z9", "Z" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AA9", "AA" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AB9", "AB" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AC9", "AC" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AD9", "AD" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AE9", "AE" + row).NumberFormat = "#,###.0;(#,###.0);;";
                                                    ws.get_Range("AF9", "AF" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AG9", "AG" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AH9", "AH" + row).NumberFormat = "#,##0;(#,##0);;";
                                                    ws.get_Range("AI9", "AI" + row).NumberFormat = "#,##0;(#,##0);;";

                                                    //ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                                    //ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    //ws.get_Range("D9", "D" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                                    //ws.get_Range("D9", "D" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                                    //ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                                    //ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
                                                break;
                                            }
                                    }
                                }
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private string CellAddress(Microsoft.Office.Interop.Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
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
        private void BorderAroundDam(Microsoft.Office.Interop.Excel.Range range)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders.Color = Color.Black;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;

            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;



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
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO, Commons.Modules.KyHieuDV == "TG" ? true : false);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO, Commons.Modules.KyHieuDV == "TG" ? true : false);
        }

        private void LoadThang()
        {
            try
            {
                string sTable = "BANG_LUONG_DM";
                switch (Commons.Modules.KyHieuDV)
                {
                    case "BT":
                        {
                            sTable = "BANG_LUONG_BT";
                            break;
                        }
                    case "TG":
                        {
                            sTable = "BANG_LUONG_TG";
                            break;
                        }
                    case "MT":
                        {
                            sTable = "BANG_LUONG_MT";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo." + sTable + " ORDER BY Y DESC , M DESC";
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
            if (Commons.Modules.KyHieuDV == "TG" || Commons.Modules.KyHieuDV == "MT") return;
            if (Commons.Modules.KyHieuDV == "DM")
            {
                cboCachTinhLuong.Visible = false;
                lblCachTinhLuong.Visible = false;

                switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                {
                    case "rdo_BangTienLuongChuyenATM":
                        rdo_ChucVu.Visible = true;
                        break;
                    default:
                        rdo_ChucVu.Visible = false;
                        break;
                }
            }
            else
            {
                switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                {
                    case "rdo_PhieuNhanLuong":
                        cboCachTinhLuong.Enabled = true;
                        break;
                    default:
                        cboCachTinhLuong.Enabled = false;
                        rdo_ChucVu.Visible = false;
                        break;
                }
            }

        }
        private void InPhieuNhanLuongCNSP(string MaSo)
        {
            System.Data.SqlClient.SqlConnection conn;
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
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dt1 = new DataTable();
            dt1 = ds.Tables[1].Copy();

            dt2 = new DataTable();
            dt2 = ds.Tables[2].Copy();

            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                ws.get_Range("I9", "I" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("J9", "J" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("K9", "N" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("O9", "O" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("P9", "P" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("Q9", "Q" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("R9", "R" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("S9", "S" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("T9", "T" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("U9", "U" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("V9", "V" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("W9", "W" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("X9", "X" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("Y9", "Y" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("Z9", "Z" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("AA9", "AA" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("AB9", "AB" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("AC9", "AC" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("AD9", "AD" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("AE9", "AT" + row).NumberFormat = "#,##0;(#,##0);;";
                ws.get_Range("AU9", "AU" + row).NumberFormat = "#,##0.0;(#,##0.0);;";
                ws.get_Range("AV9", "AY" + row).NumberFormat = "#,##0;(#,##0);;";

                ws.get_Range("A9", "B" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("A9", "B" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range("E9", "E" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("E9", "E" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range("H9", "H" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("H9", "H" + row).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

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
            if (dt1.Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dt2 = new DataTable();
            dt2 = ds.Tables[1].Copy();

            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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



                //ws.Application.ActiveWindow.SplitColumn = 7;
                //ws.Application.ActiveWindow.SplitRow = 9;
                //ws.Application.ActiveWindow.FreezePanes = true;

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
                                            row2["BHTNLD_CTY_TRA"].ToString(),row2["TONG_BH_CTY_TRA"].ToString(),row2["QUY_CONG_DOAN"].ToString(),row2["TL_CTY_TRA"].ToString(),row2["LUONG_T13"].ToString()};

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

                for (int colFormat = 10; colFormat <= 12; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "dd/MM/yyyy";
                }

                for (int colFormat = 32; colFormat < dt2.Columns.Count + 2; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0;(#,##0);;";
                }

                for (int colFormat = 14; colFormat < 28; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0.00;(#,##0.0);;";
                }

                for (int colFormat = 28; colFormat < 30; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0.0;(#,##0.0);;";
                }
                for (int colFormat = 30; colFormat < 38; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0.00;(#,##0.0);;";
                }
                ws.get_Range("BU10", "BU" + row).NumberFormat = "@";


                ws.get_Range("J10", "J" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("K10", "K" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("L10", "L" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                //rowLBTC.Merge();
                //rowLBTC.Value2 = "Tổng cộng (Total)";


                Microsoft.Office.Interop.Excel.Range myRange = ws.get_Range("A9", lastColumn + (row - 1).ToString());
                //Microsoft.Office.Interop.Excel.Range myRange = ws.get_Range("A9", lastColumn + "10");
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
        private void BangLuongThang_BT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                DataTable dtBCLSP;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                this.Cursor = Cursors.WaitCursor;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.ActiveSheet;


                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    oSheet.Name = rowC[1].ToString();

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_BT", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = rowC[0];
                    //cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), 0);
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), 1);
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[0].Copy();
                    int lastColumn = dtBCLSP.Columns.Count;

                    TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 24;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 50;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG THANH TOÁN LƯƠNG THÁNG " + cboThang.Text + "";
                    row2_TieuDe_BaoCao.WrapText = true;


                    int oCol = 1;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 25;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày vào làm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Số năm thâm niên";

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương CB phải trả cho người LĐ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[6, oCol + 3]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thời gian làm việc";

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công làm việc";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "TGLV trong giờ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "TG làm thêm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ chế độ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tổng lương sản phẩm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;

                    for (int i = oCol; i <= dtBCLSP.Columns.Count; i++)
                    {
                        if (dtBCLSP.Columns[i - 1].ColumnName.ToString() == "LSP_TRONG_GIO") break;
                        oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]].Merge();
                        oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]] = dtBCLSP.Columns[i - 1].Caption;
                        oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]].ColumnWidth = 15;
                        //oSheet.Cells[oRow, oCol].Wraptext = true;
                        oCol++;
                    }
                    int oColTemp = oCol; // colTemp dùng để biết được các mã hàng đã in ra được tới cột nào rồi, từ đó lập công thức cho các cột phía sau
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[6, oCol + 8]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Các khoản cộng lương";

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương SP trong giờ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương khác";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương chờ việc";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Bù lương";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương SP ngoài giờ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Số ngày lễ tết";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương chế độ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Số ngày phép";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;



                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Lương phép";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Phụ cấp khác";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Phụ cấp nuôi con nhỏ từ 1 đến dưới 6 tuổi";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Phụ cấp thâm niên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Phụ cấp đi lại";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[6, oCol + 1]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tiền thưởng";

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng ngày công";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng NSCL";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng đạt doanh thu";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thành tiền";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[6, oCol + 2]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Các khoản trừ";

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Các loại BH bắt buộc";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Trừ khác";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[7, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Tổng trừ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thực lãnh";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[6, oCol], oSheet.Cells[7, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ký nhận";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;



                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    row_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, lastColumn]];
                    row_TieuDe_Format.RowHeight = 60;


                    for (int i = 1; i <= dtBCLSP.Columns.Count; i++)
                    {
                        row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, i], oSheet.Cells[8, i]];
                        row2_TieuDeCot_BaoCao.Value2 = i;
                    }
                    row_TieuDe_Format = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, lastColumn]];
                    row_TieuDe_Format.Font.Italic = true;

                    oRow = 9;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dtBCLSP.Select();
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
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;

                    // set công thức cho cột thành tiền
                    Microsoft.Office.Interop.Excel.Range row_TinhThanhTien = oSheet.Range[oSheet.Cells[9, dtBCLSP.Columns["THANH_TIEN"].Ordinal + 1], oSheet.Cells[9, dtBCLSP.Columns["THANH_TIEN"].Ordinal + 1]]; // hiển thị công thức 
                    row_TinhThanhTien.Value2 = "=SUM(" + CharacterIncrement(dtBCLSP.Columns["LSP_TRONG_GIO"].Ordinal) + "9:" + CharacterIncrement(dtBCLSP.Columns["THUONG_DAT_DT"].Ordinal) + "9) - (" + CharacterIncrement(dtBCLSP.Columns["SO_NGAY_LE_TET"].Ordinal) + "9 + " + CharacterIncrement(dtBCLSP.Columns["NGAY_PHEP"].Ordinal) + "9) ";

                    Microsoft.Office.Interop.Excel.Range row_TinhThanhTienTemp = oSheet.Range[oSheet.Cells[9, dtBCLSP.Columns["THANH_TIEN"].Ordinal + 1], oSheet.Cells[oRow, dtBCLSP.Columns["THANH_TIEN"].Ordinal + 1]]; // hiển thị công thức 

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        row_TinhThanhTien.AutoFill(row_TinhThanhTienTemp, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    // SET CÔNG THỨC CHO CỘT THỰC LÃNH

                    Microsoft.Office.Interop.Excel.Range row_TinhThucLanh = oSheet.Range[oSheet.Cells[9, dtBCLSP.Columns["THUC_LANH"].Ordinal + 1], oSheet.Cells[9, dtBCLSP.Columns["THUC_LANH"].Ordinal + 1]]; // hiển thị công thức 
                    row_TinhThucLanh.Value2 = "=ROUND(" + CharacterIncrement(dtBCLSP.Columns["THANH_TIEN"].Ordinal) + "9-" + CharacterIncrement(dtBCLSP.Columns["TONG_TRU"].Ordinal) + "9,-3)";

                    Microsoft.Office.Interop.Excel.Range row_TinhThucLanhTemp = oSheet.Range[oSheet.Cells[9, dtBCLSP.Columns["THUC_LANH"].Ordinal + 1], oSheet.Cells[oRow, dtBCLSP.Columns["THUC_LANH"].Ordinal + 1]]; // hiển thị công thức 

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        row_TinhThucLanh.AutoFill(row_TinhThucLanhTemp, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }


                    // TÍNH TỔNG
                    oRow++;
                    Microsoft.Office.Interop.Excel.Range row_TinhTong = oSheet.Range[oSheet.Cells[oRow, 3], oSheet.Cells[oRow, 3]];
                    row_TinhTong.Value2 = "Tổng cộng";
                    row_TinhTong.Font.Name = fontName;
                    row_TinhTong.Font.Size = 12;
                    row_TinhTong.WrapText = true;
                    row_TinhTong.Font.Bold = true;
                    row_TinhTong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TinhTong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    Microsoft.Office.Interop.Excel.Range formatRange1;
                    formatRange1 = oSheet.Range[oSheet.Cells[oRow, 6], oSheet.Cells[oRow, 6]];
                    formatRange1.Value2 = "=SUM(F9:F" + (oRow - 1).ToString() + ")";

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 6], oSheet.Cells[oRow, lastColumn - 1]];
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;

                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 4], oSheet.Cells[oRow, 4]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                    for (int colFormat = 11; colFormat <= dtBCLSP.Columns.Count - 1; colFormat++) // format từ cột t
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, colFormat], oSheet.Cells[oRow, colFormat]];
                        formatRange.NumberFormat = "#,##0;(#,##0);;";
                        try
                        {
                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                        }
                        catch { }

                    }

                    for (int i = 5; i <= 10; i++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, i], oSheet.Cells[oRow, i]]; //format từ cột 5 -> 10
                        formatRange.NumberFormat = "0.00;-0;;@";
                        try
                        {
                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                        }
                        catch { }
                    }

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 6], oSheet.Cells[oRow, 6]]; //format cột lương cơ bản
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                    BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[oRow, lastColumn]]);

                    // GET SỐ TIỀN MUỐN ĐỔI
                    formatRange = oSheet.Range[oSheet.Cells[oRow, lastColumn - 1], oSheet.Cells[oRow, lastColumn - 1]];

                    oRow = oRow + 2;


                    string sSQL = "SELECT dbo.DoiTienSoThanhChuTiengViet(" + formatRange.Value + ",'VND')";
                    string sSoTienChu = "0";
                    try
                    {
                        sSoTienChu = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                    }
                    catch { }
                    formatRange = oSheet.Range[oSheet.Cells[oRow, 2], oSheet.Cells[oRow, 2]]; // tổng tiền bằng chữ
                    formatRange.Value2 = "Tổng cộng (Bằng chữ):" + sSoTienChu;
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;

                    oRow++;
                    oRow++;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 2], oSheet.Cells[oRow, 2]]; // 
                    formatRange.Value2 = "Tổng giám đốc";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 6], oSheet.Cells[oRow, 6]]; // 
                    formatRange.Value2 = "Kế toán trưởng";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 20], oSheet.Cells[oRow, 20]]; // 
                    formatRange.Value2 = "Trưởng P.TCHC";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 28], oSheet.Cells[oRow, 28]]; // 
                    formatRange.Value2 = "LĐTL";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = oRow + 4;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 2], oSheet.Cells[oRow, 2]]; // 
                    formatRange.Value2 = "Lê Thanh Hoàng";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 6], oSheet.Cells[oRow, 6]]; // 
                    formatRange.Value2 = "Thái Kim Oanh";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 20], oSheet.Cells[oRow, 20]]; // 
                    formatRange.Value2 = "Phùng Thị Xuân Hương";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 28], oSheet.Cells[oRow, 28]]; // 
                    formatRange.Value2 = "Phan Tú Nhi";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    oRow = 1;
                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                this.Cursor = Cursors.Default;
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void PhieuLuongThang_BT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                DataTable dtBCLSP;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                this.Cursor = Cursors.WaitCursor;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.ActiveSheet;


                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int STT = 1;
                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    oSheet.Name = rowC[1].ToString();

                    System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_BT", conn);
                    cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmdCT.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = rowC[0];
                    //cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                    cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), 0);
                    cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), 1);
                    cmdCT.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adpCT = new System.Data.SqlClient.SqlDataAdapter(cmdCT);

                    DataSet dsCT = new DataSet();
                    adpCT.Fill(dsCT);
                    dtBCLSP = new DataTable();
                    dtBCLSP = dsCT.Tables[1].Copy();

                    int colCotSoTT = 1; // cột số tiếp theo
                    int colCotTenTT = 2; // cột tên tiếp theo
                    int colCotDau2ChamTT = 3; // cột dấu 2 chấm tiếp theo
                    int colSoTien = 4; // cột dấu 2 chấm tiếp theo

                    int iSLDaIn = 0; // số công nhân đã in , cứ 4 công nhân thì xuống 1 hàng
                    int RowXuongHang = 1; // row dùng để khi in xong 4 phiếu lương thì phải + 28 để xuống row dưới 
                    int oRow = 1; // Row bắt đầu
                    foreach (DataRow rowPL in dtBCLSP.Rows)
                    {


                        Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colSoTien]]; // dòng tiêu đề
                        row2_TieuDe_BaoCao.Merge();
                        row2_TieuDe_BaoCao.Font.Size = 8;
                        row2_TieuDe_BaoCao.Font.Name = fontName;
                        row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row2_TieuDe_BaoCao.RowHeight = 15;
                        row2_TieuDe_BaoCao.Value2 = rowC[1].ToString() + "-Lương T" + cboThang.Text;

                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 1;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;


                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Số thứ tự";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";


                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = STT;


                        // họ tên
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 2;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = rowPL["HO_TEN"];
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        //Lương CB phải trả cho NLĐ

                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 3;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương CB phải trả cho NLĐ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LUONG_CO_BAN"];

                        //Ngày công trong giờ

                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 4;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Ngày công trong giờ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["NGAY_CONG"];

                        //Tổng lương SP

                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 5;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Tổng lương SP";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["TONG_LSP"];

                        //Lương SP trong giờ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 6;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương sp trong giờ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LSP_TRONG_GIO"];

                        //Lương khác
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 7;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương khác";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LUONG_KHAC"];

                        //Lương chờ việc
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 8;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương chờ việc";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LUONG_CHO_VIEC"];

                        //Bù lương
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 9;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Bù lương";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["BU_LUONG"];


                        //Lương sp ngoài giờ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 10;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương sp ngoài giờ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LSP_NGOAI_GIO"];

                        //Lương sp ngoai giờ vào ban đêm
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 11;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương sp ngoai giờ vào ban đêm";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LSP_LAM_THEM_DEM"];

                        //Lương sp CN
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 12;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương sp CN";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = "";

                        //Lương chế độ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 13;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương chế độ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LUONG_CHE_DO"];

                        //Lương phép
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 14;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Lương phép";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["LUONG_PHEP"];

                        //Phụ cấp khác
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 15;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Phụ cấp khác";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["PC_KHAC"];

                        //Phụ cấp đi lại
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 16;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Phụ cấp đi lại";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["PC_DI_LAI"];

                        //Phụ cấp nuôi con nhỏ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 17;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Phụ cấp nuôi con nhỏ";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["PC_CON_NHO"];

                        //Phụ thâm niên
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 18;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Phụ thâm niên";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["PC_THAM_NIEN"];

                        //Thưởng ngày công
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 19;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Thưởng ngày công";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["THUONG_NGAY_CONG"];

                        //Tiền thưởng NSCL
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 20;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Tiền thưởng NSCL";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["THUONG_CHI_TIEU_CL"];

                        //Tiền thưởng NCC
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 21;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Tiền thưởng NCC";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["THUONG_DAT_DT"];

                        //Tổng cộng
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Tổng cộng";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["THANH_TIEN"];
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        //Các khoản trừ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = "*";
                        row2_TieuDe_BaoCao.Font.Bold = true;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Các khoản trừ";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        //BHXH+BHYT+BHTN
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 1;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "BHXH+BHYT+BHTN";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["TRU_BHXH"];

                        //Truy thu
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]]; //  cột số thứ tự
                        row2_TieuDe_BaoCao.Value2 = 2;
                        row2_TieuDe_BaoCao.RowHeight = 10.50;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Truy thu";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["TRU_KHAC"];

                        //Tổng trừ
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Tổng trừ";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["TONG_TRU"];
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        //Thực lãnh
                        oRow++;
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]]; //  cột tên 
                        row2_TieuDe_BaoCao.Value2 = "Thực lãnh";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]]; //  cột dấu 2 chấm
                        row2_TieuDe_BaoCao.Value2 = ":";
                        row2_TieuDe_BaoCao.Font.Bold = true;

                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, colSoTien], oSheet.Cells[oRow, colSoTien]]; //  cột tiền lương
                        row2_TieuDe_BaoCao.Value2 = rowPL["THUC_LANH"];
                        row2_TieuDe_BaoCao.Font.Bold = true;


                        // format cột số thứ tự RowXuongHang = 1 + 1 để format từ dòng số thứ tự
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowXuongHang + 1, colCotSoTT], oSheet.Cells[oRow, colCotSoTT]];
                        row2_TieuDe_BaoCao.Font.Size = 8;
                        row2_TieuDe_BaoCao.ColumnWidth = 3;
                        row2_TieuDe_BaoCao.Font.Name = fontName;
                        row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        // format cột tên RowXuongHang = 1 + 1 để format từ dòng số thứ tự
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowXuongHang + 1, colCotTenTT], oSheet.Cells[oRow, colCotTenTT]];
                        row2_TieuDe_BaoCao.Font.Size = 8;
                        row2_TieuDe_BaoCao.ColumnWidth = 22;
                        row2_TieuDe_BaoCao.Font.Name = fontName;

                        // format cột dấu 2 chấm RowXuongHang = 1 + 1 để format từ dòng số thứ tự
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowXuongHang + 1, colCotDau2ChamTT], oSheet.Cells[oRow, colCotDau2ChamTT]];
                        row2_TieuDe_BaoCao.Font.Size = 8;
                        row2_TieuDe_BaoCao.ColumnWidth = 1;
                        row2_TieuDe_BaoCao.Font.Name = fontName;
                        row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        // format cột dấu 2 chấm RowXuongHang = 1 + 1 để format từ dòng số thứ tự
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowXuongHang + 1, colSoTien], oSheet.Cells[oRow, colSoTien]];
                        row2_TieuDe_BaoCao.Font.Size = 8;
                        row2_TieuDe_BaoCao.ColumnWidth = 10;
                        row2_TieuDe_BaoCao.Font.Name = fontName;
                        row2_TieuDe_BaoCao.NumberFormat = "#,##0;(#,##0);;";


                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowXuongHang, colCotSoTT], oSheet.Cells[oRow, colSoTien]];
                        row2_TieuDe_BaoCao.BorderAround();

                        // set cột bên phải cột số tiền columnWidth = 0.5 colSoTien + 1
                        row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[1, colSoTien + 1], oSheet.Cells[1, colSoTien + 1]];
                        row2_TieuDe_BaoCao.ColumnWidth = 0.5;

                        // in xong 1 phiếu lương cho công nhân
                        iSLDaIn++;

                        // in bảng tiếp theo sang col bên phải 4 col
                        if (iSLDaIn == 4)
                        {
                            colCotSoTT = 1;
                            colCotTenTT = 2;
                            colCotDau2ChamTT = 3;
                            colSoTien = 4;
                            iSLDaIn = 0;
                            RowXuongHang = oRow + 1;
                        }
                        else
                        {
                            colCotSoTT = colCotSoTT + 5;
                            colCotTenTT = colCotTenTT + 5;
                            colCotDau2ChamTT = colCotDau2ChamTT + 5;
                            colSoTien = colSoTien + 5;
                        }
                        oRow = RowXuongHang;

                        STT++;
                    }

                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                this.Cursor = Cursors.Default;
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.HideWaitForm();

            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangLuongThangNV_DM()
        {
            this.Cursor = Cursors.WaitCursor;
            string sThang = cboThang.EditValue.ToString();

            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dt1;
                DataTable dt2;
                int NgayCuoiThang = DateTime.DaysInMonth(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangNV_DM", conn);
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
                if (dt1.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dt2 = new DataTable();
                dt2 = ds.Tables[1].Copy();


                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                row3_TieuDe.Value = "BẢNG LƯƠNG NHÂN VIÊN THÁNG " + sThang + "";
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

                foreach (DataRow rowTitle in dt1.Rows)
                {
                    col++;
                    ws.Cells[row, col].ColumnWidth = Convert.ToInt32(rowTitle["CHIEU_RONG_COT"]);
                    ws.Cells[row, col] = rowTitle["TIENG_VIET"].ToString();
                    ws.Cells[row + 1, col] = col;
                }


                //ws.Application.ActiveWindow.SplitColumn = 7;
                //ws.Application.ActiveWindow.SplitRow = 9;
                //ws.Application.ActiveWindow.FreezePanes = true;

                ws.get_Range("A8", "" + lastColumn + "8").Font.Color = XlRgbColor.rgbBlue;
                ws.get_Range("A9", "" + lastColumn + "9").Font.Color = XlRgbColor.rgbRed;

                BorderAround(ws.get_Range("A8", "" + lastColumn + "9"));
                row = 9;

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
                    dynamic[] arr = { stt, row2["HO_TEN"].ToString(), row2["MS_CN"].ToString(), row2["BO_PHAN"].ToString(), row2["TEN_XN"].ToString(),
                                            row2["TEN_DV"].ToString(), row2["PHAN_BO"].ToString(), row2["TEN_LCV"].ToString(), row2["TEN_TT_HT"].ToString(), row2["NGAY_VAO_LAM"].ToString(),
                                            row2["NGAY_HD"].ToString(), row2["TRUONG_BP"].ToString(), row2["LUONG_HDLD"].ToString(),row2["HTL_TRUOC_NGAY"].ToString(),row2["HTL_TU_NGAY"].ToString(),
                                            row2["TONG_LCB_HTL"].ToString(), row2["NGAY_CONG"].ToString(), row2["NGAY_PHEP_TRONG_THANG"].ToString(), row2["GIO_CD"].ToString(), row2["NGAY_NLVR_HL"].ToString(),
                                            row2["NGHI_KL"].ToString(),row2["SN_TC_2H"].ToString(),row2["LUONG_NGAY_LVTT"].ToString(),row2["LUONG_NLVR_HL"].ToString(),row2["LUONG_PHEP_NAM"].ToString(),
                                            row2["LUONG_CD"].ToString(),row2["TONG_LUONG_TG_HC"].ToString(),row2["TONG_GIO_TC"].ToString(),row2["GIO_LAM_THEM_150"].ToString(),row2["GIO_LAM_THEM_200"].ToString(),
                                            row2["GIO_LAM_THEM_300"].ToString(),row2["LUONG_LAM_THEM"].ToString(),row2["ATVSV"].ToString(),row2["HO_TRO_AN"].ToString(),row2["PC_CON_NHO"].ToString(),
                                            row2["THUONG_CN_MOI"].ToString(),row2["THUONG_HQ_NV"].ToString(),row2["PC_QUA_DO"].ToString(),row2["THANH_TIEN_HTL_TRUOC_NGAY"].ToString(),row2["THANH_TIEN_HTL_TU_NGAY"].ToString(),
                                            row2["THUONG_HQ_QUAN_LY"].ToString(),row2["PC_KHAC"].ToString(),row2["TONG_PHU_CAP"].ToString(),row2["TL_TRUOC_GIAM_TRU"].ToString(),row2["TIEN_BHXH"].ToString(),
                                            row2["TIEN_BHYT"].ToString(),row2["TIEN_BHTN"].ToString(),row2["TONG_TIEN_BHXH"].ToString(),row2["TN_CHIU_THUE"].ToString(),row2["SO_NGUOI_GIAM_TRU"].ToString(),

                                            row2["TIEN_LUONG_GIAM_TRU"].ToString(),row2["THUE_GIAM_TRU_TC"].ToString(),row2["THU_NHAP_TINH_THUE"].ToString(),row2["THUE_TNCN"].ToString(),row2["PHI_CONG_DOAN"].ToString(),
                                            row2["THU_BHYT"].ToString(),row2["TRU_KHAC"].ToString(),row2["TONG_GIAM_TRU"].ToString(),row2["THU_NHAP_TRUOC_GT"].ToString(),row2["TL_THUC_NHAN"].ToString(),
                                            row2["TK_NGAN_HANG"].ToString(),row2["BHXH_CTY_TRA"].ToString(),row2["BHYT_CTY_TRA"].ToString(),row2["BHTN_CTY_TRA"].ToString(),row2["BHTNLD_CTY_TRA"].ToString(),
                                            row2["TONG_BH_CTY_TRA"].ToString(),row2["QUY_CONG_DOAN"].ToString(),row2["TL_CTY_TRA"].ToString(),row2["LUONG_THANG_13"].ToString(),row2["SN_TINH_LUONG"].ToString(),
                                            row2["LCB"].ToString(),row2["PHEP_CON_LAI"].ToString()};
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

                for (int colFormat = 10; colFormat <= 11; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "dd/MM/yyyy";
                }

                for (int colFormat = 13; colFormat < dt2.Columns.Count + 2; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0;(#,##0);;";
                }

                for (int colFormat = 17; colFormat <= 22; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0.00;(#,##0.0);;";
                }

                for (int colFormat = 28; colFormat <= 31; colFormat++)
                {
                    ws.get_Range(CellAddress(ws, 10, colFormat), CellAddress(ws, row, colFormat)).NumberFormat = "#,##0.0;(#,##0.0);;";
                }

                // format cột việt tinnbank
                ws.get_Range("BI10", "BI" + row).NumberFormat = "@";

                ws.get_Range("J10", "J" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("K10", "K" + row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //Range rowLBTC = ws.get_Range("A" + row, "H" + row);
                //rowLBTC.Merge();
                //rowLBTC.Value2 = "Tổng cộng (Total)";


                Microsoft.Office.Interop.Excel.Range myRange = ws.get_Range("A9", lastColumn + (row - 1).ToString());
                //Microsoft.Office.Interop.Excel.Range myRange = ws.get_Range("A9", lastColumn + "10");
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

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongThangDM", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0].Copy();
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }
        private void BangLuongATM_MT()
        {
            string sThang = cboThang.EditValue.ToString();
            DateTime dNgayIn = Convert.ToDateTime(lk_NgayIn.EditValue.ToString());

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dt;
            DataTable dt1;
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

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

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
                    row_ND3.Value2 = rowdt["TEN_TINH"].ToString() + ", Ngày " + dNgayIn.Day + " Tháng " + dNgayIn.Month + " Năm " + dNgayIn.Year;

                    Range row_ND4 = ws.get_Range("A6", "H6");
                    row_ND4.Merge();
                    row_ND4.Font.Size = fontSizeTieuDe;
                    row_ND4.Font.Name = fontName;
                    row_ND4.Font.Bold = true;
                    row_ND4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_ND4.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_ND4.Value2 = "Kính gửi: NGÂN HÀNG " + Convert.ToString(rowdt["TEN_NGAN_HANG"]) + ".";

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
                    row_ND8.Value2 = "số: 02/HĐ-KHCN-2011 ngày 06 tháng 05 năm 2011 giữa Ngân hàng " + Convert.ToString(rowdt["TEN_NGAN_HANG"]) + " và" + rowdt["TEN_DV"].ToString();

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
                        rowDataFNum.NumberFormat = "#,##0;(#,##0);;";

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
                    ws.Cells[row, 5].NumberFormat = "#,##0;(#,##0);;";
                    ws.Cells[row, 7] = "=SUM(" + CellAddress(ws, 16, 7) + ":" + CellAddress(ws, row - 1, 7) + ")";
                    ws.Cells[row, 7].NumberFormat = "#,##0;(#,##0);;";

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


        #region Báo cáo TG
        private void BangLuongThang_TG_ReportView()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBangLuongThang_TG(cboThang.Text, labelControl1.Text + " : " + LK_DON_VI.Text, lbXiNghiep.Text + " : " + LK_XI_NGHIEP.Text, lbTo.Text + " : " + LK_TO.Text, lk_NgayIn.DateTime);
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_TG", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
            cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0].Copy();
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }


        private void BangLuongThang_TG()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                DataTable dtBCLSP;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_TG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                dtBCLSP = new DataTable();
                adp.Fill(dtBCLSP);
                if (dtBCLSP.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                this.Cursor = Cursors.WaitCursor;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.ActiveSheet;


                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 13;
                int oRow = 1;

                foreach (DataRow rowC in dtChuyen.Rows)
                {
                    oSheet.Name = rowC[1].ToString();

                    cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_TG", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = rowC[0];
                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                    cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;
                    cmd.CommandType = CommandType.StoredProcedure;
                    adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    dtBCLSP = new DataTable();
                    adp.Fill(dtBCLSP);
                    if (dtBCLSP.Rows.Count == 0)
                        continue;
                    int lastColumn = dtBCLSP.Columns.Count;
                    TaoTTChung(oSheet, 1, 3, 1, 10, 0, 0);

                    Range header = oSheet.Range[oSheet.Cells[1, 2], oSheet.Cells[1, 10]];
                    header.Font.Size = 16;
                    header.Font.Name = fontName;
                    header.Font.Bold = true;

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 37;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 50;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG LƯƠNG THÁNG - " + cboThang.Text + "";
                    row2_TieuDe_BaoCao.WrapText = true;





                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 3]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 13;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.Value2 = lbTo.Text + ": " + rowC[1].ToString();
                    row2_TieuDe_BaoCao.WrapText = true;


                    int oCol = 1;
                    oRow = 7;

                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 5.5;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 26;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol + 3]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công";


                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "ĐL";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "CN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "VM";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "TD";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương HĐLĐ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 13.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương ngày CN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 9;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "GLT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tiền thêm giờ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 13.82;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng hiệu suất";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 13.82;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng c.hành";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 13.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol + 1]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương phép";


                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;
                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Tiền";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.09;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol + 1]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương lễ";

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.18;
                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Tiền";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.09;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng HTNV";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 12.82;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Trợ cấp CN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 12.82;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tiền xăng";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 12.82;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thưởng";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 12.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Bù NCCB";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 9.18;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Lương cả tháng";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15.09;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Khấu trừ tạm ứng";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "BHXH + BHTN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Nộp BHYT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Khấu trừ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thuế TNCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.82;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Thực lãnh";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 14.55;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ký nhận";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15.09;


                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow + 1, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 13;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row_TieuDe_Format.RowHeight = 27;

                    oRow = oRow + 2;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dtBCLSP.Select();
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
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Bold = true;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].RowHeight = 24;

                    // TÍNH TỔNG
                    oRow++;
                    Microsoft.Office.Interop.Excel.Range row_TinhTong = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 3]];
                    row_TinhTong.Merge();
                    row_TinhTong.Value2 = "Tổng cộng";
                    row_TinhTong.Font.Name = fontName;
                    row_TinhTong.RowHeight = 32;
                    row_TinhTong.Font.Size = 13;
                    row_TinhTong.WrapText = true;
                    row_TinhTong.Font.Bold = true;
                    row_TinhTong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TinhTong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    Microsoft.Office.Interop.Excel.Range formatRange1;
                    formatRange1 = oSheet.Range[oSheet.Cells[oRow, 4], oSheet.Cells[oRow, 4]];
                    formatRange1.Value2 = "=SUM(D9:D" + (oRow - 1).ToString() + ")";

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 4], oSheet.Cells[oRow, lastColumn - 1]];
                    formatRange.Font.Size = 13;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    //formatRange.EntireColumn.AutoFit()
                    if (dtBCLSP.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    for (int colFormat = 4; colFormat < dtBCLSP.Columns.Count; colFormat++) // format từ cột t
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, colFormat], oSheet.Cells[oRow, colFormat]];
                        switch (colFormat)
                        {
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 10:
                            case 14:
                            case 16:
                                {
                                    formatRange.NumberFormat = "0.0;-0;;@";
                                    break;
                                }
                            default:
                                {
                                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                                    break;
                                }
                        }
                        try
                        {
                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                        }
                        catch { }

                    }

                    BorderAroundDam(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[oRow, lastColumn]]);

                    oRow = oRow + 2;
                    formatRange = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 3]]; // 
                    formatRange.Merge();
                    formatRange.Value2 = "Ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString();
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = oRow + 1;
                    formatRange = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 3]]; // 
                    formatRange.Merge(); // 
                    formatRange.Value2 = "Lập bảng";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    if (Convert.ToInt32(LK_DON_VI.EditValue) == 2)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[oRow, 6], oSheet.Cells[oRow, 6]]; // 
                        formatRange.Value2 = "GĐNM CL";
                        formatRange.Font.Size = fontSizeNoiDung;
                        formatRange.Font.Name = fontName;
                        formatRange.Font.Bold = true;
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 12], oSheet.Cells[oRow, 12]]; // 
                    formatRange.Value2 = Convert.ToInt32(LK_DON_VI.EditValue) == 2 ? "GĐĐH Khu CL" : "GIÁM ĐỐC NM";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    formatRange = oSheet.Range[oSheet.Cells[oRow, 20], oSheet.Cells[oRow, 20]]; // 
                    formatRange.Value2 = "P.KT-NS";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 28], oSheet.Cells[oRow, 28]]; // 
                    formatRange.Value2 = "TỔNG GIÁM ĐỐC";
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.PageSetup.FitToPagesWide = 1;
                    oSheet.PageSetup.FitToPagesTall = false;
                    oSheet.PageSetup.Zoom = false;
                    oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                    oSheet.PageSetup.LeftMargin = oApp.InchesToPoints(0.25);
                    oSheet.PageSetup.RightMargin = oApp.InchesToPoints(0.25);
                    oSheet.PageSetup.TopMargin = oApp.InchesToPoints(0.25);
                    oSheet.PageSetup.BottomMargin = oApp.InchesToPoints(0.25);
                    oSheet.PageSetup.HeaderMargin = oApp.InchesToPoints(0.3);
                    oSheet.PageSetup.FooterMargin = oApp.InchesToPoints(0.3);
                    oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                    oRow = 1;
                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }
                this.Cursor = Cursors.Default;
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        #region báo cáo mỹ tho
        private void BangLuongThang_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangCN.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4", "AY5" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangLuongThangCBQLC_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 2;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangCBCHUYEN.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4", "AS5" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangLuongThangQCCHUYEN_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 3;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangQCCHUYEN.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4", "AU5" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangLuongThangToTruong_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 4;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangCAT.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4", "AT5" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BangLuongThangTG_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 5;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";

                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangTG.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4", "AT5" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BangLuongThangTH_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 6;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";
                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangTongHop.xlsx", ds, new string[] { "{", "}" }, new string[] { "A3", "A4"});
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }


        private void BangLuongThangAMT_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtChuyen;
                dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + LK_DON_VI.EditValue + " OR " + LK_DON_VI.EditValue + " = -1) AND (ID_XN = " + LK_XI_NGHIEP.EditValue + " OR " + LK_XI_NGHIEP.EditValue + " = -1) AND (ID_TO = " + LK_TO.EditValue + " OR " + LK_TO.EditValue + " = -1) ORDER BY STT_DV, STT_XN, STT_TO"));
                if (dtChuyen.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ///kiểm tra dữ liệu
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThang_MT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 7;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "Data";
                ds.Tables[1].TableName = "Info";
                if (ds.Tables[0].Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReportSum(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateMT\\PhieuLuongThangATM.xlsx", ds, new string[] { "{", "}" }, new string[] {"A4","A21" });
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }



        #endregion

        private void BangLuongThangATM_TG()
        {

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangChuyenATM_TG", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
            cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].TableName = "DATA1";
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmViewReport frm = new frmViewReport(Convert.ToInt32(LK_DON_VI.EditValue));
            frm.rpt = new rptLuongChuyenKhoan_TG(lk_NgayIn.DateTime, ds.Tables[1].Rows[0]["TINH_TP"].ToString());
            ds.Tables[1].TableName = "DATA";
            frm.AddDataSource(ds);
            frm.ShowDialog();
        }


        private void BangLuongThangTienMat_TG()
        {

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangChuyenTienMat_TG", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
            cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].TableName = "DATA";

            frmViewReport frm = new frmViewReport(Convert.ToInt32(LK_DON_VI.EditValue));
            frm.rpt = new rptLuongTienMat_TG(cboThang.Text, labelControl1.Text + " : " + LK_DON_VI.Text, Convert.ToInt32(LK_DON_VI.EditValue), lbTo.Text + " : " + LK_TO.Text, lk_NgayIn.DateTime);

            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frm.AddDataSource(ds);
            frm.ShowDialog();
        }

        private void BangLuongThangHoTro_TG()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangHoTro_TG", conn);
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
            ds.Tables[0].TableName = "DATA";

            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBangLuongThangHoTro_TG(cboThang.Text, lk_NgayIn.DateTime);
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frm.AddDataSource(ds);
            frm.ShowDialog();
        }

        private void BangLuongThangVanPhong_TG()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBangLuongThangVanPhong_TG(cboThang.Text, labelControl1.Text + " : " + LK_DON_VI.Text, lbXiNghiep.Text + " : " + LK_XI_NGHIEP.Text, lbTo.Text + " : " + LK_TO.Text, lk_NgayIn.DateTime);
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangVanPhong_TG", conn);
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
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }

        private void PhieuLuongThang_TG()
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptPhieuLuongThang_TG(cboThang.Text);
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuLuongThang_TG", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@TinhTrang", SqlDbType.Int).Value = rdo_ChucVu.SelectedIndex;
            cmd.Parameters.Add("@ChinhThuc", SqlDbType.Int).Value = rdoChinhThuc.SelectedIndex;




            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ds.Tables[0].TableName = "DATA";
            ds.Tables.Add(Commons.Modules.ObjSystems.DataThongTinChung());
            frm.AddDataSource(ds);
            frm.ShowDialog();
        }


        #endregion

        private int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataReportHeader(Convert.ToInt32(LK_DON_VI.EditValue));
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
        private void GetImage(byte[] Logo, string sPath, string sFile)
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


    }
}
