using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Vs.HRM
{
    public partial class ucBaoCaoThamNien : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoThamNien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        switch (rdoChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    DataTable dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    frm.rpt = new rptDSThamNien(lk_NgayIn.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSThamNien", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                        cmd.Parameters.Add("@Tu", SqlDbType.Int).Value = txtTu.EditValue;
                                        cmd.Parameters.Add("@Den", SqlDbType.Int).Value = txtDen.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[1].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                    }
                                    catch
                                    { }
                                    frm.ShowDialog();
                                    break;
                                }
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString())
                                    {
                                        case "DM":
                                            {
                                                BaoCaoTongHopThamNien_DM();
                                                break;
                                            }
                                        default:
                                            {
                                                BaoCaoTongHopThamNien_HN();
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoThamNien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            txtTu.EditValue = 0;
            txtDen.EditValue = 99;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";

        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }
        private void BaoCaoTongHopThamNien_HN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopThamNien_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                //cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondTime;
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
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string nameColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 30;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THÂM NIÊN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "4"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.get_Range("A3", "A4");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "STT";
                row5_TieuDe_DV.ColumnWidth = 6;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.get_Range("B3", "B4");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = "Chuyền/Phòng";
                row5_TieuDe_LDBQ.ColumnWidth = 30;

                Range row5_TieuDe_LDT = oSheet.get_Range("C3", "C4");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "Số lao động";
                row5_TieuDe_LDT.ColumnWidth = 11;


                Range row6_TieuDe_TT = oSheet.get_Range("D3", "E3");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "Giới tính";
                row6_TieuDe_TT.RowHeight = 14;

                Range row5_TieuDe_DT = oSheet.get_Range("D4");
                row5_TieuDe_DT.Value2 = "Nam";
                row5_TieuDe_DT.ColumnWidth = 7;

                Range row5_TieuDe_TN = oSheet.get_Range("E4");
                row5_TieuDe_TN.Value2 = "Nữ";
                row5_TieuDe_TN.ColumnWidth = 7;

                Range row5_TieuDe_LDG = oSheet.get_Range("F3", "J3");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "Thâm niên";
                row5_TieuDe_LDG.ColumnWidth = 44;

                Range row6_TieuDe_TG = oSheet.get_Range("F4");
                row6_TieuDe_TG.Value2 = "0-3 months";
                row6_TieuDe_TG.ColumnWidth = 11;

                Range row6_TieuDe_D1T = oSheet.get_Range("G4");
                row6_TieuDe_D1T.Value2 = "3-6 months";
                row6_TieuDe_D1T.ColumnWidth = 11;

                Range row6_TieuDe_1_3_T = oSheet.get_Range("H4");
                row6_TieuDe_1_3_T.Merge();
                row6_TieuDe_1_3_T.Value2 = "6-9 months";
                row6_TieuDe_1_3_T.ColumnWidth = 11;

                Range row6_TieuDe_3_6_T = oSheet.get_Range("I4");
                row6_TieuDe_3_6_T.Value2 = "9-12 months";
                row6_TieuDe_3_6_T.ColumnWidth = 11;

                Range row6_TieuDe_6_9_T = oSheet.get_Range("J4");
                row6_TieuDe_6_9_T.Value2 = "Trên 1 năm";
                row6_TieuDe_6_9_T.ColumnWidth = 11;

                Range row6_TieuDe_9_12_T = oSheet.get_Range("K4");
                row6_TieuDe_9_12_T.Value2 = "Ghi chú";
                row6_TieuDe_9_12_T.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range fortmatTitleTable = oSheet.get_Range("A3", "K4");
                fortmatTitleTable.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                fortmatTitleTable.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Microsoft.Office.Interop.Excel.Range formatRange;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 5;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[1].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    if (chanVongDau == "")
                    {
                        rowBD = (keepRowCnt + 3);
                        chanVongDau = "Chan";
                    }
                    DataTable dt = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();
                    dt = dtBCThang.AsEnumerable().Where(r => r["TEN_DV"].ToString().Equals(TEN_DV[i])).CopyToDataTable().Copy();
                    string[] TEN_XN = dt.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    // Tạo group đơn vị
                    Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Merge();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    rowBD++;
                    for (int j = 0; j < TEN_XN.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count - 2; col++)
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


                        // Tạo group xí nghiệp
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = int_to_Roman(j + 1);
                        oSheet.Cells[rowBD, 2] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        //// Dữ liệu cột tổng tăng
                        //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        //{
                        //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                        //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                        //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        //}
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }

                    // Tính tổng từng đơn vị
                    Range rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), nameColumn + "" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Interior.Color = Color.FromArgb(255, 255, 0);
                    rowTOTAL_DON_VI.Font.Bold = true;
                    rowTOTAL_DON_VI.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowTOTAL_DON_VI.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), "B" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Merge();
                    rowTOTAL_DON_VI.Value = "TỔNG";
                    for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    {
                        formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "" + (keepRowCnt + 2).ToString() + "");
                        sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                        sRowBD_XN = sRowBD_XN.Replace(';', Convert.ToChar(CharacterIncrement(col - 1)));
                        oSheet.Cells[keepRowCnt + 2, col] = "=" + sRowBD_XN;
                        sRowBD_XN = sRowBD_XN_Temp;
                    }
                    sRowBD_XN = ";";
                    sRowBD_XN_Temp = "";
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                rowCnt++;

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "7", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A5", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.get_Range("B5", "" + "B" + rowCnt + "");
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A3", nameColumn + rowCnt.ToString()));

                rowCnt++;
                rowCnt++;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tp.HCM , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "";
                rowCnt++;
                formatRange = oSheet.get_Range("E" + rowCnt + "");
                formatRange.Value = "P.TCLĐ";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tổng giám đốc";


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = fontSizeNoiDung;
                //BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                //// filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoTongHopThamNien_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopThamNien_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                //cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondTime;
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
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string nameColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 30;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THÂM NIÊN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "4"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.get_Range("A3", "A4");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "STT";
                row5_TieuDe_DV.ColumnWidth = 6;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.get_Range("B3", "B4");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = "Chuyền/Phòng";
                row5_TieuDe_LDBQ.ColumnWidth = 30;

                Range row5_TieuDe_LDT = oSheet.get_Range("C3", "C4");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "Số lao động";
                row5_TieuDe_LDT.ColumnWidth = 11;


                Range row6_TieuDe_TT = oSheet.get_Range("D3", "E3");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "Giới tính";
                row6_TieuDe_TT.RowHeight = 14;

                Range row5_TieuDe_DT = oSheet.get_Range("D4");
                row5_TieuDe_DT.Value2 = "Nam";
                row5_TieuDe_DT.ColumnWidth = 7;

                Range row5_TieuDe_TN = oSheet.get_Range("E4");
                row5_TieuDe_TN.Value2 = "Nữ";
                row5_TieuDe_TN.ColumnWidth = 7;

                Range row5_TieuDe_LDG = oSheet.get_Range("F3", "J3");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "Thâm niên";
                row5_TieuDe_LDG.ColumnWidth = 44;

                Range row6_TieuDe_TG = oSheet.get_Range("F4");
                row6_TieuDe_TG.Value2 = "0-3 months";
                row6_TieuDe_TG.ColumnWidth = 11;

                Range row6_TieuDe_D1T = oSheet.get_Range("G4");
                row6_TieuDe_D1T.Value2 = "3-6 months";
                row6_TieuDe_D1T.ColumnWidth = 11;

                Range row6_TieuDe_1_3_T = oSheet.get_Range("H4");
                row6_TieuDe_1_3_T.Merge();
                row6_TieuDe_1_3_T.Value2 = "6-9 months";
                row6_TieuDe_1_3_T.ColumnWidth = 11;

                Range row6_TieuDe_3_6_T = oSheet.get_Range("I4");
                row6_TieuDe_3_6_T.Value2 = "9-12 months";
                row6_TieuDe_3_6_T.ColumnWidth = 11;

                Range row6_TieuDe_6_9_T = oSheet.get_Range("J4");
                row6_TieuDe_6_9_T.Value2 = "Trên 1 năm";
                row6_TieuDe_6_9_T.ColumnWidth = 11;

                Range row6_TieuDe_9_12_T = oSheet.get_Range("K4");
                row6_TieuDe_9_12_T.Value2 = "Ghi chú";
                row6_TieuDe_9_12_T.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range fortmatTitleTable = oSheet.get_Range("A3", "K4");
                fortmatTitleTable.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                fortmatTitleTable.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Microsoft.Office.Interop.Excel.Range formatRange;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 5;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[1].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    if (chanVongDau == "")
                    {
                        rowBD = (keepRowCnt + 3);
                        chanVongDau = "Chan";
                    }
                    DataTable dt = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();
                    dt = dtBCThang.AsEnumerable().Where(r => r["TEN_DV"].ToString().Equals(TEN_DV[i])).CopyToDataTable().Copy();
                    string[] TEN_XN = dt.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    // Tạo group đơn vị
                    Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Merge();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    rowBD++;
                    for (int j = 0; j < TEN_XN.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count - 2; col++)
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


                        // Tạo group xí nghiệp
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = int_to_Roman(j + 1);
                        oSheet.Cells[rowBD, 2] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        //// Dữ liệu cột tổng tăng
                        //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        //{
                        //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                        //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                        //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        //}
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }

                    // Tính tổng từng đơn vị
                    Range rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), nameColumn + "" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Interior.Color = Color.FromArgb(255, 255, 0);
                    rowTOTAL_DON_VI.Font.Bold = true;
                    rowTOTAL_DON_VI.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowTOTAL_DON_VI.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), "B" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Merge();
                    rowTOTAL_DON_VI.Value = "TỔNG";
                    for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    {
                        formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "" + (keepRowCnt + 2).ToString() + "");
                        sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                        sRowBD_XN = sRowBD_XN.Replace(';', Convert.ToChar(CharacterIncrement(col - 1)));
                        oSheet.Cells[keepRowCnt + 2, col] = "=" + sRowBD_XN;
                        sRowBD_XN = sRowBD_XN_Temp;
                    }
                    sRowBD_XN = ";";
                    sRowBD_XN_Temp = "";
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                rowCnt++;

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "7", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A5", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.get_Range("B5", "" + "B" + rowCnt + "");
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A3", nameColumn + rowCnt.ToString()));

                rowCnt++;
                rowCnt++;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tp.HCM , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "";
                rowCnt++;
                formatRange = oSheet.get_Range("E" + rowCnt + "");
                formatRange.Value = "P.TCLĐ";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tổng giám đốc";


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = fontSizeNoiDung;
                //BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                //// filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
        public static string int_to_Roman(int n)
        {
            string[] roman_symbol = { "MMM", "MM", "M", "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C", "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };
            int[] int_value = { 3000, 2000, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            var roman_numerals = new System.Text.StringBuilder();
            var index_num = 0;
            while (n != 0)
            {
                if (n >= int_value[index_num])
                {
                    n -= int_value[index_num];
                    roman_numerals.Append(roman_symbol[index_num]);
                }
                else
                {
                    index_num++;
                }
            }

            return roman_numerals.ToString();
        }
    }
}
