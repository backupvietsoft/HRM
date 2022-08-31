using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucDanhGiaThuViec : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        private int iAdd = 0;
        public ucDanhGiaThuViec()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);
        }

        private void ucDanhGiaThuViec_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiDanhGia, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);
            //Commons.OSystems.SetDateEditFormat(datThang);
            //datThang.EditValue = DateTime.Now;
            LoadThang();
            Commons.Modules.sLoad = "";
            LoadData();
            LoadNDDG();
            grvDSCongNhan_FocusedRowChanged(null, null);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
        }
        private void LoadData()
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboID_PV.EditValue)));
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListCNDGThuViec", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = iAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_CO_THE_DI_LAM"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_NHAN_VIEC"].ReadOnly = false;
                //dt.Columns["ID_DGTN"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DTDH"].ReadOnly = false;
                //dt.Columns["DA_GIOI_THIEU"].ReadOnly = false;
                //dt.Columns["HUY_TUYEN_DUNG"].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCongNhan, grvDSCongNhan, dt, false, true, false, true, true, this.Name);
                grvDSCongNhan.Columns["CHON"].Visible = false;
                grvDSCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvDSCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                if (iAdd == 0)
                {
                    grvDSCongNhan.Columns["ID_CN"].Visible = false;
                    grvDSCongNhan.Columns["ID_HDLD"].Visible = false;
                    grvDSCongNhan.OptionsSelection.MultiSelect = false;
                    grvDSCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }
                if (iAdd == 2)
                {
                    grvDSCongNhan.Columns["DA_KY"].Visible = true;
                    grvDSCongNhan.Columns["ID_CN"].Visible = false;
                    grvDSCongNhan.Columns["ID_HDLD"].Visible = false;
                    grvDSCongNhan.OptionsSelection.MultiSelect = true;
                    grvDSCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }
                if (iAdd == 1)
                {
                    grvDSCongNhan.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSCongNhan.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSCongNhan.Columns["ID_CN"].Visible = false;
                    grvDSCongNhan.Columns["ID_HDLD"].Visible = false;
                    grvDSCongNhan.Columns["DA_KY"].Visible = false;
                    grvDSCongNhan.OptionsSelection.MultiSelect = true;
                    grvDSCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_LHDLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_LHDLD.NullText = "";
                    cboID_LHDLD.ValueMember = "ID_LHDLD";
                    cboID_LHDLD.DisplayMember = "TEN_LHDLD";
                    //ID_VTTD,TEN_VTTD
                    cboID_LHDLD.DataSource = Commons.Modules.ObjSystems.DataLoaiHDLD(false);
                    cboID_LHDLD.Columns.Clear();
                    cboID_LHDLD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_LHDLD"));
                    cboID_LHDLD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LHDLD"));
                    cboID_LHDLD.Columns["TEN_LHDLD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LHDLD");
                    cboID_LHDLD.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_LHDLD.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_LHDLD.Columns["ID_LHDLD"].Visible = false;
                    grvDSCongNhan.Columns["ID_LHDLD"].ColumnEdit = cboID_LHDLD;
                    cboID_LHDLD.BeforePopup += cboID_LHDLD_BeforePopup;
                    cboID_LHDLD.EditValueChanged += cboID_LHDLD_EditValueChanged;


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_NK = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_NK.NullText = "";
                    cboID_NK.ValueMember = "ID_NK";
                    cboID_NK.DisplayMember = "HO_TEN";
                    //ID_VTTD,TEN_VTTD
                    cboID_NK.DataSource = Commons.Modules.ObjSystems.DataNguoiKy();
                    cboID_NK.Columns.Clear();
                    cboID_NK.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NK"));
                    cboID_NK.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                    cboID_NK.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");
                    cboID_NK.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_NK.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_NK.Columns["ID_NK"].Visible = false;
                    grvDSCongNhan.Columns["ID_NK"].ColumnEdit = cboID_NK;
                    cboID_NK.BeforePopup += cboID_NK_BeforePopup;
                    cboID_NK.EditValueChanged += cboID_NK_EditValueChanged;


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_CV = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_CV.NullText = "";
                    cboID_CV.ValueMember = "ID_CV";
                    cboID_CV.DisplayMember = "TEN_CV";
                    //ID_VTTD,TEN_VTTD
                    cboID_CV.DataSource = Commons.Modules.ObjSystems.DataChucVu(false);
                    cboID_CV.Columns.Clear();
                    cboID_CV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CV"));
                    cboID_CV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_CV"));
                    cboID_CV.Columns["TEN_CV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CV");
                    cboID_CV.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_CV.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_CV.Columns["ID_CV"].Visible = false;
                    grvDSCongNhan.Columns["ID_CV"].ColumnEdit = cboID_CV;
                    cboID_CV.BeforePopup += cboID_CV_BeforePopup;
                    cboID_CV.EditValueChanged += cboID_CV_EditValueChanged;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_NL = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_NL.NullText = "";
                    cboID_NL.ValueMember = "ID_NL";
                    cboID_NL.DisplayMember = "MS_NL";
                    //ID_VTTD,TEN_VTTD
                    cboID_NL.DataSource = Commons.Modules.ObjSystems.DataNgachLuong(false);
                    cboID_NL.Columns.Clear();
                    cboID_NL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NL"));
                    cboID_NL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_NL"));
                    cboID_NL.Columns["MS_NL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_NL");
                    cboID_NL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_NL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_NL.Columns["ID_NL"].Visible = false;
                    grvDSCongNhan.Columns["ID_NL"].ColumnEdit = cboID_NL;
                    cboID_NL.BeforePopup += cboID_NL_BeforePopup;
                    cboID_NL.EditValueChanged += cboID_NL_EditValueChanged;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BL = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_BL.NullText = "";
                    cboID_BL.ValueMember = "ID_BL";
                    cboID_BL.DisplayMember = "TEN_BL";
                    //ID_VTTD,TEN_VTTD
                    cboID_BL.DataSource = Commons.Modules.ObjSystems.DataBacLuong(-1, Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text), false);
                    cboID_BL.Columns.Clear();
                    cboID_BL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_BL"));
                    cboID_BL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_BL"));
                    cboID_BL.Columns["TEN_BL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_BL");
                    cboID_BL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_BL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_BL.Columns["ID_BL"].Visible = false;
                    grvDSCongNhan.Columns["ID_BL"].ColumnEdit = cboID_BL;
                    cboID_BL.BeforePopup += cboID_BL_BeforePopup;
                    cboID_BL.EditValueChanged += cboID_BL_EditValueChanged;

                }
                try
                {
                    grvDSCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvDSCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_DGTN", "TEN_DGTN", "TEN_DGTN", grvDSUngVien, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), true, "ID_DGTN", this.Name, true);

                //ID_YCTD,MA_YCTD


            }
            catch (Exception ex) { }
        }
        private void LoadNDDG()
        {
            string sBTDGTV_CT = "sBTDGTV_CT" + Commons.Modules.iIDUser;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTDGTV_CT, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan), "");
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListDanhGiaTV_CT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTDGTV_CT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["XUAT_SAC"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNDDanhGia, grvNDDanhGia, dt, false, true, false, true, true, this.Name);
                grvNDDanhGia.Columns["ID_CN"].Visible = false;
                grvNDDanhGia.Columns["TEN_NDDG_TV"].OptionsColumn.AllowEdit = false;
                Commons.Modules.ObjSystems.XoaTable(sBTDGTV_CT);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTDGTV_CT);
            }
        }

        #region editComboLuoi
        // cboID_LHDLD
        private void cboID_LHDLD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_LHDLD", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_LHDLD_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataLoaiHDLD(false);
            }
            catch { }
        }
        // cboID_NK
        private void cboID_NK_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_NK", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NK_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataNguoiKy();
            }
            catch { }
        }
        // cboID_CV
        private void cboID_CV_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_CV", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_CV_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataChucVu(false);
            }
            catch { }
        }
        // cboID_NL
        private void cboID_NL_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_NL", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NL_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataNgachLuong(false);
            }
            catch { }
        }
        // cboID_BL
        private void cboID_BL_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_BL", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_BL_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue("ID_NL")), Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text), false);
            }
            catch { }
        }
        #endregion

        private DataTable TinhSoTuanCuaTHang(DateTime TN, DateTime DN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));

                //kiểm tra ngày bắc đầu có phải thứ 2 không

                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        if (TN.DayOfWeek == DayOfWeek.Monday)
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7));
                            TN = TN.AddDays(8);
                            continue;
                        }
                        else
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7 + (7 - (int)TN.DayOfWeek)));
                            TN = TN.AddDays(8 + (7 - (int)TN.DayOfWeek));
                            continue;
                        }
                    }
                    if (i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i == 4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }

        private bool SaveData(int Add)
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTDSCongNhan" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNDDG" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdNDDanhGia), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDanhGiaThuViec", "sBTDSCongNhan" + Commons.Modules.iIDUser, "sBTNDDG" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text), cboNguoiDanhGia.Text == "" ? cboNguoiDanhGia.EditValue = null : Convert.ToInt64(cboNguoiDanhGia.EditValue) ,Add);
                Commons.Modules.ObjSystems.XoaTable("sBTDSCongNhan" + Commons.Modules.iIDUser);
                Commons.Modules.ObjSystems.XoaTable("sBTNDDG" + Commons.Modules.iIDUser);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable("sBTDSCongNhan" + Commons.Modules.iIDUser);
                Commons.Modules.ObjSystems.XoaTable("sBTNDDG" + Commons.Modules.iIDUser);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;

            //grvNguonTuyen.OptionsBehavior.Editable = !visible;
            grvDSCongNhan.OptionsBehavior.Editable = !visible;
            grvNDDanhGia.OptionsBehavior.Editable = !visible;
            datThang.Properties.ReadOnly = !visible;
        }


        private void grvDSCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadLuoiND();
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //Commons.Modules.ObjSystems.RowFilter(grdTuan, grvTuan.Columns["ID_YCTD"], grvTuan.Columns["ID_VTTD"], grvVTYC.GetFocusedRowCellValue("ID_YCTD").ToString(), grvVTYC.GetFocusedRowCellValue("ID_VTTD").ToString());
                //if (btnALL.Buttons[0].Properties.Visible == false) return;
                Commons.Modules.ObjSystems.RowFilter(grdNDDanhGia, grvNDDanhGia.Columns["ID_CN"], grvDSCongNhan.GetFocusedRowCellValue("ID_CN") == null ? "-1" : grvDSCongNhan.GetFocusedRowCellValue("ID_CN").ToString());

                //String sID = grvDSCongNhan.GetFocusedRowCellValue("ID_CN") == null ? "-1" : grvDSCongNhan.GetFocusedRowCellValue("ID_CN").ToString();
                //DataTable dt = new DataTable();
                //dt = (DataTable)grdNDDanhGia.DataSource;
                //if (dt == null) return;
                //try
                //{
                //    if (sID == "-1")
                //    {
                //        dt.DefaultView.RowFilter = "1=0";
                //    }
                //    else
                //    {
                //        dt.DefaultView.RowFilter = "ID_CN = " + sID + " OR ID_CN is null";
                //    }
                //}
                //catch
                //{
                //    dt.DefaultView.RowFilter = "1 = 0";
                //}

            }
            catch (Exception ex)
            {
            }
        }

        private void datThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            LoadNDDG();
            grvDSCongNhan_FocusedRowChanged(null, null);
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        iAdd = 1;
                        int n = grvDSCongNhan.FocusedRowHandle;
                        LoadData();
                        LoadNDDG();
                        grvDSCongNhan.FocusedRowHandle = n;
                        grvDSCongNhan.SelectRow(n);
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        iAdd = 2;
                        int n = grvDSCongNhan.FocusedRowHandle;
                        LoadData();
                        LoadNDDG();
                        grvDSCongNhan.FocusedRowHandle = n;
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        enableButon(false);
                        //Commons.Modules.ObjSystems.AddnewRow(grvNDDanhGia, false);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }
                case "in":
                    {

                        DataTable dt = new DataTable();
                        DataTable dtbc = new DataTable();
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTDSCongNhan" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan), "");
                            System.Data.SqlClient.SqlConnection conn1;
                            dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptHopDongLaoDong_AllDM(DateTime.Now);

                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn1.Open();

                            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_AllDM", conn1);
                            cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "sBTDSCongNhan" + Commons.Modules.iIDUser;
                            cmd1.CommandType = CommandType.StoredProcedure;

                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);

                            dtbc = new DataTable();
                            dtbc = ds.Tables[1].Copy();
                            dtbc.TableName = "NOI_DUNG";
                            frm.AddDataSource(dtbc);

                            Commons.Modules.ObjSystems.XoaTable("sBTDSCongNhan" + Commons.Modules.iIDUser);
                            frm.ShowDialog();
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.XoaTable("sBTDSCongNhan" + Commons.Modules.iIDUser);
                        }
                        break;
                    }

                case "luu":
                    {
                        if (grvNDDanhGia.HasColumnErrors) return;
                        int n = grvDSCongNhan.FocusedRowHandle;
                        if (grvDSCongNhan.RowCount == 0) return;
                        DataTable dt_CHON = new DataTable();
                        dt_CHON = ((DataTable)grdDSCongNhan.DataSource);
                        if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (flag == true) return;
                        if (!SaveData(iAdd)) return;
                        iAdd = 0;
                        LoadData();
                        LoadNDDG();
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        grvDSCongNhan.FocusedRowHandle = n;
                        grvDSCongNhan.SelectRow(n);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        iAdd = 0;
                        int n = grvDSCongNhan.FocusedRowHandle;
                        LoadData();
                        LoadNDDG();
                        grvDSCongNhan.FocusedRowHandle = n;
                        grvDSCongNhan.SelectRow(n);
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        enableButon(true);
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

        private void grvNguonTuyen_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                //if (grvDSCongNhan.RowCount == 0)
                //{
                //    grvNguonTuyen.DeleteSelectedRows();
                //    return;
                //}
                //grvNguonTuyen.SetFocusedRowCellValue("ID_YCTD", grvDSCongNhan.GetFocusedRowCellValue("ID_YCTD"));
                //grvNguonTuyen.SetFocusedRowCellValue("ID_VTTD", grvDSCongNhan.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
            }
        }

        private void searchControl1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grvDSCongNhan_FocusedRowChanged(null, null);
        }

        private void grvNguonTuyen_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //grvNguonTuyen.ClearColumnErrors();
            //try
            //{
            //    DataTable dt = new DataTable();
            //    if (grvNguonTuyen == null) return;
            //    if (grvNguonTuyen.FocusedColumn.FieldName == "ID_NTD")
            //    {//kiểm tra máy không được để trống
            //        if (string.IsNullOrEmpty(e.Value.ToString()))
            //        {
            //            e.Valid = false;
            //            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
            //            grvNguonTuyen.SetColumnError(grvNguonTuyen.Columns["ID_NTD"], e.ErrorText);
            //            return;
            //        }
            //        else
            //        {
            //            grvNguonTuyen.SetFocusedRowCellValue("ID_NTD", e.Value);
            //            dt = new DataTable();
            //            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdNguonTuyen);
            //            if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_NTD").Equals(e.Value)) > 1)
            //            {
            //                e.Valid = false;
            //                e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
            //                grvNguonTuyen.SetColumnError(grvNguonTuyen.Columns["ID_NTD"], e.ErrorText);
            //                return;
            //            }
            //        }
            //    }
            //}
            //catch  (Exception ex)
            //{ }
        }

        private void grdNguonTuyen_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            //{
            //    grvNguonTuyen.DeleteSelectedRows();
            //}
        }
        private void grdVTYC_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
            }
        }

        private void grvNguonTuyen_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            LoadNDDG();
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            LoadNDDG();
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            LoadNDDG();
            Commons.Modules.sLoad = "";
        }

        private void grvDSCongNhan_RowCountChanged(object sender, EventArgs e)
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

        private void grvNDDanhGia_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                //if (grvNDDanhGia.RowCount == 0)
                //{
                //    grvNDDanhGia.DeleteSelectedRows();
                //    return;
                //}
                //grvNDDanhGia.SetFocusedRowCellValue("CHON", 1);
                //grvNDDanhGia.SetFocusedRowCellValue("ID_VTTD", grvNDDanhGia.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
            }
        }

        private void grvNDDanhGia_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    if (e.Column.FieldName == "XUAT_SAC")
            //    {
            //        view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], 1);
            //    }
            //    //if(e.Column.FieldName != "CHON")
            //    //{
            //    //    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], 0);
            //    //}
            //}
            //catch (Exception ex) { }
        }

        private void grvNDDanhGia_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                bool XS = Convert.ToBoolean(e.Value);
                bool TOT = Convert.ToBoolean(e.Value);
                bool TB = Convert.ToBoolean(e.Value);
                bool KEM = Convert.ToBoolean(e.Value);
                GridView view = sender as GridView;
                if (e.Column.FieldName == "XUAT_SAC")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], e.Value);
                    view.SetRowCellValue(e.RowHandle, view.Columns["XUAT_SAC"], XS);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TOT"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TRUNG_BINH"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["KEM"], false);
                }

                if (e.Column.FieldName == "TOT")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], e.Value);
                    view.SetRowCellValue(e.RowHandle, view.Columns["XUAT_SAC"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TOT"], TOT);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TRUNG_BINH"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["KEM"], false);
                }
                if (e.Column.FieldName == "TRUNG_BINH")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], e.Value);
                    view.SetRowCellValue(e.RowHandle, view.Columns["XUAT_SAC"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TOT"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TRUNG_BINH"], TB);
                    view.SetRowCellValue(e.RowHandle, view.Columns["KEM"], false);
                }
                if (e.Column.FieldName == "KEM")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], e.Value);
                    view.SetRowCellValue(e.RowHandle, view.Columns["XUAT_SAC"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TOT"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TRUNG_BINH"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["KEM"], KEM);
                }
            }
            catch (Exception ex) { }
        }

        private void grvDSCongNhan_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            GridView view = sender as GridView;
            if (e.Column.FieldName == "KY_HOP_DONG")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["KY_HOP_DONG"], e.Value);
                view.SetRowCellValue(e.RowHandle, view.Columns["KT_HOP_DONG"], false);
            }
            if (e.Column.FieldName == "KT_HOP_DONG")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["KY_HOP_DONG"], false);
                view.SetRowCellValue(e.RowHandle, view.Columns["DA_KY"], false);
                view.SetRowCellValue(e.RowHandle, view.Columns["KT_HOP_DONG"], e.Value);
            }
            if (e.Column.FieldName == "DA_KY")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["DA_KY"], e.Value);
                view.SetRowCellValue(e.RowHandle, view.Columns["KY_HOP_DONG"], e.Value);
                view.SetRowCellValue(e.RowHandle, view.Columns["KT_HOP_DONG"], false);
            }
        }

        private void grvDSCongNhan_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn SO_HDLD = View.Columns["SO_HDLD"];
                DevExpress.XtraGrid.Columns.GridColumn ID_CV = View.Columns["ID_CV"];
                //DevExpress.XtraGrid.Columns.GridColumn MS_CN = View.Columns["MS_CN"];
                //DevExpress.XtraGrid.Columns.GridColumn MS_THE_CC = View.Columns["MS_THE_CC"];
                //DevExpress.XtraGrid.Columns.GridColumn ngayvaolam = View.Columns["NGAY_VAO_LAM_LAI"];
                //if (View.GetRowCellValue(e.RowHandle, mslydovang).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(mslydovang, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage)); return;
                //}
                if (View.GetRowCellValue(e.RowHandle, SO_HDLD).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoHDLDKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.SetColumnError(SO_HDLD, "Số hợp đồng lao động không được trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ID_CV).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(ID_CV, "Chức vụ không được trống"); return;
                }
                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }

        private void grvDSCongNhan_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSCongNhan_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY_DANH_GIA,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY_DANH_GIA,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY_DANH_GIA,103),10) AS NGAY ,RIGHT(CONVERT(VARCHAR(10),NGAY_DANH_GIA,103),7) AS THANG  FROM dbo.DANH_GIA_THU_VIEC ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                    grvThang.Columns["THANG"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                datThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch
            {
                DateTime now = DateTime.Now;
                datThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                datThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + datThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                datThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
            }
            datThang.ClosePopup();
        }
        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                datThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            datThang.ClosePopup();

        }
    }
}
