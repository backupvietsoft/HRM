using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoCongNhanTheoTinhThanh : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoCongNhanTheoTinhThanh()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            string strTieuDe = "";
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();

                        switch (rdo_NguyenQuan.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (rdo_ChonBaoCao.SelectedIndex)
                                    {
                                        case 0:
                                            {
                                                {
                                                    frm.rpt = new rptBCCNTheoNguyenQuanTH(lk_NgayIn.DateTime, strTieuDe);
                                                    try
                                                    {
                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptDSCNNQuanTinhHuyen]", conn);

                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                        cmd.Parameters.Add("@NQuan", SqlDbType.NVarChar).Value = LK_NguyenQuan.EditValue;
                                                        cmd.Parameters.Add("@ID_TP", SqlDbType.Int).Value = LK_TinhThanh.EditValue;
                                                        cmd.Parameters.Add("@ID_QUAN", SqlDbType.Int).Value = LK_QuanHuyen.EditValue;
                                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                        cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 1;
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
                                                    frm.rpt = new rptBCCNTheoNguyenQuan(lk_NgayIn.DateTime, strTieuDe);
                                                    try
                                                    {
                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptDSCNNQuanTinhHuyen]", conn);

                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                        cmd.Parameters.Add("@NQuan", SqlDbType.NVarChar).Value = LK_NguyenQuan.EditValue;
                                                        cmd.Parameters.Add("@ID_TP", SqlDbType.Int).Value = LK_TinhThanh.EditValue;
                                                        cmd.Parameters.Add("@ID_QUAN", SqlDbType.Int).Value = LK_QuanHuyen.EditValue;
                                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                        cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 2;
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
                                }
                                break;
                            case 1:
                                {
                                    switch (rdo_ChonBaoCao.SelectedIndex)
                                    {
                                        case 0:
                                            {
                                                {
                                                    frm.rpt = new rptBCCNTheoTinhThanhTH(lk_NgayIn.DateTime, strTieuDe);
                                                    try
                                                    {
                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptDSCNNQuanTinhHuyen]", conn);

                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                        cmd.Parameters.Add("@NQuan", SqlDbType.NVarChar).Value = LK_NguyenQuan.EditValue;
                                                        cmd.Parameters.Add("@ID_TP", SqlDbType.Int).Value = LK_TinhThanh.EditValue;
                                                        cmd.Parameters.Add("@ID_QUAN", SqlDbType.Int).Value = LK_QuanHuyen.EditValue;
                                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                        cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 3;
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
                                                    frm.rpt = new rptBCCNTheoTinhThanh(lk_NgayIn.DateTime, strTieuDe);
                                                    try
                                                    {
                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[rptDSCNNQuanTinhHuyen]", conn);

                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                        cmd.Parameters.Add("@NQuan", SqlDbType.NVarChar).Value = LK_NguyenQuan.EditValue;
                                                        cmd.Parameters.Add("@ID_TP", SqlDbType.Int).Value = LK_TinhThanh.EditValue;
                                                        cmd.Parameters.Add("@ID_QUAN", SqlDbType.Int).Value = LK_QuanHuyen.EditValue;
                                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                        cmd.Parameters.Add("@LBCao", SqlDbType.Int).Value = 4;
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

        private void ucBaoCaoCongNhanTheoTinhThanh_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_QuocGia, Commons.Modules.ObjSystems.DataQuocGia(true), "ID_QG", "TEN_QG", "TEN_QG");
            LK_QuocGia.EditValue = 234;
            //ID_TPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TinhThanh, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(LK_QuocGia.EditValue), true), "ID_TP", "TEN_TP", "TEN_TP");

            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_QuanHuyen, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(LK_TinhThanh.EditValue), true), "ID_QUAN", "TEN_QUAN", "TEN_QUAN");
            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_PhuongXa, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(LK_QuanHuyen.EditValue), true), "ID_PX", "TEN_PX", "TEN_PX");
            //ID_TP_TAM_TRULookUpEdit 
            try
            {
                Commons.Modules.ObjSystems.LoadCboNguyenQuan(LK_NguyenQuan);
                LK_NguyenQuan.EditValue = "-1";
            }
            catch
            {

            }
            radioGroup1_SelectedIndexChanged(null, null);

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

        private void LK_QuocGia_Validated(object sender, EventArgs e)
        {

        }
        private void LK_TinhThanh_Validated(object sender, EventArgs e)
        {

        }
        private void LK_QuanHuyen_Validated(object sender, EventArgs e)
        {

        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_NguyenQuan.SelectedIndex)
            {
                case 0:
                    {
                        LK_NguyenQuan.Enabled = true;
                        LK_QuocGia.Enabled = false;
                        LK_TinhThanh.Enabled = false;
                        LK_QuanHuyen.Enabled = false;
                        LK_PhuongXa.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        LK_NguyenQuan.Enabled = false;
                        LK_QuocGia.Enabled = true;
                        LK_TinhThanh.Enabled = true;
                        LK_QuanHuyen.Enabled = true;
                        LK_PhuongXa.Enabled = true;
                    }
                    break;
                default:
                    LK_NguyenQuan.Enabled = true;
                    LK_QuocGia.Enabled = false;
                    LK_TinhThanh.Enabled = false;
                    LK_QuanHuyen.Enabled = false;
                    LK_PhuongXa.Enabled = false;
                    break;
            }
        }

        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void LK_QuocGia_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            //ID_TPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TinhThanh, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(LK_QuocGia.EditValue), true), "ID_TP", "TEN_TP", "TEN_TP");
            LK_TinhThanh.EditValue = -1;
            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_QuanHuyen, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(LK_TinhThanh.EditValue), true), "ID_QUAN", "TEN_QUAN", "TEN_QUAN");
            LK_QuanHuyen.EditValue = -1;
            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_PhuongXa, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(LK_QuanHuyen.EditValue), true), "ID_PX", "TEN_PX", "TEN_PX");
            LK_PhuongXa.EditValue = -1;
            //ID_TP_TAM_TRULookUpEdit 
        }

        private void LK_TinhThanh_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_QuanHuyen, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(LK_TinhThanh.EditValue), true), "ID_QUAN", "TEN_QUAN", "TEN_QUAN");
            LK_QuanHuyen.EditValue = -1;
            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_PhuongXa, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(LK_QuanHuyen.EditValue), true), "ID_PX", "TEN_PX", "TEN_PX");
            LK_PhuongXa.EditValue = -1;
            //ID_TP_TAM_TRULookUpEdit 
        }

        private void LK_QuanHuyen_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_PhuongXa, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(LK_QuanHuyen.EditValue), true), "ID_PX", "TEN_PX", "TEN_PX");
            LK_PhuongXa.EditValue = -1;
            //ID_TP_TAM_TRULookUpEdit
        }
    }
}