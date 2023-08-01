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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Reflection;

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

            try
            {
                Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, dt, "ID_DV", "TEN_DV", "TEN_DV");
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);

                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo, Commons.Modules.KyHieuDV =="TG" ? true :false);
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void ucTinhLuong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                if (Commons.Modules.KyHieuDV != "DM")
                {
                    switch (Commons.Modules.KyHieuDV)
                    {
                        case "TG":
                            {
                                LoadGrdGTGC_TG();
                                break;
                            }
                        case "MT":
                            {
                                LoadGrdGTGC_MT();
                                break;
                            }
                        default:
                            {
                                LoadGrdGTGC_BT();
                                break;
                            }
                    }
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

                    lblNgayBuLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lblThuongDoanhThu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }


                txtNgayCongChuan.EditValue = Commons.Modules.KyHieuDV == "DM" ? getNgayCongChuan() : 26;
                txtNgayCongLV.Text = getNgayCongChuan().ToString();
                txtNgayBuLuong.EditValue = 0;
                Commons.Modules.sLoad = "";
                EnableButon();
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
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
        private void LoadGrdGTGC_BT()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong_BT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay));
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

                    for (int i = 0; i < grvData.Columns.Count; i++)
                    {
                        if (grvData.Columns[i].FieldName.ToString().Substring(0, grvData.Columns[i].FieldName.ToString().IndexOf("_")) == "LUONG" || grvData.Columns[i].FieldName.ToString().Substring(0, grvData.Columns[i].FieldName.ToString().IndexOf("_")) == "PC" || grvData.Columns[i].FieldName.ToString().Substring(0, grvData.Columns[i].FieldName.ToString().IndexOf("_")) == "THUONG" || grvData.Columns[i].FieldName.ToString().Substring(0, grvData.Columns[i].FieldName.ToString().IndexOf("_")) == "TIEN" || grvData.Columns[i].FieldName.ToString().Substring(0, grvData.Columns[i].FieldName.ToString().IndexOf("_")) == "LSP")
                        {
                            grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                            grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N0";
                        }
                    }

                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatString = "N0";

                    grvData.Columns["TONG_BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_BU_LUONG"].DisplayFormat.FormatString = "N0";


                    grvData.Columns["TONG_LSP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TONG_LSP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRU_THUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRU_THUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRUY_THU_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRUY_THU_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRU_KHAC"].DisplayFormat.FormatString = "N0";


                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
        }
        private void LoadGrdGTGC_MT()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong_MT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay));
                dt.Columns["THANH_TOAN_KHAC"].ReadOnly = false;
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["ID_TO"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["CACH_TL"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    for (int i = 7; i < grvData.Columns.Count - 1; i++)
                    {
                        grvData.Columns[i].OptionsColumn.AllowEdit = false;
                        switch (grvData.Columns[i].FieldName)
                        {
                            case "NGAY_CONG":
                            case "GIO_CONG":
                            case "NGAY_PHEP":
                            case "NGAY_LE":
                            case "TC_THUONG":
                            case "TC_DEM":
                            case "TC_CN":
                            case "NGHI_VIEC_CL":
                            case "CD_NU":
                            case "DIEM":
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N1";
                                    }
                                    catch
                                    {
                                    }

                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N0";
                                    }
                                    catch
                                    {
                                    }

                                }
                                break;
                        }
                    }

                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;

                    grvData.Columns["THANH_TOAN_KHAC"].OptionsColumn.AllowEdit = true;
                   

                    grvData.OptionsSelection.MultiSelect = true;
                    grvData.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
        }

        private void LoadGrdGTGC_TG()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong_TG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay));
                dt.Columns["TINH_BH"].ReadOnly = false;
                dt.Columns["TINH_LUONG"].ReadOnly = false;
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["ID_TO"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["CACH_TL"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    for (int i = 7; i < grvData.Columns.Count - 1; i++)
                    {
                        grvData.Columns[i].OptionsColumn.AllowEdit = false;
                        switch (grvData.Columns[i].FieldName)
                        {
                            case "DL":
                            case "CN":
                            case "VM":
                            case "TD":
                            case "GLT":
                            case "LUONG_PHEP_NGAY":
                            case "NGAY_LE":
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N1";
                                    }
                                    catch
                                    {
                                    }

                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N0";
                                    }
                                    catch
                                    {
                                    }

                                }
                                break;
                        }
                    }

                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;

                    grvData.Columns["LUONG_NGAY_CONG"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["GLT"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["TIEN_THEM_GIO"].OptionsColumn.AllowEdit = true;

                    grvData.Columns["THUONG_HIEU_SUAT"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["THUONG_HIEU_SUAT"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["THUONG_HIEU_SUAT"].OptionsColumn.AllowEdit = true;

                    grvData.Columns["THUONG_HIEU_SUAT"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["THUONG_C_HANH"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["THUONG_HTNV"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["TRO_CAP_CN"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["TIEN_XANG"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["THUONG"].OptionsColumn.AllowEdit = true;

                    grvData.Columns["KHAU_TRU_TAM_UNG"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["KHAU_TRU"].OptionsColumn.AllowEdit = true;
                    grvData.Columns["TINH_BH"].OptionsColumn.AllowEdit = true;

                    grvData.OptionsSelection.MultiSelect = true;
                    grvData.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
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
                switch (Commons.Modules.KyHieuDV)
                {
                    case "DM":
                        {
                            sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG_DM WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                            break;
                        }
                    case "MT":
                        {
                            sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG_MT WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                            break;
                        }
                    case "TG":
                        {
                            sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG_TG WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                            break;
                        }
                    default:
                        {
                            sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG_BT WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                            break;

                        }
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
                case "export":
                    {
                        Export();
                        break;
                    }
                case "import":
                    {
                        if (cboThang.Text == "")
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonThang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        frmImportTinhLuong_TG frm = new frmImportTinhLuong_TG();
                        frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                        frm.iID_XN = Convert.ToInt32(cboXiNghiep.EditValue);
                        frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                        frm.dtThang = Convert.ToDateTime(cboThang.EditValue);
                        frm.dtDThang = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                        double iW, iH;
                        iW = Screen.PrimaryScreen.WorkingArea.Width / 1.5;
                        iH = Screen.PrimaryScreen.WorkingArea.Height / 1.5;
                        frm.Size = new Size((int)iW, (int)iH);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                            switch (Commons.Modules.KyHieuDV)
                            {
                                case "TG":
                                    {
                                        LoadGrdGTGC_TG();
                                        break;
                                    }
                                case "MT":
                                    {
                                        LoadGrdGTGC_MT();
                                        break;
                                    }
                                default:
                                    {
                                        LoadGrdGTGC_BT();
                                        break;
                                    }
                            }
                            TinhLuong();
                        }
                        break;
                    }
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
                        if (Commons.Modules.KyHieuDV == "DM")
                        {
                            frmNhapDLThangTLNV frm = new frmNhapDLThangTLNV();
                            frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                            frm.iID_XN = Convert.ToInt32(cboXiNghiep.EditValue);
                            frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                            frm.iLoai = iLoaiTL;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {

                            }
                        }
                        else
                        {
                            frmNhapHoTroLuong frm = new frmNhapHoTroLuong();
                            frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                            frm.iID_XN = Convert.ToInt32(cboXiNghiep.EditValue);
                            frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                            frm.iLoai = iLoaiTL;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {

                            }
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
                        TinhLuong();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void TinhLuong()
        {
            try
            {

                if (grvData.RowCount != 0)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_DaCoLuong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                }
                this.Cursor = Cursors.WaitCursor;
                //grdData.DataSource = null;
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                DataTable dt = new DataTable();
                if (iLoaiTL == 1) // tính lương công nhân
                {

                    switch (Commons.Modules.KyHieuDV)
                    {
                        case "BT":
                            {
                                System.Data.SqlClient.SqlConnection conn;
                                dt = new DataTable();
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetTinhLuongThang_BT", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXiNghiep.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                                cmd.Parameters.Add("@NgayCC", SqlDbType.Int).Value = txtNgayCongLV.EditValue;
                                cmd.Parameters.Add("@NgayCLV", SqlDbType.NVarChar).Value = txtNgayCongChuan.EditValue;
                                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Tngay;
                                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Dngay;
                                cmd.Parameters.Add("@NgayBu", SqlDbType.Float).Value = txtNgayBuLuong.EditValue;
                                cmd.Parameters.Add("@ThuongDoanhThu", SqlDbType.Bit).Value = chkThuongDoanhThu.Checked;
                                cmd.Parameters.Add("@LOAI", SqlDbType.Int).Value = 1;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                LoadGrdGTGC_BT();
                                break;
                            }
                        case "TG":
                            {
                                //cập nhật trên lướng vào bảng TEXTGIANG
                                if (grvData.RowCount > 0)
                                {
                                    dt = new DataTable();
                                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvData);
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBT" + Commons.Modules.iIDUser.ToString(), dt, "");

                                    string sSql = "UPDATE A SET A.THUONG_HIEU_SUAT = B.THUONG_HIEU_SUAT, A.THUONG_C_HANH = B.THUONG_C_HANH, A.THUONG_HTNV = B.THUONG_HTNV, A.TRO_CAP_CN = B.TRO_CAP_CN, A.TIEN_XANG = B.TIEN_XANG, A.THUONG = B.THUONG, A.KHAU_TRU_TAM_UNG =  B.KHAU_TRU_TAM_UNG, A.KHAU_TRU = B.KHAU_TRU, A.TINH_BH = B.TINH_BH,A.LUONG_NGAY_CONG = B.LUONG_NGAY_CONG,A.GLT = B.GLT,A.TIEN_THEM_GIO = B.TIEN_THEM_GIO,A.TINH_LUONG = B.TINH_LUONG FROM dbo.BANG_LUONG_TG A INNER JOIN dbo." + "sBT" + Commons.Modules.iIDUser.ToString() + " B ON B.ID_CN = A.ID_CN AND A.THANG = CONVERT(DATE,'" + Tngay.ToString("MM/dd/yyyy") + "')";
                                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                    Commons.Modules.ObjSystems.XoaTable("sBT" + Commons.Modules.iIDUser.ToString());
                                }
                                System.Data.SqlClient.SqlConnection conn;
                                dt = new DataTable();
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetTinhLuongThang_TG", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXiNghiep.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                                cmd.Parameters.Add("@NgayCC", SqlDbType.Int).Value = txtNgayCongLV.EditValue;
                                cmd.Parameters.Add("@NgayCLV", SqlDbType.NVarChar).Value = txtNgayCongChuan.EditValue;
                                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Tngay;
                                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Dngay;
                                cmd.Parameters.Add("@NgayBu", SqlDbType.Float).Value = txtNgayBuLuong.EditValue;
                                cmd.Parameters.Add("@ThuongDoanhThu", SqlDbType.Bit).Value = chkThuongDoanhThu.Checked;
                                cmd.Parameters.Add("@LOAI", SqlDbType.Int).Value = 1;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                LoadGrdGTGC_TG();
                                break;
                            }

                        case "MT":
                            {
                                //cập nhật trên lướng vào bảng TEXTGIANG
                                if (grvData.RowCount > 0)
                                {
                                    dt = new DataTable();
                                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvData);
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBT" + Commons.Modules.iIDUser.ToString(), dt, "");

                                    string sSql = "UPDATE A SET A.THANH_TOAN_KHAC = B.THANH_TOAN_KHAC FROM dbo.BANG_LUONG_MT A INNER JOIN dbo." + "sBT" + Commons.Modules.iIDUser.ToString() + " B ON B.ID_CN = A.ID_CN AND A.THANG = CONVERT(DATE,'" + Tngay.ToString("MM/dd/yyyy") + "')";
                                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                    Commons.Modules.ObjSystems.XoaTable("sBT" + Commons.Modules.iIDUser.ToString());
                                }
                                System.Data.SqlClient.SqlConnection conn;
                                dt = new DataTable();
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetTinhLuongThang_MT", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXiNghiep.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                                cmd.Parameters.Add("@NgayCC", SqlDbType.Int).Value = txtNgayCongLV.EditValue;
                                cmd.Parameters.Add("@NgayCLV", SqlDbType.NVarChar).Value = txtNgayCongChuan.EditValue;
                                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Tngay;
                                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Dngay;
                                cmd.Parameters.Add("@NgayBu", SqlDbType.Float).Value = txtNgayBuLuong.EditValue;
                                cmd.Parameters.Add("@ThuongDoanhThu", SqlDbType.Bit).Value = chkThuongDoanhThu.Checked;
                                cmd.Parameters.Add("@LOAI", SqlDbType.Int).Value = 1;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                LoadGrdGTGC_MT();
                                break;
                            }

                        case "DM":
                            {
                                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhLuongThang_DM", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToInt32(txtNgayCongLV.EditValue), Convert.ToInt32(txtNgayCongChuan.EditValue), Tngay, Dngay, iLoaiTL);
                                LoadGrdGTGC_DM();
                                break;
                            }
                        default:
                            {
                                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhLuongThang_DM", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToInt32(txtNgayCongLV.EditValue), Convert.ToInt32(txtNgayCongChuan.EditValue), Tngay, Dngay, iLoaiTL);
                                LoadGrdGTGC_DM();
                                break;
                            }
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

        }

        public string SaveFiles(string MFilter)
        {
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = MFilter;
                f.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        private void Export()
        {
            DateTime Tngay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
            DateTime Dngay = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).AddMonths(1).AddDays(-1);

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCLuong;
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportExportLuong_TG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Tngay;
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Dngay;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCLuong = new DataTable();
                dtBCLuong = ds.Tables[0].Copy();

                string SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);


                Microsoft.Office.Interop.Excel.Range row4_A = oSheet.get_Range("A1");
                row4_A.ColumnWidth = 16;
                row4_A.Value2 = "Mã nhân viên";

                Microsoft.Office.Interop.Excel.Range row4_B = oSheet.get_Range("B1");
                row4_B.ColumnWidth = 33;
                row4_B.Value2 = "Họ tên";

                Microsoft.Office.Interop.Excel.Range row4_C = oSheet.get_Range("C1");
                row4_C.ColumnWidth = 15;
                row4_C.Value2 = "Thưởng hiệu suất";

                Microsoft.Office.Interop.Excel.Range row4_D = oSheet.get_Range("D1");
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "Thưởng chấp hành";

                Microsoft.Office.Interop.Excel.Range row4_E = oSheet.get_Range("E1");
                row4_E.ColumnWidth = 15;
                row4_E.Value2 = "Thưởng HTNV";

                Microsoft.Office.Interop.Excel.Range row4_F = oSheet.get_Range("F1");
                row4_F.ColumnWidth = 15;
                row4_F.Value2 = "Trợ cấp CN";

                Microsoft.Office.Interop.Excel.Range row4_G = oSheet.get_Range("G1");
                row4_G.ColumnWidth = 15;
                row4_G.Value2 = "Tiền Xăng";

                Microsoft.Office.Interop.Excel.Range row4_H = oSheet.get_Range("H1");
                row4_H.ColumnWidth = 15;
                row4_H.Value2 = "Thưởng";

                Microsoft.Office.Interop.Excel.Range row4_I = oSheet.get_Range("I1");
                row4_I.ColumnWidth = 25;
                row4_I.Value2 = "Khấu trừ tạm ứng";

                Microsoft.Office.Interop.Excel.Range row4_J = oSheet.get_Range("J1");
                row4_J.ColumnWidth = 15;
                row4_J.Value2 = "Khấu trừ";

                Microsoft.Office.Interop.Excel.Range row4_FormatTieuDe = oSheet.get_Range("A1", "J1");
                row4_FormatTieuDe.Font.Size = fontSizeTieuDe;
                row4_FormatTieuDe.Font.Name = fontName;
                row4_FormatTieuDe.Font.Bold = true;
                row4_FormatTieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_FormatTieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                DataRow[] dr = dtBCLuong.Select();
                string[,] rowData = new string[dr.Length, dtBCLuong.Columns.Count];

                int col = 0;
                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCLuong.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 1;
                oSheet.get_Range("A2", "J" + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A2", "J" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A2", "J" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                ///

                Microsoft.Office.Interop.Excel.Range formatRange;
                for (int colFormat = 3; colFormat < dtBCLuong.Columns.Count - 1; colFormat++) // format từ cột t
                {
                    formatRange = oSheet.Range[oSheet.Cells[2, colFormat], oSheet.Cells[dtBCLuong.Rows.Count + 2, colFormat]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
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
                btnALL.Buttons[6].Properties.Visible = false;
                btnALL.Buttons[7].Properties.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = Commons.Modules.KyHieuDV == "TG" ? true : false;
                btnALL.Buttons[1].Properties.Visible = Commons.Modules.KyHieuDV == "TG" ? true : false;
                btnALL.Buttons[2].Properties.Visible = Commons.Modules.KyHieuDV == "DM" ? true : false;
                btnALL.Buttons[3].Properties.Visible = Commons.Modules.KyHieuDV == "TG" ? false : true;
                btnALL.Buttons[5].Properties.Visible = true;
                btnALL.Buttons[6].Properties.Visible = true;
                btnALL.Buttons[7].Properties.Visible = true;
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
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuXoa")); return; }
            if (Commons.Modules.ObjSystems.MsgQuestion(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong")) == 0) return;
            //xóa
            try
            {
                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "XoaTinhLuongThang", cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToDateTime(cboThang.EditValue), Commons.Modules.KyHieuDV);
                grdData.DataSource = null;

            }
            catch
            {
                Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuXoa"));
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
                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            LoadGrdGTGC_TG();
                            break;
                        }
                    case "MT":
                        {
                            LoadGrdGTGC_MT();
                            break;
                        }
                    default:
                        {
                            LoadGrdGTGC_BT();
                            break;
                        }
                }
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
                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            LoadGrdGTGC_TG();
                            break;
                        }
                    case "MT":
                        {
                            LoadGrdGTGC_MT();
                            break;
                        }
                    default:
                        {
                            LoadGrdGTGC_BT();
                            break;
                        }
                }
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
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo, true);
            if (Commons.Modules.KyHieuDV != "DM")
            {
                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            LoadGrdGTGC_TG();
                            break;
                        }
                    case "MT":
                        {
                            LoadGrdGTGC_MT();
                            break;
                        }
                    default:
                        {
                            LoadGrdGTGC_BT();
                            break;
                        }
                }
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
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo, true);
            if (Commons.Modules.KyHieuDV != "DM")
            {
                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            LoadGrdGTGC_TG();
                            break;
                        }
                    case "MT":
                        {
                            LoadGrdGTGC_MT();
                            break;
                        }
                    default:
                        {
                            LoadGrdGTGC_BT();
                            break;
                        }
                }
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
    }
}