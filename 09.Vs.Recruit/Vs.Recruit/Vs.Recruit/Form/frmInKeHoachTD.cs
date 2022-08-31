using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.Diagnostics;
using Vs.Report;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Recruit
{
    public partial class frmInKeHoachTD : DevExpress.XtraEditors.XtraForm
    {
        public frmInKeHoachTD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
        }

        private void frmInKeHoachTD_Load(object sender, EventArgs e)
        {
            datTThang.EditValue = DateTime.Now;
            datDThang.EditValue = DateTime.Now;
            //DateTime TN = datThang.DateTime.Date.AddDays(-datThang.DateTime.Date.Day + 1);
            //DateTime DN = TN.AddMonths(1).AddDays(-1);
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
                            InKeHoachTD(Datain());
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

        private int Datain()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Tuan", typeof(Int32));
            dt.Columns.Add("TNgay", typeof(DateTime));
            dt.Columns.Add("DNgay", typeof(DateTime));

            DateTime TN = datTThang.DateTime.Date.AddDays(-datTThang.DateTime.Date.Day + 1);
            DateTime DN = datDThang.DateTime.Date.AddDays(-datDThang.DateTime.Date.Day + 1);
            DN = DN.AddMonths(1).AddDays(-1);
            int iThang = 0;
            while (TN.Month <= DN.Month && TN.Year <= DN.Year)
            {
                dt.Merge(TinhSoTuanCuaTHang(TN, DN), true);
                TN = TN.AddMonths(1);
                iThang++;
            }

            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTuanThang" + Commons.Modules.UserName, dt, "");

            dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoTuyenDung", datTThang.DateTime.Date.AddDays(-datTThang.DateTime.Date.Day + 1), DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "sBTTuanThang" + Commons.Modules.UserName));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, false, this.Name);

            grvData.Columns[0].Caption = "STT";
            grvData.Columns[1].Caption = "YÊU CẦU TD";
            grvData.Columns[2].Caption = "VỊ TRÍ CẦN TUYỂN DỤNG";
            grvData.Columns[3].Caption = "CHỨC DANH";
            grvData.Columns[4].Caption = "SL HIỆN CÓ";
            grvData.Columns[5].Caption = "CẦN TUYỂN BỔ SUNG";
            grvData.Columns[6].Caption = "BỘ PHẬN CẦN";
            grvData.Columns[7].Caption = "THỜI GIAN CẦN";
            grvData.Columns[8].Caption = "LÝ DO TUYỂN";
            return iThang;

        }
        private string getValueCell(Microsoft.Office.Interop.Excel.Worksheet MWsheet, int DongBD, int CotBD)
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
        private void InKeHoachTD(int iThang)
        {
          
            DateTime TN = datTThang.DateTime.Date.AddDays(-datTThang.DateTime.Date.Day + 1);
            DateTime DN = datDThang.DateTime.Date.AddDays(-datDThang.DateTime.Date.Day + 1).AddMonths(1).AddDays(-1);
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
            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
            try
            {
                excelApplication.Cells.Borders.LineStyle = 0;
                excelApplication.Cells.Font.Name = "Times New Roman";
                excelApplication.Cells.Font.Size = 13;
                excelWorkSheet.AutoFilterMode = false;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = false;

                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 4, 1);

                int DONG = 1;
                int COT = 10;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 18);
                title.Merge(true);
                title.Value2 = "BÁO CÁO CHỈ TIÊU TUYỂN DỤNG THEO TUẦN/ THÁNG \n ("+ TN.ToString("dd/MM/yyyy") +" - "+ DN.ToString("dd/MM/yyyy") + ")";
                title.Font.Size = 18;
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
                title.Value2 = "SỐ LƯỢNG DỰ KIẾN & THỰC TẾ ĐI LÀM";
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
                title.RowHeight = 55;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Interior.Color = System.Drawing.Color.FromArgb(252, 213, 180);
                title.Font.Bold = true;

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot);
                title.RowHeight = 36;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Interior.Color = System.Drawing.Color.FromArgb(218, 238, 243);
                title.Font.Bold = true;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, 1, DONG, COT - 1);
                title.Interior.Color = System.Drawing.Color.FromArgb(238, 236, 225);
                title.Font.Bold = true;
                title.WrapText = true;



                //insert cottong
                int j = 2;
                for (int i = COT; i <= TCot + 1; i++)
                {
                    if ((i - COT) % 8 == 0 && i != COT)
                    {
                        Commons.Modules.MExcel.ThemCot(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftToRight, 1, i - (j - 2));
                        title = excelWorkSheet.Cells[DONG, i - (j - 2)];
                        title.Value2 = "Total Actual";
                        title.WrapText = true;

                        title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, i - j, DONG - 1, i - j);
                        title = excelWorkSheet.Cells[DONG - 1, i - (j - 2)];
                        string s = title1.Value;
                        title.Value2 = "Tháng " + s.Substring(s.Length - 3, 2);
                        title.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);


                        title = excelWorkSheet.Cells[DONG + 1, i - (j - 2)];
                        title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 1) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 3) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 5) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - (j - 2) - 7);

                        title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i - (j - 2), DONG + TDong, i - (j - 2));
                        title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                        j--;

                    }
                }

                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 6, "@", true, DONG + 1, 1, DONG + TDong, 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 17, "@", true, DONG + 1, 2, DONG + TDong, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 45, "@", true, DONG + 1, 3, DONG + TDong, 3);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 14, "@", true, DONG + 1, 4, DONG + TDong, 4);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 14, "#", true, DONG + 1, 5, DONG + TDong, 6);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 16, "@", true, DONG + 1, 7, DONG + TDong, 7);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 15, "dd/MM/yyyy", true, DONG + 1, 8, DONG + TDong, 8);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 20, "@", true, DONG + 1, 9, DONG + TDong, 9);

                //tính dòng cuối
                DONG = DONG + TDong + 1;
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 4);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Merge(true);
                title.Value2 = "TỔNG CỘNG";
                title.Interior.Color = System.Drawing.Color.FromArgb(184, 204, 228);
                title.Font.Bold = true;

                title = excelWorkSheet.Cells[DONG, 5];
                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG - TDong, 5) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG - 1, 5) + ")";

                title = excelWorkSheet.Cells[DONG, 6];
                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG - TDong, 6) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG - 1, 6) + ")";

                title = excelWorkSheet.Cells[DONG, COT];
                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG - TDong, COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG - 1, COT) + ")";

                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, TCot + iThang);
                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                excelWorkSheet.Application.ActiveWindow.SplitRow = 5;
                excelWorkSheet.Application.ActiveWindow.SplitColumn = 1;
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
