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
using NPOI.HSSF.Record.Chart;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;

namespace Vs.Payroll
{
    public partial class frmPhieuCongDoanLog : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public DateTime dThang;
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public frmPhieuCongDoanLog()
        {
            InitializeComponent();

            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();

            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmPhieuCongDoanLog_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadChuyen();
            datThang.DateTime = dThang;
            datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datThang.Properties.EditFormat.FormatString = "MM/yyyy";
            datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            datThang.Properties.Mask.EditMask = "MM/yyyy";


            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";

            repositoryItemTimeEdit1.NullText = "dd/MM/yyyy HH:mm:ss";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void LoadChuyen()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCboChuyenThucHien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_DV, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboChuyen.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboChuyen.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luugiulieu":
                    {
                        if (!LuuDuLieu())
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                            return;
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                            LoadData();
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.DialogResult = DialogResult.OK;
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
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanLog", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboChuyen.EditValue;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datThang.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, true, true, true, this.Name);
                    grvData.Columns["NGAY_LUU"].ColumnEdit = this.repositoryItemTimeEdit1;
                    grvData.Columns["ID_CHUYEN_SD"].Visible = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch { }
        }
        private bool LuuDuLieu()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanLog", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboChuyen.EditValue;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datThang.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void datThang_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
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
        public DXMenuItem MCreateMenuResetDuLieu(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblResetDuLieu", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(ResetDuLieu));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void ResetDuLieu(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacMuonRestDuLieu") + " " + grvData.GetFocusedRowCellValue("NGAY_LUU").ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhieuCongDoanLog", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@iLan", SqlDbType.Int).Value = Convert.ToInt32(grvData.GetFocusedRowCellValue("LAN_LUU"));
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = datThang.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong") + dt.Rows[0][1].ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                LoadData();
            }
            catch { }
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

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuResetDuLieu(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}