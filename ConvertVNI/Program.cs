using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertVNI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Commons.Modules.ModuleName = "VS_HRM";
            Commons.Modules.UserName = "admin";
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            Commons.IConnections.Username = ds.Tables[0].Rows[0]["U"].ToString();
            Commons.IConnections.Server = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["S"].ToString(), true);
            Commons.IConnections.Database = ds.Tables[0].Rows[0]["D"].ToString();
            Commons.IConnections.Password = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["P"].ToString(), true);
            Commons.Modules.sIP = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["IP"].ToString(), true);
            //Commons.IConnections.Username = "sa";
            //Commons.IConnections.Server = @".";
            //Commons.IConnections.Database = "DATA_TEXGIANG";
            //Commons.IConnections.Password = "123";
            //Commons.Modules.TypeLanguage = 0;
            Application.EnableVisualStyles();
            //Thread t = new Thread(new ThreadStart(MRunForm));
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            Application.Run(new frmConvertVNI());
        }
        static void MRunForm()
        {
            try
            {
                Application.Run(new frmConvertVNI());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
