using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInKeHoachNghiPhep : DevExpress.XtraEditors.XtraForm
    {
        public frmInKeHoachNghiPhep()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInKeHoachNghiPhep_Load(object sender, EventArgs e)
        {

            rdo_ChonBaoCao.SelectedIndex = 0;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(slkDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(slkDonVi, slkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(slkDonVi, slkXiNghiep, slkTo);
            Commons.Modules.ObjSystems.LoadCboLDV(slkLDV);
            Commons.Modules.ObjSystems.LoadCboCN(slkCN);
            //LoadCboLDV();
            dTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            dDenNgay.DateTime = DateTime.Now.Date.AddMonths(1).AddDays(-DateTime.Now.Date.Day);
            int SoNgay = DateTime.Today.Day - 1;
            dTuNgay.EditValue = DateTime.Today.AddDays(-SoNgay);
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
        }
        //private void LoadCboLDV()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
        //        Commons.Modules.ObjSystems.MLoadLookUpEdit(lkLDV, dt, "ID_LDV", "TEN_LDV", "TEN_LDV");
        //        Commons.Modules.sPrivate = "0LOAD";
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}
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
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            String tieude;
                            if (rdo_ChonBaoCao.SelectedIndex == 0)
                            {

                                tieude = Commons.Modules.ObjLanguages.GetLanguage("rptBCKeHoachNghiPhep", "lblTieuDe_DSNVNGHIPHEPTHEOKH");
                                frm.rpt = new rptBCKeHoachNghiPhep(dNgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime, tieude);

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCKeHoachNghiPhep", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = slkDonVi.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = slkXiNghiep.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = slkTo.EditValue;
                                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = slkCN.EditValue;
                                cmd.Parameters.Add("@ID_LDV", SqlDbType.BigInt).Value = slkLDV.EditValue;
                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;

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
                            else
                            {
                                tieude = Commons.Modules.ObjLanguages.GetLanguage("rptBCKeHoachNghiPhep", "lblTieuDe_DSNVDILAMLAITHEOKH");
                                frm.rpt = new rptBCKeHoachNghiPhepNgayVL(dNgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime, tieude);

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCKeHoachNghiPhep", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = slkDonVi.EditValue;
                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = slkXiNghiep.EditValue;
                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = slkTo.EditValue;
                                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = slkCN.EditValue;
                                cmd.Parameters.Add("@ID_LDV", SqlDbType.BigInt).Value = slkLDV.EditValue;
                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;

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
                            
                            break;
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

        private void slkDonVi_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(slkLDV, slkXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(slkLDV, slkXiNghiep, slkTo);
        }

        private void slkXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(slkLDV, slkXiNghiep, slkTo);
        }
    }
}