namespace Vs.Report
{
    partial class rptChildQTCN1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tiThietBi = new DevExpress.XtraReports.UI.XRTableCell();
            this.tiSL = new DevExpress.XtraReports.UI.XRTableCell();
            this.tiDVT = new DevExpress.XtraReports.UI.XRTableCell();
            this.tiThanhTien = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 63.5F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTable8.Dpi = 254F;
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable8.SizeF = new System.Drawing.SizeF(1340.73F, 63.5F);
            this.xrTable8.StylePriority.UseBorders = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tiThietBi,
            this.tiSL,
            this.tiDVT,
            this.tiThanhTien});
            this.xrTableRow8.Dpi = 254F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // tiThietBi
            // 
            this.tiThietBi.Dpi = 254F;
            this.tiThietBi.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TEN_LOAI_MAY]")});
            this.tiThietBi.Multiline = true;
            this.tiThietBi.Name = "tiThietBi";
            this.tiThietBi.Text = "Tên loại máy";
            this.tiThietBi.Weight = 1.5794782263087472D;
            // 
            // tiSL
            // 
            this.tiSL.Dpi = 254F;
            this.tiSL.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SO_LUONG]")});
            this.tiSL.Multiline = true;
            this.tiSL.Name = "tiSL";
            this.tiSL.StylePriority.UseTextAlignment = false;
            this.tiSL.Text = "Số lượng";
            this.tiSL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tiSL.Weight = 1.0269693690022146D;
            // 
            // tiDVT
            // 
            this.tiDVT.Dpi = 254F;
            this.tiDVT.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DV_TINH]")});
            this.tiDVT.Multiline = true;
            this.tiDVT.Name = "tiDVT";
            this.tiDVT.StylePriority.UseTextAlignment = false;
            this.tiDVT.Text = "Đơn vị tính";
            this.tiDVT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tiDVT.Weight = 1.0808002141516979D;
            // 
            // tiThanhTien
            // 
            this.tiThanhTien.Dpi = 254F;
            this.tiThanhTien.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[THANH_TIEN]")});
            this.tiThanhTien.Multiline = true;
            this.tiThanhTien.Name = "tiThanhTien";
            this.tiThanhTien.StylePriority.UseTextAlignment = false;
            this.tiThanhTien.Text = "Thành tiền";
            this.tiThanhTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tiThanhTien.Weight = 1.5912181223471333D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2});
            this.GroupFooter1.Dpi = 254F;
            this.GroupFooter1.HeightF = 5.291667F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLine2
            // 
            this.xrLine2.Dpi = 254F;
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(1340.73F, 5.291667F);
            // 
            // rptChildQTCN1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.GroupFooter1});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(119, 135, 0, 0);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Tag = "rptChildQTCN1";
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTable8;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell tiThietBi;
        private DevExpress.XtraReports.UI.XRTableCell tiSL;
        private DevExpress.XtraReports.UI.XRTableCell tiDVT;
        private DevExpress.XtraReports.UI.XRTableCell tiThanhTien;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
    }
}
