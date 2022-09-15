using DevExpress.Utils.Menu;
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
        private int iAdd = 0;
        private string ChuoiKT = "";
        private DataTable dtTemp;
        private int iID_CN = -1;
        private ucCTQLNS ucNS;
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
            Commons.OSystems.SetDateEditFormat(datTuNgay);
            Commons.OSystems.SetDateEditFormat(datDenNgay);
            Commons.Modules.sLoad = "";
            datTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
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
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTuNgay.Text);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDenNgay.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = rdoChonXem.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_CO_THE_DI_LAM"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_NHAN_VIEC"].ReadOnly = false;
                //dt.Columns["ID_DGTN"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DTDH"].ReadOnly = false;
                //dt.Columns["DA_GIOI_THIEU"].ReadOnly = false;
                //dt.Columns["HUY_TUYEN_DUNG"].ReadOnly = false;
                if (grdDSCongNhan.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCongNhan, grvDSCongNhan, dt, false, true, true, true, true, this.Name);
                    grvDSCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvDSCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvDSCongNhan.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grdDSCongNhan.DataSource = dt;
                }
                if (rdoChonXem.SelectedIndex == 0)
                {
                    if (iAdd == 1)
                    {
                        grvDSCongNhan.Columns["CHON"].Visible = true;
                    }
                    else
                    {
                        grvDSCongNhan.Columns["CHON"].Visible = false;
                    }
                    //grvDSCongNhan.OptionsSelection.MultiSelect = false;
                    //grvDSCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }
                if (rdoChonXem.SelectedIndex == 1)
                {
                    grvDSCongNhan.Columns["CHON"].Visible = true;
                    //grvDSCongNhan.OptionsSelection.MultiSelect = true;
                    //grvDSCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_NDG = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboID_NDG.NullText = "";
                cboID_NDG.ValueMember = "ID_CN";
                cboID_NDG.DisplayMember = "HO_TEN";
                //ID_VTTD,TEN_VTTD
                cboID_NDG.DataSource = dtTruongBoPhan();
                cboID_NDG.Columns.Clear();
                cboID_NDG.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
                cboID_NDG.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                cboID_NDG.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");
                cboID_NDG.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_NDG.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_NDG.Columns["ID_CN"].Visible = false;
                grvDSCongNhan.Columns["ID_NDG"].ColumnEdit = cboID_NDG;
                cboID_NDG.BeforePopup += cboID_NDG_BeforePopup;
                cboID_NDG.EditValueChanged += cboID_NDG_EditValueChanged;
                try
                {
                    grvDSCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                }
                catch { }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_DGTN", "TEN_DGTN", "TEN_DGTN", grvDSCongNhan, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), true, "ID_DGTN", this.Name, true);

                //ID_YCTD,MA_YCTD
                if (iID_CN != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_CN));
                    grvDSCongNhan.FocusedRowHandle = grvDSCongNhan.GetRowHandle(index);
                    grvDSCongNhan.ClearSelection();
                    grvDSCongNhan.SelectRow(index);
                }

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
        private void cboID_NDG_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSCongNhan.SetFocusedRowCellValue("ID_LHDLD", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NDG_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = dtTruongBoPhan();
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDanhGiaThuViec", "sBTDSCongNhan" + Commons.Modules.iIDUser, "sBTNDDG" + Commons.Modules.iIDUser, rdoChonXem.SelectedIndex);
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
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            //grvNguonTuyen.OptionsBehavior.Editable = !visible;
            grvDSCongNhan.OptionsBehavior.Editable = !visible;
            grvNDDanhGia.OptionsBehavior.Editable = !visible;
            //datThang.Properties.ReadOnly = !visible;
        }
        private void grvDSCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadLuoiND();
            try
            {
                //if (Commons.Modules.sLoad == "0Load") return;
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
                case "sua":
                    {
                        dtTemp = new DataTable();
                        dtTemp = (DataTable)grdDSCongNhan.DataSource;
                        iAdd = 1;
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
                        grvDSCongNhan.CloseEditor();
                        grvDSCongNhan.UpdateCurrentRow();
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
                        this.Cursor = Cursors.WaitCursor;
                        if (!KiemTraLuoi(dt_CHON))
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        this.Cursor = Cursors.Default;

                        if (!SaveData(iAdd)) return;
                        rdoChonXem.SelectedIndex = 0;
                        rdoChonXem_SelectedIndexChanged(null, null);
                        LoadNDDG();
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        grvDSCongNhan.FocusedRowHandle = n;
                        grvDSCongNhan.SelectRow(n);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        if (rdoChonXem.SelectedIndex == 1)
                        {
                            rdoChonXem.SelectedIndex = 0;
                            rdoChonXem_SelectedIndexChanged(null, null);
                        }
                        else
                        {
                            iAdd = 0;
                            int n = grvDSCongNhan.FocusedRowHandle;
                            LoadData();
                            LoadNDDG();
                            grvDSCongNhan.FocusedRowHandle = n;
                            grvDSCongNhan.SelectRow(n);
                            grvDSCongNhan_FocusedRowChanged(null, null);
                            enableButon(true);
                        }
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
            grvDSCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadData();
            LoadNDDG();
            grvDSCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            LoadNDDG();
            grvDSCongNhan_FocusedRowChanged(null, null);
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
            if (rdoChonXem.SelectedIndex == 0)
            {
                if (Convert.ToBoolean(dtTemp.Rows[e.RowHandle]["KT_HOP_DONG"]) == true)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgHopDongDaKTKhongTheSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    view.SetRowCellValue(e.RowHandle, view.Columns["CHON"], false);
                    view.SetRowCellValue(e.RowHandle, view.Columns["KT_HOP_DONG"], dtTemp.Rows[e.RowHandle]["KT_HOP_DONG"]);
                    view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_NGHI_VIEC"], dtTemp.Rows[e.RowHandle]["NGAY_NGHI_VIEC"]);
                    view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_DANH_GIA"], dtTemp.Rows[e.RowHandle]["NGAY_DANH_GIA"]);
                    view.SetRowCellValue(e.RowHandle, view.Columns["ID_NDG"], dtTemp.Rows[e.RowHandle]["ID_NDG"]);
                    return;
                }
            }
            if (e.Column.FieldName == "KT_HOP_DONG")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["KT_HOP_DONG"], e.Value);
                if (Convert.ToBoolean(e.Value) == false)
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_NGHI_VIEC"], null);
                }
                else
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_NGHI_VIEC"], grvDSCongNhan.GetFocusedRowCellValue("NGAY_DANH_GIA"));
                }
            }
        }

        private void grvDSCongNhan_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

        }

        private void grvDSCongNhan_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSCongNhan_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTuNgay.Text);
            int t = DateTime.DaysInMonth(datTuNgay.DateTime.Year, datTuNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTuNgay.DateTime.Year, datTuNgay.DateTime.Month, t);
            datDenNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void datDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void rdoChonXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdDSCongNhan.DataSource = null;
            switch (rdoChonXem.SelectedIndex)
            {
                case 0:
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
                case 1:
                    {
                        iAdd = 1;
                        LoadData();
                        LoadNDDG();
                        grvDSCongNhan_FocusedRowChanged(null, null);
                        enableButon(false);
                        break;
                    }
            }
        }

        #region kiemTra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvDSCongNhan.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Ngày đánh giá
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {

                    if (!KiemDuLieuNgay(grvDSCongNhan, dr, "NGAY_DANH_GIA", true, this.Name))
                    {
                        errorCount++;
                    }

                    if (!KiemDuLieu(grvDSCongNhan, dr, "ID_NDG", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                }
                if (Convert.ToBoolean(dr["CHON"]) == true && Convert.ToBoolean(dr["KT_HOP_DONG"]) == true)
                {

                    //Ngày ký
                    if (!KiemDuLieuNgay(grvDSCongNhan, dr, "NGAY_NGHI_VIEC", true, this.Name))
                    {
                        errorCount++;
                    }
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
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
        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Trim().Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                        dr.SetColumnError(sCot, sTenKTra);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        #endregion

        private void grvDSCongNhan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //GridView view = sender as GridView;
            //DateTime KT_HOP_DONG;
            //DateTime NgayKT_HD;
            //double MucLuongChinh;
            //try
            //{
            //    var row = view.GetFocusedDataRow();
            //    if (Convert.ToBoolean(grvDSCongNhan.GetFocusedRowCellValue("KT_HOP_DONG")) == true)
            //    {
            //        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgHopDongDaKTKhongTheSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    if (e.Column.FieldName == "ID_LHDLD")
            //    {
            //        //int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_LHDLD")) + ""));
            //        //NgayBD_HD = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_BD_THU_VIEC"));
            //        //NgayKT_HD = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_BD_THU_VIEC")).AddDays(iNgayTV);

            //        //row["NGAY_BD_THU_VIEC"] = NgayBD_HD;
            //        //row["NGAY_KT_THU_VIEC"] = NgayKT_HD;
            //        //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
            //    }

            //    //if (e.Column.FieldName == "ID_BL")
            //    //{
            //    //    MucLuongChinh = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(MUC_LUONG,0) FROM dbo.BAC_LUONG WHERE ID_BL = " + Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_BL")) + ""));
            //    //    row["MUC_LUONG_CHINH"] = MucLuongChinh;
            //    //}
            //}
            //catch { }
        }
        private DataTable dtTruongBoPhan()
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "	SELECT CN.ID_CN, CN.HO + ' ' + CN.TEN HO_TEN FROM dbo.CONG_NHAN CN INNER JOIN dbo.DON_VI DV ON DV.ID_CN = CN.ID_CN  WHERE DV.ID_DV = " + Convert.ToInt32(cboDonVi.EditValue) + " OR " + Convert.ToInt32(cboDonVi.EditValue) + " = -1 UNION SELECT CN.ID_CN, CN.HO + ' ' + CN.TEN HO_TEN FROM dbo.CONG_NHAN CN INNER JOIN dbo.XI_NGHIEP XN ON XN.ID_CN = CN.ID_CN WHERE XN.ID_XN = " + Convert.ToInt32(cboXiNghiep.EditValue) + " OR " + Convert.ToInt32(cboXiNghiep.EditValue) + " = -1 UNION SELECT CN.ID_CN, CN.HO + ' ' + CN.TEN HO_TEN FROM dbo.CONG_NHAN CN INNER JOIN dbo.[TO] T ON T.ID_CN = CN.ID_CN WHERE T.ID_TO = " + Convert.ToInt32(cboTo.EditValue) + " OR " + Convert.ToInt32(cboTo.EditValue) + " = -1"));
                return dt;
            }
            catch { return null; }
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

        //Thong tin nhân sự
        public DXMenuItem MCreateMenuThongTinNS(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinNS", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(ThongTinNS));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void ThongTinNS(object sender, EventArgs e)
        {
            try
            {
                iID_CN = Convert.ToInt32(grvDSCongNhan.GetFocusedRowCellValue("ID_CN"));
                ucNS = new HRM.ucCTQLNS(Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                ucNS.Refresh();
                dataLayoutControl1.Hide();
                btnALL.Visible = false;
                this.Controls.Add(ucNS);
                ucNS.Dock = DockStyle.Fill;
                ucNS.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex) { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucNS.Hide();
            dataLayoutControl1.Show();
            btnALL.Visible = true;
            LoadData();
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
                string sCotCN = grvDSCongNhan.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSCongNhan), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.FocusedColumn.FieldName)));
                    grdDSCongNhan.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
        }
        public DXMenuItem MCreateMenuCapNhatNoiDung(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatNDDanhGia", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatNDDanhGia));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatNDDanhGia(object sender, EventArgs e)
        {
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            string sBTNoiDung = "sBTNoiDungCapNhat" + Commons.Modules.iIDUser;
            string sBTNoiDung_Cu = "sBTNoiDungCu" + Commons.Modules.iIDUser;
            try
            {
                DataTable dt_capnhat = new DataTable();
                dt_capnhat = ((DataTable)grdNDDanhGia.DataSource).DefaultView.ToTable().Copy();


                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung, dt_capnhat, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung_Cu, Commons.Modules.ObjSystems.ConvertDatatable(grdNDDanhGia), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCapNhatNoiDungDG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBTCongNhan, sBTNoiDung, sBTNoiDung_Cu));
                grdNDDanhGia.DataSource = dt;
                grvDSCongNhan_FocusedRowChanged(null, null);
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
            }
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
                    DevExpress.Utils.Menu.DXMenuItem itemNS = MCreateMenuThongTinNS(view, irow);
                    e.Menu.Items.Add(itemNS);

                    if (btnALL.Buttons[2].Properties.Visible || btnALL.Buttons[0].Properties.Visible) return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);

                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatND = MCreateMenuCapNhatNoiDung(view, irow);
                    e.Menu.Items.Add(itemCapNhatND);


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
