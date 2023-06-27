using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Vs.Payroll
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            ucTinhLuong uac = new ucTinhLuong();
            //ucBCLuongThang uac = new ucBCLuongThang();
            this.Controls.Add(uac);
            uac.Dock = DockStyle.Fill;
        }
    }
}
