using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace Vs.Recruit
{
    public partial class rptInTheNV_DM : DevExpress.XtraReports.UI.XtraReport
    {
        private int iCount = 0;
        private DataTable dtemp = new DataTable();


        public rptInTheNV_DM(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;
            dtemp = dt;


            
        }

        private void rptInTheNV_DM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRPictureBox pictureBox = (XRPictureBox)this.FindControl("PicHINH_CN", true);
            //pictureBox.ImageUrl = Commons.Modules.sDDTaiLieu + "\\" + "ImageEmployees\\" + dtemp.Rows[iCount]["MS_CN"].ToString().Trim() + ".jpg";
            //iCount++;

            //PicHINH_CN.DataBindings.Add("ImageUrl", bindingSource, "ImageURL");


            //Dictionary<int, string> imageUrls = new Dictionary<int, string>();
            //for (int i = 0; i < dtemp.Rows.Count; i++)
            //{
            //    imageUrls.Add(i + 1, Commons.Modules.sDDTaiLieu + "\\" + "ImageEmployees\\" + dtemp.Rows[i]["MS_CN"].ToString().Trim() + ".jpg");
            //}

            //BindingSource bindingSource = new BindingSource();

            //DataTable dataTable = new DataTable();
            //DataColumn newColumn = new DataColumn();
            //newColumn.DataType = typeof(int);
            //newColumn.ColumnName = "ID";
            //newColumn.Caption = "ID";
            //newColumn.ReadOnly = false;
            //dataTable.Columns.Add(newColumn);

            //newColumn = new DataColumn("ImageURL", typeof(string));
            //dataTable.Columns.Add(newColumn);
            //dataTable.TableName = "DATA3";
            //bindingSource.DataSource = dataTable;

            //bindingSource.ResetBindings(false);

            //foreach (KeyValuePair<int, string> kvp in imageUrls)
            //{
            //    DataRow row = ((DataTable)bindingSource.DataSource).NewRow();
            //    row["ID"] = kvp.Key;
            //    row["ImageURL"] = kvp.Value;
            //    ((DataTable)bindingSource.DataSource).Rows.Add(row);
            //}
            ////PicHINH_CN.DataBindings.Add("ImageUrl", bindingSource, "ImageURL");

            //PicHINH_CN.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "ImageUrl", "[ImageURL] = '" + GetCurrentColumnValue("ImageURL") + "'"));
        }
    }
}
