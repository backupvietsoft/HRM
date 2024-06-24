﻿using System;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Commons;

namespace VietSoftHRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                clsMain.setConfig();
                Thread t;

                if (args.Length > 0)
                {

                    //System.Diagnostics.Process.Start("VietSoftHRM.exe", Com.Mod.Server + " " + Com.Mod.UserDB + " " + Com.Mod.Password + " " + Com.Mod.Database + " " + Com.Mod.UserID.ToString() + " " + Com.Mod.UName.ToString
                    Commons.IConnections.Server = args[0].ToString();
                    Commons.IConnections.Database = args[1].ToString();
                    Commons.IConnections.Username = args[2].ToString();
                    Commons.IConnections.Password = args[3].ToString();
                    Commons.Modules.iIDUser = Convert.ToInt32(args[4]);
                    Commons.Modules.UserName = args[5].ToString();
                    //System.Diagnostics.Process.Start("VietSoftHRM.exe", Com.Mod.Server + " " + Com.Mod.UserDB + " " + Com.Mod.Password + " " + Com.Mod.Database + " " + Com.Mod.UserID.ToString() + " " + Com.Mod.UName.ToString());
                    //insert vao user
                    //MessageBox.Show(Commons.Modules.UserName + " : " + Commons.Modules.iIDUser.ToString() + " : " + Commons.IConnections.Database + "\n" + Commons.IConnections.CNStr);
                }
                //Application.Run(new Form1(args));
                //clsMain.setTTC();
                //clsMain.CheckUpdate();
                //Application.EnableVisualStyles();

                t = new Thread(new ThreadStart(MRunForm));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        static void MRunForm()
        {
            try
            {
                //MRunInt();
                //Application.Run(new Form1());
                //Application.Run(new frmMain());

                Application.Run(new frmLogin());
                //Application.Run(new frmNotification());
                //Application.Run(new XtraForm1());
                //Application.Run(new frmThongTinChung(1));
                //Application.Run(new frmImportHinhCN(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static void MRunInt()
        {
            try
            {
                string strSQL = "SELECT ISNULL(USER_KHACH,0) USER_KHACH FROM dbo.USERS WHERE [USER_NAME] = '" + Commons.Modules.UserName.Trim() + "'";
                try
                {
                    if (Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)) == true)
                    {
                        Commons.Modules.chamCongK = true;
                    }
                }
                catch { }

                Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
