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
    public partial class frmTinhLuongCNToCat : DevExpress.XtraEditors.XtraForm
    {
        public int iID_TO = -1;
        public int iID_XN = -1;
        public int iID_DV = -1;
        public DateTime dNgay;
        private bool isAdd = false;
        public double fTongDoanhThu = 0;
        public double fTongSGLV = 0;
        public frmTinhLuongCNToCat()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        private void frmTinhLuongCNToCat_Load(object sender, EventArgs e)
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
                LoadData();
                EnabelButton(isAdd);
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
                    case "chucvusanxuat":
                        {
                            frmEditCHUC_VU_SAN_XUAT frm = new frmEditCHUC_VU_SAN_XUAT();
                            frm.iID_TO = iID_TO;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
                            }
                            else
                            {
                                LoadData();
                            }
                            break;
                        }
                    case "themsua":
                        {
                            isAdd = true;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "In":
                        {
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptDSCNToCat(dNgay);
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdData.DataSource;
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);
                            frm.ShowDialog();
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "copyCVSX":
                        {
                            string sTB = "sBTTinhLuong" + Commons.Modules.iIDUser;
                            try
                            {

                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                                System.Data.SqlClient.SqlConnection conn;
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                DataTable dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                grdData.DataSource = dt;
                                Commons.Modules.ObjSystems.XoaTable(sTB);
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Commons.Modules.ObjSystems.XoaTable(sTB);
                            }
                            break;
                        }
                    case "tinhheso":
                        {
                            frmTinhHeSoCNC frm = new frmTinhHeSoCNC();
                            frm.dNgay = datThang.DateTime;
                            frm.fTongDoanhThu = fTongDoanhThu;
                            frm.iID_DV = iID_DV;
                            frm.iID_XN = iID_XN;
                            frm.iID_TO = iID_TO;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
                            }
                            else
                            {
                                LoadData();
                            }
                            break;
                        }
                    case "luu":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdData.DataSource);
                            //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!KiemTraLuoi(dt_CHON))
                            {
                                return;
                            }
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "khongluu":
                        {
                            isAdd = false;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "xoa":
                        {
                            if (grvData.RowCount == 0) return;
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.LUONG_CONG_NHAN_CAT WHERE ID = " + grvData.GetFocusedRowCellValue("ID_CNC") + "");
                            LoadData();
                            break;
                        }
                }
            }
            catch (Exception ex) { }
        }
        private void EnabelButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = !visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            windowsUIButton.Buttons[9].Properties.Visible = visible;
            windowsUIButton.Buttons[10].Properties.Visible = visible;
            windowsUIButton.Buttons[11].Properties.Visible = visible;
            grvData.OptionsBehavior.Editable = visible;
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = iID_XN;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = isAdd;
                cmd.Parameters.Add("@TONG_DOANH_THU", SqlDbType.Float).Value = fTongDoanhThu;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgay.ToString("dd/MM/yyyy"));
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, true, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CNC"].Visible = false;
                    grvData.Columns["HE_SO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HE_SO"].DisplayFormat.FormatString = "0.0";
                    grvData.Columns["SG_LV_TT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["SG_LV_TT"].DisplayFormat.FormatString = "0.00";
                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["SG_LV_TT"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["CHON"].Visible = false;

                    RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                    txtEdit.Properties.DisplayFormat.FormatString = "00.00";
                    txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    txtEdit.Properties.EditFormat.FormatString = "00.00";
                    txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    txtEdit.Properties.Mask.EditMask = "00.00";
                    txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                    grvData.Columns["HE_SO"].ColumnEdit = txtEdit;
                    grvData.Columns["THANH_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["THANH_TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }

                dt = new DataTable();
                dt = ds.Tables[1].Copy();

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CVSX", "TEN_CVSX", "ID_CVSX", grvData, dt, this.Name);
                cbo.EditValueChanged += cboID_CVSX_EditValueChanged;

                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "NHOM", "NHOM", "NHOM", grvData, dt, this.Name);

                if (isAdd)
                {
                    grvData.OptionsSelection.MultiSelect = true;
                    grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }
                else
                {
                    grvData.OptionsSelection.MultiSelect = false;
                    grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }

                try
                {
                    grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }

                LoadText();
            }
            catch (Exception ex)
            {
            }
        }
        private void cboID_CVSX_EditValueChanged(object sender, EventArgs e)
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

        private bool Savedata()
        {
            string sTB = "sBTTinhLuong" + Commons.Modules.iIDUser;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void LoadText()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdData.DataSource;
                try
                {
                    fTongSGLV = Convert.ToDouble(dt.Compute("Sum(SG_LV_TT)", ""));
                }
                catch
                {
                    fTongSGLV = 0;
                }
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDoanhThu") + " : " + fTongDoanhThu.ToString("N0") + "     " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoGioLVTT") + " : " + fTongSGLV.ToString("N2") + "     " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoCNV") + " : " + grvData.RowCount.ToString();
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDoanhThu") + " : 0     " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoGioLVTT") + " : " + fTongSGLV.ToString("N2") + "     " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoCNV") + " : " + grvData.RowCount.ToString();
            }
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

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
