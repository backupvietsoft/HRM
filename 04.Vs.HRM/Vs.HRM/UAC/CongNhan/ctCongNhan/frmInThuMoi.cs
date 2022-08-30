using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInThuMoi : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idCT;
        DataTable dt = new DataTable();
        public frmInThuMoi(DataTable dt1)
        {
            InitializeComponent();
            dt = dt1;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void frmInThuMoi_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
        }

        private void dNgayIn_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tablePanel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    HopDongThuViecAll_DM(dt);
                                    break;
                                }
                            case 1:
                                {
                                    ThuMoi_DM(dt);
                                    break;
                                }
                            default:
                                {

                                    break;
                                }
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }
        private void HopDongThuViecAll_DM(DataTable dtTemp)
        {
            DataTable dtbc = new DataTable();
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dt, "");

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_DM(dNgayIn.DateTime);
                dtTemp = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_All", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtTemp = new DataTable();
                dtTemp = ds.Tables[0].Copy();
                dtTemp.TableName = "DATA";
                frm.AddDataSource(dtTemp);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);

                frm.ShowDialog();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
            }
        }
        private void ThuMoi_DM(DataTable dtTemp)
        {
            DataTable dtbc = new DataTable();
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dtTemp, "");

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptThuMoi();
                dtTemp = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptThuMoi", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtTemp = new DataTable();
                dtTemp = ds.Tables[0].Copy();
                dtTemp.TableName = "DATA";
                frm.AddDataSource(dtTemp);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);

                frm.ShowDialog();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
            }
        }
    }
}