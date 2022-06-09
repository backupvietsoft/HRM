using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.Payroll
{
    public partial class frmInDanhSachTamUng : DevExpress.XtraEditors.XtraForm
    {
        private int dot;
        private int to;
        private DateTime thang;

        public frmInDanhSachTamUng(int dot, DateTime thang, int to)
        {
            InitializeComponent();
          
            this.dot = dot;
            this.thang = thang;
            this.to = to;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

      

        //sự kiên load form
        private void formInHopDongCN_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
         
            
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
                        DataTable dt = new DataTable();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    frm.rpt = new rptDanhSachUngLuong(thang);

                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachTamUng", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = dot;
                                    cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = thang;
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
                                break;
                            case 1:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    String lblTUCK = "BẢNG TẠM ỨNG CHUYỂN KHOẢN";
                                    dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    frm.rpt = new rptDanhSachUngLuongCK(thang, lblTUCK);

                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachTamUngCK", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = dot;
                                    cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = thang;
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
                                break;
                            case 2:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    String lblTUCK = "BẢNG TỔNG HỢP TẠM ỨNG TIỀN MẶT";
                                    dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    frm.rpt = new rptTongHopTamUng(thang, lblTUCK);

                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangTongHopTamUngTienMat", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@To", SqlDbType.Int).Value = to;
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = dot;
                                    cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = thang;
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
                                break;
                            case 3:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    String lblTUCK = "BẢNG TỔNG HỢP TẠM ỨNG CHUYỂN KHOẢN";
                                    dt = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    frm.rpt = new rptTongHopTamUng(thang, lblTUCK);

                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();

                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangTongHopTamUngChuyenKhoan", conn);
                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@To", SqlDbType.Int).Value = to;
                                    cmd.Parameters.Add("@Dot", SqlDbType.Int).Value = dot;
                                    cmd.Parameters.Add("@Thang", SqlDbType.DateTime).Value = thang;
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
                                break;
                        }

                        try
                        {
                            if (rdo_ChonBaoCao.SelectedIndex == 0)
                            {

                            }
                            else
                            {

                            }

                        }
                        catch
                        { }


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