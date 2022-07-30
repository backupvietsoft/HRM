using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using Vs.Report;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoQuanLyPhep : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoQuanLyPhep()
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
                                        default:
                                            //TheoDoiPhepNamThucTe();
                                            TheoDoiPhepNamThucTe_SB();
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
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                PhieuTienPhep();
                                                break;
                                            }
                                        default:
                                            PhieuTienPhep();
                                            break;
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
            lk_DenNgay.Text= DateTime.Now.ToString("dd/MM/yyyy");
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
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

                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 10;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                lastColumn = "Z";
                Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A2", "Z2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "THEO DÕI PHÉP NĂM " + lk_Nam.Text;

                Range row4_TieuDe_Format = oSheet.get_Range("A4", "Z4");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.Yellow;
                row4_TieuDe_Format.WrapText = true;

                oSheet.get_Range("A4").RowHeight = 45;
                Excel.Range row4_A = oSheet.get_Range("A4");
                row4_A.ColumnWidth = 5;
                row4_A.Value2 = "STT";

                Range row4_B = oSheet.get_Range("B4");
                row4_B.ColumnWidth = 10;
                row4_B.Value2 = "MS NV";

                Range row4_C = oSheet.get_Range("C4");
                row4_C.ColumnWidth = 25;
                row4_C.Value2 = "HỌ TÊN";

                Range row4_D = oSheet.get_Range("D4");
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "SỐ TÀI KHOẢN";

                Range row4_E = oSheet.get_Range("E4");
                row4_E.ColumnWidth = 25;
                row4_E.Value2 = "P.BAN/X.NGHIỆP";

                Range row4_F = oSheet.get_Range("F4");
                row4_F.ColumnWidth = 25;
                row4_F.Value2 = "TỔ";

                Range row4_G = oSheet.get_Range("G4");
                row4_G.ColumnWidth = 12;
                row4_G.Value2 = "LƯƠNG TÍNH PHÉP";

                Range row4_H = oSheet.get_Range("H4");
                row4_H.ColumnWidth = 12;
                row4_H.Value2 = "NGÀY VÀO LÀM";

                Range row4_I = oSheet.get_Range("I4");
                row4_I.ColumnWidth = 8;
                row4_I.Value2 = "PHÉP NĂM";

                Range row4_J = oSheet.get_Range("J4");
                row4_J.ColumnWidth = 8;
                row4_J.Value2 = "PHÉP THÂM NIÊN";

                int col = 10;
                string currentColumn = string.Empty;

                while (col <= 21)
                {
                    currentColumn = CharacterIncrement(col);
                    Range row4_T = oSheet.get_Range(currentColumn + 4);
                    row4_T.ColumnWidth = 8;
                    row4_T.Value2 = "THÁNG " + Convert.ToString(col - 9) + "/" + lk_Nam.Text;

                    col++;
                }

                Range row4_W = oSheet.get_Range("W4");
                row4_W.ColumnWidth = 10;
                row4_W.Value2 = "PHÉP ĐÃ NGHỈ";

                Range row4_X = oSheet.get_Range("X4");
                row4_X.ColumnWidth = 10;
                row4_X.Value2 = "PHÉP CÒN";

                Range row4_Y = oSheet.get_Range("Y4");
                row4_Y.ColumnWidth = 12;
                row4_Y.Value2 = "THÀNH TIỀN";

                Range row4_Z = oSheet.get_Range("Z4");
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
                oSheet.get_Range("A5", "Z" + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A5", "Z" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A5", "Z" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                BorderAround(oSheet.get_Range("A4", "Z" + rowCnt.ToString()));

                Excel.Range formatRange;
                formatRange = oSheet.get_Range("A5", "A" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("G5", "G" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("H5", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("I5", "I" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("J5", "J" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                for (col = 10; col < dtBCPhep.Columns.Count - 1; col++)
                {
                    currentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(currentColumn + "5", currentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.get_Range("Y5", "Y" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);


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

                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 10;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                //lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                lastColumn = "Z";
                Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A2", "Z2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "THEO DÕI PHÉP NĂM " + lk_Nam.Text;

                Range row4_TieuDe_Format = oSheet.get_Range("A4", "Y5");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.Yellow;
                row4_TieuDe_Format.WrapText = true;

                oSheet.get_Range("A4").RowHeight = 45;
                Excel.Range row4_A = oSheet.get_Range("A4","A5");
                row4_A.ColumnWidth = 5;
                row4_A.Merge();
                row4_A.Value2 = "STT";

                Range row4_B = oSheet.get_Range("B4","B5");
                row4_B.ColumnWidth = 10;
                row4_B.Merge();
                row4_B.Value2 = "MS NV";

                Range row4_C = oSheet.get_Range("C4","C5");
                row4_C.ColumnWidth = 25;
                row4_C.Merge();
                row4_C.Value2 = "HỌ TÊN";

                //Range row4_D = oSheet.get_Range("D4");
                //row4_D.ColumnWidth = 15;
                //row4_D.Value2 = "SỐ TÀI KHOẢN";

                Range row4_E = oSheet.get_Range("D4","D5");
                row4_E.ColumnWidth = 25;
                row4_E.Merge();
                row4_E.Value2 = "P.BAN/X.NGHIỆP";

                Range row4_F = oSheet.get_Range("E4","E5");
                row4_F.ColumnWidth = 25;
                row4_F.Merge();
                row4_F.Value2 = "TỔ";

                //Range row4_G = oSheet.get_Range("G4");
                //row4_G.ColumnWidth = 12;
                //row4_G.Value2 = "LƯƠNG TÍNH PHÉP";

                Range row4_H = oSheet.get_Range("F4","F5");
                row4_H.ColumnWidth = 12;
                row4_H.Merge();
                row4_H.Value2 = "NGÀY VÀO LÀM";

                Range row4_I = oSheet.get_Range("G4","G5");
                row4_I.ColumnWidth = 8;
                row4_I.Merge();
                row4_I.Value2 = "PHÉP NĂM";

                Range row4_J = oSheet.get_Range("H4","J4");
                row4_J.Merge();
                row4_J.Value2 = "THÂM NIÊN";

                Range row4_H4 = oSheet.get_Range("H5");
                row4_H4.ColumnWidth = 4;
                row4_H4.Merge();
                row4_H4.Value2 = "N";


                Range row4_I4 = oSheet.get_Range("I5");
                row4_I4.ColumnWidth = 4;
                row4_I4.Merge();
                row4_I4.Value2 = "T";

                Range row4_J4 = oSheet.get_Range("J5");
                row4_J4.ColumnWidth = 4;
                row4_J4.Merge();
                row4_J4.Value2 = "Ng";
                row4_J4.RowHeight = 53;

                Range row4_K4 = oSheet.get_Range("K4","V4");
                //row4_K4.ColumnWidth = 4;
                row4_K4.Merge();
                row4_K4.Value2 = "Ngày phép đã nghỉ trong năm";
                row4_K4.RowHeight = 30;

                int col = 10;
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

                Range row4_W = oSheet.get_Range("W4","W5");
                row4_W.ColumnWidth = 10;
                row4_W.Merge();
                row4_W.Value2 = "PHÉP ĐÃ NGHỈ";

                Range row4_X = oSheet.get_Range("X4","X5");
                row4_X.ColumnWidth = 10;
                row4_X.Merge();
                row4_X.Value2 = "TIÊU CHUẨN";

                Range row4_Y = oSheet.get_Range("Y4","Y5");
                row4_Y.ColumnWidth = 12;
                row4_Y.Merge();
                row4_Y.Value2 = "CÒN LẠI";

                //Range row4_Z = oSheet.get_Range("Z4");
                //row4_Z.ColumnWidth = 10;
                //row4_Z.Value2 = "KÝ NHẬN";


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
                oSheet.get_Range("A6", "Z" + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A6", "Z" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A6", "Z" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                BorderAround(oSheet.get_Range("A4", "Y" + rowCnt.ToString()));

                Excel.Range formatRange;
                formatRange = oSheet.get_Range("A6", "A" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //formatRange = oSheet.get_Range("G6", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("H6", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("I6", "I" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("J6", "J" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                for (col = 10; col < dtBCPhep.Columns.Count - 1; col++)
                {
                    currentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(currentColumn + "6", currentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                formatRange = oSheet.get_Range("Y6", "Y" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);


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
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptDSTienPhep","lblTIEU_DE") + " " + lk_Nam.EditValue;
            frm.rpt = new rptDSTienPhep(Convert.ToDateTime(lk_DenNgay.EditValue),sTieuDe);
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
    }
}
