using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using DataTable = System.Data.DataTable;
using System.Globalization;
using Excel;

namespace Vs.Recruit
{
    public partial class ucDaoTaoDinhHuong : DevExpress.XtraEditors.XtraUserControl
    {
        CultureInfo cultures = new CultureInfo("en-US");

        private bool bThem = false;
        private string SaveExcelFile;
        public static ucDaoTaoDinhHuong _instance;
        public static ucDaoTaoDinhHuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDaoTaoDinhHuong();
                return _instance;
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

        // private SqlConnection conn;

        public ucDaoTaoDinhHuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvDTDinhHuong, "Diem_thang");
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvThang, "Diem_thang");
        }
        private void ucDaoTaoDinhHuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiDT, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
            LoadThang();
            LoadGrdDTDinhHuong();
            Commons.Modules.sLoad = "";
            enableButon(true);
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "sua":
                    {
                        bThem = true;
                        LoadGrdDTDinhHuong();
                        enableButon(false);
                        break;
                    }
                case "ghi":
                    {
                        if (grvDTDinhHuong.RowCount == 0)
                            return;

                        DataTable dt_CHON = new DataTable();
                        dt_CHON = ((DataTable)grdDTDinhHuong.DataSource);
                        if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUngVien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!Savedata()) return;
                        bThem = false;
                        //LoadThang();
                        LoadGrdDTDinhHuong();
                        enableButon(true);
                        break;
                    }

                case "khongghi":
                    {
                        bThem = false;
                        LoadGrdDTDinhHuong();
                        //Commons.Modules.ObjSystems.DeleteAddRow(grvPhepThang);
                        enableButon(true);
                        break;
                    }
                case "In":
                    {
                        if (grvDTDinhHuong.RowCount == 0)
                            return;
                        InDTDinhHuong();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        private void GrvPhepThang_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #region hàm xử lý dữ liệu
        private void LoadGrdDTDinhHuong()
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                //DateTime ngay = DateTime.ParseExact(cboThang.Text, "dd/MM/yyyy", cultures);

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetDTDinhHuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = bThem;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                if (grdDTDinhHuong.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDTDinhHuong, grvDTDinhHuong, dt, true, true, false, true, true, this.Name);
                    grvDTDinhHuong.Columns["ID_UV"].Visible = false;
                    grvDTDinhHuong.Columns["ID_NGUOI_DT"].Visible = false;
                }
                else
                {
                    grdDTDinhHuong.DataSource = dt;
                }
                if (bThem == false)
                {
                    grvDTDinhHuong.Columns["CHON"].Visible = false;
                    grvDTDinhHuong.Columns["STT"].Visible = false;
                    grvDTDinhHuong.Columns["KY_TEN"].Visible = false;

                }
                else
                {
                    //grvDTDinhHuong.OptionsSelection.MultiSelect = true;
                    //grvDTDinhHuong.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    grvDTDinhHuong.Columns["CHON"].Visible = true;
                    grvDTDinhHuong.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvDTDinhHuong.Columns["NGAY_SINH"].OptionsColumn.AllowEdit = false;
                    grvDTDinhHuong.Columns["NGAY_NHAN_VIEC"].OptionsColumn.AllowEdit = false;
                }

                try
                {
                    grvDTDinhHuong.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvDTDinhHuong.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }

                cboNguoiDT.EditValue = dt.Rows.Count == 0 || dt.Rows[0]["ID_NGUOI_DT"].ToString() == "" ? -1 : Convert.ToInt64(dt.Rows[0]["ID_NGUOI_DT"]);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }

        private bool Savedata()
        {
            string sBT = "sBTDTDinhHuong" + Commons.Modules.iIDUser;
            try
            {
                //DateTime ngay = DateTime.ParseExact(cboThang.Text, "dd/MM/yyyy", cultures);

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDTDinhHuong), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDTDinhHuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), cboNguoiDT.Text == "" ? cboNguoiDT.EditValue = DBNull.Value : Convert.ToInt64(cboNguoiDT.EditValue), sBT);
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;

            cboNguoiDT.Properties.ReadOnly = visible;
            grvDTDinhHuong.OptionsBehavior.Editable = !visible;
            //searchControl.Visible = visible;
            cboThang.Properties.ReadOnly = !visible;
        }
        #endregion
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdDTDinhHuong();
            Commons.Modules.sLoad = "";
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY_DT,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY_DT,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY_DT,103),10) AS NGAY ,RIGHT(CONVERT(VARCHAR(10),NGAY_DT,103),7) AS THANG  FROM dbo.DAO_TAO_NQ_DH ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                    grvThang.Columns["THANG"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void grvDTDinhHuong_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void InDTDinhHuong()
        {
            try
            {
                //DateTime dtNgay = DateTime.ParseExact(cboThang.EditValue.ToString(), "MM/dd/YYYY", System.Globalization.CultureInfo.InvariantCulture);

                //DateTime ngay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetDTDinhHuong", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = 3;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                //dt.DefaultView.RowFilter = "";
                //DataView dv = dt.DefaultView;

                //DataTable dt1 = new DataTable();
                //dt1 = dv.ToTable(false, "STT", "HO_TEN", "NGAY_SINH", "NGAY_NHAN_VIEC", "NQ_LD", "TL_THUONG", "TU_LD", "CS_TC", "GQ_KN", "AT_HC", "SO_CC", "PL_RT", "NQ_PCCC", "NQ_VSATLD", "TN_HL", "KY_TEN");
                //dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                //dt1.Columns["NGAY_SINH"].ColumnName = "Năm sinh";
                //dt1.Columns["NGAY_NHAN_VIEC"].ColumnName = "Ngày vào";
                //dt1.Columns["NQ_LD"].ColumnName = "Nội quy lao động";
                //dt1.Columns["TL_THUONG"].ColumnName = "Tiền lương thưởng";
                //dt1.Columns["TU_LD"].ColumnName = "Thỏa ước lao động";
                //dt1.Columns["CS_TC"].ColumnName = "Các chính sách TNXH và tiêu chuẩn TNXH";
                //dt1.Columns["GQ_KN"].ColumnName = "Giải quyết khiếu nại";
                //dt1.Columns["AT_HC"].ColumnName = "An toàn hóa chất";
                //dt1.Columns["SO_CC"].ColumnName = "Sơ cấp cứu ban đầu";
                //dt1.Columns["PL_RT"].ColumnName = "Phân loại rác thải";
                //dt1.Columns["NQ_PCCC"].ColumnName = "Nội Quy PCCC";
                //dt1.Columns["NQ_VSATLD"].ColumnName = "Nội Quy VSATLĐ";
                //dt1.Columns["TN_HL"].ColumnName = "Tham nhũng, hối lộ";
                //dt1.Columns["KY_TEN"].ColumnName = "Học Viên (Ký Nhận)";

                try
                {
                    SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                    if (SaveExcelFile == "")
                    {
                        return;
                    }
                    Excel.Application oXL;
                    Workbook oWB;
                    Worksheet oSheet;
                    oXL = new Excel.Application();
                    oXL.Visible = false;

                    oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                    oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                    string fontName = "Times New Roman";
                    int fontSizeTieuDe = 18;
                    int fontSizeNoiDung = 9;

                    string lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dt.Columns.Count - 1);

                    Range row1_TieuDe = oSheet.get_Range("A1", "P1");
                    row1_TieuDe.Merge();
                    row1_TieuDe.Font.Size = fontSizeTieuDe;
                    row1_TieuDe.Font.Name = fontName;
                    row1_TieuDe.Font.Bold = true;
                    row1_TieuDe.Value2 = "BÁO CÁO ĐÀO TẠO ĐỊNH HƯỚNG THÁNG " + cboThang.Text;
                    row1_TieuDe.WrapText = false;
                    row1_TieuDe.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Range row2_TenCTY = oSheet.get_Range("A2","E2");
                    row2_TenCTY.Merge();
                    row2_TenCTY.Font.Size = fontSizeNoiDung;
                    row2_TenCTY.Font.Name = fontName;
                    row2_TenCTY.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";

                    Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "4"); //27 + 31
                    row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                    row5_TieuDe_Format.Font.Name = fontName;
                    row5_TieuDe_Format.Font.Bold = true;
                    row5_TieuDe_Format.WrapText = true;
                    row5_TieuDe_Format.NumberFormat = "@";
                    row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    //row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                    //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                    //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                    //oSheet.Cells[7, 1] = "BỘ PHẬN";
                    //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                    //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                    //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                    Range row5_TieuDe_Stt = oSheet.get_Range("A3", "A4");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "STT";
                    row5_TieuDe_Stt.ColumnWidth = 5;

                    Range row6_TieuDe_Stt = oSheet.get_Range("B3", "B4");
                    row6_TieuDe_Stt.Merge();
                    row6_TieuDe_Stt.Value2 = "Họ và tên";
                    row6_TieuDe_Stt.ColumnWidth = 21;

                    Range row5_TieuDe_MaSo = oSheet.get_Range("C3", "C4");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "Năm sinh";
                    row5_TieuDe_MaSo.ColumnWidth = 12;

                    Range row6_TieuDe_MaSo = oSheet.get_Range("D3", "D4");
                    row6_TieuDe_MaSo.Merge();
                    row6_TieuDe_MaSo.Value2 = "Ngày vào";
                    row6_TieuDe_MaSo.ColumnWidth = 12;

                    Range row5_TieuDe_HoTen = oSheet.get_Range("E3", "O3");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Font.Bold = true;
                    row5_TieuDe_HoTen.Value2 = "Nội Dung Phổ Biến";
                    row5_TieuDe_HoTen.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Range row6_TieuDe_HoTen = oSheet.get_Range("E4");
                    row6_TieuDe_HoTen.Value2 = "Nội quy lao động";
                    row6_TieuDe_HoTen.ColumnWidth = 10;

                    Range row5_TieuDe_XiNgiep = oSheet.get_Range("F4");
                    row5_TieuDe_XiNgiep.Value2 = "Tiền Lương Thưởng";
                    row5_TieuDe_XiNgiep.ColumnWidth = 10;

                    Range row6_TieuDe_XiNgiep = oSheet.get_Range("G4");
                    row6_TieuDe_XiNgiep.Value2 = "Thỏa Ước Lao Động";
                    row6_TieuDe_XiNgiep.ColumnWidth = 10;

                    Range row5_TieuDe_To = oSheet.get_Range("H4");
                    row5_TieuDe_To.Value2 = "Các chính sách TNXH và tiêu chuẩn TNXH";
                    row5_TieuDe_To.ColumnWidth = 10;

                    Range row6_TieuDe_To = oSheet.get_Range("I4");
                    row6_TieuDe_To.Value2 = "Giải quyết khiếu nại";
                    row6_TieuDe_To.ColumnWidth = 10;

                    Range row6_TieuDe_AT = oSheet.get_Range("J4");
                    row6_TieuDe_AT.Value2 = "An toàn hóa chất";
                    row6_TieuDe_AT.ColumnWidth = 10;

                    Range row6_TieuDe_K = oSheet.get_Range("K4");
                    row6_TieuDe_K.Value2 = "Sơ cấp cứu ban đầu";
                    row6_TieuDe_K.ColumnWidth = 10;

                    Range row6_TieuDe_L = oSheet.get_Range("L4");
                    row6_TieuDe_L.Value2 = "Phân loại rác thải";
                    row6_TieuDe_L.ColumnWidth = 10;

                    Range row6_TieuDe_M = oSheet.get_Range("M4");
                    row6_TieuDe_M.Value2 = "Nội Quy PCCC";
                    row6_TieuDe_M.ColumnWidth = 10;

                    Range row6_TieuDe_N = oSheet.get_Range("N4");
                    row6_TieuDe_N.Value2 = "Nội Quy VSATLĐ";
                    row6_TieuDe_N.ColumnWidth = 10;

                    Range row6_TieuDe_O = oSheet.get_Range("O4");
                    row6_TieuDe_O.Value2 = "Tham nhũng, hối lộ";
                    row6_TieuDe_O.ColumnWidth = 10;

                    Range row6_TieuDe_P = oSheet.get_Range("P3", "P4");
                    row6_TieuDe_P.Merge();
                    row6_TieuDe_P.Value2 = "Học Viên (Ký Nhận)";
                    row6_TieuDe_P.ColumnWidth = 10;

                    DataRow[] dr = dt.Select();
                    string[,] rowData = new string[dr.Count(), dt.Columns.Count];
                    int col_bd = 0;
                    int rowCnt = 0;
                    foreach (DataRow row in dr)
                    {
                        for (col_bd = 0; col_bd < dt.Columns.Count; col_bd++)
                        {
                            rowData[rowCnt, col_bd] = row[col_bd].ToString();
                        }
                        rowCnt++;
                    }
                    rowCnt = rowCnt + 4;
                    oSheet.get_Range("A5", lastColumn + rowCnt.ToString()).Value2 = rowData;

                    Excel.Range formatRange;
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
                    //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    //}

                    //colKT++;
                    //CurentColumn = CharacterIncrement(colKT);
                    formatRange = oSheet.get_Range("C5", lastColumn + rowCnt.ToString());
                    formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    ////Kẻ khung toàn bộ
                    formatRange = oSheet.get_Range("A1", "A" + rowCnt.ToString());
                    formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;
                    BorderAround(oSheet.get_Range("A3", lastColumn + rowCnt.ToString()));

                    formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;

                    rowCnt++;
                    formatRange = oSheet.get_Range("L" + rowCnt + "", "N" + rowCnt.ToString());
                    formatRange.Merge();
                    formatRange.Value = "Người đào tạo";
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 12;
                    formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    formatRange.Font.Bold = true;
                    rowCnt++;
                    formatRange = oSheet.get_Range("L" + rowCnt + "", "N" + rowCnt.ToString());
                    formatRange.Merge();
                    formatRange.Value = cboNguoiDT.Text;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = 12;
                    formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;



                    // filter
                    oSheet.Application.ActiveWindow.SplitRow = 4;
                    oSheet.Application.ActiveWindow.FreezePanes = true;
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
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
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
        private void MExportExcel(DataTable dtTmp, Excel.Worksheet ExcelSheets, Excel.Range sRange)
        {
            object[,] rawData = new object[dtTmp.Rows.Count + 1, dtTmp.Columns.Count - 1 + 1];
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
                rawData[0, col] = dtTmp.Columns[col].Caption;
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            {
                for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                    rawData[row + 1, col] = dtTmp.Rows[row][col].ToString();
            }
            sRange.Value = rawData;
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
