using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmXepLoaiKhenThuong : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idCT;

        public frmXepLoaiKhenThuong(Int64 idCongNhan)
        {
            InitializeComponent();
            idCN = idCongNhan;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInQTCT_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dtThang.EditValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtNam.EditValue = new DateTime(DateTime.Today.Year, 1, 1);
            NgayIn.EditValue = DateTime.Today;

        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        frmViewReport frm = new frmViewReport();
                        try
                        {
                            if (rdo_ChonBaoCao.SelectedIndex == 0)
                            {
                                System.Data.SqlClient.SqlConnection conn;
                                DataTable dt = new DataTable();
                                frm = new frmViewReport();
                                frm.rpt = new rptBCXepLoaiKhenThuongThang(DateTime.Today, dtThang.DateTime);

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptXepLoaiKhenThuong", conn);

                                cmd.Parameters.Add("@NamThang", SqlDbType.Date).Value = dtThang.DateTime;
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 1;
                                cmd.CommandType = CommandType.StoredProcedure;

                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);

                            }
                            else
                            {
                                System.Data.SqlClient.SqlConnection conn;
                                DataTable dt = new DataTable();
                                frm = new frmViewReport();
                                frm.rpt = new rptBCXepLoaiKhenThuongNam(DateTime.Today, dtNam.DateTime);

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptXepLoaiKhenThuong", conn);

                                cmd.Parameters.Add("@NamThang", SqlDbType.Date).Value = dtNam.DateTime;
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value =2;

                                cmd.CommandType = CommandType.StoredProcedure;

                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);
                            }

                        }
                        catch (Exception ex)
                        {
                        }
                        frm.ShowDialog();


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

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(rdo_ChonBaoCao.SelectedIndex)
            {
                case -1:
                case 0:
                    dtThang.Enabled = true;
                    dtNam.Enabled = false;
                    break;
                case 1:
                    dtThang.Enabled = false;
                    dtNam.Enabled = true;
                    break;
            }
        }
    }
}