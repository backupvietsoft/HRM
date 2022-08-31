using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using VS.Report;

namespace Vs.HRM
{
    public partial class frmInKhenThuongKyLuatCN : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idKT;
        public frmInKhenThuongKyLuatCN(Int64 idCongNhan, string tencn, Int64 idKThuong)
        {
            InitializeComponent();
            NONN_HoTenCN.Text = tencn.ToUpper();
            idCN = idCongNhan;
            idKT = idKThuong;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInKhenThuongKyLuatCN_Load(object sender, EventArgs e)
        {
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);

            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            dDenNgay.EditValue = DateTime.Today;
            int SoNgay = DateTime.Today.Day - 1;
            dTuNgay.EditValue = DateTime.Today.AddDays(-SoNgay);

            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(0);
            }

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
                            int n = rdo_ChonBaoCao.SelectedIndex;
                            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM") n = 2;
                            switch (n)
                            {
                                case 0:
                                    {
                                        switch (Commons.Modules.ObjSystems.KyHieuDV_CN(idCN))
                                        {
                                            case "MT":
                                                {
                                                    KhenThuong();
                                                    break;
                                                }
                                            default:
                                                KhenThuong();
                                                break;
                                        }

                                        break;
                                    }
                                case 1:
                                    {
                                        switch (Commons.Modules.ObjSystems.KyHieuDV_CN(idCN))
                                        {
                                            case "MT":
                                                {
                                                    KyLuat();
                                                    break;
                                                }
                                            default:
                                                KyLuat();
                                                break;
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        switch (Commons.Modules.ObjSystems.KyHieuDV_CN(idCN))
                                        {
                                            case "MT":
                                                {
                                                    BienBanCanhCao();
                                                    break;
                                                }
                                            default:
                                                BienBanCanhCao();
                                                break;
                                        }

                                        break;
                                    }
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
        private void KhenThuong()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptBCKhenThuongKyLuatCN(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatCN", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = dTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dDenNgay.DateTime;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdo_ChonBaoCao.SelectedIndex;
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
            catch { }
        }
        private void KyLuat()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptBCKhenThuongKyLuatCN(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatCN", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = dTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dDenNgay.DateTime;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdo_ChonBaoCao.SelectedIndex;
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
            catch { }
        }
        private void BienBanCanhCao()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptBCBienBanCanhCao(dNgayIn.DateTime);

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBienBanCanhCao", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
            cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idKT;
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
    }
}