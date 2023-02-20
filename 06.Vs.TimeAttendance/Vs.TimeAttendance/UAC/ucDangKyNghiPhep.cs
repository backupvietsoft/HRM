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
using System.Drawing;
using DevExpress.Utils.Menu;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Vs.TimeAttendance
{
    public partial class ucDangKyNghiPhep : DevExpress.XtraEditors.XtraUserControl
    {
        private int iIDCN_Temp = -1;
        public static ucDangKyNghiPhep _instance;
        public static ucDangKyNghiPhep Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKyNghiPhep();
                return _instance;
            }
        }
        public ucDangKyNghiPhep()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucDangKyNghiPhep_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(100);
                Commons.OSystems.SetDateEditFormat(datTuNgay);
                Commons.OSystems.SetDateEditFormat(datDenNgay);

                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
                Commons.Modules.sLoad = "0Load";
                datTuNgay.DateTime = DateTime.Now;
                datTuNgay.EditValue = Convert.ToDateTime(datTuNgay.EditValue).AddDays((-datTuNgay.DateTime.Day) + 1);
                datDenNgay.EditValue = Convert.ToDateTime(datTuNgay.EditValue).AddDays((-datTuNgay.DateTime.Day)).AddMonths(+1);

                Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
                Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
                LoadGrdCongNhan();
                Commons.Modules.sLoad = "";
                enableButon(true, true);
            }
            catch { }
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
        private void LoadGrdCongNhan()
        {

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyNghiPhep", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboSearch_DV.EditValue;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboSearch_XN.EditValue;
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboSearch_TO.EditValue;
            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTuNgay.DateTime;
            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDenNgay.DateTime;
            cmd.Parameters.Add("@bTinhTrang", SqlDbType.Int).Value = rdo_TinhTrang.SelectedIndex;
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
            if (grdDSCN.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, true, true, true, this.Name);
            }
            else
            {
                grdDSCN.DataSource = dt;
            }
            grvDSCN.Columns["ID_CN"].Visible = false;

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
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyNghiPhep", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboSearch_DV.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboSearch_XN.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboSearch_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDenNgay.DateTime;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN"));
                cmd.Parameters.Add("@bTinhTrang", SqlDbType.Int).Value = rdo_TinhTrang.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (grdDKNP.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKNP, grvDKNP, dt, true, true, false, false, true, this.Name);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LDV", "TEN_LDV", grvDKNP, Commons.Modules.ObjSystems.DataLyDoVang(false, -1), "ID_LDV", this.Name);

                    grvDKNP.Columns["GHI_CHU_NDK"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["TINH_TRANG_DUYET"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["HO_TEN_ND"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["GHI_CHU_ND"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["ID_KHNP"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["ID_DKNP"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["NGAY_DK"].OptionsColumn.AllowEdit = false;
                    grvDKNP.Columns["NGAY_DUYET"].OptionsColumn.AllowEdit = false;


                    RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                    Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);

                    grvDKNP.Columns["TU_NGAY"].ColumnEdit = dEditN;
                    grvDKNP.Columns["DEN_NGAY"].ColumnEdit = dEditN;

                    grvDKNP.Columns["TU_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grvDKNP.Columns["TU_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    grvDKNP.Columns["DEN_NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grvDKNP.Columns["DEN_NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    grvDKNP.Columns["ID_KHNP"].Visible = false;
                    grvDKNP.Columns["ID_DKNP"].Visible = false;
                    grvDKNP.Columns["CHINH_SUA"].Visible = false;
                    grvDKNP.Columns["ID_CN"].Visible = false;

                    RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TTD", "TEN_TT_DUYET", "TINH_TRANG_DUYET", grvDKNP, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), this.Name);
                }
                else
                {
                    grdDKNP.DataSource = dt;
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void grvDSCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadGrdKHNP();
        }
        private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
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
                        enableButon(false, true);
                        break;
                    }
                case "xoa":
                    {
                        if (iKiemTinhTrang() == 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaDuyetKhongTheXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCapNhatPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                        break;
                    }
                case "luu":
                    {
                        string btKHNP = "TMPPRORUN" + Commons.Modules.UserName;
                        try
                        {
                            grvDKNP.CloseEditor();
                            grvDKNP.UpdateCurrentRow();
                            if (kiemTrung() == 0)
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "lblDuLieuTrung"), Commons.Form_Alert.enmType.Error);
                                return;
                            }
                            UpdateDangKyNghiPhep();
                            grvDKNP.RefreshData();
                            enableButon(true, true);
                            UpdateTinhTrangNghiPhep(Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN")));
                            LoadGrdCongNhan();
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
                        ((DataTable)grdDKNP.DataSource).Clear();
                        grvDSCN_FocusedRowChanged(null, null);
                        enableButon(true, true);
                        //DeleteAddRow(grvKHNP);
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
        private int kiemTrung()
        {
            string btKHNP = "TMPPRORUN" + Commons.Modules.UserName;
            try
            {
                DataTable dt = new DataTable();
                dt = Commons.Modules.ObjSystems.ConvertDatatable(grvDKNP);

                //dt = dt.AsEnumerable().Where(x => x["ID_CN"].Equals(ID_CN)).CopyToDataTable();
                DataView dtv = (DataView)grvDKNP.DataSource;
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
                    return 0;
                }
            }
            catch
            {
                return 0;
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

                View.ClearColumnErrors();
            }
            catch { }

        }
        private void enableButon(bool visible, bool eDit)
        {
            if (eDit == true)
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = !visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;

                grdDSCN.Enabled = visible;
                grvDKNP.OptionsBehavior.Editable = !visible;
                cboSearch_DV.Enabled = visible;
                cboSearch_XN.Enabled = visible;
                cboSearch_TO.Enabled = visible;
                datTuNgay.Enabled = visible;
                datDenNgay.Enabled = visible;
                rdo_TinhTrang.Enabled = visible;
            }
            else
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[5].Properties.Visible = false;
            }
        }
        private void grdKHNP_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCapNhatPhep"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }
        }
        private void dateNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadGrdKHNP();
        }

        private void UpdateDangKyNghiPhep()
        {
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabDKNP" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvDKNP), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyNghiPhep", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "tabDKNP" + Commons.Modules.iIDUser;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }
                Commons.Modules.ObjSystems.XoaTable("tabDKNP" + Commons.Modules.iIDUser);
                //LoadGrdKHNP();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable("tabKHNP" + Commons.Modules.iIDUser);
            }
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
                    view.SetFocusedRowCellValue("CHINH_SUA", true);
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

                    view.SetFocusedRowCellValue("CHINH_SUA", true);
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
                        //view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_VAO_LAM_LAI"], Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])).AddDays(TinhNgayVaoLam(Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["DEN_NGAY"])))));
                    }
                    view.SetFocusedRowCellValue("CHINH_SUA", true);
                }
                if (e.Column.Name == "colNGHI_NUA_NGAY")
                {
                    view.SetFocusedRowCellValue("CHINH_SUA", true);
                }
                if (e.Column.Name == "colSO_GIO")
                {
                    view.SetFocusedRowCellValue("CHINH_SUA", true);
                }
            }
            catch (Exception ex)
            {

            }
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
                        }
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO"], (SoGio / 2));
                    }
                }

                if (e.Column.Name == "colTINH_TRANG_DUYET")
                {
                    if (iKiemTinhTrang() == 1)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaDuyetKhongTheSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }
                }
            }
            catch { }
        }
        private int iKiemTinhTrang()
        {
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(TINH_TRANG_DUYET,2) FROM dbo.KE_HOACH_NGHI_PHEP WHERE ID_KHNP =" + grvDKNP.GetFocusedRowCellValue("ID_KHNP") + ""));
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

        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") { return; }
            LoadGrdCongNhan();
            LoadGrdKHNP();
        }

        private void datDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") { return; }
            LoadGrdCongNhan();
            LoadGrdKHNP();
        }

        private void grvDKNP_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvDKNP.GetRowCellValue(e.RowHandle, grvDKNP.Columns["CHINH_SUA"].FieldName)) == false) return;
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.SeaShell;
                e.HighPriority = true;
                //e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
            }
            catch { }
        }

        private void rdo_TinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_TinhTrang.SelectedIndex)
                {
                    case 0:
                        {
                            enableButon(true, true);
                            break;
                        }
                    case 1:
                        {
                            enableButon(true, false);
                            break;
                        }
                    case 2:
                        {
                            enableButon(true, false);
                            break;
                        }
                }
                LoadGrdCongNhan();
                LoadGrdKHNP();
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
        //Duyệt
        public DXMenuItem MDuyet(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblDuyet", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(Duyet));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void Duyet(object sender, EventArgs e)
        {
            UpdateDuyetNghiLam(1);
        }
        //Không duyệt
        public DXMenuItem MKhongDuyet(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblKhongDuyet", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(KhongDuyet));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void KhongDuyet(object sender, EventArgs e)
        {
            UpdateDuyetNghiLam(2);
        }
        private void UpdateDuyetNghiLam(int iTinhTrangDuyet)
        {
            string sBT = "sBTDKNP" + Commons.Modules.iIDUser;
            try
            {
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                args.Caption = "Nhập ý kiến";
                args.Prompt = "Nhập ý kiến";
                args.DefaultButtonIndex = 0;

                MemoEdit editor = new MemoEdit();
                editor.EditValue = "";
                args.Editor = editor;

                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdDKNP, grvDKNP), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDangKyNghiPhep", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@iCot1", SqlDbType.Int).Value = iTinhTrangDuyet;
                cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = result;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }
                LoadGrdCongNhan();
                LoadGrdKHNP();
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuyetThanhCong"), Commons.Form_Alert.enmType.Success);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (rdo_TinhTrang.SelectedIndex != 2) return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemDuyet = MDuyet(view, irow);
                    e.Menu.Items.Add(itemDuyet);
                    DevExpress.Utils.Menu.DXMenuItem itemKhongDuyet = MKhongDuyet(view, irow);
                    e.Menu.Items.Add(itemKhongDuyet);


                }
            }
            catch
            {
            }
        }

        #endregion
    }
}