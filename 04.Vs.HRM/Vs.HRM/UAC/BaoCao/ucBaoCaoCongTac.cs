using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoCongTac : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoCongTac()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,windowsUIButton);
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
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCQuaTrinhCongTacTH_NB(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                                try
                                                {
                                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCQuaTrinhCongTacTH_NB", conn);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
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
                                                break;
                                            }
                                        default:
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCQuaTrinhCongTacTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                                try
                                                {
                                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCQuaTrinhCongTacTH", conn);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
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
                                                break;
                                            }
                                    }
                                    
                                }
                                break;
                            case 1:
                                {
                                    //  DateTime firstDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), 1);
                                    //  DateTime secondDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue)));

                                    //  string sTieuDe = "DANH SÁCH THAY ĐỔI LƯƠNG " + Convert.ToString(txtThang.EditValue);

                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptDSCBNVDieuChuyen(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSCBCNVDieuChuyen", conn1);

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
                                    }
                                    catch 
                                    {
                                    }


                                    frm.ShowDialog();

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

        private void ucBaoCaoCongTac_Load(object sender, EventArgs e)
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
            dtThang.EditValue = DateTime.Today;
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

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }
        private void dtThang_EditValueChanged(object sender, EventArgs e)
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

        private void dtThang_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
            //    dTuNgay.EditValue = firstDateTime;
            //    int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
            //    DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
            //    dDenNgay.EditValue = secondDateTime;
            //}
            //catch
            //{

            //}
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 0:
                        {
                            dtThang.Enabled = false;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            dtThang.Enabled = true;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;
                    
                    default:
                        dtThang.Enabled = true;
                        dTuNgay.Enabled = true;
                        dDenNgay.Enabled = true;
                        break;
                }
            }
            catch
            { }
        }

       
    }
}
