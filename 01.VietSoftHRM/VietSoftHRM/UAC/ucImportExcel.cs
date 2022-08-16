using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;
using DevExpress.XtraGrid;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Spire.Xls;
using DevExpress.XtraBars.Docking2010;

namespace VietSoftHRM
{
    public partial class ucImportExcel : DevExpress.XtraEditors.XtraUserControl
    {
        string fileName = "";
        Point ptChung;
        string ChuoiKT = "";
        string ChuoiKTMa = "";
        DataTable _table = new DataTable();
        DataTable dtemp;
        public ucImportExcel()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
        }
        private void ucImportExcel_Load(object sender, EventArgs e)
        {
            //load
            LoadComboMenu();
        }

        private void LoadComboMenu()
        {
            try
            {
                string sSql = "SELECT MS_IMPORT AS MA_SO, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN[TEN_IMPORT] WHEN 1 THEN[TEN_IMPORT_A] ELSE[TEN_IMPORT_H] END TEN FROM DS_IMPORT WHERE MS_IMPORT_CHA = MS_IMPORT AND[SU_DUNG] = 1 AND IMPORT = 1 ORDER BY MS_IMPORT";
                DataTable dt = new DataTable();
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboMenuImport, sSql, "MA_SO", "TEN", lblDanhMucImport.Text);
            }
            catch
            {
            }
        }

        private void LoadComboDanhMuc(string msCha)
        {
            try
            {
                string sSql = "SELECT [MS_IMPORT] AS MA_SO , CASE 0 WHEN 0 THEN [TEN_IMPORT] WHEN 1 THEN [TEN_IMPORT_A] ELSE [TEN_IMPORT_H] END TEN FROM[DS_IMPORT] T1 WHERE[SU_DUNG] = 1 AND IMPORT = 1 AND MS_IMPORT_CHA = " + msCha + " AND MS_IMPORT != " + msCha + " ORDER BY[MA_SO]";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboDanhMucImport, sSql, "MA_SO", "TEN", lblDanhMucImport.Text);
            }
            catch
            {
            }
        }

        private void cboMenuImport_EditValueChanged(object sender, EventArgs e)
        {
            LoadComboDanhMuc(cboMenuImport.EditValue.ToString());
        }

        private void txtChonFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MLoadExcel();
        }

        private void MLoadExcel()
        {
            try
            {
                OpenFileDialog oFile = new OpenFileDialog();
                oFile.Filter = "All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*";
                if (oFile.ShowDialog() != DialogResult.OK) return;


                fileName = oFile.FileName;
                btnFile.Text = fileName;
                if (!System.IO.File.Exists(fileName)) return;

                if (MGetSheetNames(fileName))
                {
                    cboChonSheet_EditValueChanged(null, null);
                }
                else
                {
                    grdData.DataSource = null;
                    cboChonSheet.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool MGetSheetNames(string sFilePath)
        {

            try
            {
                DataTable dt = new DataTable();
                DataColumn dtColID = new DataColumn();
                dtColID.DataType = System.Type.GetType("System.Int16");
                dtColID.ColumnName = "ID";
                dt.Columns.Add(dtColID);

                DataColumn dtColName = new DataColumn();
                dtColName.DataType = System.Type.GetType("System.String");
                dtColName.ColumnName = "Name";
                dt.Columns.Add(dtColName);

                dt.Rows.Add(-1, "");



                byte[] CSVBytes = File.ReadAllBytes(sFilePath);
                var excelStream = new MemoryStream(CSVBytes);
                string FileName = Path.GetFileName(sFilePath);
                var FileExt = Path.GetExtension(FileName);


                if (FileExt == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(excelStream);
                    for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                    {
                        string SheetName = hssfwb.GetSheetName(i);
                        if (!string.IsNullOrEmpty(SheetName))
                            dt.Rows.Add(i, SheetName);
                    }
                }
                else if (FileExt == ".xlsx")
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(excelStream);
                    for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                    {
                        string SheetName = hssfwb.GetSheetName(i);
                        if (!string.IsNullOrEmpty(SheetName))
                            dt.Rows.Add(i, SheetName);
                    }
                }

                Commons.Modules.sLoad = "0Load";
                if (dt.Rows.Count > 0)
                    Commons.Modules.ObjSystems.MLoadLookUpEdit(cboChonSheet, dt, "ID", "Name", "");

                Commons.Modules.sLoad = "";
                return true;
            }
            catch (Exception ex)
            {
                cboChonSheet.Properties.DataSource = null;
                Commons.Modules.sLoad = "";
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }

        }

        private void cboChonSheet_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return;
                if (Commons.Modules.sLoad == "0Load") return;
                if (string.IsNullOrEmpty(btnFile.Text)) return;
                this.grdData.DataSource = null;
                grvData.Columns.Clear();
                if (cboChonSheet.EditValue.ToString() == "-1")
                    return;

                this.Cursor = Cursors.WaitCursor;
                var FileExt = Path.GetExtension(btnFile.Text);
                _table = new DataTable();
                if (FileExt.ToLower() == ".xls")
                    _table = Commons.Modules.MExcel.MGetData2xls(btnFile.Text, cboChonSheet.EditValue.ToString());
                else if (FileExt.ToLower() == ".xlsx")
                    _table = Commons.Modules.MExcel.MGetData2xlsx(btnFile.Text, cboChonSheet.EditValue.ToString());



                dtemp = new DataTable();
                dtemp = _table;
                this.grdData.DataSource = null;
                grvData.Columns.Clear();
                if (_table != null)
                {
                    dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                    try
                    {
                        dtemp.DefaultView.Sort = "[" + dtemp.Columns[0].ColumnName.ToString() + "]";
                    }
                    catch { }

                    if (dtemp.Columns.Count <= 13)
                        Commons.Modules.ObjSystems.MLoadXtraGridIP(grdData, grvData, dtemp, true, true, false, false);
                    else
                        Commons.Modules.ObjSystems.MLoadXtraGridIP(grdData, grvData, dtemp, true, true, false, true);

                    grvData.BestFitColumns();

                    btnFile.Text = fileName;
                    try
                    {
                        groDLImport.Text = " Total : " + grvData.RowCount.ToString() + " row";
                    }
                    catch { }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
            }
        }


        public DataTable MGetData2xlsx(String Path, string sheet)
        {
            XSSFWorkbook wb;
            XSSFSheet sh;
            int i = 0;

            try
            {

                using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    wb = new XSSFWorkbook(fs);
                    fs.Close();
                }

                DataTable DT = new DataTable();
                DT.Rows.Clear();
                DT.Columns.Clear();
                System.Globalization.DateTimeFormatInfo dtF = new System.Globalization.DateTimeFormatInfo();
                // get sheet
                sh = (XSSFSheet)wb.GetSheetAt(int.Parse(sheet));

                i = 0;
                if (DT.Columns.Count < sh.GetRow(i).Cells.Count)
                {
                    for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                    {
                        var cell = sh.GetRow(i).GetCell(j);
                        try
                        {
                            if (sh.GetRow(i).GetCell(j).StringCellValue.ToString().ToUpper() == "STT")
                            { DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue, typeof(float)); }
                            else
                            {
                                DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue, typeof(string));
                            }
                        }
                        catch
                        { DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue + "F" + j.ToString(), typeof(string)); }
                    }
                }
                int iTongCot = sh.GetRow(i).Cells.Count;

                i = 1;
                while (sh.GetRow(i) != null)
                {
                    DT.Rows.Add();
                    // write row value
                    for (int j = 0; j < iTongCot; j++)
                    {

                        var cell = sh.GetRow(i).GetCell(j);

                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case NPOI.SS.UserModel.CellType.Numeric:

                                    try
                                    {
                                        string sFormat = cell.CellStyle.GetDataFormatString().ToUpper();
                                        if (sFormat.Contains("M") || sFormat.Contains("D") || sFormat.Contains("Y") || sFormat.Contains("H") || sFormat.Contains("M") || sFormat.Contains("S") || sFormat.Contains(":") || sFormat.Contains("/"))
                                        {
                                            DateTime dtNgay;
                                            try
                                            {
                                                //dtNgay = DateTime.Parse(cell.DateCellValue.ToString(), dtF, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                                dtNgay = cell.DateCellValue;
                                            }
                                            catch { DateTime.TryParse(cell.DateCellValue.ToString(), out dtNgay); }

                                            try
                                            {
                                                DT.Rows[i - 1][j] = dtNgay;
                                            }
                                            catch
                                            {
                                                DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            double dGTi = 0;
                                            sFormat = "0.000000";
                                            int index = sFormat.IndexOf(".");
                                            if (index > 0)
                                                dGTi = Math.Round(sh.GetRow(i).GetCell(j).NumericCellValue, sFormat.Substring(index).Length);
                                            else
                                                dGTi = sh.GetRow(i).GetCell(j).NumericCellValue;

                                            DT.Rows[i - 1][j] = dGTi;
                                        }


                                    }
                                    catch { DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue; }

                                    break;
                                case NPOI.SS.UserModel.CellType.Boolean:
                                    DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).BooleanCellValue.ToString();
                                    break;

                                default:
                                    try
                                    {
                                        DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).StringCellValue;
                                    }
                                    catch { }
                                    break;
                            }

                        }
                    }

                    i++;
                    #region prb
                    try
                    {
                    }
                    catch { }
                    #endregion
                }
                wb.Close();
                return DT;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + " - ROW : " + i.ToString());
                return null;
            }
        }

        #region kiểm dữ liệu

        private int CheckLen(DataRow dr, int col, int giatri, int chieudai, string thongbao)
        {
            try
            {
                if (dr[grvData.Columns[col].FieldName.ToString()] == DBNull.Value || dr[grvData.Columns[col].FieldName.ToString()].ToString() == String.Empty)
                { giatri += 1; }
                else
                    if (dr[grvData.Columns[col].FieldName.ToString()].ToString().Length > chieudai)
                {
                    dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), thongbao + " dài hơn " + chieudai + " ký tự." + "(" + dr[grvData.Columns[col].FieldName.ToString()].ToString().Length.ToString() + ")");
                    dr["XOA"] = 1;
                }
                else
                    giatri += 1;
                return giatri;
            }
            catch { return giatri; }
        }
        private bool KiemKyTu(string strInput, string strChuoi)
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

        private bool KiemDuLieu(DataRow dr, int iCot, Boolean bKiemNull, int iDoDaiKiem)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
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
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), "error");
                dr["XOA"] = 1;
                return false;
            }
            return true;
        }

        private bool KiemTrungDL(DataTable dt, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x[iCot].Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLCSDL");
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                        dr["XOA"] = 1;
                        return false;
                    }
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
        private bool KiemTonTai(DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, Boolean bKiemNull = true)
        {
            //null không kiểm
            if (bKiemNull)
            {//nếu null
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                //khác null
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + " = N'" + sDLKiem + "'")) == 0)
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaTonTaiCSDL"));
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
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaTonTaiCSDL"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
            return true;
        }

        private bool KiemDuLieuNgay(DataRow dr, int iCot, Boolean bKiemNull)
        {
            string sDLKiem;
            sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiNgay"));
                            dr["XOA"] = 1;
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
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiNgay"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiNgay"));
                dr["XOA"] = 1;
                return false;
            }
            return true;
        }

        private bool KiemDuLieuNgay(DataRow dr, int iCot, string sTenKTra, Boolean bKiemNull, string GTSoSanh, int iKieuSS)
        {
            // iKieuSS = 1 la so sanh = 
            // iKieuSS = 2 la so sanh nho hon giá trị so sanh
            // iKieuSS = 3 la so sanh nho hon hoac bang
            // iKieuSS = 4 la so sanh lon hon
            // iKieuSS = 5 la so sanh lon hon hoac bang
            try
            {
                string sDLKiem;
                sDLKiem = DateTime.Parse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                DateTime DLKiem;
                DateTime DLSSanh;
                DateTime.TryParse(GTSoSanh, out DLSSanh);

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được để trống");
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
                            dr["XOA"] = 1;
                            return false;
                        }
                        else
                        {
                            if (DateTime.Parse(GTSoSanh) != DateTime.Parse("01/01/1900"))
                            {
                                #region Giá trị so sánh
                                //iKieuSS = 1 la so sanh = 
                                if (iKieuSS == 1)
                                {
                                    if (DLKiem == DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 2 la so sanh nho hon giá trị so sanh
                                if (iKieuSS == 2)
                                {
                                    if (DLKiem < DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 3 la so sanh nho hon hoac bang
                                if (iKieuSS == 3)
                                {
                                    if (DLKiem <= DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn hay bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 4 la so sanh lon hon
                                if (iKieuSS == 4)
                                {
                                    if (DLKiem > DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 5 la so sanh lon hon hoac bang
                                if (iKieuSS >= 5)
                                {
                                    if (DLKiem < DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn hay bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                #endregion
                            }
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
                            dr["XOA"] = 1;
                            return false;
                        }
                        else
                        {
                            if (GTSoSanh != "01/01/1900")
                            {
                                #region Giá trị so sánh
                                //iKieuSS = 1 la so sanh = 
                                if (iKieuSS == 1)
                                {
                                    if (DLKiem == DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 2 la so sanh nho hon giá trị so sanh
                                if (iKieuSS == 2)
                                {
                                    if (DLKiem < DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 3 la so sanh nho hon hoac bang
                                if (iKieuSS == 3)
                                {
                                    if (DLKiem <= DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn hay bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 4 la so sanh lon hon
                                if (iKieuSS == 4)
                                {
                                    if (DLKiem > DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                // iKieuSS = 5 la so sanh lon hon hoac bang
                                if (iKieuSS >= 5)
                                {
                                    if (DLKiem < DLSSanh)
                                    {
                                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn hay bằng " + DLSSanh.ToShortDateString());
                                        dr["XOA"] = 1;
                                        return false;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
                dr["XOA"] = 1;
                return false;
            }
            return true;
        }

        private bool KiemDuLieuSo(DataRow dr, int iCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull)
        {
            string sDLKiem;
            sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(),  Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(),  Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiSo"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[grvData.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiSo"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[grvData.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }

        private bool KiemDuLieuBool(DataRow dr, int iCot, string sTenKTra, string GTMacDinh)
        {
            if (string.IsNullOrEmpty(sTenKTra))
            {
                dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                sTenKTra = GTMacDinh.ToString();
                dr[grvData.Columns[iCot].FieldName.ToString()] = sTenKTra;

            }

            if (!string.IsNullOrEmpty(sTenKTra))
            {
                try
                {
                    sTenKTra = sTenKTra.Trim() == "1" ? "True" : "False";
                }
                catch
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "KhongPhaiKieuBool"));
                    dr["XOA"] = 1;
                    return false; ;
                }
            }
            return true;
        }

        private bool KiemDuLieuSo(DataRow dr, int iCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, double GTTKhoang)
        {
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sTenKTra))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (!double.TryParse(sTenKTra, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiSo"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh || DLKiem > GTTKhoang)
                            {
                                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongNhoHon") +
                                    GTSoSanh.ToString() + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgVaLonHon") + GTTKhoang.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sTenKTra) && GTMacDinh != -999999)
                {
                    dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sTenKTra = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sTenKTra))
                {
                    if (!double.TryParse(sTenKTra, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongPhaiSo"));
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh || DLKiem > GTTKhoang)
                            {
                                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongNhoHon") +
                                       GTSoSanh.ToString() + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgVaLonHon") + GTTKhoang.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            switch (btn.Tag.ToString())
            {
                case "export":
                    {
                        string sPath = "";
                        sPath = "";
                        //sPath = Commons.Modules.MExcel.SaveFiles("Excel Files (*.xlsx;)|*.xlsx;|" + "All Files (*.*)|*.*");
                        sPath = Commons.Modules.MExcel.SaveFiles("Excel Files (*.xls;)|*.xls;|Excel Files (*.Xlsx;)|*.Xlsx;|" + "All Files (*.*)|*.*");
                        if (sPath == "") return;
                        Workbook book = new Workbook();
                        Worksheet sheet = book.Worksheets[0];
                        DataTable dtTmp = new DataTable();
                        int iSheet = int.Parse(cboDanhMucImport.EditValue.ToString());
                        switch (iSheet)
                        {
                            case 1:
                                {
                                    //export đơn vị
                                    ExportDonVi(sPath);
                                    break;
                                }
                            case 2:
                                {
                                    //export xi ngiep
                                    ExporXiNghiep(sPath);
                                    break;
                                }
                            case 3:
                                {
                                    //export tổ
                                    ExportTo(sPath);
                                    break;
                                }
                            case 4:
                                {
                                    //export loại chức vụ
                                    ExportLoaiChucVu(sPath);
                                    break;
                                }
                            case 5:
                                {
                                    //export chức vụ
                                    ExportChucVu(sPath);
                                    break;
                                }
                            case 6:
                                {
                                    //export tình trạng hợp đồng
                                    ExportTinhTrangHD(sPath);
                                    break;
                                }
                            case 7:
                                {
                                    //export tình trạng hiện tại
                                    ExportTinhTrangHT(sPath);
                                    break;
                                }
                            case 8:
                                {
                                    //export chế độ nghĩ
                                    ExportCheDoNghi(sPath);
                                    break;
                                }
                            case 9:
                                {
                                    //export lý do nghĩ
                                    ExportLyDoNghi(sPath);
                                    break;
                                }
                            case 10:
                                {
                                    //export Loại công việc
                                    ExportLoaiCongViec(sPath);
                                    break;
                                }
                            case 11:
                                {
                                    //export loại hợp đồng lao động
                                    ExportLoaiHDLD(sPath);
                                    break;
                                }
                            case 12:
                                {
                                    //export người ký
                                    ExportNguoiKy(sPath);
                                    break;
                                }
                            case 13:
                                {
                                    //export ngạch lương
                                    ExportNgachLuong(sPath);
                                    break;
                                }
                            case 14:
                                {
                                    //export bậc lương
                                    ExportBacLuong(sPath);
                                    break;
                                }
                            case 15:
                                {
                                    //export Khen thưởng kỹ thuật
                                    ExportLoaiKTKL(sPath);
                                    break;
                                }
                            case 20:
                                {
                                    //export nhóm chấm công
                                    ExportNhomChamCong(sPath);
                                    break;
                                }
                            case 21:
                                {
                                    //export ngày nghĩ lể
                                    ExportNgayNghiLe(sPath);
                                    break;
                                }
                            case 22:
                                {
                                    //export ngày nghĩ lể
                                    ExportNoiDungQuiDinh(sPath);
                                    break;
                                }
                            case 30:
                                {
                                    //export đơn gia giây
                                    ExportDonGiaGiay(sPath);
                                    break;
                                }
                            case 31:
                                {
                                    //export Loại máy
                                    ExportLoaiMay(sPath);
                                    break;
                                }
                            case 32:
                                {
                                    //export Cụm
                                    ExportCum(sPath);
                                    break;
                                }
                            case 33:
                                {
                                    //export Hệ số bậc thợ
                                    ExportBacTho(sPath);
                                    break;
                                }
                            case 34:
                                {
                                    //export Chuyền
                                    ExportChuyen(sPath);
                                    break;
                                }
                            default: break;
                        }
                        Commons.Modules.ObjSystems.HideWaitForm();
                        break;
                    }
                case "import":
                    {
                        try
                        {
                            grvData.PostEditor();
                            grvData.UpdateCurrentRow();
                            Commons.Modules.ObjSystems.MChooseGrid(false, "XOA", grvData);
                            DataTable dtSource = Commons.Modules.ObjSystems.ConvertDatatable(grdData);
                            if (cboDanhMucImport.Text == "" || dtSource == null || dtSource.Rows.Count <= 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            grvData.Columns.View.ClearColumnErrors();
                            int iSheet = int.Parse(cboDanhMucImport.EditValue.ToString());
                            switch (iSheet)
                            {
                                case 1:
                                    {
                                        //import đơn vị
                                        ImportDonVi(dtSource);
                                        break;
                                    }
                                case 2:
                                    {
                                        //import xí nghiệp
                                        ImportXiNghiep(dtSource);
                                        break;
                                    }
                                case 3:
                                    {
                                        //import tổ
                                        ImportTo(dtSource);
                                        break;
                                    }

                                case 4:
                                    {
                                        //import Loại chức vụ
                                        ImportLoaiChucVu(dtSource);
                                        break;
                                    }
                                case 5:
                                    {
                                        //import chức vụ
                                        ImportChucVu(dtSource);
                                        break;
                                    }
                                case 6:
                                    {
                                        //import tình trạng hợp đồng
                                        ImportTinhTrangHD(dtSource);
                                        break;
                                    }
                                case 7:
                                    {
                                        //import tình trạng hiện tại
                                        ImportTinhTrangHT(dtSource);
                                        break;
                                    }
                                case 8:
                                    {
                                        //import chế độ nghĩ
                                        ImportCheDoNghi(dtSource);
                                        break;
                                    }
                                case 9:
                                    {
                                        //import lý do nghĩ
                                        ImportLyDoNghi(dtSource);
                                        break;
                                    }
                                case 10:
                                    {
                                        //import Loại công việc
                                        ImportLoaiCongViec(dtSource);
                                        break;
                                    }
                                case 11:
                                    {
                                        //import loại hợp đồng lao động
                                        ImportLoaiHDLD(dtSource);
                                        break;
                                    }
                                case 12:
                                    {
                                        //import người ký
                                        ImportNguoiKy(dtSource);
                                        break;
                                    }
                                case 13:
                                    {
                                        //import ngạch lương
                                        ImportNgachLuong(dtSource);
                                        break;
                                    }
                                case 14:
                                    {
                                        //import bậc lương
                                        ImportBacLuong(dtSource);
                                        break;
                                    }
                                case 15:
                                    {
                                        //import khen thưởng kỹ luật
                                        ImportLoaiKTKL(dtSource);
                                        break;
                                    }
                                case 16:
                                    {
                                        //import nhóm chấm công
                                        ImportNhomChamCong(dtSource);
                                        break;
                                    }
                                case 20:
                                    {
                                        //import nhóm chấm công
                                        ImportNhomChamCong(dtSource);
                                        break;
                                    }
                                case 21:
                                    {
                                        //import ngày nghĩ lể
                                        ImportNgayNghiLe(dtSource);
                                        break;
                                    }
                                case 22:
                                    {
                                        //import ngày nghĩ lể
                                        ImportNoiDungQuiDinh(dtSource);
                                        break;
                                    }
                                case 30:
                                    {
                                        //import đơn gia giây
                                        ImportDonGiaGiay(dtSource);
                                        break;
                                    }
                                case 31:
                                    {
                                        //import loại máy
                                        ImportLoaiMay(dtSource);
                                        break;
                                    }
                                case 32:
                                    {
                                        //import Cụm
                                        ImportCum(dtSource);
                                        break;
                                    }
                                case 33:
                                    {
                                        //import Cụm
                                        ImportBacTho(dtSource);
                                        break;
                                    }
                                case 34:
                                    {
                                        //import chuyền
                                        ImportChuyen(dtSource);
                                        break;
                                    }
                                default: break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.HideWaitForm();
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportKhongThanhCong") + " error(" + ex.ToString() + ")", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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

        private void GrvData_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            throw new NotImplementedException();
        }
        #region nhân sự

        #region Đơn vị
        private void ImportDonVi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //mã số đơn vị
                string MsDonVI = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, MsDonVI, "DON_VI", "MSDV"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên đơn vị
                string TenDV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, TenDV, "DON_VI", "TEN_DV"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //tên đơn vị anh
                string TenDVA = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, TenDVA, "DON_VI", "TEN_DV_A"))
                    {
                        errorCount++;
                    }
                }

                col = 3;
                //tên ngắn
                if (!KiemDuLieu(dr, col, false, 50))
                {
                    errorCount++;
                }

                col = 4;
                //địa chỉ
                if (!KiemDuLieu(dr, col, true, 200))
                {
                    errorCount++;
                }

                col = 5;
                //điện thoại
                if (!KiemDuLieu(dr, col, false, 100))
                {
                    errorCount++;
                }
                col = 6;
                //Fax
                if (!KiemDuLieu(dr, col, false, 50))
                {
                    errorCount++;
                }

                col = 7;
                //Số tài khoản
                if (!KiemDuLieu(dr, col, false, 100))
                {
                    errorCount++;
                }
                col = 8;
                //ngân hàng
                if (!KiemDuLieu(dr, col, false, 255))
                {
                    errorCount++;
                }
                col = 9;
                //tỉnh thành
                if (!KiemDuLieu(dr, col, false, 100))
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
                    string sbt = "sBTDV" + Commons.Modules.UserName;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.DON_VI(MSDV,TEN_DV,TEN_DV_A,TEN_NGAN,DIA_CHI,DIEN_THOAI,FAX,SO_TAI_KHOAN,TEN_NGAN_HANG,TINH_THANH)SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],[" + grvData.Columns[3].FieldName.ToString() + "],[" + grvData.Columns[4].FieldName.ToString() + "],[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],[" + grvData.Columns[7].FieldName.ToString() + "],[" + grvData.Columns[8].FieldName.ToString() + "],[" + grvData.Columns[9].FieldName.ToString() + "] FROM " + sbt;
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void ExportDonVi(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  MSDV as [Mã ĐV],TEN_DV as [Tên ĐV],TEN_DV_A as [Tên ĐVA],TEN_NGAN as [Tên Ngắn],DIA_CHI as [Địa Chỉ],DIEN_THOAI as [Điện Thoại],FAX,SO_TAI_KHOAN as [Số Tài Khoản],TEN_NGAN_HANG as [Tên Ngân Hàng],TINH_THANH as [Tỉnh Thành] FROM dbo.DON_VI";

            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 10].Style.WrapText = true;
            sheet.Range[1, 1, 1, 10].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 10].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 10].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 5].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Xí nghiệp
        private void ImportXiNghiep(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //đơn vị
                string stenDV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, stenDV, "DON_VI", "TEN_DV"))
                {
                    errorCount++;
                }
                col = 1;
                //mã xí nghiệp
                string sMaSN = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaSN, "XI_NGHIEP", "MS_XN"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //tên Xí ngiệp
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 3;
                //tên xí nghiệp anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 4;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTXN" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.XI_NGHIEP(ID_DV, MS_XN, TEN_XN, TEN_XN_A, STT_XN) SELECT (SELECT ID_DV FROM dbo.DON_VI WHERE TEN_DV = [" + grvData.Columns[0].FieldName.ToString() + "]),[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "] ,[" + grvData.Columns[3].FieldName.ToString() + "],[" + grvData.Columns[4].FieldName.ToString() + "] FROM " + sbt;
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
        private void ExporXiNghiep(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = " SELECT  A.TEN_DV as [Tên ĐV], B.MS_XN as [Mã XN], B.TEN_XN as [Tên XN], B.TEN_XN_A as [Tên XNA], B.STT_XN as STT FROM dbo.DON_VI A INNER JOIN dbo.XI_NGHIEP B ON B.ID_DV = A.ID_DV ORDER BY B.STT_XN";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 5].Style.WrapText = true;
            sheet.Range[1, 1, 1, 5].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 5].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 5].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 3].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }


        #endregion

        #region Tổ
        private void ImportTo(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên đơn vị
                string stenDV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, stenDV, "DON_VI", "TEN_DV"))
                {
                    errorCount++;
                }
                col = 1;
                //tên xí ngiệp
                string stenXN = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, stenXN, "XI_NGHIEP", "TEN_XN"))
                {
                    errorCount++;
                }

                col = 2;
                //mã tổ
                string sMaTo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaTo, "TO", "MS_TO"))
                    {
                        errorCount++;
                    }
                }
                col = 3;
                //tên tổ
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 4;
                //phân bổ
                if (!KiemDuLieu(dr, col, false, 100))
                {
                    errorCount++;
                }
                col = 5;
                //tên tổ anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 6;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTTo" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.[TO](ID_XN,MS_TO,TEN_TO,TEN_TO_A,STT_TO,PHAN_BO)SELECT  (SELECT TOP 1 ID_XN FROM dbo.XI_NGHIEP WHERE ID_DV = (SELECT TOP 1 ID_DV FROM dbo.DON_VI WHERE TEN_DV = A.[" + grvData.Columns[0].FieldName.ToString() + "]) AND TEN_XN = A.[" + grvData.Columns[1].FieldName.ToString() + "]),A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "] FROM " + sbt + " as A";
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

        private void ExportTo(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  A.TEN_DV AS [Tên ĐV],B.TEN_XN AS [Tên XN],C.MS_TO AS [Mã Tổ],C.TEN_TO AS [Tên Tổ],C.PHAN_BO AS [Phân Bổ],C.TEN_TO_A AS [Tên Tổ A],C.STT_TO AS [STT] FROM dbo.DON_VI A INNER JOIN dbo.XI_NGHIEP B ON B.ID_DV = A.ID_DV INNER JOIN dbo.[TO] C ON C.ID_XN = B.ID_XN";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 7].Style.WrapText = true;
            sheet.Range[1, 1, 1, 7].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 7].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 7].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 3].Style.Font.Color = Color.Red;
            sheet.Range[1, 4].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Loại chức vụ
        private void ImportLoaiChucVu(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //tên loại chức vụ
                string sTenLoaiCV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenLoaiCV, "LOAI_CHUC_VU", "TEN_LOAI_CV"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTLoaiCV" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.LOAI_CHUC_VU(TEN_LOAI_CV) SELECT [" + grvData.Columns[0].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportLoaiChucVu(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  TEN_LOAI_CV AS [Tên Loại CV] FROM dbo.LOAI_CHUC_VU ORDER BY TEN_LOAI_CV";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 1].Style.WrapText = true;
            sheet.Range[1, 1, 1, 1].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region chứ vụ
        private void ImportChucVu(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên loại chức vụ
                string sLoaiChucVu = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sLoaiChucVu, "LOAI_CHUC_VU", "TEN_LOAI_CV"))
                {
                    errorCount++;
                }
                col = 1;
                //mã chức vụ
                string sMaChucVu = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaChucVu, "CHUC_VU", "MS_CV"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //tên chức vụ
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 3;
                //tên chức vụ anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 4;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTChucVu" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO	dbo.CHUC_VU(MS_CV,TEN_CV,TEN_CV_A,ID_LOAI_CV,STT_IN_CV)SELECT A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],(SELECT ID_LOAI_CV FROM dbo.LOAI_CHUC_VU WHERE TEN_LOAI_CV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[4].FieldName.ToString() + "] FROM " + sbt + " A";
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
        private void ExportChucVu(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  A.TEN_LOAI_CV AS [Loại chức vụ],B.MS_CV AS [Mã chức vụ],B.TEN_CV AS [Tên chức vụ],B.TEN_CV_A AS [Tên chức vụ A],B.STT_IN_CV AS [STT] FROM dbo.LOAI_CHUC_VU A INNER JOIN dbo.CHUC_VU B ON B.ID_LOAI_CV = A.ID_LOAI_CV";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 5].Style.WrapText = true;
            sheet.Range[1, 1, 1, 5].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 5].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 5].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 3].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Tình trạng hợp đồng
        private void ImportTinhTrangHD(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên tình trạng
                string sTenTTHD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenTTHD, "TINH_TRANG_HD", "TEN_TT_HD"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên tình trạng anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 2;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTTTHD" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.TINH_TRANG_HD(TEN_TT_HD,TEN_TT_HD_A,STT) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "] FROM " + sbt;
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
        private void ExportTinhTrangHD(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT TEN_TT_HD AS[Tên Tình trạng HĐ],TEN_TT_HD_A AS[Tên Tình trạng HĐ Anh],STT FROM dbo.TINH_TRANG_HD";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 3].Style.WrapText = true;
            sheet.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 3].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Tình trạng hiện tại
        private void ImportTinhTrangHT(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên tình trạng
                string sTenTTHT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenTTHT, "TINH_TRANG_HT", "TEN_TT_HT"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên tình trạng anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 2;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTTTHT" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.TINH_TRANG_HT(TEN_TT_HT,TEN_TT_HT_A,STT) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "] FROM " + sbt;
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
        private void ExportTinhTrangHT(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT TEN_TT_HT AS [Tên TTHT],TEN_TT_HT_A AS [Tên TTHT A],STT FROM dbo.TINH_TRANG_HT";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 3].Style.WrapText = true;
            sheet.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 3].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Chế độ nghĩ việc
        private void ImportCheDoNghi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //tên loại chức vụ
                string sTenCheDoNghi = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenCheDoNghi, "CHE_DO_NGHI", "TEN_CHE_DO"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTCheDoNghi" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.CHE_DO_NGHI(TEN_CHE_DO) SELECT [" + grvData.Columns[0].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportCheDoNghi(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  TEN_CHE_DO AS [Tên chế độ] FROM dbo.CHE_DO_NGHI ORDER BY TEN_CHE_DO";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 1].Style.WrapText = true;
            sheet.Range[1, 1, 1, 1].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Lý do nghĩ
        private void ImportLyDoNghi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên chế độ nghĩ
                string sTenCheDo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTenCheDo, "CHE_DO_NGHI", "TEN_CHE_DO"))
                {
                    errorCount++;
                }
                col = 1;
                //mã lý do nghĩ
                string sMaLDN = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaLDN, "LY_DO_VANG", "MS_LDV"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //tên lý do nghĩ
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 3;
                //tên chức vụ anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 4;
                //tính phép
                string sTinhPhep = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sTinhPhep, "0"))
                {
                    errorCount++;
                }
                col = 5;
                //tính bảo hiểm 
                string sTinhBH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sTinhBH, "0"))
                {
                    errorCount++;
                }
                col = 6;
                //phần trăm hưởng bảo hiểm
                string sPhanTram = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sPhanTram, 0, 100, false, 100))
                {
                    errorCount++;
                }
                col = 7;
                //tính lương
                string sTinhLuong = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sTinhLuong, "0"))
                {
                    errorCount++;
                }
                col = 8;
                //tình trạng hiện tại
                string sTinhTrang = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTinhTrang, "TINH_TRANG_HT", "TEN_TT_HT", false))
                {
                    errorCount++;
                }
                col = 9;
                //stt
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
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
                        string sbt = "sBTLyDoNghi" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.LY_DO_VANG(ID_CHE_DO,MS_LDV,TEN_LDV,TEN_LDV_A,PHEP,TINH_BHXH,PHAN_TRAM_TRO_CAP,TINH_LUONG,ID_TT_HT,STT_LDV)SELECT(SELECT ID_CHE_DO FROM dbo.CHE_DO_NGHI WHERE TEN_CHE_DO = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "],A.[" + grvData.Columns[7].FieldName.ToString() + "],CASE WHEN ISNULL(A.[" + grvData.Columns[8].FieldName.ToString() + "],'') = '' THEN NULL ELSE(SELECT ID_TT_HT FROM dbo.TINH_TRANG_HT WHERE TEN_TT_HT = A.[" + grvData.Columns[8].FieldName.ToString() + "]) END,A.[" + grvData.Columns[9].FieldName.ToString() + "] FROM " + sbt + " AS A";
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
        private void ExportLyDoNghi(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  B.TEN_CHE_DO AS [Tên Chế Độ],A.MS_LDV AS [Mã LDV],A.TEN_LDV AS [Tên LDV],A.TEN_LDV_A AS [Tên LDV A],A.PHEP AS [Phép],A.TINH_BHXH AS [Tính BHXH],A.PHAN_TRAM_TRO_CAP AS [PT hưởng BHXH],A.TINH_LUONG AS [Tính lương],C.TEN_TT_HT AS [Tình Trạng] ,A.STT_LDV AS [STT] FROM dbo.LY_DO_VANG A INNER JOIN dbo.CHE_DO_NGHI B ON B.ID_CHE_DO = A.ID_CHE_DO LEFT JOIN dbo.TINH_TRANG_HT C ON C.ID_TT_HT = A.ID_TT_HT";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 10].Style.WrapText = true;
            sheet.Range[1, 1, 1, 10].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 10].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 10].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 3].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Loại công việc
        private void ImportLoaiCongViec(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên loại công việc
                string sTenLoaiCV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenLoaiCV, "LOAI_CONG_VIEC", "TEN_LCV"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên loại công việc anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 2;
                //độc hại
                string sDocHai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sDocHai, "0"))
                {
                    errorCount++;
                }
                col = 3;
                //phép cộng thêm
                string sPhepCT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sPhepCT, -999999, 0, false, -999999))
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
                        string sbt = "sBTLCV" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO	 dbo.LOAI_CONG_VIEC(TEN_LCV,TEN_LCV_A,DOC_HAI,PHEP_CT) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],[" + grvData.Columns[3].FieldName.ToString() + "] FROM " + sbt;
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
        private void ExportLoaiCongViec(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  TEN_LCV AS [Tên loại công việc],TEN_LCV_A AS [Tên loại công việc A],DOC_HAI AS [Độc hại],PHEP_CT AS [Phép cộng thêm] FROM dbo.LOAI_CONG_VIEC ORDER BY TEN_LCV";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 4].Style.WrapText = true;
            sheet.Range[1, 1, 1, 4].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 4].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 4].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Loại hợp đồng lao động
        private void ImportLoaiHDLD(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên loại loại hợp đồng
                string sTenLoaiHDLD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenLoaiHDLD, "LOAI_HDLD", "TEN_LHDLD"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên loại hợp đồng anh
                if (!KiemDuLieu(dr, col, false, 250))
                {
                    errorCount++;
                }
                col = 2;
                //Số tháng
                string sSoThang = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sSoThang, 0, 0, false, 12))
                {
                    errorCount++;
                }
                col = 3;
                //Số ngày
                string sSoNgay = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sSoNgay, 0, 0, false, 999999))
                {
                    errorCount++;
                }
                col = 4;
                //stt 
                col = 5;
                //tình trạng hiện tại
                string sTinhTrangHD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTinhTrangHD, "TINH_TRANG_HD", "TEN_TT_HD", false))
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
                        string sbt = "sBTLHDLD" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.LOAI_HDLD(TEN_LHDLD,TEN_LHDLD_A,SO_THANG,SO_NGAY,STT,ID_TT_HD) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],[" + grvData.Columns[3].FieldName.ToString() + "],[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 ID_TT_HD FROM dbo.TINH_TRANG_HD WHERE TEN_TT_HD = [" + grvData.Columns[5].FieldName.ToString() + "]) FROM " + sbt;
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
        private void ExportLoaiHDLD(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  A.TEN_LHDLD AS [Tên loại HĐLĐ],A.TEN_LHDLD_A AS [Tên loại HĐLĐ A],A.SO_THANG AS  [Số tháng],A.SO_NGAY AS [Số ngày],A.STT AS [STT],B.TEN_TT_HD AS [Tên tình trạng HĐ] FROM dbo.LOAI_HDLD A INNER JOIN dbo.TINH_TRANG_HD B ON B.ID_TT_HD = A.ID_TT_HD ORDER BY A.TEN_LHDLD";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 6].Style.WrapText = true;
            sheet.Range[1, 1, 1, 6].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 6].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 6].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Người kí giấy tờ
        private void ImportNguoiKy(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên người kí
                string sTenNK = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 100))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenNK, "NGUOI_KY_GIAY_TO", "HO_TEN"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //chức vụ
                if (!KiemDuLieu(dr, col, true, 100))
                {
                    errorCount++;
                }
                col = 2;
                //chức vụ anh
                if (!KiemDuLieu(dr, col, true, 100))
                {
                    errorCount++;
                }
                col = 3;
                //Quốc tịch
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                col = 4;
                //Ngày sinh
                if (!KiemDuLieuNgay(dr, col, false))
                {
                    errorCount++;
                }
                col = 5;
                //Nơi sinh
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 6;
                //Số CMMND
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                col = 7;
                //Cấp ngày
                if (!KiemDuLieuNgay(dr, col, false))
                {
                    errorCount++;
                }
                col = 8;
                //Nơi Cấp
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 9;
                //Địa chỉ
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                col = 10;
                //STT
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, 0, 0, false, 999999))
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
                        string sbt = "sBTNguoiKy" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.NGUOI_KY_GIAY_TO(HO_TEN,CHUC_VU,CHUC_VU_A,QUOC_TICH,NGAY_SINH,NOI_SINH,SO_CMND,CAP_NGAY,NOI_CAP,DIA_CHI,STT) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],[" + grvData.Columns[3].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[4].FieldName.ToString() + "],103),[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[7].FieldName.ToString() + "],103),[" + grvData.Columns[8].FieldName.ToString() + "],[" + grvData.Columns[9].FieldName.ToString() + "],[" + grvData.Columns[10].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportNguoiKy(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  HO_TEN AS [Họ tên],CHUC_VU AS [Chức vụ],CHUC_VU_A AS [Chức vụ anh],QUOC_TICH AS [Quốc tịch],NGAY_SINH AS [Ngày sinh],NOI_SINH AS [Nơi sinh],SO_CMND AS [CMND/CC],CAP_NGAY AS [Ngày cấp],NOI_CAP AS [Nơi cấp],DIA_CHI AS [Địa chỉ],STT FROM dbo.NGUOI_KY_GIAY_TO ORDER BY HO_TEN";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 11].Style.WrapText = true;
            sheet.Range[1, 1, 1, 11].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 11].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 11].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region nghạch lương
        private void ImportNgachLuong(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //mã ngạch lương
                string sMaNL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 20))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaNL, "NGACH_LUONG", "MS_NL"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //tên ngạch lương
                string sTenNL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenNL, "NGACH_LUONG", "TEN_NL"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTNL" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.NGACH_LUONG(MS_NL,TEN_NL) SELECT A.[" + grvData.Columns[0].FieldName.ToString() + "],A.[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt + " A";
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
        private void ExportNgachLuong(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  MS_NL AS [Mã ngạch lương],TEN_NL AS [Tên ngạch lương] FROM dbo.NGACH_LUONG";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region bậc lương
        private void ImportBacLuong(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên đơn vị
                string sTenDV = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTenDV, "DON_VI", "TEN_DV"))
                {
                    errorCount++;
                }
                col = 1;
                //tên ngạch lương
                string sTenNL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTenNL, "NGACH_LUONG", "TEN_NL"))
                {
                    errorCount++;
                }
                col = 2;
                // ngày qui định
                if (!KiemDuLieuNgay(dr, col, false))
                {
                    errorCount++;
                }
                col = 3;
                //tên bậc lương
                string sTenBL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenBL, "BAC_LUONG", "TEN_BL"))
                    {
                        errorCount++;
                    }
                }
                col = 4;
                //mức lương
                string sMucLuong = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sMucLuong, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 5;
                //Phụ cấp độc hại
                string sPCDH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sPCDH, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 6;
                //Phụ cấp sinh hoạt
                string sPCSH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sPCSH, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 7;
                //Phụ cấp kĩ năng
                string sPCKN = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sPCKN, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 8;
                //Thưởng chuyên cần
                string sThuongCC = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sThuongCC, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 9;
                //Thưởng tăng ca
                string sThuongTC = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sThuongTC, -999999, 0, false, -999999))
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
                        string sbt = "sBTBL" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        //A.[" + grvData.Columns[0].FieldName.ToString() + "]
                        string sSql = "INSERT INTO dbo.BAC_LUONG(ID_DV, ID_NL, NGAY_QD, TEN_BL, MUC_LUONG, PC_DH, PC_SINH_HOAT, PC_KY_NANG, THUONG_CV_CC, THUONG_TC)SELECT(SELECT TOP 1 ID_DV  FROM dbo.DON_VI WHERE TEN_DV = A.[" + grvData.Columns[0].FieldName.ToString() + "]) ,(SELECT TOP 1 ID_NL FROM dbo.NGACH_LUONG WHERE TEN_NL = A.[" + grvData.Columns[1].FieldName.ToString() + "]) ,CONVERT(DATETIME, A.[" + grvData.Columns[2].FieldName.ToString() + "], 103),A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "],A.[" + grvData.Columns[7].FieldName.ToString() + "],A.[" + grvData.Columns[8].FieldName.ToString() + "],A.[" + grvData.Columns[9].FieldName.ToString() + "] FROM " + sbt + " A";
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
        private void ExportBacLuong(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  B.TEN_DV AS [Tên ĐV],C.TEN_NL AS [Tên ngạch lương],NGAY_QD AS [Ngày quyết định], TEN_BL AS [Tên bậc lương], MUC_LUONG AS [Mức lương],PC_DH AS [Phụ cấp Độc hại], PC_SINH_HOAT AS [Phụ cấp sinh hoạt], PC_KY_NANG AS [Phụ cấp kỹ năng], THUONG_CV_CC AS [Thưởng chuyên cần], THUONG_TC AS [Thưởng trợ cấp] FROM dbo.BAC_LUONG A INNER JOIN dbo.DON_VI B ON B.ID_DV = A.ID_DV INNER JOIN  dbo.NGACH_LUONG C ON C.ID_NL = A.ID_NL ORDER BY B.TEN_DV,C.TEN_NL,A.TEN_BL";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 10].Style.WrapText = true;
            sheet.Range[1, 1, 1, 10].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 10].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[1, 1, 1, 10].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Loại khen thưởng kỹ luật
        private void ImportLoaiKTKL(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //Tên khen thưởng kỹ luật
                string sTenKTKL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 200))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenKTKL, "LOAI_KHEN_THUONG", "TEN_LOAI_KT"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //Tên khen thưởng kỹ luật anh
                if (!KiemDuLieu(dr, col, true, 200))
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
                        string sbt = "sBTLoaiKTKL" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.LOAI_KHEN_THUONG(TEN_LOAI_KT,TEN_LOAI_KT_A) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt;
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
        private void ExportLoaiKTKL(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  TEN_LOAI_KT AS [Tên khen thưởng/Kỹ luật],TEN_LOAI_KT_A AS [Tên khen thưởng/Kỹ luật A] FROM dbo.LOAI_KHEN_THUONG ORDER BY TEN_LOAI_KT";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #endregion

        #region Chấm công
  
        #region Nhóm Chấm công
        private void ImportNhomChamCong(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //tên nhóm chấm công
                string sNhomCC = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sNhomCC, "NHOM_CHAM_CONG", "TEN_NHOM"))
                    {
                        errorCount++;
                    }
                }

                //nhóm chấm công anh
                if (!KiemDuLieu(dr, col, false, 50))
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
                        string sbt = "sBTNhomCC" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.NHOM_CHAM_CONG(TEN_NHOM,TEN_NHOM_A) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportNhomChamCong(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  TEN_NHOM AS [Tên nhóm],TEN_NHOM_A AS [Tên nhóm A] FROM dbo.NHOM_CHAM_CONG ORDER BY TEN_NHOM";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 2].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Ngày nghĩ lễ
        private void ImportNgayNghiLe(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //Ngày
                if (!KiemDuLieuNgay(dr, col,true))
                {
                    errorCount++;
                }
                col = 1;
                string sLyDo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sLyDo, "NGAY_NGHI_LE", "LY_DO"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTNgayNL" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.NGAY_NGHI_LE(NGAY,LY_DO) SELECT CONVERT(datetime,[" + grvData.Columns[0].FieldName.ToString() + "],103),[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportNgayNghiLe(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  NGAY AS [Ngày],LY_DO AS [Lý do] FROM dbo.NGAY_NGHI_LE ORDER BY NGAY";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.InsertDataTable(dtTmp, true, 1, 1);
            sheet.InsertDataTable(dtTmp, true, 1, 2);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Nội dung qui định
        private void ImportNoiDungQuiDinh(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();

                col = 0;
                //tên nhóm chấm công
                string sNoiDung = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 250))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sNoiDung, "NOI_QUY_LAO_DONG", "NOI_DUNG"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTNNoiQuiLD" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.NOI_QUY_LAO_DONG(NOI_DUNG) SELECT[" + grvData.Columns[0].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportNoiDungQuiDinh(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  NOI_DUNG AS [Nội dung] FROM dbo.NOI_QUY_LAO_DONG ORDER BY NOI_DUNG";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 1].Style.WrapText = true;
            sheet.Range[1, 1, 1, 1].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 1].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #endregion

        #region Tính lương
        #region Đơn giá giây
        private void ImportDonGiaGiay(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Ngày
                string sngay = Convert.ToDateTime(dr[grvData.Columns[col].FieldName.ToString()]).ToString("dd/MM/yyyy");
                if (!KiemDuLieuNgay(dr, col, true))
                {
                    errorCount++;
                }
                else
                {
                    //kiểm tra ngày tồn tại
                    int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DON_GIA_GIAY WHERE CONVERT(NVARCHAR(20),NGAY_QD,103) ='"+ sngay + "'"));
                    if(n > 0)
                    {
                        dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaTonTaiCSDL"));
                        dr["XOA"] = 1;
                        errorCount++;
                    }
                }

                col = 1;
                //Hệ số
                string heso = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, heso, -999999, -999999,true))
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
                    string sbt = "sBTDonGiaGiay" + Commons.Modules.UserName;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.DON_GIA_GIAY(NGAY_QD,HS_DG_GIAY) SELECT CONVERT(datetime,[" + grvData.Columns[0].FieldName.ToString() + "],103),[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt;
                        SqlHelper.ExecuteNonQuery(sTrans, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sbt);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ExportDonGiaGiay(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  NGAY_QD AS [Ngày QĐ],HS_DG_GIAY AS [Hệ số đơn giá] FROM dbo.DON_GIA_GIAY ORDER BY NGAY_QD";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Loại Máy
        private void ImportLoaiMay(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Mã số 
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 7))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaSo, "LOAI_MAY", "MS_LOAI_MAY"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //Tên loại máy 
                string sTenLoai = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 35))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenLoai, "LOAI_MAY", "TEN_LOAI_MAY"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //STT 
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
                {
                    errorCount++;
                }
                col = 3;
                //SD QTCN 
                string sSD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sSD, "0"))
                {
                    errorCount++;
                }
                col = 4;
                //Công cụ
                string sCC = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sCC, "0"))
                {
                    errorCount++;
                }
                col = 5;
                //Ký hiệu
                if (!KiemDuLieu(dr, col, false, 200))
                {
                    errorCount++;
                }
                col = 6;
                //Tính năng cơ bản    
                if (!KiemDuLieu(dr, col, false, 400))
                {
                    errorCount++;
                }
                col = 7;
                //Tốc độ thiết bị
                string sTD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, sTD, -999999, 0, false, -999999))
                {
                    errorCount++;
                }
                col = 8;
                //Sử dụng công đoạn
                if (!KiemDuLieu(dr, col, false, 200))
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
                        string sbt = "sBTLoaiMay" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.LOAI_MAY(MS_LOAI_MAY,TEN_LOAI_MAY,STT_MAY,SD_QTCN,CONG_CU,KI_HIEU,TINH_NANG_CO_BAN,TOC_DO_THIET_BI,SU_DUNG_CONG_DOAN) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],[" + grvData.Columns[3].FieldName.ToString() + "],[" + grvData.Columns[4].FieldName.ToString() + "],[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],[" + grvData.Columns[7].FieldName.ToString() + "],[" + grvData.Columns[8].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportLoaiMay(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT  MS_LOAI_MAY [Mã số],TEN_LOAI_MAY [Tên loại máy],STT_MAY [STT],SD_QTCN [SD QTCN], CONG_CU [Công cụ],KI_HIEU [Kí hiệu],TINH_NANG_CO_BAN [Tính năng cơ bản],TOC_DO_THIET_BI[Tốc độ thiết bị],SU_DUNG_CONG_DOAN[Sử dụng công đoạn] FROM dbo.LOAI_MAY ORDER BY TEN_LOAI_MAY";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 9].Style.WrapText = true;
            sheet.Range[1, 1, 1, 9].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 9].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 9].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Cụm
        private void ImportCum(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Mã cụm 
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 10))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaSo, "CUM", "MS_CUM"))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //Tên cụm
                string sTenCum = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 100))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenCum, "CUM", "TEN_CUM"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //STT 
                string stt = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, stt, -999999, -999999, false))
                {
                    errorCount++;
                }
                col = 3;
                //Nhóm hàng hóa 
                string sNhomHH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sNhomHH, "NHOM_HANG_HOA", "TEN_NHH"))
                {
                    errorCount++;
                }

                col = 4;
                //Tính thời gian
                string stTG = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, stTG, "0"))
                {
                    errorCount++;
                }
                col = 5;
                //Loại cụm
                string sLoaiCum = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sLoaiCum, "LOAI_CUM", "TEN_LOAI_CUM"))
                {
                    errorCount++;
                }
                col = 6;
                //Cụm ps
                string sCumPS = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sCumPS, "0"))
                {
                    errorCount++;
                }
                col = 7;
                //Cụm cuối
                string sCumCuoi = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuBool(dr, col, sCumCuoi, "0"))
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
                        string sbt = "sBTCum" + Commons.Modules.UserName;
                        //A.[" + grvData.Columns[0].FieldName.ToString() + "]
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.CUM(MS_CUM,TEN_CUM,STT,ID_NHH,TINH_TG,LOAI_CUM,CUM_PS,CUM_CUOI) SELECT A.[" + grvData.Columns[0].FieldName.ToString() + "],A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],(SELECT TOP 1 ID_NHH FROM dbo.NHOM_HANG_HOA WHERE TEN_NHH = A.[" + grvData.Columns[3].FieldName.ToString() + "]),A.[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 LOAI_CUM FROM dbo.LOAI_CUM WHERE TEN_LOAI_CUM = A.[" + grvData.Columns[5].FieldName.ToString() + "]),A.[" + grvData.Columns[6].FieldName.ToString() + "],A.[" + grvData.Columns[7].FieldName.ToString() + "] FROM " + sbt +" as A";
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

        private void ExportCum(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT A.MS_CUM AS [Mã cụm], A.TEN_CUM AS [Tên cụm],A.STT AS [STT],B.TEN_NHH AS [Nhóm hàng hóa],A.TINH_TG AS [Tính TG],C.TEN_LOAI_CUM AS [Loại cụm],A.CUM_PS AS[Cụm PS],A.CUM_CUOI AS [Cụm cuối] FROM dbo.CUM A INNER JOIN  dbo.NHOM_HANG_HOA B ON B.ID_NHH = A.ID_NHH INNER JOIN  dbo.LOAI_CUM C ON C.LOAI_CUM = A.LOAI_CUM ORDER BY A.TEN_CUM";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 9].Style.WrapText = true;
            sheet.Range[1, 1, 1, 9].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 9].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 9].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region bậc thợ
        private void ImportBacTho(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Tên bậc thợ
                string sTenBT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 40))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenBT, "HSBT", "TEN_BAC_THO"))
                    {
                        errorCount++;
                    }
                }

                col = 1;
                //Hệ số
                string heso = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieuSo(dr, col, heso, -999999, -999999, true))
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
                        string sbt = "sBTHSBT" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.HSBT(TEN_BAC_THO,HE_SO_BAC_THO) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "] FROM " + sbt;
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

        private void ExportBacTho(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT TEN_BAC_THO AS [Tên bậc thợ],HE_SO_BAC_THO AS [Hệ số bậc thợ] FROM dbo.HSBT ORDER BY TEN_BAC_THO";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //export datatable to excel
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 2].Style.WrapText = true;
            sheet.Range[1, 1, 1, 2].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 2].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion

        #region Chuyền
        private void ImportChuyen(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;

            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //tên tổ
                string sTenTo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(dr, col, sTenTo, "[TO]", "TEN_TO"))
                {
                    errorCount++;
                }
                col = 1;
                //mã chuyền
                string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 20))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sMaSo, "CHUYEN", "MS_CHUYEN"))
                    {
                        errorCount++;
                    }
                }
                col = 2;
                //Tên chuyền
                string sTenCum = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemDuLieu(dr, col, true, 50))
                {
                    errorCount++;
                }
                else
                {
                    if (!KiemTrungDL(dtSource, dr, col, sTenCum, "CHUYEN", "TEN_CHUYEN"))
                    {
                        errorCount++;
                    }
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
                        string sbt = "sBTCum" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                        string sSql = "INSERT INTO dbo.CHUYEN(ID_TO,MS_CHUYEN,TEN_CHUYEN) SELECT (SELECT ID_TO FROM dbo.[TO] WHERE TEN_TO = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "] FROM " + sbt + " as A";
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

        private void ExportChuyen(string sPath)
        {
            DataTable dtTmp = new DataTable();
            string SQL = "SELECT B.TEN_TO AS [Tên Tổ],A.MS_CHUYEN AS [Ms chuyền],A.TEN_CHUYEN AS [Tên Chuyền] FROM dbo.CHUYEN A INNER JOIN dbo.[TO] B ON B.ID_TO = A.ID_TO";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            Workbook book = new Spire.Xls.Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.DefaultColumnWidth = 20;

            sheet.Range[1, 1, 1, 3].Style.WrapText = true;
            sheet.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[1, 1, 1, 3].Style.Font.IsBold = true;

            sheet.Range[1, 1].Style.Font.Color = Color.Red;
            sheet.Range[1, 2].Style.Font.Color = Color.Red;
            sheet.Range[1, 3].Style.Font.Color = Color.Red;

            sheet.InsertDataTable(dtTmp, true, 1, 1);

            book.SaveToFile(sPath);
            System.Diagnostics.Process.Start(sPath);
        }
        #endregion



        #endregion

        //private void grvData_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        //{
        //    grvData.IndicatorWidth = 40;
        //    if (e.RowHandle >= 0)
        //    {
        //        e.Info.DisplayText = (e.RowHandle + 1).ToString();
        //    }
        //}

        private void grvData_ShownEditor(object sender, EventArgs e)
        {
            ptChung = grvData.GridControl.PointToClient(Control.MousePosition);
            grvData.ActiveEditor.DoubleClick += new EventHandler(ActiveEditor_DoubleClick);
        }
        private void ActiveEditor_DoubleClick(object sender, System.EventArgs e)
        {
            DoRowDoubleClick(grvData, ptChung);
            grvData.RefreshData();
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
            if (cboDanhMucImport.Text == "") return;
            DataTable dtTmp = new DataTable();
            try
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(pt);
                int col = -1;
                col = info.Column.AbsoluteIndex;
                if (col == -1)
                    return;
                int iSheet;
                iSheet = int.Parse(cboDanhMucImport.EditValue.ToString());
                System.Data.DataRow row = grvData.GetDataRow(info.RowHandle);

                switch (iSheet)
                {
                    case 2:
                        {
                            if (col == 0)
                            {
                                KiemData("DON_VI", "TEN_DV", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (col == 0)
                            {
                                KiemData("DON_VI", "TEN_DV", info.RowHandle, col, row);
                            }
                            if (col == 1)
                            {
                                KiemData("XI_NGHIEP", "TEN_XN", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 5:
                        {
                            if (col == 0)
                            {
                                KiemData("LOAI_CHUC_VU", "TEN_LOAI_CV", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 9:
                        {
                            if (col == 0)
                            {
                                KiemData("CHE_DO_NGHI", "TEN_CHE_DO", info.RowHandle, col, row);
                            }
                            if (col == 8)
                            {
                                KiemData("TINH_TRANG_HT", "TEN_TT_HT", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 11:
                        {
                            if (col == 5)
                            {
                                KiemData("TINH_TRANG_HD", "TEN_TT_HD", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 14:
                        {
                            if (col == 0)
                            {
                                KiemData("DON_VI", "TEN_DV", info.RowHandle, col, row);
                            }
                            if (col == 1)
                            {
                                KiemData("NGACH_LUONG", "TEN_NL", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 32:
                        {
                            if (col == 3)
                            {
                                KiemData("NHOM_HANG_HOA", "TEN_NHH", info.RowHandle, col, row);
                            }
                            if (col == 5)
                            {
                                KiemData("LOAI_CUM", "TEN_LOAI_CUM", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 34:
                        {
                            if (col == 0)
                            {
                                KiemData("[TO]", "TEN_TO", info.RowHandle, col, row);
                            }
                            break;
                        }
                }
            }
            catch
            {
            }
            grvData.UpdateCurrentRow();
        }
        private void KiemData(string Table, string Field, int dong, int Cot, DataRow row)
        {
            try
            {
                frmPopUp frmPopUp = new frmPopUp();
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "select * from " + Table));
                frmPopUp.TableSource = dt;
                if (frmPopUp.ShowDialog() == DialogResult.OK)
                    row[Cot] = frmPopUp.RowSelected[Field].ToString();
            }
            catch { }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanCoMuonXoaDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view.SelectedRowsCount != 0)
                {
                    view.GridControl.BeginUpdate();
                    List<int> selectedLogItems = new List<int>(view.GetSelectedRows());

                    for (int i = selectedLogItems.Count - 1; i >= 0; i--)
                    {
                        view.DeleteRow(selectedLogItems[i]);
                    }
                    view.GridControl.EndUpdate();
                }
                else if (view.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    view.DeleteRow(view.FocusedRowHandle);
                }
                groDLImport.Text = " Total : " + grvData.RowCount.ToString() + " row";
            }
        }
    }
}
