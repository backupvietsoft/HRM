using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmImportUngVien : DevExpress.XtraEditors.XtraForm
    {
        string fileName = "";
        Point ptChung;
        DataTable _table = new DataTable();
        DataTable dtemp;
        public frmImportUngVien()
        {
            InitializeComponent();
        }
        private void btnFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                OpenFileDialog oFile = new OpenFileDialog();
                oFile.Filter = "All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*";
                if (oFile.ShowDialog() != DialogResult.OK) return;

                fileName = oFile.FileName;
                btnFile.Text = fileName;
                if (!System.IO.File.Exists(fileName)) return;

                if (Commons.Modules.MExcel.MGetSheetNames(fileName, cboChonSheet))
                {
                    cboChonSheet_EditValueChanged(null, null);
                }
                else
                {
                    grdData.DataSource = null;
                    cboChonSheet.Properties.DataSource = null;
                    btnFile.Text = "";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
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

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            //Commons.Modules.ObjSystems.ShowWaitForm(this);
            switch (btn.Tag.ToString())
            {
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

                        int iSheet = int.Parse(cboChonSheet.EditValue.ToString());
                        switch (iSheet)
                        {
                            case 0:
                                {
                                    ImportUngVien(dtSource);
                                    break;
                                }
                            case 1:
                                {
                                    ImportBangCap(dtSource);
                                    break;
                                }
                            case 2:
                                {
                                    ImportKinhNghiem(dtSource);
                                    break;
                                }
                            //case 3:
                            //    {
                            //        ImportThongTinKhac(dtSource);
                            //        break;
                            //    }
                            default:
                                break;
                        }

                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default: break;
            }
        }
        #region import ứng viên
        private void ImportUngVien(DataTable dtSource)
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
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (sMaSo != "")
                    {
                        if (!Commons.Modules.MExcel.KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "UNG_VIEN", "MS_UV", this.Name))
                        {
                            errorCount++;
                        }
                    }
                }
                col = 1;
                //ngày nhận CV
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col,false, this.Name))
                {
                    errorCount++;
                }
                col = 2;
                //Họ và tên
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 70, this.Name))
                {
                    errorCount++;
                }
                col = 3;
                //Giới tính
                string sGioiTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sGioiTinh, false))
                {
                    errorCount++;
                }
                col = 4;
                //Ngày sinh   
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, true, this.Name))
                {
                    errorCount++;
                }
                col = 5;
                //Nơi sinh    
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 6;
                //CMND 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 15, this.Name))
                {
                    errorCount++;
                }
                col = 7;
                //Ngày cấp 
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }
                col = 8;
                //Nơi cấp
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 150, this.Name))
                {
                    errorCount++;
                }
                col = 9;
                //Trình độ văn hóa
                string sVanHoa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sVanHoa, "TRINH_DO_VAN_HOA", "TEN_TDVH", false, this.Name))
                {
                    errorCount++;
                }
                col = 10;
                //Điện thoại  
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 20, this.Name))
                {
                    errorCount++;
                }
                col = 11;
                //Email 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 12;
                //Người liên hệ   
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 13;
                //Quan hệ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 14;
                //ĐT Người liên hệ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 20, this.Name))
                {
                    errorCount++;
                }
                col = 15;
                //Thành phố   
                string sThanhPho = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPho, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }
                col = 16;
                //Quận 
                string sQuan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(grvData, dr, col, sQuan, "SELECT COUNT(*) FROM dbo.QUAN WHERE TEN_QUAN = N'"+ sQuan +"' AND ID_TP = (SELECT ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = N'"+ sThanhPho +"')", false, this.Name))
                {
                    errorCount++;
                }
                col = 17;
                //Phường xã 
                string sPhuongXa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!KiemTonTai(grvData, dr, col, sPhuongXa, "SELECT COUNT(*) FROM dbo.PHUONG_XA WHERE TEN_PX = N'"+ sPhuongXa + "' AND  ID_QUAN = (SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'" + sQuan +"')", false, this.Name))
                {
                    errorCount++;
                }
                col = 18;
                //Thôn xóm 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 100, this.Name))
                {
                    errorCount++;
                }
                col = 19;
                //Địa chỉ
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 20;
                //Nguồn tuyển
                string sNguonTuyen = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sNguonTuyen, "NGUON_TUYEN_DUNG", "TEN_NTD", false, this.Name))
                {
                    errorCount++;
                }
                col = 21;
                //Người giới thiệu    
                string sNguoiGT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sNguoiGT, "CONG_NHAN", "HO", "TEN", this.Name))
                {
                    errorCount++;
                }
                col = 22;
                //tây nghề
                string sTayNghe = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTayNghe, "TAY_NGHE", "TEN_TAY_NGHE", false, this.Name))
                {
                    errorCount++;
                }
                col = 23;
                //Vị trí tuyển 1
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 24;
                //Vị trí tuyển 2
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 25;
                //Vị trí phù hơp
                string sVTT1 = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sVTT1, "LOAI_CONG_VIEC", "TEN_LCV"))
                {
                    errorCount++;
                }
                col = 26;
                //Công đoạn chủ yếu
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 27;
                //Ghi Chu
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 500, this.Name))
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
                        string sbt = "sBTUV" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN(MS_UV,NGAY_NHAN_HO_SO,HO,TEN,PHAI,NGAY_SINH,NOI_SINH,SO_CMND,NGAY_CAP,NOI_CAP,ID_TDVH,DT_DI_DONG,EMAIL,NGUOI_LIEN_HE,QUAN_HE,DT_NGUOI_LIEN_HE,ID_TP,ID_QUAN,ID_PX,THON_XOM,DIA_CHI_THUONG_TRU,ID_NTD,ID_CN,TAY_NGHE,VI_TRI_TD_1,VI_TRI_TD_2,ID_VI_TRI_PHU_HOP,CONG_DOAN_CHU_YEU,GHI_CHU,ID_TT_UV) SELECT IIF(ISNULL([" + grvData.Columns[0].FieldName.ToString() + "],'')= '','MUV-' +  RIGHT('000000' + CONVERT(NVARCHAR(6),RIGHT( dbo.AUTO_CREATE_SO_UNG_VIEN(),6) +  ROW_NUMBER() OVER (ORDER BY [Mã số]) - 1),5),[" + grvData.Columns[0].FieldName.ToString() + "]),CONVERT(datetime,[" + grvData.Columns[1].FieldName.ToString() + "],103),LEFT( [" + grvData.Columns[2].FieldName.ToString() + "], len([" + grvData.Columns[2].FieldName.ToString() + "])-charindex(' ', REVERSE([" + grvData.Columns[2].FieldName.ToString() + "]),1)),  RIGHT([" + grvData.Columns[2].FieldName.ToString() + "], (charindex(' ', REVERSE([" + grvData.Columns[2].FieldName.ToString() + "]), 1)) -1),case [" + grvData.Columns[3].FieldName.ToString() + "] when 'Nam' then 1 else 0 end,CONVERT(datetime,[" + grvData.Columns[4].FieldName.ToString() + "],103),[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[7].FieldName.ToString() + "],103),[" + grvData.Columns[8].FieldName.ToString() + "],(SELECT TOP 1 ID_TDVH FROM dbo.TRINH_DO_VAN_HOA WHERE TEN_TDVH = A.[" + grvData.Columns[9].FieldName.ToString() + "]),[" + grvData.Columns[10].FieldName.ToString() + "],[" + grvData.Columns[11].FieldName.ToString() + "],[" + grvData.Columns[12].FieldName.ToString() + "],[" + grvData.Columns[13].FieldName.ToString() + "],[" + grvData.Columns[14].FieldName.ToString() + "],(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[15].FieldName.ToString() + "]),(SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = A.[" + grvData.Columns[16].FieldName.ToString() + "] AND ID_TP = (SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[15].FieldName.ToString() + "])),(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[17].FieldName.ToString() + "] AND ID_QUAN = (SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = A.[" + grvData.Columns[16].FieldName.ToString() + "])),[" + grvData.Columns[18].FieldName.ToString() + "],[" + grvData.Columns[19].FieldName.ToString() + "],(SELECT TOP 1 ID_NTD FROM dbo.NGUON_TUYEN_DUNG WHERE TEN_NTD= A.[" + grvData.Columns[20].FieldName.ToString() + "]),(SELECT TOP 1 ID_CN FROM dbo.CONG_NHAN WHERE HO +' '+TEN = A.[" + grvData.Columns[21].FieldName.ToString() + "]),(SELECT TOP 1 ID_TAY_NGHE FROM dbo.TAY_NGHE WHERE TEN_TAY_NGHE = A.[" + grvData.Columns[22].FieldName.ToString() + "]),A.[" + grvData.Columns[23].FieldName.ToString() + "],A.[" + grvData.Columns[24].FieldName.ToString() + "],(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[25].FieldName.ToString() + "]),[" + grvData.Columns[26].FieldName.ToString() + "],[" + grvData.Columns[27].FieldName.ToString() + "],1  FROM " + sbt + " AS A";

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


        private bool KiemTonTai(GridView grvData, DataRow dr, int iCot, string sDLKiem, string sQuery, Boolean bKiemNull = true, string sform = "")
        {
            //null không kiểm
            if (bKiemNull)
            {//nếu null
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                //khác null
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text,sQuery)) == 0)
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
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text,sQuery)) == 0)
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
            return true;
        }



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
                //CHuyên ngành
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
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sXepLoai, "XEP_LOAI", "TEN_XL", false, this.Name))
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

                        string sSql = "INSERT INTO dbo.UNG_VIEN_BANG_CAP(ID_UV,CHUYEN_NGANH,TEN_TRUONG,TU_NAM,DEN_NAM,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[5].FieldName.ToString() + "]) FROM " + sbt + " AS A";
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
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 5;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }
                //Số năm 
                col = 6;
                try
                {
                    if (!string.IsNullOrEmpty(sTuNam) && !string.IsNullOrEmpty(sDenNam))
                    {
                        DateTime TN = Convert.ToDateTime(sTuNam);
                        DateTime DN = Convert.ToDateTime(sDenNam);
                        if(TN > DN)
                        {
                            dr.SetColumnError(grvData.Columns[5].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongduocTrong"));
                            dr["XOA"] = 1;
                            errorCount++;
                        }    
                        TimeSpan tim = DN - TN;
                        string s = (int)(tim.TotalDays / 365) + (Commons.Modules.TypeLanguage == 0 ? " Năm " : " Year ") + (int)((tim.TotalDays % 365) / 30) + (Commons.Modules.TypeLanguage == 0 ? " Tháng" : " Month");
                        dr[grvData.Columns[col].FieldName.ToString()] = s;
                    }
                }
                catch
                {
                }
                

                //lý do nghĩ
                col = 7;
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

                        string sSql = "INSERT INTO dbo.UNG_VIEN_KINH_NGHIEM(ID_UV,TEN_CONG_TY,CHUC_VU,MUC_LUONG,TU_NAM,DEN_NAM,SO_NAM,LD_NGHI_VIEC) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "],A.[" + grvData.Columns[7].FieldName.ToString() + "] FROM " + sbt + " AS A";
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
            {}
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
            if (cboChonSheet.Text == "") return;
            DataTable dtTmp = new DataTable();
            try
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = grvData.CalcHitInfo(pt);
                int col = -1;
                col = info.Column.AbsoluteIndex;
                if (col == -1)
                    return;
                int iSheet;
                iSheet = int.Parse(cboChonSheet.EditValue.ToString());
                System.Data.DataRow row = grvData.GetDataRow(info.RowHandle);
                switch (iSheet)
                {
                    case 0:
                        {
                            if (col == 9)
                            {
                                Commons.Modules.MExcel.KiemData("TRINH_DO_VAN_HOA", "TEN_TDVH", info.RowHandle, col, row);
                            }
                            if (col == 15)
                            {
                                Commons.Modules.MExcel.KiemData("THANH_PHO", "TEN_TP", info.RowHandle, col, row);
                            }
                            if (col == 16)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT * FROM dbo.QUAN WHERE ID_TP = (SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP =N'"+ row[14].ToString() +"')", "TEN_QUAN",col, row);
                            }
                            if (col == 17)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT * FROM dbo.PHUONG_XA WHERE ID_QUAN = (SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = N'"+ row[15] +"')", "TEN_PX", col, row);
                            }
                            if (col == 20)
                            {
                                Commons.Modules.MExcel.KiemData("NGUON_TUYEN_DUNG", "TEN_NTD", info.RowHandle, col, row);
                            }
                            //Người giới thiệu
                            if(col == 21)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT MS_CN,HO + ' '+ TEN AS HO_TEN,PHAI,NGAY_SINH FROM dbo.CONG_NHAN ORDER BY MS_CN", "HO_TEN", col, row);
                            }
                          
                            if (col == 22)
                            {
                                Commons.Modules.MExcel.KiemData("TAY_NGHE", "TEN_TAY_NGHE", info.RowHandle, col, row);
                            }
                            if (col == 25)
                            {
                                Commons.Modules.MExcel.KiemData("LOAI_CONG_VIEC", "TEN_LCV", info.RowHandle, col, row);
                            }
                           
                            break;
                        }
                    case 1:
                        {
                            if (col == 0)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT MS_UV,HO + ' '+ TEN,PHAI,NGAY_SINH FROM dbo.UNG_VIEN ORDER BY MS_UV", "MS_UV", col, row);
                            }
                            if (col == 5)
                            {
                                Commons.Modules.MExcel.KiemData("XEP_LOAI", "TEN_XL", info.RowHandle, col, row);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (col == 0)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT MS_UV,HO + ' '+ TEN,PHAI,NGAY_SINH FROM dbo.UNG_VIEN ORDER BY MS_UV", "MS_UV", col, row);

                            }
                            break;
                        }
                    case 3:
                        {
                            if (col == 0)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT MS_UV,HO + ' '+ TEN,PHAI,NGAY_SINH FROM dbo.UNG_VIEN ORDER BY MS_UV", "MS_UV", col, row);
                            }
                            if (col == 2)
                            {
                                Commons.Modules.MExcel.KiemData("XEP_LOAI", "TEN_XL", info.RowHandle, col, row);
                            }
                            break;
                        }


                    default: break;
                }
            }
            catch
            {
            }
            grvData.UpdateCurrentRow();
        }

        private void frmImportUngVien_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root ,windowsUIButton);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Bạn có chắc xóa dòng dữ liệu này ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                //GridView view = sender as GridView;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                //view.DeleteRow(view.FocusedRowHandle);
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
            }
        }
    }
}
