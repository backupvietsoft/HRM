using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

using Vs.Report;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;


namespace Vs.Recruit
{
    public partial class ucDaoTaoDinhHuong : DevExpress.XtraEditors.XtraUserControl
    {
        private bool bThem = false;
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

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetDTDinhHuong", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Convert.ToDateTime(cboThang.EditValue);
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
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }

        private bool Savedata()
        {
            string sBT = "sBTDTDinhHuong" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDTDinhHuong), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDTDinhHuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue), sBT);
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
                DateTime dtNgay = Convert.ToDateTime(cboThang.EditValue);

                string sPath = "";
                sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;
                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelApplication.DisplayAlerts = true;

                excelApplication.Visible = false;

                System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                excelWorkbook.SaveAs(sPath);

                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetDTDinhHuong", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                DateTime DT = DateTime.Now;
                try
                {
                    DT = DateTime.Parse(cboThang.Text.ToString());
                }
                catch { }
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = DT;
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = 3;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.DefaultView.RowFilter = "";
                DataView dv = dt.DefaultView;

                DataTable dt1 = new DataTable();
                dt1 = dv.ToTable(false, "STT", "HO_TEN", "NGAY_SINH", "NGAY_NHAN_VIEC", "NQ_LD", "TL_THUONG", "TU_LD", "CS_TC", "GQ_KN", "AT_HC", "SO_CC", "PL_RT", "NQ_PCCC", "NQ_VSATLD", "TN_HL", "KY_TEN");
                dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                dt1.Columns["NGAY_SINH"].ColumnName = "Năm sinh";
                dt1.Columns["NGAY_NHAN_VIEC"].ColumnName = "Ngày vào";
                dt1.Columns["NQ_LD"].ColumnName = "Nội quy lao động";
                dt1.Columns["TL_THUONG"].ColumnName = "Tiền lương thưởng";
                dt1.Columns["TU_LD"].ColumnName = "Thỏa ước lao động";
                dt1.Columns["CS_TC"].ColumnName = "Các chính sách TNXH và tiêu chuẩn TNXH";
                dt1.Columns["GQ_KN"].ColumnName = "Giải quyết khiếu nại";
                dt1.Columns["AT_HC"].ColumnName = "An toàn hóa chất";
                dt1.Columns["SO_CC"].ColumnName = "Sơ cấp cứu ban đầu";
                dt1.Columns["PL_RT"].ColumnName = "Phân loại rác thải";
                dt1.Columns["NQ_PCCC"].ColumnName = "Nội Quy PCCC";
                dt1.Columns["NQ_VSATLD"].ColumnName = "Nội Quy VSATLĐ";
                dt1.Columns["TN_HL"].ColumnName = "Tham nhũng, hối lộ";
                dt1.Columns["KY_TEN"].ColumnName = "Học Viên (Ký Nhận)";
                Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                Ranges1.Range["A1:F1"].Font.Bold = true;
                Ranges1.Range["A1:F1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Ranges1.Range["A1:F1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Ranges1.ColumnWidth = 20;
                Ranges1.Range["B1"].ColumnWidth = 30;
                MExportExcel(dt1, excelWorkSheet, Ranges1);

                excelApplication.Visible = true;
                excelWorkbook.Save();
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
        private void MExportExcel(DataTable dtTmp, Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.Range sRange)
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
    }
}
