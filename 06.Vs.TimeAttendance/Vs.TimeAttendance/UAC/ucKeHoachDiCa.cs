using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.TimeAttendance.Form;

namespace Vs.TimeAttendance
{
    public partial class ucKeHoachDiCa : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucKeHoachDiCa _instance;
        private bool them = false;
        public static ucKeHoachDiCa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKeHoachDiCa();
                return _instance;
            }
        }
        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
        public ucKeHoachDiCa()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucKeHoachDiCa_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            dateNam.DateTime = DateTime.Now;
            LoadGrdCongNhan(them);
            radTinHTrang_SelectedIndexChanged(null, null);
            LoadgrdKeHoachDiCa();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan(them);
            LoadgrdKeHoachDiCa();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan(them);
            LoadgrdKeHoachDiCa();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan(them);
            LoadgrdKeHoachDiCa();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        public void CheckDuplicateDIEM_THEO_DOI_NOP_BAI(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow row = grid.GetDataRow(e.RowHandle);
            int count = 0;
            foreach (DataRow r in GridDataSet.Tables[0].Rows)
            {
                if (r.RowState != DataRowState.Deleted)
                {
                    if (r["NHAN_SU"].ToString() == row["NHAN_SU"].ToString() && r["NGUOI_GIAO"].ToString() == row["NGUOI_GIAO"].ToString() && r["NGAY_GIAO"].ToString() == row["NGAY_GIAO"].ToString())
                    {
                        if (grid.IsNewItemRow(grid.FocusedRowHandle))
                        {
                            r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                            grid.SetColumnError(grid.Columns["NHAN_SU"], "Nhân sự, người giao và ngày giao bị trùng, xin vui lòng kiểm tra lại.");
                            e.Valid = false;
                            return;
                        }
                        else
                        {
                            count++;
                            if (count == 2)
                            {
                                r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                                grid.SetColumnError(grid.Columns["NHAN_SU"], "Nhân sự, người giao và ngày giao bị trùng, xin vui lòng kiểm tra lại.");
                                e.Valid = false;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "capnhatdieuchinh":
                    {
                        try
                        {
                            
                            if (grvCongNhan.RowCount == 0 || grvKeHoachDiCa.RowCount == 0) return;
                            frmSaveKeHoachDiCa KeHoachDiCa = new frmSaveKeHoachDiCa(Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN")), Convert.ToInt64(grvKeHoachDiCa.GetFocusedRowCellValue("ID_NHOM")), Convert.ToString(grvKeHoachDiCa.GetFocusedRowCellValue("CA")), Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY")), Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("DEN_NGAY")));
                            KeHoachDiCa.ShowDialog();
                            if(KeHoachDiCa.result == true)
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            LoadgrdKeHoachDiCa();
                            grvCongNhan_FocusedRowChanged(null, null);
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.MsgError(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongThanhCong"));
                        }
                        break;
                    }
                case "capnhat":
                    {
                        if (!Validate()) return;
                        if (grvCongNhan.HasColumnErrors) return;
                        if (Commons.Modules.ObjSystems.MsgQuestion("Bạn có muốn cập nhật nhóm: " + grvKeHoachDiCa.GetFocusedRowCellDisplayText("ID_NHOM") + ", ca: " + grvKeHoachDiCa.GetFocusedRowCellDisplayText("CA") + " cho các nhân viên được chọn") == 0) return;
                        CapNhatNhom();
                        break;
                    }

                case "themsua":
                    {

                        them = true;
                        LoadGrdCongNhan(them);
                        grvCongNhan_FocusedRowChanged(null, null);
                        Commons.Modules.ObjSystems.AddnewRow(grvKeHoachDiCa, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaKeHoachDiCa();
                        break;
                    }
                case "In":
                    {
                        Form.frmInKehoachdica frm = new Form.frmInKehoachdica();
                        frm.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        if (!Validate()) return;
                        if (grvKeHoachDiCa.HasColumnErrors) return;
                        DataTable dt_CHON = new DataTable();
                        dt_CHON = ((DataTable)grdCongNhan.DataSource);
                        if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                        {
                            Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonNhanVien"));
                            return;
                        }
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        Commons.Modules.ObjSystems.DeleteAddRow(grvKeHoachDiCa);
                        them = false;
                        LoadGrdCongNhan(them);
                        LoadgrdKeHoachDiCa();
                        enableButon(true);
                        grvCongNhan_FocusedRowChanged(null, null);

                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvKeHoachDiCa);
                        them = false;
                        LoadGrdCongNhan(them);
                        LoadgrdKeHoachDiCa();
                        enableButon(true);
                        grvCongNhan_FocusedRowChanged(null, null);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        #region hàm xử lý dữ liệu
        private void LoadGrdCongNhan(bool them)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_KeHoachDiCa", dateNam.Text, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, them));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, false, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["TINH_TRANG"].Visible = false;
                grvCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;

                if (them == false)
                {
                    grvCongNhan.Columns["CHON"].Visible = false;
                }
                else
                {
                    grvCongNhan.Columns["CHON"].Visible = true;
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

        private void LoadgrdKeHoachDiCa()
        {
            try
            {
                DataTable dt = new DataTable();
                //string select = "SELECT ID_CN,ID_NHOM,CA,TU_NGAY,DEN_NGAY,GHI_CHU FROM KE_HOACH_DI_CA  WHERE ID_CN = " + (grvCongNhan.GetFocusedRowCellValue("ID_CN")==null?-1 : grvCongNhan.GetFocusedRowCellValue("ID_CN")) + " AND YEAR(TU_NGAY) = " + dateNam.Text + "";
                //string select = "SELECT ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU FROM KE_HOACH_DI_CA  WHERE YEAR(TU_NGAY) = " + dateNam.Text + "";
                string select = "SELECT ID_CN, ID_NHOM, CA, TU_NGAY, DEN_NGAY, GHI_CHU FROM KE_HOACH_DI_CA  WHERE YEAR(TU_NGAY) = " + dateNam.DateTime.Year + "";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKeHoachDiCa", dateNam.DateTime.Year, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdKeHoachDiCa.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdKeHoachDiCa, grvKeHoachDiCa, dt, false, false, true, true, true, this.Name);
                    grvKeHoachDiCa.Columns["ID_CN"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvKeHoachDiCa, Commons.Modules.ObjSystems.DataNhom(false), false, "ID_NHOM", "NHOM_CHAM_CONG");

                    RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                    dEditN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dEditN.DisplayFormat.FormatString = "dd/MM/yyyy";
                    dEditN.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dEditN.EditFormat.FormatString = "dd/MM/yyyy";
                    dEditN.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                    dEditN.Mask.EditMask = "dd/MM/yyyy";
                    grvKeHoachDiCa.Columns["TU_NGAY"].ColumnEdit = dEditN;
                    grvKeHoachDiCa.Columns["DEN_NGAY"].ColumnEdit = dEditN;
                    //grvKeHoachDiCa.Appearance.HeaderPanel.BackColor = Color.FromArgb(240, 128, 25);
                    //for (int i = 0; i < grvKeHoachDiCa.Columns.Count; i++)
                    //{
                    //    grvKeHoachDiCa.Columns[i].AppearanceHeader.BackColor = Color.FromArgb(200, 200, 200);
                    //}
                    //addMay ID_CA,CA
                    //RepositoryItemDateEdit dEditTN = new RepositoryItemDateEdit();
                    //dEditTN.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                    //grvKeHoachDiCa.Columns["TU_NGAY"].ColumnEdit = dEditTN;

                    RepositoryItemLookUpEdit cboMSCa = new RepositoryItemLookUpEdit();
                    cboMSCa.NullText = "";
                    cboMSCa.ValueMember = "CA";
                    cboMSCa.DisplayMember = "CA";
                    cboMSCa.DataSource = Commons.Modules.ObjSystems.DataCa(-1);
                    cboMSCa.Columns.Clear();
                    cboMSCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CA"));
                    cboMSCa.Columns["CA"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "CA");
                    cboMSCa.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboMSCa.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    grvKeHoachDiCa.Columns["CA"].ColumnEdit = cboMSCa;
                    cboMSCa.BeforePopup += CboMSCa_BeforePopup;
                }
                else
                {
                    grdKeHoachDiCa.DataSource = dt;
                }
            }
            catch (Exception EX)
            {
            }

        }
        private void CboMSCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                if (sender is LookUpEdit cbo)
                {
                    int IDNHOM = Convert.ToInt32(grvKeHoachDiCa.GetFocusedRowCellValue("ID_NHOM"));
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = Commons.Modules.ObjSystems.DataCa(IDNHOM);
                }
            }
            catch
            {
            }
        }

        private bool Savedata()
        {
            DataTable dkKeHoachDiCa = new DataTable();
            string sbtKeHoachDiCa = "grvKeHoachDiCa" + Commons.Modules.iIDUser;
            string sbtCongNhan = "grvCongNhan" + Commons.Modules.iIDUser;

            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtKeHoachDiCa, (DataTable)grdKeHoachDiCa.DataSource, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtCongNhan, (DataTable)grdCongNhan.DataSource, "");

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveKeHoachDiCa", dateNam.Text, sbtKeHoachDiCa, sbtCongNhan);
                Commons.Modules.ObjSystems.XoaTable(sbtKeHoachDiCa);
                Commons.Modules.ObjSystems.XoaTable(sbtCongNhan);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(sbtKeHoachDiCa);
                Commons.Modules.ObjSystems.XoaTable(sbtCongNhan);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = visible;
            //searchControl.Visible = visible;
        }
        private void XoaKeHoachDiCa()
        {
            if (grvKeHoachDiCa.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.KE_HOACH_DI_CA WHERE ID_CN = " + grvKeHoachDiCa.GetFocusedRowCellValue("ID_CN") + "  AND TU_NGAY = '" + Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY")).ToString("MM/dd/yyyy") + "' ";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvKeHoachDiCa.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }
        #endregion

        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdCongNhan.DataSource;
                if (radTinHTrang.SelectedIndex == 0) sdkien = "(TINH_TRANG = 1)";
                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TINH_TRANG = 0)";
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

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            grvCongNhan.UpdateCurrentRow();
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdKeHoachDiCa.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }

                //if (windowsUIButton.Buttons[0].Properties.Visible == false)
                //{
                //    sDK = "1 = 0";
                //}
                //else
                //{
                if (sIDCN != "-1")
                {
                    sDK = " ID_CN = '" + sIDCN + "' ";
                }
                else
                {
                    sDK = "1=0";
                }
                //}
                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
        }

        private void grvKeHoachDiCa_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Columns.GridColumn ngayngung = View.Columns["TU_NGAY"];
            grvKeHoachDiCa.SetFocusedRowCellValue("ID_CN", grvCongNhan.GetFocusedRowCellValue("ID_CN"));

            //Kiem tra trung ngay
            string btKHDC = "TMPPRORUN" + Commons.Modules.UserName;
            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, btKHDC, Commons.Modules.ObjSystems.ConvertDatatable(grvKeHoachDiCa), "");

            try
            {
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spKiemTraKHDiCa", btKHDC, grvKeHoachDiCa.GetFocusedRowCellValue("ID_CN"), Commons.Modules.ObjSystems.ConvertDateTime(Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY")).ToString("dd/MM/yyyy")), Commons.Modules.ObjSystems.ConvertDateTime(Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("DEN_NGAY")).ToString("dd/MM/yyyy"))));
                if (n > 1)
                {
                    e.Valid = false;
                    View.SetColumnError(ngayngung, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraNgayKeHoachDiCa", Commons.Modules.TypeLanguage)); return;
                }
            }
            catch (Exception ex)
            {
            }
            Commons.Modules.ObjSystems.XoaTable(btKHDC);
            //DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(View);
            //dt = dt.AsEnumerable().Where(x => x.Field<DateTime>("TU_NGAY") <= Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY"))).CopyToDataTable();
            //var ktTrung = dt.AsEnumerable().Where(x => x.Field<int>("ID_NHOM") == 1);
            //if (dt.AsEnumerable().Where(x => x.Field<int>("ID_NHOM") == Convert.ToInt32(grvKeHoachDiCa.GetFocusedRowCellValue("ID_NHOM"))).Count(x => x.Field<DateTime>("TU_NGAY").Equals(Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, ngayngung)))) > 1)
            //{
            //}
        }

        private void grdKeHoachDiCa_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                XoaKeHoachDiCa();
            }
        }

        private void dateNam_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdCongNhan(them);
            LoadgrdKeHoachDiCa();
            grvCongNhan_FocusedRowChanged(null, null);
        }

        private void grvKeHoachDiCa_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvKeHoachDiCa.ClearColumnErrors();
            GridView view = sender as GridView;
            if (view == null) return;

            if (view.FocusedColumn.Name == "colTU_NGAY")
            {
                if (Convert.ToDateTime(e.Value) > Convert.ToDateTime(view.GetFocusedRowCellValue("DEN_NGAY")))
                {
                    e.Valid = false;
                    e.ErrorText = "This value is not valid";
                    view.SetColumnError(view.Columns["DEN_NGAY"], e.ErrorText);

                    return;
                }
            }
            if (view.FocusedColumn.Name == "colDEN_NGAY")
            {
                if (Convert.ToDateTime(e.Value) < Convert.ToDateTime(view.GetFocusedRowCellValue("TU_NGAY")))
                {
                    e.Valid = false;
                    e.ErrorText = "This value is not valid";
                    view.SetColumnError(view.Columns["DEN_NGAY"], e.ErrorText);
                    return;
                }
            }
            if (view.FocusedColumn.Name == "colCA")
            {
                view.SetFocusedRowCellValue(view.Columns["CA"], e.Value);
            }
        }

        private void grvKeHoachDiCa_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvKeHoachDiCa_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvKeHoachDiCa_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //thêm defaulst khi add một dòng mới
            try
            {

                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("TU_NGAY", Convert.ToDateTime(DateTime.Now.Date));
                view.SetFocusedRowCellValue("DEN_NGAY", Convert.ToDateTime(DateTime.Now.Date));
            }
            catch
            {
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
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
            grvCongNhan_FocusedRowChanged(null, null);
        }

        //private void CapNhatNhom()
        //{
        //    try
        //    {
        //        grvKeHoachDiCa.CloseEditor();
        //        grvKeHoachDiCa.UpdateCurrentRow();

        //        DataTable dtTemp = new DataTable();
        //        //lấy lướng công nhân được chọn
        //        DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
        //        dt = dt.AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();
        //        //lấy lưới làm thêm đã có dữ liệu 
        //        DataTable dtLT = Commons.Modules.ObjSystems.ConvertDatatable(grdKeHoachDiCa);
        //        dtTemp = dtLT.Copy();
        //        //lấy table của lưới lưới cần cập nhật
        //        DataTable tableLT = Commons.Modules.ObjSystems.ConvertDatatable(grdKeHoachDiCa);
        //        DataTable data = new DataTable();
        //        data = tableLT.Copy();
        //        //lấy data dữ liệu của làm thêm cần cập nhập
        //        string sID_NHOM, sTuNgay, sDNgay, sID_CA, sGhiChu;


        //        int MaxRow = data.Rows.Count - 1;

        //        //sID_NHOM = ""; sTuNgay = ""; sDNgay = ""; sGhiChu = "";
        //        //try { sID_NHOM = grvKeHoachDiCa.GetFocusedRowCellValue("ID_NHOM").ToString(); } catch { }
        //        //try { sID_CA = grvKeHoachDiCa.GetFocusedRowCellValue("CA").ToString(); } catch { }
        //        //try { sTuNgay = Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY").ToString()).ToString("dd/MM/yyyy"); } catch { }
        //        //try { sDNgay = Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("DEN_NGAY").ToString()).ToString("dd/MM/yyyy"); } catch { }
        //        //try { sGhiChu = grvKeHoachDiCa.GetFocusedRowCellValue("GHI_CHU").ToString(); } catch { }

        //        //try { sID_NHOM = data.Rows[MaxRow]["ID_NHOM"].ToString(); } catch { }
        //        //try { sID_CA = grvKeHoachDiCa.GetFocusedRowCellValue("CA").ToString(); } catch { }
        //        //try { sTuNgay = Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY").ToString()).ToString("dd/MM/yyyy"); } catch { }
        //        //try { sDNgay = Convert.ToDateTime(grvKeHoachDiCa.GetFocusedRowCellValue("DEN_NGAY").ToString()).ToString("dd/MM/yyyy"); } catch { }
        //        //try { sGhiChu = grvKeHoachDiCa.GetFocusedRowCellValue("GHI_CHU").ToString(); } catch { }


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            string sDK = " 1 = 1 ";
        //            string sID_CN = "";
        //            try { sID_CN = dr["ID_CN"].ToString(); } catch { }
        //            DataTable dtTM = new DataTable();
        //            //dtTM = data.AsEnumerable().Where(x => x["ID_CN"].ToString().ToLower() == sID_CN).CopyToDataTable();
        //            data.DefaultView.RowFilter = " ID_CN= '" + sID_CN + "' ";
        //            if (data.DefaultView.ToTable().Rows.Count == 0)
        //            {
        //                //data.DefaultView.RowFilter = "";
        //                //try
        //                //{
        //                //    if (sID_CN != "") sDK = sDK + " AND ID_CN = '" + sID_CN + "' ";


        //                //    if (sID_NHOM != "") sDK = sDK + " AND ID_NHOM = '" + sID_NHOM + "' ";
        //                //    if (sID_CA != "") sDK = sDK + " AND CA = '" + sID_CA + "' ";
        //                //    if (sNgay != "") sDK = sDK + " AND NGAY  = '" + sNgay + "' ";


        //                //    data.DefaultView.RowFilter = sDK;
        //                //}
        //                //catch { }
        //                //if (data.DefaultView.ToTable().Rows.Count == 0)
        //                //{

        //                DataRow newRow = data.NewRow();
        //                newRow.SetField("ID_CN", dr["ID_CN"]);
        //                //newRow.SetField("ID_NHOM", grvKeHoachDiCa.GetFocusedRowCellValue("ID_NHOM"));
        //                //newRow.SetField("CA", grvKeHoachDiCa.GetFocusedRowCellValue("CA"));
        //                //newRow.SetField("TU_NGAY", grvKeHoachDiCa.GetFocusedRowCellValue("TU_NGAY"));
        //                //newRow.SetField("DEN_NGAY", grvKeHoachDiCa.GetFocusedRowCellValue("DEN_NGAY"));
        //                //newRow.SetField("GHI_CHU", grvKeHoachDiCa.GetFocusedRowCellValue("GHI_CHU"));

        //                newRow.SetField("ID_NHOM", data.Rows[MaxRow]["ID_NHOM"]);
        //                newRow.SetField("CA", data.Rows[MaxRow]["CA"]);
        //                newRow.SetField("TU_NGAY", data.Rows[MaxRow]["TU_NGAY"]);
        //                newRow.SetField("DEN_NGAY", data.Rows[MaxRow]["DEN_NGAY"]);
        //                newRow.SetField("GHI_CHU", data.Rows[MaxRow]["GHI_CHU"]);


        //                //newRow.SetField("GIO_BD", grvKeHoachDiCa.GetFocusedRowCellValue("GIO_BD"));
        //                //newRow.SetField("GIO_KT", grvKeHoachDiCa.GetFocusedRowCellValue("GIO_KT"));
        //                //newRow.SetField("COM_CA", grvKeHoachDiCa.GetFocusedRowCellValue("COM_CA"));
        //                //newRow.SetField("PHUT_BD", grvKeHoachDiCa.GetFocusedRowCellValue("PHUT_BD"));
        //                //newRow.SetField("PHUT_KT", grvKeHoachDiCa.GetFocusedRowCellValue("PHUT_KT"));
        //                data.Rows.Add(newRow);
        //                data.AcceptChanges();
        //                //};
        //            }
        //        }
        //        data.DefaultView.RowFilter = "";

        //        grdKeHoachDiCa.DataSource = null;
        //        grdKeHoachDiCa.DataSource = data;
        //        grvCongNhan_FocusedRowChanged(null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    //
        //}

        private void CapNhatNhom()
        {
            try
            {
                grvKeHoachDiCa.CloseEditor();
                grvKeHoachDiCa.UpdateCurrentRow();
                //lấy lướng công nhân được chọn
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
                //dt = dt.AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();
                //lấy lưới làm thêm đã có dữ liệu 
                DataTable dtLT = Commons.Modules.ObjSystems.ConvertDatatable(grdKeHoachDiCa);
                DataTable dt_capnhat = new DataTable();
                //dt_capnhat = ((DataTable)grdLamThem.DataSource).DefaultView.ToTable().Copy();
                DataRow dr = grvKeHoachDiCa.GetDataRow(grvKeHoachDiCa.FocusedRowHandle);
                dt_capnhat = ((DataTable)grdKeHoachDiCa.DataSource).Clone();
                DataRow row = dt_capnhat.NewRow();
                row["ID_CN"] = dr["ID_CN"];
                row["ID_NHOM"] = dr["ID_NHOM"];
                row["CA"] = dr["CA"];
                row["TU_NGAY"] = dr["TU_NGAY"];
                row["DEN_NGAY"] = dr["DEN_NGAY"];
                row["GHI_CHU"] = dr["GHI_CHU"];

                dt_capnhat.Rows.Add(row);



                string stbCN_temP = "grvCongNhanKHDC" + Commons.Modules.UserName;
                string stbLamThemGio_temP = "grvKHDC" + Commons.Modules.UserName;
                string stbLamThemCu_temP = "grvKHDCCu" + Commons.Modules.UserName;

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCN_temP, dt, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemGio_temP, dt_capnhat, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemCu_temP, dtLT, "");

                DateTime dNgay;
                //dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                grdKeHoachDiCa.DataSource = ((DataTable)grdKeHoachDiCa.DataSource).Clone();
                try
                {
                    DataTable dt_temp = new DataTable();
                    dt_temp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCapNhatKHDC", stbCN_temP, stbLamThemGio_temP, stbLamThemCu_temP));
                    //dt_temp.Columns["ID_CDLV"].ReadOnly = false;
                    //dt_temp.Columns["COM_CA"].ReadOnly = false;
                    grdKeHoachDiCa.DataSource = dt_temp;
                    grvCongNhan_FocusedRowChanged(null, null);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(stbCN_temP);
                    Commons.Modules.ObjSystems.XoaTable(stbLamThemGio_temP);
                    Commons.Modules.ObjSystems.XoaTable(stbLamThemCu_temP);
                }

            }
            catch (Exception ex)
            {
            }
            //
        }
    }
}
