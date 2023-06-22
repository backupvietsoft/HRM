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
                                InDanhSachMangThai();
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