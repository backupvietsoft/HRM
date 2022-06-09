using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Report
{
    public partial class rptHopDongLaoDong_MT : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHopDongLaoDong_MT(DateTime ngayin)
        {   //DateTime ngayin
            InitializeComponent();


            //System.Data.SqlClient.SqlConnection conn;
            //DataTable dt = new DataTable();

            //try
            //{
            //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //    conn.Open();

            //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhLuongCN", conn);

            //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            //    cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = 161;
            //    cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = 13;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adp.Fill(ds);

            //    dt = new DataTable();
            //    dt = ds.Tables[1].Copy(); 

            //    DateTime NgayHL = Convert.ToDateTime(dt.Rows[0]["NGAY_HIEU_LUC"]);
            //    string Ngay = "0" + NgayHL.Day;
            //    string Thang = "0" + NgayHL.Month;
            //    string Nam = "00" + NgayHL.Year;

            //    lb0.Text = dt.Rows[0]["TEN_DV"].ToString().ToUpper();
            //    lb1.Text = dt.Rows[0]["SO_QUYET_DINH"].ToString();
            //    lb2.Text = "GIÁM ĐỐC " + dt.Rows[0]["TEN_DV"].ToString().ToUpper();
            //    lb3.Text = "          - Căn cứ Điều lệ tổ chức và hoạt động của " + dt.Rows[0]["TEN_DV"].ToString() + " quy định nhiệm vụ, chức năng và quyền hạn của Giám đốc.";
            //    lb4.Text = "          - Căn cứ vào Quy chế lương của " + dt.Rows[0]["TEN_DV"].ToString();
            //    lb5.Text = "          - Căn cứ những đóng góp thực tế của Ông/Bà " + dt.Rows[0]["HO_TEN"].ToString() + " đối với sự phát triển của Công ty.";
            //    lb6.Text = "           <b>Điều 1. </b> Điều chỉnh mức lương kể từ ngày " + Ngay.Substring(Ngay.Length - 2, 2) + " tháng " + Thang.Substring(Thang.Length - 2, 2) + " năm " + Nam.Substring(Nam.Length - 4, 4) + " đến khi có quyết định điều chỉnh mức lương mới đối với :";
            //    lb7.Text = dt.Rows[0]["HO_TEN"].ToString();
            //    lb8.Text = Convert.ToDateTime(dt.Rows[0]["NGAY_SINH"]).ToString("dd/MM/yyyy");
            //    lb9.Text = dt.Rows[0]["DIA_CHI_THUONG_TRU"].ToString();
            //    lb10.Text = dt.Rows[0]["MS_CN"].ToString();
            //    lb11.Text = dt.Rows[0]["CHUC_VU"].ToString();
            //    lb12.Text = dt.Rows[0]["TEN_TO"].ToString();
            //    lb13.Text = Convert.ToInt32(dt.Rows[0]["ML"]).ToString("#,#");
            //    lb14.Text = Convert.ToInt32(dt.Rows[0]["PHU_CAP"]).ToString("#,#");
            //    lb15.Text = "           <b>Điều 2. </b> Ông/bà " + dt.Rows[0]["HO_TEN"].ToString() + " và các Phòng Hành chính nhân sự, Phòng Kế toán và các Phòng có liên quan có trách nhiệm thi hành Quyết định này.";
            //    lb16.Text = dt.Rows[0]["CV_NK"].ToString();
            //    lb17.Text = dt.Rows[0]["HO_TEN_NK"].ToString();
            //}
            //catch
            //{ }

            string NgayBC = "0" + ngayin.Day;
            string ThangBC = "0" + ngayin.Month;
            string NamBC = "00" + ngayin.Year;

            //lbNgay.Text = "Tp.HCM, Ngày " + NgayBC.Substring(NgayBC.Length - 2, 2) + " Tháng " + ThangBC.Substring(ThangBC.Length - 2, 2) + " Năm " + NamBC.Substring(NamBC.Length - 4, 4);


        }

    }
}


