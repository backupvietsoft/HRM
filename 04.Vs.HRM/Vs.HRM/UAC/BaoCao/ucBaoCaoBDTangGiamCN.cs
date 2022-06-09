using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoBDTangGiamCN : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoBDTangGiamCN()
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

                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuDoTyLeTangGiamLaoDongNam", conn);
                            cmd.Parameters.Add("@DV", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                            cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToInt32(datNam.DateTime.Year);
                            cmd.CommandType = CommandType.StoredProcedure;

                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);

                            DataTable dt1 = new DataTable();
                            dt1 = ds.Tables[1].Copy();

                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);
                            frm.rpt = new VS.Report.NhanSu.rptBieuDoTangGiamCongNhan(dt1,datNam.DateTime.Year);
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

        private void ucBaoCaoBDTangGiamCN_Load(object sender, EventArgs e)
        {
            try
            {
                datNam.DateTime = DateTime.Now.AddYears(-1);
                DataTable dt = new DataTable();
                Commons.Modules.UserName = "admin";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
            }
            catch
            {

            }
           
        }
    }
}
