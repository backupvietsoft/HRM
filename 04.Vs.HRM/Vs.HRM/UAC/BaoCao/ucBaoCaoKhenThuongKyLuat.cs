using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using System.Globalization;

namespace Vs.HRM
{
    public partial class ucBaoCaoKhenThuongKyLuat : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoKhenThuongKyLuat()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
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
                                    frm.rpt = new rptBCKhenThuongKyLuat(lk_NgayIn.DateTime);
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatCN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

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
                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptBCKhenThuongKyLuatBP(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKhenThuongKyLuatTH", conn1);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

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
                            case 2:
                                {
                                    System.Data.SqlClient.SqlConnection conn1;
                                    DataTable dt = new DataTable();
                                    frm.rpt = new rptBCKhenThuongKyLuatTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);

                                    try
                                    {
                                        conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn1.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptKhenThuongKyLuatTH]", conn1);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = cbCongNhan.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                        cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = cbKhenThuongKyLuat.SelectedIndex;

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
                                break;
                        }
                        break;

                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoKhenThuongKyLuat_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);


            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE"));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year), new CultureInfo("de-DE")).AddMonths(1).AddDays(-1);
            dtThang.EditValue = DateTime.Today;
            Commons.Modules.sLoad = "";
            LoadNhanSu();
            lk_NgayIn.EditValue = DateTime.Today;
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
        private void LoadNhanSu()
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dt = Commons.Modules.ObjSystems.DataCongNhanTheoDK(true, Convert.ToInt32(LK_DON_VI.EditValue), Convert.ToInt32(LK_XI_NGHIEP.EditValue), Convert.ToInt32(LK_TO.EditValue), Convert.ToDateTime(dTuNgay.EditValue), Convert.ToDateTime(dDenNgay.EditValue));
                if (cbCongNhan.Properties.DataSource == null)
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

        private void dtThang_EditValueChanged(object sender, EventArgs e)
        {
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + dtThang.Text), new CultureInfo("de-DE"));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + dtThang.Text), new CultureInfo("de-DE")).AddMonths(1).AddDays(-1);
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadNhanSu();
        }
    }
}
