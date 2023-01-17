using System;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Vs.Recruit
{
    public partial class frmInKeHoachTD : DevExpress.XtraEditors.XtraForm
    {

        DataTable tbdonvi = new DataTable();
        DateTime TN, DN;
        public frmInKeHoachTD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,windowsUIButton);
        }

        private void frmInKeHoachTD_Load(object sender, EventArgs e)
        {
            datNam.EditValue = DateTime.Now;
            LoadTuan();
        }

        private void LoadTuan()
        {
            try
            {

                DataTable tb = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "GetTUAN_TRONG_NAM", DateTime.Now.Year).Tables[0];
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTuTuan, tb, "TUAN", "TEN_TUAN", "TEN_TUAN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDenTuan, tb, "TUAN", "TEN_TUAN", "TEN_TUAN");

                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                int Maxtuan = tb.AsEnumerable().Max(x => Convert.ToInt32(x["TUAN"]));
                //nếu tuần hiện tại nhỏ hơn 4 thì từ tuần lùi về một năm
                if (weekNum < 4)
                {
                    cboTuTuan.EditValue = 1;
                    cboDenTuan.EditValue = 8;
                }
                else
                {
                    if (weekNum > Maxtuan - 4)
                    {
                        cboTuTuan.EditValue = Maxtuan - 8;
                        cboDenTuan.EditValue = Maxtuan;
                    }
                    else
                    {
                        cboTuTuan.EditValue = weekNum - 4;
                        cboDenTuan.EditValue = weekNum + 4;
                    }
                }

            }
            catch
            {
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        try
                        {
                            if(Datain() ==false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                                return;
                            }    
                            InKeHoachTD();
                        }
                        catch { }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        private bool Datain()
        {
            //năm sau lớn hơn năm đầu
            try
            {

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[fnTUAN_TRONG_NAM](" + datNam.DateTime.Year + ")"));
                dt = dt.AsEnumerable().Where(x => Convert.ToInt32(x["TUAN"]) >= Convert.ToInt32(cboTuTuan.EditValue) && Convert.ToInt32(x["TUAN"]) <= Convert.ToInt32(cboDenTuan.EditValue)).CopyToDataTable();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTuanThang" + Commons.Modules.iIDUser, dt, "");

                TN = dt.AsEnumerable().Min(x => Convert.ToDateTime(x["TU_NGAY"]));
                DN = dt.AsEnumerable().Max(x => Convert.ToDateTime(x["DEN_NGAY"]));

                //lấy đơn vị cần in theo phân quyền
                tbdonvi = new DataTable();
                tbdonvi.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT ID_DV,TEN_DV FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") ORDER BY TEN_DV"));
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoTuyenDung", TN, DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, -1, "sBTTuanThang" + Commons.Modules.iIDUser));
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                dt.Clear();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, false, this.Name);
                grvData.Columns[0].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colSTT"); /*"STT";*/
                grvData.Columns[1].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colUCTD");/*"YÊU CẦU TD";*/
                grvData.Columns[2].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colVTTD");  /*"VỊ TRÍ CẦN TUYỂN DỤNG";*/
                grvData.Columns[3].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colChucDanh");/*"CHỨC DANH";*/
                grvData.Columns[4].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colSLHC"); /*"SL HIỆN CÓ";*/
                grvData.Columns[5].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colCanTuyenBS");/*"CẦN TUYỂN BỔ SUNG";*/
                grvData.Columns[6].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colBoPhanCan");/*"BỘ PHẬN CẦN";*/
                grvData.Columns[7].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colTGCan");/*"THỜI GIAN CẦN";*/
                grvData.Columns[8].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colLyDoTuyen");/*"LÝ DO TUYỂN";*/
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string getValueCell(Excel.Worksheet MWsheet, int DongBD, int CotBD)
        {
            string resulst = MWsheet.Cells[DongBD, CotBD].Value;
            string[] array = resulst.Split('!');
            MWsheet.Cells[DongBD, CotBD].Value2 = array[3] == "KH" ? "Plan" : "Actual";
            if (CotBD % 2 != 0)
            {
                MWsheet.Cells[DongBD, CotBD].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            }
            return array[1] + "\n" + array[2];

        }
        private void InKeHoachTD()
        {
            List<int> list = new List<int>();
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
                excelApplication.Cells.Font.Size = 13;
                excelWorkSheet.AutoFilterMode = false;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = false;
                int DONG = 0;

                DONG = Commons.Modules.MExcel.TaoTTChung(excelWorkSheet, 1, 2, 1, TCot, 0, 0);

                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 4, DONG);

                int COT = 10;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 18);
                title.Merge(true);
                title.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name,"BCKeHoachTuyenDung");  /*"BÁO CÁO THEO DÕI THỰC HIỆN KẾ HOẠCH TUYỂN DỤNG";*/
                title.Font.Size = 16;
                title.RowHeight = 54;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.RowHeight = 21;
                title.Merge(true);

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot);
                title.RowHeight = 30;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Merge(true);
                title.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblSoLuongDuLienVaThucTe");/*"SỐ LƯỢNG DỰ KIẾN & THỰC TẾ ĐI LÀM";*/
                title.Interior.Color = System.Drawing.Color.FromArgb(215, 227, 186);
                title.Font.Bold = true;

                DONG++;

                for (int i = 1; i <= TCot; i++)
                {
                    if (i <= 9)
                    {
                        title = excelWorkSheet.Cells[DONG, i];
                        title.Value2 = Commons.Modules.MExcel.getValueCell(excelWorkSheet, DONG + 1, i);
                    }
                    else
                    {
                        title = excelWorkSheet.Cells[DONG, i];
                        title.ColumnWidth = 9;
                        if (i % 2 == 0)
                        {
                            title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG, i + 1);
                            title.Merge(true);
                            title.Value2 = getValueCell(excelWorkSheet, DONG + 1, i);
                        }
                        else
                        {
                            getValueCell(excelWorkSheet, DONG + 1, i);

                        }
                    }
                }

                //định dạng style
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.RowHeight = 50;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Interior.Color = System.Drawing.Color.FromArgb(252, 213, 180);
                title.Font.Bold = true;

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot);
                title.RowHeight = 17;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Interior.Color = System.Drawing.Color.FromArgb(218, 238, 243);
                title.Font.Bold = true;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, 1, DONG, COT - 1);
                title.Interior.Color = System.Drawing.Color.FromArgb(238, 236, 225);
                title.Font.Bold = true;
                title.WrapText = true;

                //insert cottong
                //int j = 2;
                //for (int i = COT; i <= TCot + 1; i++)
                //{
                //    if ((i - COT) % 8 == 0 && i != COT)
                //    {
                //        Commons.Modules.MExcel.ThemCot(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, 1, i - (j - 2));
                //        title = excelWorkSheet.Cells[DONG, i - (j - 2)];
                //        title.Value2 = "Total Actual";
                //        title.WrapText = true;

                //        title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, i - j, DONG - 1, i - j);
                //        title = excelWorkSheet.Cells[DONG - 1, i - (j - 2)];
                //        string s = title1.Value;
                //        title.Value2 = "Tháng " + s.Substring(s.Length - 3, 2);
                //        title.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);


                //        title = excelWorkSheet.Cells[DONG + 1, i - (j - 2)];
                //        title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 1) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 3) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 5) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 7);

                //        title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i - (j - 2), DONG + TDong, i - (j - 2));
                //        title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                //        j--;

                //    }
                //}

                //Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, DONG);

                //title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 2, DONG, 2);
                //title.Value2 = tbdonvi.Rows[0][1].ToString();
                //title.RowHeight = 16.5;
                //list.Add(DONG);      
                //title = excelWorkSheet.Cells[DONG, 5];
                //title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1 , 5) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + grvData.RowCount, 5) + ")";
                //title = excelWorkSheet.Cells[DONG, 6];
                //title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, 6) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + grvData.RowCount, 6) + ")";
                //title = excelWorkSheet.Cells[DONG, COT];
                //title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + grvData.RowCount, COT) + ")";
                //title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG , COT, DONG, TCot);
                //title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                //DONG += grvData.RowCount + 1;
                DONG++;
                for (int i = 0; i < tbdonvi.Rows.Count; i++)
                {
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoTuyenDung", TN, DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, tbdonvi.Rows[i][0], "sBTTuanThang" + Commons.Modules.iIDUser));

                    if (dt.Rows.Count == 0) continue;

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 9);
                    title.Interior.Color = System.Drawing.Color.FromArgb(238, 236, 225);
                    title.Font.Bold = true;

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 10, DONG, TCot);
                    title.Interior.Color = System.Drawing.Color.FromArgb(218, 238, 243);
                    title.Font.Bold = true;

                    //vẻ dòng tổng
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 2, DONG, 2);
                    title.Value2 = tbdonvi.Rows[i][1].ToString();
                    list.Add(DONG);

                    title = excelWorkSheet.Cells[DONG, 5];
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, 5) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + dt.Rows.Count, 5) + ")";
                    title = excelWorkSheet.Cells[DONG, 6];
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, 6) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + dt.Rows.Count, 6) + ")";

                    title = excelWorkSheet.Cells[DONG, COT];
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + dt.Rows.Count, COT) + ")";
                    title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                    title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot);
                    title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, 1, DONG + dt.Rows.Count, dt.Columns.Count);
                    Commons.Modules.MExcel.MExportExcel(dt, excelWorkSheet, title, false);

                    DONG += dt.Rows.Count + 1;
                }
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 10, "@", true, 1, 1, DONG + TDong, 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 20, "@", true, 1, 2, DONG + TDong, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 45, "@", true, 1, 3, DONG + TDong, 3);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 14, "@", true, 1, 4, DONG + TDong, 4);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 14, "#", true, 1, 5, DONG + TDong, 6);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 16, "@", true, 1, 7, DONG + TDong, 7);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 15, "@", true, 1, 8, DONG + TDong, 8);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 20, "@", true, 1, 9, DONG + TDong, 9);

                //tính dòng cuối

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 4);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Merge(true);
                title.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongCong"); /*"TỔNG CỘNG";*/
                title.Interior.Color = System.Drawing.Color.FromArgb(184, 204, 228);
                title.Font.Bold = true;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 5, DONG, TCot);
                title.Interior.Color = System.Drawing.Color.FromArgb(184, 204, 228);
                title.Font.Bold = true;


                title = excelWorkSheet.Cells[DONG, 5];
                title.Value2 = "=SUM(" + GetSumLastRow(list, 5) + ")";

                title = excelWorkSheet.Cells[DONG, 6];
                title.Value2 = "=SUM(" + GetSumLastRow(list, 6) + ")";

                title = excelWorkSheet.Cells[DONG, COT];
                title.Value2 = "=SUM(" + GetSumLastRow(list, COT) + ")";
                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot);
                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, 6, 1, DONG, TCot);
                title.Borders.LineStyle = 1;

                excelWorkSheet.Application.ActiveWindow.SplitRow = 8;
                excelWorkSheet.Application.ActiveWindow.SplitColumn = 3;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = true;

                excelWorkbook.Save();
                excelApplication.Visible = true;
                Commons.Modules.MExcel.MReleaseObject(excelWorkSheet);
                Commons.Modules.MExcel.MReleaseObject(excelWorkbook);
                Commons.Modules.MExcel.MReleaseObject(excelApplication);

                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "InKhongThanhCong", Commons.Modules.TypeLanguage) + ": " + ex.Message);
            }
        }

        private string GetSumLastRow(List<int> list, int col)
        {
            string resulst = "";
            foreach (var item in list)
            {
                resulst += "" + Commons.Modules.MExcel.TimDiemExcel(item, col) + ",";
            }
            return resulst.Substring(0, resulst.Length - 1);
        }

        private void frmInKeHoachTD_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Commons.Modules.ObjSystems.XoaTable("sBTTuanThang" + Commons.Modules.iIDUser);
        }

        private DataTable TinhSoTuanCuaTHang(DateTime TN, DateTime DN)
        {
            try
            {
                DN = TN.AddMonths(1).AddDays(-1);
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));
                //kiểm tra ngày bắc đầu có phải thứ 2 không
                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        if (TN.DayOfWeek == DayOfWeek.Monday)
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7));
                            TN = TN.AddDays(8);
                            continue;
                        }
                        else
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7 + (7 - (int)TN.DayOfWeek)));
                            TN = TN.AddDays(8 + (7 - (int)TN.DayOfWeek));
                            continue;
                        }
                    }
                    if (i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i == 4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }

    }
}
