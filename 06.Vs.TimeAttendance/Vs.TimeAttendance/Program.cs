using System;
using System.Windows.Forms;
using DevExpress.UserSkins;
using System.Data;
using System.Threading;

namespace Vs.TimeAttendance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            Commons.Modules.ModuleName = "HRM";
            Commons.Modules.UserName = "admin";
            DataSet ds = new DataSet();
            //ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            Commons.IConnections.Username = "sa";
            Commons.IConnections.Server = "192.168.2.5";
            Commons.IConnections.Database = "VS_HRM_DEMO";
            Commons.IConnections.Password = "123";

            Commons.Modules.sPrivate = @"PILMICO";
            //Commons.Modules.sPrivate = @"ADC";

            Commons.Modules.iSoLeSL = 1;
            Commons.Modules.iSoLeDG = 2;
            Commons.Modules.iSoLeTT = 3;
            //Commons.Modules.sSoLeSL = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeSL);
            //Commons.Modules.sSoLeDG = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeDG);
            //Commons.Modules.sSoLeTT = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeTT);

            //Commons.Modules.sFontReport = "Monotype Corsiva";

            Commons.Modules.TypeLanguage = 0;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Commons.Modules.ObjSystems.KhoMoi = false;
            //Commons.Modules.PermisString = "Read only";
            Commons.Modules.iLink = 1;
            Thread t = new Thread(new ThreadStart(MRunForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        static void MRunForm()
        {
            try
            {
          //      Application.Run(new frmLinkDuLieuChamCong());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
