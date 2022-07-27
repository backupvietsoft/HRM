using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
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
        string ChuoiKT = "";
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
                            case 3:
                                {
                                    ImportThongTinKhac(dtSource);
                                    break;
                                }
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
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 250, this.Name))
                {
                    errorCount++;
                }
                else
                {
                    if (!Commons.Modules.MExcel.KiemTrungDL(grvData, dtSource, dr, col, sMaSo, "UNG_VIEN", "MS_UV", this.Name))
                    {
                        errorCount++;
                    }
                }
                col = 1;
                //Họ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 50, this.Name))
                {
                    errorCount++;
                }
                col = 2;
                //Tên 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, true, 20, this.Name))
                {
                    errorCount++;
                }
                col = 3;
                //Giới tính
                string sGioiTinh = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sGioiTinh, "0"))
                {
                    errorCount++;
                }
                col = 4;
                //Ngày sinh   
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
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
                //Tình trạng HN
                string sTinhTrangHN = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTinhTrangHN, "TT_HON_NHAN", "TEN_TT_HN",false,this.Name))
                {
                    errorCount++;
                }
                col = 10;
                //Họ tên V/C  
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 11;
                //Nghề nghiệp V/C 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 12;
                //Số con
                string sSoCon = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sSoCon, -999999, -999999, false, this.Name))
                {
                    errorCount++;
                }
                col = 13;
                //Điện thoại  
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 20, this.Name))
                {
                    errorCount++;
                }
                col = 14;
                //Email 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 15;
                //Người liên hệ   
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 16;
                //Quan hệ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 50, this.Name))
                {
                    errorCount++;
                }
                col = 17;
                //ĐT Người liên hệ 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 20, this.Name))
                {
                    errorCount++;
                }
                col = 18;
                //Thành phố   
                string sThanhPho = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sThanhPho, "THANH_PHO", "TEN_TP", false, this.Name))
                {
                    errorCount++;
                }
                col = 19;
                //Quận 
                string sQuan = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sQuan, "QUAN", "TEN_QUAN", false, this.Name))
                {
                    errorCount++;
                }
                col = 20;
                //Phường xã 
                string sPhuongXa = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sPhuongXa, "PHUONG_XA", "TEN_PX", false, this.Name))
                {
                    errorCount++;
                }
                col = 21;
                //Thôn xóm 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 100, this.Name))
                {
                    errorCount++;
                }
                col = 22;
                //Địa chỉ
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 23;
                //Nguồn tuyển
                string sNguonTuyen = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sNguonTuyen, "NGUON_TUYEN_DUNG", "TEN_NTD", false, this.Name))
                {
                    errorCount++;
                }
                col = 24;
                //Người giới thiệu    
                string sNguoiGT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sNguoiGT, "CONG_NHAN", "HO", "TEN", this.Name))
                {
                    errorCount++;
                }
                col = 25;
                //Hình thức tuyển
                string sHinhThucTuyen = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sHinhThucTuyen, "HINH_THUC_TUYEN", "TEN_HT_TUYEN", false, this.Name))
                {
                    errorCount++;
                }
                col = 26;
                //Trình độ 
                string sTrinhDo = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sTrinhDo, "TRINH_DO_VAN_HOA", "TEN_TDVH", false, this.Name))
                {
                    errorCount++;
                }
                col = 27;
                //Kinh nghiệm 
                string sKinhNghiem = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sKinhNghiem, "KINH_NGHIEM_LV", "TEN_KNLV", false, this.Name))
                {
                    errorCount++;
                }
                col = 28;
                //Đánh giá tây nghề 
                string sDanhGia = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sDanhGia, "DANH_GIA_TAY_NGHE", "TEN_DGTN", false, this.Name))
                {
                    errorCount++;
                }
                col = 29;
                //Vị trí tuyển 1  
                string sVTT1 = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sVTT1, "LOAI_CONG_VIEC", "TEN_LCV"))
                {
                    errorCount++;
                }
                col = 30;
                //Vị trí tuyển 2  
                string sVTT2 = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemTonTai(grvData, dr, col, sVTT2, "LOAI_CONG_VIEC", "TEN_LCV", false, this.Name))
                {
                    errorCount++;
                }
                col = 31;
                //Ngày hẹn đi làm 
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }
                col = 32;
                //Xác nhận đi làm 
                string sXNDL = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sXNDL, "0"))
                {
                    errorCount++;
                }
                col = 33;
                //Ngày nhận việc 
                if (!Commons.Modules.MExcel.KiemDuLieuNgay(grvData, dr, col, false, this.Name))
                {
                    errorCount++;
                }
                col = 34;
                //Xác nhận đào tạo định hướng 
                string sDTDH = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sDTDH, "0"))
                {
                    errorCount++;
                }
                col = 35;
                //Chuyển sang nhân sự 
                string sCNS = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sCNS, "0"))
                {
                    errorCount++;
                }
                col = 36;
                //Ghi chú 
                if (!Commons.Modules.MExcel.KiemDuLieu(grvData, dr, col, false, 250, this.Name))
                {
                    errorCount++;
                }
                col = 37;
                //Đã giới thiệu   
                string sDGT = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sDGT, "0"))
                {
                    errorCount++;
                }
                col = 38;
                //Hủy tuyển dụng
                string sHTD = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuBool(grvData, dr, col, sHTD, "0"))
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
                        string sbt = "sBTUV" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN(MS_UV,HO,TEN,PHAI,NGAY_SINH,NOI_SINH,SO_CMND,NGAY_CAP,NOI_CAP,ID_TT_HN,HO_TEN_VC,NGHE_NGHIEP_VC,SO_CON,DT_DI_DONG,EMAIL,NGUOI_LIEN_HE,QUAN_HE,DT_NGUOI_LIEN_HE,ID_TP,ID_QUAN,ID_PX,THON_XOM,DIA_CHI_THUONG_TRU,ID_NTD,ID_CN,HINH_THUC_TUYEN,ID_TDVH,ID_KNLV,ID_DGTN,VI_TRI_TD_1,VI_TRI_TD_2,NGAY_HEN_DI_LAM,XAC_NHAN_DL,NGAY_NHAN_VIEC,XAC_NHAN_DTDH,DA_CHUYEN,GHI_CHU,DA_GIOI_THIEU,HUY_TUYEN_DUNG) SELECT [" + grvData.Columns[0].FieldName.ToString() + "],[" + grvData.Columns[1].FieldName.ToString() + "],[" + grvData.Columns[2].FieldName.ToString() + "],case [" + grvData.Columns[3].FieldName.ToString() + "] when 'Nam' then 1 else 0 end,CONVERT(datetime,[" + grvData.Columns[4].FieldName.ToString() + "],103),[" + grvData.Columns[5].FieldName.ToString() + "],[" + grvData.Columns[6].FieldName.ToString() + "],[" + grvData.Columns[7].FieldName.ToString() + "],[" + grvData.Columns[8].FieldName.ToString() + "],(SELECT TOP 1 ID_TT_HN FROM dbo.TT_HON_NHAN WHERE TEN_TT_HN = A.[" + grvData.Columns[9].FieldName.ToString() + "]),[" + grvData.Columns[10].FieldName.ToString() + "],[" + grvData.Columns[11].FieldName.ToString() + "],[" + grvData.Columns[12].FieldName.ToString() + "],[" + grvData.Columns[13].FieldName.ToString() + "],[" + grvData.Columns[14].FieldName.ToString() + "],[" + grvData.Columns[15].FieldName.ToString() + "],[" + grvData.Columns[16].FieldName.ToString() + "],[" + grvData.Columns[17].FieldName.ToString() + "],(SELECT TOP 1 ID_TP FROM dbo.THANH_PHO WHERE TEN_TP = A.[" + grvData.Columns[18].FieldName.ToString() + "]),(SELECT TOP 1 ID_QUAN FROM dbo.QUAN WHERE TEN_QUAN = A.[" + grvData.Columns[19].FieldName.ToString() + "]),(SELECT TOP 1 ID_PX FROM dbo.PHUONG_XA WHERE TEN_PX = A.[" + grvData.Columns[20].FieldName.ToString() + "]),[" + grvData.Columns[21].FieldName.ToString() + "],[" + grvData.Columns[22].FieldName.ToString() + "],(SELECT TOP 1 ID_NTD FROM dbo.NGUON_TUYEN_DUNG WHERE TEN_NTD= A.[" + grvData.Columns[23].FieldName.ToString() + "]),(SELECT TOP 1 ID_CN FROM dbo.CONG_NHAN WHERE HO +' '+TEN = A.[" + grvData.Columns[24].FieldName.ToString() + "]),(SELECT ID_HTT FROM dbo.HINH_THUC_TUYEN WHERE TEN_HT_TUYEN = A.[" + grvData.Columns[25].FieldName.ToString() + "]),(SELECT TOP 1 ID_TDVH FROM dbo.TRINH_DO_VAN_HOA WHERE TEN_TDVH = A.[" + grvData.Columns[26].FieldName.ToString() + "]),(SELECT TOP 1 ID_KNLV FROM dbo.KINH_NGHIEM_LV WHERE TEN_KNLV = A.[" + grvData.Columns[27].FieldName.ToString() + "]),(SELECT TOP 1 ID_DGTN FROM dbo.DANH_GIA_TAY_NGHE WHERE TEN_DGTN = A.[" + grvData.Columns[28].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[29].FieldName.ToString() + "]),(SELECT TOP 1 ID_LCV FROM dbo.LOAI_CONG_VIEC WHERE TEN_LCV = A.[" + grvData.Columns[30].FieldName.ToString() + "]),CONVERT(datetime,[" + grvData.Columns[31].FieldName.ToString() + "],103),[" + grvData.Columns[32].FieldName.ToString() + "],CONVERT(datetime,[" + grvData.Columns[33].FieldName.ToString() + "],103),[" + grvData.Columns[34].FieldName.ToString() + "],[" + grvData.Columns[35].FieldName.ToString() + "],[" + grvData.Columns[36].FieldName.ToString() + "],[" + grvData.Columns[37].FieldName.ToString() + "],[" + grvData.Columns[38].FieldName.ToString() + "]  FROM " + sbt + " AS A";

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
                //Tên bằng    
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
                        string sbt = "sBTUVBC" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_BANG_CAP(ID_UV,TEN_BANG,TEN_TRUONG,TU_NAM,DEN_NAM,ID_XL) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],(SELECT TOP 1 ID_XL FROM dbo.XEP_LOAI WHERE TEN_XL = A.[" + grvData.Columns[5].FieldName.ToString() + "]) FROM " + sbt + " AS A";
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
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sTuNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //Đến năm 
                col = 5;
                string sDenNam = dr[grvData.Columns[col].FieldName.ToString()].ToString();
                if (!Commons.Modules.MExcel.KiemDuLieuSo(grvData, dr, col, sDenNam, 0, -999999, false, this.Name))
                {
                    errorCount++;
                }
                //lý do nghĩ
                col = 6;
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
                        string sbt = "sBTUVKN" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                        string sSql = "INSERT INTO dbo.UNG_VIEN_KINH_NGHIEM(ID_UV,TEN_CONG_TY,CHUC_VU,MUC_LUONG,TU_NAM,DEN_NAM,LD_NGHI_VIEC) SELECT (SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN WHERE MS_UV = A.[" + grvData.Columns[0].FieldName.ToString() + "]),A.[" + grvData.Columns[1].FieldName.ToString() + "],A.[" + grvData.Columns[2].FieldName.ToString() + "],A.[" + grvData.Columns[3].FieldName.ToString() + "],A.[" + grvData.Columns[4].FieldName.ToString() + "],A.[" + grvData.Columns[5].FieldName.ToString() + "],A.[" + grvData.Columns[6].FieldName.ToString() + "] FROM " + sbt + " AS A";
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
                        string sbt = "sBTTK" + Commons.Modules.UserName;
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
                                Commons.Modules.MExcel.KiemData("TT_HON_NHAN", "TEN_TT_HN", info.RowHandle, col, row);
                            }
                            if (col == 18)
                            {
                                Commons.Modules.MExcel.KiemData("THANH_PHO", "TEN_TP", info.RowHandle, col, row);
                            }
                            if (col == 19)
                            {
                                Commons.Modules.MExcel.KiemData("QUAN", "TEN_QUAN", info.RowHandle, col, row);
                            }
                            if (col == 20)
                            {
                                Commons.Modules.MExcel.KiemData("PHUONG_XA", "TEN_PX", info.RowHandle, col, row);
                            }
                            if (col == 23)
                            {
                                Commons.Modules.MExcel.KiemData("NGUON_TUYEN_DUNG", "TEN_NTD", info.RowHandle, col, row);
                            }
                            //Người giới thiệu
                            if(col == 24)
                            {
                                Commons.Modules.MExcel.KiemData("SELECT MS_CN,HO + ' '+ TEN AS HO_TEN,PHAI,NGAY_SINH FROM dbo.CONG_NHAN ORDER BY MS_CN", "HO_TEN", col, row);
                            }
                            if (col == 25)
                            {
                                Commons.Modules.MExcel.KiemData("HINH_THUC_TUYEN", "TEN_HT_TUYEN", info.RowHandle, col, row);
                            }
                            if (col == 26)
                            {
                                Commons.Modules.MExcel.KiemData("TRINH_DO_VAN_HOA", "TEN_TDVH", info.RowHandle, col, row);
                            }
                            if (col == 27)
                            {
                                Commons.Modules.MExcel.KiemData("KINH_NGHIEM_LV", "TEN_KNLV", info.RowHandle, col, row);
                            }
                            if (col == 28)
                            {
                                Commons.Modules.MExcel.KiemData("DANH_GIA_TAY_NGHE", "TEN_DGTN", info.RowHandle, col, row);
                            }
                            if (col == 29)
                            {
                                Commons.Modules.MExcel.KiemData("LOAI_CONG_VIEC", "TEN_LCV", info.RowHandle, col, row);
                            }
                            if (col == 30)
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

      
    }
}
