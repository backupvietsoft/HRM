using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using DevExpress.XtraBars.Docking2010;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using System.Collections.Generic;
using DevExpress.XtraLayout;
using Vs.Report;

namespace Vs.Recruit

{
    public partial class ucLyLichUV : DevExpress.XtraEditors.XtraUserControl
    {
        public Int64 iIDUV;

        bool cothem = false;
        public NavigationFrame back;
        public DataTable dt;

        public bool flag_Open = false; // flag = true uc mở bằng double click từ các form khác ,  flag = false ứng viên được mở từ danh sách ứng viên

        public Int64 iIDTB = -1;  //  Để nhập ứng viên từ chuột phải ucTHONG_BAO_TUYEN_DUNG
        public ucLyLichUV(Int64 iduv)
        {
            InitializeComponent();
            //Commons.Modules.ObjSystems.ThayDoiNN(this, Root, Tab, windowsUIButton);
            //Commons.Modules.ObjSystems.ThayDoiNN(this);
            iIDUV = iduv;
        }

        private void ucLyLichUV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadgrdBangCapUV();
            LoadgrdBangKNUV();
            LoadgrdTTKUV();

            //format date tiem
            Commons.OSystems.SetDateEditFormat(datNGAY_SINH);
            Commons.OSystems.SetDateEditFormat(txtNGAY_CAP);

            //ID_TPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TP, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

            //ID_TP_TAM_TRULookUpEdit 
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TP_TAM_TRU, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            ////ID_QUAN_TAM_TRULookUpEdit
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QUAN_TAM_TRU, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            ////ID_PX_TAM_TRULookUpEdit
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_PX_TAM_TRU, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

            //ID_CVLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TDVH, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV", "", true);


            //ID_DTLookUpEdit
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_DT, Commons.Modules.ObjSystems.DataDanToc(false), "ID_DT", "TEN_DT", "TEN_DT", "");

            //NOI_CAPLookupEdit 
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboNOI_CAP, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_TT_HNLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TT_HN, Commons.Modules.ObjSystems.DataTinHTrangHN(false), "ID_TT_HN", "TEN_TT_HN", "TEN_TT_HN", "");

            //ID_LOAI_TDLookUpEdit 
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_LOAI_TD, Commons.Modules.ObjSystems.DataLoaiTrinhDo(false), "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD", "");

            //ID_TDVHLookUpEdit ID_TDVH,TEN_TDVH
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TDVH, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");

            //cboID_KNLV
            DataTable dt_knlv = new DataTable();
            dt_knlv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboID_KNLV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_KNLV, dt_knlv, "ID_KNLV", "TEN_KNLV", "TEN_KNLV", "");

            // cboID_NTD
            DataTable dt_ntd = new DataTable();
            dt_ntd.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboID_NTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_NTD, dt_ntd, "ID_NTD", "TEN_NTD", "TEN_NTD", "");

            //cboTuyenDung
            //DataTable dt_td = new DataTable();
            //dt_td.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDaTuyenDung", Commons.Modules.TypeLanguage));
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboDA_TUYEN_DUNG, dt_td, "ID_TTTD", "TT_TUYEN_DUNG", "TT_TUYEN_DUNG", "");

            //cboPhai
            DataTable dt_Phai = new DataTable();
            dt_Phai.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhai", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(PHAILookUpEdit, dt_Phai, "ID_PHAI", "PHAI", "PHAI", "");

            //Hinh thuc tuyen
            DataTable dt_HTT = new DataTable();
            dt_HTT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHinhThucTuyen", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboHINH_THUC_TUYEN, dt_HTT, "ID_HTT", "TEN_HT_TUYEN", "TEN_HT_TUYEN", "");

            //Vi tri tuyen dung
            DataTable dt_VTTD = new DataTable();
            dt_VTTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_VTTD_1, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD", "");
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_VTTD_2, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD", "");

            //nguoi quen
            DataTable dt_CN = new DataTable();
            dt_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");

            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            BinDingData(cothem);

            Commons.Modules.sPS = "";
        }

        //===================Tung sua 14/09/2021

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn == null || btn.Tag == null) return;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        cothem = true;

                        iIDUV = -1;

                        BinDingData(true);
                        grdBangCap.DataSource = ((DataTable)grdBangCap.DataSource).Clone();
                        grdKNLV.DataSource = ((DataTable)grdKNLV.DataSource).Clone();
                        grdTTK.DataSource = ((DataTable)grdTTK.DataSource).Clone();
                        enableButon(false);
                        //cboDA_TUYEN_DUNG.Properties.ReadOnly = true;
                        break;
                    }
                case "sua":
                    {
                        iIDUV = Commons.Modules.iUngVien;
                        if (iIDUV == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = false;
                        enableButon(false);

                        break;
                    }

                case "xoa":
                    {
                        iIDUV = Commons.Modules.iUngVien;
                        if (iIDUV == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DeleteData() == true)
                        {
                            iIDUV = -1;
                            BinDingData(true);
                        }
                        //DeleteData();
                        //var x = back;
                        //back.SelectedPage = (INavigationPage)back.Pages[back.Pages.Count - 2];
                        //back.Refresh();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungMSUV", conn);
                        cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iIDUV;
                        cmd.Parameters.Add("@MS_UV", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(txtMS_UV.Text.Trim()) ? "" : txtMS_UV.Text.Trim();
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgMSUV_NayDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMS_UV.Focus();
                            return;
                        }
                        if (SaveData() == true)
                        {
                            enableButon(true);
                        }
                        break;
                    }
                case "khongluu":
                    {
                        if (iIDUV == -1)
                        {
                            BinDingData(true);
                        }
                        BinDingData(false);
                        enableButon(true);
                        dxValidationProvider1.Validate();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "in":
                    {
                        iIDUV = Commons.Modules.iUngVien;
                        if (iIDUV == -1)
                        {
                            return;
                        }
                        frmViewReport frm = new frmViewReport();
                        frm.rpt = new rptSoYeuLyLichUV(DateTime.Now);
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptSoYeuLyLichUV", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            //    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = -1;
                            //     cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                            //    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                            cmd.Parameters.Add("@ID_UV", SqlDbType.BigInt).Value = iIDUV;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adp.Fill(ds);

                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);


                        }
                        catch
                        {
                        }

                        frm.ShowDialog();
                        break;
                    }
                default:
                    break;
            }

        }
        #region function load
        #endregion
        private bool DeleteData()
        {
            if (iIDUV == -1)
                return false;
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteUngVien"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN WHERE ID_UV = " + iIDUV + "");
                BinDingData(false);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        #region function dung chung
        private void BinDingData(bool bthem)
        {
            dt = new DataTable();
            if (bthem == true)
            {
                txtMS_UV.EditValue = "";
                HINH_UVPictureEdit.EditValue = "";
                txtHO.EditValue = "";
                txtTEN.EditValue = "";

                datNGAY_SINH.EditValue = null;
                PHAILookUpEdit.EditValue = 0;
                cboID_TDVH.EditValue = null;
                cboID_KNLV.EditValue = null;
                //cboID_DT.EditValue = null;
                txtNOI_SINH.EditValue = "";
                txtSO_CMND.EditValue = "";
                txtNGAY_CAP.EditValue = null;
                cboNOI_CAP.EditValue = null;
                cboID_TT_HN.EditValue = null;
                txtDT_DI_DONG.EditValue = "";
                txtDT_NGUOI_LIEN_HE.EditValue = "";
                txtEmail.EditValue = "";
                txtDC_THUONG_TRU.EditValue = "";
                cboID_TP.EditValue = null;
                cboID_QUAN.EditValue = null;
                cboID_PX.EditValue = null;
                txtTHON_XOM.EditValue = "";
                cboID_NTD.EditValue = null;
                cboHINH_THUC_TUYEN.EditValue = null;
                txtHO_TEN_VC.EditValue = "";
                txtNGHE_NGHIEP_VC.EditValue = "";
                txtSO_CON.EditValue = "";
                txtNGUOI_LIEN_HE.EditValue = "";
                txtQUAN_HE.EditValue = "";
                txtDT_NGUOI_LIEN_HE.EditValue = "";
                cboID_VTTD_1.EditValue = null;
                cboID_VTTD_2.EditValue = null;
                txtMUC_LUONG_MONG_MUON.EditValue = "";
                datNGAY_CO_THE_DI_LAM.EditValue = DateTime.Now;
                cboID_CN.EditValue = null;
                datNGAY_HEN_DL.EditValue = DateTime.Now;
                datNGAY_NHAN_VIEC.EditValue = DateTime.Now;
                chkXAC_NHAN_DL.EditValue = false;
                chkDA_NHAN_VIEC.EditValue = false;
                chkDA_CHUYEN.EditValue = false;
                txtGHI_CHU.EditValue = "";
                chkHUY_TUYEN_DUNG.EditValue = false;

            }
            else
            {
                //lấy danh sách chi tiết công nhân 

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCTUNG_VIEN", Commons.Modules.iUngVien, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (dt.Rows.Count == 0) return;
                try
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dt.Rows[0]["HINH_UV"]);
                    MemoryStream mem = new MemoryStream(data);
                    HINH_UVPictureEdit.EditValue = Image.FromStream(mem);
                }
                catch
                {
                    HINH_UVPictureEdit.EditValue = null;
                }
                try
                {
                    txtMS_UV.EditValue = dt.Rows[0]["MS_UV"];
                    txtHO.EditValue = dt.Rows[0]["HO"];
                    txtTEN.EditValue = dt.Rows[0]["TEN"];
                    datNGAY_SINH.EditValue = dt.Rows[0]["NGAY_SINH"];
                    PHAILookUpEdit.EditValue = Convert.ToInt32(dt.Rows[0]["PHAI"]);
                    cboHINH_THUC_TUYEN.EditValue = dt.Rows[0]["HINH_THUC_TUYEN"];
                    cboID_TDVH.EditValue = dt.Rows[0]["ID_TDVH"];
                    cboID_KNLV.EditValue = dt.Rows[0]["ID_KNLV"];
                    cboID_NTD.EditValue = dt.Rows[0]["ID_NTD"];
                    //cboID_DT.EditValue = dt.Rows[0]["ID_DT"];
                    txtNOI_SINH.EditValue = dt.Rows[0]["NOI_SINH"];
                    txtSO_CMND.EditValue = dt.Rows[0]["SO_CMND"];
                    txtNGAY_CAP.EditValue = dt.Rows[0]["NGAY_CAP"];
                    cboNOI_CAP.EditValue = dt.Rows[0]["NOI_CAP"];
                    cboID_TT_HN.EditValue = dt.Rows[0]["ID_TT_HN"];
                    txtEmail.EditValue = dt.Rows[0]["EMAIL"];
                    txtDT_DI_DONG.EditValue = dt.Rows[0]["DT_DI_DONG"];
                    txtDT_NGUOI_LIEN_HE.EditValue = dt.Rows[0]["DT_NGUOI_THAN"];
                    txtDC_THUONG_TRU.EditValue = dt.Rows[0]["DIA_CHI_THUONG_TRU"];
                    cboID_TP.EditValue = dt.Rows[0]["ID_TP"];
                    cboID_QUAN.EditValue = dt.Rows[0]["ID_QUAN"];
                    cboID_PX.EditValue = dt.Rows[0]["ID_PX"];
                    txtTHON_XOM.EditValue = dt.Rows[0]["THON_XOM"];
                    // Mới
                    txtHO_TEN_VC.EditValue = dt.Rows[0]["HO_TEN_VC"];
                    txtNGHE_NGHIEP_VC.EditValue = dt.Rows[0]["NGHE_NGHIEP_VC"];
                    txtSO_CON.EditValue = dt.Rows[0]["SO_CON"];
                    txtNGUOI_LIEN_HE.EditValue = dt.Rows[0]["NGUOI_LIEN_HE"];
                    txtQUAN_HE.EditValue = dt.Rows[0]["QUAN_HE"];
                    txtDT_NGUOI_LIEN_HE.EditValue = dt.Rows[0]["DT_NGUOI_LIEN_HE"];
                    cboID_VTTD_1.EditValue = dt.Rows[0]["VI_TRI_TD_1"];
                    cboID_VTTD_2.EditValue = dt.Rows[0]["VI_TRI_TD_2"];
                    txtMUC_LUONG_MONG_MUON.EditValue = dt.Rows[0]["MUC_LUONG_MONG_MUON"];
                    datNGAY_CO_THE_DI_LAM.EditValue = dt.Rows[0]["NGAY_CO_THE_DI_LAM"];
                    cboID_CN.EditValue = dt.Rows[0]["ID_CN"];
                    datNGAY_HEN_DL.EditValue = dt.Rows[0]["NGAY_HEN_DI_LAM"];
                    datNGAY_NHAN_VIEC.EditValue = dt.Rows[0]["NGAY_NHAN_VIEC"];
                    chkXAC_NHAN_DL.EditValue = dt.Rows[0]["XAC_NHAN_DL"];
                    chkDA_NHAN_VIEC.EditValue = dt.Rows[0]["DA_NHAN_VIEC"];
                    chkDA_CHUYEN.EditValue = dt.Rows[0]["DA_CHUYEN"];
                    txtGHI_CHU.EditValue = dt.Rows[0]["GHI_CHU"];
                    chkHUY_TUYEN_DUNG.EditValue = dt.Rows[0]["HUY_TUYEN_DUNG"];

                    LoadgrdBangCapUV();
                    LoadgrdBangKNUV();
                    LoadgrdTTKUV();
                }
                catch
                {
                }
            }
        }

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;

            txtMS_UV.Properties.ReadOnly = visible;
            txtHO.Properties.ReadOnly = visible;
            txtTEN.Properties.ReadOnly = visible;
            txtHO_TEN_VC.Properties.ReadOnly = visible;
            txtNGHE_NGHIEP_VC.Properties.ReadOnly = visible;
            txtNGUOI_LIEN_HE.Properties.ReadOnly = visible;
            txtSO_CON.Properties.ReadOnly = visible;
            txtQUAN_HE.Properties.ReadOnly = visible;
            cboID_VTTD_1.Properties.ReadOnly = visible;
            cboID_VTTD_2.Properties.ReadOnly = visible;
            txtMUC_LUONG_MONG_MUON.Properties.ReadOnly = visible;
            datNGAY_CO_THE_DI_LAM.Properties.ReadOnly = visible;
            cboID_CN.Properties.ReadOnly = visible;
            datNGAY_HEN_DL.Properties.ReadOnly = visible;
            datNGAY_NHAN_VIEC.Properties.ReadOnly = visible;
            chkXAC_NHAN_DL.Properties.ReadOnly = visible;
            chkDA_NHAN_VIEC.Properties.ReadOnly = visible;
            chkDA_CHUYEN.Properties.ReadOnly = visible;
            txtGHI_CHU.Properties.ReadOnly = visible;
            datNGAY_SINH.Enabled = !visible;
            PHAILookUpEdit.Properties.ReadOnly = visible;
            cboHINH_THUC_TUYEN.Properties.ReadOnly = visible;
            //cboID_LOAI_TD.Properties.ReadOnly = visible;
            cboID_TDVH.Properties.ReadOnly = visible;
            cboID_KNLV.Properties.ReadOnly = visible;
            cboID_NTD.Properties.ReadOnly = visible;
            //cboDA_TUYEN_DUNG.Properties.ReadOnly = visible;
            //txtCHUYEN_MON.Properties.ReadOnly = visible;
            //txtGHI_CHU.Properties.ReadOnly = visible;
            //cboID_DT.Properties.ReadOnly = visible;
            //txtTON_GIAO.Properties.ReadOnly = visible;
            txtNOI_SINH.Properties.ReadOnly = visible;
            //txtNGUYEN_QUAN.Properties.ReadOnly = visible;
            txtSO_CMND.Properties.ReadOnly = visible;
            txtNGAY_CAP.Enabled = !visible;
            cboNOI_CAP.Properties.ReadOnly = visible;
            cboID_TT_HN.Properties.ReadOnly = visible;
            txtEmail.Properties.ReadOnly = visible;
            txtDT_DI_DONG.Properties.ReadOnly = visible;
            //txtDT_NHA.Properties.ReadOnly = visible;
            txtDT_NGUOI_LIEN_HE.Properties.ReadOnly = visible;
            //chkLD_NN.Properties.ReadOnly = visible;
            txtDC_THUONG_TRU.Properties.ReadOnly = visible;
            cboID_TP.Properties.ReadOnly = visible;
            cboID_QUAN.Properties.ReadOnly = visible;
            cboID_PX.Properties.ReadOnly = visible;
            txtTHON_XOM.Properties.ReadOnly = visible;
            cboDanhGiaTayNghe.Properties.ReadOnly = visible;
            //txtDIA_CHI_TAM_TRU.Properties.ReadOnly = visible;
            //cboID_TP_TAM_TRU.Properties.ReadOnly = visible;
            //cboID_QUAN_TAM_TRU.Properties.ReadOnly = visible;
            //cboID_PX_TAM_TRU.Properties.ReadOnly = visible;
            //txtTHON_XOM_TAM_TRU.Properties.ReadOnly = visible;
            //ID_DVLookUpEdit.Enabled = visible;
        }
        private byte[] imgToByteConverter(Image inImg)
        {

            ImageConverter imgCon = new ImageConverter();
            byte[] imgConvert = (byte[])imgCon.ConvertTo(inImg, typeof(byte[]));
            byte[] currentByteImageArray = imgConvert;
            double scale = 1f;
            try
            {
                MemoryStream inputMemoryStream = new MemoryStream(imgConvert);
                Image fullsizeImage = Image.FromStream(inputMemoryStream);
                while (currentByteImageArray.Length > 20000)
                {
                    Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                    MemoryStream resultStream = new MemoryStream();

                    fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                    currentByteImageArray = resultStream.ToArray();
                    resultStream.Dispose();
                    resultStream.Close();

                    scale -= 0.05f;
                }
            }
            catch
            {
            }
            return currentByteImageArray;
        }
        private bool SaveData()
        {
            //test();
            try
            {
                //iIDUV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateUngVien",
                //iIDUV,
                //txtMS_UV.EditValue,
                //imgToByteConverter(HINH_UVPictureEdit.Image),
                //txtHO.EditValue,
                //txtTEN.EditValue,
                //cboID_QG.Text.ToString() == "" ? DBNull.Value : cboID_QG.EditValue,
                //datNGAY_SINH.EditValue,
                ////datNAM_SINH.DateTime.Year,
                //PHAILookUpEdit.EditValue,
                //txtHINH_THUC_TUYEN.Text.ToString() == "" ? DBNull.Value : txtHINH_THUC_TUYEN.EditValue,
                ////cboID_LOAI_TD.Text.ToString() == "" ? DBNull.Value : cboID_LOAI_TD.EditValue,
                //cboID_TDVH.Text.ToString() == "" ? DBNull.Value : cboID_TDVH.EditValue,
                //cboID_KNLV.Text.ToString() == "" ? DBNull.Value : cboID_KNLV.EditValue,
                //cboID_NTD.Text.ToString() == "" ? DBNull.Value : cboID_NTD.EditValue,
                ////txtCHUYEN_MON.Text.ToString() == "" ? DBNull.Value : txtCHUYEN_MON.EditValue,
                //txtGHI_CHU.EditValue,
                ////cboDA_TUYEN_DUNG.EditValue,
                //cboID_DT.Text.ToString() == "" ? DBNull.Value : cboID_DT.EditValue,
                ////txtTON_GIAO.EditValue,
                //txtNOI_SINH.EditValue,
                ////txtNGUYEN_QUAN.EditValue,
                //txtSO_CMND.EditValue,
                //txtNGAY_CAP.Text.ToString() == "" ? DBNull.Value : txtNGAY_CAP.EditValue,
                //cboNOI_CAP.Text.ToString() == "" ? DBNull.Value : cboNOI_CAP.EditValue,
                //cboID_TT_HN.Text.ToString() == "" ? DBNull.Value : cboID_TT_HN.EditValue,
                //txtEMAIL.EditValue,
                //txtDT_DI_DONG.EditValue,
                ////txtDT_NHA.EditValue,
                //DT_NGUOI_THANTextEdit.EditValue,
                ////chkLD_NN.EditValue,
                //txtDC_THUONG_TRU.EditValue,
                //cboID_TP.Text.ToString() == "" ? DBNull.Value : cboID_TP.EditValue,
                //cboID_QUAN.Text.ToString() == "" ? DBNull.Value : cboID_QUAN.EditValue,
                //cboID_PX.Text.ToString() == "" ? DBNull.Value : cboID_PX.EditValue,
                //txtTHON_XOM.EditValue,
                //txtDIA_CHI_TAM_TRU.EditValue,
                //cboID_TP_TAM_TRU.Text.ToString() == "" ? DBNull.Value : cboID_TP_TAM_TRU.EditValue,
                //cboID_QUAN_TAM_TRU.Text.ToString() == "" ? DBNull.Value : cboID_QUAN_TAM_TRU.EditValue,
                //cboID_PX_TAM_TRU.Text.ToString() == "" ? DBNull.Value : cboID_PX_TAM_TRU.EditValue,
                //txtTHON_XOM_TAM_TRU.EditValue, cothem, flag, iIDTB, ""));

                iIDUV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateUngVien",
                iIDUV,
                imgToByteConverter(HINH_UVPictureEdit.Image)));

                Commons.Modules.iUngVien = iIDUV;
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }

        }

        #endregion

        private void ID_QGLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            //if (cboID_QG.EditValue.ToString() == "") return;
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_TP, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(cboID_QG.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", true);
        }

        private void ID_TPLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (cboID_TP.EditValue == null || cboID_TP.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(cboID_TP.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void ID_QUANLookEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (cboID_QUAN.EditValue == null || cboID_QUAN.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(cboID_QUAN.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }

        private void ID_TP_TAM_TRULookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            //if (cboID_TP_TAM_TRU.EditValue == null || cboID_TP_TAM_TRU.EditValue.ToString() == "") return;
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_QUAN_TAM_TRU, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(cboID_TP_TAM_TRU.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void ID_QUAN_TAM_TRULookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            //if (cboID_QUAN_TAM_TRU.EditValue == null || cboID_QUAN_TAM_TRU.EditValue.ToString() == "") return;
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_PX_TAM_TRU, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(cboID_QUAN_TAM_TRU.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }
        private void cboID_LOAI_TD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            //if (cboID_LOAI_TD.EditValue == null || cboID_LOAI_TD.EditValue.ToString() == "") return;
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_TDVH, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(cboID_LOAI_TD.EditValue), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", true);
        }

        private void txtMS_UV_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDanhSachUngVien frm = new frmDanhSachUngVien(-1);
            //iIDUV = frm.iID_UV;
            frm.iID_TB = -1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                iIDUV = frm.iID_UV;
                Commons.Modules.iUngVien = frm.iID_UV;
            }
            //ucCTQLUV uc = new ucCTQLUV(iIDUV);
            //uc.iID_UV = iIDUV;
            BinDingData(false);
        }

        private void LoadgrdBangCapUV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangCapUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdBangCap, grvBangCap, dt, true, true, true, true, true, this.Name);

            grvBangCap.Columns["ID_BC"].Visible = false;
            grvBangCap.Columns["ID_UV"].Visible = false;
            grvBangCap.Columns["ID_XL"].Visible = false;
            //grvDSNDPV.Columns["ID_TTD"].Visible = false;
        }
        private void LoadgrdBangKNUV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKinhNghiemUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdKNLV, grvKNLV, dt, true, true, true, true, true, this.Name);

            grvKNLV.Columns["ID_KN"].Visible = false;
            grvKNLV.Columns["ID_UV"].Visible = false;
        }

        private void LoadgrdTTKUV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListThongTinKhac", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdTTK, grvTTK, dt, true, true, true, true, true, this.Name);

            grvTTK.Columns["ID_TTK"].Visible = false;
            grvTTK.Columns["ID_UV"].Visible = false;
            grvTTK.Columns["ID_XL"].Visible = false;
        }
    }
}
