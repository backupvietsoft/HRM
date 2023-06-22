using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using System.Globalization;
using DevExpress.Map.Dashboard;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using DevExpress.XtraCharts.Design;

namespace Vs.HRM
{
    public partial class ucBaoCaoKhenThuongKyLuat : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoKhenThuongKyLuat()
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
                        frmViewReport frm = new frmViewReport();

                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {

                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptBCKhenThuongKyLuat(lk_NgayIn.DateTime);
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

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
                                    {
                                    }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    BCKhenThuongTongHop();
                                    //switch (Commons.Modules.KyHieuDV)
                                    //{
                                    //    case "NB":
                                    //        {

                                    //            break;
                                    //        }
                                    //    default:
                                    //        {
                                    //            System.Data.SqlClient.SqlConnection conn1;
                                    //            DataTable dt = new DataTable();
                                    //            frm.rpt = new rptBCKhenThuongKyLuatBP(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    //            try
                                    //            {
                                    //                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    //                conn1.Open();

                                    //                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatTH", conn1);

                                    //                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    //                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    //                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    //                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    //                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    //                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                    //                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                    //                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                    //                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

                                    //                cmd.CommandType = CommandType.StoredProcedure;
                                    //                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                    //                DataSet ds = new DataSet();
                                    //                adp.Fill(ds);
                                    //                dt = new DataTable();
                                    //                dt = ds.Tables[0].Copy();
                                    //                dt.TableName = "DA_TA";
                                    //                frm.AddDataSource(dt);
                                    //            }
                                    //            catch (Exception ex)
                                    //            {
                                    //            }

                                    //            frm.ShowDialog();
                                    //            break;
                                    //        }
                                    //}
                                    break;
                                }
                            case 2:
                                {
                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptBCKhenThuongKyLuatTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptKhenThuongKyLuatTH]", conn1);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

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

                                    break;
                                }

                            default:
                                break;
                        }
                        break;

                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoKhenThuongKyLuat_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
                Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
                Commons.OSystems.SetDateEditFormat(dTuNgay);
                Commons.OSystems.SetDateEditFormat(dDenNgay);
                Commons.OSystems.SetDateEditFormat(lk_NgayIn);
                dtThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;


                dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE"));
                dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE")).AddMonths(1).AddDays(-1);
                dtThang.EditValue = DateTime.Today;
                Commons.Modules.sLoad = "";
                LoadNhanSu();
                lk_NgayIn.EditValue = DateTime.Today;
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "ra_TongHop").FirstOrDefault());
            }
            catch { }
           
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadNhanSu();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadNhanSu();
        }
        private void LoadNhanSu()
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dt = Commons.Modules.ObjSystems.DataCongNhanTheoDK(true, Convert.ToInt32(LK_DON_VI.EditValue), Convert.ToInt32(LK_XI_NGHIEP.EditValue), Convert.ToInt32(LK_TO.EditValue), Convert.ToDateTime(dTuNgay.EditValue), Convert.ToDateTime(dDenNgay.EditValue));
                if (cbCongNhan.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbCongNhan, dt, "ID_CN", "TEN_CN", "TEN_CN");
                    cbCongNhan.Properties.View.Columns[1].Visible = false;
                }
                else
                {
                    cbCongNhan.Properties.DataSource = dt;
                }
                cbCongNhan.EditValue = -1;
            }
            catch { }
        }

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }

        private void dtThang_Validated(object sender, EventArgs e)
        {
            try
            {
                DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
                dTuNgay.EditValue = firstDateTime;
                int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
                DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
                dDenNgay.EditValue = secondDateTime;
            }
            catch
            {

            }
        }

        private void dtThang_EditValueChanged(object sender, EventArgs e)
        {
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + dtThang.Text), new CultureInfo("de-DE"));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + dtThang.Text), new CultureInfo("de-DE")).AddMonths(1).AddDays(-1);
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadNhanSu();
        }

        private void BCKhenThuongTongHop()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatTH", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "KTTH";
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (dt.Rows.Count == 0)
                {
                    Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
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

                int lastColumn = 8;

                TaoTTChung(oSheet, 1, 2, 1, 5, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[3, 8], oSheet.Cells[3, 8]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 10;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Italic = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "Ngày " + lk_NgayIn.DateTime.Day + " tháng " + lk_NgayIn.DateTime.Month + " năm " + lk_NgayIn.DateTime.Year;

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = cbKhenThuongKyLuat.SelectedIndex == 0 ? Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblCanBoCNVKhenThuong") : Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblCanBoCNVViPham");

                row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblThang") + " " + dtThang.Text;

                // format tieu de
                Range format_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                format_TieuDe_BaoCao.Font.Size = 9;
                format_TieuDe_BaoCao.Font.Name = fontName;
                format_TieuDe_BaoCao.Font.Bold = true;
                format_TieuDe_BaoCao.WrapText = true;
                format_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                format_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                format_TieuDe_BaoCao.RowHeight = 25;

                Range row5_TieuDe_Stt = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 1]];
                row5_TieuDe_Stt.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblSTT").ToUpper();
                row5_TieuDe_Stt.ColumnWidth = 10;

                Range row5_TieuDe_HoTen = oSheet.Range[oSheet.Cells[6, 2], oSheet.Cells[6, 2]];
                row5_TieuDe_HoTen.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblHoTen").ToUpper();
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_MaSo = oSheet.Range[oSheet.Cells[6, 3], oSheet.Cells[6, 3]];
                row5_TieuDe_MaSo.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblMS_CN").ToUpper();
                row5_TieuDe_MaSo.ColumnWidth = 8;


                Range row5_TieuDe_BoPhan = oSheet.Range[oSheet.Cells[6, 4], oSheet.Cells[6, 4]];
                row5_TieuDe_BoPhan.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo").ToUpper();
                row5_TieuDe_BoPhan.ColumnWidth = 15;

                Range row5_TieuDe = oSheet.Range[oSheet.Cells[6, 5], oSheet.Cells[6, 5]];
                row5_TieuDe.Value2 = cbKhenThuongKyLuat.SelectedIndex == 0 ? Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNgayKhenThuong").ToUpper() : Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNgayViPham").ToUpper();
                row5_TieuDe.ColumnWidth = 11;

                row5_TieuDe = oSheet.Range[oSheet.Cells[6, 6], oSheet.Cells[6, 6]];
                row5_TieuDe.Value2 = cbKhenThuongKyLuat.SelectedIndex == 0 ? Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblSoBienBanKT").ToUpper() : Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblSoBienBanVP").ToUpper();
                row5_TieuDe.ColumnWidth = 15;

                row5_TieuDe = oSheet.Range[oSheet.Cells[6, 7], oSheet.Cells[6, 7]];
                row5_TieuDe.Value2 = cbKhenThuongKyLuat.SelectedIndex == 0 ? Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNoiDungKhenThuong").ToUpper() : Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNoiDungViPham").ToUpper();
                row5_TieuDe.ColumnWidth = 55;

                row5_TieuDe = oSheet.Range[oSheet.Cells[6, 8], oSheet.Cells[6, 8]];
                row5_TieuDe.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblGhiChu").ToUpper();
                row5_TieuDe.ColumnWidth = 15;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range formatRange;
                int rowCnt = 0;
                int rowBD = 7;
                string[] LOAI_KT = dt.AsEnumerable().Select(r => r.Field<string>("HINH_THUC_XL")).Distinct().ToArray();
                int demRoman = 0;
                if(cbKhenThuongKyLuat.SelectedIndex == 0)
                {
                    rowCnt = rowBD;
                    DataRow[] dr = dt.Select();
                    int STT = 1;
                    foreach (DataRow row in dr)
                    {
                        dynamic[] arr = { STT, row["HO_TEN"].ToString(), row["MS_CN"].ToString(), row["BO_PHAN"].ToString(), row["NGAY_HIEU_LUC"].ToString(), row["SO_QUYET_DINH"].ToString(), row["NOI_DUNG"].ToString(),
                        row["GHI_CHU"].ToString()
                        };
                        Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 8]];
                        rowData.WrapText = true;
                        rowData.Value2 = arr;
                        rowCnt++;
                        STT++;
                    }
                }
                else
                {
                    for (int j = 0; j < LOAI_KT.Count(); j++)
                    {
                        demRoman++;
                        dt = ds.Tables[0].Copy();
                        dt = dt.AsEnumerable().Where(r => r.Field<string>("HINH_THUC_XL") == LOAI_KT[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dt.Select();

                        // Tạo group tổ
                        Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 2], oSheet.Cells[rowBD, 2]];
                        row_groupXI_NGHIEP_Format.Value2 = LOAI_KT[j].ToString();
                        row_groupXI_NGHIEP_Format.Font.Bold = true;

                        row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, 1]];
                        row_groupXI_NGHIEP_Format.Value2 = int_to_Roman(demRoman);

                        rowBD++;
                        rowCnt = rowBD;
                        foreach (DataRow row in dr)
                        {
                            dynamic[] arr = { row["STT"].ToString(), row["HO_TEN"].ToString(), row["MS_CN"].ToString(), row["BO_PHAN"].ToString(), row["NGAY_HIEU_LUC"].ToString(), row["SO_QUYET_DINH"].ToString(), row["NOI_DUNG"].ToString(),
                        row["GHI_CHU"].ToString()
                        };
                            Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 8]];
                            rowData.WrapText = true;
                            rowData.Value2 = arr;
                            rowCnt++;
                        }
                        rowBD = rowCnt;
                    }
                }
               
                

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]]; // format tất cả dữ liệu theo fontname 
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, 1]]; // format cột stt canh giữa
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[rowCnt, 3]]; // format cột mã số nhân viên canh giữa
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[rowCnt, 5]]; // format cột ngày 
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, 6]]; // format số biên bản vi phạm 
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.BorderAround();
                Microsoft.Office.Interop.Excel.Borders borders = formatRange.Borders;
                borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // Thêm border liên tục cho hàng dọc


                rowCnt++;
                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 2]];
                formatRange.Merge();
                formatRange.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblNguoiLapBieu").ToUpper();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10.5;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 4], oSheet.Cells[rowCnt, 5]];
                formatRange.Merge();
                formatRange.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblThanhTra").ToUpper();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10.5;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 8]];
                formatRange.Merge();
                formatRange.Value2 = "                P HCNS                                                    "+ Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblBanGiamDoc").ToUpper() + "";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 10.5;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
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

        public static string int_to_Roman(int n)
        {
            string[] roman_symbol = { "MMM.", "MM.", "M.", "CM.", "DCCC.", "DCC.", "DC.", "D.", "CD.", "CCC.", "CC.", "C.", "XC.", "LXXX.", "LXX.", "LX.", "L.", "XL.", "XXX.", "XX.", "X.", "IX.", "VIII.", "VII.", "VI.", "V.", "IV.", "III.", "II.", "I." };
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
