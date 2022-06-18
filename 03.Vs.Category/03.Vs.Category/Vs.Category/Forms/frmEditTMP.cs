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

namespace Vs.Category
{
    public partial class frmEditTMP : DevExpress.XtraEditors.XtraForm
    {
        Int64 iId = -1;
        Boolean bAddEdit = true;  // true la add false la edit
        public frmEditTMP(Int64 iId, Boolean bAddEdit)
        {
            InitializeComponent();
            iId = iId;
            bAddEdit = bAddEdit;
        }

        //private void frmEditTMP_Resize(object sender, EventArgs e) => dataLayoutControl1.Refresh();

    }
}