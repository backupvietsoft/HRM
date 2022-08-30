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
            LoadComboTo();
            LoadComboCN();
            LoadComboNhom();
            LoadUser(-1);
            enableButon(true);
            Commons.Modules.sLoad = "";
            Enablecontrol(DefaultBoolean.True);


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
                        Enablecontrol(DefaultBoolean.False);
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
                        Enablecontrol(DefaultBoolean.False);
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
                                int lic = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.USERS WHERE LIC = 1 AND ID_USER != " + grvNguoiDung.GetFocusedRowCellValue("ID_USER") + ""));
                                if (lic >= Commons.Modules.iLic)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLincenseDaHet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            var s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGhiUser", grvNguoiDung.GetFocusedRowCellValue("ID_USER").ToString() == "" ?  DBNull.Value : grvNguoiDung.GetFocusedRowCellValue("ID_USER"), ID_NHOMComboBoxEdit.Text == "" ? ID_NHOMComboBoxEdit.EditValue = null : Convert.ToInt64(ID_NHOMComboBoxEdit.EditValue), ID_TOComboBoxEdit.Text == "" ? ID_TOComboBoxEdit.EditValue =null : Convert.ToInt64(ID_TOComboBoxEdit.EditValue), ID_CNSearchLookUpEdit.Text == "" ? ID_CNSearchLookUpEdit.EditValue = null : Convert.ToInt64(ID_CNSearchLookUpEdit.EditValue), USER_NAMETextEdit.EditValue, FULL_NAMETextEdit.EditValue, Commons.Modules.ObjSystems.Encrypt(PASSWORDTextEdit.EditValue.ToString(), true), DESCRIPTIONMemoExEdit.EditValue, USER_MAILTextEdit.EditValue, Convert.ToInt32(ACTIVECheckEdit.EditValue), Convert.ToBoolean(co), Commons.Modules.ObjSystems.Encrypt(USER_NAMETextEdit.EditValue.ToString() + Convert.ToBoolean(chkLIC.EditValue).ToString(), true), Convert.ToBoolean(chkKhach.Checked), Convert.ToBoolean(chkLIC.Checked));
                            Enablecontrol(DefaultBoolean.True);
                            LoadUser(Convert.ToInt32(s));
                            enableButon(true);
                        }
                        catch (Exception ex)
                        {

                        }

                        break;
                    }
                case "khongluu":
                    {
                        grvNguoiDung_FocusedRowChanged(null, null);
                        Enablecontrol(DefaultBoolean.True);
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
            ID_TOComboBoxEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("ID_TO");
            ID_CNSearchLookUpEdit.EditValue = grvNguoiDung.GetFocusedRowCellValue("ID_CN");
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
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUser", Commons.Modules.sIdHT, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_USER"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNguoiDung, grvNguoiDung, dt, false, false, true, true, true, this.Name);
            grvNguoiDung.Columns["ID_USER"].Visible = false;
            grvNguoiDung.Columns["ID_NHOM"].Visible = false;
            grvNguoiDung.Columns["ID_TO"].Visible = false;
            grvNguoiDung.Columns["ID_CN"].Visible = false;
            grvNguoiDung.Columns["PASSWORD"].Visible = false;
            grvNguoiDung.Columns["DESCRIPTION"].Visible = false;
            grvNguoiDung.Columns["ACTIVE"].Visible = false;
            grvNguoiDung.Columns["USER_MAIL"].Visible = false;
            grvNguoiDung.Columns["USER_KHACH"].Visible = false;
            if (iSTT != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(iSTT));
                grvNguoiDung.FocusedRowHandle = grvNguoiDung.GetRowHandle(index);
            }
        }
        private void LoadComboTo()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_TOComboBoxEdit, dt, "ID_TO", "TEN_TO", "");
        }
        private void LoadComboNhom()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhom", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NHOMComboBoxEdit, dt, "ID_NHOM", "TEN_NHOM", "");
            ID_NHOMComboBoxEdit.EditValue = Convert.ToInt64(Commons.Modules.sIdHT);
        }
        private void LoadComboCN()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_CNSearchLookUpEdit, dt, "ID_CN", "TEN_CN", "");
        }

        #endregion

        #region sử lý control
        private void Enablecontrol(DefaultBoolean enable)
        {
            dataLayoutControl1.OptionsView.IsReadOnly = enable;
            grdNguoiDung.Enabled = !Convert.ToBoolean(enable);
        }
        private void Resettest()
        {
            USER_NAMETextEdit.ResetText();
            ACTIVECheckEdit.Checked = true;
            chkLIC.Checked = false;
            ID_NHOMComboBoxEdit.ResetText();
            ID_TOComboBoxEdit.ResetText();
            ID_CNSearchLookUpEdit.ResetText();
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
    }
}
