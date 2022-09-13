using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Recruit
{
    public partial class rptBCBangCapUV : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCBangCapUV()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

        }

    }
}
