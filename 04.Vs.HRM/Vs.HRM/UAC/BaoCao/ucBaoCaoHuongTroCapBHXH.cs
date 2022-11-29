using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoHuongTroCapBHXH : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoHuongTroCapBHXH()
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
                        try
                        {

                            if (Datain() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                                return;
                            }
                            InBaoCao();
                        }
                        catch { }
                        break;
                    }
                default:
                    break;
            }
        }

        private void InBaoCao()
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
                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, DONG);

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.Merge(true);
                title.Value2 = "DANH SÁCH THEO DÕI CNV NGHỈ HƯỞNG CHẾ ĐỘ BHXH TỪ THÁNG "+ dTuNgay.DateTime.Month + " NĂM " + dTuNgay.DateTime.Year + " ĐẾN THÁNG " + dDenNgay.DateTime.Month + " NĂM "+ dDenNgay.DateTime.Year +"";  /*"BÁO CÁO THEO DÕI THỰC HIỆN KẾ HOẠCH TUYỂN DỤNG";*/
                title.Font.Size = 13;
                title.RowHeight = 30;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.WrapText = true;
                title.Font.Bold = true;

                DONG++;


                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.RowHeight = 30;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Interior.Color = System.Drawing.Color.FromArgb(215, 227, 186);
                title.Font.Bold = true;
                title.WrapText = true;

                DONG = DONG + TDong + 1;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 3);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Merge();
                title.Value2 = "Tổng";
                title.Font.Bold = true;

                title = excelWorkSheet.Cells[DONG, 4];
                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG - TDong, 4) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG  -1, 4) + ")";
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 4, DONG, 8);
                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                title1.NumberFormat = "#,##0";


                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 10, "@", true, 1, 1, DONG, 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 10, "@", true, 1, 2, DONG, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 30, "@", true, 1, 3, DONG, 3);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 30, "#,##0", true, 1, 5, DONG, 7);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 30, "#,##0", true, 1, 8, DONG, 8);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 19, "MM/yyyy", true, 1, 9, DONG, 10);


                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - TDong - 1, 1,DONG , TCot);
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
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "InKhongThanhCong", Commons.Modules.TypeLanguage) + ": " + ex.Message);
            }
        }


        private bool Datain()
        {
            //năm sau lớn hơn năm đầu
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptTheoDoiHuongTCBHXH_DM",  Commons.Modules.UserName, Commons.Modules.TypeLanguage, LK_DON_VI.EditValue,LK_XI_NGHIEP.EditValue,LK_TO.EditValue, dTuNgay.EditValue,dDenNgay.EditValue));
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, this.Name);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private void ucBaoCaoHuongTroCapBHXH_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void lbDenNgay_Click(object sender, EventArgs e)
        {

        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
        }
    }
}
