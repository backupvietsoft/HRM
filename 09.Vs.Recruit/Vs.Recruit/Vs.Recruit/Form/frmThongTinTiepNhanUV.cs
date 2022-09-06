using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmThongTinTiepNhanUV : DevExpress.XtraEditors.XtraForm
    {
        private ucCTQLUV ucUV;
        private long iID_UV;
        private int iMS_CV;
        public AccordionControl accorMenuleft;
        private int dem = 0;
        private string sNGayChuyen = "";
        private long iID_YCTD = -1;
        private long iID_VTTD = -1;
        public frmThongTinTiepNhanUV(Int64 idUV, int MS_CV, string ngayChuyen, Int64 ID_YCTD, Int64 ID_VTTD)
        {
            iID_UV = idUV;
            iMS_CV = MS_CV;
            sNGayChuyen = ngayChuyen;
            iID_YCTD = ID_YCTD;
            iID_VTTD = ID_VTTD;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup2);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup3, btnALL);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup4, windowsUIButtonPanel1);
        }
        #region even
        private void frmThongTinTiepNhanUV_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXepLoai, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), "ID_DGTN", "TEN_DGTN", "TEN_DGTN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_DT, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_CHUYEN, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_XN, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_CHUYEN, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);

                DataTable dt = new DataTable();
                string strSQL = "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE THU_VIEC = 1";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHDLD, dt, "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD", true, true);

                Commons.OSystems.SetDateEditFormat(datNgayGioiThieu);
                Commons.OSystems.SetDateEditFormat(datNgayHenDL);
                Commons.OSystems.SetDateEditFormat(datNgayHuyTD);
                Commons.OSystems.SetDateEditFormat(datNgayKTTayNghe);
                Commons.OSystems.SetDateEditFormat(datNGAY_NHAN_VIEC);
                EnabelButton(true);
                BindingData(false);
                Commons.Modules.sLoad = "";
                windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                if (iMS_CV == 1)
                {
                    TabKiemTraTayNghe.PageVisible = false;
                    txtMUC_LUONG_DN.Text = "";
                }
                if (sNGayChuyen != "")
                {
                    windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                    btnALL.Buttons[0].Properties.Visible = false;
                    btnALL.Buttons[1].Properties.Visible = false;
                }
            }
            catch { }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "sua":
                        {
                            if (TabThongTinTiepNhanUV.SelectedTabPage == TabThongTinNhanViec)
                            {
                                TabKiemTraTayNghe.PageEnabled = false;
                                TabDaoTaoDinhHuong.PageEnabled = false;
                                TabChuyenSangNS.PageEnabled = false;
                            }
                            else if (TabThongTinTiepNhanUV.SelectedTabPage == TabKiemTraTayNghe)
                            {
                                Commons.Modules.ObjSystems.AddnewRow(grvNDDT, true);
                                TabThongTinNhanViec.PageEnabled = false;
                                TabDaoTaoDinhHuong.PageEnabled = false;
                                TabChuyenSangNS.PageEnabled = false;
                            }
                            else if (TabThongTinTiepNhanUV.SelectedTabPage == TabDaoTaoDinhHuong)
                            {
                                TabThongTinNhanViec.PageEnabled = false;
                                TabKiemTraTayNghe.PageEnabled = false;
                                TabChuyenSangNS.PageEnabled = false;
                            }
                            else
                            {
                                TabThongTinNhanViec.PageEnabled = false;
                                TabKiemTraTayNghe.PageEnabled = false;
                                TabDaoTaoDinhHuong.PageEnabled = false;
                            }
                            windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                            EnabelButton(false);
                            break;
                        }
                    case "ghi":
                        {
                            try
                            {
                                switch (TabThongTinTiepNhanUV.SelectedTabPage.Name)
                                {
                                    case "TabThongTinNhanViec":
                                        {
                                            System.Data.SqlClient.SqlConnection conn;
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "THONG_TIN_NV";
                                            cmd.Parameters.Add("@DNgay2", SqlDbType.DateTime).Value = datNgayHenDL.Text == "" ? datNgayHenDL.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHenDL.Text);
                                            cmd.Parameters.Add("@DNgay3", SqlDbType.DateTime).Value = datNgayGioiThieu.Text == "" ? datNgayGioiThieu.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayGioiThieu.Text);
                                            cmd.Parameters.Add("@DNgay5", SqlDbType.DateTime).Value = datNgayHuyTD.Text == "" ? datNgayHuyTD.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHuyTD.Text);
                                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboID_LHDLD.Text == "" ? cboID_LHDLD.EditValue = null : Convert.ToInt64(cboID_LHDLD.EditValue);
                                            cmd.Parameters.Add("@fCot1", SqlDbType.Float).Value = txtMUC_LUONG_DN.Text == "" ? txtMUC_LUONG_DN.EditValue == null : txtMUC_LUONG_DN.EditValue;
                                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.ExecuteNonQuery();
                                            break;
                                        }
                                    case "TabKiemTraTayNghe":
                                        {
                                            string sBTNoiDung = "sBTNoiDung" + Commons.Modules.iIDUser;

                                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung, Commons.Modules.ObjSystems.ConvertDatatable(grdNDDT), "");
                                            System.Data.SqlClient.SqlConnection conn;
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "KIEM_TRA_TAY_NGHE";
                                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                                            cmd.Parameters.Add("@DNgay1", SqlDbType.DateTime).Value = datNgayKTTayNghe.Text == "" ? datNgayKTTayNghe.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayKTTayNghe.Text);
                                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboXepLoai.Text == "" ? cboXepLoai.EditValue = null : Convert.ToInt64(cboXepLoai.EditValue);
                                            cmd.Parameters.Add("@fCot1", SqlDbType.Float).Value = txtMUC_LUONG_DN.Text == "" ? txtMUC_LUONG_DN.EditValue == null : txtMUC_LUONG_DN.EditValue;
                                            cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBTNoiDung;
                                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.ExecuteNonQuery();

                                            Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
                                            break;
                                        }
                                    case "TabDaoTaoDinhHuong":
                                        {
                                            if (datNGAY_DT.Text == "")
                                            {
                                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayDaoTaoKhongDcTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                                            }

                                            System.Data.SqlClient.SqlConnection conn;
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "DAO_TAO_DINH_HUONG";
                                            cmd.Parameters.Add("@DNgay1", SqlDbType.DateTime).Value = datNGAY_DT.Text == "" ? datNGAY_DT.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_DT.Text);
                                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                                            cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = chkNQ_LaoDong.EditValue;
                                            cmd.Parameters.Add("@bCot2", SqlDbType.Bit).Value = chkTienLuongThuong.EditValue;
                                            cmd.Parameters.Add("@bCot3", SqlDbType.Bit).Value = chkThoaUocLD.EditValue;
                                            cmd.Parameters.Add("@bCot4", SqlDbType.Bit).Value = chkTieuChuanTNXH.EditValue;
                                            cmd.Parameters.Add("@bCot5", SqlDbType.Bit).Value = chkGiaiQuyetKN.EditValue;
                                            cmd.Parameters.Add("@bCot6", SqlDbType.Bit).Value = chkAnToanHC.EditValue;
                                            cmd.Parameters.Add("@bCot7", SqlDbType.Bit).Value = chkSoCapCuuBD.EditValue;
                                            cmd.Parameters.Add("@bCot8", SqlDbType.Bit).Value = chkPhanLoaiRacThai.EditValue;
                                            cmd.Parameters.Add("@bCot9", SqlDbType.Bit).Value = chkNoiQuyPCCC.EditValue;
                                            cmd.Parameters.Add("@bCot10", SqlDbType.Bit).Value = chkNoiQuyVSATLD.EditValue;
                                            cmd.Parameters.Add("@bCot11", SqlDbType.Bit).Value = chkThamNhungHL.EditValue;
                                            cmd.Parameters.Add("@bCot12", SqlDbType.Bit).Value = chkHoanThanhDT.EditValue;
                                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboID_NGUOI_DT.Text == "" ? cboID_NGUOI_DT.EditValue == null : cboID_NGUOI_DT.EditValue;
                                            cmd.Parameters.Add("@DNgay2", SqlDbType.DateTime).Value = datNgayHoanThanh.Text == "" ? datNgayHoanThanh.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHoanThanh.Text);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.ExecuteNonQuery();
                                            break;
                                        }
                                    case "TabChuyenSangNS":
                                        {
                                            if (!dxValidationProvider11.Validate()) return;
                                            System.Data.SqlClient.SqlConnection conn;
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "CHUYEN_SANG_NS";
                                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                                            cmd.Parameters.Add("@DNgay1", SqlDbType.DateTime).Value = datNGAY_NHAN_VIEC.Text == "" ? datNGAY_NHAN_VIEC.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_NHAN_VIEC.Text);
                                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboID_TO.EditValue;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.ExecuteNonQuery();
                                            windowsUIButtonPanel1.Buttons[0].Properties.Visible = true;
                                            break;
                                        }
                                }
                                TabChuyenSangNS.PageEnabled = true;
                                TabThongTinNhanViec.PageEnabled = true;
                                TabKiemTraTayNghe.PageEnabled = true;
                                TabDaoTaoDinhHuong.PageEnabled = true;
                                EnabelButton(true);
                                BindingData(false);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;
                        }

                    case "khongghi":
                        {
                            TabChuyenSangNS.PageEnabled = true;
                            TabThongTinNhanViec.PageEnabled = true;
                            TabKiemTraTayNghe.PageEnabled = true;
                            TabDaoTaoDinhHuong.PageEnabled = true;
                            dxValidationProvider11.ValidateHiddenControls = false;
                            dxValidationProvider11.RemoveControlError(cboID_TO);
                            dxValidationProvider11.RemoveControlError(datNGAY_NHAN_VIEC);
                            BindingData(false);
                            windowsUIButtonPanel1.Buttons[0].Properties.Visible = true;
                            EnabelButton(true);
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                }
            }
            catch
            {
            }
        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "chuyenDL":
                        {
                            if (!dxValidationProvider11.Validate()) return;
                            int iKiem = 0;
                            iKiem = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(UV.ID_LHDLD,0) ID_LHDLD FROM dbo.UNG_VIEN UV WHERE UV.ID_UV = " + iID_UV + ""));
                            if (iKiem == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonLoaiHopDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                            }
                            iKiem = KiemSLTuyen();
                            if (iKiem == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoLuongTuyenDaHet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                            }
                            if (iKiem == 2)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhieuDaKhoaBanKhongTheChuyen"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                            }
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacMuonChuyenDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "CHUYEN_SANG_NS";
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                            cmd.Parameters.Add("@sCot1", SqlDbType.NVarChar).Value = txtMS_CN.Text;
                            cmd.Parameters.Add("@sCot2", SqlDbType.NVarChar).Value = txtMS_THE_CC.Text;
                            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = cboID_NGUOI_CHUYEN.Text == "" ? cboID_NGUOI_CHUYEN.EditValue = null : cboID_NGUOI_CHUYEN.EditValue;
                            cmd.Parameters.Add("@DNgay2", SqlDbType.DateTime).Value = datNGAY_CHUYEN.Text == "" ? datNGAY_CHUYEN.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_CHUYEN.Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuyenDLThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sNGayChuyen = datNGAY_CHUYEN.Text;
                            BindingData(false);

                            windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                            btnALL.Buttons[0].Properties.Visible = false;
                            btnALL.Buttons[1].Properties.Visible = false;
                            break;
                        }
                }
            }
            catch { }
        }
        #endregion

        #region function
        private void EnabelButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;

            cboID_LHDLD.Properties.ReadOnly = visible;
            txtSO_NGAY.Properties.ReadOnly = true;
            txtMUC_LUONG_DN.Properties.ReadOnly = visible;
            datNgayGioiThieu.Properties.ReadOnly = visible;
            datNgayHenDL.Properties.ReadOnly = visible;
            datNgayHuyTD.Properties.ReadOnly = visible;

            datNgayKTTayNghe.Properties.ReadOnly = visible;
            txtTienThuong.Properties.ReadOnly = visible;
            cboXepLoai.Properties.ReadOnly = visible;
            grvNDDT.OptionsBehavior.Editable = !visible;

            datNGAY_DT.Properties.ReadOnly = visible;
            cboID_NGUOI_DT.Properties.ReadOnly = visible;
            chkNQ_LaoDong.Properties.ReadOnly = visible;
            chkTienLuongThuong.Properties.ReadOnly = visible;
            chkThoaUocLD.Properties.ReadOnly = visible;
            chkTieuChuanTNXH.Properties.ReadOnly = visible;
            chkGiaiQuyetKN.Properties.ReadOnly = visible;
            chkAnToanHC.Properties.ReadOnly = visible;
            chkSoCapCuuBD.Properties.ReadOnly = visible;
            chkPhanLoaiRacThai.Properties.ReadOnly = visible;
            chkNoiQuyPCCC.Properties.ReadOnly = visible;
            chkNoiQuyVSATLD.Properties.ReadOnly = visible;
            chkThamNhungHL.Properties.ReadOnly = visible;
            chkHoanThanhDT.Enabled = false;
            datNgayHoanThanh.Properties.ReadOnly = visible;

            datNGAY_NHAN_VIEC.Properties.ReadOnly = visible;
            cboID_TO.Properties.ReadOnly = visible;
        }

        #endregion

        private void TabThongTinTiepNhanUV_Click(object sender, EventArgs e)
        {
            if (sNGayChuyen != "")
            {
                return;
            }
            switch (TabThongTinTiepNhanUV.SelectedTabPage.Name)
            {
                case "TabThongTinNhanViec":
                    {
                        btnALL.Buttons[0].Properties.Visible = true;
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        break;
                    }
                case "TabKiemTraTayNghe":
                    {
                        btnALL.Buttons[0].Properties.Visible = true;
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        LoadNDDT();
                        break;
                    }
                case "TabDaoTaoDinhHuong":
                    {
                        btnALL.Buttons[0].Properties.Visible = true;
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        break;
                    }
                case "TabChuyenSangNS":
                    {
                        if (sNGayChuyen == "")
                        {
                            windowsUIButtonPanel1.Buttons[0].Properties.Visible = true;
                        }
                        break;
                    }
            }
        }

        private void LoadNDDT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "KIEM_TRA_TAY_NGHE";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNDDT, grvNDDT, dt, false, true, true, false, true, this.Name);
                //grvNoiDung.Columns["ID_NDDT"].Visible = false;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboNDDT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboNDDT.NullText = "";
                cboNDDT.ValueMember = "ID_NDDT";
                cboNDDT.DisplayMember = "TEN_NDDT";
                //ID_VTTD,TEN_VTTD

                cboNDDT.DataSource = Commons.Modules.ObjSystems.DataDanhNoiDungDT(false);
                cboNDDT.Columns.Clear();
                cboNDDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NDDT"));
                cboNDDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_NDDT"));
                cboNDDT.Columns["TEN_NDDT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NDDT");
                cboNDDT.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNDDT.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboNDDT.Columns["ID_NDDT"].Visible = false;
                grvNDDT.Columns["ID_NDDT"].ColumnEdit = cboNDDT;
                cboNDDT.BeforePopup += cboNDDT_BeforePopup;
                cboNDDT.EditValueChanged += cboNDDT_EditValueChanged;
            }
            catch (Exception ex)
            {
            }
        }
        private void cboNDDT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvNDDT.SetFocusedRowCellValue("ID_NDDT", Convert.ToInt64((dataRow.Row[0])));
        }

        private void cboNDDT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhNoiDungDT(false);
            }
            catch { }
        }
        private DataTable LoadThongTinUV()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_UV";
                cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                return dt;
            }
            catch
            {
                return null;
            }
        }
        private void BindingData(bool them)
        {
            if (them == true)
            {
                return;
            }
            else
            {
                LoadNDDT();
                DataTable dtTemp = new DataTable();
                dtTemp = LoadThongTinUV();
                // tap 1
                cboID_LHDLD.EditValue = dtTemp.Rows[0]["ID_LHDLD"];
                txtSO_NGAY.EditValue = dtTemp.Rows[0]["SO_NGAY"];
                txtMUC_LUONG_DN.EditValue = dtTemp.Rows[0]["MUC_LUONG_DN"];
                datNgayGioiThieu.EditValue = dtTemp.Rows[0]["NGAY_GIOI_THIEU"];
                datNgayHenDL.EditValue = dtTemp.Rows[0]["NGAY_HEN_DI_LAM"];
                datNgayHuyTD.EditValue = dtTemp.Rows[0]["NGAY_HUY_TD"];

                // tap danh gia tay nghe
                datNgayKTTayNghe.EditValue = dtTemp.Rows[0]["NGAY_KT_TAY_NGHE"];
                cboXepLoai.EditValue = dtTemp.Rows[0]["ID_DGTN"];
                txtTienThuong.EditValue = dtTemp.Rows[0]["MUC_THUONG_TN"];

                // tab dao tao dinh huong
                datNGAY_DT.EditValue = dtTemp.Rows[0]["NGAY_DT"];
                cboID_NGUOI_DT.EditValue = dtTemp.Rows[0]["ID_NGUOI_DT"];
                chkNQ_LaoDong.EditValue = dtTemp.Rows[0]["NQ_LD"];
                chkTienLuongThuong.EditValue = dtTemp.Rows[0]["TL_THUONG"];
                chkThoaUocLD.EditValue = dtTemp.Rows[0]["TU_LD"];
                chkTieuChuanTNXH.EditValue = dtTemp.Rows[0]["CS_TC"];
                chkGiaiQuyetKN.EditValue = dtTemp.Rows[0]["GQ_KN"];
                chkAnToanHC.EditValue = dtTemp.Rows[0]["AT_HC"];
                chkSoCapCuuBD.EditValue = dtTemp.Rows[0]["SO_CC"];
                chkPhanLoaiRacThai.EditValue = dtTemp.Rows[0]["PL_RT"];
                chkNoiQuyPCCC.EditValue = dtTemp.Rows[0]["NQ_PCCC"];
                chkNoiQuyVSATLD.EditValue = dtTemp.Rows[0]["NQ_VSATLD"];
                chkThamNhungHL.EditValue = dtTemp.Rows[0]["TN_HL"];
                chkHoanThanhDT.EditValue = dtTemp.Rows[0]["HOAN_THANH_DT"];
                datNgayHoanThanh.EditValue = dtTemp.Rows[0]["NGAY_HOAN_THANH_DT"];


                // chuyen nhân sự
                datNGAY_NHAN_VIEC.EditValue = dtTemp.Rows[0]["NGAY_NHAN_VIEC"];
                datNGAY_CHUYEN.EditValue = dtTemp.Rows[0]["NGAY_CHUYEN"];
                txtMS_CN.EditValue = dtTemp.Rows[0]["MS_CN"];
                txtMS_THE_CC.EditValue = dtTemp.Rows[0]["MS_THE_CC"];
                cboID_XN.EditValue = dtTemp.Rows[0]["ID_XN"];
                cboID_TO.EditValue = dtTemp.Rows[0]["ID_TO"];
                cboID_NGUOI_CHUYEN.EditValue = dtTemp.Rows[0]["ID_NGUOI_CHUYEN"];

            }
        }


        private void chkNQ_LaoDong_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkThoaUocLD_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkGiaiQuyetKN_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkSoCapCuuBD_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkNoiQuyPCCC_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkThamNhungHL_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkTienLuongThuong_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkTieuChuanTNXH_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkAnToanHC_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkPhanLoaiRacThai_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkNoiQuyVSATLD_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            chkHoanThanhDT_CheckedChanged(null, null);
        }

        private void chkHoanThanhDT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNQ_LaoDong.Checked == true && chkTienLuongThuong.Checked == true && chkThoaUocLD.Checked == true && chkTieuChuanTNXH.Checked == true && chkGiaiQuyetKN.Checked == true
                && chkAnToanHC.Checked == true && chkSoCapCuuBD.Checked == true && chkPhanLoaiRacThai.Checked == true && chkNoiQuyPCCC.Checked == true && chkNoiQuyVSATLD.Checked == true
                && chkThamNhungHL.Checked == true)
            {
                chkHoanThanhDT.Checked = true;
                datNgayHoanThanh.DateTime = DateTime.Now;
            }
            else
            {
                chkHoanThanhDT.Checked = false;
                datNgayHoanThanh.EditValue = null;
            }
        }
        private void cboID_XN_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TO, Commons.Modules.ObjSystems.DataTo(-1, Convert.ToInt32(cboID_XN.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, true);
        }

        private void cboID_LHDLD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                txtSO_NGAY.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + cboID_LHDLD.EditValue + "");
            }
            catch { }
        }

        private void grvNDDT_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvNDDT.ClearColumnErrors();
            try
            {
                DataTable dt = new DataTable();
                if (grvNDDT == null) return;
                if (grvNDDT.FocusedColumn.FieldName == "ID_NDDT")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
                        grvNDDT.SetColumnError(grvNDDT.Columns["ID_NDDT"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        grvNDDT.SetFocusedRowCellValue("ID_NDDT", e.Value);
                        dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grvNDDT);
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_NDDT").Equals(e.Value)) > 1)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            grvNDDT.SetColumnError(grvNDDT.Columns["ID_NDDT"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void grdNDDT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            {
                grvNDDT.DeleteSelectedRows();
            }
        }

        private void ThongTinTiepNhanUV_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private int KiemSLTuyen()
        {
            try
            {
                int Kiem = 0;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "CHUYEN_SANG_NS";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = iID_YCTD;
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = iID_VTTD;
                cmd.CommandType = CommandType.StoredProcedure;
                Kiem = Convert.ToInt32(cmd.ExecuteScalar());
                return Kiem;
            }
            catch
            {
                return 3;
            }
        }
    }
}
