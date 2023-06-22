using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Commons;
using DevExpress.Utils.Extensions;

namespace Vs.Report
{
    public partial class rptBCDangKyThaiSan_NB : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBCDangKyThaiSan_NB(string Ngay, string Thang, string Nam)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        

           switch (Commons.Modules.KyHieuDV)
            {
                case "DM":
                    {
                        xrSubreport1.ReportSource = new SubReportHeader();
                        break;
                    }
                case "NB":
                    {
                        xrSubreport1.ReportSource = new SubReportHeader();
                        // Xóa các control khỏi XRTableRow
                        xrTableRow3.Controls.Remove(lblNgayH7T);
                        xrTableRow3.Controls.Remove(lblNgayDuSinh);
                        xrTableRow1.Controls.Remove(txtNH7T);
                        xrTableRow1.Controls.Remove(txtNgayDuSinh);
                        break;
                    }
            }
                
                 
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            lblNgay.Text = Commons.Modules.ObjSystems.GetNN(dtNgu, "Ngay", "NgayThangNam") + " " + Ngay + " " +
            Commons.Modules.ObjSystems.GetNN(dtNgu, "Thang", "NgayThangNam") + " " + Thang + " " +
            Commons.Modules.ObjSystems.GetNN(dtNgu, "Nam", "NgayThangNam") + " " + Nam;
            lblThang.Text = "THÁNG " + Thang + " NĂM " + Nam;
        }

    }
}
