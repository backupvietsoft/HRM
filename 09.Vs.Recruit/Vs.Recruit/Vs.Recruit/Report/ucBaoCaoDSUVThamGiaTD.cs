using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Recruit
{
    public partial class ucBaoCaoDSUVThamGiaTD : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoDSUVThamGiaTD()
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
                        switch (radioGroup1.SelectedIndex)
                        {
                            case 0:
                                {
                                    DSUngVienThamGiaTuyenDung(1);
                                    break;
                                }
                            case 1:
                                {
                                    DSUngVienThamGiaTuyenDung(2);
                                    break;
                                }
                            case 2:
                                {
                                    DSUngVienThamGiaTuyenDung(3);
                                    break;
                                }
                            case 3:
                                {
                                    BaoCaoSoSanh(5);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        private void ucBaoCaoDSUVThamGiaTD_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            //Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            //Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            //Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_YCTD,MA_YCTD FROM dbo.YEU_CAU_TUYEN_DUNG  ORDER BY MA_YCTD"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboYeuCauTD, Commons.Modules.ObjSystems.DataYeuCauTD(true, -1), "ID_YCTD", "MA_YCTD", "MA_YCTD");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTinhTrangYeuCau, Commons.Modules.ObjSystems.DataTinhTrangYC(false), "ID_TTYC", "Ten_TTYC", "Ten_TTYC");
            cboTinhTrangYeuCau.EditValue = 1;
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboViTriTD, Commons.Modules.ObjSystems.DataViTri(Convert.ToInt64(cboViTriTD.EditValue)), "ID_YCTD", "MA_YCTD", "MA_YCTD");

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            //Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            //dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            //dtThang.EditValue = DateTime.Today;
            //lk_NgayIn.EditValue = DateTime.Today;

            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sLoad == "0Load") return;
            //Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            //Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sLoad == "0Load") return;
            //Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }
        private void dtThang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
                //dTuNgay.EditValue = firstDateTime;
                //int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
                //DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
                //dDenNgay.EditValue = secondDateTime;
            }
            catch
            {

            }
        }

        private void dtThang_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
            //    dTuNgay.EditValue = firstDateTime;
            //    int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
            //    DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
            //    dDenNgay.EditValue = secondDateTime;
            //}
            //catch
            //{

            //}
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    switch (rdo_ChonBaoCao.SelectedIndex)
            //    {
            //        case 0:
            //            {
            //                dtThang.Enabled = false;
            //                dTuNgay.Enabled = false;
            //                dDenNgay.Enabled = false;
            //            }
            //            break;
            //        case 1:
            //            {
            //                dtThang.Enabled = true;
            //                dTuNgay.Enabled = true;
            //                dDenNgay.Enabled = true;
            //            }
            //            break;

            //        default:
            //            dtThang.Enabled = true;
            //            dTuNgay.Enabled = true;
            //            dDenNgay.Enabled = true;
            //            break;
            //    }
            //}
            //catch
            //{ }
        }

        #region Excel
        private void HeaderReport(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, int LoaiBaoCao, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11)
        {
            if (LoaiBaoCao == 1)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH ỨNG VIÊN THAM GIA TUYỂN DỤNG";

                Range row4_TuNgayTuyenDung = oSheet.get_Range("K4"); // Cell K4
                row4_TuNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_TuNgayTuyenDung.Font.Bold = false;
                row4_TuNgayTuyenDung.WrapText = true;
                row4_TuNgayTuyenDung.Font.Name = fontName;
                row4_TuNgayTuyenDung.ColumnWidth = 15;
                row4_TuNgayTuyenDung.NumberFormat = "@";
                row4_TuNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_TuNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TuNgayTuyenDung.Value2 = "Từ ngày";

                Range row4_datTuNgayTD = oSheet.get_Range("L4"); // Cell L4
                row4_datTuNgayTD.Font.Size = fontSizeNoiDung;
                row4_datTuNgayTD.Font.Bold = false;
                row4_datTuNgayTD.WrapText = true;
                row4_datTuNgayTD.ColumnWidth = 11;
                row4_datTuNgayTD.Font.Name = fontName;
                row4_datTuNgayTD.NumberFormat = "dd/MM/yyyy";
                row4_datTuNgayTD.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datTuNgayTD.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datTuNgayTD.Value2 = Convert.ToDateTime(dTuNgay.EditValue);

                Range row4_DenNgayTuyenDung = oSheet.get_Range("M4"); // Cell M4
                row4_DenNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_DenNgayTuyenDung.Font.Bold = false;
                row4_DenNgayTuyenDung.WrapText = true;
                row4_DenNgayTuyenDung.Font.Name = fontName;
                row4_DenNgayTuyenDung.ColumnWidth = 11;
                row4_DenNgayTuyenDung.NumberFormat = "@";
                row4_DenNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_DenNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_DenNgayTuyenDung.Value2 = "Đến ngày";

                Range row4_datDenNgayTuyenDung = oSheet.get_Range("N4"); // Cell N4
                row4_datDenNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_datDenNgayTuyenDung.Font.Bold = false;
                row4_datDenNgayTuyenDung.WrapText = true;
                row4_datDenNgayTuyenDung.ColumnWidth = 11;
                row4_datDenNgayTuyenDung.Font.Name = fontName;
                row4_datDenNgayTuyenDung.NumberFormat = "dd/MM/yyyy";
                row4_datDenNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datDenNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datDenNgayTuyenDung.Value2 = Convert.ToDateTime(dDenNgay.EditValue);

                Range row4_TinhTrangYeuCau = oSheet.get_Range("O4"); // Cell O4
                row4_TinhTrangYeuCau.Font.Size = fontSizeNoiDung;
                row4_TinhTrangYeuCau.Font.Bold = false;
                row4_TinhTrangYeuCau.WrapText = true;
                row4_TinhTrangYeuCau.Font.Name = fontName;
                row4_TinhTrangYeuCau.ColumnWidth = 18;
                row4_TinhTrangYeuCau.NumberFormat = "@";
                row4_TinhTrangYeuCau.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_TinhTrangYeuCau.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TinhTrangYeuCau.Value2 = "Tình trạng yêu cầu";
                return;
            }
            if (LoaiBaoCao == 2)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH ỨNG VIÊN THAM GIA PHỎNG VẤN";

                Range row4_NgayPhongVan = oSheet.get_Range("H4"); // Cell H4
                row4_NgayPhongVan.Font.Size = fontSizeNoiDung;
                row4_NgayPhongVan.Font.Bold = true;
                row4_NgayPhongVan.Font.Name = fontName;
                row4_NgayPhongVan.ColumnWidth = 25;
                row4_NgayPhongVan.NumberFormat = "@";
                row4_NgayPhongVan.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_NgayPhongVan.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_NgayPhongVan.Value2 = "Ngày phỏng vấn:";

                Range row4_datNgayPhongVan = oSheet.get_Range("I4"); // Cell I4
                row4_datNgayPhongVan.Font.Size = fontSizeNoiDung;
                row4_datNgayPhongVan.Font.Bold = true;
                row4_datNgayPhongVan.WrapText = true;
                row4_datNgayPhongVan.ColumnWidth = 12;
                row4_datNgayPhongVan.Font.Name = fontName;
                row4_datNgayPhongVan.NumberFormat = "dd/MM/yyyy";
                row4_datNgayPhongVan.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datNgayPhongVan.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datNgayPhongVan.Value2 = Convert.ToDateTime(dTuNgay.EditValue);

                Range row4_BuoiPhongVan = oSheet.get_Range("J4"); // Cell J4
                row4_BuoiPhongVan.Font.Size = fontSizeNoiDung;
                row4_BuoiPhongVan.Font.Bold = true;
                row4_BuoiPhongVan.Font.Name = fontName;
                row4_BuoiPhongVan.ColumnWidth = 9;
                row4_BuoiPhongVan.NumberFormat = "@";
                row4_BuoiPhongVan.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_BuoiPhongVan.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_BuoiPhongVan.Value2 = "Buổi phỏng vấn:";

                Range row4_datBuoiPhongVan = oSheet.get_Range("K4"); // Cell K4
                row4_datBuoiPhongVan.Font.Size = fontSizeNoiDung;
                row4_datBuoiPhongVan.Font.Bold = true;
                row4_datBuoiPhongVan.WrapText = true;
                row4_datBuoiPhongVan.ColumnWidth = 12;
                row4_datBuoiPhongVan.Font.Name = fontName;
                row4_datBuoiPhongVan.NumberFormat = "dd/MM/yyyy";
                row4_datBuoiPhongVan.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datBuoiPhongVan.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datBuoiPhongVan.Value2 = Convert.ToDateTime(dDenNgay.EditValue);

                Range row4_NguoiPhongVan_PTD = oSheet.get_Range("L4", "M4"); // Cells L4,M4 
                row4_NguoiPhongVan_PTD.Merge();
                row4_NguoiPhongVan_PTD.Font.Size = fontSizeNoiDung;
                row4_NguoiPhongVan_PTD.Font.Bold = true;
                row4_NguoiPhongVan_PTD.Font.Name = fontName;
                row4_NguoiPhongVan_PTD.NumberFormat = "@";
                row4_NguoiPhongVan_PTD.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_NguoiPhongVan_PTD.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_NguoiPhongVan_PTD.Value2 = "Người phỏng vấn(PTD):";

                Range row4_NguoiPhongVan_PTD_Value = oSheet.get_Range("N4"); // Cell N4
                row4_NguoiPhongVan_PTD_Value.Font.Size = fontSizeNoiDung;
                row4_NguoiPhongVan_PTD_Value.Font.Bold = true;
                row4_NguoiPhongVan_PTD_Value.WrapText = true;
                row4_NguoiPhongVan_PTD_Value.ColumnWidth = 12;
                row4_NguoiPhongVan_PTD_Value.Font.Name = fontName;
                row4_NguoiPhongVan_PTD_Value.NumberFormat = "@";
                row4_NguoiPhongVan_PTD_Value.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_NguoiPhongVan_PTD_Value.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_NguoiPhongVan_PTD_Value.Value2 = "";

                Range row4_NguoiPhongVan_TBP = oSheet.get_Range("O4", "P4"); // Cells O4,P4 
                row4_NguoiPhongVan_TBP.Merge();
                row4_NguoiPhongVan_TBP.Font.Size = fontSizeNoiDung;
                row4_NguoiPhongVan_TBP.Font.Bold = true;
                row4_NguoiPhongVan_TBP.Font.Name = fontName;
                row4_NguoiPhongVan_TBP.NumberFormat = "@";
                row4_NguoiPhongVan_TBP.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_NguoiPhongVan_TBP.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_NguoiPhongVan_TBP.Value2 = "Người phỏng vấn(TBP):";

                Range row4_NguoiPhongVan_TBP_Value = oSheet.get_Range("Q4"); // Cell Q4
                row4_NguoiPhongVan_TBP_Value.Font.Size = fontSizeNoiDung;
                row4_NguoiPhongVan_TBP_Value.Font.Bold = true;
                row4_NguoiPhongVan_TBP_Value.Font.Name = fontName;
                row4_NguoiPhongVan_TBP_Value.NumberFormat = "@";
                row4_NguoiPhongVan_TBP_Value.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_NguoiPhongVan_TBP_Value.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_NguoiPhongVan_TBP_Value.Value2 = "";
                return;
            }
            if (LoaiBaoCao == 3)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH ỨNG VIÊN ĐẠT YÊU CẦU TUYỂN DỤNG";

                Range row4_TuNgayTuyenDung = oSheet.get_Range("K4"); // Cell K4
                row4_TuNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_TuNgayTuyenDung.Font.Bold = false;
                row4_TuNgayTuyenDung.WrapText = true;
                row4_TuNgayTuyenDung.Font.Name = fontName;
                row4_TuNgayTuyenDung.ColumnWidth = 15;
                row4_TuNgayTuyenDung.NumberFormat = "@";
                row4_TuNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_TuNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TuNgayTuyenDung.Value2 = "Từ ngày";

                Range row4_datTuNgayTD = oSheet.get_Range("L4"); // Cell L4
                row4_datTuNgayTD.Font.Size = fontSizeNoiDung;
                row4_datTuNgayTD.Font.Bold = false;
                row4_datTuNgayTD.WrapText = true;
                row4_datTuNgayTD.ColumnWidth = 11;
                row4_datTuNgayTD.Font.Name = fontName;
                row4_datTuNgayTD.NumberFormat = "dd/MM/yyyy";
                row4_datTuNgayTD.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datTuNgayTD.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datTuNgayTD.Value2 = Convert.ToDateTime(dTuNgay.EditValue);

                Range row4_DenNgayTuyenDung = oSheet.get_Range("M4"); // Cell M4
                row4_DenNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_DenNgayTuyenDung.Font.Bold = false;
                row4_DenNgayTuyenDung.WrapText = true;
                row4_DenNgayTuyenDung.Font.Name = fontName;
                row4_DenNgayTuyenDung.ColumnWidth = 11;
                row4_DenNgayTuyenDung.NumberFormat = "@";
                row4_DenNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_DenNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_DenNgayTuyenDung.Value2 = "Đến ngày";

                Range row4_datDenNgayTuyenDung = oSheet.get_Range("N4"); // Cell N4
                row4_datDenNgayTuyenDung.Font.Size = fontSizeNoiDung;
                row4_datDenNgayTuyenDung.Font.Bold = false;
                row4_datDenNgayTuyenDung.WrapText = true;
                row4_datDenNgayTuyenDung.ColumnWidth = 11;
                row4_datDenNgayTuyenDung.Font.Name = fontName;
                row4_datDenNgayTuyenDung.NumberFormat = "dd/MM/yyyy";
                row4_datDenNgayTuyenDung.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_datDenNgayTuyenDung.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_datDenNgayTuyenDung.Value2 = Convert.ToDateTime(dDenNgay.EditValue);

                Range row4_TinhTrangYeuCau = oSheet.get_Range("O4"); // Cell O4
                row4_TinhTrangYeuCau.Font.Size = fontSizeNoiDung;
                row4_TinhTrangYeuCau.Font.Bold = false;
                row4_TinhTrangYeuCau.WrapText = true;
                row4_TinhTrangYeuCau.Font.Name = fontName;
                row4_TinhTrangYeuCau.ColumnWidth = 18;
                row4_TinhTrangYeuCau.NumberFormat = "@";
                row4_TinhTrangYeuCau.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_TinhTrangYeuCau.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TinhTrangYeuCau.Value2 = "Tình trạng yêu cầu";

                Range row4_TinhTrangYeuCau_value = oSheet.get_Range("P4"); // Cell O4
                row4_TinhTrangYeuCau_value.Font.Size = fontSizeNoiDung;
                row4_TinhTrangYeuCau_value.Font.Bold = false;
                row4_TinhTrangYeuCau_value.WrapText = true;
                row4_TinhTrangYeuCau_value.Font.Name = fontName;
                row4_TinhTrangYeuCau_value.ColumnWidth = 18;
                row4_TinhTrangYeuCau_value.NumberFormat = "@";
                row4_TinhTrangYeuCau_value.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                row4_TinhTrangYeuCau_value.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TinhTrangYeuCau_value.Value2 = cboTinhTrangYeuCau.Text;
                return;
            }
            if (LoaiBaoCao == 4)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH ỨNG VIÊN THAM GIA TUYỂN DỤNG";

                Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3"); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");
                return;
            }
            if (LoaiBaoCao == 4)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH ỨNG VIÊN THAM GIA TUYỂN DỤNG";

                Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3"); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");
                return;
            }
            if (LoaiBaoCao == 5)
            {
                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "1"); // = A2 - V21
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Bold = false;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.NumberFormat = "@";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH SO SÁNH TUYỂN DỤNG VÀ THỰC TẾ ĐI LÀM";

                Range row4_Sub_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3"); //A3 - V21
                row4_Sub_TieuDe_BaoCao.Merge();
                row4_Sub_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_Sub_TieuDe_BaoCao.Font.Name = fontName;
                row4_Sub_TieuDe_BaoCao.Font.Bold = false;
                row4_Sub_TieuDe_BaoCao.NumberFormat = "@";
                row4_Sub_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_Sub_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_Sub_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(dTuNgay.EditValue).ToString("dd/MM/yyyy") + "      Đến ngày  " + Convert.ToDateTime(dDenNgay.EditValue).ToString("dd/MM/yyyy");
                return;
            }
        }
        private void TitleTable(int LoaiBaoCao)
        {
            string[] TitleTableName = { };
        }
        private void DSUngVienThamGiaTuyenDung(int LoaiBaoCao)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_DM", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSUngVienThamGiaTD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                cmd.Parameters.Add("@ID_YCTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboYeuCauTD.EditValue);
                cmd.Parameters.Add("@ID_VTTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboViTriTD.EditValue);
                cmd.Parameters.Add("@ID_TTYC", SqlDbType.BigInt).Value = Convert.ToInt64(cboTinhTrangYeuCau.EditValue);
                cmd.Parameters.Add("@LOAI_BC", SqlDbType.Int).Value = LoaiBaoCao;


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


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                // Header của báo cáo
                HeaderReport(ref oSheet, LoaiBaoCao, fontName, fontSizeNoiDung, lastColumn, fontSizeTieuDe);

                // Title Table
                Range row6_STT = oSheet.get_Range("A6");
                row6_STT.Value2 = "STT";
                FormatTitleTable(ref row6_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 5);

                Range row6_BoPhanYeuCau = oSheet.get_Range("B6");
                row6_BoPhanYeuCau.Value2 = "Bộ phận yêu cầu";
                FormatTitleTable(ref row6_BoPhanYeuCau, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 8);

                Range row6_MSYeuCau = oSheet.get_Range("C6");
                row6_MSYeuCau.Value2 = "MS Yêu cầu";
                FormatTitleTable(ref row6_MSYeuCau, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 8);

                Range row6_NgayYeuCau = oSheet.get_Range("D6");
                row6_NgayYeuCau.Value2 = "Ngày yêu cầu";
                FormatTitleTable(ref row6_NgayYeuCau, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 8);

                Range row6_NgayCanDiLam = oSheet.get_Range("E6");
                row6_NgayCanDiLam.Value2 = "Ngày cần đi làm";
                FormatTitleTable(ref row6_NgayCanDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 8);

                Range row6_ViTriTuyen = oSheet.get_Range("F6");
                row6_ViTriTuyen.Value2 = "Vị Trí tuyển";
                FormatTitleTable(ref row6_ViTriTuyen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 8);

                Range row6_MaSo_UV = oSheet.get_Range("G6");
                row6_MaSo_UV.Value2 = "Mã số UV";
                FormatTitleTable(ref row6_MaSo_UV, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row6_HoTen = oSheet.get_Range("H6");
                row6_HoTen.Value2 = "Họ Tên";
                FormatTitleTable(ref row6_HoTen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 25);

                Range row6_NgaySinh = oSheet.get_Range("I6");
                row6_NgaySinh.Value2 = "Ngày sinh";
                FormatTitleTable(ref row6_NgaySinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row6_GioiTinh = oSheet.get_Range("J6");
                row6_GioiTinh.Value2 = "Giới tính";
                FormatTitleTable(ref row6_GioiTinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 9);

                Range row6_CMND = oSheet.get_Range("K6");
                row6_CMND.Value2 = "Số CMND";
                FormatTitleTable(ref row6_CMND, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row6_NgayCap = oSheet.get_Range("L6");
                row6_NgayCap.Value2 = "Ngày cấp";
                FormatTitleTable(ref row6_NgayCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row6_NoiCap = oSheet.get_Range("M6");
                row6_NoiCap.Value2 = "Nơi cấp";
                FormatTitleTable(ref row6_NoiCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 17);

                Range row6_DiaChi = oSheet.get_Range("N6");
                row6_DiaChi.Value2 = "Địa chỉ";
                FormatTitleTable(ref row6_DiaChi, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row6_Tinh = oSheet.get_Range("O6");
                row6_Tinh.Value2 = "Tỉnh";
                FormatTitleTable(ref row6_Tinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row6_Huyen = oSheet.get_Range("P6");
                row6_Huyen.Value2 = "Huyện";
                FormatTitleTable(ref row6_Huyen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row6_Xa = oSheet.get_Range("Q6");
                row6_Xa.Value2 = "Xã";
                FormatTitleTable(ref row6_Xa, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row6_ThonXom = oSheet.get_Range("R6");
                row6_ThonXom.Value2 = "Thôn xóm";
                FormatTitleTable(ref row6_ThonXom, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row6_SDT = oSheet.get_Range("S6");
                row6_SDT.Value2 = "Số điện thoại";
                FormatTitleTable(ref row6_SDT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 14);

                Range row6_TrinhDo = oSheet.get_Range("T6");
                row6_TrinhDo.Value2 = "Trình độ";
                FormatTitleTable(ref row6_TrinhDo, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row6_ChuyenNganh = oSheet.get_Range("U6");
                row6_ChuyenNganh.Value2 = "Chuyên ngành";
                FormatTitleTable(ref row6_ChuyenNganh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row6_KinhNghiem = oSheet.get_Range("V6");
                row6_KinhNghiem.Value2 = "Kinh nghiệm làm việc";
                row6_KinhNghiem.WrapText = true;
                FormatTitleTable(ref row6_KinhNghiem, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 14);

                Range row6_TenCongTyCu = oSheet.get_Range("W6");
                row6_TenCongTyCu.Value2 = "Tên công ty cũ";
                row6_TenCongTyCu.WrapText = true;
                FormatTitleTable(ref row6_TenCongTyCu, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 12);

                Range row6_NguonTuyenDung = oSheet.get_Range("X6");
                row6_NguonTuyenDung.Value2 = "Nguồn tuyển dụng";
                row6_NguonTuyenDung.WrapText = true;
                FormatTitleTable(ref row6_NguonTuyenDung, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 12);

                Range row6_NgayCoTheDiLam = oSheet.get_Range("Y6");
                row6_NgayCoTheDiLam.Value2 = "Ngày có thể đi làm";
                row6_NgayCoTheDiLam.WrapText = true;
                FormatTitleTable(ref row6_NgayCoTheDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row6_TinhTrangTuyenDung = oSheet.get_Range("Z6");
                row6_TinhTrangTuyenDung.Value2 = "Tình trạng tuyển dụng";
                row6_TinhTrangTuyenDung.WrapText = true;
                FormatTitleTable(ref row6_TinhTrangTuyenDung, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                //End title table

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                int col_bd = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
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

                // Data table
                // A7->Last
                formatRange = oSheet.get_Range("A7", "A" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // B7->Last
                formatRange = oSheet.get_Range("B7", "B" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // C7->Last
                formatRange = oSheet.get_Range("C7", "C" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // D7->Last
                formatRange = oSheet.get_Range("D7", "D" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // E7->Last
                formatRange = oSheet.get_Range("E7", "E" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // F7->Last
                formatRange = oSheet.get_Range("F7", "F" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // G7->Last
                formatRange = oSheet.get_Range("G7", "G" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // H7->Last
                formatRange = oSheet.get_Range("H7", "H" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // I7->Last
                formatRange = oSheet.get_Range("I7", "I" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // J7->Last
                formatRange = oSheet.get_Range("J7", "J" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // K7->Last
                formatRange = oSheet.get_Range("K7", "K" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // L7->Last
                formatRange = oSheet.get_Range("L7", "L" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // M7->Last
                formatRange = oSheet.get_Range("M7", "M" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // N7->Last
                formatRange = oSheet.get_Range("N7", "N" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // O7->Last
                formatRange = oSheet.get_Range("O7", "O" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // P7->Last
                formatRange = oSheet.get_Range("P7", "P" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // Q7->Last
                formatRange = oSheet.get_Range("Q7", "Q" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // R7->Last
                formatRange = oSheet.get_Range("R7", "R" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // S7->Last
                formatRange = oSheet.get_Range("S7", "S" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // T6->Last
                formatRange = oSheet.get_Range("T7", "T" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // U7->Last
                formatRange = oSheet.get_Range("U7", "U" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // V7-Last
                formatRange = oSheet.get_Range("V7", "V" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // W7->Last
                formatRange = oSheet.get_Range("W7", "W" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // X7->Last
                formatRange = oSheet.get_Range("X7", "X" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // Y7->Last
                formatRange = oSheet.get_Range("Y7", "Y" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // Z7->Last
                formatRange = oSheet.get_Range("Z7", "Z" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                switch (LoaiBaoCao)
                {
                    case 1:
                        //Title of the Table
                        Range row6_ChonPhongVan = oSheet.get_Range("AA6");
                        row6_ChonPhongVan.Value2 = "Chọn phỏng vấn";
                        row6_ChonPhongVan.WrapText = true;
                        FormatTitleTable(ref row6_ChonPhongVan, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                        // AA7->Last
                        formatRange = oSheet.get_Range("AA7", "AA" + (rowCnt).ToString());
                        FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);
                        break;
                    case 2:
                        break;
                    case 3:
                        // Title of the table
                        Range row6_NgayHenDiLam = oSheet.get_Range("AA6");
                        row6_NgayHenDiLam.Value2 = "Ngày hẹn đi làm";
                        row6_NgayHenDiLam.WrapText = true;
                        FormatTitleTable(ref row6_NgayHenDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                        Range row5_XacNhanDiLam = oSheet.get_Range("AB6");
                        row5_XacNhanDiLam.Value2 = "Xác nhận đi làm";
                        row5_XacNhanDiLam.WrapText = true;
                        FormatTitleTable(ref row5_XacNhanDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                        //AA7->Last
                        formatRange = oSheet.get_Range("AA7", "AA" + (rowCnt).ToString());
                        FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                        //AB7->Last
                        formatRange = oSheet.get_Range("AB7", "AB" + (rowCnt).ToString());
                        FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);
                        break;
                }

                //End Data Table

                BorderAround(oSheet.get_Range("A6", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DSUngVien(int LoaiBaoCao)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_DM", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSUngVienThamGiaTD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                cmd.Parameters.Add("@ID_YCTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboYeuCauTD.EditValue);
                cmd.Parameters.Add("@ID_VTTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboViTriTD.EditValue);
                cmd.Parameters.Add("@ID_TTYC", SqlDbType.BigInt).Value = Convert.ToInt64(cboTinhTrangYeuCau.EditValue);
                cmd.Parameters.Add("@LOAI_BC", SqlDbType.Int).Value = LoaiBaoCao;


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


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                //Header của báo cáo
                HeaderReport(ref oSheet, LoaiBaoCao, fontName, fontSizeNoiDung, lastColumn, fontSizeTieuDe);

                // Title Table
                Range row5_STT = oSheet.get_Range("A5");
                row5_STT.Value2 = "STT";
                FormatTitleTable(ref row5_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 5);

                Range row5_MaSo_UV = oSheet.get_Range("B5");
                row5_MaSo_UV.Value2 = "Mã số UV";
                FormatTitleTable(ref row5_MaSo_UV, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row5_HoTen = oSheet.get_Range("C5");
                row5_HoTen.Value2 = "Họ Tên";
                FormatTitleTable(ref row5_HoTen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 25);

                Range row5_NgaySinh = oSheet.get_Range("D5");
                row5_NgaySinh.Value2 = "Ngày sinh";
                FormatTitleTable(ref row5_NgaySinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row5_GioiTinh = oSheet.get_Range("E5");
                row5_GioiTinh.Value2 = "Giới tính";
                FormatTitleTable(ref row5_GioiTinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 9);

                Range row5_CMND = oSheet.get_Range("F5");
                row5_CMND.Value2 = "Số CMND";
                FormatTitleTable(ref row5_CMND, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_NgayCap = oSheet.get_Range("G5");
                row5_NgayCap.Value2 = "Ngày cấp";
                FormatTitleTable(ref row5_NgayCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row5_NoiCap = oSheet.get_Range("H5");
                row5_NoiCap.Value2 = "Nơi cấp";
                FormatTitleTable(ref row5_NoiCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 17);

                Range row5_DiaChi = oSheet.get_Range("I5");
                row5_DiaChi.Value2 = "Địa chỉ";
                FormatTitleTable(ref row5_DiaChi, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row5_Tinh = oSheet.get_Range("J5");
                row5_Tinh.Value2 = "Tỉnh";
                FormatTitleTable(ref row5_Tinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_Huyen = oSheet.get_Range("K5");
                row5_Huyen.Value2 = "Huyện";
                FormatTitleTable(ref row5_Huyen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_Xa = oSheet.get_Range("L5");
                row5_Xa.Value2 = "Xã";
                FormatTitleTable(ref row5_Xa, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_ThonXom = oSheet.get_Range("M5");
                row5_ThonXom.Value2 = "Thôn xóm";
                FormatTitleTable(ref row5_ThonXom, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_SDT = oSheet.get_Range("N5");
                row5_SDT.Value2 = "Số điện thoại";
                FormatTitleTable(ref row5_SDT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 14);

                Range row5_TrinhDo = oSheet.get_Range("O5");
                row5_TrinhDo.Value2 = "Trình độ";
                FormatTitleTable(ref row5_TrinhDo, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 11);

                Range row5_ChuyenNganh = oSheet.get_Range("P5");
                row5_ChuyenNganh.Value2 = "Chuyên ngành";
                FormatTitleTable(ref row5_ChuyenNganh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_KinhNghiem = oSheet.get_Range("Q5");
                row5_KinhNghiem.Value2 = "Kinh nghiệm làm việc";
                row5_KinhNghiem.WrapText = true;
                FormatTitleTable(ref row5_KinhNghiem, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 14);

                Range row5_ViTriTD1 = oSheet.get_Range("R5");
                row5_ViTriTD1.Value2 = "Vị trí tuyển dụng 1";
                row5_ViTriTD1.WrapText = true;
                FormatTitleTable(ref row5_ViTriTD1, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 19);

                Range row5_ViTriTD2 = oSheet.get_Range("S5");
                row5_ViTriTD2.Value2 = "Vị trí tuyển dụng 2";
                row5_ViTriTD2.WrapText = true;
                FormatTitleTable(ref row5_ViTriTD2, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 19);

                Range row5_NgayCoTheDiLam = oSheet.get_Range("T5");
                row5_NgayCoTheDiLam.Value2 = "Ngày có thể đi làm";
                row5_NgayCoTheDiLam.WrapText = true;
                FormatTitleTable(ref row5_NgayCoTheDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row5_NgayHenDiLam = oSheet.get_Range("U5");
                row5_NgayHenDiLam.Value2 = "Ngày hẹn đi làm";
                row5_NgayHenDiLam.WrapText = true;
                FormatTitleTable(ref row5_NgayHenDiLam, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row5_NgayNhanViec = oSheet.get_Range("V5");
                row5_NgayNhanViec.Value2 = "Ngày nhận việc";
                row5_NgayNhanViec.WrapText = true;
                FormatTitleTable(ref row5_NgayNhanViec, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row5_DaDaoTaoNQ = oSheet.get_Range("W5");
                row5_DaDaoTaoNQ.Value2 = "Đã đào tạo nội quy và định hướng";
                row5_DaDaoTaoNQ.WrapText = true;
                FormatTitleTable(ref row5_DaDaoTaoNQ, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 18);

                Range row5_DaChuyenSangNhanSu = oSheet.get_Range("X5");
                row5_DaChuyenSangNhanSu.Value2 = "Đã chuyển sang nhân sự";
                row5_DaChuyenSangNhanSu.WrapText = true;
                FormatTitleTable(ref row5_DaChuyenSangNhanSu, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 13);

                Range row5_TinhTrangTuyenDung = oSheet.get_Range("Y5");
                row5_TinhTrangTuyenDung.Value2 = "Tình trạng tuyển dụng";
                row5_TinhTrangTuyenDung.WrapText = true;
                FormatTitleTable(ref row5_TinhTrangTuyenDung, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                //End title table

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                int col_bd = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
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

                // Data table
                // A6->Last
                formatRange = oSheet.get_Range("A6", "A" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // B6->Last
                formatRange = oSheet.get_Range("B6", "B" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // C6->Last
                formatRange = oSheet.get_Range("C6", "C" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // D6->Last
                formatRange = oSheet.get_Range("D6", "D" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // E6->Last
                formatRange = oSheet.get_Range("E6", "E" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // F6->Last
                formatRange = oSheet.get_Range("F6", "F" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // G6->Last
                formatRange = oSheet.get_Range("G6", "G" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // H6->Last
                formatRange = oSheet.get_Range("H6", "H" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // I6->Last
                formatRange = oSheet.get_Range("I6", "I" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // J6->Last
                formatRange = oSheet.get_Range("J6", "J" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // K6->Last
                formatRange = oSheet.get_Range("K6", "K" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // L6->Last
                formatRange = oSheet.get_Range("L6", "L" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // M6->Last
                formatRange = oSheet.get_Range("M6", "M" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // N6->Last
                formatRange = oSheet.get_Range("N6", "N" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // O6->Last
                formatRange = oSheet.get_Range("O6", "O" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // P6->Last
                formatRange = oSheet.get_Range("P6", "P" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // Q6->Last
                formatRange = oSheet.get_Range("Q6", "Q" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // R6->Last
                formatRange = oSheet.get_Range("R6", "R" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // S6->Last
                formatRange = oSheet.get_Range("S6", "S" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // T6->Last
                formatRange = oSheet.get_Range("T6", "T" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // U6->Last
                formatRange = oSheet.get_Range("U6", "U" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, true);

                // V6-Last
                formatRange = oSheet.get_Range("V6", "V" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // W6->Last
                formatRange = oSheet.get_Range("W6", "W" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // X6->Last
                formatRange = oSheet.get_Range("X6", "X" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // Y6->Last
                formatRange = oSheet.get_Range("Y6", "Y" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                //End Data Table

                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoSoSanh(int LoaiBaoCao)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_DM", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSUngVienThamGiaTD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                cmd.Parameters.Add("@ID_YCTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboYeuCauTD.EditValue);
                cmd.Parameters.Add("@ID_VTTD", SqlDbType.BigInt).Value = Convert.ToInt64(cboViTriTD.EditValue);
                cmd.Parameters.Add("@ID_TTYC", SqlDbType.BigInt).Value = Convert.ToInt64(cboTinhTrangYeuCau.EditValue);
                cmd.Parameters.Add("@LOAI_BC", SqlDbType.Int).Value = LoaiBaoCao;


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


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                //Header của báo cáo
                HeaderReport(ref oSheet, LoaiBaoCao, fontName, fontSizeNoiDung, lastColumn, fontSizeTieuDe);

                // Title Table
                Range row5_STT = oSheet.get_Range("A5");
                row5_STT.Value2 = "STT";
                FormatTitleTable(ref row5_STT, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 5);

                Range row5_MaSo_UV = oSheet.get_Range("B5");
                row5_MaSo_UV.Value2 = "Mã số yêu cầu tuyển dụng";
                FormatTitleTable(ref row5_MaSo_UV, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_HoTen = oSheet.get_Range("C5");
                row5_HoTen.Value2 = "Ngày lập yêu cầu";
                FormatTitleTable(ref row5_HoTen, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_NgaySinh = oSheet.get_Range("D5");
                row5_NgaySinh.Value2 = "Ngày vào làm";
                FormatTitleTable(ref row5_NgaySinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_GioiTinh = oSheet.get_Range("E5");
                row5_GioiTinh.Value2 = "Bộ phận";
                FormatTitleTable(ref row5_GioiTinh, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_CMND = oSheet.get_Range("F5");
                row5_CMND.Value2 = "Vị trí tuyển dụng";
                FormatTitleTable(ref row5_CMND, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 20);

                Range row5_NgayCap = oSheet.get_Range("G5");
                row5_NgayCap.Value2 = "Số lượng tham gia phỏng vấn";
                FormatTitleTable(ref row5_NgayCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_NoiCap = oSheet.get_Range("H5");
                row5_NoiCap.Value2 = "Số lượng đạt phỏng vấn";
                FormatTitleTable(ref row5_NoiCap, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_SLDL = oSheet.get_Range("I5");
                row5_SLDL.Value2 = "Số lượng đi làm";
                FormatTitleTable(ref row5_SLDL, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_DiaChi = oSheet.get_Range("J5");
                row5_DiaChi.Value2 = "% Tỷ lệ đi làm";
                FormatTitleTable(ref row5_DiaChi, fontName, fontSizeNoiDung, Color.FromArgb(255, 255, 0), 15);

                Range row5_FormatTieuDe = oSheet.get_Range("A5", "J5");
                row5_FormatTieuDe.Font.Bold = true;
                //End title table

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                int col_bd = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
                int cotJ = 6;
                for (int i = 0; i < rowCnt - 5; i++)
                {
                    formatRange = oSheet.get_Range("J" + cotJ.ToString() + "");
                    formatRange.Value = "=IFERROR(I" + cotJ+"/H"+ cotJ + ",0)";
                    cotJ++;
                }
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

                // Data table
                // A6->Last
                formatRange = oSheet.get_Range("A6", "A" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // B6->Last
                formatRange = oSheet.get_Range("B6", "B" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // C6->Last
                formatRange = oSheet.get_Range("C6", "C" + (rowCnt).ToString());
                formatRange.EntireColumn.NumberFormat = "DD/MM/YYYY";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;
                //FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // D6->Last
                formatRange = oSheet.get_Range("D6", "D" + (rowCnt).ToString());
                formatRange.EntireColumn.NumberFormat = "DD/MM/YYYY";
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;
                //FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // E6->Last
                formatRange = oSheet.get_Range("E6", "E" + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                // F6->Last
                formatRange = oSheet.get_Range("F6", "F" + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                // G6->Last
                formatRange = oSheet.get_Range("G6", "G" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // H6->Last
                formatRange = oSheet.get_Range("H6", "H" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // I6->Last
                formatRange = oSheet.get_Range("I6", "I" + (rowCnt).ToString());
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);

                // J6->Last
                formatRange = oSheet.get_Range("J6", "J" + (rowCnt).ToString());
                formatRange.NumberFormat = "0.0%";
                FormatDataTable(ref formatRange, fontName, fontSizeNoiDung, false);


                //End Data Table
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormatDataTable(ref Microsoft.Office.Interop.Excel.Range formatRange, string fontName = "Times New Roman", int fontSizeNoiDung = 11, bool isFormatNumberic = false)
        {
            formatRange.Font.Name = fontName;
            formatRange.Font.Size = fontSizeNoiDung;
            formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            formatRange.WrapText = true;
            if (isFormatNumberic)
            {
                formatRange.NumberFormat = "dd/MM/yyyy";
            }
        }
        private void FormatTitleTable(ref Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, Color BackgroundColor = default(Color), int ColumnWidth = 15)
        {
            range.Font.Name = fontName;
            range.Interior.Color = Color.FromArgb(255, 255, 0);
            range.RowHeight = 40;
            range.ColumnWidth = ColumnWidth;
            range.WrapText = true;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
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

        private void cboYeuCauTD_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboViTriTD, Commons.Modules.ObjSystems.DataViTri(Convert.ToInt64(cboYeuCauTD.EditValue), true), "ID_VTTD", "TEN_VTTD", "TEN_VTTD");
        }

        private void dTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
            //dTuNgay.EditValue = firstDateTime;
            int t = DateTime.DaysInMonth(dTuNgay.DateTime.Year, dTuNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(dTuNgay.DateTime.Year, Convert.ToInt32(dTuNgay.DateTime.Month), t);
            dDenNgay.EditValue = secondDateTime;
        }
    }
}
