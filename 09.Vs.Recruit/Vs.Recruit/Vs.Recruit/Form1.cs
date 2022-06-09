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
            //if (iID == 1)
            //{
            //    ucTHONG_BAO_TUYEN_DUNG uac = new ucTHONG_BAO_TUYEN_DUNG();
            //    this.Controls.Add(uac);
            //    uac.Dock = DockStyle.Fill;
            //}
            //else
            //{
            //    ucPhongVan ucpv = new ucPhongVan(-1);
            //    this.Controls.Add(ucpv);
            //    ucpv.Dock = DockStyle.Fill;
            //}

            ucQLUV uac = new ucQLUV();
            this.Controls.Add(uac);
            uac.Dock = DockStyle.Fill;


        }

        private void xtraTabPage1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
