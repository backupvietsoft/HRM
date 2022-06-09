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
    public partial class frmPCDChonCD : DevExpress.XtraEditors.XtraForm
    {
        string sCnstr = "Server=.;database=MyTho_04022020;uid=sa;pwd=123;Connect Timeout=0;";
        public DataTable dtPCD = new DataTable();
        public frmPCDChonCD()
        {
            InitializeComponent();
        }

        private void frmPCDChonCD_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dtPCD, true, false, false, true, true, this.Name);
            grvCD.Columns[0].Width = 70;
            grvCD.Columns[1].Width = 100;
            grvCD.Columns[2].Width = 100;            
            for (int i = 4; i <= grvCD.Columns.Count - 1; i++)
            {
                grvCD.Columns[i].Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            grvCD.UpdateCurrentRow();
            this.DialogResult = DialogResult.OK;
        }

        private void grvCD_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }
    }
}