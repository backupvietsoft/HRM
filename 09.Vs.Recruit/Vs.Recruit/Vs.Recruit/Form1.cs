using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.HRM;
using Vs.Recruit.UAC;

namespace Vs.Recruit
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            ucYeuCauTuyenDung uac = new ucYeuCauTuyenDung();
            this.Controls.Add(uac);
            uac.Dock = DockStyle.Fill;
        }
    }
}
