using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoLaoDongThang : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoLaoDongThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.OSystems.SetDateEditFormat(NgayIn);
            dtThang.EditValue = DateTime.Now;
            NgayIn.EditValue = DateTime.Today;
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                       

                        DateTime dtTN = new DateTime(dtThang.DateTime.Year, dtThang.DateTime.Month, 1);
                        DateTime dtDN = dtTN.AddMonths(1);
                        dtDN = dtDN.AddDays(-1);

                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        frm.rpt = new rptBCLaoDongThang(dtTN);

                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopLaoDongThang", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dtTN;
                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtDN;
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
    }
}