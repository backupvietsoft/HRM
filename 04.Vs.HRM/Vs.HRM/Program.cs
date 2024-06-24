using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Data;
using System.Threading;

namespace Vs.HRM
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
            Commons.Modules.ModuleName = "VS_HRM";
            Commons.Modules.UserName = "admin";
            DataSet ds = new DataSet();
            //ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            Commons.IConnections.Username = "sa";
            Commons.IConnections.Server = @"27.74.240.29,1433";
            Commons.IConnections.Database = "VS_HRM_DM";
            Commons.IConnections.Password = "codaikadaiku";
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
            Thread t = new Thread(new ThreadStart(MRunForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        static void MRunForm()
        {
            try
            {
                Application.Run(new frmCNQuaTrinhCongTac());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
