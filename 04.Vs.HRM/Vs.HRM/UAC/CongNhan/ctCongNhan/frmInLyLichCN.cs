using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInLyLichCN : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;

        public frmInLyLichCN(Int64 idCongNhan)
        {
            InitializeComponent();
            idCN = idCongNhan;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
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

                        int n = rdo_ChonBaoCao.SelectedIndex;
                        switch (n)
                        {
                            case 0:
                                {
                                    InSoYeuLyLich();
                                }
                                break;
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "DM":
                                            {
                                                DataColumn dtC;
                                                DataRow dtR;
                                                DataTable dt = new DataTable();

                                                dt = new DataTable();
                                                dtC = new DataColumn();
                                                dtC.DataType = typeof(Int64);
                                                dtC.ColumnName = "ID_CN";
                                                dtC.Caption = "ID_CN";
                                                dtC.ReadOnly = false;
                                                dt.Columns.Add(dtC);

                                                dtC = new DataColumn();
                                                dtC.DataType = typeof(bool);
                                                dtC.ColumnName = "CHON";
                                                dtC.Caption = "CHON";
                                                dtC.ReadOnly = false;
                                                dt.Columns.Add(dtC);


                                                dtR = dt.NewRow();
                                                dtR["ID_CN"] = Commons.Modules.iCongNhan;
                                                dtR["CHON"] = true;
                                                dt.Rows.Add(dtR);


                                                //tạo một datatable 
                                                string strSaveThongTinNhanVien = "strSaveThongTinNhanVien" + Commons.Modules.UserName;

                                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, strSaveThongTinNhanVien, dt, "");

                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dtbc = new DataTable();
                                                frmViewReport frm = new frmViewReport();
                                                //frm.rpt = new rptTheNhanVien_DM(dt);


                                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn.Open();

                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSaveThongTinNhanVienDM", conn);
                                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = strSaveThongTinNhanVien;
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);

                                                DataTable dt1 = new DataTable();
                                                dt1 = ds.Tables[1].Copy();
                                                dt1.TableName = "DATA";
                                                frm.rpt = new Vs.Recruit.rptInTheNV_DM(dt1);
                                                frm.AddDataSource(dt1);

                                                dt = new DataTable();
                                                dt = ds.Tables[0].Copy();
                                                dt.TableName = "DATA1";
                                                frm.AddDataSource(dt);




                                                DataTable dt2 = new DataTable();
                                                dt2 = ds.Tables[2].Copy();
                                                dt2.TableName = "DATA2";
                                                frm.AddDataSource(dt2);

                                                frm.ShowDialog();

                                                Commons.Modules.ObjSystems.XoaTable(strSaveThongTinNhanVien);
                                                conn.Close();
                                                break;
                                            }
                                        case "NB":
                                            {
                                                InDanhGiaKetQuaThuViecNhanVien_NB();
                                                break;
                                            }
                                        default:
                                            break;
                                    }

                                }
                                break;
                            case 2:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {
                                                break;
                                            }
                                        case "SB":
                                            {
                                                break;
                                            }
                                        case "NB":
                                            {
                                               InPhieuDanhGiaKetQuaThuViecCongNhan_NB();
                                                break;
                                            }
                                    }

                                }
                                break;
                            case 3:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {

                                                break;
                                            }

                                        case "SB":
                                            {

                                                break;
                                            }
                                        case "NB":
                                            {
                                                InDanhGiaKetQuaQuaTrinhLamViec_NB();
                                                break;
                                            }
                                    }

                                }
                                break;

                            case 4:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {

                                                break;
                                            }

                                        case "SB":
                                            {

                                                break;
                                            }
                                        case "NB":
                                            {
                                                InPhieuDanhGiaKetQuaQuaTrinhLamViecCongNhan_NB();
                                                break;
                                            }
                                    }
                                }
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

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void formInLyLich_Load(object sender, EventArgs e)
        { 
            switch(Commons.Modules.KyHieuDV)
            {
                case "DM":
                    {
                        rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
                        rdo_ChonBaoCao.Properties.Items.RemoveAt(3);
                        rdo_ChonBaoCao.Properties.Items.RemoveAt(2);
                        break;
                    }
                case "NB":
                    {
                        rdo_ChonBaoCao.Properties.Items.RemoveAt(2);
                        break;
                    }
            }
                
            //if (Commons.Modules.KyHieuDV == "DM")
            //{
            //    rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
            //    rdo_ChonBaoCao.Properties.Items.RemoveAt(3);
            //    rdo_ChonBaoCao.Properties.Items.RemoveAt(2);
            //}
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            Commons.Modules.sLoad = "";
        }
        private void InSoYeuLyLich()
        {
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptSoYeuLyLich(DateTime.Now);
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptSoYeuLyLich", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
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
        }
        private void InDanhGiaKetQuaThuViecNhanVien_NB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhGiaKetQuaThuViec_NB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@IDCN", SqlDbType.Int).Value = Commons.Modules.iCongNhan;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "DanhGiaKetQuaThuViec";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                saveFileDialog.Title = "Export Excel File To";
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0].Copy();
                        dt1.Columns.Count.ToString();
                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\KQ_DGTVNV.xlsx", ds, new string[] { "{", "}" });
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception EX
            )
            {

            }
        }

        private void InPhieuDanhGiaKetQuaThuViecCongNhan_NB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuDanhGiaKetQuaThuViec_NB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@IDCN", SqlDbType.Int).Value = Commons.Modules.iCongNhan;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "PhieuDanhGiaKetQuaThuViec";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                saveFileDialog.Title = "Export Excel File To";
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0].Copy();
                        dt1.Columns.Count.ToString();
                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\KQ_DGTVCN.xlsx", ds, new string[] { "{", "}" });
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception EX
            )
            {

            }
        }
        private void InDanhGiaKetQuaQuaTrinhLamViec_NB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhGiaKetQuaQuaTrinhLamViec_NB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@IDCN", SqlDbType.Int).Value = Commons.Modules.iCongNhan;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "DanhGiaKetQuaQuaTrinhLamViec";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                saveFileDialog.Title = "Export Excel File To";
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0].Copy();
                        dt1.Columns.Count.ToString();
                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\KQ_DGKQLVNV.xlsx", ds, new string[] { "{", "}" });
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception EX
            )
            {

            }
        }
        private void InPhieuDanhGiaKetQuaQuaTrinhLamViecCongNhan_NB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhieuDanhGiaKetQuaQuaTrinhLamViec_NB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@IDCN", SqlDbType.Int).Value = Commons.Modules.iCongNhan;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "PhieuDanhGiaKetQuaQuaTrinhLamViec";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                //saveFileDialog.CreatePrompt = true;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = false;
                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                saveFileDialog.Title = "Export Excel File To";
                // If the file name is not an empty string open it for saving.
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0].Copy();
                        dt1.Columns.Count.ToString();
                        Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\KQ_DGKQLVCN.xlsx", ds, new string[] { "{", "}" });
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception EX
            )
            {

            }
        }
    }
}