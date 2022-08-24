using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using System.Threading;
using Spire.Xls;
using DataTable = System.Data.DataTable;
using Workbook = Spire.Xls.Workbook;
using Worksheet = Spire.Xls.Worksheet;
using System.Drawing;
using System.Collections.Generic;
using DevExpress.XtraLayout;

namespace Vs.Recruit
{
    public partial class ucQLUV : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        public LabelControl lblUV;
        public ucQLUV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }

        private void ucQLUV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCombo();
            Commons.Modules.sLoad = "";
            LoadUNG_VIEN(-1);
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        ucCTQLUV dl = new ucCTQLUV(-1);
                        navigationFrame1.SelectedPage.Visible = false;
                        PageDetails.Controls.Add(dl);
                        dl.Dock = DockStyle.Fill;
                        dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                        Thread thread = new Thread(delegate ()
                        {
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new MethodInvoker(delegate
                                {
                                    navigationFrame1.SelectedPage = PageDetails;
                                }));
                            }
                        }, 100);
                        thread.Start();
                        accorMenuleft.Visible = false;
                        break;
                    }
                case "sua":
                    {
                        grvUngVien_DoubleClick(null, null);
                        break;
                    }
                case "xoa":
                    {
                        if (grvUngVien.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DeleteData())
                        {
                            LoadUNG_VIEN(-1);
                        }
                        break;
                    }
                case "export":
                    {
                        string sPath = "";
                        sPath = Commons.Modules.MExcel.SaveFiles("Excel Files (*.xls;)|*.xls;|Excel Files (*.Xlsx;)|*.Xlsx;|" + "All Files (*.*)|*.*");
                        if (sPath == "") return;
                        Workbook book = new Workbook();
                        Worksheet sheet = book.Worksheets[0];
                        ExportUngVien(sPath);
                        break;
                    }
                case "import":
                    {
                        frmImportUngVien frm = new frmImportUngVien();
                        frm.ShowDialog();
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

        private void ExportUngVien(string sPath)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                string SQL = "SELECT TOP 0 MS_UV AS  N'Mã số',HO AS N'Họ',TEN AS N'Tên',PHAI AS N'Giới tính',NGAY_SINH AS N'Ngày sinh',NOI_SINH AS N'Nơi sinh',SO_CMND AS N'CMND',NGAY_CAP AS N'Ngày cấp',NOI_CAP AS N'Nơi cấp',CONVERT(NVARCHAR(250), ID_TT_HN) AS N'Tình trạng HN',HO_TEN_VC AS N'Họ tên V/C',NGHE_NGHIEP_VC AS N'Nghề nghiệp V/C',SO_CON AS N'Số con',DT_DI_DONG AS N'Điện thoại',EMAIL AS N'Email',NGUOI_LIEN_HE AS N'Người liên hệ',QUAN_HE AS N'Quan hệ',DT_NGUOI_LIEN_HE AS N'ĐT Người liên hệ',CONVERT(NVARCHAR(250), ID_TP) AS N'Thành phố',CONVERT(NVARCHAR(250), ID_QUAN) AS N'Quận',CONVERT(NVARCHAR(250), ID_PX) AS N'Phường xã',THON_XOM AS N'Thôn xóm',DIA_CHI_THUONG_TRU AS N'Địa chỉ',CONVERT(NVARCHAR(250), ID_NTD) AS N'Nguồn tuyển',CONVERT(NVARCHAR(250), ID_CN) AS N'Người giới thiệu',CONVERT(NVARCHAR(250), HINH_THUC_TUYEN) AS N'Hình thức tuyển',CONVERT(NVARCHAR(250), ID_TDVH) AS N'Trình độ',CONVERT(NVARCHAR(250), ID_KNLV) AS N'Kinh nghiệm',CONVERT(NVARCHAR(250), ID_DGTN) AS N'Đánh giá tây nghề',CONVERT(NVARCHAR(250), VI_TRI_TD_1) AS N'Vị trí tuyển 1',CONVERT(NVARCHAR(250), VI_TRI_TD_2) AS N'Vị trí tuyển 2',NGAY_HEN_DI_LAM AS N'Ngày hẹn đi làm',XAC_NHAN_DL AS N'Xác nhận đi làm',NGAY_NHAN_VIEC AS N'Ngày nhận việc',XAC_NHAN_DTDH AS N'Xác nhận đào tạo định hướng',DA_CHUYEN AS N'Chuyển sang nhân sự',GHI_CHU AS N'Ghi chú',DA_GIOI_THIEU AS N'Đã giới thiệu',HUY_TUYEN_DUNG AS N'Hủy tuyển dụng'FROM dbo.UNG_VIEN";

                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));

                //export datatable to excel
                Workbook book = new Workbook();
                Worksheet sheet1 = book.Worksheets[0];
                sheet1.Name = "01-Danh sách ứng viên";
                sheet1.DefaultColumnWidth = 20;

                sheet1.InsertDataTable(dtTmp, true, 1, 1);

                sheet1.Range[2, 1].Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();

                sheet1.Range[1, 1, 1, 39].Style.WrapText = true;
                sheet1.Range[1, 1, 1, 39].Style.VerticalAlignment = VerticalAlignType.Center;
                sheet1.Range[1, 1, 1, 39].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet1.Range[1, 1, 1, 39].Style.Font.IsBold = true;

                sheet1.Range[1, 1].Style.Font.Color = Color.Red;
                sheet1.Range[1, 2].Style.Font.Color = Color.Red;
                sheet1.Range[1, 3].Style.Font.Color = Color.Red;
                sheet1.Range[1, 30].Style.Font.Color = Color.Red;


                sheet1.Range[1, 1].Comment.RichText.Text = "Mã ứng viên sẽ được đặt theo cấu trúc MUV-000001 trong đó(MUV-: cố định,còn 000001 sẽ được tăng thêm 1 khi có một ứng viên mới).";
                sheet1.Range[1, 4].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataPhai());
                sheet1.Range[1, 10].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataTinHTrangHN(false));
                sheet1.Range[1, 19].Comment.RichText.Text = "Nhập đúng cấp tỉnh/thành phố trong danh mục.";
                sheet1.Range[1, 20].Comment.RichText.Text = "Nhập đúng cấp quận/huyện trong danh mục.";
                sheet1.Range[1, 21].Comment.RichText.Text = "Nhập đúng cấp phường/xã trong danh mục.";
                sheet1.Range[1, 24].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataNguonTD(false));
                sheet1.Range[1, 25].Comment.RichText.Text = "Họ và tên nhân viên trong công ty giới thiệu.";
                sheet1.Range[1, 26].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataHinhThucTuyen(false));
                sheet1.Range[1, 27].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataTDVH(-1,false));
                sheet1.Range[1, 28].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataKinhNghiemLV(false));
                sheet1.Range[1, 29].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false));
                sheet1.Range[1, 30].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false));
                sheet1.Range[1, 31].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataLoaiCV(false));

                sheet1.Range[1, 33].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
                sheet1.Range[1, 36].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
                sheet1.Range[1, 38].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";
                sheet1.Range[1, 39].Comment.RichText.Text = "Nếu có thì nhập:1\nkhông thì nhập:0";

                sheet1.FreezePanes(2,4);
                //Tên trường Từ năm	Đến năm	Xếp loại

                Worksheet sheet2 = book.Worksheets[1];
                sheet2.Name = "02-Bằng cấp";
                sheet2.DefaultColumnWidth = 20;

                sheet2.Range[1, 1].Text = "Mã số";
                sheet2.Range[1, 2].Text = "Tên bằng";
                sheet2.Range[1, 3].Text = "Tên trường";
                sheet2.Range[1, 4].Text = "Từ năm";
                sheet2.Range[1, 5].Text = "Đến năm";
                sheet2.Range[1, 6].Text = "Xếp loại";
                sheet2.Range[1, 6].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

                sheet2.Range[1, 1, 1, 6].Style.WrapText = true;
                sheet2.Range[1, 1, 1, 6].Style.VerticalAlignment = VerticalAlignType.Center;
                sheet2.Range[1, 1, 1, 6].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet2.Range[1, 1, 1, 6].Style.Font.IsBold = true;


                Worksheet sheet3 = book.Worksheets[2];
                sheet3.Name = "03-Kinh nghiệm làm việc";
                sheet3.DefaultColumnWidth = 20;

                sheet3.Range[1, 1].Text = "Mã số";
                sheet3.Range[1, 2].Text = "Tên công ty";
                sheet3.Range[1, 3].Text = "Chức vụ";
                sheet3.Range[1, 4].Text = "Mức lương";
                sheet3.Range[1, 5].Text = "Từ năm";
                sheet3.Range[1, 6].Text = "Đến năm";
                sheet3.Range[1, 7].Text = "Lý do nghĩ";

                sheet3.Range[1, 1, 1, 7].Style.WrapText = true;
                sheet3.Range[1, 1, 1, 7].Style.VerticalAlignment = VerticalAlignType.Center;
                sheet3.Range[1, 1, 1, 7].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet3.Range[1, 1, 1, 7].Style.Font.IsBold = true;

                Worksheet sheet4 = book.Worksheets.Add("04-Thông tin khác");
                sheet4.DefaultColumnWidth = 20;

                sheet4.Range[1, 1].Text = "Mã số";
                sheet4.Range[1, 2].Text = "Nội dung";
                sheet4.Range[1, 3].Text = "Xếp loại";

                sheet4.Range[1, 3].Comment.RichText.Text = Commons.Modules.ObjSystems.ConvertCombototext(Commons.Modules.ObjSystems.DataXepLoai(false));

                sheet4.Range[1, 1, 1, 3].Style.WrapText = true;
                sheet4.Range[1, 1, 1, 3].Style.VerticalAlignment = VerticalAlignType.Center;
                sheet4.Range[1, 1, 1, 3].Style.HorizontalAlignment = HorizontalAlignType.Center;
                sheet4.Range[1, 1, 1, 3].Style.Font.IsBold = true;

                book.SaveToFile(sPath);
                System.Diagnostics.Process.Start(sPath);
            }
            catch
            {
            }
        }

        private void LoadCombo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboDA_TUYEN_DUNG, Commons.Modules.ObjSystems.DataTinhTrangTD(true), "ID_TTTD", "Ten_TTTD", "Ten_TTTD");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboYeuCauTD, Commons.Modules.ObjSystems.DataYeuCauTD(true, 1), "ID_YCTD", "MA_YCTD", "MA_YCTD");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KHPV, Commons.Modules.ObjSystems.DataKeHoachPV(true,-1), "ID_KHPV", "SO_KHPV", "SO_KHPV");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, Commons.Modules.ObjSystems.DataLoaiCV(true), "ID_LCV", "TEN_LCV", "TEN_LCV");
            }
            catch
            {
            }
        }

        private void LoadUNG_VIEN(Int64 iIdUV)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDA_TUYEN_DUNG.EditValue, cboYeuCauTD.EditValue, cboID_KHPV.EditValue, cboID_VTTD.EditValue));
                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_UV"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdUngVien, grvUngVien, dtTmp, false, true, false, true, true, this.Name);
                grvUngVien.Columns["ID_UV"].Visible = false;

                if (iIdUV != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdUV));
                    grvUngVien.FocusedRowHandle = grvUngVien.GetRowHandle(index);
                }
            }
            catch { }
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            try { lblUV.Text = lblUV.Tag.ToString(); } catch { }
            navigationFrame1.SelectedPage = pageList;
            PageDetails.Controls[0].Visible = false;
            PageDetails.Controls[0].Dispose();
            accorMenuleft.Visible = true;
            LoadUNG_VIEN(Commons.Modules.iUngVien);
        }
        private bool DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteUngVien"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
            //xóa
            try
            {
                Int64 iID = Convert.ToInt64(grvUngVien.GetFocusedRowCellValue("ID_UV"));
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_BANG_CAP WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_KINH_NGHIEM WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_THONG_TIN_KHAC WHERE ID_UV = " + iID + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN WHERE ID_UV = " + iID + "");
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }


        private void cboDA_TUYEN_DUNG_EditValueChanged(object sender, EventArgs e)
        {
            LoadUNG_VIEN(-1);
        }

        private void grvUngVien_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblUV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblUV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                lblUV.Text = grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["MS_UV"]).ToString() + " - " + grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["HO_TEN"]).ToString();
            }
            catch { }
            ucCTQLUV dl = new ucCTQLUV(Convert.ToInt64(grvUngVien.GetFocusedRowCellValue(grvUngVien.Columns["ID_UV"])));
            navigationFrame1.SelectedPage.Visible = false;
            PageDetails.Controls.Add(dl);
            dl.Dock = DockStyle.Fill;
            dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            Thread thread = new Thread(delegate ()
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        navigationFrame1.SelectedPage = PageDetails;
                    }));
                }
            }, 100);
            thread.Start();
            accorMenuleft.Visible = false;
        }

        private void grdUngVien_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (grvUngVien.RowCount == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (DeleteData())
                {
                    LoadUNG_VIEN(-1);
                }
            }
        }
    }
}
