using DevExpress.DataAccess.Excel;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Spreadsheet;

namespace Vs.HRM
{
    public partial class frmImportNhanSu : DevExpress.XtraEditors.XtraForm
    {
        string fileName = "";
        Point ptChung;
        string ChuoiKT = "";
        DataTable _table = new DataTable();
        DataTable dtemp;
        public frmImportNhanSu()
        {
            InitializeComponent();
        }
        private void btnFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //try
            //{
            //    OpenFileDialog oFile = new OpenFileDialog();
            //    oFile.Filter = "All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*";
            //    if (oFile.ShowDialog() != DialogResult.OK) return;

            //    fileName = oFile.FileName;
            //    btnFile.Text = fileName;
            //    if (!System.IO.File.Exists(fileName)) return;

            //    if (Commons.Modules.MExcel.MGetSheetNames(fileName, cboChonSheet))
            //    {
            //        cboChonSheet_EditValueChanged(null, null);
            //    }
            //    else
            //    {
            //        grdData.DataSource = null;
            //        cboChonSheet.Properties.DataSource = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}
            string sPath = "";
            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");
            if (sPath == "") return;
            btnFile.Text = sPath;
            try
            {
                cboChonSheet.Properties.DataSource = null;
                Workbook workbook = new Workbook();

                string ext = System.IO.Path.GetExtension(sPath);
                if (ext.ToLower() == ".xlsx")
                    workbook.LoadDocument(btnFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                else
                    workbook.LoadDocument(btnFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xls);
                List<string> wSheet = new List<string>();
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    wSheet.Add(workbook.Worksheets[i].Name.ToString());
                }
                cboChonSheet.Properties.DataSource = wSheet;
                //cboChonSheet.Properties.Items.AddRange(wSheet);
                Commons.Modules.sLoad = "0Load";
                cboChonSheet.EditValue = wSheet[0].ToString();
                Commons.Modules.sLoad = "";
                cboChonSheet_EditValueChanged(null, null);
                ////grdChung.DataSource = dtemp;

                ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void cboChonSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            //try
            //{
            //    if (string.IsNullOrEmpty(fileName)) return;
            //    if (Commons.Modules.sLoad == "0Load") return;
            //    if (string.IsNullOrEmpty(btnFile.Text)) return;
            //    this.grdData.DataSource = null;
            //    grvData.Columns.Clear();
            //    if (cboChonSheet.EditValue.ToString() == "-1")
            //        return;

            //    this.Cursor = Cursors.WaitCursor;
            //    var FileExt = Path.GetExtension(btnFile.Text);
            //    _table = new DataTable();
            //    if (FileExt.ToLower() == ".xls")
            //        _table = Commons.Modules.MExcel.MGetData2xls(btnFile.Text, cboChonSheet.EditValue.ToString());
            //    else if (FileExt.ToLower() == ".xlsx")
            //        _table = Commons.Modules.MExcel.MGetData2xlsx(btnFile.Text, cboChonSheet.EditValue.ToString());



            //    dtemp = new DataTable();
            //    dtemp = _table;
            //    this.grdData.DataSource = null;
            //    grvData.Columns.Clear();
            //    if (_table != null)
            //    {
            //        dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
            //        try
            //        {
            //            dtemp.DefaultView.Sort = "[" + dtemp.Columns[0].ColumnName.ToString() + "]";
            //        }
            //        catch { }

            //        if (dtemp.Columns.Count <= 13)
            //            Commons.Modules.ObjSystems.MLoadXtraGridIP(grdData, grvData, dtemp, true, true, false, false);
            //        else
            //            Commons.Modules.ObjSystems.MLoadXtraGridIP(grdData, grvData, dtemp, true, true, false, true);

            //        grvData.BestFitColumns();

            //        btnFile.Text = fileName;
            //        try
            //        {
            //            groDLImport.Text = " Total : " + grvData.RowCount.ToString() + " row";
            //        }
            //        catch { }
            //    }
            //    this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //}

            DataTable dt = new DataTable();
            var source = new ExcelDataSource();
            source.FileName = btnFile.Text;
            var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text);
            source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
            source.Fill();
            dt = new DataTable();
            dt = ToDataTable(source);
            dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
            grvData.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            grvData.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            grvData.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            grvData.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            grdData.DataSource = dt;
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            //Commons.Modules.ObjSystems.ShowWaitForm(this);
            switch (btn.Tag.ToString())
            {
                case "export":
                    {
                        try
                        {
                            string sPath = "";
                            sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                            if (sPath == "") return;
                            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                            excelApplication.DisplayAlerts = true;

                            excelApplication.Visible = true;


                            System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                            Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                            object misValue = System.Reflection.Missing.Value;
                            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                            excelWorkbook.SaveAs(sPath);

                            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];


                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spImportNhanSu", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                            //dt = ((DataTable)grdData.DataSource).Copy();
                            string lastColumn = CharacterIncrement(dt.Columns.Count - 1);
                            string fontName = "Time News Roman";
                            int fontSizeTieuDe = 13;
                            int fontSizeNoiDung = 9;

                            dt.DefaultView.RowFilter = "";
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                //dt.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt.Columns[i].ColumnName.ToString()); ;
                                dt.Columns[i].ColumnName = Commons.Modules.ObjLanguages.GetLanguage(this.Name, dt.Columns[i].ColumnName.ToString());
                            }
                            Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt.Rows.Count + 1, dt.Columns.Count]];
                            Ranges1.ColumnWidth = 20;
                            Ranges1.Font.Name = fontName;
                            Ranges1.Font.Size = fontSizeNoiDung;
                            Ranges1.Range["A1", "" + lastColumn + "1"].Font.Bold = true;
                            //Ranges1.Range["F2", "F6"].NumberFormat = "dd/MM/yyy";
                            Ranges1.Range["A1", "" + lastColumn + "1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            Ranges1.Range["A1", "" + lastColumn + "1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            MExportExcel(dt, excelWorkSheet, Ranges1);
                            excelApplication.Visible = true;
                            excelWorkbook.Save();
                        }
                        catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                        break;
                    }
                case "import":
                    {
                        grvData.PostEditor();
                        grvData.UpdateCurrentRow();
                        Commons.Modules.ObjSystems.MChooseGrid(false, "XOA", grvData);
                        DataTable dtSource = Commons.Modules.ObjSystems.ConvertDatatable(grdData);
                        if (cboChonSheet.Text == "" || dtSource == null || dtSource.Rows.Count <= 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        grvData.Columns.View.ClearColumnErrors();

                        ImportUngVien(dtSource);

                        break;
                    }
                case "xoa":
                    {
                        try
                        {
                            DataTable dtTmp = new DataTable();
                            dtTmp = (DataTable)grdData.DataSource;

                            if (dtTmp == null || dtTmp.Select("XOA = 1").Count() == 0) return;

                            DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.No) return;

                            dtTmp.AcceptChanges();
                            foreach (DataRow dr in dtTmp.Rows)
                            {
                                if (dr["XOA"].ToString() == "True")
                                {
                                    dr.Delete();
                                }
                            }
                            dtTmp.AcceptChanges();
                        }
                        catch
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    }
                default: break;
            }
        }
        #region import ứng viên
        private void ImportUngVien(DataTable dtSource)
        {
            this.Cursor = Cursors.WaitCursor;
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Mã số nhân viên
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "CONG_NHAN", "MS_CN", this.Name))
                    {
                        errorCount++;
                    }
                }

                // Mã số thẻ CC
                col = 1;
                string sMS_The_CC = dr[grvData.Columns[col].FieldName.ToString()].ToString().Trim();

                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTrungDL(grvData, dtSource, dr, col, sMS_The_CC, "CONG_NHAN", "MS_THE_CC", this.Name))
                    {
                        errorCount++;
                    }
                }

                col = 2;
                //Họ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 50, this.Name))
                {
                    errorCount++;
                }
                col = 3;
                //Tên 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 20, this.Name))
                {
                    errorCount++;
                }

                // Quốc gia
                col = 4;
                string sQuocGia = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuocGia, "QUOC_GIA", "TEN_QG", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ngày sinh   
                col = 5;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                {
                    errorCount++;
                }

                col = 6;
                //Năm sinh
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 5, this.Name))
                {
                    errorCount++;
                }

                col = 7;
                //Giới tính
                string sGioiTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sGioiTinh, false))
                {
                    errorCount++;
                }

                //Đơn vị   
                col = 8;
                string sDonVi = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sDonVi, "DON_VI", "TEN_DV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Xí nghiệp   
                col = 9;
                string sXiNghiep = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXiNghiep, "XI_NGHIEP", "TEN_XN", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Tổ  
                col = 10;
                string sTo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTo, "[TO]", "TEN_TO", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Chức vụ  
                col = 11;
                string sChucVu = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sChucVu, "CHUC_VU", "TEN_CV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Loại công việc
                col = 12;
                string sLoaiCongViec = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLoaiCongViec, "LOAI_CONG_VIEC", "TEN_LCV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Loại hợp đồng lao động
                col = 13;
                string sLoaiHDLD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLoaiHDLD, "LOAI_HDLD", "TEN_LHDLD", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ngày học việc
                col = 14;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày thử việc
                col = 15;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày vào làm
                col = 16;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                {
                    errorCount++;
                }

                //Vào làm lại
                col = 17;
                string sVaoLamLai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sVaoLamLai, false))
                {
                    errorCount++;
                }

                //Tình trạng hợp đồng
                col = 18;
                string sTinhTrangHD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTinhTrangHD, "TINH_TRANG_HD", "TEN_TT_HD", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Tình trạng nhân sự
                col = 19;
                string sTinhTrangHT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTinhTrangHT, "TINH_TRANG_HT", "TEN_TT_HT", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Hình thức tuyển
                col = 20;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Phép cộng thêm
                col = 21;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Phép chế độ
                col = 22;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tham gia BHXH
                col = 23;
                string sThamGiaBHXH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sThamGiaBHXH, false))
                {
                    errorCount++;
                }

                //LD Tỉnh
                col = 24;
                string sLDTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sLDTinh, false))
                {
                    errorCount++;
                }

                //LD công nhật
                col = 25;
                string sLDCongNhat = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sLDCongNhat, false))
                {
                    errorCount++;
                }

                //Trực tiếp sản xuất
                col = 26;
                string sTTSanXuat = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sTTSanXuat, false))
                {
                    errorCount++;
                }

                //Ngày nghỉ việc
                col = 27;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Lý do thôi việc
                col = 28;
                string sLyDoThoiViec = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLyDoThoiViec, "LY_DO_THOI_VIEC", "TEN_LD_TV", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Ghi chú
                col = 29;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Dân tộc
                col = 30;
                string sTenDanToc = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTenDanToc, "DAN_TOC", "TEN_DT", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Tôn giáo
                col = 31;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Nơi sinh
                col = 32;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Nguyên quán
                col = 33;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Số CMND
                col = 34;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngày cấp
                col = 35;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Nơi cấp
                col = 36;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tình trạng hôn nhân
                col = 37;
                string sTTHonNhan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTTHonNhan, "TT_HON_NHAN", "TEN_TT_HN", false, this.Name))
                    {
                        errorCount++;
                    }
                }

                //Mã thẻ ATM
                col = 38;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Số tài khoản
                col = 39;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Mã số thuế
                col = 40;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Tên không dấu
                col = 41;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Lao động nước ngoài
                col = 42;
                string sLDNuocNgoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sLDNuocNgoai, false))
                {
                    errorCount++;
                }

                //ĐT di động
                col = 43;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //ĐT nhà
                col = 44;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //ĐT người thân
                col = 45;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Email
                col = 46;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Dia chi thuong tru
                col = 47;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Thành phố
                col = 48;
                string sThanhPho = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPho, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }

                //Quận
                col = 49;
                string sQuan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuan, "QUAN", "TEN_QUAN", false, this.Name))
                {
                    errorCount++;
                }

                //phường xã
                col = 50;
                string sPhuongXa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sPhuongXa, "PHUONG_XA", "TEN_PX", false, this.Name))
                {
                    errorCount++;
                }

                //Thôn xóm
                col = 51;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Địa chỉ tạm trú
                col = 52;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Thành phố tạm trú
                col = 53;
                string sThanhPhoTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPhoTamTru, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }

                //Quận tạm trú
                col = 54;
                string sQuanTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuanTamTru, "QUAN", "TEN_QUAN", false, this.Name))
                {
                    errorCount++;
                }

                //phường xã tạm trú
                col = 55;
                string sPhuongXaTamTru = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sPhuongXaTamTru, "PHUONG_XA", "TEN_PX", false, this.Name))
                {
                    errorCount++;
                }

                //Thôn xóm tạm trú
                col = 56;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //số BHXH
                col = 57;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngày đóng BHXH
                col = 58;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày đóng BHXH đầu tiên
                col = 59;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày chấm dứt BHXH
                col = 60;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày thu hồi BHXH
                col = 61;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Trình độ văn hóa
                col = 62;

                string sLoaiTrinhDo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sLoaiTrinhDo, "LOAI_TRINH_DO", "TEN_LTD", false, this.Name))
                {
                    errorCount++;
                }

                //loại trình độ
                col = 63;
                string sTDVanHoa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTDVanHoa, "TRINH_DO_VAN_HOA", "TEN_TDVH", false, this.Name))
                {
                    errorCount++;
                }


                //Chuyên môn
                col = 64;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngoại ngữ
                col = 65;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Cấp giấy phép
                col = 66;
                string sCapGiayPhep = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sCapGiayPhep, "CAP_GIAY_PHEP", "TEN_CAP_GIAY_PHEP", false, this.Name))
                {
                    errorCount++;
                }

                //Số giấy phép
                col = 67;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Ngày cấp giấy phép
                col = 68;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngày hết hạn giấy phép
                col = 69;
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }

                //Ngân hàng
                col = 70;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Chi nhánh ngân hàng
                col = 71;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Đánh giá tay nghề
                col = 72;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //Password
                col = 73;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //NV_PTD
                col = 74;
                string sNV_PTD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sNV_PTD, false))
                {
                    errorCount++;
                }
            }
            this.Cursor = Cursors.Default;
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    string sbt = "sBTUV" + Commons.Modules.iIDUser;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        //string sSql = "INSERT INTO dbo.UNG_VIEN(MS_UV,HO,TEN,PHAI,NGAY_SINH,NOI_SINH,SO_CMND,NGAY_CAP,NOI_CAP,ID_TT_HN,HO_TEN_VC,NGHE_NGHIEP_VC,SO_CON,DT_DI_DONG,EMAIL,NGUOI_LIEN_HE,QUAN_HE,DT_NGUOI_LIEN_HE,ID_TP,ID_QUAN,ID_PX,THON_XOM,DIA_CHI_THUONG_TRU,ID_NTD,ID_CN,HINH_THUC_TUYEN,ID_TDVH,ID_KNLV,ID_DGTN,VI_TRI_TD_1,VI_TRI_TD_2,NGAY_HEN_DI_LAM,XAC_NHAN_DL,NGAY_NHAN_VIEC,XAC_NHAN_DTDH,DA_CHUYEN,GHI_CHU,DA_GIOI_THIEU,HUY_TUYEN_DUNG) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],case [" + grvData.Columns[3].FieldName.ToString() + "] when 'Nam' then 1 else 0 end,CONVERT(datetime,[" + grvData.Columns[4].FieldName.ToString() + "],103),[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],[" + grvData.Columns[7].FieldName.ToString() + "],[" + grvData.Columns[8].FieldName.ToString() + "],(SELECT TOP 1 ID_TT_HN FROM dbo.TT_HON_NHAN WHERE TEN_TT_HN = A.[" + grvData.Columns[9].FieldName.ToString() + "]),[" + grvData.Columns[10].FieldName.ToString() + "],[" + grvData.Columns[11].FieldName.ToString() + "],[" + grvData.Columns[12].FieldName.ToString() + "],[" + grvData.Columns[13].FieldName.ToString() + "],[" + grvData.Columns[14].FieldName.ToString() + "],[" + grvData.Columns[15].FieldName.ToString() + "],[" + grvData.Columns[16].FieldName.ToString() + "],[" + grvData.Columns[17].FieldName.ToString() + "],(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[18].FieldName.ToString() + "]),(SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = A.[" + grvData.Columns[19].FieldName.ToString() + "]),(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[20].FieldName.ToString() + "]),[" + grvData.Columns[21].FieldName.ToString() + "],[" + grvData.Columns[22].FieldName.ToString() + "],(SELECT TOP 1 ID_NTD FROM dbo.NGUON_TUYEN_DUNG WHERE TEN_NTD= A.[" + grvData.Columns[23].FieldName.ToString() + "]),(SELECT TOP 1 ID_CN FROM dbo.CONG_NHAN WHERE HO +' '+TEN = A.[" + grvData.Columns[24].FieldName.ToString() + "]),(SELECT ID_HTT FROM dbo.HINH_THUC_TUYEN WHERE TEN_HT_TUYEN = A.[" + grvData.Columns[25].FieldName.ToString() + "]),(SELECT TOP 1 ID_TDVH FROM dbo.TRINH_DO_VAN_HOA WHERE TEN_TDVH = A.[" + grvData.Columns[26].FieldName.ToString() + "]),(SELECT TOP 1 ID_KNLV FROM dbo.KINH_NGHIEM_LV WHERE TEN_KNLV = A.[" + grvData.Columns[27].FieldName.ToString() + "]),(SELECT TOP 1 ID_DGTN FROM dbo.DANH_GIA_TAY_NGHE WHERE TEN_DGTN = A.[" + grvData.Columns[28].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[29].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[30].FieldName.ToString() + "]),CONVERT(datetime,[" + grvData.Columns[31].FieldName.ToString() + "],103),[" + grvData.Columns[32].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[33].FieldName.ToString() + "],103),[" + grvData.Columns[34].FieldName.ToString() + "],[" + grvData.Columns[35].FieldName.ToString() + "],[" + grvData.Columns[36].FieldName.ToString() + "],[" + grvData.Columns[37].FieldName.ToString() + "],[" + grvData.Columns[38].FieldName.ToString() + "]  FROM " + sbt + " AS A";

                        string sSql1 = "INSERT INTO	 dbo.CONG_NHAN(MS_CN, MS_THE_CC,  HO, TEN, ID_QG, NGAY_SINH, NAM_SINH, PHAI, ID_TO, ID_CV, ID_LCV, ID_LHDLD, NGAY_HOC_VIEC, NGAY_THU_VIEC, NGAY_VAO_CTY, " +
                            "NGAY_VAO_LAM, VAO_LAM_LAI ,ID_TT_HD, ID_TT_HT, HINH_THUC_TUYEN, PHEP_CT, PHEP_CD, THAM_GIA_BHXH, LD_TINH, LAO_DONG_CONG_NHAT, TRUC_TIEP_SX, NGAY_NGHI_VIEC, ID_LD_TV, GHI_CHU, ID_DT, " +
                            "TON_GIAO, NOI_SINH, NGUYEN_QUAN, SO_CMND, NGAY_CAP, NOI_CAP, ID_TT_HN, MA_THE_ATM, SO_TAI_KHOAN, MS_THUE, TEN_KHONG_DAU, LD_NN, DT_DI_DONG, DT_NHA, DT_NGUOI_THAN, EMAIL, DIA_CHI_THUONG_TRU," +
                            " ID_TP, ID_QUAN, ID_PX, THON_XOM, DIA_CHI_TAM_TRU, ID_TP_TAM_TRU, ID_QUAN_TAM_TRU, ID_PX_TAM_TRU, THON_XOM_TAM_TRU, SO_BHXH, NGAY_DBHXH, NGAY_DBHXH_DT, " +
                            "NGAY_CHAM_DUT_NOP_BHXH, NGAY_THU_HOI_BHYT, ID_LOAI_TD, ID_TDVH, CHUYEN_MON, NGOAI_NGU, CAP_GIAY_PHEP, SO_GIAY_PHEP, NGAY_CAP_GP," +
                            " NGAY_HH_GP,  NGAN_HANG, CHI_NHANH_NH, DG_TAY_NGHE, PASS_WORD, NV_PTD) " +

                            "SELECT A.[" + grvData.Columns[0].FieldName.ToString() + "], A.[" + grvData.Columns[1].FieldName.ToString() + "], A.[" + grvData.Columns[2].FieldName.ToString() + "], A.[" + grvData.Columns[3].FieldName.ToString() + "], " +
                            "(SELECT TOP 1 ID_QG FROM dbo.QUOC_GIA WHERE TEN_QG = A.[" + grvData.Columns[4].FieldName.ToString() + "]), CONVERT(DATETIME,[A].[" + grvData.Columns[5].FieldName.ToString() + "],103), [A].[" + grvData.Columns[6].FieldName.ToString() + "],  " +
                            "[A].[" + grvData.Columns[7].FieldName.ToString() + "], (SELECT TOP 1 ID_TO FROM dbo.[TO] WHERE TEN_TO = A.[" + grvData.Columns[10].FieldName.ToString() + "]), (SELECT TOP 1 ID_CV FROM dbo.CHUC_VU WHERE TEN_CV = A.[" + grvData.Columns[11].FieldName.ToString() + "]), " +
                            "(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[12].FieldName.ToString() + "]), (SELECT TOP 1 ID_LHDLD FROM dbo.LOAI_HDLD WHERE TEN_LHDLD = A.[" + grvData.Columns[13].FieldName.ToString() + "]), CONVERT(DATETIME,[A].[" + grvData.Columns[14].FieldName.ToString() + "],103), " +
                            "CONVERT(DATETIME,A.[" + grvData.Columns[15].FieldName.ToString() + "],103), CONVERT(DATETIME,A.[" + grvData.Columns[16].FieldName.ToString() + "],103), CONVERT(DATETIME,[A].[" + grvData.Columns[16].FieldName.ToString() + "],103), [A].[" + grvData.Columns[17].FieldName.ToString() + "], " +
                            "(SELECT TOP 1 ID_TT_HD FROM dbo.TINH_TRANG_HD WHERE TEN_TT_HD = A.[" + grvData.Columns[18].FieldName.ToString() + "]), (SELECT TOP 1 ID_TT_HT FROM dbo.TINH_TRANG_HT WHERE TEN_TT_HT = A.[" + grvData.Columns[19].FieldName.ToString() + "]), [A].[" + grvData.Columns[20].FieldName.ToString() + "], " +
                            "[A].[" + grvData.Columns[21].FieldName.ToString() + "], [A].[" + grvData.Columns[22].FieldName.ToString() + "], [A].[" + grvData.Columns[23].FieldName.ToString() + "], A.[" + grvData.Columns[24].FieldName.ToString() + "], A.[" + grvData.Columns[25].FieldName.ToString() + "], A.[" + grvData.Columns[26].FieldName.ToString() + "], CONVERT(DATETIME,A.[" + grvData.Columns[27].FieldName.ToString() + "],103) , " +
                            "(SELECT TOP 1 ID_LD_TV FROM dbo.LY_DO_THOI_VIEC WHERE TEN_LD_TV = A.[" + grvData.Columns[28].FieldName.ToString() + "]), A.[" + grvData.Columns[29].FieldName.ToString() + "], (SELECT TOP 1 ID_DT FROM dbo.DAN_TOC WHERE TEN_DT = A.[" + grvData.Columns[30].FieldName.ToString() + "]), A.[" + grvData.Columns[31].FieldName.ToString() + "], A.[" + grvData.Columns[32].FieldName.ToString() + "],  " +
                            "A.[" + grvData.Columns[33].FieldName.ToString() + "], A.[" + grvData.Columns[34].FieldName.ToString() + "], CONVERT(DATETIME,A.[" + grvData.Columns[35].FieldName.ToString() + "],103), A.[" + grvData.Columns[36].FieldName.ToString() + "], (SELECT TOP 1 ID_TT_HN FROM dbo.TT_HON_NHAN WHERE TEN_TT_HN = A.[" + grvData.Columns[37].FieldName.ToString() + "]), A.[" + grvData.Columns[38].FieldName.ToString() + "], " +
                            "A.[" + grvData.Columns[39].FieldName.ToString() + "],A.[" + grvData.Columns[40].FieldName.ToString() + "], A.[" + grvData.Columns[41].FieldName.ToString() + "], A.[" + grvData.Columns[42].FieldName.ToString() + "], A.[" + grvData.Columns[43].FieldName.ToString() + "], A.[" + grvData.Columns[44].FieldName.ToString() + "], A.[" + grvData.Columns[45].FieldName.ToString() + "], A." + grvData.Columns[46].FieldName.ToString() + ", A.[" + grvData.Columns[47].FieldName.ToString() + "], " +
                            "(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[48].FieldName.ToString() + "]), (SELECT TOP 1 ID_QUAN FROM  QUAN WHERE TEN_QUAN  = A.[" + grvData.Columns[49].FieldName.ToString() + "]), " +
                            "(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[50].FieldName.ToString() + "]), A.[" + grvData.Columns[51].FieldName.ToString() + "], A.[" + grvData.Columns[52].FieldName.ToString() + "], " +
                            "(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[53].FieldName.ToString() + "]), (SELECT TOP 1 ID_QUAN FROM  QUAN WHERE TEN_QUAN  = A.[" + grvData.Columns[54].FieldName.ToString() + "]), " +
                            "(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[55].FieldName.ToString() + "]), A.[" + grvData.Columns[56].FieldName.ToString() + "], A.[" + grvData.Columns[57].FieldName.ToString() + "], CONVERT(DATETIME,A.[" + grvData.Columns[58].FieldName.ToString() + "],103), " +
                            "CONVERT(DATETIME,A.[" + grvData.Columns[59].FieldName.ToString() + "],103), CONVERT(DATETIME,A.[" + grvData.Columns[60].FieldName.ToString() + "],103), CONVERT(DATETIME,A.[" + grvData.Columns[61].FieldName.ToString() + "],103), " +
                            "(SELECT TOP 1 ID_LOAI_TD FROM dbo.LOAI_TRINH_DO WHERE TEN_LOAI_TD = A.[" + grvData.Columns[62].FieldName.ToString() + "]), (SELECT TOP 1 ID_TDVH FROM dbo.TRINH_DO_VAN_HOA WHERE TEN_TDVH = A.[" + grvData.Columns[63].FieldName.ToString() + "]), A.[" + grvData.Columns[64].FieldName.ToString() + "], A.[" + grvData.Columns[65].FieldName.ToString() + "]," +
                            " A.[" + grvData.Columns[66].FieldName.ToString() + "], A.[" + grvData.Columns[67].FieldName.ToString() + "], CONVERT(DATETIME,A.[" + grvData.Columns[68].FieldName.ToString() + "],103), CONVERT(DATETIME,A.[" + grvData.Columns[69].FieldName.ToString() + "],103), A.[" + grvData.Columns[70].FieldName.ToString() + "], A.[" + grvData.Columns[71].FieldName.ToString() + "], A.[" + grvData.Columns[72].FieldName.ToString() + "], " +
                            "A.[" + grvData.Columns[73].FieldName.ToString() + "], A.[" + grvData.Columns[74].FieldName.ToString() + "]  FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql1);

                        Commons.Modules.ObjSystems.XoaTable(sbt);

                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grdData.DataSource = dtSource.Clone();
                        cboChonSheet.Text = string.Empty;
                        btnFile.Text = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();

                }
            }
        }
        #endregion

        #region  Ứng viên bằng cấp
        private void ImportBangCap(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Tên bằng    
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 200, this.Name))
                {
                    errorCount++;
                }

                //Tên trường  
                col = 2;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                //Từ năm  
                col = 3;
                string sTuNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sTuNam, -999999, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 4;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sDenNam, -999999, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Xếp loại
                col = 5;
                string sXepLoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXepLoai, "XEP_LOAI", "TEN_XL", true, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTUVBC" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_BANG_CAP(ID_UV,TEN_BANG,TEN_TRUONG,TU_NAM,DEN_NAM,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[5].FieldName.ToString() + "]) FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }

        private void ImportKinhNghiem(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Tên công ty    
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }

                //chức vụ  
                col = 2;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 200, this.Name))
                {
                    errorCount++;
                }
                //Mức lương
                col = 3;
                string sMucLuong = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sMucLuong, 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                //từ năm
                col = 4;
                string sTuNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sTuNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 5;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sDenNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //lý do nghĩ
                col = 6;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTUVKN" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_KINH_NGHIEM(ID_UV,TEN_CONG_TY,CHUC_VU,MUC_LUONG,TU_NAM,DEN_NAM,LD_NGHI_VIEC) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "] FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }

        private void ImportThongTinKhac(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Mã số   
                col = 0;
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "UNG_VIEN", "MS_UV", true, this.Name))
                {
                    errorCount++;
                }
                //Nội dung  
                col = 1;
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                //Xếp loại
                col = 2;
                string sXepLoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXepLoai, "XEP_LOAI", "TEN_XL", true, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlTransaction sTrans = conn.BeginTransaction();
                    try
                    {
                        //tạo bảm tạm trên lưới
                        string sbt = "sBTTK" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_THONG_TIN_KHAC(ID_UV,NOI_DUNG,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[2].FieldName.ToString() + "]) FROM " + sbt + " AS A";
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        sTrans.Rollback();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
        }
        #endregion
        private void grvData_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                grvData = (GridView)sender;
                ptChung = grvData.GridControl.PointToClient(Control.MousePosition);
                grvData.ActiveEditor.DoubleClick += new EventHandler(ActiveEditor_DoubleClick);
            }
            catch
            { }
        }
        private void ActiveEditor_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DoRowDoubleClick(grvData, ptChung);
                grvData.RefreshData();
            }
            catch
            { }
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
            if (cboChonSheet.Text == "") return;
            try
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
                int col = -1;
                col = info.Column.AbsoluteIndex;
                if (col == -1)
                    return;

                string sSql = "";
                string sKQ = "";

                System.Data.DataRow row = grvData.GetDataRow(info.RowHandle);
                System.Data.DataRow drow;

                switch (col)
                {
                    case 0:
                        //sSql = "SELECT T2.TEN_NHH, TEN_LHH FROM dbo.LOAI_HANG_HOA T1 INNER JOIN dbo.NHOM_HANG_HOA T2 ON T2.ID_NHH = T1.ID_NHH ORDER BY T2.THU_TU,T1.THU_TU, T2.TEN_NHH,T1.TEN_LHH";
                        //drow = GetData("ID_LHH", sSql);
                        //sKQ = Convert.ToString(drow["TEN_LHH"]);
                        //row.ClearErrors();
                        break;

                    case 1:
                        {
                            break;
                        }
                    case 4:
                        {
                            sSql = "SELECT MA_QG, TEN_QG FROM dbo.QUOC_GIA ORDER BY MA_QG";
                            drow = GetData("ID_QG", sSql);
                            sKQ = Convert.ToString(drow["TEN_QG"]);
                            row.ClearErrors();
                            break;
                        }
                    case 8:
                        {
                            sSql = "SELECT MSDV, TEN_DV FROM dbo.DON_VI ORDER BY STT_DV";
                            drow = GetData("ID_DV", sSql);
                            sKQ = Convert.ToString(drow["TEN_DV"]);
                            row.ClearErrors();
                            break;
                        }
                    case 9:
                        {
                            string strSQL = "SELECT ISNULL(ID_DV,-1) ID_DV FROM dbo.DON_VI WHERE TEN_DV = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[8]).ToString().Trim() + "'";
                            int id_dv = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                            sSql = "SELECT T2.TEN_DV ,T1.MS_XN, T1.TEN_XN FROM dbo.XI_NGHIEP T1 INNER JOIN dbo.DON_VI T2 ON T2.ID_DV = T1.ID_DV WHERE (T1.ID_DV = " + (id_dv == 0 ? -1 : id_dv) + " OR " + (id_dv == 0 ? -1 : id_dv) + " = -1)  ORDER BY T2.STT_DV, T1.STT_XN";
                            drow = GetData("ID_XN", sSql);
                            sKQ = Convert.ToString(drow["TEN_XN"]);
                            row.ClearErrors();
                            break;
                        }
                    case 10:
                        {
                            string strSQL = "SELECT ISNULL(ID_DV,-1) ID_DV FROM dbo.DON_VI WHERE TEN_DV = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[8]).ToString().Trim() + "'";
                            int id_dv = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                            string strSQL1 = "SELECT ISNULL(ID_XN,-1) ID_DV FROM dbo.XI_NGHIEP WHERE TEN_XN = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[9]).ToString().Trim() + "'";
                            int id_xn = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL1));
                            sSql = "SELECT T1.MS_TO, T1.TEN_TO + ' ' + T3.MSDV TEN_TO FROM dbo.[TO] T1 INNER JOIN dbo.XI_NGHIEP T2 ON T2.ID_XN = T1.ID_XN INNER JOIN dbo.DON_VI T3 ON T3.ID_DV = T2.ID_DV WHERE(T3.ID_DV = " + (id_dv == 0 ? -1 : id_dv) + " OR " + (id_dv == 0 ? -1 : id_dv) + " = -1) AND(T2.ID_XN = " + (id_xn == 0 ? -1 : id_xn) + " OR " + (id_xn == 0 ? -1 : id_xn) + " = -1) ORDER BY T3.STT_DV, T2.STT_XN, T1.STT_TO";
                            drow = GetData("ID_TO", sSql);
                            sKQ = Convert.ToString(drow["TEN_TO"]);
                            sKQ = sKQ.Substring(0, sKQ.Length - 3).Trim();
                            row.ClearErrors();
                            break;
                        }
                    case 11:
                        {
                            sSql = "SELECT MS_CV, TEN_CV FROM dbo.CHUC_VU ORDER BY STT_IN_CV";
                            drow = GetData("ID_CV", sSql);
                            sKQ = Convert.ToString(drow["TEN_CV"]);
                            row.ClearErrors();
                            break;
                        }

                    case 12:
                        {
                            sSql = "SELECT TEN_LCV FROM dbo.LOAI_CONG_VIEC ORDER BY STT";
                            drow = GetData("ID_LCV", sSql);
                            sKQ = Convert.ToString(drow["TEN_LCV"]);
                            row.ClearErrors();
                            break;
                        }

                    case 13:
                        {
                            sSql = "SELECT TEN_LHDLD, SO_THANG FROM dbo.LOAI_HDLD ORDER BY STT";
                            drow = GetData("ID_LHDLD", sSql);
                            sKQ = Convert.ToString(drow["TEN_LHDLD"]);
                            row.ClearErrors();
                            break;
                        }

                    case 18:
                        {
                            sSql = "SELECT TEN_TT_HD FROM dbo.TINH_TRANG_HD ORDER BY STT";
                            drow = GetData("ID_TT_HD", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HD"]);
                            row.ClearErrors();
                            break;
                        }
                    case 19:
                        {
                            sSql = "SELECT TEN_TT_HT FROM dbo.TINH_TRANG_HT ORDER BY STT";
                            drow = GetData("ID_TT_HT", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HT"]);
                            row.ClearErrors();
                            break;
                        }

                    case 28:
                        {
                            sSql = "SELECT TEN_LD_TV, HE_SO FROM dbo.LY_DO_THOI_VIEC ORDER BY STT";
                            drow = GetData("ID_LD_TV", sSql);
                            sKQ = Convert.ToString(drow["TEN_LD_TV"]);
                            row.ClearErrors();
                            break;
                        }

                    case 30:
                        {
                            sSql = "SELECT TEN_DT FROM dbo.DAN_TOC";
                            drow = GetData("ID_DT", sSql);
                            sKQ = Convert.ToString(drow["TEN_DT"]);
                            row.ClearErrors();
                            break;
                        }

                    case 37:
                        {
                            sSql = "SELECT TEN_TT_HN FROM dbo.TT_HON_NHAN";
                            drow = GetData("ID_TT_HN", sSql);
                            sKQ = Convert.ToString(drow["TEN_TT_HN"]);
                            row.ClearErrors();
                            break;
                        }

                    case 48:
                        {
                            sSql = "SELECT MS_TINH, TEN_TP FROM dbo.THANH_PHO ORDER BY TEN_TP";
                            drow = GetData("ID_TP", sSql);
                            sKQ = Convert.ToString(drow["TEN_TP"]);
                            row.ClearErrors();
                            break;
                        }
                    case 49:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[48]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            sSql = "SELECT MS_QUAN, TEN_QUAN FROM dbo.QUAN WHERE ID_TP = " + (id_tp == 0 ? -1 : id_tp) + " OR " + (id_tp == 0 ? -1 : id_tp) + " = -1 ORDER BY TEN_QUAN";
                            drow = GetData("ID_QUAN", sSql);
                            sKQ = Convert.ToString(drow["TEN_QUAN"]);
                            row.ClearErrors();
                            break;
                        }
                    case 50:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[48]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            string strSQL1 = "SELECT ISNULL(ID_QUAN,-1) ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[49]).ToString().Trim() + "'";
                            int id_quan = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL1));


                            sSql = "SELECT MS_XA, TEN_PX FROM dbo.PHUONG_XA WHERE (ID_QUAN = " + (id_quan == 0 ? -1 : id_quan) + " OR " + (id_quan == 0 ? -1 : id_quan) + " = -1) ORDER BY TEN_PX";
                            drow = GetData("ID_PX", sSql);
                            sKQ = Convert.ToString(drow["TEN_PX"]);
                            row.ClearErrors();
                            break;
                        }
                    case 53:
                        {
                            sSql = "SELECT MS_TINH, TEN_TP FROM dbo.THANH_PHO ORDER BY TEN_TP";
                            drow = GetData("ID_TP", sSql);
                            sKQ = Convert.ToString(drow["TEN_TP"]);
                            row.ClearErrors();
                            break;
                        }
                    case 54:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[48]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            sSql = "SELECT MS_QUAN, TEN_QUAN FROM dbo.QUAN WHERE ID_TP = " + (id_tp == 0 ? -1 : id_tp) + " OR " + (id_tp == 0 ? -1 : id_tp) + " = -1 ORDER BY TEN_QUAN";
                            drow = GetData("ID_QUAN", sSql);
                            sKQ = Convert.ToString(drow["TEN_QUAN"]);
                            row.ClearErrors();
                            break;
                        }
                    case 55:
                        {
                            string strSQL = "SELECT ISNULL(ID_TP,-1) ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[48]).ToString().Trim() + "'";
                            int id_tp = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));

                            string strSQL1 = "SELECT ISNULL(ID_QUAN,-1) ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'" + grvData.GetFocusedRowCellValue(grvData.Columns[49]).ToString().Trim() + "'";
                            int id_quan = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL1));


                            sSql = "SELECT MS_XA, TEN_PX FROM dbo.PHUONG_XA WHERE (ID_QUAN = " + (id_quan == 0 ? -1 : id_quan) + " OR " + (id_quan == 0 ? -1 : id_quan) + " = -1) ORDER BY TEN_PX";
                            drow = GetData("ID_PX", sSql);
                            sKQ = Convert.ToString(drow["TEN_PX"]);
                            row.ClearErrors();
                            break;
                        }
                    case 62:
                        {
                            sSql = "SELECT TEN_LOAI_TD FROM dbo.LOAI_TRINH_DO ORDER BY STT";
                            drow = GetData("ID_LOAI_TD", sSql);
                            sKQ = Convert.ToString(drow["TEN_LOAI_TD"]);
                            row.ClearErrors();
                            break;
                        }
                    case 63:
                        {
                            sSql = "SELECT T2.TEN_LOAI_TD, T1.TEN_TDVH FROM dbo.TRINH_DO_VAN_HOA T1 INNER JOIN dbo.LOAI_TRINH_DO T2 ON T2.ID_LOAI_TD = T1.ID_LOAI_TD ORDER BY T2.STT, T1.STT";
                            drow = GetData("ID_TDVH", sSql);
                            sKQ = Convert.ToString(drow["TEN_TDVH"]);
                            row.ClearErrors();

                            break;
                        }
                    default:
                        break;
                }

                if (sKQ != null && sKQ != "")
                    grvData.SetFocusedRowCellValue(info.Column.FieldName, sKQ);
                grvData.RefreshData();
            }
            catch (Exception ex) { }
        }
        private DataRow GetData(string ImportType, string SQL)
        {
            try
            {
                frmImportView frm = new frmImportView(ImportType, SQL);
                if (frm.ShowDialog() == DialogResult.OK)
                    return frm._dtrow;
            }
            catch { }
            return null;
        }
        private void frmImportNhanSu_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
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
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            string nameType = "";
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name.Trim(), prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    nameType = props[i].PropertyType.Name.ToLower();
                    if (props[i].GetValue(item) == null)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    else
                    {
                        values[i] = nameType == "string" ? props[i].GetValue(item).ToString().Trim() : props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
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

        private void frmImportNhanSu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
