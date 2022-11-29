using System;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace Vs.Recruit
{
    public partial class frmInTinhHinhTD : DevExpress.XtraEditors.XtraForm
    {

        DataTable tbdonvi = new DataTable();
        public frmInTinhHinhTD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,windowsUIButton);
        }
        private void frmInTinhHinhTD_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(true), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLCV, Commons.Modules.ObjSystems.DataLoaiCV(true), "ID_LCV", "TEN_LCV", "TEN_LCV"); 
            datTuThang.EditValue = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            //DateTime.ParseExact("01/"+DateTime.Now.Month+"/"+ DateTime.Now.Year + "", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            datDenThang.DateTime = DateTime.Now.Date.AddMonths(1).AddDays(-DateTime.Now.Date.Day);

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
                            windowsUIButton.Focus();
                            DateTime dt =  datTuThang.DateTime;
                            if (Datain() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                                return;
                            }
                            tbdonvi = new DataTable();
                            tbdonvi.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_DV,TEN_DV FROM dbo.DON_VI WHERE (ID_DV =" + cboDV.EditValue + " OR -1 = " + cboDV.EditValue + ") ORDER BY ID_DV"));
                            InTinhHinhTD();
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
                //lấy đơn vị cần in theo phân quyền
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spBaoCaoTinhHinhTuyenDung", datTuThang.EditValue, datDenThang.EditValue,cboDV.EditValue,cboLCV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                //Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, false, this.Name);
                grdData.DataSource = null;
                grdData.DataSource = dt;
                grvData.PopulateColumns(); 
                grvData.Columns["ID_DV"].Visible = false;
                grvData.Columns[0].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colSTT"); /*"STT";*/
                grvData.Columns[1].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "colVTTD");  /*"VỊ TRÍ CẦN TUYỂN DỤNG";*/
         
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
            return array[1] +"/"+array[0];
        }
        private void InTinhHinhTD()
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

                DONG = Commons.Modules.MExcel.TaoTTChung(excelWorkSheet, 1, 2, 1, TCot - 1, 0, 0);

                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 5, DONG);


                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot - 1);
                title.Merge(true);
                title.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name,"BCTinhHinhTuyenDung"); 
                title.Font.Size = 16;
                title.RowHeight = 40;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;
                DONG++;
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot - 1);
                title.Merge(true);
                title.Value2 = "Từ tháng "+ datTuThang.DateTime.Month +"/"+ datTuThang.DateTime.Year + " đến tháng "+ datDenThang.DateTime.Month + "/"+ datDenThang.DateTime.Year + "";
                title.Font.Color = Color.Red;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;
                DONG++;
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, ItemForDON_VI.Text + " :" + cboDV.Text, DONG, 1, "@", 13, true, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter, true, DONG, TCot - 1, 17);
                DONG++;
                Commons.Modules.MExcel.DinhDang(excelWorkSheet, lblLoaiCV.Text + " :" + cboLCV.Text, DONG, 1, "@", 13, true, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter, true, DONG, TCot - 1, 17);
                DONG++;
                DONG++;
                for (int i = 3; i <= TCot -1; i++)
                {
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, i, DONG, i);
                    switch ((i - 2) % 7)
                    {
                        case 1:
                            {
                                title.Value2 = "Số Lượng KHLĐ tháng";
                                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, i, DONG - 1, i + 6);
                                title.Merge();
                                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                title.Font.Color = Color.Red;
                                title.Font.Bold = true;
                                title.Value2 = "Tháng " + getValueCell(excelWorkSheet, DONG, i + 1);
                                break;
                            }
                        case 2:
                            {
                                title.Value2 = "SL LĐ tháng trước";
                                break;
                            }
                        case 3:
                            {
                                title.Value2 = "SL cần tuyển theo KH";

                                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i, DONG + 1, i);
                                title.Value2 = "=IF(ISNUMBER("+ Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 2) + "),"+ Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 2) + ", 0)  - IF(ISNUMBER("+ Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 1) + "),"+ Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 1) + ", 0)";
                                //title.Value2 = "=" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 2) + "-" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i-1) + "";
                                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i, DONG + TDong, i);
                                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                                break;
                            }
                        case 4:
                            {
                                title.Value2 = "SL cần tuyển theo PYC";
                                break;
                            }
                        case 5:
                            {
                                title.Value2 = "SL YCTD được duyệt";
                                break;
                            }
                        case 6:
                            {
                                title.Value2 = "SL tuyển đã đi làm";
                                break;
                            }
                        case 0:
                            {
                                title.Value2 = "Số LĐ hiện tại trong tháng";

                                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i, DONG + 1, i);
                                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 5) + "," + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, i - 1) + ")";
                                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG + 1, i, DONG + TDong, i);
                                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                                break;
                            }
                        default: break;
                    }
                }

            

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot - 1);
                title.Interior.Color = System.Drawing.Color.FromArgb(180, 198, 231);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.WrapText = true;
                title.Font.Bold = true;
                title.RowHeight = 65;

                DONG++;
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 10, "@", true, DONG, 1, DONG + TDong, 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 55, "@", true, DONG, 2, DONG + TDong, 2);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 12, "#", true, DONG, 3, DONG + TDong, TCot);

                for (int i = 0; i < tbdonvi.Rows.Count; i++)
                
                {
                    Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, DONG);
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot - 1);
                    title.Interior.Color = System.Drawing.Color.FromArgb(0, 176, 80);

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 2, DONG, TCot - 1);
                    title.Merge();
                    title.Value2 = tbdonvi.Rows[i][1].ToString();
                    title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    title.RowHeight = 17;


                    DONG = DONG + 1 + Commons.Modules.ObjSystems.ConvertDatatable(grdData).AsEnumerable().Count(x => x["ID_DV"].Equals(tbdonvi.Rows[i][0]));
                }

                excelWorkSheet.Application.ActiveWindow.SplitRow = 9;
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
    }
}
