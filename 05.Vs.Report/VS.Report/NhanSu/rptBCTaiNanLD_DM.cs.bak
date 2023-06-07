using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace VS.Report
{
    public partial class rptBCTaiNanLD_DM : DevExpress.XtraReports.UI.XtraReport
    {
        private int iID_DV = -1;
        private int iNam = -1;
        private int iLoai = -1;

        public rptBCTaiNanLD_DM(string sTieuDe, DateTime NgayIn, int ID_DV, int Nam, int Loai)
        {
            InitializeComponent();
            lblKyHanBC.Text = sTieuDe;
            lblNgayBaoCao.Text = "Ngày báo cáo: " + NgayIn.ToString("dd/MM/yyyy");
            iID_DV = ID_DV;
            iNam = Nam;
            iLoai = Loai;
        }

        private void rptBCTaiNanLD_DM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptThongKeTaiNanLD6Thang", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DV", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = iNam;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = iLoai;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);


                //do ngƯời sử dụng
                DataTable dtHDLD = new DataTable();
                dtHDLD = ds.Tables[1].Copy();
                dtHDLD.TableName = "DATA1";
                //// III Hợp đồng lao động
                this.xrSubreport1.ReportSource = new sprtDoNguoiSDLaoDong(dtHDLD);

                //Do người lao động
                DataTable dtQTLV = new DataTable();
                dtQTLV = ds.Tables[2].Copy();
                dtQTLV.TableName = "DATA2";
                this.xrSubreport2.ReportSource = new sprtDoNguoiLaoDong(dtQTLV);



                // khách quan khó tránh
                DataTable dtQTLuong = new DataTable();
                dtQTLuong = ds.Tables[3].Copy();
                dtQTLuong.TableName = "DATA3";
                this.xrSubreport3.ReportSource = new sprtKhachQuanKhoTranh(dtQTLuong);


                // tai nạn đc coi là tnlđ
                DataTable dtQTDT = new DataTable();
                dtQTDT = ds.Tables[4].Copy();
                dtQTDT.TableName = "DATA4";
                this.xrSubreport4.ReportSource = new sprtTaiNanLaoDong(dtQTDT);
            }
            catch { }
        }
    }
}
