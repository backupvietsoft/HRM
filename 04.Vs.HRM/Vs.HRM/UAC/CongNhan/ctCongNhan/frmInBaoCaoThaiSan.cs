using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Microsoft.ApplicationBlocks.Data;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Globalization;
using Vs.Report;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.Charts.Native;
using System.Windows.Automation.Peers;
using DevExpress.XtraLayout.Filtering.Templates;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils.Commands;
using DevExpress.PivotGrid.Design;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Xml;
using DevExpress.Utils;
using DevExpress.DataAccess.Native.ObjectBinding;

namespace Vs.HRM
{
    public partial class frmInBaoCaoThaiSan : DevExpress.XtraEditors.XtraForm
    {
        private DateTime ThangBC = new DateTime(DateTime.Now.Year, 1, 1);
        private readonly int DV;
        private readonly int TO;
        private readonly int XN;
        private readonly int TT;
        public frmInBaoCaoThaiSan(DateTime Thang, int DV, int TO, int XN, int TT)
        {
            InitializeComponent();
            if (Commons.Modules.KyHieuDV == "NB")
            {
                datTuNgay.Properties.Mask.EditMask = "MM/yyyy";
                datDenNgay.Visible = false;
                lblDenNgay.Visible = false;
            }

            this.DV = DV;
            this.TO = TO;
            this.XN = XN;
            this.TT = TT;
            this.ThangBC = Thang;


        }

        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            rad_ChonBaoCao.SelectedIndex = 0;

            DateTime tungay = Convert.ToDateTime(DateTime.Now);
 
            datTuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
           
        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rad_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                if (Commons.Modules.KyHieuDV == "NB")
                                {
                                    InExcelDanhSachMangThai();
                                }
                                else
                                {
                                    InDanhSachMangThai();
                                }
                                break;
                            case 1:
                                InDanhSachTheoDoiCheDoKhamThai();
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

        private void InDanhSachMangThai()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                System.Data.SqlClient.SqlConnection conn;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTime ChonThang = datTuNgay.DateTime;
                string Ngay = ChonThang.Day.ToString();
                string Thang1 = ChonThang.Month.ToString();
                string Nam = ChonThang.Year.ToString();
                frm.rpt = new rptBCDangKyThaiSan_NB(Ngay, Thang1, Nam);
                DataTable dt = new DataTable();

                DateTime firstDayOfMonth = DateTime.Today;
                DateTime lastDayOfMonth = DateTime.Today;
                switch (Commons.Modules.KyHieuDV)
                {
                    case "NB":
                        {
                            DateTime date = datTuNgay.DateTime;
                            firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            break;
                        }
                    default:
                        {
                            firstDayOfMonth = Convert.ToDateTime(datTuNgay.EditValue);
                            lastDayOfMonth = Convert.ToDateTime(datDenNgay.EditValue);
                            break;
                        }
                }

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
            
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachMangThai_NB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = this.DV;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = this.XN;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = this.TO;
                cmd.Parameters.Add("@RadTH", SqlDbType.Int).Value = this.TT;
                cmd.Parameters.Add("@tNgay", SqlDbType.DateTime).Value = firstDayOfMonth;
                cmd.Parameters.Add("@dNgay", SqlDbType.DateTime).Value = lastDayOfMonth;
                cmd.Parameters.Add("@sKyHieu", SqlDbType.NVarChar).Value = Commons.Modules.KyHieuDV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";

                frm.AddDataSource(dt);

                frm.ShowDialog();
            }
            catch { }
            
        }


        private void InExcelDanhSachMangThai()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTime ChonThang = datTuNgay.DateTime;
                string Ngay = ChonThang.Day.ToString();
                string Thang = ChonThang.Month.ToString();
                string Nam = ChonThang.Year.ToString();
                DateTime firstDayOfMonth = DateTime.Today;
                DateTime lastDayOfMonth = DateTime.Today;

                switch (Commons.Modules.KyHieuDV)
                {
                    case "NB":
                        {
                            DateTime date = datTuNgay.DateTime;
                            firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                            lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            break;
                        }
                    default:
                        {
                            firstDayOfMonth = Convert.ToDateTime(datTuNgay.EditValue);
                            lastDayOfMonth = Convert.ToDateTime(datDenNgay.EditValue);
                            break;
                        }
                }

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachMangThai_NB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = this.DV;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = this.XN;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = this.TO;
                cmd.Parameters.Add("@RadTH", SqlDbType.Int).Value = this.TT;
                cmd.Parameters.Add("@tNgay", SqlDbType.DateTime).Value = firstDayOfMonth;
                cmd.Parameters.Add("@dNgay", SqlDbType.DateTime).Value = lastDayOfMonth;
                cmd.Parameters.Add("@sKyHieu", SqlDbType.NVarChar).Value = Commons.Modules.KyHieuDV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                try
                {
                    string sDDFile = Commons.Modules.ObjSystems.CapnhatTL("");
                    if (sDDFile != "\\")
                        sFileName = sDDFile + "\\" + sFileName;
                }
                catch { }

                System.IO.FileInfo file = new System.IO.FileInfo(sFileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                //rptBCDangKyThaiSan_NB
                ExcelPackage pck = new ExcelPackage(file);
                var ws1 = pck.Workbook.Worksheets.Add(Commons.Modules.ObjLanguages.GetLanguage("rptBCDangKyThaiSan_NB", "lblTIEU_DE"));

                Commons.Modules.MExcel.MTTChung(ws1, 1, 1, 0, 0);
                int iDong = 4;
                Commons.Modules.MExcel.MText(ws1, "rptBCDangKyThaiSan_NB", "lblTIEU_DE", iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong++;
                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Thang") + " " + Thang + " " + Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Nam") + " " + Nam, iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong = iDong + 2;

                List<List<Object>> WidthColumns = new List<List<Object>>();
                List<Object> WidthColumnsName = new List<Object>();

                WidthColumnsName = new List<Object>() { "STT", 5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "HO_TEN", 20 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MS_THE_CC", 10, 0 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "BO_PHAN", 15};
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_MANG_THAI", 15, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_DANG_KY", 15, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "GHI_CHU", 15 };
                WidthColumns.Add(WidthColumnsName);
        

                ws1.Cells[iDong, 1].LoadFromDataTable(dt, true);
                Commons.Modules.MExcel.MFormatExcel(ws1, dt, iDong, "rptBCDangKyThaiSan_NB", WidthColumns, true, true, true);

        
                //Format
                for(int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "STT":
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "MS_THE_CC":
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            try { ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.Numberformat.Format = "0"; } catch { }
                            break;
                        case "NGAY_MANG_THAI":
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "NGAY_DANG_KY":
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                    }
                }

                ws1.Cells[1, 1, 10 + dt.Rows.Count, dt.Columns.Count + 1].Style.Font.Name = "Times New Roman";
                ws1.Cells[6, 1, 7 + dt.Rows.Count, dt.Columns.Count + 1].Style.Font.Size = 10;

                iDong = iDong + dt.Rows.Count + 1;
                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Ngay") + " " + Ngay + " " + Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Thang") + " " + Thang + " " + Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Nam") + " " + Nam, iDong + 1, dt.Columns.Count - 2, iDong + 1, dt.Columns.Count, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                Commons.Modules.MExcel.MText(ws1, "rptBCDangKyThaiSan_NB", "sNguoiLapBieu", iDong + 2, dt.Columns.Count - 2, iDong + 2, dt.Columns.Count, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                if (file.Exists)
                    file.Delete();
                pck.SaveAs(file);
                System.Diagnostics.Process.Start(file.FullName);

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void InDanhSachTheoDoiCheDoKhamThai()
        {
            frmViewReport frm = new frmViewReport();
            System.Data.SqlClient.SqlConnection conn;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTime ChonThang = datTuNgay.DateTime;

            string Ngay = ChonThang.Day.ToString();
            string Thang1 = ChonThang.Month.ToString();
            string Nam = ChonThang.Year.ToString();
            frm.rpt = new rptDSTheoDoiCheDoKhamThai_NB(Ngay, Thang1, Nam);

            DataTable dt = new DataTable();

            try
            {
                int Thang = ChonThang.Month;

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListTheoDoiCheDoKhamThai", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = this.DV;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = this.XN;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = this.TO;
                cmd.Parameters.Add("@RadTH", SqlDbType.Int).Value = this.TT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            frm.ShowDialog();
        }

        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime denngay = Convert.ToDateTime(DateTime.Now).AddMonths(+1);
                datDenNgay.EditValue = Convert.ToDateTime("01/" + denngay.Month + "/" + denngay.Year).AddDays(-1);
            }
            catch { }
        }

        private void lblTuNgay_Click(object sender, EventArgs e)
        {

        }
    }
}