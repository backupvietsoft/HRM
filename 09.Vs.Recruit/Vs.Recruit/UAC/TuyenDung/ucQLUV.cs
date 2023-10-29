using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.Spreadsheet;
using System.Threading;
using Spire.Xls;
using DataTable = System.Data.DataTable;
using Workbook = Spire.Xls.Workbook;
using Worksheet = Spire.Xls.Worksheet;
using System.Drawing;
using System.Collections.Generic;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Vs.Recruit
{
    public partial class ucQLUV : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        public LabelControl lblUV;
        public ucQLUV()
        {
            DevExpress.Utils.Paint.TextRendererHelper.UseScriptAnalyse = false;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }

        private void ucQLUV_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadCombo();
                cboTinhTrangUV.EditValue = 1;
                Commons.Modules.sLoad = "";
                LoadUNG_VIEN(-1);
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
                case "in":
                    {
                        if (grvUngVien.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                            return;
                        }
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbtUV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvUngVien), "");
                        string sSql = "SELECT A.MS_UV,UV.NGAY_NHAN_HO_SO,A.HO_TEN,A.NGAY_SINH,DATEDIFF(YEAR,A.NGAY_SINH,GETDATE()) AS TUOI,A.GIOI_TINH,A.VI_TRI_TD_1,A.VI_TRI_TD_2,A.TEN_VT_PH,A.DT_DI_DONG,B.DT_NGUOI_THAN,UV.QUAN_HE,A.THON_XOM,A.TEN_PX,A.TEN_QUAN,A.TEN_TP,A.DIA_CHI_THUONG_TRU,NT.TEN_NTD,(SELECT TEN_LCV FROM dbo.LOAI_CONG_VIEC WHERE ID_LCV = B.ID_LCV) AS VT_TRUNG_TUYEN, A.TEN_TAY_NGHE,(SELECT TEN_TO FROM dbo.[TO] WHERE ID_TO = B.ID_TO) AS TEN_TO ,CONVERT(NVARCHAR(50), A.NGAY_HEN_DI_LAM, 103) AS NGAY_HEN_DI_LAM, CONVERT(NVARCHAR(50), A.NGAY_NHAN_VIEC, 103) AS NGAY_NHAN_VIEC, UV.GHI_CHU FROM  " + "sbtUV" + Commons.Modules.iIDUser + " A INNER JOIN dbo.UNG_VIEN UV ON UV.ID_UV = A.ID_UV LEFT JOIN dbo.NGUON_TUYEN_DUNG NT ON NT.ID_NTD = UV.ID_NTD LEFT JOIN dbo.CONG_NHAN B ON B.ID_UV = A.ID_UV";
                        DataTable dt = new DataTable();
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, this.Name);
                        Commons.Modules.ObjSystems.XoaTable("sbtUV" + Commons.Modules.iIDUser);
                        InUngVien();
                        break;
                    }
                case "them":
                    {
                        ucCTQLUV dl = new ucCTQLUV(-1);
                        navigationFrame1.SelectedPage.Visible = false;
                        PageDetails.Controls.Add(dl);
                        dl.Dock = DockStyle.Fill;
                        dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                        Thread thread = new Thread(delegate ()
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate
                                {
                                    navigationFrame1.SelectedPage = PageDetails;
                                }));
                            }
                        }, 100);
                        thread.Start();
                        accorMenuleft.Visible = false;
                        break;
                    }
                case "sua":
                    {
                        if (grvUngVien.RowCount == 0)
                        {
                            ucCTQLUV dl = new ucCTQLUV(-1);
                            navigationFrame1.SelectedPage.Visible = false;
                            PageDetails.Controls.Add(dl);
                            dl.Dock = DockStyle.Fill;
                            dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                            Thread thread = new Thread(delegate ()
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        navigationFrame1.SelectedPage = PageDetails;
                                    }));
                                }
                            }, 100);
                            thread.Start();
                            accorMenuleft.Visible = false;
                        }
                        else
                        {
                            try
                            {
                                lblUV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                lblUV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                                lblUV.Text = grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["MS_UV"]).ToString() + " - " + grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["HO_TEN"]).ToString();
                            }
                            catch
                            {
                            }
                            ucCTQLUV dl = new ucCTQLUV(Convert.ToInt64(grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["ID_UV"])));
                            navigationFrame1.SelectedPage.Visible = false;
                            PageDetails.Controls.Add(dl);
                            dl.Dock = DockStyle.Fill;
                            dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                            Thread thread = new Thread(delegate ()
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        navigationFrame1.SelectedPage = PageDetails;
                                    }));
                                }
                            }, 100);
                            thread.Start();
                            accorMenuleft.Visible = false;
                        }
                        break;
                    }
                case "xoa":
                    {
                        if (grvUngVien.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DeleteData())
                        {
                            LoadUNG_VIEN(-1);
                        }
                        break;
                    }
                case "export":
                    {
                        string sPath = "";
                        sPath = Commons.Modules.MExcel.SaveFiles("Excel Files (*.xlsx;)|*.xlsx;|" + "All Files (*.*)|*.*");
                        if (sPath == "") return;
                        ExportUngVien(sPath);
                        break;
                    }
                case "import":
                    {
                        frmImportUngVien frm = new frmImportUngVien();
                        frm.ShowDialog();
                        LoadUNG_VIEN(-1);
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

        private void InUngVien()
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

                DONG = Commons.Modules.MExcel.TaoTTChung(excelWorkSheet, 1, 2, 1, 10, 0, 0);

                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 3, DONG);

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 10);
                title.Merge(true);
                title.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "BCDanhSachUngTuyenCongNhan");  /*"BÁO CÁO THEO DÕI THỰC HIỆN KẾ HOẠCH TUYỂN DỤNG";*/
                title.Font.Size = 16;
                title.Font.Name = "Times New Roman";
                title.RowHeight = 36;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 5);
                title.Merge(true);
                title.Value2 = lblTinhTrangUV.Text.Trim() + " :" + cboTinhTrangUV.Text.Trim();
                title.Font.Name = "Times New Roman";
                title.Font.Bold = true;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 6, DONG, 10);
                title.Merge(true);
                title.Value2 = lblLoaiCNV.Text.Trim() + " :" + cboLoaiCNV.Text.Trim();
                title.Font.Name = "Times New Roman";
                title.Font.Bold = true;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                DONG++;

                if (cboLocTheo.Text != "")
                {
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 5);
                    title.Merge(true);
                    title.Value2 = lblLocTheo.Text.Trim() + " :" + cboLocTheo.Text.Trim();
                    title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 6, DONG, 10);
                    title.Merge(true);
                    title.Value2 = lblTuNgay.Text.Trim() + " :" + datTuNgay.Text.Trim() + " " + lblDenNgay.Text.Trim() + " :" + datDenNgay.Text.Trim();
                    title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    title.Font.Bold = false;
                }

                DONG++;

                //định dạng style
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, TCot);
                title.RowHeight = 22;
                title.Font.Bold = true;
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG + TDong, TCot);
                title.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                title.Borders.Weight = Excel.XlBorderWeight.xlThin;
                excelWorkSheet.Columns.AutoFit();

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


        private void ExportUngVien(string sPath)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                string SQL = "SELECT TOP 0 MS_UV AS  N'Mã số',NGAY_NHAN_HO_SO AS N'ngày nhận CV',HO + ' '+ TEN AS N'Họ tên',PHAI AS N'Giới tính',NGAY_SINH AS N'Ngày sinh',NOI_SINH AS N'Nơi sinh',SO_CMND AS N'CMND',NGAY_CAP AS N'Ngày cấp',NOI_CAP AS N'Nơi cấp',CONVERT(NVARCHAR(250), '') N'Trình độ học vấn',DT_DI_DONG AS N'Điện thoại',EMAIL AS N'Email',NGUOI_LIEN_HE AS N'Người liên hệ',QUAN_HE AS N'Quan hệ',DT_NGUOI_LIEN_HE AS N'ĐT Người liên hệ',CONVERT(NVARCHAR(250), ID_TP) AS N'Tỉnh',CONVERT(NVARCHAR(250), ID_QUAN) AS N'Huyện',CONVERT(NVARCHAR(250), ID_PX) AS N'Xã',THON_XOM AS N'Đường/Thôn/Xóm',DIA_CHI_THUONG_TRU AS N'Địa chỉ',CONVERT(NVARCHAR(250), '') AS N'Nguồn tuyển',CONVERT(NVARCHAR(250), ID_CN) AS N'Người giới thiệu',CONVERT(NVARCHAR(250), TAY_NGHE) AS N'tay nghề',CONVERT(NVARCHAR(250), VI_TRI_TD_1) AS N'Vị trí tuyển 1',CONVERT(NVARCHAR(250), VI_TRI_TD_2) AS N'Vị trí tuyển 2',CONVERT(NVARCHAR(250), ID_VI_TRI_PHU_HOP) AS N'Vị trí phù hợp',CONG_DOAN_CHU_YEU AS N'Công đoạn chủ yếu',GHI_CHU AS N'Ghi Chú' FROM dbo.UNG_VIEN";

                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

                //export datatable to excel

                ExcelPackage pck = new ExcelPackage();

                var sheet1 = pck.Workbook.Worksheets.Add("01 - Danh sách ứng viên");
                var sheet2 = pck.Workbook.Worksheets.Add("02-Bằng cấp");
                var sheet3 = pck.Workbook.Worksheets.Add("03-Kinh nghiệm làm việc");
                var sheet4 = pck.Workbook.Worksheets.Add("Danh sách Tỉnh");
                var sheet5 = pck.Workbook.Worksheets.Add("Danh sách Huyện");
                var sheet6 = pck.Workbook.Worksheets.Add("Danh sách Xã");
                var sheet7 = pck.Workbook.Worksheets.Add("Danh sách Loại Công Việc");

                sheet1.DefaultColWidth = 20;
                sheet1.Cells[1, 1].LoadFromDataTable(dtTmp, true);
                sheet1.Cells[2, 1].Value = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();

                sheet1.Cells[1, 1, 1, 28].Style.WrapText = true;
                sheet1.Cells[1, 1, 1, 28].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet1.Cells[1, 1, 1, 28].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet1.Cells[1, 1, 1, 28].Style.Font.Bold = true;

                sheet1.Cells[1, 1].Style.Font.Color.SetColor(Color.Red);
                sheet1.Cells[1, 3].Style.Font.Color.SetColor(Color.Red);
                sheet1.Cells[1, 5].Style.Font.Color.SetColor(Color.Red);
                sheet1.Cells[1, 26].Style.Font.Color.SetColor(Color.Red);

                //sheet1.Cells[1, 1].Comment.RichText.Add("Mã ứng viên sẽ được đặt theo cấu trúc MUV-000001 trong đó(MUV-: cố định,còn 000001 sẽ được tăng thêm 1 khi có một ứng viên mới).");

                sheet1.Cells[1, 1].AddComment("Mã ứng viên sẽ được đặt theo cấu trúc MUV-000001 trong đó(MUV-: cố định,còn 000001 sẽ được tăng thêm 1 khi có một ứng viên mới).", "REF");


                sheet4.Cells[1, 1, 1, 1].Style.WrapText = true;
                sheet4.Cells[1, 1, 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet4.Cells[1, 1, 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet4.Cells[1, 1, 1, 1].Style.Font.Bold = true;
                sheet4.Cells[1, 1, 1, 1].Value = "Tỉnh";
                sheet4.Column(1).Width = 50;
                sheet4.Cells[2, 1].LoadFromCollection(Commons.Modules.ObjSystems.DataThanhPho(-1, false).AsEnumerable().Select(x => x.Field<string>("TEN_TP")).ToArray());


                sheet5.Cells[1, 1, 1, 2].Style.WrapText = true;
                sheet5.Cells[1, 1, 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet5.Cells[1, 1, 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet5.Cells[1, 1, 1, 2].Style.Font.Bold = true;
                sheet5.Column(1).Width = 50;
                sheet5.Column(2).Width = 50;
                DataTable tbHuyen = new DataTable();
                tbHuyen.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT B.TEN_TP AS N'Tỉnh',TEN_QUAN AS N'Huyện' FROM dbo.QUAN A INNER JOIN  dbo.THANH_PHO B ON B.ID_TP = A.ID_TP ORDER BY B.TEN_TP,A.TEN_QUAN"));
                sheet5.Cells[1, 1].LoadFromDataTable(tbHuyen, true);

                sheet6.Cells[1, 1, 1, 2].Style.WrapText = true;
                sheet6.Cells[1, 1, 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet6.Cells[1, 1, 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet6.Cells[1, 1, 1, 2].Style.Font.Bold = true;
                sheet6.Column(1).Width = 50;
                sheet6.Column(2).Width = 50;

                DataTable tbXa = new DataTable();
                tbXa.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_QUAN AS N'Huyện',A.TEN_PX N'Xã' FROM dbo.PHUONG_XA A INNER JOIN  dbo.QUAN B ON B.ID_QUAN = A.ID_QUAN ORDER BY B.TEN_QUAN,A.TEN_PX"));
                sheet6.Cells[1, 1].LoadFromDataTable(tbXa, true);


                //2 giới tính
                sheet7.Cells[1, 1, 1, 1].Style.WrapText = true;
                sheet7.Cells[1, 1, 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet7.Cells[1, 1, 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet7.Cells[1, 1, 1, 1].Style.Font.Bold = true;
                sheet7.Cells[1, 1, 1, 1].Value = "Tên công việc";
                sheet7.Column(1).Width = 50;
                sheet7.Cells[2, 1].LoadFromCollection(Commons.Modules.ObjSystems.DataLoaiCV(false).AsEnumerable().Select(x => x.Field<string>("TEN_LCV")).ToArray());

                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 4, 50, 4, "", new string[] { "Nam", "Nữ" });
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 10, 50, 10, "", Commons.Modules.ObjSystems.DataTDVH(-1, false).AsEnumerable().Select(x => x.Field<string>("TEN_TDVH")).ToArray());
                //Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 16, 50, 16, "", Commons.Modules.ObjSystems.DataThanhPho(-1, false).AsEnumerable().Select(x => x.Field<string>("TEN_TP")).ToArray());
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 21, 50, 21, "", Commons.Modules.ObjSystems.DataNguonTD(false).AsEnumerable().Select(x => x.Field<string>("TEN_NTD")).ToArray());
                //Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 22, 50, 22, "", Commons.Modules.ObjSystems.DataCongNhan(false).AsEnumerable().Select(x => x.Field<string>("TEN_CN")).ToArray());
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 23, 50, 23, "", Commons.Modules.ObjSystems.DataTayNghe(false).AsEnumerable().Select(x => x.Field<string>("TEN_TAY_NGHE")).ToArray());

                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 16, 50, 16, "'Danh sách Tỉnh'!$A$2:$A$" + Commons.Modules.ObjSystems.DataThanhPho(-1, false).Rows.Count.ToString() + "", null);
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 17, 50, 17, "'Danh sách Huyện'!$B$2:$B$" + Commons.Modules.ObjSystems.DataQuan(-1, false).Rows.Count.ToString() + "", null);
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 18, 50, 18, "'Danh sách Xã'!$B$2:$B$" + Commons.Modules.ObjSystems.DataPhuongXa(-1, false).Rows.Count.ToString() + "", null);
                Commons.Modules.MExcel.AddExcelDataValidationList(sheet1, 2, 26, 50, 26, "'Danh sách Loại Công Việc'!$A$2:$A$" + Commons.Modules.ObjSystems.DataLoaiCV(false).Rows.Count.ToString() + "", null);

                //9 trình độ văn hóa
                //sheet1.Cells[2, 9, 50, 9].DataValidation.va = Commons.Modules.ObjSystems.DataTDVH(-1, false).AsEnumerable().Select(x => x.Field<string>("TEN_TDVH")).ToArray();
                //15 thành phố
                //sheet1.Range[2, 15, 50, 15].DataValidation.Values = Commons.Modules.ObjSystems.DataThanhPho(-1, false).AsEnumerable().Select(x => x.Field<string>("TEN_TP")).ToArray();
                //20 nguồn tuyển  
                //sheet1.Cells[2, 20, 50, 20].DataValidation.Values = Commons.Modules.ObjSystems.DataNguonTD(false).AsEnumerable().Select(x => x.Field<string>("TEN_NTD")).ToArray();
                //21 người giới thiệu
                //sheet1.Range[2, 21, 50, 21].DataValidation.Values = Commons.Modules.ObjSystems.DataCongNhan(false).AsEnumerable().Select(x => x.Field<string>("TEN_CN")).ToArray();
                //22 tay nghề
                //sheet1.Range[2, 22, 50, 22].DataValidation.Values = Commons.Modules.ObjSystems.DataTayNghe(false).AsEnumerable().Select(x => x.Field<string>("TEN_TAY_NGHE")).ToArray();
                //25  vị trí công việc
                //sheet1.Range[2, 25, 50, 25].DataValidation.Values = Commons.Modules.ObjSystems.DataLoaiCV(false).AsEnumerable().Select(x => x.Field<string>("TEN_LCV")).ToArray();

                //sheet1.Range[2, 9, 50, 9].DataValidation.IsSuppressDropDownArrow = false;
                //sheet1.Range[2, 3, 50, 3].DataValidation.IsSuppressDropDownArrow = false;
                //sheet1.Range[2, 20, 50, 20].DataValidation.IsSuppressDropDownArrow = false;
                //sheet1.Range[2, 22, 50, 22].DataValidation.IsSuppressDropDownArrow = false;

                //InSheet(book, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), "Danh sách loại công việc");
                ////sheet1.Range[2, 25, 50, 25].DataValidation.DataRange = book.Worksheets["Danh sách loại công việc"].Range["B2:B15"];

                //sheet1.InsertDataTable(dtTmp, true, 1, 1);

                //sheet1.FreezePanes(2, 4);
                ////Tên trường Từ năm	Đến năm	Xếp loại

                //Worksheet sheet2 = book.Worksheets[1];
                //sheet2.Name = "02-Bằng cấp";
                sheet2.DefaultColWidth = 20;

                sheet2.Cells[1, 1].Value = "Mã số";
                sheet2.Cells[1, 2].Value = "Chuyên ngành";
                sheet2.Cells[1, 3].Value = "Tên trường";
                sheet2.Cells[1, 4].Value = "Từ năm";
                sheet2.Cells[1, 5].Value = "Đến năm";
                sheet2.Cells[1, 6].Value = "Xếp loại";

                sheet2.Cells[1, 6].AddComment(Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false)), "REF");

                Commons.Modules.MExcel.AddExcelDataValidationList(sheet2, 2, 6, 50, 6, "", Commons.Modules.ObjSystems.DataXepLoai(false).AsEnumerable().Select(x => x.Field<string>("TEN_XL")).ToArray());



                sheet2.Cells[1, 1, 1, 6].Style.WrapText = true;
                sheet2.Cells[1, 1, 1, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet2.Cells[1, 1, 1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet2.Cells[1, 1, 1, 6].Style.Font.Bold = true;

                sheet2.Cells[1, 1].Style.Font.Color.SetColor(Color.Red);


                sheet3.DefaultColWidth = 20;

                sheet3.Cells[1, 1].Value = "Mã số";
                sheet3.Cells[1, 2].Value = "Tên công ty";
                sheet3.Cells[1, 3].Value = "Chức vụ";
                sheet3.Cells[1, 4].Value = "Mức lương";
                sheet3.Cells[1, 5].Value = "Từ năm";
                sheet3.Cells[1, 6].Value = "Đến năm";
                sheet3.Cells[1, 7].Value = "Số năm kinh nghiệm";
                sheet3.Cells[1, 8].Value = "Lý do nghĩ";
                sheet3.Cells[1, 1].Style.Font.Color.SetColor(Color.Red);

                sheet3.Cells[1, 1, 1, 8].Style.WrapText = true;
                sheet3.Cells[1, 1, 1, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                sheet3.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet3.Cells[1, 1, 1, 8].Style.Font.Bold = true;



                var fi = new System.IO.FileInfo(sPath);
                if (fi.Exists)
                    fi.Delete();
                pck.SaveAs(fi);
                System.Diagnostics.Process.Start(fi.FullName);


                //book.SaveToFile(sPath);
                //System.Diagnostics.Process.Start(sPath);
            }
            catch
            {
            }
        }


        private void LoadCombo()
        {
            try
            {
                datTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
                datDenNgay.DateTime = DateTime.Now.Date.AddMonths(1).AddDays(-DateTime.Now.Date.Day);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrangUV, Commons.Modules.ObjSystems.DataTinhTrangUV(true), "ID_TT_UV", "TEN_TT_UV", "TEN_TT_UV");
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboLoaiCNV, Commons.Modules.ObjSystems.DataCongNhanVien(true), "ID_CV", "TEN_CV", "TEN_CV");
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboLocTheo, Commons.Modules.ObjSystems.DataCongTheoNgayUV(), "MA_DK", "TEN_DK", "TEN_DK");
            }
            catch
            {
            }
        }

        private void LoadUNG_VIEN(Int64 iIdUV)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboTinhTrangUV.EditValue, cboLoaiCNV.EditValue, cboLocTheo.EditValue, datTuNgay.EditValue, datDenNgay.EditValue));
                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_UV"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdUngVien, grvUngVien, dtTmp, false, true, false, true, true, this.Name);
                grvUngVien.Columns["ID_UV"].Visible = false;
                if (iIdUV != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdUV));
                    grvUngVien.FocusedRowHandle = grvUngVien.GetRowHandle(index);
                }
                grvUngVien.Columns["MS_UV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvUngVien.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvUngVien.Columns["NGAY_SINH"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvUngVien.Columns["GIOI_TINH"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch { }
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            try { lblUV.Text = lblUV.Tag.ToString(); } catch { }
            navigationFrame1.SelectedPage = pageList;
            PageDetails.Controls[0].Visible = false;
            PageDetails.Controls[0].Dispose();
            accorMenuleft.Visible = true;
            LoadUNG_VIEN(Commons.Modules.iUngVien);
        }
        private bool DeleteData()
        {
            //kiểm tra ứng viên
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.UNG_VIEN_TUYEN_DUNG WHERE ID_UV = " + grvUngVien.GetFocusedRowCellValue("ID_UV") + " ")) > 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteUngVien"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
            //xóa
            try
            {

                Int64 iID = Convert.ToInt64(grvUngVien.GetFocusedRowCellValue("ID_UV"));
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_BANG_CAP WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_KINH_NGHIEM WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_THONG_TIN_KHAC WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN WHERE ID_UV = " + iID + "");
                return true;
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }


        private void cboDA_TUYEN_DUNG_EditValueChanged(object sender, EventArgs e)
        {
            LoadUNG_VIEN(-1);
        }

        private void grdUngVien_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (grvUngVien.RowCount == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DeleteData())
                {
                    LoadUNG_VIEN(-1);
                }
            }
        }

        private void grvUngVien_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = grvUngVien.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                if (grvUngVien.RowCount == 0)
                {
                    ucCTQLUV dl = new ucCTQLUV(-1);
                    navigationFrame1.SelectedPage.Visible = false;
                    PageDetails.Controls.Add(dl);
                    dl.Dock = DockStyle.Fill;
                    dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                    Thread thread = new Thread(delegate ()
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                navigationFrame1.SelectedPage = PageDetails;
                            }));
                        }
                    }, 100);
                    thread.Start();
                    accorMenuleft.Visible = false;
                }
                else
                {
                    try
                    {
                        lblUV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblUV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                        lblUV.Text = grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["MS_UV"]).ToString() + " - " + grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["HO_TEN"]).ToString();
                    }
                    catch
                    {
                    }
                    ucCTQLUV dl = new ucCTQLUV(Convert.ToInt64(grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["ID_UV"])));
                    navigationFrame1.SelectedPage.Visible = false;
                    PageDetails.Controls.Add(dl);
                    dl.Dock = DockStyle.Fill;
                    dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                    Thread thread = new Thread(delegate ()
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                navigationFrame1.SelectedPage = PageDetails;
                            }));
                        }
                    }, 100);
                    thread.Start();
                    accorMenuleft.Visible = false;
                }
            }
        }

        private void cboLocTheo_EditValueChanged(object sender, EventArgs e)
        {
            if (cboLocTheo.EditValue.ToString() == "-1")
            {
                datTuNgay.Properties.ReadOnly = true;
                datDenNgay.Properties.ReadOnly = true;
            }
            else
            {
                datTuNgay.Properties.ReadOnly = false;
                datDenNgay.Properties.ReadOnly = false;
            }
            LoadUNG_VIEN(-1);
        }
    }
}
