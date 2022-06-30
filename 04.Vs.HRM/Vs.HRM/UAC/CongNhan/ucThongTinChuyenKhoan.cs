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

namespace Vs.HRM
{
    public partial class ucThongTinChuyenKhoan : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucThongTinChuyenKhoan _instance;
        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BV;
        int MS_TINH;
        public static ucThongTinChuyenKhoan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucThongTinChuyenKhoan();
                return _instance;
            }
        }


        public ucThongTinChuyenKhoan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region bảo hiểm y tế
        private void ucThongTinChuyenKhoan_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridTTChuyenKhoan();
            Commons.Modules.sLoad = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridTTChuyenKhoan();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridTTChuyenKhoan();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridTTChuyenKhoan();
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        enableButon(false);
                        break;
                    }

                case "luu":
                    {
                        Savedata();
                        LoadGridTTChuyenKhoan();
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        LoadGridTTChuyenKhoan();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadGridTTChuyenKhoan()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKTTenKhongDauSTK", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grdTTChuyenKhoan.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTTChuyenKhoan, grvTTChuyenKhoan, dt, true, false, false, false, true, this.Name);
                grvTTChuyenKhoan.Columns["ID_CN"].Visible = false;
                grvTTChuyenKhoan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvTTChuyenKhoan.Columns["TEN_XN"].OptionsColumn.AllowEdit = false;
                grvTTChuyenKhoan.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                grvTTChuyenKhoan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
            }
            else
            {
                grdTTChuyenKhoan.DataSource = dt;
            }
        }
        private void Savedata()
        {
            try
            {
                //tạo một datatable 
                string sBTTTCK = "sBTTTCK" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTTTCK, Commons.Modules.ObjSystems.ConvertDatatable(grvTTChuyenKhoan), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSavTTChuyenKhoan", sBTTTCK);
            }
            catch (Exception ex)
            {

            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            grvTTChuyenKhoan.OptionsBehavior.Editable = !visible;
            searchControl.Visible = visible;
        }
        #endregion

        private void grvTTChuyenKhoan_RowCountChanged(object sender, EventArgs e)
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
    }
}
