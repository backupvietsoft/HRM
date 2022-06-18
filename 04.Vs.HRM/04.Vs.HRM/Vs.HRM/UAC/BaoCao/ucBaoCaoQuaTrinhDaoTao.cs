using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoQuaTrinhDaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoQuaTrinhDaoTao()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        frmViewReport frm = new frmViewReport();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptBCKetQuaDaoTao(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhoaDaoTao", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;

                                        cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TRUONG_DT", SqlDbType.NVarChar).Value = cbTruongDaoTao.EditValue;
                                        cmd.Parameters.Add("@LINH_VUC_DT", SqlDbType.NVarChar).Value = cbLinhVucDaoTao.EditValue;
                                        cmd.Parameters.Add("@HINH_THUC_DT", SqlDbType.NVarChar).Value = cbHinhThucDaoTao.EditValue;
                                        cmd.Parameters.Add("@ID_KDT", SqlDbType.BigInt).Value = cbKhoaDaoTao.EditValue;
                                        cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value =1;

                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DA_TA";
                                        frm.AddDataSource(dt);
                                    }
                                    catch 
                                    {
                                    }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    //  DateTime firstDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), 1);
                                    //  DateTime secondDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue)));

                                    //  string sTieuDe = "DANH SÁCH THAY ĐỔI LƯƠNG " + Convert.ToString(txtThang.EditValue);
                                    {
                                        System.Data.SqlClient.SqlConnection conn1;
                                        DataTable dt = new DataTable();
                                        frm.rpt = new rptBCThongKeDaoTao(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                        try
                                        {

                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhoaDaoTao", conn1);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;

                                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = cbCongNhan.EditValue;
                                            cmd.Parameters.Add("@TRUONG_DT", SqlDbType.NVarChar).Value = cbTruongDaoTao.EditValue;
                                            cmd.Parameters.Add("@LINH_VUC_DT", SqlDbType.NVarChar).Value = cbLinhVucDaoTao.EditValue;
                                            cmd.Parameters.Add("@HINH_THUC_DT", SqlDbType.NVarChar).Value = cbHinhThucDaoTao.EditValue;
                                            cmd.Parameters.Add("@ID_KDT", SqlDbType.BigInt).Value = cbKhoaDaoTao.EditValue;
                                            cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 2;

                                            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                        }
                                        catch (Exception ex)
                                        {
                                        }


                                        frm.ShowDialog();
                                    }
                                    break;
                                }
                            case 2:
                                {

                                    // string sTieuDe2 = "QUÁ TRÌNH LƯƠNG NHÂN VIÊN " + Convert.ToString(txtThang.EditValue);
                                    {
                                        System.Data.SqlClient.SqlConnection conn2;
                                        DataTable dt = new DataTable();
                                        frm.rpt = new rptBCQuaTrinhDaoTaoCNV(lk_NgayIn.DateTime);

                                        try
                                        {
                                            conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn2.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhoaDaoTao", conn2);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;

                                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = cbCongNhan.EditValue;
                                            cmd.Parameters.Add("@TRUONG_DT", SqlDbType.NVarChar).Value = cbTruongDaoTao.EditValue;
                                            cmd.Parameters.Add("@LINH_VUC_DT", SqlDbType.NVarChar).Value = cbLinhVucDaoTao.EditValue;
                                            cmd.Parameters.Add("@HINH_THUC_DT", SqlDbType.NVarChar).Value = cbHinhThucDaoTao.EditValue;
                                            cmd.Parameters.Add("@ID_KDT", SqlDbType.BigInt).Value = cbKhoaDaoTao.EditValue;
                                            cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 3;

                                            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                     
                                        }
                                        catch (Exception ex)
                                        {
                                        }


                                        frm.ShowDialog();
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;

                    }
                default:
                    break;
            }
        }
        private void LoadNhanSu()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = Commons.Modules.ObjSystems.DataCongNhanTheoDK(true, Convert.ToInt32(LK_DON_VI.EditValue), Convert.ToInt32(LK_XI_NGHIEP.EditValue), Convert.ToInt32(LK_TO.EditValue), Convert.ToDateTime(dTuNgay.EditValue), Convert.ToDateTime(dDenNgay.EditValue));
                if(cbCongNhan.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbCongNhan, dt, "ID_CN", "TEN_CN", "TEN_CN");
                    cbCongNhan.Properties.View.Columns[1].Visible = false;
                }
                else
                {
                    cbCongNhan.Properties.DataSource = dt;
                }
                
                cbCongNhan.EditValue = -1;
            }
            catch { }
        }
        private void ucBaoCaoQuaTrinhDaoTao_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            lk_NgayIn.EditValue = DateTime.Today;

            LoadNhanSu();

            try
            {
                Commons.Modules.ObjSystems.LoadCboTruongDaoTao(cbTruongDaoTao);
                Commons.Modules.ObjSystems.LoadCboLinhVucDaoTao(cbLinhVucDaoTao);
                Commons.Modules.ObjSystems.LoadCboHinhThucDaoTao(cbHinhThucDaoTao);
                Commons.Modules.ObjSystems.LoadCboKhoaDaoTao(cbKhoaDaoTao);
            }
            catch
            {

            }
            Commons.Modules.sLoad = "";
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadNhanSu();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            LoadNhanSu();
        }

        private void lbDenNgay_Click(object sender, EventArgs e)
        {

        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadNhanSu();
        }
    }
}
