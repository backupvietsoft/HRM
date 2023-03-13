using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using DevExpress.XtraBars.Docking2010;
using System.Windows.Forms;
using Vs.Report;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Map.Native;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace Vs.HRM
{
    public partial class ucLyLich : DevExpress.XtraEditors.XtraUserControl
    {
        bool cothem = false;
        Int64 idcn = -1;
        public DataTable dt;
        bool HopLeMS = true;
        bool HopLeMT = true;
        bool HopLeNgaySinh = true;
        bool isCancel = false;
        Int64 idlcv = 0;
        //string strDuongDan = "";
        private ucCTQLNS uc;
        public ucLyLich(Int64 id)
        {
            DevExpress.Utils.Paint.TextRendererHelper.UseScriptAnalyse = false;
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, Tab, windowsUIButton);
            //string f = "c:\\Layout.xml";
            // Save the layout to an XML file.
            //dataLayoutControl1.SaveLayoutToXml(f);
            // ...
            // Restore a previously saved layout.
            //dataLayoutControl1.RestoreLayoutFromXml(f);
            //Thread threadLoadNN = new Thread(delegate ()
            //{
            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new MethodInvoker(delegate
            //        {
            //            dataLayoutControl1.RestoreLayoutFromXml(f);
            //        }));
            //    }
            //}, 100); threadLoadNN.Start();
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

            Commons.OSystems.SetDateEditFormat(NGAY_CAPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_CAP_GPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_DBHXHDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HH_GPDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_CHAM_DUT_NOP_BHXHDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HET_HANDateEdit);

            //đơn vị 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_DVLookUpEdit, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);

            //xí nghiệp 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

            ////tổ
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);
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

            //ID_TP_TAM_TRULookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TP_KS, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_QUAN_TAM_TRULookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QUAN_KS, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            //ID_PX_TAM_TRULookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_PX_KS, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");


            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);

            //ID_CVLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV", true);

            ////ID_LHDLDLookUpEdit
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LHDLDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiHDLD(false), "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD", "", true);

            //ID_TT_HDLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HDLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", "", true);

            //ID_TT_HTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HTLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHT(-1, false), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT", "", true);

            ////ID_LD_TVLookUpEdit
            //Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_LD_TVLookUpEdit, Commons.Modules.ObjSystems.DataLyDoThoiViec(), "ID_LD_TV", "TEN_LD_TV", "TEN_LD_TV", "");

            //ID_DTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_DTLookUpEdit, Commons.Modules.ObjSystems.DataDanToc(false), "ID_DT", "TEN_DT", "TEN_DT", "");

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

            //LD_GIAM_LDNNLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(LD_GIAM_LDNNLookUpEdit, Commons.Modules.ObjSystems.DataLyDoGiamLDNN(false), "ID_LDG_LDNN", "TEN_LDG_LDNN", "TEN_LDG_LDNN", "");

            // PHAILookUpEdit
            DataTable dt_Phai = new DataTable();
            dt_Phai.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhai", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(PHAILookupEdit, dt_Phai, "ID_PHAI", "PHAI", "PHAI", "");

            ItemForKHU_VUC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ItemForLD_TINH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ItemForTRUC_TIEP_SX.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ItemForLAO_DONG_CONG_NHAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (Commons.Modules.ObjSystems.KyHieuDV_CN(Commons.Modules.iCongNhan) == "SB")
            {
                // KHU_VUCLookUpEdit
                DataTable dt_kv = new DataTable();
                dt_kv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKHU_VUC", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_KV, dt_kv, "ID_KV", "TEN_KV", "TEN_KV", "");
                ItemForKHU_VUC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            if (idcn == -1)
            {
                enableButon(false);
            }
            else
            {
                enableButon(true);
            }

            Tab.SelectedTabPage = groupThongTinBoXung;
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            if (Commons.Modules.iCongNhan == -1)
                cothem = true;
            BinDingData(cothem);
            LoadgrdBangCap();
            LoadgrdTaiLieu();
            Commons.Modules.sLoad = "";
        }

        private void LoadCmbLoc()
        {
            try
            {
                //đơn vị 
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_DVLookUpEdit, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);

                //xí nghiệp 
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

                ////tổ
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);
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

                //ID_TP_TAM_TRULookUpEdit 
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TP_KS, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                //ID_QUAN_TAM_TRULookUpEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QUAN_KS, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

                //ID_PX_TAM_TRULookUpEdit
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_PX_KS, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");


                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);

                //ID_CVLookUpEdit.EditValue = "";
                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV", true);

                //ID_TINH_THANH_KHAM_BENH 
                Commons.Modules.ObjSystems.MLoadLookUpEditN(TINH_THANHLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

                //ID_BENH_VIEN_KHAM_BENH 
                Commons.Modules.ObjSystems.MLoadLookUpEditN(BENH_VIENLookUpEdit, Commons.Modules.ObjSystems.DataBenhVien(false), "ID_BV", "TEN_BV", "TEN_BV", "");

            }
            catch { }
        }

        private bool IsNumber(string pValue)
        {
            bool isNumeric = true;
            foreach (Char c in pValue)
            {
                if (!Char.IsNumber(c))
                {
                    isNumeric = false;
                    return isNumeric;
                }
            }
            return isNumeric;
        }

        /// <summary>
        /// Clear all of error
        /// </summary>
        private void ClearError()
        {
            MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            MS_CNTextEdit.ErrorText = null;
            NGAY_SINHDateEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            NGAY_SINHDateEdit.ErrorText = null;
            MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
            MS_THE_CCTextEdit.ErrorText = null;
        }

        private void Load_cboChucVu()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCTCongNhan", Commons.Modules.iCongNhan, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                ID_DVLookUpEdit.EditValue = dt.Rows[0]["ID_DV"];
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                ID_XNLookUpEdit.EditValue = dt.Rows[0]["ID_XN"];
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(ID_DVLookUpEdit.EditValue), Convert.ToInt32(ID_XNLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);
                ID_TOLookupEdit.EditValue = dt.Rows[0]["ID_TO"];
                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(ID_XNLookUpEdit.EditValue)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);
                ID_LCVLookUpEdit.EditValue = dt.Rows[0]["ID_LCV"];
                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCVLookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", true);
                ID_CVLookUpEdit.EditValue = dt.Rows[0]["ID_CV"];
            }
            catch (Exception ex)
            {
                throw ex;
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
                        idcn = -1;
                        BinDingData(true);
                        enableButon(false);
                        LoadgrdBangCap();
                        LoadgrdTaiLieu();
                        Commons.Modules.ObjSystems.AddnewRow(grvBangCapCN, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvTaiLieu, true);
                        THAM_GIA_BHXHCheckEdit_CheckedChanged(null, null);
                        LD_NNCheckEdit_CheckedChanged(null, null);
                        break;
                    }
                case "sua":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = false;
                        idcn = Commons.Modules.iCongNhan;
                        //LoadCmbLoc(2);
                        enableButon(false);

                        int TongSoQTCT = 0;
                        int TongSoHDLD = 0;
                        TongSoQTCT = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(ID_QTCT) FROM dbo.QUA_TRINH_CONG_TAC WHERE ID_CN =  " + Commons.Modules.iCongNhan + " GROUP BY ID_CN"));
                        TongSoHDLD = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(ID_HDLD) FROM dbo.HOP_DONG_LAO_DONG WHERE ID_CN =  " + Commons.Modules.iCongNhan + " GROUP BY ID_CN"));
                        if (TongSoQTCT > 0)
                        {
                            this.LockTheoQTCT();
                        }
                        if (TongSoHDLD > 0)
                        {
                            this.LockTheoHDLD();
                        }
                        Commons.Modules.ObjSystems.AddnewRow(grvBangCapCN, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvTaiLieu, true);
                        THAM_GIA_BHXHCheckEdit_CheckedChanged(null, null);
                        LD_NNCheckEdit_CheckedChanged(null, null);
                        break;
                    }
                case "xoa":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {

                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        //CheckMS();
                        //CheckMT();
                        if (!HopLeMS || !HopLeMT || !HopLeNgaySinh) return;
                        if (MS_CNTextEdit.Text != "") if (!kiemtrung(1)) return;
                        if (MS_THE_CCTextEdit.Text != "") if (!kiemtrung(2)) return;
                        if (!kiemtrung(3)) return;
                        //kiểm tra chức vụ
                        if(cothem == false)
                        {
                            //chỉ khi sữa mới kiểm tra xem chức vụ củ và hiện tại giống nhau không
                            if(Convert.ToInt64(ID_CVLookUpEdit.EditValue) != Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CV FROM dbo.LOAI_CONG_VIEC WHERE ID_LCV = "+ idlcv +"")))
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonCapNhatLaiCVchoNV"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    ID_LCVLookUpEdit.EditValue = idlcv;
                                    return;
                                }
                            }    
                        }    
                        //kiểm tra khi chọn đã nghĩ việc
                        try
                        {
                            string skHNV = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(KY_HIEU,'') FROM dbo.TINH_TRANG_HT WHERE ID_TT_HT =  " + ID_TT_HTLookUpEdit.EditValue + "").ToString();
                            if (skHNV.ToLower().Trim() == "nv")
                            {
                                //kiểm tra có hợp đồng lao động hay quá trình công tác chưa
                                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, "SELECT(SELECT COUNT(*) FROM dbo.HOP_DONG_LAO_DONG WHERE ID_CN = " + idcn + ") + (SELECT COUNT(*) FROM dbo.QUA_TRINH_CONG_TAC WHERE ID_CN = " + idcn + ")")) > 0)
                                {
                                    //kiểm tra có quyết định thôi việc chưa.
                                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, "SELECT COUNT(*) FROM dbo.QUYET_DINH_THOI_VIEC WHERE ID_CN = " + idcn + "")) == 0)
                                    //nếu có phải thông báo bạn phải lập quyết định thôi việc
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanPhaiLapQuyetDinhThoiViec"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Question);
                                        return;
                                    }
                                }
                                //nếu có kiểm tra có trong nghĩ việc chưa, nếu chưa có thì báo phải làm bên nghĩ việc
                                ID_TT_HDLookUpEdit.EditValue = 5;
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        if (Commons.Modules.iCongNhan == -1)
                        {

                            if (Commons.Modules.ObjSystems.kiemTrungMS("CONG_NHAN", "MS_CN", MS_CNTextEdit.Text))
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSCNDaTrungBanCoMuonTaoMaMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                MS_CNTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + (ID_DVLookUpEdit.Text == "" ? -1 : Convert.ToInt32(ID_DVLookUpEdit.EditValue)) + ",1)").ToString();
                                return;
                            }
                            if (Commons.Modules.ObjSystems.kiemTrungMS("CONG_NHAN", "MS_THE_CC", MS_THE_CCTextEdit.Text))
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSTheCCDaTrungBanCoMuonTaoMaMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                MS_THE_CCTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + (ID_DVLookUpEdit.Text == "" ? -1 : Convert.ToInt32(ID_DVLookUpEdit.EditValue)) + ",2)").ToString();
                                return;
                            }
                        }
                        if (SaveData())
                        {
                            //Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtTaiLieu.Text);
                            this.ClearError();
                            BinDingData(false);
                            enableButon(true);
                        }
                        Commons.Modules.ObjSystems.DeleteAddRow(grvBangCapCN);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTaiLieu);
                        break;
                    }
                case "khongluu":
                    {
                        isCancel = true;
                        Commons.Modules.sLoad = "0Load";
                        LoadCmbLoc();
                        BinDingData(false);
                        enableButon(true);
                        LoadgrdTaiLieu();
                        try
                        {
                            string[] fileList = Directory.GetFiles(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text);
                            foreach (string item in fileList)
                            {
                                if (Commons.Modules.ObjSystems.ConvertDatatable(grvTaiLieu).AsEnumerable().Count(x => x["DUONG_DAN"].Equals(item)) == 0)
                                {
                                    Commons.Modules.ObjSystems.Xoahinh(item);
                                }
                            }
                        }
                        catch
                        {
                        }
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvBangCapCN);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTaiLieu);
                        Commons.Modules.sLoad = "";
                        this.ClearError();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "giadinh":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        frmGiaDinh gd = new frmGiaDinh(HOTextEdit.EditValue + " " + TENTextEdit.EditValue, Commons.Modules.iCongNhan);
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
                        if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM" || Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "NB")
                        {
                            frmInLyLichCN InLyLichCN = new frmInLyLichCN(Commons.Modules.iCongNhan);
                            InLyLichCN.ShowDialog();
                        }
                        else
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
                            catch (Exception ex)
                            {
                            }
                            frm.ShowDialog();
                        }
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

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BAO_HIEM_Y_TE WHERE ID_CN =  " + Commons.Modules.iCongNhan + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BANG_CAP WHERE ID_CN =  " + Commons.Modules.iCongNhan + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.CONG_NHAN WHERE ID_CN = " + Commons.Modules.iCongNhan + "");
                try
                {
                    Directory.Delete(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text, true);
                }
                catch
                {
                }
                BinDingData(true);
                LoadgrdBangCap();
                LoadgrdTaiLieu();

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

        public void BinDingData(bool bthem)
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
                NAM_SINHDateEdit.EditValue = null;
                PHAILookupEdit.EditValue = 0;
                ID_DVLookUpEdit.EditValue = null;
                ID_XNLookUpEdit.EditValue = null;
                ID_TOLookupEdit.EditValue = null;
                ID_CVLookUpEdit.EditValue = null;
                ID_LCVLookUpEdit.EditValue = null;
                PHEP_CTTextEdit.EditValue = 0;
                NGAY_HOC_VIECDateEdit.EditValue = null;
                NGAY_THU_VIECDateEdit.EditValue = null;
                NGAY_VAO_LAMDateEdit.EditValue = null;
                ID_TT_HDLookUpEdit.EditValue = null;
                ID_TT_HTLookUpEdit.EditValue = null;
                ID_LHDLDLookUpEdit.EditValue = "";
                //txtTaiLieu.EditValue = "";
                HINH_THUC_TUYENTextEdit.EditValue = "";
                LD_TINHCheckEdit.EditValue = false;
                LAO_DONG_CNCheckEdit.EditValue = false;
                TRUC_TIEP_SXCheckEdit.EditValue = false;
                VAO_LAM_LAICheckEdit.EditValue = false;


                SO_CMNDTextEdit.EditValue = "";
                NGAY_CAPDateEdit.EditValue = null;
                txtNOI_CAP.EditValue = "";
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
                NGAY_NGHI_VIECDateEdit.EditValue = "";
                ID_LD_TVLookUpEdit.EditValue = "";

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
                txtDIA_CHI_KS.Text = "";
                cboID_TP_KS.EditValue = null;
                cboID_QUAN_KS.EditValue = null;
                cboID_PX_KS.EditValue = null;
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
                if (dt.Rows.Count == 0)
                {
                    BinDingData(true);
                    return;
                }
                try
                {
                    MS_CNTextEdit.EditValue = dt.Rows[0]["MS_CN"];
                    try
                    {
                        if (Commons.Modules.KyHieuDV == "SB")
                        {
                            Byte[] data = new Byte[0];
                            data = (Byte[])(dt.Rows[0]["Hinh_CN"]);
                            MemoryStream mem = new MemoryStream(data);
                            HINH_CNPictureEdit.EditValue = Image.FromStream(mem);
                        }
                        else
                        {
                            string imagePath = dt.Rows[0]["HINH_CN_URL"].ToString();
                            if (System.IO.File.Exists(imagePath))
                            {
                                HINH_CNPictureEdit.LoadAsync(imagePath);
                            }
                            else
                            {
                                HINH_CNPictureEdit.EditValue = null;
                            }
                        }

                    }
                    catch
                    {
                        HINH_CNPictureEdit.EditValue = null;
                    }
                    MS_THE_CCTextEdit.EditValue = dt.Rows[0]["MS_THE_CC"];
                    ID_QGLookUpEdit.EditValue = dt.Rows[0]["ID_QG"];
                    HOTextEdit.EditValue = dt.Rows[0]["HO"];
                    TENTextEdit.EditValue = dt.Rows[0]["TEN"];
                    TEN_KHONG_DAUTextEdit.EditValue = dt.Rows[0]["TEN_KHONG_DAU"];
                    NGAY_SINHDateEdit.EditValue = dt.Rows[0]["NGAY_SINH"];
                    //NAM_SINHDateEdit.EditValue = dt.Rows[0]["NAM_SINH"].ToString();
                    NAM_SINHDateEdit.EditValue = dt.Rows[0]["NAM_SINH"] == null || dt.Rows[0]["NAM_SINH"].ToString().Trim() == "" ? Convert.ToDateTime(NGAY_SINHDateEdit.EditValue).Year.ToString().Trim() : dt.Rows[0]["NAM_SINH"].ToString();
                    PHAILookupEdit.EditValue = Convert.ToInt32(dt.Rows[0]["PHAI"]);
                    ID_DVLookUpEdit.EditValue = dt.Rows[0]["ID_DV"];
                    ID_XNLookUpEdit.EditValue = dt.Rows[0]["ID_XN"];
                    ID_TOLookupEdit.EditValue = dt.Rows[0]["ID_TO"];
                    ID_CVLookUpEdit.EditValue = dt.Rows[0]["ID_CV"];
                    ID_LCVLookUpEdit.EditValue = dt.Rows[0]["ID_LCV"];
                    try
                    {
                        idlcv = Convert.ToInt64(ID_LCVLookUpEdit.EditValue);
                    }
                    catch
                    {
                    }
                    PHEP_CTTextEdit.EditValue = dt.Rows[0]["PHEP_CT"];
                    NGAY_HOC_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_HOC_VIEC"];
                    NGAY_THU_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_THU_VIEC"];
                    NGAY_VAO_LAMDateEdit.EditValue = dt.Rows[0]["NGAY_VAO_LAM"];
                    ID_TT_HDLookUpEdit.EditValue = dt.Rows[0]["ID_TT_HD"];
                    ID_TT_HTLookUpEdit.EditValue = dt.Rows[0]["ID_TT_HT"];
                    ID_LHDLDLookUpEdit.EditValue = dt.Rows[0]["ID_LHDLD"];
                    //txtTaiLieu.EditValue = dt.Rows[0]["FILE_DK"];
                    HINH_THUC_TUYENTextEdit.EditValue = dt.Rows[0]["HINH_THUC_TUYEN"];
                    LD_TINHCheckEdit.EditValue = dt.Rows[0]["LD_TINH"];
                    LAO_DONG_CNCheckEdit.EditValue = dt.Rows[0]["LAO_DONG_CONG_NHAT"];
                    TRUC_TIEP_SXCheckEdit.EditValue = dt.Rows[0]["TRUC_TIEP_SX"];
                    VAO_LAM_LAICheckEdit.EditValue = dt.Rows[0]["VAO_LAM_LAI"];
                    NGAY_NGHI_VIECDateEdit.EditValue = dt.Rows[0]["NGAY_NGHI_VIEC"];
                    ID_LD_TVLookUpEdit.EditValue = dt.Rows[0]["ID_LD_TV"];

                    SO_CMNDTextEdit.EditValue = dt.Rows[0]["SO_CMND"];
                    NGAY_CAPDateEdit.EditValue = dt.Rows[0]["NGAY_CAP"];
                    txtNOI_CAP.EditValue = dt.Rows[0]["NOI_CAP"];
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
                    try { txtDIA_CHI_KS.Text = dt.Rows[0]["DC_KHAI_SINH"].ToString(); } catch { }
                    cboID_TP_KS.EditValue = dt.Rows[0]["ID_TP_KS"];
                    cboID_QUAN_KS.EditValue = dt.Rows[0]["ID_QUAN_KS"];
                    cboID_PX_KS.EditValue = dt.Rows[0]["ID_PX_KS"];
                    try { cboID_KV.EditValue = dt.Rows[0]["ID_KV"]; } catch { }

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
                    isCancel = false;
                }

                //load lưới bằng cấp
            }
            isCancel = false;
        }
        private void LoadgrdBangCap()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangCap", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grdBangCapCN.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdBangCapCN, grvBangCapCN, dt, false, false, true, true, true, this.Name);
                Commons.Modules.ObjSystems.AddComboAnID("ID_LOAI_TD", "TEN_LOAI_TD", grvBangCapCN, Commons.Modules.ObjSystems.DataLoaiTrinhDo(false));
                grvBangCapCN.Columns["TEN_BANG"].Visible = false;
                grvBangCapCN.Columns["XEP_LOAI"].Visible = false;
                grvBangCapCN.Columns["NGUOI_KY"].Visible = false;
                grvBangCapCN.Columns["NOI_CAP"].Visible = false;
                grvBangCapCN.Columns["GHI_CHU"].Visible = false;
                grvBangCapCN.Columns["NGAY_KY"].Visible = false;
                grvBangCapCN.Columns["ID_BC"].Visible = false;
            }
            else
            {
                grdBangCapCN.DataSource = dt;
            }
        }

        private void LoadgrdTaiLieu()
        {
            //DataTable dt = new DataTable();
            //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr,CommandType.Text, "SELECT ID_CN,DUONG_DAN,TEN_TAI_LIEU,NGUOI_UL,THOI_GIAN_UL,GHI_CHU FROM dbo.CONG_NHAN_TAI_LIEU WHERE ID_CN = "+idcn+""));
            //if (grdTaiLIeu.DataSource == null)
            //{
            //    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTaiLIeu, grvTaiLieu, dt, false, false, true, true, true, this.Name);
            //}
            //else
            //{
            //    grdBangCapCN.DataSource = dt;
            //}

            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN,DUONG_DAN,TEN_TAI_LIEU,NGUOI_UL,THOI_GIAN_UL,GHI_CHU FROM dbo.CONG_NHAN_TAI_LIEU WHERE ID_CN = " + idcn + ""));
                if (grdTaiLIeu.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTaiLIeu, grvTaiLieu, dt, true, true, true, true, true, this.Name);
                    grvTaiLieu.Columns["ID_CN"].Visible = false;
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvTaiLieu.Columns["DUONG_DAN"].ColumnEdit = btnEdit;
                    btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                    //btnEdit.DoubleClick += BtnEdit_DoubleClick;
                    for (int i = 2; i < grvTaiLieu.Columns.Count - 1; i++)
                    {
                        grvTaiLieu.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    grdTaiLIeu.DataSource = dt;
                }
            }
            catch
            {
            }

        }

        private void BtnEdit_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    ButtonEdit a = sender as ButtonEdit;
            //    Commons.Modules.ObjSystems.OpenHinh(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text + '\\' + a.Text);
            //}
            //catch
            //{
            //}
        }

        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (Commons.Modules.iLOAI_CN == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
                    return;
                }
                ButtonEdit a = sender as ButtonEdit;
                ofdfile.Filter = "All Files|*.txt;*.docx;*.doc;*.pdf*.xls;*.xlsx;*.pptx;*.ppt|Text File (.txt)|*.txt|Word File (.docx ,.doc)|*.docx;*.doc|Spreadsheet (.xls ,.xlsx)|  *.xls ;*.xlsx";
                ofdfile.FileName = "";
                if (ofdfile.ShowDialog() == DialogResult.OK)
                {
                    string sduongDan = ofdfile.FileName.ToString().Trim();
                    if (ofdfile.FileName.ToString().Trim() == "") return;
                    Commons.Modules.ObjSystems.LuuDuongDan(ofdfile.FileName, ofdfile.SafeFileName, this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text);
                    string folderLocation = Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text + '\\' + ofdfile.SafeFileName;
                    a.Text = folderLocation;
                }
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
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
            Commons.Modules.bEnabel = !visible;


            if (Commons.Modules.KyHieuDV != "DM")
            {
                MS_CNTextEdit.Properties.ReadOnly = visible;
                MS_THE_CCTextEdit.Properties.ReadOnly = visible;
            }
            //MS_THE_CCTextEdit.Properties.ReadOnly = visible;
            ID_QGLookUpEdit.Properties.ReadOnly = visible;
            HOTextEdit.Properties.ReadOnly = visible;
            TENTextEdit.Properties.ReadOnly = visible;
            TEN_KHONG_DAUTextEdit.Properties.ReadOnly = visible;
            VAO_LAM_LAICheckEdit.Properties.ReadOnly = visible;
            NGAY_SINHDateEdit.ReadOnly = visible;
            NAM_SINHDateEdit.ReadOnly = visible;
            PHAILookupEdit.Properties.ReadOnly = visible;
            ID_XNLookUpEdit.Properties.ReadOnly = visible;
            ID_DVLookUpEdit.Properties.ReadOnly = visible;
            ID_TOLookupEdit.Properties.ReadOnly = visible;
            ID_CVLookUpEdit.Properties.ReadOnly = visible;
            ID_LCVLookUpEdit.Properties.ReadOnly = visible;
            PHEP_CTTextEdit.Properties.ReadOnly = visible;
            NGAY_HOC_VIECDateEdit.ReadOnly = visible;
            NGAY_THU_VIECDateEdit.ReadOnly = visible;
            NGAY_VAO_LAMDateEdit.ReadOnly = visible;
            ID_TT_HDLookUpEdit.Properties.ReadOnly = visible;
            ID_TT_HTLookUpEdit.Properties.ReadOnly = visible;
            HINH_THUC_TUYENTextEdit.Properties.ReadOnly = visible;
            LD_TINHCheckEdit.Properties.ReadOnly = visible;
            LAO_DONG_CNCheckEdit.Properties.ReadOnly = visible;
            TRUC_TIEP_SXCheckEdit.Properties.ReadOnly = visible;

            SO_CMNDTextEdit.Properties.ReadOnly = visible;
            NGAY_CAPDateEdit.ReadOnly = visible;
            txtNOI_CAP.Properties.ReadOnly = visible;
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

            txtDIA_CHI_KS.Properties.ReadOnly = visible;
            cboID_TP_KS.Properties.ReadOnly = visible;
            cboID_PX_KS.Properties.ReadOnly = visible;
            cboID_QUAN_KS.Properties.ReadOnly = visible;
            cboID_KV.Properties.ReadOnly = visible;


            THAM_GIA_BHXHCheckEdit.Properties.ReadOnly = visible;
            SO_BHXHTextEdit.Properties.ReadOnly = visible;
            NGAY_DBHXHDateEdit.ReadOnly = visible;
            NGAY_CHAM_DUT_NOP_BHXHDateEdit.ReadOnly = visible;
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
        private void LockTheoQTCT()
        {
            ID_XNLookUpEdit.Properties.ReadOnly = true;
            ID_DVLookUpEdit.Properties.ReadOnly = true;
            ID_TOLookupEdit.Properties.ReadOnly = true;
            ID_CVLookUpEdit.Properties.ReadOnly = true;
            ID_LCVLookUpEdit.Properties.ReadOnly = true;
        }

        private void LockTheoHDLD()
        {
            NGAY_THU_VIECDateEdit.Properties.ReadOnly = true;
            //NGAY_VAO_LAMDateEdit.Properties.ReadOnly = true;
            ID_TT_HDLookUpEdit.Properties.ReadOnly = true;
            ID_LHDLDLookUpEdit.Properties.ReadOnly = true;
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
        private string SaveImage(string imageURL) // sLoai
        {
            try
            {
                if (imageURL.Trim() == "") return "-2";
                var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("HinhCongNhan\\", false);
                string strDuongDan = "";
                strDuongDan = imageURL;
                string TenFile;
                TenFile = System.IO.Path.GetFileName(imageURL);
                string a = "";
                if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + MS_CNTextEdit.Text + ".jpg") == false)
                    a = strDuongDanTmp + @"\" + MS_CNTextEdit.Text + ".jpg";
                else
                {
                    TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, MS_CNTextEdit.Text + ".jpg");
                    a = strDuongDanTmp + @"\" + MS_CNTextEdit.Text + ".jpg";
                }
                try
                {
                    FileInfo file = new FileInfo(a);
                    file.Delete();
                }
                catch { }

                Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, a);
                return a;
            }
            catch
            {
                return "-2";
            }
        }
        private bool SaveData()
        {
            //test();
            try
            {

                //tạo bảng tạm bằng cấp
                string sTBBangCap = "sbtBC" + Commons.Modules.iIDUser;
                if (Commons.Modules.ObjSystems.ConvertDatatable(grvBangCapCN) == null)
                {
                    sTBBangCap = "";
                }
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTBBangCap, Commons.Modules.ObjSystems.ConvertDatatable(grvBangCapCN), "");

                string sTBTaiLieu = "sbtTL" + Commons.Modules.iIDUser;
                if (Commons.Modules.ObjSystems.ConvertDatatable(grvTaiLieu) == null)
                {
                    sTBTaiLieu = "";
                }
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTBTaiLieu, Commons.Modules.ObjSystems.ConvertDatatable(grvTaiLieu), "");

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateCongNhan", conn);
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                cmd.Parameters.Add("@HINH_CN", SqlDbType.Image).Value = Commons.Modules.KyHieuDV == "SB" ? imgToByteConverter(HINH_CNPictureEdit.Image) : null;
                cmd.Parameters.Add("@HINH_CN_URL", SqlDbType.NVarChar).Value = HINH_CNPictureEdit.EditValue == null ? "-1" : SaveImage(HINH_CNPictureEdit.GetLoadedImageLocation());
                cmd.Parameters.Add("@MS_CN", SqlDbType.NVarChar).Value = MS_CNTextEdit.Text;
                cmd.Parameters.Add("@MS_THE_CC", SqlDbType.NVarChar).Value = MS_THE_CCTextEdit.Text;
                cmd.Parameters.Add("@ID_QG", SqlDbType.BigInt).Value = Convert.ToString(ID_QGLookUpEdit.EditValue) == "" ? DBNull.Value : ID_QGLookUpEdit.EditValue;
                cmd.Parameters.Add("@HO", SqlDbType.NVarChar).Value = HOTextEdit.Text;
                cmd.Parameters.Add("@TEN", SqlDbType.NVarChar).Value = TENTextEdit.Text;
                cmd.Parameters.Add("@TEN_KHONG_DAU", SqlDbType.NVarChar).Value = TEN_KHONG_DAUTextEdit.Text;
                cmd.Parameters.Add("@NGAY_SINH", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(NGAY_SINHDateEdit.Text);
                cmd.Parameters.Add("@NAM_SINH", SqlDbType.Int).Value = NAM_SINHDateEdit.Text == "" ? NAM_SINHDateEdit.EditValue = null : Convert.ToInt32(NAM_SINHDateEdit.EditValue);
                cmd.Parameters.Add("@PHAI", SqlDbType.Bit).Value = PHAILookupEdit.EditValue;
                cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = Convert.ToString(ID_TOLookupEdit.EditValue) == "" ? DBNull.Value : ID_TOLookupEdit.EditValue;
                cmd.Parameters.Add("@ID_CV", SqlDbType.BigInt).Value = Convert.ToString(ID_CVLookUpEdit.EditValue) == "" ? DBNull.Value : ID_CVLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_LCV", SqlDbType.BigInt).Value = Convert.ToString(ID_LCVLookUpEdit.EditValue) == "" ? DBNull.Value : ID_LCVLookUpEdit.EditValue;
                cmd.Parameters.Add("@PHEP_CT", SqlDbType.Float).Value = PHEP_CTTextEdit.EditValue;
                cmd.Parameters.Add("@NGAY_HOC_VIEC", SqlDbType.DateTime).Value = NGAY_HOC_VIECDateEdit.Text == "" ? DBNull.Value : NGAY_HOC_VIECDateEdit.EditValue;
                cmd.Parameters.Add("@NGAY_THU_VIEC", SqlDbType.DateTime).Value = NGAY_THU_VIECDateEdit.Text == "" ? DBNull.Value : NGAY_THU_VIECDateEdit.EditValue;
                cmd.Parameters.Add("@NGAY_VAO_LAM", SqlDbType.DateTime).Value = NGAY_VAO_LAMDateEdit.Text == "" ? DBNull.Value : NGAY_VAO_LAMDateEdit.EditValue;
                cmd.Parameters.Add("@ID_TT_HD", SqlDbType.BigInt).Value = Convert.ToString(ID_TT_HDLookUpEdit.EditValue) == "" ? DBNull.Value : ID_TT_HDLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_TT_HT", SqlDbType.BigInt).Value = Convert.ToString(ID_TT_HTLookUpEdit.EditValue) == "" ? DBNull.Value : ID_TT_HTLookUpEdit.EditValue;
                cmd.Parameters.Add("@HINH_THUC_TUYEN", SqlDbType.NVarChar).Value = HINH_THUC_TUYENTextEdit.Text;
                cmd.Parameters.Add("@LD_TINH", SqlDbType.Bit).Value = LD_TINHCheckEdit.EditValue;
                cmd.Parameters.Add("@TRUC_TIEP_SX", SqlDbType.Bit).Value = TRUC_TIEP_SXCheckEdit.EditValue;
                cmd.Parameters.Add("@LAO_DONG_CONG_NHAT", SqlDbType.Bit).Value = LAO_DONG_CNCheckEdit.EditValue;
                cmd.Parameters.Add("@VAO_LAM_LAI", SqlDbType.Bit).Value = VAO_LAM_LAICheckEdit.EditValue;
                cmd.Parameters.Add("@SO_CMND", SqlDbType.NVarChar).Value = SO_CMNDTextEdit.Text;
                cmd.Parameters.Add("@NGAY_CAP", SqlDbType.DateTime).Value = NGAY_CAPDateEdit.Text == "" ? DBNull.Value : NGAY_CAPDateEdit.EditValue;
                cmd.Parameters.Add("@NOI_CAP", SqlDbType.NVarChar).Value = txtNOI_CAP.Text;
                cmd.Parameters.Add("@ID_TT_HN", SqlDbType.BigInt).Value = Convert.ToString(ID_TT_HNLookUpEdit.EditValue) == "" ? DBNull.Value : ID_TT_HNLookUpEdit.EditValue;
                cmd.Parameters.Add("@MS_THUE", SqlDbType.NVarChar).Value = MS_THUETextEdit.Text;
                cmd.Parameters.Add("@MA_THE_ATM", SqlDbType.NVarChar).Value = MA_THE_ATMTextEdit.Text;
                cmd.Parameters.Add("@SO_TAI_KHOAN", SqlDbType.NVarChar).Value = SO_TAI_KHOANTextEdit.Text;
                cmd.Parameters.Add("@CHUYEN_MON", SqlDbType.NVarChar).Value = CHUYEN_MONTextEdit.Text;
                cmd.Parameters.Add("@ID_LOAI_TD", SqlDbType.BigInt).Value = Convert.ToString(ID_LOAI_TDLookUpEdit.EditValue) == "" ? DBNull.Value : ID_LOAI_TDLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_TDVH", SqlDbType.BigInt).Value = Convert.ToString(ID_TDVHLookUpEdit.EditValue) == "" ? DBNull.Value : ID_TDVHLookUpEdit.EditValue;
                cmd.Parameters.Add("@NGOAI_NGU", SqlDbType.NVarChar).Value = NGOAI_NGUTextEdit.Text;
                cmd.Parameters.Add("@DT_NHA", SqlDbType.NVarChar).Value = DT_NHATextEdit.Text;
                cmd.Parameters.Add("@DT_NGUOI_THAN", SqlDbType.NVarChar).Value = DT_NGUOI_THANTextEdit.Text;
                cmd.Parameters.Add("@DT_DI_DONG", SqlDbType.NVarChar).Value = DT_DI_DONGTextEdit.Text;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = EMAILTextEdit.Text;
                cmd.Parameters.Add("@NOI_SINH", SqlDbType.NVarChar).Value = NOI_SINHTextEdit.Text;
                cmd.Parameters.Add("@NGUYEN_QUAN", SqlDbType.NVarChar).Value = NGUYEN_QUANTextEdit.Text;
                cmd.Parameters.Add("@ID_DT", SqlDbType.BigInt).Value = Convert.ToString(ID_DTLookUpEdit.EditValue) == "" ? DBNull.Value : ID_DTLookUpEdit.EditValue;
                cmd.Parameters.Add("@TON_GIAO", SqlDbType.NVarChar).Value = TON_GIAOTextEdit.Text;
                cmd.Parameters.Add("@DIA_CHI_THUONG_TRU", SqlDbType.NVarChar).Value = DIA_CHI_THUONG_TRUTextEdit.Text;
                cmd.Parameters.Add("@ID_TP", SqlDbType.BigInt).Value = Convert.ToString(ID_TPLookUpEdit.EditValue) == "" ? DBNull.Value : ID_TPLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_QUAN", SqlDbType.BigInt).Value = Convert.ToString(ID_QUANLookEdit.EditValue) == "" ? DBNull.Value : ID_QUANLookEdit.EditValue;
                cmd.Parameters.Add("@ID_PX", SqlDbType.BigInt).Value = Convert.ToString(ID_PXLookUpEdit.EditValue) == "" ? DBNull.Value : ID_PXLookUpEdit.EditValue;
                cmd.Parameters.Add("@THON_XOM", SqlDbType.NVarChar).Value = THON_XOMTextEdit.Text;
                cmd.Parameters.Add("@DIA_CHI_TAM_TRU", SqlDbType.NVarChar).Value = DIA_CHI_TAM_TRUTextEdit.Text;
                cmd.Parameters.Add("@ID_TP_TAM_TRU", SqlDbType.BigInt).Value = Convert.ToString(ID_TP_TAM_TRULookUpEdit.EditValue) == "" ? DBNull.Value : ID_TP_TAM_TRULookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_QUAN_TAM_TRU", SqlDbType.BigInt).Value = Convert.ToString(ID_QUAN_TAM_TRULookUpEdit.EditValue) == "" ? DBNull.Value : ID_QUAN_TAM_TRULookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_PX_TAM_TRU", SqlDbType.BigInt).Value = Convert.ToString(ID_PX_TAM_TRULookUpEdit.EditValue) == "" ? DBNull.Value : ID_PX_TAM_TRULookUpEdit.EditValue;
                cmd.Parameters.Add("@THON_XOM_TAM_TRU", SqlDbType.NVarChar).Value = THON_XOM_TAM_TRUTextEdit.Text;
                cmd.Parameters.Add("@SO_BHXH", SqlDbType.NVarChar).Value = SO_BHXHTextEdit.Text;
                cmd.Parameters.Add("@NGAY_DBHXH", SqlDbType.DateTime).Value = NGAY_DBHXHDateEdit.Text == "" ? DBNull.Value : NGAY_DBHXHDateEdit.EditValue;
                cmd.Parameters.Add("@NGAY_CHAM_DUT_NOP_BHXH", SqlDbType.DateTime).Value = NGAY_CHAM_DUT_NOP_BHXHDateEdit.Text == "" ? DBNull.Value : NGAY_CHAM_DUT_NOP_BHXHDateEdit.EditValue;
                cmd.Parameters.Add("@THAM_GIA_BHXH", SqlDbType.Bit).Value = THAM_GIA_BHXHCheckEdit.EditValue;
                cmd.Parameters.Add("@SO_THE_BHYT", SqlDbType.NVarChar).Value = SO_THE_BHYTTextEdit.Text;
                cmd.Parameters.Add("@NGAY_HET_HAN", SqlDbType.DateTime).Value = NGAY_HET_HANDateEdit.Text == "" ? DBNull.Value : NGAY_HET_HANDateEdit.EditValue;
                cmd.Parameters.Add("@ID_TT", SqlDbType.BigInt).Value = Convert.ToString(TINH_THANHLookUpEdit.EditValue) == "" ? DBNull.Value : TINH_THANHLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_BV", SqlDbType.BigInt).Value = Convert.ToString(BENH_VIENLookUpEdit.EditValue) == "" ? DBNull.Value : BENH_VIENLookUpEdit.EditValue;
                cmd.Parameters.Add("@LD_NN", SqlDbType.Bit).Value = LD_NNCheckEdit.EditValue;
                cmd.Parameters.Add("@SO_GIAY_PHEP", SqlDbType.NVarChar).Value = SO_GIAY_PHEPTextEdit.Text;
                cmd.Parameters.Add("@NGAY_CAP_GP", SqlDbType.Bit).Value = NGAY_CAP_GPDateEdit.Text == "" ? DBNull.Value : NGAY_CAP_GPDateEdit.EditValue;
                cmd.Parameters.Add("@LOAI_QUOC_TICH", SqlDbType.Int).Value = Convert.ToString(LOAI_QUOC_TICHLookUpEdit.EditValue) == "" ? DBNull.Value : LOAI_QUOC_TICHLookUpEdit.EditValue;
                cmd.Parameters.Add("@CAP_GIAY_PHEP", SqlDbType.Int).Value = CAP_GIAY_PHEPLookUpEdit.Text == "" ? DBNull.Value : CAP_GIAY_PHEPLookUpEdit.EditValue;
                cmd.Parameters.Add("@NGAY_HH_GP", SqlDbType.DateTime).Value = NGAY_HH_GPDateEdit.Text == "" ? DBNull.Value : NGAY_HH_GPDateEdit.EditValue;
                cmd.Parameters.Add("@LD_GIAM_LDNN", SqlDbType.BigInt).Value = LD_GIAM_LDNNLookUpEdit.Text == "" ? DBNull.Value : LD_GIAM_LDNNLookUpEdit.EditValue;
                cmd.Parameters.Add("@ID_KV", SqlDbType.BigInt).Value = Convert.ToString(cboID_KV.EditValue) == "" ? DBNull.Value : cboID_KV.EditValue;
                cmd.Parameters.Add("@Them", SqlDbType.Bit).Value = cothem;
                cmd.Parameters.Add("@sbtBC", SqlDbType.NVarChar).Value = sTBBangCap;
                cmd.Parameters.Add("@sbtTL", SqlDbType.NVarChar).Value = sTBTaiLieu;
                cmd.Parameters.Add("@DC_KHAI_SINH", SqlDbType.NVarChar).Value = txtDIA_CHI_KS.Text;
                cmd.Parameters.Add("@ID_TP_KS", SqlDbType.BigInt).Value = Convert.ToString(cboID_TP_KS.EditValue) == "" ? DBNull.Value : cboID_TP_KS.EditValue;
                cmd.Parameters.Add("@ID_QUAN_KS", SqlDbType.BigInt).Value = Convert.ToString(cboID_QUAN_KS.EditValue) == "" ? DBNull.Value : cboID_QUAN_KS.EditValue;
                cmd.Parameters.Add("@ID_PX_KS", SqlDbType.BigInt).Value = Convert.ToString(cboID_PX_KS.EditValue) == "" ? DBNull.Value : cboID_PX_KS.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                Commons.Modules.iCongNhan = Convert.ToInt64(cmd.ExecuteScalar());
                try
                {
                    //xóa hết file không có trong 
                    string[] fileList = Directory.GetFiles(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + MS_CNTextEdit.Text);//lay danh sách file cho vao mảng
                                                                                                                                                        //duyet mang file trong thư mục
                                                                                                                                                        //duyệt list file không có trong lưới thì xóa
                    foreach (string item in fileList)
                    {
                        //kiểm tra item có trong table không
                        if (Commons.Modules.ObjSystems.ConvertDatatable(grvTaiLieu).AsEnumerable().Count(x => x["DUONG_DAN"].Equals(item)) == 0)
                        {
                            Commons.Modules.ObjSystems.Xoahinh(item);
                        }
                    }
                }
                catch
                {
                }

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
                int nsinh = NGAY_SINHDateEdit.Text == "" ? Convert.ToInt32(NAM_SINHDateEdit.EditValue) : Convert.ToInt32((Convert.ToDateTime(NGAY_SINHDateEdit.EditValue)).Year.ToString());
                if (nsinh >= nvaolam)
                {

                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "messNgayvaolamkhonghople"));
                    NGAY_VAO_LAMDateEdit.Focus();
                    return false;
                }

            }
            cmd.CommandType = CommandType.StoredProcedure;
            //if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
            //{
            //    if (cot == 1)
            //    {

            //        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSCNDaTrungBanCoMuonTaoMaMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            MS_CNTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + Convert.ToInt32(ID_DVLookUpEdit.EditValue) + ",1)").ToString();
            //        }
            //        MS_CNTextEdit.Focus();
            //    }
            //    if (cot == 2)
            //    {
            //        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSTheCCDaTrungBanCoMuonTaoMaMoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            MS_THE_CCTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + Convert.ToInt32(ID_DVLookUpEdit.EditValue) + ",2)").ToString();
            //        }
            //        MS_THE_CCTextEdit.Focus();
            //    }
            //    return false;
            //}
            return true;
        }
        #endregion

        private void ID_QGLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            //////if (Commons.Modules.sLoad == "0Load") return;
            //////if (ID_QGLookUpEdit.EditValue.ToString() == "") return;
            //////Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TPLookUpEdit, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(ID_QGLookUpEdit.EditValue), false), "ID_TP", "TEN_TP", "TEN_TP", true);
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
            switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(ID_DVLookUpEdit.EditValue)))
            {
                case "DM":
                    {
                        if (Commons.Modules.iCongNhan == -1 || idcn == -1)
                        {
                            try
                            {
                                MS_CNTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + ID_DVLookUpEdit.EditValue + ",1)").ToString();
                                MS_THE_CCTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_CONG_NHAN(" + ID_DVLookUpEdit.EditValue + ",2)").ToString();
                                //MS_CNTextEdit.Properties.ReadOnly = false;
                                //MS_THE_CCTextEdit.Properties.ReadOnly = false;
                            }
                            catch { }
                        }

                        break;
                    }

            }

            if (isCancel) return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_XNLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(ID_DVLookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
            //CheckMS();
            //CheckMT();
        }

        private void ID_XNLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (isCancel) return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookupEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(ID_DVLookUpEdit.EditValue), Convert.ToInt32(ID_XNLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, true);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(ID_XNLookUpEdit.EditValue)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);

        }

        private void ID_LCVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (isCancel) return;
            try
            {
                PHEP_CTTextEdit.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[funPhepCongThem](" + ID_LCVLookUpEdit.EditValue + ")").ToString();

                ID_CVLookUpEdit.Properties.ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCVLookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", false);
                ID_CVLookUpEdit.Properties.ReadOnly = true;
            }
            catch (Exception)
            {
                PHEP_CTTextEdit.Text = "0";
            }
        }
        private void grdBangCapCN_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete && windowsUIButton.Buttons[5].Properties.Visible == true)
            {
                XoaUser();
            }
        }

        private void XoaUser()
        {

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteUser"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BANG_CAP WHERE ID_BC  = " + grvBangCapCN.GetFocusedRowCellValue("ID_BC") + "");
                grvBangCapCN.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString());
            }
        }

        private void THAM_GIA_BHXHCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (windowsUIButton.Buttons[5].Properties.Visible == false)
            {
                bool visible = THAM_GIA_BHXHCheckEdit.Checked;
                SO_BHXHTextEdit.Properties.ReadOnly = !visible;
                NGAY_DBHXHDateEdit.ReadOnly = !visible;
                NGAY_CHAM_DUT_NOP_BHXHDateEdit.ReadOnly = !visible;
                SO_THE_BHYTTextEdit.Properties.ReadOnly = !visible;
                NGAY_HET_HANDateEdit.Properties.ReadOnly = !visible;
                TINH_THANHLookUpEdit.Properties.ReadOnly = !visible;
                BENH_VIENLookUpEdit.Properties.ReadOnly = !visible;
            }
        }

        private void LD_NNCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (windowsUIButton.Buttons[5].Properties.Visible == false)
            {
                bool visible = LD_NNCheckEdit.Checked;
                SO_GIAY_PHEPTextEdit.Properties.ReadOnly = !visible;
                NGAY_CAP_GPDateEdit.Properties.ReadOnly = !visible;
                LOAI_QUOC_TICHLookUpEdit.Properties.ReadOnly = !visible;
                CAP_GIAY_PHEPLookUpEdit.Properties.ReadOnly = !visible;
                NGAY_HH_GPDateEdit.Properties.ReadOnly = !visible;
                LD_GIAM_LDNNLookUpEdit.Properties.ReadOnly = !visible;
            }
        }

        private void NAM_SINHDateEdit_TextChanged(object sender, EventArgs e)
        {
        }

        private void NGAY_SINHDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            NAM_SINHDateEdit.Properties.ReadOnly = false;
            DateTime NgaySinh = Convert.ToDateTime(NGAY_SINHDateEdit.EditValue);
            NAM_SINHDateEdit.EditValue = NgaySinh.Year.ToString().Trim();
            NAM_SINHDateEdit.Text = NgaySinh.Year.ToString().Trim();
            NAM_SINHDateEdit.Properties.ReadOnly = true;
        }

        private void SO_CMNDTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (SO_CMNDTextEdit.EditValue.ToString().Trim().Length == 12 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void CheckMS()
        {
            try
            {
                bool isCorrectMS = true;
                string MS = "";
                if (MS_CNTextEdit.EditValue != null)
                {
                    MS = MS_CNTextEdit.EditValue.ToString().Trim();
                }
                if (MS.Length > 9 || MS.Length < 9)
                {
                    string DV = "";
                    if (ID_DVLookUpEdit.EditValue != null)
                    {
                        DV = ID_DVLookUpEdit.EditValue.ToString().Trim();
                    }

                    switch (DV)
                    {
                        case "1":
                            {
                                MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1.";
                                isCorrectMS = false;
                                break;
                            }
                        case "2":
                            {
                                MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMT + 6 số đuôi cho đơn vị Duy Minh 2.";
                                isCorrectMS = false;
                                break;
                            }
                        default:
                            {
                                MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1 hoặc  DMT + 6 số đuôi cho đơn vị Duy Minh 2";
                                isCorrectMS = false;
                                break;
                            }
                    }
                }
                if (MS.Length == 9)
                {
                    string DV = "";
                    if (ID_DVLookUpEdit.EditValue != null)
                    {
                        DV = ID_DVLookUpEdit.EditValue.ToString().Trim();
                    }

                    switch (DV)
                    {
                        case "1":
                            {
                                if (MS.Substring(0, 3) != "DMS")
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MS.Substring(3, 6)))
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                        case "2":
                            {
                                if (MS.Substring(0, 3) != "DMT")
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMT + 6 số đuôi cho đơn vị Duy Minh 2.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MS.Substring(3, 6)))
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMT + 6 số đuôi cho đơn vị Duy Minh 2.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                        default:
                            {
                                if (MS.Substring(0, 3) != "DMS" && MS.Substring(0, 3) != "DMT")
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1 hoặc DMT + 6 số đuôi cho đơn vị Duy Minh 2.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MS.Substring(3, 6)))
                                {
                                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_CNTextEdit.ErrorText = "Vui lòng nhập đúng định dạng DMS + 6 số đuôi cho đơn vị Duy Minh 1 hoặc DMT + 6 số đuôi cho đơn vị Duy Minh 2.";
                                    isCorrectMS = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                    }
                }

                HopLeMS = isCorrectMS;

                if (HopLeMS)
                {
                    MS_CNTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
                    MS_CNTextEdit.ErrorText = null;
                }
            }
            catch { }
        }

        private void CheckMT()
        {
            try
            {


                bool isCorrectMT = true;
                string MT = "";
                if (MS_THE_CCTextEdit.EditValue != null)
                {
                    MT = MS_THE_CCTextEdit.EditValue.ToString().Trim();
                }
                if (MT.Length > 7 || MT.Length < 7)
                {
                    string DV = "";
                    if (ID_DVLookUpEdit.EditValue != null)
                    {
                        DV = ID_DVLookUpEdit.EditValue.ToString().Trim();
                    }

                    switch (DV)
                    {
                        case "1":
                            {
                                MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                isCorrectMT = false;
                                break;
                            }

                        case "2":
                            {
                                MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                isCorrectMT = false;
                                break;
                            }
                        default:
                            {
                                MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                isCorrectMT = false;
                                break;
                            }
                    }
                }
                if (MT.Length == 7)
                {
                    string DV = "";
                    if (ID_DVLookUpEdit.EditValue != null)
                    {
                        DV = ID_DVLookUpEdit.EditValue.ToString().Trim();
                    }

                    switch (DV)
                    {
                        case "1":
                            {
                                if (MT.Substring(0, 1) != "1")
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 0 + 6 số đuôi cho đơn vị Duy Minh 1.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MT.Substring(1, 6)))
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 0 + 6 số đuôi cho đơn vị Duy Minh 1.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                        case "2":
                            {
                                if (MT.Substring(0, 1) != "1")
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MT.Substring(1, 6)))
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                        default:
                            {
                                if (MT.Substring(0, 1) != "1")
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                if (!IsNumber(MT.Substring(3, 6)))
                                {
                                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                                    MS_THE_CCTextEdit.ErrorText = "Vui lòng nhập đúng định dạng số 1 + 6 số đuôi.";
                                    isCorrectMT = false;
                                }
                                else
                                {
                                    this.ClearError();
                                }
                                break;
                            }
                    }
                }

                HopLeMT = isCorrectMT;
                if (HopLeMT)
                {
                    MS_THE_CCTextEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
                    MS_THE_CCTextEdit.ErrorText = null;
                }
            }
            catch { }
        }
        private void MS_CNTextEdit_Validated(object sender, EventArgs e)
        {
            if (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(ID_DVLookUpEdit.EditValue)) == "DM")
            {
                CheckMS();
            }

        }

        private void MS_THE_CCTextEdit_Validated(object sender, EventArgs e)
        {
            if (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(ID_DVLookUpEdit.EditValue)) == "DM")
            {
                CheckMT();
            }
        }

        private void NGAY_SINHDateEdit_Validated(object sender, EventArgs e)
        {
            if (NGAY_SINHDateEdit.EditValue == null) return;

            DateTime NgaySinh = Convert.ToDateTime(NGAY_SINHDateEdit.EditValue);

            var Age = DateTime.Today.Year - NgaySinh.Year;

            if (Age < 16)
            {
                NGAY_SINHDateEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Error;
                NGAY_SINHDateEdit.ErrorText = "Nhân viên phải đủ 16 tuổi.Vui lòng kiểm tra lại!";
                HopLeNgaySinh = false;
            }
            else
            {
                HopLeNgaySinh = true;
                NGAY_SINHDateEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None;
                NGAY_SINHDateEdit.ErrorText = null;
            }
        }

        private void DT_NHATextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (DT_NHATextEdit.EditValue.ToString().Trim().Length == 11 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void DT_NGUOI_THANTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (DT_NGUOI_THANTextEdit.EditValue.ToString().Trim().Length == 11 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void DT_DI_DONGTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (DT_DI_DONGTextEdit.EditValue.ToString().Trim().Length == 11 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void MA_THE_ATMTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cboID_TP_KS_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (cboID_TP_KS.EditValue == null || cboID_TP_KS.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_QUAN_KS, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(cboID_TP_KS.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void cboID_QUAN_KS_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (cboID_QUAN_KS.EditValue == null || cboID_QUAN_KS.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_PX_KS, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(cboID_QUAN_KS.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }

        private void MS_THE_CCTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void MS_CNTextEdit_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                MS_THE_CCTextEdit.Text = MS_CNTextEdit.Text;
            }
            catch { };


        }
        //private void LayDuongDan()
        //{
        //    string strPath_DH = txtTaiLieu.Text;
        //    strDuongDan = ofdfile.FileName;

        //    var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_CN");
        //    string[] sFile;
        //    string TenFile;

        //    TenFile = ofdfile.SafeFileName.ToString();
        //    sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

        //    if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
        //        txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
        //    else
        //    {
        //        TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
        //        txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
        //    }
        //}

        //private void txtTaiLieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Index == 0)
        //    {
        //        try
        //        {
        //            if (windowsUIButton.Buttons[10].Properties.Visible)
        //            {
        //                if (ofdfile.ShowDialog() == DialogResult.Cancel) return;
        //                LayDuongDan();
        //            }
        //            else
        //            {
        //                if (txtTaiLieu.Text == "")
        //                    return;
        //                Commons.Modules.ObjSystems.OpenHinh(txtTaiLieu.Text);
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
        //            txtTaiLieu.ResetText();
        //            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.CONG_NHAN SET FILE_DK = NULL WHERE ID_CN =" + Commons.Modules.iCongNhan + "");
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        private void dataLayoutControl1_PopupMenuShowing(object sender, DevExpress.XtraLayout.PopupMenuShowingEventArgs e)
        {
            try
            {
                e.Menu.Items.Add(new DXMenuItem("&Save Layout", new EventHandler(SaveLayout)));
            }
            catch
            {
            }
        }
        private void SaveLayout(object sender, EventArgs e)
        {
        }

        private void grvTaiLieu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DUONG_DAN")
            {
                try
                {
                    grvTaiLieu.SetFocusedRowCellValue("TEN_TAI_LIEU", ofdfile.SafeFileName);
                }
                catch
                {
                }
            }
        }

        private void grvTaiLieu_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue(view.Columns["ID_CN"], idcn);
                view.SetFocusedRowCellValue(view.Columns["NGUOI_UL"], Commons.Modules.UserName);
                view.SetFocusedRowCellValue(view.Columns["THOI_GIAN_UL"], DateTime.Now);
            }
            catch
            {
            }
        }

        private void grvTaiLieu_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "DUONG_DAN")
            //{
            //    try
            //    {
            //        grvTaiLieu.SetFocusedRowCellValue("TEN_TAI_LIEU", ofdfile.SafeFileName);
            //    }
            //    catch
            //    {
            //    }
        }

        private void grvTaiLieu_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            try
            {
                if (info.Column.FieldName == "DUONG_DAN" && info.RowHandle >= 0)
                {
                    Commons.Modules.ObjSystems.OpenHinh(grvTaiLieu.GetFocusedRowCellValue("DUONG_DAN").ToString());
                }
            }
            catch
            {
            }
        }

        private void HINH_CNPictureEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ID_TT_HDLookUpEdit_BeforePopup(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HDLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHD(false).AsEnumerable().Where(x => x["ID_TT_HD"].ToString() != "5").CopyToDataTable(), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", "", true);

        }

        private void ID_TT_HTLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            //
            if (windowsUIButton.Buttons[5].Properties.Visible == true) return;
                string skHNV = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(KY_HIEU,'') FROM dbo.TINH_TRANG_HT WHERE ID_TT_HT =  " + ID_TT_HTLookUpEdit.EditValue + "").ToString();
            if (skHNV.ToLower().Trim() == "nv")
            {
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_TT_HDLookUpEdit, Commons.Modules.ObjSystems.DataTinHTrangHD(false), "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD", "", true);
                ID_TT_HDLookUpEdit.EditValue = Convert.ToInt64(5);
                ID_TT_HDLookUpEdit.Properties.ReadOnly =true;
            }
            else
            {
                ID_TT_HDLookUpEdit.EditValue = Convert.ToInt64(1);
                ID_TT_HDLookUpEdit.Properties.ReadOnly = false;
            }    
        }
    }
}
