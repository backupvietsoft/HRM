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
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Menu;

namespace Vs.TimeAttendance
{
    public partial class frmTinhTrangBangCong : DevExpress.XtraEditors.XtraForm
    {
        public DateTime dNgay;
        public Int64 iID_DV = -1;
        public Int64 iID_XN = -1;
        public Int64 iID_TO = -1;
        public frmTinhTrangBangCong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }
        private void frmTinhTrangBangCong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                //dinh dang ngay gio
                datThang.EditValue = dNgay;
                datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
                datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.EditFormat.FormatString = "MM/yyyy";
                datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datThang.Properties.Mask.EditMask = "MM/yyyy";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                cboDV.EditValue = iID_DV;
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXN, Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(cboDV.EditValue), true), "ID_XN", "TEN_XN", "TEN_XN");
                cboXN.EditValue = iID_XN;
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), true), "ID_TO", "TEN_TO", "TEN_TO");
                cboTo.EditValue = iID_TO;

                LoadData();
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void LoadData()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhTrangCong", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = iID_DV;
            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
            cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datThang.Text);
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
            grvData.Columns["ID_TO"].Visible = false;
            dt = new DataTable();
            dt = ds.Tables[1].Copy();

            DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TT", "TEN_TT", "ID_TT", grvData, dt, this.Name);
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
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

        public DXMenuItem MCreateMenuLenWeb(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblLenWeb", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(LenWeb));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void LenWeb(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacMuonChuyenTinhTrang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return;
                string sBT = "sBTKiemSoatCong" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhTrangCong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sBT);
                LoadData();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable("sBTKiemSoatCong" + Commons.Modules.iIDUser);
            }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (grvData.GetFocusedRowCellValue("ID_TT").ToString() == "2") return;
                    DevExpress.Utils.Menu.DXMenuItem itemLenWeb = MCreateMenuLenWeb(view, irow);
                    e.Menu.Items.Add(itemLenWeb);
                }
            }
            catch
            {
            }
        }

        #endregion

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO");
            LoadData();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
    }
}