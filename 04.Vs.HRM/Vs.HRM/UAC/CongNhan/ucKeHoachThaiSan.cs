using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System.Collections.Generic;
using System.Threading;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using System.Globalization;

namespace Vs.HRM
{
    public partial class ucKeHoachThaiSan : DevExpress.XtraEditors.XtraUserControl
    {
        private int iIDCN_Temp = -1;
        bool isEditor = false;
        bool isNewRow = false;
        public static ucKeHoachThaiSan _instance;
        public static ucKeHoachThaiSan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKeHoachThaiSan();
                return _instance;
            }
        }
        public ucKeHoachThaiSan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucKeHoachNghiPhep_Load(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan(false);
            Commons.Modules.sLoad = "";
            radTinHTrang.SelectedIndex = 1;
            Commons.OSystems.SetDateEditFormat(datTNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);
            Commons.OSystems.SetDateEditFormat(datNVao);
        }
        public void CheckDuplicateNgayThaiSan(GridView grid, DataTable GridDataTable, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

  
            DataRow row = grid.GetDataRow(e.RowHandle);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTime NgayKinhCuoiHandle = System.DateTime.Parse(row["NGAY_KINH_CUOI"].ToString(), culture);
            DateTime NgayNghiThaiSanHandle = System.DateTime.Parse(row["NGAY_NGHI_TS"].ToString(), culture);

            foreach (DataRow r in GridDataTable.Rows)
            {
                if (r.RowState != DataRowState.Deleted)
                {
                    DateTime NgayKinhCuoi = System.DateTime.Parse(r["NGAY_KINH_CUOI"].ToString(), culture);
                    DateTime NgayNghiTS = System.DateTime.Parse(r["NGAY_NGHI_TS"].ToString(), culture);
                    int State1 = -1;
                    int State2 = -1;
                    State1 = System.DateTime.Compare(NgayKinhCuoi, NgayKinhCuoiHandle);
                    State2 = System.DateTime.Compare(NgayNghiTS, NgayNghiThaiSanHandle);
                    if (grid.IsNewItemRow(grid.FocusedRowHandle))
                    {
                        continue;
                    }
                    else if(State1 == 0)
                    {
                        r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                        grid.SetColumnError(grid.Columns["NGAY_NGHI_KINH_CUOI"], "Ngày kinh cuối bị trùng, xin vui lòng kiểm tra lại.");
                        e.Valid = false;
                        return;
                    }
                    else if(State2 == 0)
                    {
                        r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                        grid.SetColumnError(grid.Columns["NGAY_NGHI_TS"], "Ngày nghỉ  thai sản bị trùng, xin vui lòng kiểm tra lại.");
                        e.Valid = false;
                        return;
                    }
                }
            }

        }
        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_DV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", cboSearch_DV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_XN, dt, "ID_XN", "TEN_XN", "TEN_XN");
                cboSearch_XN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", cboSearch_DV.EditValue, cboSearch_XN.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboSearch_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }
        private void LoadCboLDV()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0, -1));
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboLDV, dt, "ID_LDV", "TEN_LDV", "TEN_LDV");

                Commons.Modules.sPrivate = "0LOAD";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadGrdCongNhan(bool cochon)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDanhSachThaiSan", cboSearch_DV.EditValue, cboSearch_XN.EditValue, cboSearch_TO.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, radTinHTrang.EditValue));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"]};
            if (grdDSCN.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, true, true, true, this.Name);
            }
            else
            {
                grdDSCN.DataSource = dt;
            }

            grvDSCN.Columns["ID_CN"].Visible = false;
            grvDSCN.OptionsBehavior.ReadOnly = false;
            grvDSCN.OptionsCustomization.AllowColumnResizing = false;
            grvDSCN.OptionsCustomization.AllowRowSizing = false;

            if (cochon == false)
            {
                grvDSCN.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                grvDSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            }
            else
            {

                grvDSCN.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvDSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvDSCN.OptionsSelection.CheckBoxSelectorField = "CHON";
            }

            if (iIDCN_Temp != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(iIDCN_Temp));
                grvDSCN.FocusedRowHandle = grvDSCN.GetRowHandle(index);
                grvDSCN.ClearSelection();
                grvDSCN.SelectRow(index);
            }
            //grvDSCN.OptionsView.ColumnAutoWidth = true;
        }
        private void LoadGrdThaiSanCaNhan()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDanhSachThaiSanCaNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, grvDSCN.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, true, true, true, false, true, this.Name);
                //Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvNgay, Commons.Modules.ObjSystems.DataLyDoVang(false, -1), "ID_LDV", this.Name);

                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);

                grvNgay.Columns["NGAY_KINH_CUOI"].ColumnEdit = dEditN;
                grvNgay.Columns["NGAY_NGHI_TS"].ColumnEdit = dEditN;

                grvNgay.Columns["NGAY_NGHI_TS"].Caption = "Ngày nghỉ thai sản";
                grvNgay.Columns["NGAY_KINH_CUOI"].Caption = "Ngày kinh cuối";
                grvNgay.Columns["GHI_CHU"].Caption = "Ghi chú";

                grvNgay.Columns["NGAY_KINH_CUOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvNgay.Columns["NGAY_KINH_CUOI"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvNgay.Columns["NGAY_NGHI_TS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvNgay.Columns["NGAY_NGHI_TS"].DisplayFormat.FormatString = "dd/MM/yyyy";

                grvNgay.OptionsBehavior.Editable = false;

                grvNgay.Columns["ID_CN"].Visible = false;
            }
            catch
            {

            }
        }
        private void UpdateKeHoachThaiSan()
        {
            string strBangTam = "tbNghiThaiSan" + Commons.Modules.iIDUser;
            try
            {
                
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, strBangTam, Commons.Modules.ObjSystems.ConvertDatatable(grdNgay), "");
                //string sSql = "UPDATE A set A.TU_NGAY = B.TU_NGAY, A.DEN_NGAY = B.DEN_NGAY,A.NGAY_VAO_LAM_LAI = b.NGAY_VAO_LAM_LAI,SO_GIO = b.SO_GIO,a.GHI_CHU = b.GHI_CHU from dbo.KE_HOACH_NGHI_PHEP A, dbo.tabKHNP" + Commons.Modules.iIDUser + " B where B.ID_KHNP = A.ID_KHNP and A.ID_CN = " + grvDSCN.GetFocusedRowCellValue("ID_CN") + " INSERT INTO dbo.KE_HOACH_NGHI_PHEP(ID_LDV, ID_CN, TU_NGAY, DEN_NGAY, NGAY_VAO_LAM_LAI, SO_NGAY, SO_GIO, GHI_CHU) SELECT ID_LDV," + grvDSCN.GetFocusedRowCellValue("ID_CN") + ",TU_NGAY,DEN_NGAY,NGAY_VAO_LAM_LAI,NULL,SO_GIO,GHI_CHU FROM tabKHNP" + Commons.Modules.iIDUser + " WHERE ID_KHNP NOT IN(SELECT ID_KHNP FROM dbo.KE_HOACH_NGHI_PHEP WHERE ID_CN = " + grvDSCN.GetFocusedRowCellValue("ID_CN") + ")";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateTheoDoiThaiSan", strBangTam, Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.XoaTable(strBangTam);
                LoadGrdCongNhan(false);
                LoadGrdThaiSanCaNhan();
                grvNgay.RefreshData();
                ThongBao.ShowNotification(ThongBao.Notifications[0]);
            }
            catch(Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(strBangTam);
            }
        }

        private void grvDSCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadGrdThaiSanCaNhan();
        }
        private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan(false);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan(false);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan(false);
            Commons.Modules.sLoad = "";
        }
        private void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = true;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            }
        }
        private void DeleteAddRow(GridView view, int count)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            int newCount = view.RowCount;
            if(newCount > count)
            {
                while(count <= newCount)
                {
                    view.DeleteRow(count);
                }
            }
            LoadGrdThaiSanCaNhan();
        }

        private void SaveNewRow()
        {
            int ID_CN = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
            string sBT = "sBTNgay" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvNgay), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateTheoDoiThaiSan", sBT, ID_CN);

            }
            catch (Exception)
            {
                throw;
            }

            Commons.Modules.ObjSystems.XoaTable(sBT);
        }

        private void DeleteCapNhatNgayNghiTS(DataRow row)
        {

            try
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTime NgayKinhCuoiHandle = System.DateTime.Parse(row["NGAY_KINH_CUOI"].ToString().Trim(), culture);
                DateTime NgayNghiThaiSanHandle = System.DateTime.Parse(row["NGAY_NGHI_TS"].ToString().Trim(), culture);
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spDeleteNgayNghiThaiSan", row["ID_CN"], NgayKinhCuoiHandle, NgayNghiThaiSanHandle, Commons.Modules.UserName, Commons.Modules.TypeLanguage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grvNgay_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvNgay.SetFocusedRowCellValue("ID_CN", Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN")));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void grvNgay_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
            e.WindowCaption = "Input Error";
            XtraMessageBox.Show(e.ErrorText, e.WindowCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }

        private void grvNgay_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            //e.WindowCaption = "Input Error";
            //XtraMessageBox.Show(e.ErrorText, e.WindowCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
        }

        private void grvNgay_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if(isEditor)
            {
                GridView view = sender as GridView;
                if (view.Columns["NGAY_KINH_CUOI"].ToString() == "" || view.Columns["NGAY_NGHI_TS"].ToString() == "" || view.Columns["NGAY_KINH_CUOI"] == null || view.Columns["NGAY_NGHI_TS"] == null)
                {
                    if (view.Columns["NGAY_KINH_CUOI"].ToString() == "")
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày kinh cuối không được bỏ trống!";
                    }
                    else
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày nghỉ thai sản không được bỏ trống!";
                    }
                    return;
                }
                else if (view.FocusedColumn == view.Columns["NGAY_KINH_CUOI"])
                {
                    DateTime? fromDate = e.Value as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGAY_NGHI_TS"]) as DateTime?;
                    if (fromDate > toDate)
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày kinh cuối phải nhỏ hơn ngày nghỉ thai sản";
                        return;
                    }
                    else
                    {
                        int Count = Commons.Modules.ObjSystems.ConvertDatatable(view).AsEnumerable().Where(x => (e.Value.ToString().Trim() == x["NGAY_KINH_CUOI"].ToString().Trim())).Count();
                        if (Count >= 1)
                        {
                            e.Valid = false;
                            e.ErrorText = "Ngày kinh cuối bị trùng";
                            return;
                        }
                    }
                }
                if (view.FocusedColumn == view.Columns["NGAY_NGHI_TS"])
                {
                    DateTime? fromDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGAY_KINH_CUOI"]) as DateTime?;
                    DateTime? toDate = e.Value as DateTime?;
                    if (fromDate > toDate)
                    {
                        e.Valid = false;
                        e.ErrorText = "Ngày nghỉ thai sản phả lớn hơn ngày kinh cuối";
                        return;
                    }
                    else
                    {
                        int Count = Commons.Modules.ObjSystems.ConvertDatatable(view).AsEnumerable().Where(x => (e.Value.ToString().Trim() == x["NGAY_NGHI_TS"].ToString().Trim())).Count();

                        if (Count >= 1)
                        {
                            e.Valid = false;
                            e.ErrorText = "Ngày nghỉ thai sản phả bị trùng";
                            return;
                        }
                    }
                }
                if(e.Valid)
                {
                    view.ClearColumnErrors();
                    return;
                }
            }
        }

        private void grvNgay_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if(isNewRow)
            {
                try
                {
                    DevExpress.XtraGrid.Views.Grid.GridView View = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                    View.ClearColumnErrors();

                    DevExpress.XtraGrid.Columns.GridColumn tungay = View.Columns["NGAY_KINH_CUOI"];
                    DevExpress.XtraGrid.Columns.GridColumn denngay = View.Columns["NGAY_NGHI_TS"];
                    DevExpress.XtraGrid.Columns.GridColumn ghichu = View.Columns["GHI_CHU"];

                    if (View.GetRowCellValue(e.RowHandle, tungay).ToString() == "" || View.GetRowCellValue(e.RowHandle, tungay) == null)
                    {

                        e.Valid = false;
                        View.SetColumnError(tungay, "Ngày kinh cuối không được bỏ trống!");
                        return;

                    }
                    else if (View.GetRowCellValue(e.RowHandle, denngay).ToString() == "" || View.GetRowCellValue(e.RowHandle, denngay) == null)
                    {

                        e.Valid = false;
                        View.SetColumnError(denngay, "Ngày nghi thai sản không được bỏ trống!");
                        return;

                    }
                    else if (View.GetRowCellValue(e.RowHandle, ghichu) == null)
                    {
                        e.Valid = true;
                        View.SetFocusedRowCellValue("GHI_CHU", Convert.ToString(View.GetFocusedRowCellValue("GHI_CHU")));
                    }

                    DataRow r = View.GetDataRow(e.RowHandle);
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTime NgayKinhCuoiHandle = System.DateTime.Parse(r["NGAY_KINH_CUOI"].ToString().Trim(), culture);
                    DateTime NgayNghiThaiSanHandle = System.DateTime.Parse(r["NGAY_NGHI_TS"].ToString().Trim(), culture);

                    int Compare = System.DateTime.Compare(NgayKinhCuoiHandle, NgayNghiThaiSanHandle);

                    if(View.FocusedColumn == View.Columns["NGAY_KINH_CUOI"])
                    {
                        if (Compare >= 0)
                        {
                            e.Valid = false;
                            View.SetColumnError(tungay, "Ngày kinh cuối không được lớn hơn ngày nghỉ thai sản!");
                            return;
                        }
                    }
                    else
                    {
                        if (View.FocusedColumn == View.Columns["NGAY_NGHI_TS"])
                        {
                            if (Compare >= 0)
                            {
                                e.Valid = false;
                                View.SetColumnError(denngay, "Ngày nghỉ thai sản không được nhỏ hơn ngày kinh cuối!");
                                return;
                            }
                        }
                    }

                    DataRow row = View.GetDataRow(e.RowHandle);
                    int Count1 = Commons.Modules.ObjSystems.ConvertDatatable(View).AsEnumerable().Where(x => (row["NGAY_KINH_CUOI"].ToString().Trim() == x["NGAY_KINH_CUOI"].ToString().Trim())).Count();
                    int Count2 = Commons.Modules.ObjSystems.ConvertDatatable(View).AsEnumerable().Where(x => (row["NGAY_NGHI_TS"].ToString().Trim() == x["NGAY_NGHI_TS"].ToString().Trim())).Count();
                    if (Count1 > 1)
                    {
                        e.Valid = false;
                        View.SetColumnError(tungay, "Ngày kinh cuối bị trùng, vui lòng kiểm tra lại!");
                        return;
                    }
                    if(Count2 > 1)
                    {
                        e.Valid = false;
                        View.SetColumnError(denngay, "Ngày nghỉ thai sản bị trùng, vui lòng kiểm tra lại!");
                        return;
                    }

                    if (e.Valid)
                    {
                        View.ClearColumnErrors();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdDSCN.DataSource;

                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TinhTrang = 1)";
                if (radTinHTrang.SelectedIndex == 2) sdkien = "(TinhTrang = 0)";
                dtTmp.DefaultView.RowFilter = sdkien;
            }
            catch
            {
                try
                {
                    dtTmp.DefaultView.RowFilter = "";
                }
                catch { }
            }
            this.LoadGrdCongNhan(false);
        }

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            windowsUIButton.Buttons[9].Properties.Visible = !visible;
            windowsUIButton.Buttons[10].Properties.Visible = !visible;
            windowsUIButton.Buttons[11].Properties.Visible = !visible;
            windowsUIButton.Buttons[12].Properties.Visible = !visible;

            grdDSCN.Enabled = visible;
            cboSearch_DV.Enabled = visible;
            cboSearch_XN.Enabled = visible;
            cboSearch_TO.Enabled = visible;
            radTinHTrang.Enabled = visible;
        }

        private void grvDSCN_RowCountChanged(object sender, EventArgs e)
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
                if (Commons.Modules.sLoad == "0Load") return;
                grvDSCN_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteCapNhatTheoDoiThaiSan(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (Convert.ToBoolean(item["CHON"]) == true)
                {
                    try
                    {
                        string sSql = "DELETE dbo.THEO_DOI_THAI_SAN WHERE ID_CN = " + Convert.ToInt64(item["ID_CN"]).ToString() ;
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            grvNgay.OptionsBehavior.Editable = false;
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;

            int count = 0;
            count = grvNgay.RowCount;

            switch (btn.Tag.ToString())
            {
                case "capnhatngay":
                    {
                        isEditor = true;
                        isNewRow = false;
                        iIDCN_Temp = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                        UpdateKeHoachThaiSan();

                        break;
                    }
                case "them":
                    {
                        isNewRow = true;
                        isEditor = false;
                        iIDCN_Temp = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                        enableButon(false);
                        windowsUIButton.Buttons[1].Properties.Visible = false;
                        windowsUIButton.Buttons[11].Properties.Visible = false;
                        windowsUIButton.Buttons[12].Properties.Visible = false;
                        AddnewRow(grvNgay, true);
                        break;
                    }
                case "xoa":
                    {
                        if(MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này?","Thông báo",MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            if (navigationFrame1.SelectedPage == navigationPage1)
                            {
                                if (grvNgay.RowCount == 0) return;
                                try
                                {
                                    System.Int32[] RowsSelected = grvNgay.GetSelectedRows();
                                    if (RowsSelected.Length == 0)
                                    {
                                        XtraMessageBox.Show("Bạn chưa chọn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < RowsSelected.Length; i++)
                                        {
                                            int RowSelected = RowsSelected[i];
                                            if (RowSelected >= 0)
                                            {
                                                DeleteCapNhatNgayNghiTS(grvNgay.GetDataRow(RowSelected));
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                DataTable dt = new DataTable();
                                dt = Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN);
                                int n = dt.AsEnumerable().Count(x => x.Field<bool>("CHON").Equals(true));
                                if (n < 1)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaConCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                                }
                                DeleteCapNhatTheoDoiThaiSan(dt);
                            }
                            grvNgay.RefreshData();
                            this.LoadGrdCongNhan(false);
                            this.LoadGrdThaiSanCaNhan();
                        }
                        break;
                    }
                case "In":
                    {
                        frmInBaoCaoThaiSan BC = new frmInBaoCaoThaiSan(DateTime.Now, Convert.ToInt32(cboSearch_DV.EditValue), Convert.ToInt32(cboSearch_TO.EditValue), Convert.ToInt32(cboSearch_XN.EditValue), radTinHTrang.SelectedIndex);
                        BC.ShowDialog();
                        break;
                    }
                case "luu":
                    {

                        try
                        {
                            grvNgay.CloseEditor();
                            grvNgay.UpdateCurrentRow();
                            grvNgay.RefreshData();
                            enableButon(true);
                            SaveNewRow();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvNgay);
                            isEditor = false;
                            isNewRow = false;
                            LoadGrdCongNhan(false);
                            LoadGrdThaiSanCaNhan();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        break;
                    }
                case "khongluu":
                    {
                        isEditor = false;
                        isNewRow = false;
                        DeleteAddRow(grvNgay, count);
                        grvNgay.RefreshData();
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

        private void grvNgay_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                var row = view.GetFocusedDataRow();

                DateTime ngay = Convert.ToDateTime(row["NGAY_KINH_CUOI"]);

                row["NGAY_NGHI_TS"] = ngay.AddMonths(9).AddDays(10);
            }
            catch (Exception ex) { }

        }
    }
}