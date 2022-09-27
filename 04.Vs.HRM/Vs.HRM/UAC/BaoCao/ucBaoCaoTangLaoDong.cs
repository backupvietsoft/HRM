using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoTangLaoDong : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoTangLaoDong()
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
                                    frm = new frmViewReport();
                                    frm.rpt = new rptBCTangLaoDongThang(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTangLaoDongThang", conn);

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
                                    }
                                    catch
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    DateTime firstDateTime = new DateTime(Convert.ToInt32(txtNam.EditValue), 1, 1);
                                    DateTime secondDateTime = new DateTime(Convert.ToInt32(txtNam.EditValue), 6, 30);
                                    string sTieuDe = "BÁO CÁO TĂNG LAO ĐỘNG 6 THÁNG ĐẦU NĂM " + Convert.ToString(txtNam.EditValue);

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
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 0;
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
                                    DateTime firstDateTime2 = new DateTime(Convert.ToInt32(txtNam.EditValue), 7, 1);
                                    DateTime secondDateTime2 = new DateTime(Convert.ToInt32(txtNam.EditValue), 12, 31);
                                    string sTieuDe2 = "BÁO CÁO TĂNG LAO ĐỘNG 6 THÁNG CUỐI NĂM " + Convert.ToString(txtNam.EditValue);

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
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 0;
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
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoTangLaoDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dTuNgay.EditValue = Convert.ToDateTime(("01/"+DateTime.Today.Month +"/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            txtNam.EditValue = DateTime.Today.Year;
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

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {
                        dTuNgay.Enabled = true;
                        dDenNgay.Enabled = true;
                        txtNam.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        dTuNgay.EditValue = new DateTime((calNam.DateTime.Date.Year), 1, 1);
                        dDenNgay.EditValue = new DateTime(calNam.DateTime.Date.Year, 6, 30);
                        dTuNgay.Enabled = false;
                        dDenNgay.Enabled = false;
                        txtNam.Enabled = true;
                    }
                    break;
                case 2:
                    {
                        dTuNgay.EditValue = new DateTime(calNam.DateTime.Date.Year, 7, 1);
                        dDenNgay.EditValue = new DateTime(calNam.DateTime.Date.Year, 12, 31);
                        dTuNgay.Enabled = false;
                        txtNam.Enabled = true;
                        dDenNgay.Enabled = false;
                    }
                    break;

                default:
                    dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).ToShortDateString();
                    dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1).ToShortDateString();
                    dTuNgay.Enabled = true;
                    dDenNgay.Enabled = true;
                    break;
            }
        }

        private void mPopupContainerEdit1_BeforePopup(object sender, EventArgs e)
        {
            popNam.Width = calNam.Width;
            popNam.Height = calNam.Height;
        }

        private void calNam_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                txtNam.EditValue = calNam.DateTime.Date.Year;
                if (rdo_ChonBaoCao.SelectedIndex==1)
                {
                    dTuNgay.EditValue = "01/01/" + calNam.DateTime.Date.Year;
                    dDenNgay.EditValue = "30/06/" + calNam.DateTime.Date.Year;
                }
                else
                {
                    dTuNgay.EditValue = "01/07/" + calNam.DateTime.Date.Year;
                    dDenNgay.EditValue = "31/12/" + calNam.DateTime.Date.Year;
                }
            }
            catch
            {
            }
            txtNam.ClosePopup();
        }
    }
}