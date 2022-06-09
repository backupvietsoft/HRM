using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using DevExpress.XtraEditors.Repository;

namespace Vs.HRM
{
    public partial class ucTroCapBHXH : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucTroCapBHXH _instance;
        public static ucTroCapBHXH Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTroCapBHXH();
                return _instance;
            }
        }

        #region trợ cấp bảo hiểm y tế

        string sbtDCTroCapBHXH = "tabDCTroCapBHXH" + Commons.Modules.UserName;
        string sbtChonDCTroCapBHXH = "tabChonDCTroCapBHXH" + Commons.Modules.UserName;
        bool val = true;
        string sLDV = "";
        int iIDCN = 0;
        double dPT = 0;
        int iSoNgay = 0;
        int iSoCon = 0;

        public ucTroCapBHXH()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucTroCapBHXH_Load(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadThang();
            LoadDot();
            LoadGrdTroCapBHXH();
            LoadGrdDCTroCapBHXH(false);
            Commons.OSystems.DinhDangNgayThang(grvTroCapBHXH);
            enableButon(true);
            Commons.Modules.sPS = "";
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                LoadDot();
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

        //TUNG sua 19-02-2021
        #region function chung

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            windowsUIButton.Buttons[8].Properties.Visible = false;
            cboThang.ReadOnly = !visible;
            cboDot.ReadOnly = !visible;
        }

        private void LoadThang()
        {
            try
            {

                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.TRO_CAP_BHXH ORDER BY Y DESC , M DESC";
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

        private void LoadDot()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT DOT FROM TRO_CAP_BHXH WHERE CONVERT(NVARCHAR(10),THANG,103) = '01/" + cboThang.Text + "' ORDER BY DOT"));
                Commons.Modules.ObjSystems.MLoadComboboxEdit(cboDot, dt, "DOT");
                if (dt.Rows.Count > 0)
                {
                    cboDot.SelectedIndex = 0;
                }
                else
                {
                    cboDot.Text = Convert.ToString(1);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void LoadGrdTroCapBHXH()
        {
            try
            {
                DateTime dNgay = DateTime.Parse("01/" + cboThang.Text);
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTroCapBHXH", dNgay, cboDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTroCapBHXH, grvTroCapBHXH, dt, false, true, false, true, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvTroCapBHXH, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "CONG_NHAN");
                Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false), "ID_LDV", "LY_DO_VANG");
                Commons.Modules.ObjSystems.AddCombXtra("ID_HTNTC", "NOI_DUNG", grvTroCapBHXH, Commons.Modules.ObjSystems.DataHinhThucTroCap(-1, false), "ID_HTNTC", "HINH_THUC_NHAN_TRO_CAP");

                grvTroCapBHXH.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_NGAY_NGHI"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_NGAYLK"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["HS_LUONG"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_TIEN_TC"].OptionsColumn.ReadOnly = true;

                grvTroCapBHXH.Columns["ID_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvTroCapBHXH.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvTroCapBHXH.Columns["ID_LDV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                grvTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvTroCapBHXH.Columns["MS_CN"].Width = 100;
                grvTroCapBHXH.Columns["ID_LDV"].Width = 200;
                grvTroCapBHXH.Columns["ID_HTNTC"].Width = 200;

                grvTroCapBHXH.Columns["ID_TC_BHXH"].Visible = false;
                grvTroCapBHXH.Columns["LAN_TS"].Visible = false;
                grvTroCapBHXH.Columns["LUONG_CB"].Visible = false;

                grvTroCapBHXH.Columns["HS_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                grvTroCapBHXH.Columns["HS_LUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                grvTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);
                grvTroCapBHXH.Columns["NGHI_TU_NGAY"].ColumnEdit = dEditN;
                grvTroCapBHXH.Columns["NGHI_DEN_NGAY"].ColumnEdit = dEditN;

                grvTroCapBHXH.Columns["NGHI_TU_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvTroCapBHXH.Columns["NGHI_TU_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvTroCapBHXH.Columns["NGHI_DEN_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvTroCapBHXH.Columns["NGHI_DEN_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";

            }
            catch (Exception ex) { }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "chon":
                    {
                        //ItemForThang.Visibility = LayoutVisibility.Never;
                        //ItemForDateThang.Visibility = LayoutVisibility.Always;
                        navigationFrame1.SelectedPage = navigationPage2;
                        enableButon(false);
                        windowsUIButton.Buttons[3].Properties.Visible = false;
                        windowsUIButton.Buttons[5].Properties.Visible = false;
                        windowsUIButton.Buttons[6].Properties.Visible = false;
                        windowsUIButton.Buttons[7].Properties.Visible = true;
                        //tạo bảng tạm trợ cấp bảo hiễm xã hội
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtDCTroCapBHXH, Commons.Modules.ObjSystems.ConvertDatatable(grdDCTroCapBHXH), "");
                        LoadGrdChonTroCapBHXH();
                        break;
                    }
                case "trove":
                    {
                        navigationFrame1.SelectedPage = navigationPage1;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (string.IsNullOrEmpty(cboThang.Text) || string.IsNullOrEmpty(cboDot.Text))
                        {
                            Commons.Modules.ObjSystems.msgChung("msgThangkhongduocdetrong");
                            return;
                        }

                        Commons.Modules.ObjSystems.AddnewRow(grvTroCapBHXH, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvDCTroCapBHXH, true);
                        //ItemForThang.Visibility = LayoutVisibility.Never;
                        //ItemForDateThang.Visibility = LayoutVisibility.Always;
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaTroCapBaoHiemXH();
                        break;
                    }
                case "in":
                    {
                        string sThang = cboThang.EditValue.ToString();
                        string sDot = cboDot.EditValue.ToString();

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        DataTable dt;
                        DataTable dtO1;
                        DataTable dtO2;
                        DataTable dtO3;
                        DataTable dtO4;
                        DataTable dtO5;
                        DataTable dtO6;
                        DataTable dtO7;
                        DataTable dtO8;
                        DataTable dtO9;
                        DataTable dt10;
                        DataTable dt11;
                        DataTable dt12;
                        DataTable dt13;
                        DataTable dt14;
                        DataTable dt15;

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptTroCapBHXH", conn);

                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        cmd.Parameters.Add("@DV", SqlDbType.Int).Value = 1;
                        cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                        cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = cboDot.EditValue;
                        cmd.CommandType = CommandType.StoredProcedure;
                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        dt = new DataTable();
                        dt = ds.Tables[0].Copy();

                        dtO1 = new DataTable();
                        dtO1 = ds.Tables[1].Copy();

                        dtO2 = new DataTable();
                        dtO2 = ds.Tables[2].Copy();

                        dtO3 = new DataTable();
                        dtO3 = ds.Tables[3].Copy();

                        dtO4 = new DataTable();
                        dtO4 = ds.Tables[4].Copy();

                        dtO5 = new DataTable();
                        dtO5 = ds.Tables[5].Copy();

                        dtO6 = new DataTable();
                        dtO6 = ds.Tables[6].Copy();

                        dtO7 = new DataTable();
                        dtO7 = ds.Tables[7].Copy();

                        dtO8 = new DataTable();
                        dtO8 = ds.Tables[8].Copy();

                        dtO9 = new DataTable();
                        dtO9 = ds.Tables[9].Copy();

                        dt10 = new DataTable();
                        dt10 = ds.Tables[10].Copy();

                        dt11 = new DataTable();
                        dt11 = ds.Tables[11].Copy();

                        dt12 = new DataTable();
                        dt12 = ds.Tables[12].Copy();

                        dt13 = new DataTable();
                        dt13 = ds.Tables[13].Copy();

                        dt14 = new DataTable();
                        dt14 = ds.Tables[14].Copy();

                        dt15 = new DataTable();
                        dt15 = ds.Tables[15].Copy();

                        //string saveExcelFile = @"D:\excel_report.xlsx";
                        string saveExcelFile;

                        saveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");


                        Excel.Application xlApp = new Excel.Application();

                        if (xlApp == null)
                        {
                            MessageBox.Show("Lỗi không thể sử dụng được thư viện EXCEL");
                            return;
                        }
                        object misValue = System.Reflection.Missing.Value;

                        xlApp.Visible = true;
                        Workbook wb = xlApp.Workbooks.Add(misValue);

                        Worksheet ws = (Worksheet)wb.Worksheets[1];

                        if (ws == null)
                        {
                            MessageBox.Show("Không thể tạo được WorkSheet");
                            return;
                        }

                        int row = 1;
                        string fontName = "Times New Roman";
                        int fontSizeTieuDe = 14;
                        int fontSizeNoiDung = 12;
                        string donvi = "";
                        string madonvi = "";
                        string diachi = "";
                        string sotk = "";

                        foreach (DataRow rowDV in dt.Rows)
                        {
                            donvi = "Tên đơn vị : " + rowDV["TEN_DV"].ToString();
                            madonvi = "Mã đơn vị : " + rowDV["MS_BHXH"].ToString();
                            diachi = "Địa chỉ : " + rowDV["DIA_CHI"].ToString();
                            sotk = "Số tài khoản : " + rowDV["SO_TAI_KHOAN"].ToString() + " Mở tại : " + rowDV["TEN_NGAN_HANG"].ToString();
                        }
                        //Xuất dòng Tiêu đề của File báo cáo: Lưu ý 
                        Range row1_TieuDe_DonVi = ws.get_Range("A1", "E1");
                        row1_TieuDe_DonVi.Merge();
                        row1_TieuDe_DonVi.Font.Size = fontSizeNoiDung;
                        row1_TieuDe_DonVi.Font.Name = fontName;
                        row1_TieuDe_DonVi.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row1_TieuDe_DonVi.Value2 = donvi;

                        Range row1_TieuDe_ISO = ws.get_Range("G1", "G1");
                        row1_TieuDe_ISO.Merge();
                        row1_TieuDe_ISO.Font.Size = fontSizeNoiDung;
                        row1_TieuDe_ISO.Font.Name = fontName;
                        row1_TieuDe_ISO.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row1_TieuDe_ISO.Value2 = "Mẫu 01B - HSB";

                        Range row2_TieuDe_DonVi = ws.get_Range("A2", "E2");
                        row2_TieuDe_DonVi.Merge();
                        row2_TieuDe_DonVi.Font.Size = fontSizeNoiDung;
                        row2_TieuDe_DonVi.Font.Name = fontName;
                        row2_TieuDe_DonVi.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row2_TieuDe_DonVi.Value2 = madonvi;

                        Range row3_TieuDe_DonVi = ws.get_Range("A3", "E3");
                        row3_TieuDe_DonVi.Merge();
                        row3_TieuDe_DonVi.Font.Size = fontSizeNoiDung;
                        row3_TieuDe_DonVi.Font.Name = fontName;
                        row3_TieuDe_DonVi.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row3_TieuDe_DonVi.Value2 = diachi;

                        Range row4_TieuDe_BaoCao = ws.get_Range("A4", "J4");
                        row4_TieuDe_BaoCao.Merge();
                        row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                        row4_TieuDe_BaoCao.Font.Name = fontName;
                        row4_TieuDe_BaoCao.Font.Bold = true;
                        row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe_BaoCao.RowHeight = 50;
                        row4_TieuDe_BaoCao.Value2 = "DANH SÁCH ĐỀ NGHỊ GIẢM QUYẾT HƯỞNG CHẾ ĐỘ, ỐM ĐAU, THAI SẢN,\r\n DƯỠNG SỨC PHỤC HỒI SỨC KHỎE";

                        Range row5_TieuDe_BaoCao = ws.get_Range("A5", "J5");
                        row5_TieuDe_BaoCao.Merge();
                        row5_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row5_TieuDe_BaoCao.Font.Name = fontName;
                        row5_TieuDe_BaoCao.Font.Bold = true;
                        row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row5_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe_BaoCao.RowHeight = 25;
                        row5_TieuDe_BaoCao.Value2 = "Đợt " + sDot + " tháng " + sThang.Substring(0, 2) + " năm " + sThang.Substring(3, 4);

                        Range row6_TieuDe_BaoCao = ws.get_Range("A6", "J6");
                        row6_TieuDe_BaoCao.Merge();
                        row6_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row6_TieuDe_BaoCao.Font.Name = fontName;
                        row6_TieuDe_BaoCao.Font.Bold = true;
                        row6_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row6_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row6_TieuDe_BaoCao.RowHeight = 25;
                        row6_TieuDe_BaoCao.Value2 = sotk;

                        Range row8_TieuDe_BaoCao = ws.get_Range("A8", "J8");
                        row8_TieuDe_BaoCao.Merge();
                        row8_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                        row8_TieuDe_BaoCao.Font.Name = fontName;
                        row8_TieuDe_BaoCao.Font.Bold = true;
                        row8_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row8_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row8_TieuDe_BaoCao.RowHeight = 20;
                        row8_TieuDe_BaoCao.Value2 = "PHẦN 1 : DANH SÁCH HƯỞNG CHẾ ĐỘ MỚI PHÁT SINH";

                        Range row9_TieuDe_Format = ws.get_Range("A9", "J11");
                        row9_TieuDe_Format.Font.Size = fontSizeNoiDung;
                        row9_TieuDe_Format.Font.Name = fontName;
                        row9_TieuDe_Format.Font.Bold = true;
                        row9_TieuDe_Format.WrapText = true;
                        row9_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row9_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range row9_TieuDe_Stt = ws.get_Range("A9", "A10");
                        row9_TieuDe_Stt.Merge();
                        row9_TieuDe_Stt.Value2 = "Stt";
                        row9_TieuDe_Stt.ColumnWidth = 6;

                        Range row9_TieuDe1_Stt = ws.get_Range("A11");
                        row9_TieuDe1_Stt.Value2 = "A";

                        Range row9_TieuDe_HoTen = ws.get_Range("B9", "B10");
                        row9_TieuDe_HoTen.Merge();
                        row9_TieuDe_HoTen.Value2 = "Họ và tên";
                        row9_TieuDe_HoTen.ColumnWidth = 30;

                        Range row9_TieuDe1_HoTen = ws.get_Range("B11");
                        row9_TieuDe1_HoTen.Value2 = "B";

                        Range row9_TieuDe_Maso = ws.get_Range("C9", "C10");
                        row9_TieuDe_Maso.Merge();
                        row9_TieuDe_Maso.Value2 = "Mã số";
                        row9_TieuDe_Maso.ColumnWidth = 15;

                        Range row9_TieuDe_Masobh = ws.get_Range("D9", "D10");
                        row9_TieuDe_Masobh.Merge();
                        row9_TieuDe_Masobh.Value2 = "Mã số BHXH";
                        row9_TieuDe_Masobh.ColumnWidth = 15;

                        Range row9_TieuDe1_Masobh = ws.get_Range("D11");
                        row9_TieuDe1_Masobh.Value2 = "1";

                        Range row9_TieuDe_Songay = ws.get_Range("E9:G9");
                        row9_TieuDe_Songay.Merge();
                        row9_TieuDe_Songay.Value2 = "Số ngày nghỉ được tính hưởng trợ cấp";

                        Range row9_TieuDe_Tungay = ws.get_Range("E10");
                        row9_TieuDe_Tungay.Value2 = "Từ ngày";
                        row9_TieuDe_Tungay.ColumnWidth = 12;

                        Range row9_TieuDe1_Tungay = ws.get_Range("E11");
                        row9_TieuDe1_Tungay.Value2 = "2";

                        Range row9_TieuDe_Denngay = ws.get_Range("F10");
                        row9_TieuDe_Denngay.Value2 = "Đến ngày";
                        row9_TieuDe_Denngay.ColumnWidth = 12;

                        Range row9_TieuDe1_Denngay = ws.get_Range("F11");
                        row9_TieuDe1_Denngay.Value2 = "3";

                        Range row9_TieuDe_Tongso = ws.get_Range("G10");
                        row9_TieuDe_Tongso.Value2 = "Tổng số";
                        row9_TieuDe_Tongso.ColumnWidth = 12;

                        Range row9_TieuDe1_Tongso = ws.get_Range("G11");
                        row9_TieuDe1_Tongso.Value2 = "4";

                        Range row9_TieuDe_Thongtin = ws.get_Range("H9", "H10");
                        row9_TieuDe_Thongtin.Merge();
                        row9_TieuDe_Thongtin.Value2 = "Thông tin về tài khoản nhận trợ cấp";
                        row9_TieuDe_Thongtin.ColumnWidth = 20;

                        Range row9_TieuDe1_Thongtin = ws.get_Range("H11");
                        row9_TieuDe1_Thongtin.Value2 = "C";

                        Range row9_TieuDe_Chitieu = ws.get_Range("I9", "I10");
                        row9_TieuDe_Chitieu.Merge();
                        row9_TieuDe_Chitieu.Value2 = "Chỉ tiêu xác định điều kiện mức hưởng";
                        row9_TieuDe_Chitieu.ColumnWidth = 20;

                        Range row9_TieuDe1_Chitieu = ws.get_Range("I11");
                        row9_TieuDe1_Chitieu.Value2 = "D";

                        Range row9_TieuDe_Ghichu = ws.get_Range("J9", "J10");
                        row9_TieuDe_Ghichu.Merge();
                        row9_TieuDe_Ghichu.Value2 = "Ghi chú";
                        row9_TieuDe_Ghichu.ColumnWidth = 20;

                        Range row9_TieuDe1_Ghichu = ws.get_Range("J11");
                        row9_TieuDe1_Ghichu.Merge();
                        row9_TieuDe1_Ghichu.Value2 = "E";

                        //Chế độ ốm dau
                        Range row12_MucA_stt = ws.get_Range("A12");
                        row12_MucA_stt.Value2 = "A";
                        row12_MucA_stt.Font.Size = fontSizeNoiDung;
                        row12_MucA_stt.Font.Name = fontName;
                        row12_MucA_stt.Font.Bold = true;
                        row12_MucA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row12_MucA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range row12_MucA_ten = ws.get_Range("B12:E12");
                        row12_MucA_ten.Merge();
                        row12_MucA_ten.Font.Size = fontSizeNoiDung;
                        row12_MucA_ten.Font.Name = fontName;
                        row12_MucA_ten.Font.Bold = true;
                        row12_MucA_ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row12_MucA_ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row12_MucA_ten.Value2 = "CHẾ ĐỘ ỐM ĐAU";

                        //1. Ốm thường
                        Range rowO1_stt = ws.get_Range("A13");
                        rowO1_stt.Value2 = "I";
                        rowO1_stt.Font.Size = fontSizeNoiDung;
                        rowO1_stt.Font.Name = fontName;
                        rowO1_stt.Font.Bold = true;
                        rowO1_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO1_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowO1_Ten = ws.get_Range("B13:E13");
                        rowO1_Ten.Merge();
                        rowO1_Ten.Font.Size = fontSizeNoiDung;
                        rowO1_Ten.Font.Name = fontName;
                        rowO1_Ten.Font.Bold = true;
                        rowO1_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowO1_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO1_Ten.Value2 = "ỐM THƯỜNG";

                        int stt = 0;
                        row = 13;
                        int rowStar = 14;
                        if (dtO1.Rows.Count > 0)
                        {
                            foreach (DataRow rowO1 in dtO1.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO1["HO_TEN"].ToString(), rowO1["MS_CN"].ToString(), rowO1["SO_BHXH"].ToString(), rowO1["TU_NGAY"].ToString(),
                                rowO1["DEN_NGAY"].ToString(), rowO1["SO_NGAY_NGHI"].ToString(), rowO1["THONG_TIN_TK"].ToString(), rowO1["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO1["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }


                        row++;
                        Range rowO1_TongCong = ws.get_Range("B" + row);
                        rowO1_TongCong.Font.Size = fontSizeNoiDung;
                        rowO1_TongCong.Font.Name = fontName;
                        rowO1_TongCong.Font.Bold = true;
                        rowO1_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO1_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO1_TongCong.Value2 = "Cộng";

                        Range rowO1_TongNgayNghi = ws.get_Range("G" + row);
                        rowO1_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO1_TongNgayNghi.Font.Name = fontName;
                        rowO1_TongNgayNghi.Font.Bold = true;
                        rowO1_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO1_TongNgayNghi.NumberFormat = "#,##0";
                        rowO1_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //2. Ốm dài ngày
                        row++;
                        Range rowO2_stt = ws.get_Range("A" + row);
                        rowO2_stt.Value2 = "II";
                        rowO2_stt.Font.Size = fontSizeNoiDung;
                        rowO2_stt.Font.Name = fontName;
                        rowO2_stt.Font.Bold = true;
                        rowO2_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO2_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowO2_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowO2_Ten.Merge();
                        rowO2_Ten.Font.Size = fontSizeNoiDung;
                        rowO2_Ten.Font.Name = fontName;
                        rowO2_Ten.Font.Bold = true;
                        rowO2_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowO2_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO2_Ten.Value2 = "ỐM DÀI NGÀY";

                        rowStar = row + 1;
                        if (dtO2.Rows.Count > 0)
                        {
                            foreach (DataRow rowO2 in dtO2.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO2["HO_TEN"].ToString(), rowO2["MS_CN"].ToString(), rowO2["SO_BHXH"].ToString(), rowO2["TU_NGAY"].ToString(),
                                rowO2["DEN_NGAY"].ToString(), rowO2["SO_NGAY_NGHI"].ToString(), rowO2["THONG_TIN_TK"].ToString(), rowO2["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO2["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }


                        row++;
                        Range rowO2_TongCong = ws.get_Range("B" + row);
                        rowO2_TongCong.Font.Size = fontSizeNoiDung;
                        rowO2_TongCong.Font.Name = fontName;
                        rowO2_TongCong.Font.Bold = true;
                        rowO2_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO2_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO2_TongCong.Value2 = "Cộng";

                        Range rowO2_TongNgayNghi = ws.get_Range("G" + row);
                        rowO2_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO2_TongNgayNghi.Font.Name = fontName;
                        rowO2_TongNgayNghi.Font.Bold = true;
                        rowO2_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO2_TongNgayNghi.NumberFormat = "#,##0";
                        rowO2_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //3. Con ốm
                        row++;
                        Range rowO3_stt = ws.get_Range("A" + row);
                        rowO3_stt.Value2 = "III";
                        rowO3_stt.Font.Size = fontSizeNoiDung;
                        rowO3_stt.Font.Name = fontName;
                        rowO3_stt.Font.Bold = true;
                        rowO3_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO3_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range row03_Ten = ws.get_Range("B" + row + ":E" + row);
                        row03_Ten.Merge();
                        row03_Ten.Font.Size = fontSizeNoiDung;
                        row03_Ten.Font.Name = fontName;
                        row03_Ten.Font.Bold = true;
                        row03_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row03_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row03_Ten.Value2 = "CON ỐM";

                        rowStar = row + 1;
                        if (dtO3.Rows.Count > 0)
                        {
                            foreach (DataRow rowO3 in dtO3.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO3["HO_TEN"].ToString(), rowO3["MS_CN"].ToString(), rowO3["SO_BHXH"].ToString(), rowO3["TU_NGAY"].ToString(),
                                rowO3["DEN_NGAY"].ToString(), rowO3["SO_NGAY_NGHI"].ToString(), rowO3["THONG_TIN_TK"].ToString(), rowO3["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO3["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }


                        row++;
                        Range rowO3_TongCong = ws.get_Range("B" + row);
                        rowO3_TongCong.Font.Size = fontSizeNoiDung;
                        rowO3_TongCong.Font.Name = fontName;
                        rowO3_TongCong.Font.Bold = true;
                        rowO3_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO3_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO3_TongCong.Value2 = "Cộng";

                        Range rowO3_TongNgayNghi = ws.get_Range("G" + row);
                        rowO3_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO3_TongNgayNghi.Font.Name = fontName;
                        rowO3_TongNgayNghi.Font.Bold = true;
                        rowO3_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO3_TongNgayNghi.NumberFormat = "#,##0";
                        rowO3_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //Chế độ thai sản
                        row++;
                        Range rowB_stt = ws.get_Range("A" + row);
                        rowB_stt.Value2 = "B";
                        rowB_stt.Font.Size = fontSizeNoiDung;
                        rowB_stt.Font.Name = fontName;
                        rowB_stt.Font.Bold = true;
                        rowB_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowB_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowB_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowB_Ten.Merge();
                        rowB_Ten.Font.Size = fontSizeNoiDung;
                        rowB_Ten.Font.Name = fontName;
                        rowB_Ten.Font.Bold = true;
                        rowB_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowB_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowB_Ten.Value2 = "CHẾ ĐỘ THAI SẢN";

                        //1. Kham thai
                        row++;
                        Range rowKT_stt = ws.get_Range("A" + row);
                        rowKT_stt.Value2 = "I";
                        rowKT_stt.Font.Size = fontSizeNoiDung;
                        rowKT_stt.Font.Name = fontName;
                        rowKT_stt.Font.Bold = true;
                        rowKT_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowKT_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowKT_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowKT_Ten.Merge();
                        rowKT_Ten.Font.Size = fontSizeNoiDung;
                        rowKT_Ten.Font.Name = fontName;
                        rowKT_Ten.Font.Bold = true;
                        rowKT_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowKT_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowKT_Ten.Value2 = "KHÁM THAI";

                        stt = 0;
                        rowStar = row + 1;
                        if (dtO4.Rows.Count > 0)
                        {
                            foreach (DataRow rowO4 in dtO4.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO4["HO_TEN"].ToString(), rowO4["MS_CN"].ToString(), rowO4["SO_BHXH"].ToString(), rowO4["TU_NGAY"].ToString(),
                                rowO4["DEN_NGAY"].ToString(), rowO4["SO_NGAY_NGHI"].ToString(), rowO4["THONG_TIN_TK"].ToString(), rowO4["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO4["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO4_TongCong = ws.get_Range("B" + row);
                        rowO4_TongCong.Font.Size = fontSizeNoiDung;
                        rowO4_TongCong.Font.Name = fontName;
                        rowO4_TongCong.Font.Bold = true;
                        rowO4_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO4_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO4_TongCong.Value2 = "Cộng";

                        Range rowO4_TongNgayNghi = ws.get_Range("G" + row);
                        rowO4_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO4_TongNgayNghi.Font.Name = fontName;
                        rowO4_TongNgayNghi.Font.Bold = true;
                        rowO4_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO4_TongNgayNghi.NumberFormat = "#,##0";
                        rowO4_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //2. Sẩy thai, thai chết lưu, nạo hút thai
                        row++;
                        Range rowST_stt = ws.get_Range("A" + row);
                        rowST_stt.Value2 = "II";
                        rowST_stt.Font.Size = fontSizeNoiDung;
                        rowST_stt.Font.Name = fontName;
                        rowST_stt.Font.Bold = true;
                        rowST_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowST_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowST_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowST_Ten.Merge();
                        rowST_Ten.Font.Size = fontSizeNoiDung;
                        rowST_Ten.Font.Name = fontName;
                        rowST_Ten.Font.Bold = true;
                        rowST_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowST_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowST_Ten.Value2 = "SẨY THAI, THAI CHẾT LƯU, NẠO HÚT THAI";

                        stt = 0;
                        rowStar = row + 1;
                        int m = 0;
                        if (dtO5.Rows.Count > 0)
                        {
                            foreach (DataRow rowO5 in dtO5.Rows)
                            {
                                if (m != Convert.ToInt32(rowO5["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = rowO5["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(rowO5["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO5["HO_TEN"].ToString(), rowO5["MS_CN"].ToString(), rowO5["SO_BHXH"].ToString(), rowO5["TU_NGAY"].ToString(),
                                rowO5["DEN_NGAY"].ToString(), rowO5["SO_NGAY_NGHI"].ToString(), rowO5["THONG_TIN_TK"].ToString(), rowO5["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO5["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO5_TongCong = ws.get_Range("B" + row);
                        rowO5_TongCong.Font.Size = fontSizeNoiDung;
                        rowO5_TongCong.Font.Name = fontName;
                        rowO5_TongCong.Font.Bold = true;
                        rowO5_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO5_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO5_TongCong.Value2 = "Cộng";

                        Range rowO5_TongNgayNghi = ws.get_Range("G" + row);
                        rowO5_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO5_TongNgayNghi.Font.Name = fontName;
                        rowO5_TongNgayNghi.Font.Bold = true;
                        rowO5_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO5_TongNgayNghi.NumberFormat = "#,##0";
                        rowO5_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //3. Sinh con nuoi con nuoi
                        row++;
                        Range rowSC_stt = ws.get_Range("A" + row);
                        rowSC_stt.Value2 = "III";
                        rowSC_stt.Font.Size = fontSizeNoiDung;
                        rowSC_stt.Font.Name = fontName;
                        rowSC_stt.Font.Bold = true;
                        rowSC_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowSC_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowSC_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowSC_Ten.Merge();
                        rowSC_Ten.Font.Size = fontSizeNoiDung;
                        rowSC_Ten.Font.Name = fontName;
                        rowSC_Ten.Font.Bold = true;
                        rowSC_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowSC_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowSC_Ten.Value2 = "SINH CON, NUÔI CON NUÔI";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        string sm = "";
                        if (dtO6.Rows.Count > 0)
                        {
                            foreach (DataRow rowO6 in dtO6.Rows)
                            {

                                if (sm != rowO6["SO_MUC"].ToString())
                                {
                                    if (rowO6["SO_MUC"].ToString() == "T3A")
                                    {
                                        row++;
                                        Range rowSCA_stt = ws.get_Range("A" + row);
                                        rowSCA_stt.Value2 = "A";
                                        rowSCA_stt.Font.Size = fontSizeNoiDung;
                                        rowSCA_stt.Font.Name = fontName;
                                        rowSCA_stt.Font.Bold = true;
                                        rowSCA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowSCA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowSCA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowSCA_Ten.Merge();
                                        rowSCA_Ten.Font.Size = fontSizeNoiDung;
                                        rowSCA_Ten.Font.Name = fontName;
                                        rowSCA_Ten.Font.Bold = true;
                                        rowSCA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowSCA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowSCA_Ten.Value2 = "Trường hợp thông thường";
                                    }
                                    else
                                    {
                                        row++;
                                        Range rowSCA_stt = ws.get_Range("A" + row);
                                        rowSCA_stt.Value2 = "B";
                                        rowSCA_stt.Font.Size = fontSizeNoiDung;
                                        rowSCA_stt.Font.Name = fontName;
                                        rowSCA_stt.Font.Bold = true;
                                        rowSCA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowSCA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowSCA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowSCA_Ten.Merge();
                                        rowSCA_Ten.Font.Size = fontSizeNoiDung;
                                        rowSCA_Ten.Font.Name = fontName;
                                        rowSCA_Ten.Font.Bold = true;
                                        rowSCA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowSCA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowSCA_Ten.Value2 = "Trường hợp con chết";
                                    }
                                    sm = rowO6["SO_MUC"].ToString();
                                }
                                if (m != Convert.ToInt32(rowO6["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = rowO6["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(rowO6["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO6["HO_TEN"].ToString(), rowO6["MS_CN"].ToString(), rowO6["SO_BHXH"].ToString(), rowO6["TU_NGAY"].ToString(),
                                rowO6["DEN_NGAY"].ToString(), rowO6["SO_NGAY_NGHI"].ToString(), rowO6["THONG_TIN_TK"].ToString(), rowO6["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO6["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO6_TongCong = ws.get_Range("B" + row);
                        rowO6_TongCong.Font.Size = fontSizeNoiDung;
                        rowO6_TongCong.Font.Name = fontName;
                        rowO6_TongCong.Font.Bold = true;
                        rowO6_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO6_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO6_TongCong.Value2 = "Cộng";

                        Range rowO6_TongNgayNghi = ws.get_Range("G" + row);
                        rowO6_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO6_TongNgayNghi.Font.Name = fontName;
                        rowO6_TongNgayNghi.Font.Bold = true;
                        rowO6_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO6_TongNgayNghi.NumberFormat = "#,##0";
                        rowO6_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";


                        //4. nhận nuôi con nuôi
                        row++;
                        Range rowNCN_stt = ws.get_Range("A" + row);
                        rowNCN_stt.Value2 = "IV";
                        rowNCN_stt.Font.Size = fontSizeNoiDung;
                        rowNCN_stt.Font.Name = fontName;
                        rowNCN_stt.Font.Bold = true;
                        rowNCN_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowNCN_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowNCN_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowNCN_Ten.Merge();
                        rowNCN_Ten.Font.Size = fontSizeNoiDung;
                        rowNCN_Ten.Font.Name = fontName;
                        rowNCN_Ten.Font.Bold = true;
                        rowNCN_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowNCN_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowNCN_Ten.Value2 = "NHẬN NUÔI CON NUÔI";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        sm = "";
                        if (dtO7.Rows.Count > 0)
                        {
                            foreach (DataRow rowO7 in dtO7.Rows)
                            {
                                if (sm != rowO7["SO_MUC"].ToString())
                                {
                                    if (rowO7["SO_MUC"].ToString() == "T9A")
                                    {
                                        row++;
                                        Range rowNCNA_stt = ws.get_Range("A" + row);
                                        rowNCNA_stt.Value2 = "A";
                                        rowNCNA_stt.Font.Size = fontSizeNoiDung;
                                        rowNCNA_stt.Font.Name = fontName;
                                        rowNCNA_stt.Font.Bold = true;
                                        rowNCNA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowNCNA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowNCNA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowNCNA_Ten.Merge();
                                        rowNCNA_Ten.Font.Size = fontSizeNoiDung;
                                        rowNCNA_Ten.Font.Name = fontName;
                                        rowNCNA_Ten.Font.Bold = true;
                                        rowNCNA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowNCNA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowNCNA_Ten.Value2 = "Trường hợp thông thường";
                                    }
                                    else
                                    {
                                        row++;
                                        Range rowNCNA_stt = ws.get_Range("A" + row);
                                        rowNCNA_stt.Value2 = "B";
                                        rowNCNA_stt.Font.Size = fontSizeNoiDung;
                                        rowNCNA_stt.Font.Name = fontName;
                                        rowNCNA_stt.Font.Bold = true;
                                        rowNCNA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowNCNA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowNCNA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowNCNA_Ten.Merge();
                                        rowNCNA_Ten.Font.Size = fontSizeNoiDung;
                                        rowNCNA_Ten.Font.Name = fontName;
                                        rowNCNA_Ten.Font.Bold = true;
                                        rowNCNA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowNCNA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowNCNA_Ten.Value2 = "Trường hợp con chết";
                                    }
                                    sm = rowO7["SO_MUC"].ToString();
                                }
                                if (m != Convert.ToInt32(rowO7["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = rowO7["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(rowO7["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO7["HO_TEN"].ToString(), rowO7["MS_CN"].ToString(), rowO7["SO_BHXH"].ToString(), rowO7["TU_NGAY"].ToString(),
                                rowO7["DEN_NGAY"].ToString(), rowO7["SO_NGAY_NGHI"].ToString(), rowO7["THONG_TIN_TK"].ToString(), rowO7["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO7["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO7_TongCong = ws.get_Range("B" + row);
                        rowO7_TongCong.Font.Size = fontSizeNoiDung;
                        rowO7_TongCong.Font.Name = fontName;
                        rowO7_TongCong.Font.Bold = true;
                        rowO7_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO7_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO7_TongCong.Value2 = "Cộng";

                        Range rowO7_TongNgayNghi = ws.get_Range("G" + row);
                        rowO7_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO7_TongNgayNghi.Font.Name = fontName;
                        rowO7_TongNgayNghi.Font.Bold = true;
                        rowO7_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO7_TongNgayNghi.NumberFormat = "#,##0";
                        rowO7_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //5. lao động nhờ mang thai hộ sinh con
                        row++;
                        Range rowMTH_stt = ws.get_Range("A" + row);
                        rowMTH_stt.Value2 = "V";
                        rowMTH_stt.Font.Size = fontSizeNoiDung;
                        rowMTH_stt.Font.Name = fontName;
                        rowMTH_stt.Font.Bold = true;
                        rowMTH_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowMTH_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowMTH_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowMTH_Ten.Merge();
                        rowMTH_Ten.Font.Size = fontSizeNoiDung;
                        rowMTH_Ten.Font.Name = fontName;
                        rowMTH_Ten.Font.Bold = true;
                        rowMTH_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowMTH_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowMTH_Ten.Value2 = "LAO ĐỘNG NHỜ MANG THAI HỘ SINH CON";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        sm = "";
                        if (dtO8.Rows.Count > 0)
                        {
                            foreach (DataRow rowO8 in dtO8.Rows)
                            {
                                if (sm != rowO8["SO_MUC"].ToString())
                                {
                                    if (rowO8["SO_MUC"].ToString() == "T4A")
                                    {
                                        row++;
                                        Range rowMTHA_stt = ws.get_Range("A" + row);
                                        rowMTHA_stt.Value2 = "A";
                                        rowMTHA_stt.Font.Size = fontSizeNoiDung;
                                        rowMTHA_stt.Font.Name = fontName;
                                        rowMTHA_stt.Font.Bold = true;
                                        rowMTHA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowMTHA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowMTHA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowMTHA_Ten.Merge();
                                        rowMTHA_Ten.Font.Size = fontSizeNoiDung;
                                        rowMTHA_Ten.Font.Name = fontName;
                                        rowMTHA_Ten.Font.Bold = true;
                                        rowMTHA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowMTHA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowMTHA_Ten.Value2 = "Trường hợp thông thường";
                                    }
                                    else
                                    {
                                        row++;
                                        Range rowMTHA_stt = ws.get_Range("A" + row);
                                        rowMTHA_stt.Value2 = "B";
                                        rowMTHA_stt.Font.Size = fontSizeNoiDung;
                                        rowMTHA_stt.Font.Name = fontName;
                                        rowMTHA_stt.Font.Bold = true;
                                        rowMTHA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowMTHA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowMTHA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowMTHA_Ten.Merge();
                                        rowMTHA_Ten.Font.Size = fontSizeNoiDung;
                                        rowMTHA_Ten.Font.Name = fontName;
                                        rowMTHA_Ten.Font.Bold = true;
                                        rowMTHA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowMTHA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowMTHA_Ten.Value2 = "Trường hợp đứa trẻ chết";
                                    }
                                    sm = rowO8["SO_MUC"].ToString();
                                }
                                if (m != Convert.ToInt32(rowO8["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = rowO8["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(rowO8["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO8["HO_TEN"].ToString(), rowO8["MS_CN"].ToString(), rowO8["SO_BHXH"].ToString(), rowO8["TU_NGAY"].ToString(),
                                rowO8["DEN_NGAY"].ToString(), rowO8["SO_NGAY_NGHI"].ToString(), rowO8["THONG_TIN_TK"].ToString(), rowO8["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO8["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO8_TongCong = ws.get_Range("B" + row);
                        rowO8_TongCong.Font.Size = fontSizeNoiDung;
                        rowO8_TongCong.Font.Name = fontName;
                        rowO8_TongCong.Font.Bold = true;
                        rowO8_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO8_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO8_TongCong.Value2 = "Cộng";

                        Range rowO8_TongNgayNghi = ws.get_Range("G" + row);
                        rowO8_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO8_TongNgayNghi.Font.Name = fontName;
                        rowO8_TongNgayNghi.Font.Bold = true;
                        rowO8_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO8_TongNgayNghi.NumberFormat = "#,##0";
                        rowO8_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //6. lao động nữ nhờ mang thai hộ nhận con
                        row++;
                        Range rowNMTH_stt = ws.get_Range("A" + row);
                        rowNMTH_stt.Value2 = "VI";
                        rowNMTH_stt.Font.Size = fontSizeNoiDung;
                        rowNMTH_stt.Font.Name = fontName;
                        rowNMTH_stt.Font.Bold = true;
                        rowNMTH_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowNMTH_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowNMTH_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowNMTH_Ten.Merge();
                        rowNMTH_Ten.Font.Size = fontSizeNoiDung;
                        rowNMTH_Ten.Font.Name = fontName;
                        rowNMTH_Ten.Font.Bold = true;
                        rowNMTH_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowNMTH_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowNMTH_Ten.Value2 = "LAO ĐỘNG NỮ NHỜ MANG THAI HỘ NHẬN CON";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        sm = "";
                        if (dtO9.Rows.Count > 0)
                        {
                            foreach (DataRow rowO9 in dtO9.Rows)
                            {
                                if (sm != rowO9["SO_MUC"].ToString())
                                {
                                    if (rowO9["SO_MUC"].ToString() == "T5A")
                                    {
                                        row++;
                                        Range rowNMTHA_stt = ws.get_Range("A" + row);
                                        rowNMTHA_stt.Value2 = "A";
                                        rowNMTHA_stt.Font.Size = fontSizeNoiDung;
                                        rowNMTHA_stt.Font.Name = fontName;
                                        rowNMTHA_stt.Font.Bold = true;
                                        rowNMTHA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowNMTHA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowNMTHA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowNMTHA_Ten.Merge();
                                        rowNMTHA_Ten.Font.Size = fontSizeNoiDung;
                                        rowNMTHA_Ten.Font.Name = fontName;
                                        rowNMTHA_Ten.Font.Bold = true;
                                        rowNMTHA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowNMTHA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowNMTHA_Ten.Value2 = "Trường hợp thông thường";
                                    }
                                    else
                                    {
                                        row++;
                                        Range rowNMTHA_stt = ws.get_Range("A" + row);
                                        rowNMTHA_stt.Value2 = "B";
                                        rowNMTHA_stt.Font.Size = fontSizeNoiDung;
                                        rowNMTHA_stt.Font.Name = fontName;
                                        rowNMTHA_stt.Font.Bold = true;
                                        rowNMTHA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowNMTHA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowNMTHA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowNMTHA_Ten.Merge();
                                        rowNMTHA_Ten.Font.Size = fontSizeNoiDung;
                                        rowNMTHA_Ten.Font.Name = fontName;
                                        rowNMTHA_Ten.Font.Bold = true;
                                        rowNMTHA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowNMTHA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowNMTHA_Ten.Value2 = "Trường hợp con chết";
                                    }
                                    sm = rowO9["SO_MUC"].ToString();
                                }
                                if (m != Convert.ToInt32(rowO9["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = rowO9["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(rowO9["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, rowO9["HO_TEN"].ToString(), rowO9["MS_CN"].ToString(), rowO9["SO_BHXH"].ToString(), rowO9["TU_NGAY"].ToString(),
                                rowO9["DEN_NGAY"].ToString(), rowO9["SO_NGAY_NGHI"].ToString(), rowO9["THONG_TIN_TK"].ToString(), rowO9["CHI_TIEU_XAC_DINH"].ToString(),
                                rowO9["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range rowO9_TongCong = ws.get_Range("B" + row);
                        rowO9_TongCong.Font.Size = fontSizeNoiDung;
                        rowO9_TongCong.Font.Name = fontName;
                        rowO9_TongCong.Font.Bold = true;
                        rowO9_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowO9_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO9_TongCong.Value2 = "Cộng";

                        Range rowO9_TongNgayNghi = ws.get_Range("G" + row);
                        rowO9_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        rowO9_TongNgayNghi.Font.Name = fontName;
                        rowO9_TongNgayNghi.Font.Bold = true;
                        rowO9_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowO9_TongNgayNghi.NumberFormat = "#,##0";
                        rowO9_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //7. Nghi chế độ lao động nam người chồng của lao động nữ mang thai hộ nghỉ việc khi vợ sinh (dt10)
                        row++;
                        Range rowVSC_stt = ws.get_Range("A" + row);
                        rowVSC_stt.Value2 = "VII";
                        rowVSC_stt.Font.Size = fontSizeNoiDung;
                        rowVSC_stt.Font.Name = fontName;
                        rowVSC_stt.Font.Bold = true;
                        rowVSC_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowVSC_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowVSC_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowVSC_Ten.Merge();
                        rowVSC_Ten.Font.Size = fontSizeNoiDung;
                        rowVSC_Ten.Font.Name = fontName;
                        rowVSC_Ten.Font.Bold = true;
                        rowVSC_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowVSC_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowVSC_Ten.Value2 = "LAO ĐỘNG NAM, NGƯỜI CHỒNG CỦA LAO ĐỘNG NỮ MANG THAI HỘ NGHỈ VIỆC KHI VỢ SINH";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        sm = "";
                        if (dt10.Rows.Count > 0)
                        {
                            foreach (DataRow row10 in dt10.Rows)
                            {
                                if (sm != row10["SO_MUC"].ToString())
                                {
                                    if (row10["SO_MUC"].ToString() == "T6A")
                                    {
                                        row++;
                                        Range rowVSCA_stt = ws.get_Range("A" + row);
                                        rowVSCA_stt.Value2 = "A";
                                        rowVSCA_stt.Font.Size = fontSizeNoiDung;
                                        rowVSCA_stt.Font.Name = fontName;
                                        rowVSCA_stt.Font.Bold = true;
                                        rowVSCA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowVSCA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowVSCA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowVSCA_Ten.Merge();
                                        rowVSCA_Ten.Font.Size = fontSizeNoiDung;
                                        rowVSCA_Ten.Font.Name = fontName;
                                        rowVSCA_Ten.Font.Bold = true;
                                        rowVSCA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowVSCA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowVSCA_Ten.Value2 = "Trường hợp thông thường";
                                    }
                                    else
                                    {
                                        row++;
                                        Range rowVSCA_stt = ws.get_Range("A" + row);
                                        rowVSCA_stt.Value2 = "B";
                                        rowVSCA_stt.Font.Size = fontSizeNoiDung;
                                        rowVSCA_stt.Font.Name = fontName;
                                        rowVSCA_stt.Font.Bold = true;
                                        rowVSCA_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                        rowVSCA_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                        Range rowVSCA_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowVSCA_Ten.Merge();
                                        rowVSCA_Ten.Font.Size = fontSizeNoiDung;
                                        rowVSCA_Ten.Font.Name = fontName;
                                        rowVSCA_Ten.Font.Bold = true;
                                        rowVSCA_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowVSCA_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowVSCA_Ten.Value2 = "Trường hợp con chết";

                                        row++;
                                        Range rowVSCC_Ten = ws.get_Range("B" + row + ":E" + row);
                                        rowVSCC_Ten.Merge();
                                        rowVSCC_Ten.Font.Size = fontSizeNoiDung;
                                        rowVSCC_Ten.Font.Name = fontName;
                                        rowVSCC_Ten.Font.Bold = true;
                                        rowVSCC_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                                        rowVSCC_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        rowVSCC_Ten.Value2 = "Sao khi nhận con, con chết";
                                    }
                                    sm = row10["SO_MUC"].ToString();
                                }
                                if (m != Convert.ToInt32(row10["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = row10["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(row10["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row10["HO_TEN"].ToString(), row10["MS_CN"].ToString(), row10["SO_BHXH"].ToString(), row10["TU_NGAY"].ToString(),
                                row10["DEN_NGAY"].ToString(), row10["SO_NGAY_NGHI"].ToString(), row10["THONG_TIN_TK"].ToString(), row10["CHI_TIEU_XAC_DINH"].ToString(),
                                row10["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row10_TongCong = ws.get_Range("B" + row);
                        row10_TongCong.Font.Size = fontSizeNoiDung;
                        row10_TongCong.Font.Name = fontName;
                        row10_TongCong.Font.Bold = true;
                        row10_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row10_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row10_TongCong.Value2 = "Cộng";

                        Range row10_TongNgayNghi = ws.get_Range("G" + row);
                        row10_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row10_TongNgayNghi.Font.Name = fontName;
                        row10_TongNgayNghi.Font.Bold = true;
                        row10_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row10_TongNgayNghi.NumberFormat = "#,##0";
                        row10_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //8. Nghi chế độ lao động nam người chồng của người mẹ nhờ mang thai hộ hưởng trợ cấp 1 lần khi vợ sinh con, nhận con (dt11)
                        row++;
                        Range rowVMTH_stt = ws.get_Range("A" + row);
                        rowVMTH_stt.Value2 = "VIII";
                        rowVMTH_stt.Font.Size = fontSizeNoiDung;
                        rowVMTH_stt.Font.Name = fontName;
                        rowVMTH_stt.Font.Bold = true;
                        rowVMTH_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowVMTH_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowVMTH_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowVMTH_Ten.Merge();
                        rowVMTH_Ten.Font.Size = fontSizeNoiDung;
                        rowVMTH_Ten.Font.Name = fontName;
                        rowVMTH_Ten.Font.Bold = true;
                        rowVMTH_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowVMTH_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowVMTH_Ten.Value2 = "LAO ĐỘNG NAM, NGƯỜI CHỒNG CỦA NGƯỜI MẸ NHỜ MANG THAI HỘ HƯỞNG TRỢ CẤP MỘT LẦN KHI VỢ SINH CON, NHẬN CON";

                        stt = 0;
                        rowStar = row + 1;
                        if (dt11.Rows.Count > 0)
                        {
                            foreach (DataRow row11 in dt11.Rows)
                            {

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row11["HO_TEN"].ToString(), row11["MS_CN"].ToString(), row11["SO_BHXH"].ToString(), row11["TU_NGAY"].ToString(),
                                row11["DEN_NGAY"].ToString(), row11["SO_NGAY_NGHI"].ToString(), row11["THONG_TIN_TK"].ToString(), row11["CHI_TIEU_XAC_DINH"].ToString(),
                                row11["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row11_TongCong = ws.get_Range("B" + row);
                        row11_TongCong.Font.Size = fontSizeNoiDung;
                        row11_TongCong.Font.Name = fontName;
                        row11_TongCong.Font.Bold = true;
                        row11_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row11_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row11_TongCong.Value2 = "Cộng";

                        Range row11_TongNgayNghi = ws.get_Range("G" + row);
                        row11_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row11_TongNgayNghi.Font.Name = fontName;
                        row11_TongNgayNghi.Font.Bold = true;
                        row11_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row11_TongNgayNghi.NumberFormat = "#,##0";
                        row11_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //9. Thực hiện biện pháp tránh thai (dt12)
                        row++;
                        Range rowBPTT_stt = ws.get_Range("A" + row);
                        rowBPTT_stt.Value2 = "IX";
                        rowBPTT_stt.Font.Size = fontSizeNoiDung;
                        rowBPTT_stt.Font.Name = fontName;
                        rowBPTT_stt.Font.Bold = true;
                        rowBPTT_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowBPTT_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowBPTT_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowBPTT_Ten.Merge();
                        rowBPTT_Ten.Font.Size = fontSizeNoiDung;
                        rowBPTT_Ten.Font.Name = fontName;
                        rowBPTT_Ten.Font.Bold = true;
                        rowBPTT_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowBPTT_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowBPTT_Ten.Value2 = "THỰC HIỆN BIỆN PHÁP TRÁNH THAI";

                        stt = 0;
                        rowStar = row + 1;
                        m = 0;
                        if (dt12.Rows.Count > 0)
                        {
                            foreach (DataRow row12 in dt12.Rows)
                            {
                                if (m != Convert.ToInt32(row12["STT_MUC"].ToString()))
                                {
                                    row++;
                                    Range rowDataHT = ws.get_Range("B" + row + ":E" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                    rowDataHT.Merge();
                                    rowDataHT.Font.Size = fontSizeNoiDung;
                                    rowDataHT.Font.Name = fontName;
                                    rowDataHT.Font.Italic = true;
                                    rowDataHT.Value2 = row12["NOI_DUNG"].ToString();
                                    m = Convert.ToInt32(row12["STT_MUC"].ToString());
                                }

                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row12["HO_TEN"].ToString(), row12["MS_CN"].ToString(), row12["SO_BHXH"].ToString(), row12["TU_NGAY"].ToString(),
                                row12["DEN_NGAY"].ToString(), row12["SO_NGAY_NGHI"].ToString(), row12["THONG_TIN_TK"].ToString(), row12["CHI_TIEU_XAC_DINH"].ToString(),
                                row12["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row12_TongCong = ws.get_Range("B" + row);
                        row12_TongCong.Font.Size = fontSizeNoiDung;
                        row12_TongCong.Font.Name = fontName;
                        row12_TongCong.Font.Bold = true;
                        row12_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row12_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row12_TongCong.Value2 = "Cộng";

                        Range row12_TongNgayNghi = ws.get_Range("G" + row);
                        row12_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row12_TongNgayNghi.Font.Name = fontName;
                        row12_TongNgayNghi.Font.Bold = true;
                        row12_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row12_TongNgayNghi.NumberFormat = "#,##0";
                        row12_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //C. Dưởng sức phục hồi sức khỏe (dt13)
                        row++;
                        Range rowDSPHSK_stt = ws.get_Range("A" + row);
                        rowDSPHSK_stt.Value2 = "C";
                        rowDSPHSK_stt.Font.Size = fontSizeNoiDung;
                        rowDSPHSK_stt.Font.Name = fontName;
                        rowDSPHSK_stt.Font.Bold = true;
                        rowDSPHSK_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowDSPHSK_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowDSPHSK_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowDSPHSK_Ten.Merge();
                        rowDSPHSK_Ten.Font.Size = fontSizeNoiDung;
                        rowDSPHSK_Ten.Font.Name = fontName;
                        rowDSPHSK_Ten.Font.Bold = true;
                        rowDSPHSK_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowDSPHSK_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowDSPHSK_Ten.Value2 = "DƯỠNG SỨC PHỤC HỒI SỨC KHỎE";

                        //1. nghi duong suc om dau
                        row++;
                        Range rowNDSOD_stt = ws.get_Range("A" + row);
                        rowNDSOD_stt.Value2 = "I";
                        rowNDSOD_stt.Font.Size = fontSizeNoiDung;
                        rowNDSOD_stt.Font.Name = fontName;
                        rowNDSOD_stt.Font.Bold = true;
                        rowNDSOD_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowNDSOD_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowDSOD_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowDSOD_Ten.Merge();
                        rowDSOD_Ten.Font.Size = fontSizeNoiDung;
                        rowDSOD_Ten.Font.Name = fontName;
                        rowDSOD_Ten.Font.Bold = true;
                        rowDSOD_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowDSOD_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowDSOD_Ten.Value2 = "NGHỈ DƯỠNG SỨC ỐM ĐAU";

                        stt = 0;
                        rowStar = row + 1;
                        if (dt13.Rows.Count > 0)
                        {
                            foreach (DataRow row13 in dt13.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row13["HO_TEN"].ToString(), row13["MS_CN"].ToString(), row13["SO_BHXH"].ToString(), row13["TU_NGAY"].ToString(),
                                row13["DEN_NGAY"].ToString(), row13["SO_NGAY_NGHI"].ToString(), row13["THONG_TIN_TK"].ToString(), row13["CHI_TIEU_XAC_DINH"].ToString(),
                                row13["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row13_TongCong = ws.get_Range("B" + row);
                        row13_TongCong.Font.Size = fontSizeNoiDung;
                        row13_TongCong.Font.Name = fontName;
                        row13_TongCong.Font.Bold = true;
                        row13_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row13_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row13_TongCong.Value2 = "Cộng";

                        Range row13_TongNgayNghi = ws.get_Range("G" + row);
                        row13_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row13_TongNgayNghi.Font.Name = fontName;
                        row13_TongNgayNghi.Font.Bold = true;
                        row13_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row13_TongNgayNghi.NumberFormat = "#,##0";
                        row13_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //2. nghi duong suc sau thai san
                        row++;
                        Range rowNDSTS_stt = ws.get_Range("A" + row);
                        rowNDSTS_stt.Value2 = "II";
                        rowNDSTS_stt.Font.Size = fontSizeNoiDung;
                        rowNDSTS_stt.Font.Name = fontName;
                        rowNDSTS_stt.Font.Bold = true;
                        rowNDSTS_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowNDSTS_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowNDSTS_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowNDSTS_Ten.Merge();
                        rowNDSTS_Ten.Font.Size = fontSizeNoiDung;
                        rowNDSTS_Ten.Font.Name = fontName;
                        rowNDSTS_Ten.Font.Bold = true;
                        rowNDSTS_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowNDSTS_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowNDSTS_Ten.Value2 = "NGHỈ DƯỠNG SỨC SAU THAI SẢN";

                        stt = 0;
                        rowStar = row + 1;
                        if (dt14.Rows.Count > 0)
                        {
                            foreach (DataRow row14 in dt14.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row14["HO_TEN"].ToString(), row14["MS_CN"].ToString(), row14["SO_BHXH"].ToString(), row14["TU_NGAY"].ToString(),
                                row14["DEN_NGAY"].ToString(), row14["SO_NGAY_NGHI"].ToString(), row14["THONG_TIN_TK"].ToString(), row14["CHI_TIEU_XAC_DINH"].ToString(),
                                row14["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row14_TongCong = ws.get_Range("B" + row);
                        row14_TongCong.Font.Size = fontSizeNoiDung;
                        row14_TongCong.Font.Name = fontName;
                        row14_TongCong.Font.Bold = true;
                        row14_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row14_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row14_TongCong.Value2 = "Cộng";

                        Range row14_TongNgayNghi = ws.get_Range("G" + row);
                        row14_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row14_TongNgayNghi.Font.Name = fontName;
                        row14_TongNgayNghi.Font.Bold = true;
                        row14_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row14_TongNgayNghi.NumberFormat = "#,##0";
                        row14_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        //3. nghi duong suc sau TNLD-BNN
                        row++;
                        Range rowNDSTN_stt = ws.get_Range("A" + row);
                        rowNDSTN_stt.Value2 = "III";
                        rowNDSTN_stt.Font.Size = fontSizeNoiDung;
                        rowNDSTN_stt.Font.Name = fontName;
                        rowNDSTN_stt.Font.Bold = true;
                        rowNDSTN_stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        rowNDSTN_stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range rowNDSTN_Ten = ws.get_Range("B" + row + ":E" + row);
                        rowNDSTN_Ten.Merge();
                        rowNDSTN_Ten.Font.Size = fontSizeNoiDung;
                        rowNDSTN_Ten.Font.Name = fontName;
                        rowNDSTN_Ten.Font.Bold = true;
                        rowNDSTN_Ten.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        rowNDSTN_Ten.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        rowNDSTN_Ten.Value2 = "NGHỈ DƯỠNG SỨC SAU TNLĐ-BNN";

                        stt = 0;
                        rowStar = row + 1;
                        if (dt15.Rows.Count > 0)
                        {
                            foreach (DataRow row15 in dt15.Rows)
                            {
                                stt++;
                                row++;

                                Range rowDataFDate = ws.get_Range("E" + row, "F" + row);
                                rowDataFDate.NumberFormat = "dd/MM/yyyy";
                                dynamic[] arr = { stt, row15["HO_TEN"].ToString(), row15["MS_CN"].ToString(), row15["SO_BHXH"].ToString(), row15["TU_NGAY"].ToString(),
                                row15["DEN_NGAY"].ToString(), row15["SO_NGAY_NGHI"].ToString(), row15["THONG_TIN_TK"].ToString(), row15["CHI_TIEU_XAC_DINH"].ToString(),
                                row15["GHI_CHU"].ToString() };
                                Range rowData = ws.get_Range("A" + row, "J" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                                rowData.Font.Size = fontSizeNoiDung;
                                rowData.Font.Name = fontName;
                                rowData.Value2 = arr;
                            }
                        }
                        else
                        {
                            stt++;
                            row++;
                            Range rowData = ws.get_Range("A" + row);//Lấy dòng thứ row ra để đổ dữ liệu
                            rowData.Font.Size = fontSizeNoiDung;
                            rowData.Font.Name = fontName;
                            rowData.Value2 = "...";
                        }

                        row++;
                        Range row15_TongCong = ws.get_Range("B" + row);
                        row15_TongCong.Font.Size = fontSizeNoiDung;
                        row15_TongCong.Font.Name = fontName;
                        row15_TongCong.Font.Bold = true;
                        row15_TongCong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row15_TongCong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row15_TongCong.Value2 = "Cộng";

                        Range row15_TongNgayNghi = ws.get_Range("G" + row);
                        row15_TongNgayNghi.Font.Size = fontSizeNoiDung;
                        row15_TongNgayNghi.Font.Name = fontName;
                        row15_TongNgayNghi.Font.Bold = true;
                        row15_TongNgayNghi.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row15_TongNgayNghi.NumberFormat = "#,##0";
                        row15_TongNgayNghi.Value2 = "=Sum(G" + rowStar + ":G" + (row - 1) + ")";

                        row++;
                        Range row_TieuDe_DieuChinh = ws.get_Range("A" + row, "J" + row);
                        row_TieuDe_DieuChinh.Merge();
                        row_TieuDe_DieuChinh.Font.Size = fontSizeNoiDung;
                        row_TieuDe_DieuChinh.Font.Name = fontName;
                        row_TieuDe_DieuChinh.Font.Bold = true;
                        row_TieuDe_DieuChinh.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        row_TieuDe_DieuChinh.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        row_TieuDe_DieuChinh.RowHeight = 20;
                        row_TieuDe_DieuChinh.Value2 = "PHẦN 2 : DANH SÁCH ĐỀ NGHỊ ĐIỀU CHỈNH SỐ ĐÃ ĐƯỢC GIẢI QUYẾT";

                        row++;
                        Range row_DieuChinh_Format = ws.get_Range("A" + row, "J" + (row + 1));
                        row_DieuChinh_Format.Font.Size = fontSizeNoiDung;
                        row_DieuChinh_Format.Font.Name = fontName;
                        row_DieuChinh_Format.Font.Bold = true;
                        row_DieuChinh_Format.WrapText = true;
                        row_DieuChinh_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        row_DieuChinh_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        Range row_DieuChinh_Stt = ws.get_Range("A" + row, "A" + (row + 1));
                        row_DieuChinh_Stt.Merge();
                        row_DieuChinh_Stt.Value2 = "Stt";

                        Range row_DieuChinh_HoTen = ws.get_Range("B" + row, "B" + (row + 1));
                        row_DieuChinh_HoTen.Merge();
                        row_DieuChinh_HoTen.Value2 = "Họ và tên";

                        Range row_DieuChinh_Masobh = ws.get_Range("C" + row, "C" + (row + 1));
                        row_DieuChinh_Masobh.Merge();
                        row_DieuChinh_Masobh.Value2 = "Mã số BHXH";

                        Range row_DieuChinh_Dot = ws.get_Range("D" + row, "D" + (row + 1));
                        row_DieuChinh_Dot.Merge();
                        row_DieuChinh_Dot.Value2 = "Đợt đã giải quyết";

                        Range row_DieuChinh_Lydo = ws.get_Range("E" + row, "G" + (row + 1));
                        row_DieuChinh_Lydo.Merge();
                        row_DieuChinh_Lydo.Value2 = "Lý do đề nghị điều chỉnh";

                        Range row_DieuChinh_Thongtin = ws.get_Range("H" + row, "I" + (row + 1));
                        row_DieuChinh_Thongtin.Merge();
                        row_DieuChinh_Thongtin.Value2 = "Thông tin về tài khoản nhận trợ cấp";

                        Range row_DieuChinh_Ghichu = ws.get_Range("J" + row, "J" + (row + 1));
                        row_DieuChinh_Ghichu.Merge();
                        row_DieuChinh_Ghichu.Value2 = "Ghi chú";

                        row++;
                        row++;

                        Range row1_DieuChinh_Stt = ws.get_Range("A" + row);
                        row1_DieuChinh_Stt.Value2 = "A";

                        Range row1_DieuChinh_HoTen = ws.get_Range("B" + row);
                        row9_TieuDe1_HoTen.Value2 = "B";

                        Range row1_DieuChinh_Masobh = ws.get_Range("C" + row);
                        row1_DieuChinh_Masobh.Value2 = "1";

                        Range row1_DieuChinh_Dot = ws.get_Range("D" + row);
                        row1_DieuChinh_Dot.Value2 = "2";

                        Range row1_DieuChinh_Lydo = ws.get_Range("E" + row);
                        row1_DieuChinh_Lydo.Value2 = "3";

                        Range row1_DieuChinh_Thongtin = ws.get_Range("F" + row);
                        row1_DieuChinh_Thongtin.Value2 = "C";

                        Range row1_DieuChinh_Ghichu = ws.get_Range("G" + row);
                        row1_DieuChinh_Ghichu.Value2 = "D";
                        //Kẻ khung toàn bộ
                        BorderAround(ws.get_Range("A9", "J" + row));
                        //Lưu file excel xuống Ổ cứng
                        wb.SaveAs(saveExcelFile);

                        //đóng file để hoàn tất quá trình lưu trữ
                        wb.Close(true, misValue, misValue);
                        //thoát và thu hồi bộ nhớ cho COM
                        xlApp.Quit();
                        releaseObject(ws);
                        releaseObject(wb);
                        releaseObject(xlApp);

                        //Mở File excel sau khi Xuất thành công
                        System.Diagnostics.Process.Start(saveExcelFile);
                        break;
                    }

                case "luu":
                    {
                        Validate();
                        if (grvTroCapBHXH.HasColumnErrors) return;
                        if (grvDCTroCapBHXH.HasColumnErrors) return;
                        if (navigationFrame1.SelectedPage == navigationPage1)
                        {
                            LuuTroCapBaoHiem();
                            LuuDCTroCapBaoHiem();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvTroCapBHXH);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvDCTroCapBHXH);
                            //ItemForThang.Visibility = LayoutVisibility.Always;
                            //ItemForDateThang.Visibility = LayoutVisibility.Never;
                            enableButon(true);
                        }
                        else
                        {
                            //lấy dữ liệu được chọn insert vào bảng tạm điều chỉnh điều chỉnh(tabDCTroCapBHXH)
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtChonDCTroCapBHXH, Commons.Modules.ObjSystems.ConvertDatatable(grvChonTroCapBHXH), "");

                            string sSql = "INSERT INTO dbo." + sbtDCTroCapBHXH + " (ID_HTC, MS_CN, ID_CN, ID_LDV,PHAN_TRAM_TRO_CAP, THANG, THANG_CHUYEN, DOT, DOT_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, SO_NGAY_NGHI, MUC_HUONG_CU) " +
                                "SELECT -1,MS_CN,ID_CN,ID_LDV,PHAN_TRAM_TRO_CAP,'" + cboThang.EditValue + "','" + cboThang.EditValue + "'," + cboDot.EditValue + "," + cboDot.EditValue + ",NGHI_TU_NGAY,NGHI_DEN_NGAY,SO_NGAY_NGHI,SO_TIEN_TC FROM dbo." + sbtChonDCTroCapBHXH + " A WHERE NOT EXISTS(SELECT * FROM dbo." + sbtDCTroCapBHXH + " B WHERE A.ID_CN = B.ID_CN AND A.NGHI_TU_NGAY = B.NGHI_TU_NGAY) AND A.CHON = 1 DELETE dbo." + sbtDCTroCapBHXH + "  WHERE  EXISTS (SELECT * FROM dbo." + sbtChonDCTroCapBHXH + " B WHERE dbo." + sbtDCTroCapBHXH + ".ID_CN = B.ID_CN AND dbo." + sbtDCTroCapBHXH + ".NGHI_TU_NGAY =B.NGHI_TU_NGAY AND B.CHON = 0)";
                            //xong rồi load lại lưới điều chỉnh theo bảng tạm vào xóa table chọn
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            Commons.Modules.ObjSystems.XoaTable(sbtChonDCTroCapBHXH);
                            LoadGrdDCTroCapBHXH(true);
                            Commons.Modules.ObjSystems.AddnewRow(grvDCTroCapBHXH, true);
                            navigationFrame1.SelectedPage = navigationPage1;
                            enableButon(false);
                        }
                        break;
                    }

                case "khongluu":
                    {
                        LoadGrdTroCapBHXH();
                        LoadGrdDCTroCapBHXH(false);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTroCapBHXH);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDCTroCapBHXH);
                        //ItemForThang.Visibility = LayoutVisibility.Always;
                        //ItemForDateThang.Visibility = LayoutVisibility.Never;
                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
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
        #endregion

        private void LoadDuLieuTroCap(GridView view, int iRow, int iCN, string sLDV, double dPT, string dNgay, int iSN, int iSC)
        {
            if (sLDV == "")
            {
                return;
            }
            if (dPT > 0 && iSoNgay > 0)
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetLuongTinhTroCapBHXH", iCN, Convert.ToDateTime(dNgay), sLDV, dPT, iSN, iSC));
                view.SetRowCellValue(iRow, view.Columns["HS_LUONG"], dt.Rows[0]["LUONG_BQ"]);
                view.SetRowCellValue(iRow, view.Columns["LUONG_CB"], dt.Rows[0]["LUONG_CB"]);
                view.SetRowCellValue(iRow, view.Columns["SO_TIEN_TC"], dt.Rows[0]["TIEN_TC"]);
            }
        }

        private void LuuTroCapBaoHiem()
        {
            DataTable tb = Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH);
            string stb = "tabTroCapBHXH" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stb, tb, "");
                string sSql = "UPDATE A SET A.ID_CN = B.ID_CN,A.ID_LDV = B.ID_LDV,A.ID_HTNTC = B.ID_HTNTC,A.NGHI_TU_NGAY = B.NGHI_TU_NGAY," +
                        "A.NGHI_DEN_NGAY = B.NGHI_DEN_NGAY,A.THONG_TIN_TK = B.THONG_TIN_TK,A.CHI_TIEU_XAC_DINH = B.CHI_TIEU_XAC_DINH," +
                        "A.SO_NGAY_NGHI = B.SO_NGAY_NGHI,A.SO_TIEN_TC = B.SO_TIEN_TC,A.SO_CON_SINH = B.SO_CON_SINH " +
                        "FROM dbo.TRO_CAP_BHXH A INNER JOIN tabTroCapBHXH" + Commons.Modules.UserName + " B ON B.ID_TC_BHXH = A.ID_TC_BHXH " +
                        "INSERT INTO dbo.TRO_CAP_BHXH(ID_CN, ID_LDV, ID_HTNTC, DOT, THANG, NGHI_TU_NGAY, NGHI_DEN_NGAY, THONG_TIN_TK, CHI_TIEU_XAC_DINH, " +
                        "SO_NGAY_NGHI, HS_LUONG, LUONG_CB, SO_TIEN_TC, SO_CON_SINH, LAN_TS) " +
                        "SELECT ID_CN, ID_LDV, ID_HTNTC," + cboDot.EditValue + ",'" + Convert.ToDateTime("01/" + cboThang.Text).ToString("MM/dd/yyyy") + "',NGHI_TU_NGAY,NGHI_DEN_NGAY,THONG_TIN_TK," +
                        "CHI_TIEU_XAC_DINH,SO_NGAY_NGHI,HS_LUONG,LUONG_CB,SO_TIEN_TC,SO_CON_SINH,LAN_TS FROM tabTroCapBHXH" + Commons.Modules.UserName +
                        " WHERE ID_TC_BHXH NOT IN(SELECT ID_TC_BHXH FROM dbo.TRO_CAP_BHXH)";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable("tabTroCapBHXH" + Commons.Modules.UserName);
            }
            catch (Exception ex)
            {

            }
        }
        private void grvTroCapBHXH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                grvTroCapBHXH.ClearColumnErrors();
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn ID_CN = View.Columns["ID_CN"];
                DevExpress.XtraGrid.Columns.GridColumn NGHI_TU_NGAY = View.Columns["NGHI_TU_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn NGHI_DEN_NGAY = View.Columns["NGHI_DEN_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn ID_LDV = View.Columns["ID_LDV"];
                DevExpress.XtraGrid.Columns.GridColumn ID_HTNTC = View.Columns["ID_HTNTC"];
                if (View.GetRowCellValue(e.RowHandle, ID_LDV).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(ID_LDV, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraLDVNULL", Commons.Modules.TypeLanguage)); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ID_HTNTC).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(ID_HTNTC, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraHTNTCNULL", Commons.Modules.TypeLanguage)); return;
                }

                if (View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTuNgayCNULL", Commons.Modules.TypeLanguage)); return;
                }
                if (View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(NGHI_DEN_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraDenNgayNULL", Commons.Modules.TypeLanguage)); return;
                }

                //kiểm tra dữ liệu trùng
                DataTable tempt = Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH);
                int n = 0;
                try
                {
                    string sSql = "SELECT dbo.fuKiemTraTroCapBHXH(" + View.GetRowCellValue(e.RowHandle, ID_CN) + ",'" + Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY)).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY)).ToString("MM/dd/yyyy") + "',LEFT('" + cboThang.Text + "',2),RIGHT('" + cboThang.Text + "',4))";
                    n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                }
                catch
                { }
                if (n == 0)
                {
                    //kiểm tra từ ngày dến ngày trên lưới
                    DateTime tn = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY));
                    DateTime dn = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY));
                    n = tempt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN") == Convert.ToInt64(View.GetRowCellValue(e.RowHandle, ID_CN)) && x.Field<DateTime>("NGHI_TU_NGAY") > tn && x.Field<DateTime>("NGHI_DEN_NGAY") < dn);
                    if (n == 1)
                    {
                        e.Valid = false;
                        View.SetColumnError(ID_CN, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                        View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                        val = false;
                        return;
                    }
                }
            }
            catch { }
        }

        private bool kiemtraNgayHopLe(DateTime ngay, DataTable dt, int idcn)
        {
            bool resulst = false;
            string sSql = "SELECT count(*) FROM dbo.TRO_CAP_BHXH WHERE  NGHI_TU_NGAY <= '" + ngay.ToString("MM/dd/yyyy") + "' AND  NGHI_DEN_NGAY > '" + ngay.ToString("MM/dd/yyyy") + "' AND ID_CN = " + idcn + " AND LEFT(THANG, 2) < LEFT('" + cboThang.Text + "',2) AND RIGHT(THANG,4) = RIGHT('" + cboThang.Text + "',4)";
            int n = 0;
            n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (n == 0)
            {
                n = dt.AsEnumerable().Count(x => x.Field<DateTime>("NGHI_TU_NGAY") <= ngay && x.Field<DateTime>("NGHI_DEN_NGAY") > ngay);
                if (n > 1)
                {
                    resulst = false;
                }
                else
                    resulst = true;
            }
            else
            {
                resulst = false;
            }
            return resulst;
        }

        private void grvTroCapBHXH_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                string sSql = "";
                GridView view = sender as GridView;
                if (view == null) return;
                view.ClearColumnErrors();

                if (e.Column.Name == "colID_CN")
                {
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]).ToString() == "")
                    {
                        return;
                    }
                    iIDCN = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]));
                    sSql = "SELECT MS_CN FROM dbo.CONG_NHAN WHERE ID_CN = " + iIDCN;
                    string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                    view.SetRowCellValue(e.RowHandle, view.Columns["MS_CN"], s);
                }
                if (e.Column.Name == "colID_LDV")
                {
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]).ToString() == "")
                    {
                        return;
                    }

                    DataTable dt = new DataTable();
                    sSql = "SELECT MS_LDV, IsNull(PHAN_TRAM_TRO_CAP,0) PTTC FROM dbo.LY_DO_VANG WHERE ID_LDV = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]);
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    dPT = Convert.ToInt32(dt.Rows[0]["PTTC"]);
                    sLDV = Convert.ToString(dt.Rows[0]["MS_LDV"]);
                    view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRO_CAP"], dPT);
                    if (sLDV == "T3")
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_CON_SINH"], 1);
                    }
                    LoadDuLieuTroCap(view, e.RowHandle, iIDCN, sLDV, dPT, Convert.ToString(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"])), iSoNgay, iSoCon);
                }
                if (e.Column.Name == "colNGHI_TU_NGAY" || e.Column.Name == "colNGHI_DEN_NGAY" || e.Column.Name == "colSO_CON_SINH")
                {
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]).ToString() == "")
                    {
                        return;
                    }
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]).ToString() == "")
                    {
                        return;
                    }
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"]) as DateTime?;
                    if (fromDate == null)
                    {
                        return;
                    }
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_DEN_NGAY"]) as DateTime?;
                    if (toDate == null)
                    {
                        return;
                    }

                    iIDCN = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]));
                    DataTable dt = new DataTable();
                    sSql = "SELECT MS_LDV, IsNull(PHAN_TRAM_TRO_CAP,0) PTTC FROM dbo.LY_DO_VANG WHERE ID_LDV = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]);
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    dPT = Convert.ToInt32(dt.Rows[0]["PTTC"]);
                    sLDV = Convert.ToString(dt.Rows[0]["MS_LDV"]);
                    dPT = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRO_CAP"]));
                    iSoNgay = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                    if (view.GetRowCellValue(e.RowHandle, view.Columns["SO_CON_SINH"]).ToString() == "")
                    {
                        iSoCon = 1;
                    }
                    else
                    {
                        iSoCon = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["SO_CON_SINH"]));
                    }
                    view.SetRowCellValue(e.RowHandle, view.Columns["SO_NGAY_NGHI"], iSoNgay);

                    LoadDuLieuTroCap(view, e.RowHandle, iIDCN, sLDV, dPT, Convert.ToString(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"])), iSoNgay, iSoCon);

                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region Điều chỉnh trợ cấp bảo hiểm y tế
        private void LoadGrdDCTroCapBHXH(bool loadBT)
        {
            try
            {
                DataTable dt = new DataTable();
                if (loadBT == true)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo." + sbtDCTroCapBHXH + ""));
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDCTroCapBHXH", "01/" + cboThang.Text, cboDot.Text, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                }
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDCTroCapBHXH, grvDCTroCapBHXH, dt, false, false, false, true, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvDCTroCapBHXH, "spGetCongNhan", "ID_CN", "CONG_NHAN");
                Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvDCTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false), "ID_LDV", "LY_DO_VANG");

                grvDCTroCapBHXH.Columns["ID_HTC"].Visible = false;

                grvDCTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["SO_NGAY_NGHI"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["THANG"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["THANG_CHUYEN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["DOT"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["DOT_CHUYEN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].OptionsColumn.ReadOnly = true;

                grvDCTroCapBHXH.Columns["ID_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDCTroCapBHXH.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDCTroCapBHXH.Columns["ID_LDV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                grvDCTroCapBHXH.Columns["MS_CN"].Width = 100;
                grvDCTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvDCTroCapBHXH.Columns["ID_LDV"].Width = 200;

                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDCTroCapBHXH.Columns["MUC_HUONG_MOI"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["MUC_HUONG_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDCTroCapBHXH.Columns["SO_TIEN_LECH"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["SO_TIEN_LECH"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);
                grvDCTroCapBHXH.Columns["NGHI_TU_NGAY"].ColumnEdit = dEditN;
                grvDCTroCapBHXH.Columns["NGHI_DEN_NGAY"].ColumnEdit = dEditN;

            }
            catch { }
        }

        private void grvDCTroCapBHXH_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn colTuNgay = view.Columns["NGHI_TU_NGAY"];
            GridColumn colDenNgay = view.Columns["NGHI_DEN_NGAY"];

            GridColumn colThang = view.Columns["THANG"];
            GridColumn colThangChuyen = view.Columns["THANG_CHUYEN"];

            GridColumn colDot = view.Columns["DOT"];
            GridColumn colDotChuyen = view.Columns["DOT_CHUYEN"];
            if (e.Column.Name == "colID_CN")
            {

                string sSql = "SELECT MS_CN FROM dbo.CONG_NHAN WHERE ID_CN = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]) + "";
                string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                view.SetRowCellValue(e.RowHandle, view.Columns["MS_CN"], s);

                //kiểm tra dòng này là mới hay là cũ nếu mới thì cho gán các giá trị mặc định vào
                int ktdong = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DIEU_CHINH_TRO_CAP_BHXH WHERE ID_HTC = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_HTC"]) + " "));
                if (ktdong == 0)
                {
                    view.SetRowCellValue(e.RowHandle, colThang, cboThang.EditValue);
                    view.SetRowCellValue(e.RowHandle, colThangChuyen, cboThang.EditValue);
                    view.SetRowCellValue(e.RowHandle, colDot, cboDot.EditValue);
                    view.SetRowCellValue(e.RowHandle, colDotChuyen, cboDot.EditValue);
                }
            }
            if (e.Column.Name == "colID_LDV")
            {
                string sSql = "SELECT PHAN_TRAM_TRO_CAP FROM dbo.LY_DO_VANG WHERE ID_LDV = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]) + "";
                double d = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRO_CAP"], d);
            }
            if (e.Column.Name == "colNGHI_TU_NGAY" || e.Column.Name == "colNGHI_DEN_NGAY")
            {
                try
                {
                    DateTime tn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"]));
                    DateTime dn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_DEN_NGAY"]));
                    TimeSpan time = dn - tn;
                    if (time.Days >= 0)
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_NGAY_NGHI"], time.Days + 1);
                    }
                    else
                    {
                        view.SetColumnError(colTuNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                        view.SetColumnError(colDenNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                        val = false;
                    }
                }
                catch
                {

                }
            }
        }

        private void LuuDCTroCapBaoHiem()
        {
            DataTable tb = Commons.Modules.ObjSystems.ConvertDatatable(grvDCTroCapBHXH);
            if (tb != null && tb.Rows.Count > 0)
            {
                try
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtDCTroCapBHXH, tb, "");
                    string sSql = "UPDATE  A SET A.ID_CN = B.ID_CN ,A.ID_LDV = B.ID_LDV ,A.NGHI_TU_NGAY = B.NGHI_TU_NGAY,A.NGHI_DEN_NGAY = B.NGHI_DEN_NGAY,A.MUC_HUONG_MOI = B.MUC_HUONG_MOI,A.SO_NGAY_LECH = B.SO_NGAY_LECH,A.SO_TIEN_LECH = B.SO_TIEN_LECH,A.GHI_CHU = B.NOI_DUNG_CHINH FROM dbo.DIEU_CHINH_TRO_CAP_BHXH A INNER JOIN dbo." + sbtDCTroCapBHXH + " B ON B.ID_HTC = A.ID_HTC INSERT INTO dbo.DIEU_CHINH_TRO_CAP_BHXH (ID_CN, ID_LDV, DOT, THANG, DOT_CHUYEN, THANG_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, MUC_HUONG_MOI, SO_NGAY_LECH, SO_TIEN_LECH, GHI_CHU ) SELECT ID_CN, ID_LDV, DOT, THANG, DOT_CHUYEN, THANG_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, MUC_HUONG_MOI, SO_NGAY_LECH, SO_TIEN_LECH, NOI_DUNG_CHINH FROM " + sbtDCTroCapBHXH + " A WHERE   NOT EXISTS(SELECT * FROM    dbo.DIEU_CHINH_TRO_CAP_BHXH B WHERE A.ID_CN = B.ID_CN AND A.NGHI_TU_NGAY = B.NGHI_TU_NGAY)  DELETE dbo.DIEU_CHINH_TRO_CAP_BHXH WHERE NOT EXISTS (SELECT * FROM " + sbtDCTroCapBHXH + " A WHERE DIEU_CHINH_TRO_CAP_BHXH.THANG = a.THANG AND DIEU_CHINH_TRO_CAP_BHXH.ID_CN = A.ID_CN AND dbo.DIEU_CHINH_TRO_CAP_BHXH.NGHI_TU_NGAY = CONVERT(DATE,A.NGHI_TU_NGAY))";
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    Commons.Modules.ObjSystems.XoaTable(sbtDCTroCapBHXH);

                }
                catch
                {
                }
            }
        }
        #endregion
        #region chọn điều chỉnh bảo hiểm y tế
        private void LoadGrdChonTroCapBHXH()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListChonTroCapBHXH", cboThang.EditValue, cboDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sbtDCTroCapBHXH));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonTroCapBHXH, grvChonTroCapBHXH, dt, false, false, true, true, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvChonTroCapBHXH, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombo("ID_LDV", "TEN_LDV", grvChonTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false));
                grvChonTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].Visible = false;
                grvChonTroCapBHXH.Columns["CHON"].Visible = false;
                grvChonTroCapBHXH.Columns["CHON"].Width = 100;
                grvChonTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvChonTroCapBHXH.Columns["ID_LDV"].Width = 200;
                grvChonTroCapBHXH.Columns["ID_TC_BHXH"].Visible = false;
                grvChonTroCapBHXH.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvChonTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvChonTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
            }
            catch
            {
            }
        }
        #endregion
        private void grvDCTroCapBHXH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn ID_CN = View.Columns["ID_CN"];
                DevExpress.XtraGrid.Columns.GridColumn NGHI_TU_NGAY = View.Columns["NGHI_TU_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn NGHI_DEN_NGAY = View.Columns["NGHI_DEN_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn ID_LDV = View.Columns["ID_LDV"];
                DevExpress.XtraGrid.Columns.GridColumn ID_HTNTC = View.Columns["ID_LDV"];
                if (View.GetRowCellValue(e.RowHandle, ID_LDV).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(ID_LDV, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraLDVNULL", Commons.Modules.TypeLanguage)); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ID_HTNTC).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(ID_HTNTC, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraHTNTCNULL", Commons.Modules.TypeLanguage)); return;
                }

                if (View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTuNgayCNULL", Commons.Modules.TypeLanguage)); return;
                }
                if (View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(NGHI_DEN_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraDenNgayNULL", Commons.Modules.TypeLanguage)); return;
                }
                //kiểm tra dữ liệu trùng
                DataTable tempt = Commons.Modules.ObjSystems.ConvertDatatable(grvDCTroCapBHXH);
                int n = 0;
                //kiểm tra trùng trên view
                try
                {
                    n = tempt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN") == Convert.ToInt64(View.GetRowCellValue(e.RowHandle, ID_CN)) && x.Field<DateTime>("NGHI_TU_NGAY") == Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY)));
                }
                catch (Exception ex)
                {
                }
                if (n > 1)
                {
                    e.Valid = false;
                    View.SetColumnError(ID_CN, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                    View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                    val = false;
                    return;
                }
            }
            catch { }
        }

        private void XoaTroCapBaoHiemXH()
        {
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.TRO_CAP_BHXH WHERE ID_TC_BHXH = " + grvTroCapBHXH.GetFocusedRowCellValue("ID_TC_BHXH") + "");
                grvTroCapBHXH.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung, ex.ToString());
            }
        }
        private void grdTroCapBHXH_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaTroCapBaoHiemXH();
            }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadDot();
            LoadGrdTroCapBHXH();
            LoadGrdDCTroCapBHXH(false);
            Commons.Modules.sPS = "";
        }

        private void grvTroCapBHXH_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn == view.Columns["NGHI_TU_NGAY"])
            {
                DateTime? fromDate = e.Value as DateTime?;
                DateTime? toDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGHI_DEN_NGAY"]) as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    e.ErrorText = "Từ ngày phải nhỏ hơn đến ngày";
                }
            }
            if (view.FocusedColumn == view.Columns["NGHI_DEN_NGAY"])
            {
                DateTime? fromDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGHI_TU_NGAY"]) as DateTime?;
                DateTime? toDate = e.Value as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    e.ErrorText = "Đến ngày phải lớn hơn từ ngày";
                }
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

        private void grvDCTroCapBHXH_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTroCapBHXH_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTroCapBHXH_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }
    }
}


