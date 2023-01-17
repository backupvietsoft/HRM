using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DataTable = System.Data.DataTable;
using DevExpress.DataAccess.Excel;
using DevExpress.Spreadsheet;
using Microsoft.Office.Interop.Excel;
using Borders = Microsoft.Office.Interop.Excel.Borders;
using System.Collections;
using System.Diagnostics;

namespace Vs.TimeAttendance
{
    public partial class ucDKThoiGianKhongLamSP : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private string ChuoiKT = "";
        public static ucDKThoiGianKhongLamSP _instance;
        public static ucDKThoiGianKhongLamSP Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDKThoiGianKhongLamSP();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucDKThoiGianKhongLamSP()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucDKThoiGianKhongLamSP_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetlistDK_TG_KHONG_LAM_SP", Convert.ToDateTime(cboThang.EditValue),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, isAdd));
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["TEN_XN"].ReadOnly = true;
                dt.Columns["TEN_TO"].ReadOnly = true;
                dt.Columns["COT_1"].ReadOnly = false;
                dt.Columns["COT_2"].ReadOnly = false;
                dt.Columns["COT_3"].ReadOnly = false;
                dt.Columns["COT_4"].ReadOnly = false;
                dt.Columns["COT_5"].ReadOnly = false;
                dt.Columns["COT_6"].ReadOnly = false;
                dt.Columns["COT_7"].ReadOnly = false;
                dt.Columns["TG_HC"].ReadOnly = false;
                dt.Columns["TG_TC_NT"].ReadOnly = false;
                dt.Columns["TG_TC_CN"].ReadOnly = false;
                dt.Columns["TG_TC_NL"].ReadOnly = false;
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, isAdd ? true : false, true, false, true, true, this.Name);

                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["NGAY"].Visible = false;

                    RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                    txtEdit.Properties.DisplayFormat.FormatString = "N2";
                    txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    txtEdit.Properties.EditFormat.FormatString = "N2";
                    txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    txtEdit.Properties.Mask.EditMask = "N2";
                    txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    grvData.Columns["COT_1"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_2"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_3"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_4"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_5"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_6"].ColumnEdit = txtEdit;
                    grvData.Columns["COT_7"].ColumnEdit = txtEdit;
                    grvData.Columns["TG_HC"].ColumnEdit = txtEdit;
                    grvData.Columns["TG_TC_NT"].ColumnEdit = txtEdit;
                    grvData.Columns["TG_TC_CN"].ColumnEdit = txtEdit;
                    grvData.Columns["TG_TC_NL"].ColumnEdit = txtEdit;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }

            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatType = FormatType.Numeric;
            //grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatString = "N0";
        }

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT CONVERT(NVARCHAR(10),NGAY,103) NGAY ,SUBSTRING(CONVERT(VARCHAR(10),NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS THANG FROM dbo.DK_TG_KHONG_LAM_SP ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;
                grvThang.Columns["THANG"].Visible = false;

                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.Month + "/" + now.Year.ToString();
            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "export":
                    {
                        //try
                        //{
                        //    string sPath = "";
                        //    sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                        //    if (sPath == "") return;
                        //    Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                        //    excelApplication.DisplayAlerts = true;

                        //    excelApplication.Visible = false;


                        //    System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                        //    Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                        //    object misValue = System.Reflection.Missing.Value;
                        //    Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                        //    excelWorkbook.SaveAs(sPath);

                        //    Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                        //    DataTable dt = new DataTable();
                        //    dt = ((DataTable)grdData.DataSource).Copy();
                        //    dt.DefaultView.RowFilter = "";
                        //    DataView dv = dt.DefaultView;

                        //    DataTable dt1 = new DataTable();
                        //    dt1 = dv.ToTable(false, "MS_CN", "HO_TEN", "TEN_XN", "TEN_TO", "TG_HC", "TG_TC_NT", "TG_TC_CN");
                        //    dt1.Columns["MS_CN"].ColumnName = "MSCN";
                        //    dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                        //    dt1.Columns["TEN_XN"].ColumnName = "Xưởng/Phòng ban";
                        //    dt1.Columns["TEN_TO"].ColumnName = "Tổ";
                        //    dt1.Columns["TG_HC"].ColumnName = "Giờ hành chính";
                        //    dt1.Columns["TG_TC_NT"].ColumnName = "Giờ tăng ca ngày thường";
                        //    dt1.Columns["TG_TC_CN"].ColumnName = "Giờ tăng ca chủ nhật";
                        //    Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                        //    Ranges1.Range["A1:G1"].Font.Bold = true;
                        //    Ranges1.Range["A1:G1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        //    Ranges1.Range["A1:G1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        //    Ranges1.WrapText = true;
                        //    Ranges1.ColumnWidth = 20;
                        //    Ranges1.Range["B1"].ColumnWidth = 30;
                        //    Ranges1.Range["E2:E" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                        //    Ranges1.Range["F2:F" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                        //    Ranges1.Range["G2:G" + ((dt1.Rows.Count + 1)) + ""].NumberFormat = "0.0";
                        //    BorderAround(Ranges1.Range["A1:G" + (dt1.Rows.Count + 1) + ""]);
                        //    MExportExcel(dt1, excelWorkSheet, Ranges1);

                        //    excelApplication.Visible = true;
                        //    excelWorkbook.Save();
                        //}
                        //catch (Exception ex) { XtraMessageBox.Show(ex.Message); }

                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetlistDK_TG_KHONG_LAM_SP", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                            cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDonVi.EditValue;
                            cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = cboXiNghiep.EditValue;
                            cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = cboTo.EditValue;
                            cmd.Parameters.Add("@THEM", SqlDbType.Int).Value = 2;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            ds.Tables[0].TableName = "KhongLamSP";
                            ds.Tables[1].TableName = "KLSP";
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                            saveFileDialog.FilterIndex = 0;
                            saveFileDialog.RestoreDirectory = true;
                            //saveFileDialog.CreatePrompt = true;
                            saveFileDialog.CheckFileExists = false;
                            saveFileDialog.CheckPathExists = false;
                            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            saveFileDialog.Title = "Export Excel File To";
                            DialogResult res = saveFileDialog.ShowDialog();
                            // If the file name is not an empty string open it for saving.
                            if (res == DialogResult.OK)
                            {
                                Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateKhongLamRaSP.xlsx", ds, new string[] { "{", "}" });
                                Process.Start(saveFileDialog.FileName);
                            }
                        }
                        catch (Exception EX)
                        {

                        }



                        break;
                    }
                case "import":
                    {

                        frmImportDangKyKLSP frm = new frmImportDangKyKLSP();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadThang();
                            LoadData();
                        }
                        else
                        {
                            LoadThang();
                            LoadData();
                        }

                        break;
                    }
                case "themsua":
                    {
                        grdData.DataSource = null;
                        isAdd = true;
                        LoadData();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, false);
                        EnableButon(isAdd);
                        break;

                    }
                case "xoa":
                    {
                        XoaCheDoLV();
                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        DataTable dt = new DataTable();
                        dt = (DataTable)grdData.DataSource;
                        if (!KiemTraLuoi(dt)) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        isAdd = false;
                        LoadData();
                        LoadThang();
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadData();
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            DevExpress.DataAccess.Native.Excel.DataView dv_temp = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;

            excelDataSource.SourceOptions = new CsvSourceOptions() { CellRange = "A6:" + "N" + (dv_temp.Count + 6) + "" };
            excelDataSource.SourceOptions.SkipEmptyRows = false;
            excelDataSource.SourceOptions.UseFirstRowAsHeader = true;
            excelDataSource.Fill();
            DevExpress.DataAccess.Native.Excel.DataView dv = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;
            for (int i = 0; i < dv.Count; i++)
            {
                DevExpress.DataAccess.Native.Excel.ViewRow row = dv[i] as DevExpress.DataAccess.Native.Excel.ViewRow;
                foreach (DevExpress.DataAccess.Native.Excel.ViewColumn col in dv.Columns)
                {
                    object val = col.GetValue(row);
                }
            }

            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "NGAY";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "MS_CN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "HO_TEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "COT_1";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "COT_2";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));

                            break;
                        }
                    case 5:
                        {
                            sTenCot = "COT_3";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "COT_4";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "COT_5";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "COT_6";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "COT_7";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 10:
                        {
                            sTenCot = "TG_HC";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 11:
                        {
                            sTenCot = "TG_TC_NT";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 12:
                        {
                            sTenCot = "TG_TC_CN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 13:
                        {
                            sTenCot = "TG_TC_NL";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 14:
                        {
                            sTenCot = "GHI_CHU";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        if (props[i].GetValue(item) == null || props[i].GetValue(item).ToString() == "")
                        {
                            values[i] = 0;
                        }
                        else
                        {

                            values[i] = props[i].GetValue(item);
                        }

                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                try
                {
                    table.Rows.Add(values);
                }
                catch (Exception ex) { }
            }
            return table;
        }

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = visible;

            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = !visible;

            btnALL.Buttons[7].Properties.Visible = visible;
            btnALL.Buttons[8].Properties.Visible = visible;
            cboTo.Enabled = !visible;
            cboThang.Enabled = !visible;
            cboDonVi.Enabled = !visible;
            cboXiNghiep.Enabled = !visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.DK_TG_KHONG_LAM_SP WHERE ID_CN = " + grvData.GetFocusedRowCellValue("ID_CN") +
                                                        " AND NGAY = '"
                                                        + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    view.SetFocusedRowCellValue("THANG", cboThang.EditValue);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private bool Savedata()
        {
            string sTB = "LK_Tam" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveDK_TG_KHONG_LAM_SP", sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);

                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
        private void grvData_RowCountChanged(object sender, EventArgs e)
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

        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }


            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        //Nhap ung vien
        public DXMenuItem MCreateMenuNhapUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucDKThoiGianKhongLamSP", "CapNhatGio", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(CapNhat));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void CapNhat(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvData.FocusedColumn.FieldName;
                if (grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName).ToString() == "") return;
                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, (DataTable)grdData.DataSource, "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["TEN_XN"].ReadOnly = true;
                dt.Columns["TEN_TO"].ReadOnly = true;
                grdData.DataSource = dt;
            }
            catch { }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[3].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName == "MS_CN" || grvData.FocusedColumn.FieldName == "HO_TEN" || grvData.FocusedColumn.FieldName == "TEN_XN" || grvData.FocusedColumn.FieldName == "TEN_TO") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuNhapUngVien(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
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
        private void BorderAround(Range range)
        {
            Borders borders = range.Borders;
            borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }

        #endregion


        public bool IsNumeric(string input)
        {
            bool IsNumber = true;
            for (int i = 1; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                    IsNumber = false;
                if ((input[i] == '.' && Char.IsDigit(input[i - 1]) && Char.IsDigit(input[i + 1])))
                    IsNumber = true;
            }
            return IsNumber;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        #region kiemTra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;

                if (!KiemDuLieuSo(grvData, dr, "COT_1", grvData.Columns["COT_1"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_2", grvData.Columns["COT_2"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_3", grvData.Columns["COT_3"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_4", grvData.Columns["COT_4"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_5", grvData.Columns["COT_5"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_6", grvData.Columns["COT_6"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "COT_7", grvData.Columns["COT_7"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_HC", grvData.Columns["TG_HC"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_TC_NT", grvData.Columns["TG_TC_NT"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieuSo(grvData, dr, "TG_TC_CN", grvData.Columns["TG_TC_CN"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvData, dr, "TG_TC_NL", grvData.Columns["TG_TC_NL"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                Double dTG_HC = Convert.ToDouble(dr[grvData.Columns["TG_HC"].FieldName.ToString()]);
                Double dTG_TC_NT = Convert.ToDouble(dr[grvData.Columns["TG_TC_NT"].FieldName.ToString()]);
                Double dTG_TC_CN = Convert.ToDouble(dr[grvData.Columns["TG_TC_CN"].FieldName.ToString()]);
                Double dTG_TC_NL = Convert.ToDouble(dr[grvData.Columns["TG_TC_NL"].FieldName.ToString()]);
                Double dCOT_1 = Convert.ToDouble(dr[grvData.Columns["COT_1"].FieldName.ToString()]);
                Double dCOT_2 = Convert.ToDouble(dr[grvData.Columns["COT_2"].FieldName.ToString()]);
                Double dCOT_3 = Convert.ToDouble(dr[grvData.Columns["COT_3"].FieldName.ToString()]);
                Double dCOT_4 = Convert.ToDouble(dr[grvData.Columns["COT_4"].FieldName.ToString()]);
                Double dCOT_5 = Convert.ToDouble(dr[grvData.Columns["COT_5"].FieldName.ToString()]);
                Double dCOT_6 = Convert.ToDouble(dr[grvData.Columns["COT_6"].FieldName.ToString()]);
                Double dCOT_7 = Convert.ToDouble(dr[grvData.Columns["COT_7"].FieldName.ToString()]);

                Double dTong3Cot = dTG_HC + dTG_TC_NT + dTG_TC_CN + dTG_TC_NL;
                Double dTong7Cot = dCOT_1 + dCOT_2 + dCOT_3 + dCOT_4 + dCOT_5 + dCOT_6 + dCOT_7;
                if (dTong3Cot != dTong7Cot)
                {
                    errorCount++;
                    dr.SetColumnError("TG_HC", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("TG_TC_NT", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("TG_TC_CN", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("TG_TC_NL", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_1", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_2", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_3", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_4", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_5", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_6", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                    dr.SetColumnError("COT_7", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgGioLamViecKhongCan"));
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }

        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }
        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
        }
        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Trim().Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                        dr.SetColumnError(sCot, sTenKTra);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        #endregion

    }
}