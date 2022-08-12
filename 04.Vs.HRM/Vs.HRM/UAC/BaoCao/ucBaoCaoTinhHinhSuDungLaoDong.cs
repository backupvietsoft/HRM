using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Linq;

namespace Vs.HRM
{
    public partial class ucBaoCaoTinhHinhSuDungLaoDong : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoTinhHinhSuDungLaoDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            dtNAM.EditValue = DateTime.Now;
            dtTmp = LoadText();
            ShowText(dtTmp);
        }
        bool flag = true;
        private void ShowText(DataTable dtTmp)
        {
            try
            {
                flag = false;

                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    Id = Int64.Parse(dtTmp.Rows[0]["ID"].ToString());
                    rdo_ChonBaoCao.SelectedIndex = ((bool.Parse(dtTmp.Rows[0]["DAU_NAM"].ToString()) == true) ? 0 : 1);
                    dtNAM.DateTime = new DateTime(int.Parse(dtTmp.Rows[0]["NAM"].ToString()), 1, 1);
                    txTONG_DK.EditValue = dtTmp.Rows[0]["TONG_DK"].ToString();
                    txTONG_DK_NU.EditValue = dtTmp.Rows[0]["TONG_DK_NU"].ToString();
                    txLD_KTH_DK.EditValue = dtTmp.Rows[0]["LD_KTH_DK"].ToString();
                    txLD_KTH_DK_NU.EditValue = dtTmp.Rows[0]["LD_KTH_DK_NU"].ToString();
                    txLD_13_DK.EditValue = dtTmp.Rows[0]["LD_13_DK"].ToString();
                    txLD_13_DK_NU.EditValue = dtTmp.Rows[0]["LD_13_DK_NU"].ToString();
                    txLD_D1_DK.EditValue = dtTmp.Rows[0]["LD_D1_DK"].ToString();
                    txLD_D1_DK_NU.EditValue = dtTmp.Rows[0]["LD_D1_DK_NU"].ToString();
                    txTU_TUYEN.EditValue = dtTmp.Rows[0]["TU_TUYEN"].ToString();
                    txTUYEN_QUA_TT.EditValue = dtTmp.Rows[0]["TUYEN_QUA_TT"].ToString();
                    AddEdit = false;
                }
                else
                {
                    Id = -1;
                    txTONG_DK.EditValue = 0;
                    txTONG_DK_NU.EditValue = 0;
                    txLD_KTH_DK.EditValue = 0;
                    txLD_KTH_DK_NU.EditValue = 0;
                    txLD_13_DK.EditValue = 0;
                    txLD_13_DK_NU.EditValue = 0;
                    txLD_D1_DK.EditValue = 0;
                    txLD_D1_DK_NU.EditValue = 0;
                    txTU_TUYEN.EditValue = 0;
                    txTUYEN_QUA_TT.EditValue = 0;
                    AddEdit = true;
                }
                flag = true;

            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        DataTable dtTmp;
        private DataTable LoadText()
        {
            try
            {
                string sSql = "";

                sSql += "SELECT";
                sSql += " ID ";
                sSql += ",[NAM]";
                sSql += ",[DAU_NAM]";
                sSql += ",[TONG_DK]";
                sSql += ",[TONG_DK_NU]";
                sSql += ",[LD_KTH_DK]";
                sSql += ",[LD_KTH_DK_NU]";
                sSql += ",[LD_13_DK]";
                sSql += ",[LD_13_DK_NU]";
                sSql += ",[LD_D1_DK]";
                sSql += ",[LD_D1_DK_NU]";
                sSql += ",[TU_TUYEN]";
                sSql += ",[TUYEN_QUA_TT]";
                sSql += "FROM[LAO_DONG_DU_KIEN]";
                sSql += " ";
                sSql += " WHERE [NAM] = " + dtNAM.DateTime.Year;
                sSql += "AND [DAU_NAM] =" + ((rdo_ChonBaoCao.SelectedIndex == 0) ? 1 : 0);
                dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (dtTmp != null && dtTmp.Rows.Count > 0)
                {
                    AddEdit = true;
                }
                else
                {
                    AddEdit = false;
                    Id = -1;
                }
                return dtTmp;
            }
            catch
            {
                AddEdit = false;
            }
            return null;
        }
        static Int64 Id = -1;
        static Boolean AddEdit = true;  // true la add false la edit
        private void LuuTruocKhiIn()
        {
            try
            {
                Commons.Modules.sId = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateLAO_DONG_DU_KIEN",
                    (AddEdit ? -1 : Id).ToString(),
                    (dtNAM.EditValue == null) ? 0 : dtNAM.DateTime.Year,
                    (rdo_ChonBaoCao.SelectedIndex == 0) ? 1 : 0,
                    (txTONG_DK.EditValue == null) ? 0 : txTONG_DK.EditValue,
                    (txTONG_DK_NU.EditValue == null) ? 0 : txTONG_DK_NU.EditValue,
                    (txLD_KTH_DK.EditValue == null) ? 0 : txLD_KTH_DK.EditValue,
                    (txLD_KTH_DK_NU.EditValue == null) ? 0 : txLD_KTH_DK_NU.EditValue,
                    (txLD_13_DK.EditValue == null) ? 0 : txLD_13_DK.EditValue,
                    (txLD_13_DK_NU.EditValue == null) ? 0 : txLD_13_DK_NU.EditValue,
                    (txLD_D1_DK.EditValue == null) ? 0 : txLD_D1_DK.EditValue,
                    (txLD_D1_DK_NU.EditValue == null) ? 0 : txLD_D1_DK_NU.EditValue,
                    (txTU_TUYEN.EditValue == null) ? 0 : txTU_TUYEN.EditValue,
                    (txTUYEN_QUA_TT.EditValue == null) ? 0 : txTUYEN_QUA_TT.EditValue
                    ).ToString();

                Id = Int64.Parse(Commons.Modules.sId);
                if (Id != -1)
                    AddEdit = false;
            }
            catch
            {

            }
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {


                case "Print":
                    {
                        DateTime firstDateTime = DateTime.Today;
                        DateTime secondDateTime = DateTime.Today;


                        if (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)) == "HN")
                        {
                            // lấy dữ liệu sau khi lưu
                            switch (rdo_ChonBaoCao.SelectedIndex)
                            {
                                case 0:
                                    {
                                        firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 1, 1);
                                        secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 6, 30);
                                    }
                                    break;
                                case 1:
                                    {
                                        firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 7, 1);
                                        secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 12, 31);
                                    }
                                    break;

                                default:
                                    break;
                            }
                            BaoCaoChiTiet6ThangDau_HN(firstDateTime, secondDateTime);
                        }
                        else
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();

                            frm = new frmViewReport();

                            string sTieuDe = "";
                            string sTieuDe2 = "";

                            LuuTruocKhiIn();

                            // lấy dữ liệu sau khi lưu
                            switch (rdo_ChonBaoCao.SelectedIndex)
                            {
                                case 0:
                                    {
                                        firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 1, 1);
                                        secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 6, 30);
                                        sTieuDe = "6 THÁNG ĐẦU NĂM " + Convert.ToString(dtNAM.DateTime.Year);
                                        sTieuDe2 = "6 THÁNG CUỐI NĂM NĂM " + Convert.ToString(dtNAM.DateTime.Year);
                                    }
                                    break;
                                case 1:
                                    {
                                        firstDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 7, 1);
                                        secondDateTime = new DateTime(Convert.ToInt32(dtNAM.DateTime.Year), 12, 31);
                                        sTieuDe = "6 THÁNG CUỐI NĂM NĂM " + Convert.ToString(dtNAM.DateTime.Year);
                                        sTieuDe2 = "6 THÁNG ĐẦU NĂM " + Convert.ToString(dtNAM.DateTime.Year + 1);


                                    }
                                    break;

                                default:
                                    break;

                            }
                            frm.rpt = new rptBCTinhHinhSuDungLaoDong(lk_NgayIn.DateTime, sTieuDe, sTieuDe2);
                            try
                            {
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTinhHinhSuDungLaoDong", conn);

                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firstDateTime;
                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondDateTime;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);
                            }
                            catch
                            { }
                            frm.ShowDialog();
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoTinhHinhSuDungLaoDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dtNAM.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void dtNAM_Validated(object sender, EventArgs e)
        {
            if (flag)
            {
                dtTmp = LoadText();
                ShowText(dtTmp);
            }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                dtTmp = LoadText();
                ShowText(dtTmp);
            }

        }

        #region function
        private void BaoCaoChiTiet6ThangDau_HN(DateTime firtTime, DateTime secondTime)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTinhHinhSuDungLaoDong_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firtTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dtSLDV = new DataTable(); // Lấy số lượng đơn vị
                dtSLDV = ds.Tables[1].Copy();
                int slDV = Convert.ToInt32(dtSLDV.Rows[0][0]);

                DataTable dtSLXN = new DataTable(); // Lấy số lượng đơn vị
                dtSLXN = ds.Tables[2].Copy();
                int slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

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
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A4", lastColumn + "4");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 24;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO CHI TIẾT 6 THÁNG " + (rdo_ChonBaoCao.SelectedIndex == 0 ? "ĐẦU" : "CUỐI") + " NĂM " + dtNAM.Text + "";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.get_Range("A5", "A7");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "ĐƠN VỊ";
                row5_TieuDe_DV.ColumnWidth = 12;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.get_Range("B5", "B7");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = "LĐ BQ";
                row5_TieuDe_LDBQ.ColumnWidth = 6;

                Range row5_TieuDe_LDT = oSheet.get_Range("C5", "E5");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "LAO ĐỘNG TĂNG";

                Range row6_TieuDe_TT = oSheet.get_Range("C6", "C7");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "TỔNG TĂNG";
                row6_TieuDe_TT.ColumnWidth = 11;
                row6_TieuDe_TT.RowHeight = 30;
                row6_TieuDe_TT.Font.Color = Color.FromArgb(255, 0, 0);

                Range row5_TieuDe_DT = oSheet.get_Range("D6", "D7");
                row5_TieuDe_DT.Merge();
                row5_TieuDe_DT.Value2 = "ĐÀO TẠO";
                row5_TieuDe_DT.ColumnWidth = 11;

                Range row5_TieuDe_TN = oSheet.get_Range("E6", "E7");
                row5_TieuDe_TN.Merge();
                row5_TieuDe_TN.Value2 = "THỬ VIỆC";
                row5_TieuDe_TN.ColumnWidth = 11;


                Range row5_TieuDe_LDG = oSheet.get_Range("F5", "N5");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "LAO ĐỘNG GIẢM";

                Range row6_TieuDe_TG = oSheet.get_Range("F6", "F7");
                row6_TieuDe_TG.Merge();
                row6_TieuDe_TG.Value2 = "TỔNG GIẢM";
                row6_TieuDe_TG.ColumnWidth = 11;
                row6_TieuDe_TG.Font.Color = Color.FromArgb(255, 0, 0);


                Range row6_TieuDe_D1T = oSheet.get_Range("G6", "G7");
                row6_TieuDe_D1T.Merge();
                row6_TieuDe_D1T.Value2 = "DƯỚI 1 THÁNG";
                row6_TieuDe_D1T.ColumnWidth = 11;

                Range row6_TieuDe_1_3_T = oSheet.get_Range("H6", "H7");
                row6_TieuDe_1_3_T.Merge();
                row6_TieuDe_1_3_T.Value2 = "1-3 THÁNG";
                row6_TieuDe_1_3_T.ColumnWidth = 7.6;

                Range row6_TieuDe_3_6_T = oSheet.get_Range("I6", "I7");
                row6_TieuDe_3_6_T.Merge();
                row6_TieuDe_3_6_T.Value2 = "3-6 THÁNG";
                row6_TieuDe_3_6_T.ColumnWidth = 11;

                Range row6_TieuDe_6_9_T = oSheet.get_Range("J6", "J7");
                row6_TieuDe_6_9_T.Merge();
                row6_TieuDe_6_9_T.Value2 = "6-9 THÁNG";
                row6_TieuDe_6_9_T.ColumnWidth = 11;

                Range row6_TieuDe_9_12_T = oSheet.get_Range("K6", "K7");
                row6_TieuDe_9_12_T.Merge();
                row6_TieuDe_9_12_T.Value2 = "9-12 THÁNG";
                row6_TieuDe_9_12_T.ColumnWidth = 11;

                Range row6_TieuDe_T1N = oSheet.get_Range("L6", "L7");
                row6_TieuDe_T1N.Merge();
                row6_TieuDe_T1N.Value2 = "TRÊN 1 NĂM";
                row6_TieuDe_T1N.ColumnWidth = 11;

                Range row6_TieuDe_BV = oSheet.get_Range("M6", "M7");
                row6_TieuDe_BV.Merge();
                row6_TieuDe_BV.Value2 = "BV";
                row6_TieuDe_BV.ColumnWidth = 7.6;

                Range row6_TieuDe_NV = oSheet.get_Range("N6", "N7");
                row6_TieuDe_NV.Merge();
                row6_TieuDe_NV.Value2 = "NV";
                row6_TieuDe_NV.ColumnWidth = 11;

                Range row6_TieuDe_LDCK = oSheet.get_Range("O5", "O7");
                row6_TieuDe_LDCK.Merge();
                row6_TieuDe_LDCK.Value2 = "LĐ CUỐI KỲ";
                row6_TieuDe_LDCK.ColumnWidth = 14;

                Range row6_TieuDe_GC = oSheet.get_Range("P5", "P7");
                row6_TieuDe_GC.Merge();
                row6_TieuDe_GC.Value2 = "GHI CHÚ";
                row6_TieuDe_GC.ColumnWidth = 14;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 8;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data


                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    // Tạo group đơn vị
                    Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Underline = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    sRowBD_DV = sRowBD_DV + rowBD.ToString() + "+;";
                    rowBD++;

                    for (int j = 0; j < TEN_XN.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[j]).CopyToDataTable().Copy();
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


                        // Tạo group xí nghiệp
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Underline = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Underline = true;
                            oSheet.Cells[rowBD, col].Font.Italic = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";

                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        // Dữ liệu cột tổng tăng
                        for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        {
                            oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                            oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                            oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        }
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }
                }
                Microsoft.Office.Interop.Excel.Range formatRange;
                //Sum đơn vị
                string[] strGetRowDV = sRowBD_DV.Split(';');
                string sRowBD_DV_Temp = sRowBD_DV;
                string sRowBD_XN_Temp = sRowBD_XN; // Lưu giá trị cũ
                for (int i = 0; i < strGetRowDV.Count(); i++)
                {
                    if (strGetRowDV[i].ToString() != "")
                    {
                        for (col = 0; col < dtBCThang.Columns.Count - 4; col++) // Bỏ thêm 2 cột ghi chú và lao động cuối kỳ
                        {
                            formatRange = oSheet.get_Range("" + CharacterIncrement(col + 1) + "" + strGetRowDV[i].Substring(0, strGetRowDV[i].Length - 1).ToString() + "");
                            formatRange.Font.Bold = true;
                            formatRange.Font.Underline = true;
                            formatRange.Font.Size = 14;
                            sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                            sRowBD_XN = sRowBD_XN.Replace(';', Convert.ToChar(CharacterIncrement(col + 1)));
                            formatRange.Value = "=" + sRowBD_XN;
                            sRowBD_XN = sRowBD_XN_Temp;
                        }
                    }
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                formatRange = oSheet.get_Range("C8", "" + "C" + rowCnt + "");
                formatRange.Font.Color = Color.FromArgb(255, 0, 0);
                formatRange.Font.Bold = true;
                formatRange = oSheet.get_Range("F8", "" + "F" + rowCnt + "");
                formatRange.Font.Color = Color.FromArgb(255, 0, 0);
                formatRange.Font.Bold = true;

                rowCnt++;
                formatRange = oSheet.get_Range("A" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange.Font.Size = 14;
                formatRange.Font.Bold = true;
                formatRange.Font.Underline = true;
                formatRange = oSheet.get_Range("A" + rowCnt + "");
                formatRange.Value = "TỔNG";

                for (col = 0; col < dtBCThang.Columns.Count - 4; col++) // Bỏ thêm 2 cột ghi chú và lao động cuối kỳ
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col + 1) + "" + rowCnt + "");
                    sRowBD_DV = sRowBD_DV.Substring(0, sRowBD_DV.Length - 2);
                    sRowBD_DV = sRowBD_DV.Replace(';', Convert.ToChar(CharacterIncrement(col + 1)));
                    formatRange.Value = "=" + sRowBD_DV;
                    sRowBD_DV = sRowBD_DV_Temp;
                }

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "8", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A8", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));

                rowCnt++;
                rowCnt++;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tp.HCM , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "";
                rowCnt++;
                formatRange = oSheet.get_Range("E" + rowCnt + "");
                formatRange.Value = "P.TCLĐ";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tổng giám đốc";


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = fontSizeNoiDung;
                //BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                //// filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
    }
}