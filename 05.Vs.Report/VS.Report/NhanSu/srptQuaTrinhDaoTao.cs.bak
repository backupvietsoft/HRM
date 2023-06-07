using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class srptQuaTrinhDaoTao : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable idt = new DataTable();
        public srptQuaTrinhDaoTao(DataTable dt)
        {
            InitializeComponent();
            idt = dt;
            this.DataSource = idt;
            this.Tag = "srptQuaTrinhDaoTao";
            this.Name = "srptQuaTrinhDaoTao";
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

        }

        private void srptQuaTrinhDaoTao_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }
    }
}
