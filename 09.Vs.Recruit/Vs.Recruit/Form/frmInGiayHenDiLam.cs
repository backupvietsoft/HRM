using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Vs.Report;
using DataTable = System.Data.DataTable;

namespace Vs.Recruit
{
    public partial class frmInGiayHenDiLam : DevExpress.XtraEditors.XtraForm
    {
        public int MS_CV = 0;
        public DataTable dtTemp;
        public frmInGiayHenDiLam()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void frmInGiayHenDiLam_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
        }
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
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dtTTC = new DataTable(); // Lấy ký hiệu đơn vị trong thông tin chung

                        dtTTC = Commons.Modules.ObjSystems.DataThongTinChung();
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (MS_CV)
                                    {
                                        case 1:
                                            {
                                                DataTable dtbc = new DataTable();
                                                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                                try
                                                {
                                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dtTemp, "");
                                                    System.Data.SqlClient.SqlConnection conn1;
                                                    frmViewReport frm = new frmViewReport();
                                                    frm.rpt = new rptThuMoi();
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
                                                    DataTable dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DATA";
                                                    frm.AddDataSource(dt);

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
                                                break;
                                            }
                                        default:
                                            {
                                                frmViewReport frm = new frmViewReport();
                                                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dtTemp, "");
                                                System.Data.SqlClient.SqlConnection conn1;
                                                DataTable dt = new DataTable();
                                                frm.rpt = new rptGiayHenDiLam();
                                                try
                                                {
                                                    conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn1.Open();
                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptGiayHenDiLam", conn1);
                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DATA";
                                                    frm.AddDataSource(dt);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                                frm.ShowDialog();
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    InDTDH();
                                    break;
                                }
                            case 2:
                                {
                                    DataTable dtbc = new DataTable();
                                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                    try
                                    {
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, dtTemp, "");

                                    }
                                    catch { }
                                    break;
                                }
                            default:
                                break;
                        }

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
        private void InDTDH()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSUVDaoTaoDH", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@NgayDT", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgayIn.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "DaoTaoDH";
                ds.Tables[1].TableName = "info";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.Title = "Export Excel File To";
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                DialogResult res = saveFileDialog.ShowDialog();
                // If the file name is not an empty string open it for saving.
                if (res == DialogResult.OK)
                {
                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateDTDH.xlsx", ds, new string[] { "{", "}" });
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch
            {

            }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdo_ChonBaoCao.SelectedIndex == 0)
            {
                dNgayIn.Enabled = false;
            }
            else
            {
                dNgayIn.Enabled = true;
            }
        }
    }
}