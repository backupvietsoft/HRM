using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Microsoft.ApplicationBlocks.Data;
using System.Reflection;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class frmInBHXH : DevExpress.XtraEditors.XtraForm
    {
        private string SaveExcelFile;
        private DateTime ThangBC = new DateTime(DateTime.Now.Year, 1, 1);
        private Int32 DotBC = 1;
        public frmInBHXH(DateTime Thang, Int32 Dot)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

            ThangBC = Thang;
            DotBC = Dot;
        }

        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                System.Data.SqlClient.SqlConnection conn;
                                DataTable dt = new DataTable();
                                try
                                {

                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongTangBHXH", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    ds.Tables[0].TableName = "TangLaoDong";
                                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                                    saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                    saveFileDialog.FilterIndex = 0;
                                    saveFileDialog.RestoreDirectory = true;
                                    //saveFileDialog.CreatePrompt = true;
                                    saveFileDialog.CheckFileExists = false;
                                    saveFileDialog.CheckPathExists = false;
                                    saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                    saveFileDialog.Title = "Export Excel File To";
                                    DialogResult res = saveFileDialog.ShowDialog();
                                    // If the file name is not an empty string open it for saving.
                                    if (res == DialogResult.OK)
                                    {
                                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\lib\\Template\\TemplateTangLaoDong.xlsx", ds, new string[] { "{", "}" });
                                        Process.Start(saveFileDialog.FileName);
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case 1:
                                try
                                {
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongGiamBHXH", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    ds.Tables[0].TableName = "GiamLaoDong";
                                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                                    saveFileDialog.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx";
                                    saveFileDialog.FilterIndex = 0;
                                    saveFileDialog.RestoreDirectory = true;
                                    saveFileDialog.CreatePrompt = true;
                                    saveFileDialog.Title = "Export Excel File To";
                                    // If the file name is not an empty string open it for saving.
                                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        if (saveFileDialog.FileName != "")
                                        {
                                            Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\lib\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                            //Commons.TemplateExcel.FillReport(saveFileDialog.FileName, Application.StartupPath + "\\lib\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                            Process.Start(saveFileDialog.FileName);
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case 2:
                                InDanhSachThamGiaBH();
                                break;
                            default:
                                break;
                        }

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

        private void InDanhSachThamGiaBH()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBHXH;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachThamGiaBH_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime("01/03/2021");
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime("31/03/2021");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBHXH = new DataTable();
                dtBHXH = ds.Tables[0].Copy();

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

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeNoiDung = 9;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBHXH.Columns.Count - 3);

                Range row1_TieuDe = oSheet.get_Range("A1", "J1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = dtBHXH.Rows[0]["TEN_DV"];


                Range row2_TieuDe = oSheet.get_Range("A2", "J2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = dtBHXH.Rows[0]["DIA_CHI"];

                Range row3_TieuDe = oSheet.get_Range("A3", "J3");
                row3_TieuDe.Merge();
                row3_TieuDe.Font.Bold = true;
                row3_TieuDe.Value2 = "MÃ KCB:00028";

                Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", lastColumn + "5");
                row5_TieuDe_BaoCao.Merge();
                row5_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;
                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_BaoCao.RowHeight = 30;
                row5_TieuDe_BaoCao.Value2 = "DANH SÁCH THAM GIA BHXH, BHYT, BHTN";

                Range row6_Ngay_BaoCao = oSheet.get_Range("A6", lastColumn + "6");
                row6_Ngay_BaoCao.Merge();
                row6_Ngay_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row6_Ngay_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row6_Ngay_BaoCao.Font.Bold = true;
                row6_Ngay_BaoCao.Value2 = "Tháng 5 năm 2019";

                Range row5_TieuDe_Format = oSheet.get_Range("A8", lastColumn + "10"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                //row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row5_TieuDe_Stt = oSheet.get_Range("A8", "A10");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 5;

                Range row8_TieuDe_HoTen = oSheet.get_Range("B8", "B10");
                row8_TieuDe_HoTen.Merge();
                row8_TieuDe_HoTen.Value2 = "Họ và tên";
                row8_TieuDe_HoTen.ColumnWidth = 25;

                Range row8_TieuDe_DiaChi = oSheet.get_Range("C8", "C10");
                row8_TieuDe_DiaChi.Merge();
                row8_TieuDe_DiaChi.Value2 = "Địa chỉ";
                row8_TieuDe_DiaChi.ColumnWidth = 50;

                Range row8_TieuDe_SoBHXH = oSheet.get_Range("D8", "D10");
                row8_TieuDe_SoBHXH.Merge();
                row8_TieuDe_SoBHXH.Value2 = "Số sổ BHXH";
                row8_TieuDe_SoBHXH.ColumnWidth = 15;

                Range row8_TieuDe_SoBHYT = oSheet.get_Range("E8", "E10");
                row8_TieuDe_SoBHYT.Merge();
                row8_TieuDe_SoBHYT.Value2 = "Số thẻ BHYT";
                row8_TieuDe_SoBHYT.ColumnWidth = 15;

                Range row8_TieuDe_NgaySinh = oSheet.get_Range("F8", "F10");
                row8_TieuDe_NgaySinh.Merge();
                row8_TieuDe_NgaySinh.Value2 = "Ngày sinh";
                row8_TieuDe_NgaySinh.ColumnWidth = 18;

                Range row8_TieuDe_GioiTinh = oSheet.get_Range("G8", "G10");
                row8_TieuDe_GioiTinh.Merge();
                row8_TieuDe_GioiTinh.Value2 = "Giới tính";
                row8_TieuDe_GioiTinh.ColumnWidth = 12;

                Range row8_TieuDe_NoiKCB = oSheet.get_Range("H8", "H10");
                row8_TieuDe_NoiKCB.Merge();
                row8_TieuDe_NoiKCB.Value2 = "Nơi đăng ký KCB";
                row8_TieuDe_NoiKCB.ColumnWidth = 35;

                Range row8_TieuDe_CanCu = oSheet.get_Range("I8", "M8");
                row8_TieuDe_CanCu.Merge();
                row8_TieuDe_CanCu.Value2 = "Căn cứ đóng BHXH, BHYT, BHTN";

                Range row9_TieuDe_TienLuong = oSheet.get_Range("I9", "I10");
                row9_TieuDe_TienLuong.Merge();
                row9_TieuDe_TienLuong.Value2 = "Tiền lương tiền công";
                row9_TieuDe_TienLuong.ColumnWidth = 15;

                Range row9_TieuDe_PhuCap = oSheet.get_Range("J9", "M9");
                row9_TieuDe_PhuCap.Merge();
                row9_TieuDe_PhuCap.Value2 = "Phụ cấp";

                Range row10_TieuDe_ChucVu = oSheet.get_Range("J10");
                row10_TieuDe_ChucVu.Value2 = "Chức vụ";

                Range row10_TieuDe_TNVK = oSheet.get_Range("K10");
                row10_TieuDe_TNVK.Value2 = "TN VK";

                Range row10_TieuDe_TNNG = oSheet.get_Range("L10");
                row10_TieuDe_TNNG.Value2 = "TN NG";

                Range row10_TieuDe_Khac = oSheet.get_Range("M10");
                row10_TieuDe_Khac.Value2 = "Khác";

                Range row8_TienLuongDongBHXH = oSheet.get_Range("N8", "N10");
                row8_TienLuongDongBHXH.Merge();
                row8_TienLuongDongBHXH.Value2 = "Tiền lương đóng BHXH";
                row8_TienLuongDongBHXH.ColumnWidth = 15;

                Range row8_TienLuongDongBHYT = oSheet.get_Range("O8", "O10");
                row8_TienLuongDongBHYT.Merge();
                row8_TienLuongDongBHYT.Value2 = "Tiền lương đóng BHYT";
                row8_TienLuongDongBHYT.ColumnWidth = 15;

                Range row8_TienLuongDongBHTN = oSheet.get_Range("P8", "P10");
                row8_TienLuongDongBHTN.Merge();
                row8_TienLuongDongBHTN.Value2 = "Tiền lương đóng BHTN";
                row8_TienLuongDongBHTN.ColumnWidth = 15;

                Range row8_ChucDanh = oSheet.get_Range("Q8", "Q10");
                row8_ChucDanh.Merge();
                row8_ChucDanh.Value2 = "Chức danh công việc";
                row8_ChucDanh.ColumnWidth = 40;


                int col = 0;
                DataRow[] dr = dtBHXH.Select();
                string[,] rowData = new string[dr.Count(), dtBHXH.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBHXH.Columns.Count - 2; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 10;
                oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).NumberFormat = "";
                Excel.Range formatRange;

                //STT
                formatRange = oSheet.get_Range(CharacterIncrement(0) + "11", CharacterIncrement(0) + rowCnt.ToString());
                formatRange.NumberFormat = "0";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Ngaysinh
                formatRange = oSheet.get_Range(CharacterIncrement(5) + "11", CharacterIncrement(5) + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //gioitinh
                formatRange = oSheet.get_Range(CharacterIncrement(6) + "11", CharacterIncrement(6) + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Tienluong
                formatRange = oSheet.get_Range(CharacterIncrement(8) + "11", CharacterIncrement(8) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_Chucvu
                formatRange = oSheet.get_Range(CharacterIncrement(9) + "11", CharacterIncrement(9) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_TNVK   
                formatRange = oSheet.get_Range(CharacterIncrement(10) + "11", CharacterIncrement(10) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_TNNG
                formatRange = oSheet.get_Range(CharacterIncrement(11) + "11", CharacterIncrement(11) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_khac
                formatRange = oSheet.get_Range(CharacterIncrement(12) + "11", CharacterIncrement(12) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhxh
                formatRange = oSheet.get_Range(CharacterIncrement(13) + "11", CharacterIncrement(13) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhyt
                formatRange = oSheet.get_Range(CharacterIncrement(14) + "11", CharacterIncrement(14) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhtn
                formatRange = oSheet.get_Range(CharacterIncrement(15) + "11", CharacterIncrement(15) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                Range rowN_Tong = oSheet.get_Range("A" + (rowCnt + 1).ToString(), "H" + (rowCnt + 1).ToString());
                rowN_Tong.Merge();
                rowN_Tong.Value2 = "Cộng";
                rowN_Tong.Font.Bold = true;
                rowN_Tong.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //rowN_Tong.Cells.VerticalAlignment = Excel.XlVAlign.x;

                // Tính tổng
                for (int colSUM = 9; colSUM < dtBHXH.Columns.Count - 2; colSUM++)
                {
                    oSheet.Cells[rowCnt + 1, colSUM] = "=SUM(" + CellAddress(oSheet, 9, colSUM) + ":" + CellAddress(oSheet, rowCnt, colSUM) + ")";
                    oSheet.Cells[rowCnt + 1, colSUM].NumberFormat = "#,##0;(#,##0); ;";
                    oSheet.Cells[rowCnt + 1, colSUM].TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                int keeprowCnt = rowCnt; // dữ dòng rowCnt cuối cùng

                //Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A8", lastColumn + (rowCnt + 1).ToString()));



                //Tổng hợp ở dưới
                rowCnt = rowCnt + 3;
                Range rowText_TongHopChung = oSheet.get_Range("A" + rowCnt.ToString(), "B" + rowCnt.ToString());
                rowText_TongHopChung.Merge();
                rowText_TongHopChung.Value2 = "TỔNG HỢP CHUNG";
                rowText_TongHopChung.Font.Bold = true;

                rowCnt = rowCnt + 1;
                Range rowText_SoLD = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_SoLD.Value2 = "1. Số lao động";

                Range rowData_SoLD = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_SoLD.Value2 = dtBHXH.Rows.Count;

                Range rowNgayIn = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowNgayIn.Merge();
                rowNgayIn.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowNgayIn.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowNgayIn.Value2 = "Ngày 26 Tháng 5 Năm 2022";

                rowCnt++;
                Range rowText_SoLDTN = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_SoLDTN.Value2 = "2. Số lao động TN";

                Range rowData_SoLDTN = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_SoLDTN.Value2 = "4,90";

                Range rowText_CanBoThu = oSheet.get_Range("E" + rowCnt.ToString(), "F" + rowCnt.ToString());
                rowText_CanBoThu.Merge();
                rowText_CanBoThu.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_CanBoThu.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_CanBoThu.Font.Bold = true;
                rowText_CanBoThu.Value2 = "CÁN BỘ THU";

                Range rowText_PhuTrachBHXH = oSheet.get_Range("H" + rowCnt.ToString());
                rowText_PhuTrachBHXH.Font.Bold = true;
                rowText_PhuTrachBHXH.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_PhuTrachBHXH.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_PhuTrachBHXH.Value2 = "PHỤ TRÁCH BHXH";

                Range rowText_NGUOI_LAO_BIEU = oSheet.get_Range("J" + rowCnt.ToString(), "L" + rowCnt.ToString());
                rowText_NGUOI_LAO_BIEU.Merge();
                rowText_NGUOI_LAO_BIEU.Font.Bold = true;
                rowText_NGUOI_LAO_BIEU.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_NGUOI_LAO_BIEU.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_NGUOI_LAO_BIEU.Value2 = "NGƯỜI LẬP BIỂU";

                Range rowText_NGUOI_SU_DUNG = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowText_NGUOI_SU_DUNG.Merge();
                rowText_NGUOI_SU_DUNG.Font.Bold = true;
                rowText_NGUOI_SU_DUNG.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_NGUOI_SU_DUNG.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_NGUOI_SU_DUNG.Value2 = "NGƯỜI SỬ DỤNG";

                rowCnt++;

                Range rowText_QuyLuong = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_QuyLuong.Value2 = "3. Quỹ lương BHXH";

                Range rowData_QuyLuong = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_QuyLuong.Value2 = "=N" + (keeprowCnt + 1);

                Range rowText_Ky1 = oSheet.get_Range("E" + rowCnt.ToString(), "F" + rowCnt.ToString());
                rowText_Ky1.Merge();
                rowText_Ky1.Font.Italic = true;
                rowText_Ky1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky1.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky2 = oSheet.get_Range("H" + rowCnt.ToString());
                rowText_Ky2.Font.Italic = true;
                rowText_Ky2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky2.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky3 = oSheet.get_Range("J" + rowCnt.ToString(), "L" + rowCnt.ToString());
                rowText_Ky3.Merge();
                rowText_Ky3.Font.Italic = true;
                rowText_Ky3.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky3.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky3.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky4 = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowText_Ky4.Merge();
                rowText_Ky4.Font.Italic = true;
                rowText_Ky4.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky4.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky4.Value2 = "(Ký, ghi rõ họ tên)";

                rowCnt++;

                Range rowText_4 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_4.Value2 = "4. BHXH phải đóng";

                Range rowData_4 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_4.Value2 = "-2302012.5";

                rowCnt++;
                Range rowText_5 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_5.Value2 = "5. Trừ 2% đơn vị giữ lại";

                Range rowData_5 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_5.Value2 = "";

                rowCnt++;
                Range rowText_6 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_6.Value2 = "6. Quỹ lương BHYT";

                Range rowData_6 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_6.Value2 = "=O" + (keeprowCnt + 1);

                rowCnt++;
                Range rowText_7 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_7.Value2 = "7. BHYT phải đóng";

                Range rowData_7 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_7.Value2 = "(2,03)";

                rowCnt++;
                Range rowText_8 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_8.Value2 = "8. Quỹ lương BHTN";

                Range rowData_8 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_8.Value2 = "=P" + (keeprowCnt + 1);

                rowCnt++;
                Range rowText_9 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_9.Value2 = "9. BHTN phải đóng";

                Range rowData_9 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_9.Value2 = "(2,03)";

                //fomart All 
                formatRange = oSheet.get_Range("A11", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
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
    }
}