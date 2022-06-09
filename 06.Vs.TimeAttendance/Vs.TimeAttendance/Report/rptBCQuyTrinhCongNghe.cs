using System;
using System.Data;

namespace Vs.Report
{
    public partial class rptBCQuyTrinhCongNghe : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtChild;
        DataTable QTCNLoaiMay;
        public rptBCQuyTrinhCongNghe(DateTime ngayin, DataTable _dtChild, DataTable _qTCNLoaiMay)
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " +
                Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM " +
                "= N'NgayThangNam' "));
            dtChild = _dtChild;
            QTCNLoaiMay = _qTCNLoaiMay;
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rptChildQTCN1 rptChildQTCN1 = new rptChildQTCN1();

            rptChildQTCN1.DataSource = QTCNLoaiMay;

            xrSubreport2.ReportSource = rptChildQTCN1;
            xrSubreport2.HeightF = dtChild.Rows.Count * xrSubreport2.HeightF + 10;

            //lbTGLamViecNgay.LocationF = new DevExpress.Utils.PointFloat(146.24F, 0F);
            //lbThoiGianMayMotSanPham.LocationF = new DevExpress.Utils.PointFloat(146.24F, 58.42F);
            //lbNangSuatLaoDongBinhQuan.LocationF = new DevExpress.Utils.PointFloat(146.24F, 116.84F);
            //lbSoLaoDonTrongTo.LocationF = new DevExpress.Utils.PointFloat(146.24F, 175.26F);
            //lbNangSuatLaoDongTo.LocationF = new DevExpress.Utils.PointFloat(146.24F, 233.68F);
            //lbCuongDoLaoDong.LocationF = new DevExpress.Utils.PointFloat(146.24F, 292.1F);
            //lbTongThanhTienMay.LocationF = new DevExpress.Utils.PointFloat(146.24F, 350.52F);
            //lbTongCongDoanDongGoi.LocationF = new DevExpress.Utils.PointFloat(146.24F, 408.94F);
            //lbTongThanhTien.LocationF = new DevExpress.Utils.PointFloat(146.24F, 467.36F);
            //lbCanBoQuanLy.LocationF = new DevExpress.Utils.PointFloat(146.24F, 525.78F);
            //lbKhoiVanPhong.LocationF = new DevExpress.Utils.PointFloat(146.24F, 584.2F);
            //lbTongCong.LocationF = new DevExpress.Utils.PointFloat(146.24F, 642.62F);
            //ReportFooter.HeightF = dtChild.Rows.Count * xrSubreport2.HeightF;
        }

        //Boolean ready = false;
        private void GroupFooter4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (ready == true) return;

            //XRTable tb = new XRTable();

            //GroupFooter4.Controls.Add(tb);

            //foreach (DataRow dr in dtChild.Rows)
            //{
            //    //tb.BeginInit();
            //    GroupFooter4.HeightF = dtChild.Rows.Count * xrTable8.HeightF;
            //    xrTable8.InsertRowBelow(xrTable8.Rows[xrTable8.Rows.Count - 1]);

            //XRTableRow row = new XRTableRow();
            //row.HeightF = 63.5F;
            ////FormatRow(row);
            //tb.Rows.Add(row);

            //XRTableCell TEN_CUM = new XRTableCell();
            //FormatCell(TEN_CUM);
            //XRTableCell THOI_GIAN_THIET_KE = new XRTableCell();
            //FormatCell(THOI_GIAN_THIET_KE);
            //XRTableCell productName = new XRTableCell();
            //XRTableCell productPrice = new XRTableCell();
            //XRTableCell productName = new XRTableCell();
            //XRTableCell productPrice = new XRTableCell();
            //row.Cells.Add(TEN_CUM);
            //row.Cells.Add(THOI_GIAN_THIET_KE);


            // Bind the table cells to the data fields.
            //TEN_CUM.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", dr.Field<string>("TEN_CUM")));
            //THOI_GIAN_THIET_KE.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text",
            //dr.Field<double>("SUM_THOI_GIAN_THIET_KE").ToString()));
                //productName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[ProductName]"));
                //productPrice.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[UnitPrice]"));
                //productName.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[ProductName]"));
                //productPrice.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[UnitPrice]"));

                //xrTable8.Rows[0].Cells["xrTableCell24"].ExpressionBindings.Equals(1);
                //xrTable8.Rows[1].Cells["TEN_CUM"].ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", dr.Field<string>("TEN_CUM")));
                //XRTableCell cell = new XRTableCell();

                //xrTable8.Rows[1].Cells[""].Container ="frde"

            //}
            //tb.EndInit();
            //FormatTable(tb);
            //ready = true;
        }

        private void GroupFooter5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rptChildQTCN rptChildQTCN1 = new rptChildQTCN();
            rptChildQTCN1.DataSource = dtChild;
            xrSubreport1.ReportSource = rptChildQTCN1;
        }
    }
}
