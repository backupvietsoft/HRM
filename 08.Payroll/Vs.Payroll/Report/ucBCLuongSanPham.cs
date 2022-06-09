using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Drawing;

namespace Vs.Payroll
{
    public partial class ucBCLuongSanPham : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBCLuongSanPham()
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

        private void ucBCLuongSanPham_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadCboHopDong();
            LoadCboMaHang();
            LoadCboOrder();
            LoadCboChuyen();
            LoadGrvCongNhan();

            lk_TuNgay.EditValue = Convert.ToDateTime("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year).ToString("dd/MM/yyyy");
            DateTime dtTN = DateTime.Today;
            DateTime dtDN = DateTime.Today;
            lk_DenNgay.EditValue = dtTN.AddDays((-1));
            dtDN = dtDN.AddMonths(1);
            lk_NgayIn.EditValue = dtDN;
            Commons.Modules.sLoad = "";
        }

        private void LoadCboHopDong()
        {
            try
            {
                DataTable dtHD = new DataTable();
                dtHD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoLSP_GetCbo", 1, -1, -1, -1, -1, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if(LK_HOP_DONG.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_HOP_DONG, dtHD, "ID_DHB", "SO_DHB", "SO_DHB");
                }
                else
                {
                    LK_HOP_DONG.Properties.DataSource = dtHD;
                }
                LK_HOP_DONG.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadCboMaHang()
        {
            try
            {
                DataTable dtMH = new DataTable();
                dtMH.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoLSP_GetCbo", 2, -1, LK_HOP_DONG.EditValue, -1, -1, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if(LK_MA_HANG.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_MA_HANG, dtMH, "ID_HH", "TEN_HH", "TEN_HH");
                }
                else
                {
                    LK_MA_HANG.Properties.DataSource = dtMH;
                }

                LK_MA_HANG.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadCboOrder()
        {
            try
            {
                DataTable dtORD = new DataTable();
                dtORD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoLSP_GetCbo", 3, -1, LK_HOP_DONG.EditValue, LK_MA_HANG.EditValue, -1, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if(LK_ORDER.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_ORDER, dtORD, "ID_ORD", "ORDER_NUMBER", "ORDER_NUMBER");
                }
                else
                {
                    LK_ORDER.Properties.DataSource = dtORD;
                }

                LK_ORDER.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void LoadCboChuyen()
        {
            try
            {
                DataTable dtChuyen = new DataTable();
                dtChuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoLSP_GetCbo", 4, -1, LK_HOP_DONG.EditValue, LK_MA_HANG.EditValue, LK_ORDER.EditValue, lk_TuNgay.EditValue, lk_DenNgay.EditValue));
                if(LK_CHUYEN.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_CHUYEN, dtChuyen, "ID_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");
                }
                else
                {
                    LK_CHUYEN.Properties.DataSource = dtChuyen;
                }

                LK_CHUYEN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
                        frmViewReport frm = new frmViewReport();
                        DataTable dt;
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    //string sTieuDe = "DANH SÁCH NHÂN VIÊN ĐI TRỄ VỀ SỚM THEO GIAI ĐOẠN";

                                    frm.rpt = new rptBangLSPTongHopTheoCN(lk_TuNgay.DateTime, lk_DenNgay.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopTheoCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    //string sTieuDe = "DANH SÁCH NHÂN VIÊN ĐI TRỄ VỀ SỚM THEO GIAI ĐOẠN";

                                    frm.rpt = new rptBangLSPTheoMaHang(lk_TuNgay.DateTime, lk_DenNgay.DateTime);

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
                                        cmd.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                        cmd.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 2:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangLSPTongHopMHTheoCN(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        int idCN = -1;
                                        if (chkInTheoCongNhan.Checked)
                                        {
                                            idCN = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                                        }

                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLSPTongHopMHTheoCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CN", SqlDbType.Int).Value = idCN;
                                        cmd.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                        cmd.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 3:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangLSPChiTietMHNgayTheoCN(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        int idCN = -1;
                                        if (chkInTheoCongNhan.Checked)
                                        {
                                            idCN = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                                        }

                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHNgayTheoCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CN", SqlDbType.Int).Value = idCN;
                                        cmd.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                        cmd.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }

                                    frm.ShowDialog();
                                }
                                break;
                            case 4:
                                {
                                    try
                                    {
                                        int idCN = -1;

                                        if (chkInTheoCongNhan.Checked)
                                        {
                                            idCN = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                                        }

                                        System.Data.SqlClient.SqlConnection conn;
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        DataTable dtTTChung;
                                        DataTable dtChuyen;
                                        DataTable dtBCLSP;

                                        dtTTChung = new DataTable();
                                        dtTTChung = Commons.Modules.ObjSystems.DataThongTinChung();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetLSPChuyen", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@CN", SqlDbType.Int).Value = idCN;
                                        cmd.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                        cmd.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dtChuyen = new DataTable();
                                        dtChuyen = ds.Tables[0].Copy();

                                        Excel.Application oApp;
                                        Excel.Workbook oBook;
                                        Excel.Worksheet oSheet;

                                        oApp = new Excel.Application();
                                        oApp.Visible = true;

                                        oBook = oApp.Workbooks.Add();
                                        oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                                        string fontName = "Times New Roman";
                                        int fontSizeTieuDe = 16;
                                        int fontSizeNoiDung = 12;
                                        int oRow = 1;

                                        foreach (DataRow rowC in dtChuyen.Rows)
                                        {
                                            if (oRow == 1)
                                            {
                                                Excel.Range row1_ThongTinCty = oSheet.get_Range("A1", "H1");
                                                row1_ThongTinCty.Merge();
                                                row1_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row1_ThongTinCty.Font.Name = fontName;
                                                row1_ThongTinCty.Font.Bold = true;
                                                row1_ThongTinCty.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                                row1_ThongTinCty.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                row1_ThongTinCty.Value2 = dtTTChung.Rows[0][0];

                                                Excel.Range row2_ThongTinCty = oSheet.get_Range("A2", "H2");
                                                row2_ThongTinCty.Merge();
                                                row2_ThongTinCty.Font.Size = fontSizeNoiDung;
                                                row2_ThongTinCty.Font.Name = fontName;
                                                row2_ThongTinCty.Font.Bold = true;
                                                row2_ThongTinCty.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                                row2_ThongTinCty.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                row2_ThongTinCty.Value2 = dtTTChung.Rows[0][2];

                                                Excel.Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "H4");
                                                row4_TieuDe_BaoCao.Merge();
                                                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                                                row4_TieuDe_BaoCao.Font.Name = fontName;
                                                row4_TieuDe_BaoCao.Font.Bold = true;
                                                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                row4_TieuDe_BaoCao.RowHeight = 30;
                                                row4_TieuDe_BaoCao.Value2 = "BẢNG LƯƠNG SẢN PHẨM MÃ HÀNG CÔNG NHÂN THEO CHUYỀN";

                                                Excel.Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", "H5");
                                                row5_TieuDe_BaoCao.Merge();
                                                row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                                row5_TieuDe_BaoCao.Font.Name = fontName;
                                                row5_TieuDe_BaoCao.Font.Bold = true;
                                                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                                row5_TieuDe_BaoCao.RowHeight = 20;
                                                row5_TieuDe_BaoCao.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy");

                                                oRow = 7;
                                            }

                                            Excel.Range row_Chuyen = oSheet.get_Range("A" + oRow.ToString(), "H" + oRow.ToString());
                                            row_Chuyen.Merge();
                                            row_Chuyen.Value2 = "Chuyền : " + rowC[1].ToString();
                                            row_Chuyen.Font.Size = fontSizeNoiDung;
                                            row_Chuyen.Font.Name = fontName;
                                            row_Chuyen.Font.Bold = true;
                                            row_Chuyen.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                                            row_Chuyen.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                            row_Chuyen.RowHeight = 30;

                                            oRow++;

                                            System.Data.SqlClient.SqlCommand cmdCT = new System.Data.SqlClient.SqlCommand("rptBangLSPChiTietMHCNTheoChuyen", conn);
                                            cmdCT.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmdCT.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmdCT.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmdCT.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmdCT.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmdCT.Parameters.Add("@CN", SqlDbType.Int).Value = idCN;
                                            cmdCT.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                            cmdCT.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                            cmdCT.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                            cmdCT.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = rowC[0];
                                            cmdCT.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                            cmdCT.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                                            oSheet.Cells[oRow, 4] = "Tên xí nghiệp/P.ban";
                                            oSheet.Cells[oRow, 4].ColumnWidth = 20;
                                            oSheet.Cells[oRow, 5] = "Tên tổ";
                                            oSheet.Cells[oRow, 5].ColumnWidth = 20;
                                            oSheet.Cells[oRow, totalColumn + 1] = "Tổng cộng";
                                            oSheet.Cells[oRow, totalColumn + 1].ColumnWidth = 15;
                                            oSheet.Cells[oRow, totalColumn + 2] = "CN ký xác nhận";

                                            string LastTitleColumn = string.Empty;
                                            LastTitleColumn = CharacterIncrement(totalColumn + 1);
                                            Excel.Range row_TieuDe_BaoCao = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                                            row_TieuDe_BaoCao.Font.Name = fontName;
                                            row_TieuDe_BaoCao.Font.Bold = true;
                                            row_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            row_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
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
                                            oSheet.get_Range("A" + rowBD, "A" + oRow.ToString()).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            BorderAround(oSheet.get_Range("A" + rowBD, LastTitleColumn + oRow.ToString()));

                                            Excel.Range formatRange;
                                            string CurentColumn = string.Empty;
                                            for (int colMH = 5; colMH <= totalColumn - 1; colMH++)
                                            {
                                                CurentColumn = CharacterIncrement(colMH);
                                                formatRange = oSheet.get_Range(CurentColumn + rowBD, CurentColumn + oRow.ToString());
                                                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }

                                            //set formular
                                            oSheet.Cells[rowBD, totalColumn + 1] = "=SUM(F" + rowBD + ":" + lastColumn + rowBD + ")";
                                            oSheet.Cells[rowBD, totalColumn + 1].NumberFormat = "#,##0;(#,##0); ; ";
                                            oSheet.Cells[rowBD, totalColumn + 1].Copy();

                                            CurentColumn = CharacterIncrement(totalColumn);
                                            Excel.Range formularRange = oSheet.get_Range(CurentColumn + (rowBD + 1).ToString(), CurentColumn + oRow.ToString());
                                            formularRange.PasteSpecial(Excel.XlPasteType.xlPasteFormulas);
                                            formularRange.NumberFormat = "#,##0;(#,##0); ; ";

                                            oRow++;
                                            Excel.Range row_TongCong = oSheet.get_Range("A" + oRow.ToString(), "E" + oRow.ToString());
                                            row_TongCong.Merge();
                                            row_TongCong.Font.Size = fontSizeNoiDung;
                                            row_TongCong.Font.Name = fontName;
                                            row_TongCong.Font.Bold = true;
                                            row_TongCong.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            row_TongCong.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                            row_TongCong.RowHeight = 30;
                                            row_TongCong.Value2 = "Tổng cộng";

                                            for (int colMH = 6; colMH <= totalColumn + 1; colMH++)
                                            {
                                                CurentColumn = CharacterIncrement(colMH - 1);
                                                oSheet.Cells[oRow, colMH] = "=SUM(" + CurentColumn + rowBD.ToString() + ":" + CurentColumn + (oRow - 1).ToString() + ")";
                                                oSheet.Cells[oRow, colMH].NumberFormat = "#,##0;(#,##0); ; ";
                                            }

                                            Excel.Range row_Format_TongCong = oSheet.get_Range("A" + oRow.ToString(), LastTitleColumn + oRow.ToString());
                                            row_Format_TongCong.Font.Size = fontSizeNoiDung;
                                            row_Format_TongCong.Font.Name = fontName;
                                            row_Format_TongCong.Font.Bold = true;
                                            row_Format_TongCong.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                                            BorderAround(row_Format_TongCong);

                                            oRow = oRow + 2;
                                        }
                                    }
                                    catch
                                    { }

                                    // frm.ShowDialog();
                                }
                                break;
                            case 5:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();

                                    frm.rpt = new rptBangTongHopLuongMaHang(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

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
                                        cmd.Parameters.Add("@DH", SqlDbType.Int).Value = LK_HOP_DONG.EditValue;
                                        cmd.Parameters.Add("@MH", SqlDbType.Int).Value = LK_MA_HANG.EditValue;
                                        cmd.Parameters.Add("@ORD", SqlDbType.Int).Value = LK_ORDER.EditValue;
                                        cmd.Parameters.Add("@CHUYEN", SqlDbType.Int).Value = LK_CHUYEN.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
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
            try
            {
                DataTable dtCongNhan = new DataTable();
                dtCongNhan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanBC", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue,
                                                        LK_TO.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdCN.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dtCongNhan, true, false, false, true, true, this.Name);
                    dtCongNhan.Columns["CHON"].ReadOnly = false;
                }
                else
                {
                    grdCN.DataSource = dtCongNhan;
                }
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



        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadGrvCongNhan();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadGrvCongNhan();
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
        }

        private void lk_TuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboHopDong();
            LoadCboMaHang();
            LoadCboOrder();
            LoadCboChuyen();
        }

        private void lk_DenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboHopDong();
            LoadCboMaHang();
            LoadCboOrder();
            LoadCboChuyen();
        }

        private void LK_HOP_DONG_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboMaHang();
            LoadCboOrder();
            LoadCboChuyen();
        }

        private void LK_MA_HANG_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboOrder();
            LoadCboChuyen();
        }

        private void LK_ORDER_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboChuyen();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 1:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 2:
                    {
                        chkInTheoCongNhan.Enabled = true;
                        grdCN.Visible = true;
                    }
                    break;
                case 3:
                    {
                        chkInTheoCongNhan.Enabled = true;
                        grdCN.Visible = true;
                    }
                    break;
                case 4:
                    {
                        chkInTheoCongNhan.Enabled = true;
                        grdCN.Visible = true;
                    }
                    break;
                case 5:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
            }
        }
    }
}
