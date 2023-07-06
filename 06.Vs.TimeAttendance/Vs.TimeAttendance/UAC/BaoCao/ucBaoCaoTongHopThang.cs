using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Vs.Payroll;
using Vs.Report;
using DataTable = System.Data.DataTable;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTongHopThang : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable tbDLEX;
        public ucBaoCaoTongHopThang()
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

        #region even
        private void ucBaoCaoTongHopThang_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                LK_Thang.EditValue = DateTime.Today;
                Commons.OSystems.SetDateEditFormat(lk_TuNgay);
                Commons.OSystems.SetDateEditFormat(lk_DenNgay);
                LoadNgay();

                DateTime tungay = Convert.ToDateTime(LK_Thang.EditValue);
                DateTime denngay = Convert.ToDateTime(LK_Thang.EditValue).AddMonths(+1);
                lk_TuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
                lk_DenNgay.EditValue = Convert.ToDateTime("01/" + denngay.Month + "/" + denngay.Year).AddDays(-1);

                NgayIn.EditValue = DateTime.Today;
                LoadCboDonVi();
                LoadCboXiNghiep();
                LoadCboTo();

                // 0 rdo_BangChamCongThang
                // 1 rdo_BangChamCongThangNgayCong
                // 2 rdo_BangTongHopCongThang
                // 3 rdo_BangTongHopDiTreVeSomThang
                // 4 rdo_BaoCaoNghiBoViecThang
                // 5 rdo_BangChamCongTangCaThang
                // 6 rdo_DanhSachChuyenCongTac
                // 7 rdo_BCSoLanXacNhanCongThang
                // 8 rdo_BangChenhLechTangCaThang
                // 9 rdo_DanhSachThang
                // 10 rdo_ThongTinNhomCCThang

                lblThayDoiCa.Visible = false;
                chkThayDoiCa.Visible = false;
                rdo_DiTreVeSom.Visible = false;
                if (Commons.Modules.KyHieuDV == "SB")
                {
                    //rdo_ChonBaoCao.Properties.Items.RemoveAt(8);

                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChamCongThangNgayCong").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChenhLechTangCaThang").FirstOrDefault());
                    //rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_ThongTinNhomCCThang").FirstOrDefault());
                }
                else if (Commons.Modules.KyHieuDV == "DM")
                {
                    lblThayDoiCa.Visible = true;
                    chkThayDoiCa.Visible = true;
                    //rdo_ChonBaoCao.Properties.Items.RemoveAt(8);
                    //rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChamCongThangNgayCong").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChenhLechTangCaThang").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChamCongTangCaThang").FirstOrDefault());
                }
                else if (Commons.Modules.KyHieuDV == "NB" || Commons.Modules.KyHieuDV == "NC")
                {
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChamCongTangCaThang").FirstOrDefault());
                    //rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BCSoLanXacNhanCongThang").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_ThongTinNhomCCThang").FirstOrDefault());
                }
                else if (Commons.Modules.KyHieuDV == "VV")
                {
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_ThongTinNhomCCThang").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangChamCongThangNgayCong").FirstOrDefault());
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_BangTongHopCongThang").FirstOrDefault());
                }
                else
                {
                    rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_ThongTinNhomCCThang").FirstOrDefault());
                }
                chkThayDoiCa.Checked = true;
                LoadTinhTrangHopDong();
                Commons.Modules.sLoad = "";
                grdTTNhanVien.Visible = false;
                if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() != "rdo_DanhSachThang")
                {
                    grdTTNhanVien.Visible = false;
                    searchControl.Visible = false;
                    lblTongCN.Visible = false;
                }

                chkTinhTangCa.Visible = false;
                lblTinhTangCa.Visible = false;
                rdo_LocLaoDong.Visible = false;

                // đổi vị trí item
                if (Commons.Modules.KyHieuDV == "AP")
                {
                    // Lấy đối tượng RadioGroupItem từ tên của nó
                    RadioGroupItem radioGroupItemDSThang = rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_DanhSachThang").FirstOrDefault();

                    // Lấy index của mục đó
                    int currentIndexDSThang = rdo_ChonBaoCao.Properties.Items.IndexOf(radioGroupItemDSThang);

                    // Lấy các thuộc tính của mục hiện tại
                    string caption = radioGroupItemDSThang.Description;
                    object value = radioGroupItemDSThang.Value;
                    bool enabled = radioGroupItemDSThang.Enabled;
                    string tag = radioGroupItemDSThang.Tag.ToString();

                    // Xóa mục cũ khỏi RadioGroupItem
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(currentIndexDSThang);

                    // Tạo một mục mới với các thuộc tính tương tự
                    RadioGroupItem newItemDSThang = new RadioGroupItem(value, caption, enabled, tag);
                    newItemDSThang.Enabled = enabled;
                    rdo_ChonBaoCao.Properties.Items.Insert(3, newItemDSThang);
                }
                else
                if (Commons.Modules.KyHieuDV == "TG")
                {
                    chkThayDoiCa.Checked = false;
                    lblThayDoiCa.Visible = true;
                    chkThayDoiCa.Visible = true;

                    chkTinhTangCa.Checked = true;
                    chkTinhTangCa.Visible = true;
                    lblTinhTangCa.Visible = true;
                    rdo_LocLaoDong.Visible = true;
                }
                else
                {

                }

            }
            catch { }

        }

        private bool DatainEX()
        {
            //năm sau lớn hơn năm đầu
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spBaoCaoChamCongThangExcel"), Commons.Modules.UserName, Commons.Modules.TypeLanguage, LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, LK_TO.EditValue, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                tbDLEX = dt.Copy();
                //dt.Clear();
                grdData.DataSource = null;
                Commons.Modules.ObjSystems.MLoadXtraGridIP(grdData, grvData, dt, false, true, true, true);
                grdData.DataSource = null;
                grdData.DataSource = dt;
                grvData.Columns["STT_IN"].Caption = "STT";
                grvData.Columns["HO_TEN"].Caption = "Họ và tên";
                grvData.Columns["MA_THE"].Caption = "Mã thẻ";
                grvData.Columns["BO_PHAN"].Caption = "Bộ phận";
                grvData.Columns["NGAY_VAO_CTY"].Caption = "Ngày vào công ty";
                grvData.Columns["GIO_CONG"].Caption = "Giờ công";
                grvData.Columns["NGAY_THUONG"].Caption = "Ngày thường";
                grvData.Columns["CHU_NHAT"].Caption = "Chủ nhật";
                grvData.Columns["NGAY_LE"].Caption = "Ngày lễ";
                grvData.Columns["DEM"].Caption = "Đêm";
                grvData.Columns["CONG_DC"].Caption = "Thời gian nghỉ ngắn";
                grvData.Columns["TG_KHONG_SP"].Caption = "TG không làm ra sp";
                grvData.Columns["PHEP_NAM_TT"].Caption = "Phép năm thanh toán";
                grvData.Columns["PHEP_NAM_CL"].Caption = "phép năm còn lại";
                grvData.Columns["TIEN_CC"].Caption = "Tiền chuyên cần";
                grvData.Columns["GIO_AC"].Caption = "Tổng giờ ăn ca";
                grvData.Columns["TONG_GIO_CONG"].Caption = "Tổng giờ công";

                int i = 1;
                DataTable dtBCNghi = new DataTable();
                dtBCNghi.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LOAI_NGHI, KY_HIEU_LN, STT_IN FROM LOAI_NGHI_PHEP WHERE ISNULL(SU_DUNG,0) = 1 ORDER BY STT_IN"));
                if (dtBCNghi.Rows.Count > 0)
                {
                    //lay tieu de cot nghi 
                    DataRow[] drN = dtBCNghi.Select();
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow rowN in drN)
                    {
                        grvData.Columns["COT_" + i].Caption = rowN[1].ToString();
                        i++;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool DatainEXNgay()
        {
            //năm sau lớn hơn năm đầu
            try
            {

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoChamCongThangExcelTheoNgay", Commons.Modules.UserName, Commons.Modules.TypeLanguage, LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, LK_TO.EditValue, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, false, this.Name);
                grvData.Columns["STT"].Caption = "STT";
                grvData.Columns["HO_TEN"].Caption = "Họ và tên";
                grvData.Columns["MA_THE"].Caption = "Mã thẻ";
                grvData.Columns["NGAY_VAO_CTY"].Caption = "Ngày vào công ty";
                grvData.Columns["BO_PHAN"].Caption = "Bộ phận";
                grvData.Columns["TONG_LV"].Caption = "Tổng giờ LV";
                grvData.Columns["TONG_TC"].Caption = "Tổng giờ TC";
                grvData.Columns["TONG_CN"].Caption = "Tổng giờ CN";
                grvData.Columns["TONG"].Caption = "Tổng";


                return true;
            }
            catch
            {
                return false;
            }
        }
        private void BaoCaoChamCongThang_EX()
        {

            try
            {
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                //this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelApplication.DisplayAlerts = true;

                Microsoft.Office.Interop.Excel.Range title;
                Microsoft.Office.Interop.Excel.Range title1;

                int TCot = grvData.Columns.Count;
                int TDong = tbDLEX.Rows.Count;

                excelApplication.Visible = true;
                grvData.ActiveFilter.Clear();
                XlsxExportOptions xlsxExportOptions = new XlsxExportOptions()
                {
                    ExportMode = XlsxExportMode.SingleFile,
                    ShowGridLines = true,
                    TextExportMode = TextExportMode.Value,
                    FitToPrintedPageHeight = true
                };
                grvData.ExportToXlsx(sPath, xlsxExportOptions);
                System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelWorkbooks.Open(sPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, true);
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                excelApplication.Cells.Borders.LineStyle = 0;
                excelApplication.Cells.Font.Name = "Times New Roman";
                excelApplication.Cells.Font.Size = 11;
                excelWorkSheet.AutoFilterMode = false;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = false;

                int DONG = 0;


                DONG = TaoTTChung(excelWorkSheet, 1, 2, 1, TCot, 0, 0);
                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 4, DONG);

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.Merge(true);
                title.Value2 = "BẢNG CHẤM CÔNG";  /*"BÁO CÁO THEO DÕI THỰC HIỆN KẾ HOẠCH TUYỂN DỤNG";*/
                title.Font.Size = 16;
                title.RowHeight = 27;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;

                DONG++;
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "Tháng :" + LK_Thang.Text, DONG, 1, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot, 17);

                DONG++;

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "WORKING DAY", DONG, 6, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot - 20, 17);

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "TỔNG HỢP CÔNG", DONG, TCot - 19, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot - 5, 17);

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "Tăng ca(H)", DONG + 1, TCot - 10, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG + 1, TCot - 7, 17);

                string[] BP = tbDLEX.AsEnumerable().Select(r => r.Field<string>("BO_PHAN")).Distinct().ToArray();

                for (int i = 1; i <= TCot; i++)
                {
                    if (i < 6)
                    {
                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG + 2, i);
                        title.Merge();
                        title.WrapText = true;
                    }
                    else
                    {
                        if (i <= TCot - 20)
                        {

                            Commons.Modules.MExcel.DinhDang(excelWorkSheet, GetThu(excelWorkSheet, DONG + 2, i, tbDLEX.Rows.Count + DONG + 2), DONG + 1, i);
                        }
                        else
                        {
                            if (i <= TCot - 5)
                            {
                                if (i <= TCot - 11 || i > TCot - 7)
                                {
                                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i, DONG + 2, i);
                                    title.Merge();
                                    title.WrapText = true;
                                    title.ColumnWidth = 7;
                                }
                            }
                            else
                            {
                                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG + 2, i);
                                title.Merge();
                                title.WrapText = true;
                            }
                        }
                    }
                }

                //formart hetdet
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG + 2, TCot);
                title.WrapText = true;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;
                title.Borders.LineStyle = 1;

                DONG = DONG + 3;

                //
                int DONG1 = DONG;
                int totalCN = tbDLEX.Rows.Count / 6;

                //Double s1, s2 = 0;
                for (int i = 1; i <= totalCN; i++)
                {
                    try
                    {
                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG1, 1, DONG1 + 5, TCot);
                        title.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                        title.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG1, 6, DONG1, TCot - 20);
                        title.NumberFormat = "General";
                        title.Value = title.Value;

                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG1 + 1, 6, DONG1 + 1, TCot - 20);
                        title.NumberFormat = "General";
                        title.Value = title.Value;
                        title.NumberFormat = "#,##0.0;(#,##0.0); ; ";

                        DONG1 = DONG1 + 6;
                    }
                    catch
                    {

                    }
                }


                foreach (var item in BP.Where(x => x != null))
                {

                    DataTable dtTMP = new DataTable();
                    dtTMP = tbDLEX.AsEnumerable().Where(x => x["BO_PHAN"].Equals(item)).CopyToDataTable();
                    //title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, 1, DONG + dtTMP.Rows.Count, dtTMP.Columns.Count);
                    //title.Borders.LineStyle = 1;
                    //Commons.Modules.MExcel.MExportExcel(dtTMP, excelWorkSheet, title, false);

                    DONG = DONG + dtTMP.Rows.Count * 6;
                    Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, DONG);
                    //vẻ dòng cuối
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 5);
                    title.Font.Bold = true;
                    title.Merge();
                    title.Value2 = "Tổng cộng " + item + "";

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                    title.Interior.Color = System.Drawing.Color.FromArgb(180, 180, 205);
                    title.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

                    title = excelWorkSheet.Cells[DONG, TCot - 19];
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG - dtTMP.Rows.Count * 6, TCot - 19) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG - 1, TCot - 19) + ")";
                    title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, TCot - 19, DONG, TCot);
                    title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    DONG++;
                }



                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 21, "@", true, DONG, 2, DONG, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 18, "@", true, DONG, 4, DONG, 4);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 14, "@", true, DONG, 5, DONG, 5);
                //Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 6, "@", true, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count() + 1), 6, DONG, TCot - 20);

                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 6, "#,##0.0;(#,##0.0); ; ", true, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count()), TCot - 19, DONG, TCot - 3);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 10, "#,##0;(#,##0); ; ", true, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count()), TCot - 2, DONG, TCot - 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 6, "#,##0.0;(#,##0.0); ; ", true, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count()), TCot - 1, DONG, TCot - 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 6, "#,##0.0;(#,##0.0); ; ", true, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count()), TCot, DONG, TCot);
                //alight dữ liệu

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count() + 2), 1, DONG, 4);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count() + 2), 5, DONG, TCot - 20);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - (tbDLEX.Rows.Count + BP.Where(x => x != null).Count() + 2), 1, DONG - 1, TCot + 1);
                title.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;

                //title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - (tbDLEX.Rows.Count + BP.Count() + 2), 1, DONG - 1, TCot);
                //title.Borders.LineStyle = 1;

                //title.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                //title.Borders[XlBordersIndex.xlEdgeLeft].Color = Color.Black;
                //title.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                //title.Borders[XlBordersIndex.xlEdgeRight].Color = Color.Black;
                //borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;

                //var boder = title.Style.Border;
                //boder.Top.Style = ExcelBorderStyle.Thin;

                //Infooter
                DONG++;
                excelWorkSheet.Cells[DONG, 1].Value2 = "Đề nghị CBCNV viên kiểm tra kỹ bảng công của mình trước khi xác nhận";

                DONG++;
                excelWorkSheet.Cells[DONG, 2].Value2 = "X: Đi làm 8 giờ";
                excelWorkSheet.Cells[DONG, 10].Value2 = "T1: Nghĩ khám thai";
                excelWorkSheet.Cells[DONG, 23].Value2 = "CV: Chờ việc";
                excelWorkSheet.Cells[DONG, 35].Value2 = "C: Nghĩ cưới";
                excelWorkSheet.Cells[DONG, 41].Value2 = "DC: Công điều chuyển";
                excelWorkSheet.Cells[DONG, 48].Value2 = "O1: Bảng thân nghĩ ốm ngắn ngày";

                DONG++;
                excelWorkSheet.Cells[DONG, 2].Value2 = "Số: Số giờ làm việc";
                excelWorkSheet.Cells[DONG, 10].Value2 = "T2: Sảy thai,thai chết lưu";
                excelWorkSheet.Cells[DONG, 23].Value2 = "D: Nghĩ dưỡng sức";
                excelWorkSheet.Cells[DONG, 35].Value2 = "MC: Nghĩ hiếu";
                excelWorkSheet.Cells[DONG, 41].Value2 = "TN: Tai nạn lao động";
                excelWorkSheet.Cells[DONG, 48].Value2 = "O2: Bảng thân nghĩ ốm dài ngày";

                DONG++;
                excelWorkSheet.Cells[DONG, 2].Value2 = "P: Nghĩ phép";
                excelWorkSheet.Cells[DONG, 10].Value2 = "T3: Nghĩ thai sản";
                excelWorkSheet.Cells[DONG, 23].Value2 = "NL: Nghĩ lễ";
                excelWorkSheet.Cells[DONG, 35].Value2 = "CT: Công tác";
                excelWorkSheet.Cells[DONG, 41].Value2 = "NV: Nghĩ việc";
                excelWorkSheet.Cells[DONG, 48].Value2 = "O3: Con ốm";

                DONG++;
                excelWorkSheet.Cells[DONG, 2].Value2 = "R: Nghĩ việc riêng";
                excelWorkSheet.Cells[DONG, 10].Value2 = "T4: Thực hiện các biện pháp tránh thai";
                excelWorkSheet.Cells[DONG, 23].Value2 = "Po: Nghĩ phép không hưởng lương";
                excelWorkSheet.Cells[DONG, 35].Value2 = "CT: Công tác";
                excelWorkSheet.Cells[DONG, 41].Value2 = "NB: Nghĩ bù";
                excelWorkSheet.Cells[DONG, 48].Value2 = "Tđc: Tạm đình chỉ công tác";

                DONG++;
                excelWorkSheet.Cells[DONG, 2].Value2 = "X/2: Đi làm nữa ngày";
                excelWorkSheet.Cells[DONG, 10].Value2 = "T5: Nghĩ vợ sinh con";
                excelWorkSheet.Cells[DONG, 23].Value2 = "NL: Nghĩ lễ";
                excelWorkSheet.Cells[DONG, 41].Value2 = "Ê: Mất điện";

                DONG++;

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "Ngày....Tháng....Năm 20....", DONG, 48, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot, 13);

                DONG++;

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "NGƯỜI LẬP BIỂU", DONG, 2, "@", 13, true);
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "PHÒNG HCNS", DONG, 10, "@", 13, true);
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "TRƯỞNG BỘ PHẬN", DONG, 23, "@", 13, true);
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "QUẢN LÝ XƯỞNG", DONG, 41, "@", 13, true);
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "BANG GIÁM ĐỐC", DONG, 48, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot, 16);


                excelWorkbook.Save();
                excelApplication.Visible = true;
                Commons.Modules.MExcel.MReleaseObject(excelWorkSheet);
                Commons.Modules.MExcel.MReleaseObject(excelWorkbook);
                Commons.Modules.MExcel.MReleaseObject(excelApplication);
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "InKhongThanhCong", Commons.Modules.TypeLanguage) + ": " + ex.Message);
            }
        }
        private void BaoCaoChamCongThangNgay_EX()
        {

            string sPath = "";
            sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
            if (sPath == "") return;
            //this.Cursor = Cursors.WaitCursor;
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.DisplayAlerts = true;

            Microsoft.Office.Interop.Excel.Range title;

            int TCot = grvData.Columns.Count;
            int TDong = grvData.RowCount;

            excelApplication.Visible = false;
            grvData.ActiveFilter.Clear();
            grvData.ExportToXlsx(sPath);
            System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelWorkbooks.Open(sPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, true);
            Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
            try
            {
                excelApplication.Cells.Borders.LineStyle = 0;
                excelApplication.Cells.Font.Name = "Times New Roman";
                excelApplication.Cells.Font.Size = 11;
                excelWorkSheet.AutoFilterMode = false;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = false;
                int DONG = 0;



                DONG = TaoTTChung_TheoDV(excelWorkSheet, 1, 2, 1, TCot, 0, 0);
                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 5, DONG);

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.Merge(true);
                title.Value2 = "BẢNG CHẤM CÔNG";  /*"BÁO CÁO THEO DÕI THỰC HIỆN KẾ HOẠCH TUYỂN DỤNG";*/
                title.Font.Size = 16;
                title.RowHeight = 27;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;

                DONG++;
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "Tháng :" + LK_Thang.Text, DONG, 1, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot, 17);

                DONG++;

                Commons.Modules.MExcel.DinhDang(excelWorkSheet, "WORKING DAY", DONG, 6, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG, TCot - 4, 17);


                for (int i = 1; i <= TCot; i++)
                {
                    if (i < 6)
                    {
                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG + 3, i);
                        title.Merge();
                        title.WrapText = true;
                    }
                    else
                    {
                        if (i <= TCot - 4)
                        {
                            string schuoi = excelWorkSheet.Cells[DONG + 3, i].Value;
                            if (i % 2 == 0)
                            {
                                Commons.Modules.MExcel.DinhDang(excelWorkSheet, schuoi.Substring(2, schuoi.Length - 2), DONG + 1, i, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG + 1, i + 1, 15);

                                Commons.Modules.MExcel.DinhDang(excelWorkSheet, GetThu(schuoi.Substring(2, schuoi.Length - 2)), DONG + 2, i, "@", 13, true, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter, true, DONG + 2, i + 1, 15);

                            }
                            Commons.Modules.MExcel.DinhDang(excelWorkSheet, schuoi.Substring(0, 2), DONG + 3, i);
                        }
                        else
                        {

                            title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG + 3, i);
                            title.Merge();
                            title.WrapText = true;
                        }
                    }
                }

                //formart hetdet
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG + 3, TCot);
                title.WrapText = true;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;
                title.Borders.LineStyle = 1;

                DONG = DONG + 3;




                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 23, "@", true, DONG, 2, DONG, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 18, "@", true, DONG, 4, DONG, 4);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 15, "@", true, DONG, 5, DONG, 5);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 5, "#,##0.0;(#,##0.0); ; ", true, DONG + 1, 6, DONG + TDong, TCot - 4);


                //alight dữ liệu
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG + TDong, TCot);
                title.Borders.LineStyle = 1;



                excelWorkbook.Save();
                excelApplication.Visible = true;
                Commons.Modules.MExcel.MReleaseObject(excelWorkSheet);
                Commons.Modules.MExcel.MReleaseObject(excelWorkbook);
                Commons.Modules.MExcel.MReleaseObject(excelApplication);
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "InKhongThanhCong", 0) + " " + ex.Message);
            }
        }
        private string GetThu(Excel.Worksheet sheet, int Dong, int Cot, int lasrow)
        {
            string resulst = "";
            try
            {
                DateTime ngay = Convert.ToDateTime(sheet.Cells[Dong, Cot].Value + "/" + LK_Thang.Text.Substring(0, 2) + "/" + LK_Thang.Text.Substring(3, 4));
                switch (ngay.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        {
                            resulst = "CN";
                            Range title = Commons.Modules.MExcel.GetRange(sheet, Dong, Cot, lasrow, Cot);
                            title.Interior.Color = System.Drawing.Color.FromArgb(204, 255, 225);
                            break;
                        }
                    case DayOfWeek.Monday:
                        {
                            resulst = "T2";
                            break;
                        }
                    case DayOfWeek.Tuesday:
                        {
                            resulst = "T3";
                            break;
                        }
                    case DayOfWeek.Wednesday:
                        {
                            resulst = "T4";
                            break;
                        }
                    case DayOfWeek.Thursday:
                        {
                            resulst = "T5";
                            break;
                        }
                    case DayOfWeek.Friday:
                        {
                            resulst = "T6";
                            break;
                        }
                    case DayOfWeek.Saturday:
                        {
                            resulst = "T7";
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {
            }
            return resulst;

        }
        private string GetThu(string sngay)
        {
            string resulst = "";
            try
            {
                DateTime ngay = Convert.ToDateTime(sngay + "/" + LK_Thang.Text.Substring(0, 2) + "/" + LK_Thang.Text.Substring(3, 4));
                switch (ngay.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        {
                            resulst = "CN";
                            break;
                        }
                    case DayOfWeek.Monday:
                        {
                            resulst = "T2";
                            break;
                        }
                    case DayOfWeek.Tuesday:
                        {
                            resulst = "T3";
                            break;
                        }
                    case DayOfWeek.Wednesday:
                        {
                            resulst = "T4";
                            break;
                        }
                    case DayOfWeek.Thursday:
                        {
                            resulst = "T5";
                            break;
                        }
                    case DayOfWeek.Friday:
                        {
                            resulst = "T6";
                            break;
                        }
                    case DayOfWeek.Saturday:
                        {
                            resulst = "T7";
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {
            }
            return resulst;

        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_BangChamCongThang":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangChamCongThang_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangChamCongThang_SB2();
                                                break;
                                            }
                                        case "AP":
                                            {
                                                BangChamCongThang_AP(2);
                                                break;
                                            }
                                        case "DM":
                                            {
                                                BaoCaoTongHopThang_DM();
                                                break;
                                            }
                                        case "NB":
                                        case "NC":
                                            {
                                                try
                                                {

                                                    if (DatainEX() == false)
                                                    {
                                                        Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                                                        return;
                                                    }
                                                    BaoCaoChamCongThang_EX();
                                                }
                                                catch { }
                                                break;
                                            }
                                        case "HN":
                                            {
                                                try
                                                {

                                                    if (DatainEX() == false)
                                                    {
                                                        Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                                                        return;
                                                    }
                                                    BangChamCongThang_HN(2);
                                                }
                                                catch { }
                                                break;
                                            }
                                        case "VV":
                                            {
                                                //BangChamCongThangGioCong_VV();
                                                BangChamCongThang_VV(1);
                                                break;
                                            }
                                        case "BT":
                                            {
                                                BangChamCongThang_BT(3);
                                                break;
                                            }
                                        case "TG":
                                            {
                                                if (chkThayDoiCa.Checked)
                                                {
                                                    BCCThangTongHop_TG();
                                                }
                                                else
                                                {
                                                    BCCThangGioCong_TG();
                                                }
                                                break;
                                            }
                                        default:
                                            BangChamCongThang();
                                            break;
                                    }
                                }
                                break;
                            case "rdo_BangChamCongThangNgayCong":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "NC":
                                        case "AP":
                                            {
                                                BangChamCongThang_AP(1);
                                                break;
                                            }
                                        case "HN":
                                            {
                                                BangChamCongThang_HN(1);
                                                break;
                                            }
                                        case "NB":
                                            {
                                                try
                                                {

                                                    if (DatainEXNgay() == false)
                                                    {
                                                        Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                                                        return;
                                                    }
                                                    BaoCaoChamCongThangNgay_EX();
                                                }
                                                catch { }
                                                break;
                                            }
                                        case "BT":
                                            {
                                                BangChamCongThang_BT(1);
                                                break;
                                            }
                                        case "TG":
                                            {
                                                BCCThangNgayCong_TG();
                                                break;
                                            }
                                        default:
                                            BangChamCongThang();
                                            break;
                                    }
                                }
                                break;
                            case "rdo_BangChamCongTangCaThang":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangChamCongTangCaThang_MT();
                                                break;
                                            }
                                        case "AP":
                                            {
                                                BangChamCongThang_AP(3);
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangChamCongTangCaThang_SB();
                                                break;
                                            }
                                        case "VV":
                                            {
                                                BangChamCongThang_VV(2);
                                                break;
                                            }
                                        case "BT":
                                            {
                                                BangChamCongThang_BT(2);
                                                break;
                                            }
                                        case "HN":
                                            {
                                                BangChamCongThang_HN(3);
                                                break;
                                            }
                                        case "TG":
                                            {
                                                BCCThangTangCa_TG();
                                                break;
                                            }
                                        default:
                                            BangChamCongTangCaThang();
                                            break;
                                    }
                                }
                                break;

                            case "rdo_BangTongHopDiTreVeSomThang":
                                {
                                    BangTongHopDiTreVeSomThang();
                                    break;
                                }

                            case "rdo_BangTongHopCongThang":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "SB":
                                            {
                                                BangTongHopCongThang_SB();
                                                break;
                                            }
                                        default:
                                            BangTongHopCongThang();
                                            break;
                                    }
                                }
                                break;
                            case "rdo_BangChenhLechTangCaThang":
                                {
                                    BangChenhLechTangCaThang_MT();
                                    break;
                                }
                            case "rdo_DanhSachThang":
                                {
                                    if (Commons.Modules.KyHieuDV == "SB")
                                    {
                                        DanhSachThang_SB();
                                    }
                                    else if (Commons.Modules.KyHieuDV == "AP")
                                    {
                                        XacNhanQuetThe();
                                    }
                                    else
                                    {
                                        XacNhanQuetThe_DM(false);
                                    }
                                    break;
                                }

                            case "rdo_BaoCaoNghiBoViecThang":
                                {
                                    BaoCaoNghiBoViecThang();
                                    break;
                                }
                            case "rdo_DanhSachChuyenCongTac":
                                {
                                    DanhSachChuyenCongTac();
                                    break;
                                }
                            case "rdo_ThongTinNhomCCThang":
                                {
                                    if (Commons.Modules.KyHieuDV == "SB")
                                    {
                                        XacNhanQuetThe();
                                    }
                                    else
                                    {
                                        frmViewReport frm = new frmViewReport();
                                        DataTable dt;
                                        System.Data.SqlClient.SqlConnection conn;
                                        dt = new DataTable();
                                        frm.rpt = new rptThongTinNhomCaThang_DM(Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue), Convert.ToDateTime(NgayIn.EditValue));
                                        try
                                        {
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBCNhomCaTheoThang_DM"), conn);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                            cmd.Parameters.Add("@KiemTra", SqlDbType.Bit).Value = chkThayDoiCa.Checked;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            if (dt.Rows.Count == 0)
                                            {
                                                Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                                                return;
                                            }
                                            dt.TableName = "DATA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        }
                                        catch
                                        { }
                                        frm.ShowDialog();
                                    }
                                    break;
                                }

                            case "rdo_BCSoLanXacNhanCongThang":
                                {
                                    BaoXacNhanCongThang_DM();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.sLoad = "0Load";
                LoadCboXiNghiep();
                LoadCboTo();
                LoadGridThongTinNhanVien();
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.sLoad = "0Load";
                LoadCboTo();
                LoadGridThongTinNhanVien();
                Commons.Modules.sLoad = "";

            }
            catch { }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() == "rdo_DanhSachThang")
            {
                if (Commons.Modules.KyHieuDV == "SB")
                {
                    grdTTNhanVien.Visible = false;
                    lblTongCN.Visible = false;
                    searchControl.Visible = false;
                }
                else
                {
                    grdTTNhanVien.Visible = true;
                    lblTongCN.Visible = true;
                    searchControl.Visible = true;
                    LoadGridThongTinNhanVien();
                }
            }
            else
            {
                grdTTNhanVien.Visible = false;
                lblTongCN.Visible = false;
                searchControl.Visible = false;
            }

            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_BangTongHopDiTreVeSomThang":
                    {
                        rdo_DiTreVeSom.Visible = true;
                        chkThayDoiCa.Enabled = false;
                        lblThayDoiCa.Enabled = false;
                    }
                    break;
                case "rdo_ThongTinNhomCCThang":
                    {
                        if (Commons.Modules.KyHieuDV == "SB")
                        {
                            grdTTNhanVien.Visible = true;
                            lblTongCN.Visible = true;
                            searchControl.Visible = true;
                            LoadGridThongTinNhanVien();
                        }
                        else
                        {
                            grdTTNhanVien.Visible = false;
                            lblTongCN.Visible = false;
                            searchControl.Visible = false;
                        }

                        rdo_DiTreVeSom.Visible = false;
                        chkThayDoiCa.Enabled = true;
                        lblThayDoiCa.Enabled = true;
                        break;
                    }
                default:
                    {
                        chkThayDoiCa.Enabled = true;
                        lblThayDoiCa.Enabled = true;
                        rdo_DiTreVeSom.Visible = false;
                    }
                    break;
            }
        }

        private void grvThang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                LK_Thang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            LK_Thang.ClosePopup();

        }

        private void rdo_DiTreVeSom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + LK_Thang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            LK_Thang.ClosePopup();
        }
        private void LK_Thang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            DateTime tungay = Convert.ToDateTime(LK_Thang.EditValue);
            DateTime denngay = Convert.ToDateTime(LK_Thang.EditValue).AddMonths(+1);
            lk_TuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
            lk_DenNgay.EditValue = Convert.ToDateTime("01/" + denngay.Month + "/" + denngay.Year).AddDays(-1);
            Commons.Modules.sLoad = "";
            LoadGridThongTinNhanVien();
        }
        private void LK_Thang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (e.Button.Tag != null && e.Button.Tag.Equals("muiten"))
            {
                LoadNgay();
            }
        }
        #endregion
        private void LoadGridThongTinNhanVien()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spListCNBCTHT", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, LK_TO.EditValue, Commons.Modules.UserName, Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue), Commons.Modules.TypeLanguage));
                dt.Columns["CHON"].ReadOnly = false;
                if (grdTTNhanVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTTNhanVien, grvTTNhanVien, dt, true, true, false, true, true, this.Name);
                    grvTTNhanVien.Columns["CHON"].Visible = false;
                    grvTTNhanVien.Columns["ID_CN"].Visible = false;
                    grvTTNhanVien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["TEN_XN"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdTTNhanVien.DataSource = dt;
                }
                try
                {
                    grvTTNhanVien.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvTTNhanVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                lblTongCN.Text = "Số nhân viên : " + Convert.ToString(grvTTNhanVien.RowCount);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message.ToString());
            };
        }
        #region function

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
        private void LoadNgay()
        {
            try
            {
                string sNam = Convert.ToDateTime(LK_Thang.EditValue).Year.ToString();
                string sThang = "";
                DataTable dtthang = new DataTable();
                dtthang.Columns.Add("M", typeof(string));
                dtthang.Columns.Add("Y", typeof(string));
                dtthang.Columns.Add("THANG", typeof(string));

                for (int col = 1; col <= 12; col++)
                {
                    sThang = "0" + col.ToString();
                    sThang = sThang.Substring(sThang.Length - 2, 2);
                    dtthang.Rows.Add(sThang, sNam, sThang + "/" + sNam);
                }
                dtthang.PrimaryKey = new DataColumn[] { dtthang.Columns["M"] };



                int index = dtthang.Rows.IndexOf(dtthang.Rows.Find(grvThang.GetFocusedRowCellValue("M")));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                if (index == -1)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)grdThang.DataSource;
                    LK_Thang.EditValue = dt.Rows[DateTime.Now.Month - 1]["THANG"];
                }
                else
                {
                    grvThang.FocusedRowHandle = grvThang.GetRowHandle(index);
                    LK_Thang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                }

            }
            catch (Exception ex)
            {
            }
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
                Commons.Modules.ObjSystems.MsgError(ex.Message.ToString());
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
                Commons.Modules.ObjSystems.MsgError(ex.Message.ToString());
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
                Commons.Modules.ObjSystems.MsgError(ex.Message.ToString());
            }

        }

        private void LoadTinhTrangHopDong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTinhTrangHopDong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(LK_LOAI_HD, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
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
        #endregion

        #region functionInTheoDonVi
        private void BangChamCongThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe = oSheet.get_Range("A4", "H4");
                row5_TieuDe.Merge();
                row5_TieuDe.Value2 = "Thông tin nhân viên (Staff information)";

                Range row5_TieuDe2 = oSheet.get_Range("I4", lastColumNgay + "4");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Value2 = "Ngày làm việc (Date working)";

                Range row5_TieuDe3 = oSheet.get_Range(firstColumTT + "4", lastColumn + "4");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Value2 = "Thông tin chấm công tháng (Monthly attendance information)";

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                Range row5_TieuDe_ChucDanh = oSheet.get_Range("D5", "D6");
                row5_TieuDe_ChucDanh.Merge();
                row5_TieuDe_ChucDanh.Value2 = "Chức vụ";
                row5_TieuDe_ChucDanh.ColumnWidth = 20;

                Range row5_TieuDe_CV_A = oSheet.get_Range("D7");
                row5_TieuDe_CV_A.Value2 = "Position";

                Range row5_TieuDe_BoPhan = oSheet.get_Range("E5", "E6");
                row5_TieuDe_BoPhan.Merge();
                row5_TieuDe_BoPhan.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_BoPhan.ColumnWidth = 20;

                Range row5_TieuDe_XN_A = oSheet.get_Range("E7");
                row5_TieuDe_XN_A.Value2 = "Workshop/Department";

                Range row5_TieuDe_To = oSheet.get_Range("F5", "F6");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_To.ColumnWidth = 20;

                Range row5_TieuDe_TO_A = oSheet.get_Range("F7");
                row5_TieuDe_TO_A.Value2 = "Team";

                Range row5_TieuDe_NgayTV = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NgayTV.Merge();
                row5_TieuDe_NgayTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NgayTV.ColumnWidth = 12;

                Range row5_TieuDe_NTV_A = oSheet.get_Range("G7");
                row5_TieuDe_NTV_A.Value2 = "Start probation date";

                Range row5_TieuDe_NgayVL = oSheet.get_Range("H5", "H6");
                row5_TieuDe_NgayVL.Merge();
                row5_TieuDe_NgayVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NgayVL.ColumnWidth = 12;

                Range row5_TieuDe_NVL_A = oSheet.get_Range("H7");
                row5_TieuDe_NVL_A.Value2 = "Start working date";

                int col = 9;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Tổng số ngày làm việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Ngày thường";
                oSheet.Cells[7, col] = "Weekday";
                oSheet.Cells[6, col + 1] = "Ngày nghỉ";
                oSheet.Cells[7, col + 1] = "Weekend";

                col = col + 2;
                oSheet.Cells[5, col] = "Số ngày nghỉ phép";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Annual leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ bù";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Rostered leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ lễ";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "S.I. paid leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ hưởng BHXH";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "S.I. paid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ được hưởng lương";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Paid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ không lương, nghỉ tự do";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Unpaid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Number of days out job";

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng số công đi muộn, về sớm, ra ngoài việc riêng";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Short leave";

                col = col + 1;
                oSheet.Cells[5, col] = "TI (Số lần đi muộn)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of late arrival";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ đi muộn";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of late arrival";

                col = col + 1;
                oSheet.Cells[5, col] = "EO (Số lần về sớm)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of early leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ về sớm";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of early leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số lần ra ngoài việc riêng";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of special leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ ra ngoài";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of special leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng ngày công";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Total work days";


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                formatRange.NumberFormat = "@";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                string CurentColumn = string.Empty;
                int colBD = iSoNgay + 8;
                int colKT = colBD + 9;

                for (col = colBD; col <= colKT; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                //so lan di muon
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //so gio di muon
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //so lan ve som
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //so gio ve som
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //so lan ra ngoai
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //so gio ra ngoai
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }
                //tong ngay cong
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }

        }
        //In Excel
        private void BangChamCongThang_SB2()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                DataTable dtBCNghi = new DataTable();
                dtBCNghi = ds.Tables[2].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                DataTable dtSLTO = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLTO = ds.Tables[1].Copy();
                int slTO = Convert.ToInt32(dtSLTO.Rows[0][0]);

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeNoiDung = 9;
                DateTime dTNgay = lk_TuNgay.DateTime;
                DateTime dDNgay = lk_DenNgay.DateTime;

                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);

                Range row1_TieuDe = oSheet.get_Range("A1", "J1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = dtBCThang.Rows[0]["TEN_DV"];
                row1_TieuDe.WrapText = false;
                row1_TieuDe.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row2_TieuDe = oSheet.get_Range("A2", "J2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row2_TieuDe.WrapText = false;
                row2_TieuDe.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row5_TieuDe_Stt = oSheet.get_Range("A5");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 5;

                Range row6_TieuDe_Stt = oSheet.get_Range("A6");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "No";
                row6_TieuDe_Stt.ColumnWidth = 5;

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 12;

                Range row6_TieuDe_MaSo = oSheet.get_Range("B6");
                row6_TieuDe_MaSo.Merge();
                row6_TieuDe_MaSo.Value2 = "CODE";
                row6_TieuDe_MaSo.ColumnWidth = 12;

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "HỌ TÊN";
                row5_TieuDe_HoTen.ColumnWidth = 25;

                Range row6_TieuDe_HoTen = oSheet.get_Range("C6");
                row6_TieuDe_HoTen.Merge();
                row6_TieuDe_HoTen.Value2 = "FULL NAME";
                row6_TieuDe_HoTen.ColumnWidth = 25;

                //Range row5_TieuDe_XiNgiep = oSheet.get_Range("D5");
                //row5_TieuDe_XiNgiep.Merge();
                //row5_TieuDe_XiNgiep.Value2 = "XÍ NGHIỆP";
                //row5_TieuDe_XiNgiep.ColumnWidth = 12;

                //Range row6_TieuDe_XiNgiep = oSheet.get_Range("D6");
                //row6_TieuDe_XiNgiep.Merge();
                //row6_TieuDe_XiNgiep.Value2 = "ENTERPRISE";
                //row6_TieuDe_XiNgiep.ColumnWidth = 12;

                Range row5_TieuDe_To = oSheet.get_Range("D5");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_To.ColumnWidth = 12;

                Range row6_TieuDe_To = oSheet.get_Range("D6");
                row6_TieuDe_To.Merge();
                row6_TieuDe_To.Value2 = "DEP";
                row6_TieuDe_To.ColumnWidth = 12;

                int col = 5;
                while (dTNgay <= dDNgay)
                {
                    int iNgayLe = 0;
                    try
                    {
                        iNgayLe = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[fnKiemNgayNghi]('" + Convert.ToDateTime(dTNgay).ToString("MM/dd/yyyy") + "')"));
                    }
                    catch { }

                    oSheet.Cells[5, col] = dTNgay.Day;
                    if (iNgayLe > 0)
                    {
                        oSheet.Cells[6, col + 1] = "b (NL)";
                    }
                    else if (dTNgay.DayOfWeek.ToString() == "Sunday")
                    {
                        oSheet.Cells[6, col + 1] = "b (CN)";
                    }
                    else
                    {
                        oSheet.Cells[6, col + 1] = "b";
                    }
                    oSheet.Cells[6, col] = "a";
                    oSheet.Cells[6, col].Interior.Color = Color.White;
                    oSheet.Cells[6, col + 1].Interior.Color = Color.FromArgb(187, 255, 187);
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col)], oSheet.Cells[5, Convert.ToInt32(col + 1)]].Merge();
                    //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                    col += 2;
                    dTNgay = dTNgay.AddDays(1);
                }

                oSheet.Cells[5, col] = "Ngày công";
                oSheet.Cells[6, col] = "Workday";

                col = col + 1;
                oSheet.Cells[5, col] = "Tăng ca";
                oSheet.Cells[6, col] = "Overtime";

                col = col + 1;
                oSheet.Cells[5, col] = "Tăng ca đêm";
                oSheet.Cells[6, col] = "Night OT";

                col = col + 1;
                oSheet.Cells[5, col] = "Chủ nhật";
                oSheet.Cells[6, col] = "Sunday";

                col = col + 1;
                oSheet.Cells[5, col] = "Ngày lễ";
                oSheet.Cells[6, col] = "Holidays";

                col = col + 1;
                oSheet.Cells[5, col] = "Ghi chú (Notes)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 5]].Merge();


                //lay tieu de cot nghi 
                DataRow[] drN = dtBCNghi.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Cells[6, col] = rowN[1].ToString();
                    //oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;
                }

                //oSheet.Cells[6, col] = "P Anmual";
                //oSheet.Cells[6, col + 1] = "CĐ Policy";
                //oSheet.Cells[6, col + 2] = "KL Comp";
                //oSheet.Cells[6, col + 3] = "O1";
                //oSheet.Cells[6, col + 4] = "O3";
                //oSheet.Cells[6, col + 5] = "VLD Unreasonab";
                //col = col + 6;
                //oSheet.Cells[6, col] = "TR/S Late";
                //col = col + 1;
                //oSheet.Cells[6, col] = "QBT Forget";
                //col = col + 1;
                //oSheet.Cells[6, col] = "count overtime";


                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                                 //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 7;
                string cotCN_A = "";
                string cotCN_B = "";
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;

                for (int i = 0; i < TEN_TO.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            if (Convert.ToInt32(row[0]) == 1)
                            {
                                if (row[col].ToString() == "CN")
                                {
                                    //cotCN = cotCN + (col + 1) + ",";
                                    cotCN_A = CharacterIncrement(col);
                                    cotCN_B = CharacterIncrement(col + 1);
                                    Range ToMau = oSheet.get_Range("" + cotCN_A + "5", cotCN_B + "" + (dt_temp.Rows.Count + 6 + (slTO * 2)) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                    ToMau.Interior.Color = Color.FromArgb(255, 128, 0);
                                    //ToMau.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                }
                            }
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                        rowCONG = 1;
                    }
                    rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;


                    // Tạo group xí nghiệp
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                    oSheet.Cells[rowBD, 1] = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
                    oSheet.Cells[rowBD, 3] = TEN_TO[i].ToString();

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    formatRange = oSheet.get_Range("B8", "B" + (rowCnt + 1).ToString());
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }


                    //Tính tổng xí nghiệp
                    Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    row_groupTONG_Format.Interior.Color = Color.Yellow;
                    row_groupTONG_Format.Font.Bold = true;
                    oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";

                    for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    {
                        oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    }

                    // công thức cột tổng sản lượng công đoạn -- K
                    int colCT = 4; // col tính công thức mặc định + 4 CỘT ĐẦU + số ngày
                    colCT = colCT + iSoNgay * 2;

                    // ngày công
                    formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    formatRange1.Value2 = "=SUMIFS(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + " ,$E$6:$" + CharacterIncrement(3 + iSoNgay * 2) + @"$6,""=""&""a"")";
                    formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    if (current_dr > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    colCT++;
                    // Tăng ca
                    formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    formatRange1.Value2 = "=SUMIFS(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + " ,$E$6:$" + CharacterIncrement(3 + iSoNgay * 2) + @"$6,""=""&""b"")";
                    formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    if (current_dr > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    //Tăng ca đêm
                    colCT++;


                    colCT++;
                    // Chủ nhật
                    formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    formatRange1.Value2 = "=SUMIFS(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + " ,$E$6:$" + CharacterIncrement(3 + iSoNgay * 2) + @"$6,""=""&""b (CN)"")";
                    formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    if (current_dr > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    colCT++;
                    // Ngày lễ
                    formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    formatRange1.Value2 = "=SUMIFS(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + " ,$E$6:$" + CharacterIncrement(3 + iSoNgay * 2) + @"$6,""=""&""b (NL)"")";
                    formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    if (current_dr > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    //colCT++;
                    //// P Anmual
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""F"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// CĐ Policy
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""CĐ"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// KL Comp
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""KL"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// O1
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""O1"")" + "+ COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""O2"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// O3
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""O3"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// VLD Unreasonab
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""O"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    //colCT++;
                    //// TR/S Late
                    //formatRange1 = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowBD + 1).ToString());
                    //formatRange1.Value2 = "=COUNTIF(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + @",""ST"")";
                    //formatRange = oSheet.get_Range(CharacterIncrement(colCT) + (rowBD + 1), CharacterIncrement(colCT) + (rowCnt + 1).ToString());
                    //if (current_dr > 1)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}



                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt + 2; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
                                         //formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
                                         //formatRange.NumberFormat = "dd/MM/yyyy";
                                         //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                         //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                         //formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
                                         //formatRange.NumberFormat = "dd/MM/yyyy";
                                         //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                         //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                         //formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
                                         //formatRange.NumberFormat = "@";
                                         //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 4;
                int colKT = dtBCThang.Columns.Count;
                //format

                for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                // filter
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG TỔNG HỢP THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTongHopThang_MT(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThang_MT"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongThangGioCong_HN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThangGioCong_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 2);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                int col = 4;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Qui ra công để trả lương";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 7]].Merge();
                oSheet.Cells[6, col] = "Công TTTổ";
                oSheet.Cells[6, col + 1] = "P";
                oSheet.Cells[7, col + 1] = "P";
                oSheet.Cells[6, col + 2] = "Ô";
                oSheet.Cells[7, col + 2] = "Ô";
                oSheet.Cells[6, col + 3] = "Cô";
                oSheet.Cells[7, col + 3] = "Cô";
                oSheet.Cells[6, col + 4] = "Ro";
                oSheet.Cells[7, col + 4] = "Ro";
                oSheet.Cells[6, col + 5] = "O";
                oSheet.Cells[7, col + 5] = "O";
                oSheet.Cells[6, col + 6] = "V";
                oSheet.Cells[7, col + 6] = "V";
                oSheet.Cells[6, col + 7] = "70";
                oSheet.Cells[7, col + 7] = "70";

                col = col + 8;

                oSheet.Cells[5, col] = "Tổng giờ làm việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Total working hours";

                col++;
                oSheet.Cells[5, col] = "TC 9h";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Overtime 9h";

                col++;
                oSheet.Cells[5, col] = "TC > 9h";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Overtime > 9h";

                col++;
                oSheet.Cells[5, col] = "Phụ";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "50 %";
                oSheet.Cells[6, col + 1] = "100 %";
                oSheet.Cells[7, col] = "50 %";
                oSheet.Cells[7, col + 1] = "100 %";

                Microsoft.Office.Interop.Excel.Range formatRange;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                                 //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 8;
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
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
                        rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                        rowCONG = 1;
                    }
                    //rowBD = rowBD + dr_Cu + rowBD_XN;
                    rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
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

                    //// Dữ liệu cột tổng tăng
                    //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                    //{
                    //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                    //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                    //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                    //}

                    //Tính tổng xí nghiệp
                    Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    row_groupTONG_Format.Interior.Color = Color.Yellow;
                    row_groupTONG_Format.Font.Bold = true;
                    oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    //for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    //{
                    oSheet.Cells[rowCnt + 2, 4 + iDNgay] = "=SUM(" + CharacterIncrement(3 + iDNgay) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(3 + iDNgay) + "" + (rowCnt + 1).ToString() + ")";
                    //}
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt + 2;
                //formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                //formatRange.NumberFormat = "@";
                //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                string CurentColumn = string.Empty;
                int colBD = iSoNgay + 8;
                int colKT = colBD + 9;

                for (col = colBD; col <= colKT; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));

                rowCnt = rowCnt + 2;
                oSheet.Cells[rowCnt, 2].Font.Bold = true;
                oSheet.Cells[rowCnt, 2] = "Mã số";
                oSheet.Cells[rowCnt, 3].Font.Bold = true;
                oSheet.Cells[rowCnt, 3] = "Lý do";

                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8] = "Mã số";
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9] = "Lý do";

                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14] = "Mã số";
                oSheet.Cells[rowCnt, 15].Font.Bold = true;
                oSheet.Cells[rowCnt, 15] = "Lý do";

                oSheet.Cells[rowCnt, 20].Font.Bold = true;
                oSheet.Cells[rowCnt, 20] = "Mã số";
                oSheet.Cells[rowCnt, 21].Font.Bold = true;
                oSheet.Cells[rowCnt, 21] = "Lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "B";
                oSheet.Cells[rowCnt, 3] = "Mưa bão";
                oSheet.Cells[rowCnt, 8] = "C";
                oSheet.Cells[rowCnt, 9] = "Việc công";
                oSheet.Cells[rowCnt, 14] = "CB";
                oSheet.Cells[rowCnt, 15] = "Con bú";
                oSheet.Cells[rowCnt, 20] = "Cơ";
                oSheet.Cells[rowCnt, 21] = "Nghỉ con ốm";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "CV";
                oSheet.Cells[rowCnt, 3] = "Chờ việc";
                oSheet.Cells[rowCnt, 8] = "D1";
                oSheet.Cells[rowCnt, 9] = "DSPHSK sau ốm đau tại gia đình";
                oSheet.Cells[rowCnt, 14] = "D2";
                oSheet.Cells[rowCnt, 15] = "DSPHSK sau ốm đau nghỉ tập trung";
                oSheet.Cells[rowCnt, 20] = "D3";
                oSheet.Cells[rowCnt, 21] = "DSPHSK sau thai sản tại gia đình";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "D4";
                oSheet.Cells[rowCnt, 3] = "DSPHSK sau thai sản nghỉ tập trung";
                oSheet.Cells[rowCnt, 8] = "D5";
                oSheet.Cells[rowCnt, 9] = "DSPHSK sau tai nạn nghỉ tập trung";
                oSheet.Cells[rowCnt, 14] = "D6";
                oSheet.Cells[rowCnt, 15] = "DSPHSK sau ốm đau nghỉ tập trung";
                oSheet.Cells[rowCnt, 20] = "DH";
                oSheet.Cells[rowCnt, 21] = "Nghỉ dài hạn";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "F";
                oSheet.Cells[rowCnt, 3] = "Nghỉ phép";
                oSheet.Cells[rowCnt, 8] = "H";
                oSheet.Cells[rowCnt, 9] = "Nghỉ hội họp, học tập, công tác";
                oSheet.Cells[rowCnt, 14] = "L";
                oSheet.Cells[rowCnt, 15] = "Ngày nghỉ lễ, tết theo BLLĐ";
                oSheet.Cells[rowCnt, 20] = "O";
                oSheet.Cells[rowCnt, 21] = "Nghỉ không lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Ơ";
                oSheet.Cells[rowCnt, 3] = "Nghỉ ốm";
                oSheet.Cells[rowCnt, 8] = "O1";
                oSheet.Cells[rowCnt, 9] = "Bản thân ốm ngắn ngày";
                oSheet.Cells[rowCnt, 14] = "O2";
                oSheet.Cells[rowCnt, 15] = "Bản thân ốm dài ngày";
                oSheet.Cells[rowCnt, 20] = "O3";
                oSheet.Cells[rowCnt, 21] = "Con ốm";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Q";
                oSheet.Cells[rowCnt, 3] = "Di chuyển";
                oSheet.Cells[rowCnt, 8] = "R";
                oSheet.Cells[rowCnt, 9] = "Nghỉ việc riêng có lương";
                oSheet.Cells[rowCnt, 14] = "Ro";
                oSheet.Cells[rowCnt, 15] = "Nghỉ việc riêng không lương";
                oSheet.Cells[rowCnt, 20] = "T";
                oSheet.Cells[rowCnt, 21] = "Tai nạn lao động";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "T1";
                oSheet.Cells[rowCnt, 3] = "Khám thai";
                oSheet.Cells[rowCnt, 8] = "T2";
                oSheet.Cells[rowCnt, 9] = "Sẩy thai, nạo hút thai, thai chết lưu";
                oSheet.Cells[rowCnt, 14] = "T3";
                oSheet.Cells[rowCnt, 15] = "Sinh con, nuôi con nuôi";
                oSheet.Cells[rowCnt, 20] = "T4";
                oSheet.Cells[rowCnt, 21] = "Thực hiện các biện pháp tránh thai";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "T5";
                oSheet.Cells[rowCnt, 3] = "Lao động nghỉ việc khi vợ sinh con";
                oSheet.Cells[rowCnt, 8] = "T6";
                oSheet.Cells[rowCnt, 9] = "Lao động nữ mang thai hộ sinh con";
                oSheet.Cells[rowCnt, 14] = "T7";
                oSheet.Cells[rowCnt, 15] = "Lao động nữ nhờ mang thai hộ nhận con";
                oSheet.Cells[rowCnt, 20] = "T8";
                oSheet.Cells[rowCnt, 21] = "Lao động nam hưởng trợ cấp 1 lần khi vợ sinh con";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "V";
                oSheet.Cells[rowCnt, 3] = "Ngừng việc";

                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Ngày......tháng......năm.......";
                oSheet.Cells[rowCnt, 20] = "Người duyệt";
                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_HN(int type)
        {

            try
            {
                string storename = "";
                switch (type)
                {
                    case 1:
                        {
                            storename = "rptBangCongThangNgayCong_HN";
                            break;
                        }
                    case 2:
                        {
                            storename = "rptBangCongThangGioCong_HN";
                            break;
                        }
                    case 3:
                        {
                            storename = "rptBangCongThangNgoaiGio_HN";
                            break;
                        }
                    default:
                        break;
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;
                DataTable dtBCNghi;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(storename, conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                dtBCNghi = ds.Tables[1].Copy();

                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range formatRange;

                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;
                int TotalColumn = dtBCThang.Columns.Count;
                int TotalColNghi = dtBCNghi.Rows.Count;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);
                //string lastColumNgay = string.Empty;
                //lastColumNgay = CharacterIncrement(iSoNgay + 4);
                //string firstColumTT = string.Empty;
                //firstColumTT = CharacterIncrement(iSoNgay + 8);
                if (type != 3)
                {
                    Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 50;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                    Excel.Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                    row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                    row5_TieuDe_Format.Font.Name = fontName;
                    row5_TieuDe_Format.Font.Bold = true;
                    row5_TieuDe_Format.WrapText = true;
                    row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row5_TieuDe_Format.Interior.Color = Color.Yellow;

                    Excel.Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "Stt";
                    row5_TieuDe_Stt.ColumnWidth = 6;

                    Excel.Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "MSCN";
                    row5_TieuDe_MaSo.ColumnWidth = 15;

                    Excel.Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Họ và tên";
                    row5_TieuDe_HoTen.ColumnWidth = 30;

                    int col = 4;
                    while (iTNgay <= iDNgay)
                    {
                        oSheet.Cells[5, col] = iTNgay;
                        oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                        col++;
                        iTNgay++;
                    }


                    if (type == 2)
                    {
                        Excel.Range row5_TieuDe_Cong = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + TotalColNghi]];
                        row5_TieuDe_Cong.Merge();
                        row5_TieuDe_Cong.Value2 = "Qui ra giờ để trả lương";
                        Excel.Range row6_TieuDe_CongTT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                        row6_TieuDe_CongTT.Value2 = "Giờ TTế";
                        row6_TieuDe_CongTT.ColumnWidth = 10;
                    }
                    else
                    {
                        Excel.Range row5_TieuDe_Cong = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + TotalColNghi]];
                        row5_TieuDe_Cong.Merge();
                        row5_TieuDe_Cong.Value2 = "Qui ra công để trả lương";
                        Excel.Range row6_TieuDe_CongTT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                        row6_TieuDe_CongTT.Value2 = "Công TTế";
                        row6_TieuDe_CongTT.ColumnWidth = 10;
                    }
                    col++;

                    //lay tieu de cot nghi 
                    DataRow[] drN = dtBCNghi.Select();
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow rowN in drN)
                    {
                        oSheet.Cells[6, col] = rowN[1].ToString();
                        col++;
                    }

                    Excel.Range row5_TieuDe_TC = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TC.Merge();
                    row5_TieuDe_TC.Value2 = "TC 10h";
                    row5_TieuDe_TC.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TCD = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TCD.Merge();
                    row5_TieuDe_TCD.Value2 = "TC >10h";
                    row5_TieuDe_TCD.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_GioCD = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_GioCD.Merge();
                    row5_TieuDe_GioCD.Value2 = "Giờ +";
                    row5_TieuDe_GioCD.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TongGio = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TongGio.Merge();
                    row5_TieuDe_TongGio.Value2 = "Tổng giờ làm việc";
                    row5_TieuDe_TongGio.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_PhuTroi = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]];
                    row5_TieuDe_PhuTroi.Merge();
                    row5_TieuDe_PhuTroi.Value2 = "Phụ trội";
                    row5_TieuDe_PhuTroi.ColumnWidth = 10;

                    Excel.Range row6_TieuDe_50 = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                    row6_TieuDe_50.Merge();
                    row6_TieuDe_50.Value2 = "50%";
                    row6_TieuDe_50.ColumnWidth = 10;
                    col++;

                    Excel.Range row6_TieuDe_100 = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                    row6_TieuDe_100.Merge();
                    row6_TieuDe_100.Value2 = "100%";
                    row6_TieuDe_100.ColumnWidth = 10;

                    //Microsoft.Office.Interop.Excel.Range formatRange;
                    int rowCnt = 0;
                    int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                    int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                    int rowCONG = 0; // Row để insert dòng tổng
                                     //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                    string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                    string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                    int rowBD = 7;
                    string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                    string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    DataTable dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                    string sRowBD_XN_Temp = "";
                    for (int j = 0; j < TEN_TO.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), TotalColumn - 2];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < TotalColumn - 2; col++)
                            {
                                rowData[rowCnt, col] = row[col].ToString();
                            }
                            rowCnt++;
                        }
                        if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                        {
                            dr_Cu = 0;
                            rowBD_XN = 0;
                            rowCONG = 0;
                            chanVongDau = "";
                        }
                        else
                        {
                            rowBD_XN = 1;
                            rowCONG = 1;
                        }
                        //rowBD = rowBD + dr_Cu + rowBD_XN;
                        rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                        rowCnt = rowBD + current_dr - 1;

                        // Tạo group tổ
                        Excel.Range row_groupXI_NGHIEP = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, TotalColumn - 2]];
                        row_groupXI_NGHIEP.Merge();
                        row_groupXI_NGHIEP.Value2 = TEN_TO[j].ToString();
                        row_groupXI_NGHIEP.Interior.Color = Color.FromArgb(146, 208, 80);
                        row_groupXI_NGHIEP.Font.Bold = true;




                        //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        //sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        //oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                        oSheet.Range[oSheet.Cells[rowBD + 1, 1], oSheet.Cells[rowCnt + 1, TotalColumn - 2]].Value2 = rowData;

                        //Tính tổng xí nghiệp
                        Range row_groupTONG_Format = oSheet.Range[oSheet.Cells[rowBD + current_dr + 1, 1], oSheet.Cells[rowBD + current_dr + 1, TotalColumn - 2]]; //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                        row_groupTONG_Format.Interior.Color = Color.Yellow;
                        row_groupTONG_Format.Font.Bold = true;
                        oSheet.Cells[(rowBD + current_dr + 1), 1] = "Tổng Cộng";
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 3 + iSoNgay]].Merge();


                        string CurentColumn = string.Empty;
                        for (col = 4 + iSoNgay; col <= dtBCThang.Columns.Count - 2; col++)
                        {
                            CurentColumn = CharacterIncrement(col - 1);
                            formatRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + (rowCnt + 1).ToString());
                            formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                            try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            oSheet.Cells[rowBD + current_dr + 1, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD + current_dr + 1, col].Font.Bold = true;
                            oSheet.Cells[rowBD + current_dr + 1, col].Font.Size = 12;
                        }
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }


                    rowCnt = keepRowCnt + 2;
                    formatRange = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[keepRowCnt + 1, 3 + iSoNgay]];
                    formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    //formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                    //formatRange.NumberFormat = "@";
                    //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //Kẻ khung toàn bộ
                    formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());
                    formatRange.Borders.Color = Color.Black;

                    rowCnt = rowCnt + 2;
                    oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowCnt, 8] = "Ngày......tháng......năm.......";
                    oSheet.Cells[rowCnt, 20] = "Người duyệt";
                    rowCnt++;
                    oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                    formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;
                }
                else
                {
                    Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 50;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG NGOÀI GIỜ THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                    Excel.Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                    row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                    row5_TieuDe_Format.Font.Name = fontName;
                    row5_TieuDe_Format.Font.Bold = true;
                    row5_TieuDe_Format.WrapText = true;
                    row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row5_TieuDe_Format.Interior.Color = Color.Yellow;

                    Excel.Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "Stt";
                    row5_TieuDe_Stt.ColumnWidth = 6;

                    Excel.Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "MSCN";
                    row5_TieuDe_MaSo.ColumnWidth = 15;

                    Excel.Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Họ và tên";
                    row5_TieuDe_HoTen.ColumnWidth = 30;

                    int col = 4;
                    while (iTNgay <= iDNgay)
                    {
                        oSheet.Cells[5, col] = iTNgay;
                        oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                        col++;
                        iTNgay++;
                    }

                    Excel.Range row5_TieuDe_TCNT = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TCNT.Merge();
                    row5_TieuDe_TCNT.Value2 = "Giờ NT";
                    row5_TieuDe_TCNT.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TCCN = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TCCN.Merge();
                    row5_TieuDe_TCCN.Value2 = "Giờ CN";
                    row5_TieuDe_TCCN.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TCD = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TCD.Merge();
                    row5_TieuDe_TCD.Value2 = "Giờ TC đêm";
                    row5_TieuDe_TCD.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_GioCD = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_GioCD.Merge();
                    row5_TieuDe_GioCD.Value2 = "Giờ +";
                    row5_TieuDe_GioCD.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TongGioTC = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TongGioTC.Merge();
                    row5_TieuDe_TongGioTC.Value2 = "Tổng giờ tăng ca";
                    row5_TieuDe_TongGioTC.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TongGioLV = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TongGioLV.Merge();
                    row5_TieuDe_TongGioLV.Value2 = "Tổng giờ làm việc";
                    row5_TieuDe_TongGioLV.ColumnWidth = 10;
                    col++;

                    Excel.Range row5_TieuDe_TongGio = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]];
                    row5_TieuDe_TongGio.Merge();
                    row5_TieuDe_TongGio.Value2 = "Tổng giờ";
                    row5_TieuDe_TongGio.ColumnWidth = 10;
                    col++;

                    //Microsoft.Office.Interop.Excel.Range formatRange;
                    int rowCnt = 0;
                    int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                    int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                    int rowCONG = 0; // Row để insert dòng tổng
                                     //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                    string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                    string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                    int rowBD = 7;
                    string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                    string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    DataTable dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                    string sRowBD_XN_Temp = "";
                    for (int j = 0; j < TEN_TO.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), TotalColumn - 2];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < TotalColumn - 2; col++)
                            {
                                rowData[rowCnt, col] = row[col].ToString();
                            }
                            rowCnt++;
                        }
                        if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                        {
                            dr_Cu = 0;
                            rowBD_XN = 0;
                            rowCONG = 0;
                            chanVongDau = "";
                        }
                        else
                        {
                            rowBD_XN = 1;
                            rowCONG = 1;
                        }
                        //rowBD = rowBD + dr_Cu + rowBD_XN;
                        rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                        rowCnt = rowBD + current_dr - 1;

                        // Tạo group tổ
                        Excel.Range row_groupXI_NGHIEP = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, TotalColumn - 2]];
                        row_groupXI_NGHIEP.Merge();
                        row_groupXI_NGHIEP.Value2 = TEN_TO[j].ToString();
                        row_groupXI_NGHIEP.Interior.Color = Color.FromArgb(146, 208, 80);
                        row_groupXI_NGHIEP.Font.Bold = true;




                        //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        //sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        //oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                        oSheet.Range[oSheet.Cells[rowBD + 1, 1], oSheet.Cells[rowCnt + 1, TotalColumn - 2]].Value2 = rowData;

                        //Tính tổng xí nghiệp
                        Range row_groupTONG_Format = oSheet.Range[oSheet.Cells[rowBD + current_dr + 1, 1], oSheet.Cells[rowBD + current_dr + 1, TotalColumn - 2]]; //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                        row_groupTONG_Format.Interior.Color = Color.Yellow;
                        row_groupTONG_Format.Font.Bold = true;
                        oSheet.Cells[(rowBD + current_dr + 1), 1] = "Tổng Cộng";
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 3 + iSoNgay]].Merge();


                        string CurentColumn = string.Empty;
                        for (col = 4 + iSoNgay; col <= dtBCThang.Columns.Count - 2; col++)
                        {
                            CurentColumn = CharacterIncrement(col - 1);
                            formatRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + (rowCnt + 1).ToString());
                            formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                            try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            oSheet.Cells[rowBD + current_dr + 1, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD + current_dr + 1, col].Font.Bold = true;
                            oSheet.Cells[rowBD + current_dr + 1, col].Font.Size = 12;
                        }
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }


                    rowCnt = keepRowCnt + 2;
                    formatRange = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[keepRowCnt + 1, 3 + iSoNgay]];
                    formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    //formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                    //formatRange.NumberFormat = "@";
                    //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //Kẻ khung toàn bộ
                    formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());
                    formatRange.Borders.Color = Color.Black;

                    rowCnt = rowCnt + 2;
                    oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowCnt, 8] = "Ngày......tháng......năm.......";
                    oSheet.Cells[rowCnt, 20] = "Người duyệt";
                    rowCnt++;
                    oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                    formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;
                }


                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_BT(int type) // type == 1 ngày công , 2 tăng ca tháng, 3 giờ công (chi tiết)
        {
            try
            {
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;
                DataTable dtBCNghi;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(type == 1 ? "rptBangCongThang_BT" : type == 2 ? "rptBangTangCaThang_BT" : "rptBangCongThangGio_BT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                dtBCNghi = ds.Tables[1].Copy();

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                int lastColumn = dtBCThang.Columns.Count - 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);


                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[7, lastColumn]];
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe_Stt = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[7, 1]];
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 10;


                Range row5_TieuDe_MaSo = oSheet.Range[oSheet.Cells[6, 2], oSheet.Cells[7, 2]];
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[7, 3]];
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                int col = 4;
                if (type == 3)
                {
                    Range row5_TieuDe_NgaySinh = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]];
                    row5_TieuDe_NgaySinh.Merge();
                    row5_TieuDe_NgaySinh.Value2 = "Ngày sinh";
                    row5_TieuDe_NgaySinh.ColumnWidth = 15;
                    col++;
                }

                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();

                    col++;
                    iTNgay++;
                }

                if (type == 1 || type == 3)
                {
                    oSheet.Cells[6, col] = "Ngày công làm việc";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;
                }
                else
                {
                    oSheet.Cells[6, col] = "Giờ làm việc";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                }

                int colFormatRF = col;

                //lay tieu de cot nghi 
                DataRow[] drN = dtBCNghi.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Cells[6, col] = rowN[1].ToString();
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;
                }

                if (type == 2 || type == 3)
                {
                    oSheet.Cells[6, col] = "Giờ làm thêm BT";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                    oSheet.Cells[6, col] = "Giờ làm thêm CN";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                    oSheet.Cells[6, col] = "Giờ làm thêm chế độ";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                    oSheet.Cells[6, col] = "Giờ làm thêm lễ tết";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                    oSheet.Cells[6, col] = "Giờ làm thêm mặc định";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;

                    oSheet.Cells[6, col] = "Giờ tăng ca";
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                    col++;
                }

                //oSheet.Cells[6, col] = "Tổng cộng";
                //oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();
                //col++;

                oSheet.Cells[6, col] = "Ký nhận";
                oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[7, col]].Merge();

                Microsoft.Office.Interop.Excel.Range formatRange;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                                  //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 8;
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
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, lastColumn]];
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    oSheet.Range[oSheet.Cells[(rowBD + 1), 1], oSheet.Cells[(rowCnt + 1), lastColumn]].Value2 = rowData;
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt + 2;


                int colBD = 4;
                if (type == 3)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, 4], oSheet.Cells[rowCnt + 1, 4]];
                    formatRange.NumberFormat = "dd/MM/yyyy";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    colBD = 5;
                }

                for (col = colBD; col <= lastColumn - 1; col++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, col], oSheet.Cells[rowCnt, col]];
                    formatRange.NumberFormat = "0.0;-0;;@";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                ////Kẻ khung toàn bộ

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[6, lastColumn]]; //format dòng thông tin đơn vị
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                Commons.Modules.ObjSystems.HideWaitForm();
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
        private void BCCThangGioCong_TG() // type = 1 báo cáo chấm công tháng, type = 2 báo cáo tăng ca, type = 3 báo cáo ngày công
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangGio_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@TinhTC", SqlDbType.Bit).Value = chkTinhTangCa.Checked;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@LOC_NHAN_SU", SqlDbType.Int).Value = rdo_LocLaoDong.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                if (dtTo.Rows.Count == 0)
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

                int lastColumn = dtData.Columns.Count - 1;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 1;

                //oSheet.Name = rowXN[1].ToString();

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                int RowBD = 5;

                DataTable dtLyDoVang = new DataTable();
                dtLyDoVang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.LY_DO_VANG"));

                foreach (DataRow rowTo in dtTo.Rows)
                {
                    DataTable dataTemp = new DataTable();
                    dataTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == Convert.ToInt64(rowTo[0])).CopyToDataTable().Copy();

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowBD, 1], oSheet.Cells[RowBD, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG - " + rowTo[1].ToString() + "";

                    RowBD++;
                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[RowBD, 1], oSheet.Cells[RowBD, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "Từ ngày " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    int oCol = 1;
                    RowBD = RowBD + 2;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 25;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày vào làm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày sinh";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;

                    //CỘT NGÀY 
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    for (int i = lk_TuNgay.DateTime.Day; i <= lk_DenNgay.DateTime.Day; i++)
                    {
                        oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]] = i;
                        oCol++;
                    }

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ TC";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ký tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[RowBD, oCol], oSheet.Cells[RowBD, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ghi chú";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 30;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[RowBD, 1], oSheet.Cells[RowBD, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    RowBD++;
                    oRow = RowBD;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dataTemp.Select();
                    string[,] rowData = new string[dr.Count(), dataTemp.Columns.Count];
                    foreach (DataRow row in dataTemp.Rows)
                    {
                        for (int col = 0; col < dataTemp.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }

                    oRow = rowBD + rowCnt - 1;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;


                    Microsoft.Office.Interop.Excel.Range formatRange;


                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 4], oSheet.Cells[oRow, 4]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                    for (oCol = 6; oCol <= lastColumn - 2; oCol++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, oCol], oSheet.Cells[oRow, oCol]];
                        formatRange.NumberFormat = "0.0;-0;;@";
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }

                    BorderAround(oSheet.Range[oSheet.Cells[RowBD - 1, 1], oSheet.Cells[oRow, lastColumn]]);


                    Microsoft.Office.Interop.Excel.FormatCondition condition;
                    formatRange = oSheet.Range[oSheet.Cells[RowBD, 1], oSheet.Cells[oRow, lastColumn]];
                    condition = (formatRange.FormatConditions.Add(Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlTextString,
    TextOperator: Microsoft.Office.Interop.Excel.XlContainsOperator.xlContains, String: "/"));
                    condition.Interior.Color = Color.Yellow;


                    for (int i = 0; i < dtLyDoVang.Rows.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.FormatCondition condition2 = (formatRange.FormatConditions.Add(
    Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue,
    Operator: Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlEqual,
    Formula1: dtLyDoVang.Rows[i]["MS_LDV"].ToString()));
                        condition2.Interior.Color = Color.Yellow;
                    }


                    RowBD = oRow;
                    RowBD = RowBD + 4;
                    oRow = 1;
                }

                oSheet.PageSetup.FitToPagesWide = 1;
                oSheet.PageSetup.FitToPagesTall = 1;
                oSheet.PageSetup.Zoom = false;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                oSheet.PageSetup.LeftMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.RightMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.TopMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.BottomMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.HeaderMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.FooterMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                //oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                //oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
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
        private void BCCThangTangCa_TG() // type = 1 báo cáo chấm công tháng, type = 2 báo cáo tăng ca, type = 3 báo cáo ngày công
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangGio_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@LOC_NHAN_SU", SqlDbType.Int).Value = rdo_LocLaoDong.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                if (dtTo.Rows.Count == 0)
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

                int lastColumn = dtData.Columns.Count - 1;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 6;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);
                DataTable dtLyDoVang = new DataTable();
                dtLyDoVang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.LY_DO_VANG"));
                foreach (DataRow rowTo in dtTo.Rows)
                {
                    DataTable dataTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == Convert.ToInt64(rowTo[0])).CopyToDataTable().Copy();

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG NGOÀI GIỜ -" + rowTo[1].ToString();

                    oRow++;
                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "Từ ngày " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") + " Đến ngày " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    oRow = oRow + 2;

                    int oCol = 1;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 25;

                    oCol++;

                    //CỘT NGÀY 
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    for (int i = lk_TuNgay.DateTime.Day; i <= lk_DenNgay.DateTime.Day; i++)
                    {
                        oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]] = i;
                        oCol++;
                    }

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ LV";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ TC";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ CN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ NL";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ CĐ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tổng";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow++;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dataTemp.Select();
                    string[,] rowData = new string[dr.Count(), dataTemp.Columns.Count];
                    foreach (DataRow row in dataTemp.Rows)
                    {
                        for (int col = 0; col < dataTemp.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }

                    oRow = rowBD + rowCnt - 1;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;


                    Microsoft.Office.Interop.Excel.Range formatRange;

                    for (oCol = 4; oCol <= lastColumn; oCol++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, oCol], oSheet.Cells[oRow, oCol]];
                        formatRange.NumberFormat = "0.0;-0;;@";
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }
                    BorderAround(oSheet.Range[oSheet.Cells[rowBD - 1, 1], oSheet.Cells[oRow, lastColumn]]);

                    Microsoft.Office.Interop.Excel.FormatCondition condition;
                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]];
                    condition = (formatRange.FormatConditions.Add(Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlTextString,
    TextOperator: Microsoft.Office.Interop.Excel.XlContainsOperator.xlContains, String: "/"));
                    condition.Interior.Color = Color.Yellow;


                    for (int i = 0; i < dtLyDoVang.Rows.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.FormatCondition condition2 = (formatRange.FormatConditions.Add(
    Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue,
    Operator: Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlEqual,
    Formula1: dtLyDoVang.Rows[i]["MS_LDV"].ToString()));
                        condition2.Interior.Color = Color.Yellow;

                        oRow = oRow + 4;
                        //oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                        //oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                    }
                    this.Cursor = Cursors.Default;
                    oBook.Sheets[1].Activate();
                    oApp.Visible = true;
                    Commons.Modules.ObjSystems.HideWaitForm();
                }

                oSheet.PageSetup.FitToPagesWide = 1;
                oSheet.PageSetup.FitToPagesTall = 1;
                oSheet.PageSetup.Zoom = false;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                oSheet.PageSetup.LeftMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.RightMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.TopMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.BottomMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.HeaderMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.FooterMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BCCThangNgayCong_TG() // type = 1 báo cáo chấm công tháng, type = 2 báo cáo tăng ca, type = 3 báo cáo ngày công
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangGio_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@TinhTC", SqlDbType.Bit).Value = chkTinhTangCa.Checked;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@LOC_NHAN_SU", SqlDbType.Int).Value = rdo_LocLaoDong.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                DataTable dtLoaiNghi = new DataTable();
                dtLoaiNghi = ds.Tables[2].Copy();

                if (dtTo.Rows.Count == 0)
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

                int lastColumn = dtData.Columns.Count - 1;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 5;

                TaoTTChung_TheoDV(oSheet, 1, 3, 1, 15, 0, 0);
                DataTable dtLyDoVang = new DataTable();
                dtLyDoVang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.LY_DO_VANG"));
                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                foreach (DataRow rowTo in dtTo.Rows)
                {
                    //oSheet.Name = rowTo[1].ToString();
                    DataTable dataTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == Convert.ToInt64(rowTo[0])).CopyToDataTable().Copy();


                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 24;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 30;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG - " + rowTo[1].ToString() + "";

                    oRow++;
                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 24;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 30;
                    row2_TieuDe_BaoCao.Value2 = "Từ ngày " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    oRow = oRow + 2;
                    int oCol = 1;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 4.5;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.WrapText = true;
                    row2_TieuDeCot_BaoCao.ColumnWidth = 8.43;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                    row2_TieuDeCot_BaoCao.WrapText = true;
                    row2_TieuDeCot_BaoCao.ColumnWidth = 23.57;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày vào làm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 0;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày sinh";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 0;

                    oCol++;

                    //CỘT NGÀY 
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    for (int i = lk_TuNgay.DateTime.Day; i <= lk_DenNgay.DateTime.Day; i++)
                    {
                        oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]] = i;
                        oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]].ColumnWidth = 5.71;
                        oCol++;
                    }

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công";
                    row2_TieuDeCot_BaoCao.WrapText = true;
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6.29;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ TC";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.14;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ TCCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 0;

                    oCol++;
                    //lay tieu de cot nghi 
                    DataRow[] drN = dtLoaiNghi.Select();
                    //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow rowN in drN)
                    {
                        oSheet.Cells[oRow, oCol] = rowN[1].ToString();
                        oSheet.Cells[oRow, oCol].ColumnWidth = 7;
                        oSheet.Cells[oRow, oCol].WrapText = true;
                        oCol++;
                    }

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.WrapText = true;
                    row2_TieuDeCot_BaoCao.Value2 = "Tổng ngày nghỉ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 5.29;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol]];
                    row2_TieuDeCot_BaoCao.WrapText = true;
                    row2_TieuDeCot_BaoCao.Value2 = "Ký tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6.14;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow++;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dataTemp.Select();
                    string[,] rowData = new string[dr.Count(), dataTemp.Columns.Count];
                    foreach (DataRow row in dataTemp.Rows)
                    {
                        for (int col = 0; col < dataTemp.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }

                    oRow = rowBD + rowCnt - 1;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;

                    BorderAround(oSheet.Range[oSheet.Cells[rowBD - 1, 1], oSheet.Cells[oRow, lastColumn]]);

                    // Dòng tổng
                    oRow++;
                    formatRange = oSheet.Range[oSheet.Cells[oRow, dataTemp.Columns["NGAY_CONG_LV"].Ordinal - 1], oSheet.Cells[oRow, dataTemp.Columns["NGAY_CONG_LV"].Ordinal]];
                    formatRange.Merge();
                    formatRange.Value2 = "Tổng cộng";
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 13;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    formatRange = oSheet.Range[oSheet.Cells[oRow, dataTemp.Columns["NGAY_CONG_LV"].Ordinal + 1], oSheet.Cells[oRow, dataTemp.Columns["NGAY_CONG_LV"].Ordinal + 1]];
                    formatRange.Value2 = "=SUM(" + CharacterIncrement(dataTemp.Columns["NGAY_CONG_LV"].Ordinal) + "" + rowBD + ":" + CharacterIncrement(dataTemp.Columns["NGAY_CONG_LV"].Ordinal) + "" + (oRow - 1) + ")";
                    formatRange.NumberFormat = "0.0;-0;;@";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 12;
                    formatRange.Font.Bold = true;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //formatRange1 = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]];
                    //formatRange1.Value2 = "=SUMIFS(E" + (rowBD + 1) + ":" + CharacterIncrement(3 + iSoNgay * 2) + "" + (rowBD + 1) + " ,$E$6:$" + CharacterIncrement(3 + iSoNgay * 2) + @"$6,""=""&""b (NL)"")";

                    formatRange1 = oSheet.Range[oSheet.Cells[oRow, dataTemp.Columns["NGAY_CONG_LV"].Ordinal + 1], oSheet.Cells[oRow, lastColumn - 1]];
                    formatRange.AutoFill(formatRange1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);



                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 1]]; //format cột STT
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 2], oSheet.Cells[oRow, 2]]; //format cột MS_CN
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 3], oSheet.Cells[oRow, 3]]; //format cột HỌ TÊN
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 4], oSheet.Cells[oRow, 4]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    for (oCol = 6; oCol <= lastColumn - 1; oCol++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, oCol], oSheet.Cells[oRow - 1, oCol]];
                        formatRange.NumberFormat = "0.0;-0;;@";
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, lastColumn], oSheet.Cells[oRow, lastColumn]]; //format cột ngày vào làm
                    formatRange.RowHeight = 24.75;
                    //for (int colFormat = 5; colFormat <= lastColumn - 5; colFormat++) // format từ cột t
                    //{
                    //    formatRange = oSheet.Range[oSheet.Cells[rowBD, colFormat], oSheet.Cells[oRow, colFormat]];
                    //    formatRange.NumberFormat = "0.00;-0;;@";
                    //    try
                    //    {
                    //        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    //    }
                    //    catch { }

                    //}


                    Microsoft.Office.Interop.Excel.FormatCondition condition;
                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]];
                    condition = (formatRange.FormatConditions.Add(Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlTextString,
    TextOperator: Microsoft.Office.Interop.Excel.XlContainsOperator.xlContains, String: "/"));
                    condition.Interior.Color = Color.Yellow;

                    for (int i = 0; i < dtLyDoVang.Rows.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.FormatCondition condition2 = (formatRange.FormatConditions.Add(
    Type: Microsoft.Office.Interop.Excel.XlFormatConditionType.xlCellValue,
    Operator: Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlEqual,
    Formula1: dtLyDoVang.Rows[i]["MS_LDV"].ToString()));
                        condition2.Interior.Color = Color.Yellow;

                        //oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                        //oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                    }
                    oRow = oRow + 4;
                    this.Cursor = Cursors.Default;
                    oApp.Visible = true;
                    Commons.Modules.ObjSystems.HideWaitForm();
                }

                int column = 2;
                int rowDT = 0;
                DataTable dtLDV = new DataTable();
                dtLDV = ds.Tables[3].Copy();
                for (int i = 1; i <= 4; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[oRow, column], oSheet.Cells[oRow, column]];
                    formatRange.Value2 = "Mã số";
                    formatRange.Font.Bold = true;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 11;


                    column++;
                    formatRange = oSheet.Range[oSheet.Cells[oRow, column], oSheet.Cells[oRow, column]];
                    formatRange.Value2 = "Lý do";
                    formatRange.Font.Bold = true;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 11;

                    column = column + 10;
                }

                column = 2;
                oRow++;
                int dem = 0; // đếm dữ liệu đủ 4 dòng thì xuống dòng
                for (int j = 0; j < dtLDV.Rows.Count; j++)
                {
                    if (dem == 4)
                    {
                        dem = 0;
                        column = 2;
                        oRow++;
                    }
                    formatRange = oSheet.Range[oSheet.Cells[oRow, column], oSheet.Cells[oRow, column]];
                    formatRange.Value2 = Convert.ToString(dtLDV.Rows[j]["MS_LDV"]);
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 15;
                    column++;

                    formatRange = oSheet.Range[oSheet.Cells[oRow, column], oSheet.Cells[oRow, column]];
                    formatRange.Value2 = Convert.ToString(dtLDV.Rows[j]["TEN_LDV"]);
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 15;
                    rowDT++;
                    column = column + 10;
                    dem++;
                }


                oRow = oRow + 3;

                formatRange = oSheet.Range[oSheet.Cells[oRow, 11], oSheet.Cells[oRow, 11]];
                formatRange.Value2 = "Ngày........ tháng .......... năm ..........";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 15;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[oRow, 36], oSheet.Cells[oRow, 36]];
                formatRange.Value2 = "Người duyệt";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 15;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oRow++;
                formatRange = oSheet.Range[oSheet.Cells[oRow, 11], oSheet.Cells[oRow, 11]];
                formatRange.Value2 = "Người lập biểu";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 15;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oBook.Sheets[1].Activate();


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
                // Thiết lập các hàng tiêu đề để in trên mỗi trang
                oSheet.PageSetup.PrintTitleRows = "$A$8:$" + CharacterIncrement(lastColumn) + "$8";
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BCCThangTongHop_TG() // type = 1 báo cáo chấm công tháng, type = 2 báo cáo tăng ca, type = 3 báo cáo ngày công
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangGio_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@TinhTC", SqlDbType.Bit).Value = chkTinhTangCa.Checked;
                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@LOC_NHAN_SU", SqlDbType.Int).Value = rdo_LocLaoDong.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                if (dtTo.Rows.Count == 0)
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

                int lastColumn = 6;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 1;

                foreach (DataRow rowTo in dtTo.Rows)
                {
                    oSheet.Name = rowTo[1].ToString();
                    DataTable dataTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == Convert.ToInt64(rowTo[0])).CopyToDataTable().Copy();

                    TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG - " + rowTo[1].ToString() + "";

                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 15;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 20;
                    row2_TieuDe_BaoCao.Value2 = "Từ ngày " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") + " đến ngày " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    int oCol = 1;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "MSCN";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 10;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 25;

                    oCol++;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Giờ TC";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ký tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = 9;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dataTemp.Select();

                    foreach (DataRow row in dr)
                    {
                        dynamic[] arr = { row["STT"].ToString(), row["MS_CN"].ToString(), row["HO_TEN"].ToString(), row["NGAY_CONG_LV"].ToString(), row["GIO_TANG_CA"].ToString(), row["KY_TEN"].ToString() };
                        rowCnt++;

                        Range rowData = oSheet.Range[oSheet.Cells[rowCnt + rowBD - 1, 1], oSheet.Cells[rowCnt + +rowBD - 1, lastColumn]];
                        rowData.WrapText = true;
                        rowData.Value2 = arr;
                        rowData.Font.Size = fontSizeNoiDung;
                        rowData.Font.Name = fontName;
                    }

                    oRow = rowBD + rowCnt - 1;
                    //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;


                    Microsoft.Office.Interop.Excel.Range formatRange;
                    formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;


                    formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[oRow, 1]];
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    for (oCol = 4; oCol <= 5; oCol++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[9, oCol], oSheet.Cells[oRow, oCol]];
                        formatRange.NumberFormat = "0.0;-0;;@";
                        formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    }
                    BorderAround(oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[oRow, lastColumn]]);


                    oRow = 1;
                    oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }

                oSheet.PageSetup.FitToPagesWide = 1;
                oSheet.PageSetup.FitToPagesTall = 1;
                oSheet.PageSetup.Zoom = false;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                oSheet.PageSetup.LeftMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.RightMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.TopMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.BottomMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.HeaderMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.FooterMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

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
        private void BangChamCongThangGioCong_VV()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThangGioCong_VV", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                DateTime dTuNgay = lk_TuNgay.DateTime;
                DateTime dDenNgay = lk_DenNgay.DateTime;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count - 1;


                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG TỔNG HỢP THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, lastColumn]];
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe_Stt = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_MaSo = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                int col = 4;
                while (dTuNgay <= dDenNgay)
                {
                    oSheet.Cells[7, col] = dTuNgay.Day;
                    oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]].Merge();
                    col++;
                    dTuNgay = dTuNgay.AddDays(1);
                }

                Range row5 = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]];
                row5.Merge();
                row5.Value2 = "Giờ LV";
                row5.ColumnWidth = 12;

                col++;
                row5 = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]];
                row5.Merge();
                row5.Value2 = "Giờ TC";
                row5.ColumnWidth = 12;

                col++;
                row5 = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]];
                row5.Merge();
                row5.Value2 = "Giờ CN";
                row5.ColumnWidth = 12;

                col++;
                row5 = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]];
                row5.Merge();
                row5.Value2 = "22H";
                row5.ColumnWidth = 12;

                col++;
                row5 = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[8, col]];
                row5.Merge();
                row5.Value2 = "Tổng";
                row5.ColumnWidth = 12;

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                                 //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 9;
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
                        //rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                        //rowCONG = 1;
                    }
                    rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, lastColumn]]; //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    row_groupXI_NGHIEP_Format.Merge();
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    //Đổ dữ liệu của tổ
                    oSheet.Range[oSheet.Cells[(rowBD + 1), 1], oSheet.Cells[(rowCnt + 1), lastColumn]].Value2 = rowData;

                    ////Tính tổng tổ
                    //Range row_groupTONG_Format = oSheet.Range[oSheet.Cells[(rowBD + current_dr + 1), 1], oSheet.Cells[(rowBD + current_dr + 1), lastColumn]];  //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    //row_groupTONG_Format.Interior.Color = Color.Yellow;
                    //row_groupTONG_Format.Font.Bold = true;
                    //oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 3]].Merge();

                    ////for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    ////{
                    //oSheet.Cells[rowCnt + 2, 4 + iDNgay] = "=SUM(" + CharacterIncrement(3 + iDNgay) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(3 + iDNgay) + "" + (rowCnt + 1).ToString() + ")";

                    //formatRange1 = oSheet.Cells[rowCnt + 2, 4];
                    //formatRange1.Value2 = "=SUM(" + CharacterIncrement(3) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(3) + "" + (rowCnt + 1).ToString() + ")";
                    //formatRange = oSheet.Range[oSheet.Cells[rowCnt + 2, 4], oSheet.Cells[rowCnt + 2, lastColumn]]; 
                    //if (lastColumn > 4)
                    //{
                    //    formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    //}

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt + 2;


                for (col = 4; col <= dtBCThang.Columns.Count - 1; col++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, col], oSheet.Cells[rowCnt, col]];
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]);

                rowCnt = rowCnt + 2;
                oSheet.Cells[rowCnt, 2].Font.Bold = true;
                oSheet.Cells[rowCnt, 2] = "Mã số";
                oSheet.Cells[rowCnt, 3].Font.Bold = true;
                oSheet.Cells[rowCnt, 3] = "Lý do";

                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8] = "Mã số";
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9] = "Lý do";

                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14] = "Mã số";
                oSheet.Cells[rowCnt, 15].Font.Bold = true;
                oSheet.Cells[rowCnt, 15] = "Lý do";

                oSheet.Cells[rowCnt, 20].Font.Bold = true;
                oSheet.Cells[rowCnt, 20] = "Mã số";
                oSheet.Cells[rowCnt, 21].Font.Bold = true;
                oSheet.Cells[rowCnt, 21] = "Lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "12";
                oSheet.Cells[rowCnt, 3] = "Gia đình có việc";
                oSheet.Cells[rowCnt, 8] = "B";
                oSheet.Cells[rowCnt, 9] = "Nghỉ bệnh";
                oSheet.Cells[rowCnt, 14] = "Cô";
                oSheet.Cells[rowCnt, 15] = "Nghỉ con ốm";
                oSheet.Cells[rowCnt, 20] = "CoN";
                oSheet.Cells[rowCnt, 21] = "Con nhỏ";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "DH";
                oSheet.Cells[rowCnt, 3] = "Nghỉ dài hạn";
                oSheet.Cells[rowCnt, 8] = "Ê";
                oSheet.Cells[rowCnt, 9] = "Nghỉ mất điện";
                oSheet.Cells[rowCnt, 14] = "H";
                oSheet.Cells[rowCnt, 15] = "Nghỉ kết hôn";
                oSheet.Cells[rowCnt, 20] = "HH";
                oSheet.Cells[rowCnt, 21] = "Nghỉ Hội họp, học tập, công tác";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "HV";
                oSheet.Cells[rowCnt, 3] = "Hết việc hưởng 50% lương";
                oSheet.Cells[rowCnt, 8] = "L";
                oSheet.Cells[rowCnt, 9] = "Ngày nghỉ lễ, tết theo BLLĐ";
                oSheet.Cells[rowCnt, 14] = "NB";
                oSheet.Cells[rowCnt, 15] = "Nghỉ bù";
                oSheet.Cells[rowCnt, 20] = "NL";
                oSheet.Cells[rowCnt, 21] = "Nghỉ luôn";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "O";
                oSheet.Cells[rowCnt, 3] = "Vắng mặt";
                oSheet.Cells[rowCnt, 8] = "Ô";
                oSheet.Cells[rowCnt, 9] = "Nghỉ ốm";
                oSheet.Cells[rowCnt, 14] = "P";
                oSheet.Cells[rowCnt, 15] = "Nghỉ phép";
                oSheet.Cells[rowCnt, 20] = "R";
                oSheet.Cells[rowCnt, 21] = "Nghỉ việc riêng không lương";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Ro";
                oSheet.Cells[rowCnt, 3] = "Nghỉ việc riêng không lương";
                oSheet.Cells[rowCnt, 8] = "S";
                oSheet.Cells[rowCnt, 9] = "Nghỉ thai sản";
                oSheet.Cells[rowCnt, 14] = "Tg";
                oSheet.Cells[rowCnt, 15] = "Nghỉ tang lễ";
                oSheet.Cells[rowCnt, 20] = "TS";
                oSheet.Cells[rowCnt, 21] = "Nghỉ thai sản";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "VK";
                oSheet.Cells[rowCnt, 3] = "Nghỉ không do lỗi người LĐ";
                oSheet.Cells[rowCnt, 8] = "VLD";
                oSheet.Cells[rowCnt, 9] = "Nghỉ vô lý do";
                oSheet.Cells[rowCnt, 14] = "VR";
                oSheet.Cells[rowCnt, 15] = "Nghỉ việc riêng có lương";

                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Quảng Nam, Ngày......tháng......năm.......";
                oSheet.Cells[rowCnt, 20] = "Người duyệt";
                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                Commons.Modules.ObjSystems.HideWaitForm();
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }
        //In Xtrareport
        private void BangChamCongThang_SB()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG TỔNG HỢP THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTongHopThang_SB(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThang_MT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongTangCaThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongTCThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe = oSheet.get_Range("A4", "H4");
                row5_TieuDe.Merge();
                row5_TieuDe.Value2 = "Thông tin nhân viên (Staff information)";

                Range row5_TieuDe2 = oSheet.get_Range("I4", lastColumNgay + "4");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Value2 = "Ngày tăng ca (Overtime day)";

                Range row5_TieuDe3 = oSheet.get_Range(firstColumTT + "4", lastColumn + "4");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_CN_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_CN_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                Range row5_TieuDe_ChucDanh = oSheet.get_Range("D5", "D6");
                row5_TieuDe_ChucDanh.Merge();
                row5_TieuDe_ChucDanh.Value2 = "Chức vụ";
                row5_TieuDe_ChucDanh.ColumnWidth = 20;

                Range row5_TieuDe_CV_A = oSheet.get_Range("D7");
                row5_TieuDe_CV_A.Value2 = "Position";

                Range row5_TieuDe_BoPhan = oSheet.get_Range("E5", "E6");
                row5_TieuDe_BoPhan.Merge();
                row5_TieuDe_BoPhan.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_BoPhan.ColumnWidth = 20;

                Range row5_TieuDe_XN_A = oSheet.get_Range("E7");
                row5_TieuDe_XN_A.Value2 = "Workshop/Department";

                Range row5_TieuDe_To = oSheet.get_Range("F5", "F6");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Tổ";
                row5_TieuDe_To.ColumnWidth = 20;

                Range row5_TieuDe_T_A = oSheet.get_Range("F7");
                row5_TieuDe_T_A.Value2 = "Team";

                Range row5_TieuDe_NgayTV = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NgayTV.Merge();
                row5_TieuDe_NgayTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NgayTV.ColumnWidth = 12;

                Range row5_TieuDe_NTV_A = oSheet.get_Range("G7");
                row5_TieuDe_NTV_A.Value2 = "Start probation date";

                Range row5_TieuDe_NgayVL = oSheet.get_Range("H5", "H6");
                row5_TieuDe_NgayVL.Merge();
                row5_TieuDe_NgayVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NgayVL.ColumnWidth = 12;

                Range row5_TieuDe_NVL_A = oSheet.get_Range("H7");
                row5_TieuDe_NVL_A.Value2 = "Start working date";

                int col = 9;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ngày thường)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Tăng ca ban ngày";
                oSheet.Cells[7, col] = "Overtime during the day";
                oSheet.Cells[6, col + 1] = "Tăng ca ban đêm";
                oSheet.Cells[7, col + 1] = "Overtime at night";


                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ngày chủ nhật)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Tăng ca ban ngày";
                oSheet.Cells[7, col] = "Overtime during the day";
                oSheet.Cells[6, col + 1] = "Tăng ca ban đêm";
                oSheet.Cells[7, col + 1] = "Overtime at night";


                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ca đêm)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Số giờ ca đêm";
                oSheet.Cells[7, col] = "Night shift hours";
                oSheet.Cells[6, col + 1] = "Tăng ca ca đêm";
                oSheet.Cells[7, col + 1] = "Night shift overtime ";

                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (ngày thường)";
                oSheet.Cells[7, col] = "Total overtime hours (weekdays)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (ngày nghỉ)";
                oSheet.Cells[7, col] = "Total overtime hours (Weekend)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                string CurentColumn = string.Empty;
                for (col = 8; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongTangCaThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBangCongTangCaThang_MT", "lblTIEU_DE") + " " + LK_Thang.EditValue.ToString(); // BẢNG CÔNG TĂNG CA THÁNG
            frm.rpt = new rptBangCongTangCaThang_MT(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangTangCa_MT"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongTangCaThang_SB()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG NGOÀI GIỜ THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTangCaThang_SB(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThangTangCa_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangTongHopDiTreVeSomThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangTHDiTreVeSomThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdo_DiTreVeSom.SelectedIndex;
                cmd.Parameters.Add("@LOAI_DL", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();
                if (dtBCGaiDoan.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay);

                int lastColumn = 0;
                lastColumn = dtBCGaiDoan.Columns.Count;

                int DONG = 0;

                //DONG = Commons.Modules.MExcel.TaoTTChung(oSheet, 1, 2, 1, 4, 0, 0);
                DONG = TaoTTChung_TheoDV(oSheet, 1, 3, 1, dtBCGaiDoan.Columns.Count, 0, 0);
                //DONG = Commons.Modules.MExcel.TaoTTChung(oSheet, 1, 2, 1, dtBCGaiDoan.Columns.Count, 0, 0);
                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 30;
                if (rdo_DiTreVeSom.SelectedIndex == 0)
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP ĐI TRỄ (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }
                else if (rdo_DiTreVeSom.SelectedIndex == 1)
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP VỀ SỚM (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }
                else
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP ĐI TRỄ, VỀ SỚM (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }



                Microsoft.Office.Interop.Excel.Range row5_TieuDe = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[5, 1]];
                row5_TieuDe = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[5, 1]];
                row5_TieuDe.Merge();
                row5_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Stt";
                row5_TieuDe.Interior.Color = Color.FromArgb(198, 224, 180);

                Microsoft.Office.Interop.Excel.Range row5_TieuDe1 = oSheet.Range[oSheet.Cells[4, 2], oSheet.Cells[5, 2]];
                row5_TieuDe1.Merge();
                row5_TieuDe1.Font.Name = fontName;
                row5_TieuDe1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe1.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe1.Font.Bold = true;
                row5_TieuDe1.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe1.Value2 = "Mã số NV";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe2 = oSheet.Range[oSheet.Cells[4, 3], oSheet.Cells[5, 3]];
                row5_TieuDe2.Merge();
                row5_TieuDe2.Font.Name = fontName;
                row5_TieuDe2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe2.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe2.Font.Bold = true;
                row5_TieuDe2.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe2.Value2 = "Họ tên";

                //Microsoft.Office.Interop.Excel.Range row5_TieuDe3 = oSheet.Range[oSheet.Cells[4, 4], oSheet.Cells[5, 4]];
                //row5_TieuDe3.Merge();
                //row5_TieuDe3.Font.Name = fontName;
                //row5_TieuDe3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //row5_TieuDe3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //row5_TieuDe3.Font.Bold = true;
                //row5_TieuDe3.Interior.Color = Color.FromArgb(198, 224, 180);
                //row5_TieuDe3.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");

                Microsoft.Office.Interop.Excel.Range row5_TieuDe4 = oSheet.Range[oSheet.Cells[4, 4], oSheet.Cells[5, 4]];
                row5_TieuDe4.Merge();
                row5_TieuDe4.Font.Name = fontName;
                row5_TieuDe4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe4.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe4.Font.Bold = true;
                row5_TieuDe4.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe4.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");

                Microsoft.Office.Interop.Excel.Range formatRange;
                int col = 5;

                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.EditValue).AddDays(iTNgay - 1).Day;
                    oSheet.Cells[4, col].Font.Name = fontName;
                    oSheet.Cells[4, col].Font.Bold = true;
                    oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                    oSheet.Cells[4, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[4, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    switch (rdo_DiTreVeSom.SelectedIndex)
                    {
                        case 0:
                            {
                                //oSheet.Cells[5, col] = "Đi trễ";

                                //oSheet.Cells[5, col].Font.Bold = true;
                                //oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                                //oSheet.Cells[5, col].Font.Name = fontName;
                                //oSheet.Cells[5, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                //oSheet.Cells[5, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 5;
                                col = col + 1;
                                break;
                            }
                        case 1:
                            {
                                //oSheet.Cells[5, col] = "Về sớm";

                                //oSheet.Cells[5, col].Font.Bold = true;
                                //oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                                //oSheet.Cells[5, col].Font.Name = fontName;
                                //oSheet.Cells[5, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                //oSheet.Cells[5, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 5;
                                col = col + 1;
                                break;
                            }
                        default:
                            {
                                oSheet.Cells[5, col] = "Đi trễ";

                                oSheet.Cells[5, col].Font.Bold = true;
                                oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                                oSheet.Cells[5, col].Font.Name = fontName;
                                oSheet.Cells[5, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                oSheet.Cells[5, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                                oSheet.Cells[5, col + 1] = "Về sớm";
                                //oSheet.Cells[6, col + 1] = "Early leave";
                                oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                                //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                                oSheet.Cells[5, col + 1].Font.Bold = true;
                                //oSheet.Cells[6, col + 1].Font.Bold = true;
                                oSheet.Cells[5, col + 1].Font.Name = fontName;
                                //oSheet.Cells[6, col + 1].Font.Name = fontName;
                                oSheet.Cells[5, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                oSheet.Cells[5, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                //oSheet.Cells[6, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                                //oSheet.Cells[6, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                                oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();
                                col = col + 2;
                                break;
                            }
                    }
                    //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();

                    iTNgay++;
                }

                switch (rdo_DiTreVeSom.SelectedIndex)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    default:
                        {

                            oSheet.Cells[4, col] = "Đi trễ";
                            oSheet.Cells[4, col].Font.Name = fontName;
                            oSheet.Cells[4, col].Font.Bold = true;
                            oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                            oSheet.Cells[4, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[4, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                            oSheet.Cells[5, col] = "Số lần đi trễ";
                            //oSheet.Cells[6, col] = "Number of being late";
                            //oSheet.Cells[6, col].Interior.Color = Color.Yellow;
                            oSheet.Cells[5, col].Font.Bold = true;
                            //oSheet.Cells[6, col].Font.Bold = true;
                            oSheet.Cells[5, col].RowHeight = 20;
                            oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                            oSheet.Cells[5, col].Font.Name = fontName;
                            //oSheet.Cells[6, col].Font.Name = fontName;
                            oSheet.Cells[5, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[5, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            //oSheet.Cells[6, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            //oSheet.Cells[6, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                            oSheet.Cells[5, col + 1] = "Số giờ đi trễ";
                            //oSheet.Cells[6, col + 1] = "Number of minutes late";
                            oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                            //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                            oSheet.Cells[5, col + 1].Font.Bold = true;
                            //oSheet.Cells[6, col + 1].Font.Bold = true;
                            oSheet.Cells[5, col + 1].RowHeight = 20;
                            oSheet.Cells[5, col + 1].Font.Name = fontName;
                            //oSheet.Cells[6, col + 1].Font.Name = fontName;
                            oSheet.Cells[5, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[5, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            //oSheet.Cells[6, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            //oSheet.Cells[6, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                            oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                            formatRange = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]];
                            formatRange.ColumnWidth = 15;

                            oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();
                            formatRange = oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]];
                            formatRange.ColumnWidth = 15;


                            col = col + 2;
                            oSheet.Cells[4, col] = "Về sớm";
                            oSheet.Cells[4, col].Font.Name = fontName;
                            oSheet.Cells[4, col].Font.Bold = true;

                            oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                            oSheet.Cells[4, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[4, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                            oSheet.Cells[5, col] = "Số lần về sớm";
                            //oSheet.Cells[6, col] = "Number of early leave";
                            oSheet.Cells[5, col].Font.Bold = true;
                            //oSheet.Cells[6, col].Font.Bold = true;
                            oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                            //oSheet.Cells[6, col].Interior.Color = Color.Yellow;

                            oSheet.Cells[5, col].RowHeight = 20;
                            oSheet.Cells[5, col].Font.Name = fontName;
                            oSheet.Cells[5, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[5, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            //oSheet.Cells[6, col].Font.Name = fontName;
                            //oSheet.Cells[6, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            //oSheet.Cells[6, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;



                            oSheet.Cells[5, col + 1] = "Số giờ về sớm";
                            //oSheet.Cells[6, col + 1] = "Minutes leaving early";
                            oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                            //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                            oSheet.Cells[5, col + 1].RowHeight = 20;
                            oSheet.Cells[5, col + 1].Font.Bold = true;
                            oSheet.Cells[5, col + 1].Font.Name = fontName;
                            oSheet.Cells[5, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            oSheet.Cells[5, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            //oSheet.Cells[6, col + 1].Font.Bold = true;
                            //oSheet.Cells[6, col + 1].Font.Name = fontName;
                            //oSheet.Cells[6, col + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                            //oSheet.Cells[6, col + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                            oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                            oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                            formatRange = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]];
                            formatRange.ColumnWidth = 15;


                            oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();
                            formatRange = oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]];
                            formatRange.ColumnWidth = 15;

                            col = col + 2;
                            break;
                        }
                }



                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Cells[4, col] = "Tổng số lần";
                //oSheet.Cells[6, col] = "Total number of times";
                oSheet.Cells[4, col].Font.Name = fontName;
                oSheet.Cells[4, col].Font.Bold = true;
                //oSheet.Cells[6, col].Font.Name = fontName;
                //oSheet.Cells[6, col].Font.Bold = true;
                oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col].Interior.Color = Color.Yellow;
                oSheet.Cells[4, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[4, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]];
                formatRange.ColumnWidth = 10;

                oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[4, col + 1] = "Tổng số giờ";
                //oSheet.Cells[6, col + 1] = "Total minutes";
                oSheet.Cells[4, col + 1].Font.Name = fontName;
                oSheet.Cells[4, col + 1].Font.Bold = true;
                //oSheet.Cells[6, col + 1].Font.Name = fontName;
                //oSheet.Cells[6, col + 1].Font.Bold = true;
                oSheet.Cells[4, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                oSheet.Cells[4, col + 1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[4, col + 1].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col + 1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col + 1].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]];
                formatRange.ColumnWidth = 10;

                oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]].Merge();





                DataRow[] dr = dtBCGaiDoan.Select();
                string[,] rowData = new string[dr.Length, dtBCGaiDoan.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCGaiDoan.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt + 5, lastColumn]].Value2 = rowData;
                //oSheet.get_Range("A6", lastColumn + (rowCnt + 5).ToString()).Value2 = rowData;
                rowCnt = rowCnt + 7;
                //for (col = 5; col <= lastColumn; col++)
                //{
                //    formatRange = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[rowCnt, col]];
                //    formatRange.NumberFormat = "0.0;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                if (rdo_DiTreVeSom.SelectedIndex == 2)
                {
                    formatRange = oSheet.Range[oSheet.Cells[6, lastColumn - 5], oSheet.Cells[rowCnt, lastColumn - 5]];
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    formatRange = oSheet.Range[oSheet.Cells[6, lastColumn - 3], oSheet.Cells[rowCnt, lastColumn - 3]];
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[6, lastColumn - 1], oSheet.Cells[rowCnt, lastColumn - 1]];
                formatRange.NumberFormat = "0.0;-0;;@";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "6"); //27 + 31
                //row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                //row5_TieuDe_Format.Font.Name = fontName;
                //row5_TieuDe_Format.Font.Bold = true;
                //row5_TieuDe_Format.WrapText = true;
                //row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //row5_TieuDe_Format.Interior.Color = Color.Yellow;

                ////Kẻ khung toàn bộ
                formatRange = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Borders.Color = Color.Black;
                //dữ liệu
                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt
                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 4;
                //ma nv
                formatRange = oSheet.Range[oSheet.Cells[6, 2], oSheet.Cells[rowCnt, 2]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 9;
                //ho ten
                formatRange = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[rowCnt, 3]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 21;
                ////xí nghiệp
                //formatRange = oSheet.Range[oSheet.Cells[6, 4], oSheet.Cells[rowCnt, 4]];
                //formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                //formatRange.ColumnWidth = 40;
                //tổ
                formatRange = oSheet.Range[oSheet.Cells[6, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 24;

                //CẠNH giữ côt động
                formatRange = oSheet.Range[oSheet.Cells[6, 5], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                rowCnt = rowCnt + 3;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.Value2 = "Ngày ... Tháng ... Năm";
                formatRange.Font.Size = 11;
                formatRange.Font.Name = fontName;
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 20], oSheet.Cells[rowCnt, 20]];
                formatRange.Value2 = "Người duyệt";
                formatRange.Font.Size = 11;
                formatRange.Font.Name = fontName;
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.Value2 = "Người lập";
                formatRange.Font.Size = 11;
                formatRange.Font.Name = fontName;
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                oSheet.PageSetup.FitToPagesWide = 1;
                oSheet.PageSetup.FitToPagesTall = false;
                oSheet.PageSetup.Zoom = false;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                oSheet.PageSetup.LeftMargin = oXL.InchesToPoints(0.75);
                oSheet.PageSetup.RightMargin = oXL.InchesToPoints(0.75);
                oSheet.PageSetup.TopMargin = oXL.InchesToPoints(0.75);
                oSheet.PageSetup.BottomMargin = oXL.InchesToPoints(0.75);
                oSheet.PageSetup.HeaderMargin = oXL.InchesToPoints(0.3);
                oSheet.PageSetup.FooterMargin = oXL.InchesToPoints(0.3);
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                Commons.Modules.ObjSystems.HideWaitForm();
                oXL.Visible = true;

            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }
        private void BangTongHopDiTreVeSomThang_MT()
        {

        }
        private void BangTongHopCongThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangTongCongThang_DM"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTu ngay den ngay khong hop le"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;

                int TotalColumn = 24;

                int DONG = 0;

                DONG = TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                //=====

                Microsoft.Office.Interop.Excel.Range row3_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[3, 1], oSheet.Cells[3, (TotalColumn)]];
                row3_TieuDe_BaoCao.Merge();
                row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row3_TieuDe_BaoCao.Font.Name = fontName;
                row3_TieuDe_BaoCao.Font.Bold = true;
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP CÔNG THEO PHÒNG CHUYỀN THÁNG (" + LK_Thang.Text + ")";

                Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[6, (TotalColumn)]]; //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                oSheet.get_Range("A6").RowHeight = 40;
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1 = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[6, 1]];
                row5_TieuDe_Cot1.Merge();
                row5_TieuDe_Cot1.Value2 = "Stt";
                row5_TieuDe_Cot1.ColumnWidth = 10;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot2 = oSheet.Range[oSheet.Cells[5, 2], oSheet.Cells[6, 2]];
                row5_TieuDe_Cot2.Merge();
                row5_TieuDe_Cot2.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_Cot2.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot3 = oSheet.Range[oSheet.Cells[5, 3], oSheet.Cells[6, 3]];
                row5_TieuDe_Cot3.Merge();
                row5_TieuDe_Cot3.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_Cot3.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot4 = oSheet.Range[oSheet.Cells[5, 4], oSheet.Cells[6, 4]];
                row5_TieuDe_Cot4.Merge();
                row5_TieuDe_Cot4.Value2 = "Công chuẩn tháng";
                row5_TieuDe_Cot4.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot5 = oSheet.Range[oSheet.Cells[5, 5], oSheet.Cells[6, 5]];
                row5_TieuDe_Cot5.Merge();
                row5_TieuDe_Cot5.Value2 = "LĐ T.tế";
                row5_TieuDe_Cot5.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot6 = oSheet.Range[oSheet.Cells[5, 6], oSheet.Cells[6, 6]];
                row5_TieuDe_Cot6.Merge();
                row5_TieuDe_Cot6.Value2 = "LĐ BQ";
                row5_TieuDe_Cot6.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot79 = oSheet.Range[oSheet.Cells[5, 7], oSheet.Cells[5, 9]];
                row5_TieuDe_Cot79.Merge();
                row5_TieuDe_Cot79.Value2 = "Lao động tăng";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot7 = oSheet.Range[oSheet.Cells[6, 7], oSheet.Cells[6, 7]];
                row5_TieuDe_Cot7.ColumnWidth = 6;
                row5_TieuDe_Cot7.Value2 = "Tổng";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot8 = oSheet.Range[oSheet.Cells[6, 8], oSheet.Cells[6, 8]];
                row5_TieuDe_Cot8.ColumnWidth = 6;
                row5_TieuDe_Cot8.Value2 = "HĐLĐ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot9 = oSheet.Range[oSheet.Cells[6, 9], oSheet.Cells[6, 9]];
                row5_TieuDe_Cot9.ColumnWidth = 6;
                row5_TieuDe_Cot9.Value2 = "HĐTV";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1012 = oSheet.Range[oSheet.Cells[5, 10], oSheet.Cells[5, 12]];
                row5_TieuDe_Cot1012.Merge();
                row5_TieuDe_Cot1012.Value2 = "Lao động giảm";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot10 = oSheet.Range[oSheet.Cells[6, 10], oSheet.Cells[6, 10]];
                row5_TieuDe_Cot10.ColumnWidth = 6;
                row5_TieuDe_Cot10.Value2 = "Tổng";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot11 = oSheet.Range[oSheet.Cells[6, 11], oSheet.Cells[6, 11]];
                row5_TieuDe_Cot11.ColumnWidth = 6;
                row5_TieuDe_Cot11.Value2 = "Bỏ việc";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot12 = oSheet.Range[oSheet.Cells[6, 12], oSheet.Cells[6, 12]];
                row5_TieuDe_Cot12.ColumnWidth = 6;
                row5_TieuDe_Cot12.Value2 = "Nghỉ việc";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot13 = oSheet.Range[oSheet.Cells[5, 13], oSheet.Cells[6, 13]];
                row5_TieuDe_Cot13.Merge();
                row5_TieuDe_Cot13.ColumnWidth = 8;
                row5_TieuDe_Cot13.Value2 = "Công trong tháng";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1417 = oSheet.Range[oSheet.Cells[5, 14], oSheet.Cells[5, 18]];
                row5_TieuDe_Cot1417.Merge();
                row5_TieuDe_Cot1417.Value2 = "Công trong giờ và ngoài giờ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot14 = oSheet.Range[oSheet.Cells[6, 14], oSheet.Cells[6, 14]];
                row5_TieuDe_Cot14.ColumnWidth = 8;
                row5_TieuDe_Cot14.Value2 = "Trong giờ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot15 = oSheet.Range[oSheet.Cells[6, 15], oSheet.Cells[6, 15]];
                row5_TieuDe_Cot15.ColumnWidth = 8;
                row5_TieuDe_Cot15.Value2 = "1,5";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot16 = oSheet.Range[oSheet.Cells[6, 16], oSheet.Cells[6, 16]];
                row5_TieuDe_Cot16.ColumnWidth = 8;
                row5_TieuDe_Cot16.Value2 = "2";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot17 = oSheet.Range[oSheet.Cells[6, 17], oSheet.Cells[6, 17]];
                row5_TieuDe_Cot17.ColumnWidth = 8;
                row5_TieuDe_Cot17.Value2 = "3";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot18 = oSheet.Range[oSheet.Cells[6, 18], oSheet.Cells[6, 18]];
                row5_TieuDe_Cot18.ColumnWidth = 8;
                row5_TieuDe_Cot18.Value2 = "Tổng";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot19 = oSheet.Range[oSheet.Cells[5, 19], oSheet.Cells[6, 19]];
                row5_TieuDe_Cot19.Merge();
                row5_TieuDe_Cot19.ColumnWidth = 8;
                row5_TieuDe_Cot19.Value2 = "% Công thực tế so với công trong tháng";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1926 = oSheet.Range[oSheet.Cells[5, 20], oSheet.Cells[5, 24]];
                row5_TieuDe_Cot1926.Merge();
                row5_TieuDe_Cot1926.Value2 = "Các loại công vắng mặt";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot20 = oSheet.Range[oSheet.Cells[6, 20], oSheet.Cells[6, 20]];
                row5_TieuDe_Cot20.Value2 = "Tổng";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot21 = oSheet.Range[oSheet.Cells[6, 21], oSheet.Cells[6, 21]];
                row5_TieuDe_Cot21.Value2 = "HL";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot22 = oSheet.Range[oSheet.Cells[6, 22], oSheet.Cells[6, 22]];
                row5_TieuDe_Cot22.Value2 = "KL";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot23 = oSheet.Range[oSheet.Cells[6, 23], oSheet.Cells[6, 23]];
                row5_TieuDe_Cot23.Value2 = "O";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot24 = oSheet.Range[oSheet.Cells[6, 24], oSheet.Cells[6, 24]];
                row5_TieuDe_Cot24.Value2 = "P";

                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.Range[oSheet.Cells[5, 20], oSheet.Cells[5, 24]];
                formatRange.ColumnWidth = 6;


                DataRow[] dr = dtBCThang.Select();
                int sDonVi = 0;
                int rowCnt = 7;
                int dem = 1;
                foreach (DataRow row in dr)
                {
                    if (Convert.ToInt32(row["ID_DV"].ToString()) != sDonVi)
                    {
                        Microsoft.Office.Interop.Excel.Range row_DonVi = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 3]];
                        row_DonVi.Merge();
                        row_DonVi.Value2 = row["TEN_DV"].ToString();
                        rowCnt++;
                    }

                    Microsoft.Office.Interop.Excel.Range row_A = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]];
                    row_A.Value2 = dem;
                    Microsoft.Office.Interop.Excel.Range row_B = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]];
                    row_B.Value2 = row["TEN_XN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_C = oSheet.Range[oSheet.Cells[rowCnt, 3], oSheet.Cells[rowCnt, 3]];
                    row_C.Value2 = row["TEN_TO"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_D = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 4]];
                    row_D.Value2 = row["CONG_CHUAN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_E = oSheet.Range[oSheet.Cells[rowCnt, 5], oSheet.Cells[rowCnt, 5]];
                    row_E.Value2 = row["LDTT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_F = oSheet.Range[oSheet.Cells[rowCnt, 6], oSheet.Cells[rowCnt, 6]];
                    row_F.Value2 = "=M" + rowCnt + "/D" + rowCnt;
                    Microsoft.Office.Interop.Excel.Range row_G = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]];
                    row_G.Value2 = "=SUM(H" + rowCnt + ":I" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_H = oSheet.Range[oSheet.Cells[rowCnt, 8], oSheet.Cells[rowCnt, 8]];
                    row_H.Value2 = row["LD_TANG_CN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_I = oSheet.Range[oSheet.Cells[rowCnt, 9], oSheet.Cells[rowCnt, 9]];
                    row_I.Value2 = row["LD_TANG_DT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_J = oSheet.Range[oSheet.Cells[rowCnt, 10], oSheet.Cells[rowCnt, 10]];
                    row_J.Value2 = "=SUM(K" + rowCnt + ":L" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_K = oSheet.Range[oSheet.Cells[rowCnt, 11], oSheet.Cells[rowCnt, 11]];
                    row_K.Value2 = row["LD_GIAM_BV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_L = oSheet.Range[oSheet.Cells[rowCnt, 12], oSheet.Cells[rowCnt, 12]];
                    row_L.Value2 = row["LD_GIAM_NV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_M = oSheet.Range[oSheet.Cells[rowCnt, 13], oSheet.Cells[rowCnt, 13]];
                    row_M.Value2 = "=N" + rowCnt + "+T" + rowCnt;
                    Microsoft.Office.Interop.Excel.Range row_N = oSheet.Range[oSheet.Cells[rowCnt, 14], oSheet.Cells[rowCnt, 14]];
                    row_N.Value2 = row["SN_LV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_O = oSheet.Range[oSheet.Cells[rowCnt, 15], oSheet.Cells[rowCnt, 15]];
                    row_O.Value2 = row["SN_TC_NT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_P = oSheet.Range[oSheet.Cells[rowCnt, 16], oSheet.Cells[rowCnt, 16]];
                    row_P.Value2 = row["SN_TC_CN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_Q = oSheet.Range[oSheet.Cells[rowCnt, 17], oSheet.Cells[rowCnt, 17]];
                    row_Q.Value2 = row["SN_TC_NL"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_R = oSheet.Range[oSheet.Cells[rowCnt, 18], oSheet.Cells[rowCnt, 18]];
                    row_R.Value2 = "=SUM(N" + rowCnt + ":Q" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_S = oSheet.Range[oSheet.Cells[rowCnt, 19], oSheet.Cells[rowCnt, 19]];
                    row_S.Value2 = "=(R" + rowCnt + "/M" + rowCnt + ")*100";
                    Microsoft.Office.Interop.Excel.Range row_T = oSheet.Range[oSheet.Cells[rowCnt, 20], oSheet.Cells[rowCnt, 20]];
                    row_T.Value2 = "=SUM(U" + rowCnt + ":X" + rowCnt + ")"; ;
                    Microsoft.Office.Interop.Excel.Range row_U = oSheet.Range[oSheet.Cells[rowCnt, 21], oSheet.Cells[rowCnt, 21]];
                    row_U.Value2 = row["SNV_HL"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_V = oSheet.Range[oSheet.Cells[rowCnt, 22], oSheet.Cells[rowCnt, 22]];
                    row_V.Value2 = row["SNV_KL"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_W = oSheet.Range[oSheet.Cells[rowCnt, 23], oSheet.Cells[rowCnt, 23]];
                    row_W.Value2 = row["SNV_O"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_X = oSheet.Range[oSheet.Cells[rowCnt, 24], oSheet.Cells[rowCnt, 24]];
                    row_X.Value2 = row["SNV_P"].ToString();

                    dem++;
                    rowCnt++;
                    sDonVi = Convert.ToInt32(row["ID_DV"].ToString());

                }

                //Kẻ khung toàn bộ
                //Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt--;
                formatRange = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt, TotalColumn]];


                formatRange.Borders.Color = Color.Black;

                formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                formatRange = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[rowCnt, 12]];
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                formatRange = oSheet.Range[oSheet.Cells[7, 13], oSheet.Cells[rowCnt, TotalColumn]];
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                //dữ liệu
                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, TotalColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                ////CẠNH giữ côt động
                formatRange = oSheet.Range[oSheet.Cells[3, 6], oSheet.Cells[4, TotalColumn]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Commons.Modules.ObjSystems.HideWaitForm();
                oXL.Visible = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void BangTongHopCongThang_MT()
        {

        }
        private void BaoCaoNghiBoViecThang()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt;
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                string sTieuDe = "BÁO CÁO NGHỈ VIỆC";
                frm.rpt = new rptBaoCaoNghiBoViecThang(Convert.ToDateTime(LK_Thang.EditValue), sTieuDe, Convert.ToInt32(LK_DON_VI.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBaoCaoNghiBoViecThang", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue).ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue).ToString("yyyy/MM/dd");
                    cmd.CommandType = CommandType.StoredProcedure;


                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                        return;
                    }
                    dt.TableName = "DA_TA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BaoCaoNghiBoViecThang_MT()
        {

        }
        private void DanhSachChuyenCongTac()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            string sTieuDe = "BẢNG CHẤM CÔNG NHÂN VIÊN CHUYỂN CÔNG TÁC THÁNG";
            frm.rpt = new rptDSChuyenCongTac(lk_DenNgay.DateTime, sTieuDe, Convert.ToDateTime(NgayIn.EditValue));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDanhSachChuyenCongTac"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
            }
            catch
            { }
            frm.ShowDialog();

        }
        private void DanhSachChuyenCongTac_MT()
        {

        }
        private void BangChenhLechTangCaThang()
        {

        }
        private void BangChenhLechTangCaThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHÊNH LỆCH TĂNG CA ";
            frm.rpt = new rptBCChenhLechTangCa(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue), Convert.ToInt32(LK_DON_VI.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongChenhLechThang_MT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void DanhSachThang_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dt_TongGioiTinh_Nu = new DataTable();
                dt_TongGioiTinh_Nu = ds.Tables[1].Copy();

                DataTable dt_TongGioiTinh_Nam = new DataTable();
                dt_TongGioiTinh_Nam = ds.Tables[2].Copy();

                DataTable dtSLTO = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLTO = ds.Tables[3].Copy();
                int slto = Convert.ToInt32(dtSLTO.Rows[0][0]);

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row1_TenDV = oSheet.get_Range("B1");
                row1_TenDV.Merge();
                row1_TenDV.Font.Size = 9;
                row1_TenDV.Font.Name = fontName;
                row1_TenDV.Value2 = dtBCThang.Rows[0]["TEN_DV"];
                row1_TenDV.WrapText = false;
                row1_TenDV.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row1_DiaChiDV = oSheet.get_Range("B2");
                row1_DiaChiDV.Merge();
                row1_DiaChiDV.Font.Size = 9;
                row1_DiaChiDV.Font.Name = fontName;
                row1_DiaChiDV.Font.Italic = true;
                row1_DiaChiDV.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row1_DiaChiDV.WrapText = false;
                row1_DiaChiDV.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row1_TieuDe_BaoCao = oSheet.get_Range("H2", "U2");
                row1_TieuDe_BaoCao.Merge();
                row1_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row1_TieuDe_BaoCao.Font.Name = fontName;
                row1_TieuDe_BaoCao.Font.Bold = true;
                row1_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row1_TieuDe_BaoCao.RowHeight = 15;
                row1_TieuDe_BaoCao.Value2 = "LIST OF WORKER " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MMM").ToUpper() + " " + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";
                row1_TieuDe_BaoCao.Font.Color = Color.FromArgb(0, 0, 255);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("H3", "U3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold Italic";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 15;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH NHÂN VIÊN " + Convert.ToDateTime(lk_TuNgay.EditValue).Month + "  NĂM " + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";
                row2_TieuDe_BaoCao.Font.Color = Color.FromArgb(0, 0, 255);

                Range row2_TieuDe_THANG = oSheet.get_Range("AH2", "AI2");
                row2_TieuDe_THANG.Merge();
                row2_TieuDe_THANG.Value2 = "THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).Month + "/" + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";

                Range row2_TieuDe_Nam = oSheet.get_Range("AD3");
                row2_TieuDe_Nam.Value2 = "Nam";

                // Tinh trong sql
                Range row2_TieuDe_CountNam = oSheet.get_Range("AE3");
                row2_TieuDe_CountNam.Value2 = dt_TongGioiTinh_Nam.Rows.Count == 0 ? 0 : dt_TongGioiTinh_Nam.Rows[0][0];

                Range row2_TieuDe_NU = oSheet.get_Range("AF3");
                row2_TieuDe_NU.Value2 = "Nữ";

                // Tinh trong sql
                Range row2_TieuDe_CountNu = oSheet.get_Range("AG3");
                row2_TieuDe_CountNu.Value2 = dt_TongGioiTinh_Nu.Rows.Count == 0 ? 0 : dt_TongGioiTinh_Nu.Rows[0][0];

                // SUM
                Range row2_TieuDe_Tong = oSheet.get_Range("AI3");
                row2_TieuDe_Tong.Value2 = "=SUM(AE3,AG3)";
                row2_TieuDe_Tong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_Tong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //Range row4_TieuDe_Format = oSheet.get_Range("A4", "S4"); //27 + 31
                //row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                //row4_TieuDe_Format.Font.Name = fontName;
                //row4_TieuDe_Format.Font.Bold = true;
                //row4_TieuDe_Format.WrapText = true;
                //row4_TieuDe_Format.NumberFormat = "@";
                //row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //row4_TieuDe_Format.Font.Color = Color.FromArgb(255, 0, 0);

                Range row5_TieuDe_Format = oSheet.get_Range("A5", "AI5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Font.Color = Color.FromArgb(0, 0, 255);


                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Value2 = "No";

                Range row5_TieuDe_HoTen = oSheet.get_Range("B5");
                row5_TieuDe_HoTen.Value2 = "Fullname(Họ tên)";
                row5_TieuDe_HoTen.ColumnWidth = 25;

                Range row5_TieuDe_CODE = oSheet.get_Range("C5");
                row5_TieuDe_CODE.Value2 = "CODE";

                Range row5_TieuDe_gioitinh = oSheet.get_Range("D5");
                row5_TieuDe_gioitinh.Value2 = "Giới tính";

                int col = 5;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    //Range row6_b = oSheet.get_Range(oSheet.Cells[6, col + 1]);
                    //row6_b.Value2 = "b";
                    //row6_b.Interior.Color = Color.FromArgb(128, 255, 128);
                    //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]];
                    col += 1;
                    iTNgay++;
                }

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                                  //int rowCONG = 0; // Row để insert dòng tổng
                                  //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 6;
                string cotCN = "";
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data


                for (int i = 0; i < TEN_XN.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_XN[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            if (Convert.ToInt32(row[0]) == 1)
                            {
                                if (row[col].ToString() == "0")
                                {
                                    //cotCN = cotCN + (col + 1) + ",";
                                    cotCN = CharacterIncrement(col);
                                    Range ToMau = oSheet.get_Range("" + cotCN + "5", cotCN + "" + (dt_temp.Rows.Count + 5 + (slto)) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                                                                                                                           //Range ToMau = oSheet.get_Range("" + cotCN + "5", cotCN + "" + (dt_temp.Rows.Count + 6) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                    ToMau.Interior.Color = Color.FromArgb(0, 176, 80);
                                    //ToMau.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                }
                            }
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        //rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                    }
                    //rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    rowBD = rowBD + dr_Cu + rowBD_XN;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;


                    // Tạo group tổ
                    Range row_groupTO_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupTO_Format.Font.Color = Color.FromArgb(0, 0, 255);
                    row_groupTO_Format.Font.Name = fontName;
                    row_groupTO_Format.Font.Bold = true;
                    oSheet.Cells[rowBD, 1] = TEN_XN[i].ToString();

                    //Đổ dữ liệu của tổ
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;


                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt = keepRowCnt + 2;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 5;
                int colKT = dtBCThang.Columns.Count;
                //format

                for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                formatRange = oSheet.get_Range("A6", lastColumn + (rowCnt - 1).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString()));
                // filter
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoTongHopThang_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangChamCongThang_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgayCT = string.Empty;
                lastColumNgayCT = CharacterIncrement(iSoNgay + 22); // 23 cột đầu là cố định tinh tu 0
                string firstColumTC = string.Empty;
                firstColumTC = CharacterIncrement(iSoNgay + 23); // 23 cột đầu là cố định + 1 sau dòng cuối ngày công thưong tin tu 0

                Range ROWA1 = oSheet.get_Range("A1");
                ROWA1.Font.Size = 9;
                ROWA1.Font.Bold = true;
                ROWA1.Font.Name = fontName;
                ROWA1.Value2 = "P: Nghỉ phép năm";
                ROWA1.WrapText = false;


                Range ROWA2 = oSheet.get_Range("A2");
                ROWA2.Font.Size = 9;
                ROWA2.Font.Bold = true;
                ROWA2.Font.Name = fontName;
                ROWA2.Value2 = "KL: nghỉ không lương (đơn vị Ngày";
                ROWA2.WrapText = false;

                Range ROWA3 = oSheet.get_Range("A3");
                ROWA3.Font.Size = 9;
                ROWA3.Font.Bold = true;
                ROWA3.Font.Name = fontName;
                ROWA3.Value2 = "O: Nghỉ vô lý do (đơn vị Ngày)";
                ROWA3.WrapText = false;

                Range ROWA4 = oSheet.get_Range("A3");
                ROWA4.Font.Size = 9;
                ROWA4.Font.Bold = true;
                ROWA4.Font.Name = fontName;
                ROWA4.Value2 = "HL: Nghỉ có hưởng lương";
                ROWA4.WrapText = false;

                Range ROWD1 = oSheet.get_Range("D1");
                ROWD1.Font.Size = 9;
                ROWD1.Font.Bold = true;
                ROWD1.Font.Name = fontName;
                ROWD1.RowHeight = 24;
                ROWD1.Value2 = "1/2P,4.5: nghỉ nửa ngày phép,làm 4.5 giờ";
                ROWD1.WrapText = false;
                ROWD1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ROWD1.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Range ROWD2 = oSheet.get_Range("D2");
                ROWD2.Font.Size = 9;
                ROWD2.Font.Bold = true;
                ROWD2.Font.Name = fontName;
                ROWD2.RowHeight = 24;
                ROWD2.Value2 = "1/2P,5.1: nghỉ nửa ngày phép,làm 5.1giờ";
                ROWD2.WrapText = false;
                ROWD2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ROWD2.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range ROWD3 = oSheet.get_Range("D3");
                ROWD3.Font.Size = 9;
                ROWD3.Font.Bold = true;
                ROWD3.RowHeight = 24;
                ROWD3.Font.Name = fontName;
                ROWD3.Value2 = "1/2P,5.1: nghỉ nửa ngày phép,làm 5.1giờ";
                ROWD3.WrapText = false;
                ROWD3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ROWD3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range ROWD4 = oSheet.get_Range("D4");
                ROWD4.Font.Size = 9;
                ROWD4.Font.Bold = true;
                ROWD4.RowHeight = 24;
                ROWD4.Font.Name = fontName;
                ROWD4.Value2 = "1/2P,5.35: nghỉ nửa ngày phép,làm 5.35 giờ";
                ROWD4.WrapText = false;
                ROWD4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ROWD4.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range ROWV1 = oSheet.get_Range("V1");
                ROWV1.Font.Size = 20;
                ROWV1.Font.Bold = true;
                ROWV1.Font.Name = fontName;
                ROWV1.Value2 = "BẢNG CHẤM CÔNG THÁNG (" + LK_Thang.Text + ")";
                ROWV1.WrapText = false;


                Range ROWY2 = oSheet.get_Range("Y2");
                ROWY2.Font.Size = 9;
                ROWY2.Font.Bold = true;
                ROWY2.Font.Name = fontName;
                ROWY2.Value2 = Convert.ToDateTime(lk_TuNgay.EditValue).Year;
                ROWY2.WrapText = false;


                Range row3_TieuDeCT_Format = oSheet.get_Range("Y3", lastColumNgayCT + "3"); //27 + 31
                row3_TieuDeCT_Format.Font.Size = 9;
                row3_TieuDeCT_Format.Font.Name = fontName;
                row3_TieuDeCT_Format.Merge();
                row3_TieuDeCT_Format.Font.Bold = true;
                row3_TieuDeCT_Format.WrapText = true;
                row3_TieuDeCT_Format.Value2 = "NGÀY CÔNG THƯỜNG TRONG THÁNG (ĐƠN VỊ TÍNH GIỜ)";
                row3_TieuDeCT_Format.NumberFormat = "@";
                row3_TieuDeCT_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDeCT_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                //Cột ngày công thường
                Range row4_TieuDeCT_Format = oSheet.get_Range("Y4", lastColumNgayCT + "4"); //27 + 31
                row4_TieuDeCT_Format.Font.Size = 9;
                row4_TieuDeCT_Format.Font.Name = fontName;
                row4_TieuDeCT_Format.NumberFormat = "d";
                row4_TieuDeCT_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDeCT_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                int col = 25;
                bool vongdau = false;
                string sTenCot = "";
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }
                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";

                string LastColumn_Temp = "";
                LastColumn_Temp = CharacterIncrement(dtBCThang.Columns.Count - 2);

                Range row3_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "3", LastColumn_Temp + "3"); //27 + 31
                row3_TieuDeTC_Format.Font.Size = 9;
                row3_TieuDeTC_Format.Font.Name = fontName;
                row3_TieuDeTC_Format.Merge();
                row3_TieuDeTC_Format.Font.Bold = true;
                row3_TieuDeTC_Format.WrapText = true;
                row3_TieuDeTC_Format.Value2 = "LÀM THÊM GIỜ (ĐƠN VỊ TÍNH GIỜ)";
                row3_TieuDeTC_Format.NumberFormat = "@";
                row3_TieuDeTC_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDeTC_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row3_TieuDeTC_Format.Interior.Color = Color.FromArgb(248, 203, 173);



                //Cột tăng ca

                Range row4_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "4", LastColumn_Temp + "4"); //27 + 31
                row4_TieuDeTC_Format.Font.Size = 9;
                row4_TieuDeTC_Format.Font.Name = fontName;
                row4_TieuDeTC_Format.NumberFormat = "d";
                row4_TieuDeTC_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDeTC_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                col += 1;
                vongdau = false;
                sTenCot = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }

                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";


                Range row3Ky_Nhan = oSheet.get_Range(lastColumn + "3");
                row3Ky_Nhan.Value2 = "Ký nhận";
                row3Ky_Nhan.ColumnWidth = 11;
                row3Ky_Nhan.Font.Bold = true;
                row3Ky_Nhan.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3Ky_Nhan.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;



                Range row5_TieuDe_Format = oSheet.get_Range("A5", "X5"); //27 + 31
                row5_TieuDe_Format.Font.Size = 9;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.RowHeight = 60;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row6_TieuDe_Format = oSheet.get_Range("A6", "X6"); //27 + 31
                row6_TieuDe_Format.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 4;
                row5_TieuDe_STT.Interior.Color = Color.FromArgb(255, 255, 255);

                Range row5_TieuDe_A6 = oSheet.get_Range("A6");
                row5_TieuDe_A6.Interior.Color = Color.FromArgb(255, 255, 255);


                Range row5_TieuDe_HoTen = oSheet.get_Range("B5");
                row5_TieuDe_HoTen.Value2 = "Họ tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;


                Range row5_TieuDe_MSCN = oSheet.get_Range("C5");
                row5_TieuDe_MSCN.Value2 = "Mã nhân viên";
                row5_TieuDe_MSCN.ColumnWidth = 15;

                Range row5_TieuDe_BP = oSheet.get_Range("D5");
                row5_TieuDe_BP.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_BP.ColumnWidth = 12;

                Range row5_TieuDe_PB = oSheet.get_Range("E5");
                row5_TieuDe_PB.Value2 = "Phân bổ";
                row5_TieuDe_PB.ColumnWidth = 12;

                Range row5_TieuDe_CV = oSheet.get_Range("F5");
                row5_TieuDe_CV.Value2 = "Chức vụ";
                row5_TieuDe_CV.ColumnWidth = 15;

                Range row5_TieuDe_TT = oSheet.get_Range("G5");
                row5_TieuDe_TT.Value2 = "Tình trạng";
                row5_TieuDe_TT.ColumnWidth = 15;

                Range row5_TieuDe_GT = oSheet.get_Range("H5");
                row5_TieuDe_GT.Value2 = "Giới tính";
                row5_TieuDe_GT.ColumnWidth = 10;

                Range row5_TieuDe_NL = oSheet.get_Range("I5");
                row5_TieuDe_NL.Value2 = "Ngày vào";
                row5_TieuDe_NL.ColumnWidth = 10;

                Range row5_TieuDe_NKHDLD = oSheet.get_Range("J5");
                row5_TieuDe_NKHDLD.Value2 = "Ngày ký HĐLĐ";
                row5_TieuDe_NKHDLD.ColumnWidth = 10;

                Range row5_TieuDe_NLVC = oSheet.get_Range("K5");
                row5_TieuDe_NLVC.Value2 = "Phân bổ lương";
                row5_TieuDe_NLVC.ColumnWidth = 12;

                Range row5_TieuDe_TGNN = oSheet.get_Range("L5");
                row5_TieuDe_TGNN.Value2 = "Thời gian nghỉ ngắn";
                row5_TieuDe_TGNN.ColumnWidth = 8;
                row5_TieuDe_TGNN.Font.Color = Color.FromArgb(255, 0, 0);


                Range row5_TieuDe_TNCLV = oSheet.get_Range("M5");
                row5_TieuDe_TNCLV.Value2 = "Tổng Ngày công làm việc";
                row5_TieuDe_TNCLV.ColumnWidth = 9;
                row5_TieuDe_TNCLV.Font.Color = Color.FromArgb(255, 0, 0);


                Range row5_TieuDe_CGLVTT = oSheet.get_Range("N5");
                row5_TieuDe_CGLVTT.Value2 = "Giờ công làm việc thực tế";
                row5_TieuDe_CGLVTT.ColumnWidth = 8;

                Range row5_TieuDe_GCCD = oSheet.get_Range("O5");
                row5_TieuDe_GCCD.Value2 = "Giờ công chế độ";
                row5_TieuDe_GCCD.ColumnWidth = 8;

                Range row5_TieuDe_HL = oSheet.get_Range("P5");
                row5_TieuDe_HL.Value2 = "HL";
                row5_TieuDe_HL.ColumnWidth = 8;

                Range row5_TieuDe_KL = oSheet.get_Range("Q5");
                row5_TieuDe_KL.Value2 = "KL";
                row5_TieuDe_KL.ColumnWidth = 6;

                Range row5_TieuDe_O = oSheet.get_Range("R5");
                row5_TieuDe_O.Value2 = "O";
                row5_TieuDe_O.ColumnWidth = 8;

                Range row5_TieuDe_P = oSheet.get_Range("S5");
                row5_TieuDe_P.Value2 = "P";
                row5_TieuDe_P.ColumnWidth = 8;

                Range row5_TieuDe_GLTNT = oSheet.get_Range("T5");
                row5_TieuDe_GLTNT.Value2 = "Giờ làm thêm ngày thường";
                row5_TieuDe_GLTNT.ColumnWidth = 9;

                Range row5_TieuDe_GLTNNHT = oSheet.get_Range("U5");
                row5_TieuDe_GLTNNHT.Value2 = "Giờ làm thêm ngày nghỉ hàng tuần";
                row5_TieuDe_GLTNNHT.ColumnWidth = 9;

                Range row5_TieuDe_V5 = oSheet.get_Range("V5");
                row5_TieuDe_V5.Value2 = "Giờ tăng ca ngày lễ";
                row5_TieuDe_V5.ColumnWidth = 9;

                Range row5_TieuDe_W5 = oSheet.get_Range("W5");
                row5_TieuDe_W5.Value2 = "Tăng ca 150% không làm ra SP";
                row5_TieuDe_W5.ColumnWidth = 9;

                Range row5_TieuDe_X5 = oSheet.get_Range("X5");
                row5_TieuDe_X5.Value2 = "Tổng giờ ko làm ra sản phẩm (Offtime)";
                row5_TieuDe_X5.ColumnWidth = 10;

                // Thứ ngày cho cột công thường
                Range row5_TieuDeCT_Format = oSheet.get_Range("Y5", lastColumNgayCT + "5"); //27 + 31
                row5_TieuDeCT_Format.Font.Size = 9;
                row5_TieuDeCT_Format.Font.Name = fontName;
                row5_TieuDeCT_Format.Orientation = 90;
                row5_TieuDeCT_Format.ColumnWidth = 5;
                row5_TieuDeCT_Format.NumberFormat = "dd";
                row5_TieuDeCT_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDeCT_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom;

                string sThu = "";
                int col_r5 = 25;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //sTenCot = CharacterIncrement(col_r5 - 1);
                //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";



                //Thứ ngày cho cột tăng ca

                Range row5_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "5", LastColumn_Temp + "5"); //27 + 31
                row5_TieuDeTC_Format.Font.Size = 9;
                row5_TieuDeTC_Format.Font.Name = fontName;
                row5_TieuDeTC_Format.Orientation = 90;
                row5_TieuDeTC_Format.ColumnWidth = 5;
                row5_TieuDeTC_Format.NumberFormat = "dd";
                row5_TieuDeTC_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDeTC_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom;

                //col_r5 += 1;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);

                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //sTenCot = CharacterIncrement(col_r5 - 1);
                //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";


                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 5;
                oSheet.Application.ActiveWindow.FreezePanes = true;

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        //if (col == 10 && row[10].ToString() != "")
                        //{
                        //    sTenCot = CharacterIncrement(6);
                        //    Microsoft.Office.Interop.Excel.Range formatRange7;
                        //    formatRange7 = oSheet.get_Range(sTenCot + ((rowCnt + 1) + 6).ToString());
                        //    formatRange7.Interior.Color = Color.FromArgb(255, 204, 204);
                        //    formatRange7.Font.Color = Color.FromArgb(156, 0, 6);
                        //}
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;


                col_r5 = 25;
                sThu = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Microsoft.Office.Interop.Excel.Range formatRange5;
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //col_r5 += 1;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Microsoft.Office.Interop.Excel.Range formatRange5;
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(198, 224, 180);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(198, 224, 180);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt++;
                rowCnt++;
                Range rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                rowTONG_CONG.Value2 = "Tổng";
                rowTONG_CONG.Font.Bold = true;

                for (int colSUM = 12; colSUM < dtBCThang.Columns.Count - 1; colSUM++)
                {
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 7, colSUM) + ":" + CellAddress(oSheet, rowCnt - 2, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                rowCnt++;

                string CurentColumn = string.Empty;
                int colBD = 11;
                int colKT = dtBCThang.Columns.Count;


                //format
                for (col = colBD; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }



                //Từ stt đến - Chức vụ
                formatRange = oSheet.get_Range("A6", "F" + (rowCnt - 1).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                //Từ STT - Mã số CN
                Microsoft.Office.Interop.Excel.Range formatRange3;
                formatRange3 = oSheet.get_Range("A6", "C" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("B6", "B" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // PHANBO -  đến hết
                Microsoft.Office.Interop.Excel.Range formatRange1;
                formatRange1 = oSheet.get_Range("E6", lastColumn + (rowCnt - 1).ToString());
                formatRange1.Font.Name = fontName;
                formatRange1.Font.Size = 9;

                // COT X -  đến hết
                Microsoft.Office.Interop.Excel.Range formatRange4;
                formatRange4 = oSheet.get_Range("Y6", lastColumn + (rowCnt - 1).ToString());
                formatRange4.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange4.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                //object result = myRange.AutoFilter(1,"HO_TEN");

                BorderAround(oSheet.get_Range("Y3", lastColumn + "4"));
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString()));
                Microsoft.Office.Interop.Excel.Range myRange = oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString());
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

                // filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                Commons.Modules.ObjSystems.HideWaitForm();
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }
        private string CellAddress(Microsoft.Office.Interop.Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private void BangTongHopCongThang_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangTongCongThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTu ngay den ngay khong hop le"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;

                int TotalColumn = 24;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(TotalColumn - 1);

                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "BÁO CÁO LAO ĐỘNG THÁNG (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";

                //=====

                Microsoft.Office.Interop.Excel.Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row3_TieuDe_BaoCao.Merge();
                row3_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row3_TieuDe_BaoCao.Font.Name = fontName;
                row3_TieuDe_BaoCao.Font.Bold = true;
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.Value2 = "Công trong tháng (" + Convert.ToInt16((Convert.ToDateTime(LK_Thang.EditValue).AddMonths(1).AddDays(-1)).Day) + ")";

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                oSheet.get_Range("A6").RowHeight = 40;
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1 = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Cot1.Merge();
                row5_TieuDe_Cot1.Value2 = "Stt";
                row5_TieuDe_Cot1.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot2 = oSheet.get_Range("B5", "B6");
                row5_TieuDe_Cot2.Merge();
                row5_TieuDe_Cot2.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                row5_TieuDe_Cot2.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot3 = oSheet.get_Range("C5", "C6");
                row5_TieuDe_Cot3.Merge();
                row5_TieuDe_Cot3.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_Cot3.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot4 = oSheet.get_Range("D5", "D6");
                row5_TieuDe_Cot4.Merge();
                row5_TieuDe_Cot4.Value2 = "Công trong tháng";
                row5_TieuDe_Cot4.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot5 = oSheet.get_Range("E5", "E6");
                row5_TieuDe_Cot5.Merge();
                row5_TieuDe_Cot5.Value2 = "LĐ T.tế";
                row5_TieuDe_Cot5.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot6 = oSheet.get_Range("F5", "F6");
                row5_TieuDe_Cot6.Merge();
                row5_TieuDe_Cot6.Value2 = "LĐ BQ";
                row5_TieuDe_Cot6.ColumnWidth = 8;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot79 = oSheet.get_Range("G5", "I5");
                row5_TieuDe_Cot79.Merge();
                row5_TieuDe_Cot79.Value2 = "Lao động tăng";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot7 = oSheet.get_Range("G6", "G6");
                row5_TieuDe_Cot7.ColumnWidth = 6;
                row5_TieuDe_Cot7.Value2 = "+";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot8 = oSheet.get_Range("H6", "H6");
                row5_TieuDe_Cot8.ColumnWidth = 6;
                row5_TieuDe_Cot8.Value2 = "CN";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot9 = oSheet.get_Range("I6", "I6");
                row5_TieuDe_Cot9.ColumnWidth = 6;
                row5_TieuDe_Cot9.Value2 = "Đào tạo";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1012 = oSheet.get_Range("J5", "L5");
                row5_TieuDe_Cot1012.Merge();
                row5_TieuDe_Cot1012.Value2 = "Lao động giảm";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot10 = oSheet.get_Range("J6");
                row5_TieuDe_Cot10.ColumnWidth = 6;
                row5_TieuDe_Cot10.Value2 = "+";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot11 = oSheet.get_Range("K6");
                row5_TieuDe_Cot11.ColumnWidth = 6;
                row5_TieuDe_Cot11.Value2 = "BV";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot12 = oSheet.get_Range("L6");
                row5_TieuDe_Cot12.ColumnWidth = 6;
                row5_TieuDe_Cot12.Value2 = "NV";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot13 = oSheet.get_Range("M5", "M6");
                row5_TieuDe_Cot13.Merge();
                row5_TieuDe_Cot13.ColumnWidth = 8;
                row5_TieuDe_Cot13.Value2 = "Công chế độ";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1417 = oSheet.get_Range("N5", "Q5");
                row5_TieuDe_Cot1417.Merge();
                row5_TieuDe_Cot1417.Value2 = "Công thực tế ngoài giờ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot14 = oSheet.get_Range("N6");
                row5_TieuDe_Cot14.ColumnWidth = 8;
                row5_TieuDe_Cot14.Value2 = "Trong giờ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot15 = oSheet.get_Range("O6");
                row5_TieuDe_Cot15.ColumnWidth = 8;
                row5_TieuDe_Cot15.Value2 = "1,5";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot16 = oSheet.get_Range("P6");
                row5_TieuDe_Cot16.ColumnWidth = 8;
                row5_TieuDe_Cot16.Value2 = "2";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot17 = oSheet.get_Range("Q6");
                row5_TieuDe_Cot17.ColumnWidth = 8;
                row5_TieuDe_Cot17.Value2 = "+";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot18 = oSheet.get_Range("R5", "R6");
                row5_TieuDe_Cot18.Merge();
                row5_TieuDe_Cot18.ColumnWidth = 8;
                row5_TieuDe_Cot18.Value2 = "% Công thực tế so công chế độ";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot1926 = oSheet.get_Range("S5", "X5");
                row5_TieuDe_Cot1926.Merge();
                row5_TieuDe_Cot1926.Value2 = "Các loại công vắng mặt";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot19 = oSheet.get_Range("S6");
                row5_TieuDe_Cot19.Value2 = "+";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot20 = oSheet.get_Range("T6");
                row5_TieuDe_Cot20.Value2 = "F";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot21 = oSheet.get_Range("U6");
                row5_TieuDe_Cot21.Value2 = "CĐ";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot22 = oSheet.get_Range("V6");
                row5_TieuDe_Cot22.Value2 = "KL";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot23 = oSheet.get_Range("W6");
                row5_TieuDe_Cot23.Value2 = "BHXH";
                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Cot24 = oSheet.get_Range("X6");
                row5_TieuDe_Cot24.Value2 = "VLD";

                Microsoft.Office.Interop.Excel.Range formatRange;
                formatRange = oSheet.get_Range("S5", "X5");
                formatRange.ColumnWidth = 6;


                DataRow[] dr = dtBCThang.Select();
                int sDonVi = 0;
                int rowCnt = 7;
                int dem = 1;
                foreach (DataRow row in dr)
                {
                    if (Convert.ToInt32(row["ID_DV"].ToString()) != sDonVi)
                    {
                        Microsoft.Office.Interop.Excel.Range row_DonVi = oSheet.get_Range("B" + rowCnt, "C" + rowCnt);
                        row_DonVi.Merge();
                        row_DonVi.Value2 = row["TEN_DV"].ToString();
                        rowCnt++;
                    }

                    Microsoft.Office.Interop.Excel.Range row_A = oSheet.get_Range("A" + rowCnt);
                    row_A.Value2 = dem;
                    Microsoft.Office.Interop.Excel.Range row_B = oSheet.get_Range("B" + rowCnt);
                    row_B.Value2 = row["TEN_XN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_C = oSheet.get_Range("C" + rowCnt);
                    row_C.Value2 = row["TEN_TO"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_D = oSheet.get_Range("D" + rowCnt);
                    row_D.Value2 = row["CONG_CHUAN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_E = oSheet.get_Range("E" + rowCnt);
                    row_E.Value2 = row["LDTT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_F = oSheet.get_Range("F" + rowCnt);
                    row_F.Value2 = "=M" + rowCnt + "/D" + rowCnt;
                    Microsoft.Office.Interop.Excel.Range row_G = oSheet.get_Range("G" + rowCnt);
                    row_G.Value2 = "=SUM(H" + rowCnt + ":I" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_H = oSheet.get_Range("H" + rowCnt);
                    row_H.Value2 = row["LD_TANG_CN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_I = oSheet.get_Range("I" + rowCnt);
                    row_I.Value2 = row["LD_TANG_DT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_J = oSheet.get_Range("J" + rowCnt);
                    row_J.Value2 = "=SUM(K" + rowCnt + ":L" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_K = oSheet.get_Range("K" + rowCnt);
                    row_K.Value2 = row["LD_GIAM_BV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_L = oSheet.get_Range("L" + rowCnt);
                    row_L.Value2 = row["LD_GIAM_NV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_M = oSheet.get_Range("M" + rowCnt);
                    row_M.Value2 = "=N" + rowCnt + "+S" + rowCnt;
                    Microsoft.Office.Interop.Excel.Range row_N = oSheet.get_Range("N" + rowCnt);
                    row_N.Value2 = row["SN_LV"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_O = oSheet.get_Range("O" + rowCnt);
                    row_O.Value2 = row["SN_TC_NT"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_P = oSheet.get_Range("P" + rowCnt);
                    row_P.Value2 = row["SN_TC_CN"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_Q = oSheet.get_Range("Q" + rowCnt);
                    row_Q.Value2 = "=SUM(N" + rowCnt + ":P" + rowCnt + ")";
                    Microsoft.Office.Interop.Excel.Range row_R = oSheet.get_Range("R" + rowCnt);
                    row_R.Value2 = "=Q" + rowCnt + "/M" + rowCnt + "*100";
                    Microsoft.Office.Interop.Excel.Range row_S = oSheet.get_Range("S" + rowCnt);
                    row_S.Value2 = "=SUM(T" + rowCnt + ":X" + rowCnt + ")"; ;
                    Microsoft.Office.Interop.Excel.Range row_T = oSheet.get_Range("T" + rowCnt);
                    row_T.Value2 = row["SNV_P"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_U = oSheet.get_Range("U" + rowCnt);
                    row_U.Value2 = row["SNV_CD"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_V = oSheet.get_Range("V" + rowCnt);
                    row_V.Value2 = row["SNV_KL"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_W = oSheet.get_Range("W" + rowCnt);
                    row_W.Value2 = row["SNV_BHXH"].ToString();
                    Microsoft.Office.Interop.Excel.Range row_X = oSheet.get_Range("X" + rowCnt);
                    row_X.Value2 = row["SNV_VLD"].ToString();

                    dem++;
                    rowCnt++;
                    sDonVi = Convert.ToInt32(row["ID_DV"].ToString());

                }

                //Kẻ khung toàn bộ
                //Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt--;
                formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());


                formatRange.Borders.Color = Color.Black;

                formatRange = oSheet.get_Range("F7", "F" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                formatRange = oSheet.get_Range("G7", "L" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                formatRange = oSheet.get_Range("M7", lastColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                //dữ liệu
                formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                ////CẠNH giữ côt động
                formatRange = oSheet.get_Range("F3", lastColumn + "4");
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oXL.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        private void BaoXacNhanCongThang_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCSLXacNhanCongThang_DM", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;
                int lastColumNgayCT = 0;
                lastColumNgayCT = iSoNgay + 22; // 23 cột đầu là cố định tinh tu 0
                int firstColumTC = 0;
                firstColumTC = iSoNgay + 23; // 23 cột đầu là cố định + 1 sau dòng cuối ngày công thưong tin tu 0

                oSheet.Cells[1, 1].Value2 = "Xác nhận công giờ đến: Đ";
                oSheet.Cells[1, 1].Font.Size = 9;
                oSheet.Cells[1, 1].Font.Bold = true;
                oSheet.Cells[1, 1].Font.Name = fontName;
                oSheet.Cells[1, 1].WrapText = false;


                oSheet.Cells[2, 1].Font.Size = 9;
                oSheet.Cells[2, 1].Font.Bold = true;
                oSheet.Cells[2, 1].Font.Name = fontName;
                oSheet.Cells[2, 1].Value2 = "Xác nhận công giờ về: V";
                oSheet.Cells[2, 1].WrapText = false;

                oSheet.Cells[3, 1].Font.Size = 9;
                oSheet.Cells[3, 1].Font.Bold = true;
                oSheet.Cells[3, 1].Font.Name = fontName;
                oSheet.Cells[3, 1].Value2 = "Xác nhận công buổi trưa ăn cơm: T";
                oSheet.Cells[3, 1].WrapText = false;

                oSheet.Cells[4, 1].Font.Size = 9;
                oSheet.Cells[4, 1].Font.Bold = true;
                oSheet.Cells[4, 1].Font.Name = fontName;
                oSheet.Cells[4, 1].Value2 = "Quên thẻ,mất thẻ cả ngày: Q";
                oSheet.Cells[4, 1].WrapText = false;

                oSheet.Cells[1, 4].Font.Size = 9;
                oSheet.Cells[1, 4].Font.Bold = true;
                oSheet.Cells[1, 4].Font.Name = fontName;
                oSheet.Cells[1, 4].RowHeight = 24;
                oSheet.Cells[1, 4].Value2 = "11:30";
                oSheet.Cells[1, 4].WrapText = false;
                oSheet.Cells[1, 4].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 4].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 4].Font.Size = 9;
                oSheet.Cells[2, 4].Font.Bold = true;
                oSheet.Cells[2, 4].Font.Name = fontName;
                oSheet.Cells[2, 4].RowHeight = 24;
                oSheet.Cells[2, 4].Value2 = "T";
                oSheet.Cells[2, 4].WrapText = false;
                oSheet.Cells[2, 4].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 4].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 5].Font.Size = 9;
                oSheet.Cells[1, 5].Font.Bold = true;
                oSheet.Cells[1, 5].Font.Name = fontName;
                oSheet.Cells[1, 5].Value2 = "11:45";
                oSheet.Cells[1, 5].WrapText = false;
                oSheet.Cells[1, 5].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 5].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 5].Font.Size = 9;
                oSheet.Cells[2, 5].Font.Bold = true;
                oSheet.Cells[2, 5].Font.Name = fontName;
                oSheet.Cells[2, 5].RowHeight = 24;
                oSheet.Cells[2, 5].Value2 = "T";
                oSheet.Cells[2, 5].WrapText = false;
                oSheet.Cells[2, 5].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 5].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 6].Font.Size = 9;
                oSheet.Cells[1, 6].Font.Bold = true;
                oSheet.Cells[1, 6].Font.Name = fontName;
                oSheet.Cells[1, 6].Value2 = "12:00";
                oSheet.Cells[1, 6].WrapText = false;
                oSheet.Cells[1, 6].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 6].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 6].Font.Size = 9;
                oSheet.Cells[2, 6].Font.Bold = true;
                oSheet.Cells[2, 6].Font.Name = fontName;
                oSheet.Cells[2, 6].Value2 = "T";
                oSheet.Cells[2, 6].WrapText = false;
                oSheet.Cells[2, 6].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 6].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 7].Font.Size = 9;
                oSheet.Cells[1, 7].Font.Bold = true;
                oSheet.Cells[1, 7].Font.Name = fontName;
                oSheet.Cells[1, 7].Value2 = "17:36";
                oSheet.Cells[1, 7].WrapText = false;
                oSheet.Cells[1, 7].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 7].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 7].Font.Size = 9;
                oSheet.Cells[2, 7].Font.Bold = true;
                oSheet.Cells[2, 7].Font.Name = fontName;
                oSheet.Cells[2, 7].Value2 = "V";
                oSheet.Cells[2, 7].WrapText = false;
                oSheet.Cells[2, 7].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 7].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 8].Font.Size = 9;
                oSheet.Cells[1, 8].Font.Bold = true;
                oSheet.Cells[1, 8].Font.Name = fontName;
                oSheet.Cells[1, 8].Value2 = "07:30";
                oSheet.Cells[1, 8].WrapText = false;
                oSheet.Cells[1, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 8].Font.Size = 9;
                oSheet.Cells[2, 8].Font.Bold = true;
                oSheet.Cells[2, 8].Font.Name = fontName;
                oSheet.Cells[2, 8].Value2 = "Đ";
                oSheet.Cells[2, 8].WrapText = false;
                oSheet.Cells[2, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 9].Font.Size = 9;
                oSheet.Cells[1, 9].Font.Bold = true;
                oSheet.Cells[1, 9].Font.Name = fontName;
                oSheet.Cells[1, 9].Value2 = "12:15";
                oSheet.Cells[1, 9].WrapText = false;
                oSheet.Cells[1, 9].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[1, 9].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[2, 9].Font.Size = 9;
                oSheet.Cells[2, 9].Font.Bold = true;
                oSheet.Cells[2, 9].Font.Name = fontName;
                oSheet.Cells[2, 9].Value2 = "T";
                oSheet.Cells[2, 9].WrapText = false;
                oSheet.Cells[2, 9].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[2, 9].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[1, 22].Font.Size = 16;
                oSheet.Cells[1, 22].Font.Bold = true;
                oSheet.Cells[1, 22].Font.Name = fontName;
                oSheet.Cells[1, 22].Value2 = "BẢNG XÁC NHẬN CÔNG THÁNG (" + LK_Thang.Text + ")";
                oSheet.Cells[1, 22].WrapText = false;

                Range ROWtIEUDE = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 11]];
                ROWtIEUDE.Font.Size = fontSizeTieuDe;
                ROWtIEUDE.Font.Bold = true;
                ROWtIEUDE.Font.Name = fontName;
                ROWtIEUDE.WrapText = false;
                ROWtIEUDE.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ROWtIEUDE.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[5, 1].Value2 = "STT";
                oSheet.Cells[5, 1].ColumnWidth = 10;

                oSheet.Cells[5, 2].Value2 = "Mã nhân viên";

                oSheet.Cells[5, 3].Value2 = "Họ tên";
                oSheet.Cells[5, 3].ColumnWidth = 30;

                oSheet.Cells[5, 4].Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblXiNghiep");
                oSheet.Cells[5, 4].ColumnWidth = 25;

                oSheet.Cells[5, 5].Value2 = "Ngày vào làm";
                oSheet.Cells[5, 5].ColumnWidth = 16;

                oSheet.Cells[5, 6].Value2 = "Công nhân nhân viên";
                oSheet.Cells[5, 6].ColumnWidth = 40;

                oSheet.Cells[5, 7].Value2 = "Tổng số lần xác nhận";
                oSheet.Cells[5, 7].ColumnWidth = 12;

                oSheet.Cells[5, 8].Value2 = "Giờ đến";
                oSheet.Cells[5, 8].ColumnWidth = 12;

                oSheet.Cells[5, 9].Value2 = "Giờ về";
                oSheet.Cells[5, 9].ColumnWidth = 12;

                oSheet.Cells[5, 10].Value2 = "Giờ đi ăn trưa";
                oSheet.Cells[5, 10].ColumnWidth = 12;

                oSheet.Cells[5, 11].Value2 = "Quên thẻ/Mất thẻ";
                oSheet.Cells[5, 11].ColumnWidth = 12;

                //Cột ngày
                Range row4_TieuDeCT_Format = oSheet.Range[oSheet.Cells[4, 12], oSheet.Cells[4, lastColumn]];
                row4_TieuDeCT_Format.Font.Size = 9;
                row4_TieuDeCT_Format.Font.Name = fontName;
                row4_TieuDeCT_Format.NumberFormat = "d";
                row4_TieuDeCT_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDeCT_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                int col = 12;
                bool vongdau = false;
                string sTenCot = "";
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }
                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";

                // Thứ ngày cho cột công thường
                Range row5_TieuDeCT_Format = oSheet.Range[oSheet.Cells[5, 12], oSheet.Cells[5, lastColumn]];
                row5_TieuDeCT_Format.Font.Size = 9;
                row5_TieuDeCT_Format.Font.Name = fontName;
                row5_TieuDeCT_Format.Orientation = 90;
                row5_TieuDeCT_Format.ColumnWidth = 5;
                row5_TieuDeCT_Format.NumberFormat = "dd";
                row5_TieuDeCT_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDeCT_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom;

                string sThu = "";
                int col_r5 = 12;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();
                    int iNgayNghi = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[fnKiemTraNgayNghiTuan]('" + Convert.ToDateTime(dt).ToString("MM/dd/yyyy") + "')"));

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (iNgayNghi == 1)
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    }
                    //if (sThu == "Sunday")
                    //{
                    //    oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                    //    oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    //}
                    //if (sThu == "Saturday")
                    //{
                    //    oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                    //    oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    //}
                    col_r5 += 1;
                    iTNgay++;
                }


                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.SplitRow = 5;
                oSheet.Application.ActiveWindow.FreezePanes = true;

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                //oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;

                col_r5 = 12;
                sThu = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();
                    int iNgayNghi = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[fnKiemTraNgayNghiTuan]('" + Convert.ToDateTime(dt).ToString("MM/dd/yyyy") + "')"));
                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (iNgayNghi == 1)
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Microsoft.Office.Interop.Excel.Range formatRange5;
                        formatRange5 = oSheet.Range[oSheet.Cells[6, col_r5], oSheet.Cells[rowCnt, col_r5]];
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.Range[oSheet.Cells[6, col_r5], oSheet.Cells[rowCnt, col_r5]];
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }
                Microsoft.Office.Interop.Excel.Range formatRange;
                rowCnt++;
                string CurentColumn = string.Empty;
                int colBD = 12;
                int colKT = dtBCThang.Columns.Count;

                formatRange = oSheet.Range[oSheet.Cells[5, 2], oSheet.Cells[rowCnt + 1, lastColumn]];
                formatRange.Columns.AutoFit();

                //format
                for (col = colBD; col <= dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[rowCnt, col]];
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignBottom;
                    formatRange.WrapText = true;
                    formatRange.ColumnWidth = 8;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //Từ stt đến - Chức vụ
                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt - 1, 6]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                //Từ STT - Mã số CN
                Microsoft.Office.Interop.Excel.Range formatRange3;
                formatRange3 = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt - 1, 2]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[rowCnt - 1, 4]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.Range[oSheet.Cells[6, 5], oSheet.Cells[rowCnt - 1, 5]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.Range[oSheet.Cells[6, 6], oSheet.Cells[rowCnt - 1, 6]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.Range[oSheet.Cells[6, 7], oSheet.Cells[rowCnt - 1, 11]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // PHANBO -  đến hết
                Microsoft.Office.Interop.Excel.Range formatRange1;
                formatRange1 = oSheet.Range[oSheet.Cells[6, 5], oSheet.Cells[rowCnt - 1, lastColumn]];
                formatRange1.Font.Name = fontName;
                formatRange1.Font.Size = 9;

                // COT L -  đến hết

                formatRange3 = oSheet.Range[oSheet.Cells[6, 12], oSheet.Cells[rowCnt - 1, lastColumn]];
                formatRange3.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                //object result = myRange.AutoFilter(1,"HO_TEN");

                BorderAround(oSheet.Range[oSheet.Cells[4, 12], oSheet.Cells[4, lastColumn]]);
                BorderAround(oSheet.Range[oSheet.Cells[1, 4], oSheet.Cells[2, 9]]);
                BorderAround(oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt - 1, lastColumn]]);
                Microsoft.Office.Interop.Excel.Range myRange = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[rowCnt - 1, lastColumn]];
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);


                Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 1);

                int DONG = 0;

                DONG = TaoTTChung_TheoDV(oSheet, 1, 2, 1, 5, 0, 0);

                this.Cursor = Cursors.Default;
                // filter
                oXL.Visible = true;
                oXL.UserControl = true;
                //oWB.SaveAs("TheSavePath", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefaul);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_AP(int type) // an phát // type = 1 : Chấm công hàng ngày, type = 2: Chấm công hàng ngày + ngoài giờ, type = 3 : Chấm công ngoài giờ
        {
            try
            {
                string storename = "";
                switch (type)
                {
                    case 1:
                        {
                            storename = "rptBangCongThang_AP";
                            break;
                        }
                    case 2:
                        {
                            storename = "rptBangCongThangGio_AP";
                            break;
                        }
                    case 3:
                        {
                            storename = "rptBangCongThangNgoaiGio_AP";
                            break;
                        }
                    default:
                        break;
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;
                DataTable dtBCNghi;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(storename, conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                dtBCNghi = ds.Tables[1].Copy();

                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    this.Cursor = Cursors.Default;
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                DateTime dTNgay = lk_TuNgay.DateTime;
                DateTime dDNgay = lk_DenNgay.DateTime;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                TaoTTChung_TheoDV(oSheet, 1, 3, 1, 8, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = (type == 3 ? "BẢNG CHẤM CÔNG NGOÀI GIỜ THÁNG " : "BẢNG CHẤM CÔNG THÁNG ") + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]]; //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                int col = 1;
                Range row5_TieuDe_Stt = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                col++;
                Range row5_TieuDe_MaSo = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                col++;
                Range row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                col++;
                while (dTNgay <= dDNgay)
                {
                    oSheet.Cells[6, col] = dTNgay.Day;
                    col++;
                    dTNgay = dTNgay.AddDays(1);
                }

                int colFormatRF = col;

                //lay tieu de cot nghi 
                DataRow[] drN = dtBCNghi.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Cells[6, col] = rowN[1].ToString();
                    col++;
                }
                oSheet.Cells[6, col] = "Tổng công";
                if (type == 2 || type == 3)
                {
                    if (type == 2)
                    {
                        col++;
                        oSheet.Cells[6, col] = "Quy công";
                        col++;
                    }
                    oSheet.Cells[6, col] = "Giờ làm thêm BT";
                    col++;
                    oSheet.Cells[6, col] = "Giờ làm thêm CN";
                    col++;
                    oSheet.Cells[6, col] = "Giờ ban đêm";
                    col++;
                    oSheet.Cells[6, col] = "Giờ chế độ";
                }

                if (type == 3)
                {
                    col++;
                    oSheet.Cells[6, col] = "Giờ Lễ, Tết";
                }

                int colFormatRT = col;

                col++;
                oSheet.Cells[6, col] = "Ký tên";
                col++;
                row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HoTen.Value2 = "Bộ phận";
                row5_TieuDe_HoTen.ColumnWidth = 35;

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                int rowCnt = 0;


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                //Đổ dữ liệu của xí nghiệp
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[6 + rowCnt, lastColumn]].Value2 = rowData;

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[4, lastColumn]]; //27 + 31
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6 + rowCnt, lastColumn]]; //27 + 31
                formatRange.Font.Name = fontName;
                for (col = 4; col <= lastColumn - 2; col++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[6 + rowCnt, col]];
                    formatRange.NumberFormat = "General";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                formatRange1 = oSheet.Range[oSheet.Cells[7, colFormatRF], oSheet.Cells[6 + rowCnt, colFormatRT]];
                formatRange1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                dTNgay = lk_TuNgay.DateTime;
                col = 4;
                while (dTNgay <= dDNgay)
                {
                    if (dTNgay.DayOfWeek.ToString() == "Sunday")
                    {
                        formatRange = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6 + rowCnt, col]]; //27 + 31
                        formatRange.Interior.Color = Color.FromArgb(255, 153, 102);
                    }
                    col++;
                    dTNgay = dTNgay.AddDays(1);
                }
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6 + rowCnt, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range myRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6 + rowCnt, lastColumn]];
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BangChamCongThang_VV(int type) // việt vương // type = 1 : Bảng chấm công theo tháng (Giờ công) , type = 2: Bảng chấm công tăng ca theo tháng
        {
            try
            {
                string storename = "";
                switch (type)
                {
                    case 1:
                        {
                            storename = "rptBangCongThangGioCong_VV";
                            break;
                        }
                    case 2:
                        {
                            storename = "rptBangCongThangNgoaiGio_VV";
                            break;
                        }
                    default:
                        break;
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;
                DataTable dtBCNghi;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(storename, conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                dtBCNghi = ds.Tables[1].Copy();

                if (dtBCThang.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                    this.Cursor = Cursors.Default;
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                DateTime dTNgay = lk_TuNgay.DateTime;
                DateTime dDNgay = lk_DenNgay.DateTime;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = type == 1 ? "BẢNG CHẤM CÔNG THÁNG " : "BẢNG CHẤM CÔNG TĂNG CA THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]]; //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                int col = 1;
                Range row5_TieuDe_Stt = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                col++;
                Range row5_TieuDe_MaSo = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                col++;
                Range row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                col++;
                row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HoTen.Value2 = "Bộ phận";
                row5_TieuDe_HoTen.ColumnWidth = 35;

                col++;
                while (dTNgay <= dDNgay)
                {
                    oSheet.Cells[6, col] = dTNgay.Day;
                    col++;
                    dTNgay = dTNgay.AddDays(1);
                }

                int colFormatRF = col;

                //lay tieu de cot nghi 
                if (type == 1)
                {
                    DataRow[] drN = dtBCNghi.Select();
                    foreach (DataRow rowN in drN)
                    {
                        oSheet.Cells[6, col] = rowN[1].ToString();
                        col++;
                    }
                }
                oSheet.Cells[6, col] = "Giờ làm việc";
                col++;
                oSheet.Cells[6, col] = "Giờ làm thêm BT";
                col++;
                oSheet.Cells[6, col] = "Giờ làm thêm CN";
                col++;
                oSheet.Cells[6, col] = "Giờ Lễ, Tết";
                col++;
                oSheet.Cells[6, col] = "Giờ chế độ";
                col++;
                oSheet.Cells[6, col] = "Sau 22h";
                col++;
                oSheet.Cells[6, col] = "Tổng cộng";
                col++;
                oSheet.Cells[6, col] = "Ký tên";
                col++;

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                int rowCnt = 0;


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                //Đổ dữ liệu của xí nghiệp
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[6, lastColumn]]; //format dòng thông tin đơn vị
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]; //27 + 31
                formatRange.Font.Name = fontName;
                for (col = 5; col <= lastColumn - 1; col++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, col], oSheet.Cells[rowCnt, col]];
                    formatRange.NumberFormat = "0.0;-0;;@";
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                formatRange1 = oSheet.Range[oSheet.Cells[7, colFormatRF], oSheet.Cells[rowCnt, lastColumn]];
                formatRange1.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                dTNgay = lk_TuNgay.DateTime;
                col = 5;
                while (dTNgay <= dDNgay)
                {
                    if (dTNgay.DayOfWeek.ToString() == "Sunday")
                    {
                        formatRange = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[rowCnt, col]]; //27 + 31
                        formatRange.Interior.Color = Color.FromArgb(255, 153, 102);
                    }
                    col++;
                    dTNgay = dTNgay.AddDays(1);
                }
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range myRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]];
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

                rowCnt = rowCnt + 2;
                oSheet.Cells[rowCnt, 2].Font.Bold = true;
                oSheet.Cells[rowCnt, 2] = "Mã số";
                oSheet.Cells[rowCnt, 3].Font.Bold = true;
                oSheet.Cells[rowCnt, 3] = "Lý do";

                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8] = "Mã số";
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9] = "Lý do";

                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14] = "Mã số";
                oSheet.Cells[rowCnt, 15].Font.Bold = true;
                oSheet.Cells[rowCnt, 15] = "Lý do";

                oSheet.Cells[rowCnt, 20].Font.Bold = true;
                oSheet.Cells[rowCnt, 20] = "Mã số";
                oSheet.Cells[rowCnt, 21].Font.Bold = true;
                oSheet.Cells[rowCnt, 21] = "Lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "12";
                oSheet.Cells[rowCnt, 3] = "Gia đình có việc";
                oSheet.Cells[rowCnt, 8] = "B";
                oSheet.Cells[rowCnt, 9] = "Nghỉ bệnh";
                oSheet.Cells[rowCnt, 14] = "Cô";
                oSheet.Cells[rowCnt, 15] = "Nghỉ con ốm";
                oSheet.Cells[rowCnt, 20] = "CoN";
                oSheet.Cells[rowCnt, 21] = "Con nhỏ";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "DH";
                oSheet.Cells[rowCnt, 3] = "Nghỉ dài hạn";
                oSheet.Cells[rowCnt, 8] = "Ê";
                oSheet.Cells[rowCnt, 9] = "Nghỉ mất điện";
                oSheet.Cells[rowCnt, 14] = "H";
                oSheet.Cells[rowCnt, 15] = "Nghỉ kết hôn";
                oSheet.Cells[rowCnt, 20] = "HH";
                oSheet.Cells[rowCnt, 21] = "Nghỉ Hội họp, học tập, công tác";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "HV";
                oSheet.Cells[rowCnt, 3] = "Hết việc hưởng 50% lương";
                oSheet.Cells[rowCnt, 8] = "L";
                oSheet.Cells[rowCnt, 9] = "Ngày nghỉ lễ, tết theo BLLĐ";
                oSheet.Cells[rowCnt, 14] = "NB";
                oSheet.Cells[rowCnt, 15] = "Nghỉ bù";
                oSheet.Cells[rowCnt, 20] = "NL";
                oSheet.Cells[rowCnt, 21] = "Nghỉ luôn";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "O";
                oSheet.Cells[rowCnt, 3] = "Vắng mặt";
                oSheet.Cells[rowCnt, 8] = "Ô";
                oSheet.Cells[rowCnt, 9] = "Nghỉ ốm";
                oSheet.Cells[rowCnt, 14] = "P";
                oSheet.Cells[rowCnt, 15] = "Nghỉ phép";
                oSheet.Cells[rowCnt, 20] = "R";
                oSheet.Cells[rowCnt, 21] = "Nghỉ việc riêng không lương";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Ro";
                oSheet.Cells[rowCnt, 3] = "Nghỉ việc riêng không lương";
                oSheet.Cells[rowCnt, 8] = "S";
                oSheet.Cells[rowCnt, 9] = "Nghỉ thai sản";
                oSheet.Cells[rowCnt, 14] = "Tg";
                oSheet.Cells[rowCnt, 15] = "Nghỉ tang lễ";
                oSheet.Cells[rowCnt, 20] = "TS";
                oSheet.Cells[rowCnt, 21] = "Nghỉ thai sản";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "VK";
                oSheet.Cells[rowCnt, 3] = "Nghỉ không do lỗi người LĐ";
                oSheet.Cells[rowCnt, 8] = "VLD";
                oSheet.Cells[rowCnt, 9] = "Nghỉ vô lý do";
                oSheet.Cells[rowCnt, 14] = "VR";
                oSheet.Cells[rowCnt, 15] = "Nghỉ việc riêng có lương";

                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Quảng Nam, Ngày......tháng......năm.......";
                oSheet.Cells[rowCnt, 20] = "Người duyệt";
                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void lk_TuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            DateTime tungay = Convert.ToDateTime(lk_TuNgay.EditValue);
            lk_DenNgay.EditValue = Convert.ToDateTime(DateTime.DaysInMonth(tungay.Year, tungay.Month) + "/" + tungay.Month + "/" + tungay.Year);
            Commons.Modules.sLoad = "";
            LoadGridThongTinNhanVien();
        }
        public int TaoTTChung_TheoDV(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                string sSQL = "SELECT * FROM dbo.DON_VI WHERE ID_DV = " + LK_DON_VI.EditValue;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));

                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_DV"];
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;


                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                DataTable dtLogo = Commons.Modules.ObjSystems.DataThongTinChung(-1);
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtLogo.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
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
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;


                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

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
        private void XacNhanQuetThe_DM(bool F3)
        {
            try
            {
                string strSaveThongTinNhanVien = "rptBangXacNhanGioQuetThe_DM" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, strSaveThongTinNhanVien, Commons.Modules.ObjSystems.ConvertDatatable(grvTTNhanVien), "");
                this.Cursor = Cursors.WaitCursor;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(F3, "rptBangXacNhanGioQuetThe_DM"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@BT", SqlDbType.NVarChar, 50).Value = strSaveThongTinNhanVien;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();


                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);

                //=====

                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "CHI TIẾT CHẤM CÔNG";




                Microsoft.Office.Interop.Excel.Range row2_TieuDe_TUNGAY = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_TUNGAY.Merge();
                row2_TieuDe_TUNGAY.Font.Size = fontSizeTieuDe;
                row2_TieuDe_TUNGAY.Font.Name = fontName;
                row2_TieuDe_TUNGAY.Font.Bold = true;
                row2_TieuDe_TUNGAY.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_TUNGAY.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_TUNGAY.RowHeight = 30;
                row2_TieuDe_TUNGAY.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy") + "";

                Microsoft.Office.Interop.Excel.Range row2_Format_TieuDe = oSheet.get_Range("A3", lastColumn + "3");
                row2_Format_TieuDe.Font.Size = 12;
                row2_Format_TieuDe.Font.Name = fontName;
                row2_Format_TieuDe.Font.Bold = true;
                row2_Format_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_Format_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_Format_TieuDe.Interior.Color = Color.Yellow;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe1 = oSheet.get_Range("A3");
                row5_TieuDe1.Interior.Color = Color.Yellow;
                row5_TieuDe1.Value2 = "Mã số NV";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe2 = oSheet.get_Range("B3");
                row5_TieuDe2.Value2 = "Họ tên";


                Microsoft.Office.Interop.Excel.Range row5_TieuDe3 = oSheet.get_Range("C3");
                row5_TieuDe3.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");

                Microsoft.Office.Interop.Excel.Range row5_TieuDe4 = oSheet.get_Range("D3");
                row5_TieuDe4.Value2 = "Chức vụ";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe5 = oSheet.get_Range("E3");
                row5_TieuDe5.ColumnWidth = 15;
                row5_TieuDe5.Value2 = "Ngày";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe6 = oSheet.get_Range("F3");
                row5_TieuDe6.Value2 = "Thứ";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe7 = oSheet.get_Range(CellAddress(oSheet, 3, dtBCGaiDoan.Columns.Count - 2));
                row5_TieuDe7.Value2 = "Giờ LV";


                Microsoft.Office.Interop.Excel.Range row5_TieuDe8 = oSheet.get_Range(CellAddress(oSheet, 3, dtBCGaiDoan.Columns.Count - 1));
                row5_TieuDe8.Value2 = "Giờ TC";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe9 = oSheet.get_Range(CellAddress(oSheet, 3, dtBCGaiDoan.Columns.Count));
                row5_TieuDe9.Value2 = "Lý do vắng";

                //tô màu
                //Range range = oSheet.get_Range("A" + redRows.ToString(), "J" + redRows.ToString());
                //range.Cells.Interior.Color = System.Drawing.Color.Red;


                Microsoft.Office.Interop.Excel.Range formatRange;
                int col = 7;
                int colvr = 1;
                while (col < dtBCGaiDoan.Columns.Count - 3)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "3");
                    formatRange.Merge();
                    formatRange.Value = "Vào " + colvr.ToString();
                    formatRange.ColumnWidth = 10;

                    formatRange = oSheet.get_Range("" + CharacterIncrement(col) + "3");
                    formatRange.Merge();
                    formatRange.Value = "Ra " + colvr.ToString();
                    formatRange.ColumnWidth = 10;
                    //oSheet.Cells[4, col] = "Vào " + colvr.ToString();
                    //oSheet.Cells[4, col + 1] = "Ra " + colvr.ToString();

                    col = col + 2;
                    colvr++;
                }

                DataRow[] dr = dtBCGaiDoan.Select();
                string[,] rowData = new string[dr.Length, dtBCGaiDoan.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCGaiDoan.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 3;
                oSheet.get_Range("A4", lastColumn + rowCnt.ToString()).Value2 = rowData;

                ////Kẻ khung toàn bộ
                BorderAround(oSheet.get_Range("A3", lastColumn + "" + rowCnt + ""));
                //dữ liệu
                formatRange = oSheet.get_Range("A4", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt
                formatRange = oSheet.get_Range("A4", "A" + rowCnt.ToString());
                formatRange.ColumnWidth = 15;
                //ma nv
                formatRange = oSheet.get_Range("B4", "B" + rowCnt.ToString());
                formatRange.ColumnWidth = 25;
                //ho ten
                formatRange = oSheet.get_Range("C4", "C" + rowCnt.ToString());
                formatRange.ColumnWidth = 20;
                //xí nghiệp
                formatRange = oSheet.get_Range("D4", "D" + rowCnt.ToString());
                formatRange.ColumnWidth = 20;
                //tổ
                formatRange = oSheet.get_Range("E4", "E" + rowCnt.ToString());
                formatRange.EntireColumn.NumberFormat = "DD/MM/YYYY";
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                formatRange.ColumnWidth = 12;

                //CẠNH giữa côt động
                formatRange = oSheet.get_Range("F4", lastColumn + rowCnt.ToString());
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 10;

                formatRange = oSheet.get_Range("M4", "M" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                formatRange.ColumnWidth = 10;

                formatRange = oSheet.get_Range("N4", "N" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                formatRange.ColumnWidth = 10;

                formatRange = oSheet.get_Range("O4", "O" + rowCnt.ToString());
                formatRange.ColumnWidth = 25;
                for (int i = 7; i < dtBCGaiDoan.Columns.Count - 3; i++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(i - 1) + "4", "" + CharacterIncrement(i - 1) + "" + rowCnt.ToString());
                    formatRange.EntireColumn.NumberFormat = "hh:mm";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                Microsoft.Office.Interop.Excel.Range myRange = oSheet.get_Range("A3", lastColumn + (rowCnt).ToString());
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void XacNhanQuetThe()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;
                string sBT = "rptBangXacNhanGioQuetThe" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvTTNhanVien), "");

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangXacNhanGioQuetThe"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();
                DataTable dtBCNghi = new DataTable();
                dtBCNghi = ds.Tables[1].Copy();

                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                oXL.Visible = false;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                this.Cursor = Cursors.WaitCursor;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                Excel.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                //=====

                Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Excel.Range row5_TieuDe = oSheet.get_Range("A4", "A5");
                row5_TieuDe.Merge();
                row5_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Stt";

                Excel.Range row5_TieuDe1 = oSheet.get_Range("B4", "B5");
                row5_TieuDe1.Merge();
                row5_TieuDe1.Font.Name = fontName;
                row5_TieuDe1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe1.Font.Bold = true;

                row5_TieuDe1.Value2 = "Mã số NV";

                Excel.Range row5_TieuDe2 = oSheet.get_Range("C4", "C5");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Font.Name = fontName;
                row5_TieuDe2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe2.Font.Bold = true;
                row5_TieuDe2.Value2 = "Họ tên";


                //tô màu
                //Range range = oSheet.get_Range("A" + redRows.ToString(), "J" + redRows.ToString());
                //range.Cells.Interior.Color = System.Drawing.Color.Red;


                Excel.Range formatRange;
                int col = 4;

                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.EditValue).AddDays(iTNgay - 1);
                    oSheet.Cells[4, col].Font.Name = fontName;
                    oSheet.Cells[4, col].Font.Bold = true;
                    oSheet.Cells[4, col].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[4, col].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                    oSheet.Cells[5, col] = "Giờ Vào";
                    oSheet.Cells[5, col].Font.Bold = true;
                    oSheet.Cells[5, col].Font.Name = fontName;
                    oSheet.Cells[5, col].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;



                    oSheet.Cells[5, col + 1] = "Giờ ra";
                    oSheet.Cells[5, col + 1].Font.Bold = true;
                    oSheet.Cells[5, col + 1].Font.Name = fontName;
                    oSheet.Cells[5, col + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col + 1].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                    oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();

                    col = col + 2;
                    iTNgay++;
                }

                //lay tieu de cot nghi 
                DataRow[] drN = dtBCNghi.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]] = rowN[1].ToString();
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                    col++;
                }

                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Tổng công";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Quy công";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Giờ làm thêm BT";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Giờ làm thêm CN";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Giờ ban đêm";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;


                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Giờ chế độ";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Ký tên";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 15;

                col++;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Name = fontName;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Font.Bold = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Value2 = "Bộ phận";
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].WrapText = true;
                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].ColumnWidth = 20;

                DataRow[] dr = dtBCGaiDoan.Select();
                string[,] rowData = new string[dr.Length, dtBCGaiDoan.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCGaiDoan.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;


                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A4", lastColumn + rowCnt.ToString());
                formatRange.Borders.Color = Color.Black;
                //dữ liệu
                formatRange = oSheet.get_Range("A6", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt
                formatRange = oSheet.get_Range("A5", "A" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 5;
                //ma nv
                formatRange = oSheet.get_Range("B6", "B" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 15;
                //ho ten
                formatRange = oSheet.get_Range("C5", "C" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 35;

                //CẠNH giữa côt động
                formatRange = oSheet.get_Range("D4", lastColumn + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                //tô màu cột chủ nhật
                col = 4;
                DateTime dTNgay = lk_TuNgay.DateTime;
                while (dTNgay <= lk_DenNgay.DateTime)
                {
                    if (dTNgay.DayOfWeek.ToString() == "Sunday")
                    {
                        formatRange = oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col]]; //27 + 31
                        formatRange.Interior.Color = Color.FromArgb(255, 153, 102);
                    }
                    col = col + 2;
                    dTNgay = dTNgay.AddDays(1);
                }

                oXL.Visible = true;
                oXL.UserControl = true;

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void rdo_ChonBaoCao_KeyDown(object sender, KeyEventArgs e)
        {
            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() != "rdo_DanhSachThang") return;
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F3)
            {
                XacNhanQuetThe_DM(true);
            }
        }

        private void grvData_CellMerge(object sender, CellMergeEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "MA_THE":
                    {
                        string value1 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                        string value2 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));

                        if (value1 == value2)
                        {
                            e.Merge = true;
                            e.Handled = true;
                        }
                        else
                        {
                            e.Merge = false;
                            e.Handled = true;
                        }
                        break;
                    }
                case "STT_IN":
                case "HO_TEN":
                case "BO_PHAN":
                case "NGAY_VAO_CTY":
                    {
                        string value1 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                        string value2 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                        string value3 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle1, "STT_IN"));
                        string value4 = Convert.ToString(grvData.GetRowCellValue(e.RowHandle2, "STT_IN"));

                        if (value1 == value2 && value3 == value4)
                        {
                            e.Merge = true;
                            e.Handled = true;
                        }
                        else
                        {
                            e.Merge = false;
                            e.Handled = true;
                        }
                        break;
                    }

                default:
                    {
                        e.Merge = false;
                        e.Handled = true;
                    }
                    break;
            }

        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.sLoad = "0Load";

                LoadGridThongTinNhanVien();
                Commons.Modules.sLoad = "";



            }
            catch { }
        }

        private void lk_DenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGridThongTinNhanVien();
        }
    }
}
