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
    public partial class ucDinhBienLD : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucDinhBienLD _instance;
        public static ucDinhBienLD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDinhBienLD();
                return _instance;
            }
        }
        public ucDinhBienLD()
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDinhBienLD", datNam.DateTime.Year, cboDV.EditValue, "",Commons.Modules.UserName,Commons.Modules.TypeLanguage));
                if (grdDinhBienLD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDinhBienLD, grvDinhBienLD, dt, false, false, true, true, true, this.Name);

                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvDinhBienLD, Commons.Modules.ObjSystems.DataLoaiCV(false,-1), true, "ID_LCV", this.Name);
                    grvDinhBienLD.Columns["ID_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBienLD);
                }
                else
                {
                    grdDinhBienLD.DataSource = dt;
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
                        Commons.Modules.ObjSystems.AddnewRow(grvDinhBienLD, true);
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
                        grvDinhBienLD.ValidateEditor();
                        if (grvDinhBienLD.HasColumnErrors) return;
                        try
                        {
                            string sbt = "sBTDB" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvDinhBienLD), "");
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spGetListDinhBienLD", datNam.DateTime.Year, cboDV.EditValue, sbt, Commons.Modules.UserName, Commons.Modules.TypeLanguage);
                        }
                        catch
                        {
                            return;
                        }
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBienLD);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBienLD);
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

            int TCot = grvDinhBienLD.Columns.Count;
            int TDong = grvDinhBienLD.RowCount;

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
        private void ucDinhBienLD_Load(object sender, EventArgs e)
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
                grvDinhBienLD.ClearColumnErrors();
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
                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdDinhBienLD);
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DINH_BIEN_LD WHERE NAM = " + datNam.DateTime.Year + " AND ID_DV = " + cboDV.EditValue + " AND ID_LCV = " + grvDinhBienLD.GetFocusedRowCellValue("ID_LCV") + "");
                grvDinhBienLD.DeleteSelectedRows();
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
    }
}
