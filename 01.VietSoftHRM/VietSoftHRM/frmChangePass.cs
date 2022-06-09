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
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
namespace VietSoftHRM
{
    public partial class frmChangePass : DevExpress.XtraEditors.XtraForm
    {
        public frmChangePass(string sUser)
        {
            InitializeComponent();
            if (sUser != "-1")
            {
                Eneblecontrol(sUser);
            }
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root);

        }
        private void Eneblecontrol(string sUser)
        {
            USER_NAMETextEdit.EditValue = (sUser != "-1") ? sUser : Commons.Modules.UserName;
            PASSWORDOLDtextEdit.EditValue = Commons.Modules.ObjSystems.Decrypt(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT PASSWORD FROM dbo.USERS WHERE USER_NAME ='" + sUser + "'").ToString(), true);
            PASSWORDOLDtextEdit.ReadOnly = true;
            USER_NAMETextEdit.ReadOnly = true;
        }
        private void ChangePassWord()
        {
            //kiêm tra pass cũ có đúng không
            string sPass = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT PASSWORD FROM dbo.USERS WHERE USER_NAME ='" + USER_NAMETextEdit.EditValue + "'").ToString();
            if (PASSWORDOLDtextEdit.EditValue.ToString() != Commons.Modules.ObjSystems.Decrypt(sPass, true))
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgPassWorkkhongdung"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChangePassword"), MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (PASSWORDtextEdit.EditValue.ToString() != PASSWORDNEWtextEdit.EditValue.ToString())
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgPassWordKhongKhop"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChangePassword"), MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            //update password
            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.USERS SET PASSWORD = '" + Commons.Modules.ObjSystems.Encrypt(PASSWORDNEWtextEdit.EditValue.ToString(), true) + "' WHERE USER_NAME = '" + USER_NAMETextEdit.EditValue + "'");
            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgSaveThanhCong"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChangePassword"), MessageBoxButtons.OK, MessageBoxIcon.Information); this.Close();
            //kiểm tra pass mới có giống nhâu không
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        ChangePassWord();
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {

        }
    }
}