using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoThongKeCongNhanBD : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoThongKeCongNhanBD()
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

                        string NamBC;
                        NamBC = "";
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        switch (rdoChonBC.SelectedIndex)
                        {
                            case 0:
                                {
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoPhanLoai", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);

                                        

                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA_GT";
                                        //frm.AddDataSource(dt);

                                        DataTable dt1 = new DataTable();
                                        dt1 = ds.Tables[1].Copy();
                                        dt1.TableName = "DATA_LCV";
                                        //frm.AddDataSource(dt1);

                                        DataTable dt2 = new DataTable();
                                        dt2 = ds.Tables[2].Copy();
                                        dt2.TableName = "DATA_IDD";
                                        //frm.AddDataSource(dt2);

                                        frm.rpt = new rptBieuDoPhanLoai(dt,dt1,dt2, 2021);
                                    }
                                    catch
                                    { }

                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoChiaTheoDiaLy", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);

                                        DataTable dt1 = new DataTable();
                                        dt1 = ds.Tables[1].Copy();
                                        dt1.TableName = "DATA_PX";
                                        frm.AddDataSource(dt1);

                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA_Q";
                                        //frm.AddDataSource(dt);

                                        

                                        frm.rpt = new rptBieuDoChiaTheoDiaLy(dt, dt1);
                                    }
                                    catch
                                    { }

                                    frm.ShowDialog();
                                }
                                break;

                            case 2:
                                {
                                    
                                }
                                break;
                            default: break;
                        }
                        //if (rdoChonBC.SelectedIndex != 3)
                        //    frm.ShowDialog();

                    }
                    break;

                default:
                    break;
            }
        }

        private void ucBaoCaoThongKeCongNhanBD_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            lk_NgayIn.EditValue = DateTime.Today;
            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            txNam.Text = (DateTime.Today.Year.ToString());
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
            //try
            //{
            //    switch (rdo_ChonBaoCao.SelectedIndex)
            //    {
            //        case 0:
            //            {
            //                dtTuNgay.Enabled = true;
            //                dtDenNgay.Enabled = true;
            //                txNam.Enabled = false;
            //            }
            //            break;
                   
            //        case 1:
            //            {
            //                dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 1, 1);
            //                dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 6, 30);
            //                dtTuNgay.Enabled = false;
            //                dtDenNgay.Enabled = false;
            //                txNam.Enabled = true;
            //            }
            //            break;
            //        case 2:
            //            {
            //                dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 7, 1);
            //                dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 12, 31);
            //                dtTuNgay.Enabled = false;
            //                dtDenNgay.Enabled = false;
            //                txNam.Enabled = true;
            //            }
            //            break;

            //        default:
            //            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            //            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            //            dtTuNgay.Enabled = true;
            //            dtDenNgay.Enabled = true;
            //            break;
            //    }
            //}
            //catch
            //{ }
        }

        private void txNam_EditValueChanged(object sender, EventArgs e)
        {
            //if (rdo_ChonBaoCao.SelectedIndex==1)
            //{
            //    dtTuNgay.EditValue = Convert.ToDateTime(("01/01/" + txNam.Text));
            //    dtDenNgay.EditValue = Convert.ToDateTime(("30/06/" + txNam.Text));
            //}
            //if(rdo_ChonBaoCao.SelectedIndex ==2)
            //{
            //    dtTuNgay.EditValue = Convert.ToDateTime(("01/07/" + txNam.Text));
            //    dtDenNgay.EditValue = Convert.ToDateTime(("31/12/" + txNam.Text));
            //}
           
        }
    }
}
