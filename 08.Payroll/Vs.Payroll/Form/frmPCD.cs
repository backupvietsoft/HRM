using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Vs.Payroll
{
    public partial class frmPCD : DevExpress.XtraEditors.XtraForm
    {
        public frmPCD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void frmPCD_Load(object sender, EventArgs e)
        {

        }
    }
}