using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace Vs.Report
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

            //DevExpress.XtraReports.Configuration.DesignSettings.Default.UseOfficeInspiredRibbonStyle = false;
            //BonusSkins.Register();
            Commons.IConnections.Username = "sa";
            Commons.IConnections.Server = @"mashimaro";
            Commons.IConnections.Database = "VS_HRM_DEMO";
            Commons.IConnections.Password = "123";
            Application.Run(new XtraForm1());
            //Application.Run(new frmInXiNghiep());
            //Application.Run(new frmInTo());
            //Application.Run(new frmInGiaDinh());
           // Application.Run(new frmInLaoDongTo());
        }
    }
}
