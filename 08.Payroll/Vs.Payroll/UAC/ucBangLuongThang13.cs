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
using Microsoft.Office.Interop;

namespace Vs.Payroll
{
    public partial class ucBangLuongThang13 : DevExpress.XtraEditors.XtraUserControl
    {
        private bool ColFlag = false;  // false update cột PT_HQKD , true update cột PT_TL
        private int iLoai = 1; // 1 tính tổng lương else tính thuế TNCN
        public static ucBangLuongThang13 _instance;
        public static ucBangLuongThang13 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBangLuongThang13();
                return _instance;
            }
        }
        public ucBangLuongThang13()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucBangLuongThang13_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datNam.EditValue = DateTime.Now;
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();

            EnableButon(true);
            Commons.Modules.sLoad = "";
        }
        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangLuongT13", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboDonVi.EditValue), Convert.ToInt64(cboXiNghiep.EditValue), Convert.ToInt64(cboTo.EditValue), Convert.ToDateTime(datNam.EditValue).Year));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;

                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    grvData.Columns["T_1"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_1"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_2"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_2"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_3"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_3"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_4"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_4"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_5"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_5"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_6"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_6"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_7"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_7"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_8"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_8"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_9"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_9"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_10"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_10"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_11"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_11"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["T_12"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["T_12"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PT_TL"].DisplayFormat.FormatType = FormatType.Custom;
                    grvData.Columns["PT_TL"].DisplayFormat.FormatString = "#,##0.0";
                    grvData.Columns["LUONG_T13"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_T13"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PT_HQ_KD"].DisplayFormat.FormatType = FormatType.Custom;
                    grvData.Columns["PT_HQ_KD"].DisplayFormat.FormatString = "#,##0.0";
                    grvData.Columns["LUONG_T13"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_T13"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_HQ_KD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_HQ_KD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }
                lblTongNV.Text = Convert.ToString(grvData.RowCount);
            }
            catch
            {

            }
            //grvData.Columns["ID_BHXH_CD"].Visible = false;
            //grvData.Columns["THANG"].Visible = false;
            //grvData.Columns["ID_CN"].Visible = false;
            //grvData.Columns["TIEN_CD"].DisplayFormat.FormatType = FormatType.Numeric;
            //grvData.Columns["TIEN_CD"].DisplayFormat.FormatString = "N0";
            //lblTongNV.Text = Convert.ToString(grvData.RowCount);
            //grvData.Columns["THANG"].Visible = false;
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "tinhluong":
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhLuongThang13", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToDateTime(datNam.EditValue).Year));
                            grdData.DataSource = dt;
                            lblTongNV.Text = Convert.ToString(grvData.RowCount);
                            EnableButon(false);
                        }
                        catch 
                        {

                        }
                        break;
                    }
                case "sua":
                    {
                        txtPT_TT13.EditValue = string.Empty;
                        txtPT_THUONG_HQKD.EditValue = string.Empty;
                        EnableButon(false);
                        break;

                    }

                case "export_template":
                    {
                        try
                        {
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

                            DataTable dt = new DataTable();
                            dt = ((DataTable)grdData.DataSource).Copy();
                            dt.DefaultView.RowFilter = "";
                            dt.Columns.Add("SO_TIEN");
                            DataView dv = dt.DefaultView;

                            DataTable dt1 = new DataTable();
                            dt1 = dv.ToTable(false, "MS_CN", "SO_TIEN");
                            Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                            Ranges1.ColumnWidth = 20;
                            MExportExcel(dt1, excelWorkSheet, Ranges1);

                            excelApplication.Visible = true;
                            excelWorkbook.Save();
                        }
                        catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                        break;
                    }

                case "laydulieu":
                    {
                        try
                        {
                            if (grvData.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            frmLayDuLieuLuongT13 frm = new frmLayDuLieuLuongT13();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                DataTable dt1 = ((frmLayDuLieuLuongT13)frm).dt.Copy();
                                string TenCotSQL = frm.ColName;
                                if (dt1 == null || dt1.Rows.Count == 0) return;
                                DataTable dtTemp = new DataTable();
                                dtTemp = (DataTable)grdData.DataSource;

                                string sBT = "sBTExcel" + Commons.Modules.UserName;
                                string sBT1 = "sBTCongNhan" + Commons.Modules.UserName;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT1, dtTemp, "");

                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhapLuong", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT1;
                                cmd.Parameters.Add("@StrColName", SqlDbType.NVarChar).Value = TenCotSQL;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                grdData.DataSource = ds.Tables[0].Copy();
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception ex) { }
                        break;
                    }

                case "tinhtongluong":
                    {
                        if (grvData.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaCoDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (KiemTrong_grvData()) return;
                        iLoai = 1;
                        TinhTongLuongThueTNCN();
                        break;
                    }

                case "thueTNCN":
                    {
                        if (grvData.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaCoDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (KiemTrong_grvData()) return;
                        iLoai = 2;
                        TinhTongLuongThueTNCN();
                        break;
                    }

                case "ghi":
                    {
                        if (grvData.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaCoDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        EnableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        LoadData();
                        txtPT_TT13.EditValue = string.Empty;
                        txtPT_THUONG_HQKD.EditValue = string.Empty;
                        EnableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[11].Properties.Visible = visible;

            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = !visible;
            btnALL.Buttons[8].Properties.Visible = !visible;
            btnALL.Buttons[9].Properties.Visible = !visible;
            btnALL.Buttons[10].Properties.Visible = !visible;

            grvData.OptionsBehavior.Editable = !visible;

            cboTo.Enabled = visible;
            datNam.Enabled = visible;
            cboDonVi.Enabled = visible;
            cboXiNghiep.Enabled = visible;
            txtPT_TT13.Enabled = !visible;
            txtPT_THUONG_HQKD.Enabled = !visible;
        }
        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            //e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private bool Savedata()
        {
            string sTB = "sBTLuongT13" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveBangLuongT13", sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            //EnableButon(true);
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            //EnableButon(true);
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            //EnableButon(true);
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            //EnableButon(true);
        }

        private void txtPT_TT13_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPT_TT13.Text))
                {
                    XtraMessageBox.Show(ItemForThuong13.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtPT_TT13.Focus();
                    return;
                }
                ColFlag = true;
                UpdatePTAll();
            }
        }

        private void txtPT_THUONG_HQKD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPT_THUONG_HQKD.Text))
                {
                    XtraMessageBox.Show(ItemForThuongHieuQuaKD.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtPT_THUONG_HQKD.Focus();
                    return;
                }
                ColFlag = false;
                UpdatePTAll();
            }
        }

        private void UpdatePTAll()
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)grdData.DataSource;
                if (dt1 == null || dt1.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string sBT = "sBTBangLuong" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatPTLuongT13", conn);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@PT_TL", SqlDbType.Float).Value = string.IsNullOrEmpty(txtPT_TT13.EditValue.ToString()) ? -1 : Convert.ToDecimal(txtPT_TT13.EditValue);
                cmd.Parameters.Add("@PT_HQ_KD", SqlDbType.Float).Value = string.IsNullOrEmpty(txtPT_THUONG_HQKD.EditValue.ToString()) ? -1 : Convert.ToDecimal(txtPT_THUONG_HQKD.EditValue);
                cmd.Parameters.Add("@ColFlag", SqlDbType.Bit).Value = ColFlag;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                grdData.DataSource = ds.Tables[0].Copy();
            }
            catch (Exception ex)
            {

            }
        }

        private void TinhTongLuongThueTNCN()
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)grdData.DataSource;

                string sBT = "sBTBangTam" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhTongLuongThueTNCN", conn);
                cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = iLoai;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                grdData.DataSource = ds.Tables[0].Copy();
            }
            catch (Exception ex) { }
        }

        private bool KiemTrong_grvData()
        {
            try
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    //Kiểm trống theo từng cột
                    for (int j = 0; j < grvData.Columns.Count; j++)
                    {
                        if (grvData.Columns[j].FieldName == "PT_TL" && (grvData.GetRowCellValue(i, grvData.Columns[j])).ToString() == "")
                        {
                            XtraMessageBox.Show(grvData.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                            grvData.FocusedRowHandle = i;
                            grvData.FocusedColumn = grvData.Columns[j];
                            return true;
                        }

                        if (grvData.Columns[j].FieldName == "PT_HQ_KD" && (grvData.GetRowCellValue(i, grvData.Columns[j])).ToString() == "")
                        {
                            XtraMessageBox.Show(grvData.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                            grvData.FocusedRowHandle = i;
                            grvData.FocusedColumn = grvData.Columns[j];
                            return true;
                        }
                    }
                }
            }
            catch { return true; }
            return false;
        }

        private void grvData_MouseWheel(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
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