using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Commons;

namespace Vs.Report
{
    public partial class rptDSGiaDinh: DevExpress.XtraReports.UI.XtraReport
    {
        private object editValue;

        public rptDSGiaDinh(DateTime ngayin)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            xrSubreport1.ReportSource = new SubReportHeader();

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

            lblNgay.Text = " Ngày " + Ngay.Substring(Ngay.Length-2,2) + " Tháng " + Thang.Substring(Thang.Length - 2, 2) + " Năm " + Nam.Substring(Nam.Length - 4, 4);
            MergeByTag();
        }
        public void MergeByTag()
        {
            ExpressionBinding expressionBinding = new ExpressionBinding("BeforePrint", "Tag", "ToStr([MS_CN])");
            this.xrTableCell2.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell2.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell2.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell3.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell3.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell3.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell4.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell4.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell4.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;

            this.xrTableCell5.ExpressionBindings.Add(expressionBinding);
            this.xrTableCell5.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
            this.xrTableCell5.ProcessDuplicatesTarget = DevExpress.XtraReports.UI.ProcessDuplicatesTarget.Tag;
        }
        public rptDSGiaDinh(object editValue)
        {
            this.editValue = editValue;
        }
    }
}
