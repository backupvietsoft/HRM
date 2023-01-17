using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
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
    public partial class frmEditCHUC_VU_SAN_XUAT : DevExpress.XtraEditors.XtraForm
    {
        public int iID_TO = -1;
        public frmEditCHUC_VU_SAN_XUAT()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        private void frmEditCHUC_VU_SAN_XUAT_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                EnabelButton(true);
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
                    case "themsua":
                        {
                            LoadData();
                            Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                            EnabelButton(false);
                            break;
                        }
                    case "xoa":
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.CHUC_VU_SAN_XUAT WHERE ID = " + grvData.GetFocusedRowCellValue("ID_CVSX") + "");
                            LoadData();
                            break;
                        }

                    case "luu":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            DataTable dtSoure = new DataTable();
                            dtSoure = (DataTable)grdData.DataSource;    
                            if(!KiemTraLuoi(dtSoure))
                            {
                                return;
                            }
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            else
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            LoadData();
                            EnabelButton(true);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                            break;
                        }
                    case "khongluu":
                        {
                            LoadData();
                            EnabelButton(true);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
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

        private void LoadData()
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spEditCHUC_VU_SAN_XUAT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["ID_CVSX"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, true, true, true, this.Name);
                grvData.Columns["ID_CVSX"].Visible = false;

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_TO", "TEN_TO", "ID_TO", grvData, dt, this.Name);
            }
            catch (Exception ex)
            {
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spEditCHUC_VU_SAN_XUAT", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
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
                //Số hợp đồng lao động
                if (!KiemDuLieu(grvData, dr, "TEN_CV", true, 250, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieu(grvData, dr, "ID_TO", true, 250, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieu(grvData, dr, "NHOM", true, 250, this.Name))
                {
                    errorCount++;
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

            }
            catch (Exception ex)
            {

            }
        }

        private void grvData_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

        }
        private void EnabelButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;

            grvData.OptionsBehavior.Editable = !visible;
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
