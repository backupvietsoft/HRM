using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Drawing;

namespace Vs.HRM
{
    public partial class ucBaoCaoCongNhan : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_Temp = -1;
        private string SaveExcelFile;
        string sNameButton = "";
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
                case "them":
                    {
                        windowsUIButton.Buttons[1].Properties.Visible = false;
                        sNameButton = "them";
                        grdMauBC.Visible = false;
                        LoadGrdChonCot();
                        EnabledButton(false);
                        break;
                    }
                case "sua":
                    {
                        //grvMauBC.OptionsBehavior.Editable = true;
                        windowsUIButton.Buttons[1].Properties.Visible = false;
                        sNameButton = "sua";
                        EnabledButton(false);
                        break;
                    }
                case "khongluu":
                    {
                        sNameButton = "";
                        grdMauBC.Visible = true;
                        //grvMauBC.OptionsBehavior.Editable = false;
                        LoadMauBaoCaoCN();
                        grvMauBC_FocusedRowChanged(null, null);
                        EnabledButton(true);
                        break;
                    }
                case "Print":
                    {
                        DataTable dt = new DataTable();
                        dt = (DataTable)grdChonCot.DataSource;
                        if (dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").Count() == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaChonCotIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        DanhSachNhanVien();
                        //frmViewReport frm = new frmViewReport();

                        //int countColumns = 0;
                        //frm.rpt = new rptDSCongNhan();
                        //frm.rpt.Landscape = false;

                        //var tableH = new XRTable();
                        //var tableD = new XRTable();

                        //tableH.BeginInit();
                        //tableD.BeginInit();

                        //float totalWidth = 0f;

                        //tableH.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                        //tableH.Borders = BorderSide.All;
                        //tableD.LocationF = new DevExpress.Utils.PointFloat(0F, 0F);
                        //tableD.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;

                        //var tableHRow = new XRTableRow();
                        //var tableDRow = new XRTableRow();

                        //string dsCol = "";

                        //DataTable dt = new DataTable();
                        //dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonCot);

                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    if (dr["CHON"].ToString() == "1")
                        //    {
                        //        if (dsCol == "")
                        //        {
                        //            dsCol = dsCol + (dr["TEN_FIELD"].ToString() == "TEN_TO" ? "TEN_TO AS BO_PHAN" : dr["TEN_FIELD"].ToString());
                        //        }
                        //        else
                        //        {
                        //            dsCol = dsCol + "," + (dr["TEN_FIELD"].ToString() == "TEN_TO" ? "TEN_TO AS BO_PHAN" : dr["TEN_FIELD"].ToString());
                        //        }

                        //        var cellH = new XRTableCell()
                        //        {
                        //            Text = dr["DIEN_GIAI"].ToString(),
                        //            TextAlignment = TextAlignment.MiddleCenter,
                        //            Font = new System.Drawing.Font(uFontName, uFontSize, System.Drawing.FontStyle.Bold)
                        //        };
                        //        tableHRow.Cells.Add(cellH);

                        //        var cellD = new XRTableCell()
                        //        {
                        //            Text = dr["TEN_FIELD"].ToString(),
                        //            Font = new System.Drawing.Font(uFontName, uFontSize),
                        //            Padding = new PaddingInfo(5, 5, 0, 0)
                        //        };

                        //        if (dr["CANH_LE"].ToString() == "1")
                        //        {
                        //            cellD.TextAlignment = TextAlignment.MiddleLeft;
                        //        }
                        //        else if (dr["CANH_LE"].ToString() == "2")
                        //        {
                        //            cellD.TextAlignment = TextAlignment.MiddleCenter;
                        //        }
                        //        else
                        //        {
                        //            cellD.TextAlignment = TextAlignment.MiddleRight;
                        //        };

                        //        if (dr["DINH_DANG"].ToString() == "Num")
                        //        {
                        //            cellD.TextFormatString = "{0:#,#}";
                        //        }
                        //        else if (dr["DINH_DANG"].ToString() == "Date")
                        //        {
                        //            cellD.TextFormatString = "{0:dd/MM/yyyy}";
                        //        }
                        //        else
                        //        {
                        //            cellD.TextFormatString = "{0}";
                        //        };

                        //        cellD.ExpressionBindings.Add(new ExpressionBinding("Text", $"[{ dr["TEN_FIELD"].ToString()}]"));
                        //        tableDRow.Cells.Add(cellD);

                        //        float width = (float)Convert.ToDouble(dr["CHIEU_RONG"].ToString());
                        //        cellD.WidthF = cellH.WidthF = width;
                        //        totalWidth += width;
                        //    }
                        //}

                        //tableH.Rows.Add(tableHRow);
                        //tableD.Rows.Add(tableDRow);

                        //tableD.WidthF = tableH.WidthF = totalWidth;
                        //tableH.HeightF = 35F;
                        //tableD.HeightF = 30F;

                        //tableH.EndInit();
                        //tableD.EndInit();


                        //DetailBand detailBand = frm.rpt.Bands.GetBandByType(typeof(DetailBand)) as DetailBand;
                        //PageHeaderBand pageHeaderBand = frm.rpt.Bands.GetBandByType(typeof(PageHeaderBand)) as PageHeaderBand;

                        //pageHeaderBand.Controls.Add(tableH);
                        //detailBand.Controls.Add(tableD);

                        //pageHeaderBand.HeightF = tableH.HeightF;
                        //detailBand.HeightF = tableD.HeightF;

                        //System.Data.SqlClient.SqlConnection conn;
                        //try
                        //{
                        //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        //    conn.Open();

                        //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSCongNhan", conn);

                        //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        //    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = lkDonVi.EditValue;
                        //    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = lkXiNghiep.EditValue;
                        //    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = lkTo.EditValue;
                        //    cmd.Parameters.Add("@TTHD", SqlDbType.Int).Value = lkTTHD.EditValue;
                        //    cmd.Parameters.Add("@TTHT", SqlDbType.Int).Value = lkTTHT.EditValue;
                        //    cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = (dTuNgay.EditValue == null) ? "01/01/1900" : dTuNgay.EditValue;
                        //    cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = (dDenNgay.EditValue == null) ? "01/01/2999" : dDenNgay.EditValue;
                        //    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdoChonBC.SelectedIndex;
                        //    cmd.Parameters.Add("@TNGAY_NS", SqlDbType.Date).Value = (dTuNgayNS.EditValue == null) ? "01/01/1900" : dTuNgayNS.EditValue;
                        //    cmd.Parameters.Add("@DNGAY_NS", SqlDbType.Date).Value = (dDenNgayNS.EditValue == null) ? "01/01/2999" : dDenNgayNS.EditValue;
                        //    cmd.Parameters.Add("@NS", SqlDbType.Bit).Value = chkNgaySinh.EditValue;
                        //    cmd.Parameters.Add("@Field", SqlDbType.NVarChar, 1000).Value = dsCol;
                        //    cmd.Parameters.Add("@ID_CV", SqlDbType.BigInt, 1000).Value = cboChucVu.EditValue;
                        //    cmd.Parameters.Add("@ID_LCV", SqlDbType.BigInt, 1000).Value = cboLoaiCongViec.EditValue;
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        //    DataSet ds = new DataSet();
                        //    adp.Fill(ds);
                        //    DataTable dt2 = new DataTable();
                        //    dt2 = ds.Tables[0].Copy();
                        //    dt2.TableName = "DA_TA";
                        //    frm.AddDataSource(dt2);
                        //}
                        //catch
                        //{ }

                        //if (countColumns > 7)
                        //{
                        //    frm.rpt.Landscape = true;
                        //}
                        //frm.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        grvMauBC.CloseEditor();
                        grvMauBC.UpdateCurrentRow();
                        string sResult = "";
                        Int64 iID = -1;
                        if (sNameButton == "them")
                        {

                            //Load worksheet
                            XtraInputBoxArgs args = new XtraInputBoxArgs();
                            // set required Input Box options
                            args.Caption = "Nhập tên mẫu";
                            args.Prompt = "Nhập tên mẫu";
                            args.DefaultButtonIndex = 0;

                            // initialize a DateEdit editor with custom settings
                            TextEdit editor = new TextEdit();
                            //editor.Properties.Items.AddRange(wSheet);
                            //editor.EditValue = wSheet[0].ToString();

                            args.Editor = editor;
                            // a default DateEdit value
                            args.DefaultResponse = "";
                            // display an Input Box with the custom editor
                            var result = XtraInputBox.Show(args);
                            if (result == null || result.ToString() == "") return;
                            sResult = result.ToString();
                            string strSQL = "SELECT TOP 1 * FROM MAU_BC_NHAN_SU WHERE TEN_MAU = N'" + sResult.ToString().Trim() + "'";
                            int i = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                            if (i >= 1)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTenMauDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            sResult = grvMauBC.GetFocusedRowCellValue("TEN_MAU").ToString();
                            iID = Convert.ToInt64(grvMauBC.GetFocusedRowCellValue("ID_TPL"));
                        }

                        DataTable dt = new DataTable();
                        dt = (DataTable)grdChonCot.DataSource;
                        if (dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").Count() == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaChonCotCanLuuDinhDang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        dt = dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").CopyToDataTable();
                        string sBT = "BTDinhDang" + Commons.Modules.iIDUser;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");
                        try
                        {
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDinhDangBaoCaoCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, sResult, sBT, iID);
                            if (sNameButton == "them") grdMauBC.Visible = true;
                            iID_Temp = iID;
                            LoadMauBaoCaoCN();
                            EnabledButton(true);
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                        }
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
            LoadMauBaoCaoCN();
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            dTuNgayNS.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgayNS.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            NgayIn.EditValue = DateTime.Today;
            dTuNgayNS.Enabled = false;
            dDenNgayNS.Enabled = false;
            chkGroup.Checked = true;
            Commons.Modules.sLoad = "";
            EnabledButton(true);
        }

        private void LoadGrdChonCot()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCotChon", Commons.Modules.TypeLanguage));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonCot, grvChonCot, dt, true, false, false, true, true, "");
                dt.Columns["CHON"].ReadOnly = false;
                grvChonCot.Columns["CHON"].Visible = false;
                grvChonCot.Columns["TEN_FIELD"].Visible = false;
                grvChonCot.Columns["CANH_LE"].Visible = false;
                grvChonCot.OptionsSelection.CheckBoxSelectorField = "CHON";

                grvChonCot.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvChonCot.Columns["DIEN_GIAI"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            }
            catch
            {

            }
            //Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        private void LoadMauBaoCaoCN()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM MAU_BC_NHAN_SU"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_TPL"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdMauBC, grvMauBC, dt, true, false, false, true, true, "");
                grvMauBC.Columns["ID_TPL"].Visible = false;
                grvMauBC.Columns["TEN_FIELD"].Visible = false;

                if (iID_Temp != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_Temp));
                    grvMauBC.FocusedRowHandle = grvMauBC.GetRowHandle(index);
                    grvMauBC.ClearSelection();
                    grvMauBC.SelectRow(index);
                }
            }
            catch
            {

            }
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

        private void DanhSachNhanVien()
        {

            int countColumns = 0;
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
            dt = dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").OrderBy(x => x.Field<Int32>("STT")).CopyToDataTable();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CHON"].ToString() == "1")
                {
                    if (dsCol == "")
                    {
                        dsCol = dsCol + (dr["TEN_FIELD"].ToString() == "TEN_TO" ? "TEN_TO AS BO_PHAN" : dr["TEN_FIELD"].ToString());
                    }
                    else
                    {
                        dsCol = dsCol + "," + (dr["TEN_FIELD"].ToString() == "TEN_TO" ? "TEN_TO AS BO_PHAN" : dr["TEN_FIELD"].ToString());
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
                cmd.Parameters.Add("@Loai_sort", SqlDbType.Bit).Value = chkGroup.Checked;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 9;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 2);


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH NHÂN VIÊN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "4"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                int col = 0;
                int row_dl = 4;
                for (col = 0; col < dtBCThang.Columns.Count - 1; col++)
                {
                    //oSheet.Cells[row_dl, col + 1] =  dtBCThang.Columns[col].ToString();
                    //oSheet.Cells[row_dl, col + 1] = tableHRow.Cells[col].Text;
                    oSheet.Cells[row_dl, col + 1] = dt.Rows[col]["DIEN_GIAI"];
                    oSheet.Cells[row_dl, col + 1].ColumnWidth = dt.Rows[col]["CHIEU_RONG"];
                }

                int rowCnt = 0;
                Microsoft.Office.Interop.Excel.Range formatRange;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                if (chkGroup.Checked == false)
                {
                    DataRow[] dr = dtBCThang.Select();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                        keepRowCnt = rowCnt;
                    }
                    keepRowCnt = rowCnt + 4;
                    oSheet.get_Range("A5", lastColumn + (keepRowCnt).ToString()).Value2 = rowData;

                    for (col = 1; col < dtBCThang.Columns.Count; col++)
                    {
                        formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "5" + "", CharacterIncrement(col - 1) + (rowCnt + 1).ToString());
                        if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Num")
                        {
                            formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                            try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }
                        }
                        else if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Date")
                        {
                            formatRange.NumberFormat = "dd/mm/yyyy";
                            formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                            try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }
                        }
                        else if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Num1")
                        {
                            formatRange.NumberFormat = "0";
                            try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                        }
                    }
                    //oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                }
                else
                {
                    int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                    int rowCONG = 0; // Row để insert dòng tổng
                                     //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                    string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                    string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                    int rowBD = 5;
                    string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                    string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    DataTable dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                    string sRowBD_XN_Temp = "";
                    for (int j = 0; j < TEN_TO.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count; col++)
                            {
                                rowData[rowCnt, col] = row[col].ToString();
                            }
                            rowCnt++;
                        }
                        if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                        {
                            dr_Cu = 0;
                            rowBD_XN = 0;
                            chanVongDau = "";
                        }
                        else
                        {
                            rowBD_XN = 1;
                        }
                        rowBD = rowBD + dr_Cu + rowBD_XN;
                        //rowCnt = rowCnt + 6 + dr_Cu;
                        rowCnt = rowBD + current_dr - 1;

                        // Tạo group tổ
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        row_groupXI_NGHIEP_Format.Merge();
                        oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                        //for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                        //{
                        //    oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                        //    oSheet.Cells[rowBD, col].Font.Bold = true;
                        //    oSheet.Cells[rowBD, col].Font.Size = 12;
                        //}

                        //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        //sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        for (col = 1; col < dtBCThang.Columns.Count; col++)
                        {
                            formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + "", CharacterIncrement(col - 1) + (rowCnt + 1).ToString());
                            if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Num")
                            {
                                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }
                            }
                            else if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Date")
                            {
                                formatRange.NumberFormat = "dd/mm/yyyy";
                                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }
                            }
                            else if (dt.Rows[col - 1]["DINH_DANG"].ToString() == "Num1")
                            {
                                formatRange.NumberFormat = "0";
                                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch  { }
                            }
                        }

                        //// Dữ liệu cột tổng tăng
                        //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        //{
                        //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                        //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                        //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        //}
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }
                }
                rowCnt = keepRowCnt;
                formatRange = oSheet.get_Range("A5", "" + lastColumn + "" + (rowCnt + 1).ToString() + "");
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt + 1).ToString()));

                Microsoft.Office.Interop.Excel.Range myRange = oSheet.get_Range("A4", lastColumn + (rowCnt - 1).ToString());
                myRange.AutoFilter("1", "<>", Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlOr, "", true);

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string SaveFiles(string MFilter)
        {
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = MFilter;
                f.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }
        private void BorderAround(Range range)
        {
            Borders borders = range.Borders;
            borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }

        private void grvMauBC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                LoadGrdChonCot();
                DataTable dt = new DataTable();
                DataColumn dtC;
                DataRow dtR;
                dtC = new DataColumn();
                dtC.DataType = typeof(string);
                dtC.ColumnName = "TEN_FIELD";
                dt.Columns.Add(dtC);

                dtC = new DataColumn();
                dtC.DataType = typeof(int);
                dtC.ColumnName = "CHIEU_RONG";
                dt.Columns.Add(dtC);

                dtC = new DataColumn();
                dtC.DataType = typeof(string);
                dtC.ColumnName = "DINH_DANG";
                dt.Columns.Add(dtC);

                dtC = new DataColumn();
                dtC.DataType = typeof(int);
                dtC.ColumnName = "STT";
                dt.Columns.Add(dtC);

                string sDinhDang = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TEN_FIELD FROM MAU_BC_NHAN_SU WHERE ID_TPL = " + Convert.ToInt32(grvMauBC.GetFocusedRowCellValue("ID_TPL")) + "").ToString();
                string[] strDS = sDinhDang.Split('+');
                for (int i = 0; i < strDS.Count(); i++)
                {
                    string[] strCT = strDS[i].Split(';');
                    dtR = dt.NewRow();
                    dtR["TEN_FIELD"] = strCT[0];
                    dtR["CHIEU_RONG"] = strCT[1];
                    dtR["DINH_DANG"] = strCT[2];
                    dtR["STT"] = strCT[3];
                    dt.Rows.Add(dtR);
                }
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)grdChonCot.DataSource;
                //dt = dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").CopyToDataTable();
                string sBT_Focus = "BTFocus" + Commons.Modules.iIDUser; // Bảng tạm đã có trong dữ liệu
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_Focus, dt, "");
                string sBT = "BTChonCot" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");
                try
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateBangTempMauDSCN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBT_Focus, sBT));
                    grdChonCot.DataSource = dtTemp;
                    Commons.Modules.ObjSystems.XoaTable(sBT);
                    Commons.Modules.ObjSystems.XoaTable(sBT_Focus);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sBT);
                    Commons.Modules.ObjSystems.XoaTable(sBT_Focus);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void grvMauBC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.MAU_BC_NHAN_SU WHERE ID_TPL = " + Convert.ToInt64(grvMauBC.GetFocusedRowCellValue("ID_TPL")) + "");
                    grvMauBC.DeleteSelectedRows();
                }
                catch (Exception)
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                }
            }
        }

        private void EnabledButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            grvMauBC.OptionsBehavior.Editable = !visible;
        }

        private void grvMauBC_RowCountChanged(object sender, EventArgs e)
        {
            if (grvMauBC.RowCount == 0)
            {
                windowsUIButton.Buttons[1].Properties.Visible = false;
            }
            else
            {
                windowsUIButton.Buttons[1].Properties.Visible = true;
            }
        }
    }
}
