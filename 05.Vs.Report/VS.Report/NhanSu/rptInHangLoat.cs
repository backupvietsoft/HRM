using DevExpress.DataAccess.DataFederation;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static Vs.Report.rptInHangLoat;

namespace Vs.Report
{
    public partial class rptInHangLoat : DevExpress.XtraReports.UI.XtraReport
    {
        private int iCount = 0;
        private DataTable dt_HH;
        private string SQL;
        private string dataTable;
        private string tenLHDLD;
        private string reportTypeName;
        private Int64 intInHl;

        private DataTable sbNV;
        public rptInHangLoat(string connectSQL,string DataTable, string ReportTypeName, string TenLHDLD, DataTable SbNV, Int64 idIHL  )
        {
            InitializeComponent();
            BottomMargin.Visible = false;
            reportTypeName = ReportTypeName;
            SQL = connectSQL;
            dataTable = DataTable;
            tenLHDLD = TenLHDLD;
            sbNV = SbNV;
            intInHl = idIHL;
            string sSql = "SELECT MARGIN_RIGHT, MARGIN_TOP,MARGIN_LEFT,MARGIN_BOTTOM FROM dbo.IN_HANG_LOAT WHERE ID_IHL = " + idIHL.ToString();
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            Margins.Top = Convert.ToInt32(dtTmp.Rows[0]["MARGIN_TOP"]);
            Margins.Left = Convert.ToInt32(dtTmp.Rows[0]["MARGIN_LEFT"]);
            Margins.Bottom = Convert.ToInt32(dtTmp.Rows[0]["MARGIN_BOTTOM"]);
            Margins.Right = Convert.ToInt32(dtTmp.Rows[0]["MARGIN_RIGHT"]);

        }

        private void LoadNN()
        {
            
        }
        public class ReportData 
        {
            public DataTable[] Tables { get; set; }
            public string[] TableNames { get; set; }
        }
        private void Detail_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dtbc = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, dataTable, sbNV, "");
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSaveThongTinNhanVienNB", conn);
                cmd = new System.Data.SqlClient.SqlCommand(SQL, conn);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = dataTable;
                cmd.Parameters.Add("@sKyHieuDV", SqlDbType.NVarChar, 50).Value = Commons.Modules.KyHieuDV;
                cmd.Parameters.Add("@LoaiIn", SqlDbType.NVarChar, 50).Value = tenLHDLD;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.NVarChar, 50).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                int SL_Table = ds.Tables.Count;
                Type reportType = Type.GetType("Vs.Report." + reportTypeName + ",Vs.Report", true, true);
                XtraReport report = (XtraReport)Activator.CreateInstance(reportType, DateTime.Now);
                report.DataSource = ds;
                for (int i = 0; i < Convert.ToInt32(ds.Tables.Count - 1); i++)
                {
                    ds.Tables[i].TableName = (ds.Tables[ds.Tables.Count - 1]).Rows[i][0].ToString();
                }
                xrSubreport2.ReportSource = report;
                conn.Close();
            }
            catch { }
        }
    }
}
