﻿using DevExpress.DataAccess.Excel;
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
using System.Threading;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;

namespace Vs.Payroll
{
    public partial class frmImportCDChinhSua : DevExpress.XtraEditors.XtraForm
    {
        Point ptChung;
        DataTable _table = new DataTable();
        public DateTime dtThang;

        public frmImportCDChinhSua()
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
            try
            {
                DataTable dt = new DataTable();
                var source = new ExcelDataSource();
                source.FileName = btnFile.Text;
                var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text, "A7:O5000");
                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                source.Fill();
                dt = new DataTable();
                dt = ToDataTable(source);
                dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                dt.Columns["XOA"].ReadOnly = false;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTCongDoan" + Commons.Modules.iIDUser, dt, "");
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM " + "sBTCongDoan" + Commons.Modules.iIDUser + " WHERE ISNULL(SAN_LUONG_NHAP,0) <> 0 "));
                Commons.Modules.ObjSystems.XoaTable("sBTCongDoan" + Commons.Modules.iIDUser);
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
            }
            catch (Exception ex)
            {
                grdData.DataSource = null;
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {

                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                //Commons.Modules.ObjSystems.ShowWaitForm(this);
                switch (btn.Tag.ToString())
                {
                    case "import":
                        {
                            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[TO] WHERE ID_TO = (SELECT ID_TO FROM dbo.[TO] WHERE TEN_TO = N'" + cboChonSheet.Text + "')")) == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuyenThucHienKhongTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            this.Cursor = Cursors.WaitCursor;
                            grvData.PostEditor();
                            grvData.UpdateCurrentRow();
                            Commons.Modules.ObjSystems.MChooseGrid(false, "XOA", grvData);
                            //DataTable dtSource = Commons.Modules.ObjSystems.ConvertDatatable(grvData);

                            DataTable dtSource = (DataTable)grdData.DataSource;

                            if (cboChonSheet.Text == "" || dtSource == null || dtSource.Rows.Count <= 0)
                            {
                                this.Cursor = Cursors.Default;
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            grvData.Columns.View.ClearColumnErrors();
                            Import(dtSource);

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
                            Commons.Modules.ObjSystems.setCheckImport(0); //xoa
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {
                this.Cursor = Cursors.Default;
            }
        }
        #region import
        private void Import(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            int errorMS = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 1;
                //Mã số nhân viên
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "CONG_NHAN", "MS_CN", true, this.Name))
                {
                    errorCount++;
                }

                col = 3;
                //Mã số nhân viên
                string sTo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTo, "[TO]", "TEN_TO", true, this.Name))
                {
                    errorCount++;
                }
                col = 4;
                //ID_ORD
                string sTenHH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTenHH, "DON_HANG_BAN_ORDER", "TEN_HH", true, this.Name))
                {
                    errorCount++;
                }

                col = 5;
                //ID_ORD
                string sMaQL = dr[grvData.Columns[col].FieldName.ToString()].ToString();

                if (string.IsNullOrEmpty(sMaQL))
                {
                    dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    errorCount++;
                }
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_TO = (SELECT ID_TO FROM dbo.[TO] WHERE TEN_TO = N'" + cboChonSheet.Text + "') AND ID_ORD = (SELECT ID_ORD FROM dbo.DON_HANG_BAN_ORDER WHERE TEN_HH = N'" + sTenHH + "') AND MaQL = '" + sMaQL + "'")) == 0)
                {
                    dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaTonTaiCSDL"));
                    dr["XOA"] = 1;
                    errorCount++;
                }
            }
            this.Cursor = Cursors.Default;
            #endregion

            Commons.Modules.ObjSystems.HideWaitForm();
            int errorEmpty = 0;
            int errorExist = 0;
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

                    string sTB = "LK_Tam" + Commons.Modules.UserName;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportCongDoan", conn);
                        cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                        cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                        cmd.Parameters.AddWithValue("@TEN_CHUYEN", cboChonSheet.Text.Trim());
                        cmd.Parameters.AddWithValue("@sBT", sTB);
                        cmd.Parameters.AddWithValue("@Ngay", dtThang);
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        Commons.Modules.ObjSystems.XoaTable(sTB);
                        if (dt.Rows[0][1].ToString() == "-99")
                        {
                            XtraMessageBox.Show(dt.Rows[0][1].ToString());
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            grdData.DataSource = dtSource.Clone();
                        }
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sTB);
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

        }
        private void frmImportCDChinhSua_Load(object sender, EventArgs e)
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
            DevExpress.DataAccess.Native.Excel.DataView dv_temp = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;

            //excelDataSource.SourceOptions = new CsvSourceOptions() { CellRange = "A7:" + "M" + (dv_temp.Count + 7) + "" };
            //excelDataSource.SourceOptions.SkipEmptyRows = false;
            //excelDataSource.SourceOptions.UseFirstRowAsHeader = true;
            //excelDataSource.Fill();

            //DevExpress.DataAccess.Native.Excel.DataView dv = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;
            //for (int i = 0; i < dv.Count; i++)
            //{
            //    DevExpress.DataAccess.Native.Excel.ViewRow row = dv[i] as DevExpress.DataAccess.Native.Excel.ViewRow;
            //    foreach (DevExpress.DataAccess.Native.Excel.ViewColumn col in dv.Columns)
            //    {
            //        object val = col.GetValue(row);
            //    }
            //}

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
                            sTenCot = "TEN_TO";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "TEN_HH";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "MaQL";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "TEN_CD";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "TONG_SL_CA_NHAN_DA_KE";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "SAN_LUONG_NHAP";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "SAN_LUONG_SAU_DC";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 10:
                        {
                            sTenCot = "TONG_SL_CD";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 11:
                        {
                            sTenCot = "SAN_LUONG_CHOT_TINH_LUONG";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 12:
                        {
                            sTenCot = "SO_LUONG";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 13:
                        {
                            sTenCot = "DON_GIA";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    case 14:
                        {
                            sTenCot = "THANH_TIEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(double));
                            break;
                        }
                    default:
                        {
                            table.Columns.Add(prop.Name, prop.PropertyType);
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
                            values[i] = null;
                        }
                        else
                        {
                            if (i == 8 || i == 1)
                            {
                                values[i] = props[i].GetValue(item).ToString().Trim();
                            }
                            else
                            {
                                values[i] = props[i].GetValue(item);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmImportTienThuongPhuCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public bool KiemTonTai(GridView grvData, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, Boolean bKiemNull = true, string sform = "", double soTien = 0)
        {
            //null không kiểm
            if (bKiemNull)
            {//nếu null
                if (string.IsNullOrEmpty(sDLKiem) && soTien != 0)
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                //khác null
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + " = N'" + sDLKiem + "'")) == 0)
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + " = N'" + sDLKiem + "'")) == 0)
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(iCot).Trim().Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                    dr["XOA"] = 1;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                dr["XOA"] = 1;
                return false;
            }
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
