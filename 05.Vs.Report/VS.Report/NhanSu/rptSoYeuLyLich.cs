using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptSoYeuLyLich : DevExpress.XtraReports.UI.XtraReport
    {

        public rptSoYeuLyLich(DateTime ngayin)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dtNgu = new DataTable();
            dtNgu.Load(Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'NgayThangNam' "));

            string Ngay = "0" + ngayin.Day;
            string Thang = "0" + ngayin.Month;
            string Nam = "00" + ngayin.Year;

        }

        private void rptSoYeuLyLich_AfterPrint(object sender, EventArgs e)
        {
          
        }

        private void rptSoYeuLyLich_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptSoYeuLyLich", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                //Hop dong lao dong
                DataTable dtHDLD = new DataTable();
                dtHDLD = ds.Tables[1].Copy();
                dtHDLD.TableName = "DATA1";
                //// III Hợp đồng lao động
                this.xrSubreport4.ReportSource = new srptHopDongLaoDong(dtHDLD);

                //Qua trinh lam viec
                DataTable dtQTLV = new DataTable();
                dtQTLV = ds.Tables[2].Copy();
                dtQTLV.TableName = "DATA2";
                this.xrSubreport1.ReportSource = new srptQuaTrinhLamViec(dtQTLV);
                


                // Qua trinh luong
                DataTable dtQTLuong = new DataTable();
                dtQTLuong = ds.Tables[3].Copy();
                dtQTLuong.TableName = "DATA3";
                this.xrSubreport2.ReportSource = new srptQuaTrinhLuong(dtQTLuong);


                // Qua trinh dao tao
                DataTable dtQTDT = new DataTable();
                dtQTDT = ds.Tables[4].Copy();
                dtQTDT.TableName = "DATA4";
                this.xrSubreport3.ReportSource = new srptQuaTrinhDaoTao(dtQTDT);

                // Qua trinh khen thuong
                DataTable dtQTKT = new DataTable();
                dtQTKT = ds.Tables[5].Copy();
                dtQTKT.TableName = "DATA5";
                this.xrSubreport5.ReportSource = new srptQuaTrinhKhenThuong(dtQTKT);

                // Qua trinh ky luat
                DataTable dtQTKL = new DataTable();
                dtQTKL = ds.Tables[6].Copy();
                dtQTKL.TableName = "DATA6";
                this.xrSubreport6.ReportSource = new srptQuaTrinhKyLuat(dtQTKL);

                // Qua trinh danh gia
                DataTable dtQTDG = new DataTable();
                dtQTDG = ds.Tables[7].Copy();
                dtQTDG.TableName = "DATA7";
                this.xrSubreport7.ReportSource = new srptQuaTrinhDanhGia(dtQTDG);

                // Quan he gia dinh
                DataTable dtQHGD = new DataTable();
                dtQHGD = ds.Tables[8].Copy();
                dtQHGD.TableName = "DATA8";
                this.xrSubreport8.ReportSource = new srptQuanHeGiaDinh(dtQHGD);

            }
            catch
            {

            }
        }
    }
}


