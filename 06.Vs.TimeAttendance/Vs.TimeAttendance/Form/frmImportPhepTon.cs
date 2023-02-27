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
using System.Threading;
using DevExpress.XtraEditors.Repository;

namespace Vs.TimeAttendance
{
    public partial class frmImportPhepTon : DevExpress.XtraEditors.XtraForm
    {
        string fileName = "";
        Point ptChung;
        string ChuoiKT = "";
        DataTable _table = new DataTable();
        DataTable dtemp;
        string sCheck = "";
        public int Nam = 2022;
        public frmImportPhepTon()
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
                var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text);
                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                source.Fill();
                dt = new DataTable();
                dt = ToDataTable(source);
                dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
            }
            catch
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
                double dPhepTon = Convert.ToDouble(dr[grvData.Columns[2].FieldName.ToString()]);
                if (dPhepTon > 0)
                {
                    col = 0;
                    //Mã số nhân viên
                    string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "CONG_NHAN", "MS_CN", true, this.Name))
                    {
                        errorCount++;
                    }
                    else
                    {
                        if (!KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "CONG_NHAN", "MS_CN", this.Name))
                        {
                            errorCount++;
                            errorMS++;
                        }
                    }
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
                    SqlTransaction sTrans = conn.BeginTransaction();
                    string sTB = "LK_Tam" + Commons.Modules.UserName;
                    try
                    {
                        //tạo bảm tạm trên lưới
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        //string sSql = "INSERT INTO dbo.UNG_VIEN(MS_UV,HO,TEN,PHAI,NGAY_SINH,NOI_SINH,SO_CMND,NGAY_CAP,NOI_CAP,ID_TT_HN,HO_TEN_VC,NGHE_NGHIEP_VC,SO_CON,DT_DI_DONG,EMAIL,NGUOI_LIEN_HE,QUAN_HE,DT_NGUOI_LIEN_HE,ID_TP,ID_QUAN,ID_PX,THON_XOM,DIA_CHI_THUONG_TRU,ID_NTD,ID_CN,HINH_THUC_TUYEN,ID_TDVH,ID_KNLV,ID_DGTN,VI_TRI_TD_1,VI_TRI_TD_2,NGAY_HEN_DI_LAM,XAC_NHAN_DL,NGAY_NHAN_VIEC,XAC_NHAN_DTDH,DA_CHUYEN,GHI_CHU,DA_GIOI_THIEU,HUY_TUYEN_DUNG) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],case [" + grvData.Columns[3].FieldName.ToString() + "] when 'Nam' then 1 else 0 end,CONVERT(datetime,[" + grvData.Columns[4].FieldName.ToString() + "],103),[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],[" + grvData.Columns[7].FieldName.ToString() + "],[" + grvData.Columns[8].FieldName.ToString() + "],(SELECT TOP 1 ID_TT_HN FROM dbo.TT_HON_NHAN WHERE TEN_TT_HN = A.[" + grvData.Columns[9].FieldName.ToString() + "]),[" + grvData.Columns[10].FieldName.ToString() + "],[" + grvData.Columns[11].FieldName.ToString() + "],[" + grvData.Columns[12].FieldName.ToString() + "],[" + grvData.Columns[13].FieldName.ToString() + "],[" + grvData.Columns[14].FieldName.ToString() + "],[" + grvData.Columns[15].FieldName.ToString() + "],[" + grvData.Columns[16].FieldName.ToString() + "],[" + grvData.Columns[17].FieldName.ToString() + "],(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[18].FieldName.ToString() + "]),(SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = A.[" + grvData.Columns[19].FieldName.ToString() + "]),(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[20].FieldName.ToString() + "]),[" + grvData.Columns[21].FieldName.ToString() + "],[" + grvData.Columns[22].FieldName.ToString() + "],(SELECT TOP 1 ID_NTD FROM dbo.NGUON_TUYEN_DUNG WHERE TEN_NTD= A.[" + grvData.Columns[23].FieldName.ToString() + "]),(SELECT TOP 1 ID_CN FROM dbo.CONG_NHAN WHERE HO +' '+TEN = A.[" + grvData.Columns[24].FieldName.ToString() + "]),(SELECT ID_HTT FROM dbo.HINH_THUC_TUYEN WHERE TEN_HT_TUYEN = A.[" + grvData.Columns[25].FieldName.ToString() + "]),(SELECT TOP 1 ID_TDVH FROM dbo.TRINH_DO_VAN_HOA WHERE TEN_TDVH = A.[" + grvData.Columns[26].FieldName.ToString() + "]),(SELECT TOP 1 ID_KNLV FROM dbo.KINH_NGHIEM_LV WHERE TEN_KNLV = A.[" + grvData.Columns[27].FieldName.ToString() + "]),(SELECT TOP 1 ID_DGTN FROM dbo.DANH_GIA_TAY_NGHE WHERE TEN_DGTN = A.[" + grvData.Columns[28].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[29].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[30].FieldName.ToString() + "]),CONVERT(datetime,[" + grvData.Columns[31].FieldName.ToString() + "],103),[" + grvData.Columns[32].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[33].FieldName.ToString() + "],103),[" + grvData.Columns[34].FieldName.ToString() + "],[" + grvData.Columns[35].FieldName.ToString() + "],[" + grvData.Columns[36].FieldName.ToString() + "],[" + grvData.Columns[37].FieldName.ToString() + "],[" + grvData.Columns[38].FieldName.ToString() + "]  FROM " + sbt + " AS A";

                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spImportPhepTon", sTB, Nam);
                        Commons.Modules.ObjSystems.XoaTable(sTB);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grdData.DataSource = dtSource.Clone();
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sTB);
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

        }
        private void frmImportPhepTon_Load(object sender, EventArgs e)
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
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "HO_TEN";
                            table.Columns.Add(sTenCot.Trim(), typeof(string));
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "PHEP_TON";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "PHEP_THANH_TOAN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
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
                    if (i == 2)
                    {
                        values[i] = Convert.ToDouble(props[i].GetValue(item));
                    }
                    else
                    {
                        values[i] = props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }

        private void frmImportPhepTon_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(delegate ()
                {
                    timer1.Stop();
                    Thread.Sleep(300000);//chi nghỉ 5 phút
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Hide();
                            string[] sArray = sCheck.Split(',');
                            DateTime datOld;
                            datOld = Convert.ToDateTime(sArray[0]).AddHours(1);
                            DateTime datCurren = DateTime.Now;
                            try
                            {
                                datCurren = DateTime.Now;
                            }
                            catch { }
                            if (datOld < datCurren)
                            {
                                Commons.Modules.ObjSystems.setCheckImport(0);
                            }

                            this.Close();
                        }));
                    }
                }, Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds));
                thread.Start();
            }
            catch { }
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
    }
}
