using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;

namespace VietSoftHRM
{
    public partial class ucListUsers : DevExpress.XtraEditors.XtraUserControl
    {
        public ucListUsers()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void grdListUser_Load(object sender, EventArgs e)
        {
            LoadGridListUser();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        private void LoadGridListUser()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "getAllUsers", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdListUser, grvListUser, dt, false, false, true, true, true,this.Name);
            grvListUser.Columns["ID_NHOM"].Visible = false;
            grvListUser.Columns["TIME_LOGIN"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvListUser.Columns["TIME_LOGIN"].DisplayFormat.FormatString = "dd/MM/yyy hh:mm:ss";
        }
        private void windowButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "resetpass":
                    {
                        frmChangePass change = new frmChangePass(grvListUser.GetFocusedRowCellValue("USER_NAME").ToString());
                        change.ShowDialog();
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

        private void grvListUser_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            try
            {
                if (grvListUser.GetRowCellValue(e.RowHandle, "TIME_LOGIN").ToString() != "")
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LightCyan;
                }
            }
            catch
            {
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmiKick.Visible = false;
            tsmiResetPassword.Visible = false;
            try
            {
                if ((string.IsNullOrEmpty(grvListUser.GetRowCellValue(grvListUser.FocusedRowHandle, grvListUser.Columns["TIME_LOGIN"]).ToString()) ? "" : " ") == " ")
                {
                    tsmiKick.Visible = true;
                }

                if ((string.IsNullOrEmpty(grvListUser.GetRowCellValue(grvListUser.FocusedRowHandle, grvListUser.Columns["USER_NAME"]).ToString()) ? "" : " ") == " ")
                {
                    tsmiResetPassword.Visible = true;
                }
            }
            catch
            {
                contextMenuStrip1.Close();
            }
        }

        private void grvListUser_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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

        private void tsmiResetPassword_Click(object sender, EventArgs e)
        {
            frmChangePass change = new frmChangePass(grvListUser.GetFocusedRowCellValue("USER_NAME").ToString());
            change.ShowDialog();
        }

        private void tsmiKick_Click(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.User(grvListUser.GetFocusedRowCellValue("USER_NAME").ToString(),2);
            LoadGridListUser();
        }
    }
}
