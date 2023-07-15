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
using System.Globalization;
using DevExpress.Utils.Menu;

namespace Vs.TimeAttendance
{
    public partial class ucCheDoChamCongNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        private static DataTable dt_Temp; // Lưu data cũ khi bấm nút thêm
        private static Boolean isAdd = false;
        public static ucCheDoChamCongNhanVien _instance;
        public static ucCheDoChamCongNhanVien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCheDoChamCongNhanVien();
                return _instance;
            }
        }
        CultureInfo cultures = new CultureInfo("en-US");

        public ucCheDoChamCongNhanVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
        }

        private void ucCheDoChamCongNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                calNgay.EditValue = DateTime.Now;
                EnableButon();
                try
                {
                    LoadNgay();
                }
                catch
                {
                    MessageBox.Show("err Datetime System");
                }
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

                LoadGrdCDCCNV();
                Commons.Modules.sLoad = "";

                DataTable dCa = new DataTable();
                RepositoryItemLookUpEdit cboCa = new RepositoryItemLookUpEdit();
                dCa.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT ID_CDLV, CA ,GIO_BD,GIO_KT FROM CHE_DO_LAM_VIEC"));

                cboCa.NullText = "";
                cboCa.ValueMember = "ID_CDLV";
                cboCa.DisplayMember = "CA";


                cboCa.DataSource = dCa;
                cboCa.Columns.Clear();
                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CA"));
                cboCa.Columns["CA"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "CA");

                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_BD"));
                cboCa.Columns["GIO_BD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_BDChamCong");
                cboCa.Columns["GIO_BD"].FormatType = DevExpress.Utils.FormatType.DateTime;
                cboCa.Columns["GIO_BD"].FormatString = "HH:mm";

                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_KT"));
                cboCa.Columns["GIO_KT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_KTChamcong");
                cboCa.Columns["GIO_KT"].FormatType = DevExpress.Utils.FormatType.DateTime;
                cboCa.Columns["GIO_KT"].FormatString = "HH:mm";

                cboCa.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboCa.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvCDCCNV.Columns["CA"].ColumnEdit = cboCa;
                cboCa.BeforePopup += cboCa_BeforePopup;
                cboCa.EditValueChanged += CboCa_EditValueChanged;
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            }
            catch { }
        }

        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;

            //string id = lookUp.get;

            // Access the currently selected data row
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

            if (grvCDCCNV.GetFocusedRowCellValue("NGAY_AD").ToString() == "")
            {
                grvCDCCNV.SetFocusedRowCellValue("NGAY_AD", cboNgay.EditValue.ToString());
            }

            grvCDCCNV.SetFocusedRowCellValue("CA", dataRow.Row["ID_CDLV"]);
            //grvLamThem.SetFocusedRowCellValue("PHUT_KT", dataRow.Row["PHUT_KT"]);
        }
        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCaLV", cboNgay.EditValue, grvCDCCNV.GetFocusedRowCellValue("ID_NHOM"), Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                if (sender is LookUpEdit cbo)
                {
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadGrdCDCCNV()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                if (isAdd)
                {

                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListEditCDCCNV", cboNgay.EditValue, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    dt_Temp = new DataTable();
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCDCCNV, dt, true, false, true, false, true, this.Name);
                    dt_Temp = ((DataTable)grdData.DataSource).Copy();
                    dt.Columns["NGAY_AD"].ReadOnly = false;
                    dt.Columns["CA"].ReadOnly = false;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCheDoChamCongNhanVien", cboNgay.EditValue, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCDCCNV, dt, false, false, true, false, true, this.Name);
                    dt.Columns["NGAY_AD"].ReadOnly = false;
                }
                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvCDCCNV, dID_NHOM, "ID_NHOM", "CHE_DO_LAM_VIEC");
                FormatGridView();
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            grvCDCCNV.Columns["ID_CN"].Visible = false;
            //grvCDCCNV.Columns["NGAY_AD"].Visible = false;
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdCDCCNV();
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdCDCCNV();
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCDCCNV();
            Commons.Modules.sLoad = "";
        }

        private void dNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCDCCNV();
            Commons.Modules.sLoad = "";
        }

        private void FormatGridView()
        {
            grvCDCCNV.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvCDCCNV.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvCDCCNV.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            grvCDCCNV.Columns["TEN_XN"].OptionsColumn.ReadOnly = true;
            grvCDCCNV.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;
        }


        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        if (cboNgay.Text == "") return;
                        isAdd = true;
                        EnableButon();
                        LockControl(false);
                        LoadGrdCDCCNV();
                        break;
                    }
                case "xoa":
                    {
                        XoaCDCCNV();
                        break;
                    }
                case "ghi":
                    {
                        grvCDCCNV.CloseEditor();
                        grvCDCCNV.UpdateCurrentRow();
                        //if (grvCDCCNV.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgThemKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                            return;
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgdacongnhanthanhcong"), Commons.Form_Alert.enmType.Success);
                        }
                        isAdd = false;
                        EnableButon();
                        LockControl(true);
                        LoadGrdCDCCNV();
                        break;
                    }
                case "khongghi":
                    {

                        isAdd = false;
                        EnableButon();
                        LockControl(true);
                        LoadGrdCDCCNV();
                        break;
                    }
                case "thoat":
                    {
                        isAdd = false;
                        EnableButon();
                        LockControl(true);
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "capnhatnhom":
                    {
                        Validate();
                        if (grvCDCCNV.HasColumnErrors) return;
                        if (XtraMessageBox.Show("Bạn có muốn cập nhật nhóm: " + grvCDCCNV.GetFocusedRowCellDisplayText("ID_NHOM") + ", ca: " + grvCDCCNV.GetFocusedRowCellDisplayText("CA") + " cho các nhân viên được chọn", Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        CapNhatNhom();
                        break;
                    }
                case "xoatrangnhom":
                    {
                        Validate();
                        if (grvCDCCNV.HasColumnErrors) return;
                        XoaTrangNhom();
                        break;
                    }
            }
        }

        private void CapNhatNhom()
        {
            DataTable dt = new DataTable();
            string sTB = "CDCCNV_CapNhatNhom" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                sSql = " SELECT '" + Convert.ToDateTime(grvCDCCNV.GetFocusedRowCellValue("NGAY_AD")).ToString("dd/MM/yyyy") + "' AS NGAY_AD, ID_CN, MS_CN, HO_TEN, " + grvCDCCNV.GetFocusedRowCellValue("ID_NHOM") + " AS ID_NHOM, " + grvCDCCNV.GetFocusedRowCellValue("CA") + " AS CA, TEN_XN, TEN_TO FROM " + sTB + "";

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCDCCNV, dt, true, false, true, false, true, this.Name);
                grvCDCCNV.Columns["ID_CN"].Visible = false;
                dt.Columns["ID_NHOM"].ReadOnly = false;
                dt.Columns["CA"].ReadOnly = false;
                dt.Columns["NGAY_AD"].ReadOnly = false;
                FormatGridView();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                Commons.Modules.sLoad = "";

            }
            catch
            {
            }
        }

        private void XoaTrangNhom()
        {
            DataTable dt = new DataTable();
            string sTB = "CDCCNV_XoaNhom" + Commons.Modules.UserName;
            string sSql = "";
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvCDCCNV), "");

                sSql = " SELECT NGAY_AD, ID_CN, MS_CN, HO_TEN, NULL ID_NHOM, NULL CA, TEN_XN, TEN_TO FROM " + sTB + "";

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCDCCNV, dt, true, false, true, false, true, this.Name);
                grvCDCCNV.Columns["ID_CN"].Visible = false;
                dt.Columns["ID_NHOM"].ReadOnly = false;
                dt.Columns["CA"].ReadOnly = false;
                dt.Columns["NGAY_AD"].ReadOnly = false;
                FormatGridView();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }

        private void EnableButon()
        {
            btnALL.Buttons[0].Properties.Visible = !isAdd;
            btnALL.Buttons[1].Properties.Visible = !isAdd;
            btnALL.Buttons[2].Properties.Visible = !isAdd;
            btnALL.Buttons[3].Properties.Visible = !isAdd;
            btnALL.Buttons[4].Properties.Visible = isAdd;
            btnALL.Buttons[5].Properties.Visible = isAdd;
            btnALL.Buttons[6].Properties.Visible = isAdd;
            btnALL.Buttons[7].Properties.Visible = isAdd;
            btnALL.Buttons[8].Properties.Visible = isAdd;
        }


        private void XoaCDCCNV()
        {
            if (grvCDCCNV.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.MsgDelete(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong")) == 0) return;
            //xóa
            try
            {

                String dele = "DELETE dbo.CHE_DO_CHAM_CONG_NHAN_VIEN WHERE ID_CN = " + grvCDCCNV.GetFocusedRowCellValue("ID_CN") + "AND ID_NHOM =" + grvCDCCNV.GetFocusedRowCellValue("ID_NHOM") + " AND NGAY_AD = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, dele);
                grvCDCCNV.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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

        private bool Savedata()
        {
            string sTB = "CDCC_NV_TMP" + Commons.Modules.UserName;
            string sTB_CU = "CDCC_CU" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB_CU, dt_Temp, "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveCheDoChamCongNV", Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text), sTB_CU, sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB_CU);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB_CU);
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        //LOAD THANG
        private void LoadNgay()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY_AD FROM (SELECT NGAY_AD, NGAY_AD AS NGAY FROM CHE_DO_CHAM_CONG_NHAN_VIEN GROUP BY NGAY_AD) T1 ORDER BY NGAY DESC"));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);


            cboNgay.EditValue = DateTime.Today;
            //if (dt.Rows.Count <= 0)
            //{
            //    cboNgay.EditValue = DateTime.Today;
            //}
            //else
            //{
            //    cboNgay.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_AD"]);
            //}
        }

        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {

            try
            {
                cboNgay.Text = calNgay.DateTime.ToString("dd/MM/yyyy");
            }
            catch
            {
                cboNgay.Text = calNgay.DateTime.ToString("dd/MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void LockControl(Boolean oLock)
        {
            try
            {
                cboDonVi.Enabled = oLock;
                cboXiNghiep.Enabled = oLock;
                cboTo.Enabled = oLock;
                cboNgay.Enabled = oLock;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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

        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY_AD").ToString()).ToShortDateString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCDCCNV();
            Commons.Modules.sLoad = "";
        }

        private void grvCDCCNV_RowCountChanged(object sender, EventArgs e)
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


        #region chuot phải
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
                try
                {

                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    string sCotCN = grvCDCCNV.FocusedColumn.FieldName;
                    var data = grvCDCCNV.GetFocusedRowCellValue(sCotCN);

                    dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvCDCCNV);
                    dt = (DataTable)grdData.DataSource;

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
            catch (Exception ex) { }
        }

        #endregion
        private void grvCDCCNV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (btnALL.Buttons[0].Properties.Visible) return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);
                    //if (flag == false) return;
                }
            }
            catch { }
        }
    }
}