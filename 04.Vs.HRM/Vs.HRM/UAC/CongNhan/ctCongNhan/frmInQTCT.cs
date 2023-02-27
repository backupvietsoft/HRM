using Aspose.Words;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Diagnostics;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInQTCT : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idCT;

        public frmInQTCT(Int64 idCongNhan, Int64 idCongTac, string tencn)
        {
            InitializeComponent();
            NONN_HoTenCN.Text = tencn.ToUpper();
            idCN = idCongNhan;
            idCT = idCongTac;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInQTCT_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "SB")
            {
                return;
            }
            else if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(2);
            }
            else
            {
                //rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
            }
        }

        private void dNgayIn_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tablePanel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
        private void InQuyetDinhDieuChuyen_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhDieuChuyen_MT(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhDieuChuyen", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.TableName = "NOI_DUNG";
                frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch { }
        }
        private void InQuyetDinhDieuChuyen_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhDieuChuyen_SB(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhDieuChuyen_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.TableName = "NOI_DUNG";
                frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch { }
        }
        private void InQuyetDinhDieuChuyen_NB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhDieuChuyen_NB(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhDieuChuyen_NB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.TableName = "NOI_DUNG";
                frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch { }
        }

        private void InQuyetDinhDieuChuyen_AP()
        {
            #region in củ
            //try
            //{
            //    System.Data.SqlClient.SqlConnection conn;
            //    DataTable dt = new DataTable();
            //    frmViewReport frm = new frmViewReport();
            //    frm.rpt = new rptQuyetDinhDieuChuyen_AP(dNgayIn.DateTime);

            //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //    conn.Open();

            //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhDieuChuyen_AP", conn);
            //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            //    cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
            //    cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adp.Fill(ds);
            //    dt = new DataTable();
            //    dt = ds.Tables[0].Copy();
            //    dt.TableName = "DATA";
            //    frm.AddDataSource(dt);

            //    //DataTable dt1 = new DataTable();
            //    //dt1 = ds.Tables[1].Copy();
            //    //dt1.TableName = "NOI_DUNG";
            //    //frm.AddDataSource(dt1);

            //    frm.ShowDialog();
            //}
            //catch { }
            #endregion
            #region in mới
            try
            {
                //lấy data dữ liệu
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptQuyetDinhDieuChuyen_AP", Commons.Modules.UserName, Commons.Modules.TypeLanguage, idCN, idCT,1));
                DataRow row = dt.Rows[0];
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateAP\\QuyetDinhDieuChuyenCT.doc");
                baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { ".................................." });

                        continue;
                    }
                    switch (item.DataType.Name)
                    {
                        case "DateTime":
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { Convert.ToDateTime(row[item]).ToString("dd/MM/yyyy") });
                                break;
                            }
                        case "Double":
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { string.Format("{0:#,##0}", row[item]) });
                                break;
                            }
                        default:
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { row[item] });
                                break;

                            }
                    }
                }
                baoCao.Save(sPath);
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
            }
            #endregion
        }
        private void InQuyetDinhBoNhiem_AP()
        {
            
            #region in mới
            try
            {
                //lấy data dữ liệu
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptQuyetDinhDieuChuyen_AP", Commons.Modules.UserName, Commons.Modules.TypeLanguage, idCN, idCT,2));
                DataRow row = dt.Rows[0];
                string sPath = "";
                sPath = Commons.Modules.MExcel.SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateAP\\QuyetDinhBoNhiem.doc");
                baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { ".................................." });

                        continue;
                    }
                    switch (item.DataType.Name)
                    {
                        case "DateTime":
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { Convert.ToDateTime(row[item]).ToString("dd/MM/yyyy") });
                                break;
                            }
                        case "Double":
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { string.Format("{0:#,##0}", row[item]) });
                                break;
                            }
                        default:
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { row[item] });
                                break;

                            }
                    }
                }
                baoCao.Save(sPath);
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
            }
            #endregion
        }


        private void InQuyetDinhDieuChuyen_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhDieuChuyen_DM(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhDieuChuyen_DM", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.TableName = "NOI_DUNG";
                frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch { }
        }
        private void InQuyetDinhTuyenDung_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhTuyenDung_SB(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhTuyenDung", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                frm.ShowDialog();
            }
            catch { }
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        int n = rdo_ChonBaoCao.SelectedIndex;
                        if (rdo_ChonBaoCao.Properties.Items.Count < 3)
                        {
                            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() != "DM")
                            {
                                n = (n >= 1 ? n + 1 : n);
                            }
                        }
                        switch (n)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                InQuyetDinhDieuChuyen_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                InQuyetDinhDieuChuyen_SB();
                                                break;
                                            }
                                        case "DM":
                                            {
                                                InQuyetDinhDieuChuyen_DM();
                                                break;
                                            }
                                        case "NB":
                                            {
                                                InQuyetDinhDieuChuyen_NB();
                                                break;
                                            }
                                        case "NC":
                                            {
                                                InQuyetDinhDieuChuyen_NB();
                                                break;
                                            }
                                        case "AP":
                                            {
                                                InQuyetDinhDieuChuyen_AP();
                                                break;
                                            }
                                        default:
                                            InQuyetDinhDieuChuyen_MT();
                                            break;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                InQuyetDinhTuyenDung_SB();
                                                break;
                                            }
                                        case "AP":
                                            {
                                                InQuyetDinhBoNhiem_AP();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                InQuyetDinhTuyenDung_SB();
                                                break;
                                            }
                                        case "DM":
                                            {
                                                InQuyetDinhBoNhiem_DM();
                                                break;
                                            }
                                        default:
                                            InQuyetDinhTuyenDung_SB();
                                            break;
                                    }

                                    break;
                                }
                            case 2:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(idCN))
                                    {
                                        case "NB":
                                            {
                                                break;
                                            }
                                        default:
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frmViewReport frm = new frmViewReport();
                                                frm.rpt = new rptBCQuaTrinhCongTacCN(dNgayIn.DateTime);

                                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn.Open();

                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuaTrinhCongTacCN", conn);
                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                dt = new DataTable();
                                                dt = ds.Tables[0].Copy();
                                                dt.TableName = "DA_TA";
                                                frm.AddDataSource(dt);

                                                frm.ShowDialog();
                                                break;
                                            }
                                    }

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
        private void InQuyetDinhBoNhiem_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhBoNhiem_DM(dNgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhBoNhiem_DM", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idCT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

                //DataTable dt1 = new DataTable();
                //dt1 = ds.Tables[1].Copy();
                //dt1.TableName = "NOI_DUNG";
                //frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch { }
        }
    }
}