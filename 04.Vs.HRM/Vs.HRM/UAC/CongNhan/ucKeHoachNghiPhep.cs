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
using DevExpress.CodeParser;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DevExpress.XtraPrinting.Native;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using DevExpress.XtraEditors.Designer.Utils;

namespace Vs.HRM
{
    public partial class ucKeHoachNghiPhep : DevExpress.XtraEditors.XtraUserControl
    {
        private int iIDCN_Temp = -1;
        private string ChuoiKT = "";
        private bool bChanKiemTT = false; // Chặn kiểm tồn tại // true chặn false ko chặn
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

        RepositoryItemLookUpEdit cboLDVGrv;
        DataTable dtCboLDV;
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

                enableButon(true);
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
                Commons.Modules.sLoad = "0Load";
                dateNam.EditValue = DateTime.Now;
                Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
                Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboTINH_TRANG, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), "ID_TTD", "TEN_TT_DUYET", "TEN_TT_DUYET", "");
                cboTINH_TRANG.EditValue = 2;
                LoadGrdCongNhan(false);
                LoadGrdKHNP();
                Commons.Modules.ObjSystems.DeleteAddRow(grvKHNP);
                Commons.Modules.sLoad = "";
                grvDSCN_FocusedRowChanged(null, null);
                radTinHTrang.SelectedIndex = 0;
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.OSystems.SetDateEditFormat(datNVao);
            }
            catch { }
        }
        public void CheckDuplicateKHNP(GridView grid, DataTable GridDataTable, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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
                            return;
                        }
                        else
                        {
                            count++;
                            if (count == 2)
                            {
                                r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                                grid.SetColumnError(grid.Columns["TU_NGAY"], "Ngày nghỉ bị trùng, xin vui lòng kiểm tra lại.");
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
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "NB" ? "spGetCongNhanNghiPhep_NB" : "spGetCongNhanNghiPhep", cboSearch_DV.EditValue, cboSearch_XN.EditValue, cboSearch_TO.EditValue, dateNam.DateTime.Year, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
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
                grvDSCN.Columns["MS_CN"].Visible = false;
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
            catch (Exception ex)
            {
            }
           
        }
        private void LoadGrdKHNP()
        {
            try
            {
                string sBTCN = "sBTCN" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCN, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN), "");

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListKeHoachNghiPhep", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToInt32(dateNam.DateTime.Year);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCN;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["NGHI_NUA_NGAY"].ReadOnly = false;
                dt.Columns["ID_KHNP"].ReadOnly = false;
                if (grdKHNP.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdKHNP, grvKHNP, dt, false, true, false, false, true, this.Name);
                    grvKHNP.Columns["ID_CN"].Visible = false;
                    grvKHNP.Columns["TEN_LDV"].OptionsColumn.AllowFocus = false;
                    grvKHNP.Columns["TEN_LDV"].OptionsColumn.ReadOnly = true;
                }
                else
                {
                    grdKHNP.DataSource = dt;
                }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvKHNP, Commons.Modules.ObjSystems.DataLyDoVang(false, -1), "ID_LDV", this.Name);


                cboLDVGrv = new RepositoryItemLookUpEdit();
                dtCboLDV = new DataTable();
                dtCboLDV = Commons.Modules.ObjSystems.DataLyDoVang(false, -1);
                //dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CD, T1.MaQL, T1.TEN_CD_QT AS TEN_CD FROM QUI_TRINH_CONG_NGHE_CHI_TIET T1 LEFT JOIN PHIEU_CONG_DOAN T2 ON T1.ID_CD = T2.ID_CD"));
                cboLDVGrv.NullText = "";
                cboLDVGrv.ValueMember = "ID_LDV";
                cboLDVGrv.DisplayMember = "MS_LDV";
                cboLDVGrv.DataSource = dtCboLDV;
                cboLDVGrv.Columns.Clear();
                //TSua(false);

                cboLDVGrv.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_LDV"));
                cboLDVGrv.Columns["ID_LDV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_LDV");
                cboLDVGrv.Columns["ID_LDV"].Visible = false;

                cboLDVGrv.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_LDV"));
                cboLDVGrv.Columns["MS_LDV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_LDV");

                cboLDVGrv.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LDV"));
                cboLDVGrv.Columns["TEN_LDV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LDV");

                cboLDVGrv.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboLDVGrv.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                //cboLDV.ShowLines
                grvKHNP.Columns["ID_LDV"].ColumnEdit = cboLDVGrv;
                cboLDVGrv.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                cboLDVGrv.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;


                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);

                grvKHNP.Columns["NGAY_VAO_LAM_LAI"].Visible = false;
                grvKHNP.Columns["THANG_KTP"].Visible = false;
                grvKHNP.Columns["NGAY_NOP_DON"].Visible = false;
                grvKHNP.Columns["GIO_NOP_DON"].Visible = false;
                grvKHNP.Columns["LAN_NGHI"].Visible = false;
                grvKHNP.Columns["NGHI_HV"].Visible = false;

                if (Commons.Modules.KyHieuDV == "NB")
                {

                    grvKHNP.Columns["NGHI_NUA_NGAY"].Visible = false;
                    grvKHNP.Columns["NGAY_VAO_LAM_LAI"].Visible = true;
                    grvKHNP.Columns["THANG_KTP"].Visible = true;
                    grvKHNP.Columns["NGAY_NOP_DON"].Visible = true;
                    grvKHNP.Columns["GIO_NOP_DON"].Visible = true;
                    grvKHNP.Columns["LAN_NGHI"].Visible = true;
                    grvKHNP.Columns["NGHI_HV"].Visible = true;


                    grvKHNP.Columns["NGAY_NOP_DON"].ColumnEdit = dEditN;
                    grvKHNP.Columns["NGAY_NOP_DON"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grvKHNP.Columns["NGAY_NOP_DON"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    RepositoryItemTimeEdit repositoryItemTimeEdit1 = repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
                    repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                    repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                    repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

                    repositoryItemTimeEdit1.NullText = "00:00";
                    repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
                    repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

                    grvKHNP.Columns["GIO_NOP_DON"].ColumnEdit = repositoryItemTimeEdit1;
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
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdKHNP.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvDSCN.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                if (sIDCN != "-1")
                {
                    sDK = " ID_CN = '" + sIDCN + "' ";
                }
                else
                {
                    sDK = "1 = 0";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
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
            LoadGrdKHNP();
            grvDSCN_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan(false);
            LoadGrdKHNP();
            grvDSCN_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan(false);
            LoadGrdKHNP();
            grvDSCN_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            LoadGrdKHNP();
        }

        private bool UpdateKeHoachNghiPhep()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabKHNP" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdKHNP), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateKHNP", "tabKHNP" + Commons.Modules.iIDUser));
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateKHNP", "tabKHNP" + Commons.Modules.iIDUser);
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                    return false;
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                Commons.Modules.ObjSystems.XoaTable("tabKHNP" + Commons.Modules.iIDUser);
                return true;
            }
            catch
            {
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                Commons.Modules.ObjSystems.XoaTable("tabKHNP" + Commons.Modules.iIDUser);
                return false;
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
                        //if (iKiemTinhTrang() == 1)
                        //{
                        //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaDuyetKhongTheXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        //    return;
                        //}
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
                        try
                        {
                            grvKHNP.CloseEditor();
                            grvKHNP.UpdateCurrentRow();
                            int idcn = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                            if (grvKHNP.HasColumnErrors) return;
                            if (!UpdateKeHoachNghiPhep())
                            {
                                return;
                            }
                            UpdateTinhTrangNghiPhep(-1);
                            LoadGrdCongNhan(false);
                            LoadGrdKHNP();
                            grvDSCN_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvKHNP);
                            enableButon(true);
                        }
                        catch (Exception ex)
                        {
                        }
                        break;
                    }
                case "khongluu":
                    {
                        try
                        {
                            ((DataTable)grdKHNP.DataSource).Clear();
                            LoadGrdKHNP();
                            grvDSCN_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvKHNP);
                            enableButon(true);
                        }
                        catch
                        {
                        }
                     
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
                if (bChanKiemTT == true) return;
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

                // kiểm trống
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

                // kiểm lớn hơn nhỏ hơn
                if (Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, tungay)) > Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, denngay)))
                {
                    e.Valid = false;
                    View.SetColumnError(tungay, "Từ ngày phải nhỏ hơn đến ngày"); return;
                }
                if (View.FocusedColumn == View.Columns["DEN_NGAY"])
                {
                    if (Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, denngay)) < Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, tungay)))
                    {
                        e.Valid = false;
                        View.SetColumnError(denngay, "Đến ngày phải lớn hơn từ ngày"); return;
                    }
                }

                if (kiemTrung() == 0)
                {
                    e.Valid = false;
                    View.SetColumnError(tungay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblDuLieuTrung"));
                    View.SetColumnError(denngay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblDuLieuTrung"));
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "lblDuLieuTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
                View.ClearColumnErrors();
            }
            catch { }

        }
        private void XoaKHNP()
        {
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

            grvKHNP.OptionsBehavior.Editable = !visible;
            //grdDSCN.Enabled = visible;
            dateNam.Enabled = visible;
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
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdCongNhan(false);
            LoadGrdKHNP();
            grvDSCN_FocusedRowChanged(null, null);
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
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = dtCboLDV.AsEnumerable().Where(x => x["ID_LDV"].ToString().Equals(e.Value.ToString())).CopyToDataTable();
                    }
                    catch { }
                    view.SetRowCellValue(e.RowHandle, view.Columns["TEN_LDV"], dt.Rows[0]["TEN_LDV"]);
                    view.SetRowCellValue(e.RowHandle, view.Columns["TINH_TRANG_DUYET"], 2);
                }
                if (e.Column.Name == "colTU_NGAY")
                {
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"]) as DateTime?;

                    if (Commons.Modules.KyHieuDV == "DM")
                    {
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
                    }
                    if (toDate == null)
                    {
                        toDate = fromDate;
                        bChanKiemTT = true;
                        view.SetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"], fromDate);
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])).AddDays(TinhNgayVaoLam(Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])))));
                        bChanKiemTT = false;
                    }
                    if (fromDate != null && toDate != null)
                    {
                        double SoGio = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)) * Commons.Modules.iGio;
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], SoGio);
                    }
                    grvKHNP.UpdateCurrentRow();
                }
                if (e.Column.Name == "colDEN_NGAY")
                {
                    if (bChanKiemTT == true) return;
                    DateTime? fromDate = view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"]) as DateTime?;
                    DateTime? toDate = view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"]) as DateTime?;

                    if (Commons.Modules.KyHieuDV == "DM")
                    {
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
                    }
                    if (fromDate != null && toDate != null)
                    {
                        double SoGio = Commons.Modules.ObjSystems.TinhSoNgayTruLeChuNhat(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate)) * Commons.Modules.iGio;
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], SoGio);
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])).AddDays(TinhNgayVaoLam(Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])))));
                    }
                    grvKHNP.UpdateCurrentRow();
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
                grvKHNP.SetFocusedRowCellValue("ID_CN", Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN")));
                grvKHNP.SetFocusedRowCellValue("ID_KHNP", 0);
            }
            catch { }
        }
        private void UpdateTinhTrangNghiPhep(int ID_CN)
        {
            try
            {
                string sBT = "sBT" + Commons.Modules.iIDUser;
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

                if (e.Column.Name == "colTU_NGAY")
                {
                    // kiểm lớn hơn nhỏ hơn
                    if (view.FocusedColumn == view.Columns["TU_NGAY"])
                    {
                        if (Convert.ToDateTime(e.Value) > Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])))
                        {
                            view.SetColumnError(view.Columns["TU_NGAY"], "Từ ngày phải bé hơn đến ngày");
                            return;
                        }
                        else
                        {
                            view.ClearColumnErrors();
                        }
                    }
                }

                if (e.Column.Name == "colDEN_NGAY")
                {
                    // kiểm lớn hơn nhỏ hơn
                    if (view.FocusedColumn == view.Columns["DEN_NGAY"])
                    {
                        if (Convert.ToDateTime(e.Value) < Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["TU_NGAY"])))
                        {
                            view.SetColumnError(view.Columns["DEN_NGAY"], "Đến ngày phải lớn hơn từ ngày");
                            return;
                        }
                        else
                        {
                            view.ClearColumnErrors();
                        }
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
                DataTable dt = new DataTable();
                dt = Commons.Modules.ObjSystems.ConvertDatatable(grvKHNP);

                //dt = dt.AsEnumerable().Where(x => x["ID_CN"].Equals(ID_CN)).CopyToDataTable();
                DataView dtv = (DataView)grvKHNP.DataSource;
                DataTable tempt = dtv.ToTable();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, btKHNP, tempt, "");

                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        DataSet ds = new DataSet();
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraKHNP", conn);
                        cmd.Parameters.Add("@sBTam", SqlDbType.NVarChar, 50).Value = btKHNP;
                        cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = dt.Rows[i]["TU_NGAY"];
                        cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = dt.Rows[i]["DEN_NGAY"];
                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                        cmd.CommandType = CommandType.StoredProcedure;
                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        adp.Fill(ds);
                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0].Copy();
                        if (Convert.ToInt32(dt1.Rows[0][0]) > 1)
                        {
                            return 0;
                        }
                        dt1 = new DataTable();
                        dt1 = ds.Tables[1].Copy();
                        if (Convert.ToInt32(dt1.Rows[0][0]) > 1)
                        {
                            return 0;
                        }
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        private void searchControl1_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdDSCN.DataSource;
            //dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvTo);
            String sMSCN;
            try
            {
                string sDK = "";
                sMSCN = "";
                sDK = "MS_CN_INT = '" + Convert.ToInt32(searchControl1.EditValue) + "'";
                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch (Exception ex)
            {
                dtTmp.DefaultView.RowFilter = "";
            }
            grvDSCN_FocusedRowChanged(null, null);
        }
    }
}