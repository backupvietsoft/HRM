using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using VS.Report;

namespace Vs.HRM
{
    public partial class ucBangLuongThangNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBangLuongThangNhanVien()
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
                                    frm.rpt = new rptBangLuongThangNhanVien(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangLuongThangNhanVien", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
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
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
                                        frm.rpt = new rptDSNhanVienThayDoiLuongGD(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                        try
                                        {

                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNhanVienThayDoiLuongGD", conn1);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                            //    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                        }
                                        catch
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
                                        frm.rpt = new rptBCQuaTrinhLuongTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                        try
                                        {
                                            conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn2.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCQuaTrinhLuongTH", conn2);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                            //    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                        }
                                        catch (Exception ex)
                                        {
                                        }

                                        frm.ShowDialog();
                                    }
                                    break;
                                }

                            case 3:
                                {

                                    string sNam = Commons.Modules.TypeLanguage == 1 ? "YEAR " : "NĂM " + datNam.DateTime.Year + "";
                                    {
                                        System.Data.SqlClient.SqlConnection conn2;
                                        DataTable dt = new DataTable();
                                        frm.rpt = new rptBCDienBienLuongThangNam(sNam, Convert.ToDateTime(lk_NgayIn.EditValue));

                                        try
                                        {
                                            conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn2.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDienBienLuongThangNam", conn2);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = Convert.ToInt64(LK_DON_VI.EditValue);
                                            cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = Convert.ToInt64(LK_XI_NGHIEP.EditValue);
                                            cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = Convert.ToInt64(LK_TO.EditValue);
                                            cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = datNam.DateTime.Year;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DATA";
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

        private void ucBangLuongThangNhanVien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datNam.DateTime = DateTime.Now.AddYears(-1);
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            dtThang.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";

        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
        }

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }

        private void dtThang_Validated(object sender, EventArgs e)
        {
            try
            {
                DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
                dTuNgay.EditValue = firstDateTime;
                int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
                DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
                dDenNgay.EditValue = secondDateTime;
            }
            catch
            {

            }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 3:
                        {
                            dtThang.Enabled = false;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                            datNam.Enabled = true;
                            break;
                        }
                    default:
                        datNam.Enabled = false;
                        dtThang.Enabled = true;
                        dTuNgay.Enabled = true;
                        dDenNgay.Enabled = true;
                        break;
                }
            }
            catch { }
        }
    }
}
