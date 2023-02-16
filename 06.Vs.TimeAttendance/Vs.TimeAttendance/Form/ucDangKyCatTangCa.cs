using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using Vs.Report;
using System.Globalization;
using DevExpress.DataAccess.Excel;
using System.Collections;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Drawing;
using DevExpress.Utils.Menu;

namespace Vs.TimeAttendance
{
    public partial class ucDangKyCatTangCa : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucDangKyCatTangCa _instance;
        public static ucDangKyCatTangCa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKyCatTangCa();
                return _instance;
            }
        }
        CultureInfo cultures = new CultureInfo("en-US");

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucDangKyCatTangCa()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }
        private void ucDangKyCatTangCa_Load(object sender, EventArgs e)
        {
            isAdd = false;
            Commons.Modules.sLoad = "0Load";
            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

            repositoryItemTimeEdit1.NullText = "00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

            EnableButon();
            LoadNgay();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            //DataTable dtNCC = new DataTable();
            //dtNCC.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NCC, dtNCC, "ID_NHOM", "TEN_NHOM", "TEN_NHOM");

            LoadGridCongNhan();
            Commons.Modules.sLoad = "";

        }
        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {


                LookUpEdit lookUp = sender as LookUpEdit;

                //string id = lookUp.get;

                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;



                //dGioBatDau = new DateTime();
                //dGioKetThuc = new DateTime();
                //dGioBatDau = Convert.ToDateTime(dataRow.Row["GIO_BD"]);
                //dGioKetThuc = Convert.ToDateTime(dataRow.Row["GIO_KT"]);
                //iPhutBatDau = Convert.ToInt32(dataRow.Row["PHUT_BD"]);
                //iPhutKetThuc = Convert.ToInt32(dataRow.Row["PHUT_KT"]);
            }
            catch { }
        }
        private void cboID_NHOM_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvCongNhan.SetFocusedRowCellValue("ID_NHOM", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NHOM_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                lookUp.Properties.DataSource = dID_NHOM;
            }
            catch { }
        }
        private void LoadGridCongNhan()
        {

            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_DangKyTangCa", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"), cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, isAdd , 1));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, true, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["SO_GIO"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["NGAY"].OptionsColumn.AllowEdit = false;
                if (isAdd)
                {
                    grvCongNhan.Columns["SO_GIO"].OptionsColumn.AllowEdit = true;
                    grvCongNhan.OptionsSelection.MultiSelect = true;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    dt.Columns["SO_GIO"].ReadOnly = false;
                    dt.Columns["SG_TC"].ReadOnly = true;
                }
                else
                {
                    grvCongNhan.OptionsSelection.MultiSelect = false;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }

                try
                {
                    grvCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }
        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calNgay.DateTime.Date.ToShortDateString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            cboNgay.ClosePopup();
        }
        private void FormatGridCongNhan()
        {
            grvCongNhan.Columns["ID_CN"].Visible = false;
        }
        #region Combobox Changed
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }
         private void LoadNgay()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_DangKyTangCa", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"), cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, isAdd, 2));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);

            if (dt.Rows.Count > 0)
            {
                cboNgay.EditValue = dt.Rows[0]["NGAY"];
            }
            else
            {
                cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();

            Commons.Modules.sLoad = "";
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }
        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }
        #endregion
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
  
                    case "themsua":
                        {
                            isAdd = true;
                            EnableButon();
                            LoadGridCongNhan();
                            break;
                        }
                    case "xoa":
                        {
                            LoadGridCongNhan();
                            break;
                        }
                    case "ghi":
                        {

                            if (!Validate()) return;
                            if (grvCongNhan.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            EnableButon();
                            LoadGridCongNhan();
                            LoadNgay();
                            break;
                        }
                    case "khongghi":
                        {
                            isAdd = false;
                            EnableButon();
                            LoadGridCongNhan();
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
      
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }
       ////chuot phai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }
            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (btnALL.Buttons[2].Properties.Visible || btnALL.Buttons[0].Properties.Visible) return;
                    if (grvCongNhan.FocusedColumn.FieldName.ToString() == "MS_CN" || grvCongNhan.FocusedColumn.FieldName.ToString() == "HO_TEN") return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
            }
            catch
            {
            }
        }

        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }

        public void CapNhatAll(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvCongNhan.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName)));
                    grdCongNhan.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
        }
        private bool Savedata()
        {
            DataTable dt = new DataTable();
            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
            string dt_CongNhan = "grvCongNhan" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                grvCongNhan.PostEditor();
                grvCongNhan.UpdateCurrentRow();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, dt_CongNhan, (DataTable)grdCongNhan.DataSource, "");
                DateTime dNgay;
                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDangKyCatTangCa", dNgay, dt_CongNhan);
                Commons.Modules.ObjSystems.XoaTable(dt_CongNhan);
                return true;
            }
            catch(Exception ex) { }
            return false;
            
        }
        private void EnableButon()
        {

            try
            {
                btnALL.Buttons[0].Properties.Visible = !isAdd;
                btnALL.Buttons[1].Properties.Visible = !isAdd;
                btnALL.Buttons[2].Properties.Visible = !isAdd;
                btnALL.Buttons[3].Properties.Visible = isAdd;
                btnALL.Buttons[4].Properties.Visible = isAdd;
                btnALL.Buttons[5].Properties.Visible = isAdd;

                cboNgay.Enabled = !isAdd;
                cboDonVi.Enabled = !isAdd;
                cboXiNghiep.Enabled = !isAdd;
                cboTo.Enabled = !isAdd;
            }
            catch { }
        }
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void grvCongNhan_RowCountChanged(object sender, EventArgs e)
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
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            return true;
        }
       

    }
}