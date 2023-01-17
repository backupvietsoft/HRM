 using System;
using System.Drawing;
using System.Data;

namespace Commons
{
    public partial class SubReportHeader_DV : DevExpress.XtraReports.UI.XtraReport
    {
        public SubReportHeader_DV(int ID_DV)
        {
            InitializeComponent();
            DataTable dt = Commons.Modules.ObjSystems.DataReportHeader(ID_DV);
            this.DataSource = dt;
            try
            {
                picLogo.SizeF = new SizeF((float)Convert.ToDecimal(dt.Rows[0]["LG_WITH"]), (float)Convert.ToDecimal(dt.Rows[0]["LG_HEIGHT"]));
                //picLogo.LocationF = new PointF((float)Convert.ToDecimal(dt.Rows[0]["LG_LEFT"]), (float)Convert.ToDecimal(dt.Rows[0]["LG_TOP"]));
                lblCONG_TY.LocationF = new PointF(picLogo.SizeF.Width, (float)Convert.ToDecimal(dt.Rows[0]["LG_TOP"]));
            }
            catch (Exception ex)
            {
            }
        }
    }
}


