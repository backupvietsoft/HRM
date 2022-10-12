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
using Microsoft.ApplicationBlocks.Data;
using Commons;
using DevExpress.XtraBars.Docking2010;
using Vs.Report;
using DevExpress.XtraLayout;

namespace Vs.TimeAttendance
{
    public partial class frmVachTheLoi : DevExpress.XtraEditors.XtraForm
    {
        public frmVachTheLoi()
        {
            InitializeComponent();
        }

        private void frmVachTheLoi_Load(object sender, EventArgs e)
        {
            ucVachTheLoi ns = new ucVachTheLoi();
            this.Controls.Clear();
            this.Controls.Add(ns);
            ns.Dock = DockStyle.Fill;
        }
    }
}