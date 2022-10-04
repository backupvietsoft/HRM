using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using DevExpress.XtraBars.Docking2010;
using System.Windows.Forms;
using Vs.Report;
using DevExpress.Utils;

namespace Vs.Recruit

{
    public partial class ucLyLichUV : DevExpress.XtraEditors.XtraUserControl
    {
        public Int64 iIDUV;
        bool cothem = false;
        public DataTable dt;
        public ucLyLichUV(Int64 iduv)
        {
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,tab, windowsUIButton);
            iIDUV = iduv;
        }
        private void ucLyLichUV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            windowsUIButton.Buttons[3].Properties.Visible = false;
            windowsUIButton.Buttons[4].Properties.Visible = false;
            //format date tiem
            Commons.OSystems.SetDateEditFormat(datNGAY_SINH);
            Commons.OSystems.SetDateEditFormat(datNGAY_CAP);
            LoadCombo();
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            if (Commons.Modules.iUngVien == -1)
            {
                cothem = true;
                BinDingData(cothem);
                enableButon(false);
            }
            else
            {
                cothem = false;
                BinDingData(cothem);
                enableButon(true);
            }
            Commons.Modules.sPS = "";
            Commons.Modules.ObjSystems.HideWaitForm();
        }


        private void LoadCombo()
        {
            try
            {
                //ID_TPLookUpEdit 
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TP, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", true, true);

                //ID_QUANLookEdit
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true, true);

                //ID_PXLookUpEdit
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", true, true);

                //ID_CVLookUpEdit
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TDVH, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV", "", true);

                //ID_TT_HNLookUpEdit 
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TT_HN, Commons.Modules.ObjSystems.DataTinHTrangHN(false), "ID_TT_HN", "TEN_TT_HN", "TEN_TT_HN", "");

                //ID_TDVHLookUpEdit ID_TDVH,TEN_TDVH
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TDVH, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");

                //cboID_KNLV
                //DataTable dt_knlv = new DataTable();
                //dt_knlv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboID_KNLV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_KNLV, dt_knlv, "ID_KNLV", "TEN_KNLV", "TEN_KNLV", "");

                // cboID_NTD
                DataTable dt_ntd = new DataTable();
                dt_ntd.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboID_NTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_NTD, dt_ntd, "ID_NTD", "TEN_NTD", "TEN_NTD", "");

                //cboPhai
                Commons.Modules.ObjSystems.MLoadLookUpEditN(PHAILookUpEdit, Commons.Modules.ObjSystems.DataPhai(), "ID_PHAI", "PHAI", "PHAI", "");

                //Hinh thuc tuyen
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboHINH_THUC_TUYEN, Commons.Modules.ObjSystems.DataHinhThucTuyen(false), "ID_HTT", "TEN_HT_TUYEN", "TEN_HT_TUYEN", "");

                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTiengAnh, Commons.Modules.ObjSystems.DataMucDoTieng(false), "ID_MD", "TEN_MD", "TEN_MD",true);

                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTiengHoa, Commons.Modules.ObjSystems.DataMucDoTieng(false), "ID_MD", "TEN_MD", "TEN_MD", true);

                //Vi tri tuyen dung
                //ID_LCV,TEN_LCV
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD_1, Commons.Modules.ObjSystems.DataLoaiCV(false,-1), "ID_LCV", "TEN_LCV", "TEN_LCV", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD_2, Commons.Modules.ObjSystems.DataLoaiCV(false,-1), "ID_LCV", "TEN_LCV", "TEN_LCV", true, true);
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboDanhGiaTayNghe, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), "ID_DGTN", "TEN_DGTN", "TEN_DGTN", "");

                //nguoi quen
                DataTable dt_CN = new DataTable();
                dt_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CN, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");

            }
            catch
            {
            }
        }
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
                        enableButon(false);
                        //cboDA_TUYEN_DUNG.Properties.ReadOnly = true;
                        break;
                    }
                case "sua":
                    {
                        if (iIDUV == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Commons.Modules.iUngVien = iIDUV;
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
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
                            DialogResult dl = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgMSUV_NayDaTonTai_taoMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if(dl == DialogResult.Yes)
                            {
                                txtMS_UV.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();
                            }
                            else
                            {
                                txtMS_UV.Focus();
                                return;
                            }
                       
                        }
                        if (SaveData())
                        {
                            Commons.Modules.iUngVien = iIDUV;
                            BinDingData(false);
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
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "in":
                    {
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_BANG_CAP WHERE ID_UV = " + iIDUV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_KINH_NGHIEM WHERE ID_UV = " + iIDUV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN_THONG_TIN_KHAC WHERE ID_UV = " + iIDUV + "");
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
        public void BinDingData(bool bthem)
        {
            try
            {
                dt = new DataTable();
                if (bthem == true)
                {
                    txtMS_UV.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_UNG_VIEN()").ToString();
                    HINH_UVPictureEdit.EditValue = "";
                    txtHO.EditValue = "";
                    txtTEN.EditValue = "";
                    datNGAY_SINH.EditValue = null;
                    PHAILookUpEdit.EditValue = 0;
                    //cboID_TDVH.EditValue = null;
                    //cboID_KNLV.EditValue = null;
                    txtNOI_SINH.EditValue = "";
                    txtSO_CMND.EditValue = "";
                    datNGAY_CAP.EditValue = null;
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
                    //cboHINH_THUC_TUYEN.EditValue = null;
                    txtHO_TEN_VC.EditValue = "";
                    txtNGHE_NGHIEP_VC.EditValue = "";
                    txtSO_CON.EditValue = "";
                    txtNGUOI_LIEN_HE.EditValue = "";
                    txtQUAN_HE.EditValue = "";
                    txtDT_NGUOI_LIEN_HE.EditValue = "";
                    cboID_VTTD_1.EditValue = null;
                    cboID_VTTD_2.EditValue = null;
                    txtMUC_LUONG_MONG_MUON.EditValue = "";
                    datNGAY_CO_THE_DI_LAM.EditValue = null;
                    cboID_CN.EditValue = null;
                    datNGAY_HEN_DL.EditValue = null;
                    datNGAY_NHAN_VIEC.EditValue = null;
                    //txtGHI_CHU.EditValue = "";
                    //cboDanhGiaTayNghe.EditValue = null;
                    chkXAC_NHAN_DL.EditValue = false;
                    chkDA_GIOI_THIEU.EditValue = false;
                    chkDA_CHUYEN.EditValue = false;
                    chkHUY_TUYEN_DUNG.EditValue = false;

                    cboTiengAnh.EditValue = null;
                    cboTiengHoa.EditValue = null;
                    txtTiengKhac.EditValue = "";
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
                        iIDUV = Convert.ToInt64(dt.Rows[0]["ID_UV"]);
                        txtMS_UV.EditValue = dt.Rows[0]["MS_UV"];
                        txtHO.EditValue = dt.Rows[0]["HO"];
                        txtTEN.EditValue = dt.Rows[0]["TEN"];
                        datNGAY_SINH.EditValue = dt.Rows[0]["NGAY_SINH"];
                        PHAILookUpEdit.EditValue = Convert.ToInt32(dt.Rows[0]["PHAI"]);
                        //cboHINH_THUC_TUYEN.EditValue = dt.Rows[0]["HINH_THUC_TUYEN"];
                        //cboID_TDVH.EditValue = dt.Rows[0]["ID_TDVH"];
                        //cboID_KNLV.EditValue = dt.Rows[0]["ID_KNLV"];
                        cboID_NTD.EditValue = dt.Rows[0]["ID_NTD"];
                        txtNOI_SINH.EditValue = dt.Rows[0]["NOI_SINH"];
                        txtSO_CMND.EditValue = dt.Rows[0]["SO_CMND"];
                        datNGAY_CAP.EditValue = dt.Rows[0]["NGAY_CAP"];
                        cboNOI_CAP.EditValue = dt.Rows[0]["NOI_CAP"];
                        cboID_TT_HN.EditValue = dt.Rows[0]["ID_TT_HN"];
                        txtEmail.EditValue = dt.Rows[0]["EMAIL"];
                        txtDT_DI_DONG.EditValue = dt.Rows[0]["DT_DI_DONG"];
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
                        //txtGHI_CHU.EditValue = dt.Rows[0]["GHI_CHU"];
                        chkXAC_NHAN_DL.EditValue = dt.Rows[0]["XAC_NHAN_DL"];
                        chkDA_GIOI_THIEU.EditValue = dt.Rows[0]["DA_GIOI_THIEU"];
                        chkDA_CHUYEN.EditValue = dt.Rows[0]["DA_CHUYEN"];
                        //cboDanhGiaTayNghe.EditValue = dt.Rows[0]["ID_DGTN"];
                        chkHUY_TUYEN_DUNG.EditValue = dt.Rows[0]["HUY_TUYEN_DUNG"];
                        chkXacNhanDTDH.EditValue = dt.Rows[0]["XAC_NHAN_DTDH"];

                        cboTiengAnh.EditValue = Convert.ToInt32(dt.Rows[0]["TIENG_ANH"]);
                        cboTiengHoa.EditValue = Convert.ToInt32(dt.Rows[0]["TIENG_TRUNG"]);

                        txtTiengKhac.EditValue = dt.Rows[0]["TIENG_KHAC"];

                    }
                    catch
                    {
                    }
                }
                LoadgrdBangCapUV();
                LoadgrdBangKNUV();
       
            }
            catch (Exception ex)
            {
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

            if(!visible)
            {
                Commons.Modules.ObjSystems.AddnewRow(grvBangCap, true);
                Commons.Modules.ObjSystems.AddnewRow(grvKNLV, true);
            }
            else
            {
                Commons.Modules.ObjSystems.DeleteAddRow(grvBangCap);
                Commons.Modules.ObjSystems.DeleteAddRow(grvKNLV);
            }
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
            datNGAY_HEN_DL.Properties.ReadOnly = true;
            datNGAY_HEN_DL.Properties.Buttons[0].Enabled = !datNGAY_HEN_DL.Properties.ReadOnly;
            datNGAY_NHAN_VIEC.Properties.ReadOnly = true;
            datNGAY_NHAN_VIEC.Properties.Buttons[0].Enabled = !datNGAY_NHAN_VIEC.Properties.ReadOnly;
       
            //txtGHI_CHU.Properties.ReadOnly = visible;
            datNGAY_SINH.Properties.ReadOnly = visible;
            datNGAY_SINH.Properties.Buttons[0].Enabled = !datNGAY_SINH.Properties.ReadOnly;
            PHAILookUpEdit.Properties.ReadOnly = visible;
            //cboHINH_THUC_TUYEN.Properties.ReadOnly = visible;
            //cboID_TDVH.Properties.ReadOnly = visible;
            //cboID_KNLV.Properties.ReadOnly = visible;
            cboID_NTD.Properties.ReadOnly = visible;
            txtNOI_SINH.Properties.ReadOnly = visible;
            txtSO_CMND.Properties.ReadOnly = visible;
            datNGAY_CAP.Properties.ReadOnly = visible;
            datNGAY_CAP.Properties.Buttons[0].Enabled = !datNGAY_CAP.Properties.ReadOnly;
            cboNOI_CAP.Properties.ReadOnly = visible;
            cboID_TT_HN.Properties.ReadOnly = visible;
            txtEmail.Properties.ReadOnly = visible;
            txtDT_DI_DONG.Properties.ReadOnly = visible;
            txtDT_NGUOI_LIEN_HE.Properties.ReadOnly = visible;
            txtDC_THUONG_TRU.Properties.ReadOnly = visible;
            cboID_TP.Properties.ReadOnly = visible;
            cboID_QUAN.Properties.ReadOnly = visible;
            cboID_PX.Properties.ReadOnly = visible;
            txtTHON_XOM.Properties.ReadOnly = visible;
            cboTiengAnh.Properties.ReadOnly = visible;
            cboTiengHoa.Properties.ReadOnly = visible;
            txtTiengKhac.Properties.ReadOnly = visible;
            //cboDanhGiaTayNghe.Properties.ReadOnly = visible;
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
            try
            {
                //tao bang tam
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTBC" + Commons.Modules.iIDUser,Commons.Modules.ObjSystems.ConvertDatatable(grdBangCap),"");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTKNLV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdKNLV), "");

             iIDUV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateUngVien",
            iIDUV,
            txtMS_UV.EditValue,
            imgToByteConverter(HINH_UVPictureEdit.Image),
            txtHO.EditValue,
            txtTEN.EditValue,
            datNGAY_SINH.Text.ToString() == "" ? DBNull.Value : datNGAY_SINH.EditValue,
            PHAILookUpEdit.Text.ToString() == "" ? DBNull.Value : PHAILookUpEdit.EditValue,
            //cboHINH_THUC_TUYEN.Text.ToString() == "" ? DBNull.Value : cboHINH_THUC_TUYEN.EditValue,
            //txtGHI_CHU.EditValue,
            /*ID_DT*/ DBNull.Value, DBNull.Value, DBNull.Value,
            txtNOI_SINH.EditValue,
            txtSO_CMND.EditValue,
            datNGAY_CAP.Text.ToString() == "" ? DBNull.Value : datNGAY_CAP.EditValue,
            cboNOI_CAP.Text.ToString() == "" ? DBNull.Value : cboNOI_CAP.EditValue,
            cboID_TT_HN.Text.ToString() == "" ? DBNull.Value : cboID_TT_HN.EditValue,
            txtHO_TEN_VC.EditValue,
            txtNGHE_NGHIEP_VC.EditValue,
            txtSO_CON.Text.ToString() == "" ? DBNull.Value : txtSO_CON.EditValue,
            txtDT_DI_DONG.EditValue,
            txtEmail.EditValue,
            txtDC_THUONG_TRU.EditValue,
            cboID_TP.Text.ToString() == "" ? DBNull.Value : cboID_TP.EditValue,
            cboID_QUAN.Text.ToString() == "" ? DBNull.Value : cboID_QUAN.EditValue,
            cboID_PX.Text.ToString() == "" ? DBNull.Value : cboID_PX.EditValue,
            txtTHON_XOM.EditValue,
            //cboID_TDVH.Text.ToString() == "" ? DBNull.Value : cboID_TDVH.EditValue,
            //cboID_KNLV.Text.ToString() == "" ? DBNull.Value : cboID_KNLV.EditValue,
            DBNull.Value, DBNull.Value,
            cboID_NTD.Text.ToString() == "" ? DBNull.Value : cboID_NTD.EditValue,
            cboID_VTTD_1.Text.ToString() == "" ? DBNull.Value : cboID_VTTD_1.EditValue,
            cboID_VTTD_2.Text.ToString() == "" ? DBNull.Value : cboID_VTTD_2.EditValue,
            txtMUC_LUONG_MONG_MUON.EditValue,
            datNGAY_CO_THE_DI_LAM.Text.ToString() == "" ? DBNull.Value : datNGAY_CO_THE_DI_LAM.EditValue,
            txtDT_NGUOI_LIEN_HE.EditValue,
            txtQUAN_HE.EditValue,
            txtDT_NGUOI_LIEN_HE.EditValue,
            cboID_CN.Text.ToString() == "" ? DBNull.Value : cboID_CN.EditValue,
            chkDA_GIOI_THIEU.EditValue,
            datNGAY_NHAN_VIEC.Text.ToString() == "" ? DBNull.Value : datNGAY_NHAN_VIEC.EditValue,
            datNGAY_HEN_DL.Text.ToString() == "" ? DBNull.Value : datNGAY_HEN_DL.EditValue,
            chkXAC_NHAN_DL.EditValue,
            chkHUY_TUYEN_DUNG.EditValue,
            chkDA_CHUYEN.EditValue,
            DBNull.Value,
            //cboDanhGiaTayNghe.Text.ToString() == "" ? DBNull.Value : cboDanhGiaTayNghe.EditValue,
            chkXacNhanDTDH.EditValue,
            cboTiengAnh.EditValue,cboTiengHoa.EditValue,txtTiengKhac.EditValue,
            cothem,"sBTBC" + Commons.Modules.iIDUser,"sBTKNLV" + Commons.Modules.iIDUser));
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        #endregion


        private void ID_TPLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (cboID_TP.EditValue == null || cboID_TP.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(cboID_TP.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true, true);
        }

        private void ID_QUANLookEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (cboID_QUAN.EditValue == null || cboID_QUAN.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(cboID_QUAN.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true, true);
        }

        private void LoadgrdBangCapUV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangCapUV", iIDUV, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            if (grdBangCap.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdBangCap, grvBangCap, dt, false, false, true, true, true, this.Name);
                grvBangCap.Columns["ID_BC"].Visible = false;
                grvBangCap.Columns["ID_UV"].Visible = false;

                Commons.Modules.ObjSystems.AddComboAnID("ID_XL", "TEN_XL", grvBangCap, Commons.Modules.ObjSystems.DataXepLoai(false));
            }
            else
            {
                grdBangCap.DataSource = dt;
            }
        }
        private void LoadgrdBangKNUV()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKinhNghiemUV", iIDUV, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grvKNLV.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKNLV, grvKNLV, dt, false, false, true, true, true, this.Name);
                grvKNLV.Columns["ID_KN"].Visible = false;
                grvKNLV.Columns["ID_UV"].Visible = false;
            }
            else
            {
                grdKNLV.DataSource = dt;
            }
            grvKNLV.Columns["MUC_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
            grvKNLV.Columns["MUC_LUONG"].DisplayFormat.FormatString =Commons.Modules.sSoLeTT;
        }

        private void grvBangCap_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    grvBangCap.DeleteSelectedRows();
                    return;
                }
                grvBangCap.SetFocusedRowCellValue("ID_UV", iIDUV);
            }
            catch
            {
            }
        }

        private void grvKNLV_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    grvKNLV.DeleteSelectedRows();
                    return;
                }
                grvKNLV.SetFocusedRowCellValue("ID_UV", iIDUV);
            }
            catch
            {
            }
        }

        private void grdBangCap_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (windowsUIButton.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvBangCap.DeleteSelectedRows();
            }
        }

        private void grdKNLV_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (windowsUIButton.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvKNLV.DeleteSelectedRows();
            }
        }

        private void cboID_TT_HN_EditValueChanged(object sender, EventArgs e)
        {
            if (windowsUIButton.Buttons[0].Properties.Visible == true) return;
            if(Convert.ToInt32(cboID_TT_HN.EditValue) == 2)
            {
                txtHO_TEN_VC.Properties.ReadOnly = true;
                txtNGHE_NGHIEP_VC.Properties.ReadOnly = true;
                txtSO_CON.Properties.ReadOnly = true;
            }    
            else
            {
                txtHO_TEN_VC.Properties.ReadOnly = false;
                txtNGHE_NGHIEP_VC.Properties.ReadOnly = false;
                txtSO_CON.Properties.ReadOnly = false;
            }    
        }
    }
}
