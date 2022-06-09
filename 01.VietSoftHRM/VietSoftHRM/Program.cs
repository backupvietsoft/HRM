using System;
using System.Windows.Forms;
using System.Threading;
using System.Data;

namespace VietSoftHRM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            clsMain.setConfig();
            clsMain.setTTC();
            clsMain.CheckUpdate();
            Application.EnableVisualStyles();
            Thread t = new Thread(new ThreadStart(MRunForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        static void MRunForm()
        {
            try
            {
                Application.Run(new frmLogin());
                //Application.Run(new XtraForm1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

    }
}
