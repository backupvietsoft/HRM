using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucTiepNhanUngVien : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        public AccordionControl accorMenuleft;
        private long iID_UV = -1;
        private ucCTQLUV ucUV;
        private HRM.ucCTQLNS ucNS;
        private int iLoai = 0; // 1 : link ứng viên, 2 : link nhân sự
        public ucTiepNhanUngVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucTiepNhanUngVien_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                Commons.Modules.sLoad = "";
                LoadCbo();
                LoadData();
                Commons.Modules.sLoad = "";
            }
            catch
            {
            }
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "TN_UNG_VIEN";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@MS_CV", SqlDbType.NVarChar).Value = cboMS_CV.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["TAI_LIEU"].ReadOnly = true;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_UV"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, false, false, true, true, this.Name);
                grvDSUngVien.Columns["TAI_LIEU"].OptionsColumn.AllowEdit = true;
                grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["TEN_XN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["TEN_LCV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_PV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_CO_THE_DI_LAM"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_HEN_DI_LAM"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_NHAN_VIEC"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_DG_TN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HOAN_THANH_DT"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["MUC_LUONG_DN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["TEN_LHDLD"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["ID_CN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_HOAN_THANH_DT"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["MS_CV"].Visible = false;
                grvDSUngVien.Columns["ID_YCTD"].Visible = false;
                grvDSUngVien.Columns["ID_VTTD"].Visible = false;

                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                grvDSUngVien.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                btnEdit.ButtonClick += BtnEdit_ButtonClick;

                if (Convert.ToInt32(cboMS_CV.EditValue) == 1)
                {
                    grvDSUngVien.Columns["NGAY_DGTN"].Visible = false;
                }
                else if (Convert.ToInt32(cboMS_CV.EditValue) == 2 || Convert.ToInt32(cboMS_CV.EditValue) == 3)
                {
                    grvDSUngVien.Columns["NGAY_PV"].Visible = false;
                    grvDSUngVien.Columns["NGAY_CO_THE_DI_LAM"].Visible = false;
                }
                DataTable dtCN = new DataTable();
                dtCN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CN", "HO_TEN", "ID_CN", grvDSUngVien, dtCN, this.Name);

                if (iID_UV != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_UV));
                    grvDSUngVien.FocusedRowHandle = grvDSUngVien.GetRowHandle(index);
                    grvDSUngVien.ClearSelection();
                    grvDSUngVien.SelectRow(index);
                }


                //DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboDGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                //cboDGTN.NullText = "";
                //cboDGTN.ValueMember = "ID_DGTN";
                //cboDGTN.DisplayMember = "TEN_DGTN";
                ////ID_VTTD,TEN_VTTD
                //cboDGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                //cboDGTN.Columns.Clear();
                //cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                //cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                //cboDGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                //cboDGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //cboDGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                //cboDGTN.Columns["ID_DGTN"].Visible = false;
                //grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboDGTN;
                //cboDGTN.BeforePopup += cboDGTN_BeforePopup;
                //cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;
            }
            catch (Exception ex) { }
        }
        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.OpenHinh(grvDSUngVien.GetFocusedRowCellValue("TAI_LIEU").ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "TN_UNG_VIEN";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMS_CV, dt, "MS_CV", "TEN_CV", "TEN_CV");
                cboMS_CV.EditValue = 2;
            }
            catch { }
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "sua":
                        {
                            break;
                        }
                    case "PhieuDTDH":
                        {
                            frmPhieuDTDH frm = new frmPhieuDTDH();
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
                    case "Ingiayhen":
                        {
                            if (grvDSUngVien.RowCount == 0) return;
                            frmInGiayHenDiLam frm = new frmInGiayHenDiLam();
                            frm.MS_CV = Convert.ToInt32(cboMS_CV.EditValue);
                            frm.dtTemp = new DataTable();
                            try
                            {
                                frm.dtTemp = Commons.Modules.ObjSystems.ConvertDatatable(grdDSUngVien);
                                    //.AsEnumerable().Where(x => x["NGAY_HEN_DI_LAM"] != "").CopyToDataTable();

                            }
                            catch
                            {

                            }
                            frm.ShowDialog();
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
        private void cboMS_CV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            grvDSUngVien_FocusedRowChanged(null, null);
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
        // TT Thiếp nhận ứng viên
        public DXMenuItem MCreateMenuThongTinTNUV(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinTiepNhanUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinTNUV = new DXMenuItem(sStr, new EventHandler(TTTiepNhanUV));
            menuThongTinTNUV.Tag = new RowInfo(view, rowHandle);
            return menuThongTinTNUV;
        }
        public void TTTiepNhanUV(object sender, EventArgs e)
        {
            try
            {
                frmThongTinTiepNhanUV frm = new frmThongTinTiepNhanUV();
                //DataRow dr = grvDSUngVien.GetDataRow(grvDSUngVien.FocusedRowHandle);
                DataRow dr;
                DataRow row;
                DataTable dt = new DataTable();
                dt = ((DataTable)grdDSUngVien.DataSource).Clone();
                Int32[] selectedRowHandles = grvDSUngVien.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grvDSUngVien.GetDataRow(selectedRowHandle);
                        row = dt.NewRow();
                        row["ID_UV"] = dr["ID_UV"];
                        row["ID_YCTD"] = dr["ID_YCTD"];
                        dt.Rows.Add(row);
                    }
                }
                frm.dtTemp = new DataTable();
                frm.dtTemp = dt;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            catch (Exception ex) { }
        }
        // Cập nhật nhanh
        public DXMenuItem MCreateMenuCapNhatNhanh(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblChuyenSangNhanSu", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhatNhanh = new DXMenuItem(sStr, new EventHandler(CapNhatNhanh));
            menuCapNhatNhanh.Tag = new RowInfo(view, rowHandle);
            return menuCapNhatNhanh;
        }
        public void CapNhatNhanh(object sender, EventArgs e)
        {
            try
            {


                frmCapNhatNhanh frm = new frmCapNhatNhanh();
                DataRow dr;
                DataRow row;
                DataTable dt = new DataTable();
                dt = ((DataTable)grdDSUngVien.DataSource).Clone();
                Int32[] selectedRowHandles = grvDSUngVien.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grvDSUngVien.GetDataRow(selectedRowHandle);
                        row = dt.NewRow();
                        row["ID_UV"] = dr["ID_UV"];
                        row["ID_TO"] = dr["ID_TO"];
                        row["ID_XN"] = dr["ID_XN"];
                        row["ID_VTTD"] = dr["ID_VTTD"];
                        row["MS_CV"] = dr["MS_CV"];
                        row["ID_YCTD"] = dr["ID_YCTD"];
                        dt.Rows.Add(row);
                    }
                }
                frm.dt1 = new DataTable();
                frm.dt1 = dt;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataTable dttemp = new DataTable();
                    dttemp  = frm.dt1.Copy();
                    // chạy vòng for trên danh sách ảnh cần đổi đường dẫn
                    for (int i = 0; i < dttemp.Rows.Count; i++)
                    {
                        // copy ảnh url ứng viên = url công nhân
                        try
                        {
                            if (System.IO.File.Exists(dttemp.Rows[i]["FILE_OLD"].ToString()))
                            {
                                using (FileStream fs = new FileStream(dttemp.Rows[i]["FILE_OLD"].ToString(), FileMode.Open, FileAccess.ReadWrite))
                                {
                                    fs.Close();
                                    System.IO.File.Move(dttemp.Rows[i]["FILE_OLD"].ToString(), dttemp.Rows[i]["FILE_NEW"].ToString());
                                }
                            }
                           
                        }
                        catch { }
                    }
                    LoadData();
                }
            }
            catch { }
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
                iID_UV = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV"));
                iLoai = 2;
                ucNS = new HRM.ucCTQLNS(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                ucNS.Refresh();

                //ns.accorMenuleft = accorMenuleft;
                tableLayoutPanel1.Hide();
                this.Controls.Add(ucNS);
                ucNS.Dock = DockStyle.Fill;
                ucNS.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex) { }
        }
        //HuyTuyenDung
        public DXMenuItem MCreateMenuHuyTD(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblHuyTD", Commons.Modules.TypeLanguage);
            DXMenuItem menuHuyTD = new DXMenuItem(sStr, new EventHandler(HuyTuyenDung));
            menuHuyTD.Tag = new RowInfo(view, rowHandle);
            return menuHuyTD;
        }
        public void HuyTuyenDung(object sender, EventArgs e)
        {
            try
            {
                //Load worksheet
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Hủy tuyển dụng";
                args.Prompt = "Lý do";
                args.DefaultButtonIndex = 0;

                // initialize a DateEdit editor with custom settings
                TextEdit editor = new TextEdit();
                editor.EditValue = "";

                args.Editor = editor;
                // a default DateEdit value
                args.DefaultResponse = "";
                // display an Input Box with the custom editor
                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;

                DataRow dr;
                DataRow row;
                DataTable dt = new DataTable();
                dt = ((DataTable)grdDSUngVien.DataSource).Clone();
                Int32[] selectedRowHandles = grvDSUngVien.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grvDSUngVien.GetDataRow(selectedRowHandle);
                        row = dt.NewRow();
                        row["ID_UV"] = dr["ID_UV"];
                        dt.Rows.Add(row);
                    }
                }

                string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@sCot1", SqlDbType.NVarChar).Value = result.ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sBT);
                LoadData();
            }
            catch (Exception ex) { }
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

        private bool checkDataoDH()
        {
            try
            {
                DataRow dr;
                DataTable dt = new DataTable();
                dt = ((DataTable)grdDSUngVien.DataSource).Clone();
                Int32[] selectedRowHandles = grvDSUngVien.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = grvDSUngVien.GetDataRow(selectedRowHandle);
                        if (Convert.ToBoolean(dr["HOAN_THANH_DT"]) == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
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
                    //kiểu tra data select có tiếp đào tạo mới hiện menu

                    DevExpress.Utils.Menu.DXMenuItem itemTiepNhan = MCreateMenuThongTinTNUV(view, irow);
                    e.Menu.Items.Add(itemTiepNhan);
                    DevExpress.Utils.Menu.DXMenuItem itemTTUV = MCreateMenuThongTinUV(view, irow);
                    e.Menu.Items.Add(itemTTUV);
                    if (checkDataoDH())
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemCapNhatNhanh = MCreateMenuCapNhatNhanh(view, irow);
                        e.Menu.Items.Add(itemCapNhatNhanh);
                    }

                    DevExpress.Utils.Menu.DXMenuItem itemHuyTD = MCreateMenuHuyTD(view, irow);
                    e.Menu.Items.Add(itemHuyTD);

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
            try
            {
                DateTime dt = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_CHUYEN"));
                flag = true;
            }
            catch
            {
                flag = false;
            }
        }

        private void grdDSUngVien_ProcessGridKey(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.ControlKey)
            //{
            //    grvDSUngVien.GridControl.BeginUpdate();
            //    List<int> selectedLogItems = new List<int>(grvDSUngVien.GetSelectedRows());
            //    for (int i = selectedLogItems.Count - 1; i >= 0; i--)
            //    {
            //        grvDSUngVien.SetFocusedRowCellValue("CHON", !Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("CHON")));
            //    }
            //    grvDSUngVien.GridControl.EndUpdate();
            //}

        }
    }
}
