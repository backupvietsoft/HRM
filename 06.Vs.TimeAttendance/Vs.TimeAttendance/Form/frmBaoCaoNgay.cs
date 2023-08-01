using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Vs.TimeAttendance
{
    public partial class frmBaoCaoNgay : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idHD;
        public DateTime dNgayDL;
        public Int64 ID_DV = -1;
        public Int64 ID_XN = -1;
        public Int64 ID_TO = -1;

        public frmBaoCaoNgay()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void frmBaoCaoNgay_Load(object sender, EventArgs e)
        {
            //chkChuaThamGia.Visible = false;
            //chkDaThamGia.Visible = false;
            rdo_ChonBaoCao.SelectedIndex = 0;

            if (Commons.Modules.KyHieuDV != "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
            }

            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            datTNgay.EditValue = dNgayDL;
            datDNgay.EditValue = dNgayDL;
            Commons.OSystems.SetDateEditFormat(datTNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);
            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_BCTongHopVangDG":
                                {
                                    frmViewReport frm = new frmViewReport();
                                    DataTable dt;
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    frm.rpt = new rptDSVangDauGioTheoDV(dNgayIn.DateTime, dNgayDL,Convert.ToInt32(ID_DV));
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSVangNgayDV"), conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = ID_DV;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dNgayDL;
                                        cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 1;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                    }
                                    catch(Exception ex)
                                    { }
                                    frm.ShowDialog();
                                    break;
                                }
                            case "rdo_DSChiTietVangDG":
                                {
                                    if (Commons.Modules.KyHieuDV == "NB")
                                    {
                                        try
                                        {
                                            System.Data.SqlClient.SqlConnection conn;
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            DataTable dtBCVangDG;

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSVangNgayDV_NB", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = ID_DV;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayDL;
                                            cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 0;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dtBCVangDG = new DataTable();
                                            dtBCVangDG = ds.Tables[0].Copy();
                                            if (dtBCVangDG.Rows.Count == 0)
                                            {
                                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            this.Cursor = Cursors.WaitCursor;
                                            Microsoft.Office.Interop.Excel.Application oXL;
                                            Microsoft.Office.Interop.Excel.Workbook oWB;
                                            Excel.Worksheet oSheet;
                                            oXL = new Microsoft.Office.Interop.Excel.Application();
                                            oXL.Visible = true;

                                            //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                                            //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                                            oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                                            oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                                            string fontName = "Times New Roman";
                                            int fontSizeTieuDe = 16;
                                            int fontSizeNoiDung = 11;

                                            oSheet.Cells[1, 1].Value2 = "Excel Tailoring Co.Ltd,";
                                            oSheet.Cells[1, 1].Font.Size = fontSizeNoiDung;
                                            oSheet.Cells[1, 1].Font.Bold = true;
                                            oSheet.Cells[1, 1].Font.Name = fontName;
                                            oSheet.Cells[1, 1].WrapText = false;

                                            oSheet.Cells[2, 1].Value2 = "Yen Ninh Town - Yen Khanh District - Ninh Binh Province";
                                            oSheet.Cells[2, 1].Font.Size = fontSizeNoiDung;
                                            oSheet.Cells[2, 1].Font.Bold = true;
                                            oSheet.Cells[2, 1].Font.Name = fontName;
                                            oSheet.Cells[2, 1].WrapText = false;

                                            Excel.Range formatRange;
                                            formatRange = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 9]];
                                            formatRange.Merge();
                                            formatRange.Value2 = "DANH SÁCH NHÂN VIÊN VẮNG MẶT";
                                            formatRange.Font.Size = fontSizeTieuDe;
                                            formatRange.Font.Bold = true;
                                            formatRange.Font.Name = fontName;
                                            formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                            formatRange = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 9]];
                                            formatRange.Merge();
                                            formatRange.Value2 = "NGAY : " + Convert.ToDateTime(datDNgay.EditValue).ToString("dd/MM/yyyy");
                                            formatRange.Font.Size = 14;
                                            formatRange.Font.Bold = true;
                                            formatRange.Font.Name = fontName;
                                            formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                            oSheet.Cells[7, 1].Value2 = "STT";
                                            oSheet.Cells[7, 1].ColumnWidth = 6;
                                            oSheet.Cells[7, 2].Value2 = "HỌ TÊN";
                                            oSheet.Cells[7, 2].ColumnWidth = 30;
                                            oSheet.Cells[7, 3].Value2 = "MSCN";
                                            oSheet.Cells[7, 3].ColumnWidth = 10;
                                            oSheet.Cells[7, 4].Value2 = "BỘ PHẬN";
                                            oSheet.Cells[7, 4].ColumnWidth = 30;
                                            oSheet.Cells[7, 5].Value2 = "GIỜ ĐẾN";
                                            oSheet.Cells[7, 5].ColumnWidth = 10;
                                            oSheet.Cells[7, 6].Value2 = "LÝ DO VẮNG";
                                            oSheet.Cells[7, 6].ColumnWidth = 30;
                                            oSheet.Cells[7, 7].Value2 = "GHI CHÚ";
                                            oSheet.Cells[7, 7].ColumnWidth = 30;
                                            oSheet.Cells[7, 8].Value2 = "TỪ NGÀY";
                                            oSheet.Cells[7, 8].ColumnWidth = 15;
                                            oSheet.Cells[7, 9].Value2 = "ĐẾN NGÀY";
                                            oSheet.Cells[7, 9].ColumnWidth = 15;

                                            formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 9]];
                                            formatRange.Font.Size = fontSizeNoiDung;
                                            formatRange.Font.Bold = true;
                                            formatRange.Font.Name = fontName;
                                            formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Interior.Color = Color.FromArgb(191, 250, 253);

                                            BorderAround(oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 9]]);

                                            DataRow[] dr = dtBCVangDG.Select();
                                            string[,] rowData = new string[dr.Count(), dtBCVangDG.Columns.Count];

                                            int col = 0;
                                            int rowCnt = 0;
                                            int lastColumn = 0;
                                            lastColumn = dtBCVangDG.Columns.Count;
                                            foreach (DataRow row in dr)
                                            {
                                                for (col = 0; col < lastColumn; col++)
                                                {
                                                    rowData[rowCnt, col] = row[col].ToString();
                                                }

                                                rowCnt++;
                                            }
                                            rowCnt = rowCnt + 7;
                                            //oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                                            formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]];
                                            formatRange.Value2 = rowData;
                                            formatRange.Font.Name = fontName;
                                            formatRange.Font.Size = fontSizeNoiDung;


                                            formatRange = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, 1]];
                                            formatRange.NumberFormat = "";
                                            formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                                            try
                                            {
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }
                                            catch { }

                                            formatRange = oSheet.Range[oSheet.Cells[8, 3], oSheet.Cells[rowCnt, 3]];
                                            formatRange.NumberFormat = "";
                                            //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                                            try
                                            {
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }
                                            catch { }

                                            formatRange = oSheet.Range[oSheet.Cells[8, 5], oSheet.Cells[rowCnt, 5]];
                                            formatRange.NumberFormat = "";
                                            formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                                            try
                                            {
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }
                                            catch { }


                                            formatRange = oSheet.Range[oSheet.Cells[8, 8], oSheet.Cells[rowCnt, 8]];
                                            formatRange.NumberFormat = "";
                                            formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                                            try
                                            {
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }
                                            catch { }


                                            formatRange = oSheet.Range[oSheet.Cells[8, 9], oSheet.Cells[rowCnt, 9]];
                                            formatRange.NumberFormat = "";
                                            formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                            formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
                                            try
                                            {
                                                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                                            }
                                            catch { }

                                            BorderAround(oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]]);
                                            this.Cursor = Cursors.Default;
                                        }
                                        catch (Exception ex)
                                        {
                                            this.Cursor = Cursors.Default;
                                            XtraMessageBox.Show(ex.Message.ToString());
                                        }
                                    }
                                    else
                                    {
                                        frmViewReport frm = new frmViewReport();
                                        DataTable dt;
                                        System.Data.SqlClient.SqlConnection conn1;
                                        dt = new DataTable();
                                        frm.rpt = new rptDSVangDauGioTheoNgay(dNgayIn.DateTime, dNgayDL, Convert.ToInt32(ID_DV));

                                        try
                                        {
                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSVangNgayDV"), conn1);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = ID_DV;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayDL;
                                            cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 0;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        }
                                        catch (Exception ex)
                                        {
                                            XtraMessageBox.Show(ex.Message.ToString());
                                        }
                                        frm.ShowDialog();
                                    }

                                    break;
                                }
                            case "rdo_DSDiTreVeSom":
                                {
                                    frmViewReport frm = new frmViewReport();
                                    DataTable dt;
                                    System.Data.SqlClient.SqlConnection conn2;
                                    dt = new DataTable();
                                    string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptDSDiTreVeSom", "lblDSNhanVienDiTreVeSom");
                                    frm.rpt = new rptDSDiTreVeSom(dNgayDL, sTieuDe, dNgayIn.DateTime, Convert.ToInt32(ID_DV));

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();
                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSDiTreVeSom"), conn2);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = ID_DV;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                                        cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = dNgayDL;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 3;

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbT" + Commons.Modules.UserName, dt, "");
                                        dt = new DataTable();
                                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, " select ID_CN,MS_CN,HO_TEN,TEN_XN,TEN_TO,GIO_DEN,PHUT_TRE,GIO_VE,case PHUT_VS WHEN 0 THEN null else  PHUT_VS END as PHUT_VS from sbT" + Commons.Modules.UserName + ""));
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        Commons.Modules.ObjSystems.XoaTable("sbT" + Commons.Modules.UserName);
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                    break;
                                }
                            case "rdo_DSNhanVienVachTheLoi":
                                {
                                    frmViewReport frm = new frmViewReport();
                                    DataTable dt;
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    frm.rpt = new rptDSNVVachTheLoi(datTNgay.DateTime, datDNgay.DateTime, dNgayIn.DateTime, Convert.ToInt32(ID_DV));
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSNVVachTheLoi"), conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = ID_DV;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                                        cmd.Parameters.Add("@TNGAY", SqlDbType.DateTime).Value = datTNgay.DateTime;  //Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                                        cmd.Parameters.Add("@DNGAY", SqlDbType.DateTime).Value = datDNgay.DateTime;  //Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        //DataSet ds = new DataSet();
                                        dt = new DataTable();
                                        adp.Fill(dt);

                                        //dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                    }
                                    catch (Exception ex)
                                    { }
                                    frm.ShowDialog();
                                    break;
                                }
                            case "rdo_BaoCaoNhanSuNgay":
                                {
                                    BangChamCongNgay_DM();
                                    break;
                                }
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        #region function
        private void BangChamCongNgay_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;
                splashScreenManager1.ShowWaitForm();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNgay_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = ID_DV;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgayDL;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLXN = ds.Tables[1].Copy();
                int slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                oSheet.Name = dNgayDL.Day.ToString();
                #region TheoNgay

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row1_TieuDe = oSheet.get_Range("B1");
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";
                row1_TieuDe.WrapText = false;
                row1_TieuDe.Font.Size = 12;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.RowHeight = 21;
                row1_TieuDe.ColumnWidth = 43;



                Range row2_TieuDe = oSheet.get_Range("B2", "N2");
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = 12;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Value2 = "BÁO CÁO HÀNG NGÀY/ DAILY ATTENDANCE REPORT";
                row2_TieuDe.WrapText = false;
                row2_TieuDe.RowHeight = 33;
                row2_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                Range row3_Date = oSheet.get_Range("L3", "N3");
                row3_Date.Font.Bold = true;
                row3_Date.Merge();
                row3_Date.Font.Size = 12;
                row3_Date.Font.Name = fontName;
                row3_Date.Value2 = "Ngày/ Date:" + dNgayDL.ToString("dd/MM/yyyy") + "";
                row3_Date.WrapText = false;
                row3_Date.RowHeight = 24;
                row3_Date.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_Date.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Range row4 = oSheet.get_Range("B4");
                row4.RowHeight = 66;

                Range row5 = oSheet.get_Range("B5");
                row5.RowHeight = 79;

                Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Range row1_TieuDe_Stt = oSheet.get_Range("A1");
                row1_TieuDe_Stt.ColumnWidth = 2;

                Range row5_TieuDe_Stt = oSheet.get_Range("B4", "B5");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row6_TieuDe_Stt = oSheet.get_Range("C4", "C5");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                row6_TieuDe_Stt.ColumnWidth = 30;
                row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);


                Range row5_TieuDe_MaSo = oSheet.get_Range("D4", "D5");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "Tổng lao động hôm trước/ Total employees y";
                row5_TieuDe_MaSo.ColumnWidth = 15;
                row5_TieuDe_MaSo.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row6_TieuDe_MaSo = oSheet.get_Range("E4", "E5");
                row6_TieuDe_MaSo.Merge();
                row6_TieuDe_MaSo.Value2 = "Nghỉ việc/ Resigned";
                row6_TieuDe_MaSo.ColumnWidth = 12;
                row6_TieuDe_MaSo.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row5_TieuDe_HoTen = oSheet.get_Range("F4", "F5");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Người mới/ New comer";
                row5_TieuDe_HoTen.ColumnWidth = 14;
                row5_TieuDe_HoTen.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row6_TieuDe_HoTen = oSheet.get_Range("G4", "G5");
                row6_TieuDe_HoTen.Merge();
                row6_TieuDe_HoTen.Value2 = "Tổng lao động/ Total employees";
                row6_TieuDe_HoTen.ColumnWidth = 12;
                row6_TieuDe_HoTen.Interior.Color = Color.FromArgb(255, 255, 0);



                Range row5_TieuDe_To = oSheet.get_Range("H4", "L4");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Tổng lao động vắng mặt/ Total Absence";
                row5_TieuDe_To.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_TS = oSheet.get_Range("H5");
                row6_TieuDe_TS.Merge();
                row6_TieuDe_TS.Value2 = "Thai sản/ Maternity";
                row6_TieuDe_TS.ColumnWidth = 12;
                row6_TieuDe_TS.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_LDNCL = oSheet.get_Range("I5");
                row6_TieuDe_LDNCL.Merge();
                row6_TieuDe_LDNCL.Value2 = "LĐ nghỉ cách ly";
                row6_TieuDe_LDNCL.ColumnWidth = 12;
                row6_TieuDe_LDNCL.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_VCLD = oSheet.get_Range("J5");
                row6_TieuDe_VCLD.Merge();
                row6_TieuDe_VCLD.Value2 = "Vắng có lý do/ Absence have reason";
                row6_TieuDe_VCLD.ColumnWidth = 10;
                row6_TieuDe_VCLD.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_VKLD = oSheet.get_Range("K5");
                row6_TieuDe_VKLD.Merge();
                row6_TieuDe_VKLD.Value2 = "Vắng không có lý do/ Absence no reason";
                row6_TieuDe_VKLD.ColumnWidth = 10;
                row6_TieuDe_VKLD.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_Tong = oSheet.get_Range("L5");
                row6_TieuDe_Tong.Merge();
                row6_TieuDe_Tong.Value2 = "Tổng/ Total";
                row6_TieuDe_Tong.ColumnWidth = 8;
                row6_TieuDe_Tong.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_SLD = oSheet.get_Range("M4", "M5");
                row6_TieuDe_SLD.Merge();
                row6_TieuDe_SLD.Value2 = "Số lao động có mặt/ Total employees present";
                row6_TieuDe_SLD.ColumnWidth = 13;
                row6_TieuDe_SLD.Interior.Color = Color.FromArgb(189, 215, 238);


                Range row6_TieuDe_TLV = oSheet.get_Range("N4", "N5");
                row6_TieuDe_TLV.Merge();
                row6_TieuDe_TLV.Value2 = "Tỷ lệ vắng (%)";
                row6_TieuDe_TLV.ColumnWidth = 11;
                row6_TieuDe_TLV.Interior.Color = Color.FromArgb(255, 255, 0);

                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 7;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                int col = 1;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 7;
                string cotCN_A = "";
                string cotCN_B = "";
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                string sTenCotD = "=";
                string sTenCotE = "=";
                string sTenCotF = "=";
                string sTenCotH = "=";
                string sTenCotI = "=";
                string sTenCotJ = "=";
                string sTenCotK = "=";
                string sTenCotL = "=";
                string sTenCotM = "=";

                string sRowXN = "";
                string s = int_to_Roman(9);
                int rowSum = 8; //Row sum của cột G 
                for (int i = 0; i < TEN_XN.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            //sTenCot = CharacterIncrement(6);
                            //Excel.Range formatRange7;
                            //formatRange7 = oSheet.get_Range(sTenCot + ((rowCnt + 1) + 7).ToString());
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


                    // Tạo group xí nghiệp
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("B" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 2] = TEN_XN[i].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Size = fontSizeNoiDung;
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Name = fontName;

                    oSheet.Cells[rowBD, 3] = "Sub-Total " + int_to_Roman(i + 1) + "";
                    oSheet.Cells[rowBD, 3].Font.Bold = true;
                    oSheet.Cells[rowBD, 3].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 3].Font.Name = fontName;

                    oSheet.Cells[rowBD, 4] = "=SUM(" + CharacterIncrement(3) + "" + (rowBD + 1) + ":" + CharacterIncrement(3) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 4].Font.Bold = true;
                    oSheet.Cells[rowBD, 4].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 4].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 4].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 4].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 4].Font.Name = fontName;
                    sTenCotD = sTenCotD + CharacterIncrement(3) + rowBD + "+";
                    sRowXN = sRowXN + rowBD + ",";

                    //Fortmart cột D
                    Microsoft.Office.Interop.Excel.Range formatRange10;
                    formatRange10 = oSheet.get_Range("D" + (rowBD + 1) + "", "D" + (rowCnt + 1));
                    formatRange10.Font.Bold = true;

                    //Fortmart cột G
                    formatRange10 = oSheet.get_Range("G" + (rowBD + 1) + "", "G" + (rowCnt + 1));
                    formatRange10.Font.Bold = true;
                    formatRange10.Font.Color = Color.FromArgb(255, 0, 0);

                    //Fortmart cột L
                    Microsoft.Office.Interop.Excel.Range formatRange11;
                    formatRange11 = oSheet.get_Range("L" + (rowBD + 1) + "", "L" + (rowCnt + 1));

                    //Fortmart cột M
                    Microsoft.Office.Interop.Excel.Range formatRange12;
                    formatRange12 = oSheet.get_Range("M" + (rowBD + 1) + "", "M" + (rowCnt + 1));

                    //Fortmart cột M
                    Microsoft.Office.Interop.Excel.Range formatRange13;
                    formatRange13 = oSheet.get_Range("N" + (rowBD + 1) + "", "N" + (rowCnt + 1));

                    oSheet.Cells[rowBD, 5] = "=SUM(" + CharacterIncrement(4) + "" + (rowBD + 1) + ":" + CharacterIncrement(4) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 5].Font.Bold = true;
                    oSheet.Cells[rowBD, 5].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotE = sTenCotE + CharacterIncrement(4) + rowBD + "+";

                    oSheet.Cells[rowBD, 6] = "=SUM(" + CharacterIncrement(5) + "" + (rowBD + 1) + ":" + CharacterIncrement(5) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 6].Font.Bold = true;
                    oSheet.Cells[rowBD, 6].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotF = sTenCotF + CharacterIncrement(5) + rowBD + "+";

                    //oSheet.Cells[rowBD, 7] = "=SUM(" + CharacterIncrement(6) + "" + (rowBD + 1) + ":" + CharacterIncrement(6) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 7] = "=D" + rowBD + "+F" + rowBD + "-E" + rowBD + "";
                    oSheet.Cells[rowBD, 7].Font.Bold = true;

                    oSheet.Cells[rowBD, 8] = "=SUM(" + CharacterIncrement(7) + "" + (rowBD + 1) + ":" + CharacterIncrement(7) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 8].Font.Bold = true;
                    oSheet.Cells[rowBD, 8].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotH = sTenCotH + CharacterIncrement(7) + rowBD + "+";


                    oSheet.Cells[rowBD, 9] = "=SUM(" + CharacterIncrement(8) + "" + (rowBD + 1) + ":" + CharacterIncrement(8) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 9].Font.Bold = true;
                    oSheet.Cells[rowBD, 9].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotI = sTenCotI + CharacterIncrement(8) + rowBD + "+";


                    oSheet.Cells[rowBD, 10] = "=SUM(" + CharacterIncrement(9) + "" + (rowBD + 1) + ":" + CharacterIncrement(9) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 10].Font.Bold = true;
                    oSheet.Cells[rowBD, 10].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotJ = sTenCotJ + CharacterIncrement(9) + rowBD + "+";


                    oSheet.Cells[rowBD, 11] = "=SUM(" + CharacterIncrement(10) + "" + (rowBD + 1) + ":" + CharacterIncrement(10) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 11].Font.Bold = true;
                    oSheet.Cells[rowBD, 11].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotK = sTenCotK + CharacterIncrement(10) + rowBD + "+";


                    oSheet.Cells[rowBD, 12] = "=SUM(I" + rowBD + ":K" + rowBD + ")";
                    //oSheet.Cells[rowBD, 12] = "=SUM(H" + rowBD + ":K" + rowBD + ")";
                    oSheet.Cells[rowBD, 12].Font.Bold = true;
                    oSheet.Cells[rowBD, 12].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotL = sTenCotL + CharacterIncrement(11) + rowBD + "+";


                    oSheet.Cells[rowBD, 13] = "=G" + rowBD + "-L" + rowBD + "-H" + rowBD + "";
                    //oSheet.Cells[rowBD, 13] = "=G" + rowBD + "-L" + rowBD + "";
                    oSheet.Cells[rowBD, 13].Font.Bold = true;
                    sTenCotM = sTenCotM + CharacterIncrement(12) + rowBD + "+";


                    oSheet.Cells[rowBD, 14] = "=IFERROR(L" + rowBD + "/G" + rowBD + ",0)";
                    oSheet.Cells[rowBD, 14].Font.Bold = true;
                    oSheet.Cells[rowBD, 14].Font.Color = Color.FromArgb(255, 0, 0);

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    formatRange10.Value2 = "=D" + rowSum + "+F" + rowSum + "-E" + rowSum + "";
                    formatRange11.Value2 = "=SUM(I" + rowSum + ":K" + rowSum + ")";
                    formatRange12.Value2 = "=G" + rowSum + "-L" + rowSum + "-H" + rowSum + "";
                    formatRange13.Value2 = "=IFERROR(L" + rowSum + "/G" + rowSum + ",0)";



                    ////Tính tổng xí nghiệp
                    //Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    //row_groupTONG_Format.Interior.Color = Color.Yellow;
                    //row_groupTONG_Format.Font.Bold = true;
                    //oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    //for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    //{
                    //    oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    //}
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowSum = rowCnt + 3;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                Microsoft.Office.Interop.Excel.Range formatRange; // range hien tai
                Microsoft.Office.Interop.Excel.Range formatRange1; // range ke tiep
                Microsoft.Office.Interop.Excel.Range formatRange3;
                string CurentColumn = string.Empty;
                int rowbd;
                int rowDup = 0; // row bat dau của dữ liệu duplicate
                int colKT = dtBCThang.Columns.Count;
                bool bChan = false;
                for (rowbd = 8; rowbd <= rowCnt; rowbd++)
                {
                    formatRange = oSheet.get_Range("B" + rowbd + "");
                    formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                    if (formatRange.Value == null)
                    {
                        formatRange = oSheet.get_Range("B" + (rowDup).ToString() + "");
                    }
                    if (formatRange.Value == formatRange1.Value)
                    {
                        if (bChan == false)
                        {
                            rowDup = rowbd;
                        }
                        bChan = true;
                        formatRange.Value = null;
                        formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                        formatRange3.Merge();
                    }
                    else
                    {
                        bChan = false;
                        rowDup = 0;
                    }
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                }

                rowCnt++;
                rowCnt++;

                Range rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                rowTONG_CONG.Value2 = "Tổng/Grand Total";
                rowTONG_CONG.Font.Bold = true;

                Range rowTONG_CONG1 = oSheet.get_Range("C" + rowCnt);
                string sLama = "(";
                for (int i = 1; i <= slXN; i++)
                {
                    sLama = sLama + int_to_Roman(i) + "+";
                }
                rowTONG_CONG1.Value2 = sLama.Substring(0, sLama.Length - 1) + ")";
                rowTONG_CONG1.Font.Bold = true;
                //rowTONG_CONG1.Font.Size = fontSizeNoiDung;
                //rowTONG_CONG1.Font.Name = fontName;

                sTenCotD = sTenCotD.Substring(0, sTenCotD.Length - 1);
                sTenCotE = sTenCotE.Substring(0, sTenCotE.Length - 1);
                sTenCotF = sTenCotF.Substring(0, sTenCotF.Length - 1);
                sTenCotH = sTenCotH.Substring(0, sTenCotH.Length - 1);
                sTenCotI = sTenCotI.Substring(0, sTenCotI.Length - 1);
                sTenCotJ = sTenCotJ.Substring(0, sTenCotJ.Length - 1);
                sTenCotK = sTenCotK.Substring(0, sTenCotK.Length - 1);
                sTenCotL = sTenCotL.Substring(0, sTenCotL.Length - 1);
                sTenCotM = sTenCotM.Substring(0, sTenCotM.Length - 1);

                oSheet.Cells[rowCnt, 4] = sTenCotD;
                oSheet.Cells[rowCnt, 4].Font.Bold = true;
                oSheet.Cells[rowCnt, 4].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 5] = sTenCotE;
                oSheet.Cells[rowCnt, 5].Font.Bold = true;
                oSheet.Cells[rowCnt, 5].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 6] = sTenCotF;
                oSheet.Cells[rowCnt, 6].Font.Bold = true;
                oSheet.Cells[rowCnt, 6].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 7] = "=D" + rowCnt + "+F" + rowCnt + "-E" + rowCnt + "";
                oSheet.Cells[rowCnt, 7].Font.Bold = true;

                oSheet.Cells[rowCnt, 8] = sTenCotH;
                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 9] = sTenCotI;
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 10] = sTenCotJ;
                oSheet.Cells[rowCnt, 10].Font.Bold = true;
                oSheet.Cells[rowCnt, 10].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 11] = sTenCotK;
                oSheet.Cells[rowCnt, 11].Font.Bold = true;
                oSheet.Cells[rowCnt, 11].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 12] = sTenCotL;
                oSheet.Cells[rowCnt, 12].Font.Bold = true;
                oSheet.Cells[rowCnt, 12].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 13] = sTenCotM;
                oSheet.Cells[rowCnt, 13].Font.Bold = true;

                oSheet.Cells[rowCnt, 14] = "=IFERROR(L" + rowCnt + "/G" + rowCnt + ",0)";
                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14].Font.Color = Color.FromArgb(255, 0, 0);


                Microsoft.Office.Interop.Excel.Range formatRange4;
                formatRange4 = oSheet.get_Range("D6", "G" + (rowCnt - 1).ToString());
                formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);

                Microsoft.Office.Interop.Excel.Range formatRange5;
                formatRange5 = oSheet.get_Range("H6", "L" + (rowCnt - 1).ToString());
                formatRange5.Interior.Color = Color.FromArgb(255, 230, 153);

                Microsoft.Office.Interop.Excel.Range formatRange6;
                formatRange6 = oSheet.get_Range("M6", "M" + (rowCnt - 1).ToString());
                formatRange6.Interior.Color = Color.FromArgb(189, 215, 238);

                Microsoft.Office.Interop.Excel.Range formatRange7;
                formatRange7 = oSheet.get_Range("N6", "N" + (rowCnt - 1).ToString());
                formatRange7.Interior.Color = Color.FromArgb(255, 255, 0);

                Microsoft.Office.Interop.Excel.Range formatRange8;
                sRowXN = sRowXN.Substring(0, sRowXN.Length - 1);
                string[] strGetRowXN = sRowXN.Split(',');
                for (int i = 0; i < slXN; i++)
                {
                    formatRange8 = oSheet.get_Range("B" + strGetRowXN[i] + "", lastColumn + "" + strGetRowXN[i] + "");
                    formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Format All
                Microsoft.Office.Interop.Excel.Range formatRange9;
                formatRange9 = oSheet.get_Range("B7", lastColumn + (rowCnt));
                formatRange9.Font.Size = fontSizeNoiDung;
                formatRange9.Font.Name = fontName;
                formatRange9.WrapText = true;

                formatRange9 = oSheet.get_Range("D7", lastColumn + (rowCnt));
                formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //formatRange9.NumberFormat = "0";
                //try { formatRange9.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }

                int colBD = 3;
                for (col = colBD; col < dtBCThang.Columns.Count - 2; col++) // không format cột tỷ lệ
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange9 = oSheet.get_Range("N7", lastColumn + (rowCnt));
                formatRange9.NumberFormat = @"0%";
                try { formatRange9.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //Fortmart cột L
                formatRange9 = oSheet.get_Range("L7", "L" + (rowCnt - 1));
                formatRange9.Font.Bold = true;
                formatRange9.Font.Color = Color.FromArgb(255, 0, 0);

                //Fortmart cột M
                formatRange9 = oSheet.get_Range("M7", "M" + (rowCnt - 1));
                formatRange9.Font.Bold = true;

                //Fortmart cột N
                formatRange9 = oSheet.get_Range("N7", "N" + (rowCnt - 1));
                formatRange9.Font.Bold = true;
                formatRange9.Font.Color = Color.FromArgb(255, 0, 0);

                formatRange9 = oSheet.get_Range("O7", "O" + (rowCnt - 1));

                //var list = new System.Collections.Generic.List<string>();
                //list.Add("Charlie");
                //list.Add("Delta");
                //list.Add("Echo");
                //var flatList = string.Join(",", list.ToArray());

                //formatRange9.Validation.Delete();
                //formatRange9.Validation.Add(
                //   XlDVType.xlValidateList,
                //   XlDVAlertStyle.xlValidAlertInformation,
                //   XlFormatConditionOperator.xlBetween,
                //   flatList,
                //   Type.Missing);

                //formatRange9.Validation.IgnoreBlank = true;
                //formatRange9.Validation.InCellDropdown = true;


                BorderAround(oSheet.get_Range("B2", lastColumn + rowCnt.ToString()));

                #endregion
                //////////////////////////////////////////////////////////////////////////////// Giai đoạn /////////////////////////////////////////////////////

                #region Theo GiaiDoan
                try
                {

                    DateTime DenNgay = dNgayDL;
                    DateTime TuNgay = dNgayDL.AddDays(-5);
                    int soNgayNghi = 0;
                    try
                    {
                        soNgayNghi = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayTruLeNgayNghiMacDinh('" + TuNgay.ToString("yyyyMMdd") + "', '" + DenNgay.ToString("yyyyMMdd") + "')"));
                    }
                    catch { }
                    if (soNgayNghi < 5)
                    {
                        TuNgay = TuNgay.AddDays(-(5 - soNgayNghi));
                    }

                    DateTime TuNgayTemp = TuNgay;
                    DateTime DenNgayTemp = DenNgay;

                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNgayGiaiDoan_DM", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = ID_DV;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = TuNgay;
                    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dNgayDL;
                    cmd.CommandType = CommandType.StoredProcedure;
                    adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    ds = new DataSet();
                    adp.Fill(ds);
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();

                    dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                    dtSLXN = ds.Tables[1].Copy();
                    slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "Tổng hợp";



                    fontName = "Times New Roman";
                    fontSizeTieuDe = 12;
                    fontSizeNoiDung = 12;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    row1_TieuDe = oSheet.get_Range("B1");
                    row1_TieuDe.Font.Bold = true;
                    row1_TieuDe.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";
                    row1_TieuDe.WrapText = false;
                    row1_TieuDe.Font.Size = 12;
                    row1_TieuDe.Font.Name = fontName;
                    row1_TieuDe.RowHeight = 21;
                    row1_TieuDe.ColumnWidth = 43;



                    row2_TieuDe = oSheet.get_Range("B2", "C2");
                    row2_TieuDe.Font.Bold = true;
                    row2_TieuDe.Merge();
                    row2_TieuDe.Font.Size = 12;
                    row2_TieuDe.Font.Name = fontName;
                    row2_TieuDe.Value2 = "BÁO CÁO HÀNG NGÀY/ DAILY ATTENDANCE REPORT";
                    row2_TieuDe.WrapText = false;
                    row2_TieuDe.RowHeight = 33;
                    row2_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                    row3_Date = oSheet.get_Range("L3", "N3");
                    row3_Date.Font.Bold = true;
                    row3_Date.Merge();
                    row3_Date.Font.Size = 12;
                    row3_Date.Font.Name = fontName;
                    row3_Date.Value2 = "Ngày/ Date:" + Convert.ToDateTime(dNgayIn.EditValue).Day + "-" + (Convert.ToDateTime(dNgayIn.EditValue).Month.ToString().Length == 1 ? "0" + Convert.ToDateTime(dNgayIn.EditValue).Month.ToString() : Convert.ToDateTime(dNgayIn.EditValue).Month.ToString()) + "-" + Convert.ToDateTime(dNgayIn.EditValue).Year + "";
                    row3_Date.WrapText = false;
                    row3_Date.RowHeight = 24;
                    row3_Date.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row3_Date.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row4 = oSheet.get_Range("B4");
                    row4.RowHeight = 66;

                    row5 = oSheet.get_Range("B5");
                    row5.RowHeight = 79;

                    //Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                    row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                    row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                    row5_TieuDe_Format.Font.Name = fontName;
                    row5_TieuDe_Format.Font.Bold = true;
                    row5_TieuDe_Format.WrapText = true;
                    row5_TieuDe_Format.NumberFormat = "@";
                    row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A1");
                    row1_TieuDe_Stt.ColumnWidth = 2;

                    row5_TieuDe_Stt = oSheet.get_Range("B5", "B6");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                    row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    row6_TieuDe_Stt = oSheet.get_Range("C5", "C6");
                    row6_TieuDe_Stt.Merge();
                    row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                    row6_TieuDe_Stt.ColumnWidth = 30;
                    row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    int col_td = 4;
                    Range row4_1;
                    row4_1 = oSheet.get_Range("A4");
                    row4_1.RowHeight = 25;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            oSheet.Cells[4, col_td] = TuNgayTemp.ToString("dd/MM/yyyy");
                            oSheet.Range[oSheet.Cells[4, Convert.ToInt32(col_td)], oSheet.Cells[4, Convert.ToInt32(col_td + 4)]].Merge();
                            // cột tổng lao động
                            oSheet.Cells[5, col_td] = "Tổng lao động / Total employees";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td)], oSheet.Cells[6, Convert.ToInt32(col_td)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td)], oSheet.Cells[6, Convert.ToInt32(col_td)]].Interior.Color = Color.FromArgb(255, 255, 0);


                            //cột số lao động vắng mặt
                            oSheet.Cells[5, col_td + 1] = "Số lao động vắng mặt";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 1)], oSheet.Cells[6, Convert.ToInt32(col_td + 1)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 1)], oSheet.Cells[6, Convert.ToInt32(col_td + 1)]].Interior.Color = Color.FromArgb(255, 230, 153);

                            //cột số lao động vắng thai sản
                            oSheet.Cells[5, col_td + 2] = "Số lao động vắng thai sản";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 2)], oSheet.Cells[6, Convert.ToInt32(col_td + 2)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 2)], oSheet.Cells[6, Convert.ToInt32(col_td + 2)]].Interior.Color = Color.FromArgb(255, 230, 153);

                            //cột Số lao động có mặt
                            oSheet.Cells[5, col_td + 3] = "Số lao động có mặt/ Total employees present";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 3)], oSheet.Cells[6, Convert.ToInt32(col_td + 3)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 3)], oSheet.Cells[6, Convert.ToInt32(col_td + 3)]].Interior.Color = Color.FromArgb(189, 215, 238);


                            //cột Tỷ lệ vắng (%)
                            oSheet.Cells[5, col_td + 4] = "Tỷ lệ vắng (%)";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 4)], oSheet.Cells[6, Convert.ToInt32(col_td + 4)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 4)], oSheet.Cells[6, Convert.ToInt32(col_td + 4)]].Interior.Color = Color.FromArgb(255, 255, 0);



                            //cột Tỷ lệ có mặt/ tổng số (%)
                            oSheet.Cells[5, col_td + 5] = "Tỷ lệ có mặt/ tổng số (%)";
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 5)], oSheet.Cells[6, Convert.ToInt32(col_td + 5)]].Merge();
                            oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 5)], oSheet.Cells[6, Convert.ToInt32(col_td + 5)]].Interior.Color = Color.FromArgb(255, 255, 0);

                            col_td = col_td + 6;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }



                    oSheet.Application.ActiveWindow.SplitColumn = 3;
                    oSheet.Application.ActiveWindow.SplitRow = 6;
                    oSheet.Application.ActiveWindow.FreezePanes = true;


                    col = 1;
                    rowCnt = 0;
                    keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                    dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    rowBD_XN = 0; // Row để insert dòng xí nghiệp
                                  //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                    rowBD = 7;
                    cotCN_A = "";
                    cotCN_B = "";
                    TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                    sRowXN = "";
                    s = int_to_Roman(9);
                    Range formatRange11;
                    rowSum = 8; //Row sum của cột G 
                    for (int i = 0; i < TEN_XN.Count(); i++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[i]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count; col++)
                            {
                                //sTenCot = CharacterIncrement(6);
                                //Excell.Range formatRange7;
                                //formatRange7 = oSheet.get_Range(sTenCot + ((rowCnt + 1) + 7).ToString());
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


                        // Tạo group xí nghiệp
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("B" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                        oSheet.Cells[rowBD, 2] = TEN_XN[i].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;


                        oSheet.Cells[rowBD, 3] = "Sub-Total " + int_to_Roman(i + 1) + "";
                        oSheet.Cells[rowBD, 3].Font.Bold = true;
                        oSheet.Cells[rowBD, 3].Font.Size = fontSizeNoiDung;
                        oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        oSheet.Cells[rowBD, 3].Font.Name = fontName;

                        sRowXN = sRowXN + rowBD + ",";

                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                        col_td = 4;
                        TuNgayTemp = TuNgay;
                        //Set công thức từng row
                        while (TuNgayTemp <= DenNgayTemp)
                        {
                            if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                            {
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                            else
                            {
                                // cột tổng lao động
                                oSheet.Cells[rowBD, col_td] = "=SUM(" + CharacterIncrement(col_td - 1) + "" + (rowBD + 1) + ":" + CharacterIncrement(col_td - 1) + "" + (rowCnt + 1) + ")";

                                //cột số lao động vắng mặt
                                oSheet.Cells[rowBD, col_td + 1] = "=SUM(" + CharacterIncrement(col_td) + "" + (rowBD + 1) + ":" + CharacterIncrement(col_td) + "" + (rowCnt + 1) + ")";

                                //cột số lao động vắng thai sản
                                oSheet.Cells[rowBD, col_td + 2] = "=SUM(" + CharacterIncrement(col_td + 1) + "" + (rowBD + 1) + ":" + CharacterIncrement(col_td + 1) + "" + (rowCnt + 1) + ")";

                                //cột Số lao động có mặt
                                formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 2) + "" + rowBD + "", "" + CharacterIncrement(col_td + 2) + "" + (rowCnt + 1) + "");
                                //oSheet.Cells[rowBD, col_td + 2] = "=" + CharacterIncrement(col_td - 1) + ""+rowBD+"-"+ CharacterIncrement(col_td) + ""+rowBD+"";
                                formatRange11.Value = "=" + CharacterIncrement(col_td - 1) + "" + rowBD + "-" + CharacterIncrement(col_td) + "" + rowBD + " - " + CharacterIncrement(col_td + 1) + "" + rowBD + "";

                                //cột Tỷ lệ vắng (%)
                                formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 3) + "" + rowBD + "", "" + CharacterIncrement(col_td + 3) + "" + (rowCnt + 1) + "");
                                formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td) + "" + rowBD.ToString() + "/" + CharacterIncrement(col_td - 1) + "" + rowBD.ToString() + ",0)";


                                //cột Tỷ lệ có mặt/ tổng số (%)
                                formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 4) + "" + rowBD + "", "" + CharacterIncrement(col_td + 4) + "" + (rowCnt + 1) + "");
                                formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td + 2) + "" + rowBD + "/" + CharacterIncrement(col_td - 1) + "" + rowBD + ",0)";

                                col_td = col_td + 6;
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                        }
                        // Fortmat từ cột đầu tới cột cuối của từng Xí nghiệp
                        Range formatRange10;
                        formatRange10 = oSheet.get_Range("D" + (rowBD) + "", lastColumn + (rowBD));
                        formatRange10.Font.Color = Color.FromArgb(255, 0, 0);
                        formatRange10.Font.Bold = true;
                        formatRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange10.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowSum = rowCnt + 3;
                        rowCnt = 0;
                    }
                    rowCnt = keepRowCnt;
                    rowDup = 0; // row bat dau của dữ liệu duplicate
                    bChan = false;
                    for (rowbd = 8; rowbd <= rowCnt; rowbd++)
                    {
                        formatRange = oSheet.get_Range("B" + rowbd + "");
                        formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                        if (formatRange.Value == null)
                        {
                            formatRange = oSheet.get_Range("B" + (rowDup).ToString() + "");
                        }
                        if (formatRange.Value == formatRange1.Value)
                        {
                            if (bChan == false)
                            {
                                rowDup = rowbd;
                            }
                            bChan = true;
                            formatRange.Value = null;
                            formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                            formatRange3.Merge();
                        }
                        else
                        {
                            bChan = false;
                            rowDup = 0;
                        }
                        //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    }

                    rowCnt++;
                    rowCnt++;

                    rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                    rowTONG_CONG.Value2 = "Tổng/Grand Total";
                    rowTONG_CONG.Font.Bold = true;

                    rowTONG_CONG1 = oSheet.get_Range("C" + rowCnt);
                    sLama = "(";
                    for (int i = 1; i <= slXN; i++)
                    {
                        sLama = sLama + int_to_Roman(i) + "+";
                    }
                    rowTONG_CONG1.Value2 = sLama.Substring(0, sLama.Length - 1) + ")";
                    rowTONG_CONG1.Font.Bold = true;
                    rowTONG_CONG1.Font.Size = fontSizeNoiDung;
                    rowTONG_CONG1.Font.Name = fontName;

                    Range rowSumAll = oSheet.get_Range("B" + rowCnt + "", "C" + rowCnt);
                    rowSumAll.Font.Bold = true;
                    rowSumAll.Interior.Color = Color.FromArgb(189, 215, 238);

                    rowSumAll = oSheet.get_Range("D" + rowCnt + "", lastColumn + rowCnt);
                    rowSumAll.Font.Bold = true;
                    rowSumAll.Font.Color = Color.FromArgb(255, 0, 0);
                    rowSumAll.Interior.Color = Color.FromArgb(255, 255, 0);
                    rowSumAll.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowSumAll.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "7" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                            formatRange4.NumberFormat = "0"; // format từng cột
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, 7, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")/2"; // sUM TỪNNG CỘT

                            //cột số lao động vắng mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "7" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, 7, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")/2";

                            //cột số lao động vắng thai sản
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "7" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 2] = "=SUM(" + CellAddress(oSheet, 7, col_td + 2) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 2) + ")/2";


                            //cột Số lao động có mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 2) + "7" + "", CharacterIncrement(col_td + 2) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 3] = "=SUM(" + CellAddress(oSheet, 7, col_td + 3) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 3) + ")/2";


                            //cột Tỷ lệ vắng (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 3) + "7" + "", CharacterIncrement(col_td + 3) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 4] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                            //cột Tỷ lệ có mặt/ tổng số (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 4) + "7" + "", CharacterIncrement(col_td + 4) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 5] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 3) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                            col_td = col_td + 6;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }

                    sRowXN = sRowXN.Substring(0, sRowXN.Length - 1);
                    strGetRowXN = sRowXN.Split(',');
                    for (int i = 0; i < slXN; i++)
                    {
                        formatRange8 = oSheet.get_Range("B" + strGetRowXN[i] + "", lastColumn + "" + strGetRowXN[i] + "");
                        formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                    }

                    //Format All
                    formatRange9 = oSheet.get_Range("B8", "C" + (rowCnt));
                    formatRange9.Font.Size = fontSizeNoiDung;
                    formatRange9.Font.Name = fontName;
                    formatRange9.WrapText = true;

                    formatRange9 = oSheet.get_Range("D7", lastColumn + (rowCnt));
                    formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    formatRange9.Font.Bold = true;
                    formatRange9.Font.Size = fontSizeNoiDung;
                    formatRange9.Font.Name = fontName;

                    BorderAround(oSheet.get_Range("B2", "C3"));
                    BorderAround(oSheet.get_Range("B4", lastColumn + rowCnt.ToString()));

                    ////////////////////////////////////////////////////////////////////////////////// TABLE 2  //////////////////////////////////////////////////////////////////////////////////
                    #region table 2

                    rowCnt = rowCnt + 5; // Dòng phòng ban
                    int rowCnt2 = rowCnt - 1; // dòng ngày
                    row5_TieuDe_Stt = oSheet.get_Range("B" + rowCnt + "", "B" + (rowCnt + 1).ToString() + "");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                    row5_TieuDe_Stt.Font.Name = fontName;
                    row5_TieuDe_Stt.Font.Size = fontSizeNoiDung;
                    row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    row6_TieuDe_Stt = oSheet.get_Range("C" + rowCnt + "", "C" + (rowCnt + 1).ToString() + "");
                    row6_TieuDe_Stt.Merge();
                    row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                    row6_TieuDe_Stt.ColumnWidth = 30;
                    row6_TieuDe_Stt.Font.Name = fontName;
                    row6_TieuDe_Stt.Font.Size = fontSizeNoiDung;
                    row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    col_td = 4;
                    row4_1 = oSheet.get_Range("A" + rowCnt2 + "");
                    row4_1.RowHeight = 25;

                    row4_1 = oSheet.get_Range("A" + rowCnt + "");
                    row4_1.RowHeight = 79;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            oSheet.Cells[rowCnt - 1, col_td] = TuNgayTemp;
                            oSheet.Range[oSheet.Cells[rowCnt - 1, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt - 1, Convert.ToInt32(col_td + 2)]].Merge();
                            oSheet.Cells[rowCnt, col_td] = "Tổng lao động / Total employees";
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td)]].Merge();
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td)]].Interior.Color = Color.FromArgb(255, 255, 0);


                            oSheet.Cells[rowCnt, col_td + 1] = "Số lao động có mặt/ Total employees present";
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 1)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 1)]].Merge();
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 1)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 1)]].Interior.Color = Color.FromArgb(255, 230, 153);


                            oSheet.Cells[rowCnt, col_td + 2] = "Tỷ lệ có mặt/ tổng số (%)";
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 2)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 2)]].Merge();
                            oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 2)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 2)]].Interior.Color = Color.FromArgb(189, 215, 238);

                            col_td = col_td + 3;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }

                    int rowCnt1 = rowCnt + 2; // dòng dữ liệu
                    keepRowCnt = rowCnt + 2;// Biến này dùng để lưu lại giá trị của biến rowCnt
                    col = 1;
                    dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    rowBD_XN = 0; // Row để insert dòng xí nghiệp
                    rowBD = rowCnt + 2;
                    rowCnt = 0;
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[2].Copy();
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                    string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                    chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    dt_temp = new DataTable();
                    dt_temp = ds.Tables[2].Copy(); // Dữ row count data

                    sRowXN = "";
                    s = int_to_Roman(9);
                    rowSum = 8; //Row sum của cột G 
                    for (int i = 0; i < TEN_TO.Count(); i++)
                    {
                        dtBCThang = ds.Tables[2].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[i]).CopyToDataTable().Copy();
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

                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("B" + (rowBD) + "", lastColumn + (rowCnt).ToString()).Value2 = rowData;

                        col_td = 4;
                        TuNgayTemp = TuNgay;
                        //Set công thức từng row
                        while (TuNgayTemp <= DenNgayTemp)
                        {
                            if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                            {
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                            else
                            {
                                //cột Tỷ lệ vắng (%)
                                formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 1) + "" + rowBD + "", "" + CharacterIncrement(col_td + 1) + "" + (rowCnt + 1) + "");
                                formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td) + "" + rowBD.ToString() + "/" + CharacterIncrement(col_td - 1) + "" + rowBD.ToString() + ",0)";
                                formatRange11.NumberFormat = @"0%";
                                try { formatRange11.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                                col_td = col_td + 3;
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }

                        }
                        col_td = 4;
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowSum = rowCnt + 3;
                        rowCnt = 0;
                    }
                    rowCnt = keepRowCnt;
                    rowDup = 0; // row bat dau của dữ liệu duplicate
                    bChan = false;
                    for (rowbd = rowCnt1; rowbd <= rowCnt; rowbd++)
                    {
                        formatRange = oSheet.get_Range("B" + rowbd + "");
                        formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                        if (formatRange.Value == null)
                        {
                            formatRange = oSheet.get_Range("B" + (rowDup).ToString() + "");
                        }
                        if (formatRange.Value == formatRange1.Value)
                        {
                            if (bChan == false)
                            {
                                rowDup = rowbd;
                            }
                            bChan = true;
                            formatRange.Value = null;
                            formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                            formatRange3.Merge();
                        }
                        else
                        {
                            bChan = false;
                            rowDup = 0;
                        }
                        //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    }


                    rowCnt++;

                    formatRange9 = oSheet.get_Range("B" + rowCnt + "");
                    formatRange9.Value = "Tổng/Grand Total";

                    formatRange9 = oSheet.get_Range("D" + rowCnt + "", lastColumn + rowCnt);
                    formatRange9.Font.Bold = true;
                    formatRange9.Font.Name = fontName;
                    formatRange9.Font.Size = fontSizeNoiDung;
                    formatRange9.Font.Color = Color.FromArgb(255, 0, 0);
                    formatRange9.Interior.Color = Color.FromArgb(189, 215, 238);
                    formatRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    // SUM
                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")"; // sUM TỪNNG CỘT

                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")";

                            //cột Tỷ lệ vắng (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td + 2] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                            col_td = col_td + 3;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }

                    }

                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                            formatRange4.NumberFormat = "0"; // format từng cột
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            //cột số lao động vắng mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            col_td = col_td + 3;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }
                    formatRange9 = oSheet.get_Range("D" + rowCnt2 + "", lastColumn + rowCnt);
                    formatRange9.Font.Bold = true;
                    formatRange9.WrapText = true;
                    formatRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange9 = oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString());
                    formatRange9.Font.Name = fontName;
                    formatRange9.Font.Size = fontSizeNoiDung;

                    BorderAround(oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString()));

                    #endregion

                    #endregion


                    /////////////////////////////////////////////////////// DANH SÁCH NGHỈ VIỆC ///////////////////////////////////////////////////////

                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachNghiViec", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = ID_DV;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = ID_XN;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = ID_TO;
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dNgayDL;
                    cmd.CommandType = CommandType.StoredProcedure;
                    adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    ds = new DataSet();
                    adp.Fill(ds);
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "DS nghỉ việc";

                    fontName = "Times New Roman";
                    fontSizeTieuDe = 10;
                    fontSizeNoiDung = 9;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    Range TieuDe = oSheet.get_Range("A1", "F1");
                    TieuDe.Merge();
                    TieuDe.Font.Size = 12;
                    TieuDe.Font.Name = fontName;
                    TieuDe.Font.Bold = true;
                    TieuDe.Value2 = "DANH SÁCH CÔNG NHÂN NGHỈ VIỆC";
                    TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A3");
                    row1_TieuDe_Stt.Value2 = "STT";
                    row1_TieuDe_Stt.ColumnWidth = 8;

                    row5_TieuDe_Stt = oSheet.get_Range("B3");
                    row5_TieuDe_Stt.Value2 = "Mã thẻ";

                    row6_TieuDe_Stt = oSheet.get_Range("C3");
                    row6_TieuDe_Stt.Value2 = "Họ tên";


                    row5_TieuDe_MaSo = oSheet.get_Range("D3");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "Bộ phận";


                    row6_TieuDe_MaSo = oSheet.get_Range("E3");
                    row6_TieuDe_MaSo.Merge();
                    row6_TieuDe_MaSo.Value2 = "Chuyền/Phòng";

                    row5_TieuDe_HoTen = oSheet.get_Range("F3");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Lý do nghỉ việc";

                    row1_TieuDe_Stt = oSheet.get_Range("A3", "F3");
                    row1_TieuDe_Stt.Font.Bold = true;
                    row1_TieuDe_Stt.Font.Name = fontName;
                    row1_TieuDe_Stt.Font.Size = 11;
                    row1_TieuDe_Stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row1_TieuDe_Stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    DataRow[] dr1 = dtBCThang.Select();
                    string[,] rowData1 = new string[dr1.Count(), dtBCThang.Columns.Count];

                    rowCnt = 0;
                    foreach (DataRow row in dr1)
                    {
                        for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData1[rowCnt, col_bd] = row[col_bd].ToString();
                        }
                        rowCnt++;
                    }
                    rowCnt = rowCnt + 3;
                    oSheet.get_Range("A4", lastColumn + rowCnt.ToString()).Value2 = rowData1;

                    formatRange9 = oSheet.get_Range("A4", lastColumn + rowCnt);
                    formatRange9 = oSheet.get_Range("D4", "E" + rowCnt.ToString());
                    formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    BorderAround(oSheet.get_Range("A3", lastColumn + rowCnt.ToString()));
                    formatRange9 = oSheet.get_Range("A3", lastColumn + rowCnt);
                    formatRange9.Columns.AutoFit();

                    #region vắng ngày
                    //////////////////////////////////////////// DANH SÁCH VẮNG NGÀY /////////////////////////////////////////
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[1].Copy();

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "DS vắng";

                    fontName = "Times New Roman";
                    fontSizeTieuDe = 10;
                    fontSizeNoiDung = 9;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    TieuDe = oSheet.get_Range("A1", "G2");
                    TieuDe.Merge();
                    TieuDe.Font.Size = 12;
                    TieuDe.Font.Name = fontName;
                    TieuDe.Font.Bold = true;
                    TieuDe.Value2 = "DANH SÁCH CÔNG NHÂN VIÊN VẮNG";
                    TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A4");
                    row1_TieuDe_Stt.Value2 = "STT";
                    row1_TieuDe_Stt.ColumnWidth = 8;

                    row5_TieuDe_Stt = oSheet.get_Range("B4");
                    row5_TieuDe_Stt.Value2 = "Ngày";

                    row6_TieuDe_Stt = oSheet.get_Range("C4");
                    row6_TieuDe_Stt.Value2 = "Thứ";


                    row5_TieuDe_MaSo = oSheet.get_Range("D4");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "Mã nhân viên";


                    row6_TieuDe_MaSo = oSheet.get_Range("E4");
                    row6_TieuDe_MaSo.Merge();
                    row6_TieuDe_MaSo.Value2 = "Họ tên";

                    row5_TieuDe_HoTen = oSheet.get_Range("F4");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Chuyền/Phòng";

                    row5_TieuDe_HoTen = oSheet.get_Range("G4");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Ghi chú";

                    row1_TieuDe_Stt = oSheet.get_Range("A4", "G4");
                    row1_TieuDe_Stt.Font.Bold = true;
                    row1_TieuDe_Stt.Font.Name = fontName;
                    row1_TieuDe_Stt.Font.Size = 11;
                    row1_TieuDe_Stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row1_TieuDe_Stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    dr1 = dtBCThang.Select();
                    rowData1 = new string[dr1.Count(), dtBCThang.Columns.Count];

                    rowCnt = 0;
                    foreach (DataRow row in dr1)
                    {
                        for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData1[rowCnt, col_bd] = row[col_bd].ToString();
                        }
                        rowCnt++;
                    }
                    rowCnt = rowCnt + 3;
                    oSheet.get_Range("A5", lastColumn + rowCnt.ToString()).Value2 = rowData1;

                    formatRange9 = oSheet.get_Range("B5", "B" + rowCnt);
                    formatRange9.NumberFormat = "dd/MM/yyyy";
                    try { formatRange9.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    formatRange9 = oSheet.get_Range("A5", lastColumn + rowCnt);
                    formatRange9.Font.Name = fontName;
                    formatRange9.Font.Size = 11;

                    BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));
                    formatRange9 = oSheet.get_Range("A4", lastColumn + rowCnt);
                    formatRange9.Columns.AutoFit();

                    #endregion
                }
                catch (Exception ex)
                {
                }


                splashScreenManager1.CloseWaitForm();
                oWB.Sheets[1].Activate();

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.Message);
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
        public static string int_to_Roman(int n)
        {
            string[] roman_symbol = { "MMM", "MM", "M", "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C", "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };
            int[] int_value = { 3000, 2000, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            var roman_numerals = new System.Text.StringBuilder();
            var index_num = 0;
            while (n != 0)
            {
                if (n >= int_value[index_num])
                {
                    n -= int_value[index_num];
                    roman_numerals.Append(roman_symbol[index_num]);
                }
                else
                {
                    index_num++;
                }
            }

            return roman_numerals.ToString();
        }
        private string CellAddress(Microsoft.Office.Interop.Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        #endregion

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                {
                    case "rdo_DSNhanVienVachTheLoi":
                        {
                            tablePanel1.Rows[2].Visible = true;
                            break;
                        }
                    default:
                        {
                            tablePanel1.Rows[2].Visible = false;
                            break;
                        }
                }
            }
            catch { }
        }
    }
}