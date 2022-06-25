using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBaoCaoBaoHiemTaiNan : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoBaoHiemTaiNan()
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

                        string NamBC;
                        NamBC = "";
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        switch (rdoChonBC.SelectedIndex)
                        {
                            case 0:
                                {
                                    frm.rpt = new rptDSDeNghiMuaBaoHiemTaiNan(lk_NgayIn.DateTime, Convert.ToDateTime(dtTuNgay.EditValue), Convert.ToDateTime(dtDenNgay.EditValue));
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachDeNghiMuaBHTN", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dtTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtDenNgay.EditValue;
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
                                }
                                break;
                            case 1:
                                {
                                    frm.rpt = new rptHoSoCapCuu(lk_NgayIn.DateTime, Convert.ToDateTime(dtTuNgay.EditValue), Convert.ToDateTime(dtDenNgay.EditValue));
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHoSoCapCuu", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dtTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtDenNgay.EditValue;
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
                                }
                                break;

                            case 2:
                                {
                                    frm.rpt = new rptThongKeTaiNan(lk_NgayIn.DateTime);
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptThongKeTaiNan", conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dtTuNgay.EditValue;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dtDenNgay.EditValue;
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
                                }
                                break;
                            case 3:
                                {

                                    dt = new DataTable();
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();
                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCThongKeTaiNan6Thang", conn);
                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = -1;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = -1;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = -1;
                                        cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = DateTime.Now;
                                        cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = DateTime.Now;
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                        DataSet ds = new DataSet();
                                        adp.Fill(ds);
                                        ds.Tables[0].TableName = "TaiNanLaoDongTH";
                                        ds.Tables[1].TableName = "TaiNanLaoDong";
                                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                                        saveFileDialog.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx";
                                        saveFileDialog.FilterIndex = 0;
                                        saveFileDialog.RestoreDirectory = true;
                                        saveFileDialog.CreatePrompt = true;
                                        saveFileDialog.Title = "Export Excel File To";
                                        // If the file name is not an empty string open it for saving.
                                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            if (saveFileDialog.FileName != "")
                                            {
                                                Commons.TemplateExcel.FillReport(saveFileDialog.FileName, Application.StartupPath + "\\Template\\TemplateTaiNanLaoDong.xlsx", ds, new string[] { "{", "}" });
                                                Process.Start(saveFileDialog.FileName);
                                            }
                                        }
                                    }
                                    catch 
                                    {

                                    }
                                }
                                break;
                            default: break;
                        }
                        if (rdoChonBC.SelectedIndex != 3)
                            frm.ShowDialog();

                    }
                    break;

                default:
                    break;
            }
        }

        private void ucBaoCaoBaoHiemTaiNan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            lk_NgayIn.EditValue = DateTime.Today;
            dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            txNam.Text = (DateTime.Today.Year.ToString());
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
            try
            {
                switch (rdo_ChonBaoCao.SelectedIndex)
                {
                    case 0:
                        {
                            dtTuNgay.Enabled = true;
                            dtDenNgay.Enabled = true;
                            txNam.Enabled = false;
                        }
                        break;
                   
                    case 1:
                        {
                            dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 1, 1);
                            dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 6, 30);
                            dtTuNgay.Enabled = false;
                            dtDenNgay.Enabled = false;
                            txNam.Enabled = true;
                        }
                        break;
                    case 2:
                        {
                            dtTuNgay.EditValue = new DateTime(int.Parse(txNam.Text), 7, 1);
                            dtDenNgay.EditValue = new DateTime(int.Parse(txNam.Text), 12, 31);
                            dtTuNgay.Enabled = false;
                            dtDenNgay.Enabled = false;
                            txNam.Enabled = true;
                        }
                        break;

                    default:
                        dtTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
                        dtDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
                        dtTuNgay.Enabled = true;
                        dtDenNgay.Enabled = true;
                        break;
                }
            }
            catch
            { }
        }

        private void txNam_EditValueChanged(object sender, EventArgs e)
        {
            if (rdo_ChonBaoCao.SelectedIndex==1)
            {
                dtTuNgay.EditValue = Convert.ToDateTime(("01/01/" + txNam.Text));
                dtDenNgay.EditValue = Convert.ToDateTime(("30/06/" + txNam.Text));
            }
            if(rdo_ChonBaoCao.SelectedIndex ==2)
            {
                dtTuNgay.EditValue = Convert.ToDateTime(("01/07/" + txNam.Text));
                dtDenNgay.EditValue = Convert.ToDateTime(("31/12/" + txNam.Text));
            }
           
        }
    }
}
