using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoLaoDongTinh : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoLaoDongTinh()
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
                        frm.rpt = new rptBCLaoDongTinh(lk_NgayIn.DateTime);
                        //frm.rpt = new rptDSCongNhan(lk_NgayIn.DateTime);

                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongTinh", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                            cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = ChkTinh.EditValue;
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
                default:
                    break;
            }
        }

        private void ucBaoCaoLaoDongTinh_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP,LK_TO);
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

        private void LK_DON_VI_EditValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
