using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using NPOI.SS.Formula.Functions;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using static NPOI.HSSF.Util.HSSFColor;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraCharts.Native;
using NPOI.HSSF.Record.Chart;

namespace Vs.Payroll
{
    public partial class frmPhieuCongDoanPacking : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_CHUYEN_SD = -1;
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int IID_TO = -1;
        public DateTime dNgay;
        private DataTable dtCN;
        private int iAdd = 0;
        public frmPhieuCongDoanPacking()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmPhieuCongDoanPacking_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadChuyen();
            cboTo.EditValue = iID_CHUYEN_SD;
            datNgay.DateTime = dNgay;
            datNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datNgay.Properties.DisplayFormat.FormatString = "MM/yyyy";
            datNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datNgay.Properties.EditFormat.FormatString = "MM/yyyy";
            datNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            datNgay.Properties.Mask.EditMask = "MM/yyyy";
            LoadData();
            Commons.Modules.ObjSystems.DeleteAddRow(grvData);

            EnabelButton(true);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        iAdd = 1;
                        LoadData();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        EnabelButton(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvData.RowCount == 0) return;
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        if (grvData.RowCount == 0)
                            return;
                        grvData.CloseEditor();
                        grvData.UpdateCurrentRow();
                        DataTable dt_Scoure = new DataTable();
                        dt_Scoure = ((DataTable)grdData.DataSource);
                        //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                        this.Cursor = Cursors.WaitCursor;
                        if (!KiemTraLuoi(dt_Scoure))
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        this.Cursor = Cursors.Default;
                        if (!SaveData())
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                            return;
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgLuuThanhCong"), Commons.Form_Alert.enmType.Success);
                        }
                        iAdd = 0;
                        LoadData();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        EnabelButton(true);
                        break;
                    }
                case "khongluu":
                    {
                        iAdd = 0;
                        LoadData();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        EnabelButton(true);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanPacking", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = IID_TO;
                    cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = cboTo.EditValue;
                    cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datNgay.DateTime;
                    cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iAdd;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    dt.Columns["ID_PCD_PACKING"].ReadOnly = false;
                    if (grdData.DataSource == null)
                    {
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, "");
                        grvData.Columns["ID_CN"].Visible = false;
                        grvData.Columns["ID_CHUYEN_SD"].Visible = false;
                        grvData.Columns["ID_PCD_PACKING"].Visible = false;
                        grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                        grvData.Columns["SAN_LUONG"].OptionsColumn.AllowEdit = false;
                        grvData.Columns["THANH_TIEN"].OptionsColumn.AllowEdit = false;
                        grvData.Columns["DON_GIA"].OptionsColumn.AllowEdit = false;


                        grvData.Columns["THANH_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THANH_TIEN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["SAN_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["SAN_LUONG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["SO_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";
                    }
                    else
                    {
                        grdData.DataSource = dt;
                    }

                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();

                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_DT", "TEN_DT", "ID_DT", grvData, dt, this.Name);
                    cbo.EditValueChanged += cboID_DT_EditValueChanged;

                    dt = new DataTable();
                    dt = ds.Tables[2].Copy();
                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_ORD", "TEN_HH", "ID_ORD", grvData, dt, this.Name);
                    cbo1.EditValueChanged += cboID_ORD_EditValueChanged;

                    dtCN = new DataTable();
                    dtCN = ds.Tables[3].Copy();

                }
                catch (Exception ex)
                {
                }
            }
            catch { }
        }

        private void cboID_DT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvData.SetFocusedRowCellValue("ID_CVSX", Convert.ToInt64((dataRow.Row[0])));
                grvData.SetFocusedRowCellValue("NHOM", Convert.ToInt32(dataRow.Row[2]));
            }
            catch { }

        }

        private void cboID_ORD_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvData.SetFocusedRowCellValue("ID_ORD", Convert.ToDouble((dataRow.Row[0])));
                grvData.SetFocusedRowCellValue("DON_GIA", Convert.ToDouble((dataRow.Row[2])));
                grvData.SetFocusedRowCellValue("SAN_LUONG", Convert.ToDouble((dataRow.Row[3])));
            }
            catch { }

        }
        private void LoadChuyen()
        {
            try
            {
                string sSql = "SELECT [TO].ID_TO, [TO].TEN_TO FROM dbo.[TO] INNER JOIN dbo.XI_NGHIEP XN ON XN.ID_XN = [TO].ID_XN WHERE [TO].ID_LOAI_CHUYEN IN (1,2,3,4,5,6,7) AND (XN.ID_DV = " + iID_DV + " OR " + iID_DV + " = -1) ORDER BY [TO].STT_TO";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");
                searchLookUpEdit1View.Columns[0].Caption = "STT Chuyền";
                searchLookUpEdit1View.Columns[1].Caption = "Tên Chuyền";
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            }
            catch { }
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvData.ClearColumnErrors();
            GridView view = sender as GridView;


            if (view.FocusedColumn.FieldName == "MS_CN")
            {
                DataTable dt = new DataTable();
                try
                {
                    Commons.Modules.ObjSystems.DataCongNhan(false);
                    dt = dtCN.AsEnumerable().Where(x => x["MS_CN_4"].ToString().Equals(e.Value.ToString())).CopyToDataTable();
                }
                catch
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Không có mã nhân viên" : "Not code employes";
                    view.SetColumnError(view.Columns["MS_CN"], e.ErrorText);
                    return;
                }

                try
                {
                    grvData.SetFocusedRowCellValue("ID_CN", dt.Rows[0]["ID_CN"]);
                    grvData.SetFocusedRowCellValue("HO_TEN", dt.Rows[0]["HO_TEN"]);
                    grvData.SetFocusedRowCellValue("MS_CN", dt.Rows[0]["MS_CN"]);
                }
                catch
                {
                }
            }
            if (view.FocusedColumn.FieldName == "SO_LUONG")
            {
                if (Convert.ToInt32(e.Value == "" ? 0 : e.Value) == 0)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblKhongDuocNhoHon0");
                    view.SetColumnError(view.Columns["SO_LUONG"], e.ErrorText);
                    return;
                }
            }
        }

        private void grvData_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvData.SetFocusedRowCellValue("ID_CHUYEN_SD", cboTo.EditValue);
                grvData.SetFocusedRowCellValue("CHON", false);
                grvData.SetFocusedRowCellValue("ID_PCD_PACKING", 0);
            }
            catch { }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "SO_LUONG")
                {
                    grvData.SetFocusedRowCellValue("THANH_TIEN", (Convert.ToDouble(e.Value) * Convert.ToDouble(grvData.GetFocusedRowCellValue("DON_GIA"))));
                }
            }
            catch { }
        }

        #region kiemTra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                if (Convert.ToString(dr["MS_CN"]) != "")
                {
                    if (!KiemDuLieu(grvData, dr, "ID_ORD", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                    if (!KiemDuLieuSo(grvData, dr, "SO_LUONG", grvData.Columns["SO_LUONG"].FieldName.ToString(), 0, 0, true, this.Name))
                    {
                        errorCount++;
                    }
                    if (!KiemDuLieu(grvData, dr, "MS_CN", true, 250, this.Name))
                    {
                        errorCount++;
                    }

                    try
                    {
                        if (dtSource.AsEnumerable().Where(x => x.Field<Int64>("ID_ORD").Equals(Convert.ToInt64(dr["ID_ORD"])) && x["ID_CN"].Equals(Convert.ToInt64(dr["ID_CN"]))).CopyToDataTable().Rows.Count > 1)
                        {
                            string sTenKTra = "";
                            sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                            dr.SetColumnError("ID_ORD", sTenKTra);
                            dr.SetColumnError("MS_CN", sTenKTra);
                            dr.SetColumnError("HO_TEN", sTenKTra);
                            errorCount++;
                        }
                    }
                    catch
                    {

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
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
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
                            if (DLKiem == 0)
                            {
                                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgSoLuongKhongNhoHon") + GTSoSanh.ToString());
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
        #endregion

        private bool SaveData()
        {
            string sBTPCD = "sBTPhieuCDPacking" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTPCD, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanPacking", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = IID_TO;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = cboTo.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datNgay.DateTime;
                cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = "INS";
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTPCD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.XoaTable(sBTPCD);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTPCD);
                return false;
            }
        }
        private void DeleteData()
        {
            string sBTPCD = "sBTPhieuCDPacking" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTPCD, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanPacking", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = IID_TO;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = cboTo.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datNgay.DateTime;
                cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = "DEL";
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTPCD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaKhongThanhCong") + dt.Rows[0][1].ToString(), Commons.Form_Alert.enmType.Error);
                }
                else
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaThanhCong"), Commons.Form_Alert.enmType.Success);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTPCD);

            }
        }
        private void EnabelButton(bool visible)
        {
            try
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = !visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;

                grvData.OptionsBehavior.Editable = !visible;
            }
            catch { }
        }
    }
}