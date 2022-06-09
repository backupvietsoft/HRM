using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptBCDanhGiaCN : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCDanhGiaCN()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

        }

    }
}
