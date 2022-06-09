using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Payroll;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoDMCD : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoDMCD()
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
                        frm.rpt = new rptDanhMucCongDoan(LOAI_SP.EditValue.ToString(), CUM.EditValue.ToString());

                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhMucCongDoan", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_CUM", SqlDbType.Int).Value = CUM.EditValue;
                            cmd.Parameters.Add("@ID_LSP", SqlDbType.Int).Value = LOAI_SP.EditValue;
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

        private void ucBaoCaoDMCD_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(LOAI_SP, Commons.Modules.ObjSystems.DataLoaiSanPham(true), "ID_NHH", "TEN_NHH", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHH"));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(CUM, Commons.Modules.ObjSystems.DataCUM(Convert.ToInt32(LOAI_SP.EditValue), true), "ID_CUM", "TEN_CUM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CUM"));
        }

        private void LOAI_SP_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(CUM, Commons.Modules.ObjSystems.DataCUM(Convert.ToInt32(LOAI_SP.EditValue), true), "ID_CUM", "TEN_CUM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CUM"));

        }
    }
}
