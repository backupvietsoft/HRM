using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucDinhBien : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public static ucDinhBien _instance;
        public static ucDinhBien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDinhBien();
                return _instance;
            }
        }
        public ucDinhBien()
        {
            InitializeComponent();
            Commons.Modules.sLoad = "0Load";
            datNam.EditValue = DateTime.Now;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
            Commons.Modules.sLoad = "";
            LoadGrdDinhBienLD();
        }
        private void LoadGrdDinhBienLD()
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDinhBien", datNam.DateTime.Year, cboDV.EditValue,Commons.Modules.UserName,Commons.Modules.TypeLanguage,""));
                if (grdDinhBien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDinhBien, grvDinhBien, dt, false, false, false, true, true, this.Name);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvDinhBien, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name);
                    Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
                }
                else
                {
                    grdDinhBien.DataSource = dt;
                }
            }
            catch
            {
            }
        }

        private void LoadGrdIn()
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDinhBienLD", datNam.DateTime.Year, cboDV.EditValue, "BCTONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grdPrint.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdPrint, grvPrint, dt, false, false, true, true, true, this.Name);
            }
            else
            {
                grdPrint.DataSource = dt;
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "sua":
                    {
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvDinhBien, true);
                        break;
                    }
                case "xoa":
                    {
                        XoaDinhBien();
                        break;
                    }
                case "In":
                    {
                        LoadGrdIn();
                        if (grvPrint.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                            return;
                        }
                        InDinhBien();
                        break;
                    }
                case "luu":
                    {
                        grvDinhBien.ValidateEditor();
                        if (grvDinhBien.HasColumnErrors) return;
                        try
                        {
                            string sbt = "sBTDB" + Commons.Modules.UserName;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvDinhBien), "");
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spGetListDinhBien", datNam.DateTime.Year, cboDV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage,sbt);
                        }
                        catch (Exception ex) 
                        {
                            return;
                        }
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
                        LoadGrdDinhBienLD();
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        LoadGrdDinhBienLD();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
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
        private void InDinhBien()
        {
            string sPath = "";
            sPath = Commons.Modules.MExcel.SaveFiles("Excel file (*.xlsx)|*.xlsx");
            if (sPath == "") return;
            //this.Cursor = Cursors.WaitCursor;
            Commons.Modules.ObjSystems.ShowWaitForm(this);

            DataTable dtNM = (DataTable)cboDV.Properties.DataSource;

            Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.DisplayAlerts = true;

            Microsoft.Office.Interop.Excel.Range title;
            Microsoft.Office.Interop.Excel.Range title1;

            int TCot = grvDinhBien.Columns.Count;
            int TDong = grvDinhBien.RowCount;

            excelApplication.Visible = false;
            grvPrint.ActiveFilter.Clear();
            grvPrint.ExportToXlsx(sPath);

            System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelWorkbooks.Open(sPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, true);
            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];
            try
            {
                excelApplication.Cells.Borders.LineStyle = 0;
                excelApplication.Cells.Font.Name = "Tahoma";
                excelApplication.Cells.Font.Size = 10;
                excelWorkSheet.AutoFilterMode = false;
                excelWorkSheet.Application.ActiveWindow.FreezePanes = false;


                Commons.Modules.MExcel.ThemDong(excelWorkSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 2);
                int DONG = 1;
                int COT = 3;
                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, 1, 1, 1, 14);
                title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                title.Font.Bold = true;

                DONG++;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG, 14);
                title.Interior.Color = System.Drawing.Color.FromArgb(180, 198, 231);
                title.Font.Bold = true;

                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 2, DONG, 2);
                title.Value2 = "Tổng số CNV " + dtNM.Rows.Count + " nhà máy";


                title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, COT);
                title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG + 1, COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + TDong + 1, COT) + "";
                title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, COT, DONG, COT + 11);
                title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                DONG = DONG + TDong + 4;
                //Vẻ chi tiếc theo từng nhà máy
                foreach (DataRow item in dtNM.Rows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDinhBienLD", datNam.DateTime.Year, item["ID_DV"], "BCCHITIEC", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, 1, DONG - 1, 14);
                    title.Interior.Color = System.Drawing.Color.FromArgb(0, 176, 80);
                    title.Font.Bold = true;


                    //vẻ dòng tổng
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, 2, DONG - 1, 2);
                    title.Value2 = item["TEN_DV"];

                    //for (int i = 0; i < 12; i++)
                    //{
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, COT, DONG - 1, COT);
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG, COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + dt.Rows.Count - 1, COT) + "";
                    title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, COT, DONG -1, COT + 11);
                    title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                    //}

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, 1, DONG + dt.Rows.Count - 1, dt.Columns.Count);
                    Commons.Modules.MExcel.MExportExcel(dt, excelWorkSheet, title, false);

                    for (int i = 0; i < 11; i++)
                    {
                        title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 2, TCot + 3 + i, DONG - 2, TCot + 3 + i);
                        title.Value2 = "" + (i + 2) + " vs " + (i + 1) + "";
                        title.Font.Bold = true;
                        title.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        title.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }

                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, TCot + COT, DONG - 1, TCot + COT);
                    title.Value2 = "=SUM(" + Commons.Modules.MExcel.TimDiemExcel(DONG, TCot + COT) + ":" + Commons.Modules.MExcel.TimDiemExcel(DONG + dt.Rows.Count - 1,TCot + COT) + "";
                    title.Interior.Color = System.Drawing.Color.FromArgb(0, 176, 80);
                    title.Font.Bold = true;
                    title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG - 1, TCot + COT, DONG -1, TCot + COT + 10);
                    title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);


                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, TCot + COT, DONG, TCot + COT);
                    title.Value2 = "=" + Commons.Modules.MExcel.TimDiemExcel(DONG, COT + 1) + "-" +
                       Commons.Modules.MExcel.TimDiemExcel(DONG, COT) + "";

                    title1 = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, TCot + COT, DONG, TCot + COT + 10);
                    title.AutoFill(title1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    title = Commons.Modules.MExcel.GetRange(excelWorkSheet, DONG, TCot + COT, DONG + dt.Rows.Count - 1, TCot + COT + 10);
                    title1.AutoFill(title, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);

                    DONG = DONG + dt.Rows.Count + 2;
                }

                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 8, "@", true, 1, 1, 1, 1);
                Commons.Modules.MExcel.ColumnWidth(excelWorkSheet, 43, "@", true, 1, 2, 1, 2);

                excelWorkSheet.Application.ActiveWindow.SplitRow = 1;
                excelWorkSheet.Application.ActiveWindow.SplitColumn = 2;
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
        private void ucDinhBien_Load(object sender, EventArgs e)
        {
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
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
            datNam.ReadOnly = !visible;
            cboDV.ReadOnly = !visible;
        }
        private void grvDinhBienLD_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //kiểm tra
            try
            {
                grvDinhBien.ClearColumnErrors();
                try
                {
                    DataTable dt = new DataTable();
                    GridView view = sender as GridView;
                    if (view == null) return;
                    if (view.FocusedColumn.Name == "colID_LCV")
                    {//kiểm tra máy không được để trống
                        if (string.IsNullOrEmpty(e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erLDVKhongTrong");
                            view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                            return;
                        }
                        else
                        {
                            dt = new DataTable();
                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdDinhBien);
                            if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_LCV").Equals(e.Value)) > 0)
                            {
                                e.Valid = false;
                                e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                                view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                                return;
                            }
                        }
                    }
                }
                catch { }
            }
            catch
            {
            }
        }
        private void grvDinhBienLD_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvDinhBienLD_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void datNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadGrdDinhBienLD();
        }
        private void XoaDinhBien()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteDinhBien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DINH_BIEN WHERE NAM = " + datNam.DateTime.Year + " AND ID_DV = " + cboDV.EditValue + " AND ID_LCV = " + grvDinhBien.GetFocusedRowCellValue("ID_LCV") + "");
                grvDinhBien.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void grvDinhBienLD_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (Commons.Modules.ObjSystems.IsnullorEmpty(view.GetRowCellValue(e.RowHandle, "ID_LCV")))
            {
                e.Valid = false;
                e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erLDVKhongTrong");
                view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                return;
            }
        }
        private void grdDinhBienLD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaDinhBien();
            }
        }
        private void grvDinhBien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

                GridView view = sender as GridView;
                if (view == null) return;
                var row = view.GetFocusedDataRow();

                if (e.Column.Name == "colSL_CHUYEN")
                {
                    double sl = Convert.ToDouble(row["SL_CHUYEN"].ToString());
                    double db = Convert.ToDouble(row["DINH_BIEN"].ToString());
                    if (sl != null && db != null)
                    {
                        double tongso = Convert.ToDouble(db * sl);
                        row["TONG_SO"] = tongso;
                        //view.SetRowCellValue(e.RowHandle, view.Columns["TONG_SO"], tongso);
                    }
                }
                if (e.Column.Name == "colDINH_BIEN")
                {
                    double sl = Convert.ToDouble(row["SL_CHUYEN"].ToString());
                    double db = Convert.ToDouble(row["DINH_BIEN"].ToString());
                    if (sl != null && db != null)
                    {
                        double tongso = Convert.ToDouble(db * sl);
                        row["TONG_SO"] = tongso;
                        //view.SetRowCellValue(e.RowHandle, view.Columns["TONG_SO"], tongso);
                    }
                }

            }
            catch
            {

            }
        }
        //private void BangChamCongThang_SB2()
        //{
        //    try
        //    {
        //        System.Data.SqlClient.SqlConnection conn;
        //        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
        //        conn.Open();
        //        DataTable dtBCThang;

        //        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThang_SB", conn);

        //        //cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
        //        //cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
        //        //cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
        //        //cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
        //        //cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
        //        //cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
        //        //cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

        //        DataSet ds = new DataSet();
        //        adp.Fill(ds);
        //        dtBCThang = new DataTable();
        //        dtBCThang = ds.Tables[0].Copy();

        //        DataTable dtSLTO = new DataTable(); // Lấy số lượng xí nghiệp
        //        dtSLTO = ds.Tables[1].Copy();
        //        int slTO = Convert.ToInt32(dtSLTO.Rows[0][0]);

        //        SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
        //        if (SaveExcelFile == "")
        //        {
        //            return;
        //        }
        //        Excel.Application oXL;
        //        Excel.Workbook oWB;
        //        Excel.Worksheet oSheet;
        //        oXL = new Excel.Application();
        //        oXL.Visible = false;

        //        oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
        //        oSheet = (Excel.Worksheet)oWB.ActiveSheet;

        //        string fontName = "Times New Roman";
        //        int fontSizeTieuDe = 18;
        //        int fontSizeNoiDung = 9;
        //        int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
        //        int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
        //        int iSoNgay = (iDNgay - iTNgay) + 1;

        //        string lastColumn = string.Empty;
        //        lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);
        //        string lastColumNgay = string.Empty;
        //        lastColumNgay = CharacterIncrement(iSoNgay + 7);
        //        string firstColumTT = string.Empty;
        //        firstColumTT = CharacterIncrement(iSoNgay + 8);

        //        Range row1_TieuDe = oSheet.get_Range("A1", "J1");
        //        row1_TieuDe.Merge();
        //        row1_TieuDe.Font.Bold = true;
        //        row1_TieuDe.Value2 = dtBCThang.Rows[0]["TEN_DV"];
        //        row1_TieuDe.WrapText = false;
        //        row1_TieuDe.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


        //        Range row2_TieuDe = oSheet.get_Range("A2", "J2");
        //        row2_TieuDe.Merge();
        //        row2_TieuDe.Font.Bold = true;
        //        row2_TieuDe.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
        //        row2_TieuDe.WrapText = false;
        //        row2_TieuDe.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


        //        Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
        //        row2_TieuDe_BaoCao.Merge();
        //        row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
        //        row2_TieuDe_BaoCao.Font.Name = fontName;
        //        row2_TieuDe_BaoCao.Font.Bold = true;
        //        row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        //        row2_TieuDe_BaoCao.RowHeight = 50;
        //        row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

        //        Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
        //        row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
        //        row5_TieuDe_Format.Font.Name = fontName;
        //        row5_TieuDe_Format.Font.Bold = true;
        //        row5_TieuDe_Format.WrapText = true;
        //        row5_TieuDe_Format.NumberFormat = "@";
        //        row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        //        row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

        //        //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
        //        //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
        //        //oSheet.Cells[7, 1] = "BỘ PHẬN";
        //        //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
        //        //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




        //        //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


        //        Range row5_TieuDe_Stt = oSheet.get_Range("A5");
        //        row5_TieuDe_Stt.Merge();
        //        row5_TieuDe_Stt.Value2 = "Stt";
        //        row5_TieuDe_Stt.ColumnWidth = 5;

        //        Range row6_TieuDe_Stt = oSheet.get_Range("A6");
        //        row6_TieuDe_Stt.Merge();
        //        row6_TieuDe_Stt.Value2 = "No";
        //        row6_TieuDe_Stt.ColumnWidth = 5;

        //        Range row5_TieuDe_MaSo = oSheet.get_Range("B5");
        //        row5_TieuDe_MaSo.Merge();
        //        row5_TieuDe_MaSo.Value2 = "MSCN";
        //        row5_TieuDe_MaSo.ColumnWidth = 12;

        //        Range row6_TieuDe_MaSo = oSheet.get_Range("B6");
        //        row6_TieuDe_MaSo.Merge();
        //        row6_TieuDe_MaSo.Value2 = "CODE";
        //        row6_TieuDe_MaSo.ColumnWidth = 12;

        //        Range row5_TieuDe_HoTen = oSheet.get_Range("C5");
        //        row5_TieuDe_HoTen.Merge();
        //        row5_TieuDe_HoTen.Value2 = "HỌ TÊN";
        //        row5_TieuDe_HoTen.ColumnWidth = 25;

        //        Range row6_TieuDe_HoTen = oSheet.get_Range("C6");
        //        row6_TieuDe_HoTen.Merge();
        //        row6_TieuDe_HoTen.Value2 = "FULL NAME";
        //        row6_TieuDe_HoTen.ColumnWidth = 25;

        //        //Range row5_TieuDe_XiNgiep = oSheet.get_Range("D5");
        //        //row5_TieuDe_XiNgiep.Merge();
        //        //row5_TieuDe_XiNgiep.Value2 = "XÍ NGHIỆP";
        //        //row5_TieuDe_XiNgiep.ColumnWidth = 12;

        //        //Range row6_TieuDe_XiNgiep = oSheet.get_Range("D6");
        //        //row6_TieuDe_XiNgiep.Merge();
        //        //row6_TieuDe_XiNgiep.Value2 = "ENTERPRISE";
        //        //row6_TieuDe_XiNgiep.ColumnWidth = 12;

        //        Range row5_TieuDe_To = oSheet.get_Range("D5");
        //        row5_TieuDe_To.Merge();
        //        row5_TieuDe_To.Value2 = "TỔ";
        //        row5_TieuDe_To.ColumnWidth = 12;

        //        Range row6_TieuDe_To = oSheet.get_Range("D6");
        //        row6_TieuDe_To.Merge();
        //        row6_TieuDe_To.Value2 = "DEP";
        //        row6_TieuDe_To.ColumnWidth = 12;

        //        int col = 5;
        //        while (iTNgay <= iDNgay)
        //        {
        //            oSheet.Cells[5, col] = iTNgay;
        //            oSheet.Cells[6, col] = "a";
        //            oSheet.Cells[6, col].Interior.Color = Color.White;

        //            //Range row6_b = oSheet.get_Range(oSheet.Cells[6, col + 1]);
        //            //row6_b.Value2 = "b";
        //            //row6_b.Interior.Color = Color.FromArgb(128, 255, 128);

        //            oSheet.Cells[6, col + 1] = "b";
        //            oSheet.Cells[6, col + 1].Interior.Color = Color.FromArgb(187, 255, 187);
        //            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col)], oSheet.Cells[5, Convert.ToInt32(col + 1)]].Merge();
        //            //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
        //            col += 2;
        //            iTNgay++;
        //        }

        //        oSheet.Cells[5, col] = "Ngày công";
        //        oSheet.Cells[6, col] = "Workday";

        //        col = col + 1;
        //        oSheet.Cells[5, col] = "Tăng ca";
        //        oSheet.Cells[6, col] = "Overtime";

        //        col = col + 1;
        //        oSheet.Cells[5, col] = "Tăng ca đêm";
        //        oSheet.Cells[6, col] = "Night OT";

        //        col = col + 1;
        //        oSheet.Cells[5, col] = "Chủ nhật";
        //        oSheet.Cells[6, col] = "Sunday";

        //        col = col + 1;
        //        oSheet.Cells[5, col] = "Ngày lễ";
        //        oSheet.Cells[6, col] = "Holidays";

        //        col = col + 1;
        //        oSheet.Cells[5, col] = "Ghi chú (Notes)";
        //        oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 5]].Merge();
        //        oSheet.Cells[6, col] = "P Anmual";
        //        oSheet.Cells[6, col + 1] = "CĐ Policy";
        //        oSheet.Cells[6, col + 2] = "KL Comp";
        //        oSheet.Cells[6, col + 3] = "01";
        //        oSheet.Cells[6, col + 4] = "03";
        //        oSheet.Cells[6, col + 5] = "VLD Unreasonab";

        //        col = col + 6;
        //        oSheet.Cells[6, col] = "TR/S Late";

        //        col = col + 1;
        //        oSheet.Cells[6, col] = "QBT Forget";

        //        col = col + 1;
        //        oSheet.Cells[6, col] = "count overtime";


        //        int rowCnt = 0;
        //        int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
        //        int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
        //        int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
        //        int rowBD_XN = 0; // Row để insert dòng xí nghiệp
        //        int rowCONG = 0; // Row để insert dòng tổng
        //        //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
        //        int rowBD = 7;
        //        string cotCN_A = "";
        //        string cotCN_B = "";
        //        string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
        //        string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
        //        DataTable dt_temp = new DataTable();
        //        dt_temp = ds.Tables[0].Copy(); // Dữ row count data


        //        for (int i = 0; i < TEN_TO.Count(); i++)
        //        {
        //            dtBCThang = ds.Tables[0].Copy();
        //            dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[i]).CopyToDataTable().Copy();
        //            DataRow[] dr = dtBCThang.Select();
        //            current_dr = dr.Count();
        //            string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
        //            foreach (DataRow row in dr)
        //            {
        //                for (col = 0; col < dtBCThang.Columns.Count; col++)
        //                {
        //                    if (Convert.ToInt32(row[0]) == 1)
        //                    {
        //                        if (row[col].ToString() == "CN")
        //                        {
        //                            //cotCN = cotCN + (col + 1) + ",";
        //                            cotCN_A = CharacterIncrement(col);
        //                            cotCN_B = CharacterIncrement(col + 1);
        //                            Range ToMau = oSheet.get_Range("" + cotCN_A + "5", cotCN_B + "" + (dt_temp.Rows.Count + 6 + (slTO * 2)) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
        //                            ToMau.Interior.Color = Color.FromArgb(255, 128, 0);
        //                            //ToMau.NumberFormat = "#,##0.0;(#,##0.0); ; ";
        //                        }
        //                    }
        //                    rowData[rowCnt, col] = row[col].ToString();
        //                }
        //                rowCnt++;
        //            }
        //            if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
        //            {
        //                dr_Cu = 0;
        //                rowBD_XN = 0;
        //                rowCONG = 0;
        //                chanVongDau = "";
        //            }
        //            else
        //            {
        //                rowBD_XN = 1;
        //                rowCONG = 1;
        //            }
        //            rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
        //            //rowCnt = rowCnt + 6 + dr_Cu;
        //            rowCnt = rowBD + current_dr - 1;


        //            // Tạo group xí nghiệp
        //            Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
        //            row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
        //            oSheet.Cells[rowBD, 1] = "TỔ";
        //            oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Merge();
        //            oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
        //            oSheet.Cells[rowBD, 3] = TEN_TO[i].ToString();

        //            //Đổ dữ liệu của xí nghiệp
        //            oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

        //            //Tính tổng xí nghiệp
        //            Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
        //            row_groupTONG_Format.Interior.Color = Color.Yellow;
        //            row_groupTONG_Format.Font.Bold = true;
        //            oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
        //            oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

        //            for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
        //            {
        //                oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
        //            }

        //            dr_Cu = current_dr;
        //            keepRowCnt = rowCnt;
        //            rowCnt = 0;
        //        }

        //        Excel.Range formatRange;
        //        rowCnt = keepRowCnt + 2; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
        //        //formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
        //        //formatRange.NumberFormat = "dd/MM/yyyy";
        //        //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
        //        //formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
        //        //formatRange.NumberFormat = "dd/MM/yyyy";
        //        //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
        //        //formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
        //        //formatRange.NumberFormat = "@";
        //        //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        //dịnh dạng
        //        //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

        //        string CurentColumn = string.Empty;
        //        int colBD = 4;
        //        int colKT = dtBCThang.Columns.Count;
        //        //format

        //        for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
        //        {
        //            CurentColumn = CharacterIncrement(col);
        //            formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
        //            //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
        //            formatRange.NumberFormat = "0.00;-0;;@";
        //            try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
        //        }

        //        //colKT++;
        //        //CurentColumn = CharacterIncrement(colKT);
        //        //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
        //        //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
        //        ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
        //        ////Kẻ khung toàn bộ
        //        formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
        //        formatRange.Font.Name = fontName;
        //        formatRange.Font.Size = fontSizeNoiDung;
        //        BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
        //        // filter
        //        oSheet.Application.ActiveWindow.SplitColumn = 4;
        //        oSheet.Application.ActiveWindow.FreezePanes = true;
        //        oXL.Visible = true;
        //        oXL.UserControl = true;

        //        oWB.SaveAs(SaveExcelFile,
        //            AccessMode: Excel.XlSaveAsAccessMode.xlShared);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
