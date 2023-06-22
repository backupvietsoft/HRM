﻿using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Linq;

namespace VietSoftHRM
{
    public partial class ucNHOM : DevExpress.XtraEditors.XtraUserControl
    {
        private int grid = 1;
        private bool co = true;
        private int dem;

        public ucNHOM()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        #region sự kiện form
        private void ucNHOM_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            LoadgrdNhom();
            enableButon(true);

        }
        private void grvNhom_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadUser();
        }
        private void grvUser_Click(object sender, EventArgs e)
        {
            grid = 1;
        }
        private void grvNhom_Click(object sender, EventArgs e)
        {
            try
            {
                grid = 1;
                Commons.Modules.sIdHT = grvNhom.GetFocusedRowCellValue("ID_NHOM").ToString();
            }
            catch
            {
            }

        }
        private void grdUser_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                XoaUser();
            }
        }
        private void grdNhom_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                XoaNhom();
            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        enableButon(false);
                        enabledControl(false);
                        co = true;
                        if (grid == 1)
                        {
                            dem = grvNhom.RowCount;
                            AddnewRow(grvNhom, true);
                            //grvUser.OptionsBehavior.ReadOnly = true;
                            grvNhom.OptionsBehavior.ReadOnly = false;
                        }
                        else
                        {
                            dem = grvUser.RowCount;
                            //AddnewRow(grvUser, true);
                            grvNhom.OptionsBehavior.ReadOnly = true;
                            //grvUser.OptionsBehavior.ReadOnly = false;
                        }
                        break;
                    }
                case "xoa":
                    {
                        if (grid == 1)
                        {
                            XoaNhom();
                        }
                        else
                        {
                            XoaUser();
                        }
                        break;
                    }
                case "sua":
                    {
                        enabledControl(false);
                        enableButon(false);
                        co = false;
                        if (grid == 1)
                        {
                            AddnewRow(grvNhom, false);
                            grvUser.OptionsBehavior.ReadOnly = true;
                            grvNhom.OptionsBehavior.ReadOnly = false;
                        }
                        else
                        {
                            //AddnewRow(grvUser, false);
                            grvUser.OptionsBehavior.ReadOnly = false;
                            grvNhom.OptionsBehavior.ReadOnly = true;
                        }
                        break;
                    }
                case "luu":
                    {
                        enabledControl(true);
                        enableButon(true);
                        grvNhom.PostEditor();
                        grvNhom.UpdateCurrentRow();
                        GhiNhom();
                        DeleteAddRow(grvNhom);
                        DeleteAddRow(grvUser);
                        break;
                    }
                case "khongluu":
                    {
                        LoadgrdNhom();
                        enabledControl(true);
                        enableButon(true);
                        DeleteAddRow(grvNhom);
                        DeleteAddRow(grvUser);
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

        #endregion


        #region hàm xử lý
        private void LoadgrdNhom()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_NHOM,T1.TEN_NHOM, T1.TEN_NHOM_A, T1.TEN_NHOM_H, GHI_CHU, ISNULL(T1.SET_UP,0) SET_UP FROM dbo.NHOM T1"));
            dt.Columns["TEN_NHOM"].ReadOnly = false;
            dt.Columns["GHI_CHU"].ReadOnly = false;
            //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_NHOM"] };
            dt.Columns["ID_NHOM"].ReadOnly = false;
            dt.Columns["SET_UP"].ReadOnly = false;
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNhom, grvNhom, dt, true, false, false, true, true, this.Name);
            grdNhom.DataSource = dt;
            grvNhom.Columns["ID_NHOM"].Visible = false;
            grvNhom.Columns["GHI_CHU"].Width = 200;
            //if (!string.IsNullOrEmpty(Commons.Modules.sIdHT))
            //{
            //    int index = dt.Rows.IndexOf(dt.Rows.Find(Convert.ToInt32(Commons.Modules.sIdHT)));
            //    grvNhom.FocusedRowHandle = grvNhom.GetRowHandle(index);
            //}
            grvNhom_Click(null, null);

        }
        //load user
        private void LoadUser()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetUserbyGroup", Convert.ToInt64(grvNhom.GetFocusedRowCellValue("ID_NHOM")), Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["ACTIVE"].ReadOnly = false;
                dt.Columns["LIC"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdUser, grvUser, dt, false, false, true, true, true, this.Name);
                //add combobox nhan vien
                grvUser.Columns["ID_USER"].Visible = false;
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvUser, "spGetCongNhan", "ID_CN", "CONG_NHAN");
                Commons.Modules.ObjSystems.AddCombXtra("ID_TO", "TEN_TO", grvUser, "spGetTo", "ID_TO", "TO");
            }
            catch
            {

            }
        }
        private void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = true;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            }
        }
        private void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }
        private void LockGrid(GridView grid, bool TT)
        {
            for (int i = 0; i <= grid.Columns.Count - 1; i++)
            {
                grid.Columns[i].OptionsColumn.AllowEdit = !TT;
                grid.Columns[i].OptionsColumn.ReadOnly = TT;
            }
        }
        private void XoaNhom()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteNhom"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.NHOM WHERE ID_NHOM = " + grvNhom.GetFocusedRowCellValue("ID_NHOM") + "");
                grvNhom.DeleteSelectedRows();
                LoadUser();
                grvNhom_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void XoaUser()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteUser"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.USERS WHERE ID_USER = " + grvUser.GetFocusedRowCellValue("ID_USER") + "");
                grvUser.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void GhiNhom()
        {
            string sBT = "sBTNHOM" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdNhom), "");
                string sSQL = "INSERT INTO dbo.NHOM(TEN_NHOM, TEN_NHOM_A, TEN_NHOM_H, GHI_CHU, SET_UP)\r\nSELECT TEN_NHOM, TEN_NHOM_A, TEN_NHOM_H, GHI_CHU, SET_UP FROM " + sBT + " WHERE ISNULL(ID_NHOM,0) = 0\r\n\r\nUPDATE T1\r\nSET T1.TEN_NHOM = T2.TEN_NHOM,\r\n\tT1.TEN_NHOM_A = T2.TEN_NHOM_A,\r\n\tT1.TEN_NHOM_H = T2.TEN_NHOM_H,\r\n\tT1.GHI_CHU = T2.GHI_CHU,\r\n\tT1.SET_UP = T2.SET_UP\r\nFROM dbo.NHOM T1 INNER JOIN " + sBT + " T2 ON T1.ID_NHOM = T2.ID_NHOM";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                Commons.Modules.ObjSystems.Alert("Lưu thành công", Commons.Form_Alert.enmType.Success);
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        //private void GhiNhom()
        //{
        //    try
        //    {
        //        for (int i = 0; i < grvNhom.RowCount; i++)
        //        {
        //            //sữa những dòng đã có
        //            if(Commons.Modules.ObjSystems.IsnullorEmpty(grvNhom.GetRowCellValue(i, "TEN_NHOM")))
        //            { break; }
        //            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spGhiNhom", grvNhom.GetRowCellValue(i, "ID_NHOM"), grvNhom.GetRowCellValue(i, "TEN_NHOM"), grvNhom.GetRowCellValue(i, "GHI_CHU"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }

        //}
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;

            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
        }

        private void enabledControl(bool enabled)
        {
            if (enabled == true)
            {
                grdNhom.Enabled = true;
                return;
            }
            if (grid == 1)
            {
                grdNhom.Enabled = !enabled;
            }
            else
            {
                grdNhom.Enabled = enabled;
            }
        }
        private bool CheckUser(string user)
        {
            //kiểm tra user name đã có tồn tại hay chưa
            int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.USERS WHERE USER_NAME = '" + user + "'"));
            if (n > 1)
                return false;
            else
                return true;
        }
        private void grvUser_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Columns.GridColumn sMaNhom = View.Columns["ID_NHOM"];
            DevExpress.XtraGrid.Columns.GridColumn sTenUser = View.Columns["USER_NAME"];
            DevExpress.XtraGrid.Columns.GridColumn sFullName = View.Columns["FULL_NAME"];

            if (View.GetRowCellValue(e.RowHandle, sTenUser).ToString() == "")
            {
                e.Valid = false;
                View.SetColumnError(sTenUser, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage));
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "MsgKiemtraTenUserNULL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            if (View.GetRowCellValue(e.RowHandle, sTenUser).ToString() == "")
            {
                e.Valid = false;
                View.SetColumnError(sTenUser, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage));
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "MsgKiemtraTenUserNULL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (!CheckUser(View.GetRowCellValue(e.RowHandle, sTenUser).ToString()))
            {
                e.Valid = false;
                View.SetColumnError(sTenUser, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgDatontainguoidung", Commons.Modules.TypeLanguage));
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "MsgDatontainguoidung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            //if (View.GetRowCellValue(e.RowHandle, sFullName).ToString() == "")
            //{
            //    e.Valid = false;
            //    View.SetColumnError(sFullName, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraFullNameNULL", Commons.Modules.TypeLanguage));
            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraFullNameNULL", Commons.Modules.TypeLanguage), "", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            //}
        }

        private void grvNhom_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn sTenNhom = View.Columns["TEN_NHOM"];
                if (View.GetRowCellValue(e.RowHandle, sTenNhom).ToString() == "")
                {
                    e.Valid = false;
                    View.SetColumnError(sTenNhom, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTenNhomKhongNull", Commons.Modules.TypeLanguage));
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "MsgKiemtraTenUserNULL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }
            }
            catch
            {
            }
        }

        private void grvUser_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (grvUser.FocusedColumn.FieldName == "LIC" && Convert.ToBoolean(e.Value) == true)
            {
                if (Commons.Modules.iLic == -1)
                {
                    string sSql = @"https://api.vietsoft.com.vn/VS.Api/Support/SumNumberlicense?SoftwareProductID=2&CustomerID=" + Commons.Modules.iCustomerID;
                    //Commons.Mod.iLic = "";
                    DataTable dtTmp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.getDataAPI(sSql);
                    try
                    {
                        Commons.Modules.iLic = int.Parse(dtTmp.Rows[0][0].ToString());
                    }

                    catch { Commons.Modules.iLic = 3; }
                }

                //so sánh với dữ liệu cơ sở dữ liệu
                int lic = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.USERS WHERE LIC = 1 AND ID_NHOM != " + grvNhom.GetFocusedRowCellValue("ID_NHOM") + ""));
                int count = Commons.Modules.ObjSystems.ConvertDatatable(grvUser).AsEnumerable().Count(x => Convert.ToBoolean(x["LIC"]) == true);
                if ((lic + count) >= Commons.Modules.iLic)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLincenseDaHet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    grvUser.SetRowCellValue(e.RowHandle, "LIC", false);
                    return;
                }
            }
        }

        private void grvNhom_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("ID_NHOM", Convert.ToInt64(0));
                view.SetFocusedRowCellValue("SET_UP", 0);
            }
            catch { }
        }

        private void grvNhom_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvNhom_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
    #endregion
    //load nhom

}
