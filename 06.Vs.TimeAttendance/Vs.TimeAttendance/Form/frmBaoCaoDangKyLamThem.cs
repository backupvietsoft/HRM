using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace Vs.TimeAttendance
{
    public partial class frmBaoCaoDangKyLamThem : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idHD;
        public DateTime dNgayDL;
        public Int64 ID_DV = -1;
        public Int64 ID_XN = -1;
        public Int64 ID_TO = -1;
        public string sTenXN = "";

        public frmBaoCaoDangKyLamThem()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void frmBaoCaoDangKyLamThem_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;

            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            Commons.Modules.sLoad = "";
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
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_BieuMauLamThemGio":
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "DM":
                                            {
                                                BieuMauDangKyLamThemGio_DM();
                                                break;
                                            }
                                        default:
                                            BieuMauDangKyLamThemGio();
                                            break;
                                    }
                                    break;
                                }
                            case "rdo_BaoCaoNhanSuNgay":
                                {
                                    DataTable dt = new DataTable();
                                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptBCDKTangCa", dNgayDL.ToString("yyyyMMdd"),
                                                                    ID_DV, ID_XN, ID_TO, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                                    frmViewReport frm = new frmViewReport();
                                    //Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"))
                                    string tieuDe = "DANH SÁCH NHÂN VIÊN ĐĂNG KÍ TĂNG CA";
                                    frm.rpt = new rptDKTangCa(dNgayDL, dNgayDL, tieuDe, Convert.ToInt32(ID_DV));
                                    if (dt == null || dt.Rows.Count == 0) return;
                                    dt.TableName = "DATA";
                                    frm.AddDataSource(dt);
                                    frm.ShowDialog();
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
                default:
                    break;
            }
        }

        private void BieuMauDangKyLamThemGio()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDKLamThemGio(dNgayIn.DateTime, sTenXN, Convert.ToInt32(ID_DV));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuMauDangKyLamThemGio", conn);

                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = ID_DV;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = ID_XN;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = ID_TO;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dNgayIn;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
            }
            catch (Exception ex)
            { }
            frm.ShowDialog();
        }

        private void BieuMauDangKyLamThemGio_DM()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDKLamThemGio_DM(dNgayIn.DateTime, sTenXN);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuMauDangKyLamThemGio_DM", conn);

                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = ID_DV;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = ID_XN;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = ID_TO;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dNgayIn.DateTime;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
            }
            catch
            { }
            frm.ShowDialog();
        }
    }
}