using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.Utils;

namespace Vs.Payroll
{
    public partial class ucTinhLuong : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucTinhLuong _instance;
        public int iLoaiTL = 1; // 1 tính lương công nhân, 2 tính lương nhân viên
        public static ucTinhLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTinhLuong();
                return _instance;
            }
        }

        public ucTinhLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            Commons.Modules.sLoad = "0Load";
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, dt, "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            Commons.Modules.sLoad = "";
        }

        private void ucTinhLuong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                if (Commons.Modules.KyHieuDV != "DM")
                {
                    LoadGrdGTGC();
                }
                else
                {
                    if (iLoaiTL == 1)
                    {
                        LoadGrdGTGC_DM();
                    }
                    else
                    {
                        LoadGrdGTGCNV_DM();
                    }
                }
                txtNgayCongChuan.Text = getNgayCongChuan().ToString();
                txtNgayCongLV.Text = getNgayCongChuan().ToString();
                Commons.Modules.sLoad = "";
                EnableButon();
            }
            catch { }
        }
        private void LoadGrdGTGC()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["LUONG_CB"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CB"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CBQL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CBQL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_SP_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_SP_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CDPS"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CDPS"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_NT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_NT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_CN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_CN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_NL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_NL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226_CN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226_CN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226_NL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226_NL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_LAM_DEM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_LAM_DEM"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_PHEP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_PHEP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_LE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_LE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_VRCL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_VRCL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CDLDN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CDLDN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CHUYEN_CAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CHUYEN_CAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_THAM_NIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_THAM_NIEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_DI_LAI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_DI_LAI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CON_NHO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CON_NHO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_NGUYET_SAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_NGUYET_SAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CONG_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CONG_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHTN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_PCD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_PCD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TAM_UNG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TAM_UNG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_TRU_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_THUE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_THUE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_PHEP_TT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_PHEP_TT"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }

            grvData.Columns["ID_CN"].Visible = false;
            //for (int i = 6; i < grvData.Columns.Count; i++)
            //{

            //    grvData.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
            //    grvData.Columns[i].DisplayFormat.FormatString = "N0";
            //}

        }
        private void LoadGrdGTGC_DM()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong_DM", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay, iLoaiTL));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["ID_TO"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_LPB"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["CACH_TL"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    grvData.Columns["LUONG_TV_NC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TV_NC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_HDLD_NC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_HDLD_NC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_NGHI_NGAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_NGHI_NGAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CHU_KY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CHU_KY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_KTSP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_KTSP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_NGHI_HL_CT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_NGHI_HL_CT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_NGHI_HL_TV"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_NGHI_HL_TV"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_PHEP_NAM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_PHEP_NAM"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LUONG_TG_HC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LUONG_TG_HC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TV_150"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TV_150"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CT_150"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CT_150"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TV_200"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TV_200"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CT_200"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CT_200"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LUONG_TC_TG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LUONG_TC_TG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LTG_HC_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LTG_HC_TC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PT_HT_LSP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PT_HT_LSP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_HO_TRO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_HO_TRO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_BQ_1G_HT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_BQ_1G_HT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_BQ_1G_KHT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_BQ_1G_KHT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_LAM_HC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_LAM_HC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_BP_PHU_CHUYEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_BP_PHU_CHUYEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_LAM_HC_TG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_LAM_HC_TG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_TC_TV_150"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_TC_TV_150"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_TC_CT_150"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_TC_CT_150"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_TC_TV_200"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_TC_TV_200"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_TC_CT_200"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_TC_CT_200"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LSP_TC_TONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LSP_TC_TONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["SS_TC_TG_SP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["SS_TC_TG_SP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_THANG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_THANG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_CC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_CC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_CN_MOI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_CN_MOI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["XEP_LOAI_HQ_SX"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["XEP_LOAI_HQ_SX"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_HQ_SX"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_HQ_SX"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_HQ_QA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_HQ_QA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_PHU_CHUYEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_PHU_CHUYEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HO_TRO_AN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HO_TRO_AN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HO_TRO_HO_SO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HO_TRO_HO_SO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HO_TRO_XANG_XE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HO_TRO_XANG_XE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["GIOI_THIEU_CN_MOI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["GIOI_THIEU_CN_MOI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["ATVSV"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["ATVSV"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_CON_NHO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_CON_NHO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_QUA_DO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_QUA_DO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_PHU_CAP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_PHU_CAP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHTN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_TIEN_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_TIEN_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PHI_CONG_DOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PHI_CONG_DOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THU_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THU_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_GIAM_TRU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_GIAM_TRU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_TRUOC_GIAM_TRU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_TRUOC_GIAM_TRU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_THUC_NHAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_THUC_NHAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_TRUOC_HO_TRO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_TRUOC_HO_TRO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_THUC_NHAN_CUOI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_THUC_NHAN_CUOI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUC_NHAN_THANG_TRUOC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUC_NHAN_THANG_TRUOC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHENH_LECH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHENH_LECH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TK_NGAN_HANG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TK_NGAN_HANG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHI_NHANH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHI_NHANH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHXH_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHXH_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHYT_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHYT_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHTN_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHTN_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHTNLD_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHTNLD_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_BH_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_BH_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["QUY_CONG_DOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["QUY_CONG_DOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_CTY_TRA"].DisplayFormat.FormatString = "N0";

                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
            //for (int i = 6; i < grvData.Columns.Count; i++)
            //{

            //    grvData.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
            //    grvData.Columns[i].DisplayFormat.FormatString = "N0";
            //}
        }
        private void LoadGrdGTGCNV_DM() // load bảng lương nhân viên
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuongNV_DM", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay, iLoaiTL));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, "ucTinhLuongNV");
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["ID_TO"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TT_HT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    grvData.Columns["LUONG_HDLD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_HDLD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HTL_TRUOC_NGAY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HTL_TRUOC_NGAY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HTL_TU_NGAY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HTL_TU_NGAY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LCB_HTL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LCB_HTL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_NGAY_LVTT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_NGAY_LVTT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_NLVR_HL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_NLVR_HL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_PHEP_NAM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_PHEP_NAM"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_LUONG_TG_HC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LUONG_TG_HC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_GIO_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_GIO_TC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_LAM_THEM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_LAM_THEM"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["ATVSV"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["ATVSV"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["HO_TRO_AN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HO_TRO_AN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_CON_NHO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_CON_NHO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_CN_MOI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_CN_MOI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_HQ_NV"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_HQ_NV"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_QUA_DO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_QUA_DO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THANH_TIEN_HTL_TRUOC_NGAY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THANH_TIEN_HTL_TRUOC_NGAY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THANH_TIEN_HTL_TU_NGAY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THANH_TIEN_HTL_TU_NGAY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUONG_HQ_QUAN_LY"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUONG_HQ_QUAN_LY"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PC_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PC_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_PHU_CAP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_PHU_CAP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_TRUOC_GIAM_TRU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_TRUOC_GIAM_TRU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_BHTN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_TIEN_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_TIEN_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TN_CHIU_THUE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TN_CHIU_THUE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_LUONG_GIAM_TRU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_LUONG_GIAM_TRU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUE_GIAM_TRU_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUE_GIAM_TRU_TC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THU_NHAP_TINH_THUE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THU_NHAP_TINH_THUE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THUE_TNCN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["PHI_CONG_DOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["PHI_CONG_DOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THU_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THU_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_GIAM_TRU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_GIAM_TRU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["THU_NHAP_TRUOC_GT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THU_NHAP_TRUOC_GT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_THUC_NHAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_THUC_NHAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHXH_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHXH_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHYT_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHYT_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BHTNLD_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BHTNLD_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TONG_BH_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_BH_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["QUY_CONG_DOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["QUY_CONG_DOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TL_CTY_TRA"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TL_CTY_TRA"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_THANG_13"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_THANG_13"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LCB"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LCB"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
            //for (int i = 6; i < grvData.Columns.Count; i++)
            //{

            //    grvData.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
            //    grvData.Columns[i].DisplayFormat.FormatString = "N0";
            //}
        }
        public void LoadThang()
        {
            try
            {
                //string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo." + Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM" ? "BANG_LUONG_DM" : "BANG_LUONG" + " ORDER BY Y DESC , M DESC";
                string sSql = "";
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                if (Commons.Modules.KyHieuDV == "DM")
                {
                    sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG_DM WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                }
                else
                {
                    sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                }
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.ToString("MM/yyyy");
            }
        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "khoitao":
                    {
                        frmNhapDLKhoiTaoTLNV frm = new frmNhapDLKhoiTaoTLNV();
                        frm.ID_DV = Convert.ToInt32(cboDonVi.EditValue);
                        frm.iLoai = iLoaiTL;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                        }
                        break;
                    }
                case "dulieuthang":
                    {
                        frmNhapDLThangTLNV frm = new frmNhapDLThangTLNV();
                        frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                        frm.iID_XN = Convert.ToInt32(cboXiNghiep.EditValue);
                        frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                        }
                        break;
                    }
                case "xoa":
                    {
                        XoaCheDoLV();
                        break;
                    }
                case "in":
                    {

                        break;
                    }

                case "tinhluong":
                    {
                        try
                        {

                            if (grvData.RowCount != 0)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_DaCoLuong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            }
                            this.Cursor = Cursors.WaitCursor;
                            grdData.DataSource = null;
                            DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                            DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                            DataTable dt = new DataTable();
                            if (iLoaiTL == 1) // tính lương công nhân
                            {
                                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "DM" ? "spGetTinhLuongThang_DM" : "spGetTinhLuongThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToInt32(txtNgayCongLV.EditValue), Convert.ToInt32(txtNgayCongChuan.EditValue), Tngay, Dngay, iLoaiTL);
                                if (Commons.Modules.KyHieuDV != "DM")
                                {
                                    LoadGrdGTGC();
                                }
                                else
                                {
                                    LoadGrdGTGC_DM();
                                }
                            }
                            else // tính lương nhân viên
                            {
                                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "DM" ? "spGetTinhLuongThangNV_DM" : "spGetTinhLuongThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToInt32(txtNgayCongLV.EditValue), Convert.ToInt32(txtNgayCongChuan.EditValue), Tngay, Dngay, iLoaiTL);
                                if (Commons.Modules.KyHieuDV != "DM")
                                {
                                    LoadGrdGTGC();
                                }
                                else
                                {
                                    LoadGrdGTGCNV_DM();
                                }
                            }

                            this.Cursor = Cursors.Default;
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhLuongThanhCong"), Commons.Form_Alert.enmType.Success);
                        }
                        catch (Exception ex)    
                        {
                            this.Cursor = Cursors.Default;
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhLuongKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                            MessageBox.Show(ex.Message);    
                        }

                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void EnableButon()
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDonVi.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)) == 2)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnALL.Buttons[2].Properties.Visible = false;
                btnALL.Buttons[3].Properties.Visible = false;
                btnALL.Buttons[4].Properties.Visible = false;
                btnALL.Buttons[5].Properties.Visible = false;
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    btnALL.Buttons[1].Properties.Visible = false;
                }
                else
                {
                    btnALL.Buttons[1].Properties.Visible = true;
                }
                btnALL.Buttons[3].Properties.Visible = true;
                btnALL.Buttons[4].Properties.Visible = true;
                btnALL.Buttons[5].Properties.Visible = true;
            }
        }

        private int getNgayCongChuan()
        {
            int ngay = 0;
            try
            {
                DateTime Tngay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                DateTime Dngay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).AddMonths(1).AddDays(-1);
                ngay = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayCongQuiDinhThang('" + Tngay.ToString("MM/dd/yyyy") + "','" + Dngay.ToString("MM/dd/yyyy") + "')"));
                return ngay;
            }
            catch { return ngay; }

        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "XoaTinhLuongThang", cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToDateTime(cboThang.EditValue), Commons.Modules.KyHieuDV);
                grdData.DataSource = null;

            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    view.SetFocusedRowCellValue("THANG", cboThang.EditValue);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            txtNgayCongChuan.Text = getNgayCongChuan().ToString();
            txtNgayCongLV.Text = getNgayCongChuan().ToString();
            if (Commons.Modules.KyHieuDV != "DM")
            {
                LoadGrdGTGC();
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    LoadGrdGTGC_DM();
                }
                else
                {
                    LoadGrdGTGCNV_DM();
                }
            }
            EnableButon();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            if (Commons.Modules.KyHieuDV != "DM")
            {
                LoadGrdGTGC();
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    LoadGrdGTGC_DM();
                }
                else
                {
                    LoadGrdGTGCNV_DM();
                }
            }
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            if (Commons.Modules.KyHieuDV != "DM")
            {
                LoadGrdGTGC();
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    LoadGrdGTGC_DM();
                }
                else
                {
                    LoadGrdGTGCNV_DM();
                }
            }
            EnableButon();
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            if (Commons.Modules.KyHieuDV != "DM")
            {
                LoadGrdGTGC();
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    LoadGrdGTGC_DM();
                }
                else
                {
                    LoadGrdGTGCNV_DM();
                }
            }
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboLoaiTinhLuong_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (Commons.Modules.KyHieuDV != "DM")
            {
                LoadGrdGTGC();
            }
            else
            {
                if (iLoaiTL == 1)
                {
                    LoadGrdGTGC_DM();
                }
                else
                {
                    LoadGrdGTGCNV_DM();
                }
            }
        }
    }
}