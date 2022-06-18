using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoTrinhDoChuyenMon : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoTrinhDoChuyenMon()
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
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    {//rptBCDanhGiaTrinhDo
                                        frm.rpt = new rptBCCNTheoTrinhDo(lk_NgayIn.DateTime);
                                        try
                                        {
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptTrinhDoChuyenMon", conn);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@ID_LOAI_TD", SqlDbType.Int).Value = cbTrinhDo.EditValue;
                                            cmd.Parameters.Add("@CHUYEN_MON", SqlDbType.NVarChar).Value = cbChuyenMon.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
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


                                    }

                                    break;
                                }
                            case 1:
                                {
                                    {
                                        frm.rpt = new rptBCCNTheoChuyenMon(lk_NgayIn.DateTime);
                                        try
                                        {
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptTrinhDoChuyenMon", conn);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@ID_LOAI_TD", SqlDbType.Int).Value = cbTrinhDo.EditValue;
                                            cmd.Parameters.Add("@CHUYEN_MON", SqlDbType.NVarChar).Value = cbChuyenMon.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
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


                                    }

                                }
                                break;
                         
                            default:
                                break;

                        }
                        frm.ShowDialog();

                    }
                    break;
                default:
                    break;
            }
        }

        private void ucBaoCaoTrinhDoChuyenMon_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbTrinhDo, Commons.Modules.ObjSystems.DataLoaiTrinhDo(true), "ID_LOAI_TD", "TEN_LOAI_TD", "TEN_LOAI_TD");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbChuyenMon, Commons.Modules.ObjSystems.DataChuyenMon(true), "CHUYEN_MON", "CHUYEN_MON2", "CHUYEN_MON2");

            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
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
                        cbTrinhDo.Enabled = true;
                        cbChuyenMon.Enabled = false;
                        cbChuyenMon.EditValue = -1;
                    }
                    break;
                case 1:
                    {

                        cbTrinhDo.Enabled = false;
                        cbChuyenMon.Enabled = true;
                        cbTrinhDo.EditValue = -1;
                    }
                    break;
            }
        }
    }
}
