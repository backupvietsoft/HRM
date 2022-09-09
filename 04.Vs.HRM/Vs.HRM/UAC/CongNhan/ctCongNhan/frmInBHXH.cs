using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Excel;
using DataTable = System.Data.DataTable;
using Microsoft.ApplicationBlocks.Data;
using System.Reflection;
using System.Drawing;
using System.Linq;

namespace Vs.HRM
{
    public partial class frmInBHXH : DevExpress.XtraEditors.XtraForm
    {
        private string SaveExcelFile;
        private DateTime ThangBC = new DateTime(DateTime.Now.Year, 1, 1);
        private Int32 DotBC = 1;
        public frmInBHXH(DateTime Thang, Int32 Dot)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

            ThangBC = Thang;
            DotBC = Dot;
        }

        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
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
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dtTTC = new DataTable(); // Lấy ký hiệu đơn vị trong thông tin chung

                        dtTTC = Commons.Modules.ObjSystems.DataThongTinChung();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                               
                                switch (dtTTC.Rows[0]["KY_HIEU_DV"].ToString())
                                {
                                    case "SB":
                                        {
                                            try
                                            {
                                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn.Open();
                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongTangBHXH_SB", conn);
                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                                cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                ds.Tables[0].TableName = "TangLaoDong";
                                                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                                                //saveFileDialog.Filter = "Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht";
                                                //saveFileDialog.FilterIndex = 0;
                                                //saveFileDialog.RestoreDirectory = true;
                                                ////saveFileDialog.CreatePrompt = true;
                                                //saveFileDialog.CheckFileExists = false;
                                                //saveFileDialog.CheckPathExists = false;
                                                //saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                                //saveFileDialog.Title = "Export Excel File To";
                                                //DialogResult res = saveFileDialog.ShowDialog();
                                                //// If the file name is not an empty string open it for saving.
                                                //if (res == DialogResult.OK)
                                                //{
                                                //    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TangLaoDong..xls", ds, new string[] { "{", "}" });
                                                //    Process.Start(saveFileDialog.FileName);
                                                //}
                                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                                saveFileDialog.FilterIndex = 0;
                                                saveFileDialog.RestoreDirectory = true;
                                                //saveFileDialog.CreatePrompt = true;
                                                saveFileDialog.CheckFileExists = false;
                                                saveFileDialog.CheckPathExists = false;
                                                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                                saveFileDialog.Title = "Export Excel File To";
                                                // If the file name is not an empty string open it for saving.
                                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                                {
                                                    if (saveFileDialog.FileName != "")
                                                    {
                                                        DataTable dt1 = new DataTable();
                                                        dt1 = ds.Tables[0].Copy();
                                                        dt1.Columns.Count.ToString();
                                                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateTangLaoDong_SB.xlsx", ds, new string[] { "{", "}" });
                                                        //Commons.TemplateExcel.FillReport(saveFileDialog.FileName, Application.StartupPath + "\\lib\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                        Process.Start(saveFileDialog.FileName);
                                                    }
                                                }
                                            }
                                            catch (Exception EX
                                            )
                                            {

                                            }
                                            break;
                                        }
                                    default:
                                        DataTable dt = new DataTable();
                                        try
                                        {

                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongTangBHXH", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                            cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            ds.Tables[0].TableName = "TangLaoDong";
                                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                                            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                            saveFileDialog.FilterIndex = 0;
                                            saveFileDialog.RestoreDirectory = true;
                                            //saveFileDialog.CreatePrompt = true;
                                            saveFileDialog.CheckFileExists = false;
                                            saveFileDialog.CheckPathExists = false;
                                            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                            saveFileDialog.Title = "Export Excel File To";
                                            DialogResult res = saveFileDialog.ShowDialog();
                                            // If the file name is not an empty string open it for saving.
                                            if (res == DialogResult.OK)
                                            {
                                                Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateTangLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                Process.Start(saveFileDialog.FileName);
                                            }
                                        }
                                        catch (Exception EX
                                        )
                                        {

                                        }
                                        break;
                                }

                                break;
                            case 1:
                                try
                                {
                                    switch (dtTTC.Rows[0]["KY_HIEU_DV"].ToString())
                                    {
                                        case "SB":
                                            {
                                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn.Open();
                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongGiamBHXH_SB", conn);
                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                                cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                ds.Tables[0].TableName = "GiamLaoDong";
                                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                                saveFileDialog.FilterIndex = 0;
                                                saveFileDialog.RestoreDirectory = true;
                                                //saveFileDialog.CreatePrompt = true;
                                                saveFileDialog.CheckFileExists = false;
                                                saveFileDialog.CheckPathExists = false;
                                                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                                saveFileDialog.Title = "Export Excel File To";
                                                // If the file name is not an empty string open it for saving.
                                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                                {
                                                    if (saveFileDialog.FileName != "")
                                                    {
                                                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateGiamLaoDong_SB.xlsx", ds, new string[] { "{", "}" });
                                                        //Commons.TemplateExcel.FillReport(saveFileDialog.FileName, Application.StartupPath + "\\lib\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                        Process.Start(saveFileDialog.FileName);
                                                    }
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn.Open();
                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongGiamBHXH", conn);
                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(ThangBC).ToString("yyyy-MM-dd");
                                                cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = Convert.ToInt32(DotBC);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                ds.Tables[0].TableName = "GiamLaoDong";
                                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                                saveFileDialog.FilterIndex = 0;
                                                saveFileDialog.RestoreDirectory = true;
                                                //saveFileDialog.CreatePrompt = true;
                                                saveFileDialog.CheckFileExists = false;
                                                saveFileDialog.CheckPathExists = false;
                                                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                                saveFileDialog.Title = "Export Excel File To";
                                                // If the file name is not an empty string open it for saving.
                                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                                {
                                                    if (saveFileDialog.FileName != "")
                                                    {
                                                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                        //Commons.TemplateExcel.FillReport(saveFileDialog.FileName, Application.StartupPath + "\\lib\\Template\\TemplateGiamLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                        Process.Start(saveFileDialog.FileName);
                                                    }
                                                }
                                                break;
                                            }
                                    }

                                }
                                catch
                                {

                                }
                                break;
                            case 2:
                                if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "SB")
                                {
                                    InDanhSachThamGiaBH_SB();
                                }
                                else
                                {
                                    InDanhSachThamGiaBH();
                                }
                                break;
                            default:
                                break;
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
        private void InDanhSachThamGiaBH_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtTangLD;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTangGiamBHXHThang", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = ThangBC;
                cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = DotBC;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtTangLD = new DataTable();
                dtTangLD = ds.Tables[0].Copy();

                DataTable dtTangTL = new DataTable();
                dtTangTL = ds.Tables[1].Copy();

                DataTable dtGiamLD = new DataTable();
                dtGiamLD = ds.Tables[2].Copy();

                DataTable dtGiamTL = new DataTable();
                dtGiamTL = ds.Tables[3].Copy();

                DataTable dtKhac = new DataTable();
                dtKhac = ds.Tables[4].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtTangLD.Columns.Count - 1);

                Range row1_TieuDe = oSheet.get_Range("A1", "H1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Size = fontSizeTieuDe;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.Value2 = "Tên đơn vị: CÔNG TY TNHH THỜI TRANG S.B SAIGON";

                Range row1_TieuDe_MAU = oSheet.get_Range("I1", "L1");
                row1_TieuDe_MAU.Merge();
                row1_TieuDe_MAU.Font.Size = fontSizeTieuDe;
                row1_TieuDe_MAU.Font.Name = fontName;
                row1_TieuDe_MAU.Font.Bold = true;
                row1_TieuDe_MAU.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe_MAU.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row1_TieuDe_MAU.Value2 = "Mẫu D02-TS";


                Range row2_TieuDe = oSheet.get_Range("A2", "H2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = fontSizeTieuDe;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Value2 = "Mã đơn vị: .YN0648Z……………..";

                Range row2_TieuDe_MAU = oSheet.get_Range("I2", "L2");
                row2_TieuDe_MAU.Merge();
                row2_TieuDe_MAU.Font.Size = fontSizeTieuDe;
                row2_TieuDe_MAU.Font.Name = fontName;
                row2_TieuDe_MAU.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_MAU.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_MAU.Value2 = "(Ban hành kèm theo QĐ số: 595/QĐ-BHXH";

                Range row3_TieuDe = oSheet.get_Range("A3", "H3");
                row3_TieuDe.Merge();
                row3_TieuDe.Font.Size = fontSizeTieuDe;
                row3_TieuDe.Font.Name = fontName;
                row3_TieuDe.Value2 = "Địa chỉ: Đường số 8 - KCX Tân Thuận - P. Tân Thuận Đông - Q7 - TP.HCM";

                Range row3_TieuDe_MAU = oSheet.get_Range("I3", "L3");
                row3_TieuDe_MAU.Merge();
                row3_TieuDe_MAU.Font.Size = fontSizeTieuDe;
                row3_TieuDe_MAU.Font.Name = fontName;
                row3_TieuDe_MAU.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDe_MAU.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row3_TieuDe_MAU.Value2 = "ngày 14/4/2017 của BHXH Việt Nam)";

                Range row4_TieuDe_BaoCao = oSheet.get_Range("A4", lastColumn + "4");
                row4_TieuDe_BaoCao.Merge();
                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_TieuDe_BaoCao.Font.Name = fontName;
                row4_TieuDe_BaoCao.Font.Bold = true;
                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_BaoCao.RowHeight = 20;
                row4_TieuDe_BaoCao.Value2 = "DANH SÁCH LAO ĐỘNG SỐ : " + DotBC + " THÁNG " + ThangBC.Month + " NĂM " + ThangBC.Year + "";

                Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", lastColumn + "5");
                row5_TieuDe_BaoCao.Merge();
                row5_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;
                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_BaoCao.RowHeight = 15;
                row5_TieuDe_BaoCao.Value2 = "THAM GIA BHXH, BHYT, BHTN, BHTNLĐ, BNN";


                Range row7_TieuDe_Format = oSheet.get_Range("A7", lastColumn + "10"); //27 + 31
                row7_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row7_TieuDe_Format.Font.Name = fontName;
                row7_TieuDe_Format.Font.Bold = true;
                row7_TieuDe_Format.WrapText = true;
                row7_TieuDe_Format.NumberFormat = "@";
                row7_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row7_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row7_TieuDe_Stt = oSheet.get_Range("A7", "A9");
                row7_TieuDe_Stt.Merge();
                row7_TieuDe_Stt.Value2 = "STT";
                row7_TieuDe_Stt.ColumnWidth = 5;

                Range row8_TieuDe_HoTen = oSheet.get_Range("B7", "B9");
                row8_TieuDe_HoTen.Merge();
                row8_TieuDe_HoTen.Value2 = "Họ và tên";
                row8_TieuDe_HoTen.ColumnWidth = 30;

                Range row8_TieuDe_MSCN = oSheet.get_Range("C7", "C9");
                row8_TieuDe_MSCN.Merge();
                row8_TieuDe_MSCN.Value2 = "MSNV";
                row8_TieuDe_MSCN.ColumnWidth = 13;

                Range row8_TieuDe_SoBHXH = oSheet.get_Range("D7", "D9");
                row8_TieuDe_SoBHXH.Merge();
                row8_TieuDe_SoBHXH.Value2 = "Mã số BHXH";
                row8_TieuDe_SoBHXH.ColumnWidth = 13;

                Range row8_TieuDe_NoiLV = oSheet.get_Range("E7", "E9");
                row8_TieuDe_NoiLV.Merge();
                row8_TieuDe_NoiLV.Value2 = "Cấp bậc, chức vụ, chức danh nghề, nơi làm việc";
                row8_TieuDe_NoiLV.ColumnWidth = 23;


                Range row8_TieuDe_TienLuong = oSheet.get_Range("F7", "K7");
                row8_TieuDe_TienLuong.Merge();
                row8_TieuDe_TienLuong.Value2 = "Tiền lương";

                Range row8_TieuDe_ML = oSheet.get_Range("F8", "F9");
                row8_TieuDe_ML.Merge();
                row8_TieuDe_ML.Value2 = "Hệ số/Mức lương";
                row8_TieuDe_ML.ColumnWidth = 11;

                Range row8_TieuDe_PhuCap = oSheet.get_Range("G8", "K8");
                row8_TieuDe_PhuCap.Merge();
                row8_TieuDe_PhuCap.Value2 = "Phụ cấp";

                Range row8_TieuDe_ChucVu = oSheet.get_Range("G9");
                row8_TieuDe_ChucVu.Merge();
                row8_TieuDe_ChucVu.Value2 = "Chức vụ";
                row8_TieuDe_ChucVu.ColumnWidth = 18;

                Range row9_TieuDe_ThamNienVK = oSheet.get_Range("H9");
                row9_TieuDe_ThamNienVK.Merge();
                row9_TieuDe_ThamNienVK.Value2 = "Thâm niên VK (%)";
                row9_TieuDe_ThamNienVK.ColumnWidth = 11;


                Range row9_TieuDe_ThamNienNghe = oSheet.get_Range("I9");
                row9_TieuDe_ThamNienNghe.Merge();
                row9_TieuDe_ThamNienNghe.Value2 = "Thâm niên nghề (%)";
                row9_TieuDe_ThamNienNghe.ColumnWidth = 11;


                Range row10_TieuDe_PC_LUONG = oSheet.get_Range("J9");
                row10_TieuDe_PC_LUONG.Value2 = "Phụ cấp lương";
                row10_TieuDe_PC_LUONG.ColumnWidth = 11;

                Range row10_TieuDe_BoSung = oSheet.get_Range("K9");
                row10_TieuDe_BoSung.Value2 = "Các khoản bổ sung";
                row10_TieuDe_BoSung.ColumnWidth = 11;

                Range row10_TieuDe_TuThang = oSheet.get_Range("L7", "L9");
                row10_TieuDe_TuThang.Merge();
                row10_TieuDe_TuThang.Value2 = "Từ tháng, năm";
                row10_TieuDe_TuThang.ColumnWidth = 18;


                Range row10_TieuDe_DenThang = oSheet.get_Range("M7", "M9");
                row10_TieuDe_DenThang.Merge();
                row10_TieuDe_DenThang.Value2 = "Đến tháng, năm";
                row10_TieuDe_DenThang.ColumnWidth = 18;

                Range row10_TieuDe_GhiChu = oSheet.get_Range("N7", "N9");
                row10_TieuDe_GhiChu.Merge();
                row10_TieuDe_GhiChu.Value2 = "Ghi chú";
                row10_TieuDe_GhiChu.ColumnWidth = 30;


                Range row10_TieuDeA = oSheet.get_Range("A10");
                row10_TieuDeA.Value2 = "A";

                Range row10_TieuDeB = oSheet.get_Range("B10");
                row10_TieuDeB.Value2 = "B";

                Range row10_TieuDeC = oSheet.get_Range("D10");
                row10_TieuDeC.Value2 = "C";

                Range row10_TieuDe1 = oSheet.get_Range("E10");
                row10_TieuDe1.Value2 = "1";

                Range row10_TieuDe2 = oSheet.get_Range("F10");
                row10_TieuDe2.Value2 = "2";

                Range row10_TieuDe3 = oSheet.get_Range("G10");
                row10_TieuDe3.Value2 = "3";

                Range row10_TieuDe4 = oSheet.get_Range("H10");
                row10_TieuDe4.Value2 = "4";

                Range row10_TieuDe5 = oSheet.get_Range("I10");
                row10_TieuDe5.Value2 = "5";

                Range row10_TieuDe6 = oSheet.get_Range("J10");
                row10_TieuDe6.Value2 = "6";

                Range row10_TieuDe7 = oSheet.get_Range("K10");
                row10_TieuDe7.Value2 = "7";

                Range row10_TieuDe8 = oSheet.get_Range("L10");
                row10_TieuDe8.Value2 = "8";

                Range row10_TieuDe9 = oSheet.get_Range("M10");
                row10_TieuDe9.Value2 = "9";

                Range row10_TieuDe10 = oSheet.get_Range("N10");
                row10_TieuDe10.Value2 = "10";

                Range row11_I = oSheet.get_Range("A11");
                row11_I.Font.Bold = true;
                row11_I.Value2 = "I";

                Range row11_Tang = oSheet.get_Range("B11");
                row11_Tang.Font.Bold = true;
                row11_Tang.Value2 = "Tăng";

                Range row11_I1 = oSheet.get_Range("A12");
                row11_I1.Value2 = "I.1";

                Range row11_TangLD = oSheet.get_Range("B12");
                row11_TangLD.Value2 = "Lao động";

                // insert tang lao dong
                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                DataRow[] dr = dtTangLD.Select();
                string[,] rowData = new string[dr.Count(), dtTangLD.Columns.Count];
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtTangLD.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                //rowCnt = rowCnt + 10;
                oSheet.get_Range("A13", lastColumn + (rowCnt + 12).ToString()).Value2 = rowData;
                //oSheet.get_Range("A13", lastColumn + rowCnt.ToString()).Value2 = rowData;

                Range row11_I2 = oSheet.get_Range("A" + (rowCnt + 12 + 2) + "");
                row11_I2.Value2 = "I.2";

                Range row11_TangTL = oSheet.get_Range("B" + (rowCnt + 12 + 2) + ""); // +2 để cách 1 dòng , 12 là tính từ cột đầu tiên tới dòng đổ dữ liệu -1
                row11_TangTL.Value2 = "Tiền lương";

                keepRowCnt = rowCnt + 12 + 2;
                // insert tang tien luong
                col = 0;
                rowCnt = 0;
                DataRow[] dr1 = dtTangTL.Select();
                string[,] rowData1 = new string[dr1.Count(), dtTangTL.Columns.Count];
                foreach (DataRow row in dr1)
                {
                    for (col = 0; col < dtTangTL.Columns.Count; col++)
                    {
                        rowData1[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }

                oSheet.get_Range("A" + (keepRowCnt + 1) + "", lastColumn + ((keepRowCnt + rowCnt)).ToString()).Value2 = rowData1;

                keepRowCnt = keepRowCnt + rowCnt + 2;

                Range row11_CongTang = oSheet.get_Range("B" + keepRowCnt + "");
                row11_CongTang.Font.Bold = true;
                row11_CongTang.Value2 = "Cộng tăng";

                keepRowCnt = keepRowCnt + 2;

                Range row11_II = oSheet.get_Range("A" + keepRowCnt + "");
                row11_II.Font.Bold = true;
                row11_II.Value2 = "II";

                Range row11_giam = oSheet.get_Range("B" + keepRowCnt + "");
                row11_giam.Font.Bold = true;
                row11_giam.Value2 = "Giảm";

                keepRowCnt = keepRowCnt + 1;

                Range row11_II1 = oSheet.get_Range("A" + keepRowCnt + "");
                row11_II1.Value2 = "II.1";

                Range row11_giamLD = oSheet.get_Range("B" + keepRowCnt + "");
                row11_giamLD.Value2 = "Lao động";

                col = 0;
                rowCnt = 0;
                DataRow[] dr2 = dtGiamLD.Select();
                string[,] rowData2 = new string[dr2.Count(), dtGiamLD.Columns.Count];
                foreach (DataRow row in dr2)
                {
                    for (col = 0; col < dtGiamLD.Columns.Count; col++)
                    {
                        rowData2[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }

                oSheet.get_Range("A" + (keepRowCnt + 1) + "", lastColumn + ((keepRowCnt + rowCnt)).ToString()).Value2 = rowData2;

                keepRowCnt = keepRowCnt + rowCnt + 2;

                Range row11_II2 = oSheet.get_Range("A" + keepRowCnt + "");
                row11_II2.Value2 = "II.2";

                Range row11_giamTL = oSheet.get_Range("B" + keepRowCnt + "");
                row11_giamTL.Value2 = "Tiền lương";


                col = 0;
                rowCnt = 0;
                DataRow[] dr3 = dtGiamTL.Select();
                string[,] rowData3 = new string[dr3.Count(), dtGiamTL.Columns.Count];
                foreach (DataRow row in dr3)
                {
                    for (col = 0; col < dtGiamTL.Columns.Count; col++)
                    {
                        rowData3[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }

                oSheet.get_Range("A" + (keepRowCnt + 1) + "", lastColumn + ((keepRowCnt + rowCnt)).ToString()).Value2 = rowData3;

                keepRowCnt = keepRowCnt + rowCnt + 2;

                Range row11_III = oSheet.get_Range("A" + keepRowCnt + "");
                row11_III.Value2 = "III";

                Range row11_KHAC = oSheet.get_Range("B" + keepRowCnt + "");

                row11_KHAC.Value2 = "Khác";

                col = 0;
                rowCnt = 0;
                DataRow[] dr4 = dtKhac.Select();
                string[,] rowData4 = new string[dr4.Count(), dtKhac.Columns.Count];
                foreach (DataRow row in dr4)
                {
                    for (col = 0; col < dtKhac.Columns.Count; col++)
                    {
                        rowData4[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }

                oSheet.get_Range("A" + (keepRowCnt + 1) + "", lastColumn + ((keepRowCnt + rowCnt)).ToString()).Value2 = rowData4;
                keepRowCnt = keepRowCnt + rowCnt + 2;



                Range row11_CongGiam = oSheet.get_Range("B" + keepRowCnt + "");
                row11_CongGiam.Font.Bold = true;
                row11_CongGiam.Value2 = "Cộng giảm";

                //oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).NumberFormat = "";
                Excel.Range formatRange;

                //Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A7", lastColumn + (keepRowCnt + 1).ToString()));

                //fomart All 
                formatRange = oSheet.get_Range("A11", lastColumn + (keepRowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;


                formatRange = oSheet.get_Range(CharacterIncrement(2) + "13", CharacterIncrement(2) + keepRowCnt.ToString());
                formatRange.NumberFormat = "@";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(3) + "13", CharacterIncrement(3) + keepRowCnt.ToString());
                formatRange.NumberFormat = "@";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Tienluong
                formatRange = oSheet.get_Range(CharacterIncrement(5) + "13", CharacterIncrement(5) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(6) + "13", CharacterIncrement(6) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(7) + "13", CharacterIncrement(7) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(8) + "13", CharacterIncrement(8) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(9) + "13", CharacterIncrement(9) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.get_Range(CharacterIncrement(10) + "13", CharacterIncrement(10) + keepRowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                keepRowCnt = keepRowCnt + 3;

                Range row11_TongBHXH = oSheet.get_Range("B" + keepRowCnt + "", "D" + keepRowCnt + "");
                row11_TongBHXH.Merge();
                row11_TongBHXH.Font.Name = fontName;
                row11_TongBHXH.Font.Size = fontSizeNoiDung;
                row11_TongBHXH.Value2 = "Tổng số Sổ BHXH đề nghị cấp: …………..";

                keepRowCnt++;

                Range row11_TongBHYT = oSheet.get_Range("B" + keepRowCnt + "", "D" + keepRowCnt + "");
                row11_TongBHYT.Merge();
                row11_TongBHYT.Font.Name = fontName;
                row11_TongBHYT.Font.Size = fontSizeNoiDung;
                row11_TongBHYT.Value2 = "Tổng số thẻ BHYT đề nghị cấp: ………………….";

                keepRowCnt++;

                Range row11_Ngayin = oSheet.get_Range("H" + keepRowCnt + "", "N" + keepRowCnt + "");
                row11_Ngayin.Merge();
                row11_Ngayin.Font.Name = fontName;
                row11_Ngayin.Font.Size = fontSizeNoiDung;
                row11_Ngayin.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row11_Ngayin.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row11_Ngayin.Value2 = "Ngày …" + dNgayIn.DateTime.Day + ".. tháng " + dNgayIn.DateTime.Month + " năm " + dNgayIn.DateTime.Year + "";

                keepRowCnt++;

                Range row11_NguoiLap = oSheet.get_Range("B" + keepRowCnt + "", "D" + keepRowCnt + "");
                row11_NguoiLap.Merge();
                row11_NguoiLap.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row11_NguoiLap.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row11_NguoiLap.Font.Name = fontName;
                row11_NguoiLap.Font.Size = fontSizeNoiDung;
                row11_NguoiLap.Font.Bold = true;
                row11_NguoiLap.Value2 = "Người lập biểu";


                Range row11_Donvi = oSheet.get_Range("H" + keepRowCnt + "", "N" + keepRowCnt + "");
                row11_Donvi.Merge();
                row11_Donvi.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row11_Donvi.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row11_Donvi.Font.Bold = true;
                row11_Donvi.Font.Name = fontName;
                row11_Donvi.Font.Size = fontSizeNoiDung;
                row11_Donvi.Value2 = "Đơn vị";

                keepRowCnt++;

                Range row11_NguoiKy = oSheet.get_Range("B" + keepRowCnt + "", "D" + keepRowCnt + "");
                row11_NguoiKy.Merge();
                row11_NguoiKy.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row11_NguoiKy.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row11_NguoiKy.Font.Name = fontName;
                row11_NguoiKy.Font.Size = fontSizeNoiDung;
                row11_NguoiKy.Value2 = "(Ký, ghi rõ họ tên)";

                Range row11_NguoiKy2 = oSheet.get_Range("H" + keepRowCnt + "", "N" + keepRowCnt + "");
                row11_NguoiKy2.Merge();
                row11_NguoiKy2.Font.Name = fontName;
                row11_NguoiKy2.Font.Size = fontSizeNoiDung;
                row11_NguoiKy2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row11_NguoiKy2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row11_NguoiKy2.Value2 = "Ký, ghi rõ họ tên, đóng dấu";



                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InDanhSachThamGiaBH()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBHXH;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachThamGiaBH_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime("01/03/2021");
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime("31/03/2021");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBHXH = new DataTable();
                dtBHXH = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeNoiDung = 9;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBHXH.Columns.Count - 3);

                Range row1_TieuDe = oSheet.get_Range("A1", "J1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = dtBHXH.Rows[0]["TEN_DV"];


                Range row2_TieuDe = oSheet.get_Range("A2", "J2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = dtBHXH.Rows[0]["DIA_CHI"];

                Range row3_TieuDe = oSheet.get_Range("A3", "J3");
                row3_TieuDe.Merge();
                row3_TieuDe.Font.Bold = true;
                row3_TieuDe.Value2 = "MÃ KCB:00028";

                Range row5_TieuDe_BaoCao = oSheet.get_Range("A5", lastColumn + "5");
                row5_TieuDe_BaoCao.Merge();
                row5_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;
                row5_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe_BaoCao.RowHeight = 30;
                row5_TieuDe_BaoCao.Value2 = "DANH SÁCH THAM GIA BHXH, BHYT, BHTN";

                Range row6_Ngay_BaoCao = oSheet.get_Range("A6", lastColumn + "6");
                row6_Ngay_BaoCao.Merge();
                row6_Ngay_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row6_Ngay_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row6_Ngay_BaoCao.Font.Bold = true;
                row6_Ngay_BaoCao.Value2 = "Tháng 5 năm 2019";

                Range row5_TieuDe_Format = oSheet.get_Range("A8", lastColumn + "10"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                //row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row5_TieuDe_Stt = oSheet.get_Range("A8", "A10");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 5;

                Range row8_TieuDe_HoTen = oSheet.get_Range("B8", "B10");
                row8_TieuDe_HoTen.Merge();
                row8_TieuDe_HoTen.Value2 = "Họ và tên";
                row8_TieuDe_HoTen.ColumnWidth = 25;

                Range row8_TieuDe_DiaChi = oSheet.get_Range("C8", "C10");
                row8_TieuDe_DiaChi.Merge();
                row8_TieuDe_DiaChi.Value2 = "Địa chỉ";
                row8_TieuDe_DiaChi.ColumnWidth = 50;

                Range row8_TieuDe_SoBHXH = oSheet.get_Range("D8", "D10");
                row8_TieuDe_SoBHXH.Merge();
                row8_TieuDe_SoBHXH.Value2 = "Số sổ BHXH";
                row8_TieuDe_SoBHXH.ColumnWidth = 15;

                Range row8_TieuDe_SoBHYT = oSheet.get_Range("E8", "E10");
                row8_TieuDe_SoBHYT.Merge();
                row8_TieuDe_SoBHYT.Value2 = "Số thẻ BHYT";
                row8_TieuDe_SoBHYT.ColumnWidth = 15;

                Range row8_TieuDe_NgaySinh = oSheet.get_Range("F8", "F10");
                row8_TieuDe_NgaySinh.Merge();
                row8_TieuDe_NgaySinh.Value2 = "Ngày sinh";
                row8_TieuDe_NgaySinh.ColumnWidth = 18;

                Range row8_TieuDe_GioiTinh = oSheet.get_Range("G8", "G10");
                row8_TieuDe_GioiTinh.Merge();
                row8_TieuDe_GioiTinh.Value2 = "Giới tính";
                row8_TieuDe_GioiTinh.ColumnWidth = 12;

                Range row8_TieuDe_NoiKCB = oSheet.get_Range("H8", "H10");
                row8_TieuDe_NoiKCB.Merge();
                row8_TieuDe_NoiKCB.Value2 = "Nơi đăng ký KCB";
                row8_TieuDe_NoiKCB.ColumnWidth = 35;

                Range row8_TieuDe_CanCu = oSheet.get_Range("I8", "M8");
                row8_TieuDe_CanCu.Merge();
                row8_TieuDe_CanCu.Value2 = "Căn cứ đóng BHXH, BHYT, BHTN";

                Range row9_TieuDe_TienLuong = oSheet.get_Range("I9", "I10");
                row9_TieuDe_TienLuong.Merge();
                row9_TieuDe_TienLuong.Value2 = "Tiền lương tiền công";
                row9_TieuDe_TienLuong.ColumnWidth = 15;

                Range row9_TieuDe_PhuCap = oSheet.get_Range("J9", "M9");
                row9_TieuDe_PhuCap.Merge();
                row9_TieuDe_PhuCap.Value2 = "Phụ cấp";

                Range row10_TieuDe_ChucVu = oSheet.get_Range("J10");
                row10_TieuDe_ChucVu.Value2 = "Chức vụ";

                Range row10_TieuDe_TNVK = oSheet.get_Range("K10");
                row10_TieuDe_TNVK.Value2 = "TN VK";

                Range row10_TieuDe_TNNG = oSheet.get_Range("L10");
                row10_TieuDe_TNNG.Value2 = "TN NG";

                Range row10_TieuDe_Khac = oSheet.get_Range("M10");
                row10_TieuDe_Khac.Value2 = "Khác";

                Range row8_TienLuongDongBHXH = oSheet.get_Range("N8", "N10");
                row8_TienLuongDongBHXH.Merge();
                row8_TienLuongDongBHXH.Value2 = "Tiền lương đóng BHXH";
                row8_TienLuongDongBHXH.ColumnWidth = 15;

                Range row8_TienLuongDongBHYT = oSheet.get_Range("O8", "O10");
                row8_TienLuongDongBHYT.Merge();
                row8_TienLuongDongBHYT.Value2 = "Tiền lương đóng BHYT";
                row8_TienLuongDongBHYT.ColumnWidth = 15;

                Range row8_TienLuongDongBHTN = oSheet.get_Range("P8", "P10");
                row8_TienLuongDongBHTN.Merge();
                row8_TienLuongDongBHTN.Value2 = "Tiền lương đóng BHTN";
                row8_TienLuongDongBHTN.ColumnWidth = 15;

                Range row8_ChucDanh = oSheet.get_Range("Q8", "Q10");
                row8_ChucDanh.Merge();
                row8_ChucDanh.Value2 = "Chức danh công việc";
                row8_ChucDanh.ColumnWidth = 40;


                int col = 0;
                DataRow[] dr = dtBHXH.Select();
                string[,] rowData = new string[dr.Count(), dtBHXH.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBHXH.Columns.Count - 2; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 10;
                oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A11", lastColumn + rowCnt.ToString()).NumberFormat = "";
                Excel.Range formatRange;

                //STT
                formatRange = oSheet.get_Range(CharacterIncrement(0) + "11", CharacterIncrement(0) + rowCnt.ToString());
                formatRange.NumberFormat = "0";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Ngaysinh
                formatRange = oSheet.get_Range(CharacterIncrement(5) + "11", CharacterIncrement(5) + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //gioitinh
                formatRange = oSheet.get_Range(CharacterIncrement(6) + "11", CharacterIncrement(6) + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                //Tienluong
                formatRange = oSheet.get_Range(CharacterIncrement(8) + "11", CharacterIncrement(8) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_Chucvu
                formatRange = oSheet.get_Range(CharacterIncrement(9) + "11", CharacterIncrement(9) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_TNVK   
                formatRange = oSheet.get_Range(CharacterIncrement(10) + "11", CharacterIncrement(10) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_TNNG
                formatRange = oSheet.get_Range(CharacterIncrement(11) + "11", CharacterIncrement(11) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //PhuCap_khac
                formatRange = oSheet.get_Range(CharacterIncrement(12) + "11", CharacterIncrement(12) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhxh
                formatRange = oSheet.get_Range(CharacterIncrement(13) + "11", CharacterIncrement(13) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhyt
                formatRange = oSheet.get_Range(CharacterIncrement(14) + "11", CharacterIncrement(14) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //tien luong dong bhtn
                formatRange = oSheet.get_Range(CharacterIncrement(15) + "11", CharacterIncrement(15) + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ;";
                try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                Range rowN_Tong = oSheet.get_Range("A" + (rowCnt + 1).ToString(), "H" + (rowCnt + 1).ToString());
                rowN_Tong.Merge();
                rowN_Tong.Value2 = "Cộng";
                rowN_Tong.Font.Bold = true;
                rowN_Tong.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //rowN_Tong.Cells.VerticalAlignment = Excel.XlVAlign.x;

                // Tính tổng
                for (int colSUM = 9; colSUM < dtBHXH.Columns.Count - 2; colSUM++)
                {
                    oSheet.Cells[rowCnt + 1, colSUM] = "=SUM(" + CellAddress(oSheet, 9, colSUM) + ":" + CellAddress(oSheet, rowCnt, colSUM) + ")";
                    oSheet.Cells[rowCnt + 1, colSUM].NumberFormat = "#,##0;(#,##0); ;";
                    oSheet.Cells[rowCnt + 1, colSUM].TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                int keeprowCnt = rowCnt; // dữ dòng rowCnt cuối cùng

                //Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A8", lastColumn + (rowCnt + 1).ToString()));



                //Tổng hợp ở dưới
                rowCnt = rowCnt + 3;
                Range rowText_TongHopChung = oSheet.get_Range("A" + rowCnt.ToString(), "B" + rowCnt.ToString());
                rowText_TongHopChung.Merge();
                rowText_TongHopChung.Value2 = "TỔNG HỢP CHUNG";
                rowText_TongHopChung.Font.Bold = true;

                rowCnt = rowCnt + 1;
                Range rowText_SoLD = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_SoLD.Value2 = "1. Số lao động";

                Range rowData_SoLD = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_SoLD.Value2 = dtBHXH.Rows.Count;

                Range rowNgayIn = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowNgayIn.Merge();
                rowNgayIn.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowNgayIn.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowNgayIn.Value2 = "Ngày 26 Tháng 5 Năm 2022";

                rowCnt++;
                Range rowText_SoLDTN = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_SoLDTN.Value2 = "2. Số lao động TN";

                Range rowData_SoLDTN = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_SoLDTN.Value2 = "4,90";

                Range rowText_CanBoThu = oSheet.get_Range("E" + rowCnt.ToString(), "F" + rowCnt.ToString());
                rowText_CanBoThu.Merge();
                rowText_CanBoThu.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_CanBoThu.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_CanBoThu.Font.Bold = true;
                rowText_CanBoThu.Value2 = "CÁN BỘ THU";

                Range rowText_PhuTrachBHXH = oSheet.get_Range("H" + rowCnt.ToString());
                rowText_PhuTrachBHXH.Font.Bold = true;
                rowText_PhuTrachBHXH.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_PhuTrachBHXH.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_PhuTrachBHXH.Value2 = "PHỤ TRÁCH BHXH";

                Range rowText_NGUOI_LAO_BIEU = oSheet.get_Range("J" + rowCnt.ToString(), "L" + rowCnt.ToString());
                rowText_NGUOI_LAO_BIEU.Merge();
                rowText_NGUOI_LAO_BIEU.Font.Bold = true;
                rowText_NGUOI_LAO_BIEU.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_NGUOI_LAO_BIEU.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_NGUOI_LAO_BIEU.Value2 = "NGƯỜI LẬP BIỂU";

                Range rowText_NGUOI_SU_DUNG = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowText_NGUOI_SU_DUNG.Merge();
                rowText_NGUOI_SU_DUNG.Font.Bold = true;
                rowText_NGUOI_SU_DUNG.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_NGUOI_SU_DUNG.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_NGUOI_SU_DUNG.Value2 = "NGƯỜI SỬ DỤNG";

                rowCnt++;

                Range rowText_QuyLuong = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_QuyLuong.Value2 = "3. Quỹ lương BHXH";

                Range rowData_QuyLuong = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_QuyLuong.Value2 = "=N" + (keeprowCnt + 1);

                Range rowText_Ky1 = oSheet.get_Range("E" + rowCnt.ToString(), "F" + rowCnt.ToString());
                rowText_Ky1.Merge();
                rowText_Ky1.Font.Italic = true;
                rowText_Ky1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky1.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky2 = oSheet.get_Range("H" + rowCnt.ToString());
                rowText_Ky2.Font.Italic = true;
                rowText_Ky2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky2.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky3 = oSheet.get_Range("J" + rowCnt.ToString(), "L" + rowCnt.ToString());
                rowText_Ky3.Merge();
                rowText_Ky3.Font.Italic = true;
                rowText_Ky3.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky3.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky3.Value2 = "(Ký, ghi rõ họ tên)";

                Range rowText_Ky4 = oSheet.get_Range("N" + rowCnt.ToString(), "O" + rowCnt.ToString());
                rowText_Ky4.Merge();
                rowText_Ky4.Font.Italic = true;
                rowText_Ky4.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowText_Ky4.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                rowText_Ky4.Value2 = "(Ký, ghi rõ họ tên)";

                rowCnt++;

                Range rowText_4 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_4.Value2 = "4. BHXH phải đóng";

                Range rowData_4 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_4.Value2 = "-2302012.5";

                rowCnt++;
                Range rowText_5 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_5.Value2 = "5. Trừ 2% đơn vị giữ lại";

                Range rowData_5 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_5.Value2 = "";

                rowCnt++;
                Range rowText_6 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_6.Value2 = "6. Quỹ lương BHYT";

                Range rowData_6 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_6.Value2 = "=O" + (keeprowCnt + 1);

                rowCnt++;
                Range rowText_7 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_7.Value2 = "7. BHYT phải đóng";

                Range rowData_7 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_7.Value2 = "(2,03)";

                rowCnt++;
                Range rowText_8 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_8.Value2 = "8. Quỹ lương BHTN";

                Range rowData_8 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_8.Value2 = "=P" + (keeprowCnt + 1);

                rowCnt++;
                Range rowText_9 = oSheet.get_Range("B" + rowCnt.ToString());
                rowText_9.Value2 = "9. BHTN phải đóng";

                Range rowData_9 = oSheet.get_Range("C" + rowCnt.ToString());
                rowData_9.Value2 = "(2,03)";

                //fomart All 
                formatRange = oSheet.get_Range("A11", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.WrapText = true;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
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
        private string RangeAddress(Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
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
    }
}