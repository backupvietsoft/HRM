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
        public static string mUserName = "h.tc";
        public static string miIDUser = "45";
        public static string mDatabase = "VS_HRM";

        [STAThread]
        static void Main(string[] args)
        {
            clsMain.setConfig();
            clsMain.setTTC();
            clsMain.CheckUpdate();
            Application.EnableVisualStyles();
            Thread t;
            if (args.Length > 0)
            {
                Commons.Modules.UserName = mUserName;
                Commons.Modules.iIDUser = Convert.ToUInt32(miIDUser);
                Commons.IConnections.Database = mDatabase;
                //insert vao user
                //MessageBox.Show(Commons.Modules.UserName + " : " + Commons.Modules.iIDUser.ToString() + " : " + Commons.IConnections.Database + "\n" + Commons.IConnections.CNStr);
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

                Application.Run(new frmLogin());
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
