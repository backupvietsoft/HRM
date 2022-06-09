using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptBCPhieuTienPhep : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCPhieuTienPhep()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

        }

    }
}
