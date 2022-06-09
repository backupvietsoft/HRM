using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Vs.Report
{
    public partial class srptQuanHeGiaDinh : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable idt = new DataTable();
        public srptQuanHeGiaDinh(DataTable dt)
        {
            InitializeComponent();
            idt = dt;
            this.DataSource = idt;
            this.Tag = "srptQuanHeGiaDinh";
            this.Name = "srptQuanHeGiaDinh";
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));



        }

    }
}
