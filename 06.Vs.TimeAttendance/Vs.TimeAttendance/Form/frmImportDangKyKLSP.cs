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
    public partial class frmImportDangKyKLSP : DevExpress.XtraEditors.XtraForm
    {
        string fileName = "";
        Point ptChung;
        string ChuoiKT = "";
        DataTable _table = new DataTable();
        DataTable dtemp;
        string sCheck = "";
        public frmImportDangKyKLSP()
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
                var worksheetSettings = new ExcelWorksheetSettings(cboChonSheet.Text);
                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                source.Fill();
                dt = new DataTable();
                dt = ToDataTable(source);
                dt.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                string sBT = "sBTImportDKKLSP" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportDKTGKhongLamSP", conn);
                cmd.Parameters.Add("@sBT_Import", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNgay, dt, "NGAY_VALUE", "NGAY_VIEW", "NGAY_VIEW");
                Commons.Modules.sLoad = "";
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();

                Commons.Modules.ObjSystems.XoaTable(sBT);

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt1, true, true, false, true, true, this.Name);

                RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                txtEdit.Properties.DisplayFormat.FormatString = "0.00";
                txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.EditFormat.FormatString = "0.00";
                txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.Mask.EditMask = "0.00";
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


                grvData.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvData.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvData.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                cboNgay_EditValueChanged(null, null);
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
                DateTime dNgay = Convert.ToDateTime(dr[grvData.Columns[0].FieldName.ToString()]);
                if (dNgay == Convert.ToDateTime(cboNgay.Text))
                {

                    //Ngày   
                    col = 0;
                    if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                    {
                        errorCount++;
                    }
                    col = 1;
                    //Mã số nhân viên
                    string sMaSo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                    if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sMaSo, "CONG_NHAN", "MS_CN", true, this.Name))
                    {
                        errorCount++;
                    }
                    else
                    {
                        if (!KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "CONG_NHAN", "MS_CN", this.Name, cboNgay.Text))
                        {
                            errorCount++;
                            errorMS++;
                        }
                    }


                    col = 2;
                    //Tên 
                    if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 50, this.Name))
                    {
                        errorCount++;
                    }

                    col = 3;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Mất điện", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_1 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 4;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian hỗ trợ", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_2 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);
                    col = 5;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Sửa hàng không do lỗi của chuyền", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_3 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 6;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Hoạt động", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_4 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 7;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Chờ NPL", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_5 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 8;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, " Sửa máy", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_6 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 9;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian khác", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dCOT_7 = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 10;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian HC", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dTG_HC = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 11;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian OT 150%", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dTG_TC_NT = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 12;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian OT 200%", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dTG_TC_CN = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);

                    col = 13;
                    if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, "Thời gian OT 300%", 0, 0, false, this.Name))
                    {
                        errorCount++;
                    }
                    Double dTG_TC_NL = Convert.ToDouble(dr[grvData.Columns[col].FieldName.ToString()]);


                    Double dTong3Cot = dTG_HC + dTG_TC_NT + dTG_TC_CN + dTG_TC_NL;
                    Double dTong7Cot = dCOT_1 + dCOT_2 + dCOT_3 + dCOT_4 + dCOT_5 + dCOT_6 + dCOT_7;
                    if (dTong3Cot != dTong7Cot)
                    {
                        errorCount++;
                        dr.SetColumnError("COT_1", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_2", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_3", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_4", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_5", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_6", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("COT_7", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("TG_HC", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("TG_TC_NT", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("TG_TC_CN", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
                        dr.SetColumnError("TG_TC_NL", Commons.Modules.ObjLanguages.GetLanguage("ucDKThoiGianKhongLamSP", "msgGioLamViecKhongCan"));
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

                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDK_TG_KHONG_LAM_SP_Import", sTB);
                        Commons.Modules.ObjSystems.XoaTable(sTB);
                        sTrans.Commit();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgImportDuLieuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //grdData.DataSource = dtSource.Clone();
                        DataTable dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grdData).AsEnumerable().Where(x => x["NGAY"].ToString() != cboNgay.Text).CopyToDataTable();
                        grdData.DataSource = dt;
                        cboChonSheet.Text = string.Empty;
                        btnFile.Text = string.Empty;
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
        private void frmImportDangKyKLSP_Load(object sender, EventArgs e)
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

            excelDataSource.SourceOptions = new CsvSourceOptions() { CellRange = "A6:" + "O" + (dv_temp.Count + 6) + "" };
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
                            values[i] = null;
                        }
                        else
                        {
                            if (i == 0)
                            {
                                values[i] = Convert.ToDateTime(props[i].GetValue(item)).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                values[i] = props[i].GetValue(item);
                            }
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

        private void frmImportDangKyKLSP_FormClosing(object sender, FormClosingEventArgs e)
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

        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, string sform, string date)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(iCot).Trim().Equals(sDLKiem) && x["NGAY"].Equals(date)).CopyToDataTable().Rows.Count > 1)
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

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdData.DataSource;
                if (dt == null) return;
                try
                {
                    dt.DefaultView.RowFilter = "NGAY = '" + cboNgay.Text + "'";
                    //_view.SelectRow(0);
                }
                catch (Exception ex)
                {
                    dt.DefaultView.RowFilter = "1 = 0";
                }
                //Commons.Modules.ObjSystems.RowFilter(grdData, grvData.Columns["NGAY"], Convert.ToDateTime(cboNgay.Text).ToString("dd/MM/yyyy"));
            }
            catch (Exception ex) { }

        }
    }
}
