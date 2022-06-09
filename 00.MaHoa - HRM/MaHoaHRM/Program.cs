using System;
using System.Threading;
using System.Windows.Forms;

namespace MaHoaHRM
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
                Application.Run(new Forms.frmLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
