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
    public partial class ThongTinTiepNhanUV : DevExpress.XtraEditors.XtraForm
    {
        private ucCTQLUV ucUV;
        private long iID_UV;
        public AccordionControl accorMenuleft;
        private int dem = 0;
        public ThongTinTiepNhanUV(Int64 idUV)
        {
            iID_UV = idUV;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup2);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup3, btnALL);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup4, windowsUIButtonPanel1);
        }
        #region even
        private void ThongTinTiepNhanUV_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXepLoai, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), "ID_DGTN", "TEN_DGTN", "TEN_DGTN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_DT, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_CHUYEN, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_XN, Commons.Modules.ObjSystems.DataXiNghiep(-1,false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TT_HD, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TT_HT, Commons.Modules.ObjSystems.DataTinHTrangHT(false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", true, true);
                Commons.OSystems.SetDateEditFormat(datNgayCoTheDL);
                Commons.OSystems.SetDateEditFormat(datNgayGioiThieu);
                Commons.OSystems.SetDateEditFormat(datNgayHenDL);
                Commons.OSystems.SetDateEditFormat(datNgayHuyTD);
                Commons.OSystems.SetDateEditFormat(datNgayKTTayNghe);
                Commons.OSystems.SetDateEditFormat(datNgayNhanViec);
                BindingData(false);
                Commons.Modules.sLoad = "";
                windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                EnabelButton(true);
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
                            EnabelButton(false);
                            break;
                        }
                    case "ghi":
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
                                        cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "TN_UNG_VIEN";
                                        cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "THONG_TIN_NV";
                                        cmd.Parameters.Add("@DNgay1", SqlDbType.DateTime).Value = datNgayCoTheDL.Text == "" ? datNgayCoTheDL.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayCoTheDL.Text);
                                        cmd.Parameters.Add("@DNgay2", SqlDbType.DateTime).Value = datNgayHenDL.Text == "" ? datNgayHenDL.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHenDL.Text);
                                        cmd.Parameters.Add("@DNgay3", SqlDbType.DateTime).Value = datNgayGioiThieu.Text == "" ? datNgayGioiThieu.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayGioiThieu.Text);
                                        cmd.Parameters.Add("@DNgay4", SqlDbType.DateTime).Value = datNgayNhanViec.Text == "" ? datNgayNhanViec.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayNhanViec.Text);
                                        cmd.Parameters.Add("@DNgay5", SqlDbType.DateTime).Value = datNgayHuyTD.Text == "" ? datNgayHuyTD.EditValue = null : Commons.Modules.ObjSystems.ConvertDateTime(datNgayHuyTD.Text);
                                        cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iID_UV;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.ExecuteNonQuery();

                                        EnabelButton(true);
                                        break;
                                    }
                                case "TabChuyenSangNS":
                                    {
                                        if (!dxValidationProvider11.Validate()) return;
                                        break;
                                    }
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
                            BindingData(false);
                            EnabelButton(true);
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch
            {
            }
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

            datNgayCoTheDL.Properties.ReadOnly = visible;
            datNgayGioiThieu.Properties.ReadOnly = visible;
            datNgayHenDL.Properties.ReadOnly = visible;
            datNgayHuyTD.Properties.ReadOnly = visible;
            datNgayNhanViec.Properties.ReadOnly = visible;

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

            datNGAY_CHUYEN.Properties.ReadOnly = visible;
            cboID_NGUOI_CHUYEN.Properties.ReadOnly = visible;
            cboID_XN.Properties.ReadOnly = visible;
            cboID_TO.Properties.ReadOnly = visible;
            txtMS_CN.Properties.ReadOnly = visible;
            txtMS_THE_CC.Properties.ReadOnly = visible;
            cboID_TT_HD.Properties.ReadOnly = visible;
            cboID_TT_HT.Properties.ReadOnly = visible;
        }

        #endregion

        private void TabThongTinTiepNhanUV_Click(object sender, EventArgs e)
        {
            switch (TabThongTinTiepNhanUV.SelectedTabPage.Name)
            {
                case "TabThongTinNhanViec":
                    {
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        break;
                    }
                case "TabKiemTraTayNghe":
                    {
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        LoadNDDT();
                        break;
                    }
                case "TabDaoTaoDinhHuong":
                    {
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = false;
                        break;
                    }
                case "TabChuyenSangNS":
                    {
                        windowsUIButtonPanel1.Buttons[0].Properties.Visible = true;
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
                datNgayCoTheDL.EditValue = dtTemp.Rows[0]["NGAY_CO_THE_DI_LAM"];
                datNgayGioiThieu.EditValue = dtTemp.Rows[0]["NGAY_GIOI_THIEU"];
                datNgayHenDL.EditValue = dtTemp.Rows[0]["NGAY_HEN_DI_LAM"];
                datNgayHuyTD.EditValue = dtTemp.Rows[0]["NGAY_HUY_TD"];
                datNgayNhanViec.EditValue = dtTemp.Rows[0]["NGAY_NHAN_VIEC"];

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
                datNGAY_CHUYEN.EditValue = dtTemp.Rows[0]["NGAY_CHUYEN"];
                txtMS_CN.EditValue  = dtTemp.Rows[0]["MS_CN"];
                txtMS_THE_CC.EditValue  = dtTemp.Rows[0]["MS_THE_CC"];
                cboID_XN.EditValue  = dtTemp.Rows[0]["ID_XN"];
                cboID_TO.EditValue  = dtTemp.Rows[0]["ID_TO"];
                cboID_TT_HD.EditValue  = dtTemp.Rows[0]["ID_TT_HD"];
                cboID_TT_HT.EditValue  = dtTemp.Rows[0]["ID_TT_HT"];
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
            if(chkNQ_LaoDong.Checked == true && chkTienLuongThuong.Checked == true && chkThoaUocLD.Checked == true && chkTieuChuanTNXH.Checked == true && chkGiaiQuyetKN.Checked == true
                && chkAnToanHC.Checked == true && chkSoCapCuuBD.Checked == true && chkPhanLoaiRacThai.Checked == true && chkNoiQuyPCCC.Checked == true && chkNoiQuyVSATLD.Checked == true
                && chkThamNhungHL.Checked == true)
            {
                chkHoanThanhDT.Checked = true;
            }
            else
            {
                chkHoanThanhDT.Checked = false;
            }
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {

        }

        private void cboID_XN_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TO, Commons.Modules.ObjSystems.DataTo(-1, Convert.ToInt32(cboID_XN.EditValue) , false), "ID_TO", "TEN_TO", "TEN_TO", true, true);
        }
    }
}
