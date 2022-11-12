using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Menu;

namespace Vs.HRM
{
    public partial class ucHoanChinhHSNhanSu : DevExpress.XtraEditors.XtraUserControl
    {
        private ucCTQLNS ucNS;
        private long iID_NS = -1;
        public static ucHoanChinhHSNhanSu _instance;
        public static ucHoanChinhHSNhanSu Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucHoanChinhHSNhanSu();
                return _instance;
            }
        }


        public ucHoanChinhHSNhanSu()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region Hoàn chỉnh hồ sơ nhân sự
        private void ucHoanChinhHSNhanSu_Load(object sender, EventArgs e)
        {
            try
            {

                Thread.Sleep(1000);
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                LoadData();
                Commons.Modules.sLoad = "";
            }
            catch { }


        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }

            }
        }
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadData()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spHoanChinhHSNhanSu", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };

            if (grdData.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, false, true, this.Name);
                grvData.Columns["ID_CN"].Visible = false;
                grvData.Columns["ID_HDLD"].Visible = false;
                grvData.Columns["TT_HDLD_DANG_SOAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvData.Columns["TT_HDLD_CHUA_CO"].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvData.Columns["TT_HDLD_HET_HD"].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvData.Columns["TT_QTCT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvData.Columns["TT_QTCT"].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvData.Columns["TT_LUONG"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvData.Columns["TT_LUONG"].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvData.Columns["TT_KHEN_THUONG"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvData.Columns["TT_KHEN_THUONG"].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            else
            {
                grdData.DataSource = dt;
            }

            if (iID_NS != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(iID_NS));
                grvData.FocusedRowHandle = grvData.GetRowHandle(index);
                grvData.ClearSelection();
                grvData.SelectRow(index);
            }
        }
        #endregion
        private void grvData_RowCountChanged(object sender, EventArgs e)
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
                iID_NS = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_CN"));
                ucNS = new HRM.ucCTQLNS(Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                ucNS.Refresh();
                ucNS.flag = true;
                string sTenLab = "";
                if (grvData.FocusedColumn.FieldName.Substring(0, 7) == "TT_HDLD")
                {
                    sTenLab = "labHopDong";
                }
                else if (grvData.FocusedColumn.FieldName.ToString() == "TT_QTCT")
                {
                    sTenLab = "labCongTac";
                }
                else if (grvData.FocusedColumn.FieldName.ToString() == "TT_LUONG")
                {
                    sTenLab = "labTienLuong";
                }
                else
                {
                    sTenLab = "LabKhenThuong";
                }
                ucNS.sTenLab = sTenLab;
                //ns.accorMenuleft = accorMenuleft;
                layoutControl1.Hide();
                windowsUIButton.Visible = false;
                this.Controls.Add(ucNS);
                ucNS.Dock = DockStyle.Fill;
                ucNS.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex) { }
        }

        // cap nhat hop dong
        public DXMenuItem MCreateMenuCapNhatTT(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatTinhTrang", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatTT));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatTT(object sender, EventArgs e)
        {
            try
            {
                iID_NS = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_CN"));
                string sSQL = "UPDATE dbo.HOP_DONG_LAO_DONG SET ID_TT = 2 WHERE ID_CN = " + grvData.GetFocusedRowCellValue("ID_CN") + " AND ID_HDLD = " + grvData.GetFocusedRowCellValue("ID_HDLD") + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                LoadData();

                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK);
            }
            catch (Exception ex) { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucNS.Hide();
            layoutControl1.Show();
            windowsUIButton.Visible = true;
            LoadData();
        }
        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (grvData.FocusedColumn.FieldName.Substring(0, 3).ToString() != "TT_") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuThongTinNS(view, irow);
                    e.Menu.Items.Add(itemTTNS);
                    if (grvData.FocusedColumn.FieldName.ToString() == "TT_HDLD_DANG_SOAN" && grvData.GetFocusedRowCellValue("TT_HDLD_DANG_SOAN").ToString() != "")
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemCapNhatTT = MCreateMenuCapNhatTT(view, irow);
                        e.Menu.Items.Add(itemCapNhatTT);
                    }
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
