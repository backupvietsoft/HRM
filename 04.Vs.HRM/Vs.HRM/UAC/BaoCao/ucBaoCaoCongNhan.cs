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
                        if (chkInAll.Checked == true)
                        {
                            inAll();
                        }
                        else
                        {

                            DataTable dt = new DataTable();
                            dt = (DataTable)grdChonCot.DataSource;
                            if (dt.AsEnumerable().Where(x => x["CHON"].ToString() == "1").Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaChonCotIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            DanhSachNhanVien();
                        }
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
                                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
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

        #region InAll
        private void FormatTieuDeBaoCao(ref Range row, bool isMerge = true, bool isBold = false, int fontSizeNoiDung = 11, string fontName = "Times New Roman", string numberFormant = "@", Microsoft.Office.Interop.Excel.XlHAlign horizontalAlignment = XlHAlign.xlHAlignLeft, Microsoft.Office.Interop.Excel.XlVAlign verticalAlignment = XlVAlign.xlVAlignCenter, string Value = "")
        {
            if (isMerge)
            {
                row.Merge();
            }
            if (isBold)
            {
                row.Font.Bold = true;
            }
            row.Font.Size = fontSizeNoiDung;
            row.Font.Name = fontName;
            row.NumberFormat = numberFormant;
            row.Cells.HorizontalAlignment = horizontalAlignment;
            row.Cells.VerticalAlignment = verticalAlignment;
            row.Value2 = Value;
        }
        private void FormatTitleTable(ref Range range, string fontName = "Times New Roman", int fontSizeNoiDung = 11, int ColumnWidth = 10, int rowHeight = 30, Color BackgroundColor = default(Color), bool isMerge = false, string Value = "")
        {
            if (isMerge)
            {
                range.Merge();
            }
            range.Value2 = Value;
            range.ColumnWidth = ColumnWidth;
        }
        public void CreateHeaderTable(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 9)
        {
            int height_Single = 25;
            Range row5_Header_Table_STT = oSheet.get_Range("A5", "A6"); // A5-6
            FormatTitleTable(ref row5_Header_Table_STT, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Stt"); ;
            Range row7_Header_Table_A7 = oSheet.get_Range("A7"); // A7
            FormatTitleTable(ref row7_Header_Table_A7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "1");

            Range row5_Header_Table_Ma_The = oSheet.get_Range("B5", "B6"); // B5-6
            FormatTitleTable(ref row5_Header_Table_Ma_The, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Mã thẻ");
            Range row7_Header_Table_B7 = oSheet.get_Range("B7"); // B7
            FormatTitleTable(ref row7_Header_Table_B7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "2");

            Range row5_Header_Table_Bo_Phan = oSheet.get_Range("C5", "C6"); // C5-6
            FormatTitleTable(ref row5_Header_Table_Bo_Phan, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Bộ phận");
            Range row7_Header_Table_C7 = oSheet.get_Range("C7"); // C7
            FormatTitleTable(ref row7_Header_Table_C7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "3");


            Range row5_Header_Table_Ho_Ten = oSheet.get_Range("D5", "D6"); // D5
            FormatTitleTable(ref row5_Header_Table_Ho_Ten, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), true, "Họ và tên");
            Range row7_Header_Table_D7 = oSheet.get_Range("D7"); // D7
            FormatTitleTable(ref row7_Header_Table_D7, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), false, "4");


            Range row5_Header_Table_Ngay_Sinh = oSheet.get_Range("E5", "F5"); // E5
            FormatTitleTable(ref row5_Header_Table_Ngay_Sinh, fontName, fontSizeNoiDung, 12, height_Single, Color.FromArgb(255, 255, 255), true, "Ngày sinh");
            Range row6_Header_Table_Ngay_Sinh = oSheet.get_Range("E6"); // E6
            FormatTitleTable(ref row6_Header_Table_Ngay_Sinh, fontName, fontSizeNoiDung, 12, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày,tháng,năm");
            Range row7_Header_Table_E7 = oSheet.get_Range("E7"); // E7
            FormatTitleTable(ref row7_Header_Table_E7, fontName, fontSizeNoiDung, 12, height_Single, Color.FromArgb(255, 255, 255), false, "5");


            Range row6_Header_Table_F = oSheet.get_Range("F6"); // F6
            FormatTitleTable(ref row6_Header_Table_F, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "Tuổi");
            Range row7_Header_Table_F7 = oSheet.get_Range("F7"); // F7
            FormatTitleTable(ref row7_Header_Table_F7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "6");


            Range row5_Header_Table_Noi_Sinh = oSheet.get_Range("G5", "G6"); // G5
            FormatTitleTable(ref row5_Header_Table_Noi_Sinh, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Nơi sinh");
            Range row7_Header_Table_G7 = oSheet.get_Range("G7"); // G7
            FormatTitleTable(ref row7_Header_Table_G7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "7");


            Range row5_Header_Table_Nguyen_Quan = oSheet.get_Range("H5", "J5"); // H5-J5
            FormatTitleTable(ref row5_Header_Table_Nguyen_Quan, fontName, fontSizeNoiDung, 45, height_Single, Color.FromArgb(255, 255, 255), true, "Nguyên quán");
            Range row6_Header_Table_Xa_Phuong = oSheet.get_Range("H6"); // H6
            FormatTitleTable(ref row6_Header_Table_Xa_Phuong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Xã/Phường");
            Range row6_Header_Table_Quan_Huyen = oSheet.get_Range("I6"); // I6
            FormatTitleTable(ref row6_Header_Table_Quan_Huyen, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Quận/Huyện");
            Range row6_Header_Table_Tinh_Thanh = oSheet.get_Range("J6"); // J6
            FormatTitleTable(ref row6_Header_Table_Tinh_Thanh, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Tỉnh/Thành");
            Range row7_Header_Table_H7 = oSheet.get_Range("H7"); // H7
            FormatTitleTable(ref row7_Header_Table_H7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "8");
            Range row7_Header_Table_I7 = oSheet.get_Range("I7"); // I7
            FormatTitleTable(ref row7_Header_Table_I7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "9");
            Range row7_Header_Table_J7 = oSheet.get_Range("J7"); // J7
            FormatTitleTable(ref row7_Header_Table_J7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "10");


            Range row5_Header_Table_Dia_Chi_Thuong_Tru = oSheet.get_Range("K5", "N5"); // K5-N5
            FormatTitleTable(ref row5_Header_Table_Dia_Chi_Thuong_Tru, fontName, fontSizeNoiDung, 60, height_Single, Color.FromArgb(255, 255, 255), true, "Địa chỉ thường trú");
            Range row6_Header_Table_Thuong_Tru_Thon_Xom = oSheet.get_Range("K6"); // K6
            FormatTitleTable(ref row6_Header_Table_Thuong_Tru_Thon_Xom, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Thôn/Xóm");
            Range row6_Header_Table_Thuong_Tru_Xa_Phuong = oSheet.get_Range("L6"); // L6
            FormatTitleTable(ref row6_Header_Table_Thuong_Tru_Xa_Phuong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Xã/Phường");
            Range row6_Header_Table_Thuong_Tru_Quan_Huyen = oSheet.get_Range("M6"); // M6
            FormatTitleTable(ref row6_Header_Table_Thuong_Tru_Quan_Huyen, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Quận/Huyện");
            Range row6_Header_Table_Thuong_Tru_Tinh_Thanh = oSheet.get_Range("N6"); // N6
            FormatTitleTable(ref row6_Header_Table_Thuong_Tru_Tinh_Thanh, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Tỉnh/Thành");
            Range row7_Header_Table_K7 = oSheet.get_Range("K7"); // K7
            FormatTitleTable(ref row7_Header_Table_K7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "11");
            Range row7_Header_Table_L7 = oSheet.get_Range("L7"); // L7
            FormatTitleTable(ref row7_Header_Table_L7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "12");
            Range row7_Header_Table_M7 = oSheet.get_Range("M7"); // M7
            FormatTitleTable(ref row7_Header_Table_M7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "13");
            Range row7_Header_Table_N7 = oSheet.get_Range("N7"); // N7
            FormatTitleTable(ref row7_Header_Table_N7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "14");


            Range row5_Header_Table_CMND = oSheet.get_Range("O5", "Q5"); // O5-Q5
            FormatTitleTable(ref row5_Header_Table_CMND, fontName, fontSizeNoiDung, 45, height_Single, Color.FromArgb(255, 255, 255), true, "Chứng minh thư/Thẻ căn cước");
            Range row6_Header_Table_So_CMT = oSheet.get_Range("O6"); // O6
            FormatTitleTable(ref row6_Header_Table_So_CMT, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Số CMT");
            Range row6_Header_Table_Noi_Cap = oSheet.get_Range("P6"); // P6
            FormatTitleTable(ref row6_Header_Table_Noi_Cap, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Nơi cấp");
            Range row6_Header_Table_Ngay_Cap = oSheet.get_Range("Q6"); // Q6
            FormatTitleTable(ref row6_Header_Table_Ngay_Cap, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày cấp");
            Range row7_Header_Table_O7 = oSheet.get_Range("O7"); // O7
            FormatTitleTable(ref row7_Header_Table_O7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "15");
            Range row7_Header_Table_P7 = oSheet.get_Range("P7"); // P7
            FormatTitleTable(ref row7_Header_Table_P7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "16");
            Range row7_Header_Table_Q7 = oSheet.get_Range("Q7"); // Q7
            FormatTitleTable(ref row7_Header_Table_Q7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "17");


            Range row5_Header_Table_Hoc_Van = oSheet.get_Range("R5", "R6"); // R5
            FormatTitleTable(ref row5_Header_Table_Hoc_Van, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Trình độ học vấn");
            Range row7_Header_Table_R7 = oSheet.get_Range("R7"); // R7
            FormatTitleTable(ref row7_Header_Table_R7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "18");


            Range row5_Header_Table_Chuyen_Mon = oSheet.get_Range("S5", "S6"); // S5
            FormatTitleTable(ref row5_Header_Table_Chuyen_Mon, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Chuyên môn");
            Range row7_Header_Table_S7 = oSheet.get_Range("S7"); // S7
            FormatTitleTable(ref row7_Header_Table_S7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "19");


            Range row5_Header_Table_Ngay_Vao_Lam = oSheet.get_Range("T5", "T6"); // T5
            FormatTitleTable(ref row5_Header_Table_Ngay_Vao_Lam, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Ngày vào làm");
            Range row7_Header_Table_T7 = oSheet.get_Range("T7"); // T7
            FormatTitleTable(ref row7_Header_Table_T7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "20");


            Range row5_Header_Table_Ngay_KT_Thu_Viec = oSheet.get_Range("U5", "U6"); // U5
            FormatTitleTable(ref row5_Header_Table_Ngay_KT_Thu_Viec, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Ngày kết thúc thử việc");
            Range row7_Header_Table_U7 = oSheet.get_Range("U7"); // U7
            FormatTitleTable(ref row7_Header_Table_U7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "21");


            Range row5_Header_Table_Chuc_Vu = oSheet.get_Range("V5", "V6"); // V5
            FormatTitleTable(ref row5_Header_Table_Chuc_Vu, fontName, fontSizeNoiDung, 20, height_Single, Color.FromArgb(255, 255, 255), true, "Chức vụ");
            Range row7_Header_Table_V7 = oSheet.get_Range("V7"); // V7
            FormatTitleTable(ref row7_Header_Table_V7, fontName, fontSizeNoiDung, 20, height_Single, Color.FromArgb(255, 255, 255), false, "22");


            Range row5_Header_Table_Cong_Viec = oSheet.get_Range("W5", "W6"); // W5
            FormatTitleTable(ref row5_Header_Table_Cong_Viec, fontName, fontSizeNoiDung, 25, height_Single, Color.FromArgb(255, 255, 255), true, "Công việc");
            Range row7_Header_Table_W7 = oSheet.get_Range("W7"); // Q7
            FormatTitleTable(ref row7_Header_Table_W7, fontName, fontSizeNoiDung, 25, height_Single, Color.FromArgb(255, 255, 255), false, "23");


            Range row5_Header_Table_Gioi_Tinh = oSheet.get_Range("X5", "Y5"); // X5
            FormatTitleTable(ref row5_Header_Table_Gioi_Tinh, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Giới tính");
            Range row6_Header_Table_Nam = oSheet.get_Range("X6"); // X6
            FormatTitleTable(ref row6_Header_Table_Nam, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "Nam");
            Range row7_Header_Table_X7 = oSheet.get_Range("X7"); // X7
            FormatTitleTable(ref row7_Header_Table_X7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "24");


            Range row6_Header_Table_Nu = oSheet.get_Range("Y6"); // Y6
            FormatTitleTable(ref row6_Header_Table_Nu, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "Nữ");
            Range row7_Header_Table_Y7 = oSheet.get_Range("Y7"); // Y7
            FormatTitleTable(ref row7_Header_Table_Y7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "25");


            Range row5_Header_Table_Tinh_Trang_HD = oSheet.get_Range("Z5", "AA5"); // Z5
            FormatTitleTable(ref row5_Header_Table_Tinh_Trang_HD, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Tình trạng hợp đồng");
            Range row6_Header_Table_Tinh_Trang_HD_DK = oSheet.get_Range("Z6"); // Z6
            FormatTitleTable(ref row6_Header_Table_Tinh_Trang_HD_DK, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "Đk");
            Range row7_Header_Table_Z7 = oSheet.get_Range("Z7"); // Z7
            FormatTitleTable(ref row7_Header_Table_Z7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "26");


            Range row6_Header_Table_Tinh_Trang_HD_CK = oSheet.get_Range("AA6"); // AA6
            FormatTitleTable(ref row6_Header_Table_Tinh_Trang_HD_CK, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "Ck");
            Range row7_Header_Table_AA7 = oSheet.get_Range("AA7"); // AA7
            FormatTitleTable(ref row7_Header_Table_AA7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "27");


            Range row5_Header_Table_Qua_Trinh_LV = oSheet.get_Range("AB5", "AD5"); // AB5
            FormatTitleTable(ref row5_Header_Table_Qua_Trinh_LV, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Quá trình làm việc từ 2020 của người lao động quay lại");



            Range row5_Header_Table_AB6 = oSheet.get_Range("AB6", "AB7"); // AB6
            FormatTitleTable(ref row5_Header_Table_AB6, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Mã thẻ cũ");
            Range row6_Header_Table_AC6 = oSheet.get_Range("AC6", "AC7"); // AC6
            FormatTitleTable(ref row6_Header_Table_AC6, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Ngày vào làm");
            Range row7_Header_Table_AD6 = oSheet.get_Range("AD6", "AD7"); // AD6
            FormatTitleTable(ref row7_Header_Table_AD6, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Ngày nghỉ việc");


            Range row5_Header_Table_AB7 = oSheet.get_Range("AB7"); // AB7
            FormatTitleTable(ref row5_Header_Table_AB7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "28");
            Range row6_Header_Table_AC7 = oSheet.get_Range("AC7"); // AC7
            FormatTitleTable(ref row6_Header_Table_AC7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "29");
            Range row7_Header_Table_AD7 = oSheet.get_Range("AD7"); // AD7
            FormatTitleTable(ref row7_Header_Table_AD7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "30");



            Range row5_Header_Table_Ghi_Chu = oSheet.get_Range("AE5", "AE6"); // AE5
            FormatTitleTable(ref row5_Header_Table_Ghi_Chu, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Ghi chú");
            Range row7_Header_Table_AE7 = oSheet.get_Range("AE7"); // AE7
            FormatTitleTable(ref row7_Header_Table_AE7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "31");



            Range row5_Header_Table_Thoi_Gian_Nghi = oSheet.get_Range("AF5", "AG5"); // AF5
            FormatTitleTable(ref row5_Header_Table_Thoi_Gian_Nghi, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), true, "Thời gian nghỉ");
            Range row6_Header_Table_Tu_Ngay = oSheet.get_Range("AF6"); // AF6
            FormatTitleTable(ref row6_Header_Table_Tu_Ngay, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Từ ngày");
            Range row7_Header_Table_AF7 = oSheet.get_Range("AF7"); // AF7
            FormatTitleTable(ref row7_Header_Table_AF7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "32");


            Range row6_Header_Table_Den_Ngay = oSheet.get_Range("AG6"); // AG6
            FormatTitleTable(ref row6_Header_Table_Den_Ngay, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Đến ngày");
            Range row7_Header_Table_AG7 = oSheet.get_Range("AG7"); // AG7
            FormatTitleTable(ref row7_Header_Table_AG7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "33");


            Range row5_Header_Table_Dang_Ky_Mang_Thai = oSheet.get_Range("AH5", "AH6"); // AH5
            FormatTitleTable(ref row5_Header_Table_Dang_Ky_Mang_Thai, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Đăng ký mang thai");
            Range row7_Header_Table_AH7 = oSheet.get_Range("AH7"); // AH7
            FormatTitleTable(ref row7_Header_Table_AH7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "34");


            Range row5_Header_Table_HDLD = oSheet.get_Range("AI5", "AM5"); // AI5
            FormatTitleTable(ref row5_Header_Table_HDLD, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Hợp đồng lao động");
            Range row6_Header_Table_Loai_HDLD = oSheet.get_Range("AI6"); // AI6
            FormatTitleTable(ref row6_Header_Table_Loai_HDLD, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Loại hợp đồng");
            Range row7_Header_Table_AI7 = oSheet.get_Range("AI7"); // AE7
            FormatTitleTable(ref row7_Header_Table_AI7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "35");


            Range row6_Header_Table_So_HDLD = oSheet.get_Range("AJ6"); // AJ6
            FormatTitleTable(ref row6_Header_Table_So_HDLD, fontName, fontSizeNoiDung, 20, height_Single, Color.FromArgb(255, 255, 255), false, "Số HĐLD");
            Range row7_Header_Table_AJ7 = oSheet.get_Range("AJ7"); // AJ7
            FormatTitleTable(ref row7_Header_Table_AJ7, fontName, fontSizeNoiDung, 20, height_Single, Color.FromArgb(255, 255, 255), false, "36");


            Range row6_Header_Table_Ngay_Ky_HDLD = oSheet.get_Range("AK6"); // AK6
            FormatTitleTable(ref row6_Header_Table_Ngay_Ky_HDLD, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày ký");
            Range row7_Header_Table_AK7 = oSheet.get_Range("AK7"); // AK7
            FormatTitleTable(ref row7_Header_Table_AK7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "37");


            Range row6_Header_Table_Tu_Ngay_HDLD = oSheet.get_Range("AL6"); // AL6
            FormatTitleTable(ref row6_Header_Table_Tu_Ngay_HDLD, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Từ ngày");
            Range row7_Header_Table_AL7 = oSheet.get_Range("AL7"); // AL7
            FormatTitleTable(ref row7_Header_Table_AL7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "38");


            Range row6_Header_Table_Den_Ngay_HDLD = oSheet.get_Range("AM6"); // AM6
            FormatTitleTable(ref row6_Header_Table_Den_Ngay_HDLD, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Đến ngày");
            Range row7_Header_Table_AM7 = oSheet.get_Range("AM7"); // AM7
            FormatTitleTable(ref row7_Header_Table_AM7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "39");


            Range row5_Header_Table_Dan_Toc = oSheet.get_Range("AN5", "AN6"); // AN5
            FormatTitleTable(ref row5_Header_Table_Dan_Toc, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Dân tộc");
            Range row7_Header_Table_AN7 = oSheet.get_Range("AN7"); // AN7
            FormatTitleTable(ref row7_Header_Table_AN7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "40");


            Range row5_Header_Table_Ton_Giao = oSheet.get_Range("AO5", "AO6"); // AO5
            FormatTitleTable(ref row5_Header_Table_Ton_Giao, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Tôn giáo");
            Range row7_Header_Table_AO7 = oSheet.get_Range("AO7"); // AO7
            FormatTitleTable(ref row7_Header_Table_AO7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "41");


            Range row5_Header_Table_Dang_Vien = oSheet.get_Range("AP5", "AP6"); // AP5
            FormatTitleTable(ref row5_Header_Table_Dang_Vien, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Đảng viên");
            Range row7_Header_Table_AP7 = oSheet.get_Range("AP7"); // AJ7
            FormatTitleTable(ref row7_Header_Table_AP7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "42");


            Range row5_Header_Table_Tinh_Trang_Hon_Nhan = oSheet.get_Range("AQ5", "AQ6"); // AQ5
            FormatTitleTable(ref row5_Header_Table_Tinh_Trang_Hon_Nhan, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Tình trạng hôn nhân");
            Range row7_Header_Table_AQ7 = oSheet.get_Range("AQ7"); // AQ7
            FormatTitleTable(ref row7_Header_Table_AQ7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "43");


            Range row5_Header_Table_Thuoc_Ho_Ngheo = oSheet.get_Range("AR5", "AR6"); // AR5
            FormatTitleTable(ref row5_Header_Table_Thuoc_Ho_Ngheo, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Thuộc hộ nghèo");
            Range row7_Header_Table_AR7 = oSheet.get_Range("AR7"); // AR7
            FormatTitleTable(ref row7_Header_Table_AR7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "44");


            Range row5_Header_Table_Tinh_Chat_Cong_Viec = oSheet.get_Range("AS5", "AS6"); // AS5
            FormatTitleTable(ref row5_Header_Table_Tinh_Chat_Cong_Viec, fontName, fontSizeNoiDung, 12, height_Single, Color.FromArgb(255, 255, 255), true, "Tính chất công việc"); ;
            Range row7_Header_Table_AS7 = oSheet.get_Range("AS7"); // AS7
            FormatTitleTable(ref row7_Header_Table_AS7, fontName, fontSizeNoiDung, 12, height_Single, Color.FromArgb(255, 255, 255), false, "45");


            Range row5_Header_Table_Bao_Hiem = oSheet.get_Range("AT5", "AU5"); // AT5
            FormatTitleTable(ref row5_Header_Table_Bao_Hiem, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Bảo hiểm");
            Range row6_Header_Table_So_BHXH = oSheet.get_Range("AT6"); // AT6
            FormatTitleTable(ref row6_Header_Table_So_BHXH, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Số sổ bhxh");
            Range row7_Header_Table_AT7 = oSheet.get_Range("AT7"); // AT7
            FormatTitleTable(ref row7_Header_Table_AT7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "46");


            Range row6_Header_Table_Ngay_TG_BHXH = oSheet.get_Range("AU6"); // AU6
            FormatTitleTable(ref row6_Header_Table_Ngay_TG_BHXH, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày tham gia bhxh");
            Range row7_Header_Table_AU7 = oSheet.get_Range("AU7"); // AU7
            FormatTitleTable(ref row7_Header_Table_AU7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "47");


            Range row5_Header_Table_Tien_Luong = oSheet.get_Range("AV5", "AW5"); // AV5
            FormatTitleTable(ref row5_Header_Table_Tien_Luong, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), true, "Tiền lương");
            Range row6_Header_Table_Ngay_Hieu_Luc = oSheet.get_Range("AV6"); // AV6
            FormatTitleTable(ref row6_Header_Table_Ngay_Hieu_Luc, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày hiệu lực");
            Range row7_Header_Table_AV7 = oSheet.get_Range("AV7"); // AV7
            FormatTitleTable(ref row7_Header_Table_AV7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "48");


            Range row6_Header_Table_Muc_Luong = oSheet.get_Range("AW6"); // AW6
            FormatTitleTable(ref row6_Header_Table_Muc_Luong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Mức lương");
            Range row7_Header_Table_AW7 = oSheet.get_Range("AW7"); // AW7
            FormatTitleTable(ref row7_Header_Table_AW7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "49");


            Range row5_Header_Table_SDT = oSheet.get_Range("AX5", "AX6"); // AX5
            FormatTitleTable(ref row5_Header_Table_SDT, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Số điện thoại");
            Range row7_Header_Table_AX7 = oSheet.get_Range("AX7"); // AX7
            FormatTitleTable(ref row7_Header_Table_AX7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "50");


            Range row5_Header_Table_GD_Cung_CTy = oSheet.get_Range("AY5", "BB5"); // AY5-BB5
            FormatTitleTable(ref row5_Header_Table_GD_Cung_CTy, fontName, fontSizeNoiDung, 65, height_Single, Color.FromArgb(255, 255, 255), true, "Gia đình làm cùng công ty");
            Range row6_Header_Table_GD_Ho_Ten = oSheet.get_Range("AY6"); // AY6
            FormatTitleTable(ref row6_Header_Table_GD_Ho_Ten, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), false, "Họ và tên");
            Range row6_Header_Table_GD_Ma_The = oSheet.get_Range("AZ6"); // AZ6
            FormatTitleTable(ref row6_Header_Table_GD_Ma_The, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Mã thẻ");
            Range row6_Header_Table_GD_Bo_Phan = oSheet.get_Range("BA6"); // BA6
            FormatTitleTable(ref row6_Header_Table_GD_Bo_Phan, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Bộ phận");
            Range row6_Header_Table_GD_Quan_He = oSheet.get_Range("BB6"); // BA6
            FormatTitleTable(ref row6_Header_Table_GD_Quan_He, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Mối quan hệ");
            Range row7_Header_Table_AY7 = oSheet.get_Range("AY7"); // AY7
            FormatTitleTable(ref row7_Header_Table_AY7, fontName, fontSizeNoiDung, 30, height_Single, Color.FromArgb(255, 255, 255), false, "51");
            Range row7_Header_Table_AZ7 = oSheet.get_Range("AZ7"); // AZ7
            FormatTitleTable(ref row7_Header_Table_AZ7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "52");
            Range row7_Header_Table_BA7 = oSheet.get_Range("BA7"); // BA7
            FormatTitleTable(ref row7_Header_Table_BA7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "53");
            Range row7_Header_Table_BB7 = oSheet.get_Range("BB7"); // BB7
            FormatTitleTable(ref row7_Header_Table_BB7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "54");


            Range row5_Header_Table_Phat_DP_BHLD = oSheet.get_Range("BC5", "BE5"); // BB5-BD5
            FormatTitleTable(ref row5_Header_Table_Phat_DP_BHLD, fontName, fontSizeNoiDung, 45, height_Single, Color.FromArgb(255, 255, 255), true, "Phát đồng phục/Bảo hộ lao động");
            Range row6_Header_Table_DP_Ngay_Phat = oSheet.get_Range("BC6"); // BC6
            FormatTitleTable(ref row6_Header_Table_DP_Ngay_Phat, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày phát");
            Range row6_Header_Table_DP_So_Luong = oSheet.get_Range("BD6"); // BD6
            FormatTitleTable(ref row6_Header_Table_DP_So_Luong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Số lượng");
            Range row6_Header_Table_Thu_Hoi = oSheet.get_Range("BE6"); // BE6
            FormatTitleTable(ref row6_Header_Table_Thu_Hoi, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Thu hồi khi nghỉ");
            Range row7_Header_Table_BC7 = oSheet.get_Range("BC7"); // BC7
            FormatTitleTable(ref row7_Header_Table_BC7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "55");
            Range row7_Header_Table_BD7 = oSheet.get_Range("BD7"); // BD7
            FormatTitleTable(ref row7_Header_Table_BD7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "56");
            Range row7_Header_Table_BE7 = oSheet.get_Range("BE7"); // BE7
            FormatTitleTable(ref row7_Header_Table_BE7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "57");


            Range row5_Header_Table_Phat_Chia_Khoa_Tu_Ca_Nhan = oSheet.get_Range("BF5", "BH5"); // BF5-BH5
            FormatTitleTable(ref row5_Header_Table_Phat_Chia_Khoa_Tu_Ca_Nhan, fontName, fontSizeNoiDung, 45, height_Single, Color.FromArgb(255, 255, 255), true, "Phát chìa khóa tủ cá nhân");
            Range row6_Header_Table_Chia_Khoa_Ngay_Phat = oSheet.get_Range("BF6"); // BF6
            FormatTitleTable(ref row6_Header_Table_Chia_Khoa_Ngay_Phat, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày phát");
            Range row6_Header_Table_Chia_Khoa_So_Luong = oSheet.get_Range("BG6"); // BG6
            FormatTitleTable(ref row6_Header_Table_Chia_Khoa_So_Luong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Số tủ");
            Range row6_Header_Table_Chia_Khoa_Thu_Hoi = oSheet.get_Range("BH6"); // BH6
            FormatTitleTable(ref row6_Header_Table_Chia_Khoa_Thu_Hoi, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Thu hồi khi nghỉ");
            Range row7_Header_Table_BF7 = oSheet.get_Range("BF7"); // BF7
            FormatTitleTable(ref row7_Header_Table_BF7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "58");
            Range row7_Header_Table_BG7 = oSheet.get_Range("BG7"); // BG7
            FormatTitleTable(ref row7_Header_Table_BG7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "59");
            Range row7_Header_Table_BH7 = oSheet.get_Range("BH7"); // BH7
            FormatTitleTable(ref row7_Header_Table_BH7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "60");


            Range row5_Header_Table_Phat_Choi_Ve_Sinh = oSheet.get_Range("BI5", "BK5"); // BI5-BK5
            FormatTitleTable(ref row5_Header_Table_Phat_Choi_Ve_Sinh, fontName, fontSizeNoiDung, 45, height_Single, Color.FromArgb(255, 255, 255), true, "Phát chổi vệ sinh");
            Range row6_Header_Table_Choi_Ngay_Phat = oSheet.get_Range("BI6"); // BI6
            FormatTitleTable(ref row6_Header_Table_Choi_Ngay_Phat, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Ngày phát");
            Range row6_Header_Table_Choi_So_Luong = oSheet.get_Range("BJ6"); // BJ6
            FormatTitleTable(ref row6_Header_Table_Choi_So_Luong, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Số lượng");
            Range row6_Header_Table_Choi_Thu_Hoi = oSheet.get_Range("BK6"); // BK6
            FormatTitleTable(ref row6_Header_Table_Choi_Thu_Hoi, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "Thu hồi khi nghỉ");
            Range row7_Header_Table_BI7 = oSheet.get_Range("BI7"); // BI7
            FormatTitleTable(ref row7_Header_Table_BI7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "61");
            Range row7_Header_Table_BJ7 = oSheet.get_Range("BJ7"); // BJ7
            FormatTitleTable(ref row7_Header_Table_BJ7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "62");
            Range row7_Header_Table_BK7 = oSheet.get_Range("BK7"); // BK7
            FormatTitleTable(ref row7_Header_Table_BK7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "63");


            Range row5_Header_Table_Quan_So = oSheet.get_Range("BL5", "BL6"); // BL5
            FormatTitleTable(ref row5_Header_Table_Quan_So, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), true, "Quân số");
            Range row7_Header_Table_BL7 = oSheet.get_Range("BL7"); // BL7
            FormatTitleTable(ref row7_Header_Table_BL7, fontName, fontSizeNoiDung, 9, height_Single, Color.FromArgb(255, 255, 255), false, "64");


            Range row5_Header_Table_Khoi_Bo_Phan = oSheet.get_Range("BM5", "BM6"); // BM5
            FormatTitleTable(ref row5_Header_Table_Khoi_Bo_Phan, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), true, "Khối bộ phận");
            Range row7_Header_Table_BM7 = oSheet.get_Range("BM7"); // BM7
            FormatTitleTable(ref row7_Header_Table_BM7, fontName, fontSizeNoiDung, 15, height_Single, Color.FromArgb(255, 255, 255), false, "65");
        }
        private void HeaderReport(ref Microsoft.Office.Interop.Excel.Worksheet oSheet, string fontName = "Times New Roman", int fontSizeNoiDung = 11, string lastColumn = "", int fontSizeTieuDe = 11)
        {
            Range row1_TieuDe_BaoCao = oSheet.get_Range("A1", "D1"); // = A1 - D1
            FormatTieuDeBaoCao(ref row1_TieuDe_BaoCao, true, false, 11, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Excel tailoring co.,ltd");

            Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "D2"); // = A2 - D2
            FormatTieuDeBaoCao(ref row2_TieuDe_BaoCao, true, false, 11, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Yen Ninh Town - Yen Khanh District - Ninh Binh Province");

            Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", "BM3"); // = A3 - BL3
            FormatTieuDeBaoCao(ref row3_TieuDe_BaoCao, true, true, 18, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "DANH SÁCH CÁN BỘ CÔNG NHÂN VIÊN NHÀ MÁY");
            row3_TieuDe_BaoCao.RowHeight = 27;

            DateTime ngayin = System.Convert.ToDateTime(NgayIn.EditValue);
            string Ngay = ngayin.ToString("dd");
            string Thang = ngayin.ToString("MM");
            string Nam = ngayin.Year.ToString();
            Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", "BM4"); // = A4 - BL4
            FormatTieuDeBaoCao(ref row4_TieuDe_BaoCao, true, true, 12, fontName, "@", XlHAlign.xlHAlignLeft, XlVAlign.xlVAlignCenter, "Ngày " + Ngay + " Tháng " + Thang + " Năm " + Nam);

            return;
        }
        private void inAll()
        {
            System.Data.SqlClient.SqlConnection conn;

            try
            {

                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSCanBoCongNhanVien_NB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = lkDonVi.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = lkXiNghiep.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = lkTo.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = (dDenNgayNS.EditValue == null) ? "01/01/2999" : dDenNgayNS.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                adp.Fill(dt);
                dt.TableName = "DA_TA";

                // Format for an Excel file
                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }

                //Init object to work with Excel
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string lastColumn = String.Empty;
                lastColumn = CharacterIncrement(dt.Columns.Count - 1);

                //Create header of report
                HeaderReport(ref oSheet, "Times New Roman", 11, lastColumn, 11);

                //Create header of table
                CreateHeaderTable(ref oSheet, "Times New Roman", 9);

                DataRow[] dr = dt.Select();
                string[,] rowData = new string[dr.Count(), dt.Columns.Count];

                int rowCnt = 0;
                int col_bd = 0;

                //Transfer from Data Table class into a 2-dimention array.
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dt.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 7;


                //Fill data from dt into Data table of Excel
                oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;

                Microsoft.Office.Interop.Excel.Range formatRangeAll = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());//Format all Data table
                BorderAround(formatRangeAll);
                formatRangeAll.WrapText = false;

                Microsoft.Office.Interop.Excel.Range formatRangeTitleTable = oSheet.get_Range("A5", lastColumn + "7");//Format title of Data table

                formatRangeTitleTable.Font.Bold = true;
                formatRangeTitleTable.Font.Name = "Times New Roman";
                formatRangeTitleTable.WrapText = true;
                formatRangeTitleTable.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRangeTitleTable.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRangeTitleTable.RowHeight = 30;

                formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders.Color = Color.Black;
                formatRangeTitleTable.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
                formatRangeTitleTable.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                formatRangeTitleTable.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;

                Microsoft.Office.Interop.Excel.Range formatRange1 = oSheet.get_Range("A8", "B" + rowCnt.ToString());//Format colum A->Q of Data table
                Microsoft.Office.Interop.Excel.Range formatRange2 = oSheet.get_Range("E8", "F" + rowCnt.ToString());////Format colum E->F of Data table
                Microsoft.Office.Interop.Excel.Range formatRange3 = oSheet.get_Range("Q8", "Q" + rowCnt.ToString());////Format colum Q of Data table
                Microsoft.Office.Interop.Excel.Range formatRange4 = oSheet.get_Range("T8", "U" + rowCnt.ToString());//Format colum T->U of Data table
                Microsoft.Office.Interop.Excel.Range formatRange5 = oSheet.get_Range("X8", "AA" + rowCnt.ToString());//Format colum X->AA of Data table
                Microsoft.Office.Interop.Excel.Range formatRange6 = oSheet.get_Range("AC8", "AD" + rowCnt.ToString());////Format colum AC->AD of Data table
                Microsoft.Office.Interop.Excel.Range formatRange7 = oSheet.get_Range("AF8", "AH" + rowCnt.ToString());////Format colum AF->AH of Data table
                Microsoft.Office.Interop.Excel.Range formatRange8 = oSheet.get_Range("AK8", "AM" + rowCnt.ToString());////Format colum AK->AM of Data table
                Microsoft.Office.Interop.Excel.Range formatRange9 = oSheet.get_Range("AP8", "AP" + rowCnt.ToString());////Format colum AP of Data table
                Microsoft.Office.Interop.Excel.Range formatRange10 = oSheet.get_Range("AU8", "AV" + rowCnt.ToString());//Format colum AU->AV of Data table
                Microsoft.Office.Interop.Excel.Range formatRange11 = oSheet.get_Range("AX8", "AX" + rowCnt.ToString());////Format colum AX8 of Data table
                Microsoft.Office.Interop.Excel.Range formatRange13 = oSheet.get_Range("BC8", lastColumn + rowCnt.ToString());////Format colum BC->lastColumn of Data table


                Microsoft.Office.Interop.Excel.Range formatRange12 = oSheet.get_Range("AW8", "AW" + rowCnt.ToString());////Format colum AW8 of Data table

                formatRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange4.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange5.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange6.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange7.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange8.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange9.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange10.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange11.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange11.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange13.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange13.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                formatRange12.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile, AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);


            }
            catch
            {
            }
        }
        #endregion
    }
}
