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
using DevExpress.XtraEditors.Repository;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Payroll
{
    public partial class frmDonHangBan : DevExpress.XtraEditors.XtraUserControl
    {
        static int iPQ = 1;  // == 1  full; <> 1 la read only   private Int64 iID_DHB = -1;
        public Int64 iID_DHB = -1;
        private DataTable dt_DHB_MS = new DataTable();
        private bool bLoaded = false;
        IEnumerable<Control> allControls;
        public frmDonHangBan(int PQ)
        {
            InitializeComponent();

            var typeToBeSelected = new List<Type>
            {

                typeof(DevExpress.XtraEditors.TextEdit)
                , typeof(DevExpress.XtraEditors.MemoEdit)
                , typeof(DevExpress.XtraEditors.ButtonEdit)
            };


            allControls = GetAllConTrol(dataLayoutControl1, typeToBeSelected);

            MFieldRequest(lblSO_DHB);
            MFieldRequest(lblNGAY_LAP);
            MFieldRequest(lblID_DT);

            //Tam an
            txtSO_DHB.Properties.ReadOnly = true;

            this.grvChiTiet.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grv_PopupMenuShowing);

        }


        private IEnumerable<Control> GetAllConTrol(Control control, IEnumerable<Type> filteringTypes)
        {
            var ctrls = control.Controls.Cast<Control>();

            return ctrls.SelectMany(ctrl => GetAllConTrol(ctrl, filteringTypes))
                        .Concat(ctrls)
                        .Where(ctl => filteringTypes.Any(t => ctl.GetType() == t));
        }

        #region Event
        private void frmDonHangBan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCbo();
            LoadCboTT();
            Commons.Modules.sLoad = "";
            if (iID_DHB == -1)
            {
                LoadData(iID_DHB);
                Bindingdata(true);
                enableButon(true);
            }
            else
                LoadData(iID_DHB);
            LoadNN();
            //StatusControl();

        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        iID_DHB = -1;
                        Bindingdata(true);
                        enableButon(false);
                        grdChiTiet.DataSource = ((DataTable)grdChiTiet.DataSource).Clone();
                        break;
                    }
                case "sua":
                    {
                        if (iID_DHB == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        enableButon(false);
                        break;
                    }
                case "chonDS":
                    {
                        if (Convert.ToInt32(cboID_DT.EditValue) < 1)
                        {
                            XtraMessageBox.Show(lblID_DT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongDuocTrong"));
                            cboID_DT.Focus();
                            return;
                        }


                        if (tcgChung.SelectedTabPage.Name == "lcgTaiLieu")
                        {
                            OpenFileDialog ofd = new OpenFileDialog
                            {
                                InitialDirectory = ""
                            };
                        }

                        try
                        {

                            frmDonHangBanView_Order ctl1 = new frmDonHangBanView_Order(iPQ, "spDonHangBan", Convert.ToInt64(cboID_DT.EditValue));

                            ctl1.Size = new Size(800, 600);
                            ctl1.StartPosition = FormStartPosition.CenterParent;
                            ctl1.Size = new Size((this.Width / 2) + (ctl1.Width / 2), (this.Height / 2) + (ctl1.Height / 2));
                            ctl1.StartPosition = FormStartPosition.Manual;
                            ctl1.Location = new Point(this.Width / 2 - ctl1.Width / 2 + this.Location.X,
                                                      this.Height / 2 - ctl1.Height / 2 + this.Location.Y);


                            if (ctl1.ShowDialog() == DialogResult.OK)
                            {
                                DataTable dt = (DataTable)grdChiTiet.DataSource;
                                DataTable dt_chon = ((frmDonHangBanView_Order)ctl1).dt_frmDonHangBanView_Order_CTBG.Copy();

                                ////Xoa het de them dong moi
                                //for (int i = (dt.Rows.Count - 1); i >= 0; i--)
                                //{
                                //    dt.Rows[i].Delete();
                                //}

                                //dt.AcceptChanges();

                                if (dt_chon == null || dt_chon.Rows.Count < 1) return;

                                foreach (DataRow dr1 in dt_chon.Rows)
                                {
                                    DataRow dr = ((DataTable)grdChiTiet.DataSource).NewRow();
                                    dr["ID_DHBORD"] = dr1["ID_DHBORD"];
                                    dr["ID_DHB"] = dr1["ID_DHB"];
                                    dr["ORDER_NUMBER"] = dr1["ORDER_NUMBER"];
                                    dr["ORDER_NUMBER_KHACH"] = dr1["ORDER_NUMBER_KHACH"];
                                    dr["ID_HH"] = dr1["ID_HH"];
                                    dr["MS_HH"] = dr1["MS_HH"];
                                    dr["TEN_HH"] = dr1["TEN_HH"];
                                    dr["SO_LUONG"] = dr1["SO_LUONG"];
                                    dr["CLOSED"] = dr1["CLOSED"];

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }

                        break;
                    }
                case "xoa":
                    {
                        if (iID_DHB == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }

                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                        cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 8;
                        cmd.Parameters.Add("@ID_DHB", SqlDbType.BigInt).Value = iID_DHB;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (Convert.ToInt32(cmd.ExecuteScalar().ToString()) == 1)
                        {
                            //iID_DHB = -1;
                            //LoadData(iID_DHB);
                            Bindingdata(true);
                            enableButon(true);
                            grdChiTiet.DataSource = ((DataTable)grdChiTiet.DataSource).Clone();
                            ((DataTable)grdChiTiet.DataSource).AcceptChanges();

                            // Program.MBarXoaThanhCong();
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgXoaThatBai"));
                            // Program.MBarXoaKhongThanhCong();
                        }
                        break;
                    }
                case "ghi":
                    {
                        grdChiTiet.MainView.CloseEditor();
                        grvChiTiet.UpdateCurrentRow();

                        if (!dxValidationProvider1.Validate()) return;
                        if (KiemTrung()) return;
                        if (KiemTrong()) return;
                        if (KiemTrong_grvChiTiet()) return;
                        try
                        {
                            //Truyền datatable grvChiTiet,grvTAI_LIEU của xuống CSDL
                            string sBT_grvChiTiet = "sBT_grvChiTiet" + Commons.Modules.UserName;

                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_grvChiTiet, Commons.Modules.ObjSystems.ConvertDatatable(grdChiTiet), "");

                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 6;
                            cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_grvChiTiet;
                            cmd.Parameters.Add("@ID_DHB", SqlDbType.BigInt).Value = iID_DHB;
                            cmd.Parameters.Add("@SO_DHB", SqlDbType.NVarChar).Value = txtSO_DHB.Text;
                            cmd.Parameters.Add("@SO_PO_KHACH", SqlDbType.NVarChar).Value = txtSO_PO_KHACH.Text;
                            cmd.Parameters.Add("@NGAY_LAP", SqlDbType.DateTime).Value = datNGAY_LAP.EditValue;
                            if (Convert.ToInt32(cboTRANG_THAI.EditValue) < 0)
                            {
                                cboTRANG_THAI.EditValue = null;
                            }
                            cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = cboTRANG_THAI.EditValue;
                            cmd.Parameters.Add("@ID_DT", SqlDbType.BigInt).Value = cboID_DT.EditValue;
                            cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = txtGHI_CHU.Text;

                            cmd.CommandType = CommandType.StoredProcedure;
                            iID_DHB = Convert.ToInt64(cmd.ExecuteScalar());
                            if (iID_DHB != -1)
                            {
                                LoadData(iID_DHB);
                                enableButon(true);
                            }

                            if (conn.State == ConnectionState.Open)
                                conn.Close();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgGhiKhongThanhCong") + "\n" + ex.Message);
                        }
                        break;
                    }
                case "khongghi":
                    {
                        try
                        {
                            //iID_DHB = -1;
                            //dt_DHB_MS = null;
                            //LoadData(iID_DHB);
                            if (iID_DHB == -1)
                            {
                                Bindingdata(true);
                                grdChiTiet.DataSource = ((DataTable)grdChiTiet.DataSource).Clone();
                            }
                            else
                            { Bindingdata(false); }
                            enableButon(true);
                        }
                        catch { }
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

        #region Create chuot phai

        public static DXMenuItem MCreateMenuCopyHangHoa(GridView view, int rowHandle, string sColumn)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmChung", "mnuLinkCopyHangHoa", Commons.Modules.TypeLanguage);
            DXMenuItem menuLinkCopyHH = new DXMenuItem(sStr, new EventHandler(OnLinkCopyHHIDRowClick));
            menuLinkCopyHH.Tag = new RowInfo(view, rowHandle, sColumn);
            return menuLinkCopyHH;
        }


        static void OnLinkCopyHHIDRowClick(object sender, EventArgs e)
        {
            try
            {
                DXMenuItem menuItem = sender as DXMenuItem;
                RowInfo ri = menuItem.Tag as RowInfo;
                if (ri == null) return;
                DevExpress.XtraGrid.Views.Grid.GridView grv = (DevExpress.XtraGrid.Views.Grid.GridView)ri.View;

                if (grv.RowCount == 0 || grv.FocusedRowHandle < 0)
                {
                    Clipboard.Clear();
                }
                else
                {
                    string sSql = " SELECT TOP 1 MS_HH FROM HANG_HOA WHERE ID_HH = N'" + grv.GetRowCellValue(ri.RowHandle, ri.Column).ToString() + "' ";
                    try
                    {
                        sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    }
                    catch { sSql = ""; }
                    Clipboard.SetText(sSql);
                }

            }
            catch { Clipboard.Clear(); }
        }
        class RowInfo
        {
            public RowInfo(GridView view, int rowHandle, string column)
            {
                this.RowHandle = rowHandle;
                this.View = view;
                this.Column = column;
            }


            public GridView View;
            public int RowHandle;
            public string Column;
        }
        private void grv_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemCopyHH = MCreateMenuCopyHangHoa(view, irow, "ID_HH");
                    e.Menu.Items.Add(itemCopyHH);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void txtSO_DHB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LoadView();
        }

        private void grvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdChiTiet.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["ORDER_NUMBER_KHACH"] = dt.Rows[i]["ORDER_NUMBER"];
                }
            }
            catch
            {

            }

            if (e.Column.FieldName == "SO_LUONG")
            {
                Int64 SO_LUONG = Convert.ToInt64(string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(grvChiTiet.FocusedRowHandle, grvChiTiet.Columns["SO_LUONG"]).ToString()) ? "0" : grvChiTiet.GetRowCellValue(grvChiTiet.FocusedRowHandle, grvChiTiet.Columns["SO_LUONG"]).ToString());
            }

            if (e.Column.FieldName == "SO_LUONG")
            {
                Cal_Tong();
            }
        }

        private void grvChiTiet_RowCountChanged(object sender, EventArgs e)
        {
            Cal_Tong();
        }

        private void grvChiTiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (windowsUIButton.Buttons[0].Properties.Visible)
                {
                    try
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DON_HANG_BAN_ORDER WHERE ID_DHBORD ='" + (grvChiTiet.GetFocusedRowCellValue("ID_DHBORD") + "' AND ID_DHB = '" + grvChiTiet.GetFocusedRowCellValue("ID_DHB") + "'"));
                        grvChiTiet.DeleteSelectedRows();
                    }
                    catch (Exception)
                    {
                        Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                    }
                }
                else
                {
                    grvChiTiet.DeleteSelectedRows();
                }
                ((DataTable)grdChiTiet.DataSource).AcceptChanges();
            }
        }

        private void grvChiTiet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvChiTiet.InvalidRowException += grvChiTiet_InvalidRowException;
            string ORDER_NUMBER = string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(e.RowHandle, grvChiTiet.Columns["ORDER_NUMBER"]).ToString()) ? "" : grvChiTiet.GetRowCellValue(e.RowHandle, grvChiTiet.Columns["ORDER_NUMBER"]).ToString();
            for (int i = 0; i < grvChiTiet.RowCount; i++)
            {
                if (ORDER_NUMBER.Trim() != "")
                {
                    if (i != e.RowHandle && ORDER_NUMBER == (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns["ORDER_NUMBER"]).ToString()) ? "" : grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns["ORDER_NUMBER"]).ToString()))
                    {
                        e.Valid = false;
                        XtraMessageBox.Show(grvChiTiet.Columns["ORDER_NUMBER"].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrung"));
                        grvChiTiet.FocusedRowHandle = e.RowHandle;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns["ORDER_NUMBER"];
                        break;
                    }
                }
            }
        }

        private void grvChiTiet_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        #endregion

        #region Function
        public void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup>() { Root, lcgChiTietHopDong }, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvChiTiet, this.Name);
            //Commons.Modulesules.ObjSystems.MLoadNNXtraGrid(grvDuyet, this.Name);
        }
        private void Bindingdata(bool them)
        {
            if (them == true)
            {
                datNGAY_LAP.EditValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                cboTRANG_THAI.EditValue = -1;
                txtSO_PO_KHACH.EditValue = DBNull.Value;
                cboID_DT.EditValue = -1;
                txtGHI_CHU.EditValue = DBNull.Value;
                txtSO_DHB.Text = "";
                string Ma = "";
                try
                {
                    Ma = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "MTaoSoPhieu", "SO", this.Name.ToString(), "DON_HANG_BAN", "SO_DHB", Convert.ToDateTime(datNGAY_LAP.EditValue).ToString()).ToString();
                }
                catch { Ma = ""; }
                txtSO_DHB.Text = Ma;
            }
            else
            {
                LoadData(iID_DHB);
            }
        }

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;

            grvChiTiet.OptionsBehavior.Editable = !visible;

            //txtSO_DHB.Properties.ReadOnly = visible;
            datNGAY_LAP.Enabled = !visible;
            cboTRANG_THAI.Properties.ReadOnly = visible;
            txtSO_PO_KHACH.Properties.ReadOnly = visible;
            txtGHI_CHU.Properties.ReadOnly = visible;
            if (((DataTable)grdChiTiet.DataSource).Rows.Count > 0)
            {
                cboID_DT.ReadOnly = visible;
            }
            else
            {
                cboID_DT.ReadOnly = !visible;
            }
            cboID_DT.Properties.ReadOnly = visible;
        }
        private void LoadView()
        {
            try
            {
                frmDonHangBanView ctl = new frmDonHangBanView(iPQ, "spDonHangBan");
                ctl.Size = new Size(800, 600);
                ctl.StartPosition = FormStartPosition.CenterParent;
                ctl.Size = new Size((this.Width / 2) + (ctl.Width / 2), (this.Height / 2) + (ctl.Height / 2));
                ctl.StartPosition = FormStartPosition.Manual;
                ctl.Location = new Point(this.Width / 2 - ctl.Width / 2 + this.Location.X,
                                          this.Height / 2 - ctl.Height / 2 + this.Location.Y);

                if (ctl.ShowDialog() == DialogResult.OK)
                {
                    iID_DHB = Convert.ToInt64(Commons.Modules.sId);
                    LoadData(iID_DHB);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //Load combo DOI_TAC             
                DataTable dt0 = new DataTable();
                dt0 = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DT, dt0, "ID_DT", "TEN_NGAN", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NGAN"));

            }
            catch { }
        }
        private void LoadCboTT()
        {
            try
            {
                //cboTuyenDung
                DataTable dt_tt = new DataTable();
                dt_tt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTrangThai_DHB", Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTRANG_THAI, dt_tt, "ID_TT", "TRANG_THAI", "TRANG_THAI", true, false);
            }
            catch { }
        }
        private void LoadData(Int64 ID_DHB)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //Chưa load
                bLoaded = false;

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DHB", SqlDbType.BigInt).Value = ID_DHB;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (dt.Rows.Count > 1) return;

                if (dt.Rows.Count == 0)
                {
                    datNGAY_LAP.Text = DateTime.Now.ToShortDateString();
                    cboID_DT.EditValue = -99;
                    foreach (var ctrl in allControls)
                    {
                        try
                        {
                            if (ctrl.Name != "")
                            {
                                ctrl.Text = "";
                            }
                        }
                        catch { }
                    }
                }

                if (dt.Rows.Count == 1 && dt != null)
                {
                    datNGAY_LAP.EditValue = string.IsNullOrEmpty(dt.Rows[0]["NGAY_LAP"].ToString()) ? null : dt.Rows[0]["NGAY_LAP"];
                    cboID_DT.EditValue = Convert.ToInt64(string.IsNullOrEmpty(dt.Rows[0]["ID_DT"].ToString()) ? "-1" : dt.Rows[0]["ID_DT"].ToString());
                    cboTRANG_THAI.EditValue = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["TRANG_THAI"].ToString()) ? "-1" : dt.Rows[0]["TRANG_THAI"].ToString());
                    foreach (var ctrl in allControls)
                    {
                        try
                        {
                            if (ctrl.Name != ""/* && !string.IsNullOrEmpty(ctrl.Text)*/)
                            {
                                ctrl.Text = string.IsNullOrEmpty(dt.Rows[0][ctrl.Name.Substring(3)].ToString()) ? "" : dt.Rows[0][ctrl.Name.Substring(3)].ToString();
                            }
                        }
                        catch
                        {
                        }
                    }
                }


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                LoadData_grvChiTiet(dt1);


                //refesh dt_HANG_HOA_MAU_SIZE, dt_DHB_MS
                dt_DHB_MS = null;

                //Đã load
                bLoaded = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Format_grvChiTiet()
        {
            if (grdChiTiet.DataSource != null)
            {
                grvChiTiet.Columns["ID_DHBORD"].Visible = false;
                grvChiTiet.Columns["ID_DHB"].Visible = false;
                grvChiTiet.Columns["ID_HH"].Visible = false;
                for (int i = 0; i < grvChiTiet.Columns.Count; i++)
                {
                    grvChiTiet.Columns[i].OptionsColumn.AllowEdit = false;
                }
                grvChiTiet.Columns["CLOSED"].OptionsColumn.AllowEdit = true;
                grvChiTiet.Columns["ORDER_NUMBER"].OptionsColumn.AllowEdit = true;
                grvChiTiet.Columns["ORDER_NUMBER_KHACH"].OptionsColumn.AllowEdit = false;
                grvChiTiet.Columns["MS_HH"].OptionsColumn.AllowEdit = false;
                grvChiTiet.Columns["TEN_HH"].OptionsColumn.AllowEdit = false;
                grvChiTiet.Columns["SO_LUONG"].OptionsColumn.AllowEdit = true;
                grvChiTiet.Columns["SO_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvChiTiet.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";

            }
        }

        private void LoadData_grvChiTiet(DataTable dt)
        {
            try
            {
                if (grdChiTiet.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChiTiet, grvChiTiet, dt, true, true, true, false, true, this.Name);
                    //Format_grvChiTiet();

                    grvChiTiet.Columns["ID_DHBORD"].Visible = false;
                    grvChiTiet.Columns["ID_DHB"].Visible = false;
                    grvChiTiet.Columns["ID_HH"].Visible = false;
                    //for (int i = 0; i < grvChiTiet.Columns.Count; i++)
                    //{
                    //    grvChiTiet.Columns[i].OptionsColumn.AllowEdit = false;
                    //}
                    grvChiTiet.Columns["CLOSED"].OptionsColumn.AllowEdit = true;
                    grvChiTiet.Columns["ORDER_NUMBER"].OptionsColumn.AllowEdit = true;
                    grvChiTiet.Columns["ORDER_NUMBER_KHACH"].OptionsColumn.AllowEdit = false;
                    grvChiTiet.Columns["MS_HH"].OptionsColumn.AllowEdit = false;
                    grvChiTiet.Columns["TEN_HH"].OptionsColumn.AllowEdit = false;
                    grvChiTiet.Columns["SO_LUONG"].OptionsColumn.AllowEdit = true;
                    grvChiTiet.Columns["SO_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvChiTiet.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";

                    DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnMSize = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                    btnMSize.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    btnMSize.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    btnMSize.Buttons[0].Caption = "...";
                }
                else
                    grdChiTiet.DataSource = dt;


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Cal_Tong()
        {
            int Tong_Ma_Hang = 0;
            int Tong_Order = 0;
            Int64 Tong_San_Pham = 0;
            try
            {
                if (grdChiTiet.DataSource != null && grvChiTiet.RowCount > 0)
                {
                    Tong_Order = grvChiTiet.RowCount;
                    Int64[] List_ID_HH = new Int64[grvChiTiet.RowCount];
                    for (int i = 0; i < grvChiTiet.RowCount; i++)
                    {
                        List_ID_HH[i] = Convert.ToInt64(string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, "ID_HH").ToString()) ? "0" : grvChiTiet.GetRowCellValue(i, "ID_HH").ToString());

                        Tong_San_Pham = Tong_San_Pham + Convert.ToInt64(string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, "SO_LUONG").ToString()) ? "0" : grvChiTiet.GetRowCellValue(i, "SO_LUONG").ToString());
                    }
                    Tong_Ma_Hang = List_ID_HH.Distinct().Count();
                }

                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "Tong_Ma_Hang") + ": " + Tong_Ma_Hang.ToString("N0") + "   " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "Tong_Order") + ": " + Tong_Order.ToString("N0") + "   " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "Tong_San_Pham") + ": " + Tong_San_Pham.ToString("N0");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool KiemTrung()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 7;
                cmd.Parameters.Add("@SO_DHB", SqlDbType.NVarChar).Value = txtSO_DHB.Text;
                cmd.Parameters.Add("@iID", SqlDbType.NVarChar).Value = iID_DHB;
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrung"));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool KiemTrong()
        {
            try
            {
                if (Convert.ToInt32(cboID_DT.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblID_DT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboID_DT.Focus();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool KiemTrong_grvChiTiet()
        {
            for (int i = 0; i < grvChiTiet.RowCount; i++)
            {
                //Kiểm trống theo từng cột
                for (int j = 0; j < grvChiTiet.Columns.Count; j++)
                {
                    if (grvChiTiet.Columns[j].FieldName == "ORDER_NUMBER" && (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) ? "" : grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) == "")
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }

                    if (grvChiTiet.Columns[j].FieldName == "ORDER_NUMBER_KHACH" && (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) ? "" : grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) == "")
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }

                    if (grvChiTiet.Columns[j].FieldName == "ID_HH" && (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) ? "" : grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) == "")
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }

                    if (grvChiTiet.Columns[j].FieldName == "TEN_HH" && (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) ? "" : grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) == "")
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }
                    if (grvChiTiet.Columns[j].FieldName == "SO_LUONG" && (string.IsNullOrEmpty(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]).ToString()) ? 0 : Convert.ToInt32(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]))) == 0)
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }

                    if (grvChiTiet.Columns[j].FieldName == "SO_LUONG" && (Convert.ToInt32(grvChiTiet.GetRowCellValue(i, grvChiTiet.Columns[j]))) < 0)
                    {
                        XtraMessageBox.Show(grvChiTiet.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgLonHonKhong"));
                        grvChiTiet.FocusedRowHandle = i;
                        grvChiTiet.FocusedColumn = grvChiTiet.Columns[j];
                        return true;
                    }
                }
            }
            return false;
        }

        public static void MFieldRequest(DevExpress.XtraLayout.LayoutControlItem Mlayout)
        { ////red, green, blue
            int R = 156, G = 97, B = 65;
            try { R = int.Parse(Vs.Payroll.Properties.Settings.Default["ApplicationColorRed"].ToString()); } catch { R = 156; }
            try { G = int.Parse(Vs.Payroll.Properties.Settings.Default["ApplicationColorGreen"].ToString()); } catch { G = 97; }
            try { B = int.Parse(Vs.Payroll.Properties.Settings.Default["ApplicationColorBlue"].ToString()); } catch { B = 65; }


            Mlayout.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(R, G, B);
            Mlayout.AppearanceItemCaption.Options.UseForeColor = true;


            //try
            //{

            //    Mlayout.AppearanceItemCaption.Font = new System.Drawing.Font(Vs.Payroll.Properties.Settings.Default["ApplicationFontRequestName"].ToString(), float.Parse(Vs.Payroll.Properties.Settings.Default["ApplicationFontRequestSize"].ToString()), (Vs.Payroll.Properties.Settings.Default["ApplicationFontRequestBold"].ToString().ToUpper() == "TRUE" ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) | (Vs.Payroll.Properties.Settings.Default["ApplicationFontRequestItalic"].ToString().ToUpper() == "TRUE" ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular));


            //}
            //catch { Mlayout.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", float.Parse("8.25")); }


            //System.Drawing.FontStyle = new System.Drawing.FontStyle(Settings.Default["ApplicationFontRequestName"].ToString(), float.Parse(Settings.Default["ApplicationFontRequestSize"].ToString()));

            //Font font = new Font(VS.ERP.Properties.Settings.Default["ApplicationFontRequestName"].ToString(), FontStyle.Bold | FontStyle.Underline);


        }





        #endregion
    }
}