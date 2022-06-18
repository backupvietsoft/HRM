using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoQHGD : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoQHGD()
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
                        frm.rpt = new rptDSGiaDinh(lk_NgayIn.DateTime);
                        
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSGiaDinh", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = rdo_ConCongNhan.SelectedIndex;
                            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                            cmd.Parameters.Add("@TuoiTu", SqlDbType.Int).Value = txt_Tu.Text.ToString() == "" ? 0 : txt_Tu.EditValue;
                            cmd.Parameters.Add("@TuoiDen", SqlDbType.Int).Value = txt_Den.Text.ToString() == "" ? 99 : txt_Den.EditValue; 
                            cmd.Parameters.Add("@LoaiQH", SqlDbType.Int).Value = lk_QuanHeGD.EditValue;
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

        private void ucBaoCaoQHGD_Load(object sender, EventArgs e)
        {
            rdo_ConCongNhan.SelectedIndex = 0;
            lk_NgayTinh.EditValue = DateTime.Today;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.Modules.ObjSystems.LoadCboQHGD(lk_QuanHeGD);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
        }

        private void rdo_ConCongNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ConCongNhan.SelectedIndex)
            {
                case 0:
                    {
                        lk_NgayTinh.Enabled = true;
                        txt_Tu.Enabled = false;
                        txt_Den.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        lk_NgayTinh.Enabled = false;
                        txt_Tu.Enabled = true;
                        txt_Den.Enabled = true;
                    }
                    break;
               
                default:
                    lk_NgayTinh.Enabled = true;
                    txt_Tu.Enabled = false;
                    txt_Den.Enabled = false;
                    break;
            }
        }
    }
}
