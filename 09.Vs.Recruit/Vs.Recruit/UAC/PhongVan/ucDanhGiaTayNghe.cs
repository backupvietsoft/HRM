using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
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

namespace Vs.Recruit
{
    public partial class ucDanhGiaTayNghe : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        public AccordionControl accorMenuleft;
        private long iID_UV = -1;
        private ucCTQLUV ucUV;
        private HRM.ucCTQLNS ucNS;
        private int iLoai = 0; // 1 : link ứng viên, 2 : link nhân sự
        public ucDanhGiaTayNghe()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucDanhGiaTayNghe_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
                int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
                DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
                datDNgay.EditValue = secondDateTime;
                //datTNgay.EditValue = DateTime.Now;
                LoadCbo();
                LoadData();
                radioGroup1_SelectedIndexChanged(null, null);
                Commons.Modules.sLoad = "";
                EnabelButton(true);

            }
            catch (Exception ex)
            {
            }
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@dCot1", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@dCot2", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboLoc.EditValue;
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = cboYCTD.EditValue;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = radioGroup1.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdDSUngVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);

                    grvDSUngVien.Columns["ID_UV"].Visible = false;
                    grvDSUngVien.Columns["TINH_TRANG_DG"].Visible = false;
                    grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["THUONG_TAY_NGHE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDSUngVien.Columns["THUONG_TAY_NGHE"].DisplayFormat.FormatString = "n0";

                    if (Convert.ToInt32(cboLoc.EditValue) == 1) // may
                    {
                        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboDGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                        cboDGTN.NullText = "";
                        cboDGTN.ValueMember = "ID_DGTN";
                        cboDGTN.DisplayMember = "TEN_DGTN";
                        //ID_VTTD,TEN_VTTD
                        cboDGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                        cboDGTN.Columns.Clear();
                        cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                        cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                        cboDGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                        cboDGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        cboDGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        cboDGTN.Columns["ID_DGTN"].Visible = false;
                        grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboDGTN;
                        cboDGTN.BeforePopup += cboDGTN_BeforePopup;
                        cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;

                        DataTable dtCN = new DataTable();
                        dtCN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                        RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                        Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CN", "HO_TEN", "ID_CN", grvDSUngVien, dtCN, this.Name);
                    }
                    else
                    {

                    }
                }
                else
                {
                    grdDSUngVien.DataSource = dt;
                }
                if (btnALL.Buttons[0].Properties.Visible == true)
                {
                    grvDSUngVien.Columns["CHON"].Visible = false;
                }
                else
                {
                    grvDSUngVien.Columns["CHON"].Visible = true;
                }
                if (Convert.ToInt32(cboLoc.EditValue) == 2)
                {
                    grvDSUngVien.Columns["ID_DGTN"].Visible = false;
                    grvDSUngVien.Columns["KQ_GIAY"].Visible = false;
                    grvDSUngVien.Columns["KQ_VAI"].Visible = false;
                    grvDSUngVien.Columns["THUONG_TAY_NGHE"].Visible = false;
                    grvDSUngVien.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grvDSUngVien.Columns["ID_DGTN"].Visible = true;
                    grvDSUngVien.Columns["KQ_GIAY"].Visible = true;
                    grvDSUngVien.Columns["KQ_VAI"].Visible = true;
                    grvDSUngVien.Columns["THUONG_TAY_NGHE"].Visible = true;
                    grvDSUngVien.Columns["ID_CN"].Visible = true;
                }
            }
            catch (Exception ex) { }
        }
        private void cboDGTN_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_DGTN", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboDGTN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
            }
            catch { }
        }
        private void cboNGT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvDSUngVien.SetFocusedRowCellValue("ID_NGT", Convert.ToInt64((dataRow.Row[0])));
            }
            catch { }
        }
        private void cboNGT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dtCN = new DataTable();
                dtCN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                lookUp.Properties.DataSource = dtCN;
            }
            catch { }
        }
        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLoc, dt, "ID_LCN", "TEN_LCN", "TEN_LCN");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboYCTD, dt, "ID_YCTD", "MA_YCTD", "MA_YCTD");
            }
            catch { }
        }
        private void EnabelButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;

            datTNgay.Properties.ReadOnly = !visible;
            datDNgay.Properties.ReadOnly = !visible;
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            EnabelButton(false);
                            LoadData();
                            grvDSUngVien.OptionsBehavior.Editable = true;
                            break;
                        }
                    case "luu":
                        {
                            grvDSUngVien.CloseEditor();
                            grvDSUngVien.UpdateCurrentRow();
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            if (flag == true) return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdDSUngVien.DataSource);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUngVien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!SaveData()) return;
                            EnabelButton(true);
                            radioGroup1.SelectedIndex = 0;
                            radioGroup1_SelectedIndexChanged(null, null);
                            break;
                        }
                    case "khongluu":
                        {
                            EnabelButton(true);
                            if (radioGroup1.SelectedIndex == 1)
                            {
                                Commons.Modules.sLoad = "0Load";
                                radioGroup1.SelectedIndex = 0;
                                Commons.Modules.sLoad = "";
                            }
                            radioGroup1_SelectedIndexChanged(null, null);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool SaveData()
        {
            string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return false;
            }
        }
        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {

            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
            radioGroup1_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            radioGroup1_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void cboLocTheoNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboLoc.EditValue) == 1)
            {
                lblYeuCauTD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                lblYeuCauTD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            radioGroup1_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
            //grvDSUngVien_FocusedRowChanged(null, null);
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
        // Cập nhật All
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhatNhanh = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuCapNhatNhanh.Tag = new RowInfo(view, rowHandle);
            return menuCapNhatNhanh;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            string sCotCN = grvDSUngVien.FocusedColumn.FieldName;
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                if (grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName).ToString() == "") return;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                if (sCotCN.Length < 4)
                {

                }
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTUngVien, sCotCN, sCotCN.Substring(0, 3) == "NGA" ? Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)));
                grdDSUngVien.DataSource = dt;
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
        }
        //Thong tin ung vien
        public DXMenuItem MCreateMenuThongTinUV(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinUV = new DXMenuItem(sStr, new EventHandler(ThongTinUV));
            menuThongTinUV.Tag = new RowInfo(view, rowHandle);
            return menuThongTinUV;
        }
        public void ThongTinUV(object sender, EventArgs e)
        {
            try
            {
                iLoai = 1;
                iID_UV = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV"));
                ucUV = new ucCTQLUV(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                //ucUV.Refresh();
                ucUV.Refresh();

                //ns.accorMenuleft = accorMenuleft;
                tableLayoutPanel1.Hide();
                this.Controls.Add(ucUV);
                ucUV.Dock = DockStyle.Fill;
                ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            if (iLoai == 1)
            {
                ucUV.Hide();
            }
            else
            {
                ucNS.Hide();
            }
            tableLayoutPanel1.Show();
            LoadData();
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
                    DevExpress.Utils.Menu.DXMenuItem itemTTUV = MCreateMenuThongTinUV(view, irow);
                    e.Menu.Items.Add(itemTTUV);
                    if (btnALL.Buttons[0].Properties.Visible == true) return;
                    if (grvDSUngVien.FocusedColumn.FieldName != "MS_UV" && grvDSUngVien.FocusedColumn.FieldName != "HO_TEN")
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemCapNhatNhanh = MCreateMenuCapNhatAll(view, irow);
                        e.Menu.Items.Add(itemCapNhatNhanh);
                    }
                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioGroup1.SelectedIndex == 1)
                {
                    EnabelButton(false);
                }
                else
                {
                    EnabelButton(true);
                }
                if (Commons.Modules.sLoad == "")
                {
                    grdDSUngVien.DataSource = null;
                    LoadData();
                }
                Commons.Modules.ObjSystems.RowFilter(grdDSUngVien, grvDSUngVien.Columns["TINH_TRANG_DG"], radioGroup1.SelectedIndex.ToString());
                grvDSUngVien.OptionsBehavior.Editable = Convert.ToBoolean(radioGroup1.SelectedIndex);
            }
            catch { }
        }

        private void cboYCTD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            radioGroup1_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn NgayDG = View.Columns["NGAY_DG"];

                if (View.GetRowCellValue(e.RowHandle, NgayDG).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(NgayDG, "Ngày đánh giá không được trống"); return;
                }
                //if (View.GetRowCellValue(e.RowHandle, MS_CN).ToString() == "")
                //{
                //    flag = true;
                //    e.Valid = false;
                //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSCNKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    View.SetColumnError(MS_CN, "Mã công nhân không được bỏ trống"); return;
                //}
                //if (View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString() == "")
                //{
                //    flag = true;
                //    e.Valid = false;
                //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMTCCKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công không được bỏ trống"); return;
                //}

                //string strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_CN = '" + View.GetRowCellValue(e.RowHandle, MS_CN).ToString().Trim() + "'";
                //int iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                //if (iSL > 0)
                //{
                //    flag = true;
                //    e.Valid = false;
                //    View.SetColumnError(MS_CN, "Mã số công nhân này đã có rồi"); return;
                //}

                //strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_THE_CC = '" + View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString().Trim() + "'";
                //iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                //if (iSL > 0)
                //{
                //    flag = true;
                //    e.Valid = false;
                //    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công này đã có rồi"); return;
                //}

                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSUngVien_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
