using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HRMServerTool
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string process = "HRMServerTool";
            if (System.Diagnostics.Process.GetProcessesByName(process).Length == 0)
            {
                Application.Exit();
            }
            else
            {
                Application.Run(new frmMain());
            }
        }
    }
}
