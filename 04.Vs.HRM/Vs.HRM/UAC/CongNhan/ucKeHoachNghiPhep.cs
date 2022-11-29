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

namespace Vs.HRM
{
    public partial class ucKeHoachNghiPhep : DevExpress.XtraEditors.XtraUserControl
    {
        private int iIDCN_Temp = -1;
        public static ucKeHoachNghiPhep _instance;
        public static ucKeHoachNghiPhep Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKeHoachNghiPhep();
                return _instance;
            }
        }
        public ucKeHoachNghiPhep()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucKeHoachNghiPhep_Load(object sender, EventArgs e)
        {
            try
            {
                lblSoGio.Visible = false;
                numSoGio.Visible = false;
                lblNVao.Visible = false;
                datNVao.Visible = false;

                Thread.Sleep(100);
                dateNam.EditValue = DateTime.Now;
                enableButon(true);
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
                Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboTINH_TRANG, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), "ID_TTD", "TEN_TT_DUYET", "TEN_TT_DUYET", "");
                cboTINH_TRANG.EditValue = 2;
                LoadGrdCongNhan(false);
                Commons.Modules.sLoad = "";
                radTinHTrang.SelectedIndex = 0;
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.OSystems.SetDateEditFormat(datNVao);
            }
            catch { }
        }
        public void CheckDuplicateKHNP(GridView grid, DataTable GridDataTable, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow row = grid.GetDataRow(e.RowHandle);
            int count = 0;

            foreach (DataRow r in GridDataTable.Rows)
            {
                if (r.RowState != DataRowState.Deleted)
                {
                    if ((Convert.ToDateTime(r["TU_NGAY"].ToString()) <= Convert.ToDateTime(row["TU_NGAY"].ToString()) & Convert.ToDateTime(r["DEN_NGAY"].ToString()) >= Convert.ToDateTime(row["TU_NGAY"].ToString())) || (Convert.ToDateTime(r["TU_NGAY"].ToString()) <= Convert.ToDateTime(row["DEN_NGAY"].ToString()) & Convert.ToDateTime(r["DEN_NGAY"].ToString()) >= Convert.ToDateTime(row["DEN_NGAY"].ToString())) || (Convert.ToDateTime(r["TU_NGAY"].ToString()) >= Convert.ToDateTime(row["TU_NGAY"].ToString()) & Convert.ToDateTime(r["DEN_NGAY"].ToString()) <= Convert.ToDateTime(row["DEN_NGAY"].ToString())))
                    {
                        if (grid.IsNewItemRow(grid.FocusedRowHandle))
                        {
                            r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                            grid.SetColumnError(grid.Columns["TU_NGAY"], "Ngày nghỉ bị trùng, xin vui lòng kiểm tra lại.");
                            e.Valid = false;
                            return;
                        }
                        else
                        {
                            count++;
                            if (count == 2)
                            {
                                r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                                grid.SetColumnError(grid.Columns["TU_NGAY"], "Ngày nghỉ bị trùng, xin vui lòng kiểm tra lại.");
                                e.Valid = false;
                                return;
                            }
                        }
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
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV =="NB" ? "spGetCongNhanNghiPhep_NB" : "spGetCongNhanNghiPhep", cboSearch_DV.EditValue, cboSearch_XN.EditValue, cboSearch_TO.EditValue, dateNam.DateTime.Year, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
            if (grdDSCN.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, true, true, true, this.Name);
            }
            else
            {
                grdDSCN.DataSource = dt;
            }
            dt.Columns["CHON"].ReadOnly = false;
            grvDSCN.Columns["ID_CN"].Visible = false;
            grvDSCN.Columns["CHON"].Visible = false;
            grvDSCN.Columns["TinhTrang"].Visible = false;
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
        private void LoadGrdKHNP()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "NB" ? "spGetListKeHoachNghiPhep_NB" : "spGetListKeHoachNghiPhep", dateNam.DateTime.Year, grvDSCN.GetFocusedRowCellValue("ID_CN"), Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["NGHI_NUA_NGAY"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKHNP, grvKHNP, dt, false, true, false, false, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvKHNP, Commons.Modules.ObjSystems.DataLyDoVang(false, -1), "ID_LDV", this.Name);
                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);

                grvKHNP.Columns["NGAY_VAO_LAM_LAI"].Visible = false;

                if(Commons.Modules.KyHieuDV =="NB")
                {
                    grvKHNP.Columns["NGHI_NUA_NGAY"].Visible = false;
                }

                grvKHNP.Columns["TU_NGAY"].ColumnEdit = dEditN;
                grvKHNP.Columns["DEN_NGAY"].ColumnEdit = dEditN;
                grvKHNP.Columns["NGAY_VAO_LAM_LAI"].ColumnEdit = dEditN;



                grvKHNP.Columns["TU_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvKHNP.Columns["TU_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvKHNP.Columns["DEN_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvKHNP.Columns["DEN_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvKHNP.Columns["NGAY_VAO_LAM_LAI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvKHNP.Columns["NGAY_VAO_LAM_LAI"].DisplayFormat.FormatString = "dd/MM/yyyy";

                //dt.Columns["SO_GIO"].ReadOnly = true;
                grvKHNP.Columns["ID_KHNP"].Visible = false;
                //grvKHNP.Columns["ID_CN"].Visible = false;

                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TTD", "TEN_TT_DUYET", "TINH_TRANG_DUYET", grvKHNP, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), this.Name);
            }
            catch (Exception ex)
            {

            }
        }
        private void grvDSCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadGrdKHNP();
        }
        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdDSCN.DataSource;

                if (radTinHTrang.SelectedIndex == 0) sdkien = "(TinhTrang = 1)";
                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TinhTrang = 0)";
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
        private void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            LoadGrdKHNP();
        }

        private void UpdateKeHoachNghiPhep()
        {
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabKHNP" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvKHNP), "");
                //string sSql = "UPDATE A set A.TU_NGAY = B.TU_NGAY, A.DEN_NGAY = B.DEN_NGAY,A.NGAY_VAO_LAM_LAI = b.NGAY_VAO_LAM_LAI,SO_GIO = b.SO_GIO,a.GHI_CHU = b.GHI_CHU from dbo.KE_HOACH_NGHI_PHEP A, dbo.tabKHNP" + Commons.Modules.iIDUser + " B where B.ID_KHNP = A.ID_KHNP and A.ID_CN = " + grvDSCN.GetFocusedRowCellValue("ID_CN") + " INSERT INTO dbo.KE_HOACH_NGHI_PHEP(ID_LDV, ID_CN, TU_NGAY, DEN_NGAY, NGAY_VAO_LAM_LAI, SO_NGAY, SO_GIO, GHI_CHU) SELECT ID_LDV," + grvDSCN.GetFocusedRowCellValue("ID_CN") + ",TU_NGAY,DEN_NGAY,NGAY_VAO_LAM_LAI,NULL,SO_GIO,GHI_CHU FROM tabKHNP" + Commons.Modules.iIDUser + " WHERE ID_KHNP NOT IN(SELECT ID_KHNP FROM dbo.KE_HOACH_NGHI_PHEP WHERE ID_CN = " + grvDSCN.GetFocusedRowCellValue("ID_CN") + ")";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateKHNP", "tabKHNP" + Commons.Modules.iIDUser, Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.XoaTable("tabKHNP" + Commons.Modules.iIDUser);
                //LoadGrdKHNP();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable("tabKHNP" + Commons.Modules.iIDUser);
            }
        }
        private void LoadCapNhatPhep()
        {
            LoadGrdCongNhan(true);
            Commons.Modules.sPrivate = "0Load";
            LoadCboLDV();
            memoGhiChu.ResetText();
            datDNgay.DateTime = DateTime.Now;
            datTNgay.DateTime = DateTime.Now;
            datNVao.DateTime = datDNgay.DateTime.AddDays(1);
            numSoGio.Value = Convert.ToDecimal(Commons.Modules.iGio);
            Commons.Modules.sPrivate = "";
        }
        private bool KiemTraCapNhatPhep(DataTable dt)
        {
            bool resulst = true;
            foreach (DataRow item in dt.Rows)
            {
                if (Convert.ToBoolean(item["CHON"]) == true)
                {
                    try
                    {
                        int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text,
                            "SELECT dbo.fuKiemTraKeHoachNghiPhep(" + Convert.ToInt64(item["ID_CN"]) + ",'" + datTNgay.DateTime.ToString("MM/dd/yyyy") + "','" + datDNgay.DateTime.ToString("MM/dd/yyyy") + "')"));
                        if (n > 0)
                        {
                            resulst = false;
                        }
                    }
                    catch
                    { }
                }
            }
            return resulst;
        }
        private void InsertCapNhatPhep(DataTable dt)
        {

            foreach (DataRow item in dt.Rows)
            {
                if (Convert.ToBoolean(item["CHON"]) == true)
                {
                    try
                    {
                        int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fuKiemTraKeHoachNghiPhep(" + Convert.ToInt64(item["ID_CN"]) + ",'" + datTNgay.DateTime.ToString("MM/dd/yyyy") + "','" + datDNgay.DateTime.ToString("MM/dd/yyyy") + "')"));
                        if (n == 0)
                        {
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spInSertKeHoachNghiPhep", cboLDV.EditValue, Convert.ToInt64(item["ID_CN"]), datTNgay.EditValue, datDNgay.EditValue, datNVao.EditValue, numSoGio.Value, cboTINH_TRANG.EditValue, chkNGHI_NUA_NGAY.EditValue, memoGhiChu.EditValue);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void DeleteCapNhatPhep(DataTable dt)
        {

            foreach (DataRow item in dt.Rows)
            {
                if (Convert.ToBoolean(item["CHON"]) == true)
                {
                    try
                    {
                        string sSql = "DELETE dbo.KE_HOACH_NGHI_PHEP WHERE ID_CN = " + +Convert.ToInt64(item["ID_CN"]) + " AND ID_LDV = " + cboLDV.EditValue + " AND CONVERT(DATE,TU_NGAY) = '" + datTNgay.DateTime.ToString("MM/dd/yyyy") + "' AND CONVERT(DATE,DEN_NGAY) ='" + datDNgay.DateTime.ToString("MM/dd/yyyy") + "'";
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        iIDCN_Temp = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                        //if (Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("SPCL")) == 0) return;
                        enableButon(false);
                        windowsUIButton.Buttons[1].Properties.Visible = false;
                        windowsUIButton.Buttons[11].Properties.Visible = false;
                        windowsUIButton.Buttons[12].Properties.Visible = false;
                        Commons.Modules.ObjSystems.AddnewRow(grvKHNP, true);
                        break;
                    }
                case "capnhatphep":
                    {
                        iIDCN_Temp = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                        enableButon(false);
                        grdDSCN.Enabled = true;
                        cboSearch_DV.Enabled = true;
                        cboSearch_XN.Enabled = true;
                        cboSearch_TO.Enabled = true;
                        radTinHTrang.Enabled = true;
                        windowsUIButton.Buttons[1].Properties.Visible = true;
                        windowsUIButton.Buttons[2].Properties.Visible = true;
                        windowsUIButton.Buttons[4].Properties.Visible = true;
                        windowsUIButton.Buttons[8].Properties.Visible = false;
                        windowsUIButton.Buttons[9].Properties.Visible = false;
                        windowsUIButton.Buttons[10].Properties.Visible = false;
                        LoadCapNhatPhep();

                        navigationFrame1.SelectedPage = navigationPage2;
                        break;
                    }
                case "xoa":
                    {
                        if (iKiemTinhTrang() == 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaDuyetKhongTheXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                            return;
                        }
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCapNhatPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        if (navigationFrame1.SelectedPage == navigationPage1)
                        {
                            XoaKHNP();
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
                            DeleteCapNhatPhep(dt);
                        }
                        grvDSCN_FocusedRowChanged(null, null);
                        break;
                    }
                case "In":
                    {
                        frmInKeHoachNghiPhep InKHNP = new frmInKeHoachNghiPhep();
                        InKHNP.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        string btKHNP = "TMPPRORUN" + Commons.Modules.UserName;
                        try
                        {
                            grvKHNP.CloseEditor();
                            grvKHNP.UpdateCurrentRow();
                            int idcn = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                            DataTable dt = new DataTable();
                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvKHNP).AsEnumerable().Where(x => x["TU_NGAY"].ToString() != "").OrderBy(x => x.Field<DateTime>("TU_NGAY")).CopyToDataTable();
                            bool kt = true;
                            if (dt.Columns["ID_LDV"].ToString() == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuachonlydovang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                grdKHNP.Focus();
                                return;
                            }

                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, btKHNP, Commons.Modules.ObjSystems.ConvertDatatable(grvKHNP), "");

                            DataTable dt1 = new DataTable();
                            dt1 = (DataTable)grdKHNP.DataSource;
                            try
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spKiemTraKHNP", btKHNP, grvDSCN.GetFocusedRowCellValue("ID_CN"), Convert.ToDateTime(Convert.ToDateTime(grvKHNP.GetRowCellValue(i, "TU_NGAY").ToString()).ToShortDateString()), Convert.ToDateTime(grvKHNP.GetRowCellValue(i, "DEN_NGAY"))));
                                    if (n > 1)
                                    {
                                        kt = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                kt = false;
                            }

                            if (kt == false)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaBiTrungBanKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                grvKHNP.RefreshData();
                                enableButon(true);
                                UpdateKeHoachNghiPhep();
                                Commons.Modules.ObjSystems.DeleteAddRow(grvKHNP);
                            }

                            UpdateTinhTrangNghiPhep(Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN")));
                            LoadGrdCongNhan(false);
                            grvDSCN_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.XoaTable(btKHNP);
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable(btKHNP);
                        }
                        break;
                    }
                case "khongluu":
                    {
                        ((DataTable)grdKHNP.DataSource).Clear();
                        grvDSCN_FocusedRowChanged(null, null);
                        enableButon(true);
                        //DeleteAddRow(grvKHNP);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "capnhat":
                    {
                        DataTable dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN);
                        int n = dt.AsEnumerable().Count(x => x.Field<bool>("CHON").Equals(true));
                        if (n < 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                        if (KiemTraCapNhatPhep(dt) == false)
                        {
                            DialogResult dl = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaBiTrungNgayBanCoMuonCapNhat"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (dl == DialogResult.OK)
                            {
                                InsertCapNhatPhep(dt);
                            }
                        }
                        else
                        {
                            InsertCapNhatPhep(dt);
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgdacongnhanthanhcong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            navigationFrame1.SelectedPage = navigationPage1;
                            LoadGrdCongNhan(false);
                            enableButon(true);
                        }

                        break;
                    }
                case "trove":
                    {
                        navigationFrame1.SelectedPage = navigationPage1;
                        LoadGrdCongNhan(false);
                        enableButon(true);
                        grvDSCN_FocusedRowChanged(null, null);
                        break;
                    }
                default:
                    break;
            }
        }
        private void grvKHNP_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {

                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if (View.Columns["ID_LDV"].ToString() == "")
                {
                    return;
                }
                DevExpress.XtraGrid.Columns.GridColumn mslydovang = View.Columns["ID_LDV"];
                DevExpress.XtraGrid.Columns.GridColumn tungay = View.Columns["TU_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn denngay = View.Columns["DEN_NGAY"];
                DevExpress.XtraGrid.Columns.GridColumn ngayvaolam = View.Columns["NGAY_VAO_LAM_LAI"];
                //if (View.GetRowCellValue(e.RowHandle, mslydovang).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(mslydovang, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage)); return;
                //}
                if (View.GetRowCellValue(e.RowHandle, tungay).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(tungay, "Từ ngày không được bỏ trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, denngay).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(denngay, "Đến ngày không được bỏ trống"); return;
                }

                CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
                View.ClearColumnErrors();
            }
            catch { }

        }
        private void XoaKHNP()
        {
            //if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteUser"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            if (grvKHNP.RowCount == 0) return;
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.KE_HOACH_NGHI_PHEP WHERE ID_KHNP  = " + grvKHNP.GetFocusedRowCellValue("ID_KHNP") + "");
                grvKHNP.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        private void grdKHNP_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCapNhatPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                XoaKHNP();
            }
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPrivate == "0LOAD") return;
            double ngay = 0;
            datNVao.DateTime = datDNgay.DateTime.AddDays(TinhNgayVaoLam(datDNgay.DateTime));
            TimeSpan time = datDNgay.DateTime - datTNgay.DateTime;
            TimeSpan time1 = datNVao.DateTime - datTNgay.DateTime;
            if (time.Days < 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTuNgayKhongLonHonDenNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (time1.Days < 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTuNgayKhongLonHonNgayVaoLam"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            try
            {
                DateTime tn = datTNgay.DateTime.Date;
                if (Commons.Modules.iNNghi == 1)
                {
                    do
                    {
                        if (tn.DayOfWeek != DayOfWeek.Sunday)
                        {
                            ngay += Commons.Modules.iGio;
                        }
                        tn = tn.AddDays(1);
                    } while (datDNgay.DateTime.Date >= tn.Date);
                    numSoGio.Value = Convert.ToDecimal(ngay); return;
                }
                if (Commons.Modules.iNNghi == 0)
                {
                    do
                    {
                        ngay += Commons.Modules.iGio;
                        tn = tn.AddDays(1);
                    } while (datDNgay.DateTime.Date >= tn.Date);
                    numSoGio.Value = Convert.ToDecimal(ngay); return;
                }
                else
                {
                    do
                    {
                        if (tn.DayOfWeek != DayOfWeek.Sunday || tn.DayOfWeek != DayOfWeek.Saturday)
                        {
                            ngay += Commons.Modules.iGio;
                        }
                        tn = tn.AddDays(1);
                    } while (datDNgay.DateTime.Date >= tn.Date);
                    numSoGio.Value = Convert.ToDecimal(ngay); return;
                }
            }
            catch (Exception)
            {

            }
        }
        private void dateNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadGrdKHNP();
        }

        //private int TinhSoNgayNghi(DateTime TNgay, DateTime DNgay)
        //{
        //    int resulst = 0;

        //    string sSql = "";
        //    sSql = "SELECT [dbo].[fnGetSoNgayTruLeChuNhat]('" + Convert.ToDateTime(TNgay).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(DNgay).ToString("yyyyMMdd") + "')";
        //    resulst = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql))*Commons.Modules.iGio;
        //    return resulst;
        //}
        private int TinhNgayVaoLam(DateTime denngay)
        {
            int resulst = 0;
            switch (Commons.Modules.iNNghi)
            {
                case 0:
                    {
                        resulst = 1;
                        break;
                    }
                case 1:
                    {
                        if (denngay.DayOfWeek == DayOfWeek.Saturday)
                        {
                            resulst = 2;
                        }
                        else
                        {
                            resulst = 1;
                        }
                        break;
                    }
                case 2:
                    {
                        if (denngay.DayOfWeek == DayOfWeek.Saturday)
                        {
                            resulst = 3;
                        }
                        else
                        {
                            if (denngay.DayOfWeek == DayOfWeek.Saturday)
                            {
                                resulst = 2;
                            }
                            else
                            {
                                resulst = 1;
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            return resulst;
        }

        private void grvKHNP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view == null) return;

                if (e.Column.Name == "colID_LDV")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["TINH_TRANG_DUYET"], 2);
                }
                if (e.Column.Name == "colTU_NGAY")
                {
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"]) as DateTime?;


                    for (DateTime? dt = fromDate; dt.Value <= toDate; dt = dt.Value.AddDays(1))
                    {
                        if (dt.Value.DayOfWeek.ToString() == "Sunday" || dt.Value.DayOfWeek.ToString() == "Saturday")
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBanConNamTrongThu7ChuNhatBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            {
                                view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], 0);
                                view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], null);
                                view.SetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"], null);
                                return;
                            }
                            else
                            {

                                break;
                            }
                        }
                    }


                    if (fromDate != null && toDate != null)
                    {
                        double SoGio = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)) * Commons.Modules.iGio;
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], SoGio);
                    }
                }
                if (e.Column.Name == "colDEN_NGAY")
                {
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"]) as DateTime?;

                    for (DateTime? dt = fromDate; dt.Value <= toDate; dt = dt.Value.AddDays(1))
                    {
                        if (dt.Value.DayOfWeek.ToString() == "Sunday" || dt.Value.DayOfWeek.ToString() == "Saturday")
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBanConNamTrongThu7ChuNhatBanCoMuonTiepTuc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                            {
                                view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], 0);
                                view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], null);
                                view.SetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"], null);
                                return;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (fromDate != null && toDate != null)
                    {
                        double SoGio = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)) * Commons.Modules.iGio;
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], SoGio);
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])).AddDays(TinhNgayVaoLam(Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])))));
                    }
                }

            }
            catch
            {

            }
        }

        private void grvKHNP_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvKHNP.SetFocusedRowCellValue("NGHI_NUA_NGAY", false);
            }
            catch { }
        }

        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void grvKHNP_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn == view.Columns["TU_NGAY"])
            {
                DateTime? fromDate = e.Value as DateTime?;
                DateTime? toDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["DEN_NGAY"]) as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    e.ErrorText = "Từ ngày phải nhỏ hơn đến ngày";
                }
            }
            if (view.FocusedColumn == view.Columns["DEN_NGAY"])
            {
                DateTime? fromDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                DateTime? toDate = e.Value as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    e.ErrorText = "Đến ngày phải lớn hơn từ ngày";
                }
            }
        }

        private void UpdateTinhTrangNghiPhep(int ID_CN)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spCapNhatTinhTrangNghiPhep", DateTime.Now, ID_CN);
            }
            catch
            {

            }
        }

        private void grvKHNP_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvKHNP_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            grvDSCN_FocusedRowChanged(null, null);
        }

        private void grvKHNP_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;


                if (e.Column.Name == "colNGHI_NUA_NGAY")
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["NGHI_NUA_NGAY"], e.Value);
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"]) as DateTime?;
                    double SoGio = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)) * Commons.Modules.iGio;
                    if (!Convert.ToBoolean(e.Value))
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], SoGio);
                    }
                    else
                    {

                        if (fromDate == null)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"], fromDate == null ? DateTime.Now : fromDate);
                            view.SetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"], fromDate == null ? DateTime.Now : fromDate);
                            view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], fromDate == null ? DateTime.Now : fromDate);
                        }
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], (SoGio / 2));
                    }
                }
            }
            catch { }
        }
        private int iKiemTinhTrang()
        {
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(TINH_TRANG_DUYET,2) FROM dbo.KE_HOACH_NGHI_PHEP WHERE ID_KHNP =" + grvKHNP.GetFocusedRowCellValue("ID_KHNP") + ""));
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private void grvKHNP_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (iKiemTinhTrang() == 1)
            //{
            //    e.Cancel = (grvKHNP.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle);
            //}
        }
        private int kiemTrung()
        {
            string btKHNP = "TMPPRORUN" + Commons.Modules.UserName;
            try
            {
                bool kt = true;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, btKHNP, Commons.Modules.ObjSystems.ConvertDatatable(grvKHNP), "");

                DataTable dt1 = new DataTable();
                dt1 = (DataTable)grdKHNP.DataSource;
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spKiemTraKHNP", btKHNP, grvDSCN.GetFocusedRowCellValue("ID_CN"), Convert.ToDateTime(Convert.ToDateTime(grvKHNP.GetRowCellValue(i, "TU_NGAY").ToString()).ToShortDateString()), Convert.ToDateTime(grvKHNP.GetRowCellValue(i, "DEN_NGAY"))));
                        if (n > 1)
                        {
                            kt = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    kt = false;
                }

                if (kt == false)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaBiTrungBanKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }
    }
}