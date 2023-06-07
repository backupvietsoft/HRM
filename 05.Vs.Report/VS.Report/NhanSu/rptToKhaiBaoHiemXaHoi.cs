using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class rptToKhaiBaoHiemXaHoi : DevExpress.XtraReports.UI.XtraReport
    {
        private int type = 2;
        private long iCN = -1;
        public DataSet dsReport = new DataSet();
        private DateTime dNgayIn;
        public rptToKhaiBaoHiemXaHoi(int itype, long idCN, DateTime ngayin)
        {
            InitializeComponent();
            type = itype;
            iCN = idCN;
            dNgayIn = ngayin;

            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lbNgayIn.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay.Substring(Ngay.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang.Substring(Thang.Length - 2, 2) + " " +
                Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam.Substring(Nam.Length - 4, 4);
        }

        private void rptToKhaiBaoHiemXaHoi_BeforePrint(object sender, CancelEventArgs e)
        {
            if (type != 1)
            {
                this.xrSubreport1.ReportSource = null;
            }
            else
            {

                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand("rptToKhaiCapSoBHXH", conn);
                cmd2.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd2.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd2.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                cmd2.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iCN;
                cmd2.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd2);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[2].Copy();
                dt1.TableName = "DATA4";
                AddDataSource(dt1);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "DATA3";
                AddDataSource(dt);

                srptToKhaiBaoHiemXaHoi rpt = new srptToKhaiBaoHiemXaHoi(dNgayIn);

                rpt.DataSource = dsReport;
                rpt.CreateDocument();
                this.xrSubreport1.ReportSource = rpt;
                this.xrSubreport1.GenerateOwnPages = true;
            }
        }
        public void AddDataSource(DataTable tbSource)
        {
            try
            {
                try
                {
                    dsReport.Tables.Remove(tbSource.TableName);
                }
                catch { }
                dsReport.Tables.Add(tbSource.Copy());
            }
            catch { }
        }
    }
}
