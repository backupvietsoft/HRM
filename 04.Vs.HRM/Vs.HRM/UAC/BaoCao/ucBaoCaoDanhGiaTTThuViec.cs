using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;
using Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoDanhGiaTTThuViec : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoDanhGiaTTThuViec()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        if (chkBCThuViec.Checked == true)
                        {

                            DanhGiaTinhTrangThuViec_DM();
                        }
                        if (chkBCTrinhDo.Checked == true)
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            string strTieuDe = ("Đánh giá trình độ " + LK_NOI_DUNG.Text).ToUpper(); ;

                            frm.rpt = new rptBCDanhGiaTrinhDo(lk_NgayIn.DateTime, strTieuDe);

                            try
                            {
                                Int32 DiemTu = 0;
                                Int32 DiemDen = 99;
                                if (txDiemTu.Text != "")
                                {
                                    DiemTu = Convert.ToInt32(txDiemTu.EditValue);
                                }
                                if (txDiemDen.Text != "")
                                {
                                    DiemDen = Convert.ToInt32(txDiemDen.EditValue);
                                }

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhGiaTrinhDo", conn);

                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                cmd.Parameters.Add("@NDDG", SqlDbType.Int).Value = Convert.ToInt32(LK_NOI_DUNG.EditValue);
                                cmd.Parameters.Add("@DiemT", SqlDbType.Int).Value = DiemTu;
                                cmd.Parameters.Add("@DiemD", SqlDbType.Int).Value = DiemDen;
                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);
                            }
                            catch
                            { }


                            frm.ShowDialog();
                            break;
                        }
                        if (chkCNViPham.Checked == true)
                        {
                            System.Data.SqlClient.SqlConnection conn1;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptBCKhenThuongKyLuatTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                            try
                            {
                                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn1.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatTH", conn1);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 2;

                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);
                            }
                            catch (Exception ex)
                            {
                            }


                            frm.ShowDialog();
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoDanhGiaTTThuViec_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            lk_NgayIn.DateTime = DateTime.Now;
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_NOI_DUNG, Commons.Modules.ObjSystems.DataNoiDungDanhGia(false), "ID_NDDG", "TEN_NDDG", "Nội dung");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CV, Commons.Modules.ObjSystems.DataChucVu(true, System.Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV");
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);

            DateTime dtTN = DateTime.Today;
            DateTime dtDN = DateTime.Today;
            dTuNgay.EditValue = dtTN.AddDays((-dtTN.Day) + 1);
            dtDN = dtDN.AddMonths(1);
            dtDN = dtDN.AddDays(-(dtDN.Day));
            dDenNgay.EditValue = dtDN;
            Commons.Modules.sLoad = "";
            chkBCThuViec.EditValue = true;
        }
        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                if (LK_DON_VI.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
                }
                else
                {
                    LK_DON_VI.Properties.DataSource = dt;
                }
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
                if (LK_XI_NGHIEP.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
                }
                else
                {
                    LK_XI_NGHIEP.Properties.DataSource = dt;
                }
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
                if (LK_TO.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                }
                else
                {
                    LK_TO.Properties.DataSource = dt;
                }
                LK_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
        }


        private void DanhGiaTinhTrangThuViec_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDanhGiaTTThuViec", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dTuNgay.Text);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dDenNgay.Text);
                cmd.Parameters.Add("@ID_CV", SqlDbType.Int).Value = Convert.ToInt32(cboID_CV.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();


                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = false;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "" + lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO ĐÁNH GIÁ TÌNH TRẠNG THỬ VIỆC";

                Range rowTuNgay = oSheet.get_Range("A3", "" + lastColumn + "3");
                rowTuNgay.Merge();
                rowTuNgay.Font.Size = 12;
                rowTuNgay.Font.Name = fontName;
                rowTuNgay.Font.FontStyle = "Bold";
                rowTuNgay.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowTuNgay.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowTuNgay.Value = "Từ ngày " + dTuNgay.Text + " Đến ngày " + dDenNgay.Text + "";


                Range row4_TieuDe_Format = oSheet.get_Range("A5", "" + lastColumn + "5"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);


                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 5;


                Range row5_TieuDe_MSCN = oSheet.get_Range("B5");
                row5_TieuDe_MSCN.Value2 = "Mã số thẻ";
                row5_TieuDe_MSCN.ColumnWidth = 13;

                Range row5_TieuDe_HOTEN = oSheet.get_Range("C5");
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_XN = oSheet.get_Range("D5");
                row5_TieuDe_XN.Value2 = "Bộ phận";
                row5_TieuDe_XN.ColumnWidth = 20;


                Range row5_TieuDe_TO = oSheet.get_Range("E5");
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = 15;

                Range row5_TieuDe_NTV = oSheet.get_Range("F5");
                row5_TieuDe_NTV.Value2 = "Vị trí công việc";
                row5_TieuDe_NTV.ColumnWidth = 20;

                Range row5_TieuDe_NVL = oSheet.get_Range("G5");
                row5_TieuDe_NVL.Value2 = "Loại hợp đồng";
                row5_TieuDe_NVL.ColumnWidth = 15;

                Range row5_TieuDe_NBD = oSheet.get_Range("H5");
                row5_TieuDe_NBD.Value2 = "Ngày bắt đầu hiệu lực";
                row5_TieuDe_NBD.ColumnWidth = 13;

                Range row5_TieuDe_NHHL = oSheet.get_Range("I5");
                row5_TieuDe_NHHL.Value2 = "Ngày hết hiệu lực";
                row5_TieuDe_NHHL.ColumnWidth = 13;

                Range row5_TieuDe_NDG = oSheet.get_Range("J5");
                row5_TieuDe_NDG.Value2 = "Người đánh giá";
                row5_TieuDe_NDG.ColumnWidth = 25;

                Range row5_TieuDe_NDGG = oSheet.get_Range("K5");
                row5_TieuDe_NDGG.Value2 = "Ngày đánh giá"; // 
                row5_TieuDe_NDGG.ColumnWidth = 13;

                Range row5_TieuDe_KHD = oSheet.get_Range("L5");
                row5_TieuDe_KHD.Value2 = "Kết thúc hợp đồng";
                row5_TieuDe_KHD.ColumnWidth = 10;

                Range row5_TieuDe_M = oSheet.get_Range("M5");
                row5_TieuDe_M.Value2 = "Ký hợp đồng";
                row5_TieuDe_M.ColumnWidth = 10;

                Range row5_TieuDe_N = oSheet.get_Range("N5");
                row5_TieuDe_N.Value2 = "Đã ký";
                row5_TieuDe_N.ColumnWidth = 10;

                string strSQL = "SELECT ID_LOAI_CV FROM dbo.CHUC_VU WHERE ID_CV = " + Convert.ToInt64(cboID_CV.EditValue) + "";
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                if (Convert.ToInt32(cboID_CV.EditValue) == -1 || i != 4)
                {
                    Range row5_TieuDe_KTCV = oSheet.get_Range("O5");
                    row5_TieuDe_KTCV.Value2 = "Kiến thức công việc";
                    row5_TieuDe_KTCV.ColumnWidth = 13;

                    Range row5_TieuDe_HQCV = oSheet.get_Range("P5");
                    row5_TieuDe_HQCV.Value2 = "Hiệu quả công việc";
                    row5_TieuDe_HQCV.ColumnWidth = 13;

                    Range row5_TieuDe_TDCV = oSheet.get_Range("Q5");
                    row5_TieuDe_TDCV.Value2 = "Thái độ công việc";
                    row5_TieuDe_TDCV.ColumnWidth = 13;

                    Range row5_TieuDe_TTNQ = oSheet.get_Range("R5");
                    row5_TieuDe_TTNQ.Value2 = "Thái độ công việc";
                    row5_TieuDe_TTNQ.ColumnWidth = 13;
                }

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

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
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                //formatRange = oSheet.get_Range("F7", "S" + (rowCnt).ToString());
                //formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("A6", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlExclusive);
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
        private void EnabelButton(bool visible)
        {
            lblTNgay.Enabled = visible;
            lblDNgay.Enabled = visible;
            lblChucVu.Enabled = visible;
            cboID_CV.Enabled = visible;
            dTuNgay.Enabled = visible;
            dDenNgay.Enabled = visible;
            lblBCTrinhDo.Enabled = !visible;
            lbBCCNViPham.Enabled = !visible;

            lbNoiDung.Enabled = !visible;
            lbDiemTu.Enabled = !visible;
            lbDen.Enabled = !visible;
            LK_NOI_DUNG.Enabled = !visible;
            txDiemTu.Enabled = !visible;
            txDiemDen.Enabled = !visible;
            lblBCDanhGiaTV.Enabled = visible;
            lbBCCNViPham.Enabled = visible;
        }

        private void chkBCThuViec_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            EnabelButton(true);
            chkBCTrinhDo.EditValue = false;
            chkCNViPham.EditValue = false;
            lbBCCNViPham.Enabled = false;
            Commons.Modules.sLoad = "";

        }

        private void chkBCTrinhDo_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            EnabelButton(false);
            chkBCThuViec.EditValue = false;
            chkCNViPham.EditValue = false;
            Commons.Modules.sLoad = "";
        }

        private void chkCNViPham_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            EnabelButton(true);
            lblBCDanhGiaTV.Enabled = false;
            chkBCThuViec.EditValue = false;
            chkBCTrinhDo.EditValue = false;
            lblChucVu.Enabled = false;
            cboID_CV.Enabled = false;
            Commons.Modules.sLoad = "";
        }
    }
}
