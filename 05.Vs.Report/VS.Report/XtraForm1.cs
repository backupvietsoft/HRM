using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using Vs.Report;

namespace Vs.Report
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
            Commons.Modules.UserName = "admin";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ////////////////frmViewReport frm = new frmViewReport();
            ////////////////frm.rpt = new rptTienThuongXepLoai();
            ////////////////string sCo = @"Server=.;database=VS_HRM;uid=sa;pwd=123;Connect Timeout=9999;";
            //////////////////string sCo = @"Server=.;database=VS_HRM;uid=sa;pwd=123;Connect Timeout=9999;";
            ////////////////DataTable dt = new DataTable();
            ////////////////dt.Load(SqlHelper.ExecuteReader(sCo, "spTienThuongXepLoai"));
            ////////////////dt.TableName = "AAARE";
            ////////////////frm.AddDataSource(dt);



            ////////////////dt = new DataTable();
            ////////////////dt.Load(SqlHelper.ExecuteReader(sCo, CommandType.Text, @"SELECT 
            ////////////////        'tdBaoCao' AS tdBaoCao,'CHXHCNVN' AS CHXHCNVN,'DLTDHP' AS DLTDHP,'STT' AS STT,'MS_CN' AS MS_CN,'HO_TEN' AS HO_TEN,'MucThuong' AS MucThuong,'XepLoaiThuong' AS 	XepLoaiThuong,
            ////////////////        'HeSoThuong' AS 	HeSoThuong,'SoTienThuong' AS  SoTienThuong,'KyNhan' AS  KyNhan,'TongCong' AS  TongCong,'BangChu' AS  BangChu,'LapBang' AS  LapBang,
            ////////////////        'TPTCHC' AS  TPTCHC,'KTTruong' AS  KTTruong,'GiamDoc' AS  GiamDoc,'NgayThangNam' NgayThangNam
            ////////////////"));
            ////////////////dt.TableName = "NNGU";
            ////////////////frm.AddDataSource(dt);
            ////////////////frm.rpt = new rptTienThuongXepLoai();
            ////////////////frm.Show();




            //////////////frmViewReport frm = new frmViewReport();
            //////////////frm.rpt = new rptDSDonVi();
            //////////////DataTable dt = new DataTable();
            //////////////dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptDSDonVi", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            //////////////dt.TableName = "DATA";
            //////////////frm.AddDataSource(dt);
            //////////////frm.Show();



            

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            //frm.rpt = new rptDSDonVi();
            //frm.rpt = new rptDSThamGiaCongDoan();
            frm.rpt = new rptHopDongThuViec(dNgayIn.DateTime);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSDonVi", conn);
                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSThamGiaCongDoan", conn);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = 161;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = 14;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "TTC";
                frm.AddDataSource(dt);


            }
            catch
            { }


            frm.ShowDialog();

        }



        private void LoadNN()
        { }
    
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            dNgayIn.EditValue = DateTime.Today;
        }
    }

}





////////////////dt.Load(SqlHelper.ExecuteReader(sCo, CommandType.Text, " SELECT * FROM dbo.CAU_TRUC_THIET_BI"));
////////////////            dt.TableName = "CAU_TRUC_THIET_BI";
////////////////            frm.AddDataSource(dt);

////////////////            dt = new DataTable();
////////////////dt.Load(SqlHelper.ExecuteReader(sCo, CommandType.Text, "SELECT * FROM dbo.MAY"));
////////////////            dt.TableName = "MAY";
////////////////            frm.AddDataSource(dt);


//////////////////            frm.dsReport.Relations.Add("MAYCAU_TRUC_THIET_BI",
//////////////////frm.dsReport.Tables["MAY"].Columns["MS_MAY"],
//////////////////frm.dsReport.Tables["CAU_TRUC_THIET_BI"].Columns["MS_MAY"]);