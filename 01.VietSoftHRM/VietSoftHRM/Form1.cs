using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VietSoftHRM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            string sSQL = "SELECT TOP 20 ID_CN, ID_CV, HO + TEN HO_TEN, NGAY_SINH, NAM_SINH, DIA_CHI_THUONG_TRU, DT_DI_DONG FROM dbo.CONG_NHAN";
            dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
            dt1.TableName = "CONG_NHAN";
            DataTable dt2 = new DataTable();
            dt2.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.CHUC_VU"));
            dt2.TableName = "CHUC_VU";

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            ds.Relations.Add("ChucVu", dt2.Columns["ID_CV"], dt1.Columns["ID_CV"]);
            gridControl1.DataSource = ds;
            gridControl1.DataMember = "CHUC_VU";
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(gridView1,this.Name);
        }

        private void gridView1_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            //GridControl grid = sender as GridControl;
            //if(grid != null)
            //{
            //    GridView detailView = grid.FocusedView as GridView;
            //    if (detailView != null)
            //    {
            //        int masterRowHandle = e.RowHandle;
            //        int childRowCount = detailView.GetChildRowCount(masterRowHandle);
            //        for (int i = 0; i < childRowCount; i++)
            //        {
            //            object row = detailView.GetRow(detailView.GetChildRowHandle(masterRowHandle, i));
            //            // Do something with the row data here
            //        }
            //    }
            //}
        }
    }
}
