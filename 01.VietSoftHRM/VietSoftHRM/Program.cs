using System;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

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
            clsMain.setTTC();
            clsMain.CheckUpdate();
            Application.EnableVisualStyles();
            
            if (args.Length > 0)
            {

                Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 1);
                t = new Thread(new ThreadStart(MRunInt));
            }
            else
            {
                t = new Thread(new ThreadStart(MRunForm));
            }
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        static void MRunForm()
        {
            try
            {
                //MRunInt();
                Application.Run(new frmLogin());

                //Application.Run(new frmDLTuyenDung());
                //Vs.Report.frmViewReport frm = new Vs.Report.frmViewReport();
                //frm.rpt = new VS.Report.NhanSu.XtraReport1();
                //frm.ShowDialog();
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
