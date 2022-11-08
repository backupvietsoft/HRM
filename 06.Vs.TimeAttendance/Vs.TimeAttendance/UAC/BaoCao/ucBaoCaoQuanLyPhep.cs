using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using Vs.Report;
using Excell = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoQuanLyPhep : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoQuanLyPhep()
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
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {

                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                TheoDoiPhepNamThucTe();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                TheoDoiPhepNamThucTe_SB();
                                                break;
                                            }
                                        default:
                                            TheoDoiPhepNamThucTe();
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                ThanhToanPhepNam();
                                                break;
                                            }
                                        default:
                                            ThanhToanPhepNam();
                                            break;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                TongHopTienPhep();
                                                break;
                                            }
                                        default:
                                            TongHopTienPhep();
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    PhieuTienPhep();
                                }
                                break;
                        }

                        break;
                    }
                default:
                    break;
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
        private void ucBaoCaoQuanLyPhep_Load(object sender, EventArgs e)
        {
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            LoadTinhTrangHopDong();
            lk_Nam.Text = DateTime.Now.ToString("yyyy");
            lk_DenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //lk_DenNgay.EditValue = DateTime.Today;
            //DateTime dtTN = DateTime.Today;
            //DateTime dtDN = DateTime.Today;
            ////dTuNgay.EditValue = dtTN.AddDays((-dtTN.Day) + 1);
            //dtDN = dtDN.AddMonths(1);
            //dtDN = dtDN.AddDays(-(dtDN.Day));
            //LK_NgayXemBaoCao.EditValue = dtDN;

        }
        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", LK_DON_VI.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
                LK_XI_NGHIEP.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                LK_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadTinhTrangHopDong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTinhTrangHopDong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(LK_LOAI_HD, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboTo();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {


                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {

                    }
                    break;
                case 3:
                    {

                    }
                    break;
                case 4:
                    {
                    }
                    break;
                case 5:
                    {

                    }
                    break;
                case 6:
                    {

                    }
                    break;
                default:
                    break;
            }
        }
        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void rdo_DiTreVeSom_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {
                        //rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                case 1:
                    {
                        //rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                case 2:
                    {
                        //rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }


        private void lk_Nam_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime tungay = Convert.ToDateTime(lk_Nam.EditValue);
            //DateTime denngay = Convert.ToDateTime(lk_Nam.EditValue).AddMonths(+1);
            //lk_TuNgay.EditValue =Convert.ToDateTime("01/"+ tungay.Month+"/"+ tungay.Year).ToString("dd/MM/yyyy");
            //lk_DenNgay.EditValue =Convert.ToDateTime("01/"+ denngay.Month+"/"+ tungay.Year).AddDays(-1).ToString("dd/MM/yyyy");
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                lk_Nam.Text = calThang.DateTime.ToString("yyyy");

                lk_Nam.ClosePopup();
            }
            catch { }
        }

        private void TheoDoiPhepNamThucTe()
        {
            string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            DateTime tungay = Convert.ToDateTime(datetime);
            try { datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue); } catch { }
            DateTime denngay = Convert.ToDateTime(datetime);
            int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            try
            {

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTheoDoiPhepNam"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = iType;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = tungay;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = denngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();

                Excell.Application oXL;
                Excell._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = true;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 10;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao0 = oSheet.Range[oSheet.Cells[2, 1], oSheet.Cells[2, 26]];
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "THEO DÕI PHÉP NĂM " + lk_Nam.Text;

                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 26]]; 
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.Yellow;
                row4_TieuDe_Format.WrapText = true;

                oSheet.get_Range("A4").RowHeight = 45;
                Excell.Range row4_A = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 1]]; 
                row4_A.ColumnWidth = 10;
                row4_A.Value2 = "STT";

                Range row4_B = oSheet.Range[oSheet.Cells[4, 2], oSheet.Cells[4, 2]]; 
                row4_B.ColumnWidth = 10;
                row4_B.Value2 = "MS NV";

                Range row4_C = oSheet.Range[oSheet.Cells[4, 3], oSheet.Cells[4, 3]]; 
                row4_C.ColumnWidth = 25;
                row4_C.Value2 = "HỌ TÊN";

                Range row4_D = oSheet.Range[oSheet.Cells[4, 4], oSheet.Cells[4, 4]];
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "SỐ TÀI KHOẢN";

                Range row4_E = oSheet.Range[oSheet.Cells[4, 5], oSheet.Cells[4, 5]];
                row4_E.ColumnWidth = 35;
                row4_E.Value2 = "P.BAN/X.NGHIỆP";

                Range row4_F = oSheet.Range[oSheet.Cells[4, 6], oSheet.Cells[4, 6]];
                row4_F.ColumnWidth = 35;
                row4_F.Value2 = "Chuyền/Phòng";

                Range row4_G = oSheet.Range[oSheet.Cells[4, 7], oSheet.Cells[4, 7]]; 
                row4_G.ColumnWidth = 12;
                row4_G.Value2 = "LƯƠNG TÍNH PHÉP";

                Range row4_H = oSheet.Range[oSheet.Cells[4, 8], oSheet.Cells[4, 8]];
                row4_H.ColumnWidth = 12;
                row4_H.Value2 = "NGÀY VÀO LÀM";

                Range row4_I = oSheet.Range[oSheet.Cells[4, 9], oSheet.Cells[4, 9]];
                row4_I.ColumnWidth = 8;
                row4_I.Value2 = "PHÉP NĂM";

                Range row4_J = oSheet.Range[oSheet.Cells[4, 10], oSheet.Cells[4, 10]]; 
                row4_J.ColumnWidth = 8;
                row4_J.Value2 = "PHÉP THÂM NIÊN";

                int col = 11;
                string currentColumn = string.Empty;

                while (col <= 21)
                {
                    Range row4_T = oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col]]; 
                    row4_T.ColumnWidth = 8;
                    row4_T.Value2 = "THÁNG " + Convert.ToString(col - 10) + "/" + lk_Nam.Text;

                    col++;
                }

                Range row4_W = oSheet.Range[oSheet.Cells[4, 23], oSheet.Cells[4, 23]]; 
                row4_W.ColumnWidth = 10;
                row4_W.Value2 = "PHÉP ĐÃ NGHỈ";

                Range row4_X = oSheet.Range[oSheet.Cells[4, 24], oSheet.Cells[4, 24]]; 
                row4_X.ColumnWidth = 10;
                row4_X.Value2 = "PHÉP CÒN";

                Range row4_Y = oSheet.Range[oSheet.Cells[4, 25], oSheet.Cells[4, 25]]; 
                row4_Y.ColumnWidth = 12;
                row4_Y.Value2 = "THÀNH TIỀN";

                Range row4_Z = oSheet.Range[oSheet.Cells[4, 26], oSheet.Cells[4, 26]];
                row4_Z.ColumnWidth = 10;
                row4_Z.Value2 = "KÝ NHẬN";


                DataRow[] dr = dtBCPhep.Select();
                string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCPhep.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 4;
                oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt, 26]].Value2 = rowData;
                oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt, 26]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt, 26]].Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                BorderAround(oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, 26]]);

                Excell.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[5, 7], oSheet.Cells[rowCnt, 7]]; 
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[5, 8], oSheet.Cells[rowCnt, 8]]; 
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[5, 9], oSheet.Cells[rowCnt, 9]]; 
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[5, 10], oSheet.Cells[rowCnt, 10]];
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                for (col = 10; col < dtBCPhep.Columns.Count - 1; col++)
                {
                    currentColumn = CharacterIncrement(col);
                    formatRange = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[rowCnt, col]]; 
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.Range[oSheet.Cells[5, 25], oSheet.Cells[rowCnt, 25]]; 
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);


            }
            catch
            {

            }
        }
        private void TheoDoiPhepNamThucTe_SB()
        {
            string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            DateTime tungay = Convert.ToDateTime(datetime);
            try { datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue); } catch { }
            DateTime denngay = Convert.ToDateTime(datetime);
            int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            try
            {

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTheoDoiPhepNam_SB"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = iType;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = tungay;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = denngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }

                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = false;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 10;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                lastColumn = "W";
                Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A2", "W2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "THEO DÕI PHÉP NĂM " + lk_Nam.Text;

                Range row4_TieuDe_Format = oSheet.get_Range("A4", "W5");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.Yellow;
                row4_TieuDe_Format.WrapText = true;

                oSheet.get_Range("A4").RowHeight = 45;
                Excell.Range row4_A = oSheet.get_Range("A4", "A5");
                row4_A.ColumnWidth = 5;
                row4_A.Merge();
                row4_A.Value2 = "STT";

                Range row4_B = oSheet.get_Range("B4", "B5");
                row4_B.ColumnWidth = 10;
                row4_B.Merge();
                row4_B.Value2 = "MS NV";

                Range row4_C = oSheet.get_Range("C4", "C5");
                row4_C.ColumnWidth = 25;
                row4_C.Merge();
                row4_C.Value2 = "HỌ TÊN";

                //Range row4_D = oSheet.get_Range("D4");
                //row4_D.ColumnWidth = 15;
                //row4_D.Value2 = "SỐ TÀI KHOẢN";

                //Range row4_E = oSheet.get_Range("D4","D5");
                //row4_E.ColumnWidth = 25;
                //row4_E.Merge();
                //row4_E.Value2 = "P.BAN/X.NGHIỆP";

                //Range row4_F = oSheet.get_Range("E4","E5");
                //row4_F.ColumnWidth = 25;
                //row4_F.Merge();
                //row4_F.Value2 = "TỔ";

                //Range row4_G = oSheet.get_Range("G4");
                //row4_G.ColumnWidth = 12;
                //row4_G.Value2 = "LƯƠNG TÍNH PHÉP";

                Range row4_H = oSheet.get_Range("D4", "D5");
                row4_H.ColumnWidth = 12;
                row4_H.Merge();
                row4_H.Value2 = "NGÀY VÀO LÀM";

                Range row4_I = oSheet.get_Range("E4", "E5");
                row4_I.ColumnWidth = 8;
                row4_I.Merge();
                row4_I.Value2 = "PHÉP NĂM";

                Range row4_J = oSheet.get_Range("F4", "H4");
                row4_J.Merge();
                row4_J.Value2 = "THÂM NIÊN";

                Range row4_H4 = oSheet.get_Range("F5");
                row4_H4.ColumnWidth = 4;
                row4_H4.Merge();
                row4_H4.Value2 = "N";


                Range row4_I4 = oSheet.get_Range("G5");
                row4_I4.ColumnWidth = 4;
                row4_I4.Merge();
                row4_I4.Value2 = "T";

                Range row4_J4 = oSheet.get_Range("H5");
                row4_J4.ColumnWidth = 4;
                row4_J4.Merge();
                row4_J4.Value2 = "Ng";
                row4_J4.RowHeight = 53;

                Range row4_K4 = oSheet.get_Range("I4", "T4");
                //row4_K4.ColumnWidth = 4;
                row4_K4.Merge();
                row4_K4.Value2 = "Ngày phép đã nghỉ trong năm";
                row4_K4.RowHeight = 30;

                int col = 8;
                string currentColumn = string.Empty;

                while (col <= 21)
                {
                    currentColumn = CharacterIncrement(col);
                    Range row4_T = oSheet.get_Range(currentColumn + 5);
                    row4_T.ColumnWidth = 8;
                    //row4_T.Merge();
                    row4_T.Value2 = "THÁNG " + Convert.ToString(col - 9) + "/" + lk_Nam.Text;

                    col++;
                }

                Range row4_W = oSheet.get_Range("U4", "U5");
                row4_W.ColumnWidth = 10;
                row4_W.Merge();
                row4_W.Value2 = "PHÉP ĐÃ NGHỈ";

                Range row4_X = oSheet.get_Range("V4", "V5");
                row4_X.ColumnWidth = 10;
                row4_X.Merge();
                row4_X.Value2 = "TIÊU CHUẨN";

                Range row4_Y = oSheet.get_Range("W4", "W5");
                row4_Y.ColumnWidth = 12;
                row4_Y.Merge();
                row4_Y.Value2 = "CÒN LẠI";

                //Range row4_Z = oSheet.get_Range("Z4");
                //row4_Z.ColumnWidth = 10;
                //row4_Z.Value2 = "KÝ NHẬN";


                //DataRow[] dr = dtBCPhep.Select();
                //string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];
                //int rowCnt = 0;
                int rowCntY = 6; //Dùng để tính tổng cột Y
                Excell.Range formatRange1;
                //foreach (DataRow row in dr)
                //{
                //    for (col = 0; col < dtBCPhep.Columns.Count -2; col++)
                //    {
                //        rowData[rowCnt, col] = row[col].ToString();
                //    }
                //    //formatRange1 = oSheet.get_Range("Y" + rowCntY.ToString());
                //    //formatRange1.Value2 = "X"+ rowCntY + "-W"+ rowCntY + "";
                //    //oSheet.get_Range("Y"+ rowCntY + "").Value2 = "=X"+ rowCntY + " - W"+ rowCntY + "";
                //    //rowCntY++;
                //    rowCnt++;
                //}
                //rowCnt = rowCnt + 4;
                //oSheet.get_Range("A6", "W" + rowCnt.ToString()).Value2 = rowData;

                Excell.Range formatRange;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 6;
                string[] TEN_TO = dtBCPhep.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int j = 0; j < TEN_TO.Count(); j++)
                {
                    dtBCPhep = ds.Tables[0].Copy();
                    dtBCPhep = dtBCPhep.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCPhep.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCPhep.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCPhep.Columns.Count - 2; col++)
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

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    row_groupXI_NGHIEP_Format.Merge();
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    //for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    //{
                    //    oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                    //    oSheet.Cells[rowBD, col].Font.Bold = true;
                    //    oSheet.Cells[rowBD, col].Font.Size = 12;
                    //}

                    //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                    //sRowBD_XN_Temp = sRowBD_XN;
                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    formatRange = oSheet.get_Range("A" + (rowBD + 1).ToString() + "", "A" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("B" + (rowBD + 1).ToString() + "", "B" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "@";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("E" + (rowBD + 1).ToString() + "", "E" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("F" + (rowBD + 1).ToString() + "", "F" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("G" + (rowBD + 1).ToString() + "", "G" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    formatRange = oSheet.get_Range("H" + (rowBD + 1).ToString() + "", "H" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0";
                    formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                    for (col = 8; col < dtBCPhep.Columns.Count - 2; col++)
                    {
                        currentColumn = CharacterIncrement(col);
                        formatRange = oSheet.get_Range(currentColumn + "" + (rowBD + 1).ToString() + "", currentColumn + (rowCnt + 1).ToString());
                        formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                        try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }

                    formatRange = oSheet.get_Range("U" + (rowBD + 1).ToString() + "", "W" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "#,##0.0";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    //// Dữ liệu cột tổng tăng
                    //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                    //{
                    //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                    //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                    //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                    //}
                    //formatRange1 = oSheet.get_Range("Y" + rowCntY.ToString());
                    //formatRange1.Value2 = "X" + rowCntY + "-W" + rowCntY + "";
                    //oSheet.get_Range("Y" + rowCntY + "").Value2 = "=X" + rowCntY + " - W" + rowCntY + "";
                    //rowCntY++;
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                rowCnt++;
                oSheet.get_Range("A6", "W" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A6", "W" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ

                for (int row = 6; row <= rowCnt; row++)
                {
                    formatRange1 = oSheet.get_Range("W" + row.ToString());
                    formatRange1.Value = "=V" + row + "-U" + row + "";
                }
                BorderAround(oSheet.get_Range("A4", "W" + rowCnt.ToString()));
                oXL.Visible = true;
                oXL.UserControl = true;
                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlExclusive);

            }
            catch
            {

            }
        }
        private void ThanhToanPhepNam()
        {
            string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            DateTime tungay = Convert.ToDateTime(datetime);
            datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue);
            DateTime denngay = Convert.ToDateTime(datetime);
            int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;

            frmViewReport frm = new frmViewReport();
            DataTable dt;
            dt = new DataTable();
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptDSTienPhep", "lblTIEU_DE") + " " + lk_Nam.EditValue;
            frm.rpt = new rptDSTienPhep(Convert.ToDateTime(lk_DenNgay.EditValue), sTieuDe);
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTheoDoiPhepNam"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = iType;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = tungay;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = denngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();
                dtBCPhep.TableName = "DA_TA";
                frm.AddDataSource(dtBCPhep);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void TongHopTienPhep()
        {
            string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            DateTime tungay = Convert.ToDateTime(datetime);
            datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue);
            DateTime denngay = Convert.ToDateTime(datetime);
            int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;

            frmViewReport frm = new frmViewReport();
            DataTable dt;
            dt = new DataTable();
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBCTHTienPhep", "lblTIEU_DE") + " " + lk_Nam.EditValue;
            frm.rpt = new rptBCTHTienPhep(Convert.ToDateTime(lk_DenNgay.EditValue), sTieuDe);
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTongHopTienPhep"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = iType;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = tungay;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = denngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();
                dtBCPhep.TableName = "DA_TA";
                frm.AddDataSource(dtBCPhep);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void PhieuTienPhep()
        {
            string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            DateTime tungay = Convert.ToDateTime(datetime);
            datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue);
            DateTime denngay = Convert.ToDateTime(datetime);
            int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            dt = new DataTable();
            frm.rpt = new rptBCPhieuTienPhep();
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTheoDoiPhepNam"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = iType;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = tungay;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = denngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();
                dtBCPhep.TableName = "DA_TA";
                frm.AddDataSource(dtBCPhep);
                //frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch
            { }
            frm.ShowDialog();
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
        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung();
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
    }
}
