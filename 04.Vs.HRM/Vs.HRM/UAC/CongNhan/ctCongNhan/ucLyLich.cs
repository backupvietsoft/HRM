using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using DevExpress.XtraBars.Docking2010;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using Vs.Report;
using System.Collections.Generic;
using DevExpress.XtraLayout;

namespace Vs.HRM
{
    public partial class ucLyLich : DevExpress.XtraEditors.XtraUserControl
    {
        bool cothem = false;
        Int64 idcn = -1;
        public NavigationFrame back;
        public DataTable dt;

        public ucLyLich(Int64 id)
        {
            InitializeComponent();
            //LoadNgonNgu();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, Tab, windowsUIButton);
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            idcn = id;
        }

        private void ucLyLich_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            //format date tiem
            Commons.OSystems.SetDateEditFormat(NGAY_SINHDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HOC_VIECDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_THU_VIECDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_VAO_LAMDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_NGHI_VIECDateEdit);

            Commons.OSystems.SetDateEditFormat(NGAY_CAPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_CAP_GPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_DBHXHDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HH_GPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_CHAM_DUT_NOP_BHXHDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HET_HANDateEdit);

            //đơn vị 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_DVLookUpEdit, Commons.Modules.ObjSystems.DataDonVi(true), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);

            //xí nghiệp 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

            //tổ
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);

            //ID_QGLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QGLookUpEdit, Commons.Modules.ObjSystems.DataQuocGia(false), "ID_QG", "TEN_QG", "TEN_QG", "");

            //ID_TPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUANLookEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PXLookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

            //ID_TP_TAM_TRULookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TP_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_QUAN_TAM_TRULookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUAN_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            //ID_PX_TAM_TRULookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PX_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

            //ID_CVLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV", "", true);

            //ID_LCVLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false), "ID_LCV", "TEN_LCV", "TEN_LCV", "", true);

            //ID_LHDLDLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LHDLDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiHDLD(false), "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD", "", true);

            //ID_TT_HDLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HDLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", "", true);

            //ID_TT_HTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HTLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHT(false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", "", true);

            //ID_LD_TVLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LD_TVLookUpEdit, Commons.Modules.ObjSystems.DataLyDoThoiViec(), "ID_LD_TV", "TEN_LD_TV", "TEN_LD_TV", "");

            //ID_DTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_DTLookUpEdit, Commons.Modules.ObjSystems.DataDanToc(false), "ID_DT", "TEN_DT", "TEN_DT", "");

            //NOI_CAPLookupEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(NOI_CAPLookupEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_TT_HNLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HNLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHN(false), "ID_TT_HN", "TEN_TT_HN", "TEN_TT_HN", "");

            //ID_LOAI_TDLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LOAI_TDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiTrinhDo(false), "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD", "");

            //ID_TDVHLookUpEdit ID_TDVH,TEN_TDVH
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TDVHLookUpEdit, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");

            //ID_TINH_THANH_KHAM_BENH 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(TINH_THANHLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_BENH_VIEN_KHAM_BENH 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(BENH_VIENLookUpEdit, Commons.Modules.ObjSystems.DataBenhVien(false), "ID_BV", "TEN_BV", "TEN_BV", "");

            //LOAI_QUOC_TICHLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(LOAI_QUOC_TICHLookUpEdit, Commons.Modules.ObjSystems.DataLoaiQuocTich(false), "ID_LOAI_QT", "TEN_LOAI_QT", "TEN_LOAI_QT", "");

            //CAP_GIAY_PHEPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(CAP_GIAY_PHEPLookUpEdit, Commons.Modules.ObjSystems.DataCapGiayPhep(false), "ID_CAP_GIAY_PHEP", "TEN_CAP_GIAY_PHEP", "TEN_CAP_GIAY_PHEP", "");

            //CAP_GIAY_PHEPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(LD_GIAM_LDNNLookUpEdit, Commons.Modules.ObjSystems.DataLyDoGiamLDNN(false), "ID_LDG_LDNN", "TEN_LDG_LDNN", "TEN_LDG_LDNN", "");

            // PHAILookUpEdit
            DataTable dt_Phai = new DataTable();
            dt_Phai.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhai", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(PHAILookupEdit, dt_Phai, "ID_PHAI", "PHAI", "PHAI", "");

            // KHU_VUCLookUpEdit
            DataTable dt_kv = new DataTable();
            dt_kv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKHU_VUC", Commons.Modules.UserName, Commons.Modules.TypeLanguage,0));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_KV, dt_kv, "ID_KV", "TEN_KV", "TEN_KV", "");


            enableButon(true);
            Tab.SelectedTabPage = groupThongTinBoXung;
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            if (Commons.Modules.iCongNhan == -1)
                cothem = true;
            BinDingData(cothem);
            Commons.Modules.sLoad = "";
        }

        private void LoadCmbLoc(int intType)
        {
            try
            {


                switch (intType)
                {
                    case 1:
                        {
                            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);

                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", true);

                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TP_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", true);

                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LOAI_TDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiTrinhDo(false), "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD", true);

                            break;
                        }
                    case 2:
                        {
                            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

                            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(ID_DVLookUpEdit.EditValue), Convert.ToInt32(ID_XNLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);

                            if (ID_LOAI_TDLookUpEdit.EditValue.ToString() != "")
                            {
                                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TDVHLookUpEdit, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(ID_LOAI_TDLookUpEdit.EditValue), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");
                            }

                            //ID_TPLookUpEdit 
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                            //ID_QUANLookEdit
                            if (ID_TPLookUpEdit.EditValue.ToString() != "")
                            {
                                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUANLookEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(ID_TPLookUpEdit.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");
                            }

                            //ID_PXLookUpEdit
                            if (ID_QUANLookEdit.EditValue.ToString() != "")
                            {
                                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PXLookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(ID_QUANLookEdit.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", "");
                            }

                            //ID_TP_TAM_TRULookUpEdit 
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TP_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                            //ID_QUAN_TAM_TRULookUpEdit
                            if (ID_TP_TAM_TRULookUpEdit.EditValue.ToString() != "")
                            {
                                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUAN_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(ID_TP_TAM_TRULookUpEdit.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");
                            }

                            //ID_PX_TAM_TRULookUpEdit
                            if (ID_QUAN_TAM_TRULookUpEdit.EditValue.ToString() != "")
                            {
                                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PX_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(ID_QUAN_TAM_TRULookUpEdit.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", "");
                            }

                            break;
                        }

                    case 3:
                        {
                            //xí nghiệp 
                            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

                            //tổ
                            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);

                            //ID_TPLookUpEdit 
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                            //ID_QUANLookEdit
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUANLookEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

                            //ID_PXLookUpEdit
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PXLookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

                            //ID_TP_TAM_TRULookUpEdit 
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TP_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                            //ID_QUAN_TAM_TRULookUpEdit
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUAN_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

                            //ID_PX_TAM_TRULookUpEdit
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PX_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

                            ////ID_TDVHLookUpEdit ID_TDVH,TEN_TDVH
                            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TDVHLookUpEdit, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");

                            break;
                        }
                    default:
                        break;
                }


                //xí nghiệp 
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

                //tổ
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);

                //ID_TPLookUpEdit 
                //Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");
                //ID_QUANLookEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUANLookEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

                //ID_PXLookUpEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PXLookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

                //ID_TP_TAM_TRULookUpEdit 
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TP_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                //ID_QUAN_TAM_TRULookUpEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_QUAN_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

                //ID_PX_TAM_TRULookUpEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_PX_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");

                //ID_TDVHLookUpEdit ID_TDVH,TEN_TDVH
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TDVHLookUpEdit, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(-1), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", "");
            }
            catch { }
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
                        idcn = -1;
                        LoadCmbLoc(1);


                        BinDingData(true);
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = false;
                        idcn = Commons.Modules.iCongNhan;
                        LoadCmbLoc(2);

                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DeleteData();
                        //var x = back;
                        //back.SelectedPage = (INavigationPage)back.Pages[back.Pages.Count - 2];
                        //back.Refresh();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        if (MS_CNTextEdit.Text != "") if (!kiemtrung(1)) return;
                        if (MS_THE_CCTextEdit.Text != "") if (!kiemtrung(2)) return;
                        if (!kiemtrung(3)) return;
                        SaveData();
                        BinDingData(false);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.sLoad = "0Load";
                        LoadCmbLoc(3);
                        BinDingData(false);
                        enableButon(true);
                        dxValidationProvider1.Validate();
                        Commons.Modules.sLoad = "";
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "giadinh":
                    {
                        frmGiaDinh gd = new frmGiaDinh(HOTextEdit.EditValue + " " + TENTextEdit.EditValue);
                        gd.ShowDialog();
                        break;
                    }
                case "doanthe":
                    {
                        frmDoanThe gd = new frmDoanThe();
                        gd.ShowDialog();
                        break;
                    }

                case "lienlac":
                    {
                        frmThongTinLienLac gd = new frmThongTinLienLac(HOTextEdit.EditValue + " " + TENTextEdit.EditValue);
                        gd.ShowDialog();
                        break;
                    }
                case "in":
                    {
                        frmViewReport frm = new frmViewReport();
                        frm.rpt = new rptSoYeuLyLich(DateTime.Now);
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptSoYeuLyLich", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            //    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = -1;
                            //     cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                            //    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                            cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            DataSet ds = new DataSet();
                            adp.Fill(ds);

                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);
                            //DataTable dtQTLV, DataTable dtQTLuong, DataTable dtQTDT, DataTable dtHDLD, DataTable dtQTKT, DataTable dtQTKL, DataTable dtQTDG, DataTable dtQHGD

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
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.CONG_NHAN WHERE ID_CN = " + Commons.Modules.iCongNhan + "");
                BinDingData(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return;
            }
        }

        #region function dung chung
        private void LoadNgonNgu()
        {
            DataTable dtNN = new DataTable();
            dtNN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNN_ucLyLich", "ucLyLich", Commons.Modules.TypeLanguage));

            ItemForMS_CN.Text = dtNN.Rows[0]["ItemForMS_CN"].ToString();
            ItemForMS_THE_CC.Text = dtNN.Rows[0]["ItemForMS_THE_CC"].ToString();
            ItemForID_QG.Text = dtNN.Rows[0]["ItemForID_QG"].ToString();
            ItemForHO.Text = dtNN.Rows[0]["ItemForHO"].ToString();
            ItemForTEN.Text = dtNN.Rows[0]["ItemForTEN"].ToString();
            ItemForTEN_KHONG_DAU.Text = dtNN.Rows[0]["ItemForTEN_KHONG_DAU"].ToString();
            ItemForNGAY_SINH.Text = dtNN.Rows[0]["ItemForNGAY_SINH"].ToString();
            ItemForNAM_SINH.Text = dtNN.Rows[0]["ItemForNAM_SINH"].ToString();
            ItemForPHAI.Text = dtNN.Rows[0]["ItemForPHAI"].ToString();
            ItemForID_DV.Text = dtNN.Rows[0]["ItemForID_DV"].ToString();
            ItemForID_XN.Text = dtNN.Rows[0]["ItemForID_XN"].ToString();
            ItemForID_TO.Text = dtNN.Rows[0]["ItemForID_TO"].ToString();
            ItemForID_CV.Text = dtNN.Rows[0]["ItemForID_CV"].ToString();
            ItemForID_LCV.Text = dtNN.Rows[0]["ItemForID_LCV"].ToString();
            ItemForPHEP_CT.Text = dtNN.Rows[0]["ItemForPHEP_CT"].ToString();
            ItemForNGAY_HOC_VIEC.Text = dtNN.Rows[0]["ItemForNGAY_HOC_VIEC"].ToString();
            ItemForNGAY_THU_VIEC.Text = dtNN.Rows[0]["ItemForNGAY_THU_VIEC"].ToString();
            ItemForNGAY_VAO_LAM.Text = dtNN.Rows[0]["ItemForNGAY_VAO_LAM"].ToString();
            ItemForID_TT_HD.Text = dtNN.Rows[0]["ItemForID_TT_HD"].ToString();
            ItemForID_TT_HT.Text = dtNN.Rows[0]["ItemForID_TT_HT"].ToString();
            ItemForID_LHDLD.Text = dtNN.Rows[0]["ItemForID_LHDLD"].ToString();
            ItemForHINH_THUC.Text = dtNN.Rows[0]["ItemForHINH_THUC"].ToString();
            LD_TINHCheckEdit.Text = dtNN.Rows[0]["LD_TINHCheckEdit"].ToString();
            TRUC_TIEP_SXCheckEdit.Text = dtNN.Rows[0]["TRUC_TIEP_SXCheckEdit"].ToString();
            LAO_DONG_CNCheckEdit.Text = dtNN.Rows[0]["LAO_DONG_CNCheckEdit"].ToString();
            VAO_LAM_LAICheckEdit.Text = dtNN.Rows[0]["VAO_LAM_LAICheckEdit"].ToString();
            ItemForNGAY_NGHI_VIEC.Text = dtNN.Rows[0]["ItemForNGAY_NGHI_VIEC"].ToString();
            ItemForID_LD_TV.Text = dtNN.Rows[0]["ItemForID_LD_TV"].ToString();

            groupThongTinBoXung.Text = dtNN.Rows[0]["groupThongTinBoXung"].ToString();
            ItemForSO_CMND.Text = dtNN.Rows[0]["ItemForSO_CMND"].ToString();
            ItemForNGAY_CAP.Text = dtNN.Rows[0]["ItemForNGAY_CAP"].ToString();
            ItemForNOI_CAP.Text = dtNN.Rows[0]["ItemForNOI_CAP"].ToString();
            ID_TT_HNLookUpEdit.Text = dtNN.Rows[0]["ID_TT_HNLookUpEdit"].ToString();
            ItemForMS_THUE.Text = dtNN.Rows[0]["ItemForMS_THUE"].ToString();
            ItemForEMAIL.Text = dtNN.Rows[0]["ItemForEMAIL"].ToString();
            ItemForMA_THE_ATM.Text = dtNN.Rows[0]["ItemForMS_THE_ATM"].ToString();
            ItemForSO_TAI_KHOAN.Text = dtNN.Rows[0]["ItemForSO_TAI_KHOAN"].ToString();
            ItemForCHUYEN_MON.Text = dtNN.Rows[0]["ItemForCHUYEN_MON"].ToString();
            ItemForID_LOAI_TD.Text = dtNN.Rows[0]["ItemForID_LOAI_TD"].ToString();
            ItemForID_TDVH.Text = dtNN.Rows[0]["ItemForID_TDVH"].ToString();
            ItemForNGOAI_NGU.Text = dtNN.Rows[0]["ItemForNGOAI_NGU"].ToString();
            ItemForDT_NHA.Text = dtNN.Rows[0]["ItemForDT_NHA"].ToString();
            ItemForDT_NGUOI_THAN.Text = dtNN.Rows[0]["ItemForDT_NGUOI_THAN"].ToString();
            ItemForDT_DI_DONG.Text = dtNN.Rows[0]["ItemForDT_DI_DONG"].ToString();

            groupTamTruThuongTru.Text = dtNN.Rows[0]["groupTamTruThuongTru"].ToString();
            ItemForNOI_SINH.Text = dtNN.Rows[0]["ItemForNOI_SINH"].ToString();
            ItemForNGUYEN_QUAN.Text = dtNN.Rows[0]["ItemForNGUYEN_QUAN"].ToString();
            ItemForID_DT.Text = dtNN.Rows[0]["ItemForID_DT"].ToString();
            ItemForTON_GIAO.Text = dtNN.Rows[0]["ItemForTON_GIAO"].ToString();
            ItemForDIA_CHI_THUONG_TRU.Text = dtNN.Rows[0]["ItemForDIA_CHI_THUONG_TRU"].ToString();
            ItemForID_TP.Text = dtNN.Rows[0]["ItemForID_TP"].ToString();
            ItemForID_QUAN.Text = dtNN.Rows[0]["ItemForID_QUAN"].ToString();
            ItemForID_PX.Text = dtNN.Rows[0]["ItemForID_PX"].ToString();
            ItemForTHON_XOM.Text = dtNN.Rows[0]["ItemForTHON_XOM"].ToString();
            ItemForDIA_CHI_TAM_TRU.Text = dtNN.Rows[0]["ItemForDIA_CHI_TAM_TRU"].ToString();
            ItemForID_TP_TAM_TRU.Text = dtNN.Rows[0]["ItemForID_TP_TAM_TRU"].ToString();
            ItemForID_QUAN_TAM_TRU.Text = dtNN.Rows[0]["ItemForID_QUAN_TAM_TRU"].ToString();
            ItemForTHON_XOM_TAM_TRU.Text = dtNN.Rows[0]["ItemForTHON_XOM_TAM_TRU"].ToString();

            groupThongTinBaoHiem.Text = dtNN.Rows[0]["groupThongTinBaoHiem"].ToString();
            ItemForSO_BHXH.Text = dtNN.Rows[0]["ItemForSO_BHXH"].ToString();
            ItemForNGAY_DBHXH.Text = dtNN.Rows[0]["ItemForNGAY_DBHXH"].ToString();
            ItemForNGAY_CHAM_DUT_NOP_BHXH.Text = dtNN.Rows[0]["ItemForNGAY_CHAM_DUT_NOP_BHXH"].ToString();
            THAM_GIA_BHXHCheckEdit.Text = dtNN.Rows[0]["THAM_GIA_BHXHCheckEdit"].ToString();
            ItemForSO_THE_BHYT.Text = dtNN.Rows[0]["ItemForSO_THE_BHYT"].ToString();
            ItemForNGAY_HET_HAN.Text = dtNN.Rows[0]["ItemForNGAY_HET_HAN"].ToString();
            ItemForTINH_THANH.Text = dtNN.Rows[0]["ItemForTINH_THANH"].ToString();
            ItemForBENH_VIEN.Text = dtNN.Rows[0]["ItemForBENH_VIEN"].ToString();
            LD_NNCheckEdit.Text = dtNN.Rows[0]["LD_NNCheckEdit"].ToString();
            ItemForSO_GIAY_PHEP.Text = dtNN.Rows[0]["ItemForSO_GIAY_PHEP"].ToString();
            ItemForNGAY_CAP_GP.Text = dtNN.Rows[0]["ItemForNGAY_CAP_GP"].ToString();
            ItemForLOAI_QUOC_TICH.Text = dtNN.Rows[0]["ItemForLOAI_QUOC_TICH"].ToString();
            ItemForCAP_GIAY_PHEP.Text = dtNN.Rows[0]["ItemForCAP_GIAY_PHEP"].ToString();
            ItemForNGAY_HH_GP.Text = dtNN.Rows[0]["ItemForNGAY_HH_GP"].ToString();
            ItemForLD_GIAM_LDNN.Text = dtNN.Rows[0]["ItemForLD_GIAM_LDNN"].ToString();
        }

        private void BinDingData(bool bthem)
        {
            dt = new DataTable();

            if (bthem == true)
            {
                HINH_CNPictureEdit.EditValue = "";
                MS_CNTextEdit.EditValue = "";
                MS_THE_CCTextEdit.EditValue = "";
                ID_QGLookUpEdit.EditValue = Convert.ToInt64(234);
                HOTextEdit.EditValue = "";
                TENTextEdit.EditValue = "";
                TEN_KHONG_DAUTextEdit.EditValue = "";
                NGAY_SINHDateEdit.EditValue = null;
                txtNamSinh.EditValue = null;
                PHAILookupEdit.EditValue = 0;
                ID_TOLookupEdit.EditValue = null;
                ID_CVLookUpEdit.EditValue = null;
                ID_LCVLookUpEdit.EditValue = null;
                PHEP_CTTextEdit.EditValue = 0;
                NGAY_HOC_VIECDateEdit.EditValue = null;
                NGAY_THU_VIECDateEdit.EditValue = null;
                NGAY_VAO_LAMDateEdit.EditValue = null;
                ID_TT_HDLookUpEdit.EditValue = null;
                ID_TT_HTLookUpEdit.EditValue = null;
                ID_LHDLDLookUpEdit.EditValue = null;
                HINH_THUC_TUYENTextEdit.EditValue = "";
                LD_TINHCheckEdit.EditValue = false;
                LAO_DONG_CNCheckEdit.EditValue = false;
                TRUC_TIEP_SXCheckEdit.EditValue = false;
                VAO_LAM_LAICheckEdit.EditValue = false;


                SO_CMNDTextEdit.EditValue = "";
                NGAY_CAPDateEdit.EditValue = null;
                NOI_CAPLookupEdit.EditValue = null;
                ID_TT_HNLookUpEdit.EditValue = null;
                MS_THUETextEdit.EditValue = "";
                EMAILTextEdit.EditValue = "";
                MA_THE_ATMTextEdit.EditValue = "";
                SO_TAI_KHOANTextEdit.EditValue = "";
                CHUYEN_MONTextEdit.EditValue = "";
                ID_LOAI_TDLookUpEdit.EditValue = null;
                ID_TDVHLookUpEdit.EditValue = null;
                NGOAI_NGUTextEdit.EditValue = null;
                DT_DI_DONGTextEdit.EditValue = "";
                DT_NHATextEdit.EditValue = "";
                DT_NGUOI_THANTextEdit.EditValue = "";
                NGAY_NGHI_VIECDateEdit.EditValue = null;
                ID_LD_TVLookUpEdit.EditValue = null;

                NOI_SINHTextEdit.EditValue = "";
                NGUYEN_QUANTextEdit.EditValue = "";
                ID_DTLookUpEdit.EditValue = null;
                TON_GIAOTextEdit.EditValue = "";
                DIA_CHI_THUONG_TRUTextEdit.EditValue = "";
                ID_TPLookUpEdit.EditValue = null;
                ID_QUANLookEdit.EditValue = null;
                ID_PXLookUpEdit.EditValue = null;
                THON_XOMTextEdit.EditValue = "";
                DIA_CHI_TAM_TRUTextEdit.EditValue = "";
                ID_TP_TAM_TRULookUpEdit.EditValue = null;
                ID_QUAN_TAM_TRULookUpEdit.EditValue = null;
                ID_PX_TAM_TRULookUpEdit.EditValue = null;
                THON_XOM_TAM_TRUTextEdit.EditValue = "";
                cboID_KV.EditValue = null;

                SO_BHXHTextEdit.EditValue = "";
                NGAY_DBHXHDateEdit.EditValue = null;
                NGAY_CHAM_DUT_NOP_BHXHDateEdit.EditValue = null;
                THAM_GIA_BHXHCheckEdit.EditValue = false;
                SO_THE_BHYTTextEdit.EditValue = "";
                TINH_THANHLookUpEdit.EditValue = null;
                BENH_VIENLookUpEdit.EditValue = null;
                NGAY_HET_HANDateEdit.EditValue = null;
                LD_NNCheckEdit.EditValue = false;
                SO_GIAY_PHEPTextEdit.EditValue = "";
                NGAY_CAP_GPDateEdit.EditValue = null;
                LOAI_QUOC_TICHLookUpEdit.EditValue = null;
                CAP_GIAY_PHEPLookUpEdit.EditValue = null;
                NGAY_HH_GPDateEdit.EditValue = null;
                LD_GIAM_LDNNLookUpEdit.EditValue = "";
            }
            else
            {
                //lấy danh sách chi tiết công nhân 

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCTCongNhan", Commons.Modules.iCongNhan, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (dt.Rows.Count == 0) return;
                try
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dt.Rows[0]["Hinh_CN"]);
                    MemoryStream mem = new MemoryStream(data);
                    HINH_CNPictureEdit.EditValue = Image.FromStream(mem);
                }
                catch
                {
                }
                try
                {
                    MS_CNTextEdit.EditValue = dt.Rows[0]["MS_CN"];
                    MS_THE_CCTextEdit.EditValue = dt.Rows[0]["MS_THE_CC"];
                    ID_QGLookUpEdit.EditValue = dt.Rows[0]["ID_QG"];
                    HOTextEdit.EditValue = dt.Rows[0]["HO"];
                    TENTextEdit.EditValue = dt.Rows[0]["TEN"];
                    TEN_KHONG_DAUTextEdit.EditValue = dt.Rows[0]["TEN_KHONG_DAU"];
                    NGAY_SINHDateEdit.EditValue = dt.Rows[0]["NGAY_SINH"];
                    txtNamSinh.EditValue = dt.Rows[0]["NAM_SINH"].ToString();
                    PHAILookupEdit.EditValue = Convert.ToInt32(dt.Rows[0]["PHAI"]);
                    ID_DVLookUpEdit.EditValue = dt.Rows[0]["ID_DV"];
                    ID_XNLookUpEdit.EditValue = dt.Rows[0]["ID_XN"];
                    ID_TOLookupEdit.EditValue = dt.Rows[0]["ID_TO"];
                    ID_CVLookUpEdit.EditValue = dt.Rows[0]["ID_CV"];
                    ID_LCVLookUpEdit.EditValue = dt.Rows[0]["ID_LCV"];
                    PHEP_CTTextEdit.EditValue = dt.Rows[0]["PHEP_CT"];
                    NGAY_HOC_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_HOC_VIEC"];
                    NGAY_THU_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_THU_VIEC"];
                    NGAY_VAO_LAMDateEdit.EditValue = dt.Rows[0]["NGAY_VAO_LAM"];
                    ID_TT_HDLookUpEdit.EditValue = dt.Rows[0]["ID_TT_HD"];
                    ID_TT_HTLookUpEdit.EditValue = dt.Rows[0]["ID_TT_HT"];
                    ID_LHDLDLookUpEdit.EditValue = dt.Rows[0]["ID_LHDLD"];
                    HINH_THUC_TUYENTextEdit.EditValue = dt.Rows[0]["HINH_THUC_TUYEN"];
                    LD_TINHCheckEdit.EditValue = dt.Rows[0]["LD_TINH"];
                    LAO_DONG_CNCheckEdit.EditValue = dt.Rows[0]["LAO_DONG_CONG_NHAT"];
                    TRUC_TIEP_SXCheckEdit.EditValue = dt.Rows[0]["TRUC_TIEP_SX"];
                    VAO_LAM_LAICheckEdit.EditValue = dt.Rows[0]["VAO_LAM_LAI"];
                    NGAY_NGHI_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_NGHI_VIEC"];
                    ID_LD_TVLookUpEdit.EditValue = dt.Rows[0]["ID_LD_TV"];

                    SO_CMNDTextEdit.EditValue = dt.Rows[0]["SO_CMND"];
                    NGAY_CAPDateEdit.EditValue = dt.Rows[0]["NGAY_CAP"];
                    NOI_CAPLookupEdit.EditValue = dt.Rows[0]["NOI_CAP"];
                    ID_TT_HNLookUpEdit.EditValue = dt.Rows[0]["ID_TT_HN"];
                    MS_THUETextEdit.EditValue = dt.Rows[0]["MS_THUE"];
                    MA_THE_ATMTextEdit.EditValue = dt.Rows[0]["MA_THE_ATM"];
                    EMAILTextEdit.EditValue = dt.Rows[0]["EMAIL"];
                    SO_TAI_KHOANTextEdit.EditValue = dt.Rows[0]["SO_TAI_KHOAN"];
                    CHUYEN_MONTextEdit.EditValue = dt.Rows[0]["CHUYEN_MON"];
                    NGOAI_NGUTextEdit.EditValue = dt.Rows[0]["NGOAI_NGU"];
                    ID_LOAI_TDLookUpEdit.EditValue = dt.Rows[0]["ID_LOAI_TD"];
                    ID_TDVHLookUpEdit.EditValue = dt.Rows[0]["ID_TDVH"];
                    DT_DI_DONGTextEdit.EditValue = dt.Rows[0]["DT_DI_DONG"];
                    DT_NHATextEdit.EditValue = dt.Rows[0]["DT_NHA"];
                    DT_NGUOI_THANTextEdit.EditValue = dt.Rows[0]["DT_NGUOI_THAN"];

                    NOI_SINHTextEdit.EditValue = dt.Rows[0]["NOI_SINH"];
                    NGUYEN_QUANTextEdit.EditValue = dt.Rows[0]["NGUYEN_QUAN"];
                    ID_DTLookUpEdit.EditValue = dt.Rows[0]["ID_DT"];
                    TON_GIAOTextEdit.EditValue = dt.Rows[0]["TON_GIAO"];
                    DIA_CHI_THUONG_TRUTextEdit.EditValue = dt.Rows[0]["DIA_CHI_THUONG_TRU"];
                    ID_TPLookUpEdit.EditValue = dt.Rows[0]["ID_TP"];
                    ID_QUANLookEdit.EditValue = dt.Rows[0]["ID_QUAN"];
                    ID_PXLookUpEdit.EditValue = dt.Rows[0]["ID_PX"];
                    THON_XOMTextEdit.EditValue = dt.Rows[0]["THON_XOM"];
                    DIA_CHI_TAM_TRUTextEdit.EditValue = dt.Rows[0]["DIA_CHI_TAM_TRU"];
                    ID_TP_TAM_TRULookUpEdit.EditValue = dt.Rows[0]["ID_TP_TAM_TRU"];
                    ID_QUAN_TAM_TRULookUpEdit.EditValue = dt.Rows[0]["ID_QUAN_TAM_TRU"];
                    ID_PX_TAM_TRULookUpEdit.EditValue = dt.Rows[0]["ID_PX_TAM_TRU"];
                    THON_XOM_TAM_TRUTextEdit.EditValue = dt.Rows[0]["THON_XOM_TAM_TRU"];
                    cboID_KV.EditValue = dt.Rows[0]["ID_KV"];

                    SO_BHXHTextEdit.EditValue = dt.Rows[0]["SO_BHXH"];
                    NGAY_DBHXHDateEdit.EditValue = dt.Rows[0]["NGAY_DBHXH"];
                    NGAY_CHAM_DUT_NOP_BHXHDateEdit.EditValue = dt.Rows[0]["NGAY_CHAM_DUT_NOP_BHXH"];
                    THAM_GIA_BHXHCheckEdit.EditValue = dt.Rows[0]["THAM_GIA_BHXH"];
                    SO_THE_BHYTTextEdit.EditValue = dt.Rows[0]["SO_THE"];
                    TINH_THANHLookUpEdit.EditValue = dt.Rows[0]["TINH_THANH"];
                    BENH_VIENLookUpEdit.EditValue = dt.Rows[0]["ID_BV"];
                    NGAY_HET_HANDateEdit.EditValue = dt.Rows[0]["NGAY_HET_HAN"];
                    LD_NNCheckEdit.EditValue = dt.Rows[0]["LD_NN"];
                    SO_GIAY_PHEPTextEdit.EditValue = dt.Rows[0]["SO_GIAY_PHEP"];
                    NGAY_CAP_GPDateEdit.EditValue = dt.Rows[0]["NGAY_CAP_GP"];
                    LOAI_QUOC_TICHLookUpEdit.EditValue = dt.Rows[0]["LOAI_QUOC_TICH"];
                    CAP_GIAY_PHEPLookUpEdit.EditValue = dt.Rows[0]["CAP_GIAY_PHEP"];
                    NGAY_HH_GPDateEdit.EditValue = dt.Rows[0]["NGAY_HH_GP"];
                    LD_GIAM_LDNNLookUpEdit.EditValue = dt.Rows[0]["LD_GIAM_LDNN"];
                }
                catch (Exception)
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
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            windowsUIButton.Buttons[9].Properties.Visible = visible;
            windowsUIButton.Buttons[10].Properties.Visible = !visible;
            windowsUIButton.Buttons[11].Properties.Visible = !visible;
            windowsUIButton.Buttons[12].Properties.Visible = visible;

            MS_CNTextEdit.Properties.ReadOnly = visible;
            MS_THE_CCTextEdit.Properties.ReadOnly = visible;
            ID_QGLookUpEdit.Properties.ReadOnly = visible;
            HOTextEdit.Properties.ReadOnly = visible;
            TENTextEdit.Properties.ReadOnly = visible;
            TEN_KHONG_DAUTextEdit.Properties.ReadOnly = visible;
            VAO_LAM_LAICheckEdit.Properties.ReadOnly = visible;
            NGAY_SINHDateEdit.Enabled = !visible;
            txtNamSinh.Enabled = !visible;
            PHAILookupEdit.Properties.ReadOnly = visible;
            ID_XNLookUpEdit.Properties.ReadOnly = visible;
            ID_DVLookUpEdit.Properties.ReadOnly = visible;
            ID_TOLookupEdit.Properties.ReadOnly = visible;
            ID_CVLookUpEdit.Properties.ReadOnly = visible;
            ID_LCVLookUpEdit.Properties.ReadOnly = visible;
            PHEP_CTTextEdit.Properties.ReadOnly = visible;
            NGAY_HOC_VIECDateEdit.Enabled = !visible;
            NGAY_THU_VIECDateEdit.Enabled = !visible;
            NGAY_VAO_LAMDateEdit.Enabled = !visible;
            ID_TT_HDLookUpEdit.Properties.ReadOnly = visible;
            ID_TT_HTLookUpEdit.Properties.ReadOnly = visible;
            HINH_THUC_TUYENTextEdit.Properties.ReadOnly = visible;
            LD_TINHCheckEdit.Properties.ReadOnly = visible;
            LAO_DONG_CNCheckEdit.Properties.ReadOnly = visible;
            TRUC_TIEP_SXCheckEdit.Properties.ReadOnly = visible;

            SO_CMNDTextEdit.Properties.ReadOnly = visible;
            NGAY_CAPDateEdit.Enabled = !visible;
            NOI_CAPLookupEdit.Properties.ReadOnly = visible;
            ID_TT_HNLookUpEdit.Properties.ReadOnly = visible;
            MS_THUETextEdit.Properties.ReadOnly = visible;
            EMAILTextEdit.Properties.ReadOnly = visible;
            MA_THE_ATMTextEdit.Properties.ReadOnly = visible;
            SO_TAI_KHOANTextEdit.Properties.ReadOnly = visible;
            CHUYEN_MONTextEdit.Properties.ReadOnly = visible;
            ID_LOAI_TDLookUpEdit.Properties.ReadOnly = visible;
            ID_TDVHLookUpEdit.Properties.ReadOnly = visible;
            NGOAI_NGUTextEdit.Properties.ReadOnly = visible;
            DT_DI_DONGTextEdit.Properties.ReadOnly = visible;
            DT_NHATextEdit.Properties.ReadOnly = visible;
            DT_NGUOI_THANTextEdit.Properties.ReadOnly = visible;

            NOI_SINHTextEdit.Properties.ReadOnly = visible;
            NGUYEN_QUANTextEdit.Properties.ReadOnly = visible;
            ID_DTLookUpEdit.Properties.ReadOnly = visible;
            TON_GIAOTextEdit.Properties.ReadOnly = visible;
            DIA_CHI_THUONG_TRUTextEdit.Properties.ReadOnly = visible;
            ID_TPLookUpEdit.Properties.ReadOnly = visible;
            ID_QUANLookEdit.Properties.ReadOnly = visible;
            ID_PXLookUpEdit.Properties.ReadOnly = visible;
            THON_XOMTextEdit.Properties.ReadOnly = visible;
            DIA_CHI_TAM_TRUTextEdit.Properties.ReadOnly = visible;
            ID_TP_TAM_TRULookUpEdit.Properties.ReadOnly = visible;
            ID_QUAN_TAM_TRULookUpEdit.Properties.ReadOnly = visible;
            ID_PX_TAM_TRULookUpEdit.Properties.ReadOnly = visible;
            THON_XOM_TAM_TRUTextEdit.Properties.ReadOnly = visible;
            cboID_KV.Properties.ReadOnly = visible;

            SO_BHXHTextEdit.Properties.ReadOnly = visible;
            NGAY_DBHXHDateEdit.Enabled = !visible;
            NGAY_CHAM_DUT_NOP_BHXHDateEdit.Enabled = !visible;
            THAM_GIA_BHXHCheckEdit.Properties.ReadOnly = visible;
            SO_THE_BHYTTextEdit.Properties.ReadOnly = visible;
            NGAY_HET_HANDateEdit.Properties.ReadOnly = visible;
            TINH_THANHLookUpEdit.Properties.ReadOnly = visible;
            BENH_VIENLookUpEdit.Properties.ReadOnly = visible;

            LD_NNCheckEdit.Properties.ReadOnly = visible;
            SO_GIAY_PHEPTextEdit.Properties.ReadOnly = visible;
            NGAY_CAP_GPDateEdit.Properties.ReadOnly = visible;
            LOAI_QUOC_TICHLookUpEdit.Properties.ReadOnly = visible;
            CAP_GIAY_PHEPLookUpEdit.Properties.ReadOnly = visible;
            NGAY_HH_GPDateEdit.Properties.ReadOnly = visible;
            LD_GIAM_LDNNLookUpEdit.Properties.ReadOnly = visible;

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
                Commons.Modules.iCongNhan = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateCongNhan",
                Commons.Modules.iCongNhan,
                imgToByteConverter(HINH_CNPictureEdit.Image),
                MS_CNTextEdit.EditValue,
                MS_THE_CCTextEdit.EditValue,
                ID_QGLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_QGLookUpEdit.EditValue,
                HOTextEdit.EditValue,
                TENTextEdit.EditValue,
                TEN_KHONG_DAUTextEdit.EditValue,
                NGAY_SINHDateEdit.EditValue,
                Convert.ToInt32(txtNamSinh.EditValue),
                PHAILookupEdit.EditValue,
                ID_TOLookupEdit.Text.ToString() == "" ? DBNull.Value : ID_TOLookupEdit.EditValue,
                ID_CVLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_CVLookUpEdit.EditValue,
                ID_LCVLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_LCVLookUpEdit.EditValue,
                PHEP_CTTextEdit.EditValue,
                NGAY_HOC_VIECDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_HOC_VIECDateEdit.EditValue,
                NGAY_THU_VIECDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_THU_VIECDateEdit.EditValue,
                NGAY_VAO_LAMDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_VAO_LAMDateEdit.EditValue,
                ID_TT_HDLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TT_HDLookUpEdit.EditValue,
                ID_TT_HTLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TT_HTLookUpEdit.EditValue,
                HINH_THUC_TUYENTextEdit.EditValue,
                LAO_DONG_CNCheckEdit.EditValue,
                TRUC_TIEP_SXCheckEdit.EditValue,
                LD_TINHCheckEdit.EditValue,
                VAO_LAM_LAICheckEdit.EditValue,
                SO_CMNDTextEdit.EditValue,
                NGAY_CAPDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_CAPDateEdit.EditValue,
                NOI_CAPLookupEdit.Text.ToString() == "" ? DBNull.Value : NOI_CAPLookupEdit.EditValue,
                ID_TT_HNLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TT_HNLookUpEdit.EditValue,
                MS_THUETextEdit.EditValue,
                MA_THE_ATMTextEdit.EditValue,
                SO_TAI_KHOANTextEdit.EditValue,
                CHUYEN_MONTextEdit.EditValue,
                ID_LOAI_TDLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_LOAI_TDLookUpEdit.EditValue,
                ID_TDVHLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TDVHLookUpEdit.EditValue,
                NGOAI_NGUTextEdit.EditValue,
                DT_NHATextEdit.EditValue,
                DT_NGUOI_THANTextEdit.EditValue,
                DT_DI_DONGTextEdit.EditValue,
                EMAILTextEdit.EditValue,
                NOI_SINHTextEdit.EditValue,
                NGUYEN_QUANTextEdit.EditValue,
                ID_DTLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_DTLookUpEdit.EditValue,
                TON_GIAOTextEdit.EditValue,
                DIA_CHI_THUONG_TRUTextEdit.EditValue,
                ID_TPLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TPLookUpEdit.EditValue,
                ID_QUANLookEdit.Text.ToString() == "" ? DBNull.Value : ID_QUANLookEdit.EditValue,
                ID_PXLookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_PXLookUpEdit.EditValue,
                THON_XOMTextEdit.EditValue,
                DIA_CHI_TAM_TRUTextEdit.EditValue,
                ID_TP_TAM_TRULookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_TP_TAM_TRULookUpEdit.EditValue,
                ID_QUAN_TAM_TRULookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_QUAN_TAM_TRULookUpEdit.EditValue,
                ID_PX_TAM_TRULookUpEdit.Text.ToString() == "" ? DBNull.Value : ID_PX_TAM_TRULookUpEdit.EditValue,
                THON_XOM_TAM_TRUTextEdit.EditValue,
                SO_BHXHTextEdit.EditValue,
                NGAY_DBHXHDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_DBHXHDateEdit.EditValue,
                NGAY_CHAM_DUT_NOP_BHXHDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_CHAM_DUT_NOP_BHXHDateEdit.EditValue,
                THAM_GIA_BHXHCheckEdit.EditValue,
                SO_THE_BHYTTextEdit.EditValue,
                NGAY_HET_HANDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_HET_HANDateEdit.EditValue,
                TINH_THANHLookUpEdit.Text.ToString() == "" ? DBNull.Value : TINH_THANHLookUpEdit.EditValue,
                BENH_VIENLookUpEdit.Text.ToString() == "" ? DBNull.Value : BENH_VIENLookUpEdit.EditValue,
                LD_NNCheckEdit.EditValue,
                SO_GIAY_PHEPTextEdit.EditValue,
                NGAY_CAP_GPDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_CAP_GPDateEdit.EditValue,
                LOAI_QUOC_TICHLookUpEdit.Text.ToString() == "" ? DBNull.Value : LOAI_QUOC_TICHLookUpEdit.EditValue,
                CAP_GIAY_PHEPLookUpEdit.Text.ToString() == "" ? DBNull.Value : CAP_GIAY_PHEPLookUpEdit.EditValue,
                NGAY_HH_GPDateEdit.Text.ToString() == "" ? DBNull.Value : NGAY_HH_GPDateEdit.EditValue,
                LD_GIAM_LDNNLookUpEdit.Text.ToString() == "" ? DBNull.Value : LD_GIAM_LDNNLookUpEdit.EditValue,
                cboID_KV.Text.ToString() == "" ? DBNull.Value : cboID_KV.EditValue,
                cothem));
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private Boolean kiemtrung(int cot)
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungLyLich", conn);
            cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idcn;


            if (cot == 1)
            {
                cmd.Parameters.Add("@MS_CN", SqlDbType.NVarChar).Value = MS_CNTextEdit.Text;
            }
            if (cot == 2)
            {
                cmd.Parameters.Add("@MS_CC", SqlDbType.NVarChar).Value = MS_THE_CCTextEdit.Text;
            }
            //kiem tra ngay vao lam
            if (cot == 3)
            {
                int nvaolam = Convert.ToInt32((Convert.ToDateTime(NGAY_VAO_LAMDateEdit.EditValue)).Year.ToString());
                int nsinh = Convert.ToInt32((Convert.ToDateTime(NGAY_SINHDateEdit.EditValue)).Year.ToString());
                if (nsinh >= nvaolam)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "messNgayvaolamkhonghople"));
                    NGAY_VAO_LAMDateEdit.Focus();
                    return false;
                }

            }
            cmd.CommandType = CommandType.StoredProcedure;
            if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
            {
                if (cot == 1)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "messMS_CNbitrung"));
                    MS_CNTextEdit.Focus();
                }
                if (cot == 2)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "messMS_CCbitrung"));
                    MS_THE_CCTextEdit.Focus();
                }
                return false;
            }

            return true;
        }
        #endregion

        private void ID_QGLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_QGLookUpEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", true);
        }

        private void ID_TPLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_TPLookUpEdit.EditValue == null || ID_TPLookUpEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_QUANLookEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(ID_TPLookUpEdit.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void ID_QUANLookEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_QUANLookEdit.EditValue == null || ID_QUANLookEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_PXLookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(ID_QUANLookEdit.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }

        private void ID_TP_TAM_TRULookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_TP_TAM_TRULookUpEdit.EditValue == null || ID_TP_TAM_TRULookUpEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_QUAN_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(ID_TP_TAM_TRULookUpEdit.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void ID_QUAN_TAM_TRULookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_QUAN_TAM_TRULookUpEdit.EditValue == null || ID_QUAN_TAM_TRULookUpEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_PX_TAM_TRULookUpEdit, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(ID_QUAN_TAM_TRULookUpEdit.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }

        private void ID_LOAI_TDLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (ID_LOAI_TDLookUpEdit.EditValue == null || ID_LOAI_TDLookUpEdit.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TDVHLookUpEdit, Commons.Modules.ObjSystems.DataTDVH(Convert.ToInt32(ID_LOAI_TDLookUpEdit.EditValue), false), "ID_TDVH", "TEN_TDVH", "TEN_TDVH", true);
        }

        private void ID_DVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true);
        }

        private void ID_XNLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(ID_DVLookUpEdit.EditValue), Convert.ToInt32(ID_XNLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true);
        }

        private void ID_LCVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                PHEP_CTTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[funPhepCongThem](" + ID_LCVLookUpEdit.EditValue + ")").ToString();
            }
            catch (Exception)
            {
                PHEP_CTTextEdit.Text = "0";
            }
        }
    }
}
