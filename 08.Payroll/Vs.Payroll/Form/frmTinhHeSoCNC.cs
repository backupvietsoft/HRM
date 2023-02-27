using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Payroll
{
    public partial class frmTinhHeSoCNC : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int iID_TO = -1;
        public DateTime dNgay;
        public double fTongDoanhThu = 0;
        public frmTinhHeSoCNC()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        private void frmTinhHeSoCNC_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTO, Commons.Modules.ObjSystems.DataTo(-1, -1, false), "ID_TO", "TEN_TO", "TEN_TO");
                cboTO.EditValue = iID_TO;

                datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
                datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.EditFormat.FormatString = "MM/yyyy";
                datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datThang.Properties.Mask.EditMask = "MM/yyyy";
                datThang.EditValue = dNgay.ToString("MM/yyyy");
                LoadData(false);
                EnabelButton(true);
                LoadText();
                //lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDoanhThu") + " : " + fTongDoanhThu.ToString("N0");
            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "tinhheso":
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 6;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                            cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgay.ToString("dd/MM/yyyy"));
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "themsua":
                        {
                            LoadData(true);
                            EnabelButton(false);
                            break;
                        }

                    case "luu":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            else
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            LoadData(false);
                            EnabelButton(true);
                            break;
                        }
                    case "khongluu":
                        {
                            LoadData(false);
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
            catch (Exception ex) { }
        }

        private void LoadData(bool iAdd)
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iAdd;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgay.ToString("dd/MM/yyyy"));
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["LSP_NHOM"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, true, true, true, this.Name);
                grvData.Columns["NHOM"].OptionsColumn.AllowEdit = false;
                grvData.Columns["SO_GIO"].OptionsColumn.AllowEdit = false;
                grvData.Columns["PT_SO_CONG"].OptionsColumn.AllowEdit = false;
                grvData.Columns["LSP_NHOM"].OptionsColumn.AllowEdit = false;

                RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                txtEdit.Properties.DisplayFormat.FormatString = "00.00";
                txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.EditFormat.FormatString = "00.00";
                txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.Mask.EditMask = "00.00";
                txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                grvData.Columns["PT_SO_CONG"].ColumnEdit = txtEdit;
                grvData.Columns["PT_DIEU_CHINH"].ColumnEdit = txtEdit;
                grvData.Columns["LSP_NHOM"].DisplayFormat.FormatType = FormatType.Numeric;
                grvData.Columns["LSP_NHOM"].DisplayFormat.FormatString = "N0";
                grvData.Columns["SO_GIO"].DisplayFormat.FormatType = FormatType.Numeric;
                grvData.Columns["SO_GIO"].DisplayFormat.FormatString = "N2";

                LoadText();
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadText()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdData.DataSource;
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblPhanTramDieuChinh") + " : " + Convert.ToDouble(dt.Compute("Sum(PT_DIEU_CHINH)", "")) + "%     " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDoanhThu") + " : " + fTongDoanhThu.ToString("N0");
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblPhanTramDieuChinh") + " : 0" + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDoanhThu") + " : " + fTongDoanhThu.ToString("N0");
            }
        }
        private bool Savedata()
        {
            try
            {
                string sTB = "sBTTSH" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 7;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgay.ToString("dd/MM/yyyy"));
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
        private void grvData_RowCountChanged(object sender, EventArgs e)
        {

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
        public DXMenuItem MCreateMenuCapNhat(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhat", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(CapNhat));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void CapNhat(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvData.FocusedColumn.FieldName;
                //if (grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName).ToString() == "") return;
                //string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData,grvData), "");
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
                //grdData.DataSource = dt;

                var id_cvsx = grvData.GetFocusedRowCellValue(sCotCN);
                DataTable dt = new DataTable();
                //dt = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData);
                dt = (DataTable)grdData.DataSource;
                dt.AsEnumerable().Where(x => x["CHON"].ToString() == "True").ToList<DataRow>().ForEach(r => r[sCotCN] = (id_cvsx));
                dt.AsEnumerable().Where(x => x["CHON"].ToString() == "True").ToList<DataRow>().ForEach(r => r["CHON"] = false);
                dt.AcceptChanges();
            }
            catch { }
        }
        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (windowsUIButton.Buttons[0].Properties.Visible) return;
                if (grvData.FocusedColumn.FieldName != "ID_CVSX" && grvData.FocusedColumn.FieldName != "NHOM" && grvData.FocusedColumn.FieldName != "HE_SO") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuCapNhat(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }
        #endregion

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
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {
                    //Số hợp đồng lao động
                    if (!KiemDuLieu(grvData, dr, "ID_CVSX", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                    if (!KiemDuLieu(grvData, dr, "NHOM", true, 250, this.Name))
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
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        #endregion

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if (e.Column.FieldName == "PT_DIEU_CHINH")
                {

                    grvData.SetFocusedRowCellValue("LSP_NHOM", (Convert.ToDouble(fTongDoanhThu) * Convert.ToDouble(e.Value)) / 100);
                    LoadText();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void grvData_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;

            DevExpress.XtraGrid.Columns.GridColumn ptDieuChinh = view.Columns["PT_DIEU_CHINH"];

            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);
                if (Convert.ToDouble(dt1.Compute("Sum(PT_DIEU_CHINH)", "")) > 100)
                {
                    e.Valid = false;
                    view.SetColumnError(ptDieuChinh, Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhanTramDieuChinhKhongHopLe"));
                    return;
                }
            }
            catch
            {

            }
        }
        private void EnabelButton(bool visible)
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(iID_DV, dNgay) == 2)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[3].Properties.Visible = false;
                windowsUIButton.Buttons[5].Properties.Visible = false;
                windowsUIButton.Buttons[6].Properties.Visible = false;
                grvData.OptionsBehavior.Editable = false;
            }
            else
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;
                windowsUIButton.Buttons[6].Properties.Visible = !visible;
                grvData.OptionsBehavior.Editable = !visible;
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
    }
}
