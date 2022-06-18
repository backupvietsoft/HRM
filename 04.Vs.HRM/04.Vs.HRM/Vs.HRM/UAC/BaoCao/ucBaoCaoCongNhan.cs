using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoCongNhan : DevExpress.XtraEditors.XtraUserControl
    {
        public string uFontName = "Times New Roman";
        public float uFontSize = 11.25F;
        public ucBaoCaoCongNhan()
        {

            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
           
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        frmViewReport frm = new frmViewReport();

                        int countColumns = 0;
                        frm.rpt = new rptDSCongNhan();
                        frm.rpt.Landscape = false;

                        var tableH = new XRTable();
                        var tableD = new XRTable();

                        tableH.BeginInit();
                        tableD.BeginInit();

                        float totalWidth = 0f;

                        tableH.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                        tableH.Borders = BorderSide.All;
                        tableD.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                        tableD.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;

                        var tableHRow = new XRTableRow();
                        var tableDRow = new XRTableRow();

                        string dsCol = "";

                        DataTable dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonCot);

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["CHON"].ToString() == "1")
                            {
                                if (dsCol == "")
                                {
                                    dsCol = dsCol + dr["TEN_FIELD"].ToString();
                                }
                                else
                                {
                                    dsCol = dsCol + "," + dr["TEN_FIELD"].ToString();
                                }

                                var cellH = new XRTableCell()
                                {
                                    Text = dr["DIEN_GIAI"].ToString(),
                                    TextAlignment = TextAlignment.MiddleCenter,
                                    Font = new System.Drawing.Font(uFontName, uFontSize, System.Drawing.FontStyle.Bold)
                                };
                                tableHRow.Cells.Add(cellH);

                                var cellD = new XRTableCell()
                                {
                                    Text = dr["TEN_FIELD"].ToString(),
                                    Font = new System.Drawing.Font(uFontName, uFontSize),
                                    Padding = new PaddingInfo(5, 5, 0, 0)
                                };

                                if (dr["CANH_LE"].ToString() == "1")
                                {
                                    cellD.TextAlignment = TextAlignment.MiddleLeft;
                                }
                                else if (dr["CANH_LE"].ToString() == "2")
                                {
                                    cellD.TextAlignment = TextAlignment.MiddleCenter;
                                }
                                else
                                {
                                    cellD.TextAlignment = TextAlignment.MiddleRight;
                                };

                                if (dr["DINH_DANG"].ToString() == "Num")
                                {
                                    cellD.TextFormatString = "{0:#,#}";
                                }
                                else if (dr["DINH_DANG"].ToString() == "Date")
                                {
                                    cellD.TextFormatString = "{0:dd/MM/yyyy}";
                                }
                                else
                                {
                                    cellD.TextFormatString = "{0}";
                                };

                                cellD.ExpressionBindings.Add(new ExpressionBinding("Text", $"[{ dr["TEN_FIELD"].ToString()}]"));
                                tableDRow.Cells.Add(cellD);

                                float width = (float)Convert.ToDouble(dr["CHIEU_RONG"].ToString());
                                cellD.WidthF = cellH.WidthF = width;
                                totalWidth += width;
                            }
                        }

                        tableH.Rows.Add(tableHRow);
                        tableD.Rows.Add(tableDRow);

                        tableD.WidthF = tableH.WidthF = totalWidth;
                        tableH.HeightF = 35F;
                        tableD.HeightF = 30F;

                        tableH.EndInit();
                        tableD.EndInit();


                        DetailBand detailBand = frm.rpt.Bands.GetBandByType(typeof(DetailBand)) as DetailBand;
                        PageHeaderBand pageHeaderBand = frm.rpt.Bands.GetBandByType(typeof(PageHeaderBand)) as PageHeaderBand;

                        pageHeaderBand.Controls.Add(tableH);
                        detailBand.Controls.Add(tableD);

                        pageHeaderBand.HeightF = tableH.HeightF;
                        detailBand.HeightF = tableD.HeightF;

                        System.Data.SqlClient.SqlConnection conn;
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSCongNhan", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = lkDonVi.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = lkXiNghiep.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = lkTo.EditValue;
                            cmd.Parameters.Add("@TTHD", SqlDbType.Int).Value = lkTTHD.EditValue;
                            cmd.Parameters.Add("@TTHT", SqlDbType.Int).Value = lkTTHT.EditValue;
                            cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = (dTuNgay.EditValue == null) ? "01/01/1900" : dTuNgay.EditValue;
                            cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = (dDenNgay.EditValue == null) ? "01/01/2999" : dDenNgay.EditValue;
                            cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdoChonBC.SelectedIndex;
                            cmd.Parameters.Add("@TNGAY_NS", SqlDbType.Date).Value = (dTuNgayNS.EditValue == null) ? "01/01/1900" : dTuNgayNS.EditValue;
                            cmd.Parameters.Add("@DNGAY_NS", SqlDbType.Date).Value = (dDenNgayNS.EditValue == null) ? "01/01/2999" : dDenNgayNS.EditValue;
                            cmd.Parameters.Add("@NS", SqlDbType.Bit).Value = chkNgaySinh.EditValue;
                            cmd.Parameters.Add("@Field", SqlDbType.NVarChar, 1000).Value = dsCol;
                            cmd.Parameters.Add("@ID_CV", SqlDbType.BigInt, 1000).Value = cboChucVu.EditValue;
                            cmd.Parameters.Add("@ID_LCV", SqlDbType.BigInt, 1000).Value = cboLoaiCongViec.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt2 = new DataTable();
                            dt2 = ds.Tables[0].Copy();
                            dt2.TableName = "DA_TA";
                            frm.AddDataSource(dt2);
                        }
                        catch
                        { }

                        if (countColumns > 7)
                        {
                            frm.rpt.Landscape = true;
                        }
                        frm.ShowDialog();
                        break;
                    }
                default:
                    break;
            }
        }


        private void ucBaoCaoCongNhan_Load(object sender, EventArgs e)
        {
           
            rdoChonBC.SelectedIndex = 0;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(lkDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(lkDonVi, lkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
            Commons.Modules.ObjSystems.LoadCboTTHD(lkTTHD);
            Commons.Modules.ObjSystems.LoadCboTTHT(lkTTHT);

            // Chuc vu
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChucVu", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChucVu, dt, "ID_CV", "TEN_CV", "TEN_CV");

            // Loai cong viec
            DataTable dt1 = new DataTable();
            dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLoaiCongViec
                , dt1, "ID_LCV", "TEN_LCV", "TEN_LCV");

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(dTuNgayNS);
            Commons.OSystems.SetDateEditFormat(dDenNgayNS);
            Commons.OSystems.SetDateEditFormat(NgayIn);

            LoadGrdChonCot();
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            dTuNgayNS.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgayNS.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            NgayIn.EditValue = DateTime.Today;
            dTuNgayNS.Enabled = false;
            dDenNgayNS.Enabled = false;
            Commons.Modules.sLoad = "";

        }

        private void LoadGrdChonCot()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCotChon", Commons.Modules.TypeLanguage));
                
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonCot, grvChonCot, dt, false, false, false, true,true, "");
                dt.Columns["CHON"].ReadOnly = false;
                grvChonCot.Columns["CHON"].Visible = false;
                grvChonCot.Columns["TEN_FIELD"].Visible = false;
                grvChonCot.OptionsSelection.CheckBoxSelectorField = "CHON";

                grvChonCot.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvChonCot.Columns["DIEN_GIAI"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch
            {

            }
            //Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void lkDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(lkDonVi, lkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
        }

        private void lkXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(lkDonVi, lkXiNghiep, lkTo);
        }

        private void chkNgaySinh_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            switch (edit.Checked)
            {
                case true:
                    dTuNgayNS.Enabled = true;
                    dDenNgayNS.Enabled = true;
                    break;
                case false:
                    dTuNgayNS.Enabled = false;
                    dDenNgayNS.Enabled = false;
                    break;
            }
        }

       
    }
}
