using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoHopDong()
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
                        DataTable dt;
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    string sTieuDe = "DANH SÁCH CÔNG NHÂN HỢP ĐỒNG";
                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe, lk_NgayIn.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongCongNhan", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.DateTime;
                                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    System.Data.SqlClient.SqlConnection conn1;
                                    dt = new DataTable();
                                    string sTieuDe1 = "DANH SÁCH CÔNG NHÂN HẾT HẠN HỢP ĐỒNG";
                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe1, lk_NgayIn.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongHetHan", conn1);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.DateTime;
                                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 2:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {

                                        case "SB":
                                            {
                                                BaoCaoHopDongGiaiDoan_SB();
                                                break;
                                            }
                                        default:
                                            System.Data.SqlClient.SqlConnection conn2;
                                            dt = new DataTable();
                                            string sTieuDe2 = "DANH SÁCH CÔNG NHÂN KÝ HỢP ĐỒNG";
                                            frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                            try
                                            {
                                                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn2.Open();

                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan", conn2);

                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                                                cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                dt = new DataTable();
                                                dt = ds.Tables[0].Copy();
                                                dt.TableName = "DA_TA";
                                                frm.AddDataSource(dt);
                                                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                            }
                                            catch
                                            { }
                                            frm.ShowDialog();
                                            break;
                                    }
                                }
                                break;

                            //Tai ký hợp đồng
                            case 3:
                                {
                                    System.Data.SqlClient.SqlConnection conn2;
                                    dt = new DataTable();
                                    string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES WHO ARE DUE TO RENEW THEIR LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN TÁI KÝ HĐLĐ THÁNG ";
                                    frm.rpt = new rptBCTaiKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanTaiKyHopDong", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }
                                    frm.ShowDialog();
                                }
                                break;

                            // Tới hạn ký hợp đồng
                            case 4:
                                {
                                    System.Data.SqlClient.SqlConnection conn2;
                                    dt = new DataTable();
                                    string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES DUE TO SIGN LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN KÝ HĐLĐ THÁNG ";
                                    frm.rpt = new rptBCToiHanKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanKyHopDong", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }
                                    frm.ShowDialog();
                                }
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoHopDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            LoadLoaiHopDong();

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);

            lk_NgayIn.EditValue = DateTime.Today;
            lk_NgayTinh.EditValue = DateTime.Today;
            DateTime dtTN = DateTime.Today;
            DateTime dtDN = DateTime.Today;
            dTuNgay.EditValue = dtTN.AddDays((-dtTN.Day) + 1);
            dtDN = dtDN.AddMonths(1);
            dtDN = dtDN.AddDays(-(dtDN.Day));
            dDenNgay.EditValue = dtDN;
            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_DON_VI.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
                }
                else
                {
                    LK_DON_VI.Properties.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", LK_DON_VI.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_XI_NGHIEP.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
                }
                else
                {
                    LK_XI_NGHIEP.Properties.DataSource = dt;
                }
                LK_XI_NGHIEP.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_TO.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                }
                else
                {
                    LK_TO.Properties.DataSource = dt;
                }
                LK_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadLoaiHopDong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHopDongLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(LK_LOAI_HD, dt, "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD");
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 0:
                        {
                            lk_NgayTinh.Enabled = true;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            lk_NgayTinh.Enabled = true;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                        }
                        break;
                    case 2:
                        {
                            lk_NgayTinh.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;
                    case 3:
                        {
                            lk_NgayTinh.Enabled = false;
                            LK_LOAI_HD.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;
                    case 4:
                        {
                            lk_NgayTinh.Enabled = false;
                            LK_LOAI_HD.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;

                    default:
                        lk_NgayTinh.Enabled = true;
                        dTuNgay.Enabled = true;
                        dDenNgay.Enabled = true;
                        break;
                }
            }
            catch
            { }
        }

        private void BaoCaoHopDongGiaiDoan_SB()
        {
            //string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            //DateTime tungay = Convert.ToDateTime(datetime);
            //try { datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue); } catch { }
            //DateTime denngay = Convert.ToDateTime(datetime);
            //int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            try
            {

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }

                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                oXL.Visible = false;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                Range row1_tendv = oSheet.get_Range("A1");
                row1_tendv.Value = "SB SAIGON FASHION LTD.,CO";
                row1_tendv.Font.Bold = true;
                row1_tendv.Font.Name = fontName;
                row1_tendv.Font.Size = 9;
                row1_tendv.Font.Color = Color.FromArgb(0, 0, 255);

                Range row2_tendc = oSheet.get_Range("A2");
                row2_tendc.Value = "Tan Thuan EPZ, Dist 7, HCMC";
                row2_tendc.Font.Bold = true;
                row2_tendc.Font.Name = fontName;
                row2_tendc.Font.Size = 9;
                row2_tendc.Font.Color = Color.FromArgb(0, 0, 255);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCPhep.Columns.Count - 2);
                Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A4", lastColumn + "4");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = 14;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.RowHeight = 33;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "DANH SÁCH LAO ĐỘNG VIỆT NAM KÝ HỢP ĐỒNG LAO ĐỘNG THÁNG " + Convert.ToDateTime(dTuNgay.EditValue).ToString("MM/yyyy");
                row2_TieuDe_BaoCao0.Font.Color = Color.FromArgb(0, 0, 255);

                Range row4_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "5");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Font.Color = Color.Red;
                row4_TieuDe_Format.WrapText = true;

                Excel.Range row4_A = oSheet.get_Range("A5");
                row4_A.ColumnWidth = 5;
                row4_A.RowHeight = 45;
                row4_A.Value2 = "STT";



                Range row4_C = oSheet.get_Range("B5");
                row4_C.ColumnWidth = 25;
                row4_C.Value2 = "Họ và tên";

                Range row4_B = oSheet.get_Range("C5");
                row4_B.ColumnWidth = 10;
                row4_B.Value2 = "Mã số";

                Range row4_D = oSheet.get_Range("D5");
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "Chuyền Tổ";

                Range row4_E = oSheet.get_Range("E5");
                row4_E.ColumnWidth = 15;
                row4_E.Value2 = "Số hợp đồng";

                Range row4_H4 = oSheet.get_Range("F5");
                row4_H4.ColumnWidth = 15;
                row4_H4.Value2 = "Bậc lương";

                Range row4_I4 = oSheet.get_Range("G5");
                row4_I4.ColumnWidth = 15;
                row4_I4.Value2 = "Tiền lương";

                Range row4_J4 = oSheet.get_Range("H5");
                row4_J4.ColumnWidth = 25;
                row4_J4.Value2 = "Ngày hợp đồng";

                Range row4_K4 = oSheet.get_Range("I5");
                row4_K4.Value2 = "Lần Ký HĐ";
                row4_K4.ColumnWidth = 10;

                Range row4_NS = oSheet.get_Range("J5");
                row4_NS.Value2 = "Ngày sinh";
                row4_NS.ColumnWidth = 15;

                Range row4_NgayTV = oSheet.get_Range("K5");
                row4_NgayTV.Value2 = "Ngày thử việc";
                row4_NgayTV.ColumnWidth = 10;

                Range row4_GC = oSheet.get_Range("L5");
                row4_GC.Value2 = "Ghi chú";
                row4_GC.ColumnWidth = 15;


                DataRow[] dr = dtBCPhep.Select();
                string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];

                int col = 0;
                int rowCnt = 0;
                int rowCntY = 6; //Dùng để tính tổng cột Y
                Excel.Range formatRange1;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCPhep.Columns.Count - 2; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    //formatRange1 = oSheet.get_Range("Y" + rowCntY.ToString());
                    //formatRange1.Value2 = "X"+ rowCntY + "-W"+ rowCntY + "";
                    //oSheet.get_Range("Y"+ rowCntY + "").Value2 = "=X"+ rowCntY + " - W"+ rowCntY + "";
                    //rowCntY++;
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;

                Microsoft.Office.Interop.Excel.Range formatRange;
                //int rowCnt = 0;
                //int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                //int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                //int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                //int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowCONG = 0; // Row để insert dòng tổng
                ////int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                //string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                //string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                //int rowBD = 6;
                //string[] TEN_TO = dtBCPhep.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                //string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                //DataTable dt_temp = new DataTable();
                //dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                //string sRowBD_XN_Temp = "";
                //for (int j = 0; j < TEN_TO.Count(); j++)
                //{
                //    dtBCPhep = ds.Tables[0].Copy();
                //    dtBCPhep = dtBCPhep.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                //    DataRow[] dr = dtBCPhep.Select();
                //    current_dr = dr.Count();
                //    string[,] rowData = new string[dr.Count(), dtBCPhep.Columns.Count];
                //    foreach (DataRow row in dr)
                //    {
                //        for (col = 0; col < dtBCPhep.Columns.Count - 2; col++)
                //        {
                //            rowData[rowCnt, col] = row[col].ToString();
                //        }
                //        rowCnt++;
                //    }
                //    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                //    {
                //        dr_Cu = 0;
                //        rowBD_XN = 0;
                //        chanVongDau = "";
                //    }
                //    else
                //    {
                //        rowBD_XN = 1;
                //    }
                //    rowBD = rowBD + dr_Cu + rowBD_XN;
                //    //rowCnt = rowCnt + 6 + dr_Cu;
                //    rowCnt = rowBD + current_dr - 1;

                //    // Tạo group tổ
                //    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                //    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                //    row_groupXI_NGHIEP_Format.Merge();
                //    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                //    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                //    //for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                //    //{
                //    //    oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                //    //    oSheet.Cells[rowBD, col].Font.Bold = true;
                //    //    oSheet.Cells[rowBD, col].Font.Size = 12;
                //    //}

                //    //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                //    //sRowBD_XN_Temp = sRowBD_XN;
                //    //Đổ dữ liệu của xí nghiệp
                //    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                //    formatRange = oSheet.get_Range("A" + (rowBD + 1).ToString() + "", "A" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0";
                //    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //    formatRange = oSheet.get_Range("E" + (rowBD + 1).ToString() + "", "E" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0";
                //    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //    formatRange = oSheet.get_Range("F" + (rowBD + 1).ToString() + "", "F" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0";
                //    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //    formatRange = oSheet.get_Range("G" + (rowBD + 1).ToString() + "", "G" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0";
                //    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //    formatRange = oSheet.get_Range("H" + (rowBD + 1).ToString() + "", "H" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0";
                //    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //    formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //    for (col = 8; col < dtBCPhep.Columns.Count - 2; col++)
                //    {
                //        currentColumn = CharacterIncrement(col);
                //        formatRange = oSheet.get_Range(currentColumn + "" + (rowBD + 1).ToString() + "", currentColumn + (rowCnt + 1).ToString());
                //        formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                //        try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //    }

                //    formatRange = oSheet.get_Range("U" + (rowBD + 1).ToString() + "", "W" + (rowCnt + 1).ToString());
                //    formatRange.NumberFormat = "#,##0.0";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //    //// Dữ liệu cột tổng tăng
                //    //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                //    //{
                //    //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                //    //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                //    //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                //    //}
                //    //formatRange1 = oSheet.get_Range("Y" + rowCntY.ToString());
                //    //formatRange1.Value2 = "X" + rowCntY + "-W" + rowCntY + "";
                //    //oSheet.get_Range("Y" + rowCntY + "").Value2 = "=X" + rowCntY + " - W" + rowCntY + "";
                //    //rowCntY++;
                //    dr_Cu = current_dr;
                //    keepRowCnt = rowCnt;
                //    rowCnt = 0;
                //}
                //rowCnt = keepRowCnt;


                formatRange = oSheet.get_Range("A6", "A" + (rowCnt).ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("G6", "G" + (rowCnt).ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                string currentColumn = "";
                for (col = 2; col < dtBCPhep.Columns.Count - 2; col++)
                {
                    currentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(currentColumn + "6", currentColumn + (rowCnt).ToString());
                    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).RowHeight = 25;
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                oXL.Visible = true;
                oXL.UserControl = true;
                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);

            }
            catch (Exception ex)
            {

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

    }
}
