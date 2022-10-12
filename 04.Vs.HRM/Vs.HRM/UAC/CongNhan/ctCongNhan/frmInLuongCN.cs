using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInLuongCN : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idL;
        private DateTime dNgayHL;
        public frmInLuongCN(Int64 idCongNhan, Int64 idLuong, DateTime ngayhl, string tencn)
        {
            InitializeComponent();
            NONN_HoTenCN.Text = tencn.ToUpper();
            idCN = idCongNhan;
            idL = idLuong;
            dNgayHL = ngayhl;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
            }
        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                if (btn == null || btn.Tag == null) return;
                switch (btn.Tag.ToString())
                {
                    case "In":
                        {
                            int n = rdo_ChonBaoCao.SelectedIndex;
                            if (rdo_ChonBaoCao.Properties.Items.Count < 3)
                            {
                                n = (n >= 1 ? n + 1 : n);
                            }

                            switch (n)
                            {
                                case 0:
                                    {
                                        System.Data.SqlClient.SqlConnection conn;
                                        DataTable dt = new DataTable();
                                        try
                                        {

                                            frmViewReport frm = new frmViewReport();

                                            frm.rpt = new rptQuyetDinhLuongCN(dNgayIn.DateTime);

                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhLuongCN", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                                            cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idL;
                                            cmd.CommandType = CommandType.StoredProcedure;

                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);

                                            DataTable dt1 = new DataTable();
                                            dt1 = ds.Tables[1].Copy();
                                            dt1.TableName = "NOI_DUNG";
                                            frm.AddDataSource(dt1);

                                            frm.ShowDialog();

                                        }
                                        catch
                                        {

                                        }
                                        break;
                                    }
                                case 1:
                                    {

                                        System.Data.SqlClient.SqlConnection conn;
                                        DataTable dt = new DataTable();
                                        frmViewReport frm = new frmViewReport();
                                        try
                                        {
                                            frm.rpt = new rptQuaTrinhLuongCN(dNgayIn.DateTime);

                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuaTrinhLuongCN", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
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
                                        catch
                                        {

                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                        {
                                            case "DM":
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhNangLuong_DM(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong_DM", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                        DataSet ds = new DataSet();
                                                        adp.Fill(ds);
                                                        dt = new DataTable();
                                                        dt = ds.Tables[0].Copy();
                                                        dt.TableName = "DATA";
                                                        frm.AddDataSource(dt);

                                                        DataTable dt1 = new DataTable();
                                                        dt1 = ds.Tables[1].Copy();
                                                        dt1.TableName = "NOI_DUNG";
                                                        frm.AddDataSource(dt1);

                                                        frm.ShowDialog();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }
                                            case "HN":
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhLuongCN_HN(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                        DataSet ds = new DataSet();
                                                        adp.Fill(ds);
                                                        dt = new DataTable();
                                                        dt = ds.Tables[0].Copy();
                                                        dt.TableName = "DATA";
                                                        frm.AddDataSource(dt);

                                                        DataTable dt1 = new DataTable();
                                                        dt1 = ds.Tables[1].Copy();
                                                        dt1.TableName = "NOI_DUNG";
                                                        frm.AddDataSource(dt1);

                                                        frm.ShowDialog();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }

                                            default:
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhNangLuongCN(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                        DataSet ds = new DataSet();
                                                        adp.Fill(ds);
                                                        dt = new DataTable();
                                                        dt = ds.Tables[0].Copy();
                                                        dt.TableName = "DATA";
                                                        frm.AddDataSource(dt);

                                                        DataTable dt1 = new DataTable();
                                                        dt1 = ds.Tables[1].Copy();
                                                        dt1.TableName = "NOI_DUNG";
                                                        frm.AddDataSource(dt1);

                                                        frm.ShowDialog();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }
                                        }

                                        break;
                                    }
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
            catch { }
        }

    }
}