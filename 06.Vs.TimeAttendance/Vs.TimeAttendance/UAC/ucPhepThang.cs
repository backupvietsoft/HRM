using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using Excell = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

using Vs.Report;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;


namespace Vs.TimeAttendance
{
    public partial class ucPhepThang : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucPhepThang _instance;
        public static ucPhepThang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucPhepThang();
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

        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
       // private SqlConnection conn;

        public ucPhepThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvPhepThang, "Phep_thang");
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvThang, "Phep_thang");
        }
        private void ucPhepThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdPhepThang(false);
            Commons.Modules.sLoad = "";
            if (Modules.iPermission != 1)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[3].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[8].Properties.Visible = false;
                windowsUIButton.Buttons[9].Properties.Visible = false;
            }
            else
            {
                enableButon(true);
            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdPhepThang(false);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdPhepThang(false);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdPhepThang(false);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "phepthang":
                    {
                        if (grvPhepThang.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.ShowWaitForm(this);
                            LoadGrdPhepThang(true);
                            Commons.Modules.ObjSystems.HideWaitForm();
                            enableButon(false);
                        }
                        else
                        {
                           if(XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "MsgDuLieuCoBanCoMuonCapNhatLai"),
                            (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Commons.Modules.ObjSystems.ShowWaitForm(this);
                                LoadGrdPhepThang(true);
                                Commons.Modules.ObjSystems.HideWaitForm();
                                enableButon(false);
                            }
                        }
                        break;
                    }
                case "tinhphepton":
                    {
                        try
                        {
                            string sBT = "sBTPhepThang" + Commons.Modules.UserName;
                            //tạo bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvPhepThang), "");
                            //tính trên bảng tạm
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "update " + sBT + " SET PHEP_DA_NGHI = ISNULL(T_1,0) + ISNULL(T_2,0) + ISNULL(T_3,0) + ISNULL(T_4,0) + ISNULL(T_5,0) +ISNULL(T_6,0) + ISNULL(T_7,0) + ISNULL(T_8,0) + ISNULL(T_9,0) + ISNULL(T_10,0) + ISNULL(T_11,0)+ISNULL(T_12,0) update " + sBT + " SET PHEP_CON_LAI = PHEP_TIEU_CHUAN - PHEP_DA_NGHI - (ISNULL(TT_1, 0) + ISNULL(TT_2, 0) + ISNULL(TT_3, 0) + ISNULL(TT_4, 0) + ISNULL(TT_5, 0) + ISNULL(TT_6, 0) + ISNULL(TT_7, 0) + ISNULL(TT_8, 0) + ISNULL(TT_9, 0) + ISNULL(TT_10, 0) + ISNULL(TT_11, 0) + ISNULL(TT_12, 0)) + ISNULL(PHEP_UNG_TRUOC,0) ");
                            //Load lại lưới vừa tính
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, " SELECT * FROM " + sBT + " "));
                            grdPhepThang.DataSource = dt;
                            //xóa bảng tạm
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                        }catch{}
                        break;
                    }
                case "ThanhToan":
                    {
                        try
                        {
                            string sBT = "sBTTPhepThang" + Commons.Modules.UserName;
                            //tạo bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvPhepThang), "");
                            //tính trên bảng tạm
                            int i = Convert.ToDateTime(cboThang.EditValue).Month;
                            string COTUPDATE= "TT_" + i + "";
                               
                            
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "update " + sBT + " SET PHEP_DA_NGHI = ISNULL(T_1,0) + ISNULL(T_2,0) + ISNULL(T_3,0) + ISNULL(T_4,0) + ISNULL(T_5,0) +ISNULL(T_6,0) + ISNULL(T_7,0) + ISNULL(T_8,0) + ISNULL(T_9,0) + ISNULL(T_10,0) + ISNULL(T_11,0)+ISNULL(T_12,0) update " + sBT + " SET "+COTUPDATE+ " = PHEP_CON_LAI, PHEP_CON_LAI = 0");
                            //Load lại lưới vừa tính
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, " SELECT * FROM " + sBT + " "));
                            grdPhepThang.DataSource = dt;
                            //xóa bảng tạm
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                        }
                        catch { }
                        break;
                    }
                case "sua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvPhepThang, false);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvPhepThang.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                        //xóa
                        try
                        {
                            string sSql = "DELETE dbo.PHEP_THANG WHERE THANG = " + "'"+Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            grvPhepThang.DeleteSelectedRows();
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        LoadThang();
                        LoadGrdPhepThang(false);
                        break;
                    }
                case "luu":
                    {

                        //DataTable tb = new DataTable();
                        //tb = (DataTable)grdPhepThang.DataSource;

                        string sBT = "sBTPhepThang" + Commons.Modules.UserName;
                        //tạo bảng tạm
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvPhepThang), "");
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveTinhPhepThang", sBT, Convert.ToDateTime(cboThang.EditValue));
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        LoadThang();
                        LoadGrdPhepThang(false);
                        enableButon(true);
                        break;
                    }
                case "In":
                    {
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptCongNhanPhepThang", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = cboDV.EditValue;
                            cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = cboXN.EditValue;
                            cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = cboTo.EditValue;
                            cmd.Parameters.Add("@THANGIN", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue);
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dtBCPhep = new DataTable();
                            dtBCPhep = ds.Tables[0].Copy();

                            Excell.Application oXL;
                            Excell._Workbook oWB;
                            Excell._Worksheet oSheet;

                            oXL = new Excell.Application();
                            oXL.Visible = true;

                            oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                            oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                            string fontName = "Times New Roman";
                            int fontSizeTieuDe = 16;
                            int fontSizeNoiDung = 12;
                            int iTNgay = 1;
                            int iDNgay = 20;
                            int iSoNgay = (iDNgay - iTNgay);

                            string lastColumn = string.Empty;
                            lastColumn = "AO";

                            Excell.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A2", lastColumn + "2");
                            row2_TieuDe_BaoCao0.Merge();
                            row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                            row2_TieuDe_BaoCao0.Font.Name = fontName;
                            row2_TieuDe_BaoCao0.Font.Bold = true;
                            row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row2_TieuDe_BaoCao0.Value2 = "BẢNG TỔNG HỢP PHÉP NĂM";

                            //=====

                            Excell.Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                            row3_TieuDe_BaoCao.Merge();
                            row3_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                            row3_TieuDe_BaoCao.Font.Name = fontName;
                            row3_TieuDe_BaoCao.Font.Bold = true;
                            row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row3_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row3_TieuDe_BaoCao.Value2 = "Tháng " + Convert.ToDateTime(cboThang.EditValue).ToString("MM/yyyy");

                            oSheet.get_Range("A4").RowHeight = 30;
                            Excell.Range row4_TieuDe = oSheet.get_Range("A4", "A5");
                            row4_TieuDe.Merge();
                            row4_TieuDe.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row4_TieuDe.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row4_TieuDe.Font.Name = fontName;
                            row4_TieuDe.Font.Bold = true;
                            row4_TieuDe.ColumnWidth = 5;
                            row4_TieuDe.Value2 = "Stt";
                            row4_TieuDe.Interior.Color = Color.Yellow;

                            Excell.Range row5_TieuDe1 = oSheet.get_Range("B4", "B5");
                            row5_TieuDe1.Merge();
                            row5_TieuDe1.Font.Name = fontName;
                            row5_TieuDe1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe1.Font.Bold = true;
                            row5_TieuDe1.ColumnWidth = 35;
                            row5_TieuDe1.Interior.Color = Color.Yellow;
                            row5_TieuDe1.Value2 = "Họ và tên";

                            Excell.Range row5_TieuDe2 = oSheet.get_Range("C4", "C5");
                            row5_TieuDe2.Merge();
                            row5_TieuDe2.Font.Name = fontName;
                            row5_TieuDe2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe2.Font.Bold = true;
                            row5_TieuDe2.ColumnWidth = 12;
                            row5_TieuDe2.Interior.Color = Color.Yellow;
                            row5_TieuDe2.WrapText = true;
                            row5_TieuDe2.Value2 = "MS NV";



                            Excell.Range row5_TieuDe3 = oSheet.get_Range("D4", "D5");
                            row5_TieuDe3.Merge();
                            row5_TieuDe3.Font.Name = fontName;
                            row5_TieuDe3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe3.Font.Bold = true;
                            row5_TieuDe3.ColumnWidth = 25;
                            row5_TieuDe3.Interior.Color = Color.Yellow;
                            row5_TieuDe3.Value2 = "Công việc";

                            Excell.Range row5_TieuDe4 = oSheet.get_Range("E4", "E5");
                            row5_TieuDe4.Merge();
                            row5_TieuDe4.Font.Name = fontName;
                            row5_TieuDe4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe4.Font.Bold = true;
                            row5_TieuDe4.ColumnWidth = 25;
                            row5_TieuDe4.Interior.Color = Color.Yellow;
                            row5_TieuDe4.Value2 = "P.Ban/X.Nghiệp";

                            Excell.Range row5_TieuDe6 = oSheet.get_Range("F4", "F5");
                            row5_TieuDe6.Merge();
                            row5_TieuDe6.Font.Name = fontName;
                            row5_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe6.Font.Bold = true;
                            row5_TieuDe6.WrapText = true;
                            row5_TieuDe6.ColumnWidth = 25;
                            row5_TieuDe6.Interior.Color = Color.Yellow;
                            row5_TieuDe6.Value2 = "Tổ";

                            Excell.Range row5_TieuDe61 = oSheet.get_Range("G4", "G5");
                            row5_TieuDe61.Merge();
                            row5_TieuDe61.Font.Name = fontName;
                            row5_TieuDe61.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe61.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe61.Font.Bold = true;
                            row5_TieuDe61.WrapText = true;
                            row5_TieuDe61.ColumnWidth = 12;
                            row5_TieuDe61.Interior.Color = Color.Yellow;
                            row5_TieuDe61.Value2 = "Ngày vào công ty";

                            Excell.Range row4_TieuDe6 = oSheet.get_Range("H4", "H5");
                            row4_TieuDe6.Merge();
                            row4_TieuDe6.Font.Name = fontName;
                            row4_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row4_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row4_TieuDe6.Font.Bold = true;
                            row4_TieuDe6.ColumnWidth = 12;
                            row4_TieuDe6.WrapText = true;
                            row4_TieuDe6.Interior.Color = Color.Yellow;
                            row4_TieuDe6.Value2 = "Ngày ký hợp đồng";

                            Excell.Range row4_TieuDe6a = oSheet.get_Range("I4", "I5");
                            row4_TieuDe6a.Merge();
                            row4_TieuDe6a.Font.Name = fontName;
                            row4_TieuDe6a.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row4_TieuDe6a.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row4_TieuDe6a.Font.Bold = true;
                            row4_TieuDe6a.WrapText = true;
                            row4_TieuDe6a.ColumnWidth = 10;
                            row4_TieuDe6a.Interior.Color = Color.Yellow;
                            row4_TieuDe6a.Value2 = "Ngày phép cộng thêm";

                            Excell.Range row5_TieuDe6a = oSheet.get_Range("J4", "J5");
                            row5_TieuDe6a.Merge();
                            row5_TieuDe6a.Font.Name = fontName;
                            row5_TieuDe6a.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe6a.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe6a.Font.Bold = true;
                            row5_TieuDe6a.ColumnWidth = 10;
                            row5_TieuDe6a.WrapText = true;
                            row5_TieuDe6a.Interior.Color = Color.Yellow;
                            row5_TieuDe6a.Value2 = "Ngày phép thâm niên";

                            Excell.Range row5a_TieuDe6 = oSheet.get_Range("K4", "K5");
                            row5a_TieuDe6.Merge();
                            row5a_TieuDe6.Font.Name = fontName;
                            row5a_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5a_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5a_TieuDe6.Font.Bold = true;
                            row5a_TieuDe6.ColumnWidth = 10;
                            row5a_TieuDe6.WrapText = true;
                            row5a_TieuDe6.Interior.Color = Color.Yellow;
                            row5a_TieuDe6.Value2 = "Ngày phép ứng trước";

                            Excell.Range row5b_TieuDe6 = oSheet.get_Range("L4", "W4");
                            row5b_TieuDe6.Merge();
                            row5b_TieuDe6.Font.Name = fontName;
                            row5b_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5b_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5b_TieuDe6.Font.Bold = true;
                            row5b_TieuDe6.Interior.Color = Color.Yellow;
                            row5b_TieuDe6.Value2 = "Năm " + Convert.ToDateTime(cboThang.EditValue).ToString("yyyy");

                            Excell.Range row5c_TieuDe6 = oSheet.get_Range("L5");
                            row5c_TieuDe6.Font.Name = fontName;
                            row5c_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDe6.Font.Bold = true;
                            row5c_TieuDe6.Interior.Color = Color.Yellow;
                            row5c_TieuDe6.WrapText = true;
                            row5c_TieuDe6.ColumnWidth = 7;
                            row5c_TieuDe6.Value2 = "Tháng 1";

                            Excell.Range row5c_TieuDeT2 = oSheet.get_Range("M5");
                            row5c_TieuDeT2.Font.Name = fontName;
                            row5c_TieuDeT2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT2.Font.Bold = true;
                            row5c_TieuDeT2.Interior.Color = Color.Yellow;
                            row5c_TieuDeT2.WrapText = true;
                            row5c_TieuDeT2.ColumnWidth = 7;
                            row5c_TieuDeT2.Value2 = "Tháng 2";

                            Excell.Range row5c_TieuDeT3 = oSheet.get_Range("N5");
                            row5c_TieuDeT3.Font.Name = fontName;
                            row5c_TieuDeT3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT3.Font.Bold = true;
                            row5c_TieuDeT3.Interior.Color = Color.Yellow;
                            row5c_TieuDeT3.WrapText = true;
                            row5c_TieuDeT3.ColumnWidth = 7;
                            row5c_TieuDeT3.Value2 = "Tháng 3";

                            Excell.Range row5c_TieuDeT4 = oSheet.get_Range("O5");
                            row5c_TieuDeT4.Font.Name = fontName;
                            row5c_TieuDeT4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT4.Font.Bold = true;
                            row5c_TieuDeT4.Interior.Color = Color.Yellow;
                            row5c_TieuDeT4.WrapText = true;
                            row5c_TieuDeT4.ColumnWidth = 7;
                            row5c_TieuDeT4.Value2 = "Tháng 4";

                            Excell.Range row5c_TieuDeT5 = oSheet.get_Range("P5");
                            row5c_TieuDeT5.Font.Name = fontName;
                            row5c_TieuDeT5.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT5.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT5.Font.Bold = true;
                            row5c_TieuDeT5.Interior.Color = Color.Yellow;
                            row5c_TieuDeT5.WrapText = true;
                            row5c_TieuDeT5.ColumnWidth = 7;
                            row5c_TieuDeT5.Value2 = "Tháng 5";

                            Excell.Range row5c_TieuDeT6 = oSheet.get_Range("Q5");
                            row5c_TieuDeT6.Font.Name = fontName;
                            row5c_TieuDeT6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT6.Font.Bold = true;
                            row5c_TieuDeT6.Interior.Color = Color.Yellow;
                            row5c_TieuDeT6.WrapText = true;
                            row5c_TieuDeT6.ColumnWidth = 7;
                            row5c_TieuDeT6.Value2 = "Tháng 6";

                            Excell.Range row5c_TieuDeT7 = oSheet.get_Range("R5");
                            row5c_TieuDeT7.Font.Name = fontName;
                            row5c_TieuDeT7.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT7.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT7.Font.Bold = true;
                            row5c_TieuDeT7.Interior.Color = Color.Yellow;
                            row5c_TieuDeT7.WrapText = true;
                            row5c_TieuDeT7.ColumnWidth = 7;
                            row5c_TieuDeT7.Value2 = "Tháng 7";

                            Excell.Range row5c_TieuDeT8 = oSheet.get_Range("S5");
                            row5c_TieuDeT8.Font.Name = fontName;
                            row5c_TieuDeT8.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT8.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT8.Font.Bold = true;
                            row5c_TieuDeT8.Interior.Color = Color.Yellow;
                            row5c_TieuDeT8.WrapText = true;
                            row5c_TieuDeT8.ColumnWidth = 7;
                            row5c_TieuDeT8.Value2 = "Tháng 8";

                            Excell.Range row5c_TieuDeT9 = oSheet.get_Range("T5");
                            row5c_TieuDeT9.Font.Name = fontName;
                            row5c_TieuDeT9.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT9.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT9.Font.Bold = true;
                            row5c_TieuDeT9.Interior.Color = Color.Yellow;
                            row5c_TieuDeT9.WrapText = true;
                            row5c_TieuDeT9.ColumnWidth = 7;
                            row5c_TieuDeT9.Value2 = "Tháng 9";

                            Excell.Range row5c_TieuDeT10 = oSheet.get_Range("U5");
                            row5c_TieuDeT10.Font.Name = fontName;
                            row5c_TieuDeT10.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT10.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT10.Font.Bold = true;
                            row5c_TieuDeT10.Interior.Color = Color.Yellow;
                            row5c_TieuDeT10.WrapText = true;
                            row5c_TieuDeT10.ColumnWidth = 7;
                            row5c_TieuDeT10.Value2 = "Tháng 10";

                            Excell.Range row5c_TieuDeT11 = oSheet.get_Range("V5");
                            row5c_TieuDeT11.Font.Name = fontName;
                            row5c_TieuDeT11.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT11.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT11.Font.Bold = true;
                            row5c_TieuDeT11.Interior.Color = Color.Yellow;
                            row5c_TieuDeT11.WrapText = true;
                            row5c_TieuDeT11.ColumnWidth = 7;
                            row5c_TieuDeT11.Value2 = "Tháng 11";

                            Excell.Range row5c_TieuDeT12 = oSheet.get_Range("W5");
                            row5c_TieuDeT12.Font.Name = fontName;
                            row5c_TieuDeT12.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDeT12.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDeT12.Font.Bold = true;
                            row5c_TieuDeT12.Interior.Color = Color.Yellow;
                            row5c_TieuDeT12.WrapText = true;
                            row5c_TieuDeT12.ColumnWidth = 7;
                            row5c_TieuDeT12.Value2 = "Tháng 12";

                            Excell.Range row5_TieuDe8 = oSheet.get_Range("X4", "X5");
                            row5_TieuDe8.Merge();
                            row5_TieuDe8.Font.Name = fontName;
                            row5_TieuDe8.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe8.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe8.Font.Bold = true;
                            row5_TieuDe8.ColumnWidth = 10;
                            row5_TieuDe8.WrapText = true;
                            row5_TieuDe8.Interior.Color = Color.Yellow;
                            row5_TieuDe8.Value2 = "Đã nghỉ (tính đến tháng hiện tại)";

                            Excell.Range row5a_TieuDe8a = oSheet.get_Range("Y4", "Y5");
                            row5a_TieuDe8a.Merge();
                            row5a_TieuDe8a.Font.Name = fontName;
                            row5a_TieuDe8a.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5a_TieuDe8a.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5a_TieuDe8a.Font.Bold = true;
                            row5a_TieuDe8a.ColumnWidth = 13;
                            row5a_TieuDe8a.WrapText = true;
                            row5a_TieuDe8a.Interior.Color = Color.Yellow;
                            row5a_TieuDe8a.Value2 = "Tiêu chuẩn phép (tính đến tháng hiện tại)";

                            Excell.Range row5b_TieuDe8b = oSheet.get_Range("Z4", "Z5");
                            row5b_TieuDe8b.Font.Name = fontName;
                            row5b_TieuDe8b.Merge();
                            row5b_TieuDe8b.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5b_TieuDe8b.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5b_TieuDe8b.Font.Bold = true;
                            row5b_TieuDe8b.ColumnWidth = 10;
                            row5b_TieuDe8b.WrapText = true;
                            row5b_TieuDe8b.Interior.Color = Color.Yellow;
                            row5b_TieuDe8b.Value2 = "Còn lại";

                            Excell.Range row5c_TieuDe8c = oSheet.get_Range("AA4", "AA5");
                            row5c_TieuDe8c.Font.Name = fontName;
                            row5c_TieuDe8c.Merge();
                            row5c_TieuDe8c.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5c_TieuDe8c.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5c_TieuDe8c.Font.Bold = true;
                            row5c_TieuDe8c.Interior.Color = Color.Yellow;
                            row5c_TieuDe8c.WrapText = true;
                            row5c_TieuDe8c.ColumnWidth = 18;
                            row5c_TieuDe8c.Value2 = "Tháng làm việc năm " + Convert.ToDateTime(cboThang.EditValue).ToString("yyyy") + " (tính đến tháng hiện tại)";



                            Excell.Range rowtb_TieuDe6 = oSheet.get_Range("AB4", "AN4");
                            rowtb_TieuDe6.Merge();
                            rowtb_TieuDe6.Font.Name = fontName;
                            rowtb_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtb_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtb_TieuDe6.Font.Bold = true;
                            rowtb_TieuDe6.Interior.Color = Color.Yellow;
                            rowtb_TieuDe6.Value2 = "Thanh toán phép của năm";

                            Excell.Range rowtc_TieuDe6 = oSheet.get_Range("AB5");
                            rowtc_TieuDe6.Font.Name = fontName;
                            rowtc_TieuDe6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDe6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDe6.Font.Bold = true;
                            rowtc_TieuDe6.Interior.Color = Color.Yellow;
                            rowtc_TieuDe6.WrapText = true;
                            rowtc_TieuDe6.ColumnWidth = 7;
                            rowtc_TieuDe6.Value2 = "Tháng 1";

                            Excell.Range rowtc_TieuDeT2 = oSheet.get_Range("AC5");
                            rowtc_TieuDeT2.Font.Name = fontName;
                            rowtc_TieuDeT2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT2.Font.Bold = true;
                            rowtc_TieuDeT2.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT2.WrapText = true;
                            rowtc_TieuDeT2.ColumnWidth = 7;
                            rowtc_TieuDeT2.Value2 = "Tháng 2";

                            Excell.Range rowtc_TieuDeT3 = oSheet.get_Range("AD5");
                            rowtc_TieuDeT3.Font.Name = fontName;
                            rowtc_TieuDeT3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT3.Font.Bold = true;
                            rowtc_TieuDeT3.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT3.WrapText = true;
                            rowtc_TieuDeT3.ColumnWidth = 7;
                            rowtc_TieuDeT3.Value2 = "Tháng 3";

                            Excell.Range rowtc_TieuDeT4 = oSheet.get_Range("AE5");
                            rowtc_TieuDeT4.Font.Name = fontName;
                            rowtc_TieuDeT4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT4.Font.Bold = true;
                            rowtc_TieuDeT4.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT4.WrapText = true;
                            rowtc_TieuDeT4.ColumnWidth = 7;
                            rowtc_TieuDeT4.Value2 = "Tháng 4";

                            Excell.Range rowtc_TieuDeT5 = oSheet.get_Range("AF5");
                            rowtc_TieuDeT5.Font.Name = fontName;
                            rowtc_TieuDeT5.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT5.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT5.Font.Bold = true;
                            rowtc_TieuDeT5.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT5.WrapText = true;
                            rowtc_TieuDeT5.ColumnWidth = 7;
                            rowtc_TieuDeT5.Value2 = "Tháng 5";

                            Excell.Range rowtc_TieuDeT6 = oSheet.get_Range("AG5");
                            rowtc_TieuDeT6.Font.Name = fontName;
                            rowtc_TieuDeT6.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT6.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT6.Font.Bold = true;
                            rowtc_TieuDeT6.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT6.WrapText = true;
                            rowtc_TieuDeT6.ColumnWidth = 7;
                            rowtc_TieuDeT6.Value2 = "Tháng 6";

                            Excell.Range rowtc_TieuDeT7 = oSheet.get_Range("AH5");
                            rowtc_TieuDeT7.Font.Name = fontName;
                            rowtc_TieuDeT7.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT7.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT7.Font.Bold = true;
                            rowtc_TieuDeT7.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT7.WrapText = true;
                            rowtc_TieuDeT7.ColumnWidth = 7;
                            rowtc_TieuDeT7.Value2 = "Tháng 7";

                            Excell.Range rowtc_TieuDeT8 = oSheet.get_Range("AI5");
                            rowtc_TieuDeT8.Font.Name = fontName;
                            rowtc_TieuDeT8.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT8.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT8.Font.Bold = true;
                            rowtc_TieuDeT8.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT8.WrapText = true;
                            rowtc_TieuDeT8.ColumnWidth = 7;
                            rowtc_TieuDeT8.Value2 = "Tháng 8";

                            Excell.Range rowtc_TieuDeT9 = oSheet.get_Range("AJ5");
                            rowtc_TieuDeT9.Font.Name = fontName;
                            rowtc_TieuDeT9.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT9.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT9.Font.Bold = true;
                            rowtc_TieuDeT9.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT9.WrapText = true;
                            rowtc_TieuDeT9.ColumnWidth = 7;
                            rowtc_TieuDeT9.Value2 = "Tháng 9";

                            Excell.Range rowtc_TieuDeT10 = oSheet.get_Range("AK5");
                            rowtc_TieuDeT10.Font.Name = fontName;
                            rowtc_TieuDeT10.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT10.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT10.Font.Bold = true;
                            rowtc_TieuDeT10.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT10.WrapText = true;
                            rowtc_TieuDeT10.ColumnWidth = 7;
                            rowtc_TieuDeT10.Value2 = "Tháng 10";

                            Excell.Range rowtc_TieuDeT11 = oSheet.get_Range("AL5");
                            rowtc_TieuDeT11.Font.Name = fontName;
                            rowtc_TieuDeT11.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT11.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT11.Font.Bold = true;
                            rowtc_TieuDeT11.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT11.WrapText = true;
                            rowtc_TieuDeT11.ColumnWidth = 7;
                            rowtc_TieuDeT11.Value2 = "Tháng 11";

                            Excell.Range rowtc_TieuDeT12 = oSheet.get_Range("AM5");
                            rowtc_TieuDeT12.Font.Name = fontName;
                            rowtc_TieuDeT12.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDeT12.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDeT12.Font.Bold = true;
                            rowtc_TieuDeT12.Interior.Color = Color.Yellow;
                            rowtc_TieuDeT12.WrapText = true;
                            rowtc_TieuDeT12.ColumnWidth = 7;
                            rowtc_TieuDeT12.Value2 = "Tháng 12";

                            Excell.Range rowtc_TieuDet = oSheet.get_Range("AN5");
                            rowtc_TieuDet.Font.Name = fontName;
                            rowtc_TieuDet.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            rowtc_TieuDet.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            rowtc_TieuDet.Font.Bold = true;
                            rowtc_TieuDet.Interior.Color = Color.Yellow;
                            rowtc_TieuDet.WrapText = true;
                            rowtc_TieuDet.ColumnWidth = 6;
                            rowtc_TieuDet.Value2 = "Tổng";

                            Excell.Range row5_TieuDe7 = oSheet.get_Range("AO4", "AO5");
                            row5_TieuDe7.Merge();
                            row5_TieuDe7.Font.Name = fontName;
                            row5_TieuDe7.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            row5_TieuDe7.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                            row5_TieuDe7.Font.Bold = true;
                            row5_TieuDe7.Interior.Color = Color.Yellow;
                            row5_TieuDe7.WrapText = true;
                            row5_TieuDe7.ColumnWidth = 11;
                            row5_TieuDe7.Value2 = "Ghi tháng không được tính phép";

                            DataRow[] dr = dtBCPhep.Select();
                            string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];
                            int col = 0;
                            int rowCnt = 0;
                            foreach (DataRow row in dr)
                            {
                                for (col = 0; col < dtBCPhep.Columns.Count; col++)
                                {
                                    rowData[rowCnt, col] = row[col].ToString();
                                }

                                rowCnt++;
                            }
                            rowCnt = rowCnt + 5;
                            oSheet.get_Range("A6", "AO" + rowCnt.ToString()).Value2 = rowData;
                            oSheet.get_Range("A6", "AO" + rowCnt.ToString()).Font.Name = fontName;
                            oSheet.get_Range("A6", "AO" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                            ////Kẻ khung toàn bộ
                            BorderAround(oSheet.get_Range("A4", "AO" + rowCnt.ToString()));

                            Excell.Range formatRange;
                            formatRange = oSheet.get_Range("I6", "I" + rowCnt.ToString());
                            formatRange.NumberFormat = "#,##0";
                            formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            formatRange = oSheet.get_Range("J6", "J" + rowCnt.ToString());
                            formatRange.NumberFormat = "#,##0";
                            formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                            //formatRange = oSheet.get_Range("L6", "L" + rowCnt.ToString());
                            //formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                            //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                            //formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                            string CurrentColumn = string.Empty;
                            for (col = 10; col < 40; col++)
                            {
                                CurrentColumn = CharacterIncrement(col);
                                formatRange = oSheet.get_Range(CurrentColumn + "6", CurrentColumn + rowCnt.ToString());
                                formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                                try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            }

                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case "khongluu":
                    {
                        LoadGrdPhepThang(false);
                        //Commons.Modules.ObjSystems.DeleteAddRow(grvPhepThang);
                        enableButon(true);
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
            throw new NotImplementedException();
        }
        #region hàm xử lý dữ liệu
        private void LoadGrdPhepThang(bool bThem)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanPhepThang", bThem, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Convert.ToDateTime(cboThang.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                for (int i = 4; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                }
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
                dt.Columns[3].ReadOnly = true;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdPhepThang, grvPhepThang, dt, bThem, true, false, true,true,this.Name);
                grvPhepThang.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvPhepThang.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvPhepThang.Columns["NGAY_VAO_LAM"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvPhepThang.Columns["PHEP_THAM_NIEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvPhepThang.Columns["PHEP_UNG_TRUOC"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                grvPhepThang.Columns["PHEP_CON_LAI"].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                grvPhepThang.Columns["ID_CN"].Visible = false;
                //visible tháng lớn hơn tháng đang chọn
                int iVisible = 6;
                for (int i = 1; i <= 12; i++)
                {
                    grvPhepThang.Columns["T_" + i + ""].VisibleIndex = iVisible + 1;
                    grvPhepThang.Columns["TT_" + i + ""].VisibleIndex = iVisible + 2;
                    if (i > Convert.ToDateTime(cboThang.EditValue).Month)
                    {
                        grvPhepThang.Columns["T_" + i + ""].VisibleIndex = iVisible + 1;
                        grvPhepThang.Columns["TT_" + i + ""].VisibleIndex = iVisible + 2;
                        grvPhepThang.Columns["T_" + i + ""].Visible = false;
                        grvPhepThang.Columns["TT_" + i + ""].Visible = false;
                    }
                    else
                    {
                        grvPhepThang.Columns["T_" + i + ""].Visible = true;
                        grvPhepThang.Columns["TT_" + i + ""].Visible = true;
                    }
                    iVisible = iVisible + 2;
                }

                grvPhepThang.Columns["PHEP_DA_NGHI"].VisibleIndex = 50;
                grvPhepThang.Columns["PHEP_TIEU_CHUAN"].VisibleIndex = 51;
                grvPhepThang.Columns["SO_THANG_LAM_VIEC"].VisibleIndex = 52;
                grvPhepThang.Columns["PHEP_CON_LAI"].VisibleIndex = 53;
                Commons.Modules.sLoad = "";
            }
            catch (Exception)
            {
            }
        }

        private bool Savedata()
        {
            try
            {
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvKeHoachDiCa), "");
                //string sSql = "DELETE KE_HOACH_dI_CA WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") + " INSERT INTO KE_HOACH_dI_CA(ID_CN,ID_NHOM,CA,TU_NGAY,DEN_NGAY,GHI_CHU) SELECT ID_CN,ID_NHOM,CA,TU_NGAY,DEN_NGAY,GHI_CHU FROM " + sBT + "";
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;

            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = !visible;
            windowsUIButton.Buttons[10].Properties.Visible = !visible;
            windowsUIButton.Buttons[11].Properties.Visible = !visible;
            windowsUIButton.Buttons[12].Properties.Visible = !visible;

            //searchControl.Visible = visible;
            cboThang.Properties.ReadOnly = !visible;
            cboDV.Properties.ReadOnly = !visible;
            cboXN.Properties.ReadOnly = !visible;
            cboTo.Properties.ReadOnly = !visible;
        }
        #endregion
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdPhepThang(false);
            Commons.Modules.sLoad = "";
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.PHEP_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.ToString("MM/yyyy");
            }
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
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
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void BorderAround(Excell.Range range)
        {
            Excell.Borders borders = range.Borders;
            borders[Excell.XlBordersIndex.xlEdgeLeft].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeTop].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeBottom].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeRight].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excell.XlBordersIndex.xlInsideVertical].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlInsideHorizontal].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlDiagonalUp].LineStyle = Excell.XlLineStyle.xlLineStyleNone;
            borders[Excell.XlBordersIndex.xlDiagonalDown].LineStyle = Excell.XlLineStyle.xlLineStyleNone;
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

        private void grvPhepThang_RowCountChanged(object sender, EventArgs e)
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
    }
}
