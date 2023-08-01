﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using System.Globalization;
using DevExpress.Utils.Menu;
using System.Linq;
using System.Drawing;

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
            try
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

                LoadNgay();
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXiNghiep, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(cboDonVi.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN");

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), true), "ID_TO", "TEN_TO", "TEN_TO");
                LoadGridNgayDK();
                LoadGridCongNhan();
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
                EnableButon();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
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
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyCatTangCa", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = Convert.ToInt64(cboDonVi.EditValue);
                cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = Convert.ToInt64(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = Convert.ToInt64(cboTo.EditValue);
                cmd.Parameters.Add("@THEM", SqlDbType.Bit).Value = isAdd;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                dt.Columns["SO_GIO"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, true, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["SG_TC"].OptionsColumn.AllowEdit = false;
                grvCongNhan.OptionsSelection.MultiSelect = true;
                grvCatTC_FocusedRowChanged(null, null);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadGridNgayDK()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyCatTangCa", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = Convert.ToInt64(cboDonVi.EditValue);
                cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = Convert.ToInt64(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = Convert.ToInt64(cboTo.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
                cmd.Parameters.Add("@THEM", SqlDbType.Bit).Value = isAdd;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                dt.Columns["SO_GIO"].ReadOnly = false;
                dt.Columns["DA_CTC"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCatTC, grvCatTC, dt, isAdd ? true : false, true, true, true, true, this.Name);
                grvCatTC.Columns["NGAY"].OptionsColumn.AllowEdit = false;
                grvCongNhan.OptionsSelection.MultiSelect = true;
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
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("THANG").ToString()).ToString("MM/yyyy");
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }
        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calNgay.DateTime.Date.ToString("MM/yyyy");
            }
            catch (Exception ex)
            {
                cboNgay.Text = DateTime.Now.ToString("MM/yyyy");
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
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXiNghiep, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(cboDonVi.EditValue), false), "ID_XN", "TEN_XN", "TEN_XN");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO");
            LoadGridNgayDK();
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), true), "ID_TO", "TEN_TO", "TEN_TO");
            LoadGridNgayDK();
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }
        private void LoadNgay()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS THANG FROM dbo.DANG_KY_CAT_TC ORDER BY Y DESC , M DESC"));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);

            grvNgay.Columns["M"].Visible = false;
            grvNgay.Columns["Y"].Visible = false;
            if (dt.Rows.Count > 0)
            {
                cboNgay.EditValue = dt.Rows[0]["THANG"];
            }
            else
            {
                cboNgay.Text = DateTime.Now.ToString("MM/yyyy");
            }
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridNgayDK();
            LoadGridCongNhan();
            Commons.Modules.sLoad = "";
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }
        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridNgayDK();
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
                            LoadGridNgayDK();
                            LoadGridCongNhan();
                            break;
                        }
                    case "xoa":
                        {
                            try
                            {
                                if (Commons.Modules.ObjSystems.MsgDelete(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong")) == 0) return;
                                string sBT = "sBTCatTangCa" + Commons.Modules.iIDUser;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdCongNhan, grvCongNhan), "");
                                string sSQL = "DELETE dbo.DANG_KY_CAT_TC FROM dbo.DANG_KY_CAT_TC T1 INNER JOIN " + sBT + " T2 ON T1.ID_CN = T2.ID_CN WHERE T1.NGAY = '" + Convert.ToDateTime(grvCatTC.GetFocusedRowCellValue("NGAY")).ToString("MM/dd/yyyy") + "'";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                            }
                            catch (Exception ex)
                            {
                                Commons.Modules.ObjSystems.MsgError(ex.Message);
                            }
                            LoadGridNgayDK();
                            LoadGridCongNhan();
                            break;
                        }
                    case "ghi":
                        {

                            if (!Validate()) return;
                            if (grvCongNhan.HasColumnErrors) return;
                            Commons.Modules.ObjSystems.ShowWaitForm(this);
                            DataTable dt = new DataTable();
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            EnableButon();
                            LoadGridNgayDK();
                            LoadGridCongNhan();
                            Commons.Modules.ObjSystems.HideWaitForm();
                            break;
                        }
                    case "khongghi":
                        {
                            isAdd = false;
                            LoadGridNgayDK();
                            LoadGridCongNhan();
                            EnableButon();
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
                Commons.Modules.ObjSystems.HideWaitForm();
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
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string sCotCN = grvCongNhan.FocusedColumn.FieldName;
                var data = grvCongNhan.GetFocusedRowCellValue(sCotCN);
                dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdCongNhan, grvCongNhan);
                dt = (DataTable)grdCongNhan.DataSource;

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));

                dt.AcceptChanges();
            }
            catch (Exception ex)
            {

            }

        }
        private bool Savedata()
        {
            string sBT = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                grvCongNhan.PostEditor();
                grvCongNhan.UpdateCurrentRow();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdCongNhan.DataSource, "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyCatTangCa", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    Commons.Modules.ObjSystems.MsgError(dt.Rows[0][1].ToString());
                    return false;
                }
                Commons.Modules.ObjSystems.XoaTable(sBT);
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                return true;
            }
            catch (Exception ex) { }
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
                btnALL.Buttons[5].Properties.Visible = !isAdd;

                cboNgay.Enabled = !isAdd;
                cboDonVi.Enabled = !isAdd;
                cboXiNghiep.Enabled = !isAdd;
                cboTo.Enabled = !isAdd;
                grvCongNhan.OptionsBehavior.Editable = isAdd;
                grvCatTC.OptionsBehavior.Editable = isAdd;
            }
            catch { }
        }
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToString("MM/yyyy");
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

        private void grvCatTC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                String sNgay;
                try
                {
                    dtTmp = (DataTable)grdCongNhan.DataSource;

                    string sDK = "";
                    sNgay = "-1";
                    try { sNgay = grvCatTC.GetFocusedRowCellValue("NGAY").ToString(); } catch { }
                    if (sNgay != "-1")
                    {
                        sDK = " NGAY = '" + sNgay + "' ";
                    }
                    else
                    {
                        sDK = "1 = 0";
                    }

                    dtTmp.DefaultView.RowFilter = sDK;
                }
                catch { }
            }
            catch { }
        }

        private void grvCatTC_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //GridView view = sender as GridView;
            //try
            //{
            //    if (e.Column.FieldName == "SO_GIO")
            //    {
            //        DataTable dt = new DataTable();
            //        DataTable dt1 = new DataTable();
            //        var data = grvCatTC.GetFocusedRowCellValue("SO_GIO");
            //        dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan);
            //        dt = (DataTable)grdCongNhan.DataSource;

            //        dt.AsEnumerable().Where(row => dt1.AsEnumerable()
            //                                                 .Select(r => r.Field<DateTime>("NGAY"))
            //                                                 .Any(x => x == row.Field<DateTime>("NGAY"))
            //                                                 ).ToList<DataRow>().ForEach(r => r["SO_GIO"] = (data));

            //        dt.AcceptChanges();

            //        if (Convert.ToDouble(dt.Compute("Sum(SO_GIO)", "")) == 0)
            //        {
            //            grvCatTC.SetFocusedRowCellValue("DA_CTC", false);
            //        }
            //        else
            //        {
            //            grvCatTC.SetFocusedRowCellValue("DA_CTC", true);

            //        }
            //    }
            //}
            //catch { }
        }

        private void grvCatTC_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DataRow dr = view.GetFocusedDataRow();

            try
            {
                if (e.Column.FieldName == "SO_GIO")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    var data = e.Value;
                    //var data = grvCatTC.GetFocusedRowCellValue("SO_GIO");

                    dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan);
                    dt = (DataTable)grdCongNhan.DataSource;

                    dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                             .Select(r => r.Field<DateTime>("NGAY"))
                                                             .Any(x => x == row.Field<DateTime>("NGAY"))
                                                             ).ToList<DataRow>().ForEach(r => r["SO_GIO"] = (data));
                    dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                             .Select(r => r.Field<DateTime>("NGAY"))
                                                             .Any(x => x == row.Field<DateTime>("NGAY")) && row.Field<double>("SG_TC") > Convert.ToDouble((data))
                                                             ).ToList<DataRow>().ForEach(r => r["SG_TC"] = (data));

                    dt.AcceptChanges();
                }
                if(e.Column.FieldName == "DA_CTC")
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    var data = e.Value;
                    //var data = grvCatTC.GetFocusedRowCellValue("SO_GIO");

                    dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan);
                    dt = (DataTable)grdCongNhan.DataSource;

                    dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                             .Select(r => r.Field<DateTime>("NGAY"))
                                                             .Any(x => x == row.Field<DateTime>("NGAY"))
                                                             ).ToList<DataRow>().ForEach(r => r["DA_CTC"] = (data));

                    dt.AcceptChanges();
                }
            }
            catch { }
        }

        private void grvCatTC_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            try
            {
                grvCatTC.UpdateCurrentRow();
            }
            catch { }
        }

        private void grvCatTC_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(grvCatTC.GetRowCellValue(e.RowHandle, grvCatTC.Columns["NGAY"]).ToString().Trim()).DayOfWeek.ToString() != "Sunday") return;
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }
            }
            catch
            {
            }
        }
    }
}