using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;

namespace Vs.HRM
{
    public partial class ucQTCongTac : DevExpress.XtraEditors.XtraUserControl
    {
        Int64 idcn = 0;
        int idQTCT = -1;
        int idToCu = -1;
        bool cothem = false;
        string strDuongDan = "";
        string btnName = "";

        DataTable tableTTC_CN = new DataTable();
        DataTable tableLyLich = new DataTable();
        public ucQTCongTac(Int64 id)
        {
            InitializeComponent();

            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            idcn = id;
        }

        private void Loaddatatable()
        {
            tableTTC_CN.Clear();
            tableTTC_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.funQuaTrinhCongTac(" + Commons.Modules.iCongNhan + "," + Commons.Modules.TypeLanguage + ")"));
        }

        #region sự kiệm form
        private void ucQTCongTac_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            //format datetime
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCDateEdit);

            //load combo
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(DON_VILookUpEdit, Commons.Modules.ObjSystems.DataDonVi(true), "ID_DV", "TEN_DV", "TEN_DV", true, false, false);

            //xí nghiệp 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(XI_NGHIEPLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(-1), false), "ID_XN", "TEN_XN", "TEN_XN", true, false, false);

            //tổ
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookUpEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(-1), Convert.ToInt32(-1), false), "ID_TO", "TEN_TO", "TEN_TO", true, false, false);

            //ID_LQDLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LQDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiQuyetDinh(false), "ID_LQD", "TEN_LQD", "TEN_LQD", true);

            //ID_LCVLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);

            //ID_CVLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV", true);

            //ID_CV_CULookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CV_CULookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV", true);

            //ID_NKLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NKLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");

            //ID_LCV_CULookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCV_CULookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);

            //ID_CTL_CULookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CTL_CULookUpEdit, Commons.Modules.ObjSystems.DataCTL(false), "ID_CTL", "TEN_CTL", "TEN_CTL", true);

            //ID_CTLookUpEdit.EditValue = "";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CTLLookUpEdit, Commons.Modules.ObjSystems.DataCTL(false), "ID_CTL", "TEN_CTL", "TEN_CTL", true);
            
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrang(false), "ID_TT", "TenTT", "TenTT");
            LoadgrdCongTac(-1);

            if (grvCongTac.RowCount == 0)
            {
                this.Load_ChuyenTu();
            }

            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";

        }

        private void Load_ChuyenTu()
        {
            try
            {
                tableLyLich.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DV.ID_DV, DV.TEN_DV, T.ID_TO, T.TEN_TO, XN.ID_XN, XN.TEN_XN, LCV.ID_LCV, LCV.TEN_LCV, CV.ID_CV, CV.TEN_CV , CASE CV.MS_CV WHEN '2' THEN 2 WHEN '1' THEN 1 ELSE 1 END AS ID_CTL, " +
                                                                "CASE CV.MS_CV WHEN '2' THEN N'Lương sản phẩm' WHEN '1' THEN N'Lương thời gian' ELSE N'Lương thời gian' END AS TEN_CTL, " +
                                                                "''" + "NHIEM_VU," + "''" + "NOI_CONG_TAC " +
                                                                "FROM CONG_NHAN A LEFT JOIN dbo.[TO] T ON T.ID_TO = A.ID_TO LEFT JOIN dbo.XI_NGHIEP XN ON T.ID_XN = XN.ID_XN "
                                                                + "LEFT JOIN dbo.DON_VI DV ON DV.ID_DV = XN.ID_DV "
                                                                + "LEFT JOIN dbo.CHUC_VU CV ON CV.ID_CV = A.ID_CV "
                                                                + " LEFT JOIN dbo.LOAI_CONG_VIEC LCV ON LCV.ID_LCV = A.ID_LCV "
                                                                + " WHERE A.ID_CN = " + Commons.Modules.iCongNhan));
                Bindingdata(true);
            }
            catch (Exception ex)
            {
                
            }
        }
        private void grvCongTac_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }

        //=========Tung sua ======

        private void DON_VILookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(XI_NGHIEPLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(DON_VILookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
        }

        private void XI_NGHIEPLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                if (!windowsUIButton.Buttons[6].Properties.Visible) return;
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookUpEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(DON_VILookUpEdit.EditValue), Convert.ToInt32(XI_NGHIEPLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, true);
                switch (Commons.Modules.KyHieuDV)
                {
                    case "DM":
                        {
                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(XI_NGHIEPLookUpEdit.EditValue)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);
                            break;
                        }
                    default:
                        {
                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, -1), "ID_LCV", "TEN_LCV", "TEN_LCV", true);
                            break;
                        }
                }
            }
            catch { }
        }

        private void XI_NGHIEP_CUTextEdit_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void ID_LCV_CULookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            ID_CV_CULookUpEdit.Properties.ReadOnly = false;
            Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_CV_CULookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCV_CULookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", "", false);
            ID_CV_CULookUpEdit.ItemIndex = 0;
            ID_CV_CULookUpEdit.Properties.ReadOnly = true;
        }

        private void ID_LCVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            ID_CVLookUpEdit.Properties.ReadOnly = false;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCVLookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", false);
            ID_CVLookUpEdit.ItemIndex = 0;
            ID_CVLookUpEdit.Properties.ReadOnly = true;
        }
        private void grdCongTac_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }

        #endregion

        #region function load
        public void LoadgrdCongTac(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCongTac", Commons.Modules.iCongNhan, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_QTCT"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongTac, grvCongTac, dt, false, true, true, true, true, this.Name);

            //Hide column
            grvCongTac.Columns["ID_QTCT"].Visible = false;
            grvCongTac.Columns["ID_CN"].Visible = false;
            grvCongTac.Columns["ID_LQD"].Visible = false;
            grvCongTac.Columns["ID_NK"].Visible = false;
            grvCongTac.Columns["ID_CV"].Visible = false;
            grvCongTac.Columns["ID_LCV"].Visible = false;
            grvCongTac.Columns["ID_TO_CU"].Visible = false;
            grvCongTac.Columns["ID_TO"].Visible = false;
            grvCongTac.Columns["ID_CV_CU"].Visible = false;
            grvCongTac.Columns["ID_LCV_CU"].Visible = false;
            grvCongTac.Columns["MUC_LUONG"].Visible = false;
            grvCongTac.Columns["MUC_LUONG_CU"].Visible = false;
            grvCongTac.Columns["TEN_XN"].Visible = false;
            grvCongTac.Columns["TEN_TO"].Visible = false;
            grvCongTac.Columns["ID_XN"].Visible = false;
            grvCongTac.Columns["ID_DV"].Visible = false;
            grvCongTac.Columns["GHI_CHU"].Visible = false;
            grvCongTac.Columns["NGAY_KY"].Visible = false;
            grvCongTac.Columns["TEN_CN"].Visible = false;
            grvCongTac.Columns["ID_CTL_CU"].Visible = false;
            grvCongTac.Columns["ID_CTL"].Visible = false;
            grvCongTac.Columns["ID_TT"].Visible = false;
            grvCongTac.Columns["TAI_LIEU"].Visible = false;



            //format column
            grvCongTac.Columns["NGAY_HIEU_LUC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvCongTac.Columns["NGAY_HIEU_LUC"].DisplayFormat.FormatString = "dd/MM/yyyy";

            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvCongTac.FocusedRowHandle = grvCongTac.GetRowHandle(index);
            }

        }

        #endregion

        #region funciton dùm chung
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
            grdCongTac.Enabled = visible;
            //SO_HIEU_BANGTextEdit.Properties.ReadOnly = visible;
            SO_QUYET_DINHTextEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Properties.ReadOnly = visible;
            NGAY_HIEU_LUCDateEdit.Properties.ReadOnly = visible;
            ID_LQDLookUpEdit.Properties.ReadOnly = visible;


            ID_NKLookUpEdit.Properties.ReadOnly = visible;
            ID_CV_CULookUpEdit.Properties.ReadOnly = visible;
            DON_VI_CUTextEdit.Properties.ReadOnly = true;
            XI_NGHIEP_CUTextEdit.Properties.ReadOnly = true;
            ID_TO_CUTextEdit.Properties.ReadOnly = true;
            NOI_CONG_TACTextEdit.Properties.ReadOnly = visible;
            NHIEM_VUMemoEdit.Properties.ReadOnly = visible;
            //MUC_LUONG_CUTextEdit.Properties.ReadOnly = visible;
            ID_LCV_CULookUpEdit.Properties.ReadOnly = visible;
            ID_CTL_CULookUpEdit.Properties.ReadOnly = visible;

            DON_VILookUpEdit.Properties.ReadOnly = visible;
            XI_NGHIEPLookUpEdit.Properties.ReadOnly = visible;
            ID_TOLookUpEdit.Properties.ReadOnly = visible;
            ID_CVLookUpEdit.Properties.ReadOnly = true;
            ID_LCVLookUpEdit.Properties.ReadOnly = visible;
            //MUC_LUONGTextEdit.Properties.ReadOnly = visible;
            GHI_CHUTextEdit.Properties.ReadOnly = visible;
            ID_CTLLookUpEdit.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;

        }
        private void Bindingdata(bool bthem)
        {

            //Commons.Modules.sLoad = "0Load";
            if (bthem == true)
            {
                Loaddatatable();
                //lấy dữ liệu mặc định theo id công nhân
                try
                {
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    NGAY_HIEU_LUCDateEdit.EditValue = DateTime.Today;
                    SO_QUYET_DINHTextEdit.EditValue = "";

                    if (grvCongTac.RowCount > 0)
                    {
                        int index = SearchMaxNgayHieuLuc();
                        ID_NKLookUpEdit.EditValue = tableTTC_CN.Rows[index]["ID_NK"];
                        idToCu = Convert.ToInt32(tableTTC_CN.Rows[index]["ID_TO"]);
                        //MUC_LUONG_CUTextEdit.EditValue = tableTTC_CN.Rows[0]["MUC_LUONG_CU"];
                        DON_VI_CUTextEdit.EditValue = tableTTC_CN.Rows[index]["TEN_DV"];
                        XI_NGHIEP_CUTextEdit.EditValue = tableTTC_CN.Rows[index]["TEN_XN"];
                        ID_TO_CUTextEdit.EditValue = tableTTC_CN.Rows[index]["TEN_TO"];
                        ID_CV_CULookUpEdit.EditValue = tableTTC_CN.Rows[index]["ID_CV"];
                        ID_LCV_CULookUpEdit.EditValue = tableTTC_CN.Rows[index]["ID_LCV"];
                        ID_CTL_CULookUpEdit.EditValue = Convert.ToInt64(tableTTC_CN.Rows[index]["ID_CTL"]);
                        NHIEM_VUMemoEdit.EditValue = tableTTC_CN.Rows[index]["NHIEM_VU"];
                        NOI_CONG_TACTextEdit.EditValue = tableTTC_CN.Rows[index]["NOI_CONG_TAC"];
                    }
                    else
                    {
                        ID_NKLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_NK"];
                        idToCu = Convert.ToInt32(tableLyLich.Rows[0]["ID_TO"]);
                        //MUC_LUONG_CUTextEdit.EditValue = tableTTC_CN.Rows[0]["MUC_LUONG_CU"];
                        DON_VI_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_DV"];
                        XI_NGHIEP_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_XN"];
                        ID_TO_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_TO"];
                        ID_CV_CULookUpEdit.EditValue = tableLyLich.Rows[0]["ID_CV"];
                        ID_LCV_CULookUpEdit.EditValue = tableLyLich.Rows[0]["ID_LCV"];
                        ID_CTL_CULookUpEdit.EditValue = Convert.ToInt64(tableLyLich.Rows[0]["ID_CTL"]);
                        NHIEM_VUMemoEdit.EditValue = tableLyLich.Rows[0]["NHIEM_VU"];
                        NOI_CONG_TACTextEdit.EditValue = tableLyLich.Rows[0]["NOI_CONG_TAC"];
                    }

                    //MUC_LUONGTextEdit.EditValue = 0;
                    GHI_CHUTextEdit.EditValue = "";
                    ID_LQDLookUpEdit.EditValue = null;

                    ID_CNTextEdit.EditValue = tableTTC_CN.Rows[0]["HO_TEN"];
                    ID_CVLookUpEdit.EditValue = null;
                    ID_LCVLookUpEdit.EditValue = null;
                    ID_CTLLookUpEdit.EditValue = null;

                    cboTinhTrang.EditValue = 1;
                    txtTaiLieu.ResetText();
                    DON_VILookUpEdit.EditValue = null;
                    XI_NGHIEPLookUpEdit.EditValue = null;
                    ID_TOLookUpEdit.EditValue = null;
                    try
                    {
                        txtMoTaCVCu.EditValue = tableTTC_CN.Rows[0]["MO_TA_CV_CU"];
                        txtMoTaCVMoi.EditValue = tableTTC_CN.Rows[0]["MO_TA_CV_MOI"];
                    }
                    catch { }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (grvCongTac.RowCount > 0 && bthem == false) //load du lieu co trong luoi
            {
                try
                {
                    //error: Convert.ToDateTime
                    NGAY_KYDateEdit.EditValue = Convert.ToDateTime(grvCongTac.GetFocusedRowCellValue("NGAY_KY")) == DateTime.MinValue ? NGAY_KYDateEdit.EditValue = null : Convert.ToDateTime(grvCongTac.GetFocusedRowCellValue("NGAY_KY"));
                    NGAY_HIEU_LUCDateEdit.EditValue = Convert.ToDateTime(grvCongTac.GetFocusedRowCellValue("NGAY_HIEU_LUC")) == DateTime.MinValue ? NGAY_HIEU_LUCDateEdit.EditValue = null : Convert.ToDateTime(grvCongTac.GetFocusedRowCellValue("NGAY_HIEU_LUC"));

                    SO_QUYET_DINHTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("SO_QUYET_DINH");
                    DON_VI_CUTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("TEN_DV");
                    XI_NGHIEP_CUTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("TEN_XN");
                    ID_TO_CUTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("TEN_TO");
                    idToCu = (grvCongTac.GetFocusedRowCellValue("ID_TO_CU") == null || grvCongTac.GetFocusedRowCellValue("ID_TO_CU").ToString().Trim() == "") ? -1 : Int32.Parse(grvCongTac.GetFocusedRowCellValue("ID_TO_CU").ToString().Trim());
                    //MUC_LUONG_CUTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("MUC_LUONG_CU");
                    NOI_CONG_TACTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("NOI_CONG_TAC");
                    //MUC_LUONGTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("MUC_LUONG");
                    GHI_CHUTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("GHI_CHU");
                    NHIEM_VUMemoEdit.EditValue = grvCongTac.GetFocusedRowCellValue("NHIEM_VU");
                    ID_CNTextEdit.EditValue = grvCongTac.GetFocusedRowCellValue("TEN_CN");

                    ID_LQDLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_LQD");
                    ID_NKLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_NK");
                    ID_CV_CULookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CV_CU");
                    ID_LCV_CULookUpEdit.EditValue = (grvCongTac.GetFocusedRowCellValue("ID_LCV_CU") == null || grvCongTac.GetFocusedRowCellValue("ID_LCV_CU").ToString().Trim() == "") ? null : grvCongTac.GetFocusedRowCellValue("ID_LCV_CU");
                    ID_CTL_CULookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CTL_CU").ToString() == "" ? -99 : Convert.ToInt64(grvCongTac.GetFocusedRowCellValue("ID_CTL_CU"));

                    DON_VILookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_DV");
                    XI_NGHIEPLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_XN");
                    ID_TOLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_TO");
                    ID_CVLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CV");
                    try
                    {
                        ID_LCVLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_LCV");
                    }
                    catch { }
                    ID_CTLLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CTL").ToString() == "" ? -99 : Convert.ToInt64(grvCongTac.GetFocusedRowCellValue("ID_CTL"));

                    //error: string.IsNullOrEmpt();
                    //cboTinhTrang.EditValue = string.IsNullOrEmpty(grvCongTac.GetFocusedRowCellValue("ID_TT").ToString()) ? cboTinhTrang.EditValue = null : Convert.ToInt32(grvCongTac.GetFocusedRowCellValue("ID_TT"));
                    cboTinhTrang.EditValue = (grvCongTac.GetFocusedRowCellValue("ID_TT") == null || grvCongTac.GetFocusedRowCellValue("ID_TT").ToString().Trim() == "") ? cboTinhTrang.EditValue = null : Convert.ToInt32(grvCongTac.GetFocusedRowCellValue("ID_TT"));

                    txtTaiLieu.EditValue = grvCongTac.GetFocusedRowCellValue("TAI_LIEU");
                    try
                    {
                        txtMoTaCVCu.EditValue = grvCongTac.GetFocusedRowCellValue("MO_TA_CV_CU");
                        txtMoTaCVMoi.EditValue = grvCongTac.GetFocusedRowCellValue("MO_TA_CV_MOI");
                    }
                    catch { }
                }
                catch (Exception ex)
                {

                }

            }
            if (grvCongTac.RowCount == 0 && bthem == false)
            {
                try
                {
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    ID_NKLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_NK"];
                    NGAY_HIEU_LUCDateEdit.EditValue = DateTime.Today;
                    SO_QUYET_DINHTextEdit.EditValue = "";

                    idToCu = Convert.ToInt32(tableLyLich.Rows[0]["ID_TO"]);
                    //MUC_LUONG_CUTextEdit.EditValue = tableTTC_CN.Rows[0]["MUC_LUONG_CU"];
                    DON_VI_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_DV"];
                    XI_NGHIEP_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_XN"];
                    ID_TO_CUTextEdit.EditValue = tableLyLich.Rows[0]["TEN_TO"];
                    ID_CV_CULookUpEdit.EditValue = tableLyLich.Rows[0]["ID_CV"];
                    ID_LCV_CULookUpEdit.EditValue = tableLyLich.Rows[0]["ID_LCV"];
                    ID_CTL_CULookUpEdit.EditValue = tableLyLich.Rows[0]["ID_CTL"];
                    NHIEM_VUMemoEdit.EditValue = tableLyLich.Rows[0]["NHIEM_VU"];
                    NOI_CONG_TACTextEdit.EditValue = tableLyLich.Rows[0]["NOI_CONG_TAC"];

                    //MUC_LUONGTextEdit.EditValue = 0;
                    GHI_CHUTextEdit.EditValue = "";
                    ID_LQDLookUpEdit.EditValue = null;

                    ID_CNTextEdit.EditValue = tableTTC_CN.Rows[0]["HO_TEN"];
                    ID_CVLookUpEdit.EditValue = null;
                    ID_LCVLookUpEdit.EditValue = null;
                    ID_CTLLookUpEdit.EditValue = null;

                    cboTinhTrang.EditValue = 1;
                    txtTaiLieu.ResetText();
                    DON_VILookUpEdit.EditValue = null;
                    XI_NGHIEPLookUpEdit.EditValue = null;
                    ID_TOLookUpEdit.EditValue = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            Commons.Modules.sLoad = "";
        }

        private void Load_cboChucVu()
        {
            try
            {

                DON_VILookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_DV");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(XI_NGHIEPLookUpEdit, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(DON_VILookUpEdit.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                XI_NGHIEPLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_XN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOLookUpEdit, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(DON_VILookUpEdit.EditValue), Convert.ToInt32(XI_NGHIEPLookUpEdit.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO", true, true);
                ID_TOLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_TO");
                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_CV_CULookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCV_CULookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", "", true);
                ID_CV_CULookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CV_CU");
                switch (Commons.Modules.KyHieuDV)
                {
                    case "DM":
                        {
                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(XI_NGHIEPLookUpEdit.EditValue)), "ID_LCV", "TEN_LCV", "TEN_LCV", true);
                            ID_LCVLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_LCV");
                            break;
                        }
                    default:
                        {
                            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LCVLookUpEdit, Commons.Modules.ObjSystems.DataLoaiCV(false, -1), "ID_LCV", "TEN_LCV", "TEN_LCV", true);
                            ID_LCVLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_LCV");
                            break;
                        }

                }

                Commons.Modules.ObjSystems.MLoadLookUpEditN(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(ID_LCVLookUpEdit.EditValue)), "ID_CV", "TEN_CV", "TEN_CV", "", true);
                ID_CVLookUpEdit.EditValue = grvCongTac.GetFocusedRowCellValue("ID_CV");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveData()
        {
            try
            {
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateQuaTrinhCongTac",
                idQTCT, Commons.Modules.iCongNhan,
                ID_LQDLookUpEdit.EditValue,
                ID_NKLookUpEdit.EditValue,
                ID_TOLookUpEdit.EditValue,
                ID_CVLookUpEdit.EditValue,
                ID_LCVLookUpEdit.EditValue,
                idToCu,
                ID_CV_CULookUpEdit.EditValue,
                ID_LCV_CULookUpEdit.EditValue,
                ID_CTLLookUpEdit.EditValue,
                ID_CTL_CULookUpEdit.EditValue,
                SO_QUYET_DINHTextEdit.EditValue,
                NGAY_KYDateEdit.EditValue,
                NGAY_HIEU_LUCDateEdit.EditValue,
                NOI_CONG_TACTextEdit.EditValue,
                NHIEM_VUMemoEdit.EditValue,
                //MUC_LUONGTextEdit.EditValue,
                //MUC_LUONG_CUTextEdit.EditValue,
                GHI_CHUTextEdit.EditValue, cboTinhTrang.EditValue, txtTaiLieu.EditValue,
                cothem,
                Convert.ToInt32(cboTinhTrang.EditValue)
                ));
                LoadgrdCongTac(Convert.ToInt32(Commons.Modules.sId));
            }
            //nếu là ngày hiệu lực là ngày mới nhất thì cập nhật lại bảng công nhân về tổ và chức vụ
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteQuaTrinhCongTac"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.QUA_TRINH_CONG_TAC WHERE ID_QTCT =" + grvCongTac.GetFocusedRowCellValue("ID_QTCT") + "");
                Loaddatatable();
                LoadgrdCongTac(-1);
                Bindingdata(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        private void LockChuyenTu()
        {
            ID_LCV_CULookUpEdit.Properties.ReadOnly = true;
            ID_CV_CULookUpEdit.Properties.ReadOnly = true;
            //MUC_LUONG_CUTextEdit.Properties.ReadOnly = true;

            ID_CTL_CULookUpEdit.Properties.ReadOnly = true;
            NOI_CONG_TACTextEdit.Properties.ReadOnly = true;
            NHIEM_VUMemoEdit.Properties.ReadOnly = true;
        }

        private bool KiemTraTatCaDaKy()
        {
            try
            {
                int count = 0;
                count = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(ID_QTCT) FROM dbo.QUA_TRINH_CONG_TAC WHERE ID_CN =  " + Commons.Modules.iCongNhan + " AND ID_TT = 1 GROUP BY ID_CN"));
                if (count > 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChuyenTatCaSangDaKy()
        {
            try
            {
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateQTCTtudong", Commons.Modules.iCongNhan);
                Loaddatatable();
                LoadgrdCongTac(-1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool KiemTraNgayHieuLuc(bool isAdd)
        {
            try
            {
                if (NGAY_HIEU_LUCDateEdit.EditValue == null) return false;
                DateTime DayHL = System.Convert.ToDateTime(NGAY_HIEU_LUCDateEdit.EditValue.ToString());
                string SqlStr = " SELECT COUNT(QTCT.ID_CN) FROM QUA_TRINH_CONG_TAC QTCT WHERE QTCT.NGAY_HIEU_LUC = '" + DayHL.ToString("MM/dd/yyyy") + "' AND QTCT.ID_CN = " + Commons.Modules.iCongNhan + " GROUP BY QTCT.ID_CN";
                int count = 0;
                count = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, SqlStr));

                if (isAdd)
                {

                    if (count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (count > 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int SearchMaxNgayHieuLuc()
        {
            try
            {
                DataTable newtb = Commons.Modules.ObjSystems.ConvertDatatable(grdCongTac);
                DateTime MaxDay = Convert.ToDateTime(newtb.Rows[0]["NGAY_HIEU_LUC"].ToString());
                int index = 0;
                int i = 0;
                foreach (DataRow row in newtb.Rows)
                {
                    i++;
                    DateTime NewDay = Convert.ToDateTime(row["NGAY_HIEU_LUC"].ToString());
                    int compare = DateTime.Compare(MaxDay, NewDay);
                    if (compare < 0)
                    {
                        index = i;
                        MaxDay = NewDay;
                    }

                }
                return index;
            }
            catch
            {
                return 0;
            }
        }
        private void windowsUIButton_ButtonClick_1(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (grvCongTac.RowCount == 0)
                        {
                            Bindingdata(true);
                            cothem = true;
                            enableButon(false);
                            LockChuyenTu();
                        }
                        else
                        {
                            bool DaKy = KiemTraTatCaDaKy();
                            if (DaKy)
                            {
                                Bindingdata(true);
                                cothem = true;
                                enableButon(false);
                                LockChuyenTu();
                            }
                            else
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgConHopDongChuaKyKhongDuocThemMoi"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                ChuyenTatCaSangDaKy();
                                Bindingdata(true);
                                cothem = true;
                                enableButon(false);
                                LockChuyenTu();
                            }
                        }

                        btnName = "them";
                        break;
                    }
                case "sua":
                    {
                        if ((Convert.ToInt32(cboTinhTrang.EditValue) == 2 && Commons.Modules.UserName != "admin") || (Convert.ToInt32(cboTinhTrang.EditValue) == 3 && Commons.Modules.UserName != "admin"))
                        {
                            XtraMessageBox.Show(cboTinhTrang.Text + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangKhongSuaDuoc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (grvCongTac.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Load_cboChucVu();
                        cothem = false;
                        enableButon(false);
                        LockChuyenTu();
                        btnName = "sua";
                        break;
                    }

                case "xoa":
                    {
                        if ((Convert.ToInt32(cboTinhTrang.EditValue) == 2 && Commons.Modules.UserName != "admin") || (Convert.ToInt32(cboTinhTrang.EditValue) == 3 && Commons.Modules.UserName != "admin"))
                        {
                            XtraMessageBox.Show(cboTinhTrang.Text + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangKhongSuaDuoc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (grvCongTac.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                        DeleteData();
                        break;
                    }
                case "In":
                    {

                        if (grvCongTac.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"));
                            return;
                        }
                        idQTCT = Convert.ToInt32(grvCongTac.GetFocusedRowCellValue("ID_QTCT"));
                        frmInQTCT InQTCT = new frmInQTCT(Commons.Modules.iCongNhan, idQTCT, ID_CNTextEdit.EditValue.ToString());
                        InQTCT.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;

                        if (Convert.ToInt32(DON_VILookUpEdit.EditValue) < 0)
                        {

                            XtraMessageBox.Show(ItemForDON_VI.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"));
                            DON_VILookUpEdit.Focus();
                            return;
                        }

                        if (Convert.ToInt32(XI_NGHIEPLookUpEdit.EditValue) < 0)
                        {
                            XtraMessageBox.Show(ItemForXI_NGHIEP.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"));
                            XI_NGHIEPLookUpEdit.Focus();
                            return;
                        }

                        if (Convert.ToInt32(ID_TOLookUpEdit.EditValue) < 0)
                        {
                            XtraMessageBox.Show(ItemForID_TO.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"));
                            ID_TOLookUpEdit.Focus();
                            return;
                        }
                        if (btnName == "them")
                        {
                            bool NgayHieuLucHopLe = KiemTraNgayHieuLuc(true);
                            if (!NgayHieuLucHopLe)
                            {
                                XtraMessageBox.Show(NGAY_HIEU_LUCDateEdit.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrungNgayHieuLuc"));
                                NGAY_HIEU_LUCDateEdit.Focus();
                                return;
                            }
                        }
                        else
                        {
                            bool NgayHieuLucHopLe = KiemTraNgayHieuLuc(false);
                            if (!NgayHieuLucHopLe)
                            {
                                XtraMessageBox.Show(NGAY_HIEU_LUCDateEdit.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrungNgayHieuLuc"));
                                NGAY_HIEU_LUCDateEdit.Focus();
                                return;
                            }
                        }

                        //kiem trung ..
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        if (cothem == true)
                        {
                            idQTCT = -1;
                        }
                        else
                        {
                            idQTCT = Convert.ToInt32(grvCongTac.GetFocusedRowCellValue("ID_QTCT"));
                        }
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungQuaTrinhCongTac", conn);
                        cmd.Parameters.Add("@ID_QTCT", SqlDbType.BigInt).Value = idQTCT;
                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                        cmd.Parameters.Add("@SO_QUYET_DINH", SqlDbType.NVarChar).Value = SO_QUYET_DINHTextEdit.Text;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                        {

                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoQD_NayDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SO_QUYET_DINHTextEdit.Focus();
                            return;
                        }
                        conn.Close();
                        SaveData();
                        Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtTaiLieu.Text);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {

                        enableButon(true);
                        Bindingdata(false);
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
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
        private void LayDuongDan()
        {
            string strPath_DH = txtTaiLieu.Text;
            strDuongDan = ofdfile.FileName;

            var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_CT");
            string[] sFile;
            string TenFile;

            TenFile = ofdfile.SafeFileName.ToString();
            sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

            if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
                txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
            else
            {
                TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
            }
        }

        private void txtTaiLieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                try
                {

                    if (Convert.ToInt32(cboTinhTrang.EditValue) == 2)
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonThemTL"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo) != DialogResult.No)
                        {
                            if ((cboTinhTrang.EditValue == null || cboTinhTrang.EditValue.ToString().Trim() != "2"))
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangHopDong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                if (ofdfile.ShowDialog() == DialogResult.Cancel) return;
                                LayDuongDan();
                                cboTinhTrang.EditValue = 2;
                            }
                            else
                            {
                                ofdfile.ShowDialog();
                                LayDuongDan();
                            }
                        }
                        else
                        {
                            if ((cboTinhTrang.EditValue == null || cboTinhTrang.EditValue.ToString().Trim() != "2"))
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangHopDong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                                if (ofdfile.ShowDialog() == DialogResult.Cancel) return;
                                LayDuongDan();
                                cboTinhTrang.EditValue = 2;
                            }
                            else
                            {
                                ofdfile.ShowDialog();
                                LayDuongDan();
                            }
                        }
                    }
                    else
                    {
                        if ((cboTinhTrang.EditValue == null || cboTinhTrang.EditValue.ToString().Trim() != "2"))
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangHopDong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            if (ofdfile.ShowDialog() == DialogResult.Cancel) return;
                            LayDuongDan();
                            cboTinhTrang.EditValue = 2;
                        }
                        else
                        {
                            ofdfile.ShowDialog();
                            LayDuongDan();
                        }
                    }
                }
                catch { }
            }
            else
            {
                //xóa dữ liệu
                try
                {
                    Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                    txtTaiLieu.ResetText();
                    grvCongTac.SetFocusedRowCellValue("TAI_LIEU", null);
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.QUA_TRINH_CONG_TAC SET TAI_LIEU = NULL WHERE ID_QTCT =" + grvCongTac.GetFocusedRowCellValue("ID_QTCT") + "");
                }
                catch
                {
                }
            }
            ////////    if (e.Button.Index == 0)
            ////////    {
            ////////        try
            ////////        {
            ////////            if (windowsUIButton.Buttons[6].Properties.Visible)
            ////////            {
            ////////                ofdfile.ShowDialog();
            ////////                LayDuongDan();
            ////////            }
            ////////            else
            ////////            {
            ////////                if (txtTaiLieu.Text == "")
            ////////                    return;
            ////////                Commons.Modules.ObjSystems.OpenHinh(txtTaiLieu.Text);
            ////////            }
            ////////        }
            ////////        catch
            ////////        {
            ////////        }
            ////////    }
            ////////    else
            ////////    {
            ////////        //xóa dữ liệu
            ////////        try
            ////////        {
            ////////            Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
            ////////            txtTaiLieu.ResetText();
            ////////            grvCongTac.SetFocusedRowCellValue("TAI_LIEU", null);
            ////////            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.QUA_TRINH_CONG_TAC SET TAI_LIEU = NULL WHERE ID_QTCT =" + grvCongTac.GetFocusedRowCellValue("ID_QTCT") + "");
            ////////        }
            ////////        catch
            ////////        {
            ////////        }
            ////////    }    
        }

        private void ID_CVLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                //ID_CTLookUpEdit.EditValue = "";
                Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CTLLookUpEdit, Commons.Modules.ObjSystems.DataCTL(false), "ID_CTL", "TEN_CTL", "TEN_CTL", true);
                if (ID_CVLookUpEdit.EditValue != null && int.Parse(ID_CVLookUpEdit.EditValue.ToString()) == 206)
                {
                    ID_CTLLookUpEdit.EditValue = 2;
                }
                else
                {
                    ID_CTLLookUpEdit.EditValue = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ID_TOLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
