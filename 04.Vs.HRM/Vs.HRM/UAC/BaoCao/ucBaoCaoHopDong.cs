using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoHopDong()
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
                        DataTable dt;
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    System.Data.SqlClient.SqlConnection conn;
                                     dt = new DataTable();
                                    string sTieuDe = "DANH SÁCH CÔNG NHÂN HỢP ĐỒNG";
                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe, lk_NgayIn.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongCongNhan", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.DateTime;
                                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
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
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                {
                                    System.Data.SqlClient.SqlConnection conn1;
                                     dt = new DataTable();
                                    string sTieuDe1 = "DANH SÁCH CÔNG NHÂN HẾT HẠN HỢP ĐỒNG";
                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe1, lk_NgayIn.DateTime, lk_NgayIn.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongHetHan", conn1);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.DateTime;
                                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
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
                                    { }


                                    frm.ShowDialog();
                                }
                                break;
                            case 2:
                                {
                                    System.Data.SqlClient.SqlConnection conn2;
                                     dt = new DataTable();
                                    string sTieuDe2 = "DANH SÁCH CÔNG NHÂN KÝ HỢP ĐỒNG";
                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                                        cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = LK_LOAI_HD.EditValue;
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
                                    { }
                                    frm.ShowDialog();
                                }
                                break;

                                //Tai ký hợp đồng
                            case 3:
                                {
                                    System.Data.SqlClient.SqlConnection conn2;
                                    dt = new DataTable();
                                    string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES WHO ARE DUE TO RENEW THEIR LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN TÁI KÝ HĐLĐ THÁNG ";
                                    frm.rpt = new rptBCTaiKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanTaiKyHopDong", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }
                                    frm.ShowDialog();
                                }
                                break;

                                // Tới hạn ký hợp đồng
                            case 4:
                                {
                                    System.Data.SqlClient.SqlConnection conn2;
                                    dt = new DataTable();
                                    string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES DUE TO SIGN LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN KÝ HĐLĐ THÁNG ";
                                    frm.rpt = new rptBCToiHanKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn2.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanKyHopDong", conn2);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                        cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        dt = new DataTable();
                                        dt = ds.Tables[0].Copy();
                                        dt.TableName = "DATA";
                                        frm.AddDataSource(dt);
                                        frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    }
                                    catch
                                    { }
                                    frm.ShowDialog();
                                }
                                break;
                        }
                        
                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoHopDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            LoadLoaiHopDong();

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);

            lk_NgayIn.EditValue = DateTime.Today;
            lk_NgayTinh.EditValue = DateTime.Today;
            DateTime dtTN = DateTime.Today;
            DateTime dtDN = DateTime.Today;
            dTuNgay.EditValue = dtTN.AddDays((-dtTN.Day) + 1);
            dtDN = dtDN.AddMonths(1);
            dtDN = dtDN.AddDays(-(dtDN.Day));
            dDenNgay.EditValue = dtDN;
            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_DON_VI.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
                }
                else
                {
                    LK_DON_VI.Properties.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", LK_DON_VI.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if(LK_XI_NGHIEP.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
                }
                else
                {
                    LK_XI_NGHIEP.Properties.DataSource = dt;
                }
                LK_XI_NGHIEP.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if(LK_TO.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                }
                else
                {
                    LK_TO.Properties.DataSource = dt;
                }
                LK_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadLoaiHopDong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHopDongLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(LK_LOAI_HD, dt, "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD");
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 0:
                        {
                            lk_NgayTinh.Enabled = true;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                        }
                        break;
                    case 1:
                        {
                            lk_NgayTinh.Enabled = true;
                            dTuNgay.Enabled = false;
                            dDenNgay.Enabled = false;
                        }
                        break;
                    case 2:
                        {
                            lk_NgayTinh.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;
                    case 3:
                        {
                            lk_NgayTinh.Enabled = false;
                            LK_LOAI_HD.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;
                    case 4:
                        {
                            lk_NgayTinh.Enabled = false;
                            LK_LOAI_HD.Enabled = false;
                            dTuNgay.Enabled = true;
                            dDenNgay.Enabled = true;
                        }
                        break;

                    default:
                        lk_NgayTinh.Enabled = true;
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
