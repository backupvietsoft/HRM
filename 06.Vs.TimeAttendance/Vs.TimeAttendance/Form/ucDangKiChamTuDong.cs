using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Menu;

namespace Vs.TimeAttendance
{
    public partial class ucDangKiChamTuDong : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        public static ucDangKiChamTuDong _instance;
        public static ucDangKiChamTuDong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiChamTuDong();
                return _instance;
            }
        }


        public ucDangKiChamTuDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region Đăng kí Chấm tự động
        private void ucDangKiChamTuDong_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDenNgay);
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
                enableButon(true);
            }
            catch { }

        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        enableButon(false);
                        LoadGridDKChamTuDong(isAdd);
                        Commons.Modules.ObjSystems.AddnewRow(grvKDCTD, true);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaDuLieu"),
                        Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        XoaData();
                        enableButon(true);
                        LoadGridDKChamTuDong(isAdd);
                        break;
                    }
                case "luu":
                    {
                        try
                        {
                            Validate();
                            if (grvKDCTD.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdDKCTD.DataSource;
                            this.Cursor = Cursors.WaitCursor;
                            if (!KiemTraLuoi(dt)) return;
                            this.Cursor = Cursors.Default;
                            Savedata();

                            dt = new DataTable();
                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD);

                            //dt = dt.AsEnumerable().Where(r => r.Field<string>("").Equals("")).M
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTCongNhan" + Commons.Modules.iIDUser, dt, "");
                            Commons.Modules.sLoad = "0Load";
                            dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT MIN(NGAY) MIN_NGAY, MAX(NGAY) MAX_NGAY FROM " + "sBTCongNhan" + Commons.Modules.iIDUser + ""));
                            Commons.Modules.ObjSystems.XoaTable("sBTCongNhan" + Commons.Modules.iIDUser);
                            try
                            {
                                datTNgay.EditValue = Convert.ToDateTime(dt.Rows[0]["MIN_NGAY"]);
                                datDenNgay.EditValue = Convert.ToDateTime(dt.Rows[0]["MAX_NGAY"]);
                            }
                            catch
                            {
                            }
                            
                            Commons.Modules.sLoad = "";
                            enableButon(true);
                            LoadGridDKChamTuDong(isAdd);
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.XoaTable("sBTCongNhan" + Commons.Modules.iIDUser);
                        }
                        break;
                    }
                case "khongluu":
                    {
                        LoadGridDKChamTuDong(false);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvKDCTD);
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
        #endregion

        private void XoaData()
        {
            //string stbXoaData = "XOA_DANG_KY_CHAM_TU_DONG" + Commons.Modules.UserName;
            //try
            //{
            //    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbXoaData,
            //                                                       Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");
            //    string sSql = "DELETE DANG_KY_CHAM_TU_DONG WHERE CONVERT(NVARCHAR,NGAY,112) = '"
            //                   + Convert.ToDateTime(Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text)).ToString("yyyyMMdd") + "'"
            //                   + " AND ID_CN IN (SELECT ID_CN FROM " + stbXoaData + ")";
            //    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            //    Commons.Modules.ObjSystems.XoaTable(stbXoaData);
            //}
            //catch
            //{

            //}
            try
            {
                string sSql = "DELETE FROM dbo.DANG_KY_CHAM_TU_DONG WHERE ID_CN = " + grvKDCTD.GetFocusedRowCellValue("ID_CN") + " AND NGAY =  '" + Convert.ToDateTime(grvKDCTD.GetFocusedRowCellValue("NGAY")).ToString("MM/dd/yyyy") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            }
            catch { }
        }

        #region hàm xử lý dữ liệu 
        private void LoadGridDKChamTuDong(bool isAdd)
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    System.Data.SqlClient.SqlConnection conn;
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetEditDKChamTuDong", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                    cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                    cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDenNgay.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = ds.Tables[0].Copy();
                    //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditDKChamTuDong", Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text), cboDV.EditValue,
                    //                                cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKCTD, grvKDCTD, dt, true, true, true, true, true, this.Name);
                    grvKDCTD.Columns["ID_CN"].OptionsColumn.ReadOnly = false;
                    grvKDCTD.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["MS_CN"].Visible = false;
                    grvKDCTD.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["TEN_XN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;

                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();
                    RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CN", "MS_CN", "ID_CN", grvKDCTD, dt, this.Name);
                    cbo.View.Columns["TEN_XN"].Visible = false;
                    cbo.View.Columns["TEN_TO"].Visible = false;
                    cbo.BeforePopup += cboID_CN_BeforePopup;
                    cbo.EditValueChanged += cboID_CN_EditValueChanged;
                }
                else
                {

                    System.Data.SqlClient.SqlConnection conn;
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListDKChamTuDong", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                    cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                    cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDenNgay.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = ds.Tables[0].Copy();

                    //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDKChamTuDong", Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text), Commons.Modules.ObjSystems.ConvertDateTime(datDenNgay.Text), cboDV.EditValue,
                    //                                cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKCTD, grvKDCTD, dt, false, false, true, true, true, this.Name);
                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();
                    RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CN", "MS_CN", "ID_CN", grvKDCTD, dt, this.Name);
                    cbo.View.Columns["TEN_XN"].Visible = false;
                    cbo.View.Columns["TEN_TO"].Visible = false;
                    cbo.BeforePopup += cboID_CN_BeforePopup;
                    cbo.EditValueChanged += cboID_CN_EditValueChanged;

                    Commons.Modules.ObjSystems.DeleteAddRow(grvKDCTD);

                }
                grvKDCTD.Columns["MS_CN"].Visible = false;
                grvKDCTD.Columns["ID_CN"].Visible = true;

            }
            catch (Exception ex)
            {

            }
        }
        private void cboID_CN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvKDCTD.SetFocusedRowCellValue("ID_CN", Convert.ToInt64((dataRow.Row[0])));
                grvKDCTD.SetFocusedRowCellValue("HO_TEN", (dataRow.Row[2]).ToString());
                grvKDCTD.SetFocusedRowCellValue("TEN_XN", (dataRow.Row[3]).ToString());
                grvKDCTD.SetFocusedRowCellValue("TEN_TO", (dataRow.Row[3]).ToString());
            }
            catch { }

        }
        private void cboID_CN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetEditDKChamTuDong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[1].Copy();
                lookUp.Properties.DataSource = dt;
            }
            catch { }
        }
        private void Savedata()
        {
            string stbDKCTD = "DKCTD" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbDKCTD, Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDANG_KY_CHAM_TU_DONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, stbDKCTD);
                Commons.Modules.ObjSystems.XoaTable(stbDKCTD);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;

            cboDV.Enabled = visible;
            cboXN.Enabled = visible;
            cboTo.Enabled = visible;
            datTNgay.Enabled = visible;
            datDenNgay.Enabled = visible;

            searchControl.Visible = true;
            isAdd = !windowsUIButton.Buttons[0].Properties.Visible;
        }
        #endregion

        private void grvKDCTD_RowCountChanged(object sender, EventArgs e)
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

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDenNgay.EditValue = secondDateTime;
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void datDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGridDKChamTuDong(isAdd);
        }

        #region KiemTraLuoi

        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvKDCTD.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Ngày bắt đầu thử việc
                if (!KiemDuLieuNgay(grvKDCTD, dr, "NGAY", true, this.Name))
                {
                    errorCount++;
                }

                //Ngày kết thúc thử việc
                if (!KiemDuLieuNgay(grvKDCTD, dr, "NGAY_KT", false, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
        }
        #endregion

        private void grvKDCTD_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
            }
            catch { }
        }

        #region chuotphai
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
                string sCotCN = grvKDCTD.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvKDCTD.GetFocusedRowCellValue(grvKDCTD.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 3) == "NGA" ? Convert.ToDateTime(grvKDCTD.GetFocusedRowCellValue(grvKDCTD.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvKDCTD.GetFocusedRowCellValue(grvKDCTD.FocusedColumn.FieldName)));
                    grdDKCTD.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
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
                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    if (grvKDCTD.FocusedColumn.FieldName.ToString() == "MS_CN" || grvKDCTD.FocusedColumn.FieldName.ToString() == "HO_TEN" || grvKDCTD.FocusedColumn.FieldName.ToString() == "TEN_XN" || grvKDCTD.FocusedColumn.FieldName.ToString() == "TEN_TO") return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);
                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
