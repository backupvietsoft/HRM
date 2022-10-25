using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using DevExpress.XtraLayout;
using System.Drawing;

namespace VietSoftHRM
{
    public partial class ucNGUOIDUNG : DevExpress.XtraEditors.XtraUserControl
    {
        private bool co = true;
        private bool flag = false;
        public ucNGUOIDUNG()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { layoutControlGroup1 }, windowsUIButton);
        }

        #region sự kiện form
        private void ucNGUOIDUNG_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            ///////LoadComboTo();
            /////// LoadComboCN();
            LoadComboNhom();
            LoadUser(-1);
            enableButon(true);
            Commons.Modules.sLoad = "";
            Enablecontrol(true);


        }
        private void grdNguoiDung_ProcessGridKey(object sender, KeyEventArgs e)
        {
            var grid = sender as GridControl;
            var view = grid.FocusedView as GridView;
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (grvNguoiDung.RowCount == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuDeXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteUser"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    //xóa
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM	dbo.USERS WHERE ID_USER = " + grvNguoiDung.GetFocusedRowCellValue("ID_USER") + " ");
                    view.DeleteSelectedRows();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        enableButon(false);
                        Resettest();
                        co = true;
                        Enablecontrol(false);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }
                case "sua":
                    {
                        enableButon(false);
                        co = false;
                        Enablecontrol(false);
                        break;
                    }
                case "luu":
                    {
                        try
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            //kiểm tra lic khi 

                            if (chkLIC.Checked)
                            {
                                int lic = 0;
                                try { lic = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.USERS WHERE LIC = 1 AND ID_USER != " + grvNguoiDung.GetFocusedRowCellValue("ID_USER") + "")); } catch { }
                                if (lic >= Commons.Modules.iLic)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLincenseDaHet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            var s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGhiUser", (grvNguoiDung.RowCount == 0 ? "" : grvNguoiDung.GetFocusedRowCellValue("ID_USER").ToString()) == "" ? DBNull.Value : grvNguoiDung.GetFocusedRowCellValue("ID_USER"), ID_NHOMComboBoxEdit.Text == "" ? ID_NHOMComboBoxEdit.EditValue = null : Convert.ToInt64(ID_NHOMComboBoxEdit.EditValue), USER_NAMETextEdit.EditValue, FULL_NAMETextEdit.EditValue, Commons.Modules.ObjSystems.Encrypt(PASSWORDTextEdit.EditValue.ToString(), true), DESCRIPTIONMemoExEdit.EditValue, USER_MAILTextEdit.EditValue, Convert.ToInt32(ACTIVECheckEdit.EditValue), Convert.ToBoolean(co), Commons.Modules.ObjSystems.Encrypt(USER_NAMETextEdit.EditValue.ToString() + Convert.ToBoolean(chkLIC.EditValue).ToString(), true), Convert.ToBoolean(chkKhach.Checked), Convert.ToBoolean(chkLIC.Checked));
                            Enablecontrol(true);
                            LoadUser(Convert.ToInt32(s));
                            enableButon(true);
                        }
                        catch
                        {
                        }

                        break;
                    }
                case "khongluu":
                    {
                        grvNguoiDung_FocusedRowChanged(null, null);
                        Enablecontrol(true);
                        enableButon(true);
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
        private void grvNguoiDung_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            USER_NAMETextEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("USER_NAME");
            ACTIVECheckEdit.EditValue = Convert.ToBoolean(grvNguoiDung.GetFocusedRowCellValue("ACTIVE"));
            chkLIC.EditValue = Convert.ToBoolean(grvNguoiDung.GetFocusedRowCellValue("LIC"));
            ID_NHOMComboBoxEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("ID_NHOM");
            /// ID_TOComboBoxEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("ID_TO");
            ////ID_CNSearchLookUpEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("ID_CN");
            FULL_NAMETextEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("FULL_NAME");
            PASSWORDTextEdit.EditValue = Commons.Modules.ObjSystems.Decrypt(grvNguoiDung.GetFocusedRowCellValue("PASSWORD").ToString(), true);
            USER_MAILTextEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("USER_MAIL");
            DESCRIPTIONMemoExEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("DESCRIPTION");
            chkKhach.EditValue = Convert.ToBoolean(grvNguoiDung.GetFocusedRowCellValue("USER_KHACH"));
            Commons.Modules.sLoad = "";
        }
        #endregion

        #region hàm load
        private void LoadUser(int iSTT)
        {
            Commons.Modules.sLoad = "0Load";
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUser", Commons.Modules.sIdHT, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_USER"] };
            try
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNguoiDung, grvNguoiDung, dt, false, false, false, true, true, this.Name);
                grvNguoiDung.Columns["ID_USER"].Visible = false;
                grvNguoiDung.Columns["ID_NHOM"].Visible = false;
                grvNguoiDung.Columns["ID_TO"].Visible = false;
                grvNguoiDung.Columns["ID_CN"].Visible = false;
                grvNguoiDung.Columns["PASSWORD"].Visible = false;
                grvNguoiDung.Columns["DESCRIPTION"].Visible = false;
                grvNguoiDung.Columns["ACTIVE"].Visible = false;
                grvNguoiDung.Columns["USER_MAIL"].Visible = false;
                grvNguoiDung.Columns["USER_KHACH"].Visible = false;

                grvNguoiDung.Columns["TIME_LOGIN"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvNguoiDung.Columns["TIME_LOGIN"].DisplayFormat.FormatString = "dd/MM/yyy hh:mm:ss";

                if (iSTT != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iSTT));
                    grvNguoiDung.FocusedRowHandle = grvNguoiDung.GetRowHandle(index);
                }
                grvNguoiDung_FocusedRowChanged(null, null);
            }
            catch
            {
                grdNguoiDung.DataSource = dt;
            }
            Commons.Modules.sLoad = "";
        }
        //////private void LoadComboTo()
        //////{
        //////    DataTable dt = new DataTable();
        //////    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
        //////    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOComboBoxEdit, dt, "ID_TO", "TEN_TO", "");
        //////}
        private void LoadComboNhom()
        {
            DataTable dt = new DataTable();
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NHOMComboBoxEdit, Commons.Modules.ObjSystems.DataNhomUser(false), "ID_NHOM", "TEN_NHOM", "");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNhomUser, Commons.Modules.ObjSystems.DataNhomUser(false), "ID_NHOM", "TEN_NHOM", "");
            cboNhomUser.EditValue = Convert.ToInt64(Commons.Modules.sIdHT);
        }
        ////////private void LoadComboCN()
        ////////{
        ////////    DataTable dt = new DataTable();
        ////////    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
        ////////    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_CNSearchLookUpEdit, dt, "ID_CN", "TEN_CN", "");
        ////////}

        #endregion

        #region sử lý control
        private void Enablecontrol(bool enable)
        {
            //dataLayoutControl1.OptionsView.IsReadOnly = enable;
            USER_NAMETextEdit.Properties.ReadOnly = enable;
            ACTIVECheckEdit.Properties.ReadOnly = enable;
            chkLIC.Properties.ReadOnly = enable;
            ID_NHOMComboBoxEdit.Properties.ReadOnly = enable;
            /// ID_TOComboBoxEdit.Properties.ReadOnly = enable;
            // ID_CNSearchLookUpEdit.Properties.ReadOnly = enable;
            FULL_NAMETextEdit.Properties.ReadOnly = enable;
            PASSWORDTextEdit.Properties.ReadOnly = enable;
            USER_MAILTextEdit.Properties.ReadOnly = enable;
            DESCRIPTIONMemoExEdit.Properties.ReadOnly = enable;
            grdNguoiDung.Enabled = enable;
            cboNhomUser.Properties.ReadOnly = !enable;
        }
        private void Resettest()
        {
            USER_NAMETextEdit.ResetText();
            ACTIVECheckEdit.Checked = true;
            chkLIC.Checked = false;
            ID_NHOMComboBoxEdit.ResetText();
            ///ID_TOComboBoxEdit.ResetText();
            /// ID_CNSearchLookUpEdit.ResetText();
            FULL_NAMETextEdit.ResetText();
            PASSWORDTextEdit.ResetText();
            USER_MAILTextEdit.ResetText();
            DESCRIPTIONMemoExEdit.ResetText();
        }

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


        #endregion

        private void chkLIC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkLIC_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (chkLIC.Checked == true)
            {
                flag = false;
            }
            else
            {
                flag = false;
            }
        }

        private void grvNguoiDung_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            try
            {
                if (grvNguoiDung.GetRowCellValue(e.RowHandle, "TIME_LOGIN").ToString() != "")
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
            catch
            {
            }
        }
        private void cboNhomUser_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sIdHT = cboNhomUser.EditValue.ToString();
            LoadUser(-1);
        }

        private void tsmiResetPassword_Click(object sender, EventArgs e)
        {
            frmChangePass change = new frmChangePass(grvNguoiDung.GetFocusedRowCellValue("USER_NAME").ToString());
            change.ShowDialog();
        }

        private void tsmiKick_Click(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.User(grvNguoiDung.GetFocusedRowCellValue("USER_NAME").ToString(), 2);
            LoadUser(-1);
        }

        private void grvNguoiDung_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InDataRow)
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                {
                    contextMenuStrip1.Hide();
                }
            }
            catch
            {
            }
        }

        private void FULL_NAMETextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
