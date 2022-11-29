using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;


namespace CapNhapHRM
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

            Thread thread = new Thread(new ThreadStart(MRunForm));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(); 
            
        }

        static void MRunForm()
        {
            try
            {
                Application.Run(new frmLogin());
                //Application.Run(new frmTHien());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
