﻿using System;
using System.Data;
using System.Drawing;
using Microsoft.ApplicationBlocks.Data;
using System.Threading;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace VietSoftHRM
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
            UserLookAndFeel.Default.SkinName = VietSoftHRM.Properties.Settings.Default["ApplicationSkinName"].ToString();
        }
        #region design giao dien
        private void Txt_user_Click(object sender, EventArgs e)
        {
            txt_user.ResetText();
            txt_user.ForeColor = Color.SteelBlue;
            pan_user.BackColor = Color.SteelBlue;
            pic_user.Image = Properties.Resources.icon_user1;

        }

        private void Txt_pass_Click(object sender, EventArgs e)
        {
            txt_pass.ResetText();
            txt_pass.ForeColor = Color.SteelBlue;
            pan_pass.BackColor = Color.SteelBlue;
            pic_pass.Image = Properties.Resources.icon_pass1;
        }

        private void Cbo_database_Click(object sender, EventArgs e)
        {
            cbo_database.ForeColor = Color.SteelBlue;
            pan_database.BackColor = Color.SteelBlue;
            pic_database.Image = Properties.Resources.icon_data1;

        }
        private void Txt_user_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_user.Text))
            {
                txt_user.Text = "Username";
            }
            txt_user.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            pan_user.BackColor = Color.FromArgb(80, 80, 80);
            pic_user.Image = Properties.Resources.icon_user;
        }
        private void Txt_pass_Validated(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_pass.Text))
            {
                txt_pass.Text = "Password";
            }
            txt_pass.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            pan_pass.BackColor = Color.FromArgb(80, 80, 80);
            pic_pass.Image = Properties.Resources.icon_pass;
        }

        private void Cbo_database_Validated(object sender, EventArgs e)
        {

            cbo_database.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            pan_database.BackColor = Color.FromArgb(80, 80, 80);
            pic_database.Image = Properties.Resources.icon_data;
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(1000);
            LoadcboDataBase();
            LoadUserPass();
            Commons.Modules.chamCongK = false;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        #endregion

        #region Load control
        private void LoadcboDataBase()
        {
            //cbo_database.Properties.datas
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM sys.sysdatabases where name LIKE '%HRM%'"));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_database, dt, "name", "name", "");
        }
        #endregion
        //login
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                SaveLogin();
                SaveDatabase();
                if(txt_user.Text.Trim() == "admink")
                {
                    Commons.Modules.chamCongK = true;
                }
                //add user
                this.Hide();
                frmMain form2 = new frmMain();
                form2.ShowDialog();
                this.Close();
            }
        }
        private bool checkLogin()
        {
            string sSql;
            //kiểm tra user đã có hay chưa
            sSql = "SELECT COUNT(*) FROM dbo.USERS WHERE USER_NAME = '"+txt_user.EditValue.ToString().Trim()+"'";
            if(Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text,sSql)) == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTaiKhoanChuaDangKy"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }
            //kiểm tra mật khẩu có đúng hay không
            sSql = "SELECT PASSWORD FROM dbo.USERS WHERE USER_NAME = '" + txt_user.EditValue.ToString().Trim() + "'";
            if (Commons.Modules.ObjSystems.Decrypt(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString(), true).ToString() != txt_pass.EditValue.ToString().Trim()) 
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgsaiPassword"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }
            //kiểm tra tài khoảng có được active hay chưa
            sSql = "SELECT ACTIVE FROM dbo.USERS WHERE USER_NAME ='"+ txt_user.EditValue.ToString().Trim() + "'";
            if (Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != true)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTaiKhoanChuaKichHoat"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                //kiểm tra user check
                sSql = "SELECT USER_PQ FROM dbo.USERS WHERE USER_NAME = '" + txt_user.EditValue.ToString().Trim() + "'";
                if (Convert.ToBoolean(Commons.Modules.ObjSystems.Decrypt(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString(), true).Replace(txt_user.EditValue.ToString().Trim(), "")) != true)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgUserChuaDangKyLincense"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgUserChuaDangKyLincense"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }
            

            ////kiểm tra user đã đăng nhập
            if(Commons.Modules.ObjSystems.checkExitsUser(txt_user.EditValue.ToString().Trim()) == false)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgUserDaDuocDangNhap"), this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void SaveLogin()
        {
            //if (chk_pass.Checked == false && chk_user.Checked == false) return;
            string user;
            string pass;
            if (che_Reuser.Checked)
            {
                user = txt_user.EditValue.ToString();
            }
            else
            {
                user = "";
            }
            if (che_Repass.Checked)
            {
                pass = txt_pass.EditValue.ToString();
            }
            else
            {
                pass = "";
            }
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                ds.Tables[0].Rows[0]["U"] = user;
                ds.Tables[0].Rows[0]["P"] = pass;
                ds.Tables[0].Rows[0]["D"] = cbo_database.EditValue;
                ds.Tables[0].Rows[0]["N"] = Commons.Modules.TypeLanguage;
                ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                Commons.Modules.UserName = txt_user.Text.Trim();
                Commons.Modules.iIDUser = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_USER FROM dbo.USERS WHERE USER_NAME ='" + Commons.Modules.UserName + "'"));
            }
            catch
            {

            }
        }
        private void LoadUserPass()
        {
            try
            {
                string user, pass;
                DataSet ds = new DataSet();
                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                user = ds.Tables[0].Rows[0]["U"].ToString();
                pass = ds.Tables[0].Rows[0]["P"].ToString();
                cbo_database.EditValue = ds.Tables[0].Rows[0]["D"].ToString();
                if (!string.IsNullOrEmpty(user))
                {
                    che_Reuser.Checked = true;
                    txt_user.EditValue = user;
                }
                else
                {
                    che_Reuser.Checked = false;
                }
                if (!string.IsNullOrEmpty(pass))
                {
                    che_Repass.Checked = true;
                    txt_pass.EditValue = pass;
                }
                else
                {
                    che_Repass.Checked = false;
                }
            }
            catch
            {

            }
        }
        private void SaveDatabase()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            ds.Tables[0].Rows[0]["D"] = cbo_database.EditValue;
            ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            Commons.IConnections.Database = cbo_database.Text.Trim();
            //insert vao user
            Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 1);
        }
        private void btn_Register_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_login_Click(null, null);
            }
        }
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            //if (!clsMain.CheckServer())
            //{
            //    Application.Exit();
            //}
        }

        private void pic_database_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "USE "+cbo_database.Text+" DELETE dbo.LOGIN WHERE USER_LOGIN = '" + txt_user.Text + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                MessageBox.Show("Xoa User Thanh Cong");
            }
            catch
            {
                MessageBox.Show("Khong Xoa Duoc");
            }
            
        }
    }
}