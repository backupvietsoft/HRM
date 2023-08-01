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
using DevExpress.CodeParser;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Vs.Payroll
{
    public partial class frmImportTinhLuong_TG : DevExpress.XtraEditors.XtraForm
    {
        Point ptChung;
        DataTable _table = new DataTable();
        public DateTime dtThang, dtDThang;
        public int iID_DV, iID_XN, iID_TO;

        public int iloai = 1;//1la thang ,2 lam tháng 13


        public frmImportTinhLuong_TG()
        {
            InitializeComponent();
            
        }
        private void btnFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
            }
            catch (Exception ex)
            { XtraMessageBox.Show(ex.Message); }
        }

        private void cboChonSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                DataTable dt = new DataTable();
                var source = new ExcelDataSource();
                source.FileName = btnFile.Text;
                var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text);
                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                source.Fill();
                dt = new DataTable();
                if(iloai == 1)
                {
                    dt.Columns["THUONG_HIEU_SUAT"].DataType = typeof(double);
                    dt = ToDataTable(source);
                }
                else
                {
                    dt = ToDataTable13(source);
                }
                dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);

                for (int i = 2; i < grvData.Columns.Count; i++)
                {
                    grvData.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns[i].DisplayFormat.FormatString = "N0";
                }
                Commons.Modules.ObjSystems.HideWaitForm();

            }
            catch
            {
                grdData.DataSource = null;
                Commons.Modules.ObjSystems.HideWaitForm();
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
                            this.Cursor = Cursors.WaitCursor;
                            grvData.PostEditor();
                            grvData.UpdateCurrentRow();
                            //Commons.Modules.ObjSystems.MChooseGrid(false, "XOA", grvData);
                            DataTable dtSource = (DataTable)grdData.DataSource;

                            if (cboChonSheet.Text == "" || dtSource == null || dtSource.Rows.Count <= 0)
                            {
                                this.Cursor = Cursors.Default;
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            grvData.Columns.View.ClearColumnErrors();
                            if (iloai == 1)
                                Import(dtSource);
                            else
                                Import13(dtSource);

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
            //int count = grvData.RowCount;
            //int col = 0;
            //int errorCount = 0;
            //#region kiểm tra dữ liệu
            //foreach (DataRow dr in dtSource.Rows)
            //{
            //    dr.ClearErrors();
            //    col = 0;
            //    //Mã số nhân viên
            //    string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
            //    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "CONG_NHAN", "MS_CN", true, this.Name))
            //    {
            //        errorCount++;
            //    }
            //    else
            //    {

            //        try
            //        {
            //            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.CONG_NHAN CN INNER JOIN dbo.[TO] T ON T.ID_TO = CN.ID_TO INNER JOIN dbo.XI_NGHIEP  XN ON XN.ID_XN = T.ID_XN WHERE CN.MS_CN = '" + sMaSo + "'")) == 0)
            //            {
            //                errorCount++;
            //                dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCongNhanKhongThuocNhaMay"));
            //                dr["XOA"] = 1;
            //            }
            //        }
            //        catch
            //        {
            //            errorCount++;
            //            dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaTonTaiCSDL"));
            //            dr["XOA"] = 1;
            //        }
            //    }

            //    //col = 3;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 4;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 5;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 6;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 7;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 8;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}
            //    //col = 9;
            //    //if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Số tiền", 0, 0, false, this.Name))
            //    //{
            //    //    errorCount++;
            //    //}


            //}
            //this.Cursor = Cursors.Default;
            //#endregion
            //Commons.Modules.ObjSystems.HideWaitForm();
            //if (errorCount != 0)
            //{
            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{
            DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSangImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(Commons.IConnections.CNStr);
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlTransaction sTrans = conn.BeginTransaction();
                try
                {
                    //tạo bảm tạm trên lưới
                    string sbt = "sBTLuongCN" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");


                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportExportLuong_TG", conn, sTrans);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                    cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = dtThang;
                    cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = dtDThang;
                    cmd.Parameters.Add("@SBT", SqlDbType.NVarChar).Value = sbt;
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    Commons.Modules.ObjSystems.XoaTable(sbt);
                    sTrans.Commit();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    sTrans.Rollback();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //}
        }

        private void Import13(DataTable dtSource)
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
                    string sbt = "sBTLuongCN" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");


                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportExportLuong13_TG", conn, sTrans);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = dtThang.Year;
                    cmd.Parameters.Add("@SBT", SqlDbType.NVarChar).Value = sbt;
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    sTrans.Commit();
                    Commons.Modules.ObjSystems.XoaTable(sbt);
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    sTrans.Rollback();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            //}
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
        private void frmImportTinhLuong_TG_Load(object sender, EventArgs e)
        {
            if (iloai == 2)
            {
                this.Name = "frmImportTinhLuong13_TG";
            }
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
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "MS_CN";
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "HO_TEN";
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "THUONG_HIEU_SUAT";
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "THUONG_C_HANH";
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "THUONG_HTNV";
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "TRO_CAP_CN";
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "TIEN_XANG";
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "THUONG";
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "KHAU_TRU_TAM_UNG";
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "KHAU_TRU";
                            break;
                        }
                    default:
                        {
                            table.Columns.Add(prop.Name, prop.PropertyType);
                            break;
                        }
                }
                table.Columns.Add(sTenCot.Trim(), (i == 0 || i == 1) ? typeof(string) : typeof(double));
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        if (props[i].GetValue(item) == null || props[i].GetValue(item).ToString().Trim() == "")
                        {
                            values[i] = null;
                        }
                        else
                        {
                            values[i] = nameType == "string" ? props[i].GetValue(item).ToString().Trim() : props[i].GetValue(item);
                        }

                    }
                    catch (Exception ex)
                    {
                        values[i] = null;
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

        public DataTable ToDataTable13(ExcelDataSource excelDataSource)
        {
            string nameType = "";
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string sTenCot = "";
                table.Columns.Add(sTenCot.Trim(), (i == 0 || i == 1) ? typeof(string) : typeof(double));
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        if (props[i].GetValue(item) == null || props[i].GetValue(item).ToString().Trim() == "")
                        {
                            values[i] = null;
                        }
                        else
                        {
                            values[i] = nameType == "string" ? props[i].GetValue(item).ToString().Trim() : props[i].GetValue(item);
                        }

                    }
                    catch (Exception ex)
                    {
                        values[i] = null;
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


        private void frmImportTinhLuong_TG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
        //private void ExportUngVien(string sPath)
        //{
        //    try
        //    {
        //        DataTable dtTmp = new DataTable();
        //        string SQL = "SELECT TOP 0 MS_UV AS  N'Mã số',HO AS N'Họ',TEN AS N'Tên',PHAI AS N'Giới tính',NGAY_SINH AS N'Ngày sinh',NOI_SINH AS N'Nơi sinh',SO_CMND AS N'CMND',NGAY_CAP AS N'Ngày cấp',NOI_CAP AS N'Nơi cấp',CONVERT(NVARCHAR(250), ID_TT_HN) AS N'Tình trạng HN',HO_TEN_VC AS N'Họ tên V/C',NGHE_NGHIEP_VC AS N'Nghề nghiệp V/C',SO_CON AS N'Số con',DT_DI_DONG AS N'Điện thoại',EMAIL AS N'Email',NGUOI_LIEN_HE AS N'Người liên hệ',QUAN_HE AS N'Quan hệ',DT_NGUOI_LIEN_HE AS N'ĐT Người liên hệ',CONVERT(NVARCHAR(250), ID_TP) AS N'Thành phố',CONVERT(NVARCHAR(250), ID_QUAN) AS N'Quận',CONVERT(NVARCHAR(250), ID_PX) AS N'Phường xã',THON_XOM AS N'Thôn xóm',DIA_CHI_THUONG_TRU AS N'Địa chỉ',CONVERT(NVARCHAR(250), ID_NTD) AS N'Nguồn tuyển',CONVERT(NVARCHAR(250), ID_CN) AS N'Người giới thiệu',CONVERT(NVARCHAR(250), TIENG_ANH) AS N'TIENG_ANH',CONVERT(NVARCHAR(250), TIENG_TRUNG) AS N'TIENG_TRUNG',CONVERT(NVARCHAR(250), TIENG_KHAC) AS N'TIENG_KHAC',CONVERT(NVARCHAR(250), ID_DGTN) AS N'Đánh giá tay nghề',CONVERT(NVARCHAR(250), VI_TRI_TD_1) AS N'Vị trí tuyển 1',CONVERT(NVARCHAR(250), VI_TRI_TD_2) AS N'Vị trí tuyển 2',NGAY_HEN_DI_LAM AS N'Ngày hẹn đi làm',XAC_NHAN_DL AS N'Xác nhận đi làm',NGAY_NHAN_VIEC AS N'Ngày nhận việc',XAC_NHAN_DTDH AS N'Xác nhận đào tạo định hướng',DA_CHUYEN AS N'Chuyển sang nhân sự',GHI_CHU AS N'Ghi chú',DA_GIOI_THIEU AS N'Đã giới thiệu',HUY_TUYEN_DUNG AS N'Hủy tuyển dụng'FROM dbo.UNG_VIEN";

        //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

        //        //export datatable to excel
        //        Workbook book = new Workbook();
        //        Worksheet sheet1 = book.Worksheets[0];
        //        sheet1.Name = "01-Danh sách ứng viên";
        //        sheet1.DefaultColumnWidth = 20;

        //        sheet1.InsertDataTable(dtTmp, true, 1, 1);

        //        sheet1.Range[2, 1].Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();

        //        sheet1.Range[1, 1, 1, 39].Style.WrapText = true;
        //        sheet1.Range[1, 1, 1, 39].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet1.Range[1, 1, 1, 39].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet1.Range[1, 1, 1, 39].Style.Font.IsBold = true;

        //        sheet1.Range[1, 1].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 2].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 3].Style.Font.Color = Color.Red;
        //        sheet1.Range[1, 30].Style.Font.Color = Color.Red;


        //        sheet1.Range[1, 1].Comment.RichText.Text = "Mã ứng viên sẽ được đặt theo cấu trúc MUV-000001 trong đó(MUV-: cố định,còn 000001 sẽ được tăng thêm 1 khi có một ứng viên mới).";
        //        sheet1.Range[1, 4].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataPhai());
        //        sheet1.Range[1, 10].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataTinHTrangHN(false));
        //        sheet1.Range[1, 19].Comment.RichText.Text = "Nhập đúng cấp tỉnh/thành phố trong danh mục.";
        //        sheet1.Range[1, 20].Comment.RichText.Text = "Nhập đúng cấp quận/huyện trong danh mục.";
        //        sheet1.Range[1, 21].Comment.RichText.Text = "Nhập đúng cấp phường/xã trong danh mục.";
        //        sheet1.Range[1, 24].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataNguonTD(false));
        //        sheet1.Range[1, 25].Comment.RichText.Text = "Họ và tên nhân viên trong công ty giới thiệu.";

        //        sheet1.Range[1, 26].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataMucDoTieng(false));
        //        sheet1.Range[1, 27].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataMucDoTieng(false));
        //        //sheet1.Range[1, 28].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataKinhNghiemLV(false));
        //        sheet1.Range[1, 29].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false));

        //        sheet1.Range[1, 30].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)));
        //        sheet1.Range[1, 31].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)));

        //        sheet1.Range[1, 33].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 35].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 36].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 38].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
        //        sheet1.Range[1, 39].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";

        //        sheet1.FreezePanes(2, 4);
        //        //Tên trường Từ năm	Đến năm	Xếp loại

        //        Worksheet sheet2 = book.Worksheets[1];
        //        sheet2.Name = "02-Bằng cấp";
        //        sheet2.DefaultColumnWidth = 20;

        //        sheet2.Range[1, 1].Text = "Mã số";
        //        sheet2.Range[1, 2].Text = "Tên bằng";
        //        sheet2.Range[1, 3].Text = "Tên trường";
        //        sheet2.Range[1, 4].Text = "Từ năm";
        //        sheet2.Range[1, 5].Text = "Đến năm";
        //        sheet2.Range[1, 6].Text = "Xếp loại";
        //        sheet2.Range[1, 6].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

        //        sheet2.Range[1, 1, 1, 6].Style.WrapText = true;
        //        sheet2.Range[1, 1, 1, 6].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet2.Range[1, 1, 1, 6].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet2.Range[1, 1, 1, 6].Style.Font.IsBold = true;


        //        Worksheet sheet3 = book.Worksheets[2];
        //        sheet3.Name = "03-Kinh nghiệm làm việc";
        //        sheet3.DefaultColumnWidth = 20;

        //        sheet3.Range[1, 1].Text = "Mã số";
        //        sheet3.Range[1, 2].Text = "Tên công ty";
        //        sheet3.Range[1, 3].Text = "Chức vụ";
        //        sheet3.Range[1, 4].Text = "Mức lương";
        //        sheet3.Range[1, 5].Text = "Từ năm";
        //        sheet3.Range[1, 6].Text = "Đến năm";
        //        sheet3.Range[1, 7].Text = "Lý do nghĩ";

        //        sheet3.Range[1, 1, 1, 7].Style.WrapText = true;
        //        sheet3.Range[1, 1, 1, 7].Style.VerticalAlignment = VerticalAlignType.Center;
        //        sheet3.Range[1, 1, 1, 7].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        sheet3.Range[1, 1, 1, 7].Style.Font.IsBold = true;

        //        //Worksheet sheet4 = book.Worksheets.Add("04-Thông tin khác");
        //        //sheet4.DefaultColumnWidth = 20;

        //        //sheet4.Range[1, 1].Text = "Mã số";
        //        //sheet4.Range[1, 2].Text = "Nội dung";
        //        //sheet4.Range[1, 3].Text = "Xếp loại";

        //        //sheet4.Range[1, 3].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

        //        //sheet4.Range[1, 1, 1, 3].Style.WrapText = true;
        //        //sheet4.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
        //        //sheet4.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;
        //        //sheet4.Range[1, 1, 1, 3].Style.Font.IsBold = true;

        //        book.SaveToFile(sPath);
        //        System.Diagnostics.Process.Start(sPath);
        //    }
        //    catch
        //    {
        //    }
        //}
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
    }
}
