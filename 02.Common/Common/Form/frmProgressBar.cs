using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commons
{
    public partial class frmProgressBar : DevExpress.XtraEditors.XtraForm
    {
        public int Maximum = 0;
        public int Value = 0;
        public frmProgressBar()
        {
            InitializeComponent();
        }

        private void frmProgressBar_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum= 0;
            progressBar1.Maximum = Maximum;
            progressBar1.Value = Value; 
        }
    }
}
