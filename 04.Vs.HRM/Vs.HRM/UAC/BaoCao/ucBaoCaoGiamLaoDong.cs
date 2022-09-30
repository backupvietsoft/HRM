using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using System.Globalization;

namespace Vs.HRM
{
    public partial class ucBaoCaoGiamLaoDong : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoGiamLaoDong()
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
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "NB":
                                            {
                                                DateTime firstDateTime = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 1, 1);
                                                DateTime secondDateTime = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 6, 30);

                                                System.Data.SqlClient.SqlConnection conn1;
                                                System.Data.DataTable dt = new System.Data.DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCGiamLaoDongThang_NB(lk_NgayIn.DateTime, dtTuNgay.DateTime, dtDenNgay.DateTime);

                                                try
                                                {
                                                    conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn1.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCGiamLaoDongThang_NB", conn1);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firstDateTime;
                                                    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondDateTime;
                                                    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 0;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    dt = new System.Data.DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DA_TA";
                                                    frm.AddDataSource(dt);

                                                    frm.ShowDialog();
                                                }
                                                catch
                                                { }
                                                break;
                                            }
                                        default:
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCGiamLaoDongThang(lk_NgayIn.DateTime, dtTuNgay.DateTime, dtDenNgay.DateTime);

                                                try
                                                {
                                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCGiamLaoDongThang", conn);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dtTuNgay.EditValue;
                                                    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtDenNgay.EditValue;
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
                                                { }
                                                frm.ShowDialog();
                                                break;
                                            }
                                    }
                                     }
                                break;
                            case 1:
                                {
                                    DateTime firstDateTime = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 1, 1);
                                    DateTime secondDateTime = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 6, 30);
                                    string sTieuDe = "BÁO CÁO GIẢM LAO ĐỘNG 6 THÁNG ĐẦU NĂM " + Convert.ToDateTime(txNam.EditValue).Year;

                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt = new DataTable();
                                    frm = new frmViewReport();
                                    frm.rpt = new rptBCTangGiamLD6Thang(lk_NgayIn.DateTime, sTieuDe);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTangGiamLD6Thang", conn1);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firstDateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondDateTime;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
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
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 2:
                                {
                                    DateTime firstDateTime2 = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 7, 1);
                                    DateTime secondDateTime2 = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 12, 31);
                                    string sTieuDe2 = "BÁO CÁO GIẢM LAO ĐỘNG 6 THÁNG CUỐI NĂM " + Convert.ToDateTime(txNam.EditValue).Year;

                                    System.Data.SqlClient.SqlConnection conn2;
                                    DataTable dt = new DataTable();
                                    frm = new frmViewReport();
                                    frm.rpt = new rptBCTangGiamLD6Thang(lk_NgayIn.DateTime, sTieuDe2);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTangGiamLD6Thang", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = firstDateTime2;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondDateTime2;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
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
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            default:
                            break;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoGiamLaoDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE"));
            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE")).AddMonths(1).AddDays(-1);
            txNam.EditValue = DateTime.Now;
            lk_NgayIn.EditValue = DateTime.Today;

            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
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

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 0:
                        {
                            dtTuNgay.Enabled = true;
                            dtDenNgay.Enabled = true;
                            txNam.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            dtTuNgay.EditValue = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 1, 1);
                            dtDenNgay.EditValue = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 6, 30);
                            dtTuNgay.Enabled = false;
                            dtDenNgay.Enabled = false;
                            txNam.Enabled = true;
                        }
                        break;
                    case 2:
                        {
                            dtTuNgay.EditValue = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 7, 1);
                            dtDenNgay.EditValue = new DateTime(Convert.ToDateTime(txNam.EditValue).Year, 12, 31);
                            dtTuNgay.Enabled = false;
                            dtDenNgay.Enabled = false;
                            txNam.Enabled = true;
                        }
                        break;

                    default:
                        dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
                        dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
                        dtTuNgay.Enabled = true;
                        dtDenNgay.Enabled = true;
                        txNam.Enabled = true;
                        break;
                }
            }
            catch
            { }
        }
    }
}
