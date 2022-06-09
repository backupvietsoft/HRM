using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInNhanTienThuongXepLoai : DevExpress.XtraEditors.XtraForm
    {
        public DateTime dThang;
        public frmInNhanTienThuongXepLoai(DateTime Thang)
        {
            InitializeComponent();
            dThang = Thang;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).ToShortDateString();
            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1).ToShortDateString();
            NgayIn.EditValue = DateTime.Today;
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
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();

                            frm.rpt = new rptTienThuongXepLoaiTH(DateTime.Now, dtTuNgay.DateTime, dtDenNgay.DateTime);
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongXepLoaiTH", conn);

                            cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = dtTuNgay.DateTime;
                            cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = dtDenNgay.DateTime;
                            cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@XNghiep", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@To", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;

                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);

                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                            frm.ShowDialog();

                        }
                        catch(Exception ex)
                        {

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

    }
}